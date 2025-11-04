using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tienda_Restaurante.DTOs;
using Tienda_Restaurante.Models;
using Tienda_Restaurante.Views.Shared;

namespace Tienda_Restaurante.Controllers
{
    public class PlatilloController : Controller
    {

        private readonly IPlatilloRepository _platilloRepo;
        private readonly ICategoriaRepository _categoriaRepo;
        private readonly IFileService _fileService;

        public PlatilloController(IPlatilloRepository platilloRepo, ICategoriaRepository categoriaRepo, IFileService fileService)
        {
            _platilloRepo = platilloRepo;
            _categoriaRepo = categoriaRepo;
            _fileService = fileService;
        }

        public async Task<IActionResult> Platillo()
        {
            var platillos = await _platilloRepo.GetPlatillos();
            return View(platillos);
        }

        public async Task<IActionResult> AddPlatillo()
        {
            var categoriaSelectList = (await _categoriaRepo.GetCategoria()).Select(categoria => new SelectListItem
            {
                Text = categoria.CategoriaName,
                Value = categoria.Id.ToString(),
            });
            PlatilloDTO platilloToAdd = new() { CategoriaList = categoriaSelectList };
            return View(platilloToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlatillo(PlatilloDTO platilloToAdd)
        {
            var categoriaSelectList = (await _categoriaRepo.GetCategoria()).Select(categoria => new SelectListItem
            {
                Text = categoria.CategoriaName,
                Value = categoria.Id.ToString(),
            });
            platilloToAdd.CategoriaList = categoriaSelectList;

            if (!ModelState.IsValid)
                return View(platilloToAdd);

            try
            {
                if (platilloToAdd.ImageFile != null)
                {
                    if (platilloToAdd.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("La imagen no debe exceder 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(platilloToAdd.ImageFile, allowedExtensions);
                    platilloToAdd.ImageURL = imageName;
                }
                Platillo platillo = new()
                {
                    Id = platilloToAdd.Id,
                    PlatilloName = platilloToAdd.PlatilloName,
                    CategoriaNombre = platilloToAdd.CategoriaName,
                    ImagenUrl = platilloToAdd.ImageURL,
                    Precio = platilloToAdd.Precio
                };
                await _platilloRepo.AddPlatillo(platillo);
                TempData["successMessage"] = "Platillo añadido exitosamente";
                return RedirectToAction(nameof(AddPlatillo));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(platilloToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(platilloToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error al guardar";
                return View(platilloToAdd);
            }
        }

        public async Task<IActionResult> UpdatePlatillo(int id)
        {
            var platillo = await _platilloRepo.GetPlatilloById(id);
            if (platillo == null)
            {
                TempData["errorMessage"] = $"Platillo con ID {id} no encontrado";
                return RedirectToAction(nameof(Index));
            }

            var categorias = (await _categoriaRepo.GetCategoria()).Select(c => new SelectListItem
            {
                Text = c.CategoriaName,
                Value = c.Id.ToString(),
                Selected = c.Id == platillo.CategoriaId
            });

            PlatilloDTO dto = new()
            {
                Id = platillo.Id,
                PlatilloName = platillo.PlatilloName,
                CategoriaId = platillo.CategoriaId,
                Precio = platillo.Precio,
                ImageURL = platillo.ImagenUrl,
                CategoriaList = categorias
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlatillo(PlatilloDTO dto)
        {
            dto.CategoriaList = (await _categoriaRepo.GetCategoria()).Select(c => new SelectListItem
            {
                Text = c.CategoriaName,
                Value = c.Id.ToString(),
                Selected = c.Id == dto.CategoriaId
            });

            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var existing = await _platilloRepo.GetPlatilloById(dto.Id);
                if (existing == null)
                {
                    TempData["errorMessage"] = "Platillo no encontrado";
                    return RedirectToAction(nameof(Platillo));
                }

                if (dto.ImageFile != null)
                {
                    if (dto.ImageFile.Length > 1 * 1024 * 1024)
                        throw new InvalidOperationException("La imagen no debe exceder 1 MB");

                    string[] allowed = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(dto.ImageFile, allowed);

                    if (!string.IsNullOrWhiteSpace(existing.ImagenUrl))
                        _fileService.DeleteFile(existing.ImagenUrl);

                    existing.ImagenUrl = imageName;
                }

                existing.PlatilloName = dto.PlatilloName;
                existing.CategoriaId = dto.CategoriaId;
                existing.Precio = dto.Precio;

                await _platilloRepo.UpdatePlatillo(existing);
                TempData["successMessage"] = "Platillo actualizado exitosamente";
                return RedirectToAction(nameof(Platillo));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error al actualizar";
                return View(dto);
            }
        }

        public async Task<IActionResult> DeletePlatillo(int id)
        {
            try
            {
                var platillo = await _platilloRepo.GetPlatilloById(id);
                if (platillo == null)
                {
                    TempData["errorMessage"] = $"Platillo con el id: {id} no ha sido encontrado";
                }
                else
                {
                    await _platilloRepo.DeletePlatillo(platillo);
                    if (!string.IsNullOrWhiteSpace(platillo.ImagenUrl))
                    {
                        _fileService.DeleteFile(platillo.ImagenUrl);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error al eliminar el platillo";
            }
            return RedirectToAction(nameof(Platillo));
        }
    }
}

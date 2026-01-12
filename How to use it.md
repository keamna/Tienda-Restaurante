This project is a restaurant management system.
The MVC client consumes a Web API and includes Stripe test payments and email confirmation.

Roles:
- Admin: Full access (manage products, categories, reservations).
- User: Can browse products, make reservations, and place orders.

Roles and actions:

- Admin:
  - Log in to the system.
  - Access the dashboard.
  - Manage inventory.
  - View products.
  - View orders and reservations.

- User:
  - Register an account.
  - Browse products.
  - Make orders.
  - Make reservations.
  - Complete payments.
  - Receive email confirmation for orders and reservations.

How to test:
1. Log in using the Admin credentials to explore admin features.
2. Register a new user account to test User features.
3. Create and manage products, orders, and reservations.
4. Use Stripe test card to simulate a payment.

------------------------
Admin credentials:
Email: admin5@gmail.com
Password: Admin12345*
------------------------

Stripe Test Mode:
- Payments are in TEST mode.
- No real charges are made.

----------------------------------
Stripe test card:
Card number: 4242 4242 4242 4242
Expiration date: 09/26
CVC: 123
----------------------------------

Authentication:
- The API uses JWT-based authentication.

Email confirmation:
- Email confirmation is handled in the MVC application.

Note:
This project is for educational/demo purposes.

Below are ideas for the initial build schema. This is subject to change as the project progresses.

 
- - customers
  - id (integer, primary key)
  - name (varchar)
  - type (varchar) [individual/company]
  - email (varchar)
  - phone (varchar)
  - address (varchar)
  - notes (text)

- projects
  - id (integer, primary key)
  - customer_id (integer, foreign key)
  - name (varchar)
  - description (text)
  - status (varchar)
  - start_date (date)
  - end_date (date)

- invoices
  - id (integer, primary key)
  - customer_id (integer, foreign key)
  - project_id (integer, foreign key)
  - status (varchar)
  - date (date)
  - due_date (date)
  - line_items (jsonb)
  - subtotal (numeric)
  - tax (numeric)
  - total_amount (numeric)

- proposals
  - id (integer, primary key)
  - customer_id (integer, foreign key)
  - project_id (integer, foreign key)
  - status (varchar)
  - date (date)
  - line_items (jsonb)
  - subtotal (numeric)
  - tax (numeric)
  - total_amount (numeric)

- revenues
  - id (integer, primary key)
  - project_id (integer, foreign key)
  - invoice_id (integer, foreign key)
  - amount_received (numeric)
  - date_received (date)

- expenses
  - id (integer, primary key)
  - project_id (integer, foreign key)
  - category (varchar)
  - description (varchar)
  - amount_spent (numeric)
  - date_spent (date)

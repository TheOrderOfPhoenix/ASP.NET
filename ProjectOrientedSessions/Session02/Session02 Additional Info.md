## Cardinality
### Cardinality in Database Relationships

Cardinality in databases refers to the number of relationships between records in two tables. It defines how many instances of one entity can be associated with instances of another entity. Cardinality is a crucial concept in database design because it ensures data integrity and optimizes query performance.

---

### **Types of Cardinality**

1. **One-to-One (1:1)**
    
    - Each record in Table A is related to exactly one record in Table B, and vice versa.
        
    - Example: A _person_ has one _passport_, and a _passport_ belongs to only one _person_.
        
    - Implementation: Typically enforced with a **unique foreign key**.
        
2. **One-to-Many (1:M)**
    
    - A record in Table A can have multiple related records in Table B, but a record in Table B is linked to only one record in Table A.
        
    - Example: A _customer_ can place multiple _orders_, but each _order_ is placed by only one _customer_.
        
    - Implementation: A **foreign key** in Table B referring to the primary key in Table A.
        
3. **Many-to-Many (M:M)**
    
    - Multiple records in Table A can relate to multiple records in Table B.
        
    - Example: _Students_ enroll in multiple _courses_, and each _course_ has multiple _students_.
        
    - Implementation: A **junction (bridge) table** with foreign keys referencing both tables.
        

---

### **Cardinality Constraints**

Cardinality can be further specified using **minimum and maximum** constraints:

- **(0,1): Optional One** → A record may or may not be related.
    
- **(1,1): Mandatory One** → A record must always be related to exactly one record.
    
- **(0,N): Optional Many** → A record may have many related records or none.
    
- **(1,N): Mandatory Many** → A record must have at least one related record.
    

---

### **Practical Example**

Consider a database with `Students` and `Courses`:

- **One-to-Many:** A _teacher_ teaches multiple _courses_, but each _course_ has only one _teacher_.
    
- **Many-to-Many:** _Students_ enroll in multiple _courses_, and _courses_ have multiple _students_. This is implemented using a **StudentCourses** junction table.
    

Would you like a more detailed example or SQL implementation? 🚀


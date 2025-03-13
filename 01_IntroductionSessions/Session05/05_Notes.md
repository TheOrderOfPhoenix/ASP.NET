

# Part 1: Interfaces


Stack Overflow:
https://stackoverflow.com/questions/2866987/what-is-the-definition-of-interface-in-object-oriented-programming

An interface is one of the more overloaded and confusing terms in development.

It is actually a concept of abstraction and encapsulation. For a given "box", it _declares_ the "inputs" and "outputs" of that box. In the world of software, that usually means the operations that can be invoked on the box (along with arguments) and in some cases the return types of these operations.

What it does not do is define what the semantics of these operations are, although it is commonplace (and very good practice) to document them in proximity to the declaration (e.g., via comments), or to pick good naming conventions. Nevertheless, there are no guarantees that these intentions would be followed.

Here is an analogy: Take a look at your television when it is off. Its interface are the buttons it has, the various plugs, and the screen. Its semantics and behavior are that it takes inputs (e.g., cable programming) and has outputs (display on the screen, sound, etc.). However, when you look at a TV that is not plugged in, you are projecting your expected semantics into an interface. For all you know, the TV could just explode when you plug it in. However, based on its "interface" you can assume that it won't make any coffee since it doesn't have a water intake.

In object oriented programming, an interface generally defines the set of methods (or messages) that an instance of a class that has that interface could respond to.

What adds to the confusion is that in some languages, like Java, there is an actual interface with its language specific semantics. In Java, for example, it is a set of method declarations, with no implementation, but an interface also corresponds to a type and obeys various typing rules.

In other languages, like C++, you do not have interfaces. A class itself defines methods, but you could think of the interface of the class as the declarations of the non-private methods. Because of how C++ compiles, you get header files where you could have the "interface" of the class without actual implementation. You could also mimic Java interfaces with abstract classes with pure virtual functions, etc.

An interface is most certainly not a blueprint for a class. A blueprint, by one definition is a "detailed plan of action". An interface promises nothing about an action! The source of the confusion is that in most languages, if you have an interface type that defines a set of methods, the class that implements it "repeats" the same methods (but provides definition), so the interface looks like a skeleton or an outline of the class.




Like a class, **_Interface_** can have methods, properties, events, and indexers as its members. But interfaces will contain only the declaration of the members. The implementation of the interface’s members will be given by class who implements the interface implicitly or explicitly.

- Interfaces specify what a class must do and not how.
- Interfaces can’t have private members.
- By default all the members of Interface are public and abstract.
- The interface will always defined with the help of keyword ‘**_interface_**‘.
- Interface cannot contain fields because they represent a particular implementation of data.
- _Multiple inheritance_ is possible with the help of Interfaces but not with classes.

**Syntax for Interface Declaration:**

interface  <interface_name >
{
    // declare Events
    // declare indexers
    // declare methods 
    // declare properties
}

**Syntax for Implementing Interface:**

class class_name : interface_name

To declare an interface, use _interface_ keyword. It is used to provide total abstraction. That means all the members in the interface are declared with the empty body and are public and abstract by default. A class that implements interface must implement all the methods declared in the interface.

- **Example 1:**
```c#
// C# program to demonstrate working of 
// interface 
using System; 

// A simple interface 
interface IDisplayer 
{ 
	// method having only declaration 
	// not definition 
	void display(); 
} 

// A class that implements interface. 
class testClass : IDisplayer , 
{ 
	
	// providing the body part of function 
	public void display() 
	{ 
		Console.WriteLine("Sudo Placement GeeksforGeeks"); 
	} 

	// Main Method 
	public static void Main (String []args) 
	{ 
		// Creating object 
		testClass t = new testClass(); 	
		// calling method 
		t.display(); 
	} 
} 

```

- **Example 2:**
```c#
// C# program to illustrate the interface 
using System; 

// interface declaration 
interface IVehicle { 
	
	// all are the abstract methods. 
	void changeGear(int a); 
	void speedUp(int a); 
	void applyBrakes(int a); 
} 

// class implements interface 
class Bicycle : IVehicle{ 
	
	int speed; 
	int gear; 
	
	// to change gear 
	public void changeGear(int newGear) 
	{ 
		
		gear = newGear; 
	} 
	
	// to increase speed 
	public void speedUp(int increment) 
	{ 
		
		speed = speed + increment; 
	} 
	
	// to decrease speed 
	public void applyBrakes(int decrement) 
	{ 
		
		speed = speed - decrement; 
	} 
	
	public void printStates() 
	{ 
		Console.WriteLine("speed: " + speed + 
						" gear: " + gear); 
	} 
} 

// class implements interface 
class Bike : IVehicle { 
	
	int speed; 
	int gear; 
	
	// to change gear 
	public void changeGear(int newGear) 
	{ 
		
		gear = newGear; 
	} 
	
	// to increase speed 
	public void speedUp(int increment) 
	{ 
		speed = speed + increment; 
	} 
	
	// to decrease speed 
	public void applyBrakes(int decrement){ 
		
		speed = speed - decrement; 
	} 
	
	public void printStates() 
	{ 
		Console.WriteLine("speed: " + speed + 
						" gear: " + gear); 
	} 
	
} 

class GFG { 
	
	// Main Method 
	public static void Main(String []args) 
	{ 
	
		// creating an instance of Bicycle 
		// doing some operations 
		Bicycle bicycle = new Bicycle(); 
		bicycle.changeGear(2); 
		bicycle.speedUp(3); 
		bicycle.applyBrakes(1); 
		
		Console.WriteLine("Bicycle present state :"); 
		bicycle.printStates(); 
		
		// creating instance of bike. 
		Bike bike = new Bike(); 
		bike.changeGear(1); 
		bike.speedUp(4); 
		bike.applyBrakes(3); 
		
		Console.WriteLine("Bike present state :"); 
		bike.printStates(); 
	} 
} 

```

**Output:**
Bicycle present state :
speed: 2 gear: 2
Bike present state :
speed: 1 gear: 1


**Advantage of Interface:**

- It is used to achieve loose coupling.
- It is used to achieve total abstraction.
- To achieve component-based programming
- To achieve multiple inheritance and abstraction.
- Interfaces add a plug and play like architecture into applications.


https://www.geeksforgeeks.org/difference-between-abstract-class-and-interface-in-c-sharp/






# Part 2: Relationships


One of the advantages of Object-Oriented programming language is code reuse. This reusability is possible due to the relationship b/w the classes. Object oriented programming generally support 4 types of relationships that are: inheritance, association, composition, and aggregation. All these relationships are based on "is a" relationship, "has-a" relationship, and "part-of" relationship.

In this article, we will understand all these relationships.

## Inheritance

Inheritance is an “IS-A” type of relationship. The “IS-A” relationship is a totally based on Inheritance, which can be of two types Class Inheritance or Interface Inheritance. Inheritance is a parent-child relationship where we create a new class by using existing class code. It is just like saying that “A is a type of B”. For example “Apple is a fruit”, and “Ferrari is a car”.

For better understanding let us take a real-world scenario.

- HOD is a staff member of the college.
- All teachers are staff members of the college.
- HOD and teachers have ID cards to enter into college.
- HOD has a staff that works according to the instructions of him.
- HOD has the responsibility to undertake the work of the teacher to cover the course in the fixed time period.

Let us take the first two assumptions, “HOD is a staff member of the college” and “All teachers are staff members of the college”. For this assumption, we can create a “StaffMember” parent class and inherit this parent class in the “HOD” and “Teacher” classes.

```c#
class StaffMember
{
    public StaffMember()
    {

    }
}
class HOD : StaffMember
{
    public HOD()
    {

    }
}
class Teacher : StaffMember
{
    public Teacher()
    {
    }
}

```

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
namespace Entity2
{
    class StaffMember
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string Department { get; set; }
        public StaffMember() { }
    }
    class HOD : StaffMember
    {
        public HOD() { }
        public int Course_Completed { get; set; }

        public void Hod_Info()
        {
            string Info = $"Member Id = {this.MemberId} \n Member Name = {this.MemberName} \n Department Name = {this.Department} \n Total Course Completed = {this.Course_Completed} %";
            WriteLine(Info);
        }
    }
    class Teacher : StaffMember
    {
        public Teacher() { }
        public int Hod_Id { get; set; }
        public void Teacher_Info()
        {
            string Info = $"Member Id = {this.MemberId} \n Member Name = {this.MemberName} \n Department Name = {this.Department} \n Id of HOD = {this.Hod_Id}";
            WriteLine(Info);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HOD Obj_Hod = new HOD();
            Obj_Hod.MemberId = 10;
            Obj_Hod.MemberName = "Dazy Arya";
            Obj_Hod.Department = "CSE";
            Obj_Hod.Course_Completed = 85;

            Teacher Obj_Tech = new Teacher();
            Obj_Tech.Department = "CSE";
            Obj_Tech.MemberId = 15;
            Obj_Tech.MemberName = "Ambika Gupta";
            Obj_Tech.Hod_Id = 10;

            Obj_Hod.Hod_Info();
            Obj_Tech.Teacher_Info();
            ReadLine();
        }
    }
}
```


## Composition

Composition is a "part-of" relationship. Simply composition means mean use of instance variables that are references to other objects. In a composition relationship, both entities are interdependent on each other for example “engine is part of car”, and “heart is part of body”.

Let us take an example of a car and an engine. Engine is a part of each car and both are dependent on each other.

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
namespace Entity2
{
    class Car
    {
        public Car() { }
        public string Color { get; set; }
        public string Max_Speed { get; set; }
        public Engine Engine {get; set;}
    }
    class Suzuki : Car
    {
        public Suzuki() 
        {
	        Engine = new Engine(); // Eager Composition 
        }
        
	    public Suzuki(Engine engine) // Eager Aggregation
        {
	        Engine = engine;
        }
        
        public int Total_Seats { get; set; }
        public string Model_No { get; set; }
        public void CarInfo()
        {
            string Info = $"Color of car is {this.Color} \nMaximum speed is {this.Max_Speed}\nNumber of seats is {this.Total_Seats}\nModel No is {this.Model_No}\n";
            WriteLine(Info);
            Engine.Engine_Info();
        }
    }
    class Engine
    {
        public void Engine_Info()
        {
            WriteLine("Engine is 4 stroke and fuel efficiency is good");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Suzuki Obj = new Suzuki();
            Obj.Color = "Black";
            Obj.Max_Speed = "240KM/Hour";
            Obj.Model_No = "SUZ234";
            Obj.Total_Seats = 4;
            Obj.CarInfo();
            ReadLine();
        }
    }
}

```


## Association

The association is a “has-a” type relationship. The association establishes the relationship b/w two classes using their objects. Association relationships can be one-to-one, to-many, many-to-one, and many-to-many. For example, suppose we have two classes then these two classes are said to have a “has-a” relationship if both of these entities share each other’s object for some work and at the same time they can exist without each other's dependency or both have their own lifetime.

```c#
class Employee
{
    public Employee() { }
    public string Emp_Name { get; set; }

    public void Manager_Name(Manager Obj)
    {
        Obj.manager_Info(this);
    }
}
class Manager
{
    public Manager() { }
    public string Manager_Name { get; set; }
    public void manager_Info(Employee Obj)
    {
        WriteLine($"Manager of Employee {Obj.Emp_Name} is {this.Manager_Name}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Manager Man_Obj = new Manager();
        Man_Obj.Manager_Name = "Dazy Aray";
        Employee Emp_Obj = new Employee();
        Emp_Obj.Emp_Name = "Ambika";
        Emp_Obj.Manager_Name(Man_Obj);
        ReadLine();
    }
}


```


The above example shows an association relationship because both Employee and Manager classes use the object of each other and both their own independent life cycle.

## Aggregation

Aggregation is based on a "has-a" relationship. Aggregation is a special form of association. In association, there is not any classes (entities) that work as owners but in aggregation one entity work as an owner. In aggregation, both entities meet for some work and then get separated. Aggregation is a one-way association.

### Example

Let us take an example of “Student” and “address”. Each student must have an address so the relationship b/w Student class and Address class will be a “Has-A” type relationship but vice versa is not true(it is not necessary that each address contain by any student). So Students work as owner entities. This will be an aggregation relationship.

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
namespace Entity2
{
    class Student_
    {
        public Student_() { }
        public string Name { get; set; }
        public int roll_No { get; set; }
        public int Class { get; set; }
        public void Get_Student_Info(Address Obj)
        {
            WriteLine($"Student Name={this.Name}\n Roll_No={this.roll_No}\n Class={this.Class}\n");
            Obj.Get_Address();
        }
    }
    class Address
    {
        public Address() { }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public void Get_Address()
        {
            WriteLine($"Street={this.Street} \n City={this.City} \n State={this.State}\n Pincode={this.Pincode}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student_ Stu_Obj = new Student_();
            Stu_Obj.Name = "Pankaj Choudhary";
            Stu_Obj.roll_No = 1210038;
            Stu_Obj.Class = 12;

            Address Obj = new Address();
	            Obj.City = "Alwar";
            Obj.Street = "P-20 Gandhi Nagar";
            Obj.State = "Rajasthan";
            Obj.Pincode = "301001";

            Stu_Obj.Get_Student_Info(Obj);
            ReadLine();
        }
    }
}
```
https://www.c-sharpcorner.com/article/types-of-relationships-in-object-oriented-programming-oops/


## Dependency

#### Definition:

Dependency is a relationship where one class depends on another class for its functionality, but this is a temporary relationship. A dependent class uses another class within a method, often as a parameter or locally instantiated object.

#### Key Points:

- Represents a **temporary relationship**.
- Occurs when a method in one class uses another class.
- Does not create long-term connections between classes.

#### Example:

`class Printer {     public void Print(string content) => Console.WriteLine($"Printing: {content}"); }  class Report {     public void Generate(Printer printer)     {         printer.Print("Report Content");     } }`

Here, `Report` depends on `Printer` to print the report, but there’s no persistent association.

https://medium.com/@humzakhalid94/understanding-object-oriented-relationships-inheritance-association-composition-and-aggregation-4d298494ac1c

![[Pasted image 20241203000244.png]]

### **Scenario: Library Management System**

We will build a simple Library Management System that includes the following:

- **Inheritance (IS-A Relationship):**
    - A `Person` base class is inherited by `Librarian` and `Member`.
- **Composition (PART-OF Relationship):**
    - A `Library` class contains instances of other objects like `Bookshelf`, `Book`, and `LibraryCard`.
- **Association (HAS-A Relationship):**
    - Members and Librarians interact using `LibraryCard` to borrow or issue books.
- **Aggregation (Ownership Relationship):**
    - `Member` aggregates `Address`. The `Address` can exist independently of a `Member`.

---

### **Code Implementation**


```c#
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    // Inheritance: Base class Person
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // Inheritance: Derived class Librarian (IS-A Person)
    class Librarian : Person
    {
        public void IssueBook(Member member, Book book)
        {
            Console.WriteLine($"{Name} (Librarian) issued '{book.Title}' to {member.Name}.");
        }
    }

    // Inheritance: Derived class Member (IS-A Person)
    class Member : Person
    {
        public LibraryCard Card { get; set; } // Association with LibraryCard
        public Address MemberAddress { get; set; } // Aggregation with Address
    }

    // Composition: Library contains other objects
    class Library
    {
        public string LibraryName { get; set; }
        public List<Bookshelf> Bookshelves { get; set; }
		public Library()
		{
			Bookshelves =  new List<Bookshelf>();
		}

        public void AddBookshelf(Bookshelf shelf)
        {
            Bookshelves.Add(shelf);
        }

        public void DisplayLibraryInfo()
        {
            Console.WriteLine($"Welcome to {LibraryName} Library!");
            Console.WriteLine($"We have {Bookshelves.Count} bookshelves.");
        }
    }

    // Composition: Bookshelf is part of Library
    class Bookshelf
    {
        public int ShelfNumber { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }
    }

    // Association: Book can be shared between Members and Library
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }

    // Aggregation: Address can exist independently of Member
    class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
    }

    // Composition: LibraryCard is a part of Member
    class LibraryCard
    {
        public int CardNumber { get; set; }
        public DateTime IssuedDate { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create Library
            Library library = new Library { LibraryName = "Central Library" };

            // Create Bookshelf and Books
            Bookshelf shelf1 = new Bookshelf { ShelfNumber = 1 };
            shelf1.AddBook(new Book { Title = "C# Programming", Author = "John Doe" });
            shelf1.AddBook(new Book { Title = "Data Structures", Author = "Jane Smith"});

            // Add Bookshelf to Library (Composition)
            library.AddBookshelf(shelf1);

            // Display Library Info
            library.DisplayLibraryInfo();

            // Create Member with Aggregated Address
            Address memberAddress = new Address
            {
                Street = "123 Main St",
                City = "Metropolis",
                State = "NY",
                Pincode = "10001"
            };

            Member member = new Member
            {
                Name = "Alice",
                Age = 25,
                MemberAddress = memberAddress,
                Card = new LibraryCard { CardNumber = 101, IssuedDate = DateTime.Now }
            };

            // Create Librarian
            Librarian librarian = new Librarian
            {
                Name = "Mr. Smith",
                Age = 40
            };

            // Issue Book (Association)
            Book selectedBook = shelf1.Books[0]; // Select the first book on the shelf
            librarian.IssueBook(member, selectedBook);

            // Display Member Info and Address
            Console.WriteLine($"Member Info:\nName: {member.Name}\nAddress: {member.MemberAddress.Street}, {member.MemberAddress.City}");
        }
    }
}

```

---

### **Concepts Explained**

1. **Inheritance (IS-A Relationship):**
    - `Librarian` and `Member` inherit from `Person`.
    - Both are specialized versions of `Person` with additional functionality.
2. **Composition (PART-OF Relationship):**
    - A `Library` contains `Bookshelf` instances.
    - A `Bookshelf` contains `Book` instances.
    - A `LibraryCard` is part of `Member`.
3. **Association (HAS-A Relationship):**
    - `Librarian` interacts with `Member` and `Book` for issuing books.
    - `Library` can exist without `Librarian` or `Member`.
4. **Aggregation (Ownership Relationship):**
    - A `Member` has an `Address`.
    - An `Address` can exist independently of a `Member`.

---
This example integrates all object-oriented relationships in a cohesive project.

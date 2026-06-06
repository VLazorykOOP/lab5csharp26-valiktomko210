using System;
using System.Linq;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n--- Оберіть завдання (1-4) або 0 для виходу ---");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    case "2": RunTask1And2(); break;
                    case "3": RunTask3(); break;
                    case "4": RunTask4(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void RunTask1And2()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 1 та 2: ІЄРАРХІЯ ПЕРСОНАЛУ ===");
            
            Person[] hierarchy = new Person[]
            {
                new Employee("Іванов І.І.", 35, "Бухгалтерія"),
                new Worker("Петров П.П.", 23, "Цех №2", "Зварювальник"),
                new Engineer("Сидоров С.С.", 41, "ІТ-відділ", "DevOps")
            };

            Console.WriteLine("\nСортування масиву за віком:");
            var sorted = hierarchy.OrderBy(p => p.Age).ToArray();

            foreach (var person in sorted)
            {
                person.Show();
                Console.WriteLine();
            }

            Console.WriteLine("\nОчищення посилань для демонстрації деструкторів...");
            hierarchy = null;
            sorted = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        static void RunTask3()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 3: МАСИВ МАТЕМАТИЧНИХ ФУНКЦІЙ ===");
            
            Function[] functions = new Function[]
            {
                new Line(2, 3),
                new Quadratic(1, -2, 5),
                new Hyperbola(10)
            };

            Console.Write("Введіть точку x для обчислення функцій: ");
            double x = double.Parse(Console.ReadLine());

            Console.WriteLine($"\nРезультати обчислення у точці x = {x}:");
            foreach (var func in functions)
            {
                func.PrintInfo(x);
            }
        }

        static void RunTask4()
        {
            Console.WriteLine("\n=== ЗАВДАННЯ 4: ЗАПЕЧАТАНИЙ ЧАСТКОВИЙ КЛАС TRIANGLE ===");
            Triangle t = new Triangle(3, 4, 5, 4);
            Console.WriteLine($"Периметр трикутника: {t.CalculatePerimeter()}");
            Console.WriteLine($"Площа трикутника: {t.CalculateArea():F2}");
        }
    }

    abstract class Person
    {
        protected string name;
        protected int age;

        public Person()
        {
            name = "Невідомо";
            age = 0;
            Console.WriteLine("Виклик Person() [без параметрів]");
        }

        public Person(string name)
        {
            this.name = name;
            age = 0;
            Console.WriteLine("Виклик Person(string) [1 параметр]");
        }

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
            Console.WriteLine("Виклик Person(string, int) [2 параметри]");
        }

        ~Person()
        {
            Console.WriteLine($"Деструктор ~Person() для {name}");
        }

        public int Age => age;
        public string Name => name;

        public abstract void Show();
    }

    class Employee : Person
    {
        protected string department;

        public Employee() : base()
        {
            department = "Загальний";
            Console.WriteLine("  Виклик Employee() [без параметрів]");
        }

        public Employee(string name, int age) : base(name, age)
        {
            department = "Загальний";
            Console.WriteLine("  Виклик Employee(string, int) [2 параметри]");
        }

        public Employee(string name, int age, string department) : base(name, age)
        {
            this.department = department;
            Console.WriteLine("  Виклик Employee(string, int, string) [3 параметри]");
        }

        ~Employee()
        {
            Console.WriteLine($"  Деструктор ~Employee() для {name}");
        }

        public override void Show()
        {
            Console.Write($"[Службовець] Ім'я: {name}, Вік: {age}, Відділ: {department}");
        }
    }

    class Worker : Employee
    {
        protected string specialty;

        public Worker() : base()
        {
            specialty = "Робітник";
            Console.WriteLine("    Виклик Worker() [без параметрів]");
        }

        public Worker(string name, int age, string department) : base(name, age, department)
        {
            specialty = "Робітник";
            Console.WriteLine("    Виклик Worker(string, int, string) [3 параметри]");
        }

        public Worker(string name, int age, string department, string specialty) : base(name, age, department)
        {
            this.specialty = specialty;
            Console.WriteLine("    Виклик Worker(string, int, string, string) [4 параметри]");
        }

        ~Worker()
        {
            Console.WriteLine($"    Деструктор ~Worker() для {name}");
        }

        public override void Show()
        {
            Console.Write($"[Робітник] Ім'я: {name}, Вік: {age}, Відділ: {department}, Спеціальність: {specialty}");
        }
    }

    class Engineer : Employee
    {
        protected string field;

        public Engineer() : base()
        {
            field = "Загальна інженерія";
            Console.WriteLine("    Виклик Engineer() [без параметрів]");
        }

        public Engineer(string name, int age, string department) : base(name, age, department)
        {
            field = "Загальна інженерія";
            Console.WriteLine("    Виклик Engineer(string, int, string) [3 параметри]");
        }

        public Engineer(string name, int age, string department, string field) : base(name, age, department)
        {
            this.field = field;
            Console.WriteLine("    Виклик Engineer(string, int, string, string) [4 параметри]");
        }

        ~Engineer()
        {
            Console.WriteLine($"    Деструктор ~Engineer() для {name}");
        }

        public override void Show()
        {
            Console.Write($"[Інженер] Ім'я: {name}, Вік: {age}, Відділ: {department}, Спеціалізація: {field}");
        }
    }

    abstract class Function
    {
        public abstract double Calculate(double x);
        public abstract void PrintInfo(double x);
    }

    class Line : Function
    {
        private double a;
        private double b;

        public Line(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public override double Calculate(double x)
        {
            return a * x + b;
        }

        public override void PrintInfo(double x)
        {
            Console.WriteLine($"Лінійна функція y = {a}x + {b} | При x = {x} -> y = {Calculate(x)}");
        }
    }

    class Quadratic : Function
    {
        private double a;
        private double b;
        private double c;

        public Quadratic(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double Calculate(double x)
        {
            return a * x * x + b * x + c;
        }

        public override void PrintInfo(double x)
        {
            Console.WriteLine($"Квадратична функція y = {a}x^2 + {b}x + {c} | При x = {x} -> y = {Calculate(x)}");
        }
    }

    class Hyperbola : Function
    {
        private double k;

        public Hyperbola(double k)
        {
            this.k = k;
        }

        public override double Calculate(double x)
        {
            if (x == 0) return double.NaN;
            return k / x;
        }

        public override void PrintInfo(double x)
        {
            Console.WriteLine($"Гіпербола y = {k}/x | При x = {x} -> y = {Calculate(x)}");
        }
    }

    sealed partial class Triangle
    {
        protected int a;
        protected int b;
        protected int c;
        protected int color;

        public Triangle(int a, int b, int c, int color)
        {
            this.a = a; this.b = b; this.c = c; this.color = color;
        }

        public int A => a;
        public int B => b;
        public int C => c;
        public int Color => color;
    }

    sealed partial class Triangle
    {
        public int CalculatePerimeter()
        {
            return a + b + c;
        }

        public double CalculateArea()
        {
            double p = CalculatePerimeter() / 2.0;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }
}
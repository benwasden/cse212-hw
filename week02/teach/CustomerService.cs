using Microsoft.VisualBasic;


/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Can I add one customer and then serve them
        // Expected Result: It should display the added customer
        Console.WriteLine("Test 1");
        var cs = new CustomerService(4);
        cs.AddNewCustomer();
        cs.ServeCustomer();

        // Defect(s) Found: Program failed when trying to serve customer, was removing early from array

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Can I create an invalid amount and not have the application fail
        // Expected Result: It should default to 10
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(-1);
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();
        cs2.ServeCustomer();

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below

        // Test 3
        // Scenario: Can I get the error message to appear by adding too many to queue
        // Expected result: An error will appear when you try to +1 to queue above max amount
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(3);
        cs3.AddNewCustomer();
        cs3.AddNewCustomer();
        cs3.AddNewCustomer();
        cs3.AddNewCustomer();

        // Defect(s) Found: No message appears. Needed to change from > to >= in AddNewCustomer

        Console.WriteLine("=================");

        // Test 4
        // Scenario: What happens when I serve a queue with no customers in it
        // Expected result: An error should pop up
        Console.WriteLine("Test 4");
        var cs4 = new CustomerService(3);
        cs4.ServeCustomer();

        // Defect(s) Found: Unhandled exception error. Needed to add an if/else statement to ServeCustomer to handle a queue of length 0
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count <= 0)
        {
            Console.WriteLine("No customers in queue");
        }
        else
        {
            var customer = _queue[0];
            _queue.RemoveAt(0);
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Can I add 3 items to queue and see if they dequeue in order of priority?
    // Expected Result: It will dequeue the high priority one first, then medium, then low.
    // Defect(s) Found: The dequeue method wasn't removing the item from the list after they were completed.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Low", 1);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Can I add two items of the same priority and have it remove them in FIFO order?
    // Expected Result: It should dequeue in High1, High2, High3, Medium, Low, Low2
    // Defect(s) Found: Needed to remove = sign from Dequeue method in PriorityQueue.cs. It was grabbing the most recently added if I didn't do that.
    // Also fixed an issue not discovered from last test where search index was ending at Count - 1 in Dequeue method.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High1", 5);
        priorityQueue.Enqueue("High2", 5);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High3", 5);
        priorityQueue.Enqueue("Low2", 1);
        priorityQueue.Enqueue("High4", 5);

        Assert.AreEqual("High1", priorityQueue.Dequeue());
        Assert.AreEqual("High2", priorityQueue.Dequeue());
        Assert.AreEqual("High3", priorityQueue.Dequeue());
        Assert.AreEqual("High4", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("Low2", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Trying to dequeue from an empty queue
    // Expected Result: It should throw an error message that says "The queue is empty."
    // Defect(s) Found: None :)
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Correct exception wasn't thrown");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }
}
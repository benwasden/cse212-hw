using System.Data;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        
        // I need to create an array that only accepts doubles, then multiply the number by 1, then 2, then continue that until
        // I've multiplied it by "length" amount of times. I need to add these numbers to the array each time too, and then
        // return the array at the end.

        // Creating the array
        double[] results = new double[length];

        // Setting up a loop that starts at i = 1 and then proceeds until i is the same length as the specified amount
        for (int i = 1; i <= length; i++)
        {
            // Specifies that for the i - 1 position in the array, the value is the number times i, giving the multiple
            results[i - 1] = (number * i);
        }

        // Returns the array
        return results; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start

        // I'll create a list with the numbers that I'm removing first. Then I'll get the values that I'm removing
        // from the data list and add those to the oldNums list. Then I'll actually remove the numbers and add them
        // back on.

        // Creating the list to add the old numbers to
        List<int> oldNums = new();

        // Getting the data that will be changed. I need to go from index 0 to the length of the array minus the specified amount
        var dataToChange = data.GetRange(0, data.Count() - amount);
        // Adding the aquired numbers from the prior step to the oldNums list
        oldNums.AddRange(dataToChange);

        // Removing the numbers from the data list. Again, just like the dataToChange var, I need to remove from 0 to data.Count - specified amount
        data.RemoveRange(0, data.Count() - amount);
        // Adding the numbers that were removed back onto the end of the data list
        data.AddRange(oldNums);
    }
}
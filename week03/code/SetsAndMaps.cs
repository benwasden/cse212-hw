using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        var seen = new HashSet<string>();
        var result = new List<string>();

        foreach (var word in words)
        {
            // Checking to see if first and second letter aren't the same
            if (word[0] == word[1])
            {
                continue;
            }
            // Adding valid results to "seen" set
            seen.Add(word);
            var reverse = $"{word[1]}{word[0]}";

            // Checking if reverse matches a value in seen set, and if so then adds to result list
            if (seen.Contains(reverse))
            {
                result.Add($"{word} & {reverse}");
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            // Pulls degree name from the 4th column of .txt file
            string degree = fields[3].Trim();

            // Checking if degrees dictionary already contains the degree
            if (degrees.ContainsKey(degree))
            {
                // If so, adds +1 to the # of people who've aquired the degree
                degrees[degree]++;
            }
            else
            {
                // If not, creates it and sets the # of people to 1
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Standardizing format of both words
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // Checking if both words are the same length
        if (word1.Length != word2.Length)
        {
            return false;
        }

        // Creating a dictionary to store letters of word1, which will then have letters removed if they're in word2
        var letters = new Dictionary<char, int>();

        foreach (var letter in word1)
        {
            // Checking to see if letter's already in dictionary
            if (letters.ContainsKey(letter))
            {
                // If so, adds +1 to it's count
                letters[letter]++;
            }
            else
            {
                // If not, adds the letter and sets it's count to 1
                letters[letter] = 1;
            }
        }

        foreach (var letter in word2)
        {
            // Checking if letter is present in word2 and word1
            if (letters.ContainsKey(letter))
            {
                // If it is, subtracts 1 from appearance count
                letters[letter]--;
                // Checking to see if all appearances of a letter have been used up. If so, returns false
                if (letters[letter] < 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        // If it passes all checks, it's an anagram
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // Creating a list to add strings with place and magnitude
        var locations = new List<string>();
        // Iterating over features in JSON
        foreach (var feature in featureCollection.Features)
        {
            // Assigning the info in JSON to variables
            var place = feature.Properties.Place;
            var mag = feature.Properties.Mag;

            // Creating a string with info
            locations.Add($"{place}, - Mag {mag}");
        }
        return locations.ToArray();
    }
}
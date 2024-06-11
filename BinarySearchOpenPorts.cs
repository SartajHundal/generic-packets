using System;
using System.Collections.Generic;

/// <summary>
/// Demonstrates the use of binary search to find a specific port number within a sorted list of known open ports.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a sorted list of known open ports.
        List<int> openPorts = new List<int> { 22, 80, 443, 8080, 8443 };

        // Prompt the user to enter a port number to search for.
        Console.WriteLine("Enter the port number to search:");
        int targetPort = Convert.ToInt32(Console.ReadLine());

        // Attempt to find the target port using binary search.
        int result = BinarySearchOpenPorts(openPorts, targetPort);

        // Display the outcome of the search.
        if (result!= -1)
        {
            Console.WriteLine($"Port {targetPort} found at index: {result}");
        }
        else
        {
            Console.WriteLine("Port not found.");
        }
    }

    /// <summary>
    /// Performs a binary search on a sorted list of open ports to find a target port.
    /// </summary>
    /// <param name="ports">A sorted list of known open ports.</param>
    /// <param name="target">The port number to search for.</param>
    /// <returns>The index of the target port if found, otherwise -1.</returns>
    static int BinarySearchOpenPorts(List<int> ports, int target)
    {
        int left = 0;
        int right = ports.Count - 1;

        // Perform binary search.
        while (left <= right)
        {
            int middle = left + ((right - left) / 2);

            if (ports[middle] == target)
            {
                return middle; // Port found.
            }
            else if (ports[middle] < target)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return -1; // Port not found.
    }
}

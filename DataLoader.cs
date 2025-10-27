using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HybridSearch
{
    public static class DataLoader
    {
        public static List<Product> LoadProductsFromFile(string filePath)
        {
            var products = new List<Product>();

            try
            {
                var lines = File.ReadAllLines(filePath);
                
                // Skip the header line (first line)
                for (int i = 1; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Parse the semicolon-separated format
                    // Format: ProductName;Vi;U2i;U1i
                    var parts = line.Split(';')
                        .Select(p => p.Trim())
                        .ToArray();

                    if (parts.Length >= 4)
                    {
                        var name = parts[0];
                        
                        // Parse values with invariant culture
                        if (double.TryParse(parts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double vi) &&
                            double.TryParse(parts[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double u2i) &&
                            double.TryParse(parts[3], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double u1i))
                        {
                            products.Add(new Product(name, vi, u2i, u1i));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }

            return products;
        }
    }
}

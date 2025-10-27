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
                
                // Skip the header lines (first 2 lines: header and separator)
                for (int i = 2; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Parse the markdown table format
                    // Format: | ProductName | Vi | U2i | U1i | ? | ? |
                    var parts = line.Split('|')
                        .Select(p => p.Trim())
                        .Where(p => !string.IsNullOrWhiteSpace(p))
                        .ToArray();

                    // First column is empty pipe separator, so parts[0] is the actual name
                    if (parts.Length >= 4)
                    {
                        var name = parts[0];
                        
                        // Parse values, skip the ? columns for Qi and Mi
                        if (double.TryParse(parts[1], out double vi) &&
                            double.TryParse(parts[2], out double u2i) &&
                            double.TryParse(parts[3], out double u1i))
                        {
                            products.Add(new Product(name, vi, u2i, u1i));
                        }
                        else
                        {
                            // Try to parse with different decimal separators
                            if (double.TryParse(parts[1].Replace(',', '.'), out vi) &&
                                double.TryParse(parts[2].Replace(',', '.'), out u2i) &&
                                double.TryParse(parts[3].Replace(',', '.'), out u1i))
                            {
                                products.Add(new Product(name, vi, u2i, u1i));
                            }
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

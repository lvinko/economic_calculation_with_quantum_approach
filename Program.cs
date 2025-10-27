using System;
using System.Collections.Generic;
using System.Linq;

namespace HybridSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Hybrid Quantum Product Search ===");
            Console.WriteLine();

            // Load products from data.txt
            var products = DataLoader.LoadProductsFromFile("input/data.txt");
            
            if (products.Count == 0)
            {
                Console.WriteLine("No products loaded from data.txt");
                return;
            }

            Console.WriteLine($"Loaded {products.Count} products:");
            foreach (var product in products)
            {
                Console.WriteLine($"  - {product.Name}");
            }
            Console.WriteLine();

            // Display all products with calculated values
            Console.WriteLine("=== Products with Calculated Values ===");
            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.Name}");
                Console.WriteLine($"  Річний оборот: {product.Vi} тис. грн");
                Console.WriteLine($"  Розмір партії: {product.Qi} шт");
                Console.WriteLine($"  Норматив запасу: {product.Mi} шт");
                Console.WriteLine();
            }

            // Interactive search (optional)
            if (args.Length > 0 && args[0] == "--interactive")
            {
                string searchKey;
                do
                {
                    Console.Write("Enter product name to search (or 'exit' to quit): ");
                    searchKey = Console.ReadLine();

                    if (searchKey?.ToLower() == "exit")
                        break;

                    if (string.IsNullOrWhiteSpace(searchKey))
                        continue;

                    // Search for products matching the key
                    var foundProducts = products.Where(p => 
                        p.Name.Contains(searchKey, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (foundProducts.Count > 0)
                    {
                        foreach (var product in foundProducts)
                        {
                            Console.WriteLine($"✅ Знайдено: {product.Name}");
                            Console.WriteLine($"Річний оборот: {product.Vi} тис. грн");
                            Console.WriteLine($"Розмір партії: {product.Qi} шт");
                            Console.WriteLine($"Норматив запасу: {product.Mi} шт");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"❌ Product '{searchKey}' not found.");
                        Console.WriteLine();
                    }

                } while (true);
            }
            else
            {
                Console.WriteLine("To enable interactive search, run with --interactive flag");
            }

            Console.WriteLine("\n✅ All calculations completed successfully!");
        }
    }
}

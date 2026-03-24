/*Week-5 (DAY-1) Hands-On
Please download and refer shared Code template (LinqCodeTemplate) and solve problems as given below. 
(Please Refer Problem-1 Solved in the Code Template and solve rest of other problems in the same project accordingly)
Problem Level- 1 and 2:
1.Write a LINQ query to search and display all products with category “FMCG”.
2. Write a LINQ query to search and display all products with category “Grain”.
3. Write a LINQ query to sort products in ascending order by product code.
4. Write a LINQ query to sort products in ascending order by product Category.
5. Write a LINQ query to sort products in ascending order by product Mrp.
6. Write a LINQ query to sort products in descending order by product Mrp.
7. Write a LINQ query to display products group by product Category.
8. Write a LINQ query to display products group by product Mrp.
9. Write a LINQ query to display product detail with highest price in FMCG category.
10. Write a LINQ query to display count of total products.
11. Write a LINQ query to display count of total products with category FMCG.
12.Write a LINQ query to display Max price.
13.Write a LINQ query to display Min price.
14. Write a LINQ query to display whether all products are below Mrp Rs.30 or not.
15. Write a LINQ query to display whether any products are below Mrp Rs.30 or not.
*/
// 2. Products with category "Grain"
using LinqCodeTemplate;

Product product = new Product();
var products = product.GetProducts();
var grainProducts = products.Where(p => p.ProCategory == "Grain");
Console.WriteLine("\nGrain Products:");
foreach (var item in grainProducts)
{
    Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
}


// 3. Sort by Product Code (Ascending)
var sortByCode = products.OrderBy(p => p.ProCode);
Console.WriteLine("\nSorted by Product Code:");
foreach (var item in sortByCode)
{
    Console.WriteLine($"{item.ProCode}\t{item.ProName}");
}


// 4. Sort by Category (Ascending)
var sortByCategory = products.OrderBy(p => p.ProCategory);
Console.WriteLine("\nSorted by Category:");
foreach (var item in sortByCategory)
{
    Console.WriteLine($"{item.ProCategory}\t{item.ProName}");
}


// 5. Sort by MRP (Ascending)
var sortByMrpAsc = products.OrderBy(p => p.ProMrp);
Console.WriteLine("\nSorted by MRP (Ascending):");
foreach (var item in sortByMrpAsc)
{
    Console.WriteLine($"{item.ProName}\t{item.ProMrp}");
}


// 6. Sort by MRP (Descending)
var sortByMrpDesc = products.OrderByDescending(p => p.ProMrp);
Console.WriteLine("\nSorted by MRP (Descending):");
foreach (var item in sortByMrpDesc)
{
    Console.WriteLine($"{item.ProName}\t{item.ProMrp}");
}


// 7. Group by Category
var groupByCategory = products.GroupBy(p => p.ProCategory);
Console.WriteLine("\nGrouped by Category:");
foreach (var group in groupByCategory)
{
    Console.WriteLine($"\nCategory: {group.Key}");
    foreach (var item in group)
    {
        Console.WriteLine($"{item.ProName}\t{item.ProMrp}");
    }
}


// 8. Group by MRP
var groupByMrp = products.GroupBy(p => p.ProMrp);
Console.WriteLine("\nGrouped by MRP:");
foreach (var group in groupByMrp)
{
    Console.WriteLine($"\nMRP: {group.Key}");
    foreach (var item in group)
    {
        Console.WriteLine($"{item.ProName}");
    }
}


// 9. Highest price product in FMCG
var maxFmcg = products
    .Where(p => p.ProCategory == "FMCG")
    .OrderByDescending(p => p.ProMrp)
    .FirstOrDefault();

Console.WriteLine("\nHighest Price FMCG Product:");
Console.WriteLine($"{maxFmcg.ProName}\t{maxFmcg.ProMrp}");


// 10. Total product count
var totalCount = products.Count();
Console.WriteLine($"\nTotal Products: {totalCount}");


// 11. Count of FMCG products
var fmcgCount = products.Count(p => p.ProCategory == "FMCG");
Console.WriteLine($"FMCG Product Count: {fmcgCount}");


// 12. Max price
var maxPrice = products.Max(p => p.ProMrp);
Console.WriteLine($"\nMax Price: {maxPrice}");


// 13. Min price
var minPrice = products.Min(p => p.ProMrp);
Console.WriteLine($"Min Price: {minPrice}");


// 14. All products below Rs.30?
var allBelow30 = products.All(p => p.ProMrp < 30);
Console.WriteLine($"\nAll products below Rs.30: {allBelow30}");


// 15. Any product below Rs.30?
var anyBelow30 = products.Any(p => p.ProMrp < 30);
Console.WriteLine($"Any product below Rs.30: {anyBelow30}");
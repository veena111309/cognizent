import java.util.Arrays;
import java.util.Comparator;

public class SearchTest {

    // Linear Search
    public static Product linearSearch(Product[] products, int id) {
        for (Product product : products) {
            if (product.productId == id) {
                return product;
            }
        }
        return null;
    }

    // Binary Search
    public static Product binarySearch(Product[] products, int id) {

        int left = 0;
        int right = products.length - 1;

        while (left <= right) {

            int mid = (left + right) / 2;

            if (products[mid].productId == id)
                return products[mid];

            if (products[mid].productId < id)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return null;
    }

    public static void main(String[] args) {

        Product[] products = {
                new Product(103, "Laptop", "Electronics"),
                new Product(101, "Mobile", "Electronics"),
                new Product(104, "Shoes", "Fashion"),
                new Product(102, "Watch", "Accessories")
        };

        System.out.println("Linear Search:");

        Product result = linearSearch(products, 102);

        if (result != null)
            result.display();
        else
            System.out.println("Product not found");

        Arrays.sort(products, Comparator.comparingInt(p -> p.productId));

        System.out.println("\nBinary Search:");

        result = binarySearch(products, 102);

        if (result != null)
            result.display();
        else
            System.out.println("Product not found");

        System.out.println("\nTime Complexity");
        System.out.println("Linear Search : O(n)");
        System.out.println("Binary Search : O(log n)");
    }
}
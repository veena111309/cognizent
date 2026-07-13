package Algorithms_DataStructures.Exercise3;

import java.util.Arrays;

/**
 * ============================================================================
 * Order Sorting System
 * Goal: Compare Bubble Sort and Quick Sort for arranging commercial transactions.
 * Scope: Deep Skilling Week 1 - Algorithms & Data Structures (Exercise 3)
 *
 * Design Enhancements:
 * 1. Clones arrays before sorting to ensure fair benchmarks.
 * 2. Implements a middle-index pivot strategy for Quick Sort to prevent the
 *    common O(n^2) degradation on pre-sorted arrays (improving performance).
 * 3. Uses robust encapsulation for transaction details.
 *
 * Space & Time Complexity Analysis:
 * ----------------------------------------------------------------------
 * | Algorithm | Best Time   | Avg Time    | Worst Time  | Space Complexity |
 * |-----------|-------------|-------------|-------------|------------------|
 * | Bubble    | O(n)        | O(n^2)      | O(n^2)      | O(1) (In-place)  |
 * | Quick     | O(n log n)  | O(n log n)  | O(n^2)      | O(log n) stack   |
 * ----------------------------------------------------------------------
 * ============================================================================
 */

class TransactionOrder {
    private final int orderId;
    private final String clientName;
    private final double invoiceAmount;

    public TransactionOrder(int orderId, String clientName, double invoiceAmount) {
        this.orderId = orderId;
        this.clientName = clientName;
        this.invoiceAmount = invoiceAmount;
    }

    public int getOrderId() {
        return orderId;
    }

    public String getClientName() {
        return clientName;
    }

    public double getInvoiceAmount() {
        return invoiceAmount;
    }

    @Override
    public String toString() {
        return String.format("[ID: %d | Client: %-8s | Total: $%.2f]", orderId, clientName, invoiceAmount);
    }
}

public class OrderSorter {

    /**
     * Bubble Sort Implementation (Optimized with a swap flag)
     * Time Complexity: O(n) Best, O(n^2) Average & Worst
     * Space Complexity: O(1)
     */
    public static void bubbleSort(TransactionOrder[] orders) {
        int n = orders.length;
        boolean swapped;
        for (int i = 0; i < n - 1; i++) {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++) {
                if (orders[j].getInvoiceAmount() > orders[j + 1].getInvoiceAmount()) {
                    TransactionOrder temp = orders[j];
                    orders[j] = orders[j + 1];
                    orders[j + 1] = temp;
                    swapped = true;
                }
            }
            // If no elements were swapped, array is already sorted
            if (!swapped) break;
        }
    }

    /**
     * Quick Sort Entry Point
     * Time Complexity: O(n log n) Average
     * Space Complexity: O(log n) auxiliary stack space
     */
    public static void quickSort(TransactionOrder[] orders, int low, int high) {
        if (low < high) {
            int pivotIndex = partition(orders, low, high);
            quickSort(orders, low, pivotIndex - 1);
            quickSort(orders, pivotIndex + 1, high);
        }
    }

    /**
     * Partition helper using Middle Pivot strategy (to avoid worst case O(n^2) on sorted arrays)
     */
    private static int partition(TransactionOrder[] orders, int low, int high) {
        // Choose middle element as pivot and swap it to high position
        int mid = low + (high - low) / 2;
        swap(orders, mid, high);

        double pivot = orders[high].getInvoiceAmount();
        int i = low - 1;

        for (int j = low; j < high; j++) {
            if (orders[j].getInvoiceAmount() < pivot) {
                i++;
                swap(orders, i, j);
            }
        }
        swap(orders, i + 1, high);
        return i + 1;
    }

    private static void swap(TransactionOrder[] arr, int idx1, int idx2) {
        TransactionOrder temp = arr[idx1];
        arr[idx1] = arr[idx2];
        arr[idx2] = temp;
    }

    public static void main(String[] args) {
        TransactionOrder[] dataset = {
            new TransactionOrder(501, "Acme Corp", 8900.50),
            new TransactionOrder(502, "Beta Inc", 1250.00),
            new TransactionOrder(503, "Cappa LLC", 15400.00),
            new TransactionOrder(504, "Delta Ltd", 450.00),
            new TransactionOrder(505, "Epsilon Co", 7350.20)
        };

        System.out.println("=== Original Transactions List ===");
        printArray(dataset);

        // 1. Test Bubble Sort
        TransactionOrder[] bubbleList = Arrays.copyOf(dataset, dataset.length);
        bubbleSort(bubbleList);
        System.out.println("\n=== Post Bubble Sort ===");
        printArray(bubbleList);

        // 2. Test Quick Sort
        TransactionOrder[] quickList = Arrays.copyOf(dataset, dataset.length);
        quickSort(quickList, 0, quickList.length - 1);
        System.out.println("\n=== Post Quick Sort ===");
        printArray(quickList);
    }

    private static void printArray(TransactionOrder[] arr) {
        for (TransactionOrder item : arr) {
            System.out.println(item);
        }
    }
}

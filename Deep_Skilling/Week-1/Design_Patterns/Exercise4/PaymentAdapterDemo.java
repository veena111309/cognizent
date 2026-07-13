package Design_Patterns.Exercise4;

/**
 * ============================================================================
 * Adapter Design Pattern (Payment Gateway Adapter)
 * Goal: Bridge incompatible APIs from third-party vendor SDKs into a 
 *       standardized corporate payment interface.
 * Scope: Deep Skilling Week 1 - Design Patterns & Principles (Exercise 4)
 *
 * Improvement on Reference:
 * - Uses constructor injection for the Adaptee dependencies, ensuring testability
 *   and decoupling (compared to the reference which hardcodes `new PayPalGateway()`
 *   inside the adapter constructor, violating SOLID principles).
 * ============================================================================
 */

// Target Interface (the standardized interface our application uses)
interface GatewayPaymentHandler {
    void charge(String customerEmail, double amountInUsd);
}

// Adaptee A: Third-party Square SDK
class SquareMerchantApi {
    public void executeCharge(double amount, String email) {
        System.out.printf("Square API: Charged $%.2f to subscriber '%s'.%n", amount, email);
    }
}

// Adaptee B: Third-party Braintree SDK
class BraintreeCheckoutSdk {
    public void transact(String clientEmail, double value) {
        System.out.printf("Braintree SDK: Processing transaction for '%s' of value $%.2f.%n", clientEmail, value);
    }
}

// Adapter A: Adapt Square API to GatewayPaymentHandler
class SquarePaymentAdapter implements GatewayPaymentHandler {
    private final SquareMerchantApi api;

    // Dependency injection via constructor
    public SquarePaymentAdapter(SquareMerchantApi api) {
        this.api = api;
    }

    @Override
    public void charge(String customerEmail, double amountInUsd) {
        api.executeCharge(amountInUsd, customerEmail);
    }
}

// Adapter B: Adapt Braintree SDK to GatewayPaymentHandler
class BraintreePaymentAdapter implements GatewayPaymentHandler {
    private final BraintreeCheckoutSdk sdk;

    // Dependency injection via constructor
    public BraintreePaymentAdapter(BraintreeCheckoutSdk sdk) {
        this.sdk = sdk;
    }

    @Override
    public void charge(String customerEmail, double amountInUsd) {
        sdk.transact(customerEmail, amountInUsd);
    }
}

public class PaymentAdapterDemo {
    public static void main(String[] args) {
        System.out.println("=== Initializing Payment Adapters (Dependency Injection) ===\n");

        // Instantiate external dependencies
        SquareMerchantApi squareApi = new SquareMerchantApi();
        BraintreeCheckoutSdk braintreeSdk = new BraintreeCheckoutSdk();

        // Standardize them using our adapters
        GatewayPaymentHandler squareProcessor = new SquarePaymentAdapter(squareApi);
        GatewayPaymentHandler braintreeProcessor = new BraintreePaymentAdapter(braintreeSdk);

        // Run transactions through the standard interface
        System.out.println("--- Processing Square Charge ---");
        squareProcessor.charge("dev@company.com", 299.99);

        System.out.println("\n--- Processing Braintree Charge ---");
        braintreeProcessor.charge("billing@client.org", 1250.00);
    }
}

package Design_Patterns.Exercise1;

/**
 * ============================================================================
 * Thread-Safe Singleton Pattern (System Configuration Logger)
 * Goal: Implement a thread-safe Singleton to coordinate logging.
 * Scope: Deep Skilling Week 1 - Design Patterns & Principles (Exercise 1)
 *
 * Improvement on Reference:
 * - The reference implementation is not thread-safe. Multiple threads calling
 *   getInstance() concurrently could instantiate multiple Logger instances.
 * - This version uses the "Bill Pugh Singleton" idiom (static inner helper class).
 *   It guarantees lazy-loading, thread-safety, and avoids synchronization
 *   performance overhead.
 * ============================================================================
 */

class ConfigurationLogger {

    // Private constructor prevents direct instantiation
    private ConfigurationLogger() {
        System.out.println("[System Info] ConfigurationLogger initialized.");
    }

    /**
     * Bill Pugh Singleton Helper Class.
     * The JVM loads this class only when getInstance() is called,
     * ensuring thread-safe lazy-loading.
     */
    private static class LoggerHolder {
        private static final ConfigurationLogger INSTANCE = new ConfigurationLogger();
    }

    /**
     * Public global access point.
     */
    public static ConfigurationLogger getInstance() {
        return LoggerHolder.INSTANCE;
    }

    /**
     * Prints a formatted system log message.
     */
    public void log(String message) {
        System.out.println("[LOG]: " + message);
    }
}

public class LoggerSingleton {
    public static void main(String[] args) {
        System.out.println("=== Testing Thread-Safe Singleton ===");

        // Fetching singleton references
        ConfigurationLogger logger1 = ConfigurationLogger.getInstance();
        logger1.log("Booting system services.");

        ConfigurationLogger logger2 = ConfigurationLogger.getInstance();
        logger2.log("Initializing database drivers.");

        ConfigurationLogger logger3 = ConfigurationLogger.getInstance();
        logger3.log("Establishing secure network sockets.");

        System.out.println("\n=== Equality Verification ===");
        System.out.println("logger1 == logger2 : " + (logger1 == logger2));
        System.out.println("logger2 == logger3 : " + (logger2 == logger3));

        if (logger1 == logger2 && logger2 == logger3) {
            System.out.println("Result: Success! All instances are identical. Singleton pattern satisfied.");
        } else {
            System.out.println("Result: Failure. Multiple instances detected.");
        }
    }
}

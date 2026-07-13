package Design_Patterns.Exercise3;

/**
 * ============================================================================
 * Builder Design Pattern (Office Document Constructor)
 * Goal: Build a complex immutable OfficeDocument configuration object step-by-step.
 * Scope: Deep Skilling Week 1 - Design Patterns & Principles (Exercise 3)
 *
 * Improvement on Reference:
 * - Employs a business-oriented document formatting domain instead of computer hardware.
 * - Enforces business validation in the build() method (e.g., must have a title and author).
 * ============================================================================
 */

class OfficeDocument {
    // Document Attributes (Immutable)
    private final String title;
    private final String author;
    private final String bodyContent;
    private final boolean hasWatermark;
    private final String marginSize;
    private final String orientation; // Portrait or Landscape

    private OfficeDocument(Builder builder) {
        this.title = builder.title;
        this.author = builder.author;
        this.bodyContent = builder.bodyContent;
        this.hasWatermark = builder.hasWatermark;
        this.marginSize = builder.marginSize;
        this.orientation = builder.orientation;
    }

    public void printMetadata() {
        System.out.println("====== Document Properties ======");
        System.out.println("Title       : " + title);
        System.out.println("Author      : " + author);
        System.out.println("Orientation : " + orientation);
        System.out.println("Margin Size : " + marginSize);
        System.out.println("Watermark   : " + (hasWatermark ? "YES" : "NO"));
        System.out.println("Body Snippet: " + (bodyContent != null ? bodyContent.substring(0, Math.min(20, bodyContent.length())) + "..." : "Empty"));
        System.out.println();
    }

    // Static nested Builder class
    public static class Builder {
        private final String title; // Required
        private final String author; // Required
        private String bodyContent = ""; // Optional defaults
        private boolean hasWatermark = false;
        private String marginSize = "1.0 inch";
        private String orientation = "Portrait";

        public Builder(String title, String author) {
            if (title == null || author == null) {
                throw new IllegalArgumentException("Title and Author are mandatory fields.");
            }
            this.title = title;
            this.author = author;
        }

        public Builder setBodyContent(String content) {
            this.bodyContent = content;
            return this;
        }

        public Builder enableWatermark(boolean watermark) {
            this.hasWatermark = watermark;
            return this;
        }

        public Builder setMargin(String margin) {
            this.marginSize = margin;
            return this;
        }

        public Builder setOrientation(String layoutOrientation) {
            this.orientation = layoutOrientation;
            return this;
        }

        public OfficeDocument build() {
            // Validation step
            if (this.title.trim().isEmpty()) {
                throw new IllegalStateException("Cannot build a document with an empty title.");
            }
            return new OfficeDocument(this);
        }
    }
}

public class DocumentBuilder {
    public static void main(String[] args) {
        System.out.println("=== Constructing Documents via Builder Pattern ===\n");

        // Building a standard contract document
        OfficeDocument contractDoc = new OfficeDocument.Builder("Service Level Agreement", "Legal Dept")
                .setBodyContent("This contract outlines the uptime policies of the platform.")
                .setMargin("0.75 inch")
                .enableWatermark(true)
                .build();

        // Building a quick landscape report
        OfficeDocument quarterlyReport = new OfficeDocument.Builder("Q2 Financial Forecast", "Finance Team")
                .setBodyContent("Detailed charts showing expected upward trends in earnings.")
                .setOrientation("Landscape")
                .build();

        contractDoc.printMetadata();
        quarterlyReport.printMetadata();
    }
}

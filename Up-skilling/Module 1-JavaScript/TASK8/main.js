let prices = [10, 25, 45, 90, 120];

// Map: apply tax
let taxedPrices = prices.map(p => p * 1.10);
// Filter: premium values
let premium = prices.filter(p => p > 50);

console.log("Taxed Prices:", taxedPrices);
console.log("Premium Prices (>50):", premium);
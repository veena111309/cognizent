// Triple equals vs double equals
let val1 = 5;
let val2 = "5";

console.log("Double equals (5 == '5'):", val1 == val2); // true (coercion)
console.log("Triple equals (5 === '5'):", val1 === val2); // false (strict type check)
let num = 42;
let str = "Hello World";
let active = true;
let empty = null;
let undef;
let obj = { name: "Aiden", role: "Dev" };

console.log("Type of num:", typeof num);
console.log("Type of str:", typeof str);
console.log("Type of active:", typeof active);
console.log("Type of empty:", typeof empty); // returns object (historical JS bug)
console.log("Type of undef:", typeof undef);
console.log("Type of obj:", typeof obj);
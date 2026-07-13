function createCounter() {
    let count = 0;
    return {
        increment: function() {
            count++;
            return count;
        },
        getCount: function() {
            return count;
        }
    };
}
const counter = createCounter();
console.log("Increment:", counter.increment());
console.log("Increment:", counter.increment());
console.log("Value:", counter.getCount());
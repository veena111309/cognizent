// Var vs Let scope demonstration
function testScope() {
    var functionScoped = "I am function scoped";
    if (true) {
        let blockScoped = "I am block scoped";
        var alsoFunctionScoped = "I leak outside block";
        console.log("Inside block (let):", blockScoped);
    }
    console.log("Outside block (var):", alsoFunctionScoped);
    try {
        console.log(blockScoped);
    } catch(e) {
        console.log("Outside block (let catch):", e.message);
    }
}
testScope();
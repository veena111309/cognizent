document.getElementById('authForm').addEventListener('submit', (e) => {
    const name = document.getElementById('username').value;
    const errorBox = document.getElementById('errorBox');
    if (name.length < 5) {
        e.preventDefault(); // blocks submission
        errorBox.innerText = "Username must be at least 5 characters long.";
    } else {
        errorBox.innerText = "";
        alert("Form check passed!");
    }
});
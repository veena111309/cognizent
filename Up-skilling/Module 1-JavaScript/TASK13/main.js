function fetchPost() {
    const box = document.getElementById('outputBox');
    box.innerText = "Loading data...";
    fetch('https://jsonplaceholder.typicode.com/posts/1')
        .then(response => response.json())
        .then(data => {
            box.innerHTML = `<h3>${data.title}</h3><p>${data.body}</p>`;
        })
        .catch(err => {
            box.innerText = "Error fetching data.";
        });
}
// Load notes
document.getElementById('noteArea').value = localStorage.getItem('userNotes') || '';

function saveNote() {
    const text = document.getElementById('noteArea').value;
    localStorage.setItem('userNotes', text);
    alert("Notes saved to LocalStorage!");
}
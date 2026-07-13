$("#hoverBox").hover(
    function() {
        $(this).css("background-color", "lightblue").text("Mouse Entered");
    },
    function() {
        $(this).css("background-color", "lightgrey").text("Hover Over Me");
    }
);
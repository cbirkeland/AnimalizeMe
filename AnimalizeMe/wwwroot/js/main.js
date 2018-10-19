function readURL(input) {


    document.getElementById("blah").style.visibility="visible";

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        
        reader.onload = function (e) {
            $('#blah')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function myFunction() {
    var x = document.getElementById("myDIV");
    
        x.style.display = "block";
    
        
}
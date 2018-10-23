function readURL(input) {


    document.getElementById("userImage").style.visibility="visible";

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        
        reader.onload = function (e) {
            $('#userImage')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function visibleImage() {
    var x = document.getElementById("imageDIV");
    
        x.style.display = "block";
    
        
}
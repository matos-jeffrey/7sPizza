$(function () {
    
    //for next section
    $("button.next").on("click", function () {

        //hidden the current section
        $(this).parent().removeClass("open");
        //show the next one section
        $(this).parent().next().addClass("open");
    })

    //for previous section
    $("button.preivous").on("click", function () {
        //hidden the current section
        $(this).parent().removeClass("open");
        //show the previous one section
        $(this).parent().prev().addClass("open");
    })
    //hide or open for delivery address and credit card information
        $("div.choice").on("click", function (e) {
            var type = event.target.id;
            if (type == "true") {
                $(this).children("div.border").addClass("open");
            } else if (type == "false") {
                $(this).children("div.border").removeClass("open");

            }
        })

})

//only allow user enter integer
function IntOnly(e) {
    //the length somehow will the actual length+1
    if (e.which < 48 || e.which > 57) {
        e.preventDefault();
        return false;
    }
}


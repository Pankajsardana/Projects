// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddToCart(ItemId,Name, UnitPrice, Quantity)
{
    $.ajax({
        type: "GET",
        ContentType: "application/json; charset=utf-8",
        url: '/Cart/AddToCart/' + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (d) {
            if (d.length > 0) {
                var data = JSON.parse(d);
                var message = '<strong>' + Name + '</strong> Added to <a href="/cart">Cart</a> Successfully!';
                confirm(message);
                $("#cartCounter").text(data.CartItems.length); 
                $("#toastCart >.toast-body").html(message);
                $("#toastCart").show();

                setTimeout(function () {
                    $('#toastCart').toast('hide');
                },4000);
            }
        }



    });
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: '/Cart/GetCartCount',
        dataType: "json",
        success: function (data) {
            $("#cartCounter").text(data);
        },
        error: function (result) {
        },
    });
});


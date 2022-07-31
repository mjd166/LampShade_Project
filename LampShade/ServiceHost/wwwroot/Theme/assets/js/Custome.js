const { data } = require("jquery");

function addToCard(id, name, price, picture) {
   
    const cookieName = "cart_items";

    let products = $.cookie(cookieName);

    if (products === undefined) {
        products = [];
    }
    else {
        products = JSON.parse(products);

    }

    const count = $("#ProductCount").val();

    const CurrentCountProduct = products.find(x => x.id === id);
    
    if (CurrentCountProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(CurrentCountProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
        }

        products.push(product);


    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    UpdateCart();
}

function UpdateCart() {
    
    const cookieName = "cart_items";
    let products = $.cookie(cookieName);

    if (products !== undefined) {
        products = JSON.parse(products);
        $("#card_items_count").text(products.length);
        let cartItemWrapper = $("#cart_items_wrapper")
        if (cartItemWrapper !== undefined) {
            cartItemWrapper.html('');
            products.forEach(x => {
                const product = `<div class="single-cart-item">
                                <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCard('${x.id}')">
                                    <i class="ion-android-close"></i>
                                </a>
                                <div class="image">
                                    <a href="single-product.html">
                                        <img src="/ProductPictures/${x.picture}"
                                             class="img-fluid" alt="">
                                    </a>
                                </div>
                                <div class="content">
                                    <p class="product-title">
                                        <a href="single-product.html">محصول: ${x.name}</a>
                                    </p>
                                    <p class="count">تعداد: ${x.count}</p>
                                    <p class="count">قیمت واحد: ${x.unitPrice}</p>
                                </div>
                            </div>`;

                cartItemWrapper.append(product);

            });
        }

    }
}


function removeFromCard(id) {
    const cookieName = "cart_items";
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    if (products !== undefined) {
        

        let itemToRemove = products.findIndex(x => x.id === id);

        if (itemToRemove !== undefined) {
            products.splice(itemToRemove, 1);
            $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
            UpdateCart();
        }
    }
}


function changeCartItemCount(id, totalId, count) {
    const cookieName = "cart_items";

    let products = $.cookie(cookieName);
    products = JSON.parse(products);

    const productIndex = products.findIndex(x => x.id === id);
    if (productIndex !== undefined) {
        products[productIndex].count = count;
        const product = products[productIndex];
        const NewPrice = parseInt(product.unitPrice) * parseInt(count);
        $("#"+totalId).text(NewPrice);
        //products[productIndex].totalPrice = NewPrice;
        $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
        UpdateCart();

        const settings = {
            "url": "https://localhost:5001/api/inventory",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({ "productId": id, "count": count })
        };

        $.ajax(settings).done(function (data) {
            if (data.isStock == false) {
                const warningsDiv = $('#productStockWarnings');
                if ($(`#${id}`).length == 0) {
                    warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
                }
            } else {
                if ($(`#${id}`).length > 0) {
                    $(`#${id}`).remove();
                }
            }
        });
    }
   

}

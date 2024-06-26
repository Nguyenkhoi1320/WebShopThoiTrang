﻿function addTocart(productId, productName, price, imageUrl) {
    $.ajax({
        url: '/Cart/AddToCart',
        type: 'POST',
        data: {
            productId: productId,
            productName: productName,
            price: price,
            anhsps: imageUrl,
            quantitys: 1
        },
        success: function (response) {
            loadCartItems();
        },
        error: function (error) {
            console.log('Error: ', error);
            alert('Đã xảy ra lỗi khi thêm sản phẩm vào giỏ hàng. Vui lòng thử lại sau!');
        }
    });
}
function update(productId) {
    $.ajax({
        url: '/Cart/UpdateQuantity',
        type: 'POST',
        data: {
            productId: productId,
        },
        success: function (response) {
            loadCartItems();
        },
        error: function (error) {
            console.log('Error: ', error);
            alert('Đã xảy ra lỗi khi cập nhật số lượng sản phẩm. Vui lòng thử lại sau!');
        }
    });
}

function deleteQuantity(productId) {
    $.ajax({
        url: '/Cart/DeleteQuantity',
        type: 'POST',
        data: {
            productId: productId,
        },
        success: function (response) {
            loadCartItems();
        },
        error: function (error) {
            console.log('Error: ', error);
            alert('Đã xảy ra lỗi khi cập nhật số lượng sản phẩm. Vui lòng thử lại sau!');
        }
    });
}
/*function loadCartItems() {
    $.ajax({
        url: '/Cart/GetCartItemsJson',
        type: 'GET',
        success: function (data) {
            $('#cartItems').empty();
            if (Array.isArray(data) && data.length > 0) {
                let totalQuantity = 0;
                let totalAmount = 0;
                let rowCounter = 1;
                data.forEach(function (item) {
                    $('#cartItems').append(
                        `<tr>
                        <td>${rowCounter}</td>
                        <td>${item.productName}</td>
                        <td>
                            <div class="quantity-control">
                                <button class="quantity-btn" onclick="deletequanty(${item.productId})">-</button>
                                <span class="quantity">${item.quantity}</span>
                                <button class="quantity-btn" onclick="update(${item.productId})">+</button>
                            </div>
                        </td>
                        <td>${item.productPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
                        <td><img width="40" src="${item.imageUrl}" alt="Product Image"></td>
                    </tr>`
                    );
                    rowCounter++;
                    totalQuantity += item.quantity
                    totalAmount += item.productPrice
                });
                $('.cartLink .product-count').text(totalQuantity);
                $('.cartLink .cart-amunt').text(totalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
            } else {
                $('#cartItems').append('<p>Không có sản phẩm nào trong giỏ hàng !.</p>');
            }
        },
        error: function (error) {
            console.log('Error: ', error);
            alert('Failed to load cart items. Please try again later.');
        }
    });
}*/



function loadCartItems() {
    $.ajax({
        url: '/Cart/GetCartItemsJson',
        type: 'GET',
        success: function (data) {
            $('#cartItems').empty();
            let totalQuantity = 0;
            let totalAmount = 0;
            if (Array.isArray(data) && data.length > 0) {
                let rowCounter = 1;
                data.forEach(function (item) {
                    $('#cartItems').append(
                        `<tr>
                            <td>${rowCounter}</td>
                            <td>${item.productName}</td>
                            <td>
                                <div class="input-group">
                                    <button class="btn btn-outline-secondary" onclick="deleteQuantity(${item.productId})">-</button>
                                    <span class="quantity">${item.quantity}</span>
                                    <button class="btn btn-outline-secondary" onclick="update(${item.productId})">+</button>
                                </div>
                            </td>
                            <td>${item.productPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
                            <td><img width="40" src="${item.imageUrl}" alt="Product Image"></td>
                        </tr>`
                    );
                    rowCounter++;
                    totalQuantity += item.quantity;
                    totalAmount += item.productPrice;
                });
            }
            $('.counts').text(totalQuantity);
            $('.card-text').text(totalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
            if (data.length === 0) {
                let tong = 0;
                $('.counts').text('0');
                $('.card-text').text(tong.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
            }
        },
        error: function (error) {
            console.log('Error: ', error);
            alert('Failed to load cart items. Please try again later.');
        }
    });
}





$(document).ready(function () {
    function checkAndUpdateCart() {
        var magiamgia = $('#magiamgia').val();
        if (magiamgia === '') {
            $.ajax({
                url: '/Cart/tonggiachuagiamgia',
                method: 'GET',
                success: function (response) {
                    if (response && typeof response === 'object' && response.hasOwnProperty('totalAmount')) {
                        $('.cart-amunt').text(response.totalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
                        $('.thongbao').text("");
                    } else {
                        $('.thongbao').text("Mã giảm giá không tồn tại");
                    }
                    checkAndUpdateCart();
                },
                error: function (error) {
                    console.log(error);
                    checkAndUpdateCart();
                }
            });
        } else {
            $.ajax({
                url: '/Cart/duocgiamgia',
                method: 'GET',
                data: { magiamgia: magiamgia },
                success: function (response) {
                    if (response.discountAmount != null) {
                        $('.cart-amunt').text(response.discountAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
                        $('.thongbao').text("");
                    } else {
                        $('.thongbao').text("Mã giảm giá không tồn tại");
                    }
                    checkAndUpdateCart();
                },
                error: function (error) {
                    console.log(error);
                    checkAndUpdateCart();
                }
            });
        }
    }
    $('#magiamgia').on('input', checkAndUpdateCart);
});


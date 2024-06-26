function laycuochoithoaikhachhang() {
    $.ajax({
        url: '/Chat/laycuochoithoaikhachhang',
        type: 'POST',
        success: function (data) {
            $('.conversation-lists').html(data);
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy tin nhắn.');
        }
    });
}
function laycuochoithoainhanvien() {
    $.ajax({
        url: '/Chat/laycuochoithoainhanvien',
        type: 'POST',
        success: function (data) {
            $('.conversation-lists').html(data);
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy tin nhắn.');
        }
    });
}
$('#send-cmt').on('click', () => {
    data = {}
    data['MaPhim'] = $('.detail-title').attr('value-id')
    data['MaTaiKhoan'] = $('.rounded-circle').attr('value-id')
    data['NoiDung'] = $('.form-control-text').val()
    data['ThoiGianCmt'] = new Date($.now());
    url1 = 'https://localhost:7050/api/v1/commentapi'
    $.ajax({
        type: 'POST',
        url = url1,
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (data) {
            alter("thanhcong")
        },
        Error: function (ex) {
            alter(ex.responseText)
        }
    })

})
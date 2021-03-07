
//https://www.jq22.com/yanshi23143
$('.slider-input').jRange({
    from: 0,//最小值
    to: 20,//最大值
    step: 0.01,
    scale: ["0", "5", "10", "15", "20"],//刻度条
    showLabels: true,//显示标签
    showScale: true,//显示刻度
    //format: '%s',//设置标签格式
    //width: $(window).width() * 0.86,//宽度
    theme: "theme-blue",//主题(默认是"theme-green"绿色),还有"theme-blue"蓝色。你可以在jquery.range.less中设置
    isRange: false,//是否为范围(默认false,选择一个点),如果是true，选择的是范围,格式为'1,2'
    snap: false,//是否只允许按增值选择(默认false)
    disable: false,//是否只读(默认false),若为true,只读模式，无法选择。可以用js动态设置$('.slider').jRange('disable'); $('.slider').jRange('enable'); $('.slider').jRange('toggleDisable');
    onstatechange: function () {//数字变化的时候的回调函数
        // alert($("#slider-input").val());
    },
});
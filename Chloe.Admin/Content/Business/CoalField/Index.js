var c_Type = '1';
var c_date = '';
var color=["","red","green","blue","yellow"]
var heap = ["未入场", "一号场", "二号场", "三号场", "四号场"]
var map=[[]]

layui.use(['laydate', 'form', 'table'], function () {
    var laydate = layui.laydate;
    var form = layui.form;
    var table = layui.table;

   
    //执行一个laydate实例
    laydate.render({
        elem: '#c-date' //指定元素
        , done: function (value, date, endDate) {
            //ins1.hint(value); //在控件上弹出value值
            c_date = value
            console.log(value);
        }
    }); 


    form.on('select(c-Type)', function (data) {
        c_Type = data.value;
        console.log(c_Type);
    });

    loadtabledata(table);
    loadChart();

    $('#OK').on('click', function () {
        loadtabledata(table);
        loadChart();
    });


    
});


function loadtabledata(table) {
    for (var i = 0; i < 5; i++) {
        //第一个实例
        table.render({
            elem: `#heap${i}`
            , height: 312
            , url: url.GetMCPageData + `?date=${c_date}&heap=${i}&type=${c_Type}` //数据接口
            //, where: { Page: 1, PageSize: 10 } //如果无需传递额外参数，可不加该参数
            , method: 'get' //如果无需自定义HTTP类型，可不加该参数
            , parseData: function (res) { //将原始数据解析成 table 组件所规定的数据
                //map.push(res.Data.mdt)
                //loadChart(res.Data.mdt,i);
                return {
                    "code": '0', //解析接口状态
                    "msg": '', //解析提示文本
                    "count": res.Data.TotalCount, //解析数据长度
                    "data": res.Data.DataList //解析数据列表
                };
            }
            ,done: function (res, curr, count) {
                //如果是异步请求数据方式，res即为你接口返回的信息。
                //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度
                console.log(res);

                //得到当前页码
                console.log(curr);

                //得到数据总量
                console.log(count);
                //loadChart(res.data.mdt, i);
            }
            , page: true //开启分页
            , cols: [
                [ //标题栏
                    { align: 'center', title: heap[i], colspan: 13 } //colspan即横跨的单元格数，这种情况下不用设置field和width
                ],
                [ //表头
                    { field: 'Id', title: '', hide: true }
                    , {
                        field: 'Dh', title: '', templet: function (d) {
                            return '<div class="layui-inline" style="width:35px;height:20px;background-color:' + color[d.Dh] +'"></div>'  
                        }
                    }
                    , { field: 'Mzmc', title: '煤种' }
                    , { field: 'Cl', title: '存量' }
                    , {
                        field: 'Dq', title: '堆期', templet: function (d) {
                            return d.Dq.split('T')[0]
                        }
                    }
                    , { field: 'Cm', title: '船名' }
                    , { field: 'Hf', title: '灰' }
                    , { field: 'Hff', title: '挥发' }
                    , { field: 'Rz', title: '热值' }
                    , { field: 'Sf', title: '水分' }
                    , { field: 'T2', title: 'T2' }
                    , { field: 'Lf', title: '硫' }
                    , {
                        field: 'bj', title: '', templet: function (d) {
                            return '<button type="button" class="layui-btn  layui-btn-xs " >编辑</button>' +
                            '<button type="button" class="layui-btn  layui-btn-xs " >同步</button>'
                        } }
                ]]
        });
        
    }
}

function xdata(length) {
    var x = [];
    for (var i = 1; i <= length; i++) {
        x.push(i);
    }
    return x;
}

function loadChart() {
    for (var i = 0; i < 5; i++) {
        if (i === 0)
            continue;
        var dom = document.getElementById("container" + i);
        var myChart = echarts.init(dom);

           var option1 = {
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                   data: xdata(70)
                },
                yAxis: {
                    type: 'value'
                },
                series: [{
                    data: [null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "0", "1", "1", "1", "1", "1", "1", "1", "0", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null],
                    type: 'line',
                    areaStyle: {}
                }]
        };

        var option2 = {
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: xdata(36)
            },
            yAxis: {
                type: 'value'
            },
            series: [{
                data: [, , , , , 1, 0, 2, 1, , ,],
                type: 'line',
                areaStyle: {}
            }]
        };
        if (c_Type == "1") {
            if (option1 && typeof option1 === 'object') {
                myChart.setOption(option1);
            }
        } else {
            if (option2 && typeof option2 === 'object') {
                myChart.setOption(option2);
            }
        }
    }
}



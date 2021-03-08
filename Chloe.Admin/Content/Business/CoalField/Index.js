var c_Type = '1';
var c_date = '';
var color=["","red","green","blue","yellow"]
var heap = ["未入场", "一号场", "二号场", "三号场", "四号场"]
var map = [];
var data = [];
var model = {};

layui.use(['laydate', 'form', 'table', 'layer'], function () {
    var laydate = layui.laydate;
    var form = layui.form;
    var table = layui.table;

   
    //执行一个laydate实例
    laydate.render({
        elem: '#c-date' //指定元素
        ,done: function (value, date, endDate) {
            //ins1.hint(value); //在控件上弹出value值
            c_date = value
            console.log(value);
        }
    }); 

    laydate.render({
        elem: '#c-b-date' //指定元素
    }); 

    laydate.render({
        elem: '#c-e-date' //指定元素
    }); 
   

    //选择框
    form.on('select(c-Type)', function (data) {
        c_Type = data.value;
        console.log(c_Type);
    });

    //加载table 
    loadtabledata(table);
    //loadChart();

    //确定按钮
    $('#OK').on('click', function () {
        data = [];
        loadtabledata(table);
        //loadChart();
    });

    
});

function bj(id) {
   // data = d;
    var model = data.filter((v) => {
        return v.Id == id;
    })[0];
    console.log(model);
    //layer.msg('hello'); 
    layer.open({
        type: 1 //此处以iframe举例
        , title: '编辑'
        , area: ['75%', '50%']
        , shade: 0.3
        , maxmin: true
        , offset: 'auto'
        , content: $('#model')
        , btn: ['确定', '关闭'] 
        , yes: function (bj) {
            //layer.close(bj);
            layer.confirm('确定更新', { icon: 3, title: '提示', zIndex: layer.zIndex  }, function (index) {
              
                //layer.close(index);
                layer.closeAll();
                model.Dq = $("#dq").val();
                model.Mzmc = $("#mz").val();
                model.Cl = $("#cl").val();
                model.Cm = $("#cm").val();
                model.Hf = $("#hf").val();
                model.Hff = $("#hff").val();
                model.Rz = $("#rz").val();
                model.Sf = $("#sf").val();
                model.T2 = $("#t2").val();
                model.Lf = $("#lf").val();

                $.post(url.UpdateMC + `?type=${c_Type}`, model, function (result) {
                   var r= JSON.parse(result);
                    if (r.Status == 100) {
                        loadtabledata(layui.table);
                        layer.msg(r.Msg, {
                            time: 2000, //2s后自动关闭
                        });
                    } else {
                        layer.msg("更新错误", {
                            time: 2000, //2s后自动关闭
                        });
                    }

                    
                });

            });
        }
        , btn2: function () {
            layer.closeAll();
        }
        , zIndex: layer.zIndex //重点1
        , success: function (layero) {
            layui.laydate.render({
                elem: '#dq' //指定元素
                , value: model.Dq.split('T')[0]
                , done: function (value, date, endDate) {
                    this.value = value;
                    //ins1.hint(value); //在控件上弹出value值
                    //c_date = value
                    //console.log($("#dq").val());
                }
            }); 
            $("#mz").val(model.Mzmc);
            $("#cl").val(model.Cl);
            //$("#dq").value(model.Dq.split('T')[0]);
            $("#cm").val(model.Cm);
            $("#hf").val(model.Hf);
            $("#hff").val(model.Hff);
            $("#rz").val(model.Rz);
            $("#sf").val(model.Sf);
            $("#t2").val(model.T2);
            $("#lf").val(model.Lf);
        }
    });

}


function loadtabledata(table) {
    for (var i = 0; i < 5; i++) {
        //第一个实例
        table.render({
             bs:i
            ,elem: `#heap${i}`
            , height: 312
            , url: url.GetMCPageData + `?date=${$("#c-date").val()}&heap=${i}&type=${c_Type}` //数据接口
            //, where: { Page: 1, PageSize: 10 } //如果无需传递额外参数，可不加该参数
            , method: 'get' //如果无需自定义HTTP类型，可不加该参数
            , parseData: function (res) { //将原始数据解析成 table 组件所规定的数据
                
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
                data= data.concat(res.data);
                //加载图
                if (this.bs != 0) {
                    var aa = [];
                    if (res.data.length <= 0) {
                       
                        loadChart(aa, this.bs);
                    }
                    for (var j = 0; j < res.data.length; j++) {
                        aa.push({
                            data: res.data[j].mdt,
                            type: 'line',
                            symbol: 'none',
                            color:color[j],
                            areaStyle: {
                                opacity: 0.8,
                                color: color[j+1]
                            },
                            itemStyle: {
                                normal: {
                                    lineStyle: {
                                        color: color[j+1]
                                    }
                                }
                            }
                        });
                    }
                    loadChart(aa, this.bs);
                }
                
            }
            //, page: true //开启分页
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
                            return '<button type="button" class="layui-btn  layui-btn-xs " onClick="bj('+d.Id+')" id="bj'+i+'">编辑</button>' +
                                '<button type="button" class="layui-btn  layui-btn-xs " onClick="SynComponent(' + d.Id +')" >同步</button>'
                        } }
                ]]
        });
        
    }
}

function xdata(length) {
    var x = [];
    for (var i = 0; i <= length; i++) {
        x.push(i);
    }
    return x;
}

function loadChart(series,i) {
    
    //for (var i = 0; i < 5; i++) {
        if (i === 0)
            //continue;
            return;
        var dom = document.getElementById("container" + i);
        var myChart = echarts.init(dom);
         myChart.clear();
           var option1 = {
                xAxis: {
                    type: 'category',
                    boundaryGap: true,
                   data: xdata(81)
                },
                yAxis: {
                    type: 'value'
                },
                tooltip: {
                    trigger: 'axis'
                },
              series:series
                //series: [{
                //    data: [null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "0", "1", "1", "1", "1", "1", "1", "1", "0", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null],
                //    type: 'line',
                //    areaStyle: {}
                //}]
        };

        var option2 = {
            xAxis: {
                type: 'category',
                boundaryGap: true,
                data: xdata(36)
            },
            yAxis: {
                type: 'value'
            },
            tooltip: {
                trigger: 'axis'
            },
            series: series
            //series: [{
            //    data: [, , , , , 1, 0, 2, 1, , ,],
            //    type: 'line',
            //    areaStyle: {}
            //}]
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

       // map.push(myChart);
    //}
}

function SynComponent(id) {
    // data = d;
    var model = data.filter((v) => {
        return v.Id == id;
    })[0];
    console.log(model);
    //layer.msg('hello'); 
    layer.open({
        type: 1 //此处以iframe举例
        , title: '同步'
        , area: ['80%', '70%']
        , shade: 0.3
        , maxmin: true
        , offset: 'auto'
        , content: $('#tb_mode')
        , btn: ['确定', '关闭']
        , yes: function (bj) {
            //layer.close(bj);
            layer.confirm('确定同步', { icon: 3, title: '提示', zIndex: layer.zIndex }, function (index) {

                //layer.close(index);
                layer.closeAll();
               

            });
        }
        , btn2: function () {
            layer.closeAll();
        }
        , zIndex: layer.zIndex //重点1
        , success: function (layero) {
            console.log(layero);
            layui.table.render({
                elem: `#component`
                , height: 412
                , url: url.GetRlglCzhHyglPagedData + `?bdate=${$("#c-b-date").val()}&bdate=${$("#c-e-date").val()}` //数据接口
                //, where: { Page: 1, PageSize: 10 } //如果无需传递额外参数，可不加该参数
                , method: 'get' //如果无需自定义HTTP类型，可不加该参数
                , parseData: function (res) { //将原始数据解析成 table 组件所规定的数据

                    return {
                        "code": '0', //解析接口状态
                        "msg": '', //解析提示文本
                        "count": res.Data.TotalCount, //解析数据长度
                        "data": res.Data.DataList //解析数据列表
                    };
                }
                , done: function (res, curr, count) {
                    //如果是异步请求数据方式，res即为你接口返回的信息。
                    //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度
                   
                }
                , page: true //开启分页
                , cols: [
                    [ //表头
                        {
                            field: 'Hyrq', title: '取样日期', templet: function (d) {
                                return d.Dq.split('T')[0]
                            }
                        }
                        , { field: 'Hybh', title: '化验编号' }
                        , { field: 'Aar', title: '灰分' }
                        , {
                            field: 'Var', title: '挥发分'
                        }
                        , { field: 'Qnetar', title: '热值' }
                        , { field: 'Mar', title: '水分' }
                        , { field: 'Star', title: '硫分' }
                    ]]
            });

        }
    });
}
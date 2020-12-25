var cityName="";
var map = new BMap.Map("allmap");                        // 创建Map实例
map.centerAndZoom(new BMap.Point(119.318062,26.089746),15);     // 初始化地图,设置中心点坐标和地图级别
map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
map.enableScrollWheelZoom();                            //启用滚轮放大缩小
map.addControl(new BMap.MapTypeControl());          //添加地图类型控件
map.setCurrentCity("全国");          // 设置地图显示的城市 此项是必须设置的


//var myCity = new BMap.LocalCity();
//    myCity.get(myFun);

//设置当前位置
function MyLocalCity(){
    var myCity = new BMap.LocalCity();
    myCity.get(myFun);    
}
//获取当前ip的城市名称
function myFun(result){
    var cityName = result.name;
    map.setCenter(cityName);
}
//鼠标移动更新经纬度
map.addEventListener("mousemove",function(e){var pt = e.point;
	    window.external.ruternPoint(""+e.point.lng , "" + e.point.lat);});
//鼠标单击获取经纬度
var gc = new BMap.Geocoder();
map.addEventListener("click",function(e){
	var pt = e.point;
	    window.external.ruternPoint(""+e.point.lng , "" + e.point.lat);
	gc.getLocation(pt, function(rs){
        var addComp = rs.addressComponents;
        var address=addComp.province+ addComp.city  + addComp.district + addComp.street+ addComp.streetNumber;
        //调用C#方法
        //var str = window.external.getLon_lat(e.point.lng ,e.point.lat);
	    window.external.getLon_lat("经度："+e.point.lng + ",维度" + e.point.lat ,address);
    });        
});
//快速定位
function QuickSet(lon,lat)
{
    map.clearOverlays();
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    traget.style.display="None";
    traget.style.width="0%";
    traget1.style.width="100%";
    map.centerAndZoom(new BMap.Point(lon,lat),15);  //初始化时，即可设置中心点和地图缩放级别。
    var myIcon = new BMap.Icon("direct_mapsearch_loc.png", new BMap.Size(20,20));
    var marker = new BMap.Marker(new BMap.Point(lon,lat),{icon:myIcon});  // 创建标注
    map.addOverlay(marker);     
}
//添加右键菜单栏
var contextMenu = new BMap.ContextMenu();
var txtMenuItem = [
  {
      text: '放大',
      callback: function() { map.zoomIn() }
  },
  {
      text: '缩小',
      callback: function() { map.zoomOut() }
  },
  {
      text: '放置到最大级',
      callback: function() { map.setZoom(18) }
  },
  {
      text: '查看全国',
      callback: function() { map.setZoom(4) }
  },
  {
      text: '在此添加标注',
      callback: function(p) {
          var marker = new BMap.Marker(p), px = map.pointToPixel(p);
          map.addOverlay(marker);
      }
  } 
 ];
for (var i = 0; i < txtMenuItem.length; i++) {
    contextMenu.addItem(new BMap.MenuItem(txtMenuItem[i].text, txtMenuItem[i].callback, 100));
    if (i == 1 || i == 3) {
        contextMenu.addSeparator();
    }
}
map.addContextMenu(contextMenu);
//js调用C#方法测试
 function  InvokeFunc()
 {
	//alert("欢迎使用百度地图！");
    var str = window.external.getLon_lat('0','0');
 }

function altStr() {
    //alert("欢迎使用百度地图！");
    var cp = map.getCenter();
    return cp.lng + "," + cp.lat;
}
//添加标注
function AddMarker(lon,lat)
{
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    traget.style.display="None";
    traget.style.width="0%";
    traget1.style.width="100%";
    map.centerAndZoom(new BMap.Point(lon,lat),15);  //初始化时，即可设置中心点和地图缩放级别。
    var pt = new BMap.Point(lon,lat);
    var myIcon = new BMap.Icon("love.png", new BMap.Size(32,32));
    var marker = new BMap.Marker(pt,{icon:myIcon});  // 创建标注
    map.addOverlay(marker);              // 将标注添加到地图中
    marker.setAnimation(BMAP_ANIMATION_BOUNCE);//跳动的动画

    //创建信息窗口）
    var address="";
    gc.getLocation(pt, function(rs){
        var addComp = rs.addressComponents;
        var infoWindow = new BMap.InfoWindow("<br/><p style='font-size:14px;'>经度："+lon+"；维度："+lat+"</p><p style='font-size:14px;'>"+addComp.province +addComp.city + addComp.district + addComp.street +addComp.streetNumber+"</p>");
        marker.addEventListener("click", function(){this.openInfoWindow(infoWindow);});
    });      
    
}
    
//步行线路查询函数
function Walking(address1,address2) {
    map.clearOverlays();
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    //var walking = new BMap.WalkingRoute(map, {renderOptions:{map: map, autoViewport: true}});
    var walking = new BMap.WalkingRoute(map, {renderOptions: {map: map, panel: "r-result", autoViewport: true}});
    walking.search(cityName+address1, cityName+address2);
    setTimeout("var traget=document.getElementById('r-result');var traget1=document.getElementById('allmap');"+
    "if((document.getElementById('r-result').innerHTML)=='')"+
    "{var traget=document.getElementById('r-result');traget.style.display='none';traget.style.width='0%';traget1.style.width='100%';alert('没有查询到结果');}"+
    "else{traget.style.display='';traget.style.width='20%'; traget1.style.width='80%';}"
    ,3500)
    //3.5秒后等待加载完成再显示数据窗口
    
    //alert(traget.innerHTML);    
}
//公交线路查询函数
function Transit(address1,address2) {
    map.clearOverlays();
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    var transit = new BMap.TransitRoute(map, {
        renderOptions: {map: map,panel:"r-result"},
        policy:  
        BMAP_TRANSIT_POLICY_LEAST_TIME	//最少时间。
        //BMAP_TRANSIT_POLICY_LEAST_TRANSFER	最少换乘。
        //BMAP_TRANSIT_POLICY_LEAST_WALKING	最少步行。
        //BMAP_TRANSIT_POLICY_AVOID_SUBWAYS	不乘地铁。(自 1.2 新增)
    });
    transit.search(cityName+address1, cityName+address2);
    
    setTimeout("var traget=document.getElementById('r-result');var traget1=document.getElementById('allmap');"+
    "if((document.getElementById('r-result').innerHTML)=='')"+
    "{var traget=document.getElementById('r-result');traget.style.display='none';traget.style.width='0%';traget1.style.width='100%';alert('没有查询到结果');}"+
    "else{traget.style.display='';traget.style.width='20%'; traget1.style.width='80%';}"
    ,3500)
    //3.5秒后等待加载完成再显示数据窗口
}
//驾车线路查询函数
function Driving1(address1,address2) {
    map.clearOverlays();
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    var driving = new BMap.DrivingRoute(map, {onSearchComplete:money,renderOptions:{map: map, panel: "r-result", autoViewport: true}}); 
    //var driving = new BMap.DrivingRoute(map, {renderOptions:{map: map, autoViewport: true}});
    driving.search(cityName+address1, cityName+address2);
    setTimeout("var traget=document.getElementById('r-result');var traget1=document.getElementById('allmap');"+
    "if((document.getElementById('r-result').innerHTML)=='')"+
    "{var traget=document.getElementById('r-result');traget.style.display='none';traget.style.width='0%';traget1.style.width='100%';alert('没有查询到结果');}"+
    "else{traget.style.display='';traget.style.width='20%'; traget1.style.width='80%'; }"   //计算出白天的打车费用的总价   
    ,3500);
    //3.5秒后等待加载完成再显示数据窗口
    function money(rs){
        //判断是否查询到驾车结果
        if (driving.getStatus() == BMAP_STATUS_SUCCESS) {
            alert("从"+address1+"到"+address2+"打车总费用为："+rs.taxiFare.day.totalFare+"元","");
        }
   }
}
//可拖动驾车导航
function Driving(address1,address2) {
    map.clearOverlays();
    var transit = new BMap.DrivingRoute(map, {
        renderOptions: {map: map,panel: "r-result"},            
        onMarkersSet: function(pois){
            var start = pois[0].marker, end = pois[1].marker;
            start.enableDragging();//开启起点拖拽功能
            end.enableDragging();//开启终点拖拽功能
            start.addEventListener("dragend",function(e){                   
                map.clearOverlays();
                transit.search(e.point,end.getPosition());                   
            });
            end.addEventListener("dragend",function(e){                    
                map.clearOverlays();                      
                transit.search(start.getPosition(),e.point);                  
            });
        }
    });
    transit.search(cityName+address1,cityName+address2);
}
//搜索周边
function SearchZhoubian(name) {
    map.clearOverlays();
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    var local = new BMap.LocalSearch(map, {
      renderOptions:{map: map}
    });
    local.searchInBounds(name, map.getBounds());
    map.addEventListener("dragend",function(){
//    map.clearOverlays();
//    local.searchInBounds(name, map.getBounds());
    traget.style.display="none";
    traget.style.width="0%";
    traget1.style.width="100%";
});
}
//全国搜索
function SearchQuanguo(name) {
    map.clearOverlays();
    var traget=document.getElementById("r-result");
    var traget1=document.getElementById("allmap");
    var local = new BMap.LocalSearch("全国", {
        renderOptions: {
            map: map,
            panel : "r-result",
            autoViewport: true,
            selectFirstResult: false
        }
    });
    local.search(name);
    traget.style.display="";
    traget.style.width="20%";
    traget1.style.width="80%";

}
//本地搜索
function LocalSearch(name) {
    map.clearOverlays();
    var local = new BMap.LocalSearch(map, {
        renderOptions:{map: map}
    });
    local.search(name);
}
function Biaozhu() {
    map.clearOverlays();
    var point = new BMap.Point(119.318062,26.089746);
    map.centerAndZoom(point, 15);
    var marker = new BMap.Marker(point);  // 创建标注
    map.addOverlay(marker);              // 将标注添加到地图中
    marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
}
//鼠标测距
function MouseDis(){
    var myDis = new BMapLib.DistanceTool(map);
    myDis.open();  //开启鼠标测距
   
}
//地址解析
function AddressToPoint(address)
{
    map.clearOverlays();
    var myGeo = new BMap.Geocoder();
    // 将地址解析结果显示在地图上,并调整地图视野
    myGeo.getPoint(cityName+address, function(point){
    if (point) {
    map.centerAndZoom(point, 16);
    var myIcon = new BMap.Icon("mark1.png", new BMap.Size(64,64));
    var marker1 = new BMap.Marker(new BMap.Point(point.lng, point.lat),{icon:myIcon});  // 创建标注
    map.addOverlay(marker1);              // 将标注添加到地图中

    //创建信息窗口
    var infoWindow1 = new BMap.InfoWindow("<br/><p style='font-size:14px;'>经度："+point.lng+"；维度："+point.lat+"</p><p style='font-size:14px;'>"+address+"</p>");
    marker1.addEventListener("click", function(){this.openInfoWindow(infoWindow1);});
    //map.addOverlay(new BMap.Marker(point));
    }
    }, cityName);
}


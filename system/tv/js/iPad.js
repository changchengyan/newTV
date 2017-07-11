var iPad = 
{		
	
	Config:
	{
		debug:false,
		path:"/system/",
		nullHTML: "/system/null.html",
		zindex:1000,
		demopath: "/system/html/",
		alertHTML:"",
		menuDataPath:"",
		openWindowDom:[],
		openWindowDomTmp:[],
		FileServicesURL:"http://f3.5rs.me/",
		services:"/"
	},
	Frame:
	{
		Reset:function()
		{
			
		}
	},
	UI:
	{	
		Init:function()
		{
			iPad.UI.Core.Tab.Search.Init();
		},
		//成功提示
		Message:function(msg)
		{			
			var app = function()
			{				
				iPad.Core.Ajax
				(
					{
						url:iPad.Config.demopath + "Message.html",
						callback:function(s)
						{	
					
							var dom = top.document.createElement("div");
							dom.style.position = "fixed";
							dom.style.zIndex = 1000;			
							s = s.replace(/#title/g,msg);					
							dom.innerHTML = s;
							top.document.body.appendChild(dom);
							top.iPad.Dom.ToCenter(dom);
							setTimeout(function(){dom.parentNode.removeChild(dom);},3000);
							
						},
						data:null						
					}
				
				);
				
				
			}
			document.body?app():iPad.Listener.Add(window,"load",app);
		},
		//失败提示
		ErrorMessage:function(msg)
		{			
			var app = function()
			{				
				iPad.Core.Ajax
				(
					{
						url:iPad.Config.demopath + "ErrorMessage.html",
						callback:function(s)
						{	
							var w=250,h=60;							
							var dom = top.document.createElement("div");
							dom.style.position = "fixed";
							dom.style.zIndex = 1000;
							dom.style.width = w+"px";
							dom.style.height = h+"px";						
							s = s.replace(/#title/g,msg);
							s = s.replace(/#width/g,w+"px");
							s = s.replace(/#height/g,h+"px");						
							dom.innerHTML = s;
							top.document.body.appendChild(dom);
							top.iPad.Dom.ToCenter(dom);
							setTimeout(function () { dom.parentNode.removeChild(dom); }, 2000);
						},
						data:null						
					}
				
				);
				
				
			}
			document.body?app():iPad.Listener.Add(window,"load",app);
		},
		//弹出操作界面
		OpenWindow:function(o)
		{		
		
			var app = function()
			{				
				iPad.Core.Ajax
				(
					{
						url:iPad.Config.demopath + "OpenWindow.html",
						callback:function(s)
						{	
							var max_width = top.document.documentElement.clientWidth-60;	
							var max_height = top.document.documentElement.clientHeight-60;
							if(o.width>max_width){o.width = max_width;}
							if(o.height>max_height){o.height = max_height;}
							
							s = s.replace(/#title/g,o.title);	
							s = s.replace(/#url/g,o.url);	
							var z=1000;
							var width =  o.width + "px",height =  o.height+"px";
							if(o.width<=1)
							{
								width = o.width * 100 + "%";
							}
							if(o.height<=1)
							{
								height = (top.document.documentElement.clientHeight*o.height)+"px";								
							}
							
							
							
							
							var win = top.document.createElement("div");
							win.style.position = "fixed";
							win.style.zIndex = z + top.iPad.Config.openWindowDom.length;
							win.className = "openwindow";
							top.document.body.appendChild(win);							
							
							var mask = top.document.createElement("div");
							mask.style.position = "fixed";
							mask.style.zIndex = 1;
							mask.className = "mask-canvas";
							win.appendChild(mask);							
							
							var dom = top.document.createElement("div");
							dom.style.position = "fixed";
							dom.style.zIndex = 2;
							dom.innerHTML = s;
							win.appendChild(dom);							
							top.iPad.Config.openWindowDom.push(win);	
							
							
							dom.style.width = width;
							var iframe = dom.getElementsByTagName("IFRAME")[0];
							iframe.style.height = height;
							top.iPad.Dom.ToCenter(dom);
							iframe.src = iPad.Core.Rurl(o.url);
							iframe.setAttribute("name","OpenIframe"+top.iPad.Config.openWindowDom.length);
							iframe.setAttribute("id","OpenIframe"+top.iPad.Config.openWindowDom.length);
							
						},
						data:null						
					}
				
				);
				
				
			}
			document.body?app():iPad.Listener.Add(window,"load",app);
			
		},
		CloseWindow:function()
		{
			var leng = top.iPad.Config.openWindowDom.length;
			top.iPad.Config.openWindowDomTmp = [];
			if(leng>0)
			{
				var overdom = top.iPad.Config.openWindowDom[leng-1];				
				top.iPad.Config.openWindowDom.pop();
				overdom.parentNode.removeChild(overdom);				
			}	
			
		},
		ReloadWindow:function(backIndex)
		{
			// backIndex = 0,1,2,...n
			// backIndex 为 0时，表示刷新最后弹出的窗口，1 则是最后弹出的窗口的上一个窗口，以此类推
			var win = iPad.UI.GetWindow(backIndex);
			if(win!=null){win.location.reload();}			
		},
		GetWindow:function(backIndex)
		{
			// backIndex = 0,1,2,...n
			// backIndex 为 0时，表示刷新最后弹出的窗口，1 则是最后弹出的窗口的上一个窗口，以此类推

			var leng = top.iPad.Config.openWindowDom.length;	
	
			if(leng > backIndex)
			{
				var overdom = top.iPad.Config.openWindowDom[leng - 1 - backIndex];	
				var iframe = overdom.getElementsByTagName("IFRAME")[0];
				//var win = top.frames[iframe.getAttribute("name")];
				var win = iframe.contentWindow;
				return  win;
			}	
			else
			{
				return null;	
			}		
		},
		Select:function(config)
		{
			//参考 <script>iPad.UI.Select({id:"china_province_id",name:"china_province_id",value:"<%=merchantvo.china_province_id%>",method:"ChinaProvinceBO.getOption",cvs:"china_province_id_cvs",default:{value:"",text:"请选择"},child:{...}});
			//支持 无限子级 $value 作为父ID值发送
			var selectHTML = [];
			selectHTML.push('<select name="'+config.name+'" id="'+config.id+'" class="'+config.class+'" ');
			if(config.child || config.change)
			{
				selectHTML.push(' onchange=\'');
				if(config.child)
				{
					var changecode = 'iPad.UI.Select('+JSON.stringify(config.child).replace("'","\'").replace('$value','"+this.value+"')+');';
					selectHTML.push(changecode);
				}
				if(config.change)
				{
					selectHTML.push(config.change);
				}				
				selectHTML.push('\' ');
			}
			selectHTML.push(' >');
			if(config.default)
			{
				selectHTML.push('<option value="'+config.default.value+'">'+config.default.text+'</option>');
			}	
			var createSelect = function(createChildValue)
			{
				selectHTML.push('</select>');	
				iPad.Dom.Get(config.cvs).innerHTML = selectHTML.join("");
				
				var overselect = iPad.Dom.Get(config.id);
				//模拟onchange
				if (overselect.fireEvent)
				{
					overselect.fireEvent('onchange');
				}
				else
				{
					overselect.onchange();
				}
			}
			
			if(config.method)
			{
				iPad.Core.Ajax
				(
					{
						url:iPad.Config.services + "?method="+config.method,
						callback:function(s)
						{	
							
							var json = iPad.Core.ToJson(s);
							var key = json.key;
							var list = json.values;							
							var createChildValue = null;
							for(var i=0;i<list.length;i++)
							{
								selectHTML.push('<option value="'+list[i][key.id]+'"');
								if(config.value==list[i][key.id])
								{
									selectHTML.push(' selected ');
									createChildValue = list[i][key.id];
								}
								selectHTML.push('>'+list[i][key.text]+'</option>');
							} 
							createSelect(createChildValue);
						},
						data:null						
					}
				
				);
			}
			else
			{
				createSelect();
			}			
		},
		//重定义alert	
		Alert:function(msg)
		{		
			var app = function()
			{				
				iPad.Core.Ajax
				(
					{
						url:iPad.Config.demopath + "Alert.html",
						callback:function(s)
						{	
							
							var w=400,h=200;							
							var dom = top.document.createElement("div");
							dom.style.position = "fixed";
							dom.style.zIndex = 1000;
							dom.style.width = w+"px";
							dom.style.height = h+"px";						
							s = s.replace(/#title/g,msg);
							s = s.replace(/#width/g,w+"px");
							s = s.replace(/#height/g,h+"px");						
							dom.innerHTML = s;
							top.document.body.appendChild(dom);
							top.iPad.Dom.ToCenter(dom);
						},
						data:null						
					}
				
				);
				
				
			}
			document.body?app():iPad.Listener.Add(window,"load",app);
		},
		Wait:
		{
			Show:function()
			{
				var div = top.document.createElement("div");
				div.className = "wait";
				div.innerHTML = "<div class=\"txt\">正在处理请求，请稍候...</div>";
				top.document.body.appendChild(div);							
				setTimeout
				(
					function()
					{
						iPad.UI.Wait.Hide(div);
					},
					10000
				);
				return div;
			},
			Hide:function(dom)
			{				
				dom.parentNode.removeChild(dom);
			}	
		},
		Upload:function(config)
		{
			var html = [];
			html.push('<input type="file" name="myfile'+config.name+'"  id="myfile'+config.id+'" accept="image/jpeg,image/png,image/gif"  onchange="iPad.Core.Upload({id:\''+config.id+'\',name:\''+config.name+'\',inputFile:this,completeFuction:'+config.callback+'})" style="display:none;"  />');
			
			html.push('<label for="myfile'+config.id+'" >');
			html.push('<div class="upload_cvs" >');
			html.push('<div id="imagecvs'+config.id+'" style="'+config.show+'; ">');
			if(config.src != undefined && config.src!="" )
			{
			html.push('<img src="'+config.src+'" style="width:100%;"/>');	
			}
			else
			{
			html.push('<div class="upload_button">'+config.text+'</div>');
			}
			html.push('</div>');
			html.push('</div>');
			html.push('</label>');
			
			html.push('<div id="progress'+config.id+'" class="upload_progress"></div>');
			html.push('<input type="hidden" name="'+config.name+'" id="'+config.id+'" ');
			if(config.src != undefined && config.src!="" )
			{
				html.push(' value="'+config.src+'" ');	
			}
			if(config.datatype && config.msg)
			{
				html.push(' dataType="'+config.datatype+'" msg="'+config.msg+'" ');
			}
			html.push(' />');
			document.write(html.join(""));			
		},
		UploadBack:function(id,name,json)
		{
			var imgcvs = iPad.Dom.Get("imagecvs"+id);
			imgcvs.innerHTML = '<img src="'+json.path+'" style="width:100%;"/>';
			
			var input = iPad.Dom.Get(id);
			input.value = json.path;
		},
		UploadFile:function(config)
		{
			var html = [];
			html.push('<input type="file" name="myfile'+config.name+'"  id="myfile'+config.id+'" accept="'+config.accept+'"  '+(config.multiple?'multiple="multiple"':'')+'  onchange="iPad.Core.UploadFile({id:\''+config.id+'\',name:\''+config.name+'\',file_url:\''+config.file_url+'\',file_name:\''+config.file_name+'\',file_size:\''+config.file_size+'\',file_type:\''+config.file_type+'\',inputFile:this,completeFuction:'+config.callback+'})" style="display:none;"  />');
			
			html.push('');
			html.push('<div class="upload_file_cvs" >');
			html.push('<div style="border-bottom:1px solid #ddd; padding:0px 0px 10px 0px; text-align:center;">');
			html.push('<label for="myfile'+config.id+'" ><span class="button_txt">'+config.text+'</span></labe>');
			html.push('</div>');
			html.push('<div id="filecvs'+config.id+'" style="'+config.show+';overflow:auto;">');
			
			/*
			if(config.src != undefined && config.src!="" )
			{
			html.push('<img src="'+config.src+'" style="width:100%;"/>');	
			}
			else
			{
			html.push('<div class="upload_button">'+config.text+'</div>');
			}
			*/
			
			html.push('</div>');
			html.push('</div>');
			html.push('');
			
			html.push('<div id="progress'+config.id+'" class="upload_progress"></div>');
			
			if(config.src != undefined && config.src!="" )
			{
				html.push(' value="'+config.src+'" ');	
			}
			if(config.datatype && config.msg)
			{
				html.push(' dataType="'+config.datatype+'" msg="'+config.msg+'" ');
			}
			html.push('');
			document.write(html.join(""));			
		},
		UploadFileBack:function(changeo,json)
		{
			var size = Math.round((json.filesize/1024/1024)*100)/100;
			var imgcvs = iPad.Dom.Get("filecvs"+changeo.id);
			var html = [];
			html.push('<div style="padding:10px 0px 10px 0px;border-bottom:1px solid #ddd;">');	
			html.push('<div style="width:80%;float:left;">');
				html.push('<div>');	
				html.push(json.filename);
				html.push('</div>');
				html.push('<div style="font-size:12px;color:#999;">');	
				html.push(json.path+'<br/>');
				html.push('大小：'+(size)+'MB<br/>');
				html.push('类型：'+json.filetype);
				html.push('</div>');
				
			html.push('</div>');				
			html.push('<div style="width:20%;float:right;text-align:right; padding:10px 10px 0px 0px;">删除</div>');
			html.push('<div class="clear"></div>');
			html.push('<input type="hidden" name="'+changeo.file_url+'" id="'+changeo.file_url+'" value="'+json.path+'" />');
			html.push('<input type="hidden" name="'+changeo.file_name+'" id="'+changeo.file_name+'" value="'+json.filename+'" />');
			html.push('<input type="hidden" name="'+changeo.file_size+'" id="'+changeo.file_size+'" value="'+json.filesize+'" />');
			html.push('<input type="hidden" name="'+changeo.file_type+'" id="'+changeo.file_type+'" value="'+json.filetype+'" />');
			
			html.push('</div>');
			
			imgcvs.innerHTML +=html.join("");
		},
		Mask:
		{
			Show:function()
			{
				var div = top.iPad.Dom.Get("iPadMaskCanvas");
				if(!div)
				{
					
					div = top.document.createElement("div");
					div.className = "mask-canvas";
					div.id = "iPadMaskCanvas";
					top.document.body.appendChild(div);
				}
				div.style.display = "block";
				return div;
			},
			Hide:function()
			{
				var div = top.iPad.Dom.Get("iPadMaskCanvas");
				if(div)
				{					
					div.style.display = "none";
				}

			}
			
			
		},
		Core:
		{
			Tab:
			{
				Search:
				{
					Init:function()
					{
						var tabs = document.getElementsByClassName("table_search_area");
						for(var i=0;i<tabs.length;i++)
						{
							iPad.UI.Core.Tab.Search.Draw(tabs[i]);
						}
					},
					Draw:function(tab_dom)
					{
						var overindex = tab_dom.getAttribute("overindex")||1;
						iPad.UI.Core.Tab.Search.Show(tab_dom,overindex);						
						var menus = tab_dom.getElementsByClassName("menu")[0].getElementsByTagName("A");						
						for(var i=0;i<menus.length;i++)
						{					
							//alert(menus[i].innerHTML);
							var menu_node = menus[i];
							iPad.Listener.Add
							(
								menus[i],
								"click",
								function()
								{
									iPad.UI.Core.Tab.Search.Click(tab_dom);
								}
							);
						}
						
					},
					Click:function(tab_dom)
					{
						var e = iPad.Core.EventElement();
						var menus = tab_dom.getElementsByClassName("menu")[0].getElementsByTagName("A");
						for(var i=0;i<menus.length;i++)
						{
							var menu_node = menus[i];
							if(menu_node==e)
							{
								iPad.UI.Core.Tab.Search.Show(tab_dom,i);
								break;
							}
						}
						
					},
					Show:function(tab_dom,overindex)
					{
						tab_dom.setAttribute("overindex",overindex);
						var nodes = tab_dom.getElementsByClassName("node");
						for(var i=0;i<nodes.length;i++)
						{
							nodes[i].style.display = overindex==i?"block":"none";							
						}
						var menus = tab_dom.getElementsByClassName("menu")[0].getElementsByTagName("A");
						for(var i=0;i<menus.length;i++)
						{
							var menu_node = menus[i].className = i==overindex?"over":"out";
						}
						
						
					}
					
				}
				
			}		
		}
		
	},
	Error:
	{			
		HandleError:function(msg,url,l)
		{
			//alert("this.a="+this.iPad);
			var txt="";
			txt="There was an error on this page.\n\n";
			txt+="Error: " + msg + "\n";
			txt+="URL: " + url + "\n";
			txt+="Line: " + l + "\n\n";
			txt+="Click OK to continue.\n\n";
			alert(txt);
			return true;
		},
		AjaxError:function(o)
		{
			//alert(o);	
		}
	},
	Listener:
	{
		Add:function(obj,ev,fun)
		{
			try{obj.attachEvent("on"+ev,fun);}catch(e){obj.addEventListener(ev,fun,true);}
		}
	},
	Dom:
	{
		Get:function(id)
		{
			return document.getElementById(id);	
		},
		GetDom:function(canvas,tagName,id)
		{
			var dom = null;
			var doms = canvas.getElementsByTagName(tagName);
			for(var i =0;i<doms.length;i++)
			{
				if(doms[i].id==id)
				{
					dom = doms[i];
					break;
				}
			}
			return dom;
		},
		GetSuperDom:function(tagName,dom)
		{
			while(dom.parentNode!=null)
			{
				dom = dom.parentNode;				
				if(dom.nodeName == tagName)
				{
					return dom;
				}
			}
		},
		ToCenter:function(dom)
		{			
			
			dom.style.top = ((document.documentElement.clientHeight - dom.clientHeight)/2)*0.9 + "px";	
			dom.style.left = ((document.documentElement.clientWidth - dom.clientWidth)/2) + "px";	
		},
		Range:
		{
			width:function(dom)
			{
				return dom.clientWidth;
			},
			height:function(dom)
			{
				return dom.clientHeight;
			}			
		}
	},
	Page:
	{
		Init:function()
		{
		    iPad.Page.Listtopsize.Init();			
			if(window.name=="mainFrame" && window.location.toString().indexOf("/manage/default.aspx")==-1)
			{				
				top.iPad.Core.setCookie("mainFrameURL",window.location);
			}
		},
		Listtopsize:
		{
			Init: function ()
			{
				var topsizes = document.getElementsByName("topsize");
				for (var i = 0; i < topsizes.length; i++)
				{
					iPad.Page.Listtopsize.Attribute(topsizes[i]);
				}
			},
			Attribute: function (topsize)
			{
				topsize.setAttribute("onblur", "UpdateTopize(this)");
			}
		}
		
	},
	Form:
	{		
		Submit:function(o)
		{
			
			if(iPad.Form.CheckData(o.form))
			{
				//显示等待
			    var dom = top.iPad.UI.Wait.Show();
				//提交数据
				iPad.Core.Ajax
				(
					{
						url:o.url,
						callback:function(s)
						{
							if(o.callback)
							{
								var json = iPad.Core.ToJson(s);
								
								//关闭等待
								top.iPad.UI.Wait.Hide(dom);
								if(json.success)
								{
								    if (o.success != "" && o.success != null)
									{
										top.iPad.UI.Message(o.success);
									}
								}
								else
								{
								    if (json.message != "" && json.message != null) {
								        top.iPad.UI.Message(json.message);
								    }
								    else if (o.error != "" && o.error != null)
								    {
								        top.iPad.UI.Message(o.error);
									}									
								}
								
								if(o.callback)
								{
									o.callback(json);
								}
							}
							
						},
						data:iPad.Form.GetData(o.form)						
					}				
				);
				
			}			
		},
		GetData:function(form)
		{
			var fmpara = [];
			if(form)
			{
				for(var i=0;i<form.elements.length;i++)
				{
					if(form.elements[i].nodeName=="INPUT" || form.elements[i].nodeName=="TEXTAREA" || form.elements[i].nodeName=="SELECT" )
					{
						if(form.elements[i].type=="checkbox" || form.elements[i].type == "radio")
						{
							if(form.elements[i].checked)
							{
								fmpara.push(form.elements[i].name + "=" + encodeURIComponent (form.elements[i].value));
							}
						}
						else
						{
							fmpara.push(form.elements[i].name + "=" + encodeURIComponent (form.elements[i].value));
						}
					}	
				}
			}
			return fmpara.join("&");			
		},
		CheckData:function(form)
		{
			var valitator = new iPad.Core.Validator();
			return valitator.Validate(form,1);
			
		}		
	},
	Core:
	{
		init:function()
		{
			//重写alert
			//window.alert = function(msg){iPad.UI.Alert(msg);};	
			//捕获全局异常		
			//onerror = iPad.Error.HandleError;	
			iPad.Page.Init();
			iPad.UI.Init();
			iPad.Listener.Add(window,"reset",iPad.Core.Reset);
			
		},				
		Range:
		{	
			height:function()
			{
				return document.documentElement.clientHeight + document.documentElement.scrollTop;
			},
			width:function()
			{
				return document.documentElement.clientWidth + document.documentElement.scrollLeft;
			}		
		},
		//获得DOM Left Top 像素值
		Point:function(dom)
		{
			var p = new Object();			
			p.x = 0;
			p.y = 0;
			while(dom.offsetParent)
			{
				p.y += parseInt(dom.offsetTop);
				p.x +=  parseInt(dom.offsetLeft);
				dom = dom.offsetParent;
			}	
			return p;
		},
		browserIsIE:navigator.userAgent.toLowerCase().indexOf("msie") > -1,
		browserIsFirefox:navigator.userAgent.toLowerCase().indexOf("firefox") > -1,
		browserIsChrome:navigator.userAgent.toLowerCase().indexOf("chrome") > -1,
		browserIs360:navigator.userAgent.toLowerCase().indexOf("360") > -1,
		ToJson:function(s)
		{
			try{return eval("("+s+")");}catch(e){return null;}
		},
		Rurl:function(url)
		{
			if(url.indexOf("javascript")==-1 && url.indexOf("random=")==-1)
			{
				var cd = new Date();	
				var randomStr =  cd.getYear()+""+cd.getMonth()+""+cd.getDate()+""+cd.getHours()+""+cd.getMinutes()+""+cd.getSeconds()+Math.random()*20 + 1;
				url += url.indexOf("?")>-1?"&random="+randomStr:"?random="+randomStr;
			}
			return url;
		},
		lastReplace: function (str, oldString, newString)
		{
		    var str0 = str.substring(0, str.lastIndexOf(oldString));
		    var str1 = str.substring(str.lastIndexOf(oldString) + oldString.length, str.length);
		    return (str0 + newString + str1);
		},
		isFloatOrIsInt: function (num)
		{
            var freg = /^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/;
            var ireg = /^\d+$/;
            if (freg.test(num) || ireg.test(num)) {
                return true;
            } else {
                return false;
            }
		},
		getCookieVal: function (offset)
		{
		    var endstr = document.cookie.indexOf(";", offset);
		    if (endstr == -1)
			{
		        endstr = document.cookie.length;
			}
		    return unescape(document.cookie.substring(offset, endstr));
		},
		getCookie: function (name)
		{
		    var arg = name + "=";
		    var alen = arg.length;
		    var clen = document.cookie.length;
		    var i = 0;
		    while (i < clen)
			{
		        var j = i + alen;
		        if (document.cookie.substring(i, j) == arg)
				{
		            return this.getCookieVal(j);
				}
		        i = document.cookie.indexOf(" ", i) + 1;
		        if (i == 0)
				{
		            break;
				}
		    }
		    return null;
		},
		setCookie: function (name, value)
		{
		    var argv = this.setCookie.arguments;
		    var argc = this.setCookie.arguments.length;
		    var expires = new Date();
		    expires.setTime(expires.getTime() + (3650 * 24 * 60 * 60 * 1000));
		    var path = (3 < argc) ? argv[3] : null;
		    var domain = (4 < argc) ? argv[4] : null;
		    var secure = (5 < argc) ? argv[5] : false;
		    document.cookie = name + "=" + escape(value) +
              ((expires == null) ? "" : ("; expires=" + expires.toGMTString())) +
              ((path == null) ? "" : ("; path=" + path)) +
              ((domain == null) ? "" : ("; domain=" + domain)) +
             ((secure == true) ? "; secure" : "");
		},
		Http:function()
		{
			return this.browserIsIE ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest();
		},
		Ajax:function(o)
		{
			var Http = this.Http();				
			if(o.data)
			{
				Http.open("POST",this.Rurl(o.url), true); 
				Http.setRequestHeader("Content-Type","application/x-www-form-urlencoded");	
				Http.setRequestHeader("encoding","utf-8");
				Http.setRequestHeader("Content-length", o.data.length);
			}
			else
			{					
				Http.open("GET",encodeURI(this.Rurl(o.url)), true); 
				Http.setRequestHeader("encoding","utf-8");				
			}
			Http.onreadystatechange = function()
			{
				if (Http.readyState == 4) 
				{						
					switch (Http.status)
					{
						case 0:
						case 400:
							//alert("XmlHttpRequest status: [400] Bad Request");
							break;			
						case 404: // Not Found
							//alert(this.parent);
							//caller.Error.AjaxError("404");// alert("XmlHttpRequest status: [404] \nThe requested URL \""+o.url+"\" was not found on this server.");
							break;	
						case 500: // Not Found
							alert("XmlHttpRequest status: [500] ");
							break;		
						case 503: // Service Unavailable
							alert("XmlHttpRequest status: [503] Service Unavailable");
							break;			
						default:
							/*if(textStatus="timeout")
							alert('Ajax request timeout，please try again！'); 
							else
							alert("XmlHttpRequest status: [" + Http.status + "] Unknow status.");*/
					}						
					o.callback(Http.responseText);
					Http.abort();Http = null;delete Http;									
				}
			};
			Http.send(o.data);
		},
		Upload:function(o)
		{
			var progress = iPad.Dom.Get(o.inputFile.name.replace("myfile","progress"));
			var UploadProgress = function(evt)
			{
				if (evt.lengthComputable)
				{
				  var percentComplete = Math.round(evt.loaded * 100 / evt.total);
				  //iPad.Dom.Get(o.progress).innerHTML = percentComplete.toString() + '%';
				  progress.style.width = percentComplete + "%";
				}
				else
				{
				  alert("unable to compute");
				}
			}
			
			var UploadComplete = function(evt)
			{
				var json = iPad.Core.ToJson(evt.target.responseText);
				if(o.completeFuction)
				{
					o.completeFuction(o.id,o.name,json);
				}
				else
				{
					iPad.UI.UploadBack(o.id,o.name,json);	
				}				
				progress.style.width = "0%";
			}
			
			var UploadError = function(evt)
			{
				
			}
			
			var UploadAbort = function(evt)
			{				
				
			}
			
			var formdata = new FormData();
			var file = o.inputFile.files[0];
			var filename = file.name.toLowerCase();
			var data = "";
			if(filename.indexOf(".jpg")>-1 || filename.indexOf(".jpeg")>-1 || filename.indexOf(".png")>-1  || filename.indexOf(".gif")>-1  || filename.indexOf(".bmp")>-1)
			{
				data = "?format=900*500*a&format=450*250*b&format=300*167*c&format=225*125*d&format=180*100*e";
			}
			formdata.append(o.inputFile.name,file);
			var httprequest = new XMLHttpRequest();
			httprequest.upload.addEventListener("progress", UploadProgress, false);
			httprequest.addEventListener("load", UploadComplete, false);
			httprequest.addEventListener("error", UploadError, false);
			httprequest.addEventListener("abort", UploadAbort, false);
			httprequest.open("POST", iPad.Config.FileServicesURL+data);
			httprequest.send(formdata);	
			
			
		},
		UploadFile:function(o)
		{
			
			var index = 0;	
			
			var app = function()
			{
				
				var progress = iPad.Dom.Get(o.inputFile.name.replace("myfile","progress"));
				var UploadProgress = function(evt)
				{
					if (evt.lengthComputable)
					{
					  var percentComplete = Math.round(evt.loaded * 100 / evt.total);
					  //iPad.Dom.Get(o.progress).innerHTML = percentComplete.toString() + '%';
					  progress.style.width = percentComplete + "%";
					}
					else
					{
					  alert("unable to compute");
					}
				}
				
				var UploadComplete = function(evt)
				{
					var json = iPad.Core.ToJson(evt.target.responseText);
					if(o.completeFuction)
					{
						o.completeFuction(o.id,o.name,json);
					}
					else
					{
						iPad.UI.UploadFileBack(o,json);	
					}				
					progress.style.width = "0%";
					
					if(index<o.inputFile.files.length)
					{			
						//alert("下一个");			
						app();//继续上传文件
					}
				}
				
				var UploadError = function(evt)
				{
					
				}
				
				var UploadAbort = function(evt)
				{				
					
				}
				
				var formdata = new FormData();			
				var data = "?1=1";
				file = o.inputFile.files[index];
				formdata.append(o.inputFile.name,file);
				var filename = file.name.toLowerCase();
				var httprequest = new XMLHttpRequest();
				httprequest.upload.addEventListener("progress", UploadProgress, false);
				httprequest.addEventListener("load", UploadComplete, false);
				httprequest.addEventListener("error", UploadError, false);
				httprequest.addEventListener("abort", UploadAbort, false);
				httprequest.open("POST", iPad.Config.FileServicesURL+data);
				httprequest.send(formdata);	
				
				index ++;
			}
				
				
			app();
			
			
			
			
			
			
		},
		EventElement:function()
		{
			var evt = iPad.Core.GetEvent();
			return evt.srcElement || evt.target;
		}
		,GetEvent:function()
		{
			if(document.all)
			{
				return window.event;//如果是ie
			}
			func = iPad.Core.GetEvent.caller;
			while(func!=null)
			{
				var arg0=func.arguments[0];
					if(arg0)
					{
						if((arg0.constructor==Event || arg0.constructor ==MouseEvent) ||(typeof(arg0)=="object" && arg0.preventDefault && arg0.stopPropagation))
						{
							return arg0;
						}
					}
				func=func.caller;
			}
			return null;			
		},
		Validator:function()
		{
			this.ErrorItem = [document.forms[0]],
			this.ErrorMessage = ["Error:\t\t\t\t"]
			this.Validate = function(theForm, mode)
			{
				
				var obj = theForm || event.srcElement;
				var count = obj.elements.length;
				this.ErrorMessage.length = 1;
				this.ErrorItem.length = 1;
				this.ErrorItem[0] = obj;
				
				
				for(var i=0;i<count;i++)
				{
					with(obj.elements[i])
					{
						
						var _dataType = getAttribute("dataType");	
						var _require = getAttribute("require");
						if(_dataType && _require!="false")
						{
							var value = obj.elements[i].value;							
							switch(_dataType)
							{
								case "Date" :
								case "Repeat" :
								case "Range" :
								case "Compare" :
								case "Custom" :
								case "Group" : 
								case "Limit" :
								case "LimitInt" :
								case "LimitB" :
								case "SafeString" :
									if(!eval(this.Expres[_dataType]))
									{
										this.AddError(i, getAttribute("msg"));
									}
									break;
								default :
									//alert(_dataType);
									if(!this.Expres[_dataType].test(value))
									{
										this.AddError(i, getAttribute("msg"));
									}
									break;
							}
						}
					}
				}
				if(this.ErrorMessage.length > 1)
				{
					mode = mode || 1;
					var errCount = this.ErrorItem.length;
					switch(mode)
					{
						case 2 :
							for(var i=1;i<errCount;i++)
							this.ErrorItem[i].style.color = "red";
						case 1 :
							alert(this.ErrorMessage.join("\n"));
							break;
						case 4 :
							alert(this.ErrorMessage.join("\n"));
							break;
						case 3:			
							alert(this.ErrorMessage.join("\n"));
							break;
						default :
							alert(this.ErrorMessage.join("\n"));
							break;
					}
					return false;
				}
				return true;
			}
			
			this.AddError = function(index, str)
			{
				this.ErrorItem[this.ErrorItem.length] = this.ErrorItem[0].elements[index];
				this.ErrorMessage[this.ErrorMessage.length] = this.ErrorMessage.length + ":" + str;
			}
			
			this.ClearState = function(elem)
			{
				with(elem)
				{
					if(style.color == "red")
						style.color = "";
					var lastNode = parentNode.childNodes[parentNode.childNodes.length-1];
					if(lastNode.id == "__ErrorMessagePanel")
						parentNode.removeChild(lastNode);
				}
			}
			
			this.Expres = 
			{
				Require : /.+/,
				Email : /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/,
				Phone : /^(1[3,5]\d{9})|(0[1-9]{2,3}-[1-9]\d{6,7})$/,
				Mobile : /^1[3|4|5|7|8]\d{9}$/,
				Url : /^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,
				IdCard : /^\d{15}(\d{2}[A-Za-z0-9])?$/,
				Currency : /^\d+(\.\d+)?$/,
				Number : /^\d+$/,
				Zip : /^[1-9]\d{5}$/,
				QQ : /^[1-9]\d{4,8}$/,
				Integer : /^[-\+]?\d+$/,
				Double : /^[-\+]?\d+(\.\d+)?$/,
				English : /^[A-Za-z]+$/,
				Chinese :  /^[\u0391-\uFFE5]+$/,
				UnSafe : /^(.{0,5})$|\s/,
				IsSafe : function(str){return !this.UnSafe.test(str);},
				SafeString : "this.IsSafe(value)",
				Limit : "this.limit(value.length,getAttribute('min'),  getAttribute('max'))",
				LimitInt : "this.limitInt(value,getAttribute('min'),  getAttribute('max'))",
				LimitB : "this.limit(this.LenB(value), getAttribute('min'), getAttribute('max'))",
				Date : "this.Equation.IsDate(value, getAttribute('min'), getAttribute('format'))",
				Repeat : "value == document.getElementsByName(getAttribute('to'))[0].value",
				Range : "getAttribute('min') < value && value < getAttribute('max')",
				Compare : "this.compare(value,getAttribute('operator'),getAttribute('to'))",
				Custom : "this.Exec(value, getAttribute('regexp'))",
				Group : "this.Equation.MustChecked(getAttribute('name'), getAttribute('min'), getAttribute('max'))"
			}
			
			this.Equation =
			{
				IsDate:function(op, formatString)
				{
					formatString = formatString || "ymd";
					var m, year, month, day;
					switch(formatString)
					{
						case "ymd" :
							m = op.match(new RegExp("^((\\d{4})|(\\d{2}))([-./])(\\d{1,2})\\4(\\d{1,2})$"));
							if(m == null ) return false;
							day = m[6];				
							month = m[5]--;
							year =  (m[2].length == 4) ? m[2] : GetFullYear(parseInt(m[3], 10));
							break;
						case "dmy" :
							m = op.match(new RegExp("^(\\d{1,2})([-./])(\\d{1,2})\\2((\\d{4})|(\\d{2}))$"));
							if(m == null ) return false;
							day = m[1];
							month = m[3]--;
							year = (m[5].length == 4) ? m[5] : GetFullYear(parseInt(m[6], 10));
							break;
						default :
							break;
					}
					if(!parseInt(month)) return false;
					month --;
					month = month==12 ?0:month;		
					var date = new Date(year, month, day);
					return (typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate());
					function GetFullYear(y){return ((y<30 ? "20" : "19") + y)|0;}
				},
				MustChecked:function(name, min, max)
				{
					var groups = document.getElementsByName(name);
					var hasChecked = 0;
					min = min || 1;
					max = max || groups.length;
					for(var i=groups.length-1;i>=0;i--)
					{
						if(groups[i].checked)
						{
							hasChecked++;
						}
					}
					return min <= hasChecked && hasChecked <= max;
				}
			}			
			
		}
	}	
}
iPad.Listener.Add(window,"load",iPad.Core.init);
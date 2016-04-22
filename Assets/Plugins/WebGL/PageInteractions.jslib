var PageInteractions = {
    setCurrentKey: function (key) {
        document.key = Pointer_stringify(key);
        console.log(document.key,key);
    },
    saveKeyInCookie: function (key) {
      key = Pointer_stringify(key);
      var n = new Date();
      var d = new Date();
      d.setTime(d.getTime() + (2*24*60*60*1000));
      document.cookie = "key"+key+"={\"key\":\"" + key + "\" ,\"time\":"+n.getTime()+"}; expires="+d.toUTCString();
    },
    getAllCookies: function (){
      var cookies = document.cookie.split(';');
      console.log(cookies);
      var ret = "{\"layouts\":[";
      for(var i=0; i<cookies.length; i++) {
        var cookie = cookies[i];
        console.log(cookie.substring(0,3));
        var json = cookie.split("=");
        if(json[0].indexOf("key") > -1){
          console.log(json[1]);
          ret +=json[1]+",";
        }
      }
      ret = ret.substring(0,ret.length-1);
      ret += "]}";
      console.log(ret);
      var buffer = _malloc(lengthBytesUTF8(ret) + 1);
      writeStringToMemory(ret, buffer);
      return buffer;
    }
};

mergeInto(LibraryManager.library, PageInteractions);

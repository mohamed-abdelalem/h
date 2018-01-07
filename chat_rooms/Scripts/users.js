$(document).ready(function () {
    get_users();
    get_username();

    
});




function get_username() {
    $.ajax({
        url: 'http://localhost:10408/message/get_username',
        type: 'Get',
        contentType: 'application/json',
        success: function get_user_name(data) {
            document.getElementsByClassName("heading-name-meta")[0].innerHTML = data.User_Name;
            
        }
        ,
        error: function onerror(data) {
            alert("error" + err.responseText);
        }
       
    });
}


function get_users()
{
    $.ajax({
        url: 'http://localhost:10408/message/get_all_friend',
        type: 'Get',
contentType: 'application/json',
        success: onsuccess,
        error:onerror
    });
}
function onsuccess(data)
{
    var table = document.getElementsByClassName("row sideBar")[0];
    var count=0;
    for(var item in data)
    {count++;}
    for (var i = 0; i < count ; i++)
    { 

        var row = $(
"<div class='row sideBar-body' onclick=select("+ data[i].ID+") ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-xs-8 sideBar-name'><span class='name-meta'>" + data[i].User_Name + "</span></div><div class='col-sm-4 col-xs-4 pull-right sideBar-time'><span class='time-meta pull-right'>18:18</span></div></div></div></div>")
        $(table).append(row)

    }
    $("side-one").append(table);

  }
function onerror(err)
{
    alert("error" + err.responseText);
}


var reciver = 0;
function select(x)
{
    reciver = x;
    $.ajax({
        url: 'http://localhost:10408/message/get_all_message',
        data: { 'reciver': x },

        type: 'Get',
        contentType: 'application/json',
        success: function onsuce(data)
        {
            var table = document.getElementsByClassName("row message")[0];
            //$("<table id='message'>" + "</table>");
            $(table).empty();
            var header = $("<div class='row message-previous'><div class='col-sm-12 previous' style='height:30px;'><a onclick='previous(this)' id='ankitjain28' name='20'>Show Previous Message!</a></div></div>")
            $(table).append(header);
            var count = 0;
            for (var item in data)
            { count++; }
            for (var i = 0; i < count ; i++)
            {

                var row;
                if (x == data[i].ID____Receiver)
                {
                    row = $("<div class='row message-body'  style='height:50px;'><div class='col-sm-12 message-main-sender'><div class='sender'><div class='message-text'  style='height:80%;'>"
                        + data[i].Text +
                        "</div><span class='message-time pull-right'>" + data[i].Date + "</span></div></div></div>")

                }
                else
                {
                    row = $("<div class='row message-body' style='height:50px;'><div class='col-sm-12 message-main-receiver'><div class='receiver'><div class='message-text'  style='height:80%;'>"
                        + data[i].Text +
                        "</div><span class='message-time pull-right'>" +data[i].Date+ "</span></div></div></div>")

                }
                $(table).append(row)

            }
            $("col-sm-8 conversation").append(table);

            $("#conversation").scrollTop($("#conversation")[0].scrollHeight);


        },
        error: onerror
    });
    $("#conversation").scrollTop($("#conversation")[0].scrollHeight);

    $.ajax({
        url: 'http://localhost:10408/message/update_messages',
        data: { 'reciver': x },
        type: 'Get',
        contentType: 'application/json'
    })



}

function send_message()
{
  //  $("#conversation").scrollTop($("#conversation")[0].scrollHeight);

    var text = document.getElementById("comment").value;
    var date = new Date();
    //date = date.getDate() + "/"
    //            + (date.getMonth() + 1) + "/"
    //            + date.getFullYear() + " @ "
    //            + date.getHours() + ":"
    //            + date.getMinutes() + ":"
    //            + date.getSeconds();
    var message = { Text: text, ID____Receiver: reciver, Date: date }
    $.ajax({
        url: 'http://localhost:10408/message/send_message',
        data:JSON.stringify(message),
        type: 'POST',
        contentType: 'application/json'
        ,
        success: function insert_vlue(data)
        {
            //alert(result);
            var table = document.getElementsByClassName("row message")[0];
            var row;
            row = $("<div class='row message-body'  style='height:50px;'><div class='col-sm-12 message-main-sender'><div class='sender'><div class='message-text'  style='height:80%;'>"
                + data.Text + "</div><span class='message-time pull-right'>" + data.Date+ "</span></div></div></div>")
            $(table).append(row);
        }
    }
    );
}
setInterval(function () {
    $.ajax({
        url: 'http://localhost:10408/message/get_new_messages',
        data: { 'reciver': reciver },

        type: 'Get',
        contentType: 'application/json',
        success: function onsuce(data) {
            var table = document.getElementsByClassName("row message")[0];
            //$("<table id='message'>" + "</table>");
            //$(table).empty();
           // var header = $("<div class='row message-previous'><div class='col-sm-12 previous' style='height:30px;'><a onclick='previous(this)' id='ankitjain28' name='20'>Show Previous Message!</a></div></div>")
            //$(table).append(header);
            var count = 0;
            for (var item in data)
            { count++; }
            for (var i = 0; i < count ; i++) {
                var row;
                if (reciver == data[i].ID____Receiver)
                {
                    row = $("<div class='row message-body'  style='height:50px;'><div class='col-sm-12 message-main-sender'><div class='sender'><div class='message-text'  style='height:80%;'>"
                        + data[i].Text + "</div><span class='message-time pull-right'>"+ data[i].Date +"</span></div></div></div>")
                }
                else
                {

                    row = $("<div class='row message-body' style='height:50px;'><div class='col-sm-12 message-main-receiver'><div class='receiver'><div class='message-text'  style='height:80%;'>"
                        + data[i].Text + "</div><span class='message-time pull-right'>" + data[i].Date + "</span></div></div></div>")
                }
                $(table).append(row)

            }
            $("col-sm-8 conversation").append(table);
            $.ajax({
                url: 'http://localhost:10408/message/update_messages',
                data: { 'reciver': reciver },
                type: 'Get',
                contentType: 'application/json'
            })


        },
        error: onerror
    });




}, 5000);


//}

function previous()
{
        $.ajax({
            url: 'http://localhost:10408/message/previous_message',
            data: { 'reciver': reciver },

            type: 'Get',
            contentType: 'application/json',
            success: function onsuce(data) {
                var table = document.getElementsByClassName("row message")[0];
                //$("<table id='message'>" + "</table>");
                $(table).empty();
                var header = $("<div class='row message-previous'><div class='col-sm-12 previous' style='height:30px;'><a onclick='previous(this)' id='ankitjain28' name='20'>Show Previous Message!</a></div></div>")
                $(table).append(header);
                var count = 0;
                for (var item in data)
                { count++; }
                for (var i = 0; i < count ; i++) {

                    var row;
                    if (reciver == data[i].ID____Receiver) {
                        row = $("<div class='row message-body'  style='height:50px;'><div class='col-sm-12 message-main-sender'><div class='sender'><div class='message-text'  style='height:80%;'>"
                            + data[i].Text +
                            "</div><span class='message-time pull-right'>" + data[i].Date + "</span></div></div></div>")

                    }
                    else {
                        row = $("<div class='row message-body' style='height:50px;'><div class='col-sm-12 message-main-receiver'><div class='receiver'><div class='message-text'  style='height:80%;'>"
                            + data[i].Text +
                            "</div><span class='message-time pull-right'>" + data[i].Date + "</span></div></div></div>")

                    }
                    $(table).append(row)

                }
                $("col-sm-8 conversation").append(table);

                $("#conversation").scrollTop($("#conversation")[0].scrollHeight);


            },
            error: onerror
        });
        $("#conversation").scrollTop($("#conversation")[0].scrollHeight);

        $.ajax({
            url: 'http://localhost:10408/message/update_messages',
            data: { 'reciver': x },
            type: 'Get',
            contentType: 'application/json'
        })



}
function show_emotion() {    
    if ( document.getElementById("emotions").getAttribute("text").valueOf()== "1")
    {
        $("#emotions").css("margin-top", "-300");
        document.getElementById("emotions").setAttribute("text", "2");
    }
    else {
        $("#emotions").css("margin-top", "0");
        document.getElementById("emotions").setAttribute("text", "1");

    }
}

function add_emotion(emotion)
{
    var text = document.getElementById("comment").value;
   // var emotion = this.getAttribute("value");
    text = text + "" + emotion;
    document.getElementById("comment").value = text;
}
//function login()
//{
//    var name = document.getElementById("name").value;
//    var password = document.getElementById("password").value;
//    var user = { 'User_Name': name, 'password': password };

//    $.ajax({
//        url: 'http://localhost:10408/login/login',
//        data:JSON.stringify(user) ,
//        type: 'POST',
//        contentType: 'application/json',
//        success: function (result) {
//            if (result.success == true) {
//                window.location = result.redirecturl;
//            }
//        }
//    });
//}

$(window).click(function(evt)
{
    if (evt.target.id != "BtnEmoji" && evt.target.id != "emotions1") {

    if (document.getElementById("emotions").getAttribute("text").valueOf() == "2")
    {
        $("#emotions").css("margin-top", "0");
        document.getElementById("emotions").setAttribute("text", "1");
    }
    }
});
//$(window).click(function(evt)
//{
//    if (evt.target.class!="item_icom"||evt.target.id!="emotion_conteaner") {

//    if (document.getElementById("emotions").getAttribute("text").valueOf() == "2")
//    {
//        $("#emotions").css("margin-top", "0");
//        document.getElementById("emotions").setAttribute("text", "1");
//    }
  
//    }

//});

function sign()
{
    if (document.getElementById("sign_out").getAttribute("sign_out") == "1")
    {
        document.getElementById("sign_out").setAttribute("style", "height:25px;z-index: 99999999999999;position: fixed;margin-top:4%;visibility: visible;border:none;font-family:Aharoni;color:cornflowerblue;");
        document.getElementById("sign_out").setAttribute("sign_out", "2");
    }
    else
    {
        document.getElementById("sign_out").setAttribute("style", 'height:0px;z-index: 99999999999999;position: fixed;margin-top:4%;visibility: hidden;');
        document.getElementById("sign_out").setAttribute("sign_out", "1");


    }

} 

function search()
{
    var name = document.getElementById("searchText").value;
    if (name != "") {
        document.getElementsByClassName("sideBar")[0].innerHTML = "";
        $.ajax({
            url: 'http://localhost:10408/message/search',
            data: { 'name': name },
            type: 'Get',
            contentType: 'application/json',
            success: function onsuccess(data) {
                var table = document.getElementsByClassName("row sideBar")[0];
                var count = 0;
                for (var item in data)
                { count++; }
                for (var i = 0; i < count ; i++) {

                    var row = $(
            "<div class='row sideBar-body' onclick=select(" + data[i].ID + ") ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-xs-8 sideBar-name'><span class='name-meta'>" + data[i].User_Name + "</span></div><div class='col-sm-4 col-xs-4 pull-right sideBar-time'><span class='time-meta pull-right'>18:18</span></div></div></div></div>")
                    $(table).append(row)

                }
                $("side-one").append(table);

            }
        });
    }
    else {
        document.getElementsByClassName("sideBar")[0].innerHTML = "";

        get_users();
    }

}





function search2()
{
    var name = document.getElementById("composeText").value;
    if (name != "") {
        document.getElementsByClassName("row compose-sideBar")[0].innerHTML = "";
        $.ajax({
            url: 'http://localhost:10408/message/search_2',
            data: { 'name': name },
            type: 'Get',
            contentType: 'application/json',
            success: function onsuccess(data) {
                var table = document.getElementsByClassName("row compose-sideBar")[0];
                var count = 0;
                for (var item in data)
                { count++; }
                for (var i = 0; i < count ; i++) {
                    if (data[i].User_type == "sent friend requist") {
                        var row = $(
                "<div class='row sideBar-body' onclick=AD_friend(" + data[i].ID + ") ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-md-5 col-xs-5 sideBar-name'><span class='name-meta'>" + data[i].User_Name + "</span></div><div class='col-sm-4 col-xs-7 col-md-7 pull-right sideBar-time'><span class='time-meta pull-right'>" + data[i].User_type + "</span></div></div></div></div>")
                        $(table).append(row)

                    }
                    else if(data[i].User_type=="asked your accept") {
                        var row = $(
   "<div class='row sideBar-body'  ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-md-5 col-xs-5 sideBar-name'><span class='name-meta'>" + data[i].User_Name + "</span></div><div class='col-sm-4 col-xs-7 col-md-7 pull-right sideBar-time'><span class='time-meta pull-right'><input type='button' onclick=ADD_to_your_friend(" + data[i].ID + ") value='add friend' /></span></div></div></div></div>")
                        $(table).append(row)


                    }
                    else {
                        var row = $(
                    "<div class='row sideBar-body' onclick=ADD_friend(" + data[i].ID + ") ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-md-5 col-xs-5 sideBar-name'><span class='name-meta'>" + data[i].User_Name + "</span></div><div class='col-sm-4 col-xs-7 col-md-7 pull-right sideBar-time'><span class='time-meta pull-right'></span></div></div></div></div>")
                        $(table).append(row)
                    }
                }
            

                $("side-two").append(table);

            }
        });
    }
    else {
        document.getElementsByClassName("row compose-sideBar")[0].innerHTML = "";
    }

}

var add = 0;
function ADD_friend(ID) {


    $.ajax({
        url: 'http://localhost:10408/message/ADD_friend',
        data: { 'ID': ID },
        type: 'Get',
        contentType: 'application/json',

        success: su
        , error: onerror
    });


};
//$(document).change(
//    function () {

//        var height = document.body.scrollHeight;
//        document.getElementById("add_friens").setAttribute("style", "height:100% ;position:absolute;margin-top:-" + height + ";background-color:darkgrey;z-index:9999999999999999999;")
//    });
//$(window).resize(
//function () {
//    if (add == 1) {
//        var height = document.body.scrollHeight;
//        document.getElementById("add_friens").setAttribute("style", "height:" + height + ";position:absolute;margin-top:-" + height + ";background-color:rgba(0, 0, 0, .4);z-index:9999999999999999999;")
//        add = 0;
//    }
//}
//);
function cancel()
{
    document.getElementById("add_friens").setAttribute("style", "height:0;position:absolute;margin-top:0;background-color:rgba(0, 0, 0, .4);z-index:9999999999999999999;")
    var table = document.getElementById("add");
    $(table).empty();
    add = 0;

}
function su(data) {
    var height = document.body.scrollHeight;

    document.getElementById("add_friens").setAttribute("style", "height:100%;position:absolute;margin-top:-" + height + ";background-color:rgba(0, 0, 0, .4);z-index:9999999999999999999;")

    var table = document.getElementById("add");
    $(table).empty();

    var row = $("<div class='row sideBar-body' onclick=select(" + data.ID + ") ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-xs-8 sideBar-name'><span class='name-meta'>" + data.User_Name + "</span></div><div class='col-sm-4 col-xs-4 pull-right sideBar-time'><span class='time-meta pull-right'>18:18</span></div></div></div></div>")

    var row_buttons=$("<div class='row' style='height:50px;margin-top:20%;'><div class='col-md-5'></div><input onclick='send_friend_requist("+data.ID+")' type='button' class='col-md-3 btn btn-primary text-center' value='add to friends' /></div><div class='row' style='height:50px;margin-top:4%;'><div class='col-md-5'></div><input type='button' onclick='cancel()' class='col-md-3 btn btn-primary text-center' value='cancel' /></div>")




    add = 1;
    $(table).append(row);
    $(table).append(row_buttons)

    //add
}


function send_friend_requist(ID)
{
    $.ajax({
        url: 'http://localhost:10408/message/send_friend_requist',
        data: JSON.stringify({ "ID": ID }),
        type: 'POST',
        contentType: 'application/json'
    });
        
    cancel();
    search2();
}

function show_requist()
{

        document.getElementsByClassName("row compose-sideBar")[0].innerHTML = "";
        $.ajax({
            url: 'http://localhost:10408/message/show_requist',
            type: 'Get',
            contentType: 'application/json',
            success: function onsuccess(data) {
                var table = document.getElementsByClassName("row compose-sideBar")[0];
                var count = 0;
                for (var item in data)
                { count++; }
                for (var i = 0; i < count ; i++) {

                    var row = $(
            "<div class='row sideBar-body' onclick=ADD_friend(" + data[i].ID + ") ><div class='col-sm-3 col-xs-3 sideBar-avatar'><div class='avatar-icon'><img src='https://bootdey.com/img/Content/avatar/avatar1.png'></div></div><div class='col-sm-9 col-xs-9 sideBar-main'><div class='row'><div class='col-sm-8 col-md-5 col-xs-5 sideBar-name'><span class='name-meta'>" + data[i].User_Name + "</span></div><div class='col-sm-4 col-xs-7 col-md-7 pull-right sideBar-time'><span class='time-meta pull-right'><input type='button' value='add friend' /></span></div></div></div></div>")
                    $(table).append(row)

                }
                $("side-two").append(table);

            }
        });
    
    

}

function ADD_to_your_friend(ID) {
    $.ajax({
        url: 'http://localhost:10408/message/ADD_to_your_friend',
        data: JSON.stringify({ "ID": ID }),
        type: 'POST',
        contentType: 'application/json'
    });


}
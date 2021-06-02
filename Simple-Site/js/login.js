function Login(){
    let username = $("#username").val();
    let password = $("#password").val();

    $.get("http://localhost:5000/Login?nick="+username+"&pword="+password, LoginSuccess);
}

function LoginSuccess(data){
    console.log("Success");
}
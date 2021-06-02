function LoadUserData(){
    $.ajax({
            url: "http://localhost:5000/Account",
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: LoadDataSuccess
        }
    );
}

let userData;

function updateUserData(){
    $("#balVal").innerText = userData["balance"];
}

function LoadDataSuccess(data){
    userData = JSON.parse(data);
    updateUserData();
}

LoadUserData();
let userData;

function UpdateUserData(){
    $("#balVal").text(userData["balance"]);
    $("#usrNme").text(userData["nickname"]);

    $("#depositAddr").text(userData["inAddress"]);
}

function LoadUserData(){
    $.ajax({
            url: "http://localhost:5000/Account",
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: LoadDataSuccess
        }
    );
}

function LoadDataSuccess(data){
    userData = data;
    UpdateUserData();
}

function GenerateNewAddress(){
    $.ajax({
            url: "http://localhost:5000/PayInAddress",
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: GenerateNewSuccess
        }
    );
}

function GenerateNewSuccess(data){
    userData["inAddress"] = data;
    UpdateUserData();
}

function ConfirmTransaction(){
    let tx = $("#txId").val();
    if (tx.length==0) return;
    $.ajax({
            url: "http://localhost:5000/Confirm?txId="+tx,
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: ConfirmTxSuccess,
            error: ConfrimTXFail
        }
    );
}

function ConfrimTXFail(xhr, text, error){
    $("#txError").text(xhr.responseText);
    $("#txError").show();
}

function ConfirmTxSuccess(data){
    $("#txError").hide();
    $("#txId").val("");
    LoadUserData();
}

LoadUserData();
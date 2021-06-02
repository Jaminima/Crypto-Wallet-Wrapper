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

function GenerateNewAddressConfirmation(){
    $("#GenerateButton").hide();
    $("#GenerateConfirm").show();
}

function GenerateCancel(){
    $("#GenerateButton").show();
    $("#GenerateConfirm").hide();
}

function GenerateNewAddress(){
    $.ajax({
            url: "http://localhost:5000/PayInAddress",
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: GenerateNewSuccess
        }
    );
    GenerateCancel();
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
    $("#txErrorBox").show();
}

function HideTxError(){
    $("#txErrorBox").hide();
}

function ConfirmTxSuccess(data){
    $("#txErrorBox").hide();
    $("#txId").val("");
    LoadUserData();
}
LoadUserData();
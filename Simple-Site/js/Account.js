let userData;

function UpdateUserData(){
    $("#balVal").text(userData["balance"]);
    $("#usrNme").text(userData["nickname"]);

    $("#depositAddr").text(userData["inAddress"]);

    $("#withdrawAmount").attr({"max":userData["balance"]});
}

function LoadUserData(){
    $.ajax({
            url: "http://localhost:5000/Account",
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: LoadDataSuccess,
            error: LoadDataFail
        }
    );
}

function LoadDataFail(){
    location.href="./login.html";
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

function WithdrawConfirmation(){
    $("#WithdrawButton").hide();
    $("#WithdrawConfirm").show();
}

function WithdrawCancel(){
    $("#WithdrawButton").show();
    $("#WithdrawConfirm").hide();
}

function WithdrawAmount(){
    $.ajax({
            url: "http://localhost:5000/Withdraw?outAddr="+$("#withdrawAddr").val()+"&amount="+$("#withdrawAmount").val(),
            method: 'GET',
            xhrFields: { withCredentials: true },
            success: WithdrawSuccess,
            error: WithdrawFail
        }
    );
    WithdrawCancel();
}

function WithdrawSuccess(data){
    $("#outTxIdBox").show();
    $("#outTxId").text(data);
    $("#withdrawAddr").val("");
    $("#withdrawAmount").val("");
    LoadUserData();
}

function WithdrawFail(xhr, text, error){
    $("#WithdrawError").text(xhr.responseText);
    $("#WithdrawErrorBox").show();
}

function HideWithdrawTx(){
    $("#outTxIdBox").hide();
}

LoadUserData();
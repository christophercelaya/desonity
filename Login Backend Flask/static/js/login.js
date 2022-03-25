/*responsible for user login*/
function login() {
    identityWindow = window.open(
        "https://identity.deso.org/log-in?accessLevelRequest=2",
        null,
        "toolbar=no, width=800, height=1000, top=0, left=0"
    );
}

function logout() {
    axios.post("/logout")
        .then(() => { window.location.reload() })
        .catch((e) => { console.log(e) });
}

function handleInit(e) {
    if (!init) {
        init = true;
        iframe = document.getElementById("identity");

        for (const e of pendingRequests) {
            postMessage(e);
        }

        pendingRequests = [];
    }
    respond(e.source, e.data.id, {});
}

function handleLogin(payload) {
    // console.log(payload);
    if (identityWindow) {
        identityWindow.close();
        identityWindow = null;
    }
    if (payload.publicKeyAdded) {
        console.log("got the key " + payload.publicKeyAdded);
        axios.post("/setKey", { "PublicKey": payload.publicKeyAdded })
            .then(() => { window.location.reload() })
            .catch((e) => { console.log(e) });
    }
    if (payload.signedTransactionHex) {
        console.log("transaction signed " + payload.signedTransactionHex);
        axios.post("/submit-txn", { "TransactionHex": payload.signedTransactionHex })
            .then((res) => { console.log("transaction submitted " + res.data); alert("Transaction successfull") });
    }
}


function respond(e, t, n) {
    e.postMessage(
        {
            id: t,
            service: "identity",
            payload: n,
        },
        "*"
    );
}

function postMessage(e) {
    init
        ? this.iframe.contentWindow.postMessage(e, "*")
        : pendingRequests.push(e);
}

// const childWindow = document.getElementById('identity').contentWindow;
window.addEventListener("message", (message) => {
    // console.log("message: ");
    // console.log(message);

    const {
        data: { id: id, method: method, payload: payload },
    } = message;

    // console.log(id);
    // console.log(method);
    // console.log(payload);

    if (method == "initialize") {
        handleInit(message);
    } else if (method == "login") {
        handleLogin(payload);
    }
});

var init = false;
var iframe = null;
var pendingRequests = [];
var identityWindow = null;
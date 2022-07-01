let branch = document.getElementById("BranchId");
let cashier = document.getElementById("CashierId");

let cashierIdValue = document.querySelector('#hidden-cashier-id');

let cashierContainer = document.getElementById("cashier-container");

console.log(cashierIdValue.value)


branch.addEventListener("change", function () {
    cashierIdValue.value = 0;
    loadCashierSelectedList();
});

window.onload = function () {
    if (parseInt(branch.value)) {
        loadCashierSelectedList();
    }
};


cashier.addEventListener("change", function () {
    cashierIdValue.value = cashier.value;
    console.log(cashierIdValue.value);
})

function loadCashierSelectedList() {
    const myRequest = new XMLHttpRequest();
    myRequest.open("GET", `/InvoiceHeaders/LoadSpecificCashierSelectListItems?BranchId=${parseInt(branch.value)}`, true);
    myRequest.send();
    myRequest.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cashierContainer.classList.remove("d-none");
            let jsData = JSON.parse(this.responseText);
            if (branch.value == 0) {
                cashierContainer.classList.add("d-none");
                let cashierListItems = "<option value='0'>--Select Cashier Name--</option>";
                cashier.innerHTML = cashierListItems;
                cashierIdValue.value = 0;
                console.log(cashierIdValue.value);
            } else {
                let cashierListItems = "<option value='0'>--Select Cashier Name--</option>";
                for (let i = 0; i < jsData.length; i++) {
                    if (cashierIdValue.value !== 0 && cashierIdValue.value.toString() === jsData[i].cashierId) {
                        cashierListItems += `<option value="${jsData[i].cashierId}" selected>${jsData[i].cashierName}</option>`
                    }
                    else {
                        cashierListItems += `<option value="${jsData[i].cashierId}">${jsData[i].cashierName}</option>`
                    }
                }
                cashier.innerHTML = cashierListItems;
            }
        }
    }
}
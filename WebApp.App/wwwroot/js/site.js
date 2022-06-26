// Write your JavaScript code.

let branch = document.getElementById("InvoiceHeader_BranchId");
let cashier = document.getElementById("InvoiceHeader_CashierId");
let cashierContainer = document.getElementById("cashier-container");
const myRequest = new XMLHttpRequest();

branch.addEventListener("change", function () {
    myRequest.open("GET", `/InvoiceHeaders/LoadSpecificCashierSelectListItems?BranchId=${parseInt(branch.value)}`, true);
    myRequest.send();
    myRequest.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            cashierContainer.classList.remove("d-none");
            let jsData = JSON.parse(this.responseText);
            let cashierListItems = "<option value='0'>--Select Cashier Name--</option>";
            for (let i = 0; i < jsData.length; i++) {
                cashierListItems += `<option value="${jsData[i].cashierId}">${jsData[i].cashierName}</option>`
            }
            cashier.innerHTML = cashierListItems;
        }
    }
});

window.onload = function () {
    if (parseInt(branch.value)) {
        myRequest.open("GET", `/InvoiceHeaders/LoadSpecificCashierSelectListItems?BranchId=${parseInt(branch.value)}`, true);
        myRequest.send();
        myRequest.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                cashierContainer.classList.remove("d-none");
                let jsData = JSON.parse(this.responseText);
                let cashierListItems = "<option value='0'>--Select Cashier Name--</option>";
                for (let i = 0; i < jsData.length; i++) {
                    cashierListItems += `<option value="${jsData[i].cashierId}">${jsData[i].cashierName}</option>`
                }
                cashier.innerHTML = cashierListItems;
            }
        }
    }
};

const navTabs = document.querySelectorAll('.nav-tabs .nav-item');
const navLinks = document.querySelectorAll('.nav-item .nav-link');
const steps = document.querySelectorAll('.steps');

const imgItems = document.querySelectorAll('.img-item');

const postalCode = document.querySelector('#postal_code');
const checkPostal = document.querySelector('#check_postal');
const postalError = document.querySelector('#postal_error');

const addresses = document.querySelector('.addresses');

const addAddressBtn = document.querySelector('#add-address-btn');

const makePayment = document.querySelector('#make_payment');

const datetime = {
    date: null,
    time: null
}

const serviceRequest = {
    serviceStartDate: null,
    totalPayment: 75,
    postalCode: null,
    serviceHours: 3,
    comments: null,
    hasPets: false,
    extraServices: [],
    userAddress: null
};

//Step3
const getAddress = () => {
    fetch("/home/Get_address", {
        method: 'post',
        credentials: "same-origin",
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then((res) => res.json())
        .then((data) => {
            if (data === false) return;
            console.log(data);
            data.forEach((address) => {
                const addressLine = `
                    ${address.addressLine1} ${address.addressLine2}, ${address.city}, ${address.state}, ${address.postalCode}
                `;
                const addressHTML = `
                <label class="address">
                        <div class="radio">
                            <input type="radio" name="address_radio" class="address-radio"/>
                        </div>
                        <div class="address-info">
                            <div class="address-line">
                                    <span class="fw-bold">Address: </span>${addressLine}
                            </div>
                            <div class="contact-info">
                                    <span class="mobile"><span class="fw-bold">Phone number:</span> ${address.mobile}</span>
                            </div>
                        </div>
                </label>`;
                addresses.innerHTML += addressHTML;
            });
            const addressRadios = document.querySelectorAll('.address-radio');
            addressRadios.forEach((addressRadio, index) => {
                addressRadio.addEventListener('change', () => {
                    serviceRequest.userAddress = data[index];
                    serviceRequest.postalCode = data[index].postalCode;
                    changeStep(3);
                });
            });
        });
}

getAddress();

function dateFormat(inputDate, format) {
    const date = new Date(inputDate);
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();

    format = format.replace("MM", month.toString().padStart(2, "0"));
    if (format.indexOf("yyyy") > -1) {
        format = format.replace("yyyy", year.toString());
    } else if (format.indexOf("yy") > -1) {
        format = format.replace("yy", year.toString().substr(2, 2));
    }

    format = format.replace("dd", day.toString().padStart(2, "0"));
    return format;
}

function timeFormat(time) {
    // Check correct time format and split into components
    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
        time = time.slice(1);  // Remove full string match value
        time[5] = +time[0] < 12 ? 'AM' : 'PM'; // Set AM/PM
        time[0] = +time[0] % 12 || 12; // Adjust hours
    }
    return time.join(' '); // return adjusted time or original string
}


document.querySelector('#service-start-date').addEventListener('input', (e) => {
    datetime.date = e.target.valueAsDate;
    document.querySelector('#start-date').textContent = dateFormat(e.target.value, 'dd/MM/yyyy');
});

document.querySelector('#comment').addEventListener('input', (e) => {
    serviceRequest.comments = e.target.value;
});

document.querySelector('.has-pets').addEventListener('input', (e) => {
    serviceRequest.hasPets = e.target.checked;
});

document.querySelector('#service-start-time').addEventListener('input', (e) => {
    datetime.time = e.target.valueAsDate;
    document.querySelector('#start-time').textContent = timeFormat(e.target.value);
});

const paymentItems = document.querySelectorAll('.payment-item.extra-items');
const serviceHours = document.querySelector('#servie-hours');

const perCleaning = document.querySelector('#per-cleaning');
const totalPayment = document.querySelector('#total_payment');

const extraHoursOptions = document.querySelectorAll('#ExtraHours option');

const changeExtraHours = () => {
    serviceHours.textContent = serviceRequest.serviceHours;
    document.querySelector('#ExtraHours').value = serviceRequest.serviceHours;
}
const changeTotalPayments = () => {
    perCleaning.textContent = serviceRequest.totalPayment;
    totalPayment.textContent = serviceRequest.totalPayment;
}

document.querySelector('#ExtraHours').addEventListener('input', (e) => {
    serviceRequest.serviceHours = Number(e.target.value);
    serviceHours.textContent = serviceRequest.serviceHours;
});

// For changing active image
const changeImg = (e, index) => {

    if (e.currentTarget.classList.contains('active')) {
        serviceRequest.serviceHours -= 0.5;
        changeExtraHours();

        serviceRequest.totalPayment -= 12.5;
        changeTotalPayments();

        serviceRequest.extraServices.splice(serviceRequest.extraServices.indexOf(index), 1);
        paymentItems[index].classList.remove('active');
        e.currentTarget.classList.remove('active');
    }
    else {
        serviceRequest.serviceHours += 0.5;
        changeExtraHours();

        serviceRequest.totalPayment += 12.5;
        changeTotalPayments();

        serviceRequest.extraServices.push(index);
        paymentItems[index].classList.add('active');
        e.currentTarget.classList.add('active');
    }
    let length = serviceRequest.extraServices.length;

    extraHoursOptions.forEach((extraHoursOption, index) => {
        if (index < length) {
            extraHoursOption.style.display = 'none';
        }
        else if (index == length) {
            extraHoursOption.style.display = 'block';
        }
        else {
            extraHoursOption.style.display = 'block';
        }
    });
    
};

imgItems.forEach((imgItem, index) => {
    imgItem.addEventListener('click', (e) => changeImg(e, index));
});

// For changing tab content
const changeStep = (index) => {
    navLinks.forEach((navLink, navLinkIndex) => {
        if (navLinkIndex <= index) {
            navLink.classList.add('active');
            return navLink.classList.remove('disable');
        }
        navLink.classList.remove('active');
        navLink.classList.add('disable');
    });
    steps.forEach((step, i) => {
        if (i == index) return step.style.display = 'block';
        step.style.display = 'none';
    });
};

navTabs.forEach((navTab, index) => {
    navTab.addEventListener('click', (e) => {
        console.log(e.target.querySelector('.nav-link'));
        if (e.currentTarget.querySelector('.nav-link').classList.contains('disable')) return;
        changeStep(index);
    });
});

//Network calls
//Step 1
checkPostal.addEventListener('click', () => {
    if (postalCode.value == null) return;

    const data = JSON.stringify({ ZipCode: postalCode.value });
    fetch("/home/Check_Postal", {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
        },
        body: data
    })
        .then((res) => res.json())
        .then((data) => {
            if (!data) {
                const postalCodeModel = new bootstrap.Modal(document.querySelector('#postalcode-model'));
                return postalCodeModel.show();
            };
            changeStep(1);
        });
});

document.querySelector('#check_schedule').addEventListener('click', (e) => {

    if (datetime.date == null || datetime.time == null) {
        return document.querySelector('#start-date-error').textContent = "*Enter the Start date & time";
    }
    return changeStep(2);
});

const addAddressForm = document.querySelector('.add-address');

addAddressBtn.addEventListener('click', (e) => {
    if (addAddressForm.style.display == 'none') return addAddressForm.style.display = 'flex';
    return addAddressForm.style.display = 'none';
});

document.querySelector('#save-address').addEventListener('click', (e) => {
    e.preventDefault();

    const addAddressData = {};
    const formData = new FormData(addAddressForm);
    formData.forEach((value, key) => addAddressData[key] = value);

    fetch("/home/add_address", {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(addAddressData)
    })
        .then((res) => res.json())
        .then((data) => {
            console.log(data);
            addresses.innerHTML = null;
            getAddress();
            addAddressForm.style.display = 'none';
        });
});

new bootstrap.Modal(document.querySelector('#success-modal')).show()

//post service data
makePayment.addEventListener('click', () => {
    let date = datetime.date;
    let time = datetime.time;

    serviceRequest.serviceStartDate = new Date(date.getFullYear(), date.getMonth(), date.getDate(), time.getHours(), time.getMinutes(), time.getSeconds()).toJSON();

    fetch("/home/add_service", {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(serviceRequest)
    })
        .then((res) => res.json())
        .then((data) => {
            if (!data) return;
            new bootstrap.Modal(document.querySelector('#success-modal')).show();
            document.querySelector('#service_id').textContent = data;
        });
});
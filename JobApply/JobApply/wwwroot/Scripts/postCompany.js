$(function () {
	$('#submit').on('click', function (evt) {
		evt.preventDefault();
		if (validateModel() === false) return;
		$.ajax({
			url: "/Companies/Create/",
			type: "POST",
			beforeSend: function (xhr) {
				xhr.setRequestHeader("XSRF-TOKEN",
					$('input:hidden[name="__RequestVerificationToken"]').val());
			},
			dataType: "json",
			data: JSON.stringify(
				{
					Name: $("#Name").val(),
					City: $("#City").val(),
					Country: $("#Country").val(),
					ContactEmail: $("#ContactEmail").val(),
					FoundationDate: $("#FoundationDate").val(),
				}),
			contentType: "application/json",
			error: function (e) {
				console.log(e);
			},
			success: function (data) {
				if (data) {

					window.location.href = "/Companies/Index";
				}
				else {

					window.location.href = "/Companies/Create";
				}
			}
		});
	});
});

function validateName() {
	let name = $('#Name').val();
	if (name == undefined || name == null || name == '') {
		document.getElementById("nameWarning").style.display = '';
		return false;
	}
	else {
		document.getElementById("nameWarning").style.display = 'none';
		return true;
	}
}

function validateCity() {
	let name = $('#City').val();
	if (name == undefined || name == null || name == '') {
		document.getElementById("cityWarning").style.display = '';
		return false;
	}
	else {
		document.getElementById("cityWarning").style.display = 'none';
		return true;
	}
}

function validateCountry() {
	let name = $('#Country').val();
	if (name == undefined || name == null || name == '') {
		document.getElementById("countryWarning").style.display = '';
		return false;
	}
	else {
		document.getElementById("countryWarning").style.display = 'none';
		return true;
	}
}

function validateEmail() {
	let name = $('#ContactEmail').val();
	let emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
	if (name == undefined || name == null || name == '') {
		document.getElementById("emailWarning").style.display = '';
		return false;
	}
	else if (!name.match(emailRegex)) {
		document.getElementById("emailValidation").style.display = '';
		return false;
	}
	else {
		document.getElementById("emailWarning").style.display = 'none';
		document.getElementById("emailValidation").style.display = 'none';
		return true;
	}
}

function validateDate() {
	let name = $('#FoundationDate').val();
	let dateRegex = /([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))/;
	document.getElementById("dateValidation").style.display = 'none';
	document.getElementById("dateWarning").style.display = 'none';
	if (name == undefined || name == null || name == '') {
		document.getElementById("dateWarning").style.display = '';
		return false;
	}
	else if (!name.match(dateRegex)) {
		document.getElementById("dateValidation").style.display = '';
	}
	else {
		document.getElementById("dateValidation").style.display = 'none';
		document.getElementById("dateWarning").style.display = 'none';
		return true;
	}
}

function validateModel() {
	if (validateName() === false) return false;
	if (validateCity() === false) return false;
	if (validateCountry() === false) return false;
	if (validateEmail() === false) return false;
	if (validateDate() === false) return false;
	return true;
}






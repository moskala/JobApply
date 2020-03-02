var currentPage = 0;
$(document).ready(function () {
	fetchData(1, 1);
});
function fetchData(pageNo, dir) {
	currentPage += dir;
	var $loading = $("<div class='spinner-border' role='status'></div >");
	$('#pagingJobOffers').html($loading);
	$.ajax({
		url: '/JobOffers/GetJobOffers',
		type: 'GET',
		data: { pageNo: pageNo },
		dataType: 'json',
		success: OnSuccess,
		error: function () {
			alert('Error! Please try again.');
		}
	}).done(function () {
		$loading.remove();
	});
}

function OnSuccess(data) {
	var model = data;
	var $tbody = $('<tbody/>');
	var $thead = $('<thead/>').html("<tr><th>Job Title</th><th>Company Name</th><th>Location</th><th></th></tr>");
	$('#pagingJobOffers').append($thead);
	$.each(model.jobOffers, function (i, offer) {
		var $row = $('<tr/>');
		var detailsLink = "<a href=/JobOffers/Details/" + offer.id + ">" + offer.jobTitle + "</a>";
		$row.append($('<td/>').html(detailsLink));
		$row.append($('<td/>').html(offer.companyName));
		$row.append($('<td/>').html(offer.location));
		//$row.append($('<td/>').html(offer.applicationDeadline));
		var editLink = "<a href=/JobOffers/Edit/" + offer.id + " class='text-primary text-center'><i class='fa fa-edit' title='Edit'></i></a>";
		$row.append($('<td/>').html(editLink));
		$tbody.append($row);
	});

	var totalPage = parseInt(model.totalPage);
	var $footer = $('<tr/>');
	var $footerTD = $('<td/>').attr('colspan', 5).addClass('text-center');

	if (totalPage > 0) {
		let index = currentPage + "/" + model.totalPage;
		var $page = $("<span class='px-2'/>").html(index);

		if (currentPage > 1) {
			let prevPage = currentPage - 1;
			var $prev = "<button type='button' class='btn btn-secondary' onclick='fetchData(" + prevPage + ", -1);'>" + "<<" + "</button>";
			$footerTD.append($prev);
		}
		$footerTD.append($page);
		if (currentPage < totalPage) {
			let nextPage = currentPage + 1;
			var $next = "<button type='button' class='btn btn-secondary' onclick='fetchData(" + nextPage + ", 1);'>" + ">>" + "</button>";
			$footerTD.append($next);
		}
		$footer.append($footerTD);
	}
	$tbody.append($footer);
	$('#pagingJobOffers').append($tbody);
}

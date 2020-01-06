var currentPage = 0;
$(document).ready(function () {
	fetchData(1, 1);
});
function fetchData(pageNo, dir) {
	currentPage += dir;
	var $loading = $("<div class='spinner-border' role='status'></div >");
	$('#pagingTable').html($loading);
	$.ajax({
		url: '/JobOffers/GetJobApplications',
		type: 'GET',
		data: { pageNo: pageNo },
		dataType: 'json',
		success: function (data) {
			var $table = $('<table/>').addClass('table table-responsive');
			var $header = $('<thead/>').html('<tr><th>First Name</th><th>Last Name</th><th>Email address</th><th></th></tr>');
			$table.append($header);
			var $tbody = $('<tbody/>');
			$table.append($tbody);
			$.each(data.jobApplications, function (i, ap) {
				var $row = $('<tr/>');
				$row.append($('<td/>').html(ap.firstName));
				$row.append($('<td/>').html(ap.lastName));
				$row.append($('<td/>').html(ap.emailAddress));

				var link = '/JobApplications/Details/' + ap.id;
				var tlink = "<a href=" + link + " class='text-primary text-center'><i class='fa fa-angle-double-right' aria-hidden='true'></i></a>";
				$row.append($('<td/>').html(tlink));
				$tbody.append($row);
			});

			var totalPage = parseInt(data.totalPage);
			var $footer = $('<tr/>');
			var $footerTD = $('<td/>').attr('colspan', 4).addClass('text-center');

			if (totalPage > 0) {
				var $page = $("<span class='px-2'/>").html(currentPage);

				if (currentPage > 1) {
					var pageDist = currentPage - 1;
					var $prev = "<button type='button' class='btn btn-secondary' onclick='fetchData(" + pageDist + ", -1);'>" + "<<" + "</button>";
					$footerTD.append($prev);
				}
				$footerTD.append($page);
				if (currentPage < totalPage) {
					pageDist = currentPage + 1;
					var $next = "<button type='button' class='btn btn-secondary' onclick='fetchData(" + pageDist + ", 1);'>" + ">>" + "</button>";
					$footerTD.append($next);
				}
				$footer.append($footerTD);
			}
			$tbody.append($footer);

			$('#pagingTable').html($table);
		},
		error: function () {
			alert('Error! Please try again.');
		}
	}).done(function () {

		$loading.remove();
	});
}
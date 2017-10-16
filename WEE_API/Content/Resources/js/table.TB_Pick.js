
/*
 * Editor client script for DB table TB_Pick
 * Created by http://editor.datatables.net/generator
 */

(function($){

$(document).ready(function() {
	var editor = new $.fn.dataTable.Editor( {
		ajax: '/api/TB_Pick',
		table: '#TB_Pick',
		fields: [
			{
				"label": "JobRef:",
				"name": "JobRef",
				"type": "textarea"
			},
			{
				"label": "Comlete:",
				"name": "comlete",
				"type": "checkbox",
				"def": "false",
				"separator": ",",
				"options": [
					""
				]
			},
			{
				"label": "PickedBy:",
				"name": "PickedBy",
				"type": "select",
				"options": [
					"T\u00fa",
					"Th\u00f4ng",
					"Huy"
				]
			},
			{
				"label": "PackedBy:",
				"name": "packedby",
				"type": "select",
				"options": [
					"T\u00fa",
					"Th\u00f4ng",
					"Huy"
				]
			},
			{
				"label": "CheckedBy:",
				"name": "checkedby",
				"type": "select",
				"options": [
					"T\u00fa",
					"Th\u00f4ng",
					"Huy"
				]
			},
			{
				"label": "DateCreated:",
				"name": "datecreated",
				"type": "datetime",
				"format": "DD\/MM\/YYYY"
			},
			{
				"label": "DateCompleted:",
				"name": "datecompleted",
				"type": "datetime",
				"format": "DD\/MM\/YYYY"
			}
		]
	} );

	var table = $('#TB_Pick').DataTable( {
		ajax: '/api/TB_Pick',
		columns: [
			{
				"data": "picki"
			},
			{
				"data": "JobRef"
			},
			{
				"data": "comlete"
			},
			{
				"data": "PickedBy"
			},
			{
				"data": "packedby"
			},
			{
				"data": "checkedby"
			},
			{
				"data": "datecreated"
			},
			{
				"data": "datecompleted"
			}
		],
		select: true,
		lengthChange: false
	} );

	new $.fn.dataTable.Buttons( table, [
		{ extend: "create", editor: editor },
		{ extend: "edit",   editor: editor },
		{ extend: "remove", editor: editor }
	] );

	table.buttons().container()
		.appendTo( $('.col-sm-6:eq(0)', table.table().container() ) );
} );

}(jQuery));


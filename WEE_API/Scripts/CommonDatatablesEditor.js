
(function ($, DataTable) {

   
    if (!DataTable.ext.editorFields) {
        DataTable.ext.editorFields = {};
    }

    var Editor = DataTable.Editor;
    var _fieldTypes = Editor ?
        Editor.fieldTypes :
        DataTable.ext.editorFields;


    _fieldTypes.selectize = {
        _addOptions: function (conf, options) {
            var selectize = conf._selectize;

            selectize.clearOptions();
            selectize.addOption(options);
            selectize.refreshOptions(false);
        },

        create: function (conf) {
            var container = $('<div/>');
            conf._input = $('<select/>')
                .attr($.extend({
                    id: conf.id
                }, conf.attr || {}))
                .appendTo(container);

            conf._input.selectize($.extend({
                //valueField: 'value', 
                //labelField: 'label',
                //searchField: 'label' 
            }, conf.opts));

            conf._selectize = conf._input[0].selectize;

            if (conf.options || conf.ipOpts) {
                _fieldTypes.selectize._addOptions(conf, conf.options || conf.ipOpts);
            }

            return container[0];
        },

        get: function (conf) {
            return conf._selectize.getValue();
        },

        set: function (conf, val) {
            return conf._selectize.setValue(val);
        },

        enable: function (conf) {
            conf._selectize.enable();
            $(conf._input).removeClass('disabled');
        },

        disable: function (conf) {
            conf._selectize.disable();
            $(conf._input).addClass('disabled');
        },

        // Non-standard Editor methods - custom to this plug-in
        inst: function (conf) {
            return conf._selectize;
        },

        update: function (conf, options) {
            _fieldTypes.selectize._addOptions(conf, options);
        }
    };
})(jQuery, jQuery.fn.dataTable);



var generateFields = function (tableID) {
    var _prepareField = function (th) {
        var columnData = $(th).data();
        var field = {
            label: columnData.label || $(th).html(),// Uses the th data-label value. If it doesn't exist, uses the HTML inside
            //data: columnData.data,
            //name: columnData.name || columnData.data,// Uses the th data-name value. If it doesn't exist, uses the data-data value   
            data: columnData.type === "selectize" ?
                columnData.data.split(".")[0] + "ID" : columnData.data,
            name: columnData.type === "selectize" ?
                columnData.data.split(".")[0] + "ID" : columnData.data,
            filter_delay: 500
        };
        switch (columnData.type) {
            case 'checkbox':
                field = $.extend(true, field, {
                    type: 'checkbox',
                    separator: '|',
                    options: [{
                        label: '',
                        value: 1
                    }]
                });
                break;
            case 'DateTime':
            case 'DateTime?':
                field = $.extend(true, field, {
                    type: 'datetime',
                    format: 'DD/MM/YYYY' 
                });
                break;
            case 'selectize':
                field = $.extend(true, field, {
                    type: 'selectize',
                    opts: {
                        sortField: "value",
                        valueField: "value",
                        labelField: "label",
                        searchField: "label",
                        preload: true,
                        maxItems: 1,
                        load: function(query, callback) {
                            $.ajax({
                                url: path+field.data.replace("ID","")+"/GetList2Select",
                                type: "GET",
                                dataType: "json",
                                data: { q: query },
                                error: function() { callback(); },
                                success: function(res) { callback(res.result); }
                            });
                        } 
                    }
                });
                break;
        };
        return field;
    };
   
    var fields = [];
    $("#" + tableID).find('thead th').each(function (index, th) {
        if ($(th).data().editvisible === true) {
            fields.push(_prepareField(th));
        }
    });
    return fields;
};

var generateColumns = function (tableID) {
    var _prepareColumn = function (th) {
        var columnData = $(th).data();
        var render1;
        if (columnData.type === "selectize") {
              render1 = function ( data, type, row, meta ) {
                  return data;
            }
        }
        var column = {
            title: $(th).html(),
            data: columnData.data,
            render: render1,
            'class': columnData.class || '',
            type: columnData.align || '',
            sortable: columnData.sortable || true
        };
        return column;
    };
    var columns = [];
    $("#" + tableID).find('thead th').each(function (index, th) {
        if ($(th).data().listvisible === true) {
            columns.push(_prepareColumn(th));
        } else {
            $(th).removeAttr("style").hide();
        }
    });
    return columns;
};

var generateYdacf = function (tableID, filterMode) {
    var _prepareYdacf = function (i, th) {
        var ydacfData = $(th).data();

        var field = {
            column_number: i,
            filter_type: ydacfData.filter_type || 'auto_complete',
            select_type: (ydacfData.filter_type === 'select' || ydacfData.filter_type === 'multi_select') ? 'select2' : ''
        };
        switch (ydacfData.type) {
            case 'datetime':
            case 'DateTime':
            case 'DateTime?':
                field = $.extend(true, field, {
                    filter_type: "range_date",
                    date_format: "dd/mm/yyyy",
                    moment_date_format: "DD/MM/YYYY",
                    datepicker_type: 'bootstrap-datetimepicker',
                    filter_plugin_options: datepickerDefaults
                });
                break;
        }
        if (filterMode !== "HeaderFilter") {
            field.filter_container_id = tableID + "ExtF" + i;

            $("#" + filterMode).append(
            "<div class='form-group col-md-6 col-lg-4'>" +
            "	<label class='col-sm-5 control-label'>" + $(th).html()+"</label>" +
            "	<div class='col-sm-7'>" +
            "   <span id='" + tableID + "ExtF" + i + "'></span>" +
            "	</div>" +
            "</div>");
        }
        return field;
    };
    var Ydacfs = [];
    $("#" + tableID).find('thead th').each(function (index, th) {
        if ($(th).data().listvisible === true) {
            Ydacfs.push(_prepareYdacf(index, th));
        }
    });
    $("#" + filterMode).parent().append(
        "<div id='externaly_triggered_wrapper-controls' style='float:right;'>" +
        "	<div>" +
          "		<input type='button' onclick='yadcf.exFilterExternallyTriggered(tableFor" + tableID.replace('DataTable','')+");' value='Tìm kiếm' class='btn btn-info'>" +
        "		<input type='button' onclick='yadcf.exResetAllFilters(tableFor" + tableID.replace('DataTable', '') +");' value='Xóa' class='btn btn-warning'>" +
        "	</div>" +
          "</div>"
      );
    return Ydacfs;
};


var generateFilterBase = function (tableID) {
    var FilterBases = [];
    $("#" + tableID).find('thead th').each(function (index, th) {
        if ($(th).data().listvisible === true) {
            FilterBases.push({
                Ydacf_number: index,
                Ydacf_FieldName: $(th).data().data
            });
        }
    });
    return FilterBases;
};

var datepickerDefaults = {
    showTodayButton: true,
    showClear: true,
    format: "DD/MM/YYYY"
};

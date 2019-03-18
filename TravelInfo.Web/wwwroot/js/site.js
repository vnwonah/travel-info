// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function (){
    $("#location").autocomplete({
        source: function(request, response){
            $.ajax({
                url: "/Home/Countries",
                type: "POST",
                dataType: "json",
                data: {Prefix: request.term },
                success: function(data){
                    response($.map(data, function(item){
                        return {label: item.name, value: item.name};
                    }))
                }
            })
        },
        messages:{
            noResults: "", results: function(resultsCount) {}
        }
    });

 $("#destination").autocomplete({
        source: function(request, response){
            $.ajax({
                url: "/Home/Countries",
                type: "POST",
                dataType: "json",
                data: {Prefix: request.term },
                success: function(data){
                    response($.map(data, function(item){
                        return {label: item.name, value: item.name};
                    }))
                }
            })
        },
        messages:{
            noResults: "", results: function(resultsCount) {}
        }
    });
})
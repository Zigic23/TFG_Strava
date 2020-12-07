/*
    Cargar el resultado de una llamada (pensado para PartialViews)
    - dest: control donde cargar los datos
    - url: URL de la acción a cargar
    - data: datos a enviar a la petición
    - pre: metodo a ejecutar antes de realizar la acción
    - post: metodo a ejecutar despues de realizar la acción
 */
function Load_PartialView(dest, url, data, pre, post) {

    pre();

    $(dest).load(url, data, function (responseText, textStatus, jqXHR) {
        if (status === "error") {
            var msg = "Sorry but there was an error: ";
            $("#error").html(msg + jqXHR.status + " " + jqXHR.statusText);
        }
        else {
            post();
        }
    });
}

/*
    Habilita la funcionalidad nativa en todos los elementos PopUp
 */
function enablePopupFuntionality() {

    // Funcionalidad Cerrar
    $(".modal *[data-popup-close]").click(function (e) {
        // Lo oculta
        $(this).parents(".modal").hide();
        // Si el elemento tiene el atributo "data-popup-close" y su valor es "true" bloquea el bubbling
        if (($(e.target).attr("data-popup-close") == "true")) {
            e.preventDefault();
        }
    });
    // Funcionalidad Abrir
    $("*[data-popup]").click(function (e) {
        // Lo muestra
        $(".modal#" + $(this).attr("data-popup")).show();
        // Si el elemento tiene el atributo "data-popup-async" y su valor es "false" no bloquea el bubbling
        if ($(e.target).attr("data-popup-async") != "false") {
            e.preventDefault();
        }
    });
}

$(function () {
    enablePopupFuntionality();
})


//subir scroll
$(window).scroll(function () {
    if ($(this).scrollTop() > 300) {
        $('.scroll_up').fadeIn();
    } else {
        $('.scroll_up').fadeOut();
    }
});
$(document).ready(function () {
    // Funcionamiento de Boton de subir en scroll
    $('.scroll_up').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
    // Desplegable de idioma
    $('.lang_des').click(function () {
        $('.lang_g').toggleClass("lang_g_vis");
    });
});

$(document).ready(function () {
    // Desplegable de menú
    $('.menu_expand').click(function () {
        $(this).toggleClass('expand');
        $('.menu_sup').toggleClass('menu_sup_expand');
    });

    //Cerrar alertas de notificaciones
    $('.msg_primary .xd_message_close').click(function () {
        $('.msg_primary').addClass('hide');
    });
    $('.msg_danger .xd_message_close').click(function () {
        $('.msg_danger').addClass('hide');
    });
    $('.msg_success .xd_message_close').click(function () {
        $('.msg_success').addClass('hide');
    });
    $('.msg_warning .xd_message_close').click(function () {
        $('.msg_warning').addClass('hide');
    });

    //Tarjetas Flip
    $('.card_flip.horizontal').click(function () {
        $(this).toggleClass('flipped_Horizontal');
    });

    $('.card_flip.vertical').click(function () {
        $(this).toggleClass('flipped_Vertical');
    });

    //Desplegable revelador
    $(".link_Show_Hide").click(function () {
        $(this).toggleClass("show");
        if ($(".link_Show_Hide").hasClass("show")) {
            $(this).text("Click to read less");
            $(".detail_reveal").slideDown("slow");
        } else {
            $(this).text("Click to read more");
            $(".detail_reveal").slideUp("slow");
        }
    });

    //Cambio de pestaña en tabs
    $('.tab-link').click(function () {

        var tabID = $(this).attr('data-tab');

        $(this).addClass('active').siblings().removeClass('active');

        $('#tab-' + tabID).addClass('active').siblings().removeClass('active');
    });

    //selección/deselección inputs tabla
    $('#data_table thead label').click(function () {
        if ($('#checkbox_table_head').is(':checked') == true) {
            // Bucle a los child todos a check
            for (i = 0; i < $('#data_table tbody input').length; i++) {
                $('#data_table tbody input')[i].checked = false;
            }
        }
        else {
            // bucle al revés
            for (i = 0; i < $('#data_table tbody input').length; i++) {
                $('#data_table tbody input')[i].checked = true;
            }
        }
    });

    $('#data_table tbody input').change(function () {
        if ($('#data_table tbody input[type="checkbox"]').length == $('#data_table tbody input[type="checkbox"]:checked').length) {
            $('#checkbox_table_head')[0].checked = true;
        }
        else {
            $('#checkbox_table_head')[0].checked = false;
        }
    });
});

//seleccion de etiquetas multiple
$(document).ready(function () {

    var select = $('select[multiple]');
    var options = select.find('option');

    var div = $('<div />').addClass('selectMultiple');
    var active = $('<div />');
    var list = $('<ul />');
    var placeholder = select.data('placeholder');

    var span = $('<span />').text(placeholder).appendTo(active);

    options.each(function () {
        var text = $(this).text();
        if ($(this).is(':selected')) {
            active.append($('<a />').html('<em>' + text + '</em><i></i>'));
            span.addClass('hide');
        } else {
            list.append($('<li />').html(text));
        }
    });

    active.append($('<div />').addClass('arrow'));
    div.append(active).append(list);

    select.wrap(div);

    $(document).on('click', '.selectMultiple ul li', function (e) {
        var select = $(this).parent().parent();
        var li = $(this);
        if (!select.hasClass('clicked')) {
            select.addClass('clicked');
            li.prev().addClass('beforeRemove');
            li.next().addClass('afterRemove');
            li.addClass('remove');
            var a = $('<a />').addClass('notShown').html('<em>' + li.text() + '</em><i></i>').hide().appendTo(select.children('div'));
            a.slideDown(400, function () {
                setTimeout(function () {
                    a.addClass('shown');
                    select.children('div').children('span').addClass('hide');
                    select.find('option:contains(' + li.text() + ')').prop('selected', true);
                }, 500);
            });
            setTimeout(function () {
                if (li.prev().is(':last-child')) {
                    li.prev().removeClass('beforeRemove');
                }
                if (li.next().is(':first-child')) {
                    li.next().removeClass('afterRemove');
                }
                setTimeout(function () {
                    li.prev().removeClass('beforeRemove');
                    li.next().removeClass('afterRemove');
                }, 200);

                li.slideUp(400, function () {
                    li.remove();
                    select.removeClass('clicked');
                });
            }, 600);
        }
    });

    $(document).on('click', '.selectMultiple > div a', function (e) {
        var select = $(this).parent().parent();
        var self = $(this);
        self.removeClass().addClass('remove');
        select.addClass('open');
        setTimeout(function () {
            self.addClass('disappear');
            setTimeout(function () {
                self.animate({
                    width: 0,
                    height: 0,
                    padding: 0,
                    margin: 0
                }, 300, function () {
                    var li = $('<li />').text(self.children('em').text()).addClass('notShown').appendTo(select.find('ul'));
                    li.slideDown(400, function () {
                        li.addClass('show');
                        setTimeout(function () {
                            select.find('option:contains(' + self.children('em').text() + ')').prop('selected', false);
                            if (!select.find('option:selected').length) {
                                select.children('div').children('span').removeClass('hide');
                            }
                            li.removeClass();
                        }, 400);
                    });
                    self.remove();
                })
            }, 300);
        }, 400);
    });

    $(document).on('click', '.selectMultiple > div .arrow, .selectMultiple > div span', function (e) {
        $(this).parent().parent().toggleClass('open');
    });

    //control: etiquetas single
    $('.dropdown-container')
        .on('click', '.dropdown-button', function () {
            $('.dropdown-list').toggle();
        })
        .on('input', '.dropdown-search', function () {
            var target = $(this);
            var search = target.val().toLowerCase();

            if (!search) {
                $('.sigleTag').show();
                return false;
            }

            $('.sigleTag').each(function () {
                var text = $(this).text().toLowerCase();
                var match = text.indexOf(search) > -1;
                $(this).toggle(match);
            });
        })
        .on('change', '[type="checkbox"]', function () {
            var numChecked = $('[type="checkbox"]:checked').length;
            $('.quantity').text(numChecked || '0');
        });

    //control paa pasar etiquetas de un bloque a otro    
    $('.pasar').click(function () {
        $('#origen option:selected').remove().appendTo('#destino');
     });
    $('.quitar').click(function() {
        $('#destino option:selected').remove().appendTo('#origen');
    });
		$('.pasartodos').click(function() {
        $('#origen option').each(function () {
            $(this).remove().appendTo('#destino');
        });
    });
		$('.quitartodos').click(function() {
        $('#destino option').each(function () {
            $(this).remove().appendTo('#origen');
        });
    });$('.pasar').click(function () {
            $('#origen option:selected').remove().appendTo('#destino');
        });
    $('.quitar').click(function() {
        $('#destino option:selected').remove().appendTo('#origen');
    });
		$('.pasartodos').click(function() {
        $('#origen option').each(function () {
            $(this).remove().appendTo('#destino');
        });
    });
		$('.quitartodos').click(function() {
        $('#destino option').each(function () {
            $(this).remove().appendTo('#origen');
        });
    });
});


// Stonev2

// Desplegar Menu
function menuCollapsed() {
    $('.heaC').toggleClass('heaCDes');
}
$(document).ready(function () {
    $('.collapsed_but').click(function () {
        if ($('.heaC').hasClass('heaCDes')) {
            $('.logo img').attr('src', '/Content/images/logos/logo2.svg');
            const $navLinks = document.querySelectorAll(".nav-link");

            $navLinks.forEach(e => {
                if (!e.firstElementChild.classList.contains("far")) {
                    e.closest(".nav-item").style.visibility = "hidden";
                }

            })
        }
        else {

            $('.logo img').attr('src', '/Content/images/logos/logo.svg');
            const $navLinks = document.querySelectorAll(".nav-link");

            $navLinks.forEach(e => {
                if (!e.firstElementChild.classList.contains("far")) {
                    e.closest(".nav-item").style.visibility = "visible";
                }

            })
        }
    });

   
});
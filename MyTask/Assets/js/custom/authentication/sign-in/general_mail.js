"use strict";
var KTSigninGeneral = function () {
    var t, e, i;
    return {
        init: function () {
            t = document.querySelector("#kt_sign_in_form"), e = document.querySelector("#kt_sign_in_submit"), i = FormValidation.formValidation(t, {
                fields: {
                    username: {
                        validators: {
                            notEmpty: {
                                message: "Email address is required"
                            },
                            emailAddress: {
                                message: "The value is not a valid email address"
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger,
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row"
                    })
                }
            }), e.addEventListener("click", (function (n) {
                n.preventDefault(), i.validate().then((function (i) {
                    "Valid" == i ? (e.setAttribute("data-kt-indicator", "on"),
                        $.ajax({                            
                            url: baseUrl +'Account/CekMail/',
                            type: 'POST',
                            data: { username: t.querySelector('[name="username"]').value},
                            error: function (result) {
                                Swal.fire({
                                    text: "Sorry, looks like there are some errors detected, please try again.",
                                    icon: "error",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    },
                                }).then((function (e) {
                                    e.isConfirmed && (t.querySelector('[name="username"]').value = "");
                                    window.location.href = 'Account/LoginMail';
                                }))
                            },
                            success: function (result) {
                                if (result === "error") {
                                    Swal.fire({
                                        text: "Your email not registered",
                                        icon: "error",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        },
                                    }).then((function (e) {
                                        e.isConfirmed && (t.querySelector('[name="username"]').value = "");
                                        window.location.href = 'Account/LoginMail';
                                    }))
                                } else {
                                    Swal.fire({
                                        text: "You have successfully submit email!",
                                        icon: "success",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then((function (e) {
                                        e.isConfirmed && (t.querySelector('[name="username"]').value = "");
                                        window.location.href = 'Account/SuccessSend';
                                    }))
                                }
                            }
                        }),
                        setTimeout((function () {}), 9e3)) : Swal.fire({
                        text: "Sorry, looks like there are some errors detected, please try again.",
                        icon: "error",
                        buttonsStyling: !1,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        },
                    })
                }))
            }))
        }
    }
}();
KTUtil.onDOMContentLoaded((function () {
    KTSigninGeneral.init()
}));
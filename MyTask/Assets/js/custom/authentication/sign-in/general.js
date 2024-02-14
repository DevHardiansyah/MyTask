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
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: "The password is required"
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
                            url: baseUrl + 'Account/Login/',
                            type: 'POST',
                            data: { username: t.querySelector('[name="username"]').value, password: t.querySelector('[name="password"]').value },
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
                                    e.isConfirmed && (t.querySelector('[name="username"]').value = "", t.querySelector('[name="password"]').value = "");
                                    window.location.href = 'Account/Login';
                                }))
                            },
                            success: function (result) {
                                if (result === "Error") {
                                    Swal.fire({
                                        text: "Sorry, we couldn't find your account, or the password you entered is incorrect, or your account is currently inactive.",
                                        icon: "error",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        },
                                    }).then((function (e) {
                                        e.isConfirmed && (t.querySelector('[name="username"]').value = "", t.querySelector('[name="password"]').value = "");
                                        window.location.href = 'Account/Login';
                                    }))
                                } else {
                                    Swal.fire({
                                        text: "You have successfully logged in!",
                                        icon: "success",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then((function (e) {
                                        e.isConfirmed && (t.querySelector('[name="username"]').value = "", t.querySelector('[name="password"]').value = "");
                                        window.location.href = 'Home/Index';
                                    }))
                                }
                            }
                        }),
                        setTimeout((function () { }), 9e3)) :
                         Swal.fire({
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
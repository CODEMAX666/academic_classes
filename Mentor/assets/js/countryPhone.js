﻿"use strict";
(function (factory) {
    if (typeof define === "function" && define.amd) {
        define(["jquery"], function ($) {
            factory($, window, document);
        });
    } else if (typeof module === "object" && module.exports) {
        module.exports = factory(require("jquery"), window, document);
    } else {
        factory(jQuery, window, document);
    }
})(function ($, window, document, undefined) {
    var pluginName = "phoneInput", id = 1,
    defaults = {
        allowDropdown: true,
        autoHideDialCode: true,
        autoPlaceholder: true,
        customPlaceholder: null,
        dropdownContainer: "",
        excludeCountries: [],
        formatOnInit: true,
        geoIpLookup: null,
        initialCountry: "",
        nationalMode: true,
        numberType: "MOBILE",
        onlyCountries: [],
        preferredCountries: ["us", "gb"],
        separateDialCode: false,
        utilsScript: ""
    }, keys = {
        UP: 38,
        DOWN: 40,
        ENTER: 13,
        ESC: 27,
        PLUS: 43,
        A: 65,
        Z: 90,
        SPACE: 32,
        TAB: 9
    };
    $(window).on("load", function () {
        $.fn[pluginName].windowLoaded = true;
    });
    function Plugin(element, options) {
        this.telInput = $(element);
        this.options = $.extend({}, defaults, options);
        this.ns = "." + pluginName + id++;
        this.isGoodBrowser = Boolean(element.setSelectionRange);
        this.hadInitialPlaceholder = Boolean($(element).attr("placeholder"));
    }
    Plugin.prototype = {
        _init: function () {
            if (this.options.nationalMode) {
                this.options.autoHideDialCode = false;
            }
            if (this.options.separateDialCode) {
                this.options.autoHideDialCode = this.options.nationalMode = false;
                this.options.allowDropdown = true;
            }
            this.isMobile = /Android.+Mobile|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
            if (this.isMobile) {
                $("body").addClass("cp-mobile");
                if (!this.options.dropdownContainer) {
                    this.options.dropdownContainer = "body";
                }
            }
            this.autoCountryDeferred = new $.Deferred();
            this.utilsScriptDeferred = new $.Deferred();
            this._processCountryData();
            this._generateMarkup();
            this._setInitialState();
            this._initListeners();
            this._initRequests();
            return [this.autoCountryDeferred, this.utilsScriptDeferred];
        },
        _processCountryData: function () {
            this._processAllCountries();
            this._processCountryCodes();
            this._processPreferredCountries();
        },
        _addCountryCode: function (iso2, dialCode, priority) {
            if (!(dialCode in this.countryCodes)) {
                this.countryCodes[dialCode] = [];
            }
            var index = priority || 0;
            this.countryCodes[dialCode][index] = iso2;
        },
        _filterCountries: function (countryArray, processFunc) {
            var i;
            for (i = 0; i < countryArray.length; i++) {
                countryArray[i] = countryArray[i].toLowerCase();
            }
            this.countries = [];
            for (i = 0; i < allCountries.length; i++) {
                if (processFunc($.inArray(allCountries[i].iso2, countryArray))) {
                    this.countries.push(allCountries[i]);
                }
            }
        },
        _processAllCountries: function () {
            if (this.options.onlyCountries.length) {
                this._filterCountries(this.options.onlyCountries, function (inArray) {
                    return inArray != -1;
                });
            } else if (this.options.excludeCountries.length) {
                this._filterCountries(this.options.excludeCountries, function (inArray) {
                    return inArray == -1;
                });
            } else {
                this.countries = allCountries;
            }
        },
        _processCountryCodes: function () {
            this.countryCodes = {};
            for (var i = 0; i < this.countries.length; i++) {
                var c = this.countries[i];
                this._addCountryCode(c.iso2, c.dialCode, c.priority);
                if (c.areaCodes) {
                    for (var j = 0; j < c.areaCodes.length; j++) {
                        this._addCountryCode(c.iso2, c.dialCode + c.areaCodes[j]);
                    }
                }
            }
        },
        _processPreferredCountries: function () {
            this.preferredCountries = [];
            for (var i = 0; i < this.options.preferredCountries.length; i++) {
                var countryCode = this.options.preferredCountries[i].toLowerCase(), countryData = this._getCountryData(countryCode, false, true);
                if (countryData) {
                    this.preferredCountries.push(countryData);
                }
            }
        },
        _generateMarkup: function () {
            this.telInput.attr("autocomplete", "off");
            var parentClass = "country-phone-input";
            if (this.options.allowDropdown) {
                parentClass += " allow-dropdown";
            }
            if (this.options.separateDialCode) {
                parentClass += " separate-dial-code";
            }
            this.telInput.wrap($("<div>", {
                "class": parentClass
            }));
            this.flagsContainer = $("<div>", {
                "class": "flag-container"
            }).insertBefore(this.telInput);
            var selectedFlag = $("<div>", {
                "class": "selected-flag"
            });
            selectedFlag.appendTo(this.flagsContainer);
            this.selectedFlagInner = $("<div>", {
                "class": "cp-flag"
            }).appendTo(selectedFlag);
            if (this.options.separateDialCode) {
                this.selectedDialCode = $("<div>", {
                    "class": "selected-dial-code"
                }).appendTo(selectedFlag);
            }
            if (this.options.allowDropdown) {
                selectedFlag.attr("tabindex", "0");
                $("<div>", {
                    "class": "cp-arrow"
                }).appendTo(selectedFlag);
                this.countryList = $("<ul>", {
                    "class": "country-list hide"
                });
                if (this.preferredCountries.length) {
                    this._appendListItems(this.preferredCountries, "preferred");
                    $("<li>", {
                        "class": "divider"
                    }).appendTo(this.countryList);
                }
                this._appendListItems(this.countries, "");
                this.countryListItems = this.countryList.children(".country");
                if (this.options.dropdownContainer) {
                    this.dropdown = $("<div>", {
                        "class": "country-phone-input cp-container"
                    }).append(this.countryList);
                } else {
                    this.countryList.appendTo(this.flagsContainer);
                }
            } else {
                this.countryListItems = $();
            }
        },
        _appendListItems: function (countries, className) {
            var tmp = "";
            for (var i = 0; i < countries.length; i++) {
                var c = countries[i];
                tmp += "<li class='country " + className + "' data-dial-code='" + c.dialCode + "' data-country-code='" + c.iso2 + "'>";
                tmp += "<div class='flag-box'><div class='cp-flag " + c.iso2 + "'></div></div>";
                tmp += "<span class='country-name'>" + c.name + "</span>";
                tmp += "<span class='dial-code'>+" + c.dialCode + "</span>";
                tmp += "</li>";
            }
            this.countryList.append(tmp);
        },
        _setInitialState: function () {
            var val = this.telInput.val();
            if (this._getDialCode(val)) {
                this._updateFlagFromNumber(val, true);
            } else if (this.options.initialCountry !== "auto") {
                if (this.options.initialCountry) {
                    this._setFlag(this.options.initialCountry, true);
                } else {
                    this.defaultCountry = this.preferredCountries.length ? this.preferredCountries[0].iso2 : this.countries[0].iso2;
                    if (!val) {
                        this._setFlag(this.defaultCountry, true);
                    }
                }
                if (!val && !this.options.nationalMode && !this.options.autoHideDialCode && !this.options.separateDialCode) {
                    this.telInput.val("+" + this.selectedCountryData.dialCode);
                }
            }
            if (val) {
                this._updateValFromNumber(val, this.options.formatOnInit);
            }
        },
        _initListeners: function () {
            this._initKeyListeners();
            if (this.options.autoHideDialCode) {
                this._initFocusListeners();
            }
            if (this.options.allowDropdown) {
                this._initDropdownListeners();
            }
        },
        _initDropdownListeners: function () {
            var that = this;
            var label = this.telInput.closest("label");
            if (label.length) {
                label.on("click" + this.ns, function (e) {
                    if (that.countryList.hasClass("hide")) {
                        that.telInput.focus();
                    } else {
                        e.preventDefault();
                    }
                });
            }
            var selectedFlag = this.selectedFlagInner.parent();
            selectedFlag.on("click" + this.ns, function (e) {
                if (that.countryList.hasClass("hide") && !that.telInput.prop("disabled") && !that.telInput.prop("readonly")) {
                    that._showDropdown();
                }
            });
            this.flagsContainer.on("keydown" + that.ns, function (e) {
                var isDropdownHidden = that.countryList.hasClass("hide");
                if (isDropdownHidden && (e.which == keys.UP || e.which == keys.DOWN || e.which == keys.SPACE || e.which == keys.ENTER)) {
                    e.preventDefault();
                    e.stopPropagation();
                    that._showDropdown();
                }
                if (e.which == keys.TAB) {
                    that._closeDropdown();
                }
            });
        },
        _initRequests: function () {
            var that = this;
            if (this.options.utilsScript) {
                if ($.fn[pluginName].windowLoaded) {
                    $.fn[pluginName].loadUtils(this.options.utilsScript, this.utilsScriptDeferred);
                } else {
                    $(window).on("load", function () {
                        $.fn[pluginName].loadUtils(that.options.utilsScript, that.utilsScriptDeferred);
                    });
                }
            } else {
                this.utilsScriptDeferred.resolve();
            }
            if (this.options.initialCountry === "auto") {
                this._loadAutoCountry();
            } else {
                this.autoCountryDeferred.resolve();
            }
        },
        _loadAutoCountry: function () {
            var that = this;
            if ($.fn[pluginName].autoCountry) {
                this.handleAutoCountry();
            } else if (!$.fn[pluginName].startedLoadingAutoCountry) {
                $.fn[pluginName].startedLoadingAutoCountry = true;
                if (typeof this.options.geoIpLookup === "function") {
                    this.options.geoIpLookup(function (countryCode) {
                        $.fn[pluginName].autoCountry = countryCode.toLowerCase();
                        setTimeout(function () {
                            $(".country-phone-input input").phoneInput("handleAutoCountry");
                        });
                    });
                }
            }
        },
        _initKeyListeners: function () {
            var that = this;
            this.telInput.on("keyup" + this.ns, function () {
                that._updateFlagFromNumber(that.telInput.val());
            });
            this.telInput.on("cut" + this.ns + " paste" + this.ns, function () {
                setTimeout(function () {
                    that._updateFlagFromNumber(that.telInput.val());
                });
            });
        },
        _cap: function (number) {
            var max = this.telInput.attr("maxlength");
            return max && number.length > max ? number.substr(0, max) : number;
        },
        _initFocusListeners: function () {
            var that = this;
            this.telInput.on("mousedown" + this.ns, function (e) {
                if (!that.telInput.is(":focus") && !that.telInput.val()) {
                    e.preventDefault();
                    that.telInput.focus();
                }
            });
            this.telInput.on("focus" + this.ns, function (e) {
                if (!that.telInput.val() && !that.telInput.prop("readonly") && that.selectedCountryData.dialCode) {
                    that.telInput.val("+" + that.selectedCountryData.dialCode);
                    that.telInput.one("keypress.plus" + that.ns, function (e) {
                        if (e.which == keys.PLUS) {
                            that.telInput.val("");
                        }
                    });
                    setTimeout(function () {
                        var input = that.telInput[0];
                        if (that.isGoodBrowser) {
                            var len = that.telInput.val().length;
                            input.setSelectionRange(len, len);
                        }
                    });
                }
            });
            var form = this.telInput.prop("form");
            if (form) {
                $(form).on("submit" + this.ns, function () {
                    that._removeEmptyDialCode();
                });
            }
            this.telInput.on("blur" + this.ns, function () {
                that._removeEmptyDialCode();
            });
        },
        _removeEmptyDialCode: function () {
            var value = this.telInput.val(), startsPlus = value.charAt(0) == "+";
            if (startsPlus) {
                var numeric = this._getNumeric(value);
                if (!numeric || this.selectedCountryData.dialCode == numeric) {
                    this.telInput.val("");
                }
            }
            this.telInput.off("keypress.plus" + this.ns);
        },
        _getNumeric: function (s) {
            return s.replace(/\D/g, "");
        },
        _showDropdown: function () {
            this._setDropdownPosition();
            var activeListItem = this.countryList.children(".active");
            if (activeListItem.length) {
                this._highlightListItem(activeListItem);
                this._scrollTo(activeListItem);
            }
            this._bindDropdownListeners();
            this.selectedFlagInner.children(".cp-arrow").addClass("up");
        },
        _setDropdownPosition: function () {
            var that = this;
            if (this.options.dropdownContainer) {
                this.dropdown.appendTo(this.options.dropdownContainer);
            }
            this.dropdownHeight = this.countryList.removeClass("hide").outerHeight();
            if (!this.isMobile) {
                var pos = this.telInput.offset(), inputTop = pos.top, windowTop = $(window).scrollTop(),
                dropdownFitsBelow = inputTop + this.telInput.outerHeight() + this.dropdownHeight < windowTop + $(window).height(), dropdownFitsAbove = inputTop - this.dropdownHeight > windowTop;
                this.countryList.toggleClass("dropup", !dropdownFitsBelow && dropdownFitsAbove);
                if (this.options.dropdownContainer) {
                    var extraTop = !dropdownFitsBelow && dropdownFitsAbove ? 0 : this.telInput.innerHeight();
                    this.dropdown.css({
                        top: inputTop + extraTop,
                        left: pos.left
                    });
                    $(window).on("scroll" + this.ns, function () {
                        that._closeDropdown();
                    });
                }
            }
        },
        _bindDropdownListeners: function () {
            var that = this;
            this.countryList.on("mouseover" + this.ns, ".country", function (e) {
                that._highlightListItem($(this));
            });
            this.countryList.on("click" + this.ns, ".country", function (e) {
                that._selectListItem($(this));
            });
            var isOpening = true;
            $("html").on("click" + this.ns, function (e) {
                if (!isOpening) {
                    that._closeDropdown();
                }
                isOpening = false;
            });
            var query = "", queryTimer = null;
            $(document).on("keydown" + this.ns, function (e) {
                e.preventDefault();
                if (e.which == keys.UP || e.which == keys.DOWN) {
                    that._handleUpDownKey(e.which);
                } else if (e.which == keys.ENTER) {
                    that._handleEnterKey();
                } else if (e.which == keys.ESC) {
                    that._closeDropdown();
                } else if (e.which >= keys.A && e.which <= keys.Z || e.which == keys.SPACE) {
                    if (queryTimer) {
                        clearTimeout(queryTimer);
                    }
                    query += String.fromCharCode(e.which);
                    that._searchForCountry(query);
                    queryTimer = setTimeout(function () {
                        query = "";
                    }, 1e3);
                }
            });
        },
        _handleUpDownKey: function (key) {
            var current = this.countryList.children(".highlight").first();
            var next = key == keys.UP ? current.prev() : current.next();
            if (next.length) {
                if (next.hasClass("divider")) {
                    next = key == keys.UP ? next.prev() : next.next();
                }
                this._highlightListItem(next);
                this._scrollTo(next);
            }
        },
        _handleEnterKey: function () {
            var currentCountry = this.countryList.children(".highlight").first();
            if (currentCountry.length) {
                this._selectListItem(currentCountry);
            }
        },
        _searchForCountry: function (query) {
            for (var i = 0; i < this.countries.length; i++) {
                if (this._startsWith(this.countries[i].name, query)) {
                    var listItem = this.countryList.children("[data-country-code=" + this.countries[i].iso2 + "]").not(".preferred");
                    this._highlightListItem(listItem);
                    this._scrollTo(listItem, true);
                    break;
                }
            }
        },
        _startsWith: function (a, b) {
            return a.substr(0, b.length).toUpperCase() == b;
        },
        _updateValFromNumber: function (number, doFormat) {
            if (doFormat && window.phoneInputUtils && this.selectedCountryData) {
                var format = !this.options.separateDialCode && (this.options.nationalMode || number.charAt(0) != "+") ? phoneInputUtils.numberFormat.NATIONAL : phoneInputUtils.numberFormat.INTERNATIONAL;
                number = phoneInputUtils.formatNumber(number, this.selectedCountryData.iso2, format);
            }
            number = this._beforeSetNumber(number);
            this.telInput.val(number);
        },
        _updateFlagFromNumber: function (number, isInit) {
            if (number && this.options.nationalMode && this.selectedCountryData && this.selectedCountryData.dialCode == "1" && number.charAt(0) != "+") {
                if (number.charAt(0) != "1") {
                    number = "1" + number;
                }
                number = "+" + number;
            }
            var dialCode = this._getDialCode(number), countryCode = null;
            if (dialCode) {
                var countryCodes = this.countryCodes[this._getNumeric(dialCode)], alreadySelected = this.selectedCountryData && $.inArray(this.selectedCountryData.iso2, countryCodes) != -1;
                if (!alreadySelected || this._isUnknownNanp(number, dialCode)) {
                    for (var j = 0; j < countryCodes.length; j++) {
                        if (countryCodes[j]) {
                            countryCode = countryCodes[j];
                            break;
                        }
                    }
                }
            } else if (number.charAt(0) == "+" && this._getNumeric(number).length) {
                countryCode = "";
            } else if (!number || number == "+") {
                countryCode = this.defaultCountry;
            }
            if (countryCode !== null) {
                this._setFlag(countryCode, isInit);
            }
        },
        _isUnknownNanp: function (number, dialCode) {
            return dialCode == "+1" && this._getNumeric(number).length >= 4;
        },
        _highlightListItem: function (listItem) {
            this.countryListItems.removeClass("highlight");
            listItem.addClass("highlight");
        },
        _getCountryData: function (countryCode, ignoreOnlyCountriesOption, allowFail) {
            var countryList = ignoreOnlyCountriesOption ? allCountries : this.countries;
            for (var i = 0; i < countryList.length; i++) {
                if (countryList[i].iso2 == countryCode) {
                    return countryList[i];
                }
            }
            if (allowFail) {
                return null;
            } else {
                throw new Error("No country data for '" + countryCode + "'");
            }
        },
        _setFlag: function (countryCode, isInit) {
            var prevCountry = this.selectedCountryData && this.selectedCountryData.iso2 ? this.selectedCountryData : {};
            this.selectedCountryData = countryCode ? this._getCountryData(countryCode, false, false) : {};
            if (this.selectedCountryData.iso2) {
                this.defaultCountry = this.selectedCountryData.iso2;
            }
            this.selectedFlagInner.attr("class", "cp-flag " + countryCode);
            var title = countryCode ? this.selectedCountryData.name + ": +" + this.selectedCountryData.dialCode : "Unknown";
            this.selectedFlagInner.parent().attr("title", title);
            if (this.options.separateDialCode) {
                var dialCode = this.selectedCountryData.dialCode ? "+" + this.selectedCountryData.dialCode : "", parent = this.telInput.parent();
                if (prevCountry.dialCode) {
                    parent.removeClass("cp-sdc-" + (prevCountry.dialCode.length + 1));
                }
                if (dialCode) {
                    parent.addClass("cp-sdc-" + dialCode.length);
                }
                this.selectedDialCode.text(dialCode);
            }
            this._updatePlaceholder();
            this.countryListItems.removeClass("active");
            if (countryCode) {
                this.countryListItems.find(".cp-flag." + countryCode).first().closest(".country").addClass("active");
            }
            if (!isInit && prevCountry.iso2 !== countryCode) {
                this.telInput.trigger("countrychange", this.selectedCountryData);
            }
        },
        _updatePlaceholder: function () {
            if (window.phoneInputUtils && !this.hadInitialPlaceholder && this.options.autoPlaceholder && this.selectedCountryData) {
                var numberType = phoneInputUtils.numberType[this.options.numberType], placeholder = this.selectedCountryData.iso2 ? phoneInputUtils.getExampleNumber(this.selectedCountryData.iso2, this.options.nationalMode, numberType) : "";
                placeholder = this._beforeSetNumber(placeholder);
                if (typeof this.options.customPlaceholder === "function") {
                    placeholder = this.options.customPlaceholder(placeholder, this.selectedCountryData);
                }
                this.telInput.attr("placeholder", placeholder);
            }
        },
        _selectListItem: function (listItem) {
            this._setFlag(listItem.attr("data-country-code"));
            this._closeDropdown();
            this._updateDialCode(listItem.attr("data-dial-code"), true);
            this.telInput.focus();
            if (this.isGoodBrowser) {
                var len = this.telInput.val().length;
                this.telInput[0].setSelectionRange(len, len);
            }
        },
        _closeDropdown: function () {
            this.countryList.addClass("hide");
            this.selectedFlagInner.children(".cp-arrow").removeClass("up");
            $(document).off(this.ns);
            $("html").off(this.ns);
            this.countryList.off(this.ns);
            if (this.options.dropdownContainer) {
                if (!this.isMobile) {
                    $(window).off("scroll" + this.ns);
                }
                this.dropdown.detach();
            }
        },
        _scrollTo: function (element, middle) {
            var container = this.countryList, containerHeight = container.height(), containerTop = container.offset().top, containerBottom = containerTop + containerHeight, elementHeight = element.outerHeight(), elementTop = element.offset().top, elementBottom = elementTop + elementHeight, newScrollTop = elementTop - containerTop + container.scrollTop(), middleOffset = containerHeight / 2 - elementHeight / 2;
            if (elementTop < containerTop) {
                if (middle) {
                    newScrollTop -= middleOffset;
                }
                container.scrollTop(newScrollTop);
            } else if (elementBottom > containerBottom) {
                if (middle) {
                    newScrollTop += middleOffset;
                }
                var heightDifference = containerHeight - elementHeight;
                container.scrollTop(newScrollTop - heightDifference);
            }
        },
        _updateDialCode: function (newDialCode, hasSelectedListItem) {
            var inputVal = this.telInput.val(), newNumber;
            newDialCode = "+" + newDialCode;
            if (inputVal.charAt(0) == "+") {
                var prevDialCode = this._getDialCode(inputVal);
                if (prevDialCode) {
                    newNumber = inputVal.replace(prevDialCode, newDialCode);
                } else {
                    newNumber = newDialCode;
                }
            } else if (this.options.nationalMode || this.options.separateDialCode) {
                return;
            } else {
                if (inputVal) {
                    newNumber = newDialCode + inputVal;
                } else if (hasSelectedListItem || !this.options.autoHideDialCode) {
                    newNumber = newDialCode;
                } else {
                    return;
                }
            }
            this.telInput.val(newNumber);
        },
        _getDialCode: function (number) {
            var dialCode = "";
            if (number.charAt(0) == "+") {
                var numericChars = "";
                for (var i = 0; i < number.length; i++) {
                    var c = number.charAt(i);
                    if ($.isNumeric(c)) {
                        numericChars += c;
                        if (this.countryCodes[numericChars]) {
                            dialCode = number.substr(0, i + 1);
                        }
                        if (numericChars.length == 4) {
                            break;
                        }
                    }
                }
            }
            return dialCode;
        },
        _getFullNumber: function () {
            var prefix = this.options.separateDialCode ? "+" + this.selectedCountryData.dialCode : "";
            return prefix + this.telInput.val();
        },
        _beforeSetNumber: function (number) {
            if (this.options.separateDialCode) {
                var dialCode = this._getDialCode(number);
                if (dialCode) {
                    if (this.selectedCountryData.areaCodes !== null) {
                        dialCode = "+" + this.selectedCountryData.dialCode;
                    }
                    var start = number[dialCode.length] === " " || number[dialCode.length] === "-" ? dialCode.length + 1 : dialCode.length;
                    number = number.substr(start);
                }
            }
            return this._cap(number);
        },
        handleAutoCountry: function () {
            if (this.options.initialCountry === "auto") {
                this.defaultCountry = $.fn[pluginName].autoCountry;
                if (!this.telInput.val()) {
                    this.setCountry(this.defaultCountry);
                }
                this.autoCountryDeferred.resolve();
            }
        },
        destroy: function () {
            if (this.allowDropdown) {
                this._closeDropdown();
                this.selectedFlagInner.parent().off(this.ns);
                this.telInput.closest("label").off(this.ns);
            }
            if (this.options.autoHideDialCode) {
                var form = this.telInput.prop("form");
                if (form) {
                    $(form).off(this.ns);
                }
            }
            this.telInput.off(this.ns);
            var container = this.telInput.parent();
            container.before(this.telInput).remove();
        },
        getExtension: function () {
            if (window.phoneInputUtils) {
                return phoneInputUtils.getExtension(this._getFullNumber(), this.selectedCountryData.iso2);
            }
            return "";
        },
        getNumber: function (format) {
            if (window.phoneInputUtils) {
                return phoneInputUtils.formatNumber(this._getFullNumber(), this.selectedCountryData.iso2, format);
            }
            return "";
        },
        getNumberType: function () {
            if (window.phoneInputUtils) {
                return phoneInputUtils.getNumberType(this._getFullNumber(), this.selectedCountryData.iso2);
            }
            return -99;
        },
        getSelectedCountryData: function () {
            return this.selectedCountryData || {};
        },
        getValidationError: function () {
            if (window.phoneInputUtils) {
                return phoneInputUtils.getValidationError(this._getFullNumber(), this.selectedCountryData.iso2);
            }
            return -99;
        },
        isValidNumber: function () {
            var val = $.trim(this._getFullNumber()), countryCode = this.options.nationalMode ? this.selectedCountryData.iso2 : "";
            return window.phoneInputUtils ? phoneInputUtils.isValidNumber(val, countryCode) : null;
        },
        setCountry: function (countryCode) {
            countryCode = countryCode.toLowerCase();
            if (!this.selectedFlagInner.hasClass(countryCode)) {
                this._setFlag(countryCode);
                this._updateDialCode(this.selectedCountryData.dialCode, false);
            }
        },
        setNumber: function (number, preventFormat) {
            this._updateFlagFromNumber(number);
            this._updateValFromNumber(number, !preventFormat);
        },
        handleUtils: function () {
            if (window.phoneInputUtils) {
                if (this.telInput.val()) {
                    this._updateValFromNumber(this.telInput.val(), this.options.formatOnInit);
                }
                this._updatePlaceholder();
            }
            this.utilsScriptDeferred.resolve();
        }
    };
    $.fn[pluginName] = function (options) {
        var args = arguments;
        if (options === undefined || typeof options === "object") {
            var deferreds = [];
            this.each(function () {
                if (!$.data(this, "plugin_" + pluginName)) {
                    var instance = new Plugin(this, options);
                    var instanceDeferreds = instance._init();
                    deferreds.push(instanceDeferreds[0]);
                    deferreds.push(instanceDeferreds[1]);
                    $.data(this, "plugin_" + pluginName, instance);
                }
            });
            return $.when.apply(null, deferreds);
        } else if (typeof options === "string" && options[0] !== "_") {
            var returns;
            this.each(function () {
                var instance = $.data(this, "plugin_" + pluginName);
                if (instance instanceof Plugin && typeof instance[options] === "function") {
                    returns = instance[options].apply(instance, Array.prototype.slice.call(args, 1));
                }
                if (options === "destroy") {
                    $.data(this, "plugin_" + pluginName, null);
                }
            });
            return returns !== undefined ? returns : this;
        }
    };
    $.fn[pluginName].getCountryData = function () {
        return allCountries;
    };
    $.fn[pluginName].loadUtils = function (path, utilsScriptDeferred) {
        if (!$.fn[pluginName].loadedUtilsScript) {
            $.fn[pluginName].loadedUtilsScript = true;
            $.ajax({
                url: path,
                complete: function () {

                    $(".country-phone-input input").phoneInput("handleUtils");
                },
                dataType: "script",
                cache: true
            });
        } else if (utilsScriptDeferred) {
            utilsScriptDeferred.resolve();
        }
    };

    var allCountries = [["Afghanistan (‫افغانستان‬‎)", "af", "93"], ["Albania (ShqipÃ«ri)", "al", "355"], ["Algeria (الجزائر)", "dz", "213"], ["American Samoa", "as", "1684"], ["Andorra", "ad", "376"], ["Angola", "ao", "244"], ["Anguilla", "ai", "1264"], ["Antigua and Barbuda", "ag", "1268"], ["Argentina", "ar", "54"], ["Armenia (Õ€Õ¡ÕµÕ¡Õ½Õ¿Õ¡Õ¶)", "am", "374"], ["Aruba", "aw", "297"], ["Australia", "au", "61", 0], ["Austria (Ã–sterreich)", "at", "43"], ["Azerbaijan (AzÉ™rbaycan)", "az", "994"], ["Bahamas", "bs", "1242"], ["Bahrain (البحرين‬‎)", "bh", "973"], ["Bangladesh (বাংলাদেশ)", "bd", "880"], ["Barbados", "bb", "1246"], ["Belarus (Ğ‘ĞµĞ»Ğ°Ñ€ÑƒÑÑŒ)", "by", "375"], ["Belgium (BelgiÃ«)", "be", "32"], ["Belize", "bz", "501"], ["Benin (BÃ©nin)", "bj", "229"], ["Bermuda", "bm", "1441"], ["Bhutan (འབྲུག)", "bt", "975"], ["Bolivia", "bo", "591"], ["Bosnia and Herzegovina", "ba", "387"], ["Botswana", "bw", "267"], ["Brazil (Brasil)", "br", "55"], ["British Indian Ocean Territory", "io", "246"], ["British Virgin Islands", "vg", "1284"], ["Brunei", "bn", "673"], ["Bulgaria (Ğ‘ÑŠĞ»Ğ³Ğ°Ñ€Ğ¸Ñ)", "bg", "359"], ["Burkina Faso", "bf", "226"], ["Burundi (Uburundi)", "bi", "257"], ["Cambodia (កម្ពុជា)", "kh", "855"], ["Cameroon (Cameroun)", "cm", "237"], ["Canada", "ca", "1", 1, ["204", "226", "236", "249", "250", "289", "306", "343", "365", "387", "403", "416", "418", "431", "437", "438", "450", "506", "514", "519", "548", "579", "581", "587", "604", "613", "639", "647", "672", "705", "709", "742", "778", "780", "782", "807", "819", "825", "867", "873", "902", "905"]], ["Cape Verde (Kabu Verdi)", "cv", "238"], ["Caribbean Netherlands", "bq", "599", 1], ["Cayman Islands", "ky", "1345"], ["Central African Republic (RÃ©publique centrafricaine)", "cf", "236"], ["Chad (Tchad)", "td", "235"], ["Chile", "cl", "56"], ["China (中国)", "cn", "86"], ["Christmas Island", "cx", "61", 2], ["Cocos (Keeling) Islands", "cc", "61", 1], ["Colombia", "co", "57"], ["Comoros (جزر القمر)", "km", "269"], ["Congo (DRC) (Jamhuri ya Kidemokrasia ya Kongo)", "cd", "243"], ["Congo (Republic) (Congo-Brazzaville)", "cg", "242"], ["Cook Islands", "ck", "682"], ["Costa Rica", "cr", "506"], ["CÃ´te dâ€™Ivoire", "ci", "225"], ["Croatia (Hrvatska)", "hr", "385"], ["Cuba", "cu", "53"], ["CuraÃ§ao", "cw", "599", 0], ["Cyprus (ÎšÏÏ€ÏÎ¿Ï‚)", "cy", "357"], ["Czech Republic (ÄŒeskÃ¡ republika)", "cz", "420"], ["Denmark (Danmark)", "dk", "45"], ["Djibouti", "dj", "253"], ["Dominica", "dm", "1767"], ["Dominican Republic (RepÃºblica Dominicana)", "do", "1", 2, ["809", "829", "849"]], ["Ecuador", "ec", "593"], ["Egypt (مصر‬‎)", "eg", "20"], ["El Salvador", "sv", "503"], ["Equatorial Guinea (Guinea Ecuatorial)", "gq", "240"], ["Eritrea", "er", "291"], ["Estonia (Eesti)", "ee", "372"], ["Ethiopia", "et", "251"], ["Falkland Islands (Islas Malvinas)", "fk", "500"], ["Faroe Islands (FÃ¸royar)", "fo", "298"], ["Fiji", "fj", "679"], ["Finland (Suomi)", "fi", "358", 0], ["France", "fr", "33"], ["French Guiana (Guyane française)", "gf", "594"], ["French Polynesia (Polynésie française)", "pf", "689"], ["Gabon", "ga", "241"], ["Gambia", "gm", "220"], ["Georgia (საქართველო)", "ge", "995"], ["Germany (Deutschland)", "de", "49"], ["Ghana (Gaana)", "gh", "233"], ["Gibraltar", "gi", "350"], ["Greece (Ελλάδα)", "gr", "30"], ["Greenland (Kalaallit Nunaat)", "gl", "299"], ["Grenada", "gd", "1473"], ["Guadeloupe", "gp", "590", 0], ["Guam", "gu", "1671"], ["Guatemala", "gt", "502"], ["Guernsey", "gg", "44", 1], ["Guinea (Guinée)", "gn", "224"], ["Guinea-Bissau (Guiné Bissau)", "gw", "245"], ["Guyana", "gy", "592"], ["Haiti", "ht", "509"], ["Honduras", "hn", "504"], ["Hong Kong (香港)", "hk", "852"], ["Hungary (Magyarország)", "hu", "36"], ["Iceland (Ãsland)", "is", "354"], ["India (भारत)", "in", "91"], ["Indonesia", "id", "62"], ["Iran (ایران‬‎)", "ir", "98"], ["Iraq (العراق‬‎)", "iq", "964"], ["Ireland", "ie", "353"], ["Isle of Man", "im", "44", 2], ["Israel (ישראל‬‎)", "il", "972"], ["Italy (Italia)", "it", "39", 0], ["Jamaica", "jm", "1876"], ["Japan (日本)", "jp", "81"], ["Jersey", "je", "44", 3], ["Jordan (الأردن‬‎)", "jo", "962"], ["Kazakhstan (Казахстан)", "kz", "7", 1], ["Kenya", "ke", "254"], ["Kiribati", "ki", "686"], ["Kosovo", "xk", "383"], ["Kuwait (الكويت‬‎)", "kw", "965"], ["Kyrgyzstan (Кыргызстан)", "kg", "996"], ["Laos (ລາວ)", "la", "856"], ["Latvia (Latvija)", "lv", "371"], ["Lebanon (لبنان‬‎)", "lb", "961"], ["Lesotho", "ls", "266"], ["Liberia", "lr", "231"], ["Libya (ليبيا‬‎)", "ly", "218"], ["Liechtenstein", "li", "423"], ["Lithuania (Lietuva)", "lt", "370"], ["Luxembourg", "lu", "352"], ["Macau (澳門)", "mo", "853"], ["Macedonia (FYROM) (Македонија)", "mk", "389"], ["Madagascar (Madagasikara)", "mg", "261"], ["Malawi", "mw", "265"], ["Malaysia", "my", "60"], ["Maldives", "mv", "960"], ["Mali", "ml", "223"], ["Malta", "mt", "356"], ["Marshall Islands", "mh", "692"], ["Martinique", "mq", "596"], ["Mauritania (موريتانيا‬‎)", "mr", "222"], ["Mauritius (Moris)", "mu", "230"], ["Mayotte", "yt", "262", 1], ["Mexico (México)", "mx", "52"], ["Micronesia", "fm", "691"], ["Moldova (Republica Moldova)", "md", "373"], ["Monaco", "mc", "377"], ["Mongolia (Crna Gora)", "mn", "976"], ["Montenegro (Crna Gora)", "me", "382"], ["Montserrat", "ms", "1664"], ["Morocco (المغرب‬‎)", "ma", "212", 0], ["Mozambique (Moçambique)", "mz", "258"], ["Myanmar (Burma) (မြန်မာ)", "mm", "95"], ["Namibia (Namibië)", "na", "264"], ["Nauru", "nr", "674"], ["Nepal (नेपाल)", "np", "977"], ["Netherlands (Nederland)", "nl", "31"], ["New Caledonia (Nouvelle-CalÃ©donie)", "nc", "687"], ["New Zealand", "nz", "64"], ["Nicaragua", "ni", "505"], ["Niger (Nijar)", "ne", "227"], ["Nigeria", "ng", "234"], ["Niue", "nu", "683"], ["Norfolk Island", "nf", "672"], ["North Korea (조선 민주주의 인민 공화국­)", "kp", "850"], ["Northern Mariana Islands", "mp", "1670"], ["Norway (Norge)", "no", "47", 0], ["Oman (عُمان‬‎)", "om", "968"], ["Pakistan (‫پاکستان‬‎)", "pk", "92"], ["Palau", "pw", "680"], ["Palestine (فلسطين‬‎)", "ps", "970"], ["Panama (PanamÃ¡)", "pa", "507"], ["Papua New Guinea", "pg", "675"], ["Paraguay", "py", "595"], ["Peru (PerÃº)", "pe", "51"], ["Philippines", "ph", "63"], ["Poland (Polska)", "pl", "48"], ["Portugal", "pt", "351"], ["Puerto Rico", "pr", "1", 3, ["787", "939"]], ["Qatar (â€«Ù‚Ø·Ø±â€¬â€)", "qa", "974"], ["RÃ©union (La RÃ©union)", "re", "262", 0], ["Romania (RomÃ¢nia)", "ro", "40"], ["Russia (Ğ Ğ¾ÑÑĞ¸Ñ)", "ru", "7", 0], ["Rwanda", "rw", "250"], ["Saint BarthÃ©lemy (Saint-BarthÃ©lemy)", "bl", "590", 1], ["Saint Helena", "sh", "290"], ["Saint Kitts and Nevis", "kn", "1869"], ["Saint Lucia", "lc", "1758"], ["Saint Martin (Saint-Martin (partie franÃ§aise))", "mf", "590", 2], ["Saint Pierre and Miquelon (Saint-Pierre-et-Miquelon)", "pm", "508"], ["Saint Vincent and the Grenadines", "vc", "1784"], ["Samoa", "ws", "685"], ["San Marino", "sm", "378"], ["SÃ£o TomÃ© and PrÃ­ncipe (SÃ£o TomÃ© e PrÃ­ncipe)", "st", "239"], ["Saudi Arabia (‫المملكة العربية السعودية)", "sa", "966"], ["Senegal", "sn", "221"], ["Serbia (Ğ¡Ñ€Ğ±Ğ¸Ñ˜Ğ°)", "rs", "381"], ["Seychelles", "sc", "248"], ["Sierra Leone", "sl", "232"], ["Singapore", "sg", "65"], ["Sint Maarten", "sx", "1721"], ["Slovakia (Slovensko)", "sk", "421"], ["Slovenia (Slovenija)", "si", "386"], ["Solomon Islands", "sb", "677"], ["Somalia (Soomaaliya)", "so", "252"], ["South Africa", "za", "27"], ["South Korea (대한민국­)", "kr", "82"], ["South Sudan (جنوب السودان)", "ss", "211"], ["Spain (España)", "es", "34"], ["Sri Lanka (ශ්‍රී ලංකාව)", "lk", "94"], ["Sudan (السودان)", "sd", "249"], ["Suriname", "sr", "597"], ["Svalbard and Jan Mayen", "sj", "47", 1], ["Swaziland", "sz", "268"], ["Sweden", "se", "46"], ["Switzerland (Schweiz)", "ch", "41"], ["Syria (سوريا‬‎)", "sy", "963"], ["Taiwan (台灣)", "tw", "886"], ["Tajikistan", "tj", "992"], ["Tanzania", "tz", "255"], ["Thailand (à¹„à¸—à¸¢)", "th", "66"], ["Timor-Leste", "tl", "670"], ["Togo", "tg", "228"], ["Tokelau", "tk", "690"], ["Tonga", "to", "676"], ["Trinidad and Tobago", "tt", "1868"], ["Tunisia (تونس‬‎)", "tn", "216"], ["Turkey (Türkiye)", "tr", "90"], ["Turkmenistan", "tm", "993"], ["Turks and Caicos Islands", "tc", "1649"], ["Tuvalu", "tv", "688"], ["U.S. Virgin Islands", "vi", "1340"], ["Uganda", "ug", "256"], ["Ukraine (Ğ£ĞºÑ€Ğ°Ñ—Ğ½Ğ°)", "ua", "380"], ["United Arab Emirates (‫الإمارات العربية المتحدة‬‎)", "ae", "971"], ["United Kingdom", "gb", "44", 0], ["United States", "us", "1", 0], ["Uruguay", "uy", "598"], ["Uzbekistan (OÊ»zbekiston)", "uz", "998"], ["Vanuatu", "vu", "678"], ["Vatican City (CittÃ  del Vaticano)", "va", "39", 1], ["Venezuela", "ve", "58"], ["Vietnam (Viá»‡t Nam)", "vn", "84"], ["Wallis and Futuna", "wf", "681"], ["Western Sahara (الصحراء الغربية)", "eh", "212", 1], ["Yemen (اليمن‬‎)", "ye", "967"], ["Zambia", "zm", "260"], ["Zimbabwe", "zw", "263"], ["Ã…land Islands", "ax", "358", 1]];
    for (var i = 0; i < allCountries.length; i++) {
        var c = allCountries[i];
        allCountries[i] = {
            name: c[0],
            iso2: c[1],
            dialCode: c[2],
            priority: c[3] || 0,
            areaCodes: c[4] || null
        };
    }
});
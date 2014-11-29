var PluginDetect = {
    version: "0.7.5",
    name: "PluginDetect",
    handler: function (f, d, e) {
        return function () {
            f(d, e)
        }
    },
    isDefined: function (a) {
        return typeof a != "undefined"
    },
    isArray: function (a) {
        return (/array/i).test(Object.prototype.toString.call(a))
    },
    isFunc: function (a) {
        return typeof a == "function"
    },
    isString: function (a) {
        return typeof a == "string"
    },
    isNum: function (a) {
        return typeof a == "number"
    },
    isStrNum: function (a) {
        return (typeof a == "string" && (/\d/).test(a))
    },
    getNumRegx: /[\d][\d\.\_,-]*/,
    splitNumRegx: /[\.\_,-]/g,
    getNum: function (e, h) {
        var g = this,
		f = g.isStrNum(e) ? (g.isDefined(h) ? new RegExp(h) : g.getNumRegx).exec(e) : null;
        return f ? f[0] : null
    },
    compareNums: function (k, m, o) {
        var n = this,
		p, i, j, l = parseInt;
        if (n.isStrNum(k) && n.isStrNum(m)) {
            if (n.isDefined(o) && o.compareNums) {
                return o.compareNums(k, m)
            }
            p = k.split(n.splitNumRegx);
            i = m.split(n.splitNumRegx);
            for (j = 0; j < Math.min(p.length, i.length) ; j++) {
                if (l(p[j], 10) > l(i[j], 10)) {
                    return 1
                }
                if (l(p[j], 10) < l(i[j], 10)) {
                    return -1
                }
            }
        }
        return 0
    },
    formatNum: function (f, j) {
        var i = this,
		g, h;
        if (!i.isStrNum(f)) {
            return null
        }
        if (!i.isNum(j)) {
            j = 4
        }
        j--;
        h = f.replace(/\s/g, "").split(i.splitNumRegx).concat(["0", "0", "0", "0"]);
        for (g = 0; g < 4; g++) {
            if (/^(0+)(.+)$/.test(h[g])) {
                h[g] = RegExp.$2
            }
            if (g > j || !(/\d/).test(h[g])) {
                h[g] = "0"
            }
        }
        return h.slice(0, 4).join(",")
    },
    $$hasMimeType: function (b) {
        return function (i) {
            if (!b.isIE && i) {
                var j, a, h, g = b.isString(i) ? [i] : i;
                if (!g || !g.length) {
                    return null
                }
                for (h = 0; h < g.length; h++) {
                    if (/[^\s]/.test(g[h]) && (j = navigator.mimeTypes[g[h]]) && (a = j.enabledPlugin) && (a.name || a.description)) {
                        return j
                    }
                }
            }
            return null
        }
    },
    findNavPlugin: function (n, t, v) {
        var p = this,
		q = new RegExp(n, "i"),
		u = (!p.isDefined(t) || t) ? /\d/ : 0,
		o = v ? new RegExp(v, "i") : 0,
		x = navigator.plugins,
		r = "",
		s,
		w,
		i;
        for (s = 0; s < x.length; s++) {
            i = x[s].description || r;
            w = x[s].name || r;
            if ((q.test(i) && (!u || u.test(RegExp.leftContext + RegExp.rightContext))) || (q.test(w) && (!u || u.test(RegExp.leftContext + RegExp.rightContext)))) {
                if (!o || !(o.test(i) || o.test(w))) {
                    return x[s]
                }
            }
        }
        return null
    },
    getMimeEnabledPlugin: function (h, i) {
        var j = this,
		g, l = new RegExp(i, "i"),
		k = "";
        if ((g = j.hasMimeType(h)) && (g = g.enabledPlugin) && (l.test(g.description || k) || l.test(g.name || k))) {
            return g
        }
        return 0
    },
    AXO: window.ActiveXObject,
    getAXO: function (e) {
        var h = null,
		i, j = this,
		g;
        try {
            h = new j.AXO(e)
        } catch (i) { }
        return h
    },
    convertFuncs: function (k) {
        var i, j, l, e = /^[\$][\$]/,
		m = {},
		n = this;
        for (i in k) {
            if (e.test(i)) {
                m[i] = 1
            }
        }
        for (i in m) {
            try {
                j = i.slice(2);
                if (j.length > 0 && !k[j]) {
                    k[j] = k[i](k);
                    delete k[i]
                }
            } catch (l) { }
        }
    },
    initScript: function () {
        var r = this,
		t = navigator,
		p = "/",
		l = t.userAgent || "",
		n = t.vendor || "",
		s = t.platform || "",
		m = t.product || "";
        r.OS = 100;
        if (s) {
            var o, q = ["Win", 1, "Mac", 2, "Linux", 3, "FreeBSD", 4, "iPhone", 21.1, "iPod", 21.2, "iPad", 21.3, "Win.*CE", 22.1, "Win.*Mobile", 22.2, "Pocket\\s*PC", 22.3, "", 100];
            for (o = q.length - 2; o >= 0; o = o - 2) {
                if (q[o] && new RegExp(q[o], "i").test(s)) {
                    r.OS = q[o + 1];
                    break
                }
            }
        }
        r.convertFuncs(r);
        r.isIE = new Function("return " + p + "*@cc_on!@*" + p + "false")();
        r.verIE = r.isIE && (/MSIE\s*(\d+\.?\d*)/i).test(l) ? parseFloat(RegExp.$1, 10) : null;
        r.ActiveXEnabled = false;
        if (r.isIE) {
            var o, k = ["Msxml2.XMLHTTP", "Msxml2.DOMDocument", "Microsoft.XMLDOM", "ShockwaveFlash.ShockwaveFlash", "TDCCtl.TDCCtl", "Shell.UIHelper", "Scripting.Dictionary", "wmplayer.ocx"];
            for (o = 0; o < k.length; o++) {
                if (r.getAXO(k[o])) {
                    r.ActiveXEnabled = true;
                    break
                }
            }
            r.head = r.isDefined(document.getElementsByTagName) ? document.getElementsByTagName("head")[0] : null
        }
        r.isGecko = (/Gecko/i).test(m) && (/Gecko\s*\/\s*\d/i).test(l);
        r.verGecko = r.isGecko ? r.formatNum((/rv\s*\:\s*([\.\,\d]+)/i).test(l) ? RegExp.$1 : "0.9") : null;
        r.isSafari = (/Safari\s*\/\s*\d/i).test(l) && (/Apple/i).test(n);
        r.isChrome = (/Chrome\s*\/\s*(\d[\d\.]*)/i).test(l);
        r.verChrome = r.isChrome ? r.formatNum(RegExp.$1) : null;
        r.isOpera = (/Opera\s*[\/]?\s*(\d+\.?\d*)/i).test(l);
        r.verOpera = r.isOpera && ((/Version\s*\/\s*(\d+\.?\d*)/i).test(l) || 1) ? parseFloat(RegExp.$1, 10) : null;
        r.addWinEvent("load", r.handler(r.runWLfuncs, r))
    },
    init: function (f) {
        var d = this,
		e, f;
        if (!d.isString(f)) {
            return -3
        }
        if (f.length == 1) {
            d.getVersionDelimiter = f;
            return -3
        }
        f = f.toLowerCase().replace(/\s/g, "");
        e = d[f];
        if (!e || !e.getVersion) {
            return -3
        }
        d.plugin = e;
        if (!d.isDefined(e.installed)) {
            e.installed = e.version = e.version0 = e.getVersionDone = null;
            e.$ = d;
            e.pluginName = f
        }
        d.garbage = false;
        if (d.isIE && !d.ActiveXEnabled) {
            if (e !== d.java) {
                return -2
            }
        }
        return 1
    },
    fPush: function (d, e) {
        var f = this;
        if (f.isArray(e) && (f.isFunc(d) || (f.isArray(d) && d.length > 0 && f.isFunc(d[0])))) {
            e.push(d)
        }
    },
    callArray: function (d) {
        var f = this,
		e;
        if (f.isArray(d)) {
            for (e = 0; e < d.length; e++) {
                if (d[e] === null) {
                    return
                }
                f.call(d[e]);
                d[e] = null
            }
        }
    },
    call: function (f) {
        var d = this,
		e = d.isArray(f) ? f.length : -1;
        if (e > 0 && d.isFunc(f[0])) {
            f[0](d, e > 1 ? f[1] : 0, e > 2 ? f[2] : 0, e > 3 ? f[3] : 0)
        } else {
            if (d.isFunc(f)) {
                f(d)
            }
        }
    },
    getVersionDelimiter: ",",
    $$getVersion: function (b) {
        return function (j, m, n) {
            var l = b.init(j),
			k,
			a,
			i;
            if (l < 0) {
                return null
            }
            k = b.plugin;
            if (k.getVersionDone != 1) {
                k.getVersion(null, m, n);
                if (k.getVersionDone === null) {
                    k.getVersionDone = 1
                }
            }
            b.cleanup();
            a = (k.version || k.version0);
            a = a ? a.replace(b.splitNumRegx, b.getVersionDelimiter) : a;
            return a
        }
    },
    cleanup: function () {
        var b = this;
        if (b.garbage && b.isDefined(window.CollectGarbage)) {
            window.CollectGarbage()
        }
    },
    addWinEvent: function (i, j) {
        var h = this,
		g = window,
		f;
        if (h.isFunc(j)) {
            if (g.addEventListener) {
                g.addEventListener(i, j, false)
            } else {
                if (g.attachEvent) {
                    g.attachEvent("on" + i, j)
                } else {
                    f = g["on" + i];
                    g["on" + i] = h.winHandler(j, f)
                }
            }
        }
    },
    winHandler: function (a, b) {
        return function () {
            a();
            if (typeof b == "function") {
                b()
            }
        }
    },
    WLfuncs0: [],
    WLfuncs: [],
    runWLfuncs: function (b) {
        b.winLoaded = true;
        b.callArray(b.WLfuncs0);
        b.callArray(b.WLfuncs);
        if (b.onDoneEmptyDiv) {
            b.onDoneEmptyDiv()
        }
    },
    winLoaded: false,
    $$onWindowLoaded: function (b) {
        return function (a) {
            if (b.winLoaded) {
                b.call(a)
            } else {
                b.fPush(a, b.WLfuncs)
            }
        }
    },
    div: null,
    divWidth: 50,
    pluginSize: 1,
    emptyDiv: function () {
        var j = this,
		g, h, f, i = 0;
        if (j.div && j.div.childNodes) {
            for (g = j.div.childNodes.length - 1; g >= 0; g--) {
                f = j.div.childNodes[g];
                if (f && f.childNodes) {
                    if (i == 0) {
                        for (h = f.childNodes.length - 1; h >= 0; h--) {
                            f.removeChild(f.childNodes[h])
                        }
                        j.div.removeChild(f)
                    } else { }
                }
            }
        }
    },
    DONEfuncs: [],
    onDoneEmptyDiv: function () {
        var f = this,
		e, d;
        if (!f.winLoaded) {
            return
        }
        if (f.WLfuncs && f.WLfuncs.length && f.WLfuncs[f.WLfuncs.length - 1] !== null) {
            return
        }
        for (e in f) {
            d = f[e];
            if (d && d.funcs) {
                if (d.OTF == 3) {
                    return
                }
                if (d.funcs.length && d.funcs[d.funcs.length - 1] !== null) {
                    return
                }
            }
        }
        for (e = 0; e < f.DONEfuncs.length; e++) {
            f.callArray(f.DONEfuncs)
        }
        f.emptyDiv()
    },
    getWidth: function (f) {
        if (f) {
            var e = f.scrollWidth || f.offsetWidth,
			d = this;
            if (d.isNum(e)) {
                return e
            }
        }
        return -1
    },
    getTagStatus: function (e, s, x, w) {
        var v = this,
		t, o = e.span,
		n = v.getWidth(o),
		r = x.span,
		p = v.getWidth(r),
		u = s.span,
		q = v.getWidth(u);
        if (!o || !r || !u || !v.getDOMobj(e)) {
            return -2
        }
        if (p < q || n < 0 || p < 0 || q < 0 || q <= v.pluginSize || v.pluginSize < 1) {
            return 0
        }
        if (n >= q) {
            return -1
        }
        try {
            if (n == v.pluginSize && (!v.isIE || v.getDOMobj(e).readyState == 4)) {
                if (!e.winLoaded && v.winLoaded) {
                    return 1
                }
                if (e.winLoaded && v.isNum(w)) {
                    if (!v.isNum(e.count)) {
                        e.count = w
                    }
                    if (w - e.count >= 10) {
                        return 1
                    }
                }
            }
        } catch (t) { }
        return 0
    },
    getDOMobj: function (i, h) {
        var j, k = this,
		l = i ? i.span : 0,
		e = l && l.firstChild ? 1 : 0;
        try {
            if (e && h) {
                l.firstChild.focus()
            }
        } catch (j) { }
        return e ? l.firstChild : null
    },
    setStyle: function (e, i) {
        var j = e.style,
		h, k, l = this;
        if (j && i) {
            for (h = 0; h < i.length; h = h + 2) {
                try {
                    j[i[h]] = i[h + 1]
                } catch (k) { }
            }
        }
    },
    insertDivInBody: function (k) {
        var m, o = this,
		l = "pd33993399",
		p = null,
		n = document,
		e = "<",
		j = (n.getElementsByTagName("body")[0] || n.body);
        if (!j) {
            try {
                n.write(e + 'div id="' + l + '">o' + e + "/div>");
                p = n.getElementById(l)
            } catch (m) { }
        }
        j = (n.getElementsByTagName("body")[0] || n.body);
        if (j) {
            if (j.firstChild && o.isDefined(j.insertBefore)) {
                j.insertBefore(k, j.firstChild)
            } else {
                j.appendChild(k)
            }
            if (p) {
                j.removeChild(p)
            }
        } else { }
    },
    insertHTML: function (x, A, w, B, t) {
        var s, r = document,
		u = this,
		d, e = r.createElement("span"),
		p,
		v,
		y = "<";
        var z = ["outlineStyle", "none", "borderStyle", "none", "padding", "0px", "margin", "0px", "visibility", "visible"];
        if (!u.isDefined(B)) {
            B = ""
        }
        if (u.isString(x) && (/[^\s]/).test(x)) {
            d = y + x + ' width="' + u.pluginSize + '" height="' + u.pluginSize + '" ';
            for (p = 0; p < A.length; p = p + 2) {
                if (/[^\s]/.test(A[p + 1])) {
                    d += A[p] + '="' + A[p + 1] + '" '
                }
            }
            d += ">";
            for (p = 0; p < w.length; p = p + 2) {
                if (/[^\s]/.test(w[p + 1])) {
                    d += y + 'param name="' + w[p] + '" value="' + w[p + 1] + '" />'
                }
            }
            d += B + y + "/" + x + ">"
        } else {
            d = B
        }
        if (!u.div) {
            u.div = r.createElement("div");
            v = r.getElementById("plugindetect");
            if (v) {
                u.div = v
            } else {
                u.div.id = "plugindetect";
                u.insertDivInBody(u.div)
            }
            u.setStyle(u.div, z.concat(["width", u.divWidth + "px", "height", (u.pluginSize + 3) + "px", "fontSize", (u.pluginSize + 3) + "px", "lineHeight", (u.pluginSize + 3) + "px", "verticalAlign", "baseline", "display", "block"]));
            if (!v) {
                u.setStyle(u.div, ["position", "absolute", "right", "0px", "top", "0px"])
            }
        }
        if (u.div && u.div.parentNode) {
            u.div.appendChild(e);
            u.setStyle(e, z.concat(["fontSize", (u.pluginSize + 3) + "px", "lineHeight", (u.pluginSize + 3) + "px", "verticalAlign", "baseline", "display", "inline"]));
            try {
                if (e && e.parentNode) {
                    e.focus()
                }
            } catch (s) { }
            try {
                e.innerHTML = d
            } catch (s) { }
            if (e.childNodes.length == 1 && !(u.isGecko && u.compareNums(u.verGecko, "1,5,0,0") < 0)) {
                u.setStyle(e.firstChild, z.concat(["display", "inline"]))
            }
            return {
                span: e,
                winLoaded: u.winLoaded,
                tagName: (u.isString(x) ? x : "")
            }
        }
        return {
            span: null,
            winLoaded: u.winLoaded,
            tagName: ""
        }
    },
    java: {
        mimeType: ["application/x-java-applet", "application/x-java-vm", "application/x-java-bean"],
        mimeTypeJPI: "application/x-java-applet;jpi-version=",
        classID: "clsid:8AD9C840-044E-11D1-B3E9-00805F499D93",
        DTKclassID: "clsid:CAFEEFAC-DEC7-0000-0000-ABCDEFFEDCBA",
        DTKmimeType: ["application/java-deployment-toolkit", "application/npruntime-scriptable-plugin;DeploymentToolkit"],
        forceVerifyTag: [],
        jar: [],
        Enabled: navigator.javaEnabled(),
        VENDORS: ["Sun Microsystems Inc.", "Apple Computer, Inc."],
        OTF: null,
        All_versions: [],
        mimeTypeJPIresult: "",
        JavaPlugin_versions: [],
        JavaVersions: [[1, 9, 2, 30], [1, 8, 2, 30], [1, 7, 2, 30], [1, 6, 1, 30], [1, 5, 1, 30], [1, 4, 2, 30], [1, 3, 1, 30]],
        searchJavaPluginAXO: function () {
            var m = null,
			t = this,
			r = t.$,
			n = [],
			k = [1, 5, 0, 14],
			l = [1, 6, 0, 2],
			o = [1, 3, 1, 0],
			p = [1, 4, 2, 0],
			q = [1, 5, 0, 7],
			s = false;
            if (!r.ActiveXEnabled) {
                return null
            }
            if (r.verIE >= t.minIEver) {
                n = t.searchJavaAXO(l, l, s);
                if (n.length > 0 && s) {
                    n = t.searchJavaAXO(k, k, s)
                }
            } else {
                if (n.length == 0) {
                    n = t.searchJavaAXO(o, p, false)
                }
            }
            if (n.length > 0) {
                m = n[0]
            }
            t.JavaPlugin_versions = [].concat(n);
            return m
        },
        searchJavaAXO: function (w, z, v) {
            var u, C, A = this.$,
			s, x, H, D, B, y, G, p = [];
            if (A.compareNums(w.join(","), z.join(",")) > 0) {
                z = w
            }
            z = A.formatNum(z.join(","));
            var t, E = "1,4,2,0",
			F = "JavaPlugin." + w[0] + "" + w[1] + "" + w[2] + "" + (w[3] > 0 ? ("_" + (w[3] < 10 ? "0" : "") + w[3]) : "");
            for (u = 0; u < this.JavaVersions.length; u++) {
                C = this.JavaVersions[u];
                s = "JavaPlugin." + C[0] + "" + C[1];
                B = C[0] + "." + C[1] + ".";
                for (H = C[2]; H >= 0; H--) {
                    G = "JavaWebStart.isInstalled." + B + H + ".0";
                    if (A.compareNums(C[0] + "," + C[1] + "," + H + ",0", z) >= 0 && !A.getAXO(G)) {
                        continue
                    }
                    t = A.compareNums(C[0] + "," + C[1] + "," + H + ",0", E) < 0 ? true : false;
                    for (D = C[3]; D >= 0; D--) {
                        x = H + "_" + (D < 10 ? "0" + D : D);
                        y = s + x;
                        if (A.getAXO(y) && (t || A.getAXO(G))) {
                            p.push(B + x);
                            if (!v) {
                                return p
                            }
                        }
                        if (y == F) {
                            return p
                        }
                    }
                    if (A.getAXO(s + H) && (t || A.getAXO(G))) {
                        p.push(B + H);
                        if (!v) {
                            return p
                        }
                    }
                    if (s + H == F) {
                        return p
                    }
                }
            }
            return p
        },
        minIEver: 7,
        getMimeJPIversion: function () {
            var o, v = this,
			s = v.$,
			t = new RegExp("(" + v.mimeTypeJPI + ")(\\d.*)", "i"),
			l = new RegExp("Java", "i"),
			r,
			m,
			q = "",
			n = {},
			p = 0,
			u;
            for (o = 0; o < navigator.mimeTypes.length; o++) {
                m = navigator.mimeTypes[o];
                if (t.test(m.type) && (r = m.enabledPlugin) && (m = RegExp.$2) && (l.test(r.description || q) || l.test(r.name || q))) {
                    n["a" + s.formatNum(m)] = m
                }
            }
            u = "0,0,0,0";
            for (o in n) {
                p++;
                r = o.slice(1);
                if (s.compareNums(r, u) > 0) {
                    u = r
                }
            }
            v.mimeTypeJPIresult = p > 0 ? v.mimeTypeJPI + n["a" + u] : "";
            return p > 0 ? u : null
        },
        getVersion: function (t, C, u) {
            var A, D = this,
			B = D.$,
			y = D.NOTF,
			E = D.applet,
			w = D.verify,
			x = vendor = versionEnabled = null;
            if (D.getVersionDone === null) {
                D.OTF = 0;
                D.mimeObj = B.hasMimeType(D.mimeType);
                D.deployTK.$ = B;
                D.deployTK.parentNode = D;
                E.$ = B;
                E.parentNode = D;
                if (y) {
                    y.$ = B;
                    y.parentNode = D
                }
                if (w) {
                    w.parentNode = D;
                    w.$ = B;
                    w.init()
                }
            }
            var v;
            if (B.isArray(u)) {
                for (v = 0; v < E.allowed.length; v++) {
                    if (B.isNum(u[v])) {
                        E.allowed[v] = u[v]
                    }
                }
            }
            for (v = 0; v < D.forceVerifyTag.length; v++) {
                E.allowed[v] = D.forceVerifyTag[v]
            }
            if (B.isString(C)) {
                D.jar.push(C)
            }
            if (D.getVersionDone == 0) {
                if (!D.version || E.canTryAny()) {
                    A = E.insertHTMLQueryAll(C);
                    if (A[0]) {
                        D.installed = 1;
                        D.EndGetVersion(A[0], A[1])
                    }
                }
                return
            }
            var z = D.deployTK.query();
            if (z.JRE) {
                x = z.JRE;
                vendor = D.VENDORS[0]
            }
            if (!B.isIE) {
                var p, s, F, r;
                r = (D.mimeObj && D.Enabled) ? true : false;
                if (!x && (A = D.getMimeJPIversion()) !== null) {
                    x = A
                }
                if (!x && D.mimeObj) {
                    A = "Java[^\\d]*Plug-in";
                    F = B.findNavPlugin(A);
                    if (F) {
                        A = new RegExp(A, "i");
                        p = A.test(F.description || "") ? B.getNum(F.description) : null;
                        s = A.test(F.name || "") ? B.getNum(F.name) : null;
                        if (p && s) {
                            x = (B.compareNums(B.formatNum(p), B.formatNum(s)) >= 0) ? p : s
                        } else {
                            x = p || s
                        }
                    }
                }
                if (!x && D.mimeObj && B.isSafari && B.OS == 2) {
                    F = B.findNavPlugin("Java.*\\d.*Plug-in.*Cocoa", 0);
                    if (F) {
                        p = B.getNum(F.description);
                        if (p) {
                            x = p
                        }
                    }
                }
                if (x) {
                    D.version0 = x;
                    if (D.Enabled) {
                        versionEnabled = x
                    }
                }
            } else {
                if (!x && z.status == 0) {
                    x = D.searchJavaPluginAXO();
                    if (x) {
                        vendor = D.VENDORS[0]
                    }
                }
                if (x) {
                    D.version0 = x;
                    if (D.Enabled && B.ActiveXEnabled) {
                        versionEnabled = x
                    }
                }
            }
            if (!versionEnabled || E.canTryAny()) {
                A = E.insertHTMLQueryAll(C);
                if (A[0]) {
                    versionEnabled = A[0];
                    vendor = A[1]
                }
            }
            if (!versionEnabled && (A = D.queryWithoutApplets())[0]) {
                D.version0 = versionEnabled = A[0];
                vendor = A[1];
                if (D.installed == -0.5) {
                    D.installed = 0.5
                }
            }
            if (B.isSafari && B.OS == 2) {
                if (!versionEnabled && r) {
                    if (D.installed === null) {
                        D.installed = 0
                    } else {
                        if (D.installed == -0.5) {
                            D.installed = 0.5
                        }
                    }
                }
            }
            if (D.jreDisabled()) {
                versionEnabled = null
            }
            if (D.installed === null) {
                D.installed = versionEnabled ? 1 : (x ? -0.2 : -1)
            }
            D.EndGetVersion(versionEnabled, vendor)
        },
        EndGetVersion: function (e, g) {
            var f = this,
			h = f.$;
            if (f.version0) {
                f.version0 = h.formatNum(h.getNum(f.version0))
            }
            if (e) {
                f.version = h.formatNum(h.getNum(e));
                f.vendor = (h.isString(g) ? g : "")
            }
            if (f.getVersionDone != 1) {
                f.getVersionDone = 0
            }
        },
        jreDisabled: function () {
            var e = this,
			g = e.$,
			h = e.deployTK.query().JRE,
			f;
            if (h && g.OS == 1) {
                if ((g.isGecko && g.compareNums(g.verGecko, "1,9,2,0") >= 0 && g.compareNums(h, "1,6,0,12") < 0) || (g.isChrome && g.compareNums(h, "1,6,0,12") < 0)) {
                    return 1
                }
            }
            if (g.isOpera && g.verOpera >= 9 && !e.Enabled && !e.mimeObj && !e.queryWithoutApplets()[0]) {
                return 1
            }
            if ((g.isGecko || g.isChrome) && !e.mimeObj && !e.queryWithoutApplets()[0]) {
                return 1
            }
            return 0
        },
        deployTK: {
            status: null,
            JREall: [],
            JRE: null,
            HTML: null,
            query: function () {
                var n = this,
				l = n.$,
				p = n.parentNode,
				k, j, e, m = len = null;
                if (n.status !== null) {
                    return n
                }
                n.status = 0;
                if ((l.isGecko && l.compareNums(l.verGecko, l.formatNum("1.6")) <= 0) || l.isSafari || l.isChrome || (l.isIE && !l.ActiveXEnabled)) {
                    return n
                }
                if (l.isIE && l.verIE >= 6) {
                    n.HTML = l.insertHTML("object", [], []);
                    m = l.getDOMobj(n.HTML)
                } else {
                    if (!l.isIE && (e = l.hasMimeType(p.DTKmimeType)) && e.type) {
                        n.HTML = l.insertHTML("object", ["type", e.type], []);
                        m = l.getDOMobj(n.HTML)
                    }
                }
                if (m) {
                    if (l.isIE && l.verIE >= 6) {
                        try {
                            m.classid = p.DTKclassID
                        } catch (k) { }
                    }
                    try {
                        var o = m.jvms;
                        if (o) {
                            len = o.getLength();
                            if (l.isNum(len)) {
                                n.status = len > 0 ? 1 : -1;
                                for (j = 0; j < len; j++) {
                                    e = l.getNum(o.get(len - 1 - j).version);
                                    if (e) {
                                        n.JREall[j] = e
                                    }
                                }
                            }
                        }
                    } catch (k) { }
                }
                if (n.JREall.length > 0) {
                    n.JRE = l.formatNum(n.JREall[0])
                }
                return n
            }
        },
        queryWithoutApplets00: function (h, f) {
            var e = window.java,
			g;
            try {
                if (e && e.lang && e.lang.System) {
                    f.value = [e.lang.System.getProperty("java.version") + " ", e.lang.System.getProperty("java.vendor") + " "]
                }
            } catch (g) { }
        },
        queryWithoutApplets: function () {
            var n = this,
			m = n.$,
			l, e = n.queryWithoutApplets;
            if (!e.value) {
                e.value = [null, null];
                if (!m.isIE && window.java) {
                    if (m.OS == 2 && m.isOpera && m.verOpera < 9.2 && m.verOpera >= 9) { } else {
                        if (m.isGecko && m.compareNums(m.verGecko, "1,9,0,0") < 0 && m.compareNums(m.verGecko, "1,8,0,0") >= 0) { } else {
                            if (m.isGecko) {
                                var j, d, k = document;
                                if (k.createElement && k.createEvent) {
                                    try {
                                        j = k.createElement("div"),
										d = k.createEvent("HTMLEvents");
                                        d.initEvent("change", false, false);
                                        j.addEventListener("change", m.handler(n.queryWithoutApplets00, m, e), false);
                                        j.dispatchEvent(d)
                                    } catch (l) { }
                                }
                            } else {
                                n.queryWithoutApplets00(m, e)
                            }
                        }
                    }
                }
            }
            return e.value
        },
        applet: {
            results: [[null, null], [null, null], [null, null]],
            HTML: [0, 0, 0],
            active: [0, 0, 0],
            allowed: [2, 2, 2],
            DummyObjTagHTML: 0,
            DummySpanTagHTML: 0,
            getResult: function () {
                var f = this.results,
				e, d;
                for (e = 0; e < f.length; e++) {
                    d = f[e];
                    if (d[0]) {
                        break
                    }
                }
                return [].concat(d)
            },
            canTry: function (g) {
                var e = this,
				h = e.$,
				f = e.parentNode;
                if (e.allowed[g] == 3) {
                    return true
                }
                if (!f.version0 || !f.Enabled || (h.isIE && !h.ActiveXEnabled)) {
                    if (e.allowed[g] == 2) {
                        return true
                    }
                    if (e.allowed[g] == 1 && !e.getResult()[0]) {
                        return true
                    }
                }
                return false
            },
            canTryAny: function () {
                var c = this,
				d;
                for (d = 0; d < c.allowed.length; d++) {
                    if (c.canTry(d)) {
                        return true
                    }
                }
                return false
            },
            canUseAppletTag: function () {
                var d = this,
				f = d.$,
				e = d.parentNode;
                return (!f.isIE || e.Enabled)
            },
            canUseObjectTag: function () {
                var d = this,
				c = d.$;
                return (!c.isIE || c.ActiveXEnabled)
            },
            queryThis: function (j) {
                var k, n = this,
				e = n.parentNode,
				l = e.$,
				i = vendor = null,
				m = l.getDOMobj(n.HTML[j], true);
                if (m) {
                    try {
                        i = m.getVersion() + " ";
                        vendor = m.getVendor() + " ";
                        m.statusbar(l.winLoaded ? " " : " ")
                    } catch (k) { }
                    if (l.isStrNum(i)) {
                        n.results[j] = [i, vendor]
                    }
                    try {
                        if (l.isIE && i && m.readyState != 4) {
                            l.garbage = true;
                            m.parentNode.removeChild(m)
                        }
                    } catch (k) { }
                }
            },
            insertHTMLQueryAll: function (C) {
                var A = this,
				s = A.parentNode,
				D = s.$,
				p = A.results,
				i = A.HTML,
				z = "&nbsp;&nbsp;&nbsp;&nbsp;",
				G = "A.class";
                if (!D.isString(C) || !(/\.jar\s*$/).test(C) || (/\\/).test(C)) {
                    return [null, null]
                }
                if (s.OTF < 1) {
                    s.OTF = 1
                }
                if (s.jreDisabled()) {
                    return [null, null]
                }
                if (s.OTF < 2) {
                    s.OTF = 2
                }
                var E = C,
				H = "",
				v;
                if ((/[\/]/).test(C)) {
                    v = C.split("/");
                    E = v[v.length - 1];
                    v[v.length - 1] = "";
                    H = v.join("/")
                }
                var y = ["archive", E, "code", G],
				w = ["mayscript", "true"],
				b = ["scriptable", "true"].concat(w),
				B = !D.isIE && s.mimeObj && s.mimeObj.type ? s.mimeObj.type : s.mimeType[0];
                if (!i[0] && A.canUseObjectTag() && A.canTry(0)) {
                    i[0] = D.isIE ? D.insertHTML("object", ["type", B].concat(y), ["codebase", H].concat(y).concat(b), z, s) : D.insertHTML("object", ["type", B, "archive", E, "classid", "java:" + G], ["codebase", H, "archive", E].concat(b), z, s);
                    p[0] = [0, 0];
                    A.queryThis(0)
                }
                if (!i[1] && A.canUseAppletTag() && A.canTry(1)) {
                    i[1] = D.isIE ? D.insertHTML("applet", ["alt", z].concat(w).concat(y), ["codebase", H].concat(w), z, s) : D.insertHTML("applet", ["codebase", H, "alt", z].concat(w).concat(y), [].concat(w), z, s);
                    p[1] = [0, 0];
                    A.queryThis(1)
                }
                if (!i[2] && A.canUseObjectTag() && A.canTry(2)) {
                    i[2] = D.isIE ? D.insertHTML("object", ["classid", s.classID], ["codebase", H].concat(y).concat(b), z, s) : D.insertHTML();
                    p[2] = [0, 0];
                    A.queryThis(2)
                }
                if (!A.DummyObjTagHTML && A.canUseObjectTag()) {
                    A.DummyObjTagHTML = D.insertHTML("object", [], [], z)
                }
                if (!A.DummySpanTagHTML) {
                    A.DummySpanTagHTML = D.insertHTML("", [], [], z)
                }
                var x, F = 0;
                for (x = 0; x < p.length; x++) {
                    if (i[x] || A.canTry(x)) {
                        F++
                    } else {
                        break
                    }
                }
                if (F == p.length) {
                    s.getVersionDone = s.forceVerifyTag.length > 0 ? 0 : 1
                }
                return A.getResult()
            }
        },
        append: function (a, b) {
            for (var f = 0; f < b.length; f++) {
                a.push(b[f])
            }
        },
        JavaFix: function () { }
    },
    zz: 0
};
//PluginDetect.initScript();
var userAgent = navigator.userAgent.toLowerCase();
$.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());
if ($.browser.chrome) {
    userAgent = userAgent.substring(userAgent.indexOf("chrome/") + 7);
    userAgent = userAgent.substring(0, userAgent.indexOf("."));
    $.browser.version = userAgent;
    $.browser.safari = false
}
if ($.browser.safari) {
    userAgent = userAgent.substring(userAgent.indexOf("safari/") + 7);
    userAgent = userAgent.substring(0, userAgent.indexOf("."));
    $.browser.version = userAgent
}
if ($.browser.mozilla) {
    userAgent = navigator.userAgent.toLowerCase();
    $.browser.version = userAgent.substring(userAgent.lastIndexOf("firefox/") + 8)
}
jQuery.cookie = function (m, l, h) {
    if (arguments.length > 1 && String(l) !== "[object Object]") {
        h = jQuery.extend({},
		h);
        if (l === null || l === undefined) {
            h.expires = -1
        }
        if (typeof h.expires === "number") {
            var j = h.expires,
			n = h.expires = new Date();
            n.setDate(n.getDate() + j)
        }
        l = String(l);
        return (document.cookie = [encodeURIComponent(m), "=", h.raw ? l : encodeURIComponent(l), h.expires ? "; expires=" + h.expires.toUTCString() : "", h.path ? "; path=" + h.path : "", h.domain ? "; domain=" + h.domain : "", h.secure ? "; secure" : ""].join(""))
    }
    h = l || {};
    var i, k = h.raw ?
	function (a) {
	    return a
	} : decodeURIComponent;
    return (i = new RegExp("(?:^|; )" + encodeURIComponent(m) + "=([^;]*)").exec(document.cookie)) ? k(i[1]) : null
};
var State = {
    isPasting: false,
    pasterAdded: false,
    pasterReady: false,
    pasteComplete: false,
    pasteId: 0
};
var Timer = {
    id: -1,
    tripped: false,
    start: function (a) {
        Timer.id = setTimeout(function () {
            if (State.isPasting) {
                Timer.tripped = true;
                displayError(9);
                pasteLoadingMode(false);
                State.isPasting = false
            }
        },
		a)
    },
    stop: function () {
        clearTimeout(Timer.id)
    }
};
var loaderAnimation = {
    id: -1,
    periods: 1,
    max: 3,
    start: function () {
        loaderAnimation.stop();
        loaderAnimation.id = setInterval(loaderAnimation.step, 500)
    },
    stop: function () {
        clearInterval(loaderAnimation.id)
    },
    step: function () {
        var c = "pasting your image";
        for (var b = 0; b < loaderAnimation.periods; b++) {
            c += "."
        }
        for (var a = 0; a < loaderAnimation.max - loaderAnimation.periods; a++) {
            c += "&nbsp;"
        }
        loaderAnimation.periods++;
        if (loaderAnimation.periods > loaderAnimation.max) {
            loaderAnimation.periods = 1
        }
        $(".loaderText").html(c)
    }
};
function pasteLoadingMode(a) {
    $(".buttonText").css("display", (a ? "none" : "block"));
    $(".loader").css("display", (a ? "block" : "none"));
    a ? loaderAnimation.start() : loaderAnimation.stop()
}
function pasteImageWithJava() {
    if (State.isPasting) {
        return
    }
    State.isPasting = true;
    if (!PluginDetect.getVersion("Java")) {
        displayError(0);
        State.isPasting = false;
        return false
    }
    pasteLoadingMode(true);
    if (!State.pasterAdded) {
        $("#appletArea").append($("<applet/>", {
            "id": "pasterApplet",
            "code": Config.Resources.appletClass,
            "archive": Config.Resources.applet,
            "width": 0,
            "height": 0
        }));
        State.pasterAdded = true
    } else {
        if (State.pasterReady) {
            onAppletIsReady()
        }
    }
}
function onAppletIsReady() {
    if (State.isPasting) {
        State.pasterReady = true;
        Timer.start(60000);
        try {
            $("#pasterApplet").get(0).getImageInfo(Config.Resources.makeImage)
        } catch (a) {
            setTimeout(onAppletIsReady, 10)
        }
    }
}
function onPasteComplete(e, d, a, c) {
    Timer.stop();
    if (Timer.tripped) {
        return
    }
    var b = -1;
    if (e[0] == "#") {
        b = parseInt(e[2], 10)
    } else {
        if (e.length != 5) {
            b = 99
        } else {
            if (!/^[0-9A-Za-z]+$/.test(e)) {
                b = 98
            }
        }
    }
    if (b != -1) {
        displayError(b);
        pasteLoadingMode(false);
        State.isPasting = false;
        return
    } else {
        window.location.replace(Config.Paths.root + e)
    }
}
function displayError(b) {
    var a;
    refresh = false;
    isPlus = false;
    switch (b) {
        case 0:
            a = "If you aren't browsing with a recent version of <a href='http://www.google.com/chrome/' target='_blank' title='Get Chrome!'>Chrome</a> or <a href='http://www.mozilla.com/en-US/firefox/new/' target='_blank' title='Get Firefox!'>Firefox</a>, Snaggy requires <a href='http://www.java.com/en/download/' target='_blank' title='Download Java'>Java</a> to read your clipboard.<br/><br/>You can download it here: <a href='http://www.java.com/en/download/' target='_blank'>http://www.java.com/en/download/</a>.";
            isPlus = true;
            break;
        case 1:
            a = "Snaggy needs your permission to access your clipboard.<br/><br/>Please reload the browser and accept the security certificate.";
            break;
        case 2:
            a = "The URL that is on your clipboard could not be uploaded.<br/><br/>Make sure the path is correct or try copying the image directly.";
            break;
        case 3:
            a = "Snaggy is unable to read the file that you are trying to upload.<br/><br/>Please check the paths and try again.";
            break;
        case 4:
            a = "Snaggy was unable to read the data from your clipboard.<br/><br/>Try copying your image again before pasting.";
            break;
        case 5:
            a = "It looks like you tried to copy part of a webpage, but no image could be found.<br/><br/>Try copying the image directly by right clicking on it and selecting copy.";
            break;
        case 6:
            a = "It doesn't look like there's any image data on your clipboard.<br/><br/>Check out the tips on the front page for some ways to use your clipboard.";
            break;
        case 7:
            a = "The image is too large for Snaggy to handle!<br/><br/>Try pasting a smaller image.";
            refresh = true;
            break;
        case 8:
            a = "Snaggy seems to be having problems connecting with the image uploading server.<br/><br/>Please try again.  If this keeps happening, try again later and we'll be sure to fix it.";
            refresh = true;
            break;
        case 9:
            a = "Snaggy seems to be having problems uploading the image to our servers.<br/><br/>Please try again.  If this keeps happening, try again later and we'll be sure to fix it.";
            refresh = true;
            break;
        default:
            a = "Something broke and it's not your fault.<br/><br/>try pasting again.  We're looking into this issue now.  Sorry.";
            refresh = true;
            break
    }
    messageBox(a, refresh, false, isPlus);
    $.post("assets/server-scripts/logError.php", {
        "error": b
    })
}
function compareVersions(e, d) {
    var c = e.split(".");
    var b = d.split(".");
    for (var a = 0; a < c.length; a++) {
        if (a < c.length) {
            if (b[a] < c[a]) {
                return -1
            } else {
                if (b[a] > c[a]) {
                    return 1
                }
            }
        }
    }
    return 0
}
function confirmJava(a) {
    var b = "";
    if ($.browser.chrome) {
        if (compareVersions("12", $.browser.version) < 0) {
            b = "It looks like you are using Chrome " + $.browser.version + ".  "
        }
        b += "Snaggy needs to use a <a href='http://www.java.com/en/download/' target='_blank' title='Download Java'>Java</a> applet to operate unless you are running Chrome version 12 or higher.  Consider <a href='http://www.google.com/support/chrome/bin/answer.py?answer=95414' target='_blank' title='Update Chrome'>updating Chrome</a>.<br/><br/><div class='center bold'>Is it okay to run the applet?</div><br/>(Don't forget to make sure your Java installation is <a href='http://www.java.com/en/download/' target='_blank' title='Update Java'>up to date</a>).";
        if (a) {
            b = "It looks like you are trying to paste a local file.  This action requires the use of a <a href='http://www.java.com/en/download/' target='_blank' title='Download Java'>Java</a> applet.  You can still paste other images, such as from a screen capture, without using an applet.<br/><br/><div class='center bold'>Is it okay to run the applet?</div><br/>(Don't forget to make sure your Java installation is <a href='http://www.java.com/en/download/' target='_blank' title='Update Java'>up to date</a>)."
        }
    } else {
        if ($.browser.mozilla) {
            if (compareVersions("4", $.browser.version) < 0) {
                b = "It looks like you are using Firefox " + $.browser.version + ".  "
            }
            b += "Snaggy needs to use a <a href='http://www.java.com/en/download/' target='_blank' title='Download Java'>Java</a> applet to operate unless you are running Firefox version 4 or higher.  Consider <a href='http://www.mozilla.com/en-US/firefox/update/' target='_blank' title='Update Firefox'>updating Firefox</a>.<br/><br/><div class='center bold'>Is it okay to run the applet?</div><br/>(Don't forget to make sure your Java installation is <a href='http://www.java.com/en/download/' target='_blank' title='Update Java'>up to date</a>)."
        } else {
            b = "If you aren't browsing with a recent version of <a href='http://www.google.com/chrome/' target='_blank' title='Get Chrome!'>Chrome</a> or <a href='http://www.mozilla.com/en-US/firefox/new/' target='_blank' title='Get Firefox!'>Firefox</a>, Snaggy needs to use a <a href='http://www.java.com/en/download/' target='_blank' title='Download Java'>Java</a> applet to read your clipboard.<br/><br/><div class='center bold'>Is it okay to run the applet?</div><br/>(Don't forget to make sure your Java installation is <a href='http://www.java.com/en/download/' target='_blank' title='Update Java'>up to date</a>)."
        }
    }
    messageBox(b, false, true)
}
function messageBox(d, c, b, a) {
    if (b && $.cookie("allow_java") == "true") {
        pasteImageWithJava();
        return
    }
    State.isPasting = true;
    var e = function () {
        $(".messageBg").remove();
        $("a").attr("tabindex", "0");
        $(document).unbind("keydown", "return");
        if (c) {
            location.reload(true)
        }
        State.isPasting = false
    };
    var f = b ?
	function () {
	    $(".messageBg").remove();
	    $("a").attr("tabindex", "0");
	    $(document).unbind("keydown", "return");
	    State.isPasting = false;
	    $.cookie("allow_java", "true", {
	        expires: 365
	    });
	    pasteImageWithJava()
	} : null;
    $("a").attr("tabindex", "-1");
    $("<div/>").addClass("messageBg").append($("<div/>").addClass("messageOverlay")).append($('<div class="vcOutside"/>').append($('<div class="vcInside"/>').append($('<div class="messageBox ' + (b ? "confirm" : "normal") + " " + (a ? "normalPlus" : "") + ' vcContent txt"/>').html(d).prepend($("<p/>").html("<h3>hey,</h3>")).append($('<div class="msgButton button center ' + (b ? "confirmYes" : "normalMsg") + '"/>').click(b ? f : e).append($('<div class="buttonContent"/>').html("&nbsp;&nbsp;" + (b ? "run the applet" : "okay") + "&nbsp;&nbsp;"))).append(b ? ($('<div class="msgButton button center confirmNo"/>').click(e).append($('<div class="buttonContent"/>').html("&nbsp;&nbsp;no&nbsp;&nbsp;"))) : "")))).appendTo("body");
    $(document).bind("keydown", "return", (b ? f : e))
}
function isUrl(a) {
    var b = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
    return b.test(a)
}
var pasterTool = {
    data: "",
    mode: 0,
    processAsHtml: function (b) {
        var a = b.clipboardData.getData("text/html");
        if (a) {
            $("#pasteCapture").html(a);
            if ($("#pasteCapture img").length) {
                pasterTool.data = $("#pasteCapture img:first").attr("src");
                pasterTool.mode = 2;
                pasterTool.process();
                return true
            } else {
                if ($("#pasteCapture a").length) {
                    pasterTool.data = $("#pasteCapture a:first").attr("href");
                    pasterTool.mode = 2;
                    pasterTool.process();
                    return true
                }
            }
        }
        return false
    },
    processAsText: function (b) {
        var a = b.clipboardData.getData("text/plain");
        if (a) {
            if (isUrl(a)) {
                pasterTool.data = a;
                pasterTool.mode = 2;
                pasterTool.process();
                return true
            }
        }
        return false
    },
    checkEditableArea: function () {
        $("#pasteCapture").html(""); (function () {
            var e = null;
            var c = 50;
            var d = 500;
            var b = function () {
                clearInterval(e);
                $("#pasteCapture").attr("contenteditable", false);
                $("#pasteCapture").blur();
                $("#pasteCapture").attr("contenteditable", true);
                pasteLoadingMode(false)
            };
            var a = function () {
                if ($("#pasteCapture").html().length > 0) {
                    b();
                    if ($("#pasteCapture img").length) {
                        var h = $("#pasteCapture img:first").attr("src");
                        var g = "data";
                        var f = "file";
                        var i = "webkit-fake-url";
                        if (h.substring(0, g.length) === g) {
                            pasterTool.data = h.substr(h.indexOf(",") + 1);
                            pasterTool.mode = 1;
                            pasterTool.process()
                        } else {
                            if (h.substring(0, f.length) === f) {
                                State.isPasting = false;
                                confirmJava()
                            } else {
                                if (h.substring(0, i.length) === i) {
                                    State.isPasting = false;
                                    confirmJava()
                                } else {
                                    pasterTool.data = h;
                                    pasterTool.mode = 2;
                                    pasterTool.process()
                                }
                            }
                        }
                    } else {
                        if (isUrl($("#pasteCapture").html())) {
                            pasterTool.data = $("#pasteCapture").html();
                            pasterTool.mode = 2;
                            pasterTool.process()
                        } else {
                            State.isPasting = false;
                            displayError(6)
                        }
                    }
                } else {
                    d -= c;
                    if (d <= 0) {
                        b();
                        State.isPasting = false;
                        confirmJava()
                    }
                }
            };
            pasteLoadingMode(true);
            e = setInterval(a, c)
        })()
    },
    paste: function (d) {
        pasteCount--;
        if (State.isPasting) {
            return
        }
        State.isPasting = true;
        d = d.originalEvent;
        if (d.clipboardData) {
            if (d.clipboardData.items) {
                if (d.clipboardData.items.length == 0) {
                    State.isPasting = false;
                    confirmJava(true);
                    return
                }
                for (var b = 0; b < d.clipboardData.items.length; b++) {
                    var c = d.clipboardData.items[b];
                    if (c.type == "image/png") {
                        pasterTool.mode = 1;
                        var a = new FileReader();
                        a.onloadend = function () {
                            pasterTool.data = this.result.substr(this.result.indexOf(",") + 1);
                            pasterTool.process()
                        };
                        a.readAsDataURL(c.getAsFile());
                        break
                    } else {
                        if (c.type == "text/html") {
                            if (pasterTool.processAsHtml(d)) {
                                State.isPasting = false;
                                break
                            } else {
                                State.isPasting = false;
                                displayError(6)
                            }
                        } else {
                            if (c.type == "text/plain") {
                                if (pasterTool.processAsText(d)) {
                                    State.isPasting = false;
                                    break
                                } else {
                                    State.isPasting = false;
                                    displayError(6)
                                }
                            } else {
                                State.isPasting = false;
                                displayError(6)
                            }
                        }
                    }
                }
            } else {
                if (!(pasterTool.processAsHtml(d) || pasterTool.processAsText(d))) {
                    pasterTool.checkEditableArea()
                }
            }
        } else {
            pasterTool.checkEditableArea()
        }
    },
    process: function () {
        pasteLoadingMode(true);
        State.isPasting = true;
        Timer.start(60000);
        var a = Config.Resources.makeImage; //+ "?mode=" + parseInt(pasterTool.mode, 10);
        $.post(a, pasterTool.data,
		function (b) {
		    State.isPasting = false;
		    onPasteComplete(b)
		})
    }
};
function focusPasteArea() {
    $("#pasteCapture").focus()
}
var pasteCount = 0;
function pasteCheck() {
    //pasteCount++;
    focusPasteArea();
    //setTimeout(function () {
    //    if (!State.isPasting && pasteCount > 0) {
    //        pasteCount = 0;
    //        confirmJava()
    //    }
    //},
	//100)
}
$(document).ready(function () {
    $("<div/>").attr({
        "id": "pasteCapture",
        "contenteditable": "true",
        "_moz_resizing": "false"
    }).css({
        "position": "absolute",
        "height": "1",
        "width": "0",
        "top": "-9999",
        "left": "-9999",
        "outline": "0",
        "overflow": "auto",
        "opacity": "0",
        "z-index": "-9999"
    }).prependTo("body");
    $("body").bind("paste", pasterTool.paste);
    focusPasteArea();
    $(document).bind("keydown", "ctrl+v", pasteCheck);
    //$(document).bind("keydown", "meta+v", pasteCheck);
    var a = function () {
        $(".nonMac").css("display", "none");
        $(".mac").css("display", "block");
        $("#keymap").attr("src", "assets/images/mackeymap.png");
        $("#printScnTxt").html("Capture your screen by pressing <em>Command+Shift+Control+3</em>.")
    };
    $(".nonMac > .osSwitcher").click(a);
    $(".mac > .osSwitcher").click(function () {
        $(".mac").css("display", "none");
        $(".nonMac").css("display", "block");
        $("#keymap").attr("src", "assets/images/keymap.png");
        $("#printScnTxt").html("Capture your screen with your keyboard's <em>Print Screen</em> key.")
    });
    if (Config.isMac) {
        a()
    }
    //$("#accordion").accordion({
    //    "collapsible": true,
    //    "active": false,
    //    "autoHeight": false,
    //    "icons": {
    //        "header": "expand-plus",
    //        "headerSelected": "expand-minus"
    //    }
    //});
    $("#infoArea h3, #accordion span, #infoArea").css("visibility", "visible").fadeTo(0, 0).fadeTo(1000, 1)
});
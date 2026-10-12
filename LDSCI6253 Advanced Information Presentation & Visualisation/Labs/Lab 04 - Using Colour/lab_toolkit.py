"""
lab_toolkit.py · Week 4 Colour Lab (LDSCI6253)
Provided helpers: data loaders, design checkers and displayed challenges.
You don't need to read or edit this file. Keep it in the same folder as the notebook.
"""
import re
import altair as alt
import numpy as np
import pandas as pd

# ---------------- Colour testing ----------------
_CVD = {
    "protanopia":   np.array([[0.152286, 1.052583, -0.204868],
                              [0.114503, 0.786281,  0.099216],
                              [-0.003882,-0.048116, 1.051998]]),
    "deuteranopia": np.array([[0.367322, 0.860646, -0.227968],
                              [0.280085, 0.672501,  0.047413],
                              [-0.011820, 0.042940, 0.968881]]),
    "tritanopia":   np.array([[1.255528, -0.076749,-0.178779],
                              [-0.078411, 0.930809, 0.147602],
                              [0.004733,  0.691367, 0.303900]]),
}

def _hex_to_rgb(h):
    h = h.lstrip("#")
    return np.array([int(h[i:i+2], 16) for i in (0, 2, 4)]) / 255.0

def _to_linear(c):
    return np.where(c <= 0.04045, c / 12.92, ((c + 0.055) / 1.055) ** 2.4)

def _to_srgb(c):
    c = np.clip(c, 0, 1)
    return np.where(c <= 0.0031308, 12.92 * c, 1.055 * c ** (1 / 2.4) - 0.055)

def _rgb_to_hex(c):
    return "#" + "".join(f"{int(round(v * 255)):02X}" for v in np.clip(c, 0, 1))

def _lab(hexcode):
    lin = _to_linear(_hex_to_rgb(hexcode))
    M = np.array([[0.4124, 0.3576, 0.1805],
                  [0.2126, 0.7152, 0.0722],
                  [0.0193, 0.1192, 0.9505]])
    xyz = M @ lin / np.array([0.95047, 1.0, 1.08883])
    f = np.where(xyz > 216 / 24389, np.cbrt(xyz), (24389 / 27 * xyz + 16) / 116)
    return np.array([116 * f[1] - 16, 500 * (f[0] - f[1]), 200 * (f[1] - f[2])])

def lightness(hexcode):
    """CIELAB lightness L* (0 = black, 100 = white)."""
    return round(float(_lab(hexcode)[0]), 1)

def to_greyscale(hexcode):
    """The grey with the same perceived lightness."""
    L = _lab(hexcode)[0]
    Y = ((L + 16) / 116) ** 3 if L > 8 else L / (24389 / 27)
    return _rgb_to_hex(_to_srgb(np.repeat(Y, 3)))

def simulate(hexcode, kind="deuteranopia"):
    """How the colour may look to someone with a colour-vision deficiency."""
    lin = _to_linear(_hex_to_rgb(hexcode))
    return _rgb_to_hex(_to_srgb(_CVD[kind] @ lin))

def _delta_e(h1, h2):
    return float(np.linalg.norm(_lab(h1) - _lab(h2)))

def check_palette(colours, names=None, threshold=10):
    """Run the greyscale and colour-blindness tests on a list of hex colours.
    Returns a table and prints any pairs that become hard to tell apart."""
    names = names or [f"c{i+1}" for i in range(len(colours))]
    views = {"normal": list(colours),
             "greyscale": [to_greyscale(c) for c in colours]}
    for k in _CVD:
        views[k] = [simulate(c, k) for c in colours]
    table = pd.DataFrame({"name": names, "hex": colours,
                          "L*": [lightness(c) for c in colours],
                          **{v: views[v] for v in views if v != "normal"}})
    problems = []
    for view, cols in views.items():
        for i in range(len(cols)):
            for j in range(i + 1, len(cols)):
                d = _delta_e(cols[i], cols[j])
                if d < threshold:
                    problems.append((view, names[i], names[j], round(d, 1)))
    if problems:
        print(f"⚠ {len(problems)} pair(s) below ΔE {threshold} (hard to tell apart):")
        for p in problems:
            print(f"   {p[0]:<13} {p[1]} vs {p[2]}  ΔE = {p[3]}")
    else:
        print(f"✓ Every pair stays distinguishable (ΔE ≥ {threshold}) in all views.")
    return table

def swatches(colours, names=None):
    """Altair chart showing the palette in normal, greyscale and CVD views."""
    import altair as alt
    names = names or [f"c{i+1}" for i in range(len(colours))]
    rows = []
    for view, fn in [("normal", lambda c: c), ("greyscale", to_greyscale),
                     ("deuteranopia", lambda c: simulate(c, "deuteranopia")),
                     ("protanopia", lambda c: simulate(c, "protanopia")),
                     ("tritanopia", lambda c: simulate(c, "tritanopia"))]:
        for n, c in zip(names, colours):
            rows.append({"view": view, "name": n, "shown": fn(c)})
    df = pd.DataFrame(rows)
    return alt.Chart(df).mark_rect(stroke="white", strokeWidth=2).encode(
        x=alt.X("name:N", sort=names, title=None, axis=alt.Axis(labelAngle=0)),
        y=alt.Y("view:N", sort=[r for r in ["normal", "greyscale", "deuteranopia",
                                             "protanopia", "tritanopia"]], title=None),
        color=alt.Color("shown:N", scale=None),
    ).properties(width=60 * len(colours), height=170)


# --- Warm-up design checker ---------------------------------------------
import math as _m
_SCENARIOS = {
    "D1": {"needs": "diverging",  "hot_end": True},
    "D2": {"needs": "sequential", "hot_end": True},
    "D3": {"needs": "categorical", "hot_end": False},
    "D4": {"needs": "highlight",  "hot_end": False},
}
_HINTS = {
    "diverging":   "Does this data have a middle that means something?",
    "sequential":  "Does this data simply run from low to high?",
    "categorical": "Is one of these values 'more' than another, or just different?",
    "highlight":   "Should every value compete for attention, or only one?",
}

def _chroma(h):
    L, a, b = _lab(h); return float(_m.hypot(a, b))

def _hue(h):
    L, a, b = _lab(h); return _m.degrees(_m.atan2(b, a)) % 360

def _hue_gap(h1, h2):
    d = abs(h1 - h2) % 360; return min(d, 360 - d)

def _behaves_like(cols):
    Ls = [lightness(c) for c in cols]
    Cs = [_chroma(c) for c in cols]
    vivid = [i for i, c in enumerate(Cs) if c > 25]
    if len(vivid) == 1 and all(Cs[i] < 12 for i in range(len(cols)) if i != vivid[0]):
        return "highlight"
    mid = len(cols) // 2
    if (len(cols) >= 3 and 0 < Ls.index(max(Ls)) < len(cols) - 1
            and Cs[0] > 20 and Cs[-1] > 20 and _hue_gap(_hue(cols[0]), _hue(cols[-1])) > 90):
        return "diverging"
    inc = all(x < y for x, y in zip(Ls, Ls[1:]))
    dec = all(x > y for x, y in zip(Ls, Ls[1:]))
    if (inc or dec) and abs(Ls[-1] - Ls[0]) >= 30:
        return "sequential"
    return "categorical"

def design(scenario, colours):
    """Check a palette you designed for a warm-up scenario (D1–D4).
    List colours in order: low → high for scales."""
    import re
    if scenario not in _SCENARIOS:
        print("Scenario must be one of", list(_SCENARIOS)); return
    bad = [c for c in colours if not re.fullmatch(r"#[0-9A-Fa-f]{6}", str(c))]
    if bad or len(colours) < 2:
        print("Use at least two 6-digit hex codes like '#1B4F8A'. Problem:", bad or colours); return
    need = _SCENARIOS[scenario]["needs"]
    kind = _behaves_like(colours)
    Ls = [lightness(c) for c in colours]
    print(f"Your palette behaves like a {kind.upper()} scale.   L* = {Ls}")
    print("✓ Fits this data." if kind == need else f"✗ Rethink the family. {_HINTS[need]}")

    notes = []
    if kind == "sequential" and len(colours) >= 3:
        if abs(Ls[-1] - Ls[0]) < 40:
            notes.append("Lightness range is narrow: low and high may look alike.")
    if kind == "diverging":
        m = int(max(range(len(Ls)), key=lambda i: Ls[i]))
        if Ls[m] < 85 or _chroma(colours[m]) > 15:
            notes.append("The middle isn't a light neutral, so 'no change' won't read as neutral.")
        if abs(Ls[0] - Ls[-1]) > 15:
            notes.append("The two ends differ a lot in lightness: one side will look stronger.")
        d = _delta_e(simulate(colours[0]), simulate(colours[-1]))
        h0, h1 = _hue(colours[0]), _hue(colours[-1])
        redgreen = any(10 <= a <= 60 and 110 <= b <= 170 for a, b in [(h0, h1), (h1, h0)])
        if d < 20 or redgreen:
            notes.append("Red against green: the two ends look alike to many colour-blind readers.")
    if kind in ("categorical", "highlight"):
        for view in ["greyscale", "deuteranopia", "protanopia"]:
            f = to_greyscale if view == "greyscale" else (lambda c, v=view: simulate(c, v))
            shown = [f(c) for c in colours]
            close = [(i, j) for i in range(len(shown)) for j in range(i + 1, len(shown))
                     if _delta_e(shown[i], shown[j]) < 10 and colours[i] != colours[j]]
            if close:
                notes.append(f"In {view}, {len(close)} pair(s) become hard to tell apart.")
        if len(set(colours)) > 12:
            notes.append("More than 12 distinct colours: group or grey some out.")
    if _SCENARIOS[scenario]["hot_end"] and kind in ("sequential", "diverging"):
        h = _hue(colours[-1])
        warm = h < 100 or h > 340
        notes.append("Meaning: your 'hot' end is a warm hue ✓" if warm else
                     "Meaning: your 'hot' end (last colour) isn't a warm hue. Does that fit temperature?")
    for n in notes:
        print("  •", n)
    return swatches(list(colours))


# ---------------- Data ----------------
SEATTLE_URL = "https://cdn.jsdelivr.net/npm/vega-datasets@2/data/seattle-weather.csv"

def load_seattle():
    """Daily Seattle weather, 2012–2015 (raw: one row per day)."""
    try:
        from vega_datasets import data
        df = data.seattle_weather()
    except Exception:
        df = pd.read_csv(SEATTLE_URL)
    df["date"] = pd.to_datetime(df["date"])
    return df

REGION_ORDER = ["Nordic", "Western", "Central & East", "Southern"]

# Approximate values for teaching, modelled on the lecture's Eurostat 2022 example.
# col/row only position the tiles in a 6 x 2 grid.
EUROSTAT = pd.DataFrame([
    ("FI", "Finland",     "Nordic",         21.3, 0, 0),
    ("SE", "Sweden",      "Nordic",         21.4, 1, 0),
    ("DK", "Denmark",     "Nordic",         21.7, 2, 0),
    ("NL", "Netherlands", "Western",        23.0, 3, 0),
    ("FR", "France",      "Western",        23.4, 4, 0),
    ("DE", "Germany",     "Western",        23.9, 5, 0),
    ("CZ", "Czechia",     "Central & East", 26.2, 0, 1),
    ("HU", "Hungary",     "Central & East", 27.0, 1, 1),
    ("PL", "Poland",      "Central & East", 28.7, 2, 1),
    ("IT", "Italy",       "Southern",       30.0, 3, 1),
    ("ES", "Spain",       "Southern",       30.3, 4, 1),
    ("GR", "Greece",      "Southern",       30.6, 5, 1),
], columns=["code", "country", "region", "age", "col", "row"])


# ---------------- Data abstraction check ----------------
_TYPE_WORDS = {"Q": "quantitative", "O": "ordinal", "N": "nominal (categorical)", "T": "temporal (ordered · time)"}
_KEYS = {
    "seattle":  {"date": "T", "precipitation": "Q", "temp_max": "Q",
                 "temp_min": "Q", "wind": "Q", "weather": "N"},
    "eurostat": {"code": "N", "country": "N", "region": "N", "age": "Q"},
}
_TYPE_HINTS = {
    ("weather", "O"): "Is sun 'less' than snow? An order you impose isn't in the data.",
    ("weather", "Q"): "Can you average 'rain' and 'sun'?",
    ("date", "O"):    "Close, but dates are ordered *time*: Altair has a letter for that.",
    ("date", "N"):    "Dates have an order. What kind of order?",
    ("region", "O"):  "Is 'Southern' more than 'Nordic'? Regions are labels.",
    ("age", "O"):     "Ages are numbers you can subtract and average.",
    ("age", "N"):     "Is 30.3 just a different label from 21.3, or a different amount?",
}

def check_abstraction(dataset, passport):
    """passport = {"item": "...", "attributes": {"column": "Q/O/N/T", ...}}"""
    if dataset not in _KEYS:
        print("dataset must be one of", list(_KEYS)); return
    key = _KEYS[dataset]
    attrs = {k: str(v).strip().upper()[:1] for k, v in passport.get("attributes", {}).items()}
    print(f"📇 {dataset.upper()} · one item = {passport.get('item') or '(not given)'}\n")
    right = 0
    for col, want in key.items():
        got = attrs.get(col)
        if got is None:
            print(f"  ✗ {col:<14} missing: every attribute needs a type")
        elif got == want:
            right += 1
            print(f"  ✓ {col:<14} {_TYPE_WORDS[got]}")
        else:
            hint = _TYPE_HINTS.get((col, got), "Rethink: what can you do with these values: label, order, or measure?")
            print(f"  ✗ {col:<14} {got}: {hint}")
    for extra in set(attrs) - set(key):
        if extra in ("col", "row"):
            print(f"  ⚠ {extra:<14} layout helper for the tile grid, not a data attribute")
        else:
            print(f"  ⚠ {extra:<14} isn't a column in this dataset. Are you mixing up datasets?")
    print(f"\nScore: {right}/{len(key)}")


# ---------------- Task card check ----------------
_ACTIONS = {"discover", "present", "enjoy", "annotate", "record", "derive", "lookup",
            "browse", "locate", "explore", "identify", "compare", "summarise", "summarize"}
_TARGETS = {"trends", "trend", "outliers", "outlier", "features", "distribution",
            "extremes", "correlation", "dependency", "similarity", "shape",
            "topology", "paths"}
_EXPECT_DATA = {"C1": "seattle", "C2": "seattle", "C3": "seattle",
                "C4": "seattle", "C5": "eurostat"}

def check_task(task):
    """task = {"challenge": "C1", "dataset": "...", "question": "...",
               "action": "...", "target": "...", "colour_job": "..."}"""
    ok = True
    ch = task.get("challenge", "?")
    print(f"🎯 {ch} · {task.get('question', '(no question)')}")
    ds = str(task.get("dataset", "")).lower()
    if _EXPECT_DATA.get(ch) and ds != _EXPECT_DATA[ch]:
        ok = False
        print(f"  ✗ dataset '{ds}': check which table this question is actually about")
    a = str(task.get("action", "")).lower().strip()
    t = str(task.get("target", "")).lower().strip()
    if a not in _ACTIONS:
        ok = False
        print(f"  ✗ action '{a}' isn't in Munzner's vocabulary (e.g. lookup, locate, identify, compare, summarise)")
    if t not in _TARGETS:
        ok = False
        print(f"  ✗ target '{t}' isn't in Munzner's vocabulary (e.g. trends, outliers, extremes, distribution)")
    job = str(task.get("colour_job", "")).lower()
    if job not in {"label", "measure", "highlight", "mean"}:
        ok = False
        print("  ✗ colour_job should be one of: label, measure, highlight, mean")
    if ok:
        print(f"  ✓ {{{a}, {t}}} · colour job: {job}. Now build a chart that makes this task easy.")


# ---------------- Chart design check ----------------
_SEQ = {"blues", "greens", "greys", "oranges", "purples", "reds", "viridis", "magma", "inferno",
        "plasma", "cividis", "turbo", "bluegreen", "bluepurple", "goldgreen", "goldorange",
        "goldred", "greenblue", "orangered", "purplebluegreen", "purpleblue", "purplered",
        "redpurple", "yellowgreenblue", "yellowgreen", "yelloworangebrown", "yelloworangered",
        "teals", "warmgreys", "lightorange", "lightgreyred", "browns", "tealblues"}
_COOL = {"blues", "greens", "purples", "teals", "bluegreen", "bluepurple", "greenblue",
         "purpleblue", "purplebluegreen", "yellowgreenblue", "tealblues"}
_DIV = {"blueorange", "brownbluegreen", "purplegreen", "pinkyellowgreen", "purpleorange",
        "redblue", "redgrey", "redyellowblue", "redyellowgreen", "spectral"}
_REDGREEN = {"redyellowgreen", "pinkyellowgreen", "purplegreen", "spectral"}
_CAT = {"category10", "category20", "category20b", "category20c", "tableau10", "tableau20",
        "accent", "dark2", "paired", "pastel1", "pastel2", "set1", "set2", "set3", "observable10"}
_RAINBOW = {"rainbow", "sinebow", "turbo"}

def _units(d, inherited=None):
    inherited = inherited or {}
    enc = {**inherited, **d.get("encoding", {})}
    if "mark" in d:
        m = d["mark"]
        yield (m if isinstance(m, str) else m.get("type")), enc, (m if isinstance(m, dict) else {}), d
    for key in ("layer", "hconcat", "vconcat", "concat"):
        for sub in d.get(key, []):
            yield from _units(sub, enc if key == "layer" else {})
    if "spec" in d:
        yield from _units(d["spec"], {})

def _fieldname(e):
    f = e.get("field", "")
    if isinstance(f, dict):
        f = f.get("repeat", "")
    return f

def _report(lines):
    for ok, msg in lines:
        print(("  ✓ " if ok is True else "  ⚠ " if ok is None else "  ✗ ") + msg)
    good = sum(1 for ok, _ in lines if ok is True)
    print(f"\n{good}/{len(lines)} design checks passed.")

def _colour_enc(units):
    return [(m, e["color"], mk, d) for m, e, mk, d in units if "color" in e]

def check_chart(chart, challenge):
    """Feedback on your design decisions. For C5 pass (label_chart, measure_chart)."""
    try:
        if challenge == "C5":
            label_chart, measure_chart = chart
            print("── Label chart ──"); _check_c5(label_chart.to_dict(), "label")
            print("\n── Measure chart ──"); _check_c5(measure_chart.to_dict(), "measure")
            return
        spec = chart.to_dict()
    except Exception as err:
        print("Couldn't read the chart. Does it render? Error:", err); return
    units = list(_units(spec))
    print(f"🔍 {challenge}: {len(units)} mark layer(s): {[u[0] for u in units]}\n")
    {"C1": _check_c1, "C2": _check_c2, "C3": _check_c3, "C4": _check_c4}.get(
        challenge, lambda u, s: print("challenge must be C1–C5"))(units, spec)

def _scheme(e):
    s = e.get("scale") or {}
    return str(s.get("scheme", "")).lower(), s

def _check_c1(units, spec):
    L = []
    rects = [u for u in units if u[0] == "rect"]
    L.append((bool(rects), "a grid of cells (rect)" if rects else "this structure is a grid of cells: which mark draws cells?"))
    ce = _colour_enc(units)
    if not ce:
        L.append((False, "no colour encoding yet")); return _report(L)
    e = ce[0][1]
    L.append((e.get("type") == "quantitative", "colour encodes a quantity" if e.get("type") == "quantitative"
              else f"colour type is {e.get('type')}: is temperature a label, or an amount?"))
    agg = e.get("aggregate")
    L.append((bool(agg), f"colour is aggregated ({agg}): one value per cell" if agg
              else "not aggregated: each cell holds ~30 days. Which single value should a cell show?"))
    sch, _ = _scheme(e)
    if sch in _RAINBOW or sch in _CAT:
        L.append((False, f"'{sch}': hue isn't ordered. Let lightness do the ordering"))
    elif sch in _COOL:
        L.append((None, f"'{sch}' measures correctly but fights the meaning of heat (Mean job)"))
    elif sch in _SEQ:
        L.append((True, f"'{sch}': lightness carries order"))
    elif sch in _DIV:
        L.append((False, f"'{sch}' is diverging: is there a meaningful middle in raw temperature?"))
    else:
        L.append((None, "default colours: you haven't made a colour decision yet"))
    _report(L)

def _check_c2(units, spec):
    L = []
    ce = _colour_enc(units)
    if not ce:
        L.append((False, "no colour encoding yet")); return _report(L)
    e = ce[0][1]
    f = _fieldname(e)
    raw = {"temp_max", "temp_min", "precipitation", "wind"}
    if f in raw:
        L.append((False, f"colour shows raw '{f}': seasonality will swamp the story. Derive 'difference from usual'"))
    else:
        L.append((True, f"colour shows a derived attribute ('{f}')"))
    L.append((e.get("type") == "quantitative", "quantitative colour" if e.get("type") == "quantitative"
              else f"type is {e.get('type')}: should be quantitative"))
    sch, sc = _scheme(e)
    if sch in _REDGREEN:
        L.append((False, f"'{sch}' sets red against green: fails for many colour-blind readers"))
    elif sch in _DIV:
        L.append((True, f"'{sch}' is a diverging scheme"))
        if sch == "redblue" and not sc.get("reverse"):
            L.append((None, "redblue puts red at the LOW end by default. Check warm months look warm"))
    else:
        L.append((False, f"'{sch or 'default'}' isn't diverging: this data runs two directions from a middle"))
    mid = sc.get("domainMid")
    L.append((mid == 0, "midpoint pinned at 0 (= usual)" if mid == 0
              else "midpoint not pinned at 0: the neutral colour may not mean 'usual'"))
    _report(L)

def _check_c3(units, spec):
    L = []
    ce = _colour_enc(units)
    if not ce:
        L.append((False, "no colour encoding yet")); return _report(L)
    e = ce[0][1]
    L.append((_fieldname(e) == "weather", "colour labels weather type" if _fieldname(e) == "weather"
              else f"colour encodes '{_fieldname(e)}': which attribute needs labelling?"))
    L.append((e.get("type") == "nominal", "nominal colour: different, not more" if e.get("type") == "nominal"
              else f"type is {e.get('type')}: does weather have an order?"))
    sch, sc = _scheme(e)
    rng = sc.get("range")
    if isinstance(rng, list) and all(isinstance(c, str) and re.fullmatch(r"#[0-9A-Fa-f]{6}", c) for c in rng):
        L.append((True, f"you designed your own palette ({len(rng)} colours). Testing it:"))
        _report(L); print()
        check_palette(rng, sc.get("domain"))
        return
    if sch in _SEQ or sch in _DIV:
        L.append((False, f"'{sch}' implies order. Is snow 'more' than sun?"))
    elif sch in _CAT:
        L.append((None, f"'{sch}' separates categories, but does it carry meaning (sun, rain, snow)?"))
    else:
        L.append((None, "default palette: design your own with scale(domain=..., range=...)"))
    _report(L)

def _check_c4(units, spec):
    L = []
    lines = [u for u in units if u[0] == "line"]
    L.append((bool(lines), f"{len(lines)} line layer(s)" if lines else "a change across months: which mark shows trends?"))
    ce = _colour_enc(units)
    year_coloured = [e for _, e, _, _ in ce if "year" in str(_fieldname(e)) or e.get("timeUnit") == "year"
                     or "date" == _fieldname(e)]
    if year_coloured and any(e.get("type") in ("nominal", "ordinal") for e in year_coloured):
        L.append((False, "every year has its own hue: they all compete, so nothing is highlighted"))
    colours = []
    for _, e, mk, _ in units:
        if mk.get("color"):
            colours.append(mk["color"])
        c = e.get("color", {})
        if isinstance(c, dict):
            if "condition" in c and isinstance(c["condition"], dict) and "value" in c["condition"]:
                colours.append(c["condition"]["value"])
            if "value" in c:
                colours.append(c["value"])
    hexes = [c for c in colours if isinstance(c, str) and c.startswith("#") and len(c) == 7]
    vivid = {c for c in hexes if _chroma(c) > 25}
    grey = {c for c in hexes if _chroma(c) < 12}
    L.append((len(vivid) == 1 and bool(grey), "one vivid colour against grey" if len(vivid) == 1 and grey
              else "aim for exactly one vivid colour, with the rest grey"))
    L.append((len(lines) >= 2 or any("condition" in str(e.get("color", "")) for _, e, _, _ in units),
              "highlight built by layering or a condition"))
    texts = [u for u in units if u[0] == "text"]
    L.append((bool(texts) or None, "direct labels: no legend needed" if texts
              else "readers need a legend. Could you label the lines directly?"))
    _report(L)

def _check_c5(spec, job):
    units = list(_units(spec))
    ce = _colour_enc(units)
    L = []
    if not ce:
        L.append((False, "no colour encoding")); return _report(L)
    e = ce[0][1]
    f, t = _fieldname(e), e.get("type")
    sch, sc = _scheme(e)
    if job == "label":
        L.append((f == "region", f"colour shows '{f}'" + ("" if f == "region" else ": the question is 'which region?'")))
        L.append((t == "nominal", "nominal: regions are different, not more" if t == "nominal"
                  else f"type {t}: is 'Southern' more than 'Nordic'?"))
        if sch in _SEQ:
            L.append((False, f"'{sch}' shades of one hue imply ranking (channels swapped)"))
        else:
            L.append((True, "distinct hues"))
    else:
        L.append((f == "age", f"colour shows '{f}'" + ("" if f == "age" else ": the question is 'how old?'")))
        L.append((t == "quantitative", "quantitative: older reads as darker" if t == "quantitative"
                  else f"type {t}: binning into hue bands loses the order"))
        if sch in _RAINBOW or sch in _CAT or (isinstance(sc.get("range"), list) and t != "quantitative"):
            L.append((False, "rainbow hues: is orange older than green?"))
        elif sch in _SEQ:
            L.append((True, f"'{sch}': lightness carries order"))
        else:
            L.append((None, "choose a sequential scheme so lightness does the work"))
    texts = [u for u in units if u[0] == "text"]
    L.append((bool(texts) or None, "countries labelled directly" if texts else "add the country codes as text"))
    _report(L)


# ---------------- Displayed challenges ----------------
def _monthly(df):
    m = (df.assign(year=df["date"].dt.year, month=df["date"].dt.strftime("%b"),
                   m=df["date"].dt.month)
           .groupby(["year", "month", "m"], as_index=False)["temp_max"].mean())
    m["anomaly"] = (m["temp_max"] - m.groupby("m")["temp_max"].transform("mean")).round(1)
    return m

_MONTHS = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]

def _tiles(df):
    return alt.Chart(df).encode(x=alt.X("col:O", axis=None), y=alt.Y("row:O", axis=None))

def show_target(name):
    """Displayed challenges: C1, C2, C5."""
    if name == "C1":
        df = load_seattle()
        return alt.Chart(df).mark_rect().encode(
            x=alt.X("month(date):O", title=None), y=alt.Y("year(date):O", title=None),
            color=alt.Color("mean(temp_max):Q", scale=alt.Scale(scheme="greys"), legend=None),
        ).properties(width=420, height=140,
                     title="TARGET C1 · rebuild this structure, then make the colour decision yourself")
    if name == "C2":
        m = _monthly(load_seattle())
        return alt.Chart(m).mark_text(fontSize=11).encode(
            x=alt.X("month:O", sort=_MONTHS, title=None), y=alt.Y("year:O", title=None),
            text=alt.Text("anomaly:Q", format="+.1f"),
        ).properties(width=480, height=140,
                     title="TARGET C2 · °C above/below each month's usual. Turn these numbers into colour")
    if name == "C5":
        bands = pd.cut(EUROSTAT["age"], [0, 23, 25, 27, 29, 31, 99],
                       labels=["under 23", "23–25", "25–27", "27–29", "29–31", "over 31"])
        df = EUROSTAT.assign(band=bands.astype(str))
        t = _tiles(df)
        lab = (t.mark_rect(stroke="white", strokeWidth=2).encode(
                   color=alt.Color("region:O", sort=REGION_ORDER, scale=alt.Scale(scheme="purples"), title="Region"))
               + t.mark_text(fontWeight="bold").encode(text="code:N")
               ).properties(width=330, height=110, title="✗ Which region?")
        mea = (t.mark_rect(stroke="white", strokeWidth=2).encode(
                   color=alt.Color("band:N", title="Age",
                                   sort=["under 23", "23–25", "25–27", "27–29", "29–31", "over 31"],
                                   scale=alt.Scale(domain=["under 23", "23–25", "25–27", "27–29", "29–31", "over 31"],
                                                   range=["#4C5FB0", "#92C35B", "#F4C542", "#F28E2B", "#D9483B", "#7B3294"])))
               + t.mark_text(color="white", fontWeight="bold").encode(text="code:N")
               ).properties(width=330, height=110, title="✗ How old on leaving home?")
        return alt.vconcat(lab, mea).properties(title="TARGET C5 · channels swapped: fix both")
    print("Targets: C1, C2, C5")

def show_bug(name):
    """Broken charts to diagnose and fix: B1, B2."""
    m = _monthly(load_seattle())
    if name == "B1":
        return alt.Chart(m).mark_rect().encode(
            x=alt.X("month:O", sort=_MONTHS, title=None), y=alt.Y("year:O", title=None),
            color=alt.Color("temp_max:Q", scale=alt.Scale(scheme="rainbow"), title="°C"),
        ).properties(width=420, height=140, title="BUG B1")
    if name == "B2":
        return alt.Chart(m).mark_rect().encode(
            x=alt.X("month:O", sort=_MONTHS, title=None), y=alt.Y("year:O", title=None),
            color=alt.Color("anomaly:Q", scale=alt.Scale(scheme="redyellowgreen"), title="°C vs usual"),
        ).properties(width=420, height=140, title="BUG B2")
    print("Bugs: B1, B2")


# ---------------- Checklist audit ----------------
_CHECKLIST = {
    1: "Attribute type matched to the colour map (Map)",
    2: "Colour is the right channel, not position (Use)",
    3: "Lightness does the ordering work (Vocabulary)",
    4: "At most 6–12 categories (Map)",
    5: "Passes greyscale and colour-blindness tests (Check)",
    6: "Information carried by more than colour (Check)",
    7: "Colours fit meaning, culture and background (Mean)",
    8: "Colour scarce enough to highlight (Notice)",
}

def check_audit(audit):
    """audit = {1: ("yes" / "no" / "n/a", "evidence"), ... 8: (...)}"""
    done = 0
    for i, q in _CHECKLIST.items():
        verdict, evidence = (audit.get(i) or ("", ""))[:2]
        verdict = str(verdict).lower()
        good = verdict in {"yes", "no", "n/a"} and len(str(evidence).strip()) >= 12
        done += good
        mark = "✓" if good else "✗"
        print(f"{mark} {i}. {q}\n     {verdict or '—'}: {evidence or 'needs evidence from your chart or code'}")
    print(f"\n{done}/8 answered with evidence.")

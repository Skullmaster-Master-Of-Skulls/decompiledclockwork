using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000049 RID: 73
	public class ColorSlice
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0001029C File Offset: 0x0000E49C
		private ColorSlice()
		{
			this._colorArray = new ColorSlice.ColorName[]
			{
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "aliceblue",
					Hex = "#f0f8ff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "antiquewhite",
					Hex = "#faebd7"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "aqua",
					Hex = "#0ff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "aquamarine",
					Hex = "#7fffd4"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "azure",
					Hex = "#f0ffff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "beige",
					Hex = "#f5f5dc"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "bisque",
					Hex = "#ffe4c4"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "black",
					Hex = "#000"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "blanchedalmond",
					Hex = "#ffebcd"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "blue",
					Hex = "#00f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "blueviolet",
					Hex = "#8a2be2"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "brown",
					Hex = "#a52a2a"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "burlywood",
					Hex = "#deb887"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "cadetblue",
					Hex = "#5f9ea0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "chartreuse",
					Hex = "#7fff00"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "chocolate",
					Hex = "#d2691e"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "coral",
					Hex = "#ff7f50"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "cornflowerblue",
					Hex = "#6495ed"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "cornsilk",
					Hex = "#fff8dc"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "crimson",
					Hex = "#dc143c"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "cyan",
					Hex = "#0ff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkblue",
					Hex = "#00008b"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkcyan",
					Hex = "#008b8b"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkgoldenrod",
					Hex = "#b8860b"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkgray",
					Hex = "#a9a9a9"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkgrey",
					Hex = "#a9a9a9"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkgreen",
					Hex = "#006400"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkkhaki",
					Hex = "#bdb76b"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkmagenta",
					Hex = "#8b008b"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkolivegreen",
					Hex = "#556b2f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkorange",
					Hex = "#ff8c00"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkorchid",
					Hex = "#9932cc"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkred",
					Hex = "#8b0000"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darksalmon",
					Hex = "#e9967a"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkseagreen",
					Hex = "#8fbc8f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkslateblue",
					Hex = "#483d8b"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkslategray",
					Hex = "#2f4f4f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkslategrey",
					Hex = "#2f4f4f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkturquoise",
					Hex = "#00ced1"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "darkviolet",
					Hex = "#9400d3"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "deeppink",
					Hex = "#ff1493"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "deepskyblue",
					Hex = "#00bfff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "dimgray",
					Hex = "#696969"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "dimgrey",
					Hex = "#696969"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "dodgerblue",
					Hex = "#1e90ff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "firebrick",
					Hex = "#b22222"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "floralwhite",
					Hex = "#fffaf0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "forestgreen",
					Hex = "#228b22"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "fuchsia",
					Hex = "#f0f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "gainsboro",
					Hex = "#dcdcdc"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "ghostwhite",
					Hex = "#f8f8ff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "gold",
					Hex = "#ffd700"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "goldenrod",
					Hex = "#daa520"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "gray",
					Hex = "#808080"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "grey",
					Hex = "#808080"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "green",
					Hex = "#008000"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "greenyellow",
					Hex = "#adff2f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "honeydew",
					Hex = "#f0fff0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "hotpink",
					Hex = "#ff69b4"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "indianred",
					Hex = "#cd5c5c"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "indigo",
					Hex = "#4b0082"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "ivory",
					Hex = "#fffff0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "khaki",
					Hex = "#f0e68c"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lavender",
					Hex = "#e6e6fa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lavenderblush",
					Hex = "#fff0f5"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lawngreen",
					Hex = "#7cfc00"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lemonchiffon",
					Hex = "#fffacd"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightblue",
					Hex = "#add8e6"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightcoral",
					Hex = "#f08080"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightcyan",
					Hex = "#e0ffff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightgoldenrodyellow",
					Hex = "#fafad2"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightgray",
					Hex = "#d3d3d3"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightgrey",
					Hex = "#d3d3d3"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightgreen",
					Hex = "#90ee90"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightpink",
					Hex = "#ffb6c1"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightsalmon",
					Hex = "#ffa07a"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightseagreen",
					Hex = "#20b2aa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightskyblue",
					Hex = "#87cefa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightslategray",
					Hex = "#778899"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightslategrey",
					Hex = "#778899"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightsteelblue",
					Hex = "#b0c4de"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "lightyellow",
					Hex = "#ffffe0"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "lime",
					Hex = "#0f0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "limegreen",
					Hex = "#32cd32"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "linen",
					Hex = "#faf0e6"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "magenta",
					Hex = "#f0f"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "maroon",
					Hex = "#800000"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumaquamarine",
					Hex = "#66cdaa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumblue",
					Hex = "#0000cd"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumorchid",
					Hex = "#ba55d3"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumpurple",
					Hex = "#9370d8"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumseagreen",
					Hex = "#3cb371"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumslateblue",
					Hex = "#7b68ee"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumspringgreen",
					Hex = "#00fa9a"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumturquoise",
					Hex = "#48d1cc"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mediumvioletred",
					Hex = "#c71585"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "midnightblue",
					Hex = "#191970"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mintcream",
					Hex = "#f5fffa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "mistyrose",
					Hex = "#ffe4e1"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "moccasin",
					Hex = "#ffe4b5"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "navajowhite",
					Hex = "#ffdead"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "navy",
					Hex = "#000080"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "oldlace",
					Hex = "#fdf5e6"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "olive",
					Hex = "#808000"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "olivedrab",
					Hex = "#6b8e23"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "orange",
					Hex = "#ffa500"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "orangered",
					Hex = "#ff4500"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "orchid",
					Hex = "#da70d6"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "palegoldenrod",
					Hex = "#eee8aa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "palegreen",
					Hex = "#98fb98"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "paleturquoise",
					Hex = "#afeeee"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "palevioletred",
					Hex = "#d87093"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "papayawhip",
					Hex = "#ffefd5"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "peachpuff",
					Hex = "#ffdab9"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "peru",
					Hex = "#cd853f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "pink",
					Hex = "#ffc0cb"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "plum",
					Hex = "#dda0dd"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "powderblue",
					Hex = "#b0e0e6"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "purple",
					Hex = "#800080"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "red",
					Hex = "#f00"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "rosybrown",
					Hex = "#bc8f8f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "royalblue",
					Hex = "#4169e1"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "saddlebrown",
					Hex = "#8b4513"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "salmon",
					Hex = "#fa8072"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "sandybrown",
					Hex = "#f4a460"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "seagreen",
					Hex = "#2e8b57"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "seashell",
					Hex = "#fff5ee"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "sienna",
					Hex = "#a0522d"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "silver",
					Hex = "#c0c0c0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "skyblue",
					Hex = "#87ceeb"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "slateblue",
					Hex = "#6a5acd"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "slategray",
					Hex = "#708090"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "slategrey",
					Hex = "#708090"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "snow",
					Hex = "#fffafa"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "springgreen",
					Hex = "#00ff7f"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "steelblue",
					Hex = "#4682b4"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "tan",
					Hex = "#d2b48c"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "teal",
					Hex = "#008080"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "thistle",
					Hex = "#d8bfd8"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "tomato",
					Hex = "#ff6347"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "turquoise",
					Hex = "#40e0d0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "violet",
					Hex = "#ee82ee"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "wheat",
					Hex = "#f5deb3"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "white",
					Hex = "#fff"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "whitesmoke",
					Hex = "#f5f5f5"
				},
				new ColorSlice.ColorName
				{
					Strict = true,
					Name = "yellow",
					Hex = "#ff0"
				},
				new ColorSlice.ColorName
				{
					Strict = false,
					Name = "yellowgreen",
					Hex = "#9acd32"
				}
			};
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00011D49 File Offset: 0x0000FF49
		public static Dictionary<string, string> NameShorterThanHex
		{
			get
			{
				return ColorSlice.NestedNameShorterThanHex.Data;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00011D50 File Offset: 0x0000FF50
		public static Dictionary<string, string> StrictNameShorterThanHex
		{
			get
			{
				return ColorSlice.NestedStrictNameShorterThanHex.Data;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00011D57 File Offset: 0x0000FF57
		public static Dictionary<string, string> HexShorterThanName
		{
			get
			{
				return ColorSlice.NestedHexShorterThanName.Data;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00011D5E File Offset: 0x0000FF5E
		public static Dictionary<string, string> StrictHexShorterThanNameAndAllNonStrict
		{
			get
			{
				return ColorSlice.NestedStrictHexShorterThanNameAndAllNonStrict.Data;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00011D65 File Offset: 0x0000FF65
		public static Dictionary<string, string> AllColorNames
		{
			get
			{
				return ColorSlice.NestedAllColorNames.Data;
			}
		}

		// Token: 0x0400012B RID: 299
		private ColorSlice.ColorName[] _colorArray;

		// Token: 0x0200004A RID: 74
		private class ColorName
		{
			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000487 RID: 1159 RVA: 0x00011D6C File Offset: 0x0000FF6C
			// (set) Token: 0x06000488 RID: 1160 RVA: 0x00011D74 File Offset: 0x0000FF74
			public bool Strict { get; set; }

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000489 RID: 1161 RVA: 0x00011D7D File Offset: 0x0000FF7D
			// (set) Token: 0x0600048A RID: 1162 RVA: 0x00011D85 File Offset: 0x0000FF85
			public string Name { get; set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600048B RID: 1163 RVA: 0x00011D8E File Offset: 0x0000FF8E
			// (set) Token: 0x0600048C RID: 1164 RVA: 0x00011D96 File Offset: 0x0000FF96
			public string Hex { get; set; }
		}

		// Token: 0x0200004B RID: 75
		private static class NestedFactory
		{
			// Token: 0x0400012F RID: 303
			public static readonly ColorSlice Instance = new ColorSlice();
		}

		// Token: 0x0200004C RID: 76
		private static class NestedNameShorterThanHex
		{
			// Token: 0x0600048F RID: 1167 RVA: 0x00011DE8 File Offset: 0x0000FFE8
			private static Dictionary<string, string> Create(ColorSlice singleton)
			{
				return (from colorName in singleton._colorArray
				where colorName.Hex.Length > colorName.Name.Length
				select colorName).DistinctBy((ColorSlice.ColorName c) => c.Hex).ToDictionary((ColorSlice.ColorName p) => p.Hex, (ColorSlice.ColorName p) => p.Name);
			}

			// Token: 0x04000130 RID: 304
			public static readonly Dictionary<string, string> Data = ColorSlice.NestedNameShorterThanHex.Create(ColorSlice.NestedFactory.Instance);
		}

		// Token: 0x0200004D RID: 77
		private static class NestedStrictNameShorterThanHex
		{
			// Token: 0x06000495 RID: 1173 RVA: 0x00011ECC File Offset: 0x000100CC
			private static Dictionary<string, string> Create(ColorSlice singleton)
			{
				return (from colorName in singleton._colorArray
				where colorName.Strict && colorName.Hex.Length > colorName.Name.Length
				select colorName).DistinctBy((ColorSlice.ColorName c) => c.Hex).ToDictionary((ColorSlice.ColorName p) => p.Hex, (ColorSlice.ColorName p) => p.Name);
			}

			// Token: 0x04000135 RID: 309
			public static readonly Dictionary<string, string> Data = ColorSlice.NestedStrictNameShorterThanHex.Create(ColorSlice.NestedFactory.Instance);
		}

		// Token: 0x0200004E RID: 78
		private static class NestedHexShorterThanName
		{
			// Token: 0x0600049B RID: 1179 RVA: 0x00011FA8 File Offset: 0x000101A8
			private static Dictionary<string, string> Create(ColorSlice singleton)
			{
				return (from colorName in singleton._colorArray
				where colorName.Name.Length > colorName.Hex.Length
				select colorName).DistinctBy((ColorSlice.ColorName c) => c.Name).ToDictionary((ColorSlice.ColorName p) => p.Name, (ColorSlice.ColorName p) => p.Hex);
			}

			// Token: 0x0400013A RID: 314
			public static readonly Dictionary<string, string> Data = ColorSlice.NestedHexShorterThanName.Create(ColorSlice.NestedFactory.Instance);
		}

		// Token: 0x0200004F RID: 79
		private static class NestedStrictHexShorterThanNameAndAllNonStrict
		{
			// Token: 0x060004A1 RID: 1185 RVA: 0x00012094 File Offset: 0x00010294
			private static Dictionary<string, string> Create(ColorSlice singleton)
			{
				return (from colorName in singleton._colorArray
				where (colorName.Strict && colorName.Name.Length > colorName.Hex.Length) || !colorName.Strict
				select colorName).DistinctBy((ColorSlice.ColorName c) => c.Name).ToDictionary((ColorSlice.ColorName p) => p.Name, (ColorSlice.ColorName p) => p.Hex);
			}

			// Token: 0x0400013F RID: 319
			public static readonly Dictionary<string, string> Data = ColorSlice.NestedStrictHexShorterThanNameAndAllNonStrict.Create(ColorSlice.NestedFactory.Instance);
		}

		// Token: 0x02000050 RID: 80
		private static class NestedAllColorNames
		{
			// Token: 0x060004A7 RID: 1191 RVA: 0x00012158 File Offset: 0x00010358
			private static Dictionary<string, string> Create(ColorSlice singleton)
			{
				return (from colorName in singleton._colorArray
				select colorName).DistinctBy((ColorSlice.ColorName c) => c.Name).ToDictionary((ColorSlice.ColorName p) => p.Name, (ColorSlice.ColorName p) => p.Hex);
			}

			// Token: 0x04000144 RID: 324
			public static readonly Dictionary<string, string> Data = ColorSlice.NestedAllColorNames.Create(ColorSlice.NestedFactory.Instance);
		}
	}
}

using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001450 RID: 5200
	internal class GenericColor : ColorTypeProperty.Maker
	{
		// Token: 0x0600D3D3 RID: 54227 RVA: 0x002EFC20 File Offset: 0x002EDE20
		protected GenericColor(string name) : base(name)
		{
		}

		// Token: 0x0600D3D4 RID: 54228 RVA: 0x002EFC2C File Offset: 0x002EDE2C
		static GenericColor()
		{
			GenericColor.s_htKeywords.Add("aliceblue", "#f0f8ff");
			GenericColor.s_htKeywords.Add("antiquewhite", "#faebd7");
			GenericColor.s_htKeywords.Add("aqua", "#00ffff");
			GenericColor.s_htKeywords.Add("aquamarine", "#7fffd4");
			GenericColor.s_htKeywords.Add("azure", "#f0ffff");
			GenericColor.s_htKeywords.Add("beige", "#f5f5dc");
			GenericColor.s_htKeywords.Add("bisque", "#ffe4c4");
			GenericColor.s_htKeywords.Add("black", "#000000");
			GenericColor.s_htKeywords.Add("blanchedalmond", "#ffebcd");
			GenericColor.s_htKeywords.Add("blue", "#0000ff");
			GenericColor.s_htKeywords.Add("blueviolet", "#8a2be2");
			GenericColor.s_htKeywords.Add("brown", "#a52a2a");
			GenericColor.s_htKeywords.Add("burlywood", "#deb887");
			GenericColor.s_htKeywords.Add("cadetblue", "#5f9ea0");
			GenericColor.s_htKeywords.Add("chartreuse", "#7fff00");
			GenericColor.s_htKeywords.Add("chocolate", "#d2691e");
			GenericColor.s_htKeywords.Add("coral", "#ff7f50");
			GenericColor.s_htKeywords.Add("cornflowerblue", "#6495ed");
			GenericColor.s_htKeywords.Add("cornsilk", "#fff8dc");
			GenericColor.s_htKeywords.Add("crimson", "#dc143c");
			GenericColor.s_htKeywords.Add("cyan", "#00ffff");
			GenericColor.s_htKeywords.Add("darkblue", "#00008b");
			GenericColor.s_htKeywords.Add("darkcyan", "#008b8b");
			GenericColor.s_htKeywords.Add("darkgoldenrod", "#b8860b");
			GenericColor.s_htKeywords.Add("darkgray", "#a9a9a9");
			GenericColor.s_htKeywords.Add("darkgreen", "#006400");
			GenericColor.s_htKeywords.Add("darkgrey", "#a9a9a9");
			GenericColor.s_htKeywords.Add("darkkhaki", "#bdb76b");
			GenericColor.s_htKeywords.Add("darkmagenta", "#8b008b");
			GenericColor.s_htKeywords.Add("darkolivegreen", "#556b2f");
			GenericColor.s_htKeywords.Add("darkorange", "#ff8c00");
			GenericColor.s_htKeywords.Add("darkorchid", "#9932cc");
			GenericColor.s_htKeywords.Add("darkred", "#8b0000");
			GenericColor.s_htKeywords.Add("darksalmon", "#e9967a");
			GenericColor.s_htKeywords.Add("darkseagreen", "#8fbc8f");
			GenericColor.s_htKeywords.Add("darkslateblue", "#483d8b");
			GenericColor.s_htKeywords.Add("darkslategray", "#2f4f4f");
			GenericColor.s_htKeywords.Add("darkslategrey", "#2f4f4f");
			GenericColor.s_htKeywords.Add("darkturquoise", "#00ced1");
			GenericColor.s_htKeywords.Add("darkviolet", "#9400d3");
			GenericColor.s_htKeywords.Add("deeppink", "#ff1493");
			GenericColor.s_htKeywords.Add("deepskyblue", "#00bfff");
			GenericColor.s_htKeywords.Add("dimgray", "#696969");
			GenericColor.s_htKeywords.Add("dimgrey", "#696969");
			GenericColor.s_htKeywords.Add("dodgerblue", "#1e90ff");
			GenericColor.s_htKeywords.Add("firebrick", "#b22222");
			GenericColor.s_htKeywords.Add("floralwhite", "#fffaf0");
			GenericColor.s_htKeywords.Add("forestgreen", "#228b22");
			GenericColor.s_htKeywords.Add("fuchsia", "#ff00ff");
			GenericColor.s_htKeywords.Add("gainsboro", "#dcdcdc");
			GenericColor.s_htKeywords.Add("lightpink", "#ffb6c1");
			GenericColor.s_htKeywords.Add("lightsalmon", "#ffa07a");
			GenericColor.s_htKeywords.Add("lightseagreen", "#20b2aa");
			GenericColor.s_htKeywords.Add("lightskyblue", "#87cefa");
			GenericColor.s_htKeywords.Add("lightslategray", "#778899");
			GenericColor.s_htKeywords.Add("lightslategrey", "#778899");
			GenericColor.s_htKeywords.Add("lightsteelblue", "#b0c4de");
			GenericColor.s_htKeywords.Add("lightyellow", "#ffffe0");
			GenericColor.s_htKeywords.Add("lime", "#00ff00");
			GenericColor.s_htKeywords.Add("limegreen", "#32cd32");
			GenericColor.s_htKeywords.Add("linen", "#faf0e6");
			GenericColor.s_htKeywords.Add("magenta", "#ff00ff");
			GenericColor.s_htKeywords.Add("maroon", "#800000");
			GenericColor.s_htKeywords.Add("mediumaquamarine", "#66cdaa");
			GenericColor.s_htKeywords.Add("mediumblue", "#0000cd");
			GenericColor.s_htKeywords.Add("mediumorchid", "#ba55d3");
			GenericColor.s_htKeywords.Add("mediumpurple", "#9370db");
			GenericColor.s_htKeywords.Add("mediumseagreen", "#3cb371");
			GenericColor.s_htKeywords.Add("mediumslateblue", "#7b68ee");
			GenericColor.s_htKeywords.Add("mediumspringgreen", "#00fa9a");
			GenericColor.s_htKeywords.Add("mediumturquoise", "#48d1cc");
			GenericColor.s_htKeywords.Add("mediumvioletred", "#c71585");
			GenericColor.s_htKeywords.Add("midnightblue", "#191970");
			GenericColor.s_htKeywords.Add("mintcream", "#f5fffa");
			GenericColor.s_htKeywords.Add("mistyrose", "#ffe4e1");
			GenericColor.s_htKeywords.Add("moccasin", "#ffe4b5");
			GenericColor.s_htKeywords.Add("navajowhite", "#ffdead");
			GenericColor.s_htKeywords.Add("navy", "#000080");
			GenericColor.s_htKeywords.Add("oldlace", "#fdf5e6");
			GenericColor.s_htKeywords.Add("olive", "#808000");
			GenericColor.s_htKeywords.Add("olivedrab", "#6b8e23");
			GenericColor.s_htKeywords.Add("orange", "#ffa500");
			GenericColor.s_htKeywords.Add("orangered", "#ff4500");
			GenericColor.s_htKeywords.Add("orchid", "#da70d6");
			GenericColor.s_htKeywords.Add("palegoldenrod", "#eee8aa");
			GenericColor.s_htKeywords.Add("palegreen", "#98fb98");
			GenericColor.s_htKeywords.Add("paleturquoise", "#afeeee");
			GenericColor.s_htKeywords.Add("palevioletred", "#db7093");
			GenericColor.s_htKeywords.Add("papayawhip", "#ffefd5");
			GenericColor.s_htKeywords.Add("peachpuff", "#ffdab9");
			GenericColor.s_htKeywords.Add("peru", "#cd853f");
			GenericColor.s_htKeywords.Add("pink", "#ffc0cb");
			GenericColor.s_htKeywords.Add("plum", "#dda0dd");
			GenericColor.s_htKeywords.Add("powderblue", "#b0e0e6");
			GenericColor.s_htKeywords.Add("purple", "#800080");
			GenericColor.s_htKeywords.Add("red", "#ff0000");
			GenericColor.s_htKeywords.Add("rosybrown", "#bc8f8f");
			GenericColor.s_htKeywords.Add("royalblue", "#4169e1");
			GenericColor.s_htKeywords.Add("saddlebrown", "#8b4513");
			GenericColor.s_htKeywords.Add("salmon", "#fa8072");
			GenericColor.s_htKeywords.Add("ghostwhite", "#f8f8ff");
			GenericColor.s_htKeywords.Add("gold", "#ffd700");
			GenericColor.s_htKeywords.Add("goldenrod", "#daa520");
			GenericColor.s_htKeywords.Add("gray", "#808080");
			GenericColor.s_htKeywords.Add("grey", "#808080");
			GenericColor.s_htKeywords.Add("green", "#008000");
			GenericColor.s_htKeywords.Add("greenyellow", "#adff2f");
			GenericColor.s_htKeywords.Add("honeydew", "#f0fff0");
			GenericColor.s_htKeywords.Add("hotpink", "#ff69b4");
			GenericColor.s_htKeywords.Add("indianred", "#cd5c5c");
			GenericColor.s_htKeywords.Add("indigo", "#4b0082");
			GenericColor.s_htKeywords.Add("ivory", "#fffff0");
			GenericColor.s_htKeywords.Add("khaki", "#f0e68c");
			GenericColor.s_htKeywords.Add("lavender", "#e6e6fa");
			GenericColor.s_htKeywords.Add("lavenderblush", "#fff0f5");
			GenericColor.s_htKeywords.Add("lawngreen", "#7cfc00");
			GenericColor.s_htKeywords.Add("lemonchiffon", "#fffacd");
			GenericColor.s_htKeywords.Add("lightblue", "#add8e6");
			GenericColor.s_htKeywords.Add("lightcoral", "#f08080");
			GenericColor.s_htKeywords.Add("lightcyan", "#e0ffff");
			GenericColor.s_htKeywords.Add("lightgoldenrodyellow", "#fafad2");
			GenericColor.s_htKeywords.Add("lightgray", "#d3d3d3");
			GenericColor.s_htKeywords.Add("lightgreen", "#90ee90");
			GenericColor.s_htKeywords.Add("lightgrey", "#d3d3d3");
			GenericColor.s_htKeywords.Add("sandybrown", "#f4a460");
			GenericColor.s_htKeywords.Add("seagreen", "#2e8b57");
			GenericColor.s_htKeywords.Add("seashell", "#fff5ee");
			GenericColor.s_htKeywords.Add("sienna", "#a0522d");
			GenericColor.s_htKeywords.Add("silver", "#c0c0c0");
			GenericColor.s_htKeywords.Add("skyblue", "#87ceeb");
			GenericColor.s_htKeywords.Add("slateblue", "#6a5acd");
			GenericColor.s_htKeywords.Add("slategray", "#708090");
			GenericColor.s_htKeywords.Add("slategrey", "#708090");
			GenericColor.s_htKeywords.Add("snow", "#fffafa");
			GenericColor.s_htKeywords.Add("springgreen", "#00ff7f");
			GenericColor.s_htKeywords.Add("steelblue", "#4682b4");
			GenericColor.s_htKeywords.Add("tan", "#d2b48c");
			GenericColor.s_htKeywords.Add("teal", "#008080");
			GenericColor.s_htKeywords.Add("thistle", "#d8bfd8");
			GenericColor.s_htKeywords.Add("tomato", "#ff6347");
			GenericColor.s_htKeywords.Add("turquoise", "#40e0d0");
			GenericColor.s_htKeywords.Add("violet", "#ee82ee");
			GenericColor.s_htKeywords.Add("wheat", "#f5deb3");
			GenericColor.s_htKeywords.Add("white", "#ffffff");
			GenericColor.s_htKeywords.Add("whitesmoke", "#f5f5f5");
			GenericColor.s_htKeywords.Add("yellow", "#ffff00");
			GenericColor.s_htKeywords.Add("yellowgreen", "#9acd32");
		}

		// Token: 0x0600D3D5 RID: 54229 RVA: 0x002F07C4 File Offset: 0x002EE9C4
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericColor(propName);
		}

		// Token: 0x0600D3D6 RID: 54230 RVA: 0x002F07CC File Offset: 0x002EE9CC
		protected override string CheckValueKeywords(string keyword)
		{
			string text = (string)GenericColor.s_htKeywords[keyword];
			if (text == null)
			{
				return base.CheckValueKeywords(keyword);
			}
			return text;
		}

		// Token: 0x04003985 RID: 14725
		private static Hashtable s_htKeywords = new Hashtable(147);
	}
}

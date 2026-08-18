using System;
using System.Drawing;
using System.Globalization;
using Telerik.Web.UI;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200137B RID: 4987
	internal class ColorType : ICloneable
	{
		// Token: 0x0600D015 RID: 53269 RVA: 0x002E15C5 File Offset: 0x002DF7C5
		public ColorType(float red, float green, float blue)
		{
			this._red = red;
			this._green = green;
			this._blue = blue;
		}

		// Token: 0x0600D016 RID: 53270 RVA: 0x002E15E4 File Offset: 0x002DF7E4
		public ColorType(string value)
		{
			string text = value.ToLower();
			if (text.StartsWith("#"))
			{
				try
				{
					if (text.Length == 4)
					{
						this._red = (float)int.Parse(text.Substring(1, 1), NumberStyles.HexNumber) / 15f;
						this._green = (float)int.Parse(text.Substring(2, 1), NumberStyles.HexNumber) / 15f;
						this._blue = (float)int.Parse(text.Substring(3, 1), NumberStyles.HexNumber) / 15f;
					}
					else if (text.Length == 7)
					{
						this._red = (float)int.Parse(text.Substring(1, 2), NumberStyles.HexNumber) / 255f;
						this._green = (float)int.Parse(text.Substring(3, 2), NumberStyles.HexNumber) / 255f;
						this._blue = (float)int.Parse(text.Substring(5, 2), NumberStyles.HexNumber) / 255f;
					}
					else
					{
						this._red = 0f;
						this._green = 0f;
						this._blue = 0f;
						ApocDriver.ActiveDriver.FireApocError("Unknown colour format. Must be #RGB or #RRGGBB");
					}
					return;
				}
				catch (Exception)
				{
					this._red = 0f;
					this._green = 0f;
					this._blue = 0f;
					ApocDriver.ActiveDriver.FireApocError("Unknown colour format. Must be #RGB or #RRGGBB");
					return;
				}
			}
			if (text.StartsWith("rgb("))
			{
				int num = text.IndexOf("(");
				int num2 = text.IndexOf(")");
				if (num == -1 || num2 == -1)
				{
					return;
				}
				text = text.Substring(num + 1, num2);
				GridStringTokenizer gridStringTokenizer = new GridStringTokenizer(text, ",");
				try
				{
					if (gridStringTokenizer.HasMoreTokens())
					{
						string text2 = gridStringTokenizer.NextToken().Trim();
						if (text2.EndsWith("%"))
						{
							this.Red = (float)int.Parse(text2.Substring(0, text2.Length - 1)) * 2.55f;
						}
						else
						{
							this.Red = (float)int.Parse(text2) / 255f;
						}
					}
					if (gridStringTokenizer.HasMoreTokens())
					{
						string text3 = gridStringTokenizer.NextToken().Trim();
						if (text3.EndsWith("%"))
						{
							this.Green = (float)int.Parse(text3.Substring(0, text3.Length - 1)) * 2.55f;
						}
						else
						{
							this.Green = (float)int.Parse(text3) / 255f;
						}
					}
					if (gridStringTokenizer.HasMoreTokens())
					{
						string text4 = gridStringTokenizer.NextToken().Trim();
						if (text4.EndsWith("%"))
						{
							this.Blue = (float)int.Parse(text4.Substring(0, text4.Length - 1)) * 2.55f;
						}
						else
						{
							this.Blue = (float)int.Parse(text4) / 255f;
						}
					}
					return;
				}
				catch
				{
					this.Red = 0f;
					this.Green = 0f;
					this.Blue = 0f;
					ApocDriver.ActiveDriver.FireApocError("Unknown colour format. Must be #RGB or #RRGGBB");
					return;
				}
			}
			if (text.StartsWith("url("))
			{
				ApocDriver.ActiveDriver.FireApocError("unsupported color format");
				return;
			}
			if (text.Equals("transparent"))
			{
				this.Red = 0f;
				this.Green = 0f;
				this.Blue = 0f;
				this.Alpha = 1f;
				return;
			}
			Color color = Color.FromName(text);
			if (color.ToArgb() != 0)
			{
				this.Red = (float)color.R / 255f;
				this.Green = (float)color.G / 255f;
				this.Blue = (float)color.B / 255f;
				return;
			}
			bool flag = false;
			for (int i = 0; i < ColorType.names.Length; i++)
			{
				if (text.Equals(ColorType.names[i]))
				{
					this.Red = (float)ColorType.vals[i, 0] / 255f;
					this.Green = (float)ColorType.vals[i, 1] / 255f;
					this.Blue = (float)ColorType.vals[i, 2] / 255f;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.Red = 0f;
				this.Green = 0f;
				this.Blue = 0f;
				ApocDriver.ActiveDriver.FireApocWarning("Unknown colour name: " + text + ".  Defaulting to black.");
			}
		}

		// Token: 0x170042D1 RID: 17105
		// (get) Token: 0x0600D017 RID: 53271 RVA: 0x002E1A7C File Offset: 0x002DFC7C
		// (set) Token: 0x0600D018 RID: 53272 RVA: 0x002E1A84 File Offset: 0x002DFC84
		public float Blue
		{
			get
			{
				return this._blue;
			}
			set
			{
				this._blue = value;
			}
		}

		// Token: 0x170042D2 RID: 17106
		// (get) Token: 0x0600D019 RID: 53273 RVA: 0x002E1A8D File Offset: 0x002DFC8D
		// (set) Token: 0x0600D01A RID: 53274 RVA: 0x002E1A95 File Offset: 0x002DFC95
		public float Green
		{
			get
			{
				return this._green;
			}
			set
			{
				this._green = value;
			}
		}

		// Token: 0x170042D3 RID: 17107
		// (get) Token: 0x0600D01B RID: 53275 RVA: 0x002E1A9E File Offset: 0x002DFC9E
		// (set) Token: 0x0600D01C RID: 53276 RVA: 0x002E1AA6 File Offset: 0x002DFCA6
		public float Red
		{
			get
			{
				return this._red;
			}
			set
			{
				this._red = value;
			}
		}

		// Token: 0x170042D4 RID: 17108
		// (get) Token: 0x0600D01D RID: 53277 RVA: 0x002E1AAF File Offset: 0x002DFCAF
		// (set) Token: 0x0600D01E RID: 53278 RVA: 0x002E1AB7 File Offset: 0x002DFCB7
		public float Alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				this._alpha = value;
			}
		}

		// Token: 0x0600D01F RID: 53279 RVA: 0x002E1AC0 File Offset: 0x002DFCC0
		public object Clone()
		{
			return new ColorType(this.Red, this.Green, this.Blue);
		}

		// Token: 0x040037C9 RID: 14281
		protected float _red;

		// Token: 0x040037CA RID: 14282
		protected float _green;

		// Token: 0x040037CB RID: 14283
		protected float _blue;

		// Token: 0x040037CC RID: 14284
		protected float _alpha;

		// Token: 0x040037CD RID: 14285
		private static readonly string[] names = new string[]
		{
			"aliceblue",
			"antiquewhite",
			"aqua",
			"aquamarine",
			"azure",
			"beige",
			"bisque",
			"black",
			"blanchedalmond",
			"blue",
			"blueviolet",
			"brown",
			"burlywood",
			"cadetblue",
			"chartreuse",
			"chocolate",
			"coral",
			"cornflowerblue",
			"cornsilk",
			"crimson",
			"cyan",
			"darkblue",
			"darkcyan",
			"darkgoldenrod",
			"darkgray",
			"darkgreen",
			"darkgrey",
			"darkkhaki",
			"darkmagenta",
			"darkolivegreen",
			"darkorange",
			"darkorchid",
			"darkred",
			"darksalmon",
			"darkseagreen",
			"darkslateblue",
			"darkslategray",
			"darkslategrey",
			"darkturquoise",
			"darkviolet",
			"deeppink",
			"deepskyblue",
			"dimgray",
			"dimgrey",
			"dodgerblue",
			"firebrick",
			"floralwhite",
			"forestgreen",
			"fuchsia",
			"gainsboro",
			"lightpink",
			"lightsalmon",
			"lightseagreen",
			"lightskyblue",
			"lightslategray",
			"lightslategrey",
			"lightsteelblue",
			"lightyellow",
			"lime",
			"limegreen",
			"linen",
			"magenta",
			"maroon",
			"mediumaquamarine",
			"mediumblue",
			"mediumorchid",
			"mediumpurple",
			"mediumseagreen",
			"mediumslateblue",
			"mediumspringgreen",
			"mediumturquoise",
			"mediumvioletred",
			"midnightblue",
			"mintcream",
			"mistyrose",
			"moccasin",
			"navajowhite",
			"navy",
			"oldlace",
			"olive",
			"olivedrab",
			"orange",
			"orangered",
			"orchid",
			"palegoldenrod",
			"palegreen",
			"paleturquoise",
			"palevioletred",
			"papayawhip",
			"peachpuff",
			"peru",
			"pink",
			"plum",
			"powderblue",
			"purple",
			"red",
			"rosybrown",
			"royalblue",
			"saddlebrown",
			"salmon",
			"ghostwhite",
			"gold",
			"goldenrod",
			"gray",
			"grey",
			"green",
			"greenyellow",
			"honeydew",
			"hotpink",
			"indianred",
			"indigo",
			"ivory",
			"khaki",
			"lavender",
			"lavenderblush",
			"lawngreen",
			"lemonchiffon",
			"lightblue",
			"lightcoral",
			"lightcyan",
			"lightgoldenrodyellow",
			"lightgray",
			"lightgreen",
			"lightgrey",
			"sandybrown",
			"seagreen",
			"seashell",
			"sienna",
			"silver",
			"skyblue",
			"slateblue",
			"slategray",
			"slategrey",
			"snow",
			"springgreen",
			"steelblue",
			"tan",
			"teal",
			"thistle",
			"tomato",
			"turquoise",
			"violet",
			"wheat",
			"white",
			"whitesmoke",
			"yellow",
			"yellowgreen"
		};

		// Token: 0x040037CE RID: 14286
		private static readonly int[,] vals = new int[,]
		{
			{
				240,
				248,
				255
			},
			{
				250,
				235,
				215
			},
			{
				0,
				255,
				255
			},
			{
				127,
				255,
				212
			},
			{
				240,
				255,
				255
			},
			{
				245,
				245,
				220
			},
			{
				255,
				228,
				196
			},
			{
				0,
				0,
				0
			},
			{
				255,
				235,
				205
			},
			{
				0,
				0,
				255
			},
			{
				138,
				43,
				226
			},
			{
				165,
				42,
				42
			},
			{
				222,
				184,
				135
			},
			{
				95,
				158,
				160
			},
			{
				127,
				255,
				0
			},
			{
				210,
				105,
				30
			},
			{
				255,
				127,
				80
			},
			{
				100,
				149,
				237
			},
			{
				255,
				248,
				220
			},
			{
				220,
				20,
				60
			},
			{
				0,
				255,
				255
			},
			{
				0,
				0,
				139
			},
			{
				0,
				139,
				139
			},
			{
				184,
				134,
				11
			},
			{
				169,
				169,
				169
			},
			{
				0,
				100,
				0
			},
			{
				169,
				169,
				169
			},
			{
				189,
				183,
				107
			},
			{
				139,
				0,
				139
			},
			{
				85,
				107,
				47
			},
			{
				255,
				140,
				0
			},
			{
				153,
				50,
				204
			},
			{
				139,
				0,
				0
			},
			{
				233,
				150,
				122
			},
			{
				143,
				188,
				143
			},
			{
				72,
				61,
				139
			},
			{
				47,
				79,
				79
			},
			{
				47,
				79,
				79
			},
			{
				0,
				206,
				209
			},
			{
				148,
				0,
				211
			},
			{
				255,
				20,
				147
			},
			{
				0,
				191,
				255
			},
			{
				105,
				105,
				105
			},
			{
				105,
				105,
				105
			},
			{
				30,
				144,
				255
			},
			{
				178,
				34,
				34
			},
			{
				255,
				250,
				240
			},
			{
				34,
				139,
				34
			},
			{
				255,
				0,
				255
			},
			{
				220,
				220,
				220
			},
			{
				255,
				182,
				193
			},
			{
				255,
				160,
				122
			},
			{
				32,
				178,
				170
			},
			{
				135,
				206,
				250
			},
			{
				119,
				136,
				153
			},
			{
				119,
				136,
				153
			},
			{
				176,
				196,
				222
			},
			{
				255,
				255,
				224
			},
			{
				0,
				255,
				0
			},
			{
				50,
				205,
				50
			},
			{
				250,
				240,
				230
			},
			{
				255,
				0,
				255
			},
			{
				128,
				0,
				0
			},
			{
				102,
				205,
				170
			},
			{
				0,
				0,
				205
			},
			{
				186,
				85,
				211
			},
			{
				147,
				112,
				219
			},
			{
				60,
				179,
				113
			},
			{
				123,
				104,
				238
			},
			{
				0,
				250,
				154
			},
			{
				72,
				209,
				204
			},
			{
				199,
				21,
				133
			},
			{
				25,
				25,
				112
			},
			{
				245,
				255,
				250
			},
			{
				255,
				228,
				225
			},
			{
				255,
				228,
				181
			},
			{
				255,
				222,
				173
			},
			{
				0,
				0,
				128
			},
			{
				253,
				245,
				230
			},
			{
				128,
				128,
				0
			},
			{
				107,
				142,
				35
			},
			{
				255,
				165,
				0
			},
			{
				255,
				69,
				0
			},
			{
				218,
				112,
				214
			},
			{
				238,
				232,
				170
			},
			{
				152,
				251,
				152
			},
			{
				175,
				238,
				238
			},
			{
				219,
				112,
				147
			},
			{
				255,
				239,
				213
			},
			{
				255,
				218,
				185
			},
			{
				205,
				133,
				63
			},
			{
				255,
				192,
				203
			},
			{
				221,
				160,
				221
			},
			{
				176,
				224,
				230
			},
			{
				128,
				0,
				128
			},
			{
				255,
				0,
				0
			},
			{
				188,
				143,
				143
			},
			{
				65,
				105,
				225
			},
			{
				139,
				69,
				19
			},
			{
				250,
				128,
				114
			},
			{
				248,
				248,
				255
			},
			{
				255,
				215,
				0
			},
			{
				218,
				165,
				32
			},
			{
				128,
				128,
				128
			},
			{
				128,
				128,
				128
			},
			{
				0,
				128,
				0
			},
			{
				173,
				255,
				47
			},
			{
				240,
				255,
				240
			},
			{
				255,
				105,
				180
			},
			{
				205,
				92,
				92
			},
			{
				75,
				0,
				130
			},
			{
				255,
				255,
				240
			},
			{
				240,
				230,
				140
			},
			{
				230,
				230,
				250
			},
			{
				255,
				240,
				245
			},
			{
				124,
				252,
				0
			},
			{
				255,
				250,
				205
			},
			{
				173,
				216,
				230
			},
			{
				240,
				128,
				128
			},
			{
				224,
				255,
				255
			},
			{
				250,
				250,
				210
			},
			{
				211,
				211,
				211
			},
			{
				144,
				238,
				144
			},
			{
				211,
				211,
				211
			},
			{
				244,
				164,
				96
			},
			{
				46,
				139,
				87
			},
			{
				255,
				245,
				238
			},
			{
				160,
				82,
				45
			},
			{
				192,
				192,
				192
			},
			{
				135,
				206,
				235
			},
			{
				106,
				90,
				205
			},
			{
				112,
				128,
				144
			},
			{
				112,
				128,
				144
			},
			{
				255,
				250,
				250
			},
			{
				0,
				255,
				127
			},
			{
				70,
				130,
				180
			},
			{
				210,
				180,
				140
			},
			{
				0,
				128,
				128
			},
			{
				216,
				191,
				216
			},
			{
				255,
				99,
				71
			},
			{
				64,
				224,
				208
			},
			{
				238,
				130,
				238
			},
			{
				245,
				222,
				179
			},
			{
				255,
				255,
				255
			},
			{
				245,
				245,
				245
			},
			{
				255,
				255,
				0
			},
			{
				154,
				205,
				50
			}
		};
	}
}

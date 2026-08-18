using System;
using System.Collections.Generic;
using System.Globalization;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.html
{
	// Token: 0x020005F9 RID: 1529
	public class WebColors : Dictionary<string, int[]>
	{
		// Token: 0x0600341C RID: 13340 RVA: 0x001431C0 File Offset: 0x001421C0
		static WebColors()
		{
			WebColors.NAMES["aliceblue"] = new int[]
			{
				240,
				248,
				255,
				0
			};
			WebColors.NAMES["antiquewhite"] = new int[]
			{
				250,
				235,
				215,
				0
			};
			Dictionary<string, int[]> names = WebColors.NAMES;
			string key = "aqua";
			int[] array = new int[4];
			array[1] = 255;
			array[2] = 255;
			names[key] = array;
			WebColors.NAMES["aquamarine"] = new int[]
			{
				127,
				255,
				212,
				0
			};
			WebColors.NAMES["azure"] = new int[]
			{
				240,
				255,
				255,
				0
			};
			WebColors.NAMES["beige"] = new int[]
			{
				245,
				245,
				220,
				0
			};
			WebColors.NAMES["bisque"] = new int[]
			{
				255,
				228,
				196,
				0
			};
			Dictionary<string, int[]> names2 = WebColors.NAMES;
			string key2 = "black";
			int[] value = new int[4];
			names2[key2] = value;
			WebColors.NAMES["blanchedalmond"] = new int[]
			{
				255,
				235,
				205,
				0
			};
			Dictionary<string, int[]> names3 = WebColors.NAMES;
			string key3 = "blue";
			int[] array2 = new int[4];
			array2[2] = 255;
			names3[key3] = array2;
			WebColors.NAMES["blueviolet"] = new int[]
			{
				138,
				43,
				226,
				0
			};
			WebColors.NAMES["brown"] = new int[]
			{
				165,
				42,
				42,
				0
			};
			WebColors.NAMES["burlywood"] = new int[]
			{
				222,
				184,
				135,
				0
			};
			WebColors.NAMES["cadetblue"] = new int[]
			{
				95,
				158,
				160,
				0
			};
			Dictionary<string, int[]> names4 = WebColors.NAMES;
			string key4 = "chartreuse";
			int[] array3 = new int[4];
			array3[0] = 127;
			array3[1] = 255;
			names4[key4] = array3;
			WebColors.NAMES["chocolate"] = new int[]
			{
				210,
				105,
				30,
				0
			};
			WebColors.NAMES["coral"] = new int[]
			{
				255,
				127,
				80,
				0
			};
			WebColors.NAMES["cornflowerblue"] = new int[]
			{
				100,
				149,
				237,
				0
			};
			WebColors.NAMES["cornsilk"] = new int[]
			{
				255,
				248,
				220,
				0
			};
			WebColors.NAMES["crimson"] = new int[]
			{
				220,
				20,
				60,
				0
			};
			Dictionary<string, int[]> names5 = WebColors.NAMES;
			string key5 = "cyan";
			int[] array4 = new int[4];
			array4[1] = 255;
			array4[2] = 255;
			names5[key5] = array4;
			Dictionary<string, int[]> names6 = WebColors.NAMES;
			string key6 = "darkblue";
			int[] array5 = new int[4];
			array5[2] = 139;
			names6[key6] = array5;
			Dictionary<string, int[]> names7 = WebColors.NAMES;
			string key7 = "darkcyan";
			int[] array6 = new int[4];
			array6[1] = 139;
			array6[2] = 139;
			names7[key7] = array6;
			WebColors.NAMES["darkgoldenrod"] = new int[]
			{
				184,
				134,
				11,
				0
			};
			WebColors.NAMES["darkgray"] = new int[]
			{
				169,
				169,
				169,
				0
			};
			Dictionary<string, int[]> names8 = WebColors.NAMES;
			string key8 = "darkgreen";
			int[] array7 = new int[4];
			array7[1] = 100;
			names8[key8] = array7;
			WebColors.NAMES["darkkhaki"] = new int[]
			{
				189,
				183,
				107,
				0
			};
			Dictionary<string, int[]> names9 = WebColors.NAMES;
			string key9 = "darkmagenta";
			int[] array8 = new int[4];
			array8[0] = 139;
			array8[2] = 139;
			names9[key9] = array8;
			WebColors.NAMES["darkolivegreen"] = new int[]
			{
				85,
				107,
				47,
				0
			};
			Dictionary<string, int[]> names10 = WebColors.NAMES;
			string key10 = "darkorange";
			int[] array9 = new int[4];
			array9[0] = 255;
			array9[1] = 140;
			names10[key10] = array9;
			WebColors.NAMES["darkorchid"] = new int[]
			{
				153,
				50,
				204,
				0
			};
			Dictionary<string, int[]> names11 = WebColors.NAMES;
			string key11 = "darkred";
			int[] array10 = new int[4];
			array10[0] = 139;
			names11[key11] = array10;
			WebColors.NAMES["darksalmon"] = new int[]
			{
				233,
				150,
				122,
				0
			};
			WebColors.NAMES["darkseagreen"] = new int[]
			{
				143,
				188,
				143,
				0
			};
			WebColors.NAMES["darkslateblue"] = new int[]
			{
				72,
				61,
				139,
				0
			};
			WebColors.NAMES["darkslategray"] = new int[]
			{
				47,
				79,
				79,
				0
			};
			Dictionary<string, int[]> names12 = WebColors.NAMES;
			string key12 = "darkturquoise";
			int[] array11 = new int[4];
			array11[1] = 206;
			array11[2] = 209;
			names12[key12] = array11;
			Dictionary<string, int[]> names13 = WebColors.NAMES;
			string key13 = "darkviolet";
			int[] array12 = new int[4];
			array12[0] = 148;
			array12[2] = 211;
			names13[key13] = array12;
			WebColors.NAMES["deeppink"] = new int[]
			{
				255,
				20,
				147,
				0
			};
			Dictionary<string, int[]> names14 = WebColors.NAMES;
			string key14 = "deepskyblue";
			int[] array13 = new int[4];
			array13[1] = 191;
			array13[2] = 255;
			names14[key14] = array13;
			WebColors.NAMES["dimgray"] = new int[]
			{
				105,
				105,
				105,
				0
			};
			WebColors.NAMES["dodgerblue"] = new int[]
			{
				30,
				144,
				255,
				0
			};
			WebColors.NAMES["firebrick"] = new int[]
			{
				178,
				34,
				34,
				0
			};
			WebColors.NAMES["floralwhite"] = new int[]
			{
				255,
				250,
				240,
				0
			};
			WebColors.NAMES["forestgreen"] = new int[]
			{
				34,
				139,
				34,
				0
			};
			Dictionary<string, int[]> names15 = WebColors.NAMES;
			string key15 = "fuchsia";
			int[] array14 = new int[4];
			array14[0] = 255;
			array14[2] = 255;
			names15[key15] = array14;
			WebColors.NAMES["gainsboro"] = new int[]
			{
				220,
				220,
				220,
				0
			};
			WebColors.NAMES["ghostwhite"] = new int[]
			{
				248,
				248,
				255,
				0
			};
			Dictionary<string, int[]> names16 = WebColors.NAMES;
			string key16 = "gold";
			int[] array15 = new int[4];
			array15[0] = 255;
			array15[1] = 215;
			names16[key16] = array15;
			WebColors.NAMES["goldenrod"] = new int[]
			{
				218,
				165,
				32,
				0
			};
			WebColors.NAMES["gray"] = new int[]
			{
				128,
				128,
				128,
				0
			};
			Dictionary<string, int[]> names17 = WebColors.NAMES;
			string key17 = "green";
			int[] array16 = new int[4];
			array16[1] = 128;
			names17[key17] = array16;
			WebColors.NAMES["greenyellow"] = new int[]
			{
				173,
				255,
				47,
				0
			};
			WebColors.NAMES["honeydew"] = new int[]
			{
				240,
				255,
				240,
				0
			};
			WebColors.NAMES["hotpink"] = new int[]
			{
				255,
				105,
				180,
				0
			};
			WebColors.NAMES["indianred"] = new int[]
			{
				205,
				92,
				92,
				0
			};
			Dictionary<string, int[]> names18 = WebColors.NAMES;
			string key18 = "indigo";
			int[] array17 = new int[4];
			array17[0] = 75;
			array17[2] = 130;
			names18[key18] = array17;
			WebColors.NAMES["ivory"] = new int[]
			{
				255,
				255,
				240,
				0
			};
			WebColors.NAMES["khaki"] = new int[]
			{
				240,
				230,
				140,
				0
			};
			WebColors.NAMES["lavender"] = new int[]
			{
				230,
				230,
				250,
				0
			};
			WebColors.NAMES["lavenderblush"] = new int[]
			{
				255,
				240,
				245,
				0
			};
			Dictionary<string, int[]> names19 = WebColors.NAMES;
			string key19 = "lawngreen";
			int[] array18 = new int[4];
			array18[0] = 124;
			array18[1] = 252;
			names19[key19] = array18;
			WebColors.NAMES["lemonchiffon"] = new int[]
			{
				255,
				250,
				205,
				0
			};
			WebColors.NAMES["lightblue"] = new int[]
			{
				173,
				216,
				230,
				0
			};
			WebColors.NAMES["lightcoral"] = new int[]
			{
				240,
				128,
				128,
				0
			};
			WebColors.NAMES["lightcyan"] = new int[]
			{
				224,
				255,
				255,
				0
			};
			WebColors.NAMES["lightgoldenrodyellow"] = new int[]
			{
				250,
				250,
				210,
				0
			};
			WebColors.NAMES["lightgreen"] = new int[]
			{
				144,
				238,
				144,
				0
			};
			WebColors.NAMES["lightgrey"] = new int[]
			{
				211,
				211,
				211,
				0
			};
			WebColors.NAMES["lightpink"] = new int[]
			{
				255,
				182,
				193,
				0
			};
			WebColors.NAMES["lightsalmon"] = new int[]
			{
				255,
				160,
				122,
				0
			};
			WebColors.NAMES["lightseagreen"] = new int[]
			{
				32,
				178,
				170,
				0
			};
			WebColors.NAMES["lightskyblue"] = new int[]
			{
				135,
				206,
				250,
				0
			};
			WebColors.NAMES["lightslategray"] = new int[]
			{
				119,
				136,
				153,
				0
			};
			WebColors.NAMES["lightsteelblue"] = new int[]
			{
				176,
				196,
				222,
				0
			};
			WebColors.NAMES["lightyellow"] = new int[]
			{
				255,
				255,
				224,
				0
			};
			Dictionary<string, int[]> names20 = WebColors.NAMES;
			string key20 = "lime";
			int[] array19 = new int[4];
			array19[1] = 255;
			names20[key20] = array19;
			WebColors.NAMES["limegreen"] = new int[]
			{
				50,
				205,
				50,
				0
			};
			WebColors.NAMES["linen"] = new int[]
			{
				250,
				240,
				230,
				0
			};
			Dictionary<string, int[]> names21 = WebColors.NAMES;
			string key21 = "magenta";
			int[] array20 = new int[4];
			array20[0] = 255;
			array20[2] = 255;
			names21[key21] = array20;
			Dictionary<string, int[]> names22 = WebColors.NAMES;
			string key22 = "maroon";
			int[] array21 = new int[4];
			array21[0] = 128;
			names22[key22] = array21;
			WebColors.NAMES["mediumaquamarine"] = new int[]
			{
				102,
				205,
				170,
				0
			};
			Dictionary<string, int[]> names23 = WebColors.NAMES;
			string key23 = "mediumblue";
			int[] array22 = new int[4];
			array22[2] = 205;
			names23[key23] = array22;
			WebColors.NAMES["mediumorchid"] = new int[]
			{
				186,
				85,
				211,
				0
			};
			WebColors.NAMES["mediumpurple"] = new int[]
			{
				147,
				112,
				219,
				0
			};
			WebColors.NAMES["mediumseagreen"] = new int[]
			{
				60,
				179,
				113,
				0
			};
			WebColors.NAMES["mediumslateblue"] = new int[]
			{
				123,
				104,
				238,
				0
			};
			Dictionary<string, int[]> names24 = WebColors.NAMES;
			string key24 = "mediumspringgreen";
			int[] array23 = new int[4];
			array23[1] = 250;
			array23[2] = 154;
			names24[key24] = array23;
			WebColors.NAMES["mediumturquoise"] = new int[]
			{
				72,
				209,
				204,
				0
			};
			WebColors.NAMES["mediumvioletred"] = new int[]
			{
				199,
				21,
				133,
				0
			};
			WebColors.NAMES["midnightblue"] = new int[]
			{
				25,
				25,
				112,
				0
			};
			WebColors.NAMES["mintcream"] = new int[]
			{
				245,
				255,
				250,
				0
			};
			WebColors.NAMES["mistyrose"] = new int[]
			{
				255,
				228,
				225,
				0
			};
			WebColors.NAMES["moccasin"] = new int[]
			{
				255,
				228,
				181,
				0
			};
			WebColors.NAMES["navajowhite"] = new int[]
			{
				255,
				222,
				173,
				0
			};
			Dictionary<string, int[]> names25 = WebColors.NAMES;
			string key25 = "navy";
			int[] array24 = new int[4];
			array24[2] = 128;
			names25[key25] = array24;
			WebColors.NAMES["oldlace"] = new int[]
			{
				253,
				245,
				230,
				0
			};
			Dictionary<string, int[]> names26 = WebColors.NAMES;
			string key26 = "olive";
			int[] array25 = new int[4];
			array25[0] = 128;
			array25[1] = 128;
			names26[key26] = array25;
			WebColors.NAMES["olivedrab"] = new int[]
			{
				107,
				142,
				35,
				0
			};
			Dictionary<string, int[]> names27 = WebColors.NAMES;
			string key27 = "orange";
			int[] array26 = new int[4];
			array26[0] = 255;
			array26[1] = 165;
			names27[key27] = array26;
			Dictionary<string, int[]> names28 = WebColors.NAMES;
			string key28 = "orangered";
			int[] array27 = new int[4];
			array27[0] = 255;
			array27[1] = 69;
			names28[key28] = array27;
			WebColors.NAMES["orchid"] = new int[]
			{
				218,
				112,
				214,
				0
			};
			WebColors.NAMES["palegoldenrod"] = new int[]
			{
				238,
				232,
				170,
				0
			};
			WebColors.NAMES["palegreen"] = new int[]
			{
				152,
				251,
				152,
				0
			};
			WebColors.NAMES["paleturquoise"] = new int[]
			{
				175,
				238,
				238,
				0
			};
			WebColors.NAMES["palevioletred"] = new int[]
			{
				219,
				112,
				147,
				0
			};
			WebColors.NAMES["papayawhip"] = new int[]
			{
				255,
				239,
				213,
				0
			};
			WebColors.NAMES["peachpuff"] = new int[]
			{
				255,
				218,
				185,
				0
			};
			WebColors.NAMES["peru"] = new int[]
			{
				205,
				133,
				63,
				0
			};
			WebColors.NAMES["pink"] = new int[]
			{
				255,
				192,
				203,
				0
			};
			WebColors.NAMES["plum"] = new int[]
			{
				221,
				160,
				221,
				0
			};
			WebColors.NAMES["powderblue"] = new int[]
			{
				176,
				224,
				230,
				0
			};
			Dictionary<string, int[]> names29 = WebColors.NAMES;
			string key29 = "purple";
			int[] array28 = new int[4];
			array28[0] = 128;
			array28[2] = 128;
			names29[key29] = array28;
			Dictionary<string, int[]> names30 = WebColors.NAMES;
			string key30 = "red";
			int[] array29 = new int[4];
			array29[0] = 255;
			names30[key30] = array29;
			WebColors.NAMES["rosybrown"] = new int[]
			{
				188,
				143,
				143,
				0
			};
			WebColors.NAMES["royalblue"] = new int[]
			{
				65,
				105,
				225,
				0
			};
			WebColors.NAMES["saddlebrown"] = new int[]
			{
				139,
				69,
				19,
				0
			};
			WebColors.NAMES["salmon"] = new int[]
			{
				250,
				128,
				114,
				0
			};
			WebColors.NAMES["sandybrown"] = new int[]
			{
				244,
				164,
				96,
				0
			};
			WebColors.NAMES["seagreen"] = new int[]
			{
				46,
				139,
				87,
				0
			};
			WebColors.NAMES["seashell"] = new int[]
			{
				255,
				245,
				238,
				0
			};
			WebColors.NAMES["sienna"] = new int[]
			{
				160,
				82,
				45,
				0
			};
			WebColors.NAMES["silver"] = new int[]
			{
				192,
				192,
				192,
				0
			};
			WebColors.NAMES["skyblue"] = new int[]
			{
				135,
				206,
				235,
				0
			};
			WebColors.NAMES["slateblue"] = new int[]
			{
				106,
				90,
				205,
				0
			};
			WebColors.NAMES["slategray"] = new int[]
			{
				112,
				128,
				144,
				0
			};
			WebColors.NAMES["snow"] = new int[]
			{
				255,
				250,
				250,
				0
			};
			Dictionary<string, int[]> names31 = WebColors.NAMES;
			string key31 = "springgreen";
			int[] array30 = new int[4];
			array30[1] = 255;
			array30[2] = 127;
			names31[key31] = array30;
			WebColors.NAMES["steelblue"] = new int[]
			{
				70,
				130,
				180,
				0
			};
			WebColors.NAMES["tan"] = new int[]
			{
				210,
				180,
				140,
				0
			};
			WebColors.NAMES["transparent"] = new int[]
			{
				0,
				0,
				0,
				255
			};
			Dictionary<string, int[]> names32 = WebColors.NAMES;
			string key32 = "teal";
			int[] array31 = new int[4];
			array31[1] = 128;
			array31[2] = 128;
			names32[key32] = array31;
			WebColors.NAMES["thistle"] = new int[]
			{
				216,
				191,
				216,
				0
			};
			WebColors.NAMES["tomato"] = new int[]
			{
				255,
				99,
				71,
				0
			};
			WebColors.NAMES["turquoise"] = new int[]
			{
				64,
				224,
				208,
				0
			};
			WebColors.NAMES["violet"] = new int[]
			{
				238,
				130,
				238,
				0
			};
			WebColors.NAMES["wheat"] = new int[]
			{
				245,
				222,
				179,
				0
			};
			WebColors.NAMES["white"] = new int[]
			{
				255,
				255,
				255,
				0
			};
			WebColors.NAMES["whitesmoke"] = new int[]
			{
				245,
				245,
				245,
				0
			};
			Dictionary<string, int[]> names33 = WebColors.NAMES;
			string key33 = "yellow";
			int[] array32 = new int[4];
			array32[0] = 255;
			array32[1] = 255;
			names33[key33] = array32;
			WebColors.NAMES["yellowgreen"] = new int[]
			{
				154,
				205,
				50,
				0
			};
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x0014445C File Offset: 0x0014345C
		public static BaseColor GetRGBColor(string name)
		{
			int[] array = new int[4];
			int[] array2 = array;
			if (name.StartsWith("#"))
			{
				if (name.Length == 4)
				{
					array2[0] = int.Parse(name.Substring(1, 1), NumberStyles.HexNumber) * 16;
					array2[1] = int.Parse(name.Substring(2, 1), NumberStyles.HexNumber) * 16;
					array2[2] = int.Parse(name.Substring(3), NumberStyles.HexNumber) * 16;
					return new BaseColor(array2[0], array2[1], array2[2], array2[3]);
				}
				if (name.Length == 7)
				{
					array2[0] = int.Parse(name.Substring(1, 2), NumberStyles.HexNumber);
					array2[1] = int.Parse(name.Substring(3, 2), NumberStyles.HexNumber);
					array2[2] = int.Parse(name.Substring(5), NumberStyles.HexNumber);
					return new BaseColor(array2[0], array2[1], array2[2], array2[3]);
				}
				throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.color.format.must.be.rgb.or.rrggbb"));
			}
			else
			{
				if (name.StartsWith("rgb("))
				{
					StringTokenizer stringTokenizer = new StringTokenizer(name, "rgb(), \t\r\n\f");
					for (int i = 0; i < 3; i++)
					{
						string text = stringTokenizer.NextToken();
						if (text.EndsWith("%"))
						{
							array2[i] = int.Parse(text.Substring(0, text.Length - 1)) * 255 / 100;
						}
						else
						{
							array2[i] = int.Parse(text);
						}
						if (array2[i] < 0)
						{
							array2[i] = 0;
						}
						else if (array2[i] > 255)
						{
							array2[i] = 255;
						}
					}
					return new BaseColor(array2[0], array2[1], array2[2], array2[3]);
				}
				name = name.ToLower(CultureInfo.InvariantCulture);
				if (!WebColors.NAMES.ContainsKey(name))
				{
					throw new ArgumentException("Color '" + name + "' not found.");
				}
				array2 = WebColors.NAMES[name];
				return new BaseColor(array2[0], array2[1], array2[2], array2[3]);
			}
		}

		// Token: 0x0400231C RID: 8988
		public static WebColors NAMES = new WebColors();
	}
}

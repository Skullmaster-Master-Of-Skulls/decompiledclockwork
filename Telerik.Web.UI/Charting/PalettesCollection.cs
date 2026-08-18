using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Charting
{
	// Token: 0x020017B2 RID: 6066
	public struct PalettesCollection
	{
		// Token: 0x0600EC35 RID: 60469 RVA: 0x0035B210 File Offset: 0x00359410
		static PalettesCollection()
		{
			PalettesCollection.Palettes.Sort();
		}

		// Token: 0x0600EC36 RID: 60470 RVA: 0x0035B30C File Offset: 0x0035950C
		public static Palette GetPalette(string name)
		{
			ColorBlend[] array = null;
			switch (name)
			{
			case "Default":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(213, 247, 255),
						Color.FromArgb(193, 239, 252),
						Color.FromArgb(157, 217, 238)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(218, 254, 122),
						Color.FromArgb(198, 244, 80),
						Color.FromArgb(153, 205, 46)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(136, 221, 246),
						Color.FromArgb(97, 203, 234),
						Color.FromArgb(59, 161, 197)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(163, 222, 78),
						Color.FromArgb(132, 207, 27),
						Color.FromArgb(102, 181, 3)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(79, 152, 198),
						Color.FromArgb(34, 123, 182),
						Color.FromArgb(4, 85, 156)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(86, 153, 78),
						Color.FromArgb(43, 126, 33),
						Color.FromArgb(18, 96, 3)
					})
				};
				break;
			case "Default2006":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(150, 150, 150),
						Color.FromArgb(194, 194, 194)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(215, 215, 214),
						Color.FromArgb(241, 241, 241)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(230, 230, 230),
						Color.FromArgb(249, 249, 249)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(241, 241, 241),
						Color.FromArgb(248, 249, 248)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(224, 224, 224),
						Color.FromArgb(255, 255, 255)
					})
				};
				break;
			case "Blue":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(222, 245, 245),
						Color.FromArgb(222, 245, 245)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(140, 198, 220),
						Color.FromArgb(140, 198, 220)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(27, 144, 196),
						Color.FromArgb(27, 144, 196)
					})
				};
				break;
			case "Brick":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(251, 236, 228),
						Color.FromArgb(254, 252, 251)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(234, 156, 126),
						Color.FromArgb(252, 239, 235)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(223, 68, 0),
						Color.FromArgb(250, 226, 215)
					})
				};
				break;
			case "Classic":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 206, 38),
						Color.FromArgb(255, 247, 221)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(99, 99, 99),
						Color.FromArgb(231, 231, 231)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(214, 214, 214),
						Color.FromArgb(249, 249, 249)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(192, 255, 192),
						Color.Azure
					})
				};
				break;
			case "Colorful":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 186, 74),
						Color.FromArgb(255, 244, 227)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(21, 197, 22),
						Color.FromArgb(218, 246, 218)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 91, 0),
						Color.FromArgb(255, 229, 215)
					}),
					new ColorBlend(new Color[]
					{
						Color.Blue,
						Color.FromArgb(192, 192, 255)
					}),
					new ColorBlend(new Color[]
					{
						Color.Gray,
						Color.WhiteSmoke
					}),
					new ColorBlend(new Color[]
					{
						Color.Yellow,
						Color.FromArgb(255, 255, 192)
					})
				};
				break;
			case "Rainbow":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.Red,
						Color.Red
					}),
					new ColorBlend(new Color[]
					{
						Color.Orange,
						Color.Orange
					}),
					new ColorBlend(new Color[]
					{
						Color.Yellow,
						Color.Yellow
					}),
					new ColorBlend(new Color[]
					{
						Color.Green,
						Color.Green
					}),
					new ColorBlend(new Color[]
					{
						Color.LightBlue,
						Color.LightBlue
					}),
					new ColorBlend(new Color[]
					{
						Color.Blue,
						Color.Blue
					}),
					new ColorBlend(new Color[]
					{
						Color.Violet,
						Color.Violet
					})
				};
				break;
			case "Gradient":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(199, 243, 178),
						Color.FromArgb(17, 147, 7)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(253, 226, 0),
						Color.FromArgb(255, 156, 0)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 128, 128),
						Color.FromArgb(192, 0, 0)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(128, 128, 255),
						Color.FromArgb(0, 0, 192)
					})
				};
				break;
			case "Green":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.DarkSeaGreen,
						Color.DarkSeaGreen
					}),
					new ColorBlend(new Color[]
					{
						Color.LightGreen,
						Color.LightGreen
					}),
					new ColorBlend(new Color[]
					{
						Color.ForestGreen,
						Color.ForestGreen
					}),
					new ColorBlend(new Color[]
					{
						Color.LimeGreen,
						Color.LimeGreen
					}),
					new ColorBlend(new Color[]
					{
						Color.PaleGreen,
						Color.PaleGreen
					}),
					new ColorBlend(new Color[]
					{
						Color.Green,
						Color.Green
					}),
					new ColorBlend(new Color[]
					{
						Color.Lime,
						Color.Lime
					}),
					new ColorBlend(new Color[]
					{
						Color.SeaGreen,
						Color.SeaGreen
					}),
					new ColorBlend(new Color[]
					{
						Color.SpringGreen,
						Color.SpringGreen
					})
				};
				break;
			case "ExcelClassic":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(156, 154, 255),
						Color.FromArgb(156, 154, 255)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(156, 48, 99),
						Color.FromArgb(156, 48, 99)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 255, 206),
						Color.FromArgb(255, 255, 206)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(206, 255, 255),
						Color.FromArgb(206, 255, 255)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(99, 0, 99),
						Color.FromArgb(99, 0, 99)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 130, 132),
						Color.FromArgb(255, 130, 132)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(0, 101, 206),
						Color.FromArgb(0, 101, 206)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(206, 207, 255),
						Color.FromArgb(206, 207, 255)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(0, 0, 132),
						Color.FromArgb(0, 0, 132)
					})
				};
				break;
			case "Outlook":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(128, 128, 255),
						Color.FromArgb(128, 128, 255)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(128, 32, 96),
						Color.FromArgb(128, 32, 96)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 255, 160),
						Color.FromArgb(255, 255, 160)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(160, 224, 224),
						Color.FromArgb(160, 224, 224)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(96, 0, 128),
						Color.FromArgb(96, 0, 128)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 206, 253),
						Color.FromArgb(255, 206, 253)
					})
				};
				break;
			case "Pastel":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(178, 186, 97),
						Color.FromArgb(243, 244, 230)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(213, 109, 79),
						Color.FromArgb(248, 232, 227)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(61, 186, 194),
						Color.FromArgb(225, 244, 245)
					}),
					new ColorBlend(new Color[]
					{
						Color.Khaki,
						Color.LemonChiffon
					}),
					new ColorBlend(new Color[]
					{
						Color.MediumSlateBlue,
						Color.Thistle
					})
				};
				break;
			case "Web":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(97, 131, 217),
						Color.FromArgb(194, 194, 194)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 153, 0),
						Color.FromArgb(241, 241, 241)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(172, 203, 45),
						Color.FromArgb(249, 249, 249)
					})
				};
				break;
			case "Office2007":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(69, 115, 167),
						Color.FromArgb(69, 115, 167)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(107, 70, 68),
						Color.FromArgb(107, 70, 68)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(136, 166, 78),
						Color.FromArgb(136, 166, 78)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(113, 88, 143),
						Color.FromArgb(113, 88, 143)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(66, 152, 175),
						Color.FromArgb(66, 152, 175)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(219, 132, 61),
						Color.FromArgb(219, 132, 61)
					})
				};
				break;
			case "BabyBlue":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(194, 230, 252),
						Color.FromArgb(194, 230, 252)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(231, 253, 199),
						Color.FromArgb(231, 253, 199)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(219, 253, 254),
						Color.FromArgb(219, 253, 254)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 226, 245),
						Color.FromArgb(255, 226, 245)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 252, 199),
						Color.FromArgb(255, 252, 199)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(235, 218, 254),
						Color.FromArgb(235, 218, 254)
					})
				};
				break;
			case "Vista":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(186, 207, 141),
						Color.FromArgb(146, 176, 83)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(235, 140, 104),
						Color.FromArgb(214, 110, 90)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(129, 192, 225),
						Color.FromArgb(79, 129, 189)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(200, 182, 80),
						Color.FromArgb(176, 162, 83)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(171, 62, 29),
						Color.FromArgb(115, 36, 12)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(156, 121, 182),
						Color.FromArgb(127, 91, 154)
					})
				};
				break;
			case "Stripes":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(222, 202, 152),
						Color.FromArgb(211, 185, 123),
						Color.FromArgb(183, 154, 84)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(172, 208, 217),
						Color.FromArgb(149, 193, 204),
						Color.FromArgb(114, 162, 175)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(185, 208, 152),
						Color.FromArgb(164, 194, 122),
						Color.FromArgb(131, 166, 80)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(187, 174, 165),
						Color.FromArgb(163, 146, 135),
						Color.FromArgb(134, 115, 103)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(192, 161, 188),
						Color.FromArgb(174, 136, 169),
						Color.FromArgb(154, 108, 149)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(149, 179, 179),
						Color.FromArgb(117, 155, 155),
						Color.FromArgb(96, 134, 134)
					})
				};
				break;
			case "Deep":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(213, 247, 255),
						Color.FromArgb(193, 239, 252),
						Color.FromArgb(157, 217, 238)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(163, 222, 78),
						Color.FromArgb(132, 207, 27),
						Color.FromArgb(102, 181, 3)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(136, 221, 246),
						Color.FromArgb(97, 203, 234),
						Color.FromArgb(59, 161, 197)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(218, 254, 122),
						Color.FromArgb(193, 240, 74),
						Color.FromArgb(163, 205, 46)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(79, 152, 198),
						Color.FromArgb(34, 123, 182),
						Color.FromArgb(4, 85, 156)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(86, 153, 78),
						Color.FromArgb(43, 126, 33),
						Color.FromArgb(18, 96, 3)
					})
				};
				break;
			case "Ultra":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 224, 78),
						Color.FromArgb(247, 204, 22),
						Color.FromArgb(226, 175, 3)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(136, 221, 246),
						Color.FromArgb(97, 203, 234),
						Color.FromArgb(59, 161, 197)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(177, 222, 84),
						Color.FromArgb(150, 209, 35),
						Color.FromArgb(118, 187, 10)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 170, 78),
						Color.FromArgb(245, 139, 27),
						Color.FromArgb(201, 102, 3)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(243, 147, 213),
						Color.FromArgb(237, 111, 197),
						Color.FromArgb(221, 72, 174)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(194, 143, 236),
						Color.FromArgb(175, 106, 227),
						Color.FromArgb(153, 76, 211)
					})
				};
				break;
			case "Light":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(243, 206, 119),
						Color.FromArgb(236, 190, 82),
						Color.FromArgb(210, 157, 44)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(154, 220, 230),
						Color.FromArgb(121, 207, 220),
						Color.FromArgb(89, 185, 204)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(161, 218, 123),
						Color.FromArgb(130, 202, 82),
						Color.FromArgb(104, 179, 48)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(227, 129, 99),
						Color.FromArgb(213, 91, 53),
						Color.FromArgb(185, 68, 23)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(206, 134, 220),
						Color.FromArgb(189, 98, 205),
						Color.FromArgb(170, 75, 183)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(108, 145, 238),
						Color.FromArgb(69, 114, 229),
						Color.FromArgb(38, 79, 189)
					})
				};
				break;
			case "Mac":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(55, 167, 226),
						Color.FromArgb(22, 85, 161)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(223, 87, 60),
						Color.FromArgb(200, 38, 37)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(118, 203, 76),
						Color.FromArgb(73, 166, 22)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(119, 83, 76),
						Color.FromArgb(88, 59, 52)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(255, 222, 0),
						Color.FromArgb(242, 188, 0)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(164, 65, 221),
						Color.FromArgb(125, 24, 167)
					})
				};
				break;
			case "Inox":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(195, 163, 130),
						Color.FromArgb(226, 207, 180),
						Color.FromArgb(249, 241, 220)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(130, 142, 158),
						Color.FromArgb(178, 194, 205),
						Color.FromArgb(220, 238, 244)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(194, 204, 215),
						Color.FromArgb(226, 231, 236),
						Color.FromArgb(251, 252, 253)
					})
				};
				break;
			case "WebBlue":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(94, 117, 142),
						Color.FromArgb(116, 138, 162),
						Color.FromArgb(139, 160, 183)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(164, 175, 187),
						Color.FromArgb(196, 203, 212),
						Color.FromArgb(221, 226, 233)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(230, 234, 237),
						Color.FromArgb(242, 244, 246),
						Color.FromArgb(255, 255, 255)
					})
				};
				break;
			case "Gray":
				array = new ColorBlend[]
				{
					new ColorBlend(new Color[]
					{
						Color.FromArgb(234, 234, 234),
						Color.FromArgb(222, 222, 222),
						Color.FromArgb(197, 197, 197)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(191, 191, 191),
						Color.FromArgb(165, 165, 165),
						Color.Gray
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(150, 150, 150),
						Color.FromArgb(117, 117, 117),
						Color.FromArgb(92, 92, 92)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(188, 188, 188),
						Color.FromArgb(164, 164, 164),
						Color.FromArgb(125, 125, 125)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(138, 138, 138),
						Color.FromArgb(108, 108, 108),
						Color.FromArgb(80, 80, 80)
					}),
					new ColorBlend(new Color[]
					{
						Color.FromArgb(115, 115, 115),
						Color.FromArgb(79, 79, 79),
						Color.FromArgb(49, 49, 49)
					})
				};
				break;
			}
			if (array != null)
			{
				return new Palette(name, array);
			}
			return null;
		}

		// Token: 0x0600EC37 RID: 60471 RVA: 0x0035E151 File Offset: 0x0035C351
		public static bool Contains(string name)
		{
			return PalettesCollection.Palettes.Contains(name);
		}

		// Token: 0x0600EC38 RID: 60472 RVA: 0x0035E15E File Offset: 0x0035C35E
		public static Palette GetPalette(string name, Chart chart)
		{
			if (chart.CustomPalettes.Contains(name))
			{
				return chart.CustomPalettes.GetPalette(name);
			}
			throw new ChartException(string.Format("There is no Palette with name: {0}", name));
		}

		// Token: 0x04004426 RID: 17446
		public static List<string> Palettes = new List<string>(new string[]
		{
			"Default",
			"Blue",
			"Brick",
			"Classic",
			"Colorful",
			"Rainbow",
			"Gradient",
			"Green",
			"ExcelClassic",
			"Pastel",
			"Web",
			"Office2007",
			"BabyBlue",
			"Stripes",
			"Deep",
			"Ultra",
			"Light",
			"Default2006",
			"Vista",
			"Outlook",
			"Inox",
			"Mac",
			"WebBlue",
			"Gray"
		});
	}
}

using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000327 RID: 807
	public sealed class ProfessionalColors
	{
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000E3C6A File Offset: 0x000E1E6A
		internal static ProfessionalColorTable ColorTable
		{
			get
			{
				if (ProfessionalColors.professionalColorTable == null)
				{
					ProfessionalColors.professionalColorTable = new ProfessionalColorTable();
				}
				return ProfessionalColors.professionalColorTable;
			}
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000E3C82 File Offset: 0x000E1E82
		static ProfessionalColors()
		{
			SystemEvents.UserPreferenceChanged += ProfessionalColors.OnUserPreferenceChanged;
			ProfessionalColors.SetScheme();
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x00002843 File Offset: 0x00000A43
		private ProfessionalColors()
		{
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000E3C9A File Offset: 0x000E1E9A
		internal static string ColorScheme
		{
			get
			{
				return ProfessionalColors.colorScheme;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000E3CA1 File Offset: 0x000E1EA1
		internal static object ColorFreshnessKey
		{
			get
			{
				return ProfessionalColors.colorFreshnessKey;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000E3CA8 File Offset: 0x000E1EA8
		[SRDescription("ProfessionalColorsButtonSelectedHighlightDescr")]
		public static Color ButtonSelectedHighlight
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedHighlight;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000E3CB4 File Offset: 0x000E1EB4
		[SRDescription("ProfessionalColorsButtonSelectedHighlightBorderDescr")]
		public static Color ButtonSelectedHighlightBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedHighlightBorder;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000E3CC0 File Offset: 0x000E1EC0
		[SRDescription("ProfessionalColorsButtonPressedHighlightDescr")]
		public static Color ButtonPressedHighlight
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedHighlight;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000E3CCC File Offset: 0x000E1ECC
		[SRDescription("ProfessionalColorsButtonPressedHighlightBorderDescr")]
		public static Color ButtonPressedHighlightBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedHighlightBorder;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000E3CD8 File Offset: 0x000E1ED8
		[SRDescription("ProfessionalColorsButtonCheckedHighlightDescr")]
		public static Color ButtonCheckedHighlight
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedHighlight;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000E3CE4 File Offset: 0x000E1EE4
		[SRDescription("ProfessionalColorsButtonCheckedHighlightBorderDescr")]
		public static Color ButtonCheckedHighlightBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedHighlightBorder;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000E3CF0 File Offset: 0x000E1EF0
		[SRDescription("ProfessionalColorsButtonPressedBorderDescr")]
		public static Color ButtonPressedBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedBorder;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000E3CFC File Offset: 0x000E1EFC
		[SRDescription("ProfessionalColorsButtonSelectedBorderDescr")]
		public static Color ButtonSelectedBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedBorder;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000E3D08 File Offset: 0x000E1F08
		[SRDescription("ProfessionalColorsButtonCheckedGradientBeginDescr")]
		public static Color ButtonCheckedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedGradientBegin;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000E3D14 File Offset: 0x000E1F14
		[SRDescription("ProfessionalColorsButtonCheckedGradientMiddleDescr")]
		public static Color ButtonCheckedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedGradientMiddle;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000E3D20 File Offset: 0x000E1F20
		[SRDescription("ProfessionalColorsButtonCheckedGradientEndDescr")]
		public static Color ButtonCheckedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedGradientEnd;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000E3D2C File Offset: 0x000E1F2C
		[SRDescription("ProfessionalColorsButtonSelectedGradientBeginDescr")]
		public static Color ButtonSelectedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedGradientBegin;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000E3D38 File Offset: 0x000E1F38
		[SRDescription("ProfessionalColorsButtonSelectedGradientMiddleDescr")]
		public static Color ButtonSelectedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedGradientMiddle;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000E3D44 File Offset: 0x000E1F44
		[SRDescription("ProfessionalColorsButtonSelectedGradientEndDescr")]
		public static Color ButtonSelectedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedGradientEnd;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000E3D50 File Offset: 0x000E1F50
		[SRDescription("ProfessionalColorsButtonPressedGradientBeginDescr")]
		public static Color ButtonPressedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedGradientBegin;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000E3D5C File Offset: 0x000E1F5C
		[SRDescription("ProfessionalColorsButtonPressedGradientMiddleDescr")]
		public static Color ButtonPressedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedGradientMiddle;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000E3D68 File Offset: 0x000E1F68
		[SRDescription("ProfessionalColorsButtonPressedGradientEndDescr")]
		public static Color ButtonPressedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedGradientEnd;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000E3D74 File Offset: 0x000E1F74
		[SRDescription("ProfessionalColorsCheckBackgroundDescr")]
		public static Color CheckBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.CheckBackground;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000E3D80 File Offset: 0x000E1F80
		[SRDescription("ProfessionalColorsCheckSelectedBackgroundDescr")]
		public static Color CheckSelectedBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.CheckSelectedBackground;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000E3D8C File Offset: 0x000E1F8C
		[SRDescription("ProfessionalColorsCheckPressedBackgroundDescr")]
		public static Color CheckPressedBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.CheckPressedBackground;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000E3D98 File Offset: 0x000E1F98
		[SRDescription("ProfessionalColorsGripDarkDescr")]
		public static Color GripDark
		{
			get
			{
				return ProfessionalColors.ColorTable.GripDark;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000E3DA4 File Offset: 0x000E1FA4
		[SRDescription("ProfessionalColorsGripLightDescr")]
		public static Color GripLight
		{
			get
			{
				return ProfessionalColors.ColorTable.GripLight;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000E3DB0 File Offset: 0x000E1FB0
		[SRDescription("ProfessionalColorsImageMarginGradientBeginDescr")]
		public static Color ImageMarginGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginGradientBegin;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000E3DBC File Offset: 0x000E1FBC
		[SRDescription("ProfessionalColorsImageMarginGradientMiddleDescr")]
		public static Color ImageMarginGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginGradientMiddle;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000E3DC8 File Offset: 0x000E1FC8
		[SRDescription("ProfessionalColorsImageMarginGradientEndDescr")]
		public static Color ImageMarginGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginGradientEnd;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000E3DD4 File Offset: 0x000E1FD4
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientBeginDescr")]
		public static Color ImageMarginRevealedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginRevealedGradientBegin;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000E3DE0 File Offset: 0x000E1FE0
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientMiddleDescr")]
		public static Color ImageMarginRevealedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginRevealedGradientMiddle;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000E3DEC File Offset: 0x000E1FEC
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientEndDescr")]
		public static Color ImageMarginRevealedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginRevealedGradientEnd;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000E3DF8 File Offset: 0x000E1FF8
		[SRDescription("ProfessionalColorsMenuStripGradientBeginDescr")]
		public static Color MenuStripGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuStripGradientBegin;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000E3E04 File Offset: 0x000E2004
		[SRDescription("ProfessionalColorsMenuStripGradientEndDescr")]
		public static Color MenuStripGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuStripGradientEnd;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000E3E10 File Offset: 0x000E2010
		[SRDescription("ProfessionalColorsMenuBorderDescr")]
		public static Color MenuBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuBorder;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000E3E1C File Offset: 0x000E201C
		[SRDescription("ProfessionalColorsMenuItemSelectedDescr")]
		public static Color MenuItemSelected
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemSelected;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000E3E28 File Offset: 0x000E2028
		[SRDescription("ProfessionalColorsMenuItemBorderDescr")]
		public static Color MenuItemBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemBorder;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000E3E34 File Offset: 0x000E2034
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientBeginDescr")]
		public static Color MenuItemSelectedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemSelectedGradientBegin;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000E3E40 File Offset: 0x000E2040
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientEndDescr")]
		public static Color MenuItemSelectedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemSelectedGradientEnd;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000E3E4C File Offset: 0x000E204C
		[SRDescription("ProfessionalColorsMenuItemPressedGradientBeginDescr")]
		public static Color MenuItemPressedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemPressedGradientBegin;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000E3E58 File Offset: 0x000E2058
		[SRDescription("ProfessionalColorsMenuItemPressedGradientMiddleDescr")]
		public static Color MenuItemPressedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemPressedGradientMiddle;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000E3E64 File Offset: 0x000E2064
		[SRDescription("ProfessionalColorsMenuItemPressedGradientEndDescr")]
		public static Color MenuItemPressedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemPressedGradientEnd;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000E3E70 File Offset: 0x000E2070
		[SRDescription("ProfessionalColorsRaftingContainerGradientBeginDescr")]
		public static Color RaftingContainerGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.RaftingContainerGradientBegin;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000E3E7C File Offset: 0x000E207C
		[SRDescription("ProfessionalColorsRaftingContainerGradientEndDescr")]
		public static Color RaftingContainerGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.RaftingContainerGradientEnd;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000E3E88 File Offset: 0x000E2088
		[SRDescription("ProfessionalColorsSeparatorDarkDescr")]
		public static Color SeparatorDark
		{
			get
			{
				return ProfessionalColors.ColorTable.SeparatorDark;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000E3E94 File Offset: 0x000E2094
		[SRDescription("ProfessionalColorsSeparatorLightDescr")]
		public static Color SeparatorLight
		{
			get
			{
				return ProfessionalColors.ColorTable.SeparatorLight;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000E3EA0 File Offset: 0x000E20A0
		[SRDescription("ProfessionalColorsStatusStripGradientBeginDescr")]
		public static Color StatusStripGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.StatusStripGradientBegin;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000E3EAC File Offset: 0x000E20AC
		[SRDescription("ProfessionalColorsStatusStripGradientEndDescr")]
		public static Color StatusStripGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.StatusStripGradientEnd;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000E3EB8 File Offset: 0x000E20B8
		[SRDescription("ProfessionalColorsToolStripBorderDescr")]
		public static Color ToolStripBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripBorder;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000E3EC4 File Offset: 0x000E20C4
		[SRDescription("ProfessionalColorsToolStripDropDownBackgroundDescr")]
		public static Color ToolStripDropDownBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripDropDownBackground;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000E3ED0 File Offset: 0x000E20D0
		[SRDescription("ProfessionalColorsToolStripGradientBeginDescr")]
		public static Color ToolStripGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripGradientBegin;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000E3EDC File Offset: 0x000E20DC
		[SRDescription("ProfessionalColorsToolStripGradientMiddleDescr")]
		public static Color ToolStripGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripGradientMiddle;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000E3EE8 File Offset: 0x000E20E8
		[SRDescription("ProfessionalColorsToolStripGradientEndDescr")]
		public static Color ToolStripGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripGradientEnd;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000E3EF4 File Offset: 0x000E20F4
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientBeginDescr")]
		public static Color ToolStripContentPanelGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripContentPanelGradientBegin;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000E3F00 File Offset: 0x000E2100
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientEndDescr")]
		public static Color ToolStripContentPanelGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripContentPanelGradientEnd;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000E3F0C File Offset: 0x000E210C
		[SRDescription("ProfessionalColorsToolStripPanelGradientBeginDescr")]
		public static Color ToolStripPanelGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripPanelGradientBegin;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000E3F18 File Offset: 0x000E2118
		[SRDescription("ProfessionalColorsToolStripPanelGradientEndDescr")]
		public static Color ToolStripPanelGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripPanelGradientEnd;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000E3F24 File Offset: 0x000E2124
		[SRDescription("ProfessionalColorsOverflowButtonGradientBeginDescr")]
		public static Color OverflowButtonGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.OverflowButtonGradientBegin;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000E3F30 File Offset: 0x000E2130
		[SRDescription("ProfessionalColorsOverflowButtonGradientMiddleDescr")]
		public static Color OverflowButtonGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.OverflowButtonGradientMiddle;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000E3F3C File Offset: 0x000E213C
		[SRDescription("ProfessionalColorsOverflowButtonGradientEndDescr")]
		public static Color OverflowButtonGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.OverflowButtonGradientEnd;
			}
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000E3F48 File Offset: 0x000E2148
		private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			ProfessionalColors.SetScheme();
			if (e.Category == UserPreferenceCategory.Color)
			{
				ProfessionalColors.colorFreshnessKey = new object();
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000E3F62 File Offset: 0x000E2162
		private static void SetScheme()
		{
			if (VisualStyleRenderer.IsSupported)
			{
				ProfessionalColors.colorScheme = VisualStyleInformation.ColorScheme;
				return;
			}
			ProfessionalColors.colorScheme = null;
		}

		// Token: 0x04001EC3 RID: 7875
		[ThreadStatic]
		private static ProfessionalColorTable professionalColorTable;

		// Token: 0x04001EC4 RID: 7876
		[ThreadStatic]
		private static string colorScheme;

		// Token: 0x04001EC5 RID: 7877
		[ThreadStatic]
		private static object colorFreshnessKey;
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000328 RID: 808
	public class ProfessionalColorTable
	{
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000E3F90 File Offset: 0x000E2190
		private Dictionary<ProfessionalColorTable.KnownColors, Color> ColorTable
		{
			get
			{
				if (this.UseSystemColors)
				{
					if (!this.usingSystemColors || this.professionalRGB == null)
					{
						if (this.professionalRGB == null)
						{
							this.professionalRGB = new Dictionary<ProfessionalColorTable.KnownColors, Color>(212);
						}
						this.InitSystemColors(ref this.professionalRGB);
					}
				}
				else if (ToolStripManager.VisualStylesEnabled)
				{
					if (this.usingSystemColors || this.professionalRGB == null)
					{
						if (this.professionalRGB == null)
						{
							this.professionalRGB = new Dictionary<ProfessionalColorTable.KnownColors, Color>(212);
						}
						this.InitThemedColors(ref this.professionalRGB);
					}
				}
				else if (!this.usingSystemColors || this.professionalRGB == null)
				{
					if (this.professionalRGB == null)
					{
						this.professionalRGB = new Dictionary<ProfessionalColorTable.KnownColors, Color>(212);
					}
					this.InitSystemColors(ref this.professionalRGB);
				}
				return this.professionalRGB;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000E4055 File Offset: 0x000E2255
		// (set) Token: 0x06003359 RID: 13145 RVA: 0x000E405D File Offset: 0x000E225D
		public bool UseSystemColors
		{
			get
			{
				return this.useSystemColors;
			}
			set
			{
				if (this.useSystemColors != value)
				{
					this.useSystemColors = value;
					this.ResetRGBTable();
				}
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000E4078 File Offset: 0x000E2278
		internal Color FromKnownColor(ProfessionalColorTable.KnownColors color)
		{
			if (ProfessionalColors.ColorFreshnessKey != this.colorFreshnessKey || ProfessionalColors.ColorScheme != this.lastKnownColorScheme)
			{
				this.ResetRGBTable();
			}
			this.colorFreshnessKey = ProfessionalColors.ColorFreshnessKey;
			this.lastKnownColorScheme = ProfessionalColors.ColorScheme;
			return this.ColorTable[color];
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000E40CC File Offset: 0x000E22CC
		private void ResetRGBTable()
		{
			if (this.professionalRGB != null)
			{
				this.professionalRGB.Clear();
			}
			this.professionalRGB = null;
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000E40E8 File Offset: 0x000E22E8
		[SRDescription("ProfessionalColorsButtonSelectedHighlightDescr")]
		public virtual Color ButtonSelectedHighlight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.ButtonSelectedHighlight);
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000E40F5 File Offset: 0x000E22F5
		[SRDescription("ProfessionalColorsButtonSelectedHighlightBorderDescr")]
		public virtual Color ButtonSelectedHighlightBorder
		{
			get
			{
				return this.ButtonPressedBorder;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000E40FD File Offset: 0x000E22FD
		[SRDescription("ProfessionalColorsButtonPressedHighlightDescr")]
		public virtual Color ButtonPressedHighlight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.ButtonPressedHighlight);
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000E410A File Offset: 0x000E230A
		[SRDescription("ProfessionalColorsButtonPressedHighlightBorderDescr")]
		public virtual Color ButtonPressedHighlightBorder
		{
			get
			{
				return SystemColors.Highlight;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000E4111 File Offset: 0x000E2311
		[SRDescription("ProfessionalColorsButtonCheckedHighlightDescr")]
		public virtual Color ButtonCheckedHighlight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.ButtonCheckedHighlight);
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000E410A File Offset: 0x000E230A
		[SRDescription("ProfessionalColorsButtonCheckedHighlightBorderDescr")]
		public virtual Color ButtonCheckedHighlightBorder
		{
			get
			{
				return SystemColors.Highlight;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000E411E File Offset: 0x000E231E
		[SRDescription("ProfessionalColorsButtonPressedBorderDescr")]
		public virtual Color ButtonPressedBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver);
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000E411E File Offset: 0x000E231E
		[SRDescription("ProfessionalColorsButtonSelectedBorderDescr")]
		public virtual Color ButtonSelectedBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x000E4127 File Offset: 0x000E2327
		[SRDescription("ProfessionalColorsButtonCheckedGradientBeginDescr")]
		public virtual Color ButtonCheckedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000E4131 File Offset: 0x000E2331
		[SRDescription("ProfessionalColorsButtonCheckedGradientMiddleDescr")]
		public virtual Color ButtonCheckedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000E413B File Offset: 0x000E233B
		[SRDescription("ProfessionalColorsButtonCheckedGradientEndDescr")]
		public virtual Color ButtonCheckedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000E4145 File Offset: 0x000E2345
		[SRDescription("ProfessionalColorsButtonSelectedGradientBeginDescr")]
		public virtual Color ButtonSelectedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin);
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000E414F File Offset: 0x000E234F
		[SRDescription("ProfessionalColorsButtonSelectedGradientMiddleDescr")]
		public virtual Color ButtonSelectedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000E4159 File Offset: 0x000E2359
		[SRDescription("ProfessionalColorsButtonSelectedGradientEndDescr")]
		public virtual Color ButtonSelectedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000E4163 File Offset: 0x000E2363
		[SRDescription("ProfessionalColorsButtonPressedGradientBeginDescr")]
		public virtual Color ButtonPressedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin);
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000E416D File Offset: 0x000E236D
		[SRDescription("ProfessionalColorsButtonPressedGradientMiddleDescr")]
		public virtual Color ButtonPressedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle);
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000E4177 File Offset: 0x000E2377
		[SRDescription("ProfessionalColorsButtonPressedGradientEndDescr")]
		public virtual Color ButtonPressedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd);
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000E4181 File Offset: 0x000E2381
		[SRDescription("ProfessionalColorsCheckBackgroundDescr")]
		public virtual Color CheckBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected);
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000E418B File Offset: 0x000E238B
		[SRDescription("ProfessionalColorsCheckSelectedBackgroundDescr")]
		public virtual Color CheckSelectedBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver);
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000E418B File Offset: 0x000E238B
		[SRDescription("ProfessionalColorsCheckPressedBackgroundDescr")]
		public virtual Color CheckPressedBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver);
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000E4195 File Offset: 0x000E2395
		[SRDescription("ProfessionalColorsGripDarkDescr")]
		public virtual Color GripDark
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle);
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000E419F File Offset: 0x000E239F
		[SRDescription("ProfessionalColorsGripLightDescr")]
		public virtual Color GripLight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow);
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000E41A9 File Offset: 0x000E23A9
		[SRDescription("ProfessionalColorsImageMarginGradientBeginDescr")]
		public virtual Color ImageMarginGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin);
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000E41B3 File Offset: 0x000E23B3
		[SRDescription("ProfessionalColorsImageMarginGradientMiddleDescr")]
		public virtual Color ImageMarginGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle);
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000E41BD File Offset: 0x000E23BD
		[SRDescription("ProfessionalColorsImageMarginGradientEndDescr")]
		public virtual Color ImageMarginGradientEnd
		{
			get
			{
				if (!this.usingSystemColors)
				{
					return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd);
				}
				return SystemColors.Control;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000E41D5 File Offset: 0x000E23D5
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientBeginDescr")]
		public virtual Color ImageMarginRevealedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin);
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000E41DF File Offset: 0x000E23DF
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientMiddleDescr")]
		public virtual Color ImageMarginRevealedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle);
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000E41E9 File Offset: 0x000E23E9
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientEndDescr")]
		public virtual Color ImageMarginRevealedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd);
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000E41F3 File Offset: 0x000E23F3
		[SRDescription("ProfessionalColorsMenuStripGradientBeginDescr")]
		public virtual Color MenuStripGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000E41FD File Offset: 0x000E23FD
		[SRDescription("ProfessionalColorsMenuStripGradientEndDescr")]
		public virtual Color MenuStripGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000E4207 File Offset: 0x000E2407
		[SRDescription("ProfessionalColorsMenuItemSelectedDescr")]
		public virtual Color MenuItemSelected
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver);
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000E4211 File Offset: 0x000E2411
		[SRDescription("ProfessionalColorsMenuItemBorderDescr")]
		public virtual Color MenuItemBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected);
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000E421A File Offset: 0x000E241A
		[SRDescription("ProfessionalColorsMenuBorderDescr")]
		public virtual Color MenuBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter);
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000E4145 File Offset: 0x000E2345
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientBeginDescr")]
		public virtual Color MenuItemSelectedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin);
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000E4159 File Offset: 0x000E2359
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientEndDescr")]
		public virtual Color MenuItemSelectedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd);
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000E4224 File Offset: 0x000E2424
		[SRDescription("ProfessionalColorsMenuItemPressedGradientBeginDescr")]
		public virtual Color MenuItemPressedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin);
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000E41DF File Offset: 0x000E23DF
		[SRDescription("ProfessionalColorsMenuItemPressedGradientMiddleDescr")]
		public virtual Color MenuItemPressedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle);
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000E422E File Offset: 0x000E242E
		[SRDescription("ProfessionalColorsMenuItemPressedGradientEndDescr")]
		public virtual Color MenuItemPressedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd);
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000E41F3 File Offset: 0x000E23F3
		[SRDescription("ProfessionalColorsRaftingContainerGradientBeginDescr")]
		public virtual Color RaftingContainerGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000E41FD File Offset: 0x000E23FD
		[SRDescription("ProfessionalColorsRaftingContainerGradientEndDescr")]
		public virtual Color RaftingContainerGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000E4238 File Offset: 0x000E2438
		[SRDescription("ProfessionalColorsSeparatorDarkDescr")]
		public virtual Color SeparatorDark
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine);
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000E4242 File Offset: 0x000E2442
		[SRDescription("ProfessionalColorsSeparatorLightDescr")]
		public virtual Color SeparatorLight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000E41F3 File Offset: 0x000E23F3
		[SRDescription("ProfessionalColorsStatusStripGradientBeginDescr")]
		public virtual Color StatusStripGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000E41FD File Offset: 0x000E23FD
		[SRDescription("ProfessionalColorsStatusStripGradientEndDescr")]
		public virtual Color StatusStripGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000E424C File Offset: 0x000E244C
		[SRDescription("ProfessionalColorsToolStripBorderDescr")]
		public virtual Color ToolStripBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBShadow);
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000E4256 File Offset: 0x000E2456
		[SRDescription("ProfessionalColorsToolStripDropDownBackgroundDescr")]
		public virtual Color ToolStripDropDownBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd);
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000E41A9 File Offset: 0x000E23A9
		[SRDescription("ProfessionalColorsToolStripGradientBeginDescr")]
		public virtual Color ToolStripGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin);
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000E41B3 File Offset: 0x000E23B3
		[SRDescription("ProfessionalColorsToolStripGradientMiddleDescr")]
		public virtual Color ToolStripGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle);
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000E4260 File Offset: 0x000E2460
		[SRDescription("ProfessionalColorsToolStripGradientEndDescr")]
		public virtual Color ToolStripGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd);
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x000E41F3 File Offset: 0x000E23F3
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientBeginDescr")]
		public virtual Color ToolStripContentPanelGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000E41FD File Offset: 0x000E23FD
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientEndDescr")]
		public virtual Color ToolStripContentPanelGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000E41F3 File Offset: 0x000E23F3
		[SRDescription("ProfessionalColorsToolStripPanelGradientBeginDescr")]
		public virtual Color ToolStripPanelGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000E41FD File Offset: 0x000E23FD
		[SRDescription("ProfessionalColorsToolStripPanelGradientEndDescr")]
		public virtual Color ToolStripPanelGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x000E426A File Offset: 0x000E246A
		[SRDescription("ProfessionalColorsOverflowButtonGradientBeginDescr")]
		public virtual Color OverflowButtonGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin);
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000E4274 File Offset: 0x000E2474
		[SRDescription("ProfessionalColorsOverflowButtonGradientMiddleDescr")]
		public virtual Color OverflowButtonGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000E427E File Offset: 0x000E247E
		[SRDescription("ProfessionalColorsOverflowButtonGradientEndDescr")]
		public virtual Color OverflowButtonGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000E4288 File Offset: 0x000E2488
		internal Color ComboBoxButtonGradientBegin
		{
			get
			{
				return this.MenuItemPressedGradientBegin;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000E4290 File Offset: 0x000E2490
		internal Color ComboBoxButtonGradientEnd
		{
			get
			{
				return this.MenuItemPressedGradientEnd;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000E4298 File Offset: 0x000E2498
		internal Color ComboBoxButtonSelectedGradientBegin
		{
			get
			{
				return this.MenuItemSelectedGradientBegin;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000E42A0 File Offset: 0x000E24A0
		internal Color ComboBoxButtonSelectedGradientEnd
		{
			get
			{
				return this.MenuItemSelectedGradientEnd;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000E42A8 File Offset: 0x000E24A8
		internal Color ComboBoxButtonPressedGradientBegin
		{
			get
			{
				return this.ButtonPressedGradientBegin;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000E42B0 File Offset: 0x000E24B0
		internal Color ComboBoxButtonPressedGradientEnd
		{
			get
			{
				return this.ButtonPressedGradientEnd;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x000E42B8 File Offset: 0x000E24B8
		internal Color ComboBoxButtonOnOverflow
		{
			get
			{
				return this.ToolStripDropDownBackground;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000E42C0 File Offset: 0x000E24C0
		internal Color ComboBoxBorder
		{
			get
			{
				return this.ButtonSelectedHighlightBorder;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000E42C0 File Offset: 0x000E24C0
		internal Color TextBoxBorder
		{
			get
			{
				return this.ButtonSelectedHighlightBorder;
			}
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000E42C8 File Offset: 0x000E24C8
		private static Color GetAlphaBlendedColor(Graphics g, Color src, Color dest, int alpha)
		{
			int red = ((int)src.R * alpha + (255 - alpha) * (int)dest.R) / 255;
			int green = ((int)src.G * alpha + (255 - alpha) * (int)dest.G) / 255;
			int blue = ((int)src.B * alpha + (255 - alpha) * (int)dest.B) / 255;
			int alpha2 = ((int)src.A * alpha + (255 - alpha) * (int)dest.A) / 255;
			if (g == null)
			{
				return Color.FromArgb(alpha2, red, green, blue);
			}
			return g.GetNearestColor(Color.FromArgb(alpha2, red, green, blue));
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000E4374 File Offset: 0x000E2574
		private static Color GetAlphaBlendedColorHighRes(Graphics graphics, Color src, Color dest, int alpha)
		{
			int num;
			int num2;
			if (alpha < 100)
			{
				num = 100 - alpha;
				num2 = 100;
			}
			else
			{
				num = 1000 - alpha;
				num2 = 1000;
			}
			int red = (alpha * (int)src.R + num * (int)dest.R + num2 / 2) / num2;
			int green = (alpha * (int)src.G + num * (int)dest.G + num2 / 2) / num2;
			int blue = (alpha * (int)src.B + num * (int)dest.B + num2 / 2) / num2;
			if (graphics == null)
			{
				return Color.FromArgb(red, green, blue);
			}
			return graphics.GetNearestColor(Color.FromArgb(red, green, blue));
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000E4414 File Offset: 0x000E2614
		private void InitCommonColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			if (!DisplayInformation.LowResolution)
			{
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					rgbTable[ProfessionalColorTable.KnownColors.ButtonPressedHighlight] = ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Window, ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Highlight, SystemColors.Window, 160), 50);
					rgbTable[ProfessionalColorTable.KnownColors.ButtonCheckedHighlight] = ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Window, ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Highlight, SystemColors.Window, 80), 20);
					rgbTable[ProfessionalColorTable.KnownColors.ButtonSelectedHighlight] = rgbTable[ProfessionalColorTable.KnownColors.ButtonCheckedHighlight];
					return;
				}
			}
			rgbTable[ProfessionalColorTable.KnownColors.ButtonPressedHighlight] = SystemColors.Highlight;
			rgbTable[ProfessionalColorTable.KnownColors.ButtonCheckedHighlight] = SystemColors.ControlLight;
			rgbTable[ProfessionalColorTable.KnownColors.ButtonSelectedHighlight] = SystemColors.ControlLight;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000E44F4 File Offset: 0x000E26F4
		internal void InitSystemColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			this.usingSystemColors = true;
			this.InitCommonColors(ref rgbTable);
			Color buttonFace = SystemColors.ButtonFace;
			Color buttonShadow = SystemColors.ButtonShadow;
			Color highlight = SystemColors.Highlight;
			Color window = SystemColors.Window;
			Color empty = Color.Empty;
			Color controlText = SystemColors.ControlText;
			Color buttonHighlight = SystemColors.ButtonHighlight;
			Color grayText = SystemColors.GrayText;
			Color highlightText = SystemColors.HighlightText;
			Color windowText = SystemColors.WindowText;
			Color value = buttonFace;
			Color value2 = buttonFace;
			Color value3 = buttonFace;
			Color value4 = highlight;
			Color value5 = highlight;
			bool lowResolution = DisplayInformation.LowResolution;
			bool highContrast = DisplayInformation.HighContrast;
			if (lowResolution)
			{
				value4 = window;
			}
			else if (!highContrast)
			{
				value = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 23);
				value2 = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 50);
				value3 = SystemColors.ButtonFace;
				value4 = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, highlight, window, 30);
				value5 = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, highlight, window, 50);
			}
			if (lowResolution || highContrast)
			{
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = buttonFace;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = SystemColors.ControlLight;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = controlText;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = buttonFace;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = controlText;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = window;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = buttonShadow;
			}
			else
			{
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, window, buttonFace, 165);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, highlight, window, 50);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonShadow, window, 75);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 205);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 70);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 90);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 40);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 70);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 90);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, controlText, buttonShadow, 20);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 143);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonShadow, window, 70);
			}
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = (lowResolution ? SystemColors.ControlLight : highlight);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = value4;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = value4;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = value4;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = value;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = value2;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = value3;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = value5;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = value5;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = value5;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = value;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = value2;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd];
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = SystemColors.MenuText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = SystemColors.MenuText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = SystemColors.MenuText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = SystemColors.InactiveCaption;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = SystemColors.InactiveCaptionText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = SystemColors.Info;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = SystemColors.InfoText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = buttonFace;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000E511C File Offset: 0x000E331C
		internal void InitOliveLunaColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(81, 94, 51);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(81, 94, 51);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(209, 222, 173);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(209, 222, 173);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(96, 119, 66);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(81, 94, 51);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(217, 217, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(230, 230, 209);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(160, 177, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(186, 201, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(237, 240, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(181, 196, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(255, 177, 109);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(255, 203, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(186, 204, 150);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(96, 119, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(141, 160, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(255, 193, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(254, 140, 73);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(255, 221, 152);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(255, 184, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(255, 166, 76);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(255, 195, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(255, 255, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(181, 196, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(206, 220, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(131, 144, 113);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(243, 244, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(159, 174, 122);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(117, 141, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(244, 244, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(216, 227, 182);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(173, 181, 157);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(173, 181, 157);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(134, 148, 108);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(244, 247, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(197, 212, 159);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(220, 224, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(153, 84, 10);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(96, 119, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(176, 194, 140);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(183, 198, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(183, 198, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(191, 191, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(126, 125, 104);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(159, 171, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(159, 171, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(217, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(230, 234, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(150, 145, 133);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(161, 176, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(210, 223, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(90, 107, 70);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(90, 107, 70);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(243, 242, 231);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(217, 217, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(217, 217, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(255, 255, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(226, 231, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(171, 192, 138);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(117, 141, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(242, 240, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(96, 119, 66);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(175, 192, 130);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(234, 233, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(181, 196, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(253, 238, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(175, 186, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(115, 137, 84);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(253, 247, 233);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(187, 85, 3);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(251, 200, 79);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(200, 212, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(176, 191, 138);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(234, 240, 207);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(234, 240, 207);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(247, 190, 87);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 255, 220);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(50, 69, 105);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(248, 222, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(232, 127, 8);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(238, 147, 17);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(251, 230, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(64, 81, 59);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(120, 142, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(120, 142, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(242, 240, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(206, 220, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(107, 129, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(107, 129, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(107, 129, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(193, 198, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(249, 249, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(237, 242, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(191, 206, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(243, 242, 231);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(164, 185, 127);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(197, 212, 159);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(222, 222, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(188, 187, 177);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 255, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(216, 227, 182);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(188, 205, 131);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(217, 217, 167);
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000E6A1C File Offset: 0x000E4C1C
		internal void InitSilverLunaColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(173, 174, 193);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(219, 218, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(219, 218, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(84, 84, 117);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(215, 215, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(118, 116, 151);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(184, 185, 202);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(232, 233, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(172, 170, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(255, 177, 109);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(255, 203, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(186, 185, 206);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(118, 116, 146);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(156, 155, 180);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(255, 193, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(254, 140, 73);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(255, 221, 152);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(255, 184, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(255, 166, 76);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(255, 195, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(249, 249, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(147, 145, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(225, 226, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(247, 245, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(168, 167, 190);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(198, 200, 215);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(253, 250, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(214, 211, 231);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(185, 187, 200);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(185, 187, 200);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(154, 140, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(192, 192, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(59, 59, 63);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(7, 70, 213);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(118, 116, 146);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(186, 185, 206);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(148, 148, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(148, 148, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(171, 169, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(171, 169, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(191, 191, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(126, 125, 104);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(223, 223, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(162, 162, 181);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(162, 162, 181);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(212, 213, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(227, 227, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(150, 145, 133);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(169, 168, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(208, 208, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(92, 91, 121);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(92, 91, 121);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(238, 238, 244);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(249, 249, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(223, 223, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(177, 176, 195);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(239, 239, 244);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(168, 167, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(234, 233, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(165, 164, 189);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(253, 238, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(229, 229, 235);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(112, 111, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(253, 247, 233);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(187, 85, 3);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(251, 200, 79);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(204, 206, 219);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(147, 145, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(225, 226, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(225, 226, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(247, 190, 87);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 255, 220);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(50, 69, 105);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(248, 222, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(232, 127, 8);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(238, 147, 17);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(251, 230, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(168, 167, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(168, 167, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(142, 142, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(142, 142, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(142, 142, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(195, 195, 210);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(236, 234, 218);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(247, 247, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(239, 239, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(179, 178, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(238, 238, 244);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(165, 172, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(192, 192, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(222, 222, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(161, 160, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 255, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(184, 188, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(198, 198, 217);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(215, 215, 229);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000E830C File Offset: 0x000E650C
		private void InitRoyaleColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(189, 188, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(193, 193, 196);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(142, 141, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(176, 175, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(252, 252, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(193, 193, 196);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(134, 133, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(228, 226, 230);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(193, 193, 196);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(176, 175, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(251, 250, 251);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(252, 252, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(242, 242, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(224, 224, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(252, 252, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(247, 246, 248);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(241, 240, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(228, 226, 230);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(237, 235, 239);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(155, 154, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(188, 202, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(118, 116, 146);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(186, 185, 206);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(193, 210, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(154, 183, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(246, 244, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(179, 178, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(152, 181, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(193, 210, 238);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000E99C4 File Offset: 0x000E7BC4
		internal void InitThemedColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			string colorScheme = VisualStyleInformation.ColorScheme;
			string fileName = Path.GetFileName(VisualStyleInformation.ThemeFilename);
			bool flag = false;
			if (string.Equals("luna.msstyles", fileName, StringComparison.OrdinalIgnoreCase))
			{
				if (colorScheme == "NormalColor")
				{
					this.InitBlueLunaColors(ref rgbTable);
					this.usingSystemColors = false;
					flag = true;
				}
				else if (colorScheme == "HomeStead")
				{
					this.InitOliveLunaColors(ref rgbTable);
					this.usingSystemColors = false;
					flag = true;
				}
				else if (colorScheme == "Metallic")
				{
					this.InitSilverLunaColors(ref rgbTable);
					this.usingSystemColors = false;
					flag = true;
				}
			}
			else if (string.Equals("aero.msstyles", fileName, StringComparison.OrdinalIgnoreCase))
			{
				this.InitSystemColors(ref rgbTable);
				this.usingSystemColors = true;
				flag = true;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = rgbTable[ProfessionalColorTable.KnownColors.ButtonSelectedHighlight];
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver];
			}
			else if (string.Equals("royale.msstyles", fileName, StringComparison.OrdinalIgnoreCase) && (colorScheme == "NormalColor" || colorScheme == "Royale"))
			{
				this.InitRoyaleColors(ref rgbTable);
				this.usingSystemColors = false;
				flag = true;
			}
			if (!flag)
			{
				this.InitSystemColors(ref rgbTable);
				this.usingSystemColors = true;
			}
			this.InitCommonColors(ref rgbTable);
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000E9AF0 File Offset: 0x000E7CF0
		internal void InitBlueLunaColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(196, 205, 218);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(196, 205, 218);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(42, 102, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(196, 219, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(196, 219, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(39, 65, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(203, 221, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(114, 155, 215);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(161, 197, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(227, 239, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(255, 177, 109);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(255, 203, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(127, 177, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(82, 127, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(255, 193, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(254, 140, 73);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(255, 221, 152);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(255, 184, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(255, 166, 76);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(255, 195, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(227, 239, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(97, 122, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(233, 236, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(109, 150, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(153, 204, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(0, 45, 150);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(246, 246, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(172, 183, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(172, 183, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(95, 130, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(106, 140, 203);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(241, 249, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(169, 199, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(42, 102, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(187, 206, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(0, 70, 213);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(0, 53, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(117, 166, 241);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(94, 94, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(94, 94, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(129, 169, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(129, 169, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(89, 89, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(126, 125, 104);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(191, 191, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(74, 122, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(74, 122, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(185, 208, 241);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(221, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(150, 145, 133);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(101, 143, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(196, 219, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(0, 45, 134);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(0, 45, 134);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(221, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(227, 239, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(216, 231, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(75, 120, 202);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(242, 240, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(89, 135, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(234, 233, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(253, 238, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(190, 218, 251);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(55, 104, 185);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(253, 247, 233);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(187, 85, 3);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(251, 200, 79);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(215, 228, 251);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(0, 45, 150);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(247, 190, 87);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 255, 220);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(50, 69, 105);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(248, 222, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(232, 127, 8);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(238, 147, 17);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(251, 230, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(89, 135, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(89, 135, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(195, 218, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(61, 108, 192);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(61, 108, 192);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(61, 108, 192);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(189, 194, 207);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(251, 251, 248);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(220, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(167, 197, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(185, 212, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(42, 102, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(221, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(127, 157, 185);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(169, 199, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(222, 222, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 255, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(74, 122, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(148, 187, 239);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(158, 190, 245);
		}

		// Token: 0x04001EC6 RID: 7878
		private Dictionary<ProfessionalColorTable.KnownColors, Color> professionalRGB;

		// Token: 0x04001EC7 RID: 7879
		private bool usingSystemColors;

		// Token: 0x04001EC8 RID: 7880
		private bool useSystemColors;

		// Token: 0x04001EC9 RID: 7881
		private string lastKnownColorScheme = string.Empty;

		// Token: 0x04001ECA RID: 7882
		private const string oliveColorScheme = "HomeStead";

		// Token: 0x04001ECB RID: 7883
		private const string normalColorScheme = "NormalColor";

		// Token: 0x04001ECC RID: 7884
		private const string silverColorScheme = "Metallic";

		// Token: 0x04001ECD RID: 7885
		private const string royaleColorScheme = "Royale";

		// Token: 0x04001ECE RID: 7886
		private const string lunaFileName = "luna.msstyles";

		// Token: 0x04001ECF RID: 7887
		private const string royaleFileName = "royale.msstyles";

		// Token: 0x04001ED0 RID: 7888
		private const string aeroFileName = "aero.msstyles";

		// Token: 0x04001ED1 RID: 7889
		private object colorFreshnessKey;

		// Token: 0x020007CC RID: 1996
		internal enum KnownColors
		{
			// Token: 0x040041CB RID: 16843
			msocbvcrCBBdrOuterDocked,
			// Token: 0x040041CC RID: 16844
			msocbvcrCBBdrOuterFloating,
			// Token: 0x040041CD RID: 16845
			msocbvcrCBBkgd,
			// Token: 0x040041CE RID: 16846
			msocbvcrCBCtlBdrMouseDown,
			// Token: 0x040041CF RID: 16847
			msocbvcrCBCtlBdrMouseOver,
			// Token: 0x040041D0 RID: 16848
			msocbvcrCBCtlBdrSelected,
			// Token: 0x040041D1 RID: 16849
			msocbvcrCBCtlBdrSelectedMouseOver,
			// Token: 0x040041D2 RID: 16850
			msocbvcrCBCtlBkgd,
			// Token: 0x040041D3 RID: 16851
			msocbvcrCBCtlBkgdLight,
			// Token: 0x040041D4 RID: 16852
			msocbvcrCBCtlBkgdMouseDown,
			// Token: 0x040041D5 RID: 16853
			msocbvcrCBCtlBkgdMouseOver,
			// Token: 0x040041D6 RID: 16854
			msocbvcrCBCtlBkgdSelected,
			// Token: 0x040041D7 RID: 16855
			msocbvcrCBCtlBkgdSelectedMouseOver,
			// Token: 0x040041D8 RID: 16856
			msocbvcrCBCtlText,
			// Token: 0x040041D9 RID: 16857
			msocbvcrCBCtlTextDisabled,
			// Token: 0x040041DA RID: 16858
			msocbvcrCBCtlTextLight,
			// Token: 0x040041DB RID: 16859
			msocbvcrCBCtlTextMouseDown,
			// Token: 0x040041DC RID: 16860
			msocbvcrCBCtlTextMouseOver,
			// Token: 0x040041DD RID: 16861
			msocbvcrCBDockSeparatorLine,
			// Token: 0x040041DE RID: 16862
			msocbvcrCBDragHandle,
			// Token: 0x040041DF RID: 16863
			msocbvcrCBDragHandleShadow,
			// Token: 0x040041E0 RID: 16864
			msocbvcrCBDropDownArrow,
			// Token: 0x040041E1 RID: 16865
			msocbvcrCBGradMainMenuHorzBegin,
			// Token: 0x040041E2 RID: 16866
			msocbvcrCBGradMainMenuHorzEnd,
			// Token: 0x040041E3 RID: 16867
			msocbvcrCBGradMenuIconBkgdDroppedBegin,
			// Token: 0x040041E4 RID: 16868
			msocbvcrCBGradMenuIconBkgdDroppedEnd,
			// Token: 0x040041E5 RID: 16869
			msocbvcrCBGradMenuIconBkgdDroppedMiddle,
			// Token: 0x040041E6 RID: 16870
			msocbvcrCBGradMenuTitleBkgdBegin,
			// Token: 0x040041E7 RID: 16871
			msocbvcrCBGradMenuTitleBkgdEnd,
			// Token: 0x040041E8 RID: 16872
			msocbvcrCBGradMouseDownBegin,
			// Token: 0x040041E9 RID: 16873
			msocbvcrCBGradMouseDownEnd,
			// Token: 0x040041EA RID: 16874
			msocbvcrCBGradMouseDownMiddle,
			// Token: 0x040041EB RID: 16875
			msocbvcrCBGradMouseOverBegin,
			// Token: 0x040041EC RID: 16876
			msocbvcrCBGradMouseOverEnd,
			// Token: 0x040041ED RID: 16877
			msocbvcrCBGradMouseOverMiddle,
			// Token: 0x040041EE RID: 16878
			msocbvcrCBGradOptionsBegin,
			// Token: 0x040041EF RID: 16879
			msocbvcrCBGradOptionsEnd,
			// Token: 0x040041F0 RID: 16880
			msocbvcrCBGradOptionsMiddle,
			// Token: 0x040041F1 RID: 16881
			msocbvcrCBGradOptionsMouseOverBegin,
			// Token: 0x040041F2 RID: 16882
			msocbvcrCBGradOptionsMouseOverEnd,
			// Token: 0x040041F3 RID: 16883
			msocbvcrCBGradOptionsMouseOverMiddle,
			// Token: 0x040041F4 RID: 16884
			msocbvcrCBGradOptionsSelectedBegin,
			// Token: 0x040041F5 RID: 16885
			msocbvcrCBGradOptionsSelectedEnd,
			// Token: 0x040041F6 RID: 16886
			msocbvcrCBGradOptionsSelectedMiddle,
			// Token: 0x040041F7 RID: 16887
			msocbvcrCBGradSelectedBegin,
			// Token: 0x040041F8 RID: 16888
			msocbvcrCBGradSelectedEnd,
			// Token: 0x040041F9 RID: 16889
			msocbvcrCBGradSelectedMiddle,
			// Token: 0x040041FA RID: 16890
			msocbvcrCBGradVertBegin,
			// Token: 0x040041FB RID: 16891
			msocbvcrCBGradVertEnd,
			// Token: 0x040041FC RID: 16892
			msocbvcrCBGradVertMiddle,
			// Token: 0x040041FD RID: 16893
			msocbvcrCBIconDisabledDark,
			// Token: 0x040041FE RID: 16894
			msocbvcrCBIconDisabledLight,
			// Token: 0x040041FF RID: 16895
			msocbvcrCBLabelBkgnd,
			// Token: 0x04004200 RID: 16896
			msocbvcrCBLowColorIconDisabled,
			// Token: 0x04004201 RID: 16897
			msocbvcrCBMainMenuBkgd,
			// Token: 0x04004202 RID: 16898
			msocbvcrCBMenuBdrOuter,
			// Token: 0x04004203 RID: 16899
			msocbvcrCBMenuBkgd,
			// Token: 0x04004204 RID: 16900
			msocbvcrCBMenuCtlText,
			// Token: 0x04004205 RID: 16901
			msocbvcrCBMenuCtlTextDisabled,
			// Token: 0x04004206 RID: 16902
			msocbvcrCBMenuIconBkgd,
			// Token: 0x04004207 RID: 16903
			msocbvcrCBMenuIconBkgdDropped,
			// Token: 0x04004208 RID: 16904
			msocbvcrCBMenuShadow,
			// Token: 0x04004209 RID: 16905
			msocbvcrCBMenuSplitArrow,
			// Token: 0x0400420A RID: 16906
			msocbvcrCBOptionsButtonShadow,
			// Token: 0x0400420B RID: 16907
			msocbvcrCBShadow,
			// Token: 0x0400420C RID: 16908
			msocbvcrCBSplitterLine,
			// Token: 0x0400420D RID: 16909
			msocbvcrCBSplitterLineLight,
			// Token: 0x0400420E RID: 16910
			msocbvcrCBTearOffHandle,
			// Token: 0x0400420F RID: 16911
			msocbvcrCBTearOffHandleMouseOver,
			// Token: 0x04004210 RID: 16912
			msocbvcrCBTitleBkgd,
			// Token: 0x04004211 RID: 16913
			msocbvcrCBTitleText,
			// Token: 0x04004212 RID: 16914
			msocbvcrDisabledFocuslessHighlightedText,
			// Token: 0x04004213 RID: 16915
			msocbvcrDisabledHighlightedText,
			// Token: 0x04004214 RID: 16916
			msocbvcrDlgGroupBoxText,
			// Token: 0x04004215 RID: 16917
			msocbvcrDocTabBdr,
			// Token: 0x04004216 RID: 16918
			msocbvcrDocTabBdrDark,
			// Token: 0x04004217 RID: 16919
			msocbvcrDocTabBdrDarkMouseDown,
			// Token: 0x04004218 RID: 16920
			msocbvcrDocTabBdrDarkMouseOver,
			// Token: 0x04004219 RID: 16921
			msocbvcrDocTabBdrLight,
			// Token: 0x0400421A RID: 16922
			msocbvcrDocTabBdrLightMouseDown,
			// Token: 0x0400421B RID: 16923
			msocbvcrDocTabBdrLightMouseOver,
			// Token: 0x0400421C RID: 16924
			msocbvcrDocTabBdrMouseDown,
			// Token: 0x0400421D RID: 16925
			msocbvcrDocTabBdrMouseOver,
			// Token: 0x0400421E RID: 16926
			msocbvcrDocTabBdrSelected,
			// Token: 0x0400421F RID: 16927
			msocbvcrDocTabBkgd,
			// Token: 0x04004220 RID: 16928
			msocbvcrDocTabBkgdMouseDown,
			// Token: 0x04004221 RID: 16929
			msocbvcrDocTabBkgdMouseOver,
			// Token: 0x04004222 RID: 16930
			msocbvcrDocTabBkgdSelected,
			// Token: 0x04004223 RID: 16931
			msocbvcrDocTabText,
			// Token: 0x04004224 RID: 16932
			msocbvcrDocTabTextMouseDown,
			// Token: 0x04004225 RID: 16933
			msocbvcrDocTabTextMouseOver,
			// Token: 0x04004226 RID: 16934
			msocbvcrDocTabTextSelected,
			// Token: 0x04004227 RID: 16935
			msocbvcrDWActiveTabBkgd,
			// Token: 0x04004228 RID: 16936
			msocbvcrDWActiveTabText,
			// Token: 0x04004229 RID: 16937
			msocbvcrDWActiveTabTextDisabled,
			// Token: 0x0400422A RID: 16938
			msocbvcrDWInactiveTabBkgd,
			// Token: 0x0400422B RID: 16939
			msocbvcrDWInactiveTabText,
			// Token: 0x0400422C RID: 16940
			msocbvcrDWTabBkgdMouseDown,
			// Token: 0x0400422D RID: 16941
			msocbvcrDWTabBkgdMouseOver,
			// Token: 0x0400422E RID: 16942
			msocbvcrDWTabTextMouseDown,
			// Token: 0x0400422F RID: 16943
			msocbvcrDWTabTextMouseOver,
			// Token: 0x04004230 RID: 16944
			msocbvcrFocuslessHighlightedBkgd,
			// Token: 0x04004231 RID: 16945
			msocbvcrFocuslessHighlightedText,
			// Token: 0x04004232 RID: 16946
			msocbvcrGDHeaderBdr,
			// Token: 0x04004233 RID: 16947
			msocbvcrGDHeaderBkgd,
			// Token: 0x04004234 RID: 16948
			msocbvcrGDHeaderCellBdr,
			// Token: 0x04004235 RID: 16949
			msocbvcrGDHeaderCellBkgd,
			// Token: 0x04004236 RID: 16950
			msocbvcrGDHeaderCellBkgdSelected,
			// Token: 0x04004237 RID: 16951
			msocbvcrGDHeaderSeeThroughSelection,
			// Token: 0x04004238 RID: 16952
			msocbvcrGSPDarkBkgd,
			// Token: 0x04004239 RID: 16953
			msocbvcrGSPGroupContentDarkBkgd,
			// Token: 0x0400423A RID: 16954
			msocbvcrGSPGroupContentLightBkgd,
			// Token: 0x0400423B RID: 16955
			msocbvcrGSPGroupContentText,
			// Token: 0x0400423C RID: 16956
			msocbvcrGSPGroupContentTextDisabled,
			// Token: 0x0400423D RID: 16957
			msocbvcrGSPGroupHeaderDarkBkgd,
			// Token: 0x0400423E RID: 16958
			msocbvcrGSPGroupHeaderLightBkgd,
			// Token: 0x0400423F RID: 16959
			msocbvcrGSPGroupHeaderText,
			// Token: 0x04004240 RID: 16960
			msocbvcrGSPGroupline,
			// Token: 0x04004241 RID: 16961
			msocbvcrGSPHyperlink,
			// Token: 0x04004242 RID: 16962
			msocbvcrGSPLightBkgd,
			// Token: 0x04004243 RID: 16963
			msocbvcrHyperlink,
			// Token: 0x04004244 RID: 16964
			msocbvcrHyperlinkFollowed,
			// Token: 0x04004245 RID: 16965
			msocbvcrJotNavUIBdr,
			// Token: 0x04004246 RID: 16966
			msocbvcrJotNavUIGradBegin,
			// Token: 0x04004247 RID: 16967
			msocbvcrJotNavUIGradEnd,
			// Token: 0x04004248 RID: 16968
			msocbvcrJotNavUIGradMiddle,
			// Token: 0x04004249 RID: 16969
			msocbvcrJotNavUIText,
			// Token: 0x0400424A RID: 16970
			msocbvcrListHeaderArrow,
			// Token: 0x0400424B RID: 16971
			msocbvcrNetLookBkgnd,
			// Token: 0x0400424C RID: 16972
			msocbvcrOABBkgd,
			// Token: 0x0400424D RID: 16973
			msocbvcrOBBkgdBdr,
			// Token: 0x0400424E RID: 16974
			msocbvcrOBBkgdBdrContrast,
			// Token: 0x0400424F RID: 16975
			msocbvcrOGMDIParentWorkspaceBkgd,
			// Token: 0x04004250 RID: 16976
			msocbvcrOGRulerActiveBkgd,
			// Token: 0x04004251 RID: 16977
			msocbvcrOGRulerBdr,
			// Token: 0x04004252 RID: 16978
			msocbvcrOGRulerBkgd,
			// Token: 0x04004253 RID: 16979
			msocbvcrOGRulerInactiveBkgd,
			// Token: 0x04004254 RID: 16980
			msocbvcrOGRulerTabBoxBdr,
			// Token: 0x04004255 RID: 16981
			msocbvcrOGRulerTabBoxBdrHighlight,
			// Token: 0x04004256 RID: 16982
			msocbvcrOGRulerTabStopTicks,
			// Token: 0x04004257 RID: 16983
			msocbvcrOGRulerText,
			// Token: 0x04004258 RID: 16984
			msocbvcrOGTaskPaneGroupBoxHeaderBkgd,
			// Token: 0x04004259 RID: 16985
			msocbvcrOGWorkspaceBkgd,
			// Token: 0x0400425A RID: 16986
			msocbvcrOLKFlagNone,
			// Token: 0x0400425B RID: 16987
			msocbvcrOLKFolderbarDark,
			// Token: 0x0400425C RID: 16988
			msocbvcrOLKFolderbarLight,
			// Token: 0x0400425D RID: 16989
			msocbvcrOLKFolderbarText,
			// Token: 0x0400425E RID: 16990
			msocbvcrOLKGridlines,
			// Token: 0x0400425F RID: 16991
			msocbvcrOLKGroupLine,
			// Token: 0x04004260 RID: 16992
			msocbvcrOLKGroupNested,
			// Token: 0x04004261 RID: 16993
			msocbvcrOLKGroupShaded,
			// Token: 0x04004262 RID: 16994
			msocbvcrOLKGroupText,
			// Token: 0x04004263 RID: 16995
			msocbvcrOLKIconBar,
			// Token: 0x04004264 RID: 16996
			msocbvcrOLKInfoBarBkgd,
			// Token: 0x04004265 RID: 16997
			msocbvcrOLKInfoBarText,
			// Token: 0x04004266 RID: 16998
			msocbvcrOLKPreviewPaneLabelText,
			// Token: 0x04004267 RID: 16999
			msocbvcrOLKTodayIndicatorDark,
			// Token: 0x04004268 RID: 17000
			msocbvcrOLKTodayIndicatorLight,
			// Token: 0x04004269 RID: 17001
			msocbvcrOLKWBActionDividerLine,
			// Token: 0x0400426A RID: 17002
			msocbvcrOLKWBButtonDark,
			// Token: 0x0400426B RID: 17003
			msocbvcrOLKWBButtonLight,
			// Token: 0x0400426C RID: 17004
			msocbvcrOLKWBDarkOutline,
			// Token: 0x0400426D RID: 17005
			msocbvcrOLKWBFoldersBackground,
			// Token: 0x0400426E RID: 17006
			msocbvcrOLKWBHoverButtonDark,
			// Token: 0x0400426F RID: 17007
			msocbvcrOLKWBHoverButtonLight,
			// Token: 0x04004270 RID: 17008
			msocbvcrOLKWBLabelText,
			// Token: 0x04004271 RID: 17009
			msocbvcrOLKWBPressedButtonDark,
			// Token: 0x04004272 RID: 17010
			msocbvcrOLKWBPressedButtonLight,
			// Token: 0x04004273 RID: 17011
			msocbvcrOLKWBSelectedButtonDark,
			// Token: 0x04004274 RID: 17012
			msocbvcrOLKWBSelectedButtonLight,
			// Token: 0x04004275 RID: 17013
			msocbvcrOLKWBSplitterDark,
			// Token: 0x04004276 RID: 17014
			msocbvcrOLKWBSplitterLight,
			// Token: 0x04004277 RID: 17015
			msocbvcrPlacesBarBkgd,
			// Token: 0x04004278 RID: 17016
			msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd,
			// Token: 0x04004279 RID: 17017
			msocbvcrPPOutlineThumbnailsPaneTabBdr,
			// Token: 0x0400427A RID: 17018
			msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd,
			// Token: 0x0400427B RID: 17019
			msocbvcrPPOutlineThumbnailsPaneTabText,
			// Token: 0x0400427C RID: 17020
			msocbvcrPPSlideBdrActiveSelected,
			// Token: 0x0400427D RID: 17021
			msocbvcrPPSlideBdrActiveSelectedMouseOver,
			// Token: 0x0400427E RID: 17022
			msocbvcrPPSlideBdrInactiveSelected,
			// Token: 0x0400427F RID: 17023
			msocbvcrPPSlideBdrMouseOver,
			// Token: 0x04004280 RID: 17024
			msocbvcrPubPrintDocScratchPageBkgd,
			// Token: 0x04004281 RID: 17025
			msocbvcrPubWebDocScratchPageBkgd,
			// Token: 0x04004282 RID: 17026
			msocbvcrSBBdr,
			// Token: 0x04004283 RID: 17027
			msocbvcrScrollbarBkgd,
			// Token: 0x04004284 RID: 17028
			msocbvcrToastGradBegin,
			// Token: 0x04004285 RID: 17029
			msocbvcrToastGradEnd,
			// Token: 0x04004286 RID: 17030
			msocbvcrWPBdrInnerDocked,
			// Token: 0x04004287 RID: 17031
			msocbvcrWPBdrOuterDocked,
			// Token: 0x04004288 RID: 17032
			msocbvcrWPBdrOuterFloating,
			// Token: 0x04004289 RID: 17033
			msocbvcrWPBkgd,
			// Token: 0x0400428A RID: 17034
			msocbvcrWPCtlBdr,
			// Token: 0x0400428B RID: 17035
			msocbvcrWPCtlBdrDefault,
			// Token: 0x0400428C RID: 17036
			msocbvcrWPCtlBdrDisabled,
			// Token: 0x0400428D RID: 17037
			msocbvcrWPCtlBkgd,
			// Token: 0x0400428E RID: 17038
			msocbvcrWPCtlBkgdDisabled,
			// Token: 0x0400428F RID: 17039
			msocbvcrWPCtlText,
			// Token: 0x04004290 RID: 17040
			msocbvcrWPCtlTextDisabled,
			// Token: 0x04004291 RID: 17041
			msocbvcrWPCtlTextMouseDown,
			// Token: 0x04004292 RID: 17042
			msocbvcrWPGroupline,
			// Token: 0x04004293 RID: 17043
			msocbvcrWPInfoTipBkgd,
			// Token: 0x04004294 RID: 17044
			msocbvcrWPInfoTipText,
			// Token: 0x04004295 RID: 17045
			msocbvcrWPNavBarBkgnd,
			// Token: 0x04004296 RID: 17046
			msocbvcrWPText,
			// Token: 0x04004297 RID: 17047
			msocbvcrWPTextDisabled,
			// Token: 0x04004298 RID: 17048
			msocbvcrWPTitleBkgdActive,
			// Token: 0x04004299 RID: 17049
			msocbvcrWPTitleBkgdInactive,
			// Token: 0x0400429A RID: 17050
			msocbvcrWPTitleTextActive,
			// Token: 0x0400429B RID: 17051
			msocbvcrWPTitleTextInactive,
			// Token: 0x0400429C RID: 17052
			msocbvcrXLFormulaBarBkgd,
			// Token: 0x0400429D RID: 17053
			ButtonSelectedHighlight,
			// Token: 0x0400429E RID: 17054
			ButtonPressedHighlight,
			// Token: 0x0400429F RID: 17055
			ButtonCheckedHighlight,
			// Token: 0x040042A0 RID: 17056
			lastKnownColor = 212
		}
	}
}

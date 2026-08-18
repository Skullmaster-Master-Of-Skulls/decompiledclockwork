using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B43 RID: 6979
	[DataContract]
	[Serializable]
	public class RadMenuItemData : ControlItemData
	{
		// Token: 0x06010DF8 RID: 69112 RVA: 0x003BE0B4 File Offset: 0x003BC2B4
		public RadMenuItemData()
		{
			this.ExpandMode = MenuItemExpandMode.ClientSide;
			this.Selected = false;
			this.NavigateUrl = string.Empty;
			this.PostBack = true;
			this.Target = string.Empty;
			this.IsSeparator = false;
			this.CssClass = string.Empty;
			this.DisabledCssClass = "rmDisabled";
			this.ExpandedCssClass = "rmExpanded";
			this.FocusedCssClass = "rmFocused";
			this.ClickedCssClass = "rmClicked";
			this.ImageUrl = string.Empty;
			this.HoveredImageUrl = string.Empty;
			this.ClickedImageUrl = string.Empty;
			this.DisabledImageUrl = string.Empty;
			this.ExpandedImageUrl = string.Empty;
		}

		// Token: 0x17005247 RID: 21063
		// (get) Token: 0x06010DF9 RID: 69113 RVA: 0x003BE167 File Offset: 0x003BC367
		// (set) Token: 0x06010DFA RID: 69114 RVA: 0x003BE16F File Offset: 0x003BC36F
		[DataMember]
		public MenuItemExpandMode ExpandMode { get; set; }

		// Token: 0x17005248 RID: 21064
		// (get) Token: 0x06010DFB RID: 69115 RVA: 0x003BE178 File Offset: 0x003BC378
		// (set) Token: 0x06010DFC RID: 69116 RVA: 0x003BE180 File Offset: 0x003BC380
		[DataMember]
		public bool Selected { get; set; }

		// Token: 0x17005249 RID: 21065
		// (get) Token: 0x06010DFD RID: 69117 RVA: 0x003BE189 File Offset: 0x003BC389
		// (set) Token: 0x06010DFE RID: 69118 RVA: 0x003BE191 File Offset: 0x003BC391
		[DataMember]
		public string NavigateUrl { get; set; }

		// Token: 0x1700524A RID: 21066
		// (get) Token: 0x06010DFF RID: 69119 RVA: 0x003BE19A File Offset: 0x003BC39A
		// (set) Token: 0x06010E00 RID: 69120 RVA: 0x003BE1A2 File Offset: 0x003BC3A2
		[DataMember]
		public bool PostBack { get; set; }

		// Token: 0x1700524B RID: 21067
		// (get) Token: 0x06010E01 RID: 69121 RVA: 0x003BE1AB File Offset: 0x003BC3AB
		// (set) Token: 0x06010E02 RID: 69122 RVA: 0x003BE1B3 File Offset: 0x003BC3B3
		[DataMember]
		public string Target { get; set; }

		// Token: 0x1700524C RID: 21068
		// (get) Token: 0x06010E03 RID: 69123 RVA: 0x003BE1BC File Offset: 0x003BC3BC
		// (set) Token: 0x06010E04 RID: 69124 RVA: 0x003BE1C4 File Offset: 0x003BC3C4
		[DataMember]
		public bool IsSeparator { get; set; }

		// Token: 0x1700524D RID: 21069
		// (get) Token: 0x06010E05 RID: 69125 RVA: 0x003BE1CD File Offset: 0x003BC3CD
		// (set) Token: 0x06010E06 RID: 69126 RVA: 0x003BE1D5 File Offset: 0x003BC3D5
		[DataMember]
		public string CssClass { get; set; }

		// Token: 0x1700524E RID: 21070
		// (get) Token: 0x06010E07 RID: 69127 RVA: 0x003BE1DE File Offset: 0x003BC3DE
		// (set) Token: 0x06010E08 RID: 69128 RVA: 0x003BE1E6 File Offset: 0x003BC3E6
		[DataMember]
		public string DisabledCssClass { get; set; }

		// Token: 0x1700524F RID: 21071
		// (get) Token: 0x06010E09 RID: 69129 RVA: 0x003BE1EF File Offset: 0x003BC3EF
		// (set) Token: 0x06010E0A RID: 69130 RVA: 0x003BE1F7 File Offset: 0x003BC3F7
		[DataMember]
		public string ExpandedCssClass { get; set; }

		// Token: 0x17005250 RID: 21072
		// (get) Token: 0x06010E0B RID: 69131 RVA: 0x003BE200 File Offset: 0x003BC400
		// (set) Token: 0x06010E0C RID: 69132 RVA: 0x003BE208 File Offset: 0x003BC408
		[DataMember]
		public string FocusedCssClass { get; set; }

		// Token: 0x17005251 RID: 21073
		// (get) Token: 0x06010E0D RID: 69133 RVA: 0x003BE211 File Offset: 0x003BC411
		// (set) Token: 0x06010E0E RID: 69134 RVA: 0x003BE219 File Offset: 0x003BC419
		[DataMember]
		public string ClickedCssClass { get; set; }

		// Token: 0x17005252 RID: 21074
		// (get) Token: 0x06010E0F RID: 69135 RVA: 0x003BE222 File Offset: 0x003BC422
		// (set) Token: 0x06010E10 RID: 69136 RVA: 0x003BE22A File Offset: 0x003BC42A
		[DataMember]
		public string ImageUrl { get; set; }

		// Token: 0x17005253 RID: 21075
		// (get) Token: 0x06010E11 RID: 69137 RVA: 0x003BE233 File Offset: 0x003BC433
		// (set) Token: 0x06010E12 RID: 69138 RVA: 0x003BE23B File Offset: 0x003BC43B
		[DataMember]
		public string HoveredImageUrl { get; set; }

		// Token: 0x17005254 RID: 21076
		// (get) Token: 0x06010E13 RID: 69139 RVA: 0x003BE244 File Offset: 0x003BC444
		// (set) Token: 0x06010E14 RID: 69140 RVA: 0x003BE24C File Offset: 0x003BC44C
		[DataMember]
		public string ClickedImageUrl { get; set; }

		// Token: 0x17005255 RID: 21077
		// (get) Token: 0x06010E15 RID: 69141 RVA: 0x003BE255 File Offset: 0x003BC455
		// (set) Token: 0x06010E16 RID: 69142 RVA: 0x003BE25D File Offset: 0x003BC45D
		[DataMember]
		public string DisabledImageUrl { get; set; }

		// Token: 0x17005256 RID: 21078
		// (get) Token: 0x06010E17 RID: 69143 RVA: 0x003BE266 File Offset: 0x003BC466
		// (set) Token: 0x06010E18 RID: 69144 RVA: 0x003BE26E File Offset: 0x003BC46E
		[DataMember]
		public string ExpandedImageUrl { get; set; }
	}
}

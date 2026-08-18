using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F35 RID: 3893
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadRibbonBarClientState
	{
		// Token: 0x17002EE7 RID: 12007
		// (get) Token: 0x06009454 RID: 37972 RVA: 0x00213FEC File Offset: 0x002121EC
		// (set) Token: 0x06009455 RID: 37973 RVA: 0x00213FF4 File Offset: 0x002121F4
		public string[] ToggledIndices { get; set; }

		// Token: 0x17002EE8 RID: 12008
		// (get) Token: 0x06009456 RID: 37974 RVA: 0x00213FFD File Offset: 0x002121FD
		// (set) Token: 0x06009457 RID: 37975 RVA: 0x00214005 File Offset: 0x00212205
		public int? Width { get; set; }

		// Token: 0x17002EE9 RID: 12009
		// (get) Token: 0x06009458 RID: 37976 RVA: 0x0021400E File Offset: 0x0021220E
		// (set) Token: 0x06009459 RID: 37977 RVA: 0x00214016 File Offset: 0x00212216
		public bool Minimized { get; set; }

		// Token: 0x17002EEA RID: 12010
		// (get) Token: 0x0600945A RID: 37978 RVA: 0x0021401F File Offset: 0x0021221F
		// (set) Token: 0x0600945B RID: 37979 RVA: 0x00214027 File Offset: 0x00212227
		public bool Enabled { get; set; }

		// Token: 0x17002EEB RID: 12011
		// (get) Token: 0x0600945C RID: 37980 RVA: 0x00214030 File Offset: 0x00212230
		// (set) Token: 0x0600945D RID: 37981 RVA: 0x00214038 File Offset: 0x00212238
		public int SelectedTabIndex { get; set; }

		// Token: 0x17002EEC RID: 12012
		// (get) Token: 0x0600945E RID: 37982 RVA: 0x00214041 File Offset: 0x00212241
		// (set) Token: 0x0600945F RID: 37983 RVA: 0x00214049 File Offset: 0x00212249
		public bool Activated { get; set; }

		// Token: 0x17002EED RID: 12013
		// (get) Token: 0x06009460 RID: 37984 RVA: 0x00214052 File Offset: 0x00212252
		// (set) Token: 0x06009461 RID: 37985 RVA: 0x0021405A File Offset: 0x0021225A
		public string[] SplitButtonSelectedIndices { get; set; }

		// Token: 0x17002EEE RID: 12014
		// (get) Token: 0x06009462 RID: 37986 RVA: 0x00214063 File Offset: 0x00212263
		// (set) Token: 0x06009463 RID: 37987 RVA: 0x0021406B File Offset: 0x0021226B
		public string[] ComboBoxSelectedIndices { get; set; }

		// Token: 0x17002EEF RID: 12015
		// (get) Token: 0x06009464 RID: 37988 RVA: 0x00214074 File Offset: 0x00212274
		// (set) Token: 0x06009465 RID: 37989 RVA: 0x0021407C File Offset: 0x0021227C
		public string[] DropDownSelectedIndices { get; set; }

		// Token: 0x17002EF0 RID: 12016
		// (get) Token: 0x06009466 RID: 37990 RVA: 0x00214085 File Offset: 0x00212285
		// (set) Token: 0x06009467 RID: 37991 RVA: 0x0021408D File Offset: 0x0021228D
		public string[] ColorPickerColorIndices { get; set; }

		// Token: 0x17002EF1 RID: 12017
		// (get) Token: 0x06009468 RID: 37992 RVA: 0x00214096 File Offset: 0x00212296
		// (set) Token: 0x06009469 RID: 37993 RVA: 0x0021409E File Offset: 0x0021229E
		public string[] GallerySelectedIndices { get; set; }

		// Token: 0x17002EF2 RID: 12018
		// (get) Token: 0x0600946A RID: 37994 RVA: 0x002140A7 File Offset: 0x002122A7
		// (set) Token: 0x0600946B RID: 37995 RVA: 0x002140AF File Offset: 0x002122AF
		public string[] ClientSideDisabledItems { get; set; }

		// Token: 0x17002EF3 RID: 12019
		// (get) Token: 0x0600946C RID: 37996 RVA: 0x002140B8 File Offset: 0x002122B8
		// (set) Token: 0x0600946D RID: 37997 RVA: 0x002140C0 File Offset: 0x002122C0
		public string[] ClientSideEnabledItems { get; set; }

		// Token: 0x17002EF4 RID: 12020
		// (get) Token: 0x0600946E RID: 37998 RVA: 0x002140C9 File Offset: 0x002122C9
		// (set) Token: 0x0600946F RID: 37999 RVA: 0x002140D1 File Offset: 0x002122D1
		public string[] ClientSideDisabledGroups { get; set; }

		// Token: 0x17002EF5 RID: 12021
		// (get) Token: 0x06009470 RID: 38000 RVA: 0x002140DA File Offset: 0x002122DA
		// (set) Token: 0x06009471 RID: 38001 RVA: 0x002140E2 File Offset: 0x002122E2
		public string[] ClientSideEnabledGroups { get; set; }

		// Token: 0x17002EF6 RID: 12022
		// (get) Token: 0x06009472 RID: 38002 RVA: 0x002140EB File Offset: 0x002122EB
		// (set) Token: 0x06009473 RID: 38003 RVA: 0x002140F3 File Offset: 0x002122F3
		public string[] ClientSideDisabledTabs { get; set; }

		// Token: 0x17002EF7 RID: 12023
		// (get) Token: 0x06009474 RID: 38004 RVA: 0x002140FC File Offset: 0x002122FC
		// (set) Token: 0x06009475 RID: 38005 RVA: 0x00214104 File Offset: 0x00212304
		public string[] ClientSideEnabledTabs { get; set; }

		// Token: 0x17002EF8 RID: 12024
		// (get) Token: 0x06009476 RID: 38006 RVA: 0x0021410D File Offset: 0x0021230D
		// (set) Token: 0x06009477 RID: 38007 RVA: 0x00214115 File Offset: 0x00212315
		public string[] QatActiveItemIndices { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004B1 RID: 1201
	public class AppTypeWithExtendedInfo : AppType
	{
		// Token: 0x0600243E RID: 9278 RVA: 0x0002770B File Offset: 0x0002590B
		public AppTypeWithExtendedInfo()
		{
			this.DefaultIconIndex = -1;
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x0002771D File Offset: 0x0002591D
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x00027725 File Offset: 0x00025925
		public bool IsBackground { get; set; }

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x0002772E File Offset: 0x0002592E
		// (set) Token: 0x06002442 RID: 9282 RVA: 0x00027736 File Offset: 0x00025936
		public int DefaultOverrideColourArgb { get; set; }

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x0002773F File Offset: 0x0002593F
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x00027747 File Offset: 0x00025947
		public int DefaultIconIndex { get; set; }

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00027750 File Offset: 0x00025950
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x00027758 File Offset: 0x00025958
		public bool ShowInHighlights { get; set; }

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00027761 File Offset: 0x00025961
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x00027769 File Offset: 0x00025969
		public IList<int> PerAppScreenNumsForTabs { get; set; }

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x00027772 File Offset: 0x00025972
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x0002777A File Offset: 0x0002597A
		public int PerJustAppScreenNum { get; set; }

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00027783 File Offset: 0x00025983
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x0002778B File Offset: 0x0002598B
		public int IconIndex { get; set; }

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x00027794 File Offset: 0x00025994
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x0002779C File Offset: 0x0002599C
		public IList<int> ClientGroupIds { get; set; }

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000277A5 File Offset: 0x000259A5
		// (set) Token: 0x06002450 RID: 9296 RVA: 0x000277AD File Offset: 0x000259AD
		public bool RequiresRoom { get; set; }
	}
}

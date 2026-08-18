using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B26 RID: 2854
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadDropDownListClientState
	{
		// Token: 0x06006AFC RID: 27388 RVA: 0x00190249 File Offset: 0x0018E449
		public RadDropDownListClientState()
		{
			this.Enabled = true;
			this.SelectedIndex = -1;
			this.SelectedValue = string.Empty;
			this.SelectedText = string.Empty;
		}

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x06006AFD RID: 27389 RVA: 0x00190275 File Offset: 0x0018E475
		// (set) Token: 0x06006AFE RID: 27390 RVA: 0x0019027D File Offset: 0x0018E47D
		public ClientStateLogEntry[] LogEntries { get; set; }

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x06006AFF RID: 27391 RVA: 0x00190286 File Offset: 0x0018E486
		// (set) Token: 0x06006B00 RID: 27392 RVA: 0x0019028E File Offset: 0x0018E48E
		public int SelectedIndex { get; set; }

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x06006B01 RID: 27393 RVA: 0x00190297 File Offset: 0x0018E497
		// (set) Token: 0x06006B02 RID: 27394 RVA: 0x0019029F File Offset: 0x0018E49F
		public string SelectedValue { get; set; }

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x06006B03 RID: 27395 RVA: 0x001902A8 File Offset: 0x0018E4A8
		// (set) Token: 0x06006B04 RID: 27396 RVA: 0x001902B0 File Offset: 0x0018E4B0
		public string SelectedText { get; set; }

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x06006B05 RID: 27397 RVA: 0x001902B9 File Offset: 0x0018E4B9
		// (set) Token: 0x06006B06 RID: 27398 RVA: 0x001902C1 File Offset: 0x0018E4C1
		public bool Enabled { get; set; }
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200045B RID: 1115
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class DropDownTreeClientState
	{
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x00082F60 File Offset: 0x00081160
		// (set) Token: 0x06002857 RID: 10327 RVA: 0x00082F68 File Offset: 0x00081168
		public ClientStateLogEntry[] LogEntries { get; set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x00082F71 File Offset: 0x00081171
		// (set) Token: 0x06002859 RID: 10329 RVA: 0x00082F79 File Offset: 0x00081179
		public bool Enabled { get; set; }

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x00082F82 File Offset: 0x00081182
		// (set) Token: 0x0600285B RID: 10331 RVA: 0x00082F8A File Offset: 0x0008118A
		public bool FireServerEvents { get; set; }
	}
}

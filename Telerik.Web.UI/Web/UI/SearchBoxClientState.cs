using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000EE9 RID: 3817
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SearchBoxClientState
	{
		// Token: 0x060090D5 RID: 37077 RVA: 0x00209F85 File Offset: 0x00208185
		public SearchBoxClientState()
		{
			this.Enabled = true;
		}

		// Token: 0x17002DDD RID: 11741
		// (get) Token: 0x060090D6 RID: 37078 RVA: 0x00209F94 File Offset: 0x00208194
		// (set) Token: 0x060090D7 RID: 37079 RVA: 0x00209F9C File Offset: 0x0020819C
		public ClientStateLogEntry[] LogEntries { get; set; }

		// Token: 0x17002DDE RID: 11742
		// (get) Token: 0x060090D8 RID: 37080 RVA: 0x00209FA5 File Offset: 0x002081A5
		// (set) Token: 0x060090D9 RID: 37081 RVA: 0x00209FAD File Offset: 0x002081AD
		public bool Enabled { get; set; }

		// Token: 0x17002DDF RID: 11743
		// (get) Token: 0x060090DA RID: 37082 RVA: 0x00209FB6 File Offset: 0x002081B6
		// (set) Token: 0x060090DB RID: 37083 RVA: 0x00209FBE File Offset: 0x002081BE
		public int SelectedContextIndex { get; set; }
	}
}

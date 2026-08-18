using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000EE5 RID: 3813
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SearchBoxPostBackCommand
	{
		// Token: 0x17002DD5 RID: 11733
		// (get) Token: 0x060090BF RID: 37055 RVA: 0x00209BF1 File Offset: 0x00207DF1
		// (set) Token: 0x060090C0 RID: 37056 RVA: 0x00209BF9 File Offset: 0x00207DF9
		public SearchBoxCommand Type { get; set; }

		// Token: 0x17002DD6 RID: 11734
		// (get) Token: 0x060090C1 RID: 37057 RVA: 0x00209C02 File Offset: 0x00207E02
		// (set) Token: 0x060090C2 RID: 37058 RVA: 0x00209C0A File Offset: 0x00207E0A
		public string Text { get; set; }

		// Token: 0x17002DD7 RID: 11735
		// (get) Token: 0x060090C3 RID: 37059 RVA: 0x00209C13 File Offset: 0x00207E13
		// (set) Token: 0x060090C4 RID: 37060 RVA: 0x00209C1B File Offset: 0x00207E1B
		public string Value { get; set; }

		// Token: 0x17002DD8 RID: 11736
		// (get) Token: 0x060090C5 RID: 37061 RVA: 0x00209C24 File Offset: 0x00207E24
		// (set) Token: 0x060090C6 RID: 37062 RVA: 0x00209C2C File Offset: 0x00207E2C
		public object DataItem { get; set; }

		// Token: 0x17002DD9 RID: 11737
		// (get) Token: 0x060090C7 RID: 37063 RVA: 0x00209C35 File Offset: 0x00207E35
		// (set) Token: 0x060090C8 RID: 37064 RVA: 0x00209C3D File Offset: 0x00207E3D
		public string CommandName { get; set; }

		// Token: 0x17002DDA RID: 11738
		// (get) Token: 0x060090C9 RID: 37065 RVA: 0x00209C46 File Offset: 0x00207E46
		// (set) Token: 0x060090CA RID: 37066 RVA: 0x00209C4E File Offset: 0x00207E4E
		public string CommandArgument { get; set; }
	}
}

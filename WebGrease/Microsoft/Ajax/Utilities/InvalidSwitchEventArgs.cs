using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000DB RID: 219
	public class InvalidSwitchEventArgs : EventArgs
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00042A3B File Offset: 0x00040C3B
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x00042A43 File Offset: 0x00040C43
		public string SwitchPart { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00042A4C File Offset: 0x00040C4C
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x00042A54 File Offset: 0x00040C54
		public string ParameterPart { get; set; }
	}
}

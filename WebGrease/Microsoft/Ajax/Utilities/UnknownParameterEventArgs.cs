using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000DC RID: 220
	public class UnknownParameterEventArgs : EventArgs
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00042A65 File Offset: 0x00040C65
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00042A6D File Offset: 0x00040C6D
		public IList<string> Arguments { get; private set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00042A76 File Offset: 0x00040C76
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00042A7E File Offset: 0x00040C7E
		public int Index { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00042A87 File Offset: 0x00040C87
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x00042A8F File Offset: 0x00040C8F
		public string SwitchPart { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00042A98 File Offset: 0x00040C98
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00042AA0 File Offset: 0x00040CA0
		public string ParameterPart { get; set; }

		// Token: 0x06000E74 RID: 3700 RVA: 0x00042AA9 File Offset: 0x00040CA9
		public UnknownParameterEventArgs(IList<string> arguments)
		{
			this.Arguments = arguments;
		}
	}
}

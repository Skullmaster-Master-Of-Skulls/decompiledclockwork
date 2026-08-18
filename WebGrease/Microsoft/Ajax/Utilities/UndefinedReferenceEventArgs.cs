using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000A4 RID: 164
	public class UndefinedReferenceEventArgs : EventArgs
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00032238 File Offset: 0x00030438
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x00032240 File Offset: 0x00030440
		public UndefinedReference Reference { get; private set; }

		// Token: 0x06000A9E RID: 2718 RVA: 0x00032249 File Offset: 0x00030449
		public UndefinedReferenceEventArgs(UndefinedReference reference)
		{
			this.Reference = reference;
		}
	}
}

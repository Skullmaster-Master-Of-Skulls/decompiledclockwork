using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x0200000C RID: 12
	public class BundleFileSetOrdering
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000366E File Offset: 0x0000186E
		public BundleFileSetOrdering(string name)
		{
			this.Name = name;
			this.Files = new List<string>();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003688 File Offset: 0x00001888
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003690 File Offset: 0x00001890
		public string Name { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003699 File Offset: 0x00001899
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000036A1 File Offset: 0x000018A1
		public IList<string> Files { get; private set; }
	}
}

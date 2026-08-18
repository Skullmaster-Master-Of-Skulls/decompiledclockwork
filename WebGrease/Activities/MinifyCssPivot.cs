using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Configuration;

namespace WebGrease.Activities
{
	// Token: 0x02000002 RID: 2
	internal class MinifyCssPivot
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D8 File Offset: 0x000002D8
		public MinifyCssPivot(IEnumerable<IDictionary<string, string>> mergedResource, ResourcePivotKey[] newContentResourcePivotKeys, float dpi)
		{
			this.MergedResource = mergedResource;
			this.NewContentResourcePivotKeys = newContentResourcePivotKeys;
			this.Dpi = dpi;
			this.stringValue = string.Join("-", from p in this.NewContentResourcePivotKeys
			select p.Key);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
		public IEnumerable<IDictionary<string, string>> MergedResource { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002149 File Offset: 0x00000349
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002151 File Offset: 0x00000351
		public ResourcePivotKey[] NewContentResourcePivotKeys { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000215A File Offset: 0x0000035A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002162 File Offset: 0x00000362
		public float Dpi { get; private set; }

		// Token: 0x06000008 RID: 8 RVA: 0x0000216B File Offset: 0x0000036B
		public override string ToString()
		{
			return this.stringValue;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002173 File Offset: 0x00000373
		public override int GetHashCode()
		{
			return this.stringValue.GetHashCode();
		}

		// Token: 0x04000001 RID: 1
		private readonly string stringValue;
	}
}

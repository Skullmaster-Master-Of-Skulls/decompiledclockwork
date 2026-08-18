using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class RadDataFormIndexesCollection : List<int>
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x00044474 File Offset: 0x00042674
		public RadDataFormIndexesCollection()
		{
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004447C File Offset: 0x0004267C
		public RadDataFormIndexesCollection(IEnumerable<int> collection) : base(collection)
		{
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00044485 File Offset: 0x00042685
		public new void Add(int item)
		{
			if (!base.Contains(item))
			{
				base.Add(item);
			}
		}
	}
}

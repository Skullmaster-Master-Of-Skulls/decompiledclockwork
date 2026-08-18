using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001293 RID: 4755
	[Serializable]
	public class TreeListIndexesCollection<T> : List<T>
	{
		// Token: 0x0600C64A RID: 50762 RVA: 0x002C3D1C File Offset: 0x002C1F1C
		public TreeListIndexesCollection()
		{
		}

		// Token: 0x0600C64B RID: 50763 RVA: 0x002C3D24 File Offset: 0x002C1F24
		public TreeListIndexesCollection(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x0600C64C RID: 50764 RVA: 0x002C3D2D File Offset: 0x002C1F2D
		public new void Add(T item)
		{
			if (!base.Contains(item))
			{
				base.Add(item);
			}
		}

		// Token: 0x0600C64D RID: 50765 RVA: 0x002C3D3F File Offset: 0x002C1F3F
		internal void UnCheckedAdd(T item)
		{
			base.Add(item);
		}
	}
}

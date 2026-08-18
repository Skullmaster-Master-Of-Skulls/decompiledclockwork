using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001236 RID: 4662
	internal class TreeListDeleteContext
	{
		// Token: 0x0600C04D RID: 49229 RVA: 0x002AAF88 File Offset: 0x002A9188
		public TreeListDeleteContext(Hashtable keys, Hashtable oldValues, TreeListDataItem item, bool suppressRebind)
		{
			this.Keys = keys;
			this.OldValues = oldValues;
			this.SuppressRebind = suppressRebind;
			this.Item = item;
			this.Indexes = new List<TreeListIndexesCollection<TreeListHierarchyIndex>>
			{
				new TreeListIndexesCollection<TreeListHierarchyIndex>(),
				new TreeListIndexesCollection<TreeListHierarchyIndex>(),
				new TreeListIndexesCollection<TreeListHierarchyIndex>(),
				new TreeListIndexesCollection<TreeListHierarchyIndex>()
			};
		}

		// Token: 0x17003E16 RID: 15894
		// (get) Token: 0x0600C04E RID: 49230 RVA: 0x002AAFF1 File Offset: 0x002A91F1
		// (set) Token: 0x0600C04F RID: 49231 RVA: 0x002AAFF9 File Offset: 0x002A91F9
		public Hashtable Keys { get; private set; }

		// Token: 0x17003E17 RID: 15895
		// (get) Token: 0x0600C050 RID: 49232 RVA: 0x002AB002 File Offset: 0x002A9202
		// (set) Token: 0x0600C051 RID: 49233 RVA: 0x002AB00A File Offset: 0x002A920A
		public Hashtable OldValues { get; private set; }

		// Token: 0x17003E18 RID: 15896
		// (get) Token: 0x0600C052 RID: 49234 RVA: 0x002AB013 File Offset: 0x002A9213
		// (set) Token: 0x0600C053 RID: 49235 RVA: 0x002AB01B File Offset: 0x002A921B
		public bool SuppressRebind { get; private set; }

		// Token: 0x17003E19 RID: 15897
		// (get) Token: 0x0600C054 RID: 49236 RVA: 0x002AB024 File Offset: 0x002A9224
		// (set) Token: 0x0600C055 RID: 49237 RVA: 0x002AB02C File Offset: 0x002A922C
		public TreeListDataItem Item { get; private set; }

		// Token: 0x17003E1A RID: 15898
		// (get) Token: 0x0600C056 RID: 49238 RVA: 0x002AB035 File Offset: 0x002A9235
		// (set) Token: 0x0600C057 RID: 49239 RVA: 0x002AB03D File Offset: 0x002A923D
		public List<TreeListIndexesCollection<TreeListHierarchyIndex>> Indexes { get; private set; }
	}
}

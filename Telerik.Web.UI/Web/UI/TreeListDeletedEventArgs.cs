using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F26 RID: 3878
	public class TreeListDeletedEventArgs : TreeListDataChangeEventArgs
	{
		// Token: 0x060093F2 RID: 37874 RVA: 0x00212E39 File Offset: 0x00211039
		public TreeListDeletedEventArgs(int affectedRows, Exception e, IDictionary keys) : this(affectedRows, e, keys, null)
		{
			this.Keys = keys;
		}

		// Token: 0x060093F3 RID: 37875 RVA: 0x00212E4C File Offset: 0x0021104C
		public TreeListDeletedEventArgs(int affectedRows, Exception e, IDictionary keys, TreeListEditableItem item) : base(affectedRows, e, item)
		{
			this.Keys = keys;
		}

		// Token: 0x17002ECD RID: 11981
		// (get) Token: 0x060093F4 RID: 37876 RVA: 0x00212E5F File Offset: 0x0021105F
		// (set) Token: 0x060093F5 RID: 37877 RVA: 0x00212E67 File Offset: 0x00211067
		public IDictionary Keys { get; private set; }
	}
}

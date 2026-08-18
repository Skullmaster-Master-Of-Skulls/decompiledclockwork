using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B3B RID: 6971
	public class ContextMenuTargetCollection : StateManagedCollection
	{
		// Token: 0x06010DB4 RID: 69044 RVA: 0x003BD7D0 File Offset: 0x003BB9D0
		public ContextMenuTargetCollection(RadContextMenu owner)
		{
			this._owner = owner;
		}

		// Token: 0x1700522F RID: 21039
		public ContextMenuTarget this[int index]
		{
			get
			{
				return (ContextMenuTarget)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x06010DB7 RID: 69047 RVA: 0x003BD801 File Offset: 0x003BBA01
		public void Add(ContextMenuTarget target)
		{
			this.List.Add(target);
		}

		// Token: 0x06010DB8 RID: 69048 RVA: 0x003BD810 File Offset: 0x003BBA10
		public bool Contains(ContextMenuTarget target)
		{
			return this.List.Contains(target);
		}

		// Token: 0x06010DB9 RID: 69049 RVA: 0x003BD81E File Offset: 0x003BBA1E
		public void CopyTo(ContextMenuTarget[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x06010DBA RID: 69050 RVA: 0x003BD830 File Offset: 0x003BBA30
		public void AddRange(IEnumerable<ContextMenuTarget> targets)
		{
			foreach (ContextMenuTarget target in targets)
			{
				this.Add(target);
			}
		}

		// Token: 0x06010DBB RID: 69051 RVA: 0x003BD878 File Offset: 0x003BBA78
		public int IndexOf(ContextMenuTarget target)
		{
			return this.List.IndexOf(target);
		}

		// Token: 0x06010DBC RID: 69052 RVA: 0x003BD886 File Offset: 0x003BBA86
		public void Insert(int index, ContextMenuTarget target)
		{
			this.List.Insert(index, target);
		}

		// Token: 0x06010DBD RID: 69053 RVA: 0x003BD895 File Offset: 0x003BBA95
		public void Remove(ContextMenuTarget target)
		{
			this.List.Remove(target);
		}

		// Token: 0x06010DBE RID: 69054 RVA: 0x003BD8A3 File Offset: 0x003BBAA3
		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x06010DBF RID: 69055 RVA: 0x003BD8B4 File Offset: 0x003BBAB4
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			ContextMenuTarget contextMenuTarget = (ContextMenuTarget)value;
			contextMenuTarget.Owner = this._owner;
		}

		// Token: 0x06010DC0 RID: 69056 RVA: 0x003BD8DC File Offset: 0x003BBADC
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			((ContextMenuTarget)value).Owner = null;
		}

		// Token: 0x06010DC1 RID: 69057 RVA: 0x003BD8F4 File Offset: 0x003BBAF4
		protected override void OnClear()
		{
			foreach (object obj in this)
			{
				ContextMenuTarget contextMenuTarget = (ContextMenuTarget)obj;
				contextMenuTarget.Owner = null;
			}
			base.OnClear();
		}

		// Token: 0x06010DC2 RID: 69058 RVA: 0x003BD950 File Offset: 0x003BBB50
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x17005230 RID: 21040
		// (get) Token: 0x06010DC3 RID: 69059 RVA: 0x003BD95D File Offset: 0x003BBB5D
		private IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04004B76 RID: 19318
		private readonly RadContextMenu _owner;
	}
}

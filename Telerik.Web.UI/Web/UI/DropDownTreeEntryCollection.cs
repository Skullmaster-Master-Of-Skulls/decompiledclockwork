using System;
using System.Collections;
using System.Diagnostics;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B2D RID: 2861
	public class DropDownTreeEntryCollection : StateManagedCollection
	{
		// Token: 0x06006B88 RID: 27528 RVA: 0x00191104 File Offset: 0x0018F304
		public DropDownTreeEntryCollection(RadDropDownTree parentContainer)
		{
			this._parentContainer = parentContainer;
		}

		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x06006B89 RID: 27529 RVA: 0x00191113 File Offset: 0x0018F313
		protected IList List
		{
			[DebuggerStepThrough]
			get
			{
				return this;
			}
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x00191116 File Offset: 0x0018F316
		public int IndexOf(DropDownTreeEntry entry)
		{
			return this.List.IndexOf(entry);
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x00191124 File Offset: 0x0018F324
		protected override void OnClear()
		{
			base.OnClear();
			this._parentContainer.EmbeddedTreeAdapter.ClearNodesState();
		}

		// Token: 0x06006B8C RID: 27532 RVA: 0x0019113C File Offset: 0x0018F33C
		internal void ClearAll()
		{
			int count = this.List.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06006B8D RID: 27533 RVA: 0x0019116A File Offset: 0x0018F36A
		internal void Insert(int index, DropDownTreeEntry entry)
		{
			this.List.Insert(index, entry);
		}

		// Token: 0x06006B8E RID: 27534 RVA: 0x00191179 File Offset: 0x0018F379
		internal void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x1700233D RID: 9021
		public virtual DropDownTreeEntry this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (DropDownTreeEntry)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x001911AB File Offset: 0x0018F3AB
		internal void Add(DropDownTreeEntry entry)
		{
			this.List.Add(entry);
		}

		// Token: 0x06006B92 RID: 27538 RVA: 0x001911BA File Offset: 0x0018F3BA
		public bool Contains(DropDownTreeEntry entry)
		{
			return this.List.Contains(entry);
		}

		// Token: 0x06006B93 RID: 27539 RVA: 0x001911C8 File Offset: 0x0018F3C8
		internal void CopyTo(DropDownTreeEntry[] array, int arrayIndex)
		{
			this.List.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x06006B94 RID: 27540 RVA: 0x001911D7 File Offset: 0x0018F3D7
		internal bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006B95 RID: 27541 RVA: 0x001911DC File Offset: 0x0018F3DC
		internal bool Remove(DropDownTreeEntry entry)
		{
			bool result = false;
			if (this.List.Contains(entry))
			{
				this.List.Remove(entry);
				result = true;
			}
			return result;
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x00191208 File Offset: 0x0018F408
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x04001CF9 RID: 7417
		private RadDropDownTree _parentContainer;
	}
}

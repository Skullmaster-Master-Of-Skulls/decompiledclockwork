using System;
using System.Collections;
using System.Diagnostics;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020001C2 RID: 450
	public abstract class GenericStateManagedCollection<TItem> : StateManagedCollection where TItem : IMarkableStateManager
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0003C2FC File Offset: 0x0003A4FC
		protected IList List
		{
			[DebuggerStepThrough]
			get
			{
				return this;
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003C2FF File Offset: 0x0003A4FF
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003C30C File Offset: 0x0003A50C
		public int IndexOf(TItem item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003C31F File Offset: 0x0003A51F
		public void Insert(int index, TItem item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003C333 File Offset: 0x0003A533
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x17000593 RID: 1427
		public virtual TItem this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (TItem)((object)this.List[index]);
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003C36A File Offset: 0x0003A56A
		public void Add(TItem item)
		{
			this.List.Add(item);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003C37E File Offset: 0x0003A57E
		public bool Contains(TItem item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003C391 File Offset: 0x0003A591
		public void CopyTo(TItem[] array, int arrayIndex)
		{
			this.List.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		public bool Remove(TItem item)
		{
			bool result = false;
			if (this.List.Contains(item))
			{
				this.List.Remove(item);
				result = true;
			}
			return result;
		}
	}
}

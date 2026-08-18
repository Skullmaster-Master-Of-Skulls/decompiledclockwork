using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B16 RID: 2838
	public class DropDownListItemCollection : ControlItemCollection, IList<DropDownListItem>, ICollection<DropDownListItem>, IEnumerable<DropDownListItem>, IEnumerable
	{
		// Token: 0x06006A1C RID: 27164 RVA: 0x0018E514 File Offset: 0x0018C714
		public DropDownListItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x0018E520 File Offset: 0x0018C720
		protected override void SetOwner(ControlItem item)
		{
			DropDownListItem dropDownListItem = item as DropDownListItem;
			RadDropDownList dropDownList = dropDownListItem.DropDownList;
			if (dropDownList != null && dropDownList.Items.Contains(item) && dropDownList != base.Parent)
			{
				dropDownList.Items.Remove(item);
			}
			dropDownListItem.DropDownList = (RadDropDownList)base.Parent;
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x0018E572 File Offset: 0x0018C772
		public void Add(DropDownListItem item)
		{
			base.Add(item);
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x0018E57C File Offset: 0x0018C77C
		public virtual void Add(string text)
		{
			DropDownListItem item = new DropDownListItem(text);
			base.Add(item);
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x0018E597 File Offset: 0x0018C797
		public void Remove(DropDownListItem item)
		{
			base.Remove(item);
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x0018E5A0 File Offset: 0x0018C7A0
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x0018E5AF File Offset: 0x0018C7AF
		public void Insert(int index, DropDownListItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x0018E5BC File Offset: 0x0018C7BC
		public virtual void Insert(int index, string text)
		{
			DropDownListItem item = new DropDownListItem(text);
			base.Insert(index, item);
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x0018E5D8 File Offset: 0x0018C7D8
		bool ICollection<DropDownListItem>.Contains(DropDownListItem item)
		{
			return this.Contains(item);
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x0018E5E1 File Offset: 0x0018C7E1
		void ICollection<DropDownListItem>.CopyTo(DropDownListItem[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x0018E5EB File Offset: 0x0018C7EB
		bool ICollection<DropDownListItem>.Remove(DropDownListItem item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x06006A27 RID: 27175 RVA: 0x0018E5F5 File Offset: 0x0018C7F5
		bool ICollection<DropDownListItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x0018E5F8 File Offset: 0x0018C7F8
		int IList<DropDownListItem>.IndexOf(DropDownListItem item)
		{
			return this.IndexOf(item);
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x0018E601 File Offset: 0x0018C801
		void IList<DropDownListItem>.Insert(int index, DropDownListItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x0018E60B File Offset: 0x0018C80B
		void IList<DropDownListItem>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x0018E754 File Offset: 0x0018C954
		IEnumerator<DropDownListItem> IEnumerable<DropDownListItem>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				DropDownListItem item = (DropDownListItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x170022BF RID: 8895
		public DropDownListItem this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (DropDownListItem)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x0018E788 File Offset: 0x0018C988
		public void Sort()
		{
			List<ControlItem> list = new List<ControlItem>();
			foreach (object obj in this)
			{
				ControlItem item = (ControlItem)obj;
				list.Add(item);
			}
			ArrayList.Adapter(list).Sort();
			base.Clear();
			this.AddRange(list);
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x0018E7FC File Offset: 0x0018C9FC
		public void Sort(IComparer comparer)
		{
			List<ControlItem> list = new List<ControlItem>();
			foreach (object obj in this)
			{
				ControlItem item = (ControlItem)obj;
				list.Add(item);
			}
			ArrayList.Adapter(list).Sort(comparer);
			base.Clear();
			this.AddRange(list);
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x0018E870 File Offset: 0x0018CA70
		public void Sort(Func<DropDownListItem, object> predicate)
		{
			List<ControlItem> list = new List<ControlItem>();
			IOrderedEnumerable<DropDownListItem> orderedEnumerable = this.OrderBy(predicate);
			foreach (ControlItem item in orderedEnumerable)
			{
				list.Add(item);
			}
			base.Clear();
			this.AddRange(list);
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x0018E8D4 File Offset: 0x0018CAD4
		void ICollection<DropDownListItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x0018E8DC File Offset: 0x0018CADC
		int ICollection<DropDownListItem>.get_Count()
		{
			return base.Count;
		}
	}
}

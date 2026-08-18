using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001941 RID: 6465
	public class RadListBoxItemCollection : ControlItemCollection, IList<RadListBoxItem>, ICollection<RadListBoxItem>, IEnumerable<RadListBoxItem>, IEnumerable
	{
		// Token: 0x0600FA4D RID: 64077 RVA: 0x00385F8B File Offset: 0x0038418B
		public RadListBoxItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x0600FA4E RID: 64078 RVA: 0x00385F94 File Offset: 0x00384194
		public void Add(RadListBoxItem item)
		{
			base.Add(item);
		}

		// Token: 0x0600FA4F RID: 64079 RVA: 0x00385FA0 File Offset: 0x003841A0
		public virtual void Add(string text)
		{
			RadListBoxItem item = new RadListBoxItem(text);
			base.Add(item);
		}

		// Token: 0x0600FA50 RID: 64080 RVA: 0x00385FBB File Offset: 0x003841BB
		public void Insert(int index, RadListBoxItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x0600FA51 RID: 64081 RVA: 0x00385FC8 File Offset: 0x003841C8
		public virtual void Insert(int index, string text)
		{
			RadListBoxItem item = new RadListBoxItem(text);
			base.Insert(index, item);
		}

		// Token: 0x0600FA52 RID: 64082 RVA: 0x00385FE4 File Offset: 0x003841E4
		protected override void AddItemToParentControls(int index, ControlItem item)
		{
			if (index >= 0)
			{
				foreach (object obj in base.Parent.Controls)
				{
					Control control = (Control)obj;
					index++;
					if (control.ID == (base.Parent as RadListBox).Footer.ID)
					{
						break;
					}
				}
			}
			base.AddItemToParentControls(index, item);
		}

		// Token: 0x0600FA53 RID: 64083 RVA: 0x00386070 File Offset: 0x00384270
		bool ICollection<RadListBoxItem>.Contains(RadListBoxItem item)
		{
			return this.Contains(item);
		}

		// Token: 0x0600FA54 RID: 64084 RVA: 0x00386079 File Offset: 0x00384279
		void ICollection<RadListBoxItem>.CopyTo(RadListBoxItem[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600FA55 RID: 64085 RVA: 0x00386083 File Offset: 0x00384283
		bool ICollection<RadListBoxItem>.Remove(RadListBoxItem item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x17004BA5 RID: 19365
		// (get) Token: 0x0600FA56 RID: 64086 RVA: 0x0038608D File Offset: 0x0038428D
		bool ICollection<RadListBoxItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600FA57 RID: 64087 RVA: 0x00386090 File Offset: 0x00384290
		int IList<RadListBoxItem>.IndexOf(RadListBoxItem item)
		{
			return this.IndexOf(item);
		}

		// Token: 0x0600FA58 RID: 64088 RVA: 0x00386099 File Offset: 0x00384299
		void IList<RadListBoxItem>.Insert(int index, RadListBoxItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x0600FA59 RID: 64089 RVA: 0x003860A3 File Offset: 0x003842A3
		void IList<RadListBoxItem>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x0600FA5A RID: 64090 RVA: 0x003861EC File Offset: 0x003843EC
		IEnumerator<RadListBoxItem> IEnumerable<RadListBoxItem>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				RadListBoxItem item = (RadListBoxItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x17004BA6 RID: 19366
		public RadListBoxItem this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return (RadListBoxItem)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x0600FA5D RID: 64093 RVA: 0x00386220 File Offset: 0x00384420
		public IList<RadListBoxItem> FindAll(Predicate<RadListBoxItem> match)
		{
			List<RadListBoxItem> list = new List<RadListBoxItem>();
			foreach (object obj in this)
			{
				RadListBoxItem radListBoxItem = (RadListBoxItem)obj;
				if (match(radListBoxItem))
				{
					list.Add(radListBoxItem);
				}
			}
			return list;
		}

		// Token: 0x0600FA5E RID: 64094 RVA: 0x00386284 File Offset: 0x00384484
		protected override void SetOwner(ControlItem item)
		{
			RadListBoxItem radListBoxItem = item as RadListBoxItem;
			RadListBox listBox = radListBoxItem.ListBox;
			if (listBox != null && listBox.Items.Contains(item) && listBox != base.Parent)
			{
				listBox.Items.Remove(item);
			}
			radListBoxItem.ListBox = (RadListBox)base.Parent;
		}

		// Token: 0x0600FA5F RID: 64095 RVA: 0x003862D6 File Offset: 0x003844D6
		public void Remove(RadListBoxItem item)
		{
			base.Remove(item);
		}

		// Token: 0x0600FA60 RID: 64096 RVA: 0x003862E0 File Offset: 0x003844E0
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

		// Token: 0x0600FA61 RID: 64097 RVA: 0x00386354 File Offset: 0x00384554
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

		// Token: 0x0600FA62 RID: 64098 RVA: 0x003863C8 File Offset: 0x003845C8
		public void Sort(Func<RadListBoxItem, object> predicate)
		{
			List<ControlItem> list = new List<ControlItem>();
			IOrderedEnumerable<RadListBoxItem> orderedEnumerable = this.OrderBy(predicate);
			foreach (ControlItem item in orderedEnumerable)
			{
				list.Add(item);
			}
			base.Clear();
			this.AddRange(list);
		}

		// Token: 0x0600FA63 RID: 64099 RVA: 0x0038642C File Offset: 0x0038462C
		void ICollection<RadListBoxItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x0600FA64 RID: 64100 RVA: 0x00386434 File Offset: 0x00384634
		int ICollection<RadListBoxItem>.get_Count()
		{
			return base.Count;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.ComboBox;

namespace Telerik.Web.UI
{
	// Token: 0x0200101C RID: 4124
	public class RadComboBoxItemCollection : ControlItemCollection, IList<RadComboBoxItem>, ICollection<RadComboBoxItem>, IEnumerable<RadComboBoxItem>, IEnumerable
	{
		// Token: 0x0600A2CA RID: 41674 RVA: 0x00243E28 File Offset: 0x00242028
		public RadComboBoxItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x0600A2CB RID: 41675 RVA: 0x00243E31 File Offset: 0x00242031
		protected override void AddItemToParentControls(int index, ControlItem item)
		{
			if (index >= 0)
			{
				index += 2;
			}
			base.AddItemToParentControls(index, item);
		}

		// Token: 0x1700337A RID: 13178
		public RadComboBoxItem this[int index]
		{
			get
			{
				RadComboBoxItem result;
				try
				{
					RadComboBoxItem radComboBoxItem = (RadComboBoxItem)base[index];
					result = radComboBoxItem;
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new ArgumentOutOfRangeException("index", string.Concat(new object[]
					{
						base.Parent.ID,
						": Items selection is out of range. Index ",
						index,
						" is not available in the Items collection."
					}));
				}
				return result;
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x0600A2CE RID: 41678 RVA: 0x00243EBE File Offset: 0x002420BE
		public virtual void Add(RadComboBoxItem item)
		{
			if (item.GetType() == typeof(RadComboBoxDefaultItem))
			{
				throw new Exception("A default item could not be added manually in the RadComboBoxItemCollection");
			}
			base.Add(item);
		}

		// Token: 0x0600A2CF RID: 41679 RVA: 0x00243EEC File Offset: 0x002420EC
		public virtual void Add(string text)
		{
			RadComboBoxItem item = new RadComboBoxItem(text);
			base.Add(item);
		}

		// Token: 0x0600A2D0 RID: 41680 RVA: 0x00243F07 File Offset: 0x00242107
		public RadComboBoxItem FindItemByText(string text)
		{
			return base.FindChildByText<RadComboBoxItem>(text);
		}

		// Token: 0x0600A2D1 RID: 41681 RVA: 0x00243F10 File Offset: 0x00242110
		public RadComboBoxItem FindItemByValue(string value)
		{
			return base.FindChildByValue<RadComboBoxItem>(value);
		}

		// Token: 0x0600A2D2 RID: 41682 RVA: 0x00243F19 File Offset: 0x00242119
		public RadComboBoxItem FindItemByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadComboBoxItem>(attributeName, attributeValue);
		}

		// Token: 0x0600A2D3 RID: 41683 RVA: 0x00243F23 File Offset: 0x00242123
		public RadComboBoxItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadComboBoxItem>(text, ignoreCase);
		}

		// Token: 0x0600A2D4 RID: 41684 RVA: 0x00243F2D File Offset: 0x0024212D
		public RadComboBoxItem FindItemByValue(string value, bool ignoreCase)
		{
			return base.FindChildByValue<RadComboBoxItem>(value, ignoreCase);
		}

		// Token: 0x0600A2D5 RID: 41685 RVA: 0x00243F37 File Offset: 0x00242137
		public int FindItemIndexByText(string text)
		{
			return this.FindItemIndexByText(text, false);
		}

		// Token: 0x0600A2D6 RID: 41686 RVA: 0x00243F44 File Offset: 0x00242144
		public int FindItemIndexByText(string text, bool ignoreCase)
		{
			RadComboBoxItem radComboBoxItem = this.FindItemByText(text, ignoreCase);
			if (radComboBoxItem == null)
			{
				return -1;
			}
			return this.IndexOf(radComboBoxItem);
		}

		// Token: 0x0600A2D7 RID: 41687 RVA: 0x00243F66 File Offset: 0x00242166
		public int FindItemIndexByValue(string value)
		{
			return this.FindItemIndexByValue(value, false);
		}

		// Token: 0x0600A2D8 RID: 41688 RVA: 0x00243F70 File Offset: 0x00242170
		public int FindItemIndexByValue(string value, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			RadComboBoxItem radComboBoxItem = this.FindItemByValue(value, ignoreCase);
			if (radComboBoxItem == null)
			{
				return -1;
			}
			return this.IndexOf(radComboBoxItem);
		}

		// Token: 0x0600A2D9 RID: 41689 RVA: 0x00243F9C File Offset: 0x0024219C
		public RadComboBoxItem FindItem(Predicate<RadComboBoxItem> match)
		{
			return base.FindChild<RadComboBoxItem>(match);
		}

		// Token: 0x0600A2DA RID: 41690 RVA: 0x00243FA5 File Offset: 0x002421A5
		public virtual bool Contains(RadComboBoxItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x0600A2DB RID: 41691 RVA: 0x00243FB0 File Offset: 0x002421B0
		public virtual void AddRange(IEnumerable<RadComboBoxItem> items)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadComboBoxItem radComboBoxItem in items)
			{
				if (radComboBoxItem.GetType() == typeof(RadComboBoxDefaultItem))
				{
					throw new Exception("A default item could not be added manually in the RadComboBoxItemCollection");
				}
				list.Add(radComboBoxItem);
			}
			base.AddRange(list);
		}

		// Token: 0x0600A2DC RID: 41692 RVA: 0x00244028 File Offset: 0x00242228
		public virtual int IndexOf(RadComboBoxItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x0600A2DD RID: 41693 RVA: 0x00244031 File Offset: 0x00242231
		public virtual void Insert(int index, RadComboBoxItem item)
		{
			if (item.GetType() == typeof(RadComboBoxDefaultItem))
			{
				throw new Exception("A default item could not be added manually in the RadComboBoxItemCollection");
			}
			base.Insert(index, item);
		}

		// Token: 0x0600A2DE RID: 41694 RVA: 0x00244060 File Offset: 0x00242260
		public virtual void Insert(int index, string text)
		{
			RadComboBoxItem item = new RadComboBoxItem(text);
			base.Insert(index, item);
		}

		// Token: 0x0600A2DF RID: 41695 RVA: 0x0024407C File Offset: 0x0024227C
		internal void InsertItem(int index, RadComboBoxItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x0600A2E0 RID: 41696 RVA: 0x00244086 File Offset: 0x00242286
		public virtual void Remove(RadComboBoxItem item)
		{
			base.Remove(item);
			item.Owner = null;
		}

		// Token: 0x0600A2E1 RID: 41697 RVA: 0x00244096 File Offset: 0x00242296
		public virtual void Remove(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x0600A2E2 RID: 41698 RVA: 0x002440A8 File Offset: 0x002422A8
		protected override void SetOwner(ControlItem item)
		{
			RadComboBoxItem radComboBoxItem = (RadComboBoxItem)item;
			RadComboBox owner = radComboBoxItem.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Parent)
			{
				owner.Items.Remove(item);
			}
			radComboBoxItem.Owner = (RadComboBox)base.Parent;
		}

		// Token: 0x0600A2E3 RID: 41699 RVA: 0x002440FC File Offset: 0x002422FC
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

		// Token: 0x0600A2E4 RID: 41700 RVA: 0x00244170 File Offset: 0x00242370
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

		// Token: 0x0600A2E5 RID: 41701 RVA: 0x002441E4 File Offset: 0x002423E4
		bool ICollection<RadComboBoxItem>.Contains(RadComboBoxItem item)
		{
			return this.Contains(item);
		}

		// Token: 0x0600A2E6 RID: 41702 RVA: 0x002441ED File Offset: 0x002423ED
		void ICollection<RadComboBoxItem>.CopyTo(RadComboBoxItem[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600A2E7 RID: 41703 RVA: 0x002441F7 File Offset: 0x002423F7
		bool ICollection<RadComboBoxItem>.Remove(RadComboBoxItem item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x1700337B RID: 13179
		// (get) Token: 0x0600A2E8 RID: 41704 RVA: 0x00244201 File Offset: 0x00242401
		bool ICollection<RadComboBoxItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A2E9 RID: 41705 RVA: 0x00244204 File Offset: 0x00242404
		int IList<RadComboBoxItem>.IndexOf(RadComboBoxItem item)
		{
			return this.IndexOf(item);
		}

		// Token: 0x0600A2EA RID: 41706 RVA: 0x0024420D File Offset: 0x0024240D
		void IList<RadComboBoxItem>.Insert(int index, RadComboBoxItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x0600A2EB RID: 41707 RVA: 0x00244217 File Offset: 0x00242417
		void IList<RadComboBoxItem>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x0600A2EC RID: 41708 RVA: 0x00244360 File Offset: 0x00242560
		IEnumerator<RadComboBoxItem> IEnumerable<RadComboBoxItem>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				RadComboBoxItem item = (RadComboBoxItem)obj;
				yield return item;
			}
			yield break;
		}

		// Token: 0x0600A2ED RID: 41709 RVA: 0x0024437C File Offset: 0x0024257C
		void ICollection<RadComboBoxItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x0600A2EE RID: 41710 RVA: 0x00244384 File Offset: 0x00242584
		int ICollection<RadComboBoxItem>.get_Count()
		{
			return base.Count;
		}
	}
}

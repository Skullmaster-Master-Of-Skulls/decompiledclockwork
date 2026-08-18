using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005EB RID: 1515
	public class MultiColumnComboBoxItemCollection : BaseCollection<MultiColumnComboBoxItem>, IList<MultiColumnComboBoxItem>, ICollection<MultiColumnComboBoxItem>, IEnumerable<MultiColumnComboBoxItem>, IEnumerable
	{
		// Token: 0x060036D1 RID: 14033 RVA: 0x000B5A72 File Offset: 0x000B3C72
		public MultiColumnComboBoxItemCollection()
		{
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000B5A7A File Offset: 0x000B3C7A
		public MultiColumnComboBoxItemCollection(RadMultiColumnComboBox owner) : base(owner)
		{
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000B5A84 File Offset: 0x000B3C84
		protected void SetOwner(MultiColumnComboBoxItem item)
		{
			RadMultiColumnComboBox owner = item.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Owner)
			{
				owner.Items.Remove(item);
			}
			item.Owner = base.Owner;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000B5ACC File Offset: 0x000B3CCC
		public override void AddRange(IEnumerable<MultiColumnComboBoxItem> items)
		{
			foreach (MultiColumnComboBoxItem entity in items)
			{
				this.Add(entity);
			}
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000B5B14 File Offset: 0x000B3D14
		public override void Add(MultiColumnComboBoxItem item)
		{
			base.Add(item);
			if (base.Owner != null)
			{
				item.Owner = base.Owner;
			}
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000B5B34 File Offset: 0x000B3D34
		protected internal virtual IList<MultiColumnComboBoxItem> ToList()
		{
			List<MultiColumnComboBoxItem> list = new List<MultiColumnComboBoxItem>();
			foreach (MultiColumnComboBoxItem item in this)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000B5B84 File Offset: 0x000B3D84
		public void RemoveAt(int index)
		{
			this.Remove(base[index]);
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000B5B93 File Offset: 0x000B3D93
		bool ICollection<MultiColumnComboBoxItem>.Contains(MultiColumnComboBoxItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000B5B9C File Offset: 0x000B3D9C
		void ICollection<MultiColumnComboBoxItem>.CopyTo(MultiColumnComboBoxItem[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000B5BA6 File Offset: 0x000B3DA6
		bool ICollection<MultiColumnComboBoxItem>.Remove(MultiColumnComboBoxItem item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000B5BB0 File Offset: 0x000B3DB0
		bool ICollection<MultiColumnComboBoxItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000B5BB3 File Offset: 0x000B3DB3
		int IList<MultiColumnComboBoxItem>.IndexOf(MultiColumnComboBoxItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000B5BBC File Offset: 0x000B3DBC
		void IList<MultiColumnComboBoxItem>.Insert(int index, MultiColumnComboBoxItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000B5BC6 File Offset: 0x000B3DC6
		void IList<MultiColumnComboBoxItem>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000B5CFC File Offset: 0x000B3EFC
		IEnumerator<MultiColumnComboBoxItem> IEnumerable<MultiColumnComboBoxItem>.GetEnumerator()
		{
			foreach (MultiColumnComboBoxItem item in this)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000B5D18 File Offset: 0x000B3F18
		public MultiColumnComboBoxItem FindChild(Predicate<MultiColumnComboBoxItem> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (MultiColumnComboBoxItem multiColumnComboBoxItem in this)
			{
				if (match(multiColumnComboBoxItem))
				{
					return multiColumnComboBoxItem;
				}
			}
			return null;
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000B5D78 File Offset: 0x000B3F78
		public MultiColumnComboBoxItem FindChildByText(string text)
		{
			return this.FindChildByText(text, false);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000B5D82 File Offset: 0x000B3F82
		public MultiColumnComboBoxItem FindChildByValue(string value)
		{
			return this.FindChildByValue(value, false);
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000B5D8C File Offset: 0x000B3F8C
		public MultiColumnComboBoxItem FindChildByAttribute(string attributeName, string attributeValue)
		{
			foreach (MultiColumnComboBoxItem multiColumnComboBoxItem in this)
			{
				if (multiColumnComboBoxItem.Attributes[attributeName] == attributeValue)
				{
					return multiColumnComboBoxItem;
				}
			}
			return null;
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000B5DE8 File Offset: 0x000B3FE8
		public MultiColumnComboBoxItem FindChildByValue(string value, bool ignoreCase)
		{
			foreach (MultiColumnComboBoxItem multiColumnComboBoxItem in this)
			{
				if (string.Compare(multiColumnComboBoxItem.Value, value, ignoreCase) == 0)
				{
					return multiColumnComboBoxItem;
				}
			}
			return null;
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000B5E40 File Offset: 0x000B4040
		public MultiColumnComboBoxItem FindChildByText(string text, bool ignoreCase)
		{
			foreach (MultiColumnComboBoxItem multiColumnComboBoxItem in this)
			{
				if (string.Compare(multiColumnComboBoxItem.Text, text, ignoreCase) == 0)
				{
					return multiColumnComboBoxItem;
				}
			}
			return null;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B5E98 File Offset: 0x000B4098
		void ICollection<MultiColumnComboBoxItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000B5EA0 File Offset: 0x000B40A0
		int ICollection<MultiColumnComboBoxItem>.get_Count()
		{
			return base.Count;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x020005F9 RID: 1529
	public class MultiSelectItemCollection : BaseCollection<MultiSelectItem>, IList<MultiSelectItem>, ICollection<MultiSelectItem>, IEnumerable<MultiSelectItem>, IEnumerable
	{
		// Token: 0x06003744 RID: 14148 RVA: 0x000B6FA1 File Offset: 0x000B51A1
		public MultiSelectItemCollection()
		{
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000B6FA9 File Offset: 0x000B51A9
		public MultiSelectItemCollection(RadMultiSelect owner) : base(owner)
		{
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000B6FB4 File Offset: 0x000B51B4
		protected void SetOwner(MultiSelectItem item)
		{
			RadMultiSelect owner = item.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Owner)
			{
				owner.Items.Remove(item);
			}
			item.Owner = base.Owner;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000B6FFC File Offset: 0x000B51FC
		public override void AddRange(IEnumerable<MultiSelectItem> items)
		{
			foreach (MultiSelectItem entity in items)
			{
				this.Add(entity);
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000B7044 File Offset: 0x000B5244
		public override void Add(MultiSelectItem item)
		{
			base.Add(item);
			if (base.Owner != null)
			{
				item.Owner = base.Owner;
			}
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000B7064 File Offset: 0x000B5264
		protected internal virtual IList<MultiSelectItem> ToList()
		{
			List<MultiSelectItem> list = new List<MultiSelectItem>();
			foreach (MultiSelectItem item in this)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000B70B4 File Offset: 0x000B52B4
		public void RemoveAt(int index)
		{
			this.Remove(base[index]);
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000B70C3 File Offset: 0x000B52C3
		bool ICollection<MultiSelectItem>.Contains(MultiSelectItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000B70CC File Offset: 0x000B52CC
		void ICollection<MultiSelectItem>.CopyTo(MultiSelectItem[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000B70D6 File Offset: 0x000B52D6
		bool ICollection<MultiSelectItem>.Remove(MultiSelectItem item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000B70E0 File Offset: 0x000B52E0
		bool ICollection<MultiSelectItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000B70E3 File Offset: 0x000B52E3
		int IList<MultiSelectItem>.IndexOf(MultiSelectItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000B70EC File Offset: 0x000B52EC
		void IList<MultiSelectItem>.Insert(int index, MultiSelectItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000B70F6 File Offset: 0x000B52F6
		void IList<MultiSelectItem>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000B722C File Offset: 0x000B542C
		IEnumerator<MultiSelectItem> IEnumerable<MultiSelectItem>.GetEnumerator()
		{
			foreach (MultiSelectItem item in this)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000B7248 File Offset: 0x000B5448
		public MultiSelectItem FindChild(Predicate<MultiSelectItem> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (MultiSelectItem multiSelectItem in this)
			{
				if (match(multiSelectItem))
				{
					return multiSelectItem;
				}
			}
			return null;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000B72A8 File Offset: 0x000B54A8
		public MultiSelectItem FindChildByText(string text)
		{
			return this.FindChildByText(text, false);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000B72B2 File Offset: 0x000B54B2
		public MultiSelectItem FindChildByValue(string value)
		{
			return this.FindChildByValue(value, false);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000B72BC File Offset: 0x000B54BC
		public MultiSelectItem FindChildByAttribute(string attributeName, string attributeValue)
		{
			foreach (MultiSelectItem multiSelectItem in this)
			{
				if (multiSelectItem.Attributes[attributeName] == attributeValue)
				{
					return multiSelectItem;
				}
			}
			return null;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000B7318 File Offset: 0x000B5518
		public MultiSelectItem FindChildByValue(string value, bool ignoreCase)
		{
			foreach (MultiSelectItem multiSelectItem in this)
			{
				if (string.Compare(multiSelectItem.Value, value, ignoreCase) == 0)
				{
					return multiSelectItem;
				}
			}
			return null;
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000B7370 File Offset: 0x000B5570
		public MultiSelectItem FindChildByText(string text, bool ignoreCase)
		{
			foreach (MultiSelectItem multiSelectItem in this)
			{
				if (string.Compare(multiSelectItem.Text, text, ignoreCase) == 0)
				{
					return multiSelectItem;
				}
			}
			return null;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000B73C8 File Offset: 0x000B55C8
		void ICollection<MultiSelectItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000B73D0 File Offset: 0x000B55D0
		int ICollection<MultiSelectItem>.get_Count()
		{
			return base.Count;
		}
	}
}

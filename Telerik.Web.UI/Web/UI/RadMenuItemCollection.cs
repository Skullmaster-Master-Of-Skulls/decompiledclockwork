using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011BC RID: 4540
	public class RadMenuItemCollection : ControlItemCollection, IList<RadMenuItem>, ICollection<RadMenuItem>, IEnumerable<RadMenuItem>, IEnumerable
	{
		// Token: 0x0600BAB2 RID: 47794 RVA: 0x00298B7C File Offset: 0x00296D7C
		public RadMenuItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x17003C40 RID: 15424
		public RadMenuItem this[int index]
		{
			get
			{
				return (RadMenuItem)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x0600BAB5 RID: 47797 RVA: 0x00298B9D File Offset: 0x00296D9D
		public virtual void Add(RadMenuItem item)
		{
			base.Add(item);
		}

		// Token: 0x0600BAB6 RID: 47798 RVA: 0x00298BA6 File Offset: 0x00296DA6
		public RadMenuItem FindItemByText(string text)
		{
			return base.FindChildByText<RadMenuItem>(text);
		}

		// Token: 0x0600BAB7 RID: 47799 RVA: 0x00298BAF File Offset: 0x00296DAF
		public RadMenuItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadMenuItem>(text, ignoreCase);
		}

		// Token: 0x0600BAB8 RID: 47800 RVA: 0x00298BB9 File Offset: 0x00296DB9
		public RadMenuItem FindItemByValue(string value)
		{
			return base.FindChildByValue<RadMenuItem>(value);
		}

		// Token: 0x0600BAB9 RID: 47801 RVA: 0x00298BC2 File Offset: 0x00296DC2
		public RadMenuItem FindItemByValue(string value, bool ignoreCase)
		{
			return base.FindChildByValue<RadMenuItem>(value, ignoreCase);
		}

		// Token: 0x0600BABA RID: 47802 RVA: 0x00298BCC File Offset: 0x00296DCC
		public RadMenuItem FindItemByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadMenuItem>(attributeName, attributeValue);
		}

		// Token: 0x0600BABB RID: 47803 RVA: 0x00298BD6 File Offset: 0x00296DD6
		public RadMenuItem FindItem(Predicate<RadMenuItem> match)
		{
			return base.FindChild<RadMenuItem>(match);
		}

		// Token: 0x0600BABC RID: 47804 RVA: 0x00298BDF File Offset: 0x00296DDF
		public virtual bool Contains(RadMenuItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x0600BABD RID: 47805 RVA: 0x00298BE8 File Offset: 0x00296DE8
		public virtual void CopyTo(RadMenuItem[] array, int index)
		{
			base.CopyTo(array, index);
		}

		// Token: 0x0600BABE RID: 47806 RVA: 0x00298BF4 File Offset: 0x00296DF4
		public virtual void AddRange(IEnumerable<RadMenuItem> items)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadMenuItem item in items)
			{
				list.Add(item);
			}
			base.AddRange(list);
		}

		// Token: 0x0600BABF RID: 47807 RVA: 0x00298C4C File Offset: 0x00296E4C
		public virtual int IndexOf(RadMenuItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x0600BAC0 RID: 47808 RVA: 0x00298C55 File Offset: 0x00296E55
		public virtual void Insert(int index, RadMenuItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x0600BAC1 RID: 47809 RVA: 0x00298C5F File Offset: 0x00296E5F
		public virtual void Remove(RadMenuItem item)
		{
			base.Remove(item);
			item.Owner = null;
		}

		// Token: 0x0600BAC2 RID: 47810 RVA: 0x00298C6F File Offset: 0x00296E6F
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x0600BAC3 RID: 47811 RVA: 0x00298C80 File Offset: 0x00296E80
		protected override void SetOwner(ControlItem item)
		{
			RadMenuItem radMenuItem = item as RadMenuItem;
			IRadMenuItemContainer owner = radMenuItem.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Parent)
			{
				owner.Items.Remove(item);
			}
			radMenuItem.Owner = (IRadMenuItemContainer)base.Parent;
		}

		// Token: 0x0600BAC4 RID: 47812 RVA: 0x00298CD2 File Offset: 0x00296ED2
		int IList<RadMenuItem>.IndexOf(RadMenuItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600BAC5 RID: 47813 RVA: 0x00298CD9 File Offset: 0x00296ED9
		void IList<RadMenuItem>.Insert(int index, RadMenuItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600BAC6 RID: 47814 RVA: 0x00298CE0 File Offset: 0x00296EE0
		void IList<RadMenuItem>.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17003C41 RID: 15425
		RadMenuItem IList<RadMenuItem>.this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600BAC9 RID: 47817 RVA: 0x00298CF5 File Offset: 0x00296EF5
		void ICollection<RadMenuItem>.Add(RadMenuItem item)
		{
			this.Add(item);
		}

		// Token: 0x0600BACA RID: 47818 RVA: 0x00298CFE File Offset: 0x00296EFE
		void ICollection<RadMenuItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x0600BACB RID: 47819 RVA: 0x00298D06 File Offset: 0x00296F06
		bool ICollection<RadMenuItem>.Contains(RadMenuItem item)
		{
			return this.Contains(item);
		}

		// Token: 0x0600BACC RID: 47820 RVA: 0x00298D0F File Offset: 0x00296F0F
		void ICollection<RadMenuItem>.CopyTo(RadMenuItem[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x17003C42 RID: 15426
		// (get) Token: 0x0600BACD RID: 47821 RVA: 0x00298D19 File Offset: 0x00296F19
		int ICollection<RadMenuItem>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x17003C43 RID: 15427
		// (get) Token: 0x0600BACE RID: 47822 RVA: 0x00298D21 File Offset: 0x00296F21
		bool ICollection<RadMenuItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600BACF RID: 47823 RVA: 0x00298D24 File Offset: 0x00296F24
		bool ICollection<RadMenuItem>.Remove(RadMenuItem item)
		{
			bool result = this.Contains(item);
			this.Remove(item);
			return result;
		}

		// Token: 0x0600BAD0 RID: 47824 RVA: 0x00298E84 File Offset: 0x00297084
		IEnumerator<RadMenuItem> IEnumerable<RadMenuItem>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				RadMenuItem menuItem = (RadMenuItem)obj;
				yield return menuItem;
			}
			yield break;
		}

		// Token: 0x0600BAD1 RID: 47825 RVA: 0x00298FE0 File Offset: 0x002971E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (object obj in this)
			{
				RadMenuItem menuItem = (RadMenuItem)obj;
				yield return menuItem;
			}
			yield break;
		}
	}
}

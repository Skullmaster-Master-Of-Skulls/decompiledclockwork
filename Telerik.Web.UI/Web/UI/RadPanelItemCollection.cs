using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B4C RID: 6988
	public class RadPanelItemCollection : ControlItemCollection
	{
		// Token: 0x06010EBE RID: 69310 RVA: 0x003BFA8E File Offset: 0x003BDC8E
		public RadPanelItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x170052A2 RID: 21154
		public RadPanelItem this[int index]
		{
			get
			{
				return (RadPanelItem)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06010EC1 RID: 69313 RVA: 0x003BFAAF File Offset: 0x003BDCAF
		public void Add(RadPanelItem item)
		{
			base.Add(item);
		}

		// Token: 0x06010EC2 RID: 69314 RVA: 0x003BFAB8 File Offset: 0x003BDCB8
		public void AddRange(RadPanelItem[] items)
		{
			foreach (RadPanelItem item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x06010EC3 RID: 69315 RVA: 0x003BFAE0 File Offset: 0x003BDCE0
		public void Remove(RadPanelItem item)
		{
			base.Remove(item);
			item.Owner = null;
		}

		// Token: 0x06010EC4 RID: 69316 RVA: 0x003BFAF0 File Offset: 0x003BDCF0
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06010EC5 RID: 69317 RVA: 0x003BFAFF File Offset: 0x003BDCFF
		public int IndexOf(RadPanelItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06010EC6 RID: 69318 RVA: 0x003BFB08 File Offset: 0x003BDD08
		public bool Contains(RadPanelItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x06010EC7 RID: 69319 RVA: 0x003BFB11 File Offset: 0x003BDD11
		public void Insert(int index, RadPanelItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06010EC8 RID: 69320 RVA: 0x003BFB1B File Offset: 0x003BDD1B
		public RadPanelItem FindItemByText(string text)
		{
			return base.FindChildByText<RadPanelItem>(text);
		}

		// Token: 0x06010EC9 RID: 69321 RVA: 0x003BFB24 File Offset: 0x003BDD24
		public RadPanelItem FindItemByValue(string value)
		{
			return base.FindChildByValue<RadPanelItem>(value);
		}

		// Token: 0x06010ECA RID: 69322 RVA: 0x003BFB2D File Offset: 0x003BDD2D
		public RadPanelItem FindItemByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadPanelItem>(attributeName, attributeValue);
		}

		// Token: 0x06010ECB RID: 69323 RVA: 0x003BFB37 File Offset: 0x003BDD37
		public RadPanelItem FindItem(Predicate<RadPanelItem> match)
		{
			return base.FindChild<RadPanelItem>(match);
		}

		// Token: 0x06010ECC RID: 69324 RVA: 0x003BFB40 File Offset: 0x003BDD40
		public RadPanelItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadPanelItem>(text, ignoreCase);
		}

		// Token: 0x06010ECD RID: 69325 RVA: 0x003BFB4A File Offset: 0x003BDD4A
		public RadPanelItem FindItemByValue(string value, bool ignoreCase)
		{
			return base.FindChildByValue<RadPanelItem>(value, ignoreCase);
		}

		// Token: 0x06010ECE RID: 69326 RVA: 0x003BFB54 File Offset: 0x003BDD54
		protected override void SetOwner(ControlItem item)
		{
			RadPanelItem radPanelItem = item as RadPanelItem;
			IRadPanelItemContainer owner = radPanelItem.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Parent)
			{
				owner.Items.Remove(item);
			}
			radPanelItem.Owner = (IRadPanelItemContainer)base.Parent;
			radPanelItem.ApplySelection();
		}
	}
}

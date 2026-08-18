using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B53 RID: 6995
	public class RadToolBarButtonCollection : RadToolBarItemCollection
	{
		// Token: 0x06010F2D RID: 69421 RVA: 0x003C0790 File Offset: 0x003BE990
		public RadToolBarButtonCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x170052C1 RID: 21185
		public RadToolBarButton this[int index]
		{
			get
			{
				return (RadToolBarButton)base[index];
			}
		}

		// Token: 0x06010F2F RID: 69423 RVA: 0x003C07A7 File Offset: 0x003BE9A7
		public void Add(RadToolBarButton item)
		{
			base.Add(item);
		}

		// Token: 0x06010F30 RID: 69424 RVA: 0x003C07B0 File Offset: 0x003BE9B0
		public RadToolBarButton FindButtonByText(string text)
		{
			return base.FindChildByText<RadToolBarButton>(text);
		}

		// Token: 0x06010F31 RID: 69425 RVA: 0x003C07B9 File Offset: 0x003BE9B9
		public RadToolBarButton FindButtonByValue(string value)
		{
			return base.FindChildByValue<RadToolBarButton>(value);
		}

		// Token: 0x06010F32 RID: 69426 RVA: 0x003C07C2 File Offset: 0x003BE9C2
		public RadToolBarButton FindButtonByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadToolBarButton>(attributeName, attributeValue);
		}

		// Token: 0x06010F33 RID: 69427 RVA: 0x003C07CC File Offset: 0x003BE9CC
		public bool Contains(RadToolBarButton button)
		{
			return base.Contains(button);
		}

		// Token: 0x06010F34 RID: 69428 RVA: 0x003C07D8 File Offset: 0x003BE9D8
		public void AddRange(IEnumerable<RadToolBarButton> buttons)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadToolBarButton item in buttons)
			{
				list.Add(item);
			}
			base.AddRange(list);
		}

		// Token: 0x06010F35 RID: 69429 RVA: 0x003C0830 File Offset: 0x003BEA30
		public int IndexOf(RadToolBarButton button)
		{
			return base.IndexOf(button);
		}

		// Token: 0x06010F36 RID: 69430 RVA: 0x003C0839 File Offset: 0x003BEA39
		public void Insert(int index, RadToolBarButton button)
		{
			base.Insert(index, button);
		}

		// Token: 0x06010F37 RID: 69431 RVA: 0x003C0843 File Offset: 0x003BEA43
		public void Remove(RadToolBarButton button)
		{
			base.Remove(button);
		}

		// Token: 0x06010F38 RID: 69432 RVA: 0x003C084C File Offset: 0x003BEA4C
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06010F39 RID: 69433 RVA: 0x003C085C File Offset: 0x003BEA5C
		protected override void SetOwner(ControlItem item)
		{
			RadToolBarButton radToolBarButton = item as RadToolBarButton;
			IControlItemContainer owner = radToolBarButton.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Parent)
			{
				owner.Items.Remove(item);
			}
			radToolBarButton.Owner = (IRadToolBarItemContainer)base.Parent;
		}
	}
}

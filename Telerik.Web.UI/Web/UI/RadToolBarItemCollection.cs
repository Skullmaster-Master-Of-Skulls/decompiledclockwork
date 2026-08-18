using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B52 RID: 6994
	public class RadToolBarItemCollection : ControlItemCollection
	{
		// Token: 0x06010F1D RID: 69405 RVA: 0x003C064D File Offset: 0x003BE84D
		public RadToolBarItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x170052C0 RID: 21184
		public RadToolBarItem this[int index]
		{
			get
			{
				return (RadToolBarItem)base[index];
			}
		}

		// Token: 0x06010F1F RID: 69407 RVA: 0x003C0664 File Offset: 0x003BE864
		public void Add(RadToolBarItem item)
		{
			base.Add(item);
		}

		// Token: 0x06010F20 RID: 69408 RVA: 0x003C066D File Offset: 0x003BE86D
		public RadToolBarItem FindItemByText(string text)
		{
			return base.FindChildByText<RadToolBarItem>(text);
		}

		// Token: 0x06010F21 RID: 69409 RVA: 0x003C0676 File Offset: 0x003BE876
		public RadToolBarItem FindItemByValue(string value)
		{
			return base.FindChildByValue<RadToolBarItem>(value);
		}

		// Token: 0x06010F22 RID: 69410 RVA: 0x003C067F File Offset: 0x003BE87F
		public RadToolBarItem FindItemByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadToolBarItem>(text, ignoreCase);
		}

		// Token: 0x06010F23 RID: 69411 RVA: 0x003C0689 File Offset: 0x003BE889
		public RadToolBarItem FindItemByValue(string value, bool ignoreCase)
		{
			return base.FindChildByValue<RadToolBarItem>(value, ignoreCase);
		}

		// Token: 0x06010F24 RID: 69412 RVA: 0x003C0693 File Offset: 0x003BE893
		public RadToolBarItem FindItemByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadToolBarItem>(attributeName, attributeValue);
		}

		// Token: 0x06010F25 RID: 69413 RVA: 0x003C069D File Offset: 0x003BE89D
		public RadToolBarItem FindItem(Predicate<RadToolBarItem> match)
		{
			return base.FindChild<RadToolBarItem>(match);
		}

		// Token: 0x06010F26 RID: 69414 RVA: 0x003C06A6 File Offset: 0x003BE8A6
		public bool Contains(RadToolBarItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x06010F27 RID: 69415 RVA: 0x003C06B0 File Offset: 0x003BE8B0
		public void AddRange(IEnumerable<RadToolBarItem> items)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadToolBarItem item in items)
			{
				list.Add(item);
			}
			base.AddRange(list);
		}

		// Token: 0x06010F28 RID: 69416 RVA: 0x003C0708 File Offset: 0x003BE908
		public int IndexOf(RadToolBarItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06010F29 RID: 69417 RVA: 0x003C0711 File Offset: 0x003BE911
		public void Insert(int index, RadToolBarItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06010F2A RID: 69418 RVA: 0x003C071B File Offset: 0x003BE91B
		public void Remove(RadToolBarItem item)
		{
			base.Remove(item);
		}

		// Token: 0x06010F2B RID: 69419 RVA: 0x003C0724 File Offset: 0x003BE924
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06010F2C RID: 69420 RVA: 0x003C0734 File Offset: 0x003BE934
		protected override void SetOwner(ControlItem item)
		{
			RadToolBarItem radToolBarItem = item as RadToolBarItem;
			RadToolBarButton radToolBarButton = item as RadToolBarButton;
			IRadToolBarItemContainer toolBar = radToolBarItem.ToolBar;
			if (toolBar != null && toolBar.Items.Contains(item) && toolBar != base.Parent)
			{
				toolBar.Items.Remove(item);
			}
			if (radToolBarButton != null)
			{
				radToolBarButton.Owner = (RadToolBar)base.Parent;
			}
		}
	}
}

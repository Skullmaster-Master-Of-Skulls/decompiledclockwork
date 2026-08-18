using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001ADC RID: 6876
	public class RadTabCollection : ControlItemCollection, IEnumerable<RadTab>, IEnumerable
	{
		// Token: 0x1700511F RID: 20767
		// (get) Token: 0x06010AAC RID: 68268 RVA: 0x003B6EE0 File Offset: 0x003B50E0
		internal IRadTabContainer Owner
		{
			get
			{
				return base.Parent as IRadTabContainer;
			}
		}

		// Token: 0x06010AAD RID: 68269 RVA: 0x003B6EED File Offset: 0x003B50ED
		public RadTabCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x06010AAE RID: 68270 RVA: 0x003B6EF6 File Offset: 0x003B50F6
		public void Add(RadTab tab)
		{
			base.Add(tab);
		}

		// Token: 0x06010AAF RID: 68271 RVA: 0x003B6F00 File Offset: 0x003B5100
		public void AddRange(RadTab[] tabs)
		{
			foreach (RadTab tab in tabs)
			{
				this.Add(tab);
			}
		}

		// Token: 0x06010AB0 RID: 68272 RVA: 0x003B6F28 File Offset: 0x003B5128
		public void Insert(int index, RadTab tab)
		{
			base.Insert(index, tab);
		}

		// Token: 0x06010AB1 RID: 68273 RVA: 0x003B6F32 File Offset: 0x003B5132
		public int IndexOf(RadTab tab)
		{
			return base.IndexOf(tab);
		}

		// Token: 0x06010AB2 RID: 68274 RVA: 0x003B6F3B File Offset: 0x003B513B
		public bool Contains(RadTab tab)
		{
			return base.Contains(tab);
		}

		// Token: 0x06010AB3 RID: 68275 RVA: 0x003B6F44 File Offset: 0x003B5144
		public void Remove(RadTab tab)
		{
			base.Remove(tab);
			tab.Owner = null;
		}

		// Token: 0x06010AB4 RID: 68276 RVA: 0x003B6F54 File Offset: 0x003B5154
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06010AB5 RID: 68277 RVA: 0x003B6F63 File Offset: 0x003B5163
		public RadTab FindTabByValue(string value)
		{
			return base.FindChildByValue<RadTab>(value);
		}

		// Token: 0x06010AB6 RID: 68278 RVA: 0x003B6F6C File Offset: 0x003B516C
		public RadTab FindTabByValue(string value, bool ignoreCase)
		{
			return base.FindChildByValue<RadTab>(value, ignoreCase);
		}

		// Token: 0x06010AB7 RID: 68279 RVA: 0x003B6F76 File Offset: 0x003B5176
		public RadTab FindTabByText(string text)
		{
			return base.FindChildByText<RadTab>(text);
		}

		// Token: 0x06010AB8 RID: 68280 RVA: 0x003B6F7F File Offset: 0x003B517F
		public RadTab FindTabByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadTab>(text, ignoreCase);
		}

		// Token: 0x06010AB9 RID: 68281 RVA: 0x003B6F89 File Offset: 0x003B5189
		public RadTab FindTab(Predicate<RadTab> match)
		{
			return base.FindChild<RadTab>(match);
		}

		// Token: 0x17005120 RID: 20768
		public RadTab this[int index]
		{
			get
			{
				return (RadTab)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06010ABC RID: 68284 RVA: 0x003B70EC File Offset: 0x003B52EC
		IEnumerator<RadTab> IEnumerable<RadTab>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				RadTab tab = (RadTab)obj;
				yield return tab;
			}
			yield break;
		}

		// Token: 0x06010ABD RID: 68285 RVA: 0x003B7108 File Offset: 0x003B5308
		protected override void OnInsert(int index, object value)
		{
			this._cachedSelectedTab = this.Owner.SelectedTab;
			base.OnInsert(index, value);
		}

		// Token: 0x06010ABE RID: 68286 RVA: 0x003B7123 File Offset: 0x003B5323
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			if (this._cachedSelectedTab != null)
			{
				this._cachedSelectedTab.Selected = true;
			}
		}

		// Token: 0x06010ABF RID: 68287 RVA: 0x003B7144 File Offset: 0x003B5344
		protected override void SetOwner(ControlItem item)
		{
			RadTab radTab = (RadTab)item;
			if (radTab.Owner != null && radTab.Owner.Tabs.Contains(radTab) && radTab.Owner != base.Parent)
			{
				radTab.Owner.Tabs.Remove(radTab);
			}
			radTab.Owner = (IRadTabContainer)base.Parent;
			radTab.ApplySelection();
		}

		// Token: 0x04004A61 RID: 19041
		private RadTab _cachedSelectedTab;
	}
}

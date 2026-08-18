using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F4A RID: 3914
	public class RibbonBarTabCollection : List<RibbonBarTab>, IRibbonBarSubComponent
	{
		// Token: 0x17002F43 RID: 12099
		// (get) Token: 0x06009550 RID: 38224 RVA: 0x0021615F File Offset: 0x0021435F
		// (set) Token: 0x06009551 RID: 38225 RVA: 0x00216167 File Offset: 0x00214367
		public RadRibbonBar RibbonBar
		{
			get
			{
				return this._ribbonBar;
			}
			internal set
			{
				this._ribbonBar = value;
			}
		}

		// Token: 0x17002F44 RID: 12100
		// (get) Token: 0x06009552 RID: 38226 RVA: 0x00216170 File Offset: 0x00214370
		// (set) Token: 0x06009553 RID: 38227 RVA: 0x00216178 File Offset: 0x00214378
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (RibbonBarTab ribbonBarTab in this)
				{
					ribbonBarTab.ParentWebControl = this._parentWebControl;
				}
			}
		}

		// Token: 0x17002F45 RID: 12101
		// (set) Token: 0x06009554 RID: 38228 RVA: 0x002161D4 File Offset: 0x002143D4
		internal RibbonBarContextualTabGroup ContextualTabGroup
		{
			set
			{
				this._contextualTabGroup = value;
			}
		}

		// Token: 0x06009555 RID: 38229 RVA: 0x002161DD File Offset: 0x002143DD
		public new void Add(RibbonBarTab tab)
		{
			base.Add(tab);
			this.OnTabAdded(tab);
		}

		// Token: 0x06009556 RID: 38230 RVA: 0x002161F0 File Offset: 0x002143F0
		public new void AddRange(IEnumerable<RibbonBarTab> collection)
		{
			base.AddRange(collection);
			foreach (RibbonBarTab tab in collection)
			{
				this.OnTabAdded(tab);
			}
		}

		// Token: 0x06009557 RID: 38231 RVA: 0x00216240 File Offset: 0x00214440
		public new void Insert(int index, RibbonBarTab tab)
		{
			base.Insert(index, tab);
			this.OnTabAdded(tab);
		}

		// Token: 0x06009558 RID: 38232 RVA: 0x00216254 File Offset: 0x00214454
		public new void InsertRange(int index, IEnumerable<RibbonBarTab> collection)
		{
			base.InsertRange(index, collection);
			foreach (RibbonBarTab tab in collection)
			{
				this.OnTabAdded(tab);
			}
		}

		// Token: 0x06009559 RID: 38233 RVA: 0x002162A4 File Offset: 0x002144A4
		public new void Remove(RibbonBarTab tab)
		{
			base.Remove(tab);
			tab.Container = null;
			tab.ParentWebControl = null;
			tab.ContextualTabGroup = null;
		}

		// Token: 0x0600955A RID: 38234 RVA: 0x002162C3 File Offset: 0x002144C3
		private void OnTabAdded(RibbonBarTab tab)
		{
			tab.Container = this;
			tab.ParentWebControl = this.ParentWebControl;
			tab.ContextualTabGroup = this._contextualTabGroup;
		}

		// Token: 0x04002AB9 RID: 10937
		private RadRibbonBar _ribbonBar;

		// Token: 0x04002ABA RID: 10938
		private WebControl _parentWebControl;

		// Token: 0x04002ABB RID: 10939
		private RibbonBarContextualTabGroup _contextualTabGroup;
	}
}

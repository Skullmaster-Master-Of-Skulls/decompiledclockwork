using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E5D RID: 3677
	[ToolboxItem(false)]
	[ParseChildren(typeof(RibbonBarTab), ChildrenAsProperties = true, DefaultProperty = "Tabs")]
	public class RibbonBarContextualTabGroup : IRibbonBarSubComponent, IStateManager
	{
		// Token: 0x17002C18 RID: 11288
		// (get) Token: 0x06008B82 RID: 35714 RVA: 0x001FBBE2 File Offset: 0x001F9DE2
		private StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
				}
				return this._viewState;
			}
		}

		// Token: 0x06008B83 RID: 35715 RVA: 0x001FBBFD File Offset: 0x001F9DFD
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.ViewState).LoadViewState(savedState);
			}
		}

		// Token: 0x06008B84 RID: 35716 RVA: 0x001FBC0E File Offset: 0x001F9E0E
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.ViewState).SaveViewState();
		}

		// Token: 0x06008B85 RID: 35717 RVA: 0x001FBC1B File Offset: 0x001F9E1B
		void IStateManager.TrackViewState()
		{
			((IStateManager)this.ViewState).TrackViewState();
		}

		// Token: 0x17002C19 RID: 11289
		// (get) Token: 0x06008B86 RID: 35718 RVA: 0x001FBC28 File Offset: 0x001F9E28
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this.ViewState).IsTrackingViewState;
			}
		}

		// Token: 0x17002C1A RID: 11290
		// (get) Token: 0x06008B87 RID: 35719 RVA: 0x001FBC35 File Offset: 0x001F9E35
		// (set) Token: 0x06008B88 RID: 35720 RVA: 0x001FBC55 File Offset: 0x001F9E55
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002C1B RID: 11291
		// (get) Token: 0x06008B89 RID: 35721 RVA: 0x001FBC68 File Offset: 0x001F9E68
		// (set) Token: 0x06008B8A RID: 35722 RVA: 0x001FBC8D File Offset: 0x001F9E8D
		[DefaultValue("")]
		public Color BackColor
		{
			get
			{
				return (Color)(this.ViewState["BackColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["BackColor"] = value;
			}
		}

		// Token: 0x17002C1C RID: 11292
		// (get) Token: 0x06008B8B RID: 35723 RVA: 0x001FBCA5 File Offset: 0x001F9EA5
		// (set) Token: 0x06008B8C RID: 35724 RVA: 0x001FBCCA File Offset: 0x001F9ECA
		[DefaultValue("")]
		public Color ForeColor
		{
			get
			{
				return (Color)(this.ViewState["ForeColor"] ?? Color.Empty);
			}
			set
			{
				this.ViewState["ForeColor"] = value;
			}
		}

		// Token: 0x17002C1D RID: 11293
		// (get) Token: 0x06008B8D RID: 35725 RVA: 0x001FBCE2 File Offset: 0x001F9EE2
		// (set) Token: 0x06008B8E RID: 35726 RVA: 0x001FBD02 File Offset: 0x001F9F02
		[DefaultValue("")]
		public string CssClass
		{
			get
			{
				return (string)(this.ViewState["CssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x17002C1E RID: 11294
		// (get) Token: 0x06008B8F RID: 35727 RVA: 0x001FBD15 File Offset: 0x001F9F15
		// (set) Token: 0x06008B90 RID: 35728 RVA: 0x001FBD36 File Offset: 0x001F9F36
		[DefaultValue(false)]
		public bool Active
		{
			get
			{
				return (bool)(this.ViewState["Active"] ?? false);
			}
			set
			{
				this.ViewState["Active"] = value;
			}
		}

		// Token: 0x17002C1F RID: 11295
		// (get) Token: 0x06008B91 RID: 35729 RVA: 0x001FBD4E File Offset: 0x001F9F4E
		// (set) Token: 0x06008B92 RID: 35730 RVA: 0x001FBD56 File Offset: 0x001F9F56
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002C20 RID: 11296
		// (get) Token: 0x06008B93 RID: 35731 RVA: 0x001FBD5F File Offset: 0x001F9F5F
		public RadRibbonBar RibbonBar
		{
			get
			{
				if (this.Container == null)
				{
					return null;
				}
				return this.Container.RibbonBar;
			}
		}

		// Token: 0x17002C21 RID: 11297
		// (get) Token: 0x06008B94 RID: 35732 RVA: 0x001FBD76 File Offset: 0x001F9F76
		// (set) Token: 0x06008B95 RID: 35733 RVA: 0x001FBD7E File Offset: 0x001F9F7E
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				this.Tabs.ParentWebControl = value;
			}
		}

		// Token: 0x17002C22 RID: 11298
		// (get) Token: 0x06008B96 RID: 35734 RVA: 0x001FBD93 File Offset: 0x001F9F93
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarTabCollection Tabs
		{
			get
			{
				if (this._tabs == null)
				{
					this._tabs = new RibbonBarTabCollection();
					this._tabs.ContextualTabGroup = this;
				}
				return this._tabs;
			}
		}

		// Token: 0x06008B97 RID: 35735 RVA: 0x001FBDBC File Offset: 0x001F9FBC
		public List<RibbonBarTab> GetVisibleTabs()
		{
			List<RibbonBarTab> list = new List<RibbonBarTab>();
			foreach (RibbonBarTab ribbonBarTab in this.Tabs)
			{
				if (ribbonBarTab.Visible)
				{
					list.Add(ribbonBarTab);
				}
			}
			return list;
		}

		// Token: 0x04002713 RID: 10003
		private StateBag _viewState;

		// Token: 0x04002714 RID: 10004
		private WebControl _parentWebControl;

		// Token: 0x04002715 RID: 10005
		private RibbonBarTabCollection _tabs;
	}
}

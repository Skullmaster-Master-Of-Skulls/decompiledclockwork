using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005E2 RID: 1506
	[ParseChildren(true, "ChildItems")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItem : IStateManager, ICloneable
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x0012F28A File Offset: 0x0012E28A
		public MenuItem()
		{
			this._selectDesired = 0;
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0012F2AC File Offset: 0x0012E2AC
		internal MenuItem(Menu owner, bool isRoot) : this()
		{
			this._owner = owner;
			this._isRoot = isRoot;
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x0012F2C2 File Offset: 0x0012E2C2
		public MenuItem(string text) : this(text, null, null, null, null)
		{
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0012F2CF File Offset: 0x0012E2CF
		public MenuItem(string text, string value) : this(text, value, null, null, null)
		{
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0012F2DC File Offset: 0x0012E2DC
		public MenuItem(string text, string value, string imageUrl) : this(text, value, imageUrl, null, null)
		{
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0012F2E9 File Offset: 0x0012E2E9
		public MenuItem(string text, string value, string imageUrl, string navigateUrl) : this(text, value, imageUrl, navigateUrl, null)
		{
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0012F2F8 File Offset: 0x0012E2F8
		public MenuItem(string text, string value, string imageUrl, string navigateUrl, string target) : this()
		{
			if (text != null)
			{
				this.Text = text;
			}
			if (value != null)
			{
				this.Value = value;
			}
			if (!string.IsNullOrEmpty(imageUrl))
			{
				this.ImageUrl = imageUrl;
			}
			if (!string.IsNullOrEmpty(navigateUrl))
			{
				this.NavigateUrl = navigateUrl;
			}
			if (!string.IsNullOrEmpty(target))
			{
				this.Target = target;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x0012F350 File Offset: 0x0012E350
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[MergableProperty(false)]
		[Browsable(false)]
		public MenuItemCollection ChildItems
		{
			get
			{
				if (this._childItems == null)
				{
					this._childItems = new MenuItemCollection(this);
				}
				return this._childItems;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x0012F36C File Offset: 0x0012E36C
		// (set) Token: 0x06004A3D RID: 19005 RVA: 0x0012F374 File Offset: 0x0012E374
		internal MenuItemTemplateContainer Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x0012F380 File Offset: 0x0012E380
		[DefaultValue(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DataBound
		{
			get
			{
				object obj = this.ViewState["DataBound"];
				return obj != null && (bool)obj;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06004A3F RID: 19007 RVA: 0x0012F3AC File Offset: 0x0012E3AC
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DataPath
		{
			get
			{
				object obj = this.ViewState["DataPath"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x0012F3D9 File Offset: 0x0012E3D9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Depth
		{
			get
			{
				if (this._depth == -2)
				{
					if (this._isRoot)
					{
						return -1;
					}
					if (this.Parent == null)
					{
						return 0;
					}
					this._depth = this.Parent.Depth + 1;
				}
				return this._depth;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x0012F414 File Offset: 0x0012E414
		[Browsable(false)]
		[DefaultValue(null)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x0012F41C File Offset: 0x0012E41C
		// (set) Token: 0x06004A43 RID: 19011 RVA: 0x0012F445 File Offset: 0x0012E445
		[DefaultValue(true)]
		[Browsable(true)]
		[WebSysDescription("MenuItem_Enabled")]
		public bool Enabled
		{
			get
			{
				object obj = this.ViewState["Enabled"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x0012F460 File Offset: 0x0012E460
		internal string FormattedText
		{
			get
			{
				if (this._owner.StaticItemFormatString.Length > 0 && this.Depth < this._owner.StaticDisplayLevels)
				{
					return string.Format(CultureInfo.CurrentCulture, this._owner.StaticItemFormatString, new object[]
					{
						this.Text
					});
				}
				if (this._owner.DynamicItemFormatString.Length > 0 && this.Depth >= this._owner.StaticDisplayLevels)
				{
					return string.Format(CultureInfo.CurrentCulture, this._owner.DynamicItemFormatString, new object[]
					{
						this.Text
					});
				}
				return this.Text;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06004A45 RID: 19013 RVA: 0x0012F510 File Offset: 0x0012E510
		internal string Id
		{
			get
			{
				if (this._id.Length == 0)
				{
					this.Index = this._owner.CreateItemIndex();
					this._id = this._owner.ClientID + 'n' + this.Index;
				}
				return this._id;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06004A46 RID: 19014 RVA: 0x0012F56C File Offset: 0x0012E56C
		// (set) Token: 0x06004A47 RID: 19015 RVA: 0x0012F599 File Offset: 0x0012E599
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("MenuItem_ImageUrl")]
		public string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0012F5AC File Offset: 0x0012E5AC
		// (set) Token: 0x06004A49 RID: 19017 RVA: 0x0012F5B4 File Offset: 0x0012E5B4
		internal int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x0012F5C0 File Offset: 0x0012E5C0
		internal string InternalValuePath
		{
			get
			{
				if (this._internalValuePath != null)
				{
					return this._internalValuePath;
				}
				if (this._parent != null)
				{
					List<string> list = new List<string>();
					list.Add(TreeView.Escape(this.Value));
					MenuItem parent = this._parent;
					while (parent != null && !parent._isRoot)
					{
						if (parent._internalValuePath != null)
						{
							list.Add(parent._internalValuePath);
							break;
						}
						list.Add(TreeView.Escape(parent.Value));
						parent = parent._parent;
					}
					list.Reverse();
					this._internalValuePath = string.Join('\\'.ToString(), list.ToArray());
					return this._internalValuePath;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x0012F66D File Offset: 0x0012E66D
		internal bool IsEnabled
		{
			get
			{
				return this.IsEnabledNoOwner && this.Owner.IsEnabled;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x0012F684 File Offset: 0x0012E684
		internal bool IsEnabledNoOwner
		{
			get
			{
				for (MenuItem menuItem = this; menuItem != null; menuItem = menuItem.Parent)
				{
					if (!menuItem.Enabled)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06004A4D RID: 19021 RVA: 0x0012F6AC File Offset: 0x0012E6AC
		// (set) Token: 0x06004A4E RID: 19022 RVA: 0x0012F6D9 File Offset: 0x0012E6D9
		[DefaultValue("")]
		[WebSysDescription("MenuItem_NavigateUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				object obj = this.ViewState["NavigateUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0012F6EC File Offset: 0x0012E6EC
		internal Menu Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0012F6F4 File Offset: 0x0012E6F4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MenuItem Parent
		{
			get
			{
				if (this._parent == null || this._parent._isRoot)
				{
					return null;
				}
				return this._parent;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x0012F714 File Offset: 0x0012E714
		// (set) Token: 0x06004A52 RID: 19026 RVA: 0x0012F741 File Offset: 0x0012E741
		[WebSysDescription("MenuItem_PopOutImageUrl")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string PopOutImageUrl
		{
			get
			{
				object obj = this.ViewState["PopOutImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["PopOutImageUrl"] = value;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x0012F754 File Offset: 0x0012E754
		// (set) Token: 0x06004A54 RID: 19028 RVA: 0x0012F77D File Offset: 0x0012E77D
		[Browsable(true)]
		[WebSysDescription("MenuItem_Selectable")]
		[DefaultValue(true)]
		public bool Selectable
		{
			get
			{
				object obj = this.ViewState["Selectable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x0012F798 File Offset: 0x0012E798
		// (set) Token: 0x06004A56 RID: 19030 RVA: 0x0012F7C1 File Offset: 0x0012E7C1
		[WebSysDescription("MenuItem_Selected")]
		[DefaultValue(false)]
		[Browsable(true)]
		public bool Selected
		{
			get
			{
				object obj = this.ViewState["Selected"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.SetSelected(value);
				this.NotifyOwnerSelected();
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x0012F7D0 File Offset: 0x0012E7D0
		// (set) Token: 0x06004A58 RID: 19032 RVA: 0x0012F7FD File Offset: 0x0012E7FD
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("MenuItem_SeparatorImageUrl")]
		[DefaultValue("")]
		public string SeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["SeparatorImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SeparatorImageUrl"] = value;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x0012F810 File Offset: 0x0012E810
		// (set) Token: 0x06004A5A RID: 19034 RVA: 0x0012F83D File Offset: 0x0012E83D
		[WebSysDescription("MenuItem_Target")]
		[DefaultValue("")]
		public string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x0012F850 File Offset: 0x0012E850
		// (set) Token: 0x06004A5C RID: 19036 RVA: 0x0012F891 File Offset: 0x0012E891
		[DefaultValue("")]
		[WebSysDescription("MenuItem_Text")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					obj = this.ViewState["Value"];
					if (obj == null)
					{
						return string.Empty;
					}
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0012F8A4 File Offset: 0x0012E8A4
		// (set) Token: 0x06004A5E RID: 19038 RVA: 0x0012F8D1 File Offset: 0x0012E8D1
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("MenuItem_ToolTip")]
		public string ToolTip
		{
			get
			{
				object obj = this.ViewState["ToolTip"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x0012F8E4 File Offset: 0x0012E8E4
		// (set) Token: 0x06004A60 RID: 19040 RVA: 0x0012F925 File Offset: 0x0012E925
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("MenuItem_Value")]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj == null)
				{
					obj = this.ViewState["Text"];
					if (obj == null)
					{
						return string.Empty;
					}
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Value"] = value;
				this.ResetValuePathRecursive();
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x0012F940 File Offset: 0x0012E940
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string ValuePath
		{
			get
			{
				if (this._valuePath != null)
				{
					return this._valuePath;
				}
				if (this._parent != null)
				{
					string valuePath = this._parent.ValuePath;
					this._valuePath = ((valuePath.Length == 0 && this._parent.Depth == -1) ? this.Value : (valuePath + this._owner.PathSeparator + this.Value));
					return this._valuePath;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x0012F9BC File Offset: 0x0012E9BC
		private StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._isTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x0012F9EC File Offset: 0x0012E9EC
		internal string GetExpandImageUrl()
		{
			if (this.ChildItems.Count > 0)
			{
				if (this.PopOutImageUrl.Length != 0)
				{
					return this._owner.ResolveClientUrl(this.PopOutImageUrl);
				}
				if (this.Depth < this._owner.StaticDisplayLevels)
				{
					if (this._owner.StaticPopOutImageUrl.Length != 0)
					{
						return this._owner.ResolveClientUrl(this._owner.StaticPopOutImageUrl);
					}
					if (this._owner.StaticEnableDefaultPopOutImage)
					{
						return this._owner.GetImageUrl(2);
					}
				}
				else
				{
					if (this._owner.DynamicPopOutImageUrl.Length != 0)
					{
						return this._owner.ResolveClientUrl(this._owner.DynamicPopOutImageUrl);
					}
					if (this._owner.DynamicEnableDefaultPopOutImage)
					{
						return this._owner.GetImageUrl(2);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x0012FAC8 File Offset: 0x0012EAC8
		internal bool NotTemplated()
		{
			return (this._owner.StaticItemTemplate == null || this.Depth >= this._owner.StaticDisplayLevels) && (this._owner.DynamicItemTemplate == null || this.Depth < this._owner.StaticDisplayLevels);
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x0012FB1C File Offset: 0x0012EB1C
		private void NotifyOwnerSelected()
		{
			object obj = this.ViewState["Selected"];
			bool flag = obj != null && (bool)obj;
			if (this._owner == null)
			{
				this._selectDesired = (flag ? 1 : -1);
				return;
			}
			if (flag)
			{
				this._owner.SetSelectedItem(this);
				return;
			}
			if (this == this._owner.SelectedItem)
			{
				this._owner.SetSelectedItem(null);
			}
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x0012FB87 File Offset: 0x0012EB87
		internal void Render(HtmlTextWriter writer, bool enabled, bool staticOnly)
		{
			this.Render(writer, enabled, staticOnly, true);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x0012FB94 File Offset: 0x0012EB94
		internal void Render(HtmlTextWriter writer, bool enabled, bool staticOnly, bool recursive)
		{
			enabled = (enabled && this.Enabled);
			int num = this.Depth + 1;
			if (this.ChildItems.Count > 0 && num < this._owner.MaximumDepth)
			{
				SubMenuStyle subMenuStyle = this._owner.GetSubMenuStyle(this);
				string text = null;
				if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
				{
					text = this._owner.GetSubMenuCssClassName(this);
				}
				if (num >= this._owner.StaticDisplayLevels)
				{
					if (!staticOnly && enabled && (!this._owner.DesignMode || !recursive))
					{
						PopOutPanel panel = this._owner.Panel;
						if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
						{
							panel.ScrollerClass = this._owner.GetCssClassName(this.ChildItems[0], false);
							panel.ScrollerStyle = null;
						}
						else
						{
							panel.ScrollerClass = null;
							panel.ScrollerStyle = this._owner.GetMenuItemStyle(this.ChildItems[0]);
						}
						if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
						{
							panel.CssClass = text;
							panel.SetInternalStyle(null);
						}
						else if (!subMenuStyle.IsEmpty)
						{
							panel.CssClass = string.Empty;
							panel.SetInternalStyle(subMenuStyle);
						}
						else
						{
							panel.CssClass = string.Empty;
							panel.SetInternalStyle(null);
							panel.BackColor = Color.Empty;
						}
						panel.ID = this.Id + "Items";
						panel.RenderBeginTag(writer);
						writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
						writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
						writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
						writer.RenderBeginTag(HtmlTextWriterTag.Table);
						for (int i = 0; i < this.ChildItems.Count; i++)
						{
							this.ChildItems[i].RenderItem(writer, i, enabled, Orientation.Vertical);
						}
						writer.RenderEndTag();
						panel.RenderEndTag(writer);
						if (recursive)
						{
							for (int j = 0; j < this.ChildItems.Count; j++)
							{
								this.ChildItems[j].Render(writer, enabled, false);
							}
							return;
						}
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
					if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
					{
						if (text != null && text.Length > 0)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
						}
					}
					else
					{
						subMenuStyle.AddAttributesToRender(writer);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Table);
					if (this._owner.Orientation == Orientation.Horizontal)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					}
					bool flag = num + 1 < this._owner.StaticDisplayLevels;
					bool flag2 = num + 1 < this._owner.MaximumDepth;
					for (int k = 0; k < this.ChildItems.Count; k++)
					{
						if (recursive && this.ChildItems[k].ChildItems.Count != 0 && ((enabled && this.ChildItems[k].Enabled) || flag) && flag2)
						{
							if (flag)
							{
								this.ChildItems[k].RenderItem(writer, k, enabled, this._owner.Orientation);
								if (this._owner.Orientation == Orientation.Vertical)
								{
									writer.RenderBeginTag(HtmlTextWriterTag.Tr);
									writer.RenderBeginTag(HtmlTextWriterTag.Td);
									this.ChildItems[k].Render(writer, enabled, staticOnly);
									writer.RenderEndTag();
									writer.RenderEndTag();
								}
								else
								{
									writer.RenderBeginTag(HtmlTextWriterTag.Td);
									this.ChildItems[k].Render(writer, enabled, staticOnly);
									writer.RenderEndTag();
								}
							}
							else
							{
								this.ChildItems[k].RenderItem(writer, k, enabled, this._owner.Orientation, staticOnly);
							}
						}
						else
						{
							this.ChildItems[k].RenderItem(writer, k, enabled, this._owner.Orientation);
						}
					}
					if (this._owner.Orientation == Orientation.Horizontal)
					{
						writer.RenderEndTag();
					}
					writer.RenderEndTag();
					if (!flag && !staticOnly && recursive && flag2)
					{
						for (int l = 0; l < this.ChildItems.Count; l++)
						{
							if (this.ChildItems[l].ChildItems.Count != 0 && enabled && this.ChildItems[l].Enabled)
							{
								this.ChildItems[l].Render(writer, enabled, false, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00130054 File Offset: 0x0012F054
		internal void RenderItem(HtmlTextWriter writer, int position, bool enabled, Orientation orientation)
		{
			this.RenderItem(writer, position, enabled, orientation, false);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00130064 File Offset: 0x0012F064
		internal void RenderItem(HtmlTextWriter writer, int position, bool enabled, Orientation orientation, bool staticOnly)
		{
			enabled = (enabled && this.Enabled);
			int depth = this.Depth;
			MenuItemStyle menuItemStyle = this._owner.GetMenuItemStyle(this);
			int num = this.Depth + 1;
			bool flag = depth < this._owner.StaticDisplayLevels && this._owner.StaticTopSeparatorImageUrl.Length != 0;
			bool flag2 = depth >= this._owner.StaticDisplayLevels && this._owner.DynamicTopSeparatorImageUrl.Length != 0;
			if (flag || flag2)
			{
				if (orientation == Orientation.Vertical)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (flag)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(this._owner.StaticTopSeparatorImageUrl));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(this._owner.DynamicTopSeparatorImageUrl));
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
				if (orientation == Orientation.Vertical)
				{
					writer.RenderEndTag();
				}
			}
			if (menuItemStyle != null && !menuItemStyle.ItemSpacing.IsEmpty && (depth != 0 || position != 0))
			{
				this.RenderItemSpacing(writer, menuItemStyle.ItemSpacing, orientation);
			}
			if (!staticOnly && this._owner.Enabled)
			{
				if (num > this._owner.StaticDisplayLevels)
				{
					if ((this.Selectable && this.Enabled) || this.ChildItems.Count != 0)
					{
						writer.AddAttribute("onmouseover", "Menu_HoverDynamic(this)");
						this.RenderItemEvents(writer);
					}
					else
					{
						writer.AddAttribute("onmouseover", "Menu_HoverDisabled(this)");
						writer.AddAttribute("onmouseout", "Menu_Unhover(this)");
					}
				}
				else if (num == this._owner.StaticDisplayLevels)
				{
					if ((this.Selectable && this.Enabled) || this.ChildItems.Count != 0)
					{
						writer.AddAttribute("onmouseover", "Menu_HoverStatic(this)");
						this.RenderItemEvents(writer);
					}
				}
				else if (this.Selectable && this.Enabled)
				{
					writer.AddAttribute("onmouseover", "Menu_HoverRoot(this)");
					this.RenderItemEvents(writer);
				}
			}
			if (this.ToolTip.Length != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.Id);
			if (orientation == Orientation.Vertical)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
			{
				string cssClassName = this._owner.GetCssClassName(this, false);
				if (cssClassName.Trim().Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClassName);
				}
			}
			else if (menuItemStyle != null)
			{
				menuItemStyle.AddAttributesToRender(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!this._owner.ItemWrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			if (orientation == Orientation.Vertical)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (this._owner.Page != null && this._owner.Page.SupportsStyleSheets)
			{
				bool flag3;
				string cssClassName2 = this._owner.GetCssClassName(this, true, out flag3);
				if (cssClassName2.Trim().Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClassName2);
					if (flag3)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
						writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "1em");
					}
				}
			}
			else if (menuItemStyle != null)
			{
				menuItemStyle.HyperLinkStyle.AddAttributesToRender(writer);
			}
			string accessKey = this._owner.AccessKey;
			if (enabled && this.Selectable)
			{
				if (this.NavigateUrl.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this._owner.ResolveClientUrl(this.NavigateUrl));
					string text = this.ViewState["Target"] as string;
					if (text == null)
					{
						text = this._owner.Target;
					}
					if (text.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Target, text);
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this._owner.Page.ClientScript.GetPostBackClientHyperlink(this._owner, this.InternalValuePath, true, true));
				}
				if (!this._owner.AccessKeyRendered && accessKey.Length != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey, true);
					this._owner.AccessKeyRendered = true;
				}
			}
			else if (!enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
			}
			else if (this.ChildItems.Count != 0 && num >= this._owner.StaticDisplayLevels)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "text");
				if (!this._owner.AccessKeyRendered && accessKey.Length != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey, true);
					this._owner.AccessKeyRendered = true;
				}
			}
			if (depth != 0 && depth < this._owner.StaticDisplayLevels)
			{
				Unit staticSubMenuIndent = this._owner.StaticSubMenuIndent;
				if (!staticSubMenuIndent.IsEmpty && staticSubMenuIndent.Value != 0.0)
				{
					double num2 = staticSubMenuIndent.Value * (double)depth;
					if (num2 < 32767.0)
					{
						staticSubMenuIndent = new Unit(num2, staticSubMenuIndent.Type);
					}
					else
					{
						staticSubMenuIndent = new Unit(32767.0, staticSubMenuIndent.Type);
					}
					writer.AddStyleAttribute("margin-left", staticSubMenuIndent.ToString(CultureInfo.InvariantCulture));
				}
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (this.ImageUrl.Length > 0 && this.NotTemplated())
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(this.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.ToolTip);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.AddStyleAttribute("vertical-align", "middle");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			this.RenderText(writer);
			writer.RenderEndTag();
			bool flag4 = num >= this._owner.StaticDisplayLevels && num < this._owner.MaximumDepth;
			string text2 = flag4 ? this.GetExpandImageUrl() : string.Empty;
			bool flag5 = false;
			if (orientation == Orientation.Horizontal && depth < this._owner.StaticDisplayLevels && (!flag4 || text2.Length == 0) && (menuItemStyle == null || menuItemStyle.ItemSpacing.IsEmpty))
			{
				if (this.Depth + 1 < this._owner.StaticDisplayLevels && this.ChildItems.Count != 0)
				{
					flag5 = true;
				}
				else
				{
					for (MenuItem menuItem = this; menuItem != null; menuItem = menuItem.Parent)
					{
						if ((menuItem.Parent == null && this._owner.Items.Count != 0 && menuItem != this._owner.Items[this._owner.Items.Count - 1]) || (menuItem.Parent != null && menuItem.Parent.ChildItems.Count != 0 && menuItem != menuItem.Parent.ChildItems[menuItem.Parent.ChildItems.Count - 1]))
						{
							flag5 = true;
							break;
						}
					}
				}
			}
			writer.RenderEndTag();
			if (flag4 && text2.Length > 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "0");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, text2);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle");
				if (depth < this._owner.StaticDisplayLevels)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Format(CultureInfo.CurrentCulture, this._owner.StaticPopOutImageTextFormatString, new object[]
					{
						this.Text
					}));
				}
				else if (depth >= this._owner.StaticDisplayLevels)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Format(CultureInfo.CurrentCulture, this._owner.DynamicPopOutImageTextFormatString, new object[]
					{
						this.Text
					}));
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			if (orientation == Orientation.Vertical)
			{
				writer.RenderEndTag();
			}
			if (menuItemStyle != null && !menuItemStyle.ItemSpacing.IsEmpty)
			{
				this.RenderItemSpacing(writer, menuItemStyle.ItemSpacing, orientation);
			}
			else if (flag5)
			{
				this.RenderItemSpacing(writer, MenuItem.HorizontalDefaultSpacing, orientation);
			}
			bool flag6 = this.SeparatorImageUrl.Length != 0;
			bool flag7 = depth < this._owner.StaticDisplayLevels && this._owner.StaticBottomSeparatorImageUrl.Length != 0;
			bool flag8 = depth >= this._owner.StaticDisplayLevels && this._owner.DynamicBottomSeparatorImageUrl.Length != 0;
			if (flag6 || flag7 || flag8)
			{
				if (orientation == Orientation.Vertical)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (flag6)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(this.SeparatorImageUrl));
				}
				else if (flag7)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(this._owner.StaticBottomSeparatorImageUrl));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Src, this._owner.ResolveClientUrl(this._owner.DynamicBottomSeparatorImageUrl));
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
				if (orientation == Orientation.Vertical)
				{
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x001309FE File Offset: 0x0012F9FE
		private void RenderItemEvents(HtmlTextWriter writer)
		{
			writer.AddAttribute("onmouseout", "Menu_Unhover(this)");
			if (this._owner.IsNotIE)
			{
				writer.AddAttribute("onkeyup", "Menu_Key(event)");
				return;
			}
			writer.AddAttribute("onkeyup", "Menu_Key(this)");
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x00130A40 File Offset: 0x0012FA40
		private void RenderItemSpacing(HtmlTextWriter writer, Unit spacing, Orientation orientation)
		{
			if (orientation == Orientation.Vertical)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, spacing.ToString(CultureInfo.InvariantCulture));
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
				return;
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, spacing.ToString(CultureInfo.InvariantCulture));
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.RenderEndTag();
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x00130AA4 File Offset: 0x0012FAA4
		internal void RenderText(HtmlTextWriter writer)
		{
			if (this.Container != null && ((this._owner.StaticItemTemplate != null && this.Depth < this._owner.StaticDisplayLevels) || (this._owner.DynamicItemTemplate != null && this.Depth >= this._owner.StaticDisplayLevels)))
			{
				this.Container.RenderControl(writer);
				return;
			}
			writer.Write(this.FormattedText);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00130B14 File Offset: 0x0012FB14
		internal void ResetValuePathRecursive()
		{
			if (this._valuePath != null)
			{
				this._valuePath = null;
				foreach (object obj in this.ChildItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					menuItem.ResetValuePathRecursive();
				}
			}
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x00130B7C File Offset: 0x0012FB7C
		internal void SetDataBound(bool dataBound)
		{
			this.ViewState["DataBound"] = dataBound;
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00130B94 File Offset: 0x0012FB94
		internal void SetDataItem(object dataItem)
		{
			this._dataItem = dataItem;
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x00130B9D File Offset: 0x0012FB9D
		internal void SetDataPath(string dataPath)
		{
			this.ViewState["DataPath"] = dataPath;
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00130BB0 File Offset: 0x0012FBB0
		internal void SetDepth(int depth)
		{
			this._depth = depth;
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00130BB9 File Offset: 0x0012FBB9
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.ChildItems.Count > 0)
			{
				this.ChildItems.SetDirty();
			}
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00130BE0 File Offset: 0x0012FBE0
		internal void SetOwner(Menu owner)
		{
			this._owner = owner;
			if (this._selectDesired == 1)
			{
				this._selectDesired = 0;
				this.Selected = true;
			}
			else if (this._selectDesired == -1)
			{
				this._selectDesired = 0;
				this.Selected = false;
			}
			foreach (object obj in this.ChildItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				menuItem.SetOwner(this._owner);
			}
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00130C78 File Offset: 0x0012FC78
		internal void SetParent(MenuItem parent)
		{
			this._parent = parent;
			this.SetPath(null);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00130C88 File Offset: 0x0012FC88
		internal void SetPath(string newPath)
		{
			this._internalValuePath = newPath;
			this._depth = -2;
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00130C99 File Offset: 0x0012FC99
		internal void SetSelected(bool value)
		{
			this.ViewState["Selected"] = value;
			if (this._owner == null)
			{
				this._selectDesired = (value ? 1 : -1);
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x00130CC6 File Offset: 0x0012FCC6
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x00130CD0 File Offset: 0x0012FCD0
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			if (array != null)
			{
				if (array[0] != null)
				{
					((IStateManager)this.ViewState).LoadViewState(array[0]);
				}
				this.NotifyOwnerSelected();
				if (array[1] != null)
				{
					((IStateManager)this.ChildItems).LoadViewState(array[1]);
				}
			}
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00130D14 File Offset: 0x0012FD14
		object IStateManager.SaveViewState()
		{
			object[] array = new object[2];
			if (this._viewState != null)
			{
				array[0] = ((IStateManager)this._viewState).SaveViewState();
			}
			if (this._childItems != null)
			{
				array[1] = ((IStateManager)this._childItems).SaveViewState();
			}
			if (array[0] == null && array[1] == null)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00130D61 File Offset: 0x0012FD61
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
			if (this._childItems != null)
			{
				((IStateManager)this._childItems).TrackViewState();
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00130D90 File Offset: 0x0012FD90
		object ICloneable.Clone()
		{
			return new MenuItem
			{
				Enabled = this.Enabled,
				ImageUrl = this.ImageUrl,
				NavigateUrl = this.NavigateUrl,
				PopOutImageUrl = this.PopOutImageUrl,
				Selectable = this.Selectable,
				Selected = this.Selected,
				SeparatorImageUrl = this.SeparatorImageUrl,
				Target = this.Target,
				Text = this.Text,
				ToolTip = this.ToolTip,
				Value = this.Value
			};
		}

		// Token: 0x04002B6D RID: 11117
		private static readonly Unit HorizontalDefaultSpacing = Unit.Pixel(3);

		// Token: 0x04002B6E RID: 11118
		private bool _isTrackingViewState;

		// Token: 0x04002B6F RID: 11119
		private StateBag _viewState;

		// Token: 0x04002B70 RID: 11120
		private MenuItemCollection _childItems;

		// Token: 0x04002B71 RID: 11121
		private Menu _owner;

		// Token: 0x04002B72 RID: 11122
		private MenuItem _parent;

		// Token: 0x04002B73 RID: 11123
		private int _selectDesired;

		// Token: 0x04002B74 RID: 11124
		private object _dataItem;

		// Token: 0x04002B75 RID: 11125
		private MenuItemTemplateContainer _container;

		// Token: 0x04002B76 RID: 11126
		private int _index;

		// Token: 0x04002B77 RID: 11127
		internal string _id = string.Empty;

		// Token: 0x04002B78 RID: 11128
		private string _valuePath;

		// Token: 0x04002B79 RID: 11129
		private string _internalValuePath;

		// Token: 0x04002B7A RID: 11130
		private int _depth = -2;

		// Token: 0x04002B7B RID: 11131
		private bool _isRoot;
	}
}

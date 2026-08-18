using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200046E RID: 1134
	[ParseChildren(true, "ChildItems")]
	public sealed class MenuItem : IStateManager, ICloneable
	{
		// Token: 0x06003791 RID: 14225 RVA: 0x000B498D File Offset: 0x000B2B8D
		public MenuItem()
		{
			this._selectDesired = 0;
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000B49AF File Offset: 0x000B2BAF
		internal MenuItem(Menu owner, bool isRoot) : this()
		{
			this._owner = owner;
			this._isRoot = isRoot;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000B49C5 File Offset: 0x000B2BC5
		public MenuItem(string text) : this(text, null, null, null, null)
		{
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000B49D2 File Offset: 0x000B2BD2
		public MenuItem(string text, string value) : this(text, value, null, null, null)
		{
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000B49DF File Offset: 0x000B2BDF
		public MenuItem(string text, string value, string imageUrl) : this(text, value, imageUrl, null, null)
		{
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000B49EC File Offset: 0x000B2BEC
		public MenuItem(string text, string value, string imageUrl, string navigateUrl) : this(text, value, imageUrl, navigateUrl, null)
		{
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000B49FC File Offset: 0x000B2BFC
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

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000B4A54 File Offset: 0x000B2C54
		[Browsable(false)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
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

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000B4A70 File Offset: 0x000B2C70
		// (set) Token: 0x0600379A RID: 14234 RVA: 0x000B4A78 File Offset: 0x000B2C78
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

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000B4A84 File Offset: 0x000B2C84
		[Browsable(false)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DataBound
		{
			get
			{
				object obj = this.ViewState["DataBound"];
				return obj != null && (bool)obj;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x000B4AB0 File Offset: 0x000B2CB0
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

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000B4ADD File Offset: 0x000B2CDD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000B4B18 File Offset: 0x000B2D18
		[Browsable(false)]
		[DefaultValue(null)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000B4B20 File Offset: 0x000B2D20
		// (set) Token: 0x060037A0 RID: 14240 RVA: 0x000B4B49 File Offset: 0x000B2D49
		[Browsable(true)]
		[DefaultValue(true)]
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

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000B4B64 File Offset: 0x000B2D64
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

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000B4C10 File Offset: 0x000B2E10
		internal string Id
		{
			get
			{
				if (this._id.Length == 0)
				{
					this.Index = this._owner.CreateItemIndex();
					this._id = this._owner.ClientID + "n" + this.Index.ToString();
				}
				return this._id;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000B4C6C File Offset: 0x000B2E6C
		// (set) Token: 0x060037A4 RID: 14244 RVA: 0x000B4C99 File Offset: 0x000B2E99
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000B4CAC File Offset: 0x000B2EAC
		// (set) Token: 0x060037A6 RID: 14246 RVA: 0x000B4CB4 File Offset: 0x000B2EB4
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

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000B4CC0 File Offset: 0x000B2EC0
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

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000B4D6D File Offset: 0x000B2F6D
		internal bool IsEnabled
		{
			get
			{
				return this.IsEnabledNoOwner && this.Owner.IsEnabled;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000B4D84 File Offset: 0x000B2F84
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

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000B4DAC File Offset: 0x000B2FAC
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x000B4DD9 File Offset: 0x000B2FD9
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("MenuItem_NavigateUrl")]
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

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000B4DEC File Offset: 0x000B2FEC
		internal Menu Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000B4DF4 File Offset: 0x000B2FF4
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

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000B4E14 File Offset: 0x000B3014
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x000B4E41 File Offset: 0x000B3041
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("MenuItem_PopOutImageUrl")]
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

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x000B4E54 File Offset: 0x000B3054
		// (set) Token: 0x060037B1 RID: 14257 RVA: 0x000B4E7D File Offset: 0x000B307D
		[Browsable(true)]
		[DefaultValue(true)]
		[WebSysDescription("MenuItem_Selectable")]
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

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000B4E98 File Offset: 0x000B3098
		// (set) Token: 0x060037B3 RID: 14259 RVA: 0x000B4EC1 File Offset: 0x000B30C1
		[Browsable(true)]
		[DefaultValue(false)]
		[WebSysDescription("MenuItem_Selected")]
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

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000B4ED0 File Offset: 0x000B30D0
		// (set) Token: 0x060037B5 RID: 14261 RVA: 0x000B4EFD File Offset: 0x000B30FD
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("MenuItem_SeparatorImageUrl")]
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

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000B4F10 File Offset: 0x000B3110
		// (set) Token: 0x060037B7 RID: 14263 RVA: 0x000B4F3D File Offset: 0x000B313D
		[DefaultValue("")]
		[WebSysDescription("MenuItem_Target")]
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

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000B4F50 File Offset: 0x000B3150
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000B4F91 File Offset: 0x000B3191
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("MenuItem_Text")]
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

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000B4FA4 File Offset: 0x000B31A4
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000B4FD1 File Offset: 0x000B31D1
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

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000B4FE4 File Offset: 0x000B31E4
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x000B5025 File Offset: 0x000B3225
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

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000B5040 File Offset: 0x000B3240
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
					this._valuePath = ((valuePath.Length == 0 && this._parent.Depth == -1) ? this.Value : (valuePath + this._owner.PathSeparator.ToString() + this.Value));
					return this._valuePath;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000B50BF File Offset: 0x000B32BF
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

		// Token: 0x060037C0 RID: 14272 RVA: 0x000B50F0 File Offset: 0x000B32F0
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

		// Token: 0x060037C1 RID: 14273 RVA: 0x000B51CC File Offset: 0x000B33CC
		internal bool NotTemplated()
		{
			return (this._owner.StaticItemTemplate == null || this.Depth >= this._owner.StaticDisplayLevels) && (this._owner.DynamicItemTemplate == null || this.Depth < this._owner.StaticDisplayLevels);
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000B5220 File Offset: 0x000B3420
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

		// Token: 0x060037C3 RID: 14275 RVA: 0x000B528B File Offset: 0x000B348B
		internal void Render(HtmlTextWriter writer, bool enabled, bool staticOnly)
		{
			this.Render(writer, enabled, staticOnly, true);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000B5298 File Offset: 0x000B3498
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

		// Token: 0x060037C5 RID: 14277 RVA: 0x000B5755 File Offset: 0x000B3955
		internal void RenderItem(HtmlTextWriter writer, int position, bool enabled, Orientation orientation)
		{
			this.RenderItem(writer, position, enabled, orientation, false);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000B5764 File Offset: 0x000B3964
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
				Unit unit = this._owner.StaticSubMenuIndent;
				if (unit.IsEmpty)
				{
					unit = Unit.Pixel(16);
				}
				if (unit.Value != 0.0)
				{
					double num2 = unit.Value * (double)depth;
					if (num2 < 32767.0)
					{
						unit = new Unit(num2, unit.Type);
					}
					else
					{
						unit = new Unit(32767.0, unit.Type);
					}
					writer.AddStyleAttribute("margin-left", unit.ToString(CultureInfo.InvariantCulture));
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

		// Token: 0x060037C7 RID: 14279 RVA: 0x000B60EB File Offset: 0x000B42EB
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

		// Token: 0x060037C8 RID: 14280 RVA: 0x000B612C File Offset: 0x000B432C
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

		// Token: 0x060037C9 RID: 14281 RVA: 0x000B6190 File Offset: 0x000B4390
		internal void RenderText(HtmlTextWriter writer)
		{
			if (this.Container != null && ((this._owner.StaticItemTemplate != null && this.Depth < this._owner.StaticDisplayLevels) || (this._owner.DynamicItemTemplate != null && this.Depth >= this._owner.StaticDisplayLevels)))
			{
				this.Container.RenderControl(writer);
				return;
			}
			writer.Write(this.FormattedText);
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000B6200 File Offset: 0x000B4400
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

		// Token: 0x060037CB RID: 14283 RVA: 0x000B6268 File Offset: 0x000B4468
		internal void SetDataBound(bool dataBound)
		{
			this.ViewState["DataBound"] = dataBound;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000B6280 File Offset: 0x000B4480
		internal void SetDataItem(object dataItem)
		{
			this._dataItem = dataItem;
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000B6289 File Offset: 0x000B4489
		internal void SetDataPath(string dataPath)
		{
			this.ViewState["DataPath"] = dataPath;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000B629C File Offset: 0x000B449C
		internal void SetDepth(int depth)
		{
			this._depth = depth;
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000B62A5 File Offset: 0x000B44A5
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.ChildItems.Count > 0)
			{
				this.ChildItems.SetDirty();
			}
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000B62CC File Offset: 0x000B44CC
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

		// Token: 0x060037D1 RID: 14289 RVA: 0x000B6364 File Offset: 0x000B4564
		internal void SetParent(MenuItem parent)
		{
			this._parent = parent;
			this.SetPath(null);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000B6374 File Offset: 0x000B4574
		internal void SetPath(string newPath)
		{
			this._internalValuePath = newPath;
			this._depth = -2;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000B6385 File Offset: 0x000B4585
		internal void SetSelected(bool value)
		{
			this.ViewState["Selected"] = value;
			if (this._owner == null)
			{
				this._selectDesired = (value ? 1 : -1);
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000B63B2 File Offset: 0x000B45B2
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000B63BC File Offset: 0x000B45BC
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

		// Token: 0x060037D6 RID: 14294 RVA: 0x000B6400 File Offset: 0x000B4600
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

		// Token: 0x060037D7 RID: 14295 RVA: 0x000B644D File Offset: 0x000B464D
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

		// Token: 0x060037D8 RID: 14296 RVA: 0x000B647C File Offset: 0x000B467C
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

		// Token: 0x04002260 RID: 8800
		private static readonly Unit HorizontalDefaultSpacing = Unit.Pixel(3);

		// Token: 0x04002261 RID: 8801
		private bool _isTrackingViewState;

		// Token: 0x04002262 RID: 8802
		private StateBag _viewState;

		// Token: 0x04002263 RID: 8803
		private MenuItemCollection _childItems;

		// Token: 0x04002264 RID: 8804
		private Menu _owner;

		// Token: 0x04002265 RID: 8805
		private MenuItem _parent;

		// Token: 0x04002266 RID: 8806
		private int _selectDesired;

		// Token: 0x04002267 RID: 8807
		private object _dataItem;

		// Token: 0x04002268 RID: 8808
		private MenuItemTemplateContainer _container;

		// Token: 0x04002269 RID: 8809
		private int _index;

		// Token: 0x0400226A RID: 8810
		internal string _id = string.Empty;

		// Token: 0x0400226B RID: 8811
		private string _valuePath;

		// Token: 0x0400226C RID: 8812
		private string _internalValuePath;

		// Token: 0x0400226D RID: 8813
		private int _depth = -2;

		// Token: 0x0400226E RID: 8814
		private bool _isRoot;
	}
}

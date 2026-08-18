using System;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x0200069B RID: 1691
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class MenuAdapter : WebControlAdapter, IPostBackEventHandler
	{
		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x0014D660 File Offset: 0x0014C660
		protected new Menu Control
		{
			get
			{
				return (Menu)base.Control;
			}
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0014D670 File Offset: 0x0014C670
		protected internal override void LoadAdapterControlState(object state)
		{
			if (state != null)
			{
				Pair pair = state as Pair;
				if (pair != null)
				{
					base.LoadAdapterViewState(pair.First);
					this._path = (string)pair.Second;
					return;
				}
				base.LoadAdapterViewState(null);
				this._path = (state as string);
			}
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x0014D6BC File Offset: 0x0014C6BC
		private string Escape(string path)
		{
			StringBuilder stringBuilder = null;
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			int startIndex = 0;
			int num = 0;
			for (int i = 0; i < path.Length; i++)
			{
				char c = path[i];
				if (c != '\\')
				{
					if (c != '_')
					{
						num++;
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(path.Length + 5);
						}
						if (num > 0)
						{
							stringBuilder.Append(path, startIndex, num);
						}
						stringBuilder.Append("__");
						startIndex = i + 1;
						num = 0;
					}
				}
				else if (i + 1 < path.Length && path[i + 1] == '\\')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(path.Length + 5);
					}
					if (num > 0)
					{
						stringBuilder.Append(path, startIndex, num);
					}
					stringBuilder.Append("\\_\\");
					i++;
					startIndex = i + 1;
					num = 0;
				}
				else
				{
					num++;
				}
			}
			if (stringBuilder == null)
			{
				return path;
			}
			if (num > 0)
			{
				stringBuilder.Append(path, startIndex, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0014D7B3 File Offset: 0x0014C7B3
		private string UnEscape(string path)
		{
			return path.Replace("\\\\", "\\").Replace("\\_\\", "\\\\").Replace("__", "_");
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0014D7E3 File Offset: 0x0014C7E3
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Control.Page.RegisterRequiresControlState(this.Control);
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0014D802 File Offset: 0x0014C802
		protected internal override void OnPreRender(EventArgs e)
		{
			this.Control.OnPreRender(e, false);
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0014D814 File Offset: 0x0014C814
		protected internal override object SaveAdapterControlState()
		{
			object obj = base.SaveAdapterViewState();
			if (obj != null)
			{
				return new Pair(obj, this._path);
			}
			if (this._path != null)
			{
				return this._path;
			}
			return null;
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0014D848 File Offset: 0x0014C848
		private void RenderBreak(HtmlTextWriter writer)
		{
			if (this.Control.Orientation == Orientation.Vertical)
			{
				writer.WriteBreak();
				return;
			}
			writer.Write(' ');
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0014D868 File Offset: 0x0014C868
		protected override void RenderBeginTag(HtmlTextWriter writer)
		{
			Menu control = this.Control;
			if (control.SkipLinkText.Length != 0)
			{
				new HyperLink
				{
					NavigateUrl = '#' + control.ClientID + "_SkipLink",
					ImageUrl = control.SpacerImageUrl,
					Text = control.SkipLinkText,
					Height = Unit.Pixel(1),
					Width = Unit.Pixel(1),
					Page = base.Page
				}.RenderControl(writer);
			}
			this._menuPanel = new Panel();
			this._menuPanel.ID = control.UniqueID;
			this._menuPanel.Page = base.Page;
			MenuItem menuItem;
			if (this._path != null)
			{
				menuItem = control.Items.FindItem(this._path.Split(new char[]
				{
					'\\'
				}), 0);
				this._titleItem = menuItem;
			}
			else
			{
				menuItem = control.RootItem;
			}
			SubMenuStyle subMenuStyle = control.GetSubMenuStyle(menuItem);
			if (!subMenuStyle.IsEmpty)
			{
				if (base.Page != null && base.Page.SupportsStyleSheets)
				{
					string subMenuCssClassName = control.GetSubMenuCssClassName(menuItem);
					if (subMenuCssClassName.Trim().Length > 0)
					{
						this._menuPanel.CssClass = subMenuCssClassName;
					}
				}
				else
				{
					this._menuPanel.ApplyStyle(subMenuStyle);
				}
			}
			this._menuPanel.Width = control.Width;
			this._menuPanel.Height = control.Height;
			this._menuPanel.Enabled = control.IsEnabled;
			this._menuPanel.RenderBeginTag(writer);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0014D9F4 File Offset: 0x0014C9F4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			Menu control = this.Control;
			int num = 0;
			if (this._titleItem == null)
			{
				num = 1;
				this._path = null;
				foreach (object obj in control.Items)
				{
					MenuItem menuItem = (MenuItem)obj;
					this.RenderItem(writer, menuItem, num++);
					if (control.StaticDisplayLevels > 1 && menuItem.ChildItems.Count > 0)
					{
						this.RenderContentsRecursive(writer, menuItem, 1, control.StaticDisplayLevels);
					}
				}
				return;
			}
			if (this._titleItem.Depth + 1 >= control.MaximumDepth)
			{
				throw new InvalidOperationException(SR.GetString("Menu_InvalidDepth"));
			}
			if (!this._titleItem.IsEnabled)
			{
				throw new InvalidOperationException(SR.GetString("Menu_InvalidNavigation"));
			}
			this.RenderItem(writer, this._titleItem, num++);
			foreach (object obj2 in this._titleItem.ChildItems)
			{
				MenuItem item = (MenuItem)obj2;
				this.RenderItem(writer, item, num++);
			}
			if (base.PageAdapter != null)
			{
				base.PageAdapter.RenderPostBackEvent(writer, control.UniqueID, "u", SR.GetString("MenuAdapter_Up"), SR.GetString("MenuAdapter_UpOneLevel"));
				return;
			}
			new HyperLink
			{
				NavigateUrl = base.Page.ClientScript.GetPostBackClientHyperlink(control, "u"),
				Text = SR.GetString("MenuAdapter_UpOneLevel"),
				Page = base.Page
			}.RenderControl(writer);
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x0014DBCC File Offset: 0x0014CBCC
		private void RenderContentsRecursive(HtmlTextWriter writer, MenuItem parentItem, int depth, int maxDepth)
		{
			int num = 1;
			foreach (object obj in parentItem.ChildItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.RenderItem(writer, menuItem, num++);
				if (depth + 1 < maxDepth && menuItem.ChildItems.Count > 0)
				{
					this.RenderContentsRecursive(writer, menuItem, depth + 1, maxDepth);
				}
			}
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0014DC50 File Offset: 0x0014CC50
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			this._menuPanel.RenderEndTag(writer);
			if (this.Control.SkipLinkText.Length != 0)
			{
				new HtmlAnchor
				{
					Name = this.Control.ClientID + "_SkipLink",
					Page = base.Page
				}.RenderControl(writer);
			}
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0014DCB0 File Offset: 0x0014CCB0
		private void RenderExpand(HtmlTextWriter writer, MenuItem item, Menu owner)
		{
			string expandImageUrl = item.GetExpandImageUrl();
			if (expandImageUrl.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = expandImageUrl;
				image.GenerateEmptyAlternateText = true;
				if (item.Depth < owner.StaticDisplayLevels)
				{
					image.AlternateText = string.Format(CultureInfo.CurrentCulture, owner.StaticPopOutImageTextFormatString, new object[]
					{
						item.Text
					});
				}
				else
				{
					image.AlternateText = string.Format(CultureInfo.CurrentCulture, owner.DynamicPopOutImageTextFormatString, new object[]
					{
						item.Text
					});
				}
				image.ImageAlign = ImageAlign.AbsMiddle;
				image.Page = base.Page;
				image.RenderControl(writer);
				return;
			}
			writer.Write(' ');
			if (item.Depth < owner.StaticDisplayLevels && owner.StaticPopOutImageTextFormatString.Length != 0)
			{
				writer.Write(HttpUtility.HtmlEncode(string.Format(CultureInfo.CurrentCulture, owner.StaticPopOutImageTextFormatString, new object[]
				{
					item.Text
				})));
				return;
			}
			if (item.Depth >= owner.StaticDisplayLevels && owner.DynamicPopOutImageTextFormatString.Length != 0)
			{
				writer.Write(HttpUtility.HtmlEncode(string.Format(CultureInfo.CurrentCulture, owner.DynamicPopOutImageTextFormatString, new object[]
				{
					item.Text
				})));
				return;
			}
			writer.Write(HttpUtility.HtmlEncode(SR.GetString("MenuAdapter_Expand", new object[]
			{
				item.Text
			})));
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0014DE24 File Offset: 0x0014CE24
		protected internal virtual void RenderItem(HtmlTextWriter writer, MenuItem item, int position)
		{
			Menu control = this.Control;
			MenuItemStyle menuItemStyle = control.GetMenuItemStyle(item);
			string imageUrl = item.ImageUrl;
			int depth = item.Depth;
			int num = depth + 1;
			string toolTip = item.ToolTip;
			string navigateUrl = item.NavigateUrl;
			string text = item.Text;
			bool isEnabled = item.IsEnabled;
			bool selectable = item.Selectable;
			MenuItemCollection childItems = item.ChildItems;
			string text2 = null;
			if (depth < control.StaticDisplayLevels && control.StaticTopSeparatorImageUrl.Length != 0)
			{
				text2 = control.StaticTopSeparatorImageUrl;
			}
			else if (depth >= control.StaticDisplayLevels && control.DynamicTopSeparatorImageUrl.Length != 0)
			{
				text2 = control.DynamicTopSeparatorImageUrl;
			}
			if (text2 != null)
			{
				new Image
				{
					ImageUrl = text2,
					GenerateEmptyAlternateText = true,
					Page = base.Page
				}.RenderControl(writer);
				this.RenderBreak(writer);
			}
			if (menuItemStyle != null && !menuItemStyle.ItemSpacing.IsEmpty && (this._titleItem != null || position != 0))
			{
				this.RenderSpace(writer, menuItemStyle.ItemSpacing, control.Orientation);
			}
			Panel panel = new MenuAdapter.SpanPanel();
			panel.Enabled = isEnabled;
			panel.Page = base.Page;
			if (base.Page != null && base.Page.SupportsStyleSheets)
			{
				string cssClassName = control.GetCssClassName(item, false);
				if (cssClassName.Trim().Length > 0)
				{
					panel.CssClass = cssClassName;
				}
			}
			else if (menuItemStyle != null)
			{
				panel.ApplyStyle(menuItemStyle);
			}
			if (item.ToolTip.Length != 0)
			{
				panel.ToolTip = item.ToolTip;
			}
			panel.RenderBeginTag(writer);
			bool flag = position != 0 && childItems.Count != 0 && num >= control.StaticDisplayLevels && num < control.MaximumDepth;
			if (position != 0 && depth > 0 && control.StaticSubMenuIndent != Unit.Pixel(0) && depth < control.StaticDisplayLevels)
			{
				Image image = new Image();
				image.ImageUrl = control.SpacerImageUrl;
				image.GenerateEmptyAlternateText = true;
				double num2 = control.StaticSubMenuIndent.Value * (double)depth;
				if (num2 < 32767.0)
				{
					image.Width = new Unit(num2, control.StaticSubMenuIndent.Type);
				}
				else
				{
					image.Width = new Unit(32767.0, control.StaticSubMenuIndent.Type);
				}
				image.Height = Unit.Pixel(1);
				image.Page = base.Page;
				image.RenderControl(writer);
			}
			if (imageUrl.Length > 0 && item.NotTemplated())
			{
				Image image2 = new Image();
				image2.ImageUrl = imageUrl;
				if (toolTip.Length != 0)
				{
					image2.AlternateText = toolTip;
				}
				else
				{
					image2.GenerateEmptyAlternateText = true;
				}
				image2.Page = base.Page;
				image2.RenderControl(writer);
				writer.Write(' ');
			}
			bool applyInlineBorder;
			string className;
			if (base.Page != null && base.Page.SupportsStyleSheets)
			{
				className = control.GetCssClassName(item, true, out applyInlineBorder);
			}
			else
			{
				className = string.Empty;
				applyInlineBorder = false;
			}
			if (isEnabled && (flag || selectable))
			{
				string accessKey = control.AccessKey;
				string text3 = ((position == 0 || (position == 1 && depth == 0)) && accessKey.Length != 0) ? accessKey : null;
				if (navigateUrl.Length > 0 && !flag)
				{
					if (base.PageAdapter != null)
					{
						base.PageAdapter.RenderBeginHyperlink(writer, control.ResolveClientUrl(navigateUrl), true, SR.GetString("Adapter_GoLabel"), (text3 != null) ? text3 : ((this._currentAccessKey < 10) ? this._currentAccessKey++.ToString(CultureInfo.InvariantCulture) : null));
						writer.Write(HttpUtility.HtmlEncode(item.FormattedText));
						base.PageAdapter.RenderEndHyperlink(writer);
					}
					else
					{
						HyperLink hyperLink = new HyperLink();
						hyperLink.NavigateUrl = control.ResolveClientUrl(navigateUrl);
						string target = item.Target;
						if (string.IsNullOrEmpty(target))
						{
							target = control.Target;
						}
						if (!string.IsNullOrEmpty(target))
						{
							hyperLink.Target = target;
						}
						hyperLink.AccessKey = text3;
						hyperLink.Page = base.Page;
						if (writer is Html32TextWriter)
						{
							hyperLink.RenderBeginTag(writer);
							MenuAdapter.SpanPanel spanPanel = new MenuAdapter.SpanPanel();
							spanPanel.Page = base.Page;
							this.RenderStyle(writer, spanPanel, className, menuItemStyle, applyInlineBorder);
							spanPanel.RenderBeginTag(writer);
							item.RenderText(writer);
							spanPanel.RenderEndTag(writer);
							hyperLink.RenderEndTag(writer);
						}
						else
						{
							this.RenderStyle(writer, hyperLink, className, menuItemStyle, applyInlineBorder);
							hyperLink.RenderBeginTag(writer);
							item.RenderText(writer);
							hyperLink.RenderEndTag(writer);
						}
					}
				}
				else if (base.PageAdapter != null)
				{
					base.PageAdapter.RenderPostBackEvent(writer, control.UniqueID, (flag ? 'o' : 'b') + this.Escape(item.InternalValuePath), SR.GetString("Adapter_OKLabel"), item.FormattedText, null, (text3 != null) ? text3 : ((this._currentAccessKey < 10) ? this._currentAccessKey++.ToString(CultureInfo.InvariantCulture) : null));
					if (flag)
					{
						this.RenderExpand(writer, item, control);
					}
				}
				else
				{
					HyperLink hyperLink2 = new HyperLink();
					hyperLink2.NavigateUrl = base.Page.ClientScript.GetPostBackClientHyperlink(control, (flag ? 'o' : 'b') + this.Escape(item.InternalValuePath), true);
					hyperLink2.AccessKey = text3;
					hyperLink2.Page = base.Page;
					if (writer is Html32TextWriter)
					{
						hyperLink2.RenderBeginTag(writer);
						MenuAdapter.SpanPanel spanPanel2 = new MenuAdapter.SpanPanel();
						spanPanel2.Page = base.Page;
						this.RenderStyle(writer, spanPanel2, className, menuItemStyle, applyInlineBorder);
						spanPanel2.RenderBeginTag(writer);
						item.RenderText(writer);
						if (flag)
						{
							this.RenderExpand(writer, item, control);
						}
						spanPanel2.RenderEndTag(writer);
						hyperLink2.RenderEndTag(writer);
					}
					else
					{
						this.RenderStyle(writer, hyperLink2, className, menuItemStyle, applyInlineBorder);
						hyperLink2.RenderBeginTag(writer);
						item.RenderText(writer);
						if (flag)
						{
							this.RenderExpand(writer, item, control);
						}
						hyperLink2.RenderEndTag(writer);
					}
				}
			}
			else
			{
				item.RenderText(writer);
			}
			panel.RenderEndTag(writer);
			this.RenderBreak(writer);
			if (menuItemStyle != null && !menuItemStyle.ItemSpacing.IsEmpty)
			{
				this.RenderSpace(writer, menuItemStyle.ItemSpacing, control.Orientation);
			}
			string text4 = null;
			if (item.SeparatorImageUrl.Length != 0)
			{
				text4 = item.SeparatorImageUrl;
			}
			else if (depth < control.StaticDisplayLevels && control.StaticBottomSeparatorImageUrl.Length != 0)
			{
				text4 = control.StaticBottomSeparatorImageUrl;
			}
			else if (depth >= control.StaticDisplayLevels && control.DynamicBottomSeparatorImageUrl.Length != 0)
			{
				text4 = control.DynamicBottomSeparatorImageUrl;
			}
			if (text4 != null)
			{
				new Image
				{
					ImageUrl = text4,
					GenerateEmptyAlternateText = true,
					Page = base.Page
				}.RenderControl(writer);
				this.RenderBreak(writer);
			}
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0014E51C File Offset: 0x0014D51C
		private void RenderSpace(HtmlTextWriter writer, Unit space, Orientation orientation)
		{
			Image image = new Image();
			image.ImageUrl = this.Control.SpacerImageUrl;
			image.GenerateEmptyAlternateText = true;
			image.Page = base.Page;
			if (orientation == Orientation.Vertical)
			{
				image.Height = space;
				image.Width = Unit.Pixel(1);
				image.RenderControl(writer);
				writer.WriteBreak();
				return;
			}
			image.Width = space;
			image.Height = Unit.Pixel(1);
			image.RenderControl(writer);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0014E592 File Offset: 0x0014D592
		private void RenderStyle(HtmlTextWriter writer, WebControl control, string className, MenuItemStyle style, bool applyInlineBorder)
		{
			if (!string.IsNullOrEmpty(className))
			{
				control.CssClass = className;
				if (applyInlineBorder)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
					writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "1em");
					return;
				}
			}
			else if (style != null)
			{
				control.ApplyStyle(style);
			}
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0014E5CC File Offset: 0x0014D5CC
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0014E5D8 File Offset: 0x0014D5D8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Length == 0)
			{
				return;
			}
			char c = eventArgument[0];
			char c2 = c;
			if (c2 != 'b')
			{
				if (c2 != 'o')
				{
					if (c2 != 'u')
					{
						return;
					}
					if (this._path != null)
					{
						MenuItem menuItem = this.Control.Items.FindItem(this._path.Split(new char[]
						{
							'\\'
						}), 0);
						if (menuItem != null)
						{
							MenuItem parent = menuItem.Parent;
							if (parent != null && menuItem.Depth + 1 > this.Control.StaticDisplayLevels)
							{
								this._path = parent.InternalValuePath;
								return;
							}
							this._path = null;
							return;
						}
					}
				}
				else
				{
					string text = this.UnEscape(HttpUtility.UrlDecode(eventArgument.Substring(1)));
					int num = 0;
					for (int i = 0; i < text.Length; i++)
					{
						if (text[i] == '\\' && ++num >= this.Control.MaximumDepth)
						{
							throw new InvalidOperationException(SR.GetString("Menu_InvalidDepth"));
						}
					}
					MenuItem menuItem2 = this.Control.Items.FindItem(text.Split(new char[]
					{
						'\\'
					}), 0);
					if (menuItem2 != null)
					{
						if (menuItem2.ChildItems.Count > 0)
						{
							this._path = text;
							return;
						}
						this.Control.InternalRaisePostBackEvent(text);
						return;
					}
				}
			}
			else
			{
				this.Control.InternalRaisePostBackEvent(this.UnEscape(HttpUtility.UrlDecode(eventArgument.Substring(1))));
			}
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0014E74C File Offset: 0x0014D74C
		internal void SetPath(string path)
		{
			this._path = path;
		}

		// Token: 0x04002E03 RID: 11779
		private string _path;

		// Token: 0x04002E04 RID: 11780
		private Panel _menuPanel;

		// Token: 0x04002E05 RID: 11781
		private int _currentAccessKey;

		// Token: 0x04002E06 RID: 11782
		private MenuItem _titleItem;

		// Token: 0x0200069C RID: 1692
		private class SpanPanel : Panel
		{
			// Token: 0x1700150B RID: 5387
			// (get) Token: 0x060052C5 RID: 21189 RVA: 0x0014E75D File Offset: 0x0014D75D
			protected override HtmlTextWriterTag TagKey
			{
				get
				{
					return HtmlTextWriterTag.Span;
				}
			}
		}
	}
}

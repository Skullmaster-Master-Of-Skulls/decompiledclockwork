using System;
using System.Globalization;
using System.Text;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x020005C1 RID: 1473
	public class MenuAdapter : WebControlAdapter, IPostBackEventHandler
	{
		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06004AA5 RID: 19109 RVA: 0x000F802F File Offset: 0x000F622F
		protected new Menu Control
		{
			get
			{
				return (Menu)base.Control;
			}
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x000F803C File Offset: 0x000F623C
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

		// Token: 0x06004AA7 RID: 19111 RVA: 0x000F8088 File Offset: 0x000F6288
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

		// Token: 0x06004AA8 RID: 19112 RVA: 0x000F817F File Offset: 0x000F637F
		private string UnEscape(string path)
		{
			return path.Replace("\\\\", "\\").Replace("\\_\\", "\\\\").Replace("__", "_");
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x000F81AF File Offset: 0x000F63AF
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Control.Page.RegisterRequiresControlState(this.Control);
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x000F81CE File Offset: 0x000F63CE
		protected internal override void OnPreRender(EventArgs e)
		{
			this.Control.OnPreRender(e, false);
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x000F81E0 File Offset: 0x000F63E0
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

		// Token: 0x06004AAC RID: 19116 RVA: 0x000F8214 File Offset: 0x000F6414
		private void RenderBreak(HtmlTextWriter writer)
		{
			if (this.Control.Orientation == Orientation.Vertical)
			{
				writer.WriteBreak();
				return;
			}
			writer.Write(' ');
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x000F8234 File Offset: 0x000F6434
		protected override void RenderBeginTag(HtmlTextWriter writer)
		{
			Menu control = this.Control;
			if (control.SkipLinkText.Length != 0)
			{
				new HyperLink
				{
					NavigateUrl = "#" + control.ClientID + "_SkipLink",
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

		// Token: 0x06004AAE RID: 19118 RVA: 0x000F83B8 File Offset: 0x000F65B8
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

		// Token: 0x06004AAF RID: 19119 RVA: 0x000F8590 File Offset: 0x000F6790
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

		// Token: 0x06004AB0 RID: 19120 RVA: 0x000F8614 File Offset: 0x000F6814
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

		// Token: 0x06004AB1 RID: 19121 RVA: 0x000F8674 File Offset: 0x000F6874
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

		// Token: 0x06004AB2 RID: 19122 RVA: 0x000F87D8 File Offset: 0x000F69D8
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
						PageAdapter pageAdapter = base.PageAdapter;
						string targetUrl = control.ResolveClientUrl(navigateUrl);
						bool encodeUrl = true;
						string @string = SR.GetString("Adapter_GoLabel");
						string accessKey2;
						if (text3 == null)
						{
							if (this._currentAccessKey >= 10)
							{
								accessKey2 = null;
							}
							else
							{
								int currentAccessKey = this._currentAccessKey;
								this._currentAccessKey = currentAccessKey + 1;
								accessKey2 = currentAccessKey.ToString(CultureInfo.InvariantCulture);
							}
						}
						else
						{
							accessKey2 = text3;
						}
						pageAdapter.RenderBeginHyperlink(writer, targetUrl, encodeUrl, @string, accessKey2);
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
					PageAdapter pageAdapter2 = base.PageAdapter;
					string uniqueID = control.UniqueID;
					string argument = (flag ? 'o' : 'b').ToString() + this.Escape(item.InternalValuePath);
					string string2 = SR.GetString("Adapter_OKLabel");
					string formattedText = item.FormattedText;
					string postUrl = null;
					string accessKey3;
					if (text3 == null)
					{
						if (this._currentAccessKey >= 10)
						{
							accessKey3 = null;
						}
						else
						{
							int currentAccessKey = this._currentAccessKey;
							this._currentAccessKey = currentAccessKey + 1;
							accessKey3 = currentAccessKey.ToString(CultureInfo.InvariantCulture);
						}
					}
					else
					{
						accessKey3 = text3;
					}
					pageAdapter2.RenderPostBackEvent(writer, uniqueID, argument, string2, formattedText, postUrl, accessKey3);
					if (flag)
					{
						this.RenderExpand(writer, item, control);
					}
				}
				else
				{
					HyperLink hyperLink2 = new HyperLink();
					hyperLink2.NavigateUrl = base.Page.ClientScript.GetPostBackClientHyperlink(control, (flag ? 'o' : 'b').ToString() + this.Escape(item.InternalValuePath), true);
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

		// Token: 0x06004AB3 RID: 19123 RVA: 0x000F8ED0 File Offset: 0x000F70D0
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

		// Token: 0x06004AB4 RID: 19124 RVA: 0x000F8F46 File Offset: 0x000F7146
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

		// Token: 0x06004AB5 RID: 19125 RVA: 0x000F8F80 File Offset: 0x000F7180
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x000F8F8C File Offset: 0x000F718C
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Length == 0)
			{
				return;
			}
			char c = eventArgument[0];
			if (c != 'b')
			{
				if (c != 'o')
				{
					if (c != 'u')
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

		// Token: 0x06004AB7 RID: 19127 RVA: 0x000F90F2 File Offset: 0x000F72F2
		internal void SetPath(string path)
		{
			this._path = path;
		}

		// Token: 0x0400281C RID: 10268
		private string _path;

		// Token: 0x0400281D RID: 10269
		private Panel _menuPanel;

		// Token: 0x0400281E RID: 10270
		private int _currentAccessKey;

		// Token: 0x0400281F RID: 10271
		private MenuItem _titleItem;

		// Token: 0x020009FD RID: 2557
		private class SpanPanel : Panel
		{
			// Token: 0x17001E21 RID: 7713
			// (get) Token: 0x06006D51 RID: 27985 RVA: 0x00187383 File Offset: 0x00185583
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

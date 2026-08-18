using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SearchBox.Renderers
{
	// Token: 0x02000EF5 RID: 3829
	public class SearchBoxRenderer : IRenderer
	{
		// Token: 0x0600910D RID: 37133 RVA: 0x0020A51C File Offset: 0x0020871C
		public SearchBoxRenderer(RadSearchBox owner)
		{
			this._control = owner;
		}

		// Token: 0x17002DF2 RID: 11762
		// (get) Token: 0x0600910E RID: 37134 RVA: 0x0020A52B File Offset: 0x0020872B
		public virtual string CssClassFormatString
		{
			get
			{
				return "RadSearchBox RadSearchBox_{0}";
			}
		}

		// Token: 0x17002DF3 RID: 11763
		// (get) Token: 0x0600910F RID: 37135 RVA: 0x0020A532 File Offset: 0x00208732
		protected virtual string DropDownCssClassFormatString
		{
			get
			{
				return "rsbPopup rsbPopup_{0} {1}";
			}
		}

		// Token: 0x17002DF4 RID: 11764
		// (get) Token: 0x06009110 RID: 37136 RVA: 0x0020A539 File Offset: 0x00208739
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06009111 RID: 37137 RVA: 0x0020A540 File Offset: 0x00208740
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this._control.CssClass;
			short tabIndex = this._control.TabIndex;
			string arg = this._control.Enabled ? string.Empty : "rsbDisabled";
			string arg2 = string.IsNullOrEmpty(this._control.Label) ? string.Empty : "RadSearchBoxWithLabel";
			this._control.CssClass = string.Format("{0} {1} {2}", this._control.CssClass, arg, arg2).Trim();
			this._control.TabIndex = 0;
			this._control.CallBaseAddAttributesToRender(writer);
			this._control.CssClass = cssClass;
			this._control.TabIndex = tabIndex;
		}

		// Token: 0x06009112 RID: 37138 RVA: 0x0020A5F8 File Offset: 0x002087F8
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this._control.Label))
			{
				this.RenderLabel(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (this._control.InDesignMode)
			{
				this.RenderDesignTimeStyles(writer);
				this.RenderDesignTimeHtml(writer);
			}
			else
			{
				if (this._control._context != null)
				{
					this._control.ContextControl.RenderControl(writer);
				}
				this.RenderButtonsLeft(writer);
				this.RenderInput(writer);
				this.RenderButtonsRight(writer);
				if (this._control.ShowSearchButton)
				{
					this.RenderSearchButton(writer);
				}
			}
			writer.RenderEndTag();
			if (this._control.EnableAutoComplete)
			{
				this.RenderDropDown(writer);
			}
			if (this._control._context != null)
			{
				this.RenderContextDropDown(writer);
			}
		}

		// Token: 0x06009113 RID: 37139 RVA: 0x0020A6C8 File Offset: 0x002088C8
		protected virtual void RenderLabel(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1}", "rsbLabel", this._control.LabelCssClass).Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddAttribute(HtmlTextWriterAttribute.For, this._control.ClientID + "_Input");
			if (!this._control.LabelWidth.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this._control.LabelWidth.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(this._control.Label);
			writer.RenderEndTag();
		}

		// Token: 0x06009114 RID: 37140 RVA: 0x0020A770 File Offset: 0x00208970
		protected virtual void RenderButtonsLeft(HtmlTextWriter writer)
		{
			this.RenderPositionedButtons(writer, SearchBoxButtonPosition.Left);
		}

		// Token: 0x06009115 RID: 37141 RVA: 0x0020A77A File Offset: 0x0020897A
		protected virtual void RenderButtonsRight(HtmlTextWriter writer)
		{
			this.RenderPositionedButtons(writer, SearchBoxButtonPosition.Right);
		}

		// Token: 0x06009116 RID: 37142 RVA: 0x0020A784 File Offset: 0x00208984
		protected void RenderPositionedButtons(HtmlTextWriter writer, SearchBoxButtonPosition position)
		{
			List<SearchBoxButton> positionedButtons = this.GetPositionedButtons(position);
			if (positionedButtons.Count == 0)
			{
				return;
			}
			string value = string.Format("{0} {1}", "rsbButtons", (position == SearchBoxButtonPosition.Left) ? "rsbButtonsLeft" : "rsbButtonsRight");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			foreach (SearchBoxButton button in positionedButtons)
			{
				this.RenderButton(writer, button);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009117 RID: 37143 RVA: 0x0020A81C File Offset: 0x00208A1C
		protected virtual void RenderButton(HtmlTextWriter writer, SearchBoxButton button)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			if (!string.IsNullOrEmpty(button.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, button.ToolTip);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, button.ResolveClientUrl(button.ImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, button.AlternateText);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009118 RID: 37144 RVA: 0x0020A89C File Offset: 0x00208A9C
		protected virtual void RenderSearchButton(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1}", "rsbButton", "rsbButtonSearch");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbIcon rsbIconSearch");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009119 RID: 37145 RVA: 0x0020A908 File Offset: 0x00208B08
		protected virtual void RenderInput(HtmlTextWriter writer)
		{
			string arg = string.IsNullOrEmpty(this._control.EmptyMessage) ? string.Empty : "rsbEmptyMessage";
			string value = this._control.EmptyMessage;
			if (!string.IsNullOrEmpty(this._control.Text))
			{
				value = this._control.Text;
				arg = string.Empty;
			}
			string value2 = string.Format("{0} {1} {2}", "rsbInput", "radPreventDecorate", arg).Trim();
			if (this._control.TabIndex > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this._control.TabIndex.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value2);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this._control.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this._control.ClientID + "_Input");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x0600911A RID: 37146 RVA: 0x0020AA08 File Offset: 0x00208C08
		protected virtual void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbSlide");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.DropDownCssClassFormatString, this._control.RuntimeSkin, this._control.DropDownSettings.CssClass).Trim());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.HasHeaderTemplate)
			{
				this.RenderHeader(writer);
			}
			if (this.HasFooterTemplate)
			{
				this.RenderFooter(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600911B RID: 37147 RVA: 0x0020AA9C File Offset: 0x00208C9C
		protected virtual void RenderContextDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", "rsbSlide", "rsbSCSlide"));
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.DropDownCssClassFormatString, this._control.RuntimeSkin, this._control.SearchContext.DropDownCssClass).Trim());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this._control.ContextControl.ShowDefaultItem || this._control.ContextControl.Items.Count > 0)
			{
				this.RenderContextDropDownContent(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600911C RID: 37148 RVA: 0x0020AB54 File Offset: 0x00208D54
		protected virtual void RenderContextDropDownContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rsbList");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			bool flag = false;
			if (this._control.SearchContext.ShowDefaultItem)
			{
				this.RenderDefaultItem(writer);
			}
			foreach (object obj in this._control.ContextControl.Items)
			{
				SearchContextItem searchContextItem = (SearchContextItem)obj;
				if (searchContextItem.Selected)
				{
					if (flag)
					{
						throw new HttpException("Cannot have multiple items selected.");
					}
					flag = true;
				}
				searchContextItem.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600911D RID: 37149 RVA: 0x0020AC08 File Offset: 0x00208E08
		protected virtual void RenderDefaultItem(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1}", "rsbListItem", "rsbDefaultListItem");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.Write(this._control.Localization.DefaultItemText);
			writer.RenderEndTag();
		}

		// Token: 0x0600911E RID: 37150 RVA: 0x0020AC57 File Offset: 0x00208E57
		protected virtual void RenderHeader(HtmlTextWriter writer)
		{
			this._control.DropDownSettings.Header.CssClass = "rsbHeader";
			this._control.DropDownSettings.Header.RenderControl(writer);
		}

		// Token: 0x0600911F RID: 37151 RVA: 0x0020AC89 File Offset: 0x00208E89
		protected virtual void RenderFooter(HtmlTextWriter writer)
		{
			this._control.DropDownSettings.Footer.CssClass = "rsbFooter";
			this._control.DropDownSettings.Footer.RenderControl(writer);
		}

		// Token: 0x06009120 RID: 37152 RVA: 0x0020ACBC File Offset: 0x00208EBC
		protected virtual void RenderDesignTimeStyles(HtmlTextWriter writer)
		{
			string str = "160px";
			if (!this._control.Width.IsEmpty)
			{
				UnitType type = this._control.Width.Type;
				if (type != UnitType.Pixel)
				{
					if (type == UnitType.Percentage)
					{
						str = this._control.Width.Value + "%";
					}
				}
				else
				{
					str = this._control.Width.Value + "px";
				}
			}
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this._control));
			writer.Write("<style type=\"text/css\">");
			writer.Write(" .RadSearchBox { display:inline-block !important;\r\n                                            width: " + str + " !important; }");
			writer.Write(" .RadSearchBox .rsbButtonSearch .rsbIconSearch { \r\n                                            margin: 0; \r\n                                            display: block; \r\n                                            position: static;}");
			writer.Write("</style>");
		}

		// Token: 0x06009121 RID: 37153 RVA: 0x0020AD94 File Offset: 0x00208F94
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "1%");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderButtonsLeft(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderInput(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "1%");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderButtonsRight(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Width, "1%");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (this._control.ShowSearchButton)
			{
				this.RenderSearchButton(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06009122 RID: 37154 RVA: 0x0020AE94 File Offset: 0x00209094
		protected List<SearchBoxButton> GetPositionedButtons(SearchBoxButtonPosition position)
		{
			List<SearchBoxButton> list = new List<SearchBoxButton>();
			foreach (object obj in this._control.Buttons)
			{
				SearchBoxButton searchBoxButton = (SearchBoxButton)obj;
				if (searchBoxButton.Position == position)
				{
					list.Add(searchBoxButton);
				}
			}
			return list;
		}

		// Token: 0x17002DF5 RID: 11765
		// (get) Token: 0x06009123 RID: 37155 RVA: 0x0020AF04 File Offset: 0x00209104
		private bool HasHeaderTemplate
		{
			get
			{
				return this._control.DropDownSettings.HeaderTemplate != null || this._control.DropDownSettings.Header.Controls.Count > 0;
			}
		}

		// Token: 0x17002DF6 RID: 11766
		// (get) Token: 0x06009124 RID: 37156 RVA: 0x0020AF37 File Offset: 0x00209137
		private bool HasFooterTemplate
		{
			get
			{
				return this._control.DropDownSettings.FooterTemplate != null || this._control.DropDownSettings.Footer.Controls.Count > 0;
			}
		}

		// Token: 0x0400293B RID: 10555
		private RadSearchBox _control;
	}
}

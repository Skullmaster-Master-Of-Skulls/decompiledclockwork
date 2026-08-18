using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x02000FED RID: 4077
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(DatePickingInput))]
	internal class DatePickingInput : RadDateInput
	{
		// Token: 0x1700326A RID: 12906
		// (get) Token: 0x06009F9A RID: 40858 RVA: 0x0023952A File Offset: 0x0023772A
		// (set) Token: 0x06009F9B RID: 40859 RVA: 0x00239532 File Offset: 0x00237732
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x1700326B RID: 12907
		// (get) Token: 0x06009F9C RID: 40860 RVA: 0x0023953B File Offset: 0x0023773B
		// (set) Token: 0x06009F9D RID: 40861 RVA: 0x00239543 File Offset: 0x00237743
		[NotifyParentProperty(true)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x1700326C RID: 12908
		// (get) Token: 0x06009F9E RID: 40862 RVA: 0x0023954C File Offset: 0x0023774C
		// (set) Token: 0x06009F9F RID: 40863 RVA: 0x00239554 File Offset: 0x00237754
		[NotifyParentProperty(true)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x1700326D RID: 12909
		// (get) Token: 0x06009FA0 RID: 40864 RVA: 0x0023955D File Offset: 0x0023775D
		// (set) Token: 0x06009FA1 RID: 40865 RVA: 0x00239565 File Offset: 0x00237765
		[NotifyParentProperty(true)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x1700326E RID: 12910
		// (get) Token: 0x06009FA2 RID: 40866 RVA: 0x0023956E File Offset: 0x0023776E
		// (set) Token: 0x06009FA3 RID: 40867 RVA: 0x00239576 File Offset: 0x00237776
		[NotifyParentProperty(true)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x1700326F RID: 12911
		// (get) Token: 0x06009FA4 RID: 40868 RVA: 0x0023957F File Offset: 0x0023777F
		// (set) Token: 0x06009FA5 RID: 40869 RVA: 0x00239587 File Offset: 0x00237787
		[NotifyParentProperty(true)]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x17003270 RID: 12912
		// (get) Token: 0x06009FA6 RID: 40870 RVA: 0x00239590 File Offset: 0x00237790
		// (set) Token: 0x06009FA7 RID: 40871 RVA: 0x00239598 File Offset: 0x00237798
		[NotifyParentProperty(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x17003271 RID: 12913
		// (get) Token: 0x06009FA8 RID: 40872 RVA: 0x002395A1 File Offset: 0x002377A1
		// (set) Token: 0x06009FA9 RID: 40873 RVA: 0x002395A9 File Offset: 0x002377A9
		[NotifyParentProperty(true)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x17003272 RID: 12914
		// (get) Token: 0x06009FAA RID: 40874 RVA: 0x002395B2 File Offset: 0x002377B2
		// (set) Token: 0x06009FAB RID: 40875 RVA: 0x002395BA File Offset: 0x002377BA
		[NotifyParentProperty(true)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17003273 RID: 12915
		// (get) Token: 0x06009FAC RID: 40876 RVA: 0x002395C3 File Offset: 0x002377C3
		// (set) Token: 0x06009FAD RID: 40877 RVA: 0x002395CB File Offset: 0x002377CB
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17003274 RID: 12916
		// (get) Token: 0x06009FAE RID: 40878 RVA: 0x002395D4 File Offset: 0x002377D4
		// (set) Token: 0x06009FAF RID: 40879 RVA: 0x002395DC File Offset: 0x002377DC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Label
		{
			get
			{
				return base.Label;
			}
			set
			{
				base.Label = value;
			}
		}

		// Token: 0x17003275 RID: 12917
		// (get) Token: 0x06009FB0 RID: 40880 RVA: 0x002395E5 File Offset: 0x002377E5
		// (set) Token: 0x06009FB1 RID: 40881 RVA: 0x002395ED File Offset: 0x002377ED
		[Browsable(false)]
		[NotifyParentProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string LabelCssClass
		{
			get
			{
				return base.LabelCssClass;
			}
			set
			{
				base.LabelCssClass = value;
			}
		}

		// Token: 0x17003276 RID: 12918
		// (get) Token: 0x06009FB2 RID: 40882 RVA: 0x002395F6 File Offset: 0x002377F6
		// (set) Token: 0x06009FB3 RID: 40883 RVA: 0x002395FE File Offset: 0x002377FE
		[NotifyParentProperty(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17003277 RID: 12919
		// (get) Token: 0x06009FB4 RID: 40884 RVA: 0x00239607 File Offset: 0x00237807
		// (set) Token: 0x06009FB5 RID: 40885 RVA: 0x0023960F File Offset: 0x0023780F
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17003278 RID: 12920
		// (get) Token: 0x06009FB6 RID: 40886 RVA: 0x00239618 File Offset: 0x00237818
		// (set) Token: 0x06009FB7 RID: 40887 RVA: 0x00239620 File Offset: 0x00237820
		[NotifyParentProperty(true)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x17003279 RID: 12921
		// (get) Token: 0x06009FB8 RID: 40888 RVA: 0x00239629 File Offset: 0x00237829
		// (set) Token: 0x06009FB9 RID: 40889 RVA: 0x00239631 File Offset: 0x00237831
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x1700327A RID: 12922
		// (get) Token: 0x06009FBA RID: 40890 RVA: 0x0023963A File Offset: 0x0023783A
		[NotifyParentProperty(true)]
		protected override string TagName
		{
			get
			{
				if ((this.isOnlyInputRendered() || this.EnableSingleInputRendering) && !this.IsLightweightRendering)
				{
					return "span";
				}
				return "div";
			}
		}

		// Token: 0x1700327B RID: 12923
		// (get) Token: 0x06009FBB RID: 40891 RVA: 0x0023965F File Offset: 0x0023785F
		private bool IsLightweightRendering
		{
			get
			{
				if (this._isLightweightRendering == null)
				{
					this._isLightweightRendering = new bool?(this.ResolvedRenderMode == RenderMode.Lightweight);
				}
				return this._isLightweightRendering.Value;
			}
		}

		// Token: 0x1700327C RID: 12924
		// (get) Token: 0x06009FBC RID: 40892 RVA: 0x0023968D File Offset: 0x0023788D
		// (set) Token: 0x06009FBD RID: 40893 RVA: 0x00239695 File Offset: 0x00237895
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "100%")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x06009FBE RID: 40894 RVA: 0x0023969E File Offset: 0x0023789E
		public override string ToString()
		{
			return "RadDateInput";
		}

		// Token: 0x1700327D RID: 12925
		// (get) Token: 0x06009FBF RID: 40895 RVA: 0x002396A5 File Offset: 0x002378A5
		// (set) Token: 0x06009FC0 RID: 40896 RVA: 0x002396AD File Offset: 0x002378AD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x06009FC1 RID: 40897 RVA: 0x002396B6 File Offset: 0x002378B6
		public override void Focus()
		{
			if (this.ClientID == null)
			{
				throw new ArgumentException("Please, use the picker Focus() method instead to focus the Input component.");
			}
			base.Focus();
		}

		// Token: 0x06009FC2 RID: 40898 RVA: 0x002396D1 File Offset: 0x002378D1
		protected override string GetPostBackEventReference()
		{
			return this.Page.ClientScript.GetPostBackEventReference(this.Parent, "");
		}

		// Token: 0x06009FC3 RID: 40899 RVA: 0x002396F0 File Offset: 0x002378F0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			RadDatePicker radDatePicker = this.Parent as RadDatePicker;
			RadMonthYearPicker radMonthYearPicker = this.Parent as RadMonthYearPicker;
			bool flag = false;
			if (radDatePicker != null && !radDatePicker.isOnlyInputRendered() && this.isOnlyInputRendered())
			{
				flag = true;
			}
			if (radMonthYearPicker != null && !radMonthYearPicker.IsOnlyInputRendered() && this.isOnlyInputRendered())
			{
				flag = true;
			}
			if (flag && !this.IsLightweightRendering)
			{
				writer.AddStyleAttribute("display", "block");
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x06009FC4 RID: 40900 RVA: 0x00239763 File Offset: 0x00237963
		protected override void RenderBrowserSpecificStyles(HtmlTextWriter writer)
		{
		}

		// Token: 0x06009FC5 RID: 40901 RVA: 0x00239765 File Offset: 0x00237965
		protected override bool shouldRenderWhiteSpace()
		{
			return false;
		}

		// Token: 0x06009FC6 RID: 40902 RVA: 0x00239768 File Offset: 0x00237968
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = false;
			bool flag2 = base.LoadPostData(postDataKey, postCollection);
			if (this.invalidDateStringFlag)
			{
				flag = true;
			}
			RadDatePicker radDatePicker = this.Parent as RadDatePicker;
			RadMonthYearPicker radMonthYearPicker = this.Parent as RadMonthYearPicker;
			if (radDatePicker != null)
			{
				base.MinDate = radDatePicker.MinDate;
				base.MaxDate = radDatePicker.MaxDate;
			}
			else if (radMonthYearPicker != null)
			{
				base.MinDate = radMonthYearPicker.MinDate;
				base.MaxDate = radMonthYearPicker.MaxDate;
			}
			if (flag2)
			{
				if (radDatePicker != null)
				{
					radDatePicker.SelectedDateLoaded(base.DbSelectedDate);
				}
				else if (radMonthYearPicker != null)
				{
					radMonthYearPicker.SelectedDateLoaded(base.DbSelectedDate);
				}
			}
			if (flag)
			{
				this.invalidDateStringFlag = true;
			}
			return flag2;
		}

		// Token: 0x06009FC7 RID: 40903 RVA: 0x00239808 File Offset: 0x00237A08
		protected override void SetStyleClasses()
		{
			if (!base.EmptySkin)
			{
				base.HoveredStyle.CssClass = base.FormatCssClass("riTextBox riHover", base.HoveredStyle.CssClass);
				base.InvalidStyle.CssClass = base.FormatCssClass("riTextBox riError", base.InvalidStyle.CssClass);
				base.DisabledStyle.CssClass = base.FormatCssClass("riTextBox riDisabled", base.DisabledStyle.CssClass);
				base.EnabledStyle.CssClass = base.FormatCssClass("riTextBox riEnabled", base.EnabledStyle.CssClass);
				base.FocusedStyle.CssClass = base.FormatCssClass("riTextBox riFocused", base.FocusedStyle.CssClass);
				base.EmptyMessageStyle.CssClass = base.FormatCssClass("riTextBox riEmpty", base.EmptyMessageStyle.CssClass);
				base.ReadOnlyStyle.CssClass = base.FormatCssClass("riTextBox riRead", base.ReadOnlyStyle.CssClass);
				this.LabelCssClass = base.FormatCssClass("riLabel", this.LabelCssClass);
			}
		}

		// Token: 0x06009FC8 RID: 40904 RVA: 0x0023991E File Offset: 0x00237B1E
		protected override bool isOnlyInputRendered()
		{
			return true;
		}

		// Token: 0x06009FC9 RID: 40905 RVA: 0x00239924 File Offset: 0x00237B24
		protected override void OnPreRender(EventArgs e)
		{
			if (this.Parent is RadTimePicker && this.ViewState["OriginalValue"] == null && !this.EnableSingleInputRendering)
			{
				this.ViewState["OriginalValue"] = (((this.Parent as RadTimePicker).SelectedDate != null) ? (this.Parent as RadTimePicker).SelectedDate.Value.ToShortTimeString() : "");
			}
			base.OnPreRender(e);
		}

		// Token: 0x06009FCA RID: 40906 RVA: 0x002399B0 File Offset: 0x00237BB0
		protected override void RenderContentsSingleInput(HtmlTextWriter writer)
		{
			this.RenderLabel(writer, this.ClientID);
			if (!string.IsNullOrEmpty(this.Label) && !this.IsLightweightRendering)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "riContentWrapper");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.CalculateInputWidth().ToString());
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			this.RenderInnerWrapperContent(writer);
			if (this.IsLightweightRendering)
			{
				this.RenderLightweightPopups(writer);
			}
			if (!string.IsNullOrEmpty(this.Label) && !this.IsLightweightRendering)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06009FCB RID: 40907 RVA: 0x00239A44 File Offset: 0x00237C44
		protected override void RenderBeginTagSingleInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_wrapper");
			if (!base.Display)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			if (this.ToolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			string absolutePositionValue = InputUtil.GetAbsolutePositionValue(base.Style);
			if (!string.IsNullOrEmpty(absolutePositionValue))
			{
				writer.AddAttribute("style", absolutePositionValue);
			}
			string str = this.IsLightweightRendering ? string.Empty : "riSingle ";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, str + this.GetOffsetAdditionalClasses() + base.FormatCssClass("RadInput", this.CssClass));
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "static");
				this.SetDefaultSize();
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.CalculateWrapperWidth().ToString());
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
				writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, "15px");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				return;
			}
			if (this.ResolvedRenderMode != RenderMode.Lightweight)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.CalculateWrapperWidth().ToString());
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				return;
			}
			if (!base.EnabledStyle.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.EnabledStyle.Width.ToString());
			}
			if (!base.EnabledStyle.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.EnabledStyle.Height.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06009FCC RID: 40908 RVA: 0x00239C10 File Offset: 0x00237E10
		protected virtual void RenderLightweightPopups(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcSelect");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			RadTimePicker radTimePicker = this.Parent as RadTimePicker;
			RadDateTimePicker radDateTimePicker = this.Parent as RadDateTimePicker;
			RadDatePicker radDatePicker = this.Parent as RadDatePicker;
			RadMonthYearPicker radMonthYearPicker = this.Parent as RadMonthYearPicker;
			if (radTimePicker != null)
			{
				if (radTimePicker.TimePopupButton.Visible)
				{
					radTimePicker.TimePopupButton.RenderControl(writer);
				}
				radTimePicker.TimeView.RenderControl(writer);
			}
			else if (radDateTimePicker != null)
			{
				if (radDateTimePicker.DatePopupButton.Visible)
				{
					radDateTimePicker.DatePopupButton.RenderControl(writer);
				}
				if (radDateTimePicker.TimePopupButton.Visible)
				{
					radDateTimePicker.TimePopupButton.RenderControl(writer);
				}
				radDateTimePicker.Calendar.RenderControl(writer);
				radDateTimePicker.TimeView.RenderControl(writer);
			}
			else if (radDatePicker != null)
			{
				if (radDatePicker.DatePopupButton.Visible)
				{
					radDatePicker.DatePopupButton.RenderControl(writer);
				}
				radDatePicker.Calendar.RenderControl(writer);
			}
			else if (radMonthYearPicker != null)
			{
				if (radMonthYearPicker.DatePopupButton.Visible)
				{
					radMonthYearPicker.DatePopupButton.RenderControl(writer);
				}
				radMonthYearPicker.MonthYearTableView.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06009FCD RID: 40909 RVA: 0x00239D38 File Offset: 0x00237F38
		protected override void RenderLabel(HtmlTextWriter writer, string forID)
		{
			if (string.IsNullOrEmpty(this.Label) || this.IsLightweightRendering)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.LabelCssClass))
			{
				writer.AddAttribute("class", this.LabelCssClass);
			}
			if (base.LabelWidth != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.LabelWidth.ToString());
			}
			writer.AddAttribute("for", forID);
			writer.AddAttribute("id", this.ClientID + "_Label");
			writer.RenderBeginTag("label");
			writer.Write(this.Label);
			writer.RenderEndTag();
		}

		// Token: 0x06009FCE RID: 40910 RVA: 0x00239DEC File Offset: 0x00237FEC
		public override PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			return PropertyFilter.Filter(properties);
		}

		// Token: 0x06009FCF RID: 40911 RVA: 0x00239E08 File Offset: 0x00238008
		public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			return PropertyFilter.Filter(properties);
		}

		// Token: 0x04002C9B RID: 11419
		private bool? _isLightweightRendering = null;
	}
}

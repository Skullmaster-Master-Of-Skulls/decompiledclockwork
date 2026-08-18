using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A13 RID: 2579
	public abstract class ComboRendererBase : RendererBase
	{
		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x00170AD7 File Offset: 0x0016ECD7
		// (set) Token: 0x060061CE RID: 25038 RVA: 0x00170ADF File Offset: 0x0016ECDF
		protected RadComboBox Owner { get; set; }

		// Token: 0x060061CF RID: 25039 RVA: 0x00170AE8 File Offset: 0x0016ECE8
		public ComboRendererBase(RadComboBox owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x060061D0 RID: 25040 RVA: 0x00170AF7 File Offset: 0x0016ECF7
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x060061D1 RID: 25041 RVA: 0x00170AFB File Offset: 0x0016ECFB
		public override string CssClassFormatString
		{
			get
			{
				return this.GetCssClass();
			}
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x00170B03 File Offset: 0x0016ED03
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x00170B0A File Offset: 0x0016ED0A
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderLabel(writer);
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x00170B1C File Offset: 0x0016ED1C
		protected void RenderLabel(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.Owner.Label))
			{
				return;
			}
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("label");
			htmlGenericControl.Attributes.Add("for", this.Owner.ClientID + "_Input");
			htmlGenericControl.InnerText = this.Owner.Label;
			if (!string.IsNullOrEmpty(this.Owner.LabelCssClass))
			{
				htmlGenericControl.Attributes.Add("class", string.Format("rcbLabel {0}", this.Owner.LabelCssClass));
			}
			else
			{
				htmlGenericControl.Attributes.Add("class", "rcbLabel");
			}
			htmlGenericControl.RenderControl(writer);
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x00170BD2 File Offset: 0x0016EDD2
		protected virtual void RenderDropDown(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x00170BDC File Offset: 0x0016EDDC
		protected virtual void RenderItems(HtmlTextWriter writer)
		{
			foreach (object obj in this.Owner.Items)
			{
				RadComboBoxItem radComboBoxItem = (RadComboBoxItem)obj;
				radComboBoxItem.RenderControl(writer);
			}
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x00170C3C File Offset: 0x0016EE3C
		protected string GetCssClass()
		{
			string format = "RadComboBox RadComboBox_{{0}}{0}";
			string arg = string.Empty;
			if (this.Owner.Label.Length > 0)
			{
				arg = " RadComboBoxWithLabel";
			}
			return string.Format(format, arg);
		}
	}
}

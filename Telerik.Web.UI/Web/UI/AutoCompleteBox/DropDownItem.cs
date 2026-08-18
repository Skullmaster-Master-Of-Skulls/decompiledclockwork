using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.AutoCompleteBox
{
	// Token: 0x020009B4 RID: 2484
	[ToolboxItem(false)]
	public class DropDownItem : Control, INamingContainer
	{
		// Token: 0x06005F1E RID: 24350 RVA: 0x001223F9 File Offset: 0x001205F9
		public DropDownItem()
		{
			this.Templated = false;
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00122408 File Offset: 0x00120608
		public DropDownItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x00122417 File Offset: 0x00120617
		public DropDownItem(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x0012242D File Offset: 0x0012062D
		public DropDownItem(object dataItem, string text, string value) : this(text, value)
		{
			this.DataItem = dataItem;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x0012243E File Offset: 0x0012063E
		public DropDownItem(object dataItem, string dataTextField, string dataTextFormatString, string dataValueField) : this()
		{
			this.BindItem(dataItem, dataTextField, dataTextFormatString, dataValueField);
		}

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x06005F23 RID: 24355 RVA: 0x00122451 File Offset: 0x00120651
		// (set) Token: 0x06005F24 RID: 24356 RVA: 0x00122459 File Offset: 0x00120659
		[TemplateContainer(typeof(DropDownItem))]
		public virtual ITemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
			}
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x00122462 File Offset: 0x00120662
		// (set) Token: 0x06005F26 RID: 24358 RVA: 0x0012246A File Offset: 0x0012066A
		[Browsable(false)]
		public virtual object DataItem { get; set; }

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06005F27 RID: 24359 RVA: 0x00122473 File Offset: 0x00120673
		// (set) Token: 0x06005F28 RID: 24360 RVA: 0x0012247B File Offset: 0x0012067B
		public string Text { get; set; }

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x06005F29 RID: 24361 RVA: 0x00122484 File Offset: 0x00120684
		// (set) Token: 0x06005F2A RID: 24362 RVA: 0x0012248C File Offset: 0x0012068C
		public string Value { get; set; }

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x00122495 File Offset: 0x00120695
		// (set) Token: 0x06005F2C RID: 24364 RVA: 0x0012249D File Offset: 0x0012069D
		public bool Templated { get; set; }

		// Token: 0x06005F2D RID: 24365 RVA: 0x001224A6 File Offset: 0x001206A6
		private void BindItem(object dataItem, string dataTextField, string dataTextFormatString, string dataValueField)
		{
			this.Text = DropDownItem.GetTextFromDataItem(dataItem, dataTextField, dataTextFormatString);
			this.Value = DropDownItem.GetValueFromDataItem(dataItem, dataValueField);
			this.DataItem = dataItem;
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x001224CC File Offset: 0x001206CC
		internal static string GetValueFromDataItem(object dataItem, string dataValueField)
		{
			string result = null;
			if (!string.IsNullOrEmpty(dataValueField))
			{
				result = DataBinder.GetPropertyValue(dataItem, dataValueField, null);
			}
			return result;
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x001224F0 File Offset: 0x001206F0
		internal static string GetTextFromDataItem(object dataItem, string dataTextField, string dataTextFormatString)
		{
			if (!string.IsNullOrEmpty(dataTextField))
			{
				try
				{
					return DataBinder.GetPropertyValue(dataItem, dataTextField, dataTextFormatString);
				}
				catch (ArgumentException)
				{
					if (dataItem is DataRowView)
					{
						return "Databound";
					}
					throw;
				}
			}
			string result;
			if (!string.IsNullOrEmpty(dataTextFormatString))
			{
				result = string.Format(CultureInfo.CurrentCulture, dataTextFormatString, new object[]
				{
					dataItem
				});
			}
			else
			{
				result = dataItem.ToString();
			}
			return result;
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x00122560 File Offset: 0x00120760
		internal void RenderContents(HtmlTextWriter writer)
		{
			string value = this.Templated ? "racItem racTemplate" : "racItem";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if (this.Templated)
			{
				this.RenderTemplate(writer);
			}
			else
			{
				writer.Write(this.Text);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x001225B8 File Offset: 0x001207B8
		private void RenderTemplate(HtmlTextWriter writer)
		{
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				control.RenderControl(writer);
			}
		}

		// Token: 0x040016DC RID: 5852
		private ITemplate _template;
	}
}

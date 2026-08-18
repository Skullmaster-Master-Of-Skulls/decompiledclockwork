using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web.UI;
using Telerik.Web.UI.SearchBox.Renderers;

namespace Telerik.Web.UI.SearchBox
{
	// Token: 0x02000EF0 RID: 3824
	[ToolboxItem(false)]
	public class DropDownItem : Control, INamingContainer
	{
		// Token: 0x060090E0 RID: 37088 RVA: 0x0020A01E File Offset: 0x0020821E
		public DropDownItem()
		{
			this.Templated = false;
		}

		// Token: 0x060090E1 RID: 37089 RVA: 0x0020A02D File Offset: 0x0020822D
		public DropDownItem(string text) : this()
		{
			this.DisplayText = text;
		}

		// Token: 0x060090E2 RID: 37090 RVA: 0x0020A03C File Offset: 0x0020823C
		public DropDownItem(object dataItem, string text, string value, string[] dataKeyNames) : this()
		{
			this.BindItem(dataItem, text, value, dataKeyNames);
		}

		// Token: 0x060090E3 RID: 37091 RVA: 0x0020A04F File Offset: 0x0020824F
		public DropDownItem(object dataItem, string dataTextField, string dataTextFormatString, string dataValueField, string[] dataKeyNames) : this()
		{
			this.BindItem(dataItem, dataTextField, dataTextFormatString, dataValueField, dataKeyNames);
		}

		// Token: 0x17002DE1 RID: 11745
		// (get) Token: 0x060090E4 RID: 37092 RVA: 0x0020A064 File Offset: 0x00208264
		protected IRenderer Renderer
		{
			get
			{
				if (this._renderer != null)
				{
					return this._renderer;
				}
				return this._renderer = this.CreateControlRenderer();
			}
		}

		// Token: 0x060090E5 RID: 37093 RVA: 0x0020A08F File Offset: 0x0020828F
		protected internal virtual IRenderer CreateControlRenderer()
		{
			return new DropDownItemRenderer(this);
		}

		// Token: 0x17002DE2 RID: 11746
		// (get) Token: 0x060090E6 RID: 37094 RVA: 0x0020A097 File Offset: 0x00208297
		// (set) Token: 0x060090E7 RID: 37095 RVA: 0x0020A09F File Offset: 0x0020829F
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x17002DE3 RID: 11747
		// (get) Token: 0x060090E8 RID: 37096 RVA: 0x0020A0A8 File Offset: 0x002082A8
		internal Dictionary<string, object> _DataItem
		{
			get
			{
				if (this._dataItem == null)
				{
					this._dataItem = new Dictionary<string, object>();
				}
				return this._dataItem;
			}
		}

		// Token: 0x17002DE4 RID: 11748
		// (get) Token: 0x060090E9 RID: 37097 RVA: 0x0020A0C3 File Offset: 0x002082C3
		// (set) Token: 0x060090EA RID: 37098 RVA: 0x0020A0CB File Offset: 0x002082CB
		internal string DisplayText { get; set; }

		// Token: 0x17002DE5 RID: 11749
		// (get) Token: 0x060090EB RID: 37099 RVA: 0x0020A0D4 File Offset: 0x002082D4
		// (set) Token: 0x060090EC RID: 37100 RVA: 0x0020A0DC File Offset: 0x002082DC
		internal string Value { get; set; }

		// Token: 0x17002DE6 RID: 11750
		// (get) Token: 0x060090ED RID: 37101 RVA: 0x0020A0E5 File Offset: 0x002082E5
		// (set) Token: 0x060090EE RID: 37102 RVA: 0x0020A0ED File Offset: 0x002082ED
		public bool Templated { get; set; }

		// Token: 0x060090EF RID: 37103 RVA: 0x0020A0F8 File Offset: 0x002082F8
		internal void ApplyTemplate(ITemplate parentTemplate)
		{
			if (parentTemplate == null)
			{
				return;
			}
			int num = this.Controls.Count;
			parentTemplate.InstantiateIn(this);
			while (num > 0 && !this.Controls.IsReadOnly)
			{
				this.Controls.Add(this.Controls[0]);
				num--;
			}
			this.Templated = true;
			this.DataBind();
		}

		// Token: 0x060090F0 RID: 37104 RVA: 0x0020A158 File Offset: 0x00208358
		private void BindItem(object dataItem, string dataTextField, string dataTextFormatString, string dataValueField, string[] dataKeyNames)
		{
			string textFromDataItem = DropDownItem.GetTextFromDataItem(dataItem, dataTextField, dataTextFormatString);
			string valueFromDataItem = DropDownItem.GetValueFromDataItem(dataItem, dataValueField);
			this.BindItem(dataItem, textFromDataItem, valueFromDataItem, dataKeyNames);
		}

		// Token: 0x060090F1 RID: 37105 RVA: 0x0020A184 File Offset: 0x00208384
		private void BindItem(object dataItem, string text, string value, string[] dataKeyNames)
		{
			this.DisplayText = text;
			this.Value = value;
			for (int i = 0; i < dataKeyNames.Length; i++)
			{
				string text2 = dataKeyNames[i].Trim();
				this._DataItem.Add(text2, DataBinder.GetPropertyValue(dataItem, text2, null));
			}
			this.DataItem = dataItem;
		}

		// Token: 0x060090F2 RID: 37106 RVA: 0x0020A1D4 File Offset: 0x002083D4
		internal static string GetValueFromDataItem(object dataItem, string dataValueField)
		{
			string result = null;
			if (!string.IsNullOrEmpty(dataValueField))
			{
				result = DataBinder.GetPropertyValue(dataItem, dataValueField, null);
			}
			return result;
		}

		// Token: 0x060090F3 RID: 37107 RVA: 0x0020A1F8 File Offset: 0x002083F8
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

		// Token: 0x060090F4 RID: 37108 RVA: 0x0020A268 File Offset: 0x00208468
		internal void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x04002928 RID: 10536
		private IRenderer _renderer;

		// Token: 0x04002929 RID: 10537
		private Dictionary<string, object> _dataItem;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.AutoCompleteBox;

namespace Telerik.Web.UI
{
	// Token: 0x020009C2 RID: 2498
	[RequiredScript(typeof(MaterialRipple))]
	[ToolboxData("<{0}:RadAutoCompleteBox Runat=\"server\"></{0}:RadAutoCompleteBox>")]
	[Designer("Telerik.Web.Design.RadAutoCompleteBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[RequiredScript(typeof(DropDown), 3)]
	[ClientScriptResource("Telerik.Web.UI.RadAutoCompleteBox", "Telerik.Web.UI.AutoCompleteBox.RadAutoCompleteBoxScripts.js", LoadOrder = 4)]
	[DefaultProperty("Items")]
	[XmlRoot("AutoCompleteBox")]
	[ValidationProperty("Text")]
	[ControlValueProperty("Text")]
	[TelerikToolboxCategory("Data Editing")]
	[ToolboxBitmap(typeof(RadAutoCompleteBox), "Telerik.Web.UI.AutoCompleteBox.png")]
	[EmbeddedSkin("AutoCompleteBox", "Default", typeof(RadAutoCompleteBox))]
	[RequiredScript(typeof(ChangeLog), 2)]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("AutoCompleteBox", typeof(RadAutoCompleteBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadAutoCompleteBox))]
	public class RadAutoCompleteBox : RadDataBoundControl, INamingContainer, IPostBackEventHandler, ICallbackEventHandler, IFlatBoundContainer, ILocalizableControl, ILabelableControl
	{
		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x001227A3 File Offset: 0x001209A3
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06005F63 RID: 24419 RVA: 0x001227A6 File Offset: 0x001209A6
		protected internal AutoCompleteBoxEntryCollection Children
		{
			[DebuggerStepThrough]
			get
			{
				if (this._children == null)
				{
					this._children = this.CreateChildItemCollection();
				}
				return this._children;
			}
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x001227C2 File Offset: 0x001209C2
		protected AutoCompleteBoxEntryCollection CreateChildItemCollection()
		{
			return new AutoCompleteBoxEntryCollection(this);
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06005F65 RID: 24421 RVA: 0x001227CA File Offset: 0x001209CA
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x001227CE File Offset: 0x001209CE
		// (set) Token: 0x06005F67 RID: 24423 RVA: 0x001227D6 File Offset: 0x001209D6
		internal bool EndOfItems
		{
			get
			{
				return this._endOfItems;
			}
			set
			{
				this._endOfItems = true;
			}
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x001227DF File Offset: 0x001209DF
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x06005F69 RID: 24425 RVA: 0x001227E2 File Offset: 0x001209E2
		// (set) Token: 0x06005F6A RID: 24426 RVA: 0x001227FD File Offset: 0x001209FD
		protected internal List<DropDownItem> DropDownItems
		{
			get
			{
				if (this._dropDownItems == null)
				{
					this._dropDownItems = new List<DropDownItem>();
				}
				return this._dropDownItems;
			}
			set
			{
				this._dropDownItems = value;
			}
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x06005F6B RID: 24427 RVA: 0x00122806 File Offset: 0x00120A06
		internal bool UsingWebServiceBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.WebServiceSettings.Path);
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06005F6C RID: 24428 RVA: 0x0012281B File Offset: 0x00120A1B
		internal bool IsUsingODataBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataModelID);
			}
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x0012282B File Offset: 0x00120A2B
		private bool IsBoundToLinqDataSource()
		{
			return this.GetData() is LinqDataSourceView;
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x00122840 File Offset: 0x00120A40
		private bool IsBoundToEntityDataSource()
		{
			return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.EntityDataSourceView";
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06005F6F RID: 24431 RVA: 0x00122869 File Offset: 0x00120A69
		// (set) Token: 0x06005F70 RID: 24432 RVA: 0x00122871 File Offset: 0x00120A71
		private protected new DataSourceSelectArguments SelectArguments { protected get; private set; }

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06005F71 RID: 24433 RVA: 0x0012287A File Offset: 0x00120A7A
		public string ControlId
		{
			get
			{
				return this.ClientID + "_Input";
			}
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x0012288C File Offset: 0x00120A8C
		public RadAutoCompleteBox()
		{
			this._webServiceSettings = new WebServiceSettings(this.ViewState);
			this._expandAnimation = new AnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new AnimationSettings("Collapse", this.ViewState);
			this._tokensSettings = new AutoCompleteBoxTokensSettings(this.ViewState);
			this._textSettings = new AutoCompleteBoxTextSettings(this.ViewState);
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06005F73 RID: 24435 RVA: 0x00122914 File Offset: 0x00120B14
		protected override string CssClassFormatString
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Label))
				{
					return this.cssClassFormatString + " RadAutoCompleteBoxWithLabel";
				}
				return this.cssClassFormatString;
			}
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x0012293C File Offset: 0x00120B3C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			short tabIndex = this.TabIndex;
			this.TabIndex = 0;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			base.AddAttributesToRender(writer);
			this.TabIndex = tabIndex;
			this.AccessKey = accessKey;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x00122980 File Offset: 0x00120B80
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (base.DesignMode)
			{
				this.RenderDesignTimeHtml(writer);
				return;
			}
			if (!string.IsNullOrEmpty(this.Label))
			{
				this.RenderLabel(writer);
			}
			if (!base.IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "racTokenList racDisabled");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "racTokenList");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_taglist");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "racTagList");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			this.RenderInputArea(writer);
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x00122A2C File Offset: 0x00120C2C
		private void RenderLabel(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1}", "racLabel", this.LabelCssClass).Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID + "_Input");
			if (!this.LabelWidth.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.LabelWidth.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.Write(this.Label);
			writer.RenderEndTag();
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x00122ABC File Offset: 0x00120CBC
		private void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "racSlide");
			writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;");
			writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, this.ZIndex.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.cssPopUpFormatString, base.RuntimeSkin));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "racList");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			if (this.EnableClientFiltering && !this.UsingWebServiceBinding && !this.IsUsingODataBinding)
			{
				using (List<DropDownItem>.Enumerator enumerator = this.DropDownItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DropDownItem dropDownItem = enumerator.Current;
						dropDownItem.RenderContents(writer);
					}
					goto IL_E2;
				}
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "racItem");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
			IL_E2:
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x00122BD0 File Offset: 0x00120DD0
		private void RenderInputArea(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "racInput radPreventDecorate");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Input");
			if (!base.IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (this.TabIndex > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
			}
			if (!string.IsNullOrEmpty(this.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x00122C80 File Offset: 0x00120E80
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write("<style type=\"text/css\">");
			writer.Write("\r\n\t\t\t .RadAutoCompleteBox .racTokenList\r\n\t\t\t{\r\n    \t\t\theight: 20px;\r\n\t\t\t}\r\n\t\t\t.RadAutoCompleteBox .racInput\r\n\t\t\t{\r\n    \t\t\twidth: 150px;\r\n\t\t\t}");
			writer.Write("</style>");
			if (this.InputType == RadAutoCompleteInputType.Token)
			{
				writer.Write("\t<div class='racTokenList'>\r\n\t\t\t\t\t<span class='racToken'>AutoCompleteBoxEntry\r\n\t\t\t\t\t\t<a href='#' title='Remove' class='racDeleteTokenLink'></a>\r\n\t\t\t\t\t</span>\t\t\t\t\t\r\n\t\t\t\t</div>");
				return;
			}
			writer.Write("\t<div class='racTokenList'>\t\t\t\t\t\r\n\t\t\t\t\t<input type='text' class='racInput' value='AutoCompleteBoxEntry' id='dasInput' />\r\n\t\t\t\t</div>");
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x00122CD9 File Offset: 0x00120ED9
		protected internal void RaiseDropDownTemplateNeeded(object item)
		{
			this.OnDropDownTemplateNeeded(new AutoCompleteDropDownItemEventArgs((DropDownItem)item));
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x00122CEC File Offset: 0x00120EEC
		private void RaiseDropDownItemEvent(object eventKey, AutoCompleteDropDownItemEventArgs e)
		{
			AutoCompleteDropDownItemEventHandler autoCompleteDropDownItemEventHandler = (AutoCompleteDropDownItemEventHandler)base.Events[eventKey];
			if (autoCompleteDropDownItemEventHandler != null)
			{
				autoCompleteDropDownItemEventHandler(this, e);
			}
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x00122D18 File Offset: 0x00120F18
		private void RaiseEntryEvent(object eventKey, AutoCompleteEntryEventArgs e)
		{
			AutoCompleteEntryEventHandler autoCompleteEntryEventHandler = (AutoCompleteEntryEventHandler)base.Events[eventKey];
			if (autoCompleteEntryEventHandler != null)
			{
				autoCompleteEntryEventHandler(this, e);
			}
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x00122D44 File Offset: 0x00120F44
		private void RaiseTextEvent(object eventKey, AutoCompleteTextEventArgs e)
		{
			AutoCompleteTextEventHandler autoCompleteTextEventHandler = (AutoCompleteTextEventHandler)base.Events[eventKey];
			if (autoCompleteTextEventHandler != null)
			{
				autoCompleteTextEventHandler(this, e);
			}
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x00122D6E File Offset: 0x00120F6E
		protected override void OnPreRender(EventArgs e)
		{
			base.RequiresDataBinding = (this.EnableClientFiltering && !this.IsUsingODataBinding && !this.UsingWebServiceBinding);
			base.OnPreRender(e);
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x00122D9C File Offset: 0x00120F9C
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (this.IsUsingODataBinding || this.UsingWebServiceBinding)
			{
				return;
			}
			if (data == null && this.DataSource == null)
			{
				throw new Exception("DataSource not set");
			}
			this.DropDownItems = new List<DropDownItem>();
			foreach (object obj in data)
			{
				string textFromDataItem = DropDownItem.GetTextFromDataItem(obj, this.DataTextField, this.DataTextFormatString);
				string valueFromDataItem = DropDownItem.GetValueFromDataItem(obj, this.DataValueField);
				DropDownItem item = this.CreateDropDownItem(obj, textFromDataItem, valueFromDataItem);
				this.DropDownItems.Add(item);
			}
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x00122E54 File Offset: 0x00121054
		private DropDownItem CreateDropDownItem(object dataObject, string text, string value)
		{
			DropDownItem dropDownItem = new DropDownItem(dataObject, text, value);
			this.RaiseDropDownTemplateNeeded(dropDownItem);
			this.ApplyTemplate(dropDownItem);
			dropDownItem.DataItem = null;
			return dropDownItem;
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x00122E80 File Offset: 0x00121080
		string ICallbackEventHandler.GetCallbackResult()
		{
			StringWriter stringWriter = new StringWriter();
			if (this.DropDownItems == null)
			{
				return string.Empty;
			}
			foreach (DropDownItem dropDownItem in this.DropDownItems)
			{
				dropDownItem.RenderContents(new HtmlTextWriter(stringWriter));
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new DropDownItemConverter()
			});
			string text = javaScriptSerializer.Serialize(this.DropDownItems);
			return string.Concat(new object[]
			{
				text,
				"_$$_",
				stringWriter,
				"_$$_",
				this._endOfItems
			});
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x00122F60 File Offset: 0x00121160
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			RadAutoCompleteContext context = javaScriptSerializer.Deserialize<RadAutoCompleteContext>(eventArgument);
			this.LoadDropDownItems(context);
		}

		// Token: 0x06005F83 RID: 24451 RVA: 0x00122FC8 File Offset: 0x001211C8
		protected void LoadDropDownItems(RadAutoCompleteContext context)
		{
			string text = context.Text;
			bool isUsingModelBinders = this.IsUsingModelBinders;
			if (this.DataSource != null || this.DataSourceID != "" || isUsingModelBinders)
			{
				this._showAllResults = context.ShowAllResults;
				AutoCompleteBoxDataSourceSelectEventArgs e = new AutoCompleteBoxDataSourceSelectEventArgs(this.GetDataSource(), text);
				bool requiresFiltering = this.OnDataSourceSelect(e);
				if (this.IsBoundToEntityDataSource())
				{
					this.SetEntityDataSourceFilter(text);
					requiresFiltering = false;
				}
				else if (this.IsBoundToLinqDataSource())
				{
					this.SetLinqDataSourceFilter(text);
					requiresFiltering = false;
				}
				this.GetData().Select(this.CreateDataSourceSelectArguments(), delegate(IEnumerable data)
				{
					this.DropDownItems = this.GetFilteredDropDownItems(data, context, requiresFiltering);
				});
				return;
			}
			throw new Exception("DataSource not set");
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x001230B2 File Offset: 0x001212B2
		private void SetEndOfItems(int totalItemsCount)
		{
			if (totalItemsCount <= this.MaxResultCount)
			{
				this._endOfItems = true;
			}
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x001230C4 File Offset: 0x001212C4
		private List<DropDownItem> GetFilteredDropDownItems(IEnumerable data, RadAutoCompleteContext context, bool requiresFiltering)
		{
			List<DropDownItem> list = new List<DropDownItem>();
			string text = context.Text;
			if (!string.IsNullOrEmpty(text))
			{
				string value = this.IsCaseSensitive ? text : text.ToLowerInvariant();
				if (this.IsBoundToLinqDataSource() || this.IsBoundToEntityDataSource())
				{
					this.SetEndOfItems(this.SelectArguments.TotalRowCount);
				}
				else if (this.MaxResultCount > 0)
				{
					this._endOfItems = true;
				}
				foreach (object obj in data)
				{
					string textFromDataItem = DropDownItem.GetTextFromDataItem(obj, this.DataTextField, this.DataTextFormatString);
					string text2 = this.IsCaseSensitive ? textFromDataItem : textFromDataItem.ToLowerInvariant();
					if (!requiresFiltering || (this.Filter == RadAutoCompleteFilter.StartsWith && text2.StartsWith(value)) || (this.Filter == RadAutoCompleteFilter.Contains && text2.Contains(value)))
					{
						if (!context.ShowAllResults && this.MaxResultCount > 0 && list.Count == this.MaxResultCount)
						{
							this._endOfItems = false;
							break;
						}
						string valueFromDataItem = DropDownItem.GetValueFromDataItem(obj, this.DataValueField);
						DropDownItem item = this.CreateDropDownItem(obj, textFromDataItem, valueFromDataItem);
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x00123214 File Offset: 0x00121414
		private void SetEntityDataSourceFilter(string filterString)
		{
			string text = string.Format("it.[{0}] LIKE '{{0}}{1}%'", this.DataTextField, filterString.Replace("'", "''"));
			text = string.Format(text, (this.Filter == RadAutoCompleteFilter.Contains) ? "%" : string.Empty);
			object component = base.IsBoundUsingDataSourceID ? this.GetDataSource() : this.DataSource;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Where"];
			object value = propertyDescriptor.GetValue(component);
			string text2 = (value != null) ? value.ToString() : string.Empty;
			if (string.IsNullOrEmpty(text2))
			{
				propertyDescriptor.SetValue(component, text);
				return;
			}
			string arg = string.IsNullOrEmpty(text) ? string.Empty : string.Format(" AND {0}", text);
			propertyDescriptor.SetValue(component, string.Format("{0}{1}", text2, arg));
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x001232E4 File Offset: 0x001214E4
		private void SetLinqDataSourceFilter(string filterString)
		{
			LinqDataSourceView linqDataSourceView = this.GetData() as LinqDataSourceView;
			string text = string.Format("{0}.{{0}}(\"{1}\")", this.DataTextField, filterString.Replace("\"", "\"\""));
			text = string.Format(text, (this.Filter == RadAutoCompleteFilter.Contains) ? "Contains" : "StartsWith");
			if (linqDataSourceView.Where.Length > 0)
			{
				string format = "{0} && {1}";
				string where = linqDataSourceView.Where;
				linqDataSourceView.Where = string.Format(format, where, text);
				return;
			}
			linqDataSourceView.Where = text;
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x0012336C File Offset: 0x0012156C
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			this.SelectArguments = new DataSourceSelectArguments();
			if ((this.IsBoundToLinqDataSource() || this.IsBoundToEntityDataSource()) && !this._showAllResults && this.MaxResultCount > 0)
			{
				this.SelectArguments.MaximumRows = this.MaxResultCount;
				this.SelectArguments.RetrieveTotalRowCount = true;
			}
			return this.SelectArguments;
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x001233C8 File Offset: 0x001215C8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Children.Clear();
				return;
			}
			((IStateManager)this.Children).LoadViewState(array[1]);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x00123404 File Offset: 0x00121604
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Children).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x0012343E File Offset: 0x0012163E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Children).TrackViewState();
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x00123454 File Offset: 0x00121654
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			return new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x00123474 File Offset: 0x00121674
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x001234B0 File Offset: 0x001216B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new AutoCompleteBoxEntryConverter(),
				new DropDownItemConverter(),
				new AttributeCollectionConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			base.DescribeRenderingMode(descriptor);
			if (this.Entries.Count > 0)
			{
				string script = javaScriptSerializer.Serialize(this.Entries);
				descriptor.AddScriptProperty("entryData", script);
			}
			if (this.EnableClientFiltering && this.DropDownItems.Count > 0)
			{
				string script2 = javaScriptSerializer.Serialize(this.DropDownItems);
				descriptor.AddScriptProperty("dropDownItemData", script2);
			}
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (!string.IsNullOrEmpty(this.ClientDropDownItemTemplate))
			{
				descriptor.AddProperty("_clientDropDownTemplate", this.ClientDropDownItemTemplate);
			}
			if (base.Events[RadAutoCompleteBox.EntryAddedEvent] != null && this.InputType == RadAutoCompleteInputType.Token)
			{
				descriptor.AddProperty("_postBackOnAdd", true);
			}
			if (base.Events[RadAutoCompleteBox.EntryRemovedEvent] != null && this.InputType == RadAutoCompleteInputType.Token)
			{
				descriptor.AddProperty("_postBackOnRemove", true);
			}
			if (base.Events[RadAutoCompleteBox.TextChangedEvent] != null && this.InputType == RadAutoCompleteInputType.Text)
			{
				descriptor.AddProperty("_postBackOnChange", true);
			}
			if (!this.HighlightFirstMatch)
			{
				descriptor.AddProperty("_highlightFirstMatch", this.HighlightFirstMatch);
			}
			base.DescribeProperty<bool>(descriptor, "_shouldFocus", this._shouldFocus, false);
			descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			descriptor.AddProperty("_removeTokenTitle", this.Localization.RemoveTokenTitle);
			descriptor.AddProperty("_showAllResultsText", this.Localization.ShowAllResults);
			if (this.UsingWebServiceBinding)
			{
				this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			}
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.DescribeClientDataSource(descriptor);
			this.DescribeSettings(javaScriptSerializer, descriptor);
			this.AriaSettings.Describe(descriptor);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x00123718 File Offset: 0x00121918
		private void DescribeClientDataSource(IScriptDescriptor descriptor)
		{
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				if (!string.IsNullOrEmpty(this.DataTextField))
				{
					descriptor.AddProperty("_dataTextField", this.DataTextField);
				}
				if (!string.IsNullOrEmpty(this.DataValueField))
				{
					descriptor.AddProperty("_dataValueField", this.DataValueField);
				}
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x001237B4 File Offset: 0x001219B4
		private void DescribeSettings(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			this.TokensSettings.Describe("tokensSettings", serializer, descriptor);
			this.TextSettings.Describe("textSettings", serializer, descriptor);
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x001237DC File Offset: 0x001219DC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(postCollection[this.ID + "_ClientState"]))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				this.clientState = javaScriptSerializer.Deserialize<AutoCompleteBoxClientState>(text);
				if (base.IsEnabled)
				{
					this.Enabled = this.clientState.Enabled;
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			this.LoadClientState(this.clientState);
			return false;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x00123880 File Offset: 0x00121A80
		internal void LoadClientState(AutoCompleteBoxClientState state)
		{
			if (state.LogEntries != null)
			{
				this.LoadLogEntries(state.LogEntries);
			}
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x00123898 File Offset: 0x00121A98
		private void LoadLogEntries(ClientStateLogEntry[] entries)
		{
			this.Entries.Clear();
			foreach (ClientStateLogEntry clientStateLogEntry in entries)
			{
				AutoCompleteBoxEntry autoCompleteBoxEntry = this.CreateEntry();
				int index = Convert.ToInt32(clientStateLogEntry.Index);
				autoCompleteBoxEntry.LoadFromDictionary(clientStateLogEntry.Data);
				if (clientStateLogEntry.Type == ClientStateLogEntryType.Insert)
				{
					this.Entries.Insert(index, autoCompleteBoxEntry);
				}
			}
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x001238FE File Offset: 0x00121AFE
		private AutoCompleteBoxEntry CreateEntry()
		{
			return new AutoCompleteBoxEntry();
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x00123908 File Offset: 0x00121B08
		private void ApplyTemplate(DropDownItem item)
		{
			if (this.DropDownItemTemplate == null && item.Template == null)
			{
				return;
			}
			int num = item.Controls.Count;
			if (item.Template != null)
			{
				item.Template.InstantiateIn(item);
			}
			else
			{
				this.DropDownItemTemplate.InstantiateIn(item);
			}
			while (num > 0 && !item.Controls.IsReadOnly)
			{
				item.Controls.Add(item.Controls[0]);
				num--;
			}
			item.Templated = true;
			item.DataBind();
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x00123990 File Offset: 0x00121B90
		public void RaisePostBackEvent(string eventArgument)
		{
			AutoCompletePostBackArguments autoCompletePostBackArguments = null;
			try
			{
				autoCompletePostBackArguments = new JavaScriptSerializer().Deserialize<AutoCompletePostBackArguments>(HttpUtility.UrlDecode(eventArgument).Replace("&squote", "'"));
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (autoCompletePostBackArguments == null)
			{
				return;
			}
			string command;
			if ((command = autoCompletePostBackArguments.Command) != null)
			{
				if (command == "Add")
				{
					this._shouldFocus = true;
					this.OnEntryAdd(new AutoCompleteBoxEntry(autoCompletePostBackArguments.Text, autoCompletePostBackArguments.Value, autoCompletePostBackArguments.Attributes));
					return;
				}
				if (command == "Remove")
				{
					this._shouldFocus = true;
					this.OnEntryRemove(this.Entries[int.Parse(autoCompletePostBackArguments.Index)]);
					return;
				}
				if (!(command == "Change"))
				{
					return;
				}
				this.OnTextChange(autoCompletePostBackArguments.Text);
			}
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x00123A70 File Offset: 0x00121C70
		private void OnEntryAdd(AutoCompleteBoxEntry entry)
		{
			this.Entries.Add(entry);
			this.OnEntryAdded(new AutoCompleteEntryEventArgs(entry));
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x00123A8A File Offset: 0x00121C8A
		private void OnEntryRemove(AutoCompleteBoxEntry entry)
		{
			this.Entries.Remove(entry);
			this.OnEntryRemoved(new AutoCompleteEntryEventArgs(entry));
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x00123AA5 File Offset: 0x00121CA5
		private void OnTextChange(string text)
		{
			this.OnTextChanged(new AutoCompleteTextEventArgs(text));
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x00123AB4 File Offset: 0x00121CB4
		internal void PopulateFromString(string text)
		{
			this.Entries.Clear();
			char[] separator = new char[]
			{
				this.Delimiter[0]
			};
			string[] array = text.Split(separator);
			foreach (string text2 in array)
			{
				string text3 = text2.Trim();
				if (!string.IsNullOrEmpty(text3))
				{
					this.Entries.Add(new AutoCompleteBoxEntry(text3));
				}
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06005F9B RID: 24475 RVA: 0x00123B2B File Offset: 0x00121D2B
		// (set) Token: 0x06005F9C RID: 24476 RVA: 0x00123B4C File Offset: 0x00121D4C
		[Category("Behavior")]
		[DefaultValue(false)]
		[Bindable(false)]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x06005F9D RID: 24477 RVA: 0x00123B64 File Offset: 0x00121D64
		// (set) Token: 0x06005F9E RID: 24478 RVA: 0x00123B84 File Offset: 0x00121D84
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataTextFormatString
		{
			get
			{
				return (string)(this.ViewState["DataTextFormatString"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x00123B97 File Offset: 0x00121D97
		// (set) Token: 0x06005FA0 RID: 24480 RVA: 0x00123BB8 File Offset: 0x00121DB8
		[Category("Behavior")]
		[Description("Defines the maximum number of results shown in the autocomplete dropdown.")]
		[ClientControlProperty]
		[ClientPropertyName("maxResultCount")]
		[DefaultValue(-1)]
		public virtual int MaxResultCount
		{
			get
			{
				return (int)(this.ViewState["MaxResultCount"] ?? -1);
			}
			set
			{
				this.ViewState["MaxResultCount"] = value;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x06005FA1 RID: 24481 RVA: 0x00123BD0 File Offset: 0x00121DD0
		// (set) Token: 0x06005FA2 RID: 24482 RVA: 0x00123BF1 File Offset: 0x00121DF1
		[DefaultValue(1)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("minFilterLength")]
		[Description("Defines the minimum number of characters that must be typed before the autocomplete is triggered.")]
		public virtual int MinFilterLength
		{
			get
			{
				return (int)(this.ViewState["MinFilterLength"] ?? 1);
			}
			set
			{
				this.ViewState["MinFilterLength"] = value;
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06005FA3 RID: 24483 RVA: 0x00123C09 File Offset: 0x00121E09
		// (set) Token: 0x06005FA4 RID: 24484 RVA: 0x00123C2A File Offset: 0x00121E2A
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether first matching result will be highlighted upon drop down opening.")]
		public virtual bool HighlightFirstMatch
		{
			get
			{
				return (bool)(this.ViewState["HighlightFirstMatch"] ?? true);
			}
			set
			{
				this.ViewState["HighlightFirstMatch"] = value;
			}
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x00123C42 File Offset: 0x00121E42
		// (set) Token: 0x06005FA6 RID: 24486 RVA: 0x00123C63 File Offset: 0x00121E63
		[Bindable(false)]
		[DefaultValue(RadAutoCompleteFilter.Contains)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("filter")]
		public RadAutoCompleteFilter Filter
		{
			get
			{
				return (RadAutoCompleteFilter)(this.ViewState["Filter"] ?? RadAutoCompleteFilter.Contains);
			}
			set
			{
				this.ViewState["Filter"] = value;
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x00123C7B File Offset: 0x00121E7B
		// (set) Token: 0x06005FA8 RID: 24488 RVA: 0x00123C9C File Offset: 0x00121E9C
		[DefaultValue(false)]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("isCaseSensitive")]
		public bool IsCaseSensitive
		{
			get
			{
				return (bool)(this.ViewState["IsCaseSensitive"] ?? false);
			}
			set
			{
				this.ViewState["IsCaseSensitive"] = value;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x06005FA9 RID: 24489 RVA: 0x00123CB4 File Offset: 0x00121EB4
		// (set) Token: 0x06005FAA RID: 24490 RVA: 0x00123CD5 File Offset: 0x00121ED5
		[DefaultValue(RadAutoCompleteInputType.Token)]
		[ClientControlProperty]
		[ClientPropertyName("inputType")]
		[Bindable(false)]
		[Category("Behavior")]
		public RadAutoCompleteInputType InputType
		{
			get
			{
				return (RadAutoCompleteInputType)(this.ViewState["InputType"] ?? RadAutoCompleteInputType.Token);
			}
			set
			{
				this.ViewState["InputType"] = value;
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x06005FAB RID: 24491 RVA: 0x00123CED File Offset: 0x00121EED
		// (set) Token: 0x06005FAC RID: 24492 RVA: 0x00123D0E File Offset: 0x00121F0E
		[Bindable(false)]
		[DefaultValue(RadAutoCompleteDropDownPosition.Automatic)]
		[ClientControlProperty]
		[ClientPropertyName("dropDownPosition")]
		[Category("Behavior")]
		public RadAutoCompleteDropDownPosition DropDownPosition
		{
			get
			{
				return (RadAutoCompleteDropDownPosition)(this.ViewState["DropDownPosition"] ?? RadAutoCompleteDropDownPosition.Automatic);
			}
			set
			{
				this.ViewState["DropDownPosition"] = value;
			}
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x06005FAD RID: 24493 RVA: 0x00123D26 File Offset: 0x00121F26
		[Description("Gets the text of the input field")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.Entries.ToString();
			}
		}

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x00123D33 File Offset: 0x00121F33
		// (set) Token: 0x06005FAF RID: 24495 RVA: 0x00123D53 File Offset: 0x00121F53
		[Category("Appearance")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[DefaultValue("Default")]
		[NotifyParentProperty(true)]
		[Description("Specifies the skin that will be used by the control")]
		public override string Skin
		{
			get
			{
				return (string)(this.ViewState["Skin"] ?? "Default");
			}
			set
			{
				this.ViewState["Skin"] = value;
			}
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x00123D66 File Offset: 0x00121F66
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public AutoCompleteBoxEntryCollection Entries
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x06005FB1 RID: 24497 RVA: 0x00123D6E File Offset: 0x00121F6E
		// (set) Token: 0x06005FB2 RID: 24498 RVA: 0x00123D8E File Offset: 0x00121F8E
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				return (string)(this.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x06005FB3 RID: 24499 RVA: 0x00123DA1 File Offset: 0x00121FA1
		// (set) Token: 0x06005FB4 RID: 24500 RVA: 0x00123DC1 File Offset: 0x00121FC1
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataValueField
		{
			get
			{
				return (string)(this.ViewState["DataValueField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x06005FB5 RID: 24501 RVA: 0x00123DD4 File Offset: 0x00121FD4
		// (set) Token: 0x06005FB6 RID: 24502 RVA: 0x00123DF5 File Offset: 0x00121FF5
		[ClientPropertyName("enableClientFiltering")]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		public bool EnableClientFiltering
		{
			get
			{
				return (bool)(this.ViewState["EnableClientFiltering"] ?? false);
			}
			set
			{
				this.ViewState["EnableClientFiltering"] = value;
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06005FB7 RID: 24503 RVA: 0x00123E0D File Offset: 0x0012200D
		// (set) Token: 0x06005FB8 RID: 24504 RVA: 0x00123E2E File Offset: 0x0012202E
		[ClientControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("allowCustomEntry")]
		[Bindable(false)]
		[Category("Behavior")]
		public bool AllowCustomEntry
		{
			get
			{
				return (bool)(this.ViewState["AllowCustomEntry"] ?? false);
			}
			set
			{
				this.ViewState["AllowCustomEntry"] = value;
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x06005FB9 RID: 24505 RVA: 0x00123E46 File Offset: 0x00122046
		// (set) Token: 0x06005FBA RID: 24506 RVA: 0x00123E67 File Offset: 0x00122067
		[DefaultValue(true)]
		[ClientControlProperty]
		[Category("Appearance")]
		[ClientPropertyName("showLoadingIcon")]
		[Bindable(false)]
		public bool ShowLoadingIcon
		{
			get
			{
				return (bool)(this.ViewState["ShowLoadingIcon"] ?? true);
			}
			set
			{
				this.ViewState["ShowLoadingIcon"] = value;
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x06005FBB RID: 24507 RVA: 0x00123E7F File Offset: 0x0012207F
		// (set) Token: 0x06005FBC RID: 24508 RVA: 0x00123E9F File Offset: 0x0012209F
		[ClientPropertyName("delimiter")]
		[DefaultValue(";")]
		[ClientControlProperty]
		[Bindable(false)]
		[Category("Behavior")]
		public string Delimiter
		{
			get
			{
				return (string)(this.ViewState["Delimiter"] ?? ";");
			}
			set
			{
				this.ViewState["Delimiter"] = value;
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x06005FBD RID: 24509 RVA: 0x00123EB2 File Offset: 0x001220B2
		[ClientPropertyName("_uniqueId")]
		[ClientControlProperty]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x00123EBA File Offset: 0x001220BA
		// (set) Token: 0x06005FBF RID: 24511 RVA: 0x00123EC2 File Offset: 0x001220C2
		[Browsable(false)]
		[Bindable(false)]
		[TemplateContainer(typeof(DropDownItem))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate DropDownItemTemplate
		{
			get
			{
				return this._dropDownItemTemplate;
			}
			set
			{
				this._dropDownItemTemplate = value;
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x00123ECB File Offset: 0x001220CB
		// (set) Token: 0x06005FC1 RID: 24513 RVA: 0x00123EEB File Offset: 0x001220EB
		[Browsable(false)]
		[DefaultValue("")]
		[Category("Client")]
		[Description("Gets or sets the template for the items that appear in the dropdown")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string ClientDropDownItemTemplate
		{
			get
			{
				return (this.ViewState["ClientDropDownItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientDropDownItemTemplate"] = value;
			}
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x00123EFE File Offset: 0x001220FE
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Description("Gets the settings(service path and method name)for the web service used to populate items.")]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x00123F06 File Offset: 0x00122106
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The animation played when the dropdown is opened")]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x00123F0E File Offset: 0x0012210E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Description("The animation played when the dropdown is closed")]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x06005FC5 RID: 24517 RVA: 0x00123F16 File Offset: 0x00122116
		// (set) Token: 0x06005FC6 RID: 24518 RVA: 0x00123F37 File Offset: 0x00122137
		[Bindable(false)]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool EnableScreenBoundaryDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableScreenBoundaryDetection"] ?? true);
			}
			set
			{
				this.ViewState["EnableScreenBoundaryDetection"] = value;
			}
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x06005FC7 RID: 24519 RVA: 0x00123F4F File Offset: 0x0012214F
		// (set) Token: 0x06005FC8 RID: 24520 RVA: 0x00123F70 File Offset: 0x00122170
		[Category("Behavior")]
		[ClientPropertyName("_enableDirectionDetection")]
		[Bindable(false)]
		[DefaultValue(false)]
		[ClientControlProperty]
		public bool EnableDirectionDetection
		{
			get
			{
				return (bool)(this.ViewState["EnableDirectionDetection"] ?? false);
			}
			set
			{
				this.ViewState["EnableDirectionDetection"] = value;
			}
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x00123F88 File Offset: 0x00122188
		// (set) Token: 0x06005FCA RID: 24522 RVA: 0x00123FAD File Offset: 0x001221AD
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[ClientControlProperty]
		public Unit DropDownHeight
		{
			get
			{
				return (Unit)(this.ViewState["DropDownHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownHeight"] = value;
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x00123FE2 File Offset: 0x001221E2
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x00123FEA File Offset: 0x001221EA
		// (set) Token: 0x06005FCD RID: 24525 RVA: 0x0012400F File Offset: 0x0012220F
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit DropDownWidth
		{
			get
			{
				return (Unit)(this.ViewState["DropDownWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownWidth"] = value;
			}
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x06005FCE RID: 24526 RVA: 0x00124044 File Offset: 0x00122244
		// (set) Token: 0x06005FCF RID: 24527 RVA: 0x00124064 File Offset: 0x00122264
		[Bindable(false)]
		[ClientControlProperty]
		[ClientPropertyName("emptyMessage")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string EmptyMessage
		{
			get
			{
				return (string)(this.ViewState["EmptyMessage"] ?? string.Empty);
			}
			set
			{
				this.ViewState["EmptyMessage"] = value;
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x06005FD0 RID: 24528 RVA: 0x00124077 File Offset: 0x00122277
		// (set) Token: 0x06005FD1 RID: 24529 RVA: 0x0012409C File Offset: 0x0012229C
		[DefaultValue(7000)]
		[Bindable(false)]
		[Category("Behavior")]
		public int ZIndex
		{
			get
			{
				return (int)(this.ViewState["ZIndex"] ?? 7000);
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x001240B4 File Offset: 0x001222B4
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Description("Tokens settings")]
		public AutoCompleteBoxTokensSettings TokensSettings
		{
			get
			{
				return this._tokensSettings;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x001240BC File Offset: 0x001222BC
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Description("Text settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public AutoCompleteBoxTextSettings TextSettings
		{
			get
			{
				return this._textSettings;
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x06005FD4 RID: 24532 RVA: 0x001240C4 File Offset: 0x001222C4
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AutoCompleteBoxLocalization Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new AutoCompleteBoxLocalization(new LocalizationProvider("RadAutoCompleteBox", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x00124103 File Offset: 0x00122303
		// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x00124124 File Offset: 0x00122324
		[Description("Gets or sets a value indicating where RadAutoCompleteBox will look for its .resx localization files.")]
		[DefaultValue("")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x00124177 File Offset: 0x00122377
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x00124197 File Offset: 0x00122397
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Misc")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x001241AA File Offset: 0x001223AA
		// (set) Token: 0x06005FDA RID: 24538 RVA: 0x001241CA File Offset: 0x001223CA
		[Description("The label of the control.")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string Label
		{
			get
			{
				return ((string)this.ViewState["Label"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Label"] = value;
			}
		}

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x001241DD File Offset: 0x001223DD
		// (set) Token: 0x06005FDC RID: 24540 RVA: 0x001241FD File Offset: 0x001223FD
		[Category("Appearance")]
		[Description("Css class of the label")]
		[DefaultValue("")]
		public string LabelCssClass
		{
			get
			{
				return ((string)this.ViewState["LabelCssClass"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["LabelCssClass"] = value;
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x00124210 File Offset: 0x00122410
		// (set) Token: 0x06005FDE RID: 24542 RVA: 0x00124235 File Offset: 0x00122435
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public Unit LabelWidth
		{
			get
			{
				return (Unit)(this.ViewState["LabelWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["LabelWidth"] = value;
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x0012426A File Offset: 0x0012246A
		// (set) Token: 0x06005FE0 RID: 24544 RVA: 0x0012428B File Offset: 0x0012248B
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("When set to true enables support for WAI-ARIA.")]
		[ClientPropertyName("enableAriaSupport")]
		[Category("Behavior")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x001242A4 File Offset: 0x001224A4
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[DefaultValue(null)]
		public WaiAriaSettings AriaSettings
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x001242C9 File Offset: 0x001224C9
		// (set) Token: 0x06005FE3 RID: 24547 RVA: 0x001242EE File Offset: 0x001224EE
		[ClientPropertyName("_requestDelay")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		[DefaultValue(250)]
		public int RequestDelay
		{
			get
			{
				return (int)(this.ViewState["RequestDelay"] ?? 250);
			}
			set
			{
				this.ViewState["RequestDelay"] = value;
			}
		}

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06005FE4 RID: 24548 RVA: 0x00124306 File Offset: 0x00122506
		// (remove) Token: 0x06005FE5 RID: 24549 RVA: 0x00124319 File Offset: 0x00122519
		public event AutoCompleteDropDownItemEventHandler DropDownTemplateNeeded
		{
			add
			{
				base.Events.AddHandler(RadAutoCompleteBox.DropDownTemplateNeededEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadAutoCompleteBox.DropDownTemplateNeededEvent, value);
			}
		}

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06005FE6 RID: 24550 RVA: 0x0012432C File Offset: 0x0012252C
		// (remove) Token: 0x06005FE7 RID: 24551 RVA: 0x0012433F File Offset: 0x0012253F
		public event AutoCompleteEntryEventHandler EntryAdded
		{
			add
			{
				base.Events.AddHandler(RadAutoCompleteBox.EntryAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadAutoCompleteBox.EntryAddedEvent, value);
			}
		}

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06005FE8 RID: 24552 RVA: 0x00124352 File Offset: 0x00122552
		// (remove) Token: 0x06005FE9 RID: 24553 RVA: 0x00124365 File Offset: 0x00122565
		public event AutoCompleteEntryEventHandler EntryRemoved
		{
			add
			{
				base.Events.AddHandler(RadAutoCompleteBox.EntryRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadAutoCompleteBox.EntryRemovedEvent, value);
			}
		}

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06005FEA RID: 24554 RVA: 0x00124378 File Offset: 0x00122578
		// (remove) Token: 0x06005FEB RID: 24555 RVA: 0x0012438B File Offset: 0x0012258B
		public event AutoCompleteTextEventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(RadAutoCompleteBox.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadAutoCompleteBox.TextChangedEvent, value);
			}
		}

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06005FEC RID: 24556 RVA: 0x0012439E File Offset: 0x0012259E
		// (remove) Token: 0x06005FED RID: 24557 RVA: 0x001243B1 File Offset: 0x001225B1
		public event AutoCompleteBoxDataSourceSelectEventHandler DataSourceSelect
		{
			add
			{
				base.Events.AddHandler(RadAutoCompleteBox.DataSourceSelectEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadAutoCompleteBox.DataSourceSelectEvent, value);
			}
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x001243C4 File Offset: 0x001225C4
		protected void OnDropDownTemplateNeeded(AutoCompleteDropDownItemEventArgs e)
		{
			this.RaiseDropDownItemEvent(RadAutoCompleteBox.DropDownTemplateNeededEvent, e);
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x001243D2 File Offset: 0x001225D2
		protected virtual void OnEntryAdded(AutoCompleteEntryEventArgs e)
		{
			this.RaiseEntryEvent(RadAutoCompleteBox.EntryAddedEvent, e);
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x001243E0 File Offset: 0x001225E0
		protected virtual void OnEntryRemoved(AutoCompleteEntryEventArgs e)
		{
			this.RaiseEntryEvent(RadAutoCompleteBox.EntryRemovedEvent, e);
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x001243EE File Offset: 0x001225EE
		protected virtual void OnTextChanged(AutoCompleteTextEventArgs e)
		{
			this.RaiseTextEvent(RadAutoCompleteBox.TextChangedEvent, e);
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x001243FC File Offset: 0x001225FC
		protected bool OnDataSourceSelect(AutoCompleteBoxDataSourceSelectEventArgs e)
		{
			AutoCompleteBoxDataSourceSelectEventHandler autoCompleteBoxDataSourceSelectEventHandler = (AutoCompleteBoxDataSourceSelectEventHandler)base.Events[RadAutoCompleteBox.DataSourceSelectEvent];
			if (autoCompleteBoxDataSourceSelectEventHandler != null)
			{
				autoCompleteBoxDataSourceSelectEventHandler(this, e);
				return false;
			}
			return true;
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x0012442D File Offset: 0x0012262D
		// (set) Token: 0x06005FF4 RID: 24564 RVA: 0x0012444D File Offset: 0x0012264D
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Description("The JavaScript function executed when RadListBox is initialized")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x00124460 File Offset: 0x00122660
		// (set) Token: 0x06005FF6 RID: 24566 RVA: 0x00124480 File Offset: 0x00122680
		[Description("Gets or sets the name of the JavaScript function called when the dropdown is about to be opened")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[ClientPropertyName("dropDownOpening")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDropDownOpening
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownOpening"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownOpening"] = value;
			}
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x00124493 File Offset: 0x00122693
		// (set) Token: 0x06005FF8 RID: 24568 RVA: 0x001244B3 File Offset: 0x001226B3
		[ClientPropertyName("dropDownOpened")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function called when the dropdown is opened")]
		public string OnClientDropDownOpened
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownOpened"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownOpened"] = value;
			}
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x001244C6 File Offset: 0x001226C6
		// (set) Token: 0x06005FFA RID: 24570 RVA: 0x001244E6 File Offset: 0x001226E6
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("dropDownClosing")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the JavaScript function called when the dropdown is about to be closed")]
		[DefaultValue("")]
		public string OnClientDropDownClosing
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownClosing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownClosing"] = value;
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06005FFB RID: 24571 RVA: 0x001244F9 File Offset: 0x001226F9
		// (set) Token: 0x06005FFC RID: 24572 RVA: 0x00124519 File Offset: 0x00122719
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function called when the dropdown is about to be closed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("dropDownClosed")]
		[DefaultValue("")]
		public string OnClientDropDownClosed
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownClosed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownClosed"] = value;
			}
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x0012452C File Offset: 0x0012272C
		// (set) Token: 0x06005FFE RID: 24574 RVA: 0x0012454C File Offset: 0x0012274C
		[ClientPropertyName("dropDownItemDataBound")]
		[Description("Gets or sets the name of the JavaScript function called when an DropDownItem is created during Web Service Load on Demand")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientDropDownItemDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientDropDownItemDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDropDownItemDataBound"] = value;
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x0012455F File Offset: 0x0012275F
		// (set) Token: 0x06006000 RID: 24576 RVA: 0x0012457F File Offset: 0x0012277F
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("entryAdding")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function called when an entry is about to be added")]
		public string OnClientEntryAdding
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryAdding"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryAdding"] = value;
			}
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x00124592 File Offset: 0x00122792
		// (set) Token: 0x06006002 RID: 24578 RVA: 0x001245B2 File Offset: 0x001227B2
		[ClientPropertyName("entryAdded")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function called when an entry was added")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientEntryAdded
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryAdded"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryAdded"] = value;
			}
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x06006003 RID: 24579 RVA: 0x001245C5 File Offset: 0x001227C5
		// (set) Token: 0x06006004 RID: 24580 RVA: 0x001245E5 File Offset: 0x001227E5
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("entryRemoving")]
		[Description("Gets or sets the name of the JavaScript function called when an entry is about to be removed")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientEntryRemoving
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryRemoving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryRemoving"] = value;
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x06006005 RID: 24581 RVA: 0x001245F8 File Offset: 0x001227F8
		// (set) Token: 0x06006006 RID: 24582 RVA: 0x00124618 File Offset: 0x00122818
		[ClientControlEvent]
		[ClientPropertyName("entryRemoved")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function called when an entry was removed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientEntryRemoved
		{
			get
			{
				return (string)(this.ViewState["OnClientEntryRemoved"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEntryRemoved"] = value;
			}
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x0012462B File Offset: 0x0012282B
		// (set) Token: 0x06006008 RID: 24584 RVA: 0x0012464B File Offset: 0x0012284B
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the name of the JavaScript function called when text is changed")]
		[DefaultValue("")]
		[ClientPropertyName("textChanged")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientTextChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientTextChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTextChanged"] = value;
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x0012465E File Offset: 0x0012285E
		// (set) Token: 0x0600600A RID: 24586 RVA: 0x0012467E File Offset: 0x0012287E
		[DefaultValue("")]
		[ClientPropertyName("requesting")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Description("The client-side event that is fired before the items are requested server-side.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientRequesting
		{
			get
			{
				return (string)(this.ViewState["OnClientRequesting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequesting"] = value;
			}
		}

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x00124691 File Offset: 0x00122891
		// (set) Token: 0x0600600C RID: 24588 RVA: 0x001246B1 File Offset: 0x001228B1
		[ClientPropertyName("requested")]
		[Description("The name of the javascript function called after the request for items has completed.")]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientRequested
		{
			get
			{
				return (string)(this.ViewState["OnClientRequested"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequested"] = value;
			}
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x001246C4 File Offset: 0x001228C4
		// (set) Token: 0x0600600E RID: 24590 RVA: 0x001246E4 File Offset: 0x001228E4
		[Bindable(false)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The name of the javascript function called after the request for items has failed.")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("requestFailed")]
		public string OnClientRequestFailed
		{
			get
			{
				return (string)(this.ViewState["OnClientRequestFailed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequestFailed"] = value;
			}
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x001246F7 File Offset: 0x001228F7
		public override void Focus()
		{
			this._shouldFocus = true;
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x00124700 File Offset: 0x00122900
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowCustomEntry", this.AllowCustomEntry, false);
			base.DescribeProperty<string>(descriptor, "delimiter", this.Delimiter, ";");
			base.DescribeProperty<string>(descriptor, "dropDownHeight", this.DropDownHeight.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<RadAutoCompleteDropDownPosition>(descriptor, "dropDownPosition", this.DropDownPosition, RadAutoCompleteDropDownPosition.Automatic);
			base.DescribeProperty<string>(descriptor, "dropDownWidth", this.DropDownWidth.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "emptyMessage", this.EmptyMessage, "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableClientFiltering", this.EnableClientFiltering, false);
			base.DescribeProperty<bool>(descriptor, "_enableDirectionDetection", this.EnableDirectionDetection, false);
			base.DescribeProperty<bool>(descriptor, "_enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<RadAutoCompleteFilter>(descriptor, "filter", this.Filter, RadAutoCompleteFilter.Contains);
			base.DescribeProperty<RadAutoCompleteInputType>(descriptor, "inputType", this.InputType, RadAutoCompleteInputType.Token);
			base.DescribeProperty<bool>(descriptor, "isCaseSensitive", this.IsCaseSensitive, false);
			base.DescribeProperty<int>(descriptor, "maxResultCount", this.MaxResultCount, -1);
			base.DescribeProperty<int>(descriptor, "minFilterLength", this.MinFilterLength, 1);
			base.DescribeProperty<int>(descriptor, "_requestDelay", this.RequestDelay, 250);
			base.DescribeProperty<bool>(descriptor, "showLoadingIcon", this.ShowLoadingIcon, true);
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x00124898 File Offset: 0x00122A98
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosed", this.OnClientDropDownClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownClosing", this.OnClientDropDownClosing);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownItemDataBound", this.OnClientDropDownItemDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpened", this.OnClientDropDownOpened);
			RadDataBoundControl.DescribeEvent(descriptor, "dropDownOpening", this.OnClientDropDownOpening);
			RadDataBoundControl.DescribeEvent(descriptor, "entryAdded", this.OnClientEntryAdded);
			RadDataBoundControl.DescribeEvent(descriptor, "entryAdding", this.OnClientEntryAdding);
			RadDataBoundControl.DescribeEvent(descriptor, "entryRemoved", this.OnClientEntryRemoved);
			RadDataBoundControl.DescribeEvent(descriptor, "entryRemoving", this.OnClientEntryRemoving);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "requested", this.OnClientRequested);
			RadDataBoundControl.DescribeEvent(descriptor, "requestFailed", this.OnClientRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "requesting", this.OnClientRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "textChanged", this.OnClientTextChanged);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x0012499A File Offset: 0x00122B9A
		// Note: this type is marked as 'beforefieldinit'.
		static RadAutoCompleteBox()
		{
			RadAutoCompleteBox.DropDownTemplateNeededEvent = new object();
			RadAutoCompleteBox.EntryAddedEvent = new object();
			RadAutoCompleteBox.EntryRemovedEvent = new object();
			RadAutoCompleteBox.TextChangedEvent = new object();
			RadAutoCompleteBox.DataSourceSelectEvent = new object();
		}

		// Token: 0x040016FA RID: 5882
		internal const string racTokenList = "racTokenList";

		// Token: 0x040016FB RID: 5883
		internal const string racTokenListDisabled = "racTokenList racDisabled";

		// Token: 0x040016FC RID: 5884
		internal const string racToken = "racToken";

		// Token: 0x040016FD RID: 5885
		internal const string racLabel = "racLabel";

		// Token: 0x040016FE RID: 5886
		internal const string racWithLabel = "RadAutoCompleteBoxWithLabel";

		// Token: 0x040016FF RID: 5887
		internal const string racTokenSeparator = "racTokenSeparator";

		// Token: 0x04001700 RID: 5888
		internal string cssClassFormatString = "RadAutoCompleteBox RadAutoCompleteBox_{0}";

		// Token: 0x04001701 RID: 5889
		internal string cssPopUpFormatString = "RadAutoCompleteBoxPopup RadAutoCompleteBoxPopup_{0}";

		// Token: 0x04001707 RID: 5895
		private readonly WebServiceSettings _webServiceSettings;

		// Token: 0x04001708 RID: 5896
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x04001709 RID: 5897
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x0400170A RID: 5898
		private AutoCompleteBoxTokensSettings _tokensSettings;

		// Token: 0x0400170B RID: 5899
		private AutoCompleteBoxTextSettings _textSettings;

		// Token: 0x0400170C RID: 5900
		private AutoCompleteBoxLocalization _localization;

		// Token: 0x0400170D RID: 5901
		private bool _showAllResults;

		// Token: 0x0400170E RID: 5902
		private bool _endOfItems;

		// Token: 0x0400170F RID: 5903
		internal bool _shouldFocus;

		// Token: 0x04001710 RID: 5904
		private AutoCompleteBoxEntryCollection _children;

		// Token: 0x04001711 RID: 5905
		internal AutoCompleteBoxClientState clientState;

		// Token: 0x04001712 RID: 5906
		private ITemplate _dropDownItemTemplate;

		// Token: 0x04001713 RID: 5907
		private List<DropDownItem> _dropDownItems;

		// Token: 0x04001714 RID: 5908
		private WaiAriaSettings _ariaSettings;
	}
}

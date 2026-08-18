using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.SearchBox;
using Telerik.Web.UI.SearchBox.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x0200086E RID: 2158
	[ToolboxBitmap(typeof(RadSearchBox), "Telerik.Web.UI.SearchBox.png")]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[Designer("Telerik.Web.Design.RadSearchBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[TelerikToolboxCategory("Data Editing")]
	[RequiredScript(typeof(ChangeLog), 3)]
	[ToolboxData("<{0}:RadSearchBox runat=\"server\"></{0}:RadSearchBox>")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(DropDown), 2)]
	[ClientScriptResource("Telerik.Web.UI.RadSearchBox", "Telerik.Web.UI.SearchBox.RadSearchBoxScripts.js")]
	[RequiredScript(typeof(MaterialRipple))]
	[EmbeddedSkin("SearchBox", typeof(RadSearchBox))]
	[EmbeddedSkin("SearchBox", "Default", typeof(RadSearchBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadSearchBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	public class RadSearchBox : RadDataBoundControl, IPostBackEventHandler, ICallbackEventHandler, IFlatBoundContainer, INamingContainer, ILocalizableControl, ILabelableControl
	{
		// Token: 0x06004F2F RID: 20271 RVA: 0x000F82A4 File Offset: 0x000F64A4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "_emptyMessage", this.EmptyMessage, "");
			base.DescribeProperty<bool>(descriptor, "_enableAutoComplete", this.EnableAutoComplete, true);
			base.DescribeProperty<bool>(descriptor, "_enableDirectionDetection", this.EnableDirectionDetection, false);
			base.DescribeProperty<bool>(descriptor, "_enableScreenBoundaryDetection", this.EnableScreenBoundaryDetection, true);
			base.DescribeProperty<SearchBoxFilter>(descriptor, "filter", this.Filter, SearchBoxFilter.Contains);
			base.DescribeProperty<bool>(descriptor, "_highlightFirstMatch", this.HighlightFirstMatch, false);
			base.DescribeProperty<bool>(descriptor, "isCaseSensitive", this.IsCaseSensitive, false);
			base.DescribeProperty<int>(descriptor, "maxResultCount", this.MaxResultCount, -1);
			base.DescribeProperty<int>(descriptor, "minFilterLength", this.MinFilterLength, 1);
			base.DescribeProperty<bool>(descriptor, "showLoadingIcon", this.ShowLoadingIcon, true);
			base.DescribeProperty<bool>(descriptor, "_showSearchButton", this.ShowSearchButton, true);
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x000F83A0 File Offset: 0x000F65A0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "buttonCommand", this.OnClientButtonCommand);
			RadDataBoundControl.DescribeEvent(descriptor, "dataRequesting", this.OnClientDataRequesting);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "search", this.OnClientSearch);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x000F83F8 File Offset: 0x000F65F8
		// (set) Token: 0x06004F32 RID: 20274 RVA: 0x000F8400 File Offset: 0x000F6600
		private List<DropDownItem> _DropDownItems { get; set; }

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x000F8409 File Offset: 0x000F6609
		protected internal SearchBoxButtonCollection Children
		{
			[DebuggerStepThrough]
			get
			{
				if (this._children == null)
				{
					this._children = new SearchBoxButtonCollection();
				}
				return this._children;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x000F8424 File Offset: 0x000F6624
		protected internal SearchContext InnerContext
		{
			[DebuggerStepThrough]
			get
			{
				if (this._context == null)
				{
					this._context = new SearchContext();
					if (this._localization != null)
					{
						this.Localization_PropertyChanged(this.Localization, new PropertyChangedEventArgs("DefaultItemText"));
						this.Localization_PropertyChanged(this.Localization, new PropertyChangedEventArgs("LoadingItemsMessage"));
					}
				}
				return this._context;
			}
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x000F8480 File Offset: 0x000F6680
		internal void Localization_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this._context == null)
			{
				return;
			}
			if (e.PropertyName.Equals("DefaultItemText"))
			{
				this.InnerContext.ContextControl.DefaultItemText = this.Localization.DefaultItemText;
				return;
			}
			this.InnerContext.ContextControl.LoadingItemsMessage = this.Localization.LoadingItemsMessage;
		}

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x000F84DF File Offset: 0x000F66DF
		internal SearchContextControl ContextControl
		{
			[DebuggerStepThrough]
			get
			{
				return this.InnerContext.ContextControl;
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x000F84EC File Offset: 0x000F66EC
		internal bool InDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x000F84F4 File Offset: 0x000F66F4
		// (set) Token: 0x06004F39 RID: 20281 RVA: 0x000F84FC File Offset: 0x000F66FC
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

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x000F8505 File Offset: 0x000F6705
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x000F8508 File Offset: 0x000F6708
		protected internal override IRenderer CreateControlRenderer()
		{
			return new SearchBoxRenderer(this);
		}

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x000F8510 File Offset: 0x000F6710
		internal bool IsUsingWebServiceBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.WebServiceSettings.Path);
			}
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x000F8525 File Offset: 0x000F6725
		private bool IsBoundToLinqDataSource()
		{
			return this.GetData() is LinqDataSourceView;
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x000F8538 File Offset: 0x000F6738
		private bool IsBoundToEntityDataSource()
		{
			return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.EntityDataSourceView";
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x000F8561 File Offset: 0x000F6761
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			this.EnsureChildControls();
			base.OnPagePreLoad(sender, e);
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x000F8574 File Offset: 0x000F6774
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this._context != null && !this.Controls.Contains(this.InnerContext.ContextControl))
			{
				this.Controls.Add(this.InnerContext.ContextControl);
			}
			if (this.DropDownSettings.HeaderTemplate != null && !this.Controls.Contains(this.DropDownSettings.Header))
			{
				this.Controls.Add(this.DropDownSettings.Header);
				RadSearchBox.ApplyTemplate(this.DropDownSettings.Header, this.DropDownSettings.HeaderTemplate);
			}
			if (this.DropDownSettings.FooterTemplate != null && !this.Controls.Contains(this.DropDownSettings.Footer))
			{
				this.Controls.Add(this.DropDownSettings.Footer);
				RadSearchBox.ApplyTemplate(this.DropDownSettings.Footer, this.DropDownSettings.FooterTemplate);
			}
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x000F866C File Offset: 0x000F686C
		private static void ApplyTemplate(WebControl control, ITemplate template)
		{
			int i = control.Controls.Count;
			if (template != null)
			{
				template.InstantiateIn(control);
			}
			while (i > 0)
			{
				control.Controls.Add(control.Controls[0]);
				i--;
			}
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x000F86B0 File Offset: 0x000F68B0
		// (set) Token: 0x06004F43 RID: 20291 RVA: 0x000F86B8 File Offset: 0x000F68B8
		private protected new DataSourceSelectArguments SelectArguments { protected get; private set; }

		// Token: 0x06004F44 RID: 20292 RVA: 0x000F86C4 File Offset: 0x000F68C4
		public RadSearchBox()
		{
			this._dropDown = new DropDown(this.ViewState);
			this._expandAnimation = new AnimationSettings("Expand", this.ViewState);
			this._collapseAnimation = new AnimationSettings("Collapse", this.ViewState);
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x000F871F File Offset: 0x000F691F
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x000F8730 File Offset: 0x000F6930
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._context != null)
				{
					this._context.Dispose();
					this._context = null;
					this.Localization.PropertyChanged -= this.Localization_PropertyChanged;
				}
				base.Dispose();
				this._dropDown.Dispose();
			}
		}

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x000F8782 File Offset: 0x000F6982
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x000F878F File Offset: 0x000F698F
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x000F879C File Offset: 0x000F699C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x000F87AA File Offset: 0x000F69AA
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x000F87B3 File Offset: 0x000F69B3
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x000F87C8 File Offset: 0x000F69C8
		public string ControlId
		{
			get
			{
				return this.ClientID + "_Input";
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x000F87DC File Offset: 0x000F69DC
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			SearchBoxContext searchBoxContext = javaScriptSerializer.Deserialize<SearchBoxContext>(eventArgument);
			this.Text = searchBoxContext.Text;
			this.LoadDropDownItems(searchBoxContext);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x000F880C File Offset: 0x000F6A0C
		string ICallbackEventHandler.GetCallbackResult()
		{
			StringWriter stringWriter = new StringWriter();
			if (this._DropDownItems == null)
			{
				return string.Empty;
			}
			foreach (DropDownItem dropDownItem in this._DropDownItems)
			{
				dropDownItem.ApplyTemplate(this.DropDownSettings.ItemTemplate);
				dropDownItem.DataItem = null;
				dropDownItem.RenderContents(new HtmlTextWriter(stringWriter));
			}
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new DropDownItemConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(converters);
			string text = javaScriptSerializer.Serialize(this._DropDownItems);
			return string.Concat(new object[]
			{
				text,
				"_$$_",
				stringWriter,
				"_$$_",
				this._endOfItems
			});
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x000F894C File Offset: 0x000F6B4C
		protected void LoadDropDownItems(SearchBoxContext context)
		{
			bool isUsingModelBinders = this.IsUsingModelBinders;
			if (this.DataSource != null || this.DataSourceID != "" || isUsingModelBinders)
			{
				this._showAllResults = context.ShowAllResults;
				SearchBoxDataSourceSelectEventArgs e;
				if (this._context == null)
				{
					e = new SearchBoxDataSourceSelectEventArgs(this.GetDataSource(), context.Text, context.ShowAllResults, context.UserContext);
				}
				else
				{
					e = new SearchBoxDataSourceSelectEventArgs(this.GetDataSource(), context.Text, context.ShowAllResults, context.UserContext, context.SelectedContextItem);
				}
				bool requiresFiltering = this.OnDataSourceSelect(e);
				if (this.IsBoundToEntityDataSource())
				{
					this.SetEntityDataSourceFilter(context.Text);
					requiresFiltering = false;
				}
				else if (this.IsBoundToLinqDataSource())
				{
					this.SetLinqDataSourceFilter(context.Text);
					requiresFiltering = false;
				}
				this.GetData().Select(this.CreateDataSourceSelectArguments(), delegate(IEnumerable data)
				{
					this._DropDownItems = this.GetFilteredDropDownItems(data, context, requiresFiltering);
				});
				return;
			}
			throw new Exception("DataSource not set");
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x000F8A9A File Offset: 0x000F6C9A
		private void SetEndOfItems(int totalItemsCount)
		{
			if (totalItemsCount <= this.MaxResultCount)
			{
				this._endOfItems = true;
			}
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x000F8AAC File Offset: 0x000F6CAC
		private List<DropDownItem> GetFilteredDropDownItems(IEnumerable data, SearchBoxContext context, bool requiresFiltering)
		{
			List<DropDownItem> list = new List<DropDownItem>();
			bool flag = this._context != null && context.SelectedContextItem != null && !string.IsNullOrEmpty(this.DataContextKeyField);
			if (this.IsBoundToLinqDataSource() || this.IsBoundToEntityDataSource())
			{
				this.SetEndOfItems(this.SelectArguments.TotalRowCount);
			}
			else if (this.MaxResultCount > 0)
			{
				this._endOfItems = true;
			}
			if (!string.IsNullOrEmpty(context.Text))
			{
				string value = this.IsCaseSensitive ? context.Text : context.Text.ToLowerInvariant();
				foreach (object obj in data)
				{
					string textFromDataItem = DropDownItem.GetTextFromDataItem(obj, this.DataTextField, this.DataTextFormatString);
					string text = this.IsCaseSensitive ? textFromDataItem : textFromDataItem.ToLowerInvariant();
					if (!requiresFiltering || (this.Filter == SearchBoxFilter.StartsWith && text.StartsWith(value)) || (this.Filter == SearchBoxFilter.Contains && text.Contains(value)))
					{
						if (!context.ShowAllResults && this.MaxResultCount > 0 && list.Count == this.MaxResultCount)
						{
							this._endOfItems = false;
							break;
						}
						if (flag)
						{
							string text2 = DataBinder.GetPropertyValue(obj, this.DataContextKeyField, string.Empty).ToLowerInvariant();
							if (!text2.Equals(context.SelectedContextItem.Key, StringComparison.InvariantCultureIgnoreCase))
							{
								continue;
							}
						}
						string valueFromDataItem = DropDownItem.GetValueFromDataItem(obj, this.DataValueField);
						list.Add(new DropDownItem(obj, textFromDataItem, valueFromDataItem, this.DataKeyNames));
					}
				}
			}
			return list;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x000F8C5C File Offset: 0x000F6E5C
		private void SetEntityDataSourceFilter(string filterString)
		{
			string text = string.Format("it.[{0}] LIKE '{{0}}{1}%'", this.DataTextField, filterString.Replace("'", "''"));
			text = string.Format(text, (this.Filter == SearchBoxFilter.Contains) ? "%" : string.Empty);
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

		// Token: 0x06004F53 RID: 20307 RVA: 0x000F8D2C File Offset: 0x000F6F2C
		private void SetLinqDataSourceFilter(string filterString)
		{
			LinqDataSourceView linqDataSourceView = this.GetData() as LinqDataSourceView;
			string text = string.Format("{0}.{{0}}(\"{1}\")", this.DataTextField, filterString.Replace("\"", "\"\""));
			text = string.Format(text, (this.Filter == SearchBoxFilter.Contains) ? "Contains" : "StartsWith");
			if (linqDataSourceView.Where.Length > 0)
			{
				string format = "{0} && {1}";
				string where = linqDataSourceView.Where;
				linqDataSourceView.Where = string.Format(format, where, text);
				return;
			}
			linqDataSourceView.Where = text;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x000F8DB4 File Offset: 0x000F6FB4
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

		// Token: 0x06004F55 RID: 20309 RVA: 0x000F8E10 File Offset: 0x000F7010
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderingMode(descriptor);
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new SearchBoxButtonConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			if (this.Buttons.Count > 0)
			{
				descriptor.AddScriptProperty("buttonData", javaScriptSerializer.Serialize(this.Buttons));
			}
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			if (this.IsUsingWebServiceBinding)
			{
				this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			}
			if (!string.IsNullOrEmpty(this.DropDownSettings.ClientTemplate))
			{
				descriptor.AddProperty("_clientTemplate", this.DropDownSettings.ClientTemplate);
			}
			if (!this.DropDownSettings.Width.IsEmpty)
			{
				descriptor.AddProperty("dropDownWidth", this.DropDownSettings.Width.ToString(CultureInfo.InvariantCulture));
			}
			if (!this.DropDownSettings.Height.IsEmpty)
			{
				descriptor.AddProperty("dropDownHeight", this.DropDownSettings.Height.ToString(CultureInfo.InvariantCulture));
			}
			if (this._context != null)
			{
				this.ContextControl.Describe(javaScriptSerializer, descriptor);
				if (!string.IsNullOrEmpty(this.DataContextKeyField))
				{
					descriptor.AddProperty("dataContextKeyField", this.DataContextKeyField);
				}
			}
			this.DescribeClientDataSource(descriptor);
			descriptor.AddProperty("_showAllResultsText", this.Localization.ShowAllResults);
			descriptor.AddProperty("_skinName", base.RuntimeSkin);
			base.DescribeProperty<bool>(descriptor, "_shouldFocus", this._shouldFocus, false);
			this.DescribePostBack(descriptor);
			this.ExpandAnimation.Describe("expandAnimation", javaScriptSerializer, descriptor);
			this.CollapseAnimation.Describe("collapseAnimation", javaScriptSerializer, descriptor);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x000F8FF0 File Offset: 0x000F71F0
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

		// Token: 0x06004F57 RID: 20311 RVA: 0x000F908C File Offset: 0x000F728C
		private void DescribePostBack(IScriptDescriptor descriptor)
		{
			bool flag = false;
			if (base.Events[RadSearchBox.SearchEvent] != null)
			{
				descriptor.AddProperty("_postBackOnSearch", true);
				flag = true;
			}
			if (base.Events[RadSearchBox.ButtonCommandEvent] != null)
			{
				descriptor.AddProperty("_postBackOnButtonCommand", true);
				flag = true;
			}
			if (flag)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x000F90FC File Offset: 0x000F72FC
		protected string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x000F9138 File Offset: 0x000F7338
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			return new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x000F9157 File Offset: 0x000F7357
		internal void Describe(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x000F9160 File Offset: 0x000F7360
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

		// Token: 0x06004F5C RID: 20316 RVA: 0x000F919C File Offset: 0x000F739C
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Children).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x000F91D6 File Offset: 0x000F73D6
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Children).TrackViewState();
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x000F91E9 File Offset: 0x000F73E9
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x000F91F4 File Offset: 0x000F73F4
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			SearchBoxPostBackCommand searchBoxPostBackCommand = null;
			try
			{
				searchBoxPostBackCommand = new JavaScriptSerializer().Deserialize<SearchBoxPostBackCommand>(HttpUtility.UrlDecode(eventArgument).Replace("&squote", "'"));
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (searchBoxPostBackCommand == null)
			{
				return;
			}
			switch (searchBoxPostBackCommand.Type)
			{
			case SearchBoxCommand.Search:
			{
				SearchBoxEventArgs e = new SearchBoxEventArgs(searchBoxPostBackCommand.Text, searchBoxPostBackCommand.Value, searchBoxPostBackCommand.DataItem);
				this.OnSearch(e);
				return;
			}
			case SearchBoxCommand.ButtonCommand:
			{
				SearchBoxButtonEventArgs e2 = new SearchBoxButtonEventArgs(searchBoxPostBackCommand.CommandName, searchBoxPostBackCommand.CommandArgument);
				this.OnButtonCommand(e2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x000F929C File Offset: 0x000F749C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[this.ClientID];
			if (!string.IsNullOrEmpty(text) && !text.Equals(this.EmptyMessage, StringComparison.CurrentCulture))
			{
				this.Text = text;
			}
			string text2 = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text2))
			{
				return false;
			}
			SearchBoxClientState searchBoxClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				searchBoxClientState = javaScriptSerializer.Deserialize<SearchBoxClientState>(text2);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (searchBoxClientState == null)
			{
				return false;
			}
			this.LoadClientState(searchBoxClientState);
			return false;
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x000F9334 File Offset: 0x000F7534
		private void LoadClientState(SearchBoxClientState clientState)
		{
			this.Enabled = clientState.Enabled;
			if (this._context != null)
			{
				this.ContextControl.SelectedIndex = clientState.SelectedContextIndex;
			}
			if (clientState.LogEntries != null)
			{
				this.LoadLogEntries(clientState.LogEntries);
			}
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x000F9370 File Offset: 0x000F7570
		private void LoadLogEntries(ClientStateLogEntry[] entries)
		{
			for (int i = 0; i < entries.Length; i++)
			{
				int num = Convert.ToInt32(entries[i].Index);
				if (num >= 0 && num <= this.Buttons.Count)
				{
					switch (entries[i].Type)
					{
					case ClientStateLogEntryType.Insert:
					{
						SearchBoxButton searchBoxButton = new SearchBoxButton();
						searchBoxButton.LoadFromDictionary(entries[i].Data);
						this.Buttons.Insert(num, searchBoxButton);
						break;
					}
					case ClientStateLogEntryType.Remove:
						this.Buttons.RemoveAt(num);
						break;
					case ClientStateLogEntryType.Update:
						this.Buttons[num].LoadFromDictionary(entries[i].Data);
						break;
					}
				}
			}
		}

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06004F63 RID: 20323 RVA: 0x000F941E File Offset: 0x000F761E
		// (remove) Token: 0x06004F64 RID: 20324 RVA: 0x000F9431 File Offset: 0x000F7631
		public event SearchBoxEventHandler Search
		{
			add
			{
				base.Events.AddHandler(RadSearchBox.SearchEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSearchBox.SearchEvent, value);
			}
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x000F9444 File Offset: 0x000F7644
		protected void OnSearch(SearchBoxEventArgs e)
		{
			this.RaiseEvent(RadSearchBox.SearchEvent, e);
		}

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06004F66 RID: 20326 RVA: 0x000F9452 File Offset: 0x000F7652
		// (remove) Token: 0x06004F67 RID: 20327 RVA: 0x000F9465 File Offset: 0x000F7665
		public event SearchBoxButtonEventHandler ButtonCommand
		{
			add
			{
				base.Events.AddHandler(RadSearchBox.ButtonCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSearchBox.ButtonCommandEvent, value);
			}
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x000F9478 File Offset: 0x000F7678
		protected void OnButtonCommand(SearchBoxButtonEventArgs e)
		{
			this.RaiseButtonEvent(RadSearchBox.ButtonCommandEvent, e);
		}

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06004F69 RID: 20329 RVA: 0x000F9486 File Offset: 0x000F7686
		// (remove) Token: 0x06004F6A RID: 20330 RVA: 0x000F9499 File Offset: 0x000F7699
		public event SearchBoxDataSourceSelectEventHandler DataSourceSelect
		{
			add
			{
				base.Events.AddHandler(RadSearchBox.DataSourceSelectEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadSearchBox.DataSourceSelectEvent, value);
			}
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x000F94AC File Offset: 0x000F76AC
		protected bool OnDataSourceSelect(SearchBoxDataSourceSelectEventArgs e)
		{
			SearchBoxDataSourceSelectEventHandler searchBoxDataSourceSelectEventHandler = (SearchBoxDataSourceSelectEventHandler)base.Events[RadSearchBox.DataSourceSelectEvent];
			if (searchBoxDataSourceSelectEventHandler != null)
			{
				searchBoxDataSourceSelectEventHandler(this, e);
				return false;
			}
			return true;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x000F94E0 File Offset: 0x000F76E0
		private void RaiseEvent(object eventKey, SearchBoxEventArgs e)
		{
			SearchBoxEventHandler searchBoxEventHandler = (SearchBoxEventHandler)base.Events[eventKey];
			if (searchBoxEventHandler != null)
			{
				searchBoxEventHandler(this, e);
			}
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x000F950C File Offset: 0x000F770C
		private void RaiseButtonEvent(object eventKey, SearchBoxButtonEventArgs e)
		{
			SearchBoxButtonEventHandler searchBoxButtonEventHandler = (SearchBoxButtonEventHandler)base.Events[eventKey];
			if (searchBoxButtonEventHandler != null)
			{
				searchBoxButtonEventHandler(this, e);
			}
		}

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x000F9536 File Offset: 0x000F7736
		// (set) Token: 0x06004F6F RID: 20335 RVA: 0x000F9556 File Offset: 0x000F7756
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed when RadSearchBox is initialized")]
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

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x000F9569 File Offset: 0x000F7769
		// (set) Token: 0x06004F71 RID: 20337 RVA: 0x000F9589 File Offset: 0x000F7789
		[DefaultValue("")]
		[Description("The JavaScript function executed when a search is triggered in RadSearchBox")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("search")]
		[Category("Client-side events")]
		public string OnClientSearch
		{
			get
			{
				return (string)(this.ViewState["OnClientSearch"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSearch"] = value;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x000F959C File Offset: 0x000F779C
		// (set) Token: 0x06004F73 RID: 20339 RVA: 0x000F95BC File Offset: 0x000F77BC
		[Category("Client-side events")]
		[Description("The JavaScript function executed when a search is triggered in RadSearchBox")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("buttonCommand")]
		public string OnClientButtonCommand
		{
			get
			{
				return (string)(this.ViewState["OnClientButtonCommand"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientButtonCommand"] = value;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x000F95CF File Offset: 0x000F77CF
		// (set) Token: 0x06004F75 RID: 20341 RVA: 0x000F95EF File Offset: 0x000F77EF
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The JavaScript function executed before a search is triggered in RadSearchBox")]
		[ClientPropertyName("dataRequesting")]
		public string OnClientDataRequesting
		{
			get
			{
				return (string)(this.ViewState["OnClientDataRequesting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataRequesting"] = value;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x000F9602 File Offset: 0x000F7802
		[ClientControlProperty]
		[ClientPropertyName("_uniqueId")]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06004F77 RID: 20343 RVA: 0x000F960A File Offset: 0x000F780A
		[Category("Behavior")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Description("DropDown Settings")]
		public DropDownSettings DropDownSettings
		{
			get
			{
				return this._dropDown.DropDownSettings;
			}
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x000F9617 File Offset: 0x000F7817
		// (set) Token: 0x06004F79 RID: 20345 RVA: 0x000F9638 File Offset: 0x000F7838
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		[DefaultValue(true)]
		[Description("Whether the AutoComplete functionality is enabled")]
		[ClientPropertyName("_enableAutoComplete")]
		public virtual bool EnableAutoComplete
		{
			get
			{
				return (bool)(this.ViewState["EnableAutoComplete"] ?? true);
			}
			set
			{
				this.ViewState["EnableAutoComplete"] = value;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x000F9650 File Offset: 0x000F7850
		// (set) Token: 0x06004F7B RID: 20347 RVA: 0x000F9671 File Offset: 0x000F7871
		[ClientControlProperty]
		[DefaultValue(true)]
		[Bindable(false)]
		[Category("Behavior")]
		[Description("Whether the default search button is rendered.")]
		[ClientPropertyName("_showSearchButton")]
		public virtual bool ShowSearchButton
		{
			get
			{
				return (bool)(this.ViewState["ShowSearchButton"] ?? true);
			}
			set
			{
				this.ViewState["ShowSearchButton"] = value;
			}
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x000F9689 File Offset: 0x000F7889
		// (set) Token: 0x06004F7D RID: 20349 RVA: 0x000F96AA File Offset: 0x000F78AA
		[DefaultValue(true)]
		[Bindable(false)]
		[ClientControlProperty]
		[Category("Appearance")]
		[ClientPropertyName("showLoadingIcon")]
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

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06004F7E RID: 20350 RVA: 0x000F96C2 File Offset: 0x000F78C2
		// (set) Token: 0x06004F7F RID: 20351 RVA: 0x000F96E3 File Offset: 0x000F78E3
		[Description("Whether first matching result will be highlighted upon drop down opening.")]
		[Bindable(false)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_highlightFirstMatch")]
		public virtual bool HighlightFirstMatch
		{
			get
			{
				return (bool)(this.ViewState["HighlightFirstMatch"] ?? false);
			}
			set
			{
				this.ViewState["HighlightFirstMatch"] = value;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x000F96FB File Offset: 0x000F78FB
		// (set) Token: 0x06004F81 RID: 20353 RVA: 0x000F971C File Offset: 0x000F791C
		[Description("Defines how many results are shown in the dropdown")]
		[ClientControlProperty]
		[ClientPropertyName("maxResultCount")]
		[Bindable(false)]
		[DefaultValue(-1)]
		[Category("Behavior")]
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

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06004F82 RID: 20354 RVA: 0x000F9734 File Offset: 0x000F7934
		// (set) Token: 0x06004F83 RID: 20355 RVA: 0x000F9755 File Offset: 0x000F7955
		[DefaultValue(1)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Defines the minimum number of characters that must be typed before a search is made.")]
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

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x000F976D File Offset: 0x000F796D
		// (set) Token: 0x06004F85 RID: 20357 RVA: 0x000F978E File Offset: 0x000F798E
		[Bindable(false)]
		[DefaultValue(false)]
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

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x000F97A6 File Offset: 0x000F79A6
		// (set) Token: 0x06004F87 RID: 20359 RVA: 0x000F97C6 File Offset: 0x000F79C6
		[Category("Setup")]
		[DefaultValue("")]
		[ClientControlProperty]
		[ClientPropertyName("_emptyMessage")]
		[Description("The message which will be shown when the input of the RadSearchBox is empty.")]
		[Bindable(false)]
		public virtual string EmptyMessage
		{
			get
			{
				return (this.ViewState["EmptyMessage"] as string) ?? string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.ViewState["EmptyMessage"] = value;
				}
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x000F97E1 File Offset: 0x000F79E1
		// (set) Token: 0x06004F89 RID: 20361 RVA: 0x000F9811 File Offset: 0x000F7A11
		[DefaultValue("")]
		[Browsable(false)]
		[Description("The text from the input of the control.")]
		[Bindable(false)]
		public virtual string Text
		{
			get
			{
				if (this.Enabled)
				{
					return this._text;
				}
				return (string)(this.ViewState["Text"] ?? this._text);
			}
			set
			{
				this._text = value;
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x000F982B File Offset: 0x000F7A2B
		// (set) Token: 0x06004F8B RID: 20363 RVA: 0x000F984C File Offset: 0x000F7A4C
		[Bindable(false)]
		[Category("Behavior")]
		[DefaultValue(SearchBoxFilter.Contains)]
		[ClientControlProperty]
		public SearchBoxFilter Filter
		{
			get
			{
				return (SearchBoxFilter)(this.ViewState["Filter"] ?? SearchBoxFilter.Contains);
			}
			set
			{
				this.ViewState["Filter"] = value;
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x000F9864 File Offset: 0x000F7A64
		// (set) Token: 0x06004F8D RID: 20365 RVA: 0x000F9884 File Offset: 0x000F7A84
		[DefaultValue("")]
		[Category("Data")]
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

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x000F9897 File Offset: 0x000F7A97
		// (set) Token: 0x06004F8F RID: 20367 RVA: 0x000F98B7 File Offset: 0x000F7AB7
		[DefaultValue("")]
		[Category("Data")]
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

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x000F98CA File Offset: 0x000F7ACA
		// (set) Token: 0x06004F91 RID: 20369 RVA: 0x000F98EA File Offset: 0x000F7AEA
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataContextKeyField
		{
			get
			{
				return (string)(this.ViewState["DataContextKeyField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataContextKeyField"] = value;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x000F98FD File Offset: 0x000F7AFD
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06004F93 RID: 20371 RVA: 0x000F9905 File Offset: 0x000F7B05
		// (set) Token: 0x06004F94 RID: 20372 RVA: 0x000F9926 File Offset: 0x000F7B26
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(ListConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Description("Comma delimited list of data-field Names")]
		[Category("Data")]
		public virtual string[] DataKeyNames
		{
			get
			{
				return (string[])(this.ViewState["DataKeyNames"] ?? new string[0]);
			}
			set
			{
				this.ViewState["DataKeyNames"] = value;
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x06004F95 RID: 20373 RVA: 0x000F9939 File Offset: 0x000F7B39
		// (set) Token: 0x06004F96 RID: 20374 RVA: 0x000F9959 File Offset: 0x000F7B59
		[DefaultValue("")]
		[Category("Data")]
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

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x06004F97 RID: 20375 RVA: 0x000F996C File Offset: 0x000F7B6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public SearchBoxButtonCollection Buttons
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x000F9974 File Offset: 0x000F7B74
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public SearchContext SearchContext
		{
			get
			{
				return this.InnerContext;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x000F997C File Offset: 0x000F7B7C
		[Category("Behavior")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Description("Gets the settings(service path and method name)for the web service used to provide search results.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				if (this._webServiceSettings == null)
				{
					this._webServiceSettings = new WebServiceSettings(this.ViewState);
				}
				return this._webServiceSettings;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x000F999D File Offset: 0x000F7B9D
		[Category("Behavior")]
		[Description("The animation played when the dropdown is opened")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public AnimationSettings ExpandAnimation
		{
			get
			{
				return this._expandAnimation;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x000F99A5 File Offset: 0x000F7BA5
		[Category("Behavior")]
		[DefaultValue(null)]
		[Description("The animation played when the dropdown is closed")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AnimationSettings CollapseAnimation
		{
			get
			{
				return this._collapseAnimation;
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x000F99AD File Offset: 0x000F7BAD
		// (set) Token: 0x06004F9D RID: 20381 RVA: 0x000F99CE File Offset: 0x000F7BCE
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Bindable(false)]
		[ClientPropertyName("_enableScreenBoundaryDetection")]
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

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x06004F9E RID: 20382 RVA: 0x000F99E6 File Offset: 0x000F7BE6
		// (set) Token: 0x06004F9F RID: 20383 RVA: 0x000F9A07 File Offset: 0x000F7C07
		[ClientPropertyName("_enableDirectionDetection")]
		[Bindable(false)]
		[Category("Behavior")]
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

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x000F9A20 File Offset: 0x000F7C20
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public SearchBoxLocalization Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new SearchBoxLocalization(new LocalizationProvider("RadSearchBox", this, this.LocalizationPath));
					this._localization.PropertyChanged += this.Localization_PropertyChanged;
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x000F9A81 File Offset: 0x000F7C81
		// (set) Token: 0x06004FA2 RID: 20386 RVA: 0x000F9AA4 File Offset: 0x000F7CA4
		[DefaultValue("")]
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadSearchBox will look for its .resx localization files.")]
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

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x06004FA3 RID: 20387 RVA: 0x000F9AF7 File Offset: 0x000F7CF7
		// (set) Token: 0x06004FA4 RID: 20388 RVA: 0x000F9B17 File Offset: 0x000F7D17
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

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x000F9B2A File Offset: 0x000F7D2A
		// (set) Token: 0x06004FA6 RID: 20390 RVA: 0x000F9B4A File Offset: 0x000F7D4A
		[DefaultValue("")]
		[Description("The label of the control.")]
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

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x000F9B5D File Offset: 0x000F7D5D
		// (set) Token: 0x06004FA8 RID: 20392 RVA: 0x000F9B7D File Offset: 0x000F7D7D
		[DefaultValue("")]
		[Description("Css class of the label")]
		[Category("Appearance")]
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

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06004FA9 RID: 20393 RVA: 0x000F9B90 File Offset: 0x000F7D90
		// (set) Token: 0x06004FAA RID: 20394 RVA: 0x000F9BB5 File Offset: 0x000F7DB5
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

		// Token: 0x06004FAB RID: 20395 RVA: 0x000F9BEA File Offset: 0x000F7DEA
		public override void Focus()
		{
			this._shouldFocus = true;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x000F9BF3 File Offset: 0x000F7DF3
		// Note: this type is marked as 'beforefieldinit'.
		static RadSearchBox()
		{
			RadSearchBox.SearchEvent = new object();
			RadSearchBox.ButtonCommandEvent = new object();
			RadSearchBox.DataSourceSelectEvent = new object();
		}

		// Token: 0x040013C5 RID: 5061
		private DropDown _dropDown;

		// Token: 0x040013C6 RID: 5062
		private WebServiceSettings _webServiceSettings;

		// Token: 0x040013C7 RID: 5063
		private readonly AnimationSettings _expandAnimation;

		// Token: 0x040013C8 RID: 5064
		private readonly AnimationSettings _collapseAnimation;

		// Token: 0x040013C9 RID: 5065
		private SearchBoxLocalization _localization;

		// Token: 0x040013CA RID: 5066
		private string _text = string.Empty;

		// Token: 0x040013CB RID: 5067
		private bool _showAllResults;

		// Token: 0x040013CC RID: 5068
		private bool _endOfItems;

		// Token: 0x040013CD RID: 5069
		private bool _shouldFocus;

		// Token: 0x040013CE RID: 5070
		private SearchBoxButtonCollection _children;

		// Token: 0x040013CF RID: 5071
		internal SearchContext _context;
	}
}

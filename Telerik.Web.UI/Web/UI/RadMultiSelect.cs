using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.MultiSelect;

namespace Telerik.Web.UI
{
	// Token: 0x02000607 RID: 1543
	[ValidationProperty("Text")]
	[ToolboxBitmap(typeof(RadMultiSelect), "Telerik.Web.UI.MultiSelect.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadMultiSelect))]
	[ParseChildren(ChildrenAsProperties = true)]
	[DefaultEvent("SelectionChanged")]
	[DefaultProperty("Items")]
	[ControlValueProperty("Value")]
	[TelerikToolboxCategory("Data Editing")]
	[Designer("Telerik.Web.Design.RadMultiSelectDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadMultiSelect Runat=\"server\"></{0}:RadMultiSelect>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(Html5MultiSelect))]
	[ClientScriptResource("Telerik.Web.UI.RadMultiSelect", "Telerik.Web.UI.MultiSelect.Scripts.RadMultiSelect.js")]
	[EmbeddedSkin("MultiSelect", typeof(RadMultiSelect))]
	[EmbeddedSkin("MultiSelect", "Default", typeof(RadMultiSelect))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadMultiSelect))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadMultiSelect))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadMultiSelect))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadMultiSelect))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadMultiSelect))]
	public class RadMultiSelect : RadDataBoundControl, INamingContainer, IItemContainer
	{
		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060037B5 RID: 14261 RVA: 0x000B7D15 File Offset: 0x000B5F15
		// (remove) Token: 0x060037B6 RID: 14262 RVA: 0x000B7D28 File Offset: 0x000B5F28
		public event RadMultiSelectItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadMultiSelect.MultiSelectItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiSelect.MultiSelectItemDataBoundEvent, value);
			}
		}

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x060037B7 RID: 14263 RVA: 0x000B7D3B File Offset: 0x000B5F3B
		// (remove) Token: 0x060037B8 RID: 14264 RVA: 0x000B7D4E File Offset: 0x000B5F4E
		public event RadMultiSelectItemEventHandler ItemSelected
		{
			add
			{
				base.Events.AddHandler(RadMultiSelect.ItemSelectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiSelect.ItemSelectedEvent, value);
			}
		}

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x060037B9 RID: 14265 RVA: 0x000B7D61 File Offset: 0x000B5F61
		// (remove) Token: 0x060037BA RID: 14266 RVA: 0x000B7D74 File Offset: 0x000B5F74
		public event RadMultiSelectItemEventHandler ItemDeselected
		{
			add
			{
				base.Events.AddHandler(RadMultiSelect.ItemDeselectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiSelect.ItemDeselectedEvent, value);
			}
		}

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x060037BB RID: 14267 RVA: 0x000B7D87 File Offset: 0x000B5F87
		// (remove) Token: 0x060037BC RID: 14268 RVA: 0x000B7D9A File Offset: 0x000B5F9A
		public event RadMulitSelectSeletionChangedEventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(RadMultiSelect.SelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiSelect.SelectionChangedEvent, value);
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000B7DB0 File Offset: 0x000B5FB0
		private void RaiseEvent(object eventKey, RadMultiSelectItemEventArgs e)
		{
			RadMultiSelectItemEventHandler radMultiSelectItemEventHandler = (RadMultiSelectItemEventHandler)base.Events[eventKey];
			if (radMultiSelectItemEventHandler != null)
			{
				radMultiSelectItemEventHandler(this, e);
			}
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000B7DDA File Offset: 0x000B5FDA
		protected virtual void RaiseItemSelectedEvent(MultiSelectItem args)
		{
			string empty = string.Empty;
			if (!string.IsNullOrEmpty(this.DataValueField) && !string.IsNullOrEmpty(args.Value))
			{
				string value = args.Value;
			}
			else
			{
				string text = args.Text;
			}
			this.OnItemSelected(args);
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000B7E13 File Offset: 0x000B6013
		protected virtual void RaiseItemDeselectedEvent(MultiSelectItem args)
		{
			string empty = string.Empty;
			if (!string.IsNullOrEmpty(this.DataValueField) && !string.IsNullOrEmpty(args.Value))
			{
				string value = args.Value;
			}
			else
			{
				string text = args.Text;
			}
			this.OnItemDeselected(args);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000B7E4C File Offset: 0x000B604C
		protected virtual void RaiseSelectionChangedEvent()
		{
			this.OnSelectionChanged();
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000B7E54 File Offset: 0x000B6054
		protected virtual void OnItemSelected(MultiSelectItem item)
		{
			RadMultiSelectItemEventHandler radMultiSelectItemEventHandler = (RadMultiSelectItemEventHandler)base.Events[RadMultiSelect.ItemSelectedEvent];
			if (radMultiSelectItemEventHandler != null)
			{
				RadMultiSelectItemEventArgs e = new RadMultiSelectItemEventArgs(item);
				radMultiSelectItemEventHandler(this, e);
			}
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000B7E8C File Offset: 0x000B608C
		protected virtual void OnItemDeselected(MultiSelectItem item)
		{
			RadMultiSelectItemEventHandler radMultiSelectItemEventHandler = (RadMultiSelectItemEventHandler)base.Events[RadMultiSelect.ItemDeselectedEvent];
			if (radMultiSelectItemEventHandler != null)
			{
				RadMultiSelectItemEventArgs e = new RadMultiSelectItemEventArgs(item);
				radMultiSelectItemEventHandler(this, e);
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000B7EC4 File Offset: 0x000B60C4
		protected virtual void OnSelectionChanged()
		{
			RadMulitSelectSeletionChangedEventHandler radMulitSelectSeletionChangedEventHandler = (RadMulitSelectSeletionChangedEventHandler)base.Events[RadMultiSelect.SelectionChangedEvent];
			if (radMulitSelectSeletionChangedEventHandler != null)
			{
				radMulitSelectSeletionChangedEventHandler(this, new EventArgs());
			}
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000B7EF6 File Offset: 0x000B60F6
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000B7F14 File Offset: 0x000B6114
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.ClientEvents.OnDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "open", this.ClientEvents.OnOpen);
			RadDataBoundControl.DescribeEvent(descriptor, "close", this.ClientEvents.OnClose);
			RadDataBoundControl.DescribeEvent(descriptor, "filtering", this.ClientEvents.OnFiltering);
			RadDataBoundControl.DescribeEvent(descriptor, "change", this.ClientEvents.OnChange);
			RadDataBoundControl.DescribeEvent(descriptor, "select", this.ClientEvents.OnSelect);
			RadDataBoundControl.DescribeEvent(descriptor, "deselect", this.ClientEvents.OnDeselect);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000B7FEE File Offset: 0x000B61EE
		[Browsable(true)]
		[Description("Gets the RadClientDataSource instance.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual WebServiceClientDataSource WebServiceClientDataSource
		{
			get
			{
				if (this._WebServiceClientDataSource == null)
				{
					this._WebServiceClientDataSource = new WebServiceClientDataSource();
				}
				return this._WebServiceClientDataSource;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000B8009 File Offset: 0x000B6209
		protected override void CreateChildControls()
		{
			this.WebServiceClientDataSource.ID = "WebServiceCDS";
			this.Controls.Add(this.WebServiceClientDataSource);
			this.OnChildrenCreated();
			base.CreateChildControls();
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000B8038 File Offset: 0x000B6238
		protected virtual void OnChildrenCreated()
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadMultiSelect.EventChildrenCreated];
			if (eventHandler != null)
			{
				eventHandler(this, new EventArgs());
			}
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000B806A File Offset: 0x000B626A
		public RadMultiSelect()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000B8083 File Offset: 0x000B6283
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000B809A File Offset: 0x000B629A
		internal string SerializedDataSource
		{
			get
			{
				return (string)this.ViewState["SerializedDataSource"];
			}
			set
			{
				this.ViewState["SerializedDataSource"] = value;
			}
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000B80AD File Offset: 0x000B62AD
		private string DecodeText(string text)
		{
			if (text != null)
			{
				text = HttpUtility.UrlDecode(text).Replace("&squote", "'");
			}
			return text;
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000B80CA File Offset: 0x000B62CA
		protected virtual void BindItems(IEnumerable<MultiSelectItem> items)
		{
			this.Items.Clear();
			this.Items.AddRange(items);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000B80E4 File Offset: 0x000B62E4
		protected virtual void DescribeItems(IScriptDescriptor descriptor)
		{
			IList<MultiSelectItem> allItems = this.GetAllItems();
			if (this.SerializedDataSource != null)
			{
				descriptor.AddScriptProperty("itemsData", this.SerializedDataSource);
				return;
			}
			if (allItems.Count > 0)
			{
				descriptor.AddScriptProperty("itemsData", this.serializer.Serialize(allItems));
				return;
			}
			descriptor.AddScriptProperty("itemsData", "[]");
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x000B8143 File Offset: 0x000B6343
		// (set) Token: 0x060037D0 RID: 14288 RVA: 0x000B8163 File Offset: 0x000B6363
		[DefaultValue("")]
		[Category("Behavior")]
		[Bindable(true)]
		[Description("Gets or sets the name of the validation group to which this validation control belongs.")]
		public virtual string ValidationGroup
		{
			get
			{
				return (string)(this.ViewState["ValidationGroup"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x060037D1 RID: 14289 RVA: 0x000B8176 File Offset: 0x000B6376
		// (set) Token: 0x060037D2 RID: 14290 RVA: 0x000B8196 File Offset: 0x000B6396
		[DefaultValue("")]
		[UrlProperty("*.aspx")]
		[Themeable(false)]
		[Category("Behavior")]
		public virtual string PostBackUrl
		{
			get
			{
				return (string)(this.ViewState["PostBackUrl"] ?? "");
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000B81A9 File Offset: 0x000B63A9
		// (set) Token: 0x060037D4 RID: 14292 RVA: 0x000B81CA File Offset: 0x000B63CA
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether the control causes validation to fire.")]
		public virtual bool CausesValidation
		{
			get
			{
				return (bool)(this.ViewState["CausesValidation"] ?? true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000B81E2 File Offset: 0x000B63E2
		protected override void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack)
			{
				this.PerformValidation();
			}
			this.OnSelectionChanged();
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000B81F8 File Offset: 0x000B63F8
		private void PerformValidation()
		{
			if (!this.CausesValidation)
			{
				return;
			}
			this.Page.Validate(this.ValidationGroup);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000B8214 File Offset: 0x000B6414
		internal virtual bool RequiresValidation()
		{
			return this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000B823C File Offset: 0x000B643C
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string validationGroup, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null)
			{
				if (this.RequiresValidation())
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = validationGroup;
				}
				if (!string.IsNullOrEmpty(postBackUrl))
				{
					postBackOptions.ActionUrl = postBackUrl;
				}
			}
			return postBackOptions;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000B828C File Offset: 0x000B648C
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string postBackUrl = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			return this.GetPostBackOptions(control, argument, this.ValidationGroup, postBackUrl);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000B82D0 File Offset: 0x000B64D0
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000B830C File Offset: 0x000B650C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			this.DescribeItems(descriptor);
			if (!string.IsNullOrEmpty(this.CascadeFrom))
			{
				try
				{
					Control control = CascadeFromControlHelper.FindControl(this, this.CascadeFrom);
					descriptor.AddProperty("cascadeFromClientID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("cascadeFromClientID", this.CascadeFrom);
				}
			}
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", this.serializer.Serialize(base.Attributes));
			}
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control2 = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("clientDataSourceID", control2.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("clientDataSourceID", this.ClientDataSourceID);
				}
			}
			descriptor.AddProperty("_webServiceClientDataSource", this.WebServiceClientDataSource.ClientID);
			if (base.Events[RadMultiSelect.ItemSelectedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnSelect", true);
				descriptor.AddProperty("_postBackOnChange", true);
			}
			if (base.Events[RadMultiSelect.ItemDeselectedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnDeselect", true);
				descriptor.AddProperty("_postBackOnChange", true);
			}
			if (base.Events[RadMultiSelect.SelectionChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackOnDeselect", true);
				descriptor.AddProperty("_postBackOnChange", true);
			}
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000B84E4 File Offset: 0x000B66E4
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			this.Items.Clear();
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			controlDataBinder.BindToEnumerableData(data);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000B8518 File Offset: 0x000B6718
		internal void AddSerializedData(StringBuilder sb, PropertyDescriptorCollection props, object dataItem, bool isXmlDataSource = false)
		{
			foreach (object obj in props)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				string name = propertyDescriptor.Name;
				sb.Append('"');
				sb.Append(name);
				sb.Append("\":");
				object obj2 = DataBinder.Eval(dataItem, name);
				if (obj2 != null && !obj2.GetType().IsArray)
				{
					object propertyValue = DataBinder.GetPropertyValue(dataItem, name);
					if (propertyValue is string && !isXmlDataSource)
					{
						sb.AppendFormat("\"{0}\",", propertyValue);
					}
					else
					{
						string value = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
						{
							propertyValue
						});
						if (obj2 is DateTime || obj2 is DateTime?)
						{
							sb.Append(HtmlChartHelper.GetSerializedValueField(value, true)).Append(",");
						}
						else
						{
							sb.Append(HtmlChartHelper.GetSerializedValueField(value, false)).Append(",");
						}
					}
				}
				else
				{
					sb.AppendFormat("{0},", this.serializer.Serialize(obj2));
				}
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000B8668 File Offset: 0x000B6868
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			this.Skin = "Default";
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write("<style>\r\n                .RadMultiSelect {\r\n                    width:400px;\r\n                    display: inline;\r\n                    position: relative;\r\n                }\r\n\r\n                .RadMultiSelect_Default .k-multiselect-wrap {\r\n                    border-color: #b3b3b3;\r\n                    color: #333333;\r\n                    background-color: #ffffff;\r\n                }\r\n\r\n                .RadMultiSelect .k-multiselect-wrap {\r\n                    border-radius: 2px;\r\n                    padding: 1px calc( 8px + 16px) 1px 0;\r\n                    width: 400px;\r\n                    border-width: 1px;\r\n                    border-style: solid;\r\n                    box-sizing: border-box;\r\n                    position: relative;\r\n                    transition: all .1s ease;\r\n                    cursor: text;\r\n                    outline: 0;\r\n                }\r\n\r\n                    .RadMultiSelect .k-multiselect-wrap ul {\r\n                        vertical-align: top;\r\n                    }\r\n\r\n                .RadMultiSelect .k-reset, .RadMultiSelectDropDown .k-reset {\r\n                    margin: 0;\r\n                    padding: 0;\r\n                    border: 0;\r\n                    outline: 0;\r\n                    text-decoration: none;\r\n                    font-size: 100%;\r\n                    list-style: none;\r\n                }\r\n\r\n                .RadMultiSelect .k-multiselect-wrap li.k-button {\r\n                    float: left;\r\n                }\r\n\r\n                .RadMultiSelect .k-multiselect-wrap .k-button {\r\n                    border-radius: 2px;\r\n                    box-sizing: border-box;\r\n                    border-width: 1px;\r\n                    border-style: solid;\r\n                    min-height: calc( 1.42857143em + 2px + 2px);\r\n                    padding: 1px 4px;\r\n                    margin: 2px 0 0 2px;\r\n                    cursor: default;\r\n                    display: inline-flex;\r\n                    flex-direction: row;\r\n                    align-items: center;\r\n                    justify-content: center;\r\n                    line-height: inherit;\r\n                    vertical-align: middle;\r\n                    white-space: normal;\r\n                    font-size: 14px;\r\n                    line-height: 1.42857143;\r\n                    font-family: inherit;\r\n                    text-align: center;\r\n                    text-decoration: none;\r\n                    user-select: none;\r\n                    outline: none;\r\n                    -webkit-appearance: none;\r\n                    position: relative;\r\n                }\r\n\r\n                .RadMultiSelect_Default .k-multiselect-wrap li.k-button {\r\n                    border-color: #b3b3b3;\r\n                    color: #333333;\r\n                    background-color: #e6e6e6;\r\n                    background-image: linear-gradient(white, #e6e6e6);\r\n                }\r\n\r\n                .RadMultiSelect .k-multiselect-wrap .k-button {\r\n                    border-radius: 2px;\r\n                    box-sizing: border-box;\r\n                    border-width: 1px;\r\n                    border-style: solid;\r\n                    min-height: calc( 1.42857143em + 2px + 2px);\r\n                    padding: 1px 4px;\r\n                    margin: 2px 0 0 2px;\r\n                    cursor: default;\r\n                    display: inline-flex;\r\n                    flex-direction: row;\r\n                    align-items: center;\r\n                    justify-content: center;\r\n                    line-height: inherit;\r\n                    vertical-align: middle;\r\n                    white-space: normal;\r\n                    font-size: 14px;\r\n                    line-height: 1.42857143;\r\n                    font-family: inherit;\r\n                    text-align: center;\r\n                    text-decoration: none;\r\n                    user-select: none;\r\n                    outline: none;\r\n                    -webkit-appearance: none;\r\n                    position: relative;\r\n                }\r\n\r\n            </style>\r\n            <div class=\"k-widget k-multiselect RadMultiSelect RadMultiSelect_Default k-multiselect-clearable\" style=\"width:400px\" >\r\n                <div class=\"k-multiselect-wrap k-floatwrap\">\r\n                    <ul class=\"k-reset\">\r\n                        <li class=\"k-button\">\r\n                            <span>Item 1</span><span class=\"k-select\">\r\n                                <span class=\"k-icon k-i-close\"></span>\r\n                            </span>\r\n                        </li>\r\n                        <li class=\"k-button\">\r\n                            <span>Item 2</span>\r\n                            <span class=\"k-select\">\r\n                                <span class=\"k-icon k-i-close\"></span>\r\n                            </span>\r\n                        </li>\r\n                    </ul>\r\n                </div>\r\n            </div>");
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000B868C File Offset: 0x000B688C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			base.RenderContents(writer);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000B869C File Offset: 0x000B689C
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
				this.RenderScriptsNoScriptManager(writer);
			}
			if (!base.DesignMode)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
				writer.AddAttribute(HtmlTextWriterAttribute.Multiple, string.Empty);
				base.Render(writer);
			}
			else
			{
				this.RenderDesignTimeHtml(writer);
			}
			writer.Write(string.Format("<span id='{0}' style='display:none'></span>", this.WebServiceClientDataSource.ClientID));
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000B871B File Offset: 0x000B691B
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000B872C File Offset: 0x000B692C
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadMultiSelectConverter(),
				new CloseConverter(),
				new OpenConverter(),
				new AnimationConverter(),
				new MessagesConverter(),
				new PopupConverter(),
				new VirtualConverter(),
				new AttributeCollectionConverter(),
				new MultiSelectItemConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000B87B0 File Offset: 0x000B69B0
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Select;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000B87B4 File Offset: 0x000B69B4
		protected override string CssClassFormatString
		{
			get
			{
				return "RadMultiSelect RadMultiSelect_{0}";
			}
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000B87BC File Offset: 0x000B69BC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			if (array[num] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[num++]);
			((IStateManager)this.AnimationSettings).LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.MessagesSettings).LoadViewState(array[num++]);
			((IStateManager)this.PopupSettings).LoadViewState(array[num++]);
			((IStateManager)this.VirtualSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000B885C File Offset: 0x000B6A5C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState(),
				((IStateManager)this.AnimationSettings).SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.MessagesSettings).SaveViewState(),
				((IStateManager)this.PopupSettings).SaveViewState(),
				((IStateManager)this.VirtualSettings).SaveViewState()
			};
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000B88D0 File Offset: 0x000B6AD0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
			((IStateManager)this.AnimationSettings).TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.MessagesSettings).TrackViewState();
			((IStateManager)this.PopupSettings).TrackViewState();
			((IStateManager)this.VirtualSettings).TrackViewState();
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000B8925 File Offset: 0x000B6B25
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x000B8946 File Offset: 0x000B6B46
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Category("Data")]
		[TypeConverter(typeof(ListConverter))]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Comma delimited list of data-field Names")]
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

		// Token: 0x060037EA RID: 14314 RVA: 0x000B8959 File Offset: 0x000B6B59
		IItem IItemContainer.CreateItem()
		{
			return new MultiSelectItem(this);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000B896C File Offset: 0x000B6B6C
		void IItemContainer.RaiseItemDataBound(IItem item)
		{
			RadMultiSelectItemEventHandler radMultiSelectItemEventHandler = (RadMultiSelectItemEventHandler)base.Events[RadMultiSelect.MultiSelectItemDataBoundEvent];
			MultiSelectItem multiSelectItem = item as MultiSelectItem;
			multiSelectItem.TemplateData = new Dictionary<string, object>();
			List<string> list = (from x in this.DataKeyNames
			select x.Trim()).ToList<string>();
			if (this.DataTextField != null && this.DataTextField.Trim().Length > 0)
			{
				list.Add(this.DataTextField.Trim());
			}
			if (this.DataValueField != null && this.DataValueField.Trim().Length > 0)
			{
				list.Add(this.DataValueField.Trim());
			}
			IEnumerable<string> enumerable = list.Distinct<string>();
			foreach (string text in enumerable)
			{
				try
				{
					object value = DataBinder.Eval(multiSelectItem.DataItem, text);
					multiSelectItem.TemplateData.Add(text, value);
				}
				catch (Exception)
				{
					throw new Exception("The data item does not contain the " + text + " data field");
				}
			}
			RadMultiSelectItemEventArgs e = new RadMultiSelectItemEventArgs(multiSelectItem);
			if (radMultiSelectItemEventHandler != null)
			{
				radMultiSelectItemEventHandler(this, e);
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000B8AC0 File Offset: 0x000B6CC0
		IList IItemContainer.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000B8AC8 File Offset: 0x000B6CC8
		public IList<MultiSelectItem> GetAllItems()
		{
			return this.Items.ToList();
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000B8AD8 File Offset: 0x000B6CD8
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadMultiSelectClientState radMultiSelectClientState = null;
			AdvancedJavaScriptSerializer advancedJavaScriptSerializer = new AdvancedJavaScriptSerializer();
			try
			{
				radMultiSelectClientState = advancedJavaScriptSerializer.Deserialize<RadMultiSelectClientState>(text);
				radMultiSelectClientState.Text = this.DecodeText(radMultiSelectClientState.Text);
			}
			catch (InvalidOperationException ex)
			{
			}
			catch (ArgumentException ex2)
			{
			}
			if (radMultiSelectClientState == null)
			{
				return false;
			}
			this.LoadClientState(radMultiSelectClientState);
			return false;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000B8B50 File Offset: 0x000B6D50
		private void LoadClientState(RadMultiSelectClientState clientState)
		{
			if (base.IsEnabled && this.Enabled != clientState.Enabled)
			{
				this.Enabled = clientState.Enabled;
			}
			this.Value = clientState.Value;
			if (clientState.SelectedItems.Count > 0)
			{
				foreach (MultiSelectClientStateDataItem multiSelectClientStateDataItem in clientState.SelectedItems)
				{
					MultiSelectItem args = new MultiSelectItem
					{
						Text = multiSelectClientStateDataItem.Text,
						Value = multiSelectClientStateDataItem.Value,
						DataItem = multiSelectClientStateDataItem.DataItem
					};
					this.RaiseItemSelectedEvent(args);
				}
			}
			if (clientState.DeselectedItems.Count > 0)
			{
				foreach (MultiSelectClientStateDataItem multiSelectClientStateDataItem2 in clientState.DeselectedItems)
				{
					MultiSelectItem args2 = new MultiSelectItem
					{
						Text = multiSelectClientStateDataItem2.Text,
						Value = multiSelectClientStateDataItem2.Value,
						DataItem = multiSelectClientStateDataItem2.DataItem
					};
					this.RaiseItemDeselectedEvent(args2);
				}
			}
			if (clientState.SelectedItems.Count > 0 || clientState.DeselectedItems.Count > 0)
			{
				this.RaiseSelectionChangedEvent();
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000B8CB4 File Offset: 0x000B6EB4
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x000B8CD5 File Offset: 0x000B6ED5
		[DefaultValue(false)]
		public bool Animation
		{
			get
			{
				return (bool)(this.ViewState["Animation"] ?? false);
			}
			set
			{
				this.ViewState["Animation"] = value;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000B8CED File Offset: 0x000B6EED
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Animation AnimationSettings
		{
			get
			{
				if (this._animation == null)
				{
					this._animation = new Animation();
				}
				return this._animation;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x000B8D08 File Offset: 0x000B6F08
		// (set) Token: 0x060037F4 RID: 14324 RVA: 0x000B8D29 File Offset: 0x000B6F29
		[DefaultValue(true)]
		public bool AutoClose
		{
			get
			{
				return (bool)(this.ViewState["AutoClose"] ?? true);
			}
			set
			{
				this.ViewState["AutoClose"] = value;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x000B8D41 File Offset: 0x000B6F41
		// (set) Token: 0x060037F6 RID: 14326 RVA: 0x000B8D62 File Offset: 0x000B6F62
		[DefaultValue(true)]
		public bool AutoBind
		{
			get
			{
				return (bool)(this.ViewState["AutoBind"] ?? true);
			}
			set
			{
				this.ViewState["AutoBind"] = value;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060037F7 RID: 14327 RVA: 0x000B8D7A File Offset: 0x000B6F7A
		// (set) Token: 0x060037F8 RID: 14328 RVA: 0x000B8D9B File Offset: 0x000B6F9B
		[DefaultValue(false)]
		public bool AutoWidth
		{
			get
			{
				return (bool)(this.ViewState["AutoWidth"] ?? false);
			}
			set
			{
				this.ViewState["AutoWidth"] = value;
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000B8DB3 File Offset: 0x000B6FB3
		// (set) Token: 0x060037FA RID: 14330 RVA: 0x000B8DD4 File Offset: 0x000B6FD4
		[DefaultValue(true)]
		public bool ClearButton
		{
			get
			{
				return (bool)(this.ViewState["ClearButton"] ?? true);
			}
			set
			{
				this.ViewState["ClearButton"] = value;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000B8DEC File Offset: 0x000B6FEC
		// (set) Token: 0x060037FC RID: 14332 RVA: 0x000B8E0C File Offset: 0x000B700C
		[DefaultValue("")]
		public string CascadeFrom
		{
			get
			{
				return (string)(this.ViewState["CascadeFrom"] ?? "");
			}
			set
			{
				this.ViewState["CascadeFrom"] = value;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000B8E1F File Offset: 0x000B701F
		// (set) Token: 0x060037FE RID: 14334 RVA: 0x000B8E3F File Offset: 0x000B703F
		[DefaultValue("")]
		public string CascadeFromField
		{
			get
			{
				return (string)(this.ViewState["CascadeFromField"] ?? "");
			}
			set
			{
				this.ViewState["CascadeFromField"] = value;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000B8E52 File Offset: 0x000B7052
		// (set) Token: 0x06003800 RID: 14336 RVA: 0x000B8E72 File Offset: 0x000B7072
		[DefaultValue("")]
		public string CascadeFromParentField
		{
			get
			{
				return (string)(this.ViewState["CascadeFromParentField"] ?? "");
			}
			set
			{
				this.ViewState["CascadeFromParentField"] = value;
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000B8E85 File Offset: 0x000B7085
		// (set) Token: 0x06003802 RID: 14338 RVA: 0x000B8EA6 File Offset: 0x000B70A6
		[DefaultValue(false)]
		[Category("Behavior")]
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

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000B8EBE File Offset: 0x000B70BE
		// (set) Token: 0x06003804 RID: 14340 RVA: 0x000B8EDE File Offset: 0x000B70DE
		[DefaultValue("")]
		public string DataTextField
		{
			get
			{
				return (string)(this.ViewState["DataTextField"] ?? "");
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x000B8EF1 File Offset: 0x000B70F1
		// (set) Token: 0x06003806 RID: 14342 RVA: 0x000B8F12 File Offset: 0x000B7112
		[DefaultValue("")]
		public string DataValueField
		{
			get
			{
				return (string)(this.ViewState["DataValueField"] ?? this.DataTextField);
			}
			set
			{
				this.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000B8F25 File Offset: 0x000B7125
		// (set) Token: 0x06003808 RID: 14344 RVA: 0x000B8F4E File Offset: 0x000B714E
		[DefaultValue(200.0)]
		public double Delay
		{
			get
			{
				return (double)(this.ViewState["Delay"] ?? 200.0);
			}
			set
			{
				this.ViewState["Delay"] = value;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000B8F66 File Offset: 0x000B7166
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x000B8F6E File Offset: 0x000B716E
		[DefaultValue(true)]
		[Obsolete("This property is becoming obsolete in favor the inherited WebControl.Enabled property. Therefore, please, use .Enabled instead.", false)]
		public bool Enable
		{
			get
			{
				return this.Enabled;
			}
			set
			{
				this.Enabled = value;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x000B8F77 File Offset: 0x000B7177
		internal bool GetEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000B8F7F File Offset: 0x000B717F
		// (set) Token: 0x0600380D RID: 14349 RVA: 0x000B8FA0 File Offset: 0x000B71A0
		[DefaultValue(false)]
		public bool EnforceMinLength
		{
			get
			{
				return (bool)(this.ViewState["EnforceMinLength"] ?? false);
			}
			set
			{
				this.ViewState["EnforceMinLength"] = value;
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000B8FB8 File Offset: 0x000B71B8
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x000B8FD9 File Offset: 0x000B71D9
		[DefaultValue(RadMultiSelectFilter.StartsWith)]
		[Bindable(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("filter")]
		public RadMultiSelectFilter Filter
		{
			get
			{
				return (RadMultiSelectFilter)(this.ViewState["Filter"] ?? RadMultiSelectFilter.StartsWith);
			}
			set
			{
				this.ViewState["Filter"] = value;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000B8FF1 File Offset: 0x000B71F1
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x000B9011 File Offset: 0x000B7211
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Bindable(true)]
		public string FixedGroupTemplate
		{
			get
			{
				return (string)(this.ViewState["FixedGroupTemplate"] ?? "");
			}
			set
			{
				this.ViewState["FixedGroupTemplate"] = value;
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x000B9024 File Offset: 0x000B7224
		// (set) Token: 0x06003813 RID: 14355 RVA: 0x000B9044 File Offset: 0x000B7244
		[Browsable(true)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(true)]
		public string FooterTemplate
		{
			get
			{
				return (string)(this.ViewState["FooterTemplate"] ?? "");
			}
			set
			{
				this.ViewState["FooterTemplate"] = value;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000B9057 File Offset: 0x000B7257
		// (set) Token: 0x06003815 RID: 14357 RVA: 0x000B9077 File Offset: 0x000B7277
		[DefaultValue("")]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(true)]
		public string GroupTemplate
		{
			get
			{
				return (string)(this.ViewState["GroupTemplate"] ?? "");
			}
			set
			{
				this.ViewState["GroupTemplate"] = value;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000B908A File Offset: 0x000B728A
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x000B90BE File Offset: 0x000B72BE
		[DefaultValue("200px")]
		[Category("Appearance")]
		[Description("The height of the suggestion popup in pixels. The default value is 200 pixels.")]
		[ClientControlProperty]
		[ClientPropertyName("height")]
		public Unit DropDownHeight
		{
			get
			{
				if (this.ViewState["DropDownHeight"] != null)
				{
					return (Unit)this.ViewState["DropDownHeight"];
				}
				return Unit.Pixel(200);
			}
			set
			{
				this.ViewState["DropDownHeight"] = value;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000B90D6 File Offset: 0x000B72D6
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x000B90F7 File Offset: 0x000B72F7
		[DefaultValue(true)]
		public bool HighlightFirst
		{
			get
			{
				return (bool)(this.ViewState["HighlightFirst"] ?? true);
			}
			set
			{
				this.ViewState["HighlightFirst"] = value;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000B910F File Offset: 0x000B730F
		// (set) Token: 0x0600381B RID: 14363 RVA: 0x000B9130 File Offset: 0x000B7330
		[DefaultValue(true)]
		public bool IgnoreCase
		{
			get
			{
				return (bool)(this.ViewState["IgnoreCase"] ?? true);
			}
			set
			{
				this.ViewState["IgnoreCase"] = value;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000B9148 File Offset: 0x000B7348
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Messages MessagesSettings
		{
			get
			{
				if (this._messages == null)
				{
					this._messages = new Messages();
				}
				return this._messages;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000B9163 File Offset: 0x000B7363
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x000B918C File Offset: 0x000B738C
		[DefaultValue(1.0)]
		public double MinLength
		{
			get
			{
				return (double)(this.ViewState["MinLength"] ?? 1.0);
			}
			set
			{
				this.ViewState["MinLength"] = value;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000B91A4 File Offset: 0x000B73A4
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x000B91CD File Offset: 0x000B73CD
		[DefaultValue(0.0)]
		public double MaxSelectedItems
		{
			get
			{
				return (double)(this.ViewState["MaxSelectedItems"] ?? 0.0);
			}
			set
			{
				this.ViewState["MaxSelectedItems"] = value;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x000B91E5 File Offset: 0x000B73E5
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x000B9205 File Offset: 0x000B7405
		[Browsable(true)]
		[DefaultValue("There is no data.")]
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string NoDataTemplate
		{
			get
			{
				return (string)(this.ViewState["NoDataTemplate"] ?? "There is no data.");
			}
			set
			{
				this.ViewState["NoDataTemplate"] = value;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x000B9218 File Offset: 0x000B7418
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x000B9238 File Offset: 0x000B7438
		[DefaultValue("")]
		public string Placeholder
		{
			get
			{
				return (string)(this.ViewState["Placeholder"] ?? "");
			}
			set
			{
				this.ViewState["Placeholder"] = value;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x000B924B File Offset: 0x000B744B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Popup PopupSettings
		{
			get
			{
				if (this._popup == null)
				{
					this._popup = new Popup();
				}
				return this._popup;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x000B9266 File Offset: 0x000B7466
		// (set) Token: 0x06003827 RID: 14375 RVA: 0x000B9286 File Offset: 0x000B7486
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DefaultValue("")]
		public string HeaderTemplate
		{
			get
			{
				return (string)(this.ViewState["HeaderTemplate"] ?? "");
			}
			set
			{
				this.ViewState["HeaderTemplate"] = value;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000B9299 File Offset: 0x000B7499
		// (set) Token: 0x06003829 RID: 14377 RVA: 0x000B92B9 File Offset: 0x000B74B9
		[Browsable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string ItemTemplate
		{
			get
			{
				return (string)(this.ViewState["ItemTemplate"] ?? "");
			}
			set
			{
				this.ViewState["ItemTemplate"] = value;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000B92CC File Offset: 0x000B74CC
		// (set) Token: 0x0600382B RID: 14379 RVA: 0x000B92EC File Offset: 0x000B74EC
		[Browsable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string TagTemplate
		{
			get
			{
				return (string)(this.ViewState["TagTemplate"] ?? "");
			}
			set
			{
				this.ViewState["TagTemplate"] = value;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x0600382C RID: 14380 RVA: 0x000B92FF File Offset: 0x000B74FF
		// (set) Token: 0x0600382D RID: 14381 RVA: 0x000B9320 File Offset: 0x000B7520
		[Category("Behavior")]
		[Bindable(false)]
		[DefaultValue(RadMultiSelectTagMode.Multiple)]
		[ClientControlProperty]
		[ClientPropertyName("tagMode")]
		public RadMultiSelectTagMode TagMode
		{
			get
			{
				return (RadMultiSelectTagMode)(this.ViewState["TagMode"] ?? RadMultiSelectTagMode.Multiple);
			}
			set
			{
				this.ViewState["TagMode"] = value;
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x000B9338 File Offset: 0x000B7538
		// (set) Token: 0x0600382F RID: 14383 RVA: 0x000B9359 File Offset: 0x000B7559
		[DefaultValue(false)]
		public bool ValuePrimitive
		{
			get
			{
				return (bool)(this.ViewState["ValuePrimitive"] ?? false);
			}
			set
			{
				this.ViewState["ValuePrimitive"] = value;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003830 RID: 14384 RVA: 0x000B9371 File Offset: 0x000B7571
		// (set) Token: 0x06003831 RID: 14385 RVA: 0x000B9392 File Offset: 0x000B7592
		[DefaultValue(false)]
		public bool Virtual
		{
			get
			{
				return (bool)(this.ViewState["Virtual"] ?? false);
			}
			set
			{
				this.ViewState["Virtual"] = value;
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000B93AA File Offset: 0x000B75AA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Virtual VirtualSettings
		{
			get
			{
				if (this._virtual == null)
				{
					this._virtual = new Virtual();
				}
				return this._virtual;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003833 RID: 14387 RVA: 0x000B93C5 File Offset: 0x000B75C5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MultiSelectClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new MultiSelectClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000B93E0 File Offset: 0x000B75E0
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The items of the dropdownlist")]
		public virtual MultiSelectItemCollection Items
		{
			get
			{
				if (this._itemsCollection == null)
				{
					this._itemsCollection = new MultiSelectItemCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._itemsCollection).TrackViewState();
					}
				}
				return this._itemsCollection;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003835 RID: 14389 RVA: 0x000B940F File Offset: 0x000B760F
		// (set) Token: 0x06003836 RID: 14390 RVA: 0x000B9426 File Offset: 0x000B7626
		[Description("The value of the MultiSelect Selected Items")]
		[Bindable(true, BindingDirection.TwoWay)]
		[Browsable(false)]
		[Category("Setup")]
		[DefaultValue("")]
		public IEnumerable<object> Value
		{
			get
			{
				return this.ViewState["Value"] as IEnumerable<object>;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x000B9439 File Offset: 0x000B7639
		[ClientControlProperty]
		[ClientPropertyName("_uniqueId")]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000B9441 File Offset: 0x000B7641
		// Note: this type is marked as 'beforefieldinit'.
		static RadMultiSelect()
		{
			RadMultiSelect.ItemSelectedEvent = new object();
			RadMultiSelect.ItemDeselectedEvent = new object();
			RadMultiSelect.SelectionChangedEvent = new object();
			RadMultiSelect.EventChildrenCreated = new object();
		}

		// Token: 0x04000EEB RID: 3819
		private static readonly object MultiSelectItemDataBoundEvent = new object();

		// Token: 0x04000EEF RID: 3823
		private WebServiceClientDataSource _WebServiceClientDataSource;

		// Token: 0x04000EF0 RID: 3824
		private static readonly object EventChildrenCreated;

		// Token: 0x04000EF1 RID: 3825
		protected StringBuilder dataBindData;

		// Token: 0x04000EF2 RID: 3826
		private MultiSelectItemCollection _itemsCollection;

		// Token: 0x04000EF3 RID: 3827
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();

		// Token: 0x04000EF4 RID: 3828
		private Animation _animation;

		// Token: 0x04000EF5 RID: 3829
		private Messages _messages;

		// Token: 0x04000EF6 RID: 3830
		private Popup _popup;

		// Token: 0x04000EF7 RID: 3831
		private Virtual _virtual;

		// Token: 0x04000EF8 RID: 3832
		private MultiSelectClientEvents _clientEvents;
	}
}

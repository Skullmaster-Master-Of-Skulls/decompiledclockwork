using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.Timeline;

namespace Telerik.Web.UI
{
	// Token: 0x02000921 RID: 2337
	[RequiredScript(typeof(Html5Timeline))]
	[ToolboxData("<{0}:RadTimeline Runat=\"server\"></{0}:RadTimeline>")]
	[DefaultProperty("Items")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[Designer("Telerik.Web.Design.RadTimelineDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ClientScriptResource("Telerik.Web.UI.RadTimeline", "Telerik.Web.UI.Timeline.Scripts.RadTimeline.js")]
	[EmbeddedSkin("Timeline", typeof(RadTimeline))]
	[EmbeddedSkin("Timeline", "Default", typeof(RadTimeline))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadTimeline))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadTimeline))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadTimeline))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadTimeline))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadTimeline))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadTimeline))]
	[ParseChildren(ChildrenAsProperties = true)]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadTimeline), "Telerik.Web.UI.Timeline.png")]
	public class RadTimeline : RadDataBoundControl, INamingContainer, IItemContainer
	{
		// Token: 0x0600587B RID: 22651 RVA: 0x0010E16D File Offset: 0x0010C36D
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0010E18C File Offset: 0x0010C38C
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.ClientEvents.OnDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "expand", this.ClientEvents.OnExpand);
			RadDataBoundControl.DescribeEvent(descriptor, "collapse", this.ClientEvents.OnCollapse);
			RadDataBoundControl.DescribeEvent(descriptor, "actionClick", this.ClientEvents.OnActionClick);
			RadDataBoundControl.DescribeEvent(descriptor, "change", this.ClientEvents.OnChange);
			RadDataBoundControl.DescribeEvent(descriptor, "navigate", this.ClientEvents.OnNavigate);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x0600587D RID: 22653 RVA: 0x0010E250 File Offset: 0x0010C450
		// (set) Token: 0x0600587E RID: 22654 RVA: 0x0010E271 File Offset: 0x0010C471
		[DefaultValue(false)]
		[Bindable(false)]
		[Category("Behavior")]
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

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x0010E289 File Offset: 0x0010C489
		[Description("The items of the timeline")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual TimelineItemCollection Items
		{
			get
			{
				if (this._itemsCollection == null)
				{
					this._itemsCollection = new TimelineItemCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._itemsCollection).TrackViewState();
					}
				}
				return this._itemsCollection;
			}
		}

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06005880 RID: 22656 RVA: 0x0010E2B8 File Offset: 0x0010C4B8
		// (remove) Token: 0x06005881 RID: 22657 RVA: 0x0010E2CB File Offset: 0x0010C4CB
		public event RadTimelineItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadTimeline.TimelineItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTimeline.TimelineItemDataBoundEvent, value);
			}
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x0010E2E0 File Offset: 0x0010C4E0
		private void RaiseEvent(object eventKey, RadTimelineItemEventArgs e)
		{
			RadTimelineItemEventHandler radTimelineItemEventHandler = (RadTimelineItemEventHandler)base.Events[eventKey];
			if (radTimelineItemEventHandler != null)
			{
				radTimelineItemEventHandler(this, e);
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06005883 RID: 22659 RVA: 0x0010E30A File Offset: 0x0010C50A
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the RadClientDataSource instance.")]
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

		// Token: 0x06005884 RID: 22660 RVA: 0x0010E325 File Offset: 0x0010C525
		protected override void CreateChildControls()
		{
			this.WebServiceClientDataSource.ID = "WebServiceCDS";
			this.Controls.Add(this.WebServiceClientDataSource);
			this.OnChildrenCreated();
			base.CreateChildControls();
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x0010E354 File Offset: 0x0010C554
		protected virtual void OnChildrenCreated()
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadTimeline.EventChildrenCreated];
			if (eventHandler != null)
			{
				eventHandler(this, new EventArgs());
			}
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x0010E386 File Offset: 0x0010C586
		public RadTimeline()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06005887 RID: 22663 RVA: 0x0010E39F File Offset: 0x0010C59F
		// (set) Token: 0x06005888 RID: 22664 RVA: 0x0010E3B6 File Offset: 0x0010C5B6
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

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06005889 RID: 22665 RVA: 0x0010E3C9 File Offset: 0x0010C5C9
		// (set) Token: 0x0600588A RID: 22666 RVA: 0x0010E3EA File Offset: 0x0010C5EA
		[DefaultValue(false)]
		public bool Navigatable
		{
			get
			{
				return (bool)(this.ViewState["Navigatable"] ?? false);
			}
			set
			{
				this.ViewState["Navigatable"] = value;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x0010E402 File Offset: 0x0010C602
		// (set) Token: 0x0600588C RID: 22668 RVA: 0x0010E423 File Offset: 0x0010C623
		[DefaultValue(false)]
		public bool AlternatingMode
		{
			get
			{
				return (bool)(this.ViewState["AlternatingMode"] ?? false);
			}
			set
			{
				this.ViewState["AlternatingMode"] = value;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x0600588D RID: 22669 RVA: 0x0010E43B File Offset: 0x0010C63B
		// (set) Token: 0x0600588E RID: 22670 RVA: 0x0010E45C File Offset: 0x0010C65C
		[DefaultValue(RadTimelineOrientation.Vertical)]
		[Category("Behavior")]
		[ClientPropertyName("orientation")]
		[ClientControlProperty]
		[Bindable(false)]
		public RadTimelineOrientation Orientation
		{
			get
			{
				return (RadTimelineOrientation)(this.ViewState["Orientation"] ?? RadTimelineOrientation.Vertical);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x0600588F RID: 22671 RVA: 0x0010E474 File Offset: 0x0010C674
		// (set) Token: 0x06005890 RID: 22672 RVA: 0x0010E495 File Offset: 0x0010C695
		[DefaultValue(false)]
		public bool CollapsibleEvents
		{
			get
			{
				return (bool)(this.ViewState["CollapsibleEvents"] ?? false);
			}
			set
			{
				this.ViewState["CollapsibleEvents"] = value;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x0010E4AD File Offset: 0x0010C6AD
		// (set) Token: 0x06005892 RID: 22674 RVA: 0x0010E4CD File Offset: 0x0010C6CD
		[DefaultValue("")]
		public string DataActionsField
		{
			get
			{
				return (string)(this.ViewState["DataActionsField"] ?? "");
			}
			set
			{
				this.ViewState["DataActionsField"] = value;
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x06005893 RID: 22675 RVA: 0x0010E4E0 File Offset: 0x0010C6E0
		// (set) Token: 0x06005894 RID: 22676 RVA: 0x0010E500 File Offset: 0x0010C700
		[DefaultValue("")]
		public string DataDescriptionField
		{
			get
			{
				return (string)(this.ViewState["DataDescriptionField"] ?? "");
			}
			set
			{
				this.ViewState["DataDescriptionField"] = value;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x0010E513 File Offset: 0x0010C713
		// (set) Token: 0x06005896 RID: 22678 RVA: 0x0010E533 File Offset: 0x0010C733
		[DefaultValue("")]
		public string DataDateField
		{
			get
			{
				return (string)(this.ViewState["DataDateField"] ?? "");
			}
			set
			{
				this.ViewState["DataDateField"] = value;
			}
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06005897 RID: 22679 RVA: 0x0010E546 File Offset: 0x0010C746
		// (set) Token: 0x06005898 RID: 22680 RVA: 0x0010E566 File Offset: 0x0010C766
		[DefaultValue("")]
		public string DataImagesField
		{
			get
			{
				return (string)(this.ViewState["DataImagesField"] ?? "");
			}
			set
			{
				this.ViewState["DataImagesField"] = value;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x06005899 RID: 22681 RVA: 0x0010E579 File Offset: 0x0010C779
		// (set) Token: 0x0600589A RID: 22682 RVA: 0x0010E599 File Offset: 0x0010C799
		[DefaultValue("")]
		public string DataSubtitleField
		{
			get
			{
				return (string)(this.ViewState["DataSubtitleField"] ?? "");
			}
			set
			{
				this.ViewState["DataSubtitleField"] = value;
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x0600589B RID: 22683 RVA: 0x0010E5AC File Offset: 0x0010C7AC
		// (set) Token: 0x0600589C RID: 22684 RVA: 0x0010E5CC File Offset: 0x0010C7CC
		[DefaultValue("")]
		public string DataTitleField
		{
			get
			{
				return (string)(this.ViewState["DataTitleField"] ?? "");
			}
			set
			{
				this.ViewState["DataTitleField"] = value;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x0600589D RID: 22685 RVA: 0x0010E5DF File Offset: 0x0010C7DF
		// (set) Token: 0x0600589E RID: 22686 RVA: 0x0010E5FF File Offset: 0x0010C7FF
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(true)]
		[Browsable(true)]
		[DefaultValue("")]
		public string EventTemplate
		{
			get
			{
				return (string)(this.ViewState["EventTemplate"] ?? "");
			}
			set
			{
				this.ViewState["EventTemplate"] = value;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x0010E612 File Offset: 0x0010C812
		// (set) Token: 0x060058A0 RID: 22688 RVA: 0x0010E632 File Offset: 0x0010C832
		[DefaultValue("MMM d, yyyy")]
		public string DateFormat
		{
			get
			{
				return (string)(this.ViewState["DateFormat"] ?? "MMM d, yyyy");
			}
			set
			{
				this.ViewState["DateFormat"] = value;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x0010E645 File Offset: 0x0010C845
		// (set) Token: 0x060058A2 RID: 22690 RVA: 0x0010E66E File Offset: 0x0010C86E
		[DefaultValue(600.0)]
		public double EventHeight
		{
			get
			{
				return (double)(this.ViewState["EventHeight"] ?? 600.0);
			}
			set
			{
				this.ViewState["EventHeight"] = value;
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x0010E686 File Offset: 0x0010C886
		// (set) Token: 0x060058A4 RID: 22692 RVA: 0x0010E6AF File Offset: 0x0010C8AF
		[DefaultValue(400.0)]
		public double EventWidth
		{
			get
			{
				return (double)(this.ViewState["EventWidth"] ?? 400.0);
			}
			set
			{
				this.ViewState["EventWidth"] = value;
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x060058A5 RID: 22693 RVA: 0x0010E6C7 File Offset: 0x0010C8C7
		// (set) Token: 0x060058A6 RID: 22694 RVA: 0x0010E6E8 File Offset: 0x0010C8E8
		[DefaultValue(true)]
		public bool ShowDateLabels
		{
			get
			{
				return (bool)(this.ViewState["ShowDateLabels"] ?? true);
			}
			set
			{
				this.ViewState["ShowDateLabels"] = value;
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x060058A7 RID: 22695 RVA: 0x0010E700 File Offset: 0x0010C900
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TimelineClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new TimelineClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x0010E71B File Offset: 0x0010C91B
		protected virtual void BindItems(IEnumerable<TimelineItem> items)
		{
			this.Items.Clear();
			this.Items.AddRange(items);
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x0010E73C File Offset: 0x0010C93C
		protected virtual void DescribeItems(IScriptDescriptor descriptor)
		{
			List<TimelineItem> list = (from x in this.GetAllItems()
			where x.Visible
			select x).ToList<TimelineItem>();
			if (this.SerializedDataSource != null)
			{
				descriptor.AddScriptProperty("itemsData", this.SerializedDataSource);
				return;
			}
			if (list.Count > 0)
			{
				descriptor.AddScriptProperty("itemsData", this.serializer.Serialize(list));
				return;
			}
			descriptor.AddScriptProperty("itemsData", "[]");
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x0010E7C2 File Offset: 0x0010C9C2
		// (set) Token: 0x060058AB RID: 22699 RVA: 0x0010E7E2 File Offset: 0x0010C9E2
		[Category("Behavior")]
		[DefaultValue("")]
		[Themeable(false)]
		[UrlProperty("*.aspx")]
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

		// Token: 0x060058AC RID: 22700 RVA: 0x0010E7F8 File Offset: 0x0010C9F8
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string validationGroup, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null && !string.IsNullOrEmpty(postBackUrl))
			{
				postBackOptions.ActionUrl = postBackUrl;
			}
			return postBackOptions;
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x0010E830 File Offset: 0x0010CA30
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				text = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null && !string.IsNullOrEmpty(text))
			{
				postBackOptions.ActionUrl = text;
			}
			return postBackOptions;
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x0010E88C File Offset: 0x0010CA8C
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x0010E8C8 File Offset: 0x0010CAC8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			this.DescribeItems(descriptor);
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("clientDataSourceID", this.ClientDataSourceID);
				}
			}
			descriptor.AddProperty("_webServiceClientDataSource", this.WebServiceClientDataSource.ClientID);
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0010E98C File Offset: 0x0010CB8C
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

		// Token: 0x060058B1 RID: 22705 RVA: 0x0010E9BE File Offset: 0x0010CBBE
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x060058B2 RID: 22706 RVA: 0x0010E9D0 File Offset: 0x0010CBD0
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
				base.Render(writer);
			}
			writer.Write(string.Format("<span id='{0}' style='display:none'></span>", this.WebServiceClientDataSource.ClientID));
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x060058B3 RID: 22707 RVA: 0x0010EA3C File Offset: 0x0010CC3C
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadTimelineConverter(),
				new AttributeCollectionConverter(),
				new TimelineItemConverter(),
				new TimelineItemActionConverter(),
				new TimelineItemImageConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x0010EA94 File Offset: 0x0010CC94
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x0010EA98 File Offset: 0x0010CC98
		protected override string CssClassFormatString
		{
			get
			{
				return "RadTimeline RadTimeline_{0}";
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0010EA9F File Offset: 0x0010CC9F
		// (set) Token: 0x060058B7 RID: 22711 RVA: 0x0010EAC0 File Offset: 0x0010CCC0
		[Description("Comma delimited list of data-field Names")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(ListConverter))]
		[Category("Data")]
		[PersistenceMode(PersistenceMode.Attribute)]
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

		// Token: 0x060058B8 RID: 22712 RVA: 0x0010EAD3 File Offset: 0x0010CCD3
		IItem IItemContainer.CreateItem()
		{
			return new TimelineItem(this);
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x0010EAE4 File Offset: 0x0010CCE4
		void IItemContainer.RaiseItemDataBound(IItem item)
		{
			RadTimelineItemEventHandler radTimelineItemEventHandler = (RadTimelineItemEventHandler)base.Events[RadTimeline.TimelineItemDataBoundEvent];
			TimelineItem timelineItem = item as TimelineItem;
			timelineItem.TemplateData = new Dictionary<string, object>();
			List<string> source = (from x in this.DataKeyNames
			select x.Trim()).ToList<string>();
			IEnumerable<string> enumerable = source.Distinct<string>();
			foreach (string text in enumerable)
			{
				try
				{
					object value = DataBinder.Eval(timelineItem.DataItem, text);
					timelineItem.TemplateData.Add(text, value);
				}
				catch (Exception)
				{
					throw new Exception("The data item does not contain the " + text + " data field");
				}
			}
			RadTimelineItemEventArgs e = new RadTimelineItemEventArgs(timelineItem);
			if (radTimelineItemEventHandler != null)
			{
				radTimelineItemEventHandler(this, e);
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x0010EBE0 File Offset: 0x0010CDE0
		IList IItemContainer.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x0010EBE8 File Offset: 0x0010CDE8
		public IList<TimelineItem> GetAllItems()
		{
			return this.Items.ToList();
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x0010EBF5 File Offset: 0x0010CDF5
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return false;
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x0010EBF8 File Offset: 0x0010CDF8
		private void LoadClientState(RadTimelineClientState clientState)
		{
		}

		// Token: 0x04001594 RID: 5524
		private static readonly object TimelineItemDataBoundEvent = new object();

		// Token: 0x04001595 RID: 5525
		private WebServiceClientDataSource _WebServiceClientDataSource;

		// Token: 0x04001596 RID: 5526
		private static readonly object EventChildrenCreated = new object();

		// Token: 0x04001597 RID: 5527
		private TimelineItemCollection _itemsCollection;

		// Token: 0x04001598 RID: 5528
		private TimelineClientEvents _clientEvents;

		// Token: 0x04001599 RID: 5529
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();
	}
}

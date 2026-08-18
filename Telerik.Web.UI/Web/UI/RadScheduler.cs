using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Analytics;
using Telerik.Web.UI.Scheduler;
using Telerik.Web.UI.Scheduler.OData;
using Telerik.Web.UI.Scheduler.TimeZones;
using Telerik.Web.UI.Scheduler.Views;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020007F1 RID: 2033
	[EmbeddedSkin("Scheduler", "Default", typeof(RadScheduler))]
	[RequiredScript(typeof(TouchScrollExtender))]
	[RequiredScript(typeof(ModalExtender))]
	[LightweightRendering]
	[AdaptiveRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadScheduler))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadScheduler))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadScheduler))]
	[TelerikToolboxCategory("Calendar, Scheduler and Gantt")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Mobile, typeof(RadButton))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Scheduler", typeof(RadScheduler))]
	[ToolboxBitmap(typeof(RadScheduler), "Telerik.Web.UI.Scheduler.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(SchedulerDateTime))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadScheduler", "Telerik.Web.UI.Scheduler.RadSchedulerScripts.js")]
	[Designer("Telerik.Web.Design.RadSchedulerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadScheduler : RadDataBoundControl, IPostBackEventHandler, INamingContainer, ILocalizableControl, IScheduler, IAppointmentFactory, ISchedulerData, ICallbackEventHandler, ICallbackCommandContext
	{
		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x000DD99C File Offset: 0x000DBB9C
		internal string AdjustedRowHeight
		{
			get
			{
				if (this._adjustedRowHeight == string.Empty)
				{
					this._adjustedRowHeight = this.RowHeight.ToString();
					if (this.RowHeight.Type == UnitType.Pixel)
					{
						this._adjustedRowHeight = Unit.Pixel(Convert.ToInt32(this.RowHeight.Value) - 1).ToString();
					}
				}
				return this._adjustedRowHeight;
			}
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000DDA1A File Offset: 0x000DBC1A
		internal WebControl CreateSpacer()
		{
			return RadScheduler.CreateSpacer(this);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000DDA22 File Offset: 0x000DBC22
		internal WebControl CreateWrapper()
		{
			return RadScheduler.CreateWrapper(this);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000DDA2C File Offset: 0x000DBC2C
		protected static WebControl CreateSpacer(RadScheduler scheduler)
		{
			WebControl webControl = RadScheduler.CreateWrapper(scheduler);
			webControl.Controls.Add(new LiteralControl("<!-- -->"));
			webControl.EnableViewState = false;
			return webControl;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000DDA60 File Offset: 0x000DBC60
		protected static WebControl CreateWrapper(RadScheduler scheduler)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsWrap";
			webControl.Style[HtmlTextWriterStyle.Height] = scheduler.AdjustedRowHeight;
			return webControl;
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x060046B1 RID: 18097 RVA: 0x000DDA94 File Offset: 0x000DBC94
		// (set) Token: 0x060046B2 RID: 18098 RVA: 0x000DDA9C File Offset: 0x000DBC9C
		string IScheduler.ActiveSlotIndex
		{
			get
			{
				return this.ActiveSlotIndex;
			}
			set
			{
				this.ActiveSlotIndex = value;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x000DDAA5 File Offset: 0x000DBCA5
		bool IScheduler.EnableAdvancedForm
		{
			get
			{
				return this.AdvancedForm.Enabled;
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x000DDAB2 File Offset: 0x000DBCB2
		protected internal bool HasDataSource
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataSourceID) || this.DataSource != null;
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x000DDACF File Offset: 0x000DBCCF
		protected internal bool HasCustomProvider
		{
			get
			{
				return !(this.Provider is DataSourceViewSchedulerProvider);
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x000DDAE2 File Offset: 0x000DBCE2
		protected internal bool HasDescriptionField
		{
			get
			{
				return (!this.HasCustomProvider && !string.IsNullOrEmpty(this.DataDescriptionField)) || (this.HasCustomProvider && this.EnableDescriptionField) || (this.UsingWebServiceBinding && this.EnableDescriptionField);
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060046B7 RID: 18103 RVA: 0x000DDB1B File Offset: 0x000DBD1B
		protected internal bool InAdvancedMode
		{
			get
			{
				return this.ActiveFormMode == SchedulerFormMode.AdvancedEdit || this.ActiveFormMode == SchedulerFormMode.AdvancedInsert;
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x000DDB34 File Offset: 0x000DBD34
		protected internal DateTime VisualToday
		{
			get
			{
				TimeSpan visualTimeZoneOffset = this.VisualTimeZoneOffset;
				DateTime utcDate = DateTime.UtcNow.Add(visualTimeZoneOffset);
				return this.UtcToDisplay(this.UtcDayStart(utcDate));
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060046B9 RID: 18105 RVA: 0x000DDB64 File Offset: 0x000DBD64
		protected override string CssClassFormatString
		{
			get
			{
				return "RadScheduler RadScheduler_{0}";
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060046BA RID: 18106 RVA: 0x000DDB6B File Offset: 0x000DBD6B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060046BB RID: 18107 RVA: 0x000DDB6F File Offset: 0x000DBD6F
		internal new bool DesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060046BC RID: 18108 RVA: 0x000DDB77 File Offset: 0x000DBD77
		// (set) Token: 0x060046BD RID: 18109 RVA: 0x000DDB7F File Offset: 0x000DBD7F
		internal SchedulerFormMode ActiveFormMode
		{
			get
			{
				return this._activeFormMode;
			}
			set
			{
				this._activeFormMode = value;
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x060046BE RID: 18110 RVA: 0x000DDB88 File Offset: 0x000DBD88
		// (set) Token: 0x060046BF RID: 18111 RVA: 0x000DDB90 File Offset: 0x000DBD90
		internal string ActiveSlotIndex { get; set; }

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x060046C0 RID: 18112 RVA: 0x000DDB99 File Offset: 0x000DBD99
		// (set) Token: 0x060046C1 RID: 18113 RVA: 0x000DDBA1 File Offset: 0x000DBDA1
		internal Appointment ActiveFormAppointment { get; set; }

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x000DDBAA File Offset: 0x000DBDAA
		// (set) Token: 0x060046C3 RID: 18115 RVA: 0x000DDBB2 File Offset: 0x000DBDB2
		internal SchedulerFormContainer FormContainer { get; set; }

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x000DDBBB File Offset: 0x000DBDBB
		// (set) Token: 0x060046C5 RID: 18117 RVA: 0x000DDBC3 File Offset: 0x000DBDC3
		internal AppointmentController AppointmentController
		{
			get
			{
				return this._appointmentController;
			}
			set
			{
				this._appointmentController = value;
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x000DDBCC File Offset: 0x000DBDCC
		internal DataSourceView DataSourceView
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x000DDBD4 File Offset: 0x000DBDD4
		// (set) Token: 0x060046C8 RID: 18120 RVA: 0x000DDBDC File Offset: 0x000DBDDC
		internal ISchedulerModel ActiveModel { get; set; }

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x000DDBE5 File Offset: 0x000DBDE5
		internal int WeekLength
		{
			get
			{
				return DateHelper.GetWeekLength(this.SelectedDate, this.FirstDayOfWeek, this.LastDayOfWeek);
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x000DDBFE File Offset: 0x000DBDFE
		// (set) Token: 0x060046CB RID: 18123 RVA: 0x000DDC06 File Offset: 0x000DBE06
		private bool UsingDefaultTimeSlotContextMenus { get; set; }

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x060046CC RID: 18124 RVA: 0x000DDC0F File Offset: 0x000DBE0F
		// (set) Token: 0x060046CD RID: 18125 RVA: 0x000DDC17 File Offset: 0x000DBE17
		private bool UsingDefaultAppointmentContextMenus { get; set; }

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x000DDC20 File Offset: 0x000DBE20
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_defaultAdvancedFormRendered")]
		internal bool DefaultAdvancedFormRendered
		{
			get
			{
				if (this.FormContainer != null)
				{
					AdvancedTemplate advancedTemplate = this.FormContainer.Template as AdvancedTemplate;
					return advancedTemplate != null;
				}
				return false;
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x000DDC4F File Offset: 0x000DBE4F
		internal override bool SupportsOData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x000DDC52 File Offset: 0x000DBE52
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x000DDC55 File Offset: 0x000DBE55
		internal bool UseDefaultAdvancedInsert
		{
			get
			{
				return this.UsingWebServiceBinding && this.AdvancedForm.Enabled && this.AdvancedInsertTemplate is AdvancedTemplate;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x000DDC7C File Offset: 0x000DBE7C
		internal bool UseDefaultAdvancedEdit
		{
			get
			{
				return this.UsingWebServiceBinding && this.AdvancedForm.Enabled && this.AdvancedEditTemplate is AdvancedTemplate;
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x000DDCA4 File Offset: 0x000DBEA4
		internal AppointmentCollection VisibleAppointments
		{
			get
			{
				AppointmentCollection appointmentCollection = new AppointmentCollection();
				foreach (Appointment appointment in this.Appointments)
				{
					if (appointment.Visible)
					{
						appointmentCollection.Add(appointment);
					}
				}
				return appointmentCollection;
			}
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x000DDD00 File Offset: 0x000DBF00
		internal string RuntimeSkinInternal
		{
			get
			{
				return base.RuntimeSkin;
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x000DDD08 File Offset: 0x000DBF08
		[ClientControlProperty]
		private string PostBackReference
		{
			get
			{
				return this.Page.ClientScript.GetPostBackEventReference(this, "arguments");
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x000DDD20 File Offset: 0x000DBF20
		// (set) Token: 0x060046D7 RID: 18135 RVA: 0x000DDD28 File Offset: 0x000DBF28
		private bool ShouldBindFormTemplate { get; set; }

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x000DDD31 File Offset: 0x000DBF31
		[DefaultValue(true)]
		[ClientControlProperty]
		private bool ShouldPostbackOnClick
		{
			get
			{
				return base.Events[RadScheduler.AppointmentClickEvent] != null;
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x000DDD49 File Offset: 0x000DBF49
		[ClientControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("_shouldPostbackOnReminderSnooze")]
		private bool ShouldPostbackOnReminderSnooze
		{
			get
			{
				return this.Reminders.Enabled && base.Events[RadScheduler.ReminderSnoozeEvent] != null;
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x000DDD70 File Offset: 0x000DBF70
		[DefaultValue(false)]
		[ClientControlProperty]
		private bool ShouldPostbackOnAppointmentContextMenuItemClick
		{
			get
			{
				return base.Events[RadScheduler.AppointmentContextMenuItemClickingEvent] != null || base.Events[RadScheduler.AppointmentContextMenuItemClickedEvent] != null;
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x000DDD9C File Offset: 0x000DBF9C
		[ClientControlProperty]
		[DefaultValue(false)]
		private bool ShouldPostbackOnTimeSlotContextMenuItemClick
		{
			get
			{
				return base.Events[RadScheduler.TimeSlotContextMenuItemClickingEvent] != null || base.Events[RadScheduler.TimeSlotContextMenuItemClickedEvent] != null;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x000DDDC8 File Offset: 0x000DBFC8
		[ClientControlProperty]
		[DefaultValue(true)]
		private bool ShouldUseClientInlineInsertForm
		{
			get
			{
				return !this.StartInsertingInAdvancedForm && this.ActiveFormMode != SchedulerFormMode.Insert && this.ActiveFormMode != SchedulerFormMode.Edit && base.Events[RadScheduler.FormCreatingEvent] == null && base.Events[RadScheduler.FormCreatedEvent] == null && this.InlineInsertTemplate is InlineInsertTemplate;
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x000DDE28 File Offset: 0x000DC028
		[ClientControlProperty]
		[DefaultValue(true)]
		private bool ShouldUseClientInlineEditForm
		{
			get
			{
				return !this.StartEditingInAdvancedForm && this.ActiveFormMode != SchedulerFormMode.Insert && this.ActiveFormMode != SchedulerFormMode.Edit && base.Events[RadScheduler.FormCreatingEvent] == null && base.Events[RadScheduler.FormCreatedEvent] == null && this.InlineEditTemplate is InlineEditTemplate;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x000DDE87 File Offset: 0x000DC087
		// (set) Token: 0x060046DF RID: 18143 RVA: 0x000DDE8F File Offset: 0x000DC08F
		[DefaultValue(0)]
		[ClientControlProperty]
		private int ScrollTop { get; set; }

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x000DDE98 File Offset: 0x000DC098
		// (set) Token: 0x060046E1 RID: 18145 RVA: 0x000DDEA0 File Offset: 0x000DC0A0
		[DefaultValue(0)]
		[ClientControlProperty]
		private int ScrollLeft { get; set; }

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x000DDEA9 File Offset: 0x000DC0A9
		// (set) Token: 0x060046E3 RID: 18147 RVA: 0x000DDEB1 File Offset: 0x000DC0B1
		private int CurrentSlotWidth { get; set; }

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x000DDEBA File Offset: 0x000DC0BA
		// (set) Token: 0x060046E5 RID: 18149 RVA: 0x000DDEC2 File Offset: 0x000DC0C2
		private int CurrentSlotHeight { get; set; }

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x000DDECB File Offset: 0x000DC0CB
		private bool UseControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x000DDED6 File Offset: 0x000DC0D6
		[ClientControlProperty]
		[ClientPropertyName("_useHorizontalScrolling")]
		[DefaultValue(false)]
		internal bool UseHorizontalScrolling
		{
			get
			{
				return this.ColumnWidth != Unit.Empty;
			}
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x000DDEE8 File Offset: 0x000DC0E8
		private ResourceCollection CreateSampleResources()
		{
			ResourceCollection resourceCollection = new ResourceCollection();
			string text = string.IsNullOrEmpty(this.GroupBy) ? "User" : this.GroupBy;
			Resource item = new Resource(text, 1, text + " 1");
			Resource item2 = new Resource(text, 2, text + " 2");
			resourceCollection.Add(item);
			resourceCollection.Add(item2);
			return resourceCollection;
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x000DDF55 File Offset: 0x000DC155
		internal bool UsingWebServiceBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.WebServiceSettings.Path) || this.UsingODataWebServiceBinding;
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x000DDF71 File Offset: 0x000DC171
		internal bool UsingODataWebServiceBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.ODataDataSourceID) || !string.IsNullOrEmpty(this.WebServiceSettings.ODataSettings.ODataDataSourceID);
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x000DDF9A File Offset: 0x000DC19A
		bool IScheduler.UsingWebServiceBinding
		{
			get
			{
				return this.UsingWebServiceBinding;
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x000DDFA2 File Offset: 0x000DC1A2
		private bool SupportsFullTime
		{
			get
			{
				return this.SelectedView != SchedulerViewType.TimelineView && this.SelectedView != SchedulerViewType.MonthView && this.SelectedView != SchedulerViewType.AgendaView && this.SelectedView != SchedulerViewType.YearView;
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x000DDFCD File Offset: 0x000DC1CD
		private bool CanSwitchToFullTime
		{
			get
			{
				return this.SupportsFullTime && !this.ShowFullTime;
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x000DDFE2 File Offset: 0x000DC1E2
		[Category("Export")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Export settings")]
		public SchedulerExportSettings ExportSettings
		{
			get
			{
				if (this._exportSettings == null)
				{
					this._exportSettings = new SchedulerExportSettings(this.ViewState);
				}
				return this._exportSettings;
			}
		}

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060046EF RID: 18159 RVA: 0x000DE003 File Offset: 0x000DC203
		// (remove) Token: 0x060046F0 RID: 18160 RVA: 0x000DE016 File Offset: 0x000DC216
		[Description("Fires before RadScheduler's HTML is transformed to PDF.")]
		public event SchedulerPdfExportingEventHandler PdfExporting
		{
			add
			{
				base.Events.AddHandler(RadScheduler.PdfExportingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.PdfExportingEvent, value);
			}
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000DE02C File Offset: 0x000DC22C
		protected override object SaveViewState()
		{
			this.EnsureChildControls();
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Appointments).SaveViewState(),
				((IStateManager)this.ResourceTypes).SaveViewState(),
				((IStateManager)this.Resources).SaveViewState(),
				((IStateManager)this.ResourceStyles).SaveViewState(),
				((IStateManager)this.AppointmentContextMenus).SaveViewState(),
				((IStateManager)this.TimeSlotContextMenus).SaveViewState()
			};
			if (!this.UseControlState)
			{
				arrayList.Add(this.GetControlState());
			}
			return arrayList.ToArray();
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x000DE0DC File Offset: 0x000DC2DC
		protected override void LoadViewState(object savedState)
		{
			this.UsingDefaultTimeSlotContextMenus = (this.TimeSlotContextMenus.Count == 0);
			this.UsingDefaultAppointmentContextMenus = (this.AppointmentContextMenus.Count == 0);
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			((IStateManager)this.Appointments).LoadViewState(array[1]);
			((IStateManager)this.ResourceTypes).LoadViewState(array[2]);
			((IStateManager)this.Resources).LoadViewState(array[3]);
			((IStateManager)this.ResourceStyles).LoadViewState(array[4]);
			((IStateManager)this.AppointmentContextMenus).LoadViewState(array[5]);
			((IStateManager)this.TimeSlotContextMenus).LoadViewState(array[6]);
			foreach (Appointment appointment in this.Appointments)
			{
				appointment.Owner = this;
			}
			if (!this.UseControlState)
			{
				this.LoadControlState(array[7]);
			}
			this.LoadTimeZoneProvider();
			this.CreateView();
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000DE1D4 File Offset: 0x000DC3D4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Appointments).TrackViewState();
			((IStateManager)this.ResourceTypes).TrackViewState();
			((IStateManager)this.Resources).TrackViewState();
			((IStateManager)this.ResourceStyles).TrackViewState();
			((IStateManager)this.AppointmentContextMenus).TrackViewState();
			((IStateManager)this.TimeSlotContextMenus).TrackViewState();
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x000DE229 File Offset: 0x000DC429
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.UseControlState)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x000DE248 File Offset: 0x000DC448
		protected override void LoadControlState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				this.ActiveFormMode = (SchedulerFormMode)array[0];
				this.ActiveFormAppointment = (Appointment)array[1];
				if (this.ActiveFormAppointment != null)
				{
					this.ActiveFormAppointment.Owner = this;
				}
				this.SelectedDate = (DateTime)array[2];
				this.SelectedView = (SchedulerViewType)array[3];
				this.ShowFullTime = (bool)array[4];
				this.ActiveSlotIndex = (string)array[5];
				this.CurrentSlotWidth = (int)array[6];
				this.CurrentSlotHeight = (int)array[7];
				this.GroupBy = (string)array[8];
				this.GroupingDirection = (GroupingDirection)array[9];
			}
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x000DE303 File Offset: 0x000DC503
		protected override object SaveControlState()
		{
			if (this.UseControlState)
			{
				return this.GetControlState();
			}
			return null;
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x000DE318 File Offset: 0x000DC518
		private object GetControlState()
		{
			return new object[]
			{
				this.ActiveFormMode,
				this.ActiveFormAppointment,
				this.SelectedDate,
				this.SelectedView,
				this.ShowFullTime,
				this.ActiveSlotIndex,
				this.CurrentSlotWidth,
				this.CurrentSlotHeight,
				this.GroupBy,
				this.GroupingDirection
			};
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x000DE3AC File Offset: 0x000DC5AC
		public RadScheduler()
		{
			this.LoadTimeZoneProvider();
			this.CreateViewSettings();
			this.LoadAppointmentProvider();
			this._appointmentController = new AppointmentController(this);
			this.CreateView();
			this.CreateWebServiceSettings();
			this.CreateContextMenuSettings();
			this.CreateReminderSettings();
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x000DE417 File Offset: 0x000DC617
		private void CreateContextMenuSettings()
		{
			this._appointmentContextMenuSettings = new ContextMenuSettings(this, "AppointmentContextMenu", this.ViewState);
			this._timeSlotContextMenuSettings = new ContextMenuSettings(this, "TimeSlotContextMenu", this.ViewState);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x000DE447 File Offset: 0x000DC647
		private void LoadTimeZoneProvider()
		{
			this._timeZoneProvider = TimeZoneProviderFactory.GetProvider(this, typeof(TimeZoneInfoProvider).Name);
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x000DE464 File Offset: 0x000DC664
		private void CreateViewSettings()
		{
			this._timelineViewSettings = new TimelineViewSettings(this, this.ViewState);
			this._weekViewSettings = new WeekViewSettings(this, this.ViewState);
			this._dayViewSettings = new DayViewSettings(this, this.ViewState);
			this._multiDayViewSettings = new MultiDayViewSettings(this, this.ViewState);
			this._monthViewSettings = new MonthViewSettings(this, this.ViewState);
			this._agendaViewSettings = new AgendaViewSettings(this, this.ViewState);
			this._yearViewSettings = new YearViewSettings(this, this.ViewState);
			this._advancedFormSettings = new AdvancedFormSettings(this, this.ViewState);
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x000DE501 File Offset: 0x000DC701
		private void CreateWebServiceSettings()
		{
			this._webServiceSettings = new SchedulerWebServiceSettings(this.ViewState);
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x000DE514 File Offset: 0x000DC714
		private void CreateReminderSettings()
		{
			this._reminderSettings = new ReminderSettings(this.ViewState);
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x000DE528 File Offset: 0x000DC728
		protected internal override RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			RenderMode renderMode = base.PreferredRenderMode(browser);
			if (renderMode == RenderMode.Mobile && browser.IsBrowser("IE"))
			{
				return RenderMode.Classic;
			}
			return renderMode;
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x000DE551 File Offset: 0x000DC751
		void IScheduler.HandleResize(Appointment appointmentToResize, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			this.ResizeAppointment(appointmentToResize, appointmentStart, appointmentEnd, editSeries);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000DE560 File Offset: 0x000DC760
		internal void ResizeAppointment(Appointment appointmentToResize, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			Appointment appointment = this._appointmentController.PrepareToEdit(appointmentToResize, editSeries);
			Appointment appointment2 = appointment.Clone();
			if (Math.Truncate((appointmentToResize.Start - appointmentStart).TotalMinutes) == 0.0)
			{
				TimeSpan t = appointmentEnd - appointmentToResize.Start;
				appointment2.End = appointment2.Start + t;
			}
			else
			{
				TimeSpan t = appointmentToResize.End - appointmentStart;
				appointment2.Start = appointment2.End - t;
			}
			this.FinishResize(appointment, appointment2);
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000DE5F0 File Offset: 0x000DC7F0
		void IScheduler.HandleMove(Appointment appointmentToMove, DateTime start, DateTime end, bool editSeries, ResourceUpdateInfo resourceUpdateInfo)
		{
			Appointment appointment = this._appointmentController.PrepareToEdit(appointmentToMove, editSeries);
			TimeSpan t = start - appointmentToMove.Start;
			DateTime dateTime = appointment.Start + t;
			TimeSpan t2 = end - start;
			DateTime end2 = dateTime + t2;
			Appointment appointment2 = appointment.Clone();
			appointment2.Start = dateTime;
			appointment2.End = end2;
			if (resourceUpdateInfo != null)
			{
				int index = appointment2.Resources.IndexOf(resourceUpdateInfo.OldResource);
				appointment2.Resources[index] = resourceUpdateInfo.NewResource;
			}
			this.FinishResize(appointment, appointment2);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x000DE688 File Offset: 0x000DC888
		internal Unit GetDefaultRowHeight()
		{
			string runtimeSkin;
			if ((runtimeSkin = base.RuntimeSkin) != null)
			{
				if (runtimeSkin == "BlackMetroTouch" || runtimeSkin == "MetroTouch")
				{
					return Unit.Pixel(32);
				}
				if (runtimeSkin == "Glow" || runtimeSkin == "Silk")
				{
					return Unit.Pixel(25);
				}
				if (runtimeSkin == "Bootstrap")
				{
					return Unit.Pixel(30);
				}
			}
			return Unit.Pixel(25);
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x000DE704 File Offset: 0x000DC904
		internal void InsertAppointmentInline()
		{
			IOrderedDictionary orderedDictionary = this.FormContainer.Template.ExtractValues(this.FormContainer);
			orderedDictionary = this.TranslateDataKeysFromTemplate(orderedDictionary);
			Appointment appointment = this.FormContainer.Appointment;
			appointment.LoadFromDictionary(orderedDictionary);
			AppointmentInsertEventArgs appointmentInsertEventArgs = new AppointmentInsertEventArgs(appointment, new SchedulerInfo(this));
			if (this.OnAppointmentInsert(appointmentInsertEventArgs))
			{
				this._appointmentController.InsertAppointment(appointmentInsertEventArgs.SchedulerInfo, appointment);
				this.ActiveFormMode = SchedulerFormMode.Hidden;
				this.Rebind();
			}
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x000DE778 File Offset: 0x000DC978
		internal void UpdateAppointmentInline(bool removeExceptions)
		{
			Appointment appointment = this.FormContainer.Appointment;
			Appointment appointment2 = appointment.Clone();
			IOrderedDictionary orderedDictionary = this.FormContainer.Template.ExtractValues(this.FormContainer);
			orderedDictionary = this.TranslateDataKeysFromTemplate(orderedDictionary);
			appointment2.LoadFromDictionary(orderedDictionary);
			if (appointment.ID == null && appointment.RecurrenceState == RecurrenceState.Exception)
			{
				this._appointmentController.UpdateAppointment(new SchedulerInfo(this), appointment, appointment2);
				if (!this._updateOperationCanceled)
				{
					this.ActiveFormMode = SchedulerFormMode.Hidden;
					this.Rebind();
					return;
				}
			}
			else
			{
				AppointmentUpdateEventArgs appointmentUpdateEventArgs = new AppointmentUpdateEventArgs(appointment, appointment2, new SchedulerInfo(this));
				if (this.OnAppointmentUpdate(appointmentUpdateEventArgs))
				{
					this._appointmentController.UpdateAppointment(appointmentUpdateEventArgs.SchedulerInfo, appointment, appointment2);
					if (removeExceptions)
					{
						this._appointmentController.RemoveRecurrenceExceptions(new SchedulerInfo(this), appointment2);
					}
					this.ActiveFormMode = SchedulerFormMode.Hidden;
					this.Rebind();
				}
			}
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x000DE844 File Offset: 0x000DCA44
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = (CommandEventArgs)args;
			this.OnAppointmentCommand(RadScheduler.FindAppointmentContainer((Control)source), commandEventArgs.CommandName, commandEventArgs.CommandArgument);
			string commandName;
			if ((commandName = commandEventArgs.CommandName) != null)
			{
				if (!(commandName == "Update") && !(commandName == "Insert"))
				{
					if (!(commandName == "More"))
					{
						if (!(commandName == "Delete"))
						{
							if (commandName == "Cancel")
							{
								if (this.OnAppointmentCancelingEdit(this.FormContainer.Appointment, this.FormContainer))
								{
									this.ActiveFormMode = SchedulerFormMode.Hidden;
									this.ClearChildControls();
								}
							}
						}
						else
						{
							if (!this.Page.IsValid)
							{
								return true;
							}
							Appointment appointment = this.FormContainer.Appointment;
							if (appointment.ID == null && appointment.RecurrenceState == RecurrenceState.Exception)
							{
								appointment.RecurrenceState = RecurrenceState.Occurrence;
							}
							this.DeleteAppointment(appointment);
							this.ActiveFormMode = SchedulerFormMode.Hidden;
						}
					}
					else
					{
						SchedulerFormMode mode = (this.ActiveFormMode == SchedulerFormMode.Insert) ? SchedulerFormMode.AdvancedInsert : SchedulerFormMode.AdvancedEdit;
						if (this.OnFormCreating(this.FormContainer.Appointment, mode))
						{
							this.SwitchToAdvancedMode();
							this.ClearChildControls();
							this.ShouldBindFormTemplate = true;
						}
					}
				}
				else
				{
					if (!this.Page.IsValid)
					{
						return true;
					}
					if (commandEventArgs.CommandName == "Update")
					{
						bool removeExceptions;
						bool.TryParse(commandEventArgs.CommandArgument.ToString(), out removeExceptions);
						this.UpdateAppointmentInline(removeExceptions);
					}
					else
					{
						this.InsertAppointmentInline();
					}
				}
			}
			return true;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x000DE9C0 File Offset: 0x000DCBC0
		protected void ClearChildControls()
		{
			if (!base.ChildControlsCreated)
			{
				base.ChildControlsCreated = true;
				this.CreateChildControls(false);
			}
			foreach (Appointment appointment in this.Appointments)
			{
				appointment.AppointmentControls.Clear();
			}
			foreach (object obj in this.Resources)
			{
				Resource resource = (Resource)obj;
				resource.HeaderControls.Clear();
			}
			if (this.UsingDefaultAppointmentContextMenus)
			{
				this.AppointmentContextMenus.Clear();
			}
			if (this.UsingDefaultTimeSlotContextMenus)
			{
				this.TimeSlotContextMenus.Clear();
			}
			this.ActiveSlotIndex = string.Empty;
			this.CreateView();
			base.ClearChildState();
			base.ChildControlsCreated = false;
			this.Controls.Clear();
			this._shouldBindAppointmentControls = true;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x000DEACC File Offset: 0x000DCCCC
		protected override void CreateChildControls()
		{
			this.CreateChildControls(true);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x000DEAD8 File Offset: 0x000DCCD8
		protected void CreateChildControls(bool bindFromDataSource)
		{
			this.CreateFormContainer();
			if (bindFromDataSource)
			{
				this.EnsureDataBound();
			}
			if (this.AdvancedForm.Modal || !this.InAdvancedMode)
			{
				this.CreateContent();
			}
			this._dataPropertyChanged = false;
			if (this.InAdvancedMode)
			{
				this.CreateAdvancedForm();
			}
			if (this.UsingWebServiceBinding && !this.DesignMode)
			{
				this.CreateHiddenViews();
				if (this.AdvancedForm.Enabled)
				{
					this.CreateHiddenAdvancedForms();
				}
			}
			if (bindFromDataSource)
			{
				this.FireAppointmentCreated();
				this.FireTimeSlotCreated();
				this.FireResourceHeaderCreated();
			}
			if (bindFromDataSource && this._shouldBindAppointmentControls)
			{
				foreach (Appointment appointment in this.ActiveModel.Appointments)
				{
					foreach (AppointmentControl appointmentControl in appointment.AppointmentControls)
					{
						appointmentControl.DataBind();
					}
				}
				foreach (object obj in this.Resources)
				{
					Resource resource = (Resource)obj;
					foreach (SchedulerResourceContainer schedulerResourceContainer in resource.HeaderControls)
					{
						schedulerResourceContainer.DataBind();
					}
				}
			}
			if (!this.DesignMode && this.FormContainer.Mode != SchedulerFormMode.Hidden)
			{
				if (this.FormContainer.Appointment == null)
				{
					this.FormContainer.Appointment = this.ActiveFormAppointment;
				}
				this.FormContainer.Template.InstantiateIn(this.FormContainer);
				if (this.ShouldBindFormTemplate)
				{
					this.FormContainer.DataBind();
				}
				this.OnFormCreated(this.FormContainer.Appointment, this.FormContainer);
			}
			if (!this.DesignMode)
			{
				this.AddContextMenus();
				this.AddReminderDialog();
			}
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x000DED08 File Offset: 0x000DCF08
		private void AddContextMenus()
		{
			if (this.AppointmentContextMenus.Count == 0 && this.AppointmentContextMenuSettings.EnableDefault)
			{
				RadSchedulerContextMenu target = this.CreateDefaultAppointmentContextMenu();
				this.AppointmentContextMenus.Add(target);
			}
			if (this.TimeSlotContextMenus.Count == 0 && this.TimeSlotContextMenuSettings.EnableDefault)
			{
				RadSchedulerContextMenu target2 = this.CreateDefaultTimeSlotContextMenu();
				this.TimeSlotContextMenus.Add(target2);
			}
			foreach (object obj in this.AppointmentContextMenus)
			{
				RadSchedulerContextMenu child = (RadSchedulerContextMenu)obj;
				this.Controls.Add(child);
			}
			foreach (object obj2 in this.TimeSlotContextMenus)
			{
				RadSchedulerContextMenu child2 = (RadSchedulerContextMenu)obj2;
				this.Controls.Add(child2);
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x000DEE1C File Offset: 0x000DD01C
		private void AddReminderDialog()
		{
			if (this.RemindersSupport)
			{
				this.CreateReminderDialog();
				this._reminderDialog.Localization.CopyFromSchedulerStrings(this.Localization);
				WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
				webControl.Style[HtmlTextWriterStyle.Display] = "none";
				webControl.Controls.Add(this._reminderDialog);
				this.Controls.Add(webControl);
			}
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x000DEE84 File Offset: 0x000DD084
		private void CreateReminderDialog()
		{
			this._reminderDialog = new ReminderDialog();
			this._reminderDialog.ID = "ReminderDialog";
			if (this.ResolvedRenderMode != RenderMode.Lightweight)
			{
				this._reminderDialog.Height = Unit.Pixel(335);
			}
			this._reminderDialog.Width = Unit.Pixel(463);
			this._reminderDialog.RenderMode = this.ResolvedRenderMode;
			this._reminderDialog.Skin = base.RuntimeSkin;
			this._reminderDialog.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
			this._reminderDialog.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x000DEF23 File Offset: 0x000DD123
		private void SetContextMenuSettings(RadSchedulerContextMenu contextMenu, ContextMenuSettings settings)
		{
			contextMenu.Skin = settings.SkinResolved;
			contextMenu.RenderMode = this.ResolvedRenderMode;
			contextMenu.EnableEmbeddedSkins = settings.EnableEmbeddedSkinsResolved;
			contextMenu.EnableEmbeddedScripts = settings.EnableEmbeddedScriptsResolved;
			contextMenu.EnableEmbeddedBaseStylesheet = settings.EnableEmbeddedBaseStylesheetResolved;
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x000DEF64 File Offset: 0x000DD164
		private RadSchedulerContextMenu CreateDefaultTimeSlotContextMenu()
		{
			RadSchedulerContextMenu radSchedulerContextMenu = new RadSchedulerContextMenu
			{
				ID = "timeSlotContextMenu"
			};
			radSchedulerContextMenu.RenderMode = this.ResolvedRenderMode;
			radSchedulerContextMenu.Items.AddRange(this.ActiveModel.GetTimeSlotContextMenuItems());
			return radSchedulerContextMenu;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x000DEFA8 File Offset: 0x000DD1A8
		private RadSchedulerContextMenu CreateDefaultAppointmentContextMenu()
		{
			RadSchedulerContextMenu radSchedulerContextMenu = new RadSchedulerContextMenu
			{
				ID = "appointmentContextMenu"
			};
			radSchedulerContextMenu.RenderMode = this.ResolvedRenderMode;
			RadMenuItem item = new RadMenuItem
			{
				Text = this.Localization.ContextMenuEdit,
				Value = "CommandEdit"
			};
			radSchedulerContextMenu.Items.Add(item);
			RadMenuItem item2 = new RadMenuItem
			{
				Text = this.Localization.ContextMenuDelete,
				Value = "CommandDelete"
			};
			radSchedulerContextMenu.Items.Add(item2);
			return radSchedulerContextMenu;
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x000DF040 File Offset: 0x000DD240
		private void CreateContent()
		{
			this.ActiveModel.DataBind(this.VisibleAppointments);
			if (!string.IsNullOrEmpty(this.ActiveSlotIndex) && this.ActiveFormMode != SchedulerFormMode.Hidden)
			{
				ISchedulerTimeSlot slotByIndex = this.ActiveModel.GetSlotByIndex(this.ActiveSlotIndex);
				slotByIndex.FormContainer = this.FormContainer;
			}
			Control content = this.ActiveModel.GetRenderer().GetContent();
			this.CreateFooter(content, this.ActiveModel.GetRenderer());
			this.Controls.Add(content);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x000DF0C0 File Offset: 0x000DD2C0
		private void CreateAdvancedForm()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.Controls.Add(webControl);
			webControl.CssClass = "rsAdvFormWrap";
			if (this.AdvancedForm.Modal)
			{
				webControl.Style[HtmlTextWriterStyle.Display] = "none";
			}
			webControl.Controls.Add(this.FormContainer);
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x000DF11C File Offset: 0x000DD31C
		private void CreateHiddenViews()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.Controls.Add(webControl);
			webControl.CssClass = "rsHiddenViews";
			webControl.Style[HtmlTextWriterStyle.Display] = "none";
			foreach (ISchedulerModel schedulerModel in this.GetHiddenViewModels())
			{
				schedulerModel.DataBind(new AppointmentCollection());
				webControl.Controls.Add(schedulerModel.GetRenderer().GetInnerContent());
			}
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x000DF1B4 File Offset: 0x000DD3B4
		private void CreateHiddenAdvancedForms()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.Controls.Add(webControl);
			webControl.CssClass = "rsHiddenAdvancedForm";
			webControl.Style[HtmlTextWriterStyle.Display] = "none";
			this.CreateHiddenAdvancedInsertForm(webControl);
			this.CreateHiddenAdvancedEditForm(webControl);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x000DF200 File Offset: 0x000DD400
		private void CreateFooter(Control container, ISchedulerRenderer renderer)
		{
			if (!this.ShowFooter)
			{
				return;
			}
			if (!this.UsingWebServiceBinding && !renderer.ShouldRenderFooter)
			{
				return;
			}
			WebControl webControl;
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				webControl = new FooterControlLite(true, this.ShowFullTime ? this.Localization.ShowBusinessHours : this.Localization.Show24Hours);
			}
			else
			{
				webControl = new FooterControl(true, this.ShowFullTime ? this.Localization.ShowBusinessHours : this.Localization.Show24Hours);
			}
			if (!renderer.ShouldRenderFooter)
			{
				webControl.Style["display"] = "none";
			}
			container.Controls.Add(webControl);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x000DF2AC File Offset: 0x000DD4AC
		private void CreateHiddenAdvancedInsertForm(Control container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			container.Controls.Add(webControl);
			webControl.CssClass = "rsAdvancedInsertWrapper";
			string validationGroup = this.ValidationGroup;
			this.ValidationGroup += "Insert";
			SchedulerFormContainer schedulerFormContainer = new SchedulerFormContainer(this);
			webControl.Controls.Add(schedulerFormContainer);
			schedulerFormContainer.Mode = SchedulerFormMode.AdvancedInsert;
			schedulerFormContainer.ID = "AdvancedInsertForm";
			schedulerFormContainer.Appointment = this.CreateAppointment();
			schedulerFormContainer.Appointment.Start = DateTime.Now;
			schedulerFormContainer.Appointment.End = DateTime.Now.AddHours(1.0);
			this.AdvancedInsertTemplate.InstantiateIn(schedulerFormContainer);
			schedulerFormContainer.DataBind();
			this.ValidationGroup = validationGroup;
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x000DF370 File Offset: 0x000DD570
		private void CreateHiddenAdvancedEditForm(Control container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			container.Controls.Add(webControl);
			webControl.CssClass = "rsAdvancedEditWrapper";
			string validationGroup = this.ValidationGroup;
			this.ValidationGroup += "Edit";
			SchedulerFormContainer schedulerFormContainer = new SchedulerFormContainer(this);
			webControl.Controls.Add(schedulerFormContainer);
			schedulerFormContainer.Mode = SchedulerFormMode.AdvancedEdit;
			schedulerFormContainer.ID = "AdvancedEditForm";
			schedulerFormContainer.Appointment = this.CreateAppointment();
			schedulerFormContainer.Appointment.Start = DateTime.Now;
			schedulerFormContainer.Appointment.End = DateTime.Now.AddHours(1.0);
			this.AdvancedEditTemplate.InstantiateIn(schedulerFormContainer);
			schedulerFormContainer.DataBind();
			this.ValidationGroup = validationGroup;
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x000DF434 File Offset: 0x000DD634
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string value = base.Style[HtmlTextWriterStyle.OverflowY];
			Unit height = this.Height;
			if (this.OverflowBehavior == OverflowBehavior.Expand)
			{
				base.Style[HtmlTextWriterStyle.OverflowY] = "visible";
				this.Height = Unit.Empty;
			}
			base.AddAttributesToRender(writer);
			this.Height = height;
			base.Style[HtmlTextWriterStyle.OverflowY] = value;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x000DF498 File Offset: 0x000DD698
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			base.RenderContents(writer);
			if (this.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			if (!this.DesignMode)
			{
				this.RenderOverflowScripts(writer);
			}
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000DF4CC File Offset: 0x000DD6CC
		protected override void OnPreRender(EventArgs e)
		{
			if (this.ResolvedRenderMode == RenderMode.Classic && (this.SelectedView == SchedulerViewType.YearView || this.YearView.UserSelectable))
			{
				throw new NotSupportedException("The YearView is not available in the Classic render mode. To enable it set the Scheduler's RenderMode property to Lightweight or Mobile.");
			}
			if (this._dataPropertyChanged)
			{
				this.ClearChildControls();
				this.EnsureChildControls();
			}
			base.OnPreRender(e);
			foreach (object obj in this.AppointmentContextMenus)
			{
				RadSchedulerContextMenu contextMenu = (RadSchedulerContextMenu)obj;
				this.SetContextMenuSettings(contextMenu, this.AppointmentContextMenuSettings);
			}
			foreach (object obj2 in this.TimeSlotContextMenus)
			{
				RadSchedulerContextMenu contextMenu2 = (RadSchedulerContextMenu)obj2;
				this.SetContextMenuSettings(contextMenu2, this.TimeSlotContextMenuSettings);
			}
			if (base.ScriptManager.LoadScriptsBeforeUI && this.Page.Form != null)
			{
				string preinitializeScript = this.GetPreinitializeScript();
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadScheduler), "SchedulerAdjustAppointmentHeight", preinitializeScript, true);
			}
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x000DF608 File Offset: 0x000DD808
		private string GetPreinitializeScript()
		{
			return string.Format("Telerik.Web.UI.RadScheduler._preInitialize(\"{0}\",{1},{2},{3},{4},{5});", new object[]
			{
				this.ClientID,
				this.ScrollTop,
				this.ScrollLeft,
				(int)this.OverflowBehavior,
				this.UseHorizontalScrolling.ToString().ToLowerInvariant(),
				(int)this.ResolvedRenderMode
			});
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x000DF680 File Offset: 0x000DD880
		private void FireTimeSlotCreated()
		{
			if (this.InAdvancedMode)
			{
				return;
			}
			foreach (ISchedulerTimeSlot schedulerTimeSlot in this.ActiveModel.GetTimeSlots())
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(schedulerTimeSlot.CssClass))
				{
					list.AddRange(schedulerTimeSlot.CssClass.Split(new char[]
					{
						' '
					}));
					schedulerTimeSlot.CssClass = string.Empty;
					schedulerTimeSlot.Control.CssClass = string.Empty;
				}
				this.OnTimeSlotCreated(schedulerTimeSlot);
				if (schedulerTimeSlot.Control != null)
				{
					if (!string.IsNullOrEmpty(schedulerTimeSlot.CssClass))
					{
						list.AddRange(schedulerTimeSlot.CssClass.Split(new char[]
						{
							' '
						}));
					}
					schedulerTimeSlot.Control.CssClass = string.Join(" ", list.ToArray());
				}
			}
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x000DF780 File Offset: 0x000DD980
		private void FireResourceHeaderCreated()
		{
			foreach (object obj in this.Resources)
			{
				Resource resource = (Resource)obj;
				foreach (SchedulerResourceContainer resourceContainer in resource.HeaderControls)
				{
					this.OnResourceHeaderCreated(resourceContainer);
				}
			}
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x000DF814 File Offset: 0x000DDA14
		private void RenderOverflowScripts(HtmlTextWriter writer)
		{
			if (base.ScriptManager.LoadScriptsBeforeUI && this.Page.Form != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
				writer.RenderBeginTag("script");
				if (this.OverflowBehavior == OverflowBehavior.Scroll && this.DefaultAdvancedFormRendered && !this.AdvancedForm.Modal)
				{
					writer.Write(string.Format("Telerik.Web.UI.Scheduling.AdvancedTemplate._adjustHeight($get(\"{0}\"),{1});", this.ClientID, (int)this.ResolvedRenderMode));
				}
				writer.Write(this.GetPreinitializeScript());
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x000DF8A4 File Offset: 0x000DDAA4
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.EnableEmbeddedScripts)
			{
				string fullName = Assembly.GetExecutingAssembly().FullName;
				if (this.DefaultAdvancedFormRendered || this.UseDefaultAdvancedInsert || this.UseDefaultAdvancedEdit)
				{
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Scheduling.AdvancedTemplate.js", fullName));
				}
				list.AddRange(this.ActiveModel.GetScriptReferences());
				if (this.UsingWebServiceBinding)
				{
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.ClientRendering.js", fullName));
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.BlockCollection.js", fullName));
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.HorizontalBlockCollection.js", fullName));
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.RenderingManager.js", fullName));
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.WebApiLoader.js", fullName));
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.RecurrenceRule.RecurrenceRule.js", fullName));
					if (this.UsingODataWebServiceBinding)
					{
						list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.OData.SchedulerODataSettings.js", fullName));
					}
					list.AddRange(this.GetScriptReferencesForHiddenViews());
				}
				list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.ResourceStyleMappingCollection.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ClientRendering.ResourceStyleMapping.js", fullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Helpers.ResizeHelper.js", fullName));
				if (this.AppointmentContextMenus.Count > 0 || this.TimeSlotContextMenus.Count > 0)
				{
					list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.ContextMenu.Plugin.js", fullName));
				}
				if (this.RemindersSupport)
				{
					list.AddRange(new ScriptReference[]
					{
						new ScriptReference("Telerik.Web.UI.Scheduler.Reminders.ReminderScripts.js", fullName)
					});
				}
			}
			return list;
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x000DFA2A File Offset: 0x000DDC2A
		protected override bool IsBoundUsingOData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x000DFA30 File Offset: 0x000DDC30
		private IEnumerable<ScriptReference> GetScriptReferencesForHiddenViews()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			foreach (ISchedulerModel schedulerModel in this.GetHiddenViewModels())
			{
				list.AddRange(schedulerModel.GetScriptReferences());
			}
			return list;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x000DFA8C File Offset: 0x000DDC8C
		private IList<ISchedulerModel> GetHiddenViewModels()
		{
			IList<ISchedulerModel> list = new List<ISchedulerModel>();
			if (this.SelectedView != SchedulerViewType.DayView && this.DayView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.DayView).CreateModel());
			}
			if (this.SelectedView != SchedulerViewType.WeekView && this.WeekView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.WeekView).CreateModel());
			}
			if (this.SelectedView != SchedulerViewType.MonthView && this.MonthView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.MonthView).CreateModel());
			}
			if (this.SelectedView != SchedulerViewType.YearView && this.YearView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.YearView).CreateModel());
			}
			if (this.SelectedView != SchedulerViewType.TimelineView && this.TimelineView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.TimelineView).CreateModel());
			}
			if (this.SelectedView != SchedulerViewType.MultiDayView && this.MultiDayView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.MultiDayView).CreateModel());
			}
			if (this.SelectedView != SchedulerViewType.AgendaView && this.AgendaView.UserSelectable)
			{
				list.Add(this.GetModelFactory(SchedulerViewType.AgendaView).CreateModel());
			}
			return list;
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x000DFBB8 File Offset: 0x000DDDB8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new SchedulerAttributeCollectionConverter(),
				new ResourceStyleMappingConverter()
			});
			descriptor.AddProperty("localization", javaScriptSerializer.Serialize(this.Localization));
			descriptor.AddProperty("selectedDate", this.SelectedDate.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture));
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", javaScriptSerializer.Serialize(base.Attributes));
			}
			if (this.CustomAttributeNames.Length > 0)
			{
				descriptor.AddProperty("customAttributeNames", this.CustomAttributeNames);
			}
			string value = this.UtcToDisplay(this.ActiveModel.VisibleRangeStart).ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
			descriptor.AddProperty("firstDayStart", value);
			if (this.RemindersSupport)
			{
				base.DescribeProperty<double>(descriptor, "_remindersMaxAge", this.Reminders.MaxAge.TotalMilliseconds, TimeSpan.FromDays(14.0).TotalMilliseconds);
			}
			base.DescribeProperty<int>(descriptor, "_timeZoneOffset", (int)this.TimeZoneOffset.TotalMilliseconds, 0);
			base.DescribeProperty<string>(descriptor, "_timeZoneId", this._timeZoneProvider.OperationTimeZone.StandardName, string.Empty);
			if (this.TimeZonesEnabled && this._timeZoneProvider.OperationTimeZone.SupportsDayLightSaving)
			{
				this.DescribeAdjustmentRules(descriptor, javaScriptSerializer);
			}
			this.DescribeAppointments(descriptor, javaScriptSerializer);
			this.DescribeResources(descriptor, javaScriptSerializer);
			this.DescribeResourceTypes(descriptor, javaScriptSerializer);
			this.DescribeContextMenuIDs(descriptor, javaScriptSerializer);
			if (this.FormContainer != null && this.FormContainer.Appointment != null)
			{
				base.DescribeProperty<SchedulerFormMode>(descriptor, "_formContainerMode", this.FormContainer.Mode, SchedulerFormMode.Hidden);
				base.DescribeProperty<bool>(descriptor, "_editingRecurringSeries", this.EditingRecurringSeries, false);
			}
			if (this.UsingWebServiceBinding)
			{
				base.DescribeProperty<bool>(descriptor, "_useDefaultAdvancedInsert", this.UseDefaultAdvancedInsert, true);
				base.DescribeProperty<bool>(descriptor, "_useDefaultAdvancedEdit", this.UseDefaultAdvancedEdit, true);
				if (this.UsingODataWebServiceBinding)
				{
					this.PopulateODataSettings();
				}
				this.WebServiceSettings.Describe("webServiceSettings", javaScriptSerializer, descriptor);
			}
			if (this.ResourceStyles.Count > 0)
			{
				descriptor.AddProperty("resourceStyles", javaScriptSerializer.Serialize(this.ResourceStyles));
			}
			this.AdvancedForm.Describe("advancedFormSettings", javaScriptSerializer, descriptor);
			this.DescribeViewSettings(javaScriptSerializer, descriptor);
			this.DescribeViewData(javaScriptSerializer, descriptor);
			base.DescribeRenderingMode(descriptor);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x000DFE58 File Offset: 0x000DE058
		private void PopulateODataSettings()
		{
			this.WebServiceSettings.ODataSettings.ODataDataSourceID = RadScheduler.FindODataDataSourceClientID(this, this.ODataDataSourceID);
			this.WebServiceSettings.ODataSettings.DataModelID = this.DataModelID;
			this.WebServiceSettings.ODataSettings.DataKeyField = this.DataKeyField;
			this.WebServiceSettings.ODataSettings.DataSubjectField = this.DataSubjectField;
			this.WebServiceSettings.ODataSettings.DataStartField = this.DataStartField;
			this.WebServiceSettings.ODataSettings.DataEndField = this.DataEndField;
			this.WebServiceSettings.ODataSettings.DataDescriptionField = this.DataDescriptionField;
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000DFF08 File Offset: 0x000DE108
		private static string FindODataDataSourceClientID(Control control, string controlID)
		{
			Control control2 = control;
			Control control3 = null;
			if (control == control.Page)
			{
				Control control4 = control.FindControl(controlID);
				if (control4 == null)
				{
					return controlID;
				}
				return control4.ClientID;
			}
			else
			{
				while (control3 == null && control2 != control.Page)
				{
					control2 = control2.NamingContainer;
					if (control2 == null)
					{
						return controlID;
					}
					control3 = control2.FindControl(controlID);
				}
				if (control3 == null)
				{
					return controlID;
				}
				return control3.ClientID;
			}
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000DFF64 File Offset: 0x000DE164
		private void DescribeViewSettings(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			this.DayView.Describe("dayViewSettings", serializer, descriptor);
			this.WeekView.Describe("weekViewSettings", serializer, descriptor);
			this.MonthView.Describe("monthViewSettings", serializer, descriptor);
			this.MultiDayView.Describe("multiDayViewSettings", serializer, descriptor);
			this.TimelineView.Describe("timelineViewSettings", serializer, descriptor);
			this.AgendaView.Describe("agendaViewSettings", serializer, descriptor);
			this.YearView.Describe("yearViewSettings", serializer, descriptor);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x000DFFEF File Offset: 0x000DE1EF
		private void DescribeViewData(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			this.ActiveModel.DescribeModelData("_modelData", serializer, descriptor);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000E0003 File Offset: 0x000DE203
		protected override void OnDataPropertyChanged()
		{
			base.OnDataPropertyChanged();
			this.CreateView();
			this._dataPropertyChanged = true;
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000E0018 File Offset: 0x000DE218
		protected override Style CreateControlStyle()
		{
			return new RadScheduler.SchedulerStyle(this.ViewState);
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x000E0025 File Offset: 0x000DE225
		void IScheduler.NotifyDataPropertyChanged()
		{
			this.OnDataPropertyChanged();
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x000E002D File Offset: 0x000DE22D
		private void CreateView()
		{
			this.ActiveModel = this.GetModelFactory(this.SelectedView).CreateModel();
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x000E0048 File Offset: 0x000DE248
		private ISchedulerModelFactory GetModelFactory(SchedulerViewType viewType)
		{
			switch (viewType)
			{
			case SchedulerViewType.WeekView:
				return new WeekModelFactory(this);
			case SchedulerViewType.MonthView:
				return new MonthModelFactory(this);
			case SchedulerViewType.TimelineView:
				return new TimelineModelFactory(this);
			case SchedulerViewType.MultiDayView:
				return new MultiDayModelFactory(this);
			case SchedulerViewType.AgendaView:
				return new AgendaModelFactory(this);
			case SchedulerViewType.YearView:
				return new YearModelFactory(this);
			}
			return new DayModelFactory(this);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x000E00BC File Offset: 0x000DE2BC
		private void DescribeContextMenuIDs(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			if (this.AppointmentContextMenus.Count > 0)
			{
				List<string> list = new List<string>();
				foreach (object obj in this.AppointmentContextMenus)
				{
					RadSchedulerContextMenu radSchedulerContextMenu = (RadSchedulerContextMenu)obj;
					list.Add(radSchedulerContextMenu.ID);
				}
				descriptor.AddScriptProperty("appointmentContextMenuIDs", serializer.Serialize(list));
			}
			if (this.TimeSlotContextMenus.Count > 0)
			{
				List<string> list2 = new List<string>();
				foreach (object obj2 in this.TimeSlotContextMenus)
				{
					RadSchedulerContextMenu radSchedulerContextMenu2 = (RadSchedulerContextMenu)obj2;
					list2.Add(radSchedulerContextMenu2.ID);
				}
				descriptor.AddScriptProperty("timeSlotContextMenuIDs", serializer.Serialize(list2));
			}
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x000E01C4 File Offset: 0x000DE3C4
		private void DescribeAppointments(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new AppointmentConverter(this)
			});
			AppointmentCollection appointmentCollection = new AppointmentCollection();
			foreach (Appointment appointment in this.Appointments)
			{
				if (appointment.DomElements.Count > 0 || appointment.Reminders.Count > 0)
				{
					appointmentCollection.Add(appointment);
				}
			}
			descriptor.AddProperty("appointments", serializer.Serialize(appointmentCollection));
			if (this.FormContainer != null && this.FormContainer.Appointment != null)
			{
				descriptor.AddProperty("currentAppointment", serializer.Serialize(this.FormContainer.Appointment));
			}
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x000E0290 File Offset: 0x000DE490
		private void DescribeAdjustmentRules(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new AdjustmentRuleConverter()
			});
			descriptor.AddProperty("adjustmentRules", serializer.Serialize(this._timeZoneProvider.OperationTimeZone.AdjustmentRules));
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000E02D4 File Offset: 0x000DE4D4
		private void DescribeResources(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			IList<Resource> list = new List<Resource>(this.Resources);
			if (list.Count > 0)
			{
				serializer.RegisterConverters(new JavaScriptConverter[]
				{
					new ResourceConverter()
				});
				descriptor.AddProperty("resources", serializer.Serialize(list));
			}
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000E0320 File Offset: 0x000DE520
		private void DescribeResourceTypes(IScriptDescriptor descriptor, JavaScriptSerializer serializer)
		{
			IList<ResourceType> list = new List<ResourceType>(this.ResourceTypes);
			if (list.Count > 0)
			{
				serializer.RegisterConverters(new JavaScriptConverter[]
				{
					new ResourceTypeConverter()
				});
				descriptor.AddProperty("resourceTypes", serializer.Serialize(list));
			}
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000E036C File Offset: 0x000DE56C
		private void DisplayPreviousAppointmentSegment(Appointment appointment)
		{
			if (!(appointment.Start < this.VisibleRangeStart))
			{
				if (this.CanSwitchToFullTime)
				{
					this.SwitchFullTime();
				}
				return;
			}
			if (this.CanSwitchToFullTime)
			{
				this.SwitchFullTime();
				return;
			}
			this.SelectedDate = this.ActiveModel.PreviousPeriodDate;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000E03BC File Offset: 0x000DE5BC
		private void DisplayNextAppointmentSegment(Appointment appointment)
		{
			if (!(appointment.End > this.VisibleRangeEnd))
			{
				if (this.CanSwitchToFullTime)
				{
					this.SwitchFullTime();
				}
				return;
			}
			if (this.CanSwitchToFullTime)
			{
				this.SwitchFullTime();
				return;
			}
			this.SelectedDate = this.ActiveModel.NextPeriodDate;
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000E040B File Offset: 0x000DE60B
		private void SwitchFullTime()
		{
			this.ShowFullTime = !this.ShowFullTime;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000E041C File Offset: 0x000DE61C
		private void SwitchToDayView()
		{
			this.SelectedView = SchedulerViewType.DayView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x000E0439 File Offset: 0x000DE639
		private void SwitchToWeekView()
		{
			this.SelectedView = SchedulerViewType.WeekView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000E0456 File Offset: 0x000DE656
		private void SwitchToMonthView()
		{
			this.SelectedView = SchedulerViewType.MonthView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x000E0473 File Offset: 0x000DE673
		private void SwitchToTimelineView()
		{
			this.SelectedView = SchedulerViewType.TimelineView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x000E0490 File Offset: 0x000DE690
		private void SwitchToMultiDayView()
		{
			this.SelectedView = SchedulerViewType.MultiDayView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x000E04AD File Offset: 0x000DE6AD
		private void SwitchToAgendaView()
		{
			this.SelectedView = SchedulerViewType.AgendaView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x000E04CA File Offset: 0x000DE6CA
		private void SwitchToYearView()
		{
			this.SelectedView = SchedulerViewType.YearView;
			this.ScrollTop = 0;
			this.ScrollLeft = 0;
			this.RevertToDefaultState();
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x000E04E8 File Offset: 0x000DE6E8
		private void SwitchToAdvancedMode()
		{
			if (this.AdvancedForm.Enabled && (this.FormContainer.Mode == SchedulerFormMode.Insert || this.FormContainer.Mode == SchedulerFormMode.Edit))
			{
				IOrderedDictionary orderedDictionary = this.FormContainer.Template.ExtractValues(this.FormContainer);
				if (this.FormContainer.Mode == SchedulerFormMode.Insert)
				{
					DateTime dateTime = this.UtcToDisplay(this.FormContainer.Appointment.Start);
					if (this.FormContainer.Appointment.End > DateTime.MinValue)
					{
						DateTime endDate = this.UtcToDisplay(this.FormContainer.Appointment.End);
						this.SwitchToInsertMode(dateTime, endDate, true);
					}
					else
					{
						this.ShowInlineInsertForm(dateTime);
					}
					using (IEnumerator enumerator = this.FormContainer.Appointment.Resources.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Resource item = (Resource)obj;
							this.ActiveFormAppointment.Resources.Add(item);
						}
						goto IL_122;
					}
				}
				this.SwitchToEditMode(this.FormContainer.Appointment, this.EditingRecurringSeries, true);
				IL_122:
				if (orderedDictionary.Contains("Subject"))
				{
					this.ActiveFormAppointment.Subject = orderedDictionary["Subject"].ToString();
				}
			}
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x000E0650 File Offset: 0x000DE850
		private void RevertToDefaultState()
		{
			this.ActiveFormMode = SchedulerFormMode.Hidden;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x000E065C File Offset: 0x000DE85C
		private void FireAppointmentCreated()
		{
			foreach (Appointment appointment in this.ActiveModel.Appointments)
			{
				foreach (AppointmentControl appointmentControl in appointment.AppointmentControls)
				{
					this.OnAppointmentCreated(appointment, appointmentControl.AppointmentContainer);
				}
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x000E06EC File Offset: 0x000DE8EC
		private void CreateFormContainer()
		{
			if (this.CurrentSlotWidth > 0 && this.CurrentSlotHeight > 0)
			{
				int width = 0;
				if (this.CurrentSlotWidth < this.MinimumInlineFormWidth)
				{
					width = this.MinimumInlineFormWidth;
				}
				int height = 0;
				if (this.CurrentSlotHeight < this.MinimumInlineFormHeight)
				{
					height = this.MinimumInlineFormHeight;
				}
				this.FormContainer = new SchedulerFormContainer(this, width, height);
			}
			else
			{
				this.FormContainer = new SchedulerFormContainer(this);
			}
			this.FormContainer.Mode = this.ActiveFormMode;
			this.FormContainer.ID = "Form";
			switch (this.FormContainer.Mode)
			{
			case SchedulerFormMode.Insert:
				this.FormContainer.Template = (IBindableTemplate)this.InlineInsertTemplate;
				return;
			case SchedulerFormMode.Edit:
				this.FormContainer.Template = (IBindableTemplate)this.InlineEditTemplate;
				return;
			case SchedulerFormMode.AdvancedInsert:
				this.FormContainer.Template = (IBindableTemplate)this.AdvancedInsertTemplate;
				return;
			case SchedulerFormMode.AdvancedEdit:
				this.FormContainer.Template = (IBindableTemplate)this.AdvancedEditTemplate;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x000E07F8 File Offset: 0x000DE9F8
		private static SchedulerAppointmentContainer FindAppointmentContainer(Control source)
		{
			SchedulerAppointmentContainer schedulerAppointmentContainer = null;
			Control control = source;
			while (schedulerAppointmentContainer == null && control != null)
			{
				schedulerAppointmentContainer = (control as SchedulerAppointmentContainer);
				control = control.Parent;
			}
			return schedulerAppointmentContainer;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000E0820 File Offset: 0x000DEA20
		void IScheduler.HandleInsert(Appointment appointmentToInsert)
		{
			SchedulerFormMode schedulerFormMode = this.StartInsertingInAdvancedForm ? SchedulerFormMode.AdvancedInsert : SchedulerFormMode.Insert;
			if (appointmentToInsert.RecurrenceState == RecurrenceState.Master)
			{
				schedulerFormMode = SchedulerFormMode.AdvancedInsert;
			}
			if (this.OnFormCreating(appointmentToInsert, schedulerFormMode))
			{
				this.ClearChildControls();
				this.ActiveFormMode = schedulerFormMode;
				this.ActiveFormAppointment = appointmentToInsert;
				this.ShouldBindFormTemplate = true;
			}
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x000E086C File Offset: 0x000DEA6C
		private void SwitchToInsertMode(DateTime startDate, DateTime endDate, bool useAdvancedForm)
		{
			Appointment appointment = this.CreateAppointment();
			appointment.Start = this.DisplayToUtc(startDate);
			appointment.End = this.DisplayToUtc(endDate);
			appointment.TimeZoneID = this.TimeZoneID;
			SchedulerFormMode schedulerFormMode = useAdvancedForm ? SchedulerFormMode.AdvancedInsert : SchedulerFormMode.Insert;
			if (this.OnFormCreating(appointment, schedulerFormMode))
			{
				this.ClearChildControls();
				this.ActiveFormMode = schedulerFormMode;
				this.ActiveFormAppointment = appointment;
				if (schedulerFormMode == SchedulerFormMode.Insert)
				{
					ISchedulerModel schedulerModel = this.GetModelFactory(this.SelectedView).CreateModel();
					schedulerModel.DataBind(new AppointmentCollection(new Appointment[]
					{
						appointment
					}));
					ISchedulerTimeSlot appointmentSlot = schedulerModel.GetAppointmentSlot(appointment);
					if (appointmentSlot != null)
					{
						this.ActiveSlotIndex = appointmentSlot.Index;
					}
				}
				this.ShouldBindFormTemplate = true;
			}
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x000E091C File Offset: 0x000DEB1C
		private void SwitchToEditMode(Appointment appointmentToEdit, bool editSeries, bool useAdvancedForm)
		{
			SchedulerFormMode schedulerFormMode = useAdvancedForm ? SchedulerFormMode.AdvancedEdit : SchedulerFormMode.Edit;
			if (this.OnFormCreating(appointmentToEdit, schedulerFormMode))
			{
				this.ClearChildControls();
				this.ActiveModel.DataBind(this.VisibleAppointments);
				ISchedulerTimeSlot appointmentSlot = this.ActiveModel.GetAppointmentSlot(appointmentToEdit);
				if (appointmentSlot != null)
				{
					this.ActiveSlotIndex = appointmentSlot.Index;
				}
				this.ActiveFormMode = schedulerFormMode;
				this.ActiveFormAppointment = this._appointmentController.PrepareToEdit(appointmentToEdit, editSeries);
				if (!this.StartEditingInAdvancedForm && editSeries)
				{
					this.ActiveFormAppointment.Subject = appointmentToEdit.Subject;
				}
				this.ShouldBindFormTemplate = true;
			}
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x000E09AC File Offset: 0x000DEBAC
		private IOrderedDictionary TranslateDataKeysFromTemplate(IDictionary data)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (object obj in data.Keys)
			{
				string text = (string)obj;
				if (!string.IsNullOrEmpty(this.DataSubjectField) && text == this.DataSubjectField)
				{
					orderedDictionary.Add("Subject", data[text]);
				}
				else if (!string.IsNullOrEmpty(this.DataTimeZoneIdField) && text == this.DataTimeZoneIdField)
				{
					orderedDictionary.Add("TimeZoneID", data[text]);
				}
				else if ((!string.IsNullOrEmpty(this.DataDescriptionField) && text == this.DataDescriptionField) || (text == "Description" && this.EnableDescriptionField))
				{
					orderedDictionary.Add("$$Description$$", data[text]);
				}
				else if ((!string.IsNullOrEmpty(this.DataReminderField) && text == this.DataReminderField) || (text == "Reminder" && this.Reminders.Enabled))
				{
					string text2 = data[text].ToString();
					int triggerMinutes;
					if (int.TryParse(text2, out triggerMinutes))
					{
						orderedDictionary.Add("$$Reminders$$", new Reminder(triggerMinutes).ToString());
					}
					else if (string.IsNullOrEmpty(text2))
					{
						orderedDictionary.Add("$$Reminders$$", string.Empty);
					}
				}
				else if (!string.IsNullOrEmpty(this.DataStartField) && text == this.DataStartField)
				{
					orderedDictionary.Add("Start", data[text]);
				}
				else if (!string.IsNullOrEmpty(this.DataEndField) && text == this.DataEndField)
				{
					orderedDictionary.Add("End", data[text]);
				}
				else if (!string.IsNullOrEmpty(this.DataRecurrenceField) && text == this.DataRecurrenceField)
				{
					orderedDictionary.Add("RecurrenceRule", data[text]);
				}
				else if (!string.IsNullOrEmpty(this.DataRecurrenceParentKeyField) && text == this.DataRecurrenceParentKeyField)
				{
					orderedDictionary.Add("RecurrenceParentID", data[text]);
				}
				else
				{
					orderedDictionary.Add(text, data[text]);
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x000E0C20 File Offset: 0x000DEE20
		private void FinishResize(Appointment originalAppointment, Appointment modifiedAppointment)
		{
			if (originalAppointment.ID == null && originalAppointment.RecurrenceState == RecurrenceState.Exception)
			{
				this._appointmentController.UpdateAppointment(new SchedulerInfo(this), originalAppointment, modifiedAppointment);
				this.Rebind();
				return;
			}
			if (modifiedAppointment.RecurrenceState == RecurrenceState.Master)
			{
				this.FixRecurrenceRule(modifiedAppointment);
			}
			AppointmentUpdateEventArgs appointmentUpdateEventArgs = new AppointmentUpdateEventArgs(originalAppointment, modifiedAppointment, new SchedulerInfo(this));
			if (this.OnAppointmentUpdate(appointmentUpdateEventArgs))
			{
				this._appointmentController.UpdateAppointment(appointmentUpdateEventArgs.SchedulerInfo, originalAppointment, modifiedAppointment);
				this.Rebind();
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x000E0C98 File Offset: 0x000DEE98
		private void FixRecurrenceRule(Appointment modifiedAppointment)
		{
			RecurrenceRule recurrenceRule;
			if (RecurrenceRule.TryParse(modifiedAppointment.RecurrenceRule, out recurrenceRule))
			{
				recurrenceRule.Range.Start = modifiedAppointment.Start;
				recurrenceRule.Range.EventDuration = modifiedAppointment.Duration;
				modifiedAppointment.RecurrenceRule = recurrenceRule.ToString();
			}
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000E0CE4 File Offset: 0x000DEEE4
		private bool RaiseNavigationCommandEvent(SchedulerNavigationCommandEventArgs args)
		{
			SchedulerNavigationCommandEventHandler schedulerNavigationCommandEventHandler = (SchedulerNavigationCommandEventHandler)base.Events[RadScheduler.SchedulerNavigationCommandEvent];
			if (schedulerNavigationCommandEventHandler != null)
			{
				schedulerNavigationCommandEventHandler(this, args);
			}
			this.TrackNavigationCommand(args);
			return !args.Cancel;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x000E0D24 File Offset: 0x000DEF24
		protected internal virtual void OnAppointmentCreated(Appointment appointment, SchedulerAppointmentContainer container)
		{
			AppointmentCreatedEventHandler appointmentCreatedEventHandler = (AppointmentCreatedEventHandler)base.Events[RadScheduler.AppointmentCreatedEvent];
			if (appointmentCreatedEventHandler != null)
			{
				AppointmentCreatedEventArgs e = new AppointmentCreatedEventArgs(appointment, container);
				appointmentCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x000E0DB4 File Offset: 0x000DEFB4
		protected virtual void TrackNavigationCommand(SchedulerNavigationCommandEventArgs e)
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "NavigationCommand").OfPriority(FeaturePriority.High).OfClass(FeatureClass.Appearance).OfValue(() => Enum.GetName(typeof(SchedulerNavigationCommand), e.Command)));
			if (e.Command == SchedulerNavigationCommand.SwitchToSelectedDay)
			{
				Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "NavigateToDate").OfClass(FeatureClass.Appearance).OfValue(() => e.SelectedDate.ToShortDateString()));
			}
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x000E0E7C File Offset: 0x000DF07C
		protected internal virtual void OnAppointmentDataBound(Appointment appointment)
		{
			AppointmentDataBoundEventHandler appointmentDataBoundEventHandler = (AppointmentDataBoundEventHandler)base.Events[RadScheduler.AppointmentDataBoundEvent];
			if (appointmentDataBoundEventHandler != null)
			{
				SchedulerEventArgs e = new SchedulerEventArgs(appointment);
				appointmentDataBoundEventHandler(this, e);
			}
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x000E0EB4 File Offset: 0x000DF0B4
		protected virtual void OnAppointmentCommand(SchedulerAppointmentContainer container, string commandName, object commandArgument)
		{
			AppointmentCommandEventHandler appointmentCommandEventHandler = (AppointmentCommandEventHandler)base.Events[RadScheduler.AppointmentCommandEvent];
			AppointmentCommandEventArgs e = new AppointmentCommandEventArgs(container, commandName, commandArgument);
			if (appointmentCommandEventHandler != null)
			{
				appointmentCommandEventHandler(this, e);
			}
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x000E0EEC File Offset: 0x000DF0EC
		protected virtual bool OnAppointmentContextMenuItemClicking(Appointment appointment, RadMenuItem menuItem)
		{
			AppointmentContextMenuItemClickingEventHandler appointmentContextMenuItemClickingEventHandler = (AppointmentContextMenuItemClickingEventHandler)base.Events[RadScheduler.AppointmentContextMenuItemClickingEvent];
			AppointmentContextMenuItemClickingEventArgs appointmentContextMenuItemClickingEventArgs = new AppointmentContextMenuItemClickingEventArgs(appointment, menuItem);
			if (appointmentContextMenuItemClickingEventHandler != null)
			{
				appointmentContextMenuItemClickingEventHandler(this, appointmentContextMenuItemClickingEventArgs);
			}
			return !appointmentContextMenuItemClickingEventArgs.Cancel;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x000E0F2C File Offset: 0x000DF12C
		protected virtual void OnAppointmentContextMenuItemClicked(Appointment appointment, RadMenuItem menuItem)
		{
			AppointmentContextMenuItemClickedEventHandler appointmentContextMenuItemClickedEventHandler = (AppointmentContextMenuItemClickedEventHandler)base.Events[RadScheduler.AppointmentContextMenuItemClickedEvent];
			AppointmentContextMenuItemClickedEventArgs e = new AppointmentContextMenuItemClickedEventArgs(appointment, menuItem);
			if (appointmentContextMenuItemClickedEventHandler != null)
			{
				appointmentContextMenuItemClickedEventHandler(this, e);
			}
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x000E0F64 File Offset: 0x000DF164
		protected virtual bool OnTimeSlotContextMenuItemClicking(ISchedulerTimeSlot timeSlot, RadMenuItem menuItem, ISchedulerTimeSlot startSlot, ISchedulerTimeSlot endSlot)
		{
			TimeSlotContextMenuItemClickingEventHandler timeSlotContextMenuItemClickingEventHandler = (TimeSlotContextMenuItemClickingEventHandler)base.Events[RadScheduler.TimeSlotContextMenuItemClickingEvent];
			TimeSlotContextMenuItemClickingEventArgs timeSlotContextMenuItemClickingEventArgs = new TimeSlotContextMenuItemClickingEventArgs(timeSlot, menuItem, startSlot, endSlot);
			if (timeSlotContextMenuItemClickingEventHandler != null)
			{
				timeSlotContextMenuItemClickingEventHandler(this, timeSlotContextMenuItemClickingEventArgs);
			}
			return !timeSlotContextMenuItemClickingEventArgs.Cancel;
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x000E0FA8 File Offset: 0x000DF1A8
		protected virtual void OnTimeSlotContextMenuItemClicked(ISchedulerTimeSlot timeSlot, RadMenuItem menuItem, ISchedulerTimeSlot startSlot, ISchedulerTimeSlot endSlot)
		{
			TimeSlotContextMenuItemClickedEventHandler timeSlotContextMenuItemClickedEventHandler = (TimeSlotContextMenuItemClickedEventHandler)base.Events[RadScheduler.TimeSlotContextMenuItemClickedEvent];
			TimeSlotContextMenuItemClickedEventArgs e = new TimeSlotContextMenuItemClickedEventArgs(timeSlot, menuItem, startSlot, endSlot);
			if (timeSlotContextMenuItemClickedEventHandler != null)
			{
				timeSlotContextMenuItemClickedEventHandler(this, e);
			}
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x000E0FE4 File Offset: 0x000DF1E4
		protected internal virtual bool OnAppointmentInsert(AppointmentInsertEventArgs args)
		{
			AppointmentInsertEventHandler appointmentInsertEventHandler = (AppointmentInsertEventHandler)base.Events[RadScheduler.AppointmentInsertEvent];
			if (appointmentInsertEventHandler != null)
			{
				appointmentInsertEventHandler(this, args);
			}
			return !args.Cancel;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000E101C File Offset: 0x000DF21C
		protected internal virtual bool OnAppointmentUpdate(AppointmentUpdateEventArgs args)
		{
			AppointmentUpdateEventHandler appointmentUpdateEventHandler = (AppointmentUpdateEventHandler)base.Events[RadScheduler.AppointmentUpdateEvent];
			if (appointmentUpdateEventHandler != null)
			{
				appointmentUpdateEventHandler(this, args);
			}
			this._updateOperationCanceled = args.Cancel;
			return !args.Cancel;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x000E1060 File Offset: 0x000DF260
		protected internal virtual bool OnAppointmentDelete(AppointmentDeleteEventArgs args)
		{
			AppointmentDeleteEventHandler appointmentDeleteEventHandler = (AppointmentDeleteEventHandler)base.Events[RadScheduler.AppointmentDeleteEvent];
			if (appointmentDeleteEventHandler != null)
			{
				appointmentDeleteEventHandler(this, args);
			}
			return !args.Cancel;
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x000E1098 File Offset: 0x000DF298
		protected virtual void OnAppointmentClick(Appointment clickedAppointment)
		{
			AppointmentClickEventHandler appointmentClickEventHandler = (AppointmentClickEventHandler)base.Events[RadScheduler.AppointmentClickEvent];
			SchedulerEventArgs e = new SchedulerEventArgs(clickedAppointment);
			if (appointmentClickEventHandler != null)
			{
				appointmentClickEventHandler(this, e);
			}
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x000E10D0 File Offset: 0x000DF2D0
		protected virtual bool OnSchedulerNavigationCommand(SchedulerNavigationCommand command)
		{
			SchedulerNavigationCommandEventArgs args = new SchedulerNavigationCommandEventArgs(command);
			return this.RaiseNavigationCommandEvent(args);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x000E10EC File Offset: 0x000DF2EC
		protected internal virtual bool OnSchedulerNavigationCommand(SchedulerNavigationCommand command, DateTime selectedDate)
		{
			SchedulerNavigationCommandEventArgs args = new SchedulerNavigationCommandEventArgs(command, selectedDate);
			return this.RaiseNavigationCommandEvent(args);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x000E1108 File Offset: 0x000DF308
		protected internal virtual void OnSchedulerNavigationComplete(SchedulerNavigationCommand command)
		{
			SchedulerNavigationCompleteEventHandler schedulerNavigationCompleteEventHandler = (SchedulerNavigationCompleteEventHandler)base.Events[RadScheduler.SchedulerNavigationCompleteEvent];
			if (schedulerNavigationCompleteEventHandler != null)
			{
				schedulerNavigationCompleteEventHandler(this, new SchedulerNavigationCompleteEventArgs(command));
			}
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x000E113C File Offset: 0x000DF33C
		protected virtual bool OnFormCreating(Appointment appointment, SchedulerFormMode mode)
		{
			SchedulerFormCreatingEventHandler schedulerFormCreatingEventHandler = (SchedulerFormCreatingEventHandler)base.Events[RadScheduler.FormCreatingEvent];
			SchedulerFormCreatingEventArgs schedulerFormCreatingEventArgs = new SchedulerFormCreatingEventArgs(appointment, mode);
			if (schedulerFormCreatingEventHandler != null)
			{
				schedulerFormCreatingEventHandler(this, schedulerFormCreatingEventArgs);
			}
			return !schedulerFormCreatingEventArgs.Cancel;
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x000E117C File Offset: 0x000DF37C
		protected virtual void OnFormCreated(Appointment appointment, SchedulerFormContainer container)
		{
			SchedulerFormCreatedEventHandler schedulerFormCreatedEventHandler = (SchedulerFormCreatedEventHandler)base.Events[RadScheduler.FormCreatedEvent];
			SchedulerFormCreatedEventArgs e = new SchedulerFormCreatedEventArgs(appointment, container);
			if (schedulerFormCreatedEventHandler != null)
			{
				schedulerFormCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x000E11B4 File Offset: 0x000DF3B4
		protected virtual bool OnAppointmentCancelingEdit(Appointment appointment, SchedulerFormContainer container)
		{
			AppointmentCancelingEditEventHandler appointmentCancelingEditEventHandler = (AppointmentCancelingEditEventHandler)base.Events[RadScheduler.AppointmentCancelingEditEvent];
			AppointmentCancelingEditEventArgs appointmentCancelingEditEventArgs = new AppointmentCancelingEditEventArgs(appointment, container);
			if (appointmentCancelingEditEventHandler != null)
			{
				appointmentCancelingEditEventHandler(this, appointmentCancelingEditEventArgs);
			}
			return !appointmentCancelingEditEventArgs.Cancel;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x000E11F4 File Offset: 0x000DF3F4
		protected virtual void OnTimeSlotCreated(ISchedulerTimeSlot timeSlot)
		{
			TimeSlotCreatedEventHandler timeSlotCreatedEventHandler = (TimeSlotCreatedEventHandler)base.Events[RadScheduler.TimeSlotCreatedEvent];
			if (timeSlotCreatedEventHandler != null)
			{
				TimeSlotCreatedEventArgs e = new TimeSlotCreatedEventArgs(timeSlot);
				timeSlotCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x000E122C File Offset: 0x000DF42C
		protected internal virtual bool OnOccurrenceDelete(Appointment masterAppointment, Appointment occurrenceAppointment)
		{
			OccurrenceDeleteEventHandler occurrenceDeleteEventHandler = (OccurrenceDeleteEventHandler)base.Events[RadScheduler.OccurrenceDeleteEvent];
			OccurrenceDeleteEventArgs occurrenceDeleteEventArgs = new OccurrenceDeleteEventArgs(masterAppointment, occurrenceAppointment);
			if (occurrenceDeleteEventHandler != null)
			{
				occurrenceDeleteEventHandler(this, occurrenceDeleteEventArgs);
			}
			return !occurrenceDeleteEventArgs.Cancel;
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x000E126C File Offset: 0x000DF46C
		protected internal virtual bool OnRecurrenceExceptionCreated(Appointment masterAppointment, Appointment exceptionAppointment, Appointment occurrenceAppointment)
		{
			RecurrenceExceptionCreatedEventHandler recurrenceExceptionCreatedEventHandler = (RecurrenceExceptionCreatedEventHandler)base.Events[RadScheduler.RecurrenceExceptionCreatedEvent];
			RecurrenceExceptionCreatedEventArgs recurrenceExceptionCreatedEventArgs = new RecurrenceExceptionCreatedEventArgs(masterAppointment, exceptionAppointment, occurrenceAppointment);
			if (recurrenceExceptionCreatedEventHandler != null)
			{
				recurrenceExceptionCreatedEventHandler(this, recurrenceExceptionCreatedEventArgs);
			}
			return !recurrenceExceptionCreatedEventArgs.Cancel;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000E12AC File Offset: 0x000DF4AC
		protected virtual void OnResourceHeaderCreated(SchedulerResourceContainer resourceContainer)
		{
			ResourceHeaderCreatedEventHandler resourceHeaderCreatedEventHandler = (ResourceHeaderCreatedEventHandler)base.Events[RadScheduler.ResourceHeaderCreatedEvent];
			if (resourceHeaderCreatedEventHandler != null)
			{
				ResourceHeaderCreatedEventArgs e = new ResourceHeaderCreatedEventArgs(resourceContainer);
				resourceHeaderCreatedEventHandler(this, e);
			}
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x000E12E4 File Offset: 0x000DF4E4
		protected internal virtual bool OnResourcesPopulating(ResourcesPopulatingEventArgs args)
		{
			ResourcesPopulatingEventHandler resourcesPopulatingEventHandler = (ResourcesPopulatingEventHandler)base.Events[RadScheduler.ResourcesPopulatingEvent];
			if (resourcesPopulatingEventHandler != null)
			{
				resourcesPopulatingEventHandler(this, args);
			}
			return !args.Cancel;
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x000E131C File Offset: 0x000DF51C
		protected internal virtual bool OnAppointmentsPopulating(AppointmentsPopulatingEventArgs args)
		{
			AppointmentsPopulatingEventHandler appointmentsPopulatingEventHandler = (AppointmentsPopulatingEventHandler)base.Events[RadScheduler.AppointmentsPopulatingEvent];
			if (appointmentsPopulatingEventHandler != null)
			{
				appointmentsPopulatingEventHandler(this, args);
			}
			return !args.Cancel;
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x000E1354 File Offset: 0x000DF554
		void ICallbackCommandContext.OnReminderSnooze(ReminderSnoozeEventArgs args)
		{
			ReminderSnoozeEventHandler reminderSnoozeEventHandler = (ReminderSnoozeEventHandler)base.Events[RadScheduler.ReminderSnoozeEvent];
			if (reminderSnoozeEventHandler != null)
			{
				reminderSnoozeEventHandler(this, args);
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000E1384 File Offset: 0x000DF584
		bool ICallbackCommandContext.OnReminderDismiss(ReminderDismissEventArgs args)
		{
			ReminderDismissEventHandler reminderDismissEventHandler = (ReminderDismissEventHandler)base.Events[RadScheduler.ReminderDismissEvent];
			if (reminderDismissEventHandler != null)
			{
				reminderDismissEventHandler(this, args);
			}
			return !args.Cancel;
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000E13BC File Offset: 0x000DF5BC
		void ICallbackCommandContext.SlotAppointments(IList<Appointment> appointments)
		{
			this._callbackAppointments.Clear();
			foreach (Appointment item in appointments)
			{
				this._callbackAppointments.Add(item);
			}
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000E1414 File Offset: 0x000DF614
		internal void CallOnPdfExporting(SchedulerPdfExportingEventArgs e)
		{
			this.OnPdfExporting(e);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000E1420 File Offset: 0x000DF620
		protected virtual void OnPdfExporting(SchedulerPdfExportingEventArgs args)
		{
			SchedulerPdfExportingEventHandler schedulerPdfExportingEventHandler = (SchedulerPdfExportingEventHandler)base.Events[RadScheduler.PdfExportingEvent];
			if (schedulerPdfExportingEventHandler != null)
			{
				schedulerPdfExportingEventHandler(this, args);
			}
			this.TrackPdfExport();
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000E145B File Offset: 0x000DF65B
		protected virtual void TrackPdfExport()
		{
			Tracker.TrackFeature(new FeatureSignature().OfInstance(this).OfName(() => "ExportToPDF"));
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000E1490 File Offset: 0x000DF690
		public void ExportToPdf()
		{
			this.PrepareForExport();
			SchedulerExporter schedulerExporter = new SchedulerExporter(this);
			schedulerExporter.ExportToPdf();
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x000E14B0 File Offset: 0x000DF6B0
		private void PrepareForExport()
		{
			this.TimeSlotContextMenus.Clear();
			this.TimeSlotContextMenuSettings.EnableDefault = false;
			this.AppointmentContextMenuSettings.EnableDefault = false;
			this.AppointmentContextMenus.Clear();
			this.EnableDatePicker = false;
			this.OverflowBehavior = OverflowBehavior.Expand;
			this.ShouldBindFormTemplate = true;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000E1500 File Offset: 0x000DF700
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureChildControls();
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			SchedulerClientState schedulerClientState = javaScriptSerializer.Deserialize<SchedulerClientState>(text);
			this.ScrollTop = schedulerClientState.ScrollTop;
			this.ScrollLeft = schedulerClientState.ScrollLeft;
			if (schedulerClientState.IsDirty)
			{
				this.Rebind();
				this.DataBind();
			}
			return false;
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x000E1568 File Offset: 0x000DF768
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			this.EnsureDataBound();
			this.ActiveModel.DataBind(this.VisibleAppointments);
			SchedulerPostBackEvent postBack = SchedulerPostBackEvent.DeserializeFromJSON(eventArgument, this);
			this.ProcessPostBackCommand(postBack);
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000E15A0 File Offset: 0x000DF7A0
		internal void ProcessPostBackCommand(SchedulerPostBackEvent postBack)
		{
			Appointment appointment = this.Appointments.FindByID(postBack.AppointmentID);
			this.EditingRecurringSeries = postBack.EditSeries;
			this.CurrentSlotWidth = postBack.SlotWidth;
			this.CurrentSlotHeight = postBack.SlotHeight;
			switch (postBack.Command)
			{
			case SchedulerPostBackCommand.InsertAppointment:
			{
				AppointmentInsertEventArgs appointmentInsertEventArgs = new AppointmentInsertEventArgs(postBack.Appointment, new SchedulerInfo(this));
				if (this.OnAppointmentInsert(appointmentInsertEventArgs))
				{
					this.AppointmentController.InsertAppointment(appointmentInsertEventArgs.SchedulerInfo, postBack.Appointment);
					this.Rebind();
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.Resize:
			case SchedulerPostBackCommand.Move:
			case SchedulerPostBackCommand.MoveToAllDay:
			case SchedulerPostBackCommand.AdvancedInsertRecurring:
				goto IL_69C;
			case SchedulerPostBackCommand.Edit:
				if (appointment != null)
				{
					this.OnAppointmentClick(appointment);
					this.EditAppointment(appointment);
					return;
				}
				return;
			case SchedulerPostBackCommand.Delete:
			{
				Appointment appointmentToDelete = this.Appointments.FindByID(postBack.AppointmentID);
				this.DeleteAppointment(appointmentToDelete);
				return;
			}
			case SchedulerPostBackCommand.Click:
				if (appointment != null)
				{
					this.OnAppointmentClick(appointment);
					return;
				}
				return;
			case SchedulerPostBackCommand.GoToPrevious:
				if (appointment != null && this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.DisplayPreviousAppointmentSegment))
				{
					this.ClearChildControls();
					this.DisplayPreviousAppointmentSegment(appointment);
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.DisplayPreviousAppointmentSegment);
					return;
				}
				return;
			case SchedulerPostBackCommand.GoToNext:
				if (appointment != null && this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.DisplayNextAppointmentSegment))
				{
					this.ClearChildControls();
					this.DisplayNextAppointmentSegment(appointment);
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.DisplayNextAppointmentSegment);
					return;
				}
				return;
			case SchedulerPostBackCommand.GoToToday:
			{
				DateTime selectedDate = this.VisualToday;
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToSelectedDay, selectedDate))
				{
					this.SelectedDate = selectedDate;
					this.RevertToDefaultState();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToSelectedDay);
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.SwitchFullTime:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchFullTime))
				{
					this.ClearChildControls();
					this.SwitchFullTime();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchFullTime);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToDayView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToDayView))
				{
					this.ClearChildControls();
					this.SwitchToDayView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToDayView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToWeekView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToWeekView))
				{
					this.ClearChildControls();
					this.SwitchToWeekView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToWeekView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToMonthView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToMonthView))
				{
					this.ClearChildControls();
					this.SwitchToMonthView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToMonthView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToTimelineView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToTimelineView))
				{
					this.ClearChildControls();
					this.SwitchToTimelineView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToTimelineView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToMultiDayView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToMultiDayView))
				{
					this.ClearChildControls();
					this.SwitchToMultiDayView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToMultiDayView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToAgendaView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToAgendaView))
				{
					this.ClearChildControls();
					this.SwitchToAgendaView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToAgendaView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToYearView:
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToYearView))
				{
					this.ClearChildControls();
					this.SwitchToYearView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToYearView);
					return;
				}
				return;
			case SchedulerPostBackCommand.SwitchToSelectedDay:
			{
				DateTime selectedDate = this.UtcToDisplay(postBack.StartDateParsed).Date;
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToSelectedDay, selectedDate))
				{
					this.SelectedDate = selectedDate;
					this.SwitchToDayView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToSelectedDay);
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.SwitchToSelectedMonth:
			{
				DateTime selectedDate = this.UtcToDisplay(postBack.StartDateParsed).Date;
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.SwitchToSelectedMonth, selectedDate))
				{
					this.SelectedDate = selectedDate;
					this.SwitchToMonthView();
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.SwitchToSelectedMonth);
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.NavigateToNextPeriod:
			{
				DateTime nextPeriodDate = this.ActiveModel.NextPeriodDate;
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.NavigateToNextPeriod, nextPeriodDate))
				{
					this.SelectedDate = nextPeriodDate;
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.NavigateToNextPeriod);
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.NavigateToPreviousPeriod:
			{
				DateTime previousPeriodDate = this.ActiveModel.PreviousPeriodDate;
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.NavigateToPreviousPeriod, previousPeriodDate))
				{
					this.SelectedDate = previousPeriodDate;
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.NavigateToPreviousPeriod);
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.NavigateToSelectedDate:
			{
				DateTime date = this.UtcToDisplay(postBack.StartDateParsed).Date;
				if (this.OnSchedulerNavigationCommand(SchedulerNavigationCommand.NavigateToSelectedDate, date))
				{
					this.SelectedDate = date;
					this.Rebind();
					this.OnSchedulerNavigationComplete(SchedulerNavigationCommand.NavigateToSelectedDate);
					return;
				}
				return;
			}
			case SchedulerPostBackCommand.UpdateAppointment:
			{
				Appointment appointment2 = this.AppointmentController.PrepareToEdit(appointment, postBack.EditSeries);
				Appointment appointment3 = appointment2.Clone();
				appointment3.Start -= appointment.Start - postBack.Appointment.Start;
				appointment3.End -= appointment.End - postBack.Appointment.End;
				appointment3.Subject = postBack.Appointment.Subject;
				appointment3.Resources.Clear();
				foreach (object obj in postBack.Appointment.Resources)
				{
					Resource item = (Resource)obj;
					appointment3.Resources.Add(item);
				}
				this.FinishResize(appointment2, appointment3);
				return;
			}
			case SchedulerPostBackCommand.AdvancedInsert:
				this.SwitchToInsertMode(this.UtcToDisplay(postBack.Appointment.Start), this.UtcToDisplay(postBack.Appointment.End), true);
				this.ActiveFormAppointment.Subject = postBack.Appointment.Subject;
				using (IEnumerator enumerator2 = postBack.Appointment.Resources.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						Resource item2 = (Resource)obj2;
						this.ActiveFormAppointment.Resources.Add(item2);
					}
					return;
				}
				break;
			case SchedulerPostBackCommand.AdvancedEdit:
				break;
			case SchedulerPostBackCommand.ContextMenuDelete:
				this.HandleAppointmentContextMenuClick(postBack, new Action<Appointment>(this.DeleteAppointment));
				return;
			case SchedulerPostBackCommand.ContextMenuEdit:
				this.HandleAppointmentContextMenuClick(postBack, new Action<Appointment>(this.EditAppointment));
				return;
			case SchedulerPostBackCommand.ContextMenuTimeSlotCommand:
			{
				ISchedulerTimeSlot slotByIndex = this.ActiveModel.GetSlotByIndex(postBack.TargetSlotIndex);
				ISchedulerTimeSlot slotByIndex2 = this.ActiveModel.GetSlotByIndex(postBack.TargetSlotIndex);
				ISchedulerTimeSlot slotByIndex3 = this.ActiveModel.GetSlotByIndex(postBack.LastSlotIndex);
				RadSchedulerContextMenu radSchedulerContextMenu = this.TimeSlotContextMenus.FindByClientId(postBack.ContextMenuID);
				RadMenuItem menuItem = (RadMenuItem)radSchedulerContextMenu.FindItemByHierarchicalIndex(postBack.MenuItemIndex);
				if (!this.OnTimeSlotContextMenuItemClicking(slotByIndex, menuItem, slotByIndex2, slotByIndex3))
				{
					return;
				}
				this.ProcessTimeSlotContextMenuItemCommand(postBack);
				this.OnTimeSlotContextMenuItemClicked(slotByIndex, menuItem, slotByIndex2, slotByIndex3);
				return;
			}
			case SchedulerPostBackCommand.ContextMenuAppointmentCommand:
				this.HandleAppointmentContextMenuClick(postBack, delegate(Appointment apt)
				{
				});
				return;
			default:
				goto IL_69C;
			}
			Appointment appointment4 = appointment.Clone();
			appointment4.Subject = postBack.Appointment.Subject;
			this.SwitchToEditMode(appointment4, postBack.EditSeries, true);
			return;
			IL_69C:
			this.ActiveModel.ProcessPostBackCommand(postBack);
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000E1C74 File Offset: 0x000DFE74
		private void ProcessTimeSlotContextMenuItemCommand(SchedulerPostBackEvent postBack)
		{
			Dictionary<string, ContextMenuAction> timeSlotContextMenuCommands = this.ActiveModel.GetTimeSlotContextMenuCommands();
			ContextMenuAction contextMenuAction = null;
			if (timeSlotContextMenuCommands.TryGetValue(postBack.ContextMenuCommandName, out contextMenuAction))
			{
				contextMenuAction(this.ActiveModel, postBack);
			}
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000E1CAC File Offset: 0x000DFEAC
		private void HandleAppointmentContextMenuClick(SchedulerPostBackEvent postBack, Action<Appointment> handler)
		{
			Appointment appointment = this.Appointments.FindByID(postBack.AppointmentID);
			RadSchedulerContextMenu radSchedulerContextMenu = this.AppointmentContextMenus.FindByClientId(postBack.ContextMenuID);
			RadMenuItem menuItem = (RadMenuItem)radSchedulerContextMenu.FindItemByHierarchicalIndex(postBack.MenuItemIndex);
			if (!this.OnAppointmentContextMenuItemClicking(appointment, menuItem))
			{
				return;
			}
			handler(appointment);
			this.OnAppointmentContextMenuItemClicked(appointment, menuItem);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000E1D09 File Offset: 0x000DFF09
		private void DeleteAppointment(Appointment appointmentToDelete)
		{
			if (this.ActiveModel.ReadOnly || appointmentToDelete == null)
			{
				return;
			}
			this._appointmentController.DeleteAppointment(new SchedulerInfo(this), appointmentToDelete, this.EditingRecurringSeries);
			this.Rebind();
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x000E1D3C File Offset: 0x000DFF3C
		private void EditAppointment(Appointment originalAppointment)
		{
			if (this.ActiveModel.ReadOnly)
			{
				return;
			}
			bool useAdvancedForm = this.AdvancedForm.Enabled && this.StartEditingInAdvancedForm;
			this.SwitchToEditMode(originalAppointment, this.EditingRecurringSeries, useAdvancedForm);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x000E1D7C File Offset: 0x000DFF7C
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x000E1D88 File Offset: 0x000DFF88
		public string GetCallbackResult()
		{
			if (this._callbackAppointments.Count > 0)
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer
				{
					MaxJsonLength = int.MaxValue
				};
				javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
				{
					new AppointmentConverter(this)
				});
				return javaScriptSerializer.Serialize(this._callbackAppointments);
			}
			return "OK";
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x000E1DE0 File Offset: 0x000DFFE0
		public void RaiseCallbackEvent(string eventArgument)
		{
			this.EnsureDataBound();
			foreach (ICallbackCommand callbackCommand in CallbackDeserializer.DeserializeCommands(eventArgument))
			{
				callbackCommand.Execute(this);
			}
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x000E1E34 File Offset: 0x000E0034
		private void LoadAppointmentProvider()
		{
			RadSchedulerConfigurationSection radSchedulerConfigurationSection = (RadSchedulerConfigurationSection)WebConfigurationManager.GetSection("telerik.web.ui/radScheduler");
			this.ProviderName = ((radSchedulerConfigurationSection != null) ? radSchedulerConfigurationSection.DefaultAppointmentProvider : "Integrated");
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x000E1E68 File Offset: 0x000E0068
		private void PerformDataBindingFromProvider(IEnumerable<Appointment> providedAppointments)
		{
			if (this.DesignMode || providedAppointments == null)
			{
				return;
			}
			this.Appointments.Clear();
			foreach (Appointment appointment in providedAppointments)
			{
				appointment.Validate();
				this._appointmentController.AddAppointmentAndExpand(appointment);
			}
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x000E1ED4 File Offset: 0x000E00D4
		private void BindResourcesFromProvider(IDictionary<ResourceType, IEnumerable<Resource>> providedResources)
		{
			if (providedResources == null)
			{
				return;
			}
			this.ResourceTypes.Clear();
			this.Resources.Clear();
			foreach (ResourceType resourceType in providedResources.Keys)
			{
				if (string.IsNullOrEmpty(resourceType.Name))
				{
					throw new InvalidOperationException("Cannot register resource type with an empty name.");
				}
				ResourceType resourceType2 = new ResourceType();
				resourceType2.Name = resourceType.Name;
				resourceType2.AllowMultipleValues = resourceType.AllowMultipleValues;
				this.ResourceTypes.Add(resourceType2);
				foreach (Resource item in providedResources[resourceType])
				{
					this.Resources.Add(item);
				}
			}
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x000E1FC4 File Offset: 0x000E01C4
		private void BindResourcesFromDataSource()
		{
			foreach (object obj in this.ResourceTypes)
			{
				ResourceType resourceType = (ResourceType)obj;
				if (resourceType.DataSource != null || !string.IsNullOrEmpty(resourceType.DataSourceID))
				{
					foreach (Resource item in this.Resources.GetResourcesByType(resourceType.Name))
					{
						this.Resources.Remove(item);
					}
					ResourceTypeControl resourceTypeControl = new ResourceTypeControl(resourceType);
					resourceTypeControl.ID = resourceType.Name;
					resourceTypeControl.DataSourceID = resourceType.DataSourceID;
					resourceTypeControl.DataSource = resourceType.DataSource;
					this.Controls.Add(resourceTypeControl);
					resourceTypeControl.DataBound += this.resourceTypeControl_DataBound;
					resourceTypeControl.DataBind();
					this._resourceTypeLoaded.WaitOne();
					resourceTypeControl.DataBound -= this.resourceTypeControl_DataBound;
					this.Controls.Remove(resourceTypeControl);
				}
			}
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000E2104 File Offset: 0x000E0304
		private void BindResourcesFromWebService()
		{
			SchedulerWebServiceClient webClient = this.GetWebClient();
			Dictionary<string, ResourceType> dictionary = new Dictionary<string, ResourceType>();
			foreach (Resource resource in webClient.GetResources())
			{
				if (!dictionary.ContainsKey(resource.Type))
				{
					dictionary.Add(resource.Type, new ResourceType(resource.Type));
				}
				this.Resources.Add(resource);
			}
			if (this.ResourceTypes.Count == 0)
			{
				this.ResourceTypes.AddRange(dictionary.Values);
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000E21A8 File Offset: 0x000E03A8
		private SchedulerWebServiceClient GetWebClient()
		{
			if (this.WebServiceSettings.IsOData)
			{
				return new ODataWebServiceClient(this);
			}
			return new SchedulerWebServiceClient(this);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000E21C4 File Offset: 0x000E03C4
		private void resourceTypeControl_DataBound(object sender, EventArgs e)
		{
			this._resourceTypeLoaded.Set();
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000E21D2 File Offset: 0x000E03D2
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			if (!this._ignoreDataSourceViewChanged)
			{
				base.RequiresDataBinding = true;
				this._dataPropertyChanged = true;
			}
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000E21EC File Offset: 0x000E03EC
		protected override void PerformSelect()
		{
			if (this.DesignMode)
			{
				return;
			}
			if (this.DataSourceID.Length == 0)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			this._ignoreDataSourceViewChanged = true;
			this._shouldBindAppointmentControls = true;
			base.RequiresDataBinding = false;
			base.MarkAsDataBound();
			this.BindResources();
			this.BindAppointments();
			this._ignoreDataSourceViewChanged = false;
			this.OnDataBound(EventArgs.Empty);
			this.ClearDataItems();
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000E225C File Offset: 0x000E045C
		private void BindResources()
		{
			if (this.UsingWebServiceBinding)
			{
				if (this.WebServiceSettings.ResourcePopulationMode == SchedulerResourcePopulationMode.ServerSide)
				{
					this.BindResourcesFromWebService();
					return;
				}
			}
			else
			{
				this.Provider.LegacyOwner = this;
				ResourcesPopulatingEventArgs resourcesPopulatingEventArgs = new ResourcesPopulatingEventArgs(new SchedulerInfo(this));
				if (this.OnResourcesPopulating(resourcesPopulatingEventArgs))
				{
					if (this.HasCustomProvider)
					{
						this.BindResourcesFromProvider(this.Provider.GetResources(resourcesPopulatingEventArgs.SchedulerInfo));
						return;
					}
					this.BindResourcesFromDataSource();
				}
			}
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000E22D0 File Offset: 0x000E04D0
		private void BindAppointments()
		{
			if (this.UsingWebServiceBinding)
			{
				return;
			}
			this.Provider.LegacyOwner = this;
			AppointmentsPopulatingEventArgs appointmentsPopulatingEventArgs = new AppointmentsPopulatingEventArgs(new SchedulerInfo(this));
			if (this.OnAppointmentsPopulating(appointmentsPopulatingEventArgs))
			{
				this.PerformDataBindingFromProvider(this.Provider.GetAppointments(appointmentsPopulatingEventArgs.SchedulerInfo));
			}
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000E2320 File Offset: 0x000E0520
		private void ClearDataItems()
		{
			foreach (Appointment appointment in this.Appointments)
			{
				appointment.DataItem = null;
			}
			foreach (object obj in this.Resources)
			{
				Resource resource = (Resource)obj;
				resource.DataItem = null;
			}
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000E23B8 File Offset: 0x000E05B8
		public void Rebind()
		{
			this.ClearChildControls();
			this.ActiveFormMode = SchedulerFormMode.Hidden;
			this.OnDataPropertyChanged();
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000E23CD File Offset: 0x000E05CD
		protected override void EnsureDataBound()
		{
			base.EnsureDataBound();
			if (base.RequiresDataBinding && (this.HasCustomProvider || !base.IsBoundUsingDataSourceID))
			{
				this.DataBind();
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x000E23F3 File Offset: 0x000E05F3
		public bool TimeZonesEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.TimeZoneID);
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x000E2403 File Offset: 0x000E0603
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AppointmentCollection Appointments
		{
			get
			{
				if (this._appointments == null)
				{
					this._appointments = new AppointmentCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._appointments).TrackViewState();
					}
				}
				return this._appointments;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x000E2432 File Offset: 0x000E0632
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TimeZoneProviderBase TimeZonesProvider
		{
			get
			{
				return this._timeZoneProvider;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06004781 RID: 18305 RVA: 0x000E243A File Offset: 0x000E063A
		// (set) Token: 0x06004782 RID: 18306 RVA: 0x000E2455 File Offset: 0x000E0655
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IAppointmentFactory AppointmentFactory
		{
			get
			{
				if (this._appointmentFactory == null)
				{
					this._appointmentFactory = new DefaultAppointmentFactory();
				}
				return this._appointmentFactory;
			}
			set
			{
				this._appointmentFactory = value;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x000E245E File Offset: 0x000E065E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ResourceCollection Resources
		{
			get
			{
				if (this.DesignMode)
				{
					return this.CreateSampleResources();
				}
				if (this._resources == null)
				{
					this._resources = new ResourceCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._resources).TrackViewState();
					}
				}
				return this._resources;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x000E249C File Offset: 0x000E069C
		// (set) Token: 0x06004785 RID: 18309 RVA: 0x000E24CC File Offset: 0x000E06CC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime VisibleRangeStart
		{
			get
			{
				DateTime? visibleRangeStart = this._visibleRangeStart;
				if (visibleRangeStart == null)
				{
					return this.ActiveModel.VisibleRangeStart;
				}
				return visibleRangeStart.GetValueOrDefault();
			}
			internal set
			{
				this._visibleRangeStart = new DateTime?(value);
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x000E24DC File Offset: 0x000E06DC
		// (set) Token: 0x06004787 RID: 18311 RVA: 0x000E250C File Offset: 0x000E070C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime VisibleRangeEnd
		{
			get
			{
				DateTime? visibleRangeEnd = this._visibleRangeEnd;
				if (visibleRangeEnd == null)
				{
					return this.ActiveModel.VisibleRangeEnd;
				}
				return visibleRangeEnd.GetValueOrDefault();
			}
			internal set
			{
				this._visibleRangeEnd = new DateTime?(value);
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x000E251A File Offset: 0x000E071A
		// (set) Token: 0x06004789 RID: 18313 RVA: 0x000E253B File Offset: 0x000E073B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EditingRecurringSeries
		{
			get
			{
				return (bool)(this.ViewState["EditingRecurringSeries"] ?? false);
			}
			internal set
			{
				this.ViewState["EditingRecurringSeries"] = value;
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x000E2554 File Offset: 0x000E0754
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool RecurrenceSupport
		{
			get
			{
				bool flag = this.DataRecurrenceField != string.Empty && this.DataRecurrenceParentKeyField != string.Empty;
				return this.EnableRecurrenceSupport && (this.HasCustomProvider || flag || this.UsingWebServiceBinding);
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x0600478B RID: 18315 RVA: 0x000E25A4 File Offset: 0x000E07A4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool RemindersSupport
		{
			get
			{
				bool flag = this.DataReminderField != string.Empty;
				return this.Reminders.Enabled && (this.HasCustomProvider || flag || this.UsingWebServiceBinding);
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000E25E4 File Offset: 0x000E07E4
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x000E260D File Offset: 0x000E080D
		[SimplePersistenceSetting]
		[DefaultValue(SchedulerViewType.DayView)]
		[Description("The selected view type")]
		[ClientControlProperty]
		[ClientPropertyName("selectedView")]
		[Category("Layout")]
		public SchedulerViewType SelectedView
		{
			get
			{
				object obj = this.ViewState["SelectedView"];
				if (obj == null)
				{
					return SchedulerViewType.DayView;
				}
				return (SchedulerViewType)obj;
			}
			set
			{
				if (this.FormContainer != null)
				{
					this.FormContainer.Mode = SchedulerFormMode.Hidden;
				}
				this.ViewState["SelectedView"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x000E263F File Offset: 0x000E083F
		// (set) Token: 0x0600478F RID: 18319 RVA: 0x000E265F File Offset: 0x000E085F
		[ClientPropertyName("groupBy")]
		[Category("Layout")]
		[ClientControlProperty]
		[Description("The name of the resource type to group by.")]
		[DefaultValue("")]
		public string GroupBy
		{
			get
			{
				return (string)(this.ViewState["GroupBy"] ?? string.Empty);
			}
			set
			{
				this.ViewState["GroupBy"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x000E2678 File Offset: 0x000E0878
		[ClientControlProperty]
		[ClientPropertyName("_uniqueId")]
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x000E2680 File Offset: 0x000E0880
		// (set) Token: 0x06004792 RID: 18322 RVA: 0x000E268D File Offset: 0x000E088D
		[Description("Enables the advanced insert/edit form.")]
		[Obsolete("Obsoleted. Please, use AdvancedForm-Enabled instead.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool EnableAdvancedForm
		{
			get
			{
				return this.AdvancedForm.Enabled;
			}
			set
			{
				this.AdvancedForm.Enabled = value;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x000E269B File Offset: 0x000E089B
		// (set) Token: 0x06004794 RID: 18324 RVA: 0x000E26BC File Offset: 0x000E08BC
		[Description("Sets the default edit mode")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientPropertyName("_startEditingInAdvancedForm")]
		public bool StartEditingInAdvancedForm
		{
			get
			{
				return (bool)(this.ViewState["StartEditingInAdvancedForm"] ?? true);
			}
			set
			{
				this.ViewState["StartEditingInAdvancedForm"] = value;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x000E26D4 File Offset: 0x000E08D4
		// (set) Token: 0x06004796 RID: 18326 RVA: 0x000E26F5 File Offset: 0x000E08F5
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Sets the default insert mode")]
		[ClientPropertyName("_startInsertingInAdvancedForm")]
		public bool StartInsertingInAdvancedForm
		{
			get
			{
				return (bool)(this.ViewState["StartInsertingInAdvancedForm"] ?? false);
			}
			set
			{
				this.ViewState["StartInsertingInAdvancedForm"] = value;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x000E270D File Offset: 0x000E090D
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Advanced form settings")]
		[Category("Layout")]
		public AdvancedFormSettings AdvancedForm
		{
			get
			{
				return this._advancedFormSettings;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x000E2715 File Offset: 0x000E0915
		// (set) Token: 0x06004799 RID: 18329 RVA: 0x000E2736 File Offset: 0x000E0936
		[ClientPropertyName("displayDeleteConfirmation")]
		[Description("Enables the delete confirmation dialog.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool DisplayDeleteConfirmation
		{
			get
			{
				return (bool)(this.ViewState["DisplayDeleteConfirmation"] ?? true);
			}
			set
			{
				this.ViewState["DisplayDeleteConfirmation"] = value;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x000E274E File Offset: 0x000E094E
		// (set) Token: 0x0600479B RID: 18331 RVA: 0x000E276F File Offset: 0x000E096F
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Enables the recurrence confirmation dialog when moving appointments.")]
		[Category("Behavior")]
		[ClientPropertyName("displayRecurrenceActionDialogOnMove")]
		public bool DisplayRecurrenceActionDialogOnMove
		{
			get
			{
				return (bool)(this.ViewState["DisplayRecurrenceActionDialogOnMove"] ?? false);
			}
			set
			{
				this.ViewState["DisplayRecurrenceActionDialogOnMove"] = value;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x0600479C RID: 18332 RVA: 0x000E2788 File Offset: 0x000E0988
		// (set) Token: 0x0600479D RID: 18333 RVA: 0x000E27B1 File Offset: 0x000E09B1
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Make the control read-only.")]
		[ClientControlProperty]
		[ClientPropertyName("readOnly")]
		public bool ReadOnly
		{
			get
			{
				object obj = this.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x0600479E RID: 18334 RVA: 0x000E27C9 File Offset: 0x000E09C9
		// (set) Token: 0x0600479F RID: 18335 RVA: 0x000E27D1 File Offset: 0x000E09D1
		[Description("Gets or sets the ODataDataSource used for data binding.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		public override string ODataDataSourceID
		{
			get
			{
				return base.ODataDataSourceID;
			}
			set
			{
				base.ODataDataSourceID = value;
				this.ReadOnly = true;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x060047A0 RID: 18336 RVA: 0x000E27E1 File Offset: 0x000E09E1
		[Description("The resource types used by RadScheduler")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[Editor("Telerik.Web.Design.ResourceTypeCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", "System.Drawing.Design.UITypeEditor")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ResourceTypeCollection ResourceTypes
		{
			get
			{
				if (this._resourceTypes == null)
				{
					this._resourceTypes = new ResourceTypeCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._resourceTypes).TrackViewState();
					}
				}
				return this._resourceTypes;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x060047A1 RID: 18337 RVA: 0x000E280F File Offset: 0x000E0A0F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The resource types used by RadScheduler")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public ResourceStyleMappingCollection ResourceStyles
		{
			get
			{
				if (this._resourceStyles == null)
				{
					this._resourceStyles = new ResourceStyleMappingCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._resourceStyles).TrackViewState();
					}
				}
				return this._resourceStyles;
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x000E283D File Offset: 0x000E0A3D
		// (set) Token: 0x060047A3 RID: 18339 RVA: 0x000E2862 File Offset: 0x000E0A62
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[Description("The time zone offset to use when displaying appointments.")]
		[Category("Behavior")]
		public TimeSpan TimeZoneOffset
		{
			get
			{
				return (TimeSpan)(this.ViewState["TimeZoneOffset"] ?? TimeSpan.Zero);
			}
			set
			{
				this.ViewState["TimeZoneOffset"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x000E2880 File Offset: 0x000E0A80
		// (set) Token: 0x060047A5 RID: 18341 RVA: 0x000E28A5 File Offset: 0x000E0AA5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public TimeSpan VisualTimeZoneOffset
		{
			get
			{
				return (TimeSpan)(this.ViewState["VisualTimeZoneOffset"] ?? TimeSpan.Zero);
			}
			set
			{
				this.ViewState["VisualTimeZoneOffset"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x000E28C4 File Offset: 0x000E0AC4
		// (set) Token: 0x060047A7 RID: 18343 RVA: 0x000E28F7 File Offset: 0x000E0AF7
		[Description("Currently displayed date in day view or the highlighted day in week and month view")]
		[SimplePersistenceSetting]
		public DateTime SelectedDate
		{
			get
			{
				object obj = this.ViewState["SelectedDate"];
				return (DateTime)(obj ?? this.VisualToday);
			}
			set
			{
				this.ViewState["SelectedDate"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x000E2915 File Offset: 0x000E0B15
		// (set) Token: 0x060047A9 RID: 18345 RVA: 0x000E2936 File Offset: 0x000E0B36
		[Description("Specifies how many rows a time lable should span.")]
		[DefaultValue(2)]
		[ClientControlProperty]
		[Category("Behavior")]
		public int TimeLabelRowSpan
		{
			get
			{
				return (int)(this.ViewState["TimeLabelRowSpan"] ?? 2);
			}
			set
			{
				this.ViewState["TimeLabelRowSpan"] = value;
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x000E2950 File Offset: 0x000E0B50
		// (set) Token: 0x060047AB RID: 18347 RVA: 0x000E297A File Offset: 0x000E0B7A
		[ClientControlProperty]
		[Description("Specifies how many minutes should a row represent in day and week views")]
		[Category("Behavior")]
		[DefaultValue(30)]
		public int MinutesPerRow
		{
			get
			{
				object obj = this.ViewState["MinutesPerRow"];
				if (obj == null)
				{
					return 30;
				}
				return (int)obj;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Please set MinutesPerRow to a value greater than zero.");
				}
				this.ViewState["MinutesPerRow"] = value;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x060047AC RID: 18348 RVA: 0x000E29A4 File Offset: 0x000E0BA4
		// (set) Token: 0x060047AD RID: 18349 RVA: 0x000E29CD File Offset: 0x000E0BCD
		[Category("Behavior")]
		[Description("Specifies the number of rows that are hovered when the mouse is over the appointment area")]
		[ClientControlProperty]
		[DefaultValue(2)]
		public int NumberOfHoveredRows
		{
			get
			{
				object obj = this.ViewState["NumberOfHoveredRows"];
				if (obj == null)
				{
					return 2;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["NumberOfHoveredRows"] = value;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x060047AE RID: 18350 RVA: 0x000E29E8 File Offset: 0x000E0BE8
		// (set) Token: 0x060047AF RID: 18351 RVA: 0x000E2A15 File Offset: 0x000E0C15
		[Category("Behavior")]
		[Description("The start of the day in day and week view")]
		[DefaultValue(typeof(TimeSpan), "08:00:00")]
		public TimeSpan DayStartTime
		{
			get
			{
				object obj = this.ViewState["StartTime"];
				if (obj == null)
				{
					return RadScheduler.Defaults.DayStartTime;
				}
				return (TimeSpan)obj;
			}
			set
			{
				this.ViewState["StartTime"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x060047B0 RID: 18352 RVA: 0x000E2A34 File Offset: 0x000E0C34
		// (set) Token: 0x060047B1 RID: 18353 RVA: 0x000E2A64 File Offset: 0x000E0C64
		[Category("Behavior")]
		[Description("The end of the day in day and week view")]
		[DefaultValue(typeof(TimeSpan), "18:00:00")]
		public TimeSpan DayEndTime
		{
			get
			{
				object obj = this.ViewState["EndTime"];
				if (obj == null)
				{
					return RadScheduler.Defaults.DayEndTime;
				}
				return (TimeSpan)obj;
			}
			set
			{
				TimeSpan timeSpan = value;
				if (timeSpan == TimeSpan.FromHours(0.0))
				{
					timeSpan = TimeSpan.FromHours(24.0);
				}
				this.ViewState["EndTime"] = timeSpan;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x060047B2 RID: 18354 RVA: 0x000E2AB4 File Offset: 0x000E0CB4
		// (set) Token: 0x060047B3 RID: 18355 RVA: 0x000E2AD9 File Offset: 0x000E0CD9
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "8:00")]
		[Description("The start of the business day in day and week view")]
		public TimeSpan WorkDayStartTime
		{
			get
			{
				return (TimeSpan)(this.ViewState["WorkDayStartTime"] ?? RadScheduler.Defaults.WorkDayStartTime);
			}
			set
			{
				this.ViewState["WorkDayStartTime"] = value;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x060047B4 RID: 18356 RVA: 0x000E2AF1 File Offset: 0x000E0CF1
		// (set) Token: 0x060047B5 RID: 18357 RVA: 0x000E2B1A File Offset: 0x000E0D1A
		[DefaultValue(typeof(TimeSpan), "17:00")]
		[Category("Behavior")]
		[Description("The end of the business day in day and week view")]
		public TimeSpan WorkDayEndTime
		{
			get
			{
				return (TimeSpan)(this.ViewState["WorkDayEndTime"] ?? new TimeSpan(17, 0, 0));
			}
			set
			{
				this.ViewState["WorkDayEndTime"] = value;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x060047B6 RID: 18358 RVA: 0x000E2B32 File Offset: 0x000E0D32
		// (set) Token: 0x060047B7 RID: 18359 RVA: 0x000E2B3F File Offset: 0x000E0D3F
		[Obsolete("Obsoleted. Please, use AdvancedForm-EnableResourceEditing instead.")]
		[Category("Behavior")]
		[Description("Controls the visibility of the resource selection drop-downs in the advanced form.")]
		[DefaultValue(true)]
		public bool EnableResourceEditing
		{
			get
			{
				return this.AdvancedForm.EnableResourceEditing;
			}
			set
			{
				this.AdvancedForm.EnableResourceEditing = value;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x000E2B4D File Offset: 0x000E0D4D
		// (set) Token: 0x060047B9 RID: 18361 RVA: 0x000E2B5A File Offset: 0x000E0D5A
		[Category("Behavior")]
		[DefaultValue(false)]
		[Obsolete("Obsoleted. Please, use AdvancedForm-EnableCustomAttributeEditing instead.")]
		[Description("Controls the visibility of the attribute selection text boxes in the advanced form.")]
		public bool EnableCustomAttributeEditing
		{
			get
			{
				return this.AdvancedForm.EnableCustomAttributeEditing;
			}
			set
			{
				this.AdvancedForm.EnableCustomAttributeEditing = value;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x000E2B68 File Offset: 0x000E0D68
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x000E2B89 File Offset: 0x000E0D89
		[Category("Appearance")]
		[ClientControlProperty]
		[DefaultValue(DayOfWeek.Sunday)]
		[Description("The first day in week view")]
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				return (DayOfWeek)(this.ViewState["FirstDayOfWeek"] ?? DayOfWeek.Sunday);
			}
			set
			{
				this.ViewState["FirstDayOfWeek"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x000E2BA7 File Offset: 0x000E0DA7
		// (set) Token: 0x060047BD RID: 18365 RVA: 0x000E2BC8 File Offset: 0x000E0DC8
		[ClientControlProperty]
		[Description("The last day in week view")]
		[Category("Appearance")]
		[DefaultValue(DayOfWeek.Saturday)]
		public DayOfWeek LastDayOfWeek
		{
			get
			{
				return (DayOfWeek)(this.ViewState["LastDayOfWeek"] ?? DayOfWeek.Saturday);
			}
			set
			{
				this.ViewState["LastDayOfWeek"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x000E2BE6 File Offset: 0x000E0DE6
		// (set) Token: 0x060047BF RID: 18367 RVA: 0x000E2C07 File Offset: 0x000E0E07
		[ClientPropertyName("overflowBehavior")]
		[ClientControlProperty]
		[Category("Appearance")]
		[Description("Overflow behavior")]
		[DefaultValue(OverflowBehavior.Scroll)]
		public OverflowBehavior OverflowBehavior
		{
			get
			{
				return (OverflowBehavior)(this.ViewState["OverflowBehavior"] ?? OverflowBehavior.Scroll);
			}
			set
			{
				this.ViewState["OverflowBehavior"] = value;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x000E2C1F File Offset: 0x000E0E1F
		// (set) Token: 0x060047C1 RID: 18369 RVA: 0x000E2C40 File Offset: 0x000E0E40
		[DefaultValue(true)]
		[Description("Controls the visibility of the hours column")]
		[Category("Appearance")]
		public bool ShowHoursColumn
		{
			get
			{
				return (bool)(this.ViewState["ShowHoursColumn"] ?? true);
			}
			set
			{
				this.ViewState["ShowHoursColumn"] = value;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x000E2C58 File Offset: 0x000E0E58
		// (set) Token: 0x060047C3 RID: 18371 RVA: 0x000E2C79 File Offset: 0x000E0E79
		[Description("Controls the visibility of the date headers for the current view")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool ShowDateHeaders
		{
			get
			{
				return (bool)(this.ViewState["ShowDateHeaders"] ?? true);
			}
			set
			{
				this.ViewState["ShowDateHeaders"] = value;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x000E2C91 File Offset: 0x000E0E91
		// (set) Token: 0x060047C5 RID: 18373 RVA: 0x000E2CB2 File Offset: 0x000E0EB2
		[DefaultValue(true)]
		[Description("Controls the visibility of the resource headers for the current view")]
		[Category("Appearance")]
		public bool ShowResourceHeaders
		{
			get
			{
				return (bool)(this.ViewState["ShowResourceHeaders"] ?? true);
			}
			set
			{
				this.ViewState["ShowResourceHeaders"] = value;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x060047C6 RID: 18374 RVA: 0x000E2CCA File Offset: 0x000E0ECA
		// (set) Token: 0x060047C7 RID: 18375 RVA: 0x000E2CF5 File Offset: 0x000E0EF5
		[DefaultValue(true)]
		[Description("Controls the visibility of the header")]
		[Category("Appearance")]
		public bool ShowHeader
		{
			get
			{
				return this.ViewState["ShowHeader"] == null || (bool)this.ViewState["ShowHeader"];
			}
			set
			{
				this.ViewState["ShowHeader"] = value;
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x000E2D0D File Offset: 0x000E0F0D
		// (set) Token: 0x060047C9 RID: 18377 RVA: 0x000E2D38 File Offset: 0x000E0F38
		[Category("Appearance")]
		[Description("Controls the visibility of the footer")]
		[DefaultValue(true)]
		public bool ShowFooter
		{
			get
			{
				return this.ViewState["ShowFooter"] == null || (bool)this.ViewState["ShowFooter"];
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x000E2D50 File Offset: 0x000E0F50
		// (set) Token: 0x060047CB RID: 18379 RVA: 0x000E2D7B File Offset: 0x000E0F7B
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Controls the visibility of the navigation pane")]
		public bool ShowNavigationPane
		{
			get
			{
				return this.ViewState["ShowNavigationPane"] == null || (bool)this.ViewState["ShowNavigationPane"];
			}
			set
			{
				this.ViewState["ShowNavigationPane"] = value;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x000E2D93 File Offset: 0x000E0F93
		// (set) Token: 0x060047CD RID: 18381 RVA: 0x000E2DBE File Offset: 0x000E0FBE
		[Description("Controls the visibility of the view tabs.")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool ShowViewTabs
		{
			get
			{
				return this.ViewState["ShowViewTabs"] == null || (bool)this.ViewState["ShowViewTabs"];
			}
			set
			{
				this.ViewState["ShowViewTabs"] = value;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x000E2DD6 File Offset: 0x000E0FD6
		// (set) Token: 0x060047CF RID: 18383 RVA: 0x000E2E01 File Offset: 0x000E1001
		[Description("Controls the visibility of the all day row.")]
		[Category("Appearance")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool ShowAllDayRow
		{
			get
			{
				return this.ViewState["ShowAllDayRow"] == null || (bool)this.ViewState["ShowAllDayRow"];
			}
			set
			{
				this.ViewState["ShowAllDayRow"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x000E2E1F File Offset: 0x000E101F
		// (set) Token: 0x060047D1 RID: 18385 RVA: 0x000E2E2C File Offset: 0x000E102C
		[Category("Appearance")]
		[Description("The edit form date format string.")]
		[Obsolete("Obsoleted. Please, use AdvancedForm-DateFormat instead.")]
		public string EditFormDateFormat
		{
			get
			{
				return this.AdvancedForm.DateFormat;
			}
			set
			{
				this.AdvancedForm.DateFormat = value;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x000E2E3A File Offset: 0x000E103A
		// (set) Token: 0x060047D3 RID: 18387 RVA: 0x000E2E47 File Offset: 0x000E1047
		[Description("The edit form time format string.")]
		[Obsolete("Obsoleted. Please, use AdvancedForm-TimeFormat instead.")]
		[Category("Appearance")]
		public string EditFormTimeFormat
		{
			get
			{
				return this.AdvancedForm.TimeFormat;
			}
			set
			{
				this.AdvancedForm.TimeFormat = value;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x000E2E58 File Offset: 0x000E1058
		// (set) Token: 0x060047D5 RID: 18389 RVA: 0x000E2EB1 File Offset: 0x000E10B1
		[Category("Appearance")]
		[Description("The hours panel time format string.")]
		[ClientControlProperty]
		public string HoursPanelTimeFormat
		{
			get
			{
				string result;
				if ((string)this.ViewState["HoursPanelTimeFormat"] == null)
				{
					if (this.ShouldUseLongHoursPanelTimeFormat)
					{
						result = "h:mmtt";
					}
					else
					{
						result = "htt";
					}
				}
				else
				{
					result = (string)this.ViewState["HoursPanelTimeFormat"];
				}
				return result;
			}
			set
			{
				this.ViewState["HoursPanelTimeFormat"] = value;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x000E2EC4 File Offset: 0x000E10C4
		private bool ShouldUseLongHoursPanelTimeFormat
		{
			get
			{
				return this.DayStartTime.Minutes != 0 || this.TimeLabelRowSpan * this.MinutesPerRow % 60 != 0;
			}
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x000E2EF9 File Offset: 0x000E10F9
		private bool ShouldSerializeHoursPanelTimeFormat()
		{
			if (this.ShouldUseLongHoursPanelTimeFormat)
			{
				return this.HoursPanelTimeFormat != "h:mmtt";
			}
			return this.HoursPanelTimeFormat != "htt";
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x000E2F24 File Offset: 0x000E1124
		// (set) Token: 0x060047D9 RID: 18393 RVA: 0x000E2F45 File Offset: 0x000E1145
		[ClientControlProperty]
		[Description("Whether to start in full (24 hours) mode")]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool ShowFullTime
		{
			get
			{
				return (bool)(this.ViewState["ShowFullTime"] ?? false);
			}
			set
			{
				this.ViewState["ShowFullTime"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x060047DA RID: 18394 RVA: 0x000E2F63 File Offset: 0x000E1163
		// (set) Token: 0x060047DB RID: 18395 RVA: 0x000E2F84 File Offset: 0x000E1184
		[DefaultValue(AppointmentStyleMode.Auto)]
		[ClientControlProperty]
		[Description("Defines the styling mode for appointments")]
		[Category("Appearance")]
		public AppointmentStyleMode AppointmentStyleMode
		{
			get
			{
				return (AppointmentStyleMode)(this.ViewState["AppointmentStyleMode"] ?? AppointmentStyleMode.Auto);
			}
			set
			{
				this.ViewState["AppointmentStyleMode"] = value;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x060047DC RID: 18396 RVA: 0x000E2F9C File Offset: 0x000E119C
		// (set) Token: 0x060047DD RID: 18397 RVA: 0x000E2FBD File Offset: 0x000E11BD
		[Category("Layout")]
		[DefaultValue(GroupingDirection.Horizontal)]
		[Description("Grouping direction of RadScheduler.")]
		public GroupingDirection GroupingDirection
		{
			get
			{
				return (GroupingDirection)(this.ViewState["GroupingDirection"] ?? GroupingDirection.Horizontal);
			}
			set
			{
				this.ViewState["GroupingDirection"] = value;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x000E2FD5 File Offset: 0x000E11D5
		// (set) Token: 0x060047DF RID: 18399 RVA: 0x000E2FF5 File Offset: 0x000E11F5
		[Category("Misc")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x000E300E File Offset: 0x000E120E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SchedulerStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new SchedulerStrings(new LocalizationProvider("RadScheduler.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x000E304D File Offset: 0x000E124D
		// (set) Token: 0x060047E2 RID: 18402 RVA: 0x000E3070 File Offset: 0x000E1270
		[Description("Gets or sets a value indicating where RadScheduler will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
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

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x000E30C3 File Offset: 0x000E12C3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Timeline view settings")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		public TimelineViewSettings TimelineView
		{
			get
			{
				return this._timelineViewSettings;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x060047E4 RID: 18404 RVA: 0x000E30CB File Offset: 0x000E12CB
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Week view settings")]
		public WeekViewSettings WeekView
		{
			get
			{
				return this._weekViewSettings;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x000E30D3 File Offset: 0x000E12D3
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Day view settings")]
		public DayViewSettings DayView
		{
			get
			{
				return this._dayViewSettings;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x000E30DB File Offset: 0x000E12DB
		[NotifyParentProperty(true)]
		[Description("Multi-day view settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Layout")]
		public MultiDayViewSettings MultiDayView
		{
			get
			{
				return this._multiDayViewSettings;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x000E30E3 File Offset: 0x000E12E3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Month view settings")]
		[Category("Layout")]
		public MonthViewSettings MonthView
		{
			get
			{
				return this._monthViewSettings;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x000E30EB File Offset: 0x000E12EB
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Layout")]
		[Description("Agenda view settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AgendaViewSettings AgendaView
		{
			get
			{
				return this._agendaViewSettings;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x000E30F3 File Offset: 0x000E12F3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Year view settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		public YearViewSettings YearView
		{
			get
			{
				return this._yearViewSettings;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x000E30FB File Offset: 0x000E12FB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("Appointment context menu settings")]
		public ContextMenuSettings AppointmentContextMenuSettings
		{
			get
			{
				return this._appointmentContextMenuSettings;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x000E3103 File Offset: 0x000E1303
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Time slot context menu settings")]
		public ContextMenuSettings TimeSlotContextMenuSettings
		{
			get
			{
				return this._timeSlotContextMenuSettings;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x000E310B File Offset: 0x000E130B
		// (set) Token: 0x060047ED RID: 18413 RVA: 0x000E312C File Offset: 0x000E132C
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Whether to enable the date picker for quick navigation")]
		public bool EnableDatePicker
		{
			get
			{
				return (bool)(this.ViewState["EnableDatePicker"] ?? true);
			}
			set
			{
				this.ViewState["EnableDatePicker"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x000E314A File Offset: 0x000E134A
		// (set) Token: 0x060047EF RID: 18415 RVA: 0x000E3170 File Offset: 0x000E1370
		[Category("Appearance")]
		[Description("The height of each RadScheduler row")]
		[ClientPropertyName("rowHeight")]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "25px")]
		public Unit RowHeight
		{
			get
			{
				return (Unit)(this.ViewState["RowHeight"] ?? this.GetDefaultRowHeight());
			}
			set
			{
				this.ViewState["RowHeight"] = value;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x000E3188 File Offset: 0x000E1388
		// (set) Token: 0x060047F1 RID: 18417 RVA: 0x000E31AD File Offset: 0x000E13AD
		[Description("The width of each content column")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		public Unit ColumnWidth
		{
			get
			{
				return (Unit)(this.ViewState["ColumnWidth"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["ColumnWidth"] = value;
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x000E31C5 File Offset: 0x000E13C5
		// (set) Token: 0x060047F3 RID: 18419 RVA: 0x000E31EC File Offset: 0x000E13EC
		[Description("The width of each row header")]
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "52px")]
		public Unit RowHeaderWidth
		{
			get
			{
				return (Unit)(this.ViewState["RowHeaderWidth"] ?? Unit.Pixel(52));
			}
			set
			{
				this.ViewState["RowHeaderWidth"] = value;
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060047F4 RID: 18420 RVA: 0x000E3204 File Offset: 0x000E1404
		// (set) Token: 0x060047F5 RID: 18421 RVA: 0x000E3226 File Offset: 0x000E1426
		[ClientControlProperty]
		[Description("The minimum height of the inline insert/edit template.")]
		[DefaultValue(50)]
		[Category("Appearance")]
		[ClientPropertyName("minimumInlineFormHeight")]
		public int MinimumInlineFormHeight
		{
			get
			{
				return (int)(this.ViewState["MinimumInlineFormHeight"] ?? 50);
			}
			set
			{
				this.ViewState["MinimumInlineFormHeight"] = value;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x000E323E File Offset: 0x000E143E
		// (set) Token: 0x060047F7 RID: 18423 RVA: 0x000E3263 File Offset: 0x000E1463
		[Description("The minimum width of the inline insert/edit template.")]
		[Category("Appearance")]
		[ClientPropertyName("minimumInlineFormWidth")]
		[DefaultValue(250)]
		[ClientControlProperty]
		public int MinimumInlineFormWidth
		{
			get
			{
				return (int)(this.ViewState["MinimumInlineFormWidth"] ?? 250);
			}
			set
			{
				this.ViewState["MinimumInlineFormWidth"] = value;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x000E327B File Offset: 0x000E147B
		// (set) Token: 0x060047F9 RID: 18425 RVA: 0x000E3283 File Offset: 0x000E1483
		[DefaultValue(typeof(Unit), "400px")]
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

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x000E328C File Offset: 0x000E148C
		// (set) Token: 0x060047FB RID: 18427 RVA: 0x000E32AD File Offset: 0x000E14AD
		[Description("Gets or sets a value indicating whether the appointment start and end time should be rendered exactly")]
		[DefaultValue(false)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public bool EnableExactTimeRendering
		{
			get
			{
				return (bool)(this.ViewState["EnableExactTimeRendering"] ?? false);
			}
			set
			{
				this.ViewState["EnableExactTimeRendering"] = value;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x000E32C5 File Offset: 0x000E14C5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadSchedulerContextMenuCollection TimeSlotContextMenus
		{
			get
			{
				if (this._timeSlotContextMenus == null)
				{
					this._timeSlotContextMenus = new RadSchedulerContextMenuCollection(this);
				}
				return this._timeSlotContextMenus;
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x000E32E1 File Offset: 0x000E14E1
		[MergableProperty(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadSchedulerContextMenuCollection AppointmentContextMenus
		{
			get
			{
				if (this._appointmentContextMenus == null)
				{
					this._appointmentContextMenus = new RadSchedulerContextMenuCollection(this);
				}
				return this._appointmentContextMenus;
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x000E32FD File Offset: 0x000E14FD
		// (set) Token: 0x060047FF RID: 18431 RVA: 0x000E331E File Offset: 0x000E151E
		[Description("The name of the validation group to be used for the integrated validation controls")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("validationGroup")]
		public string ValidationGroup
		{
			get
			{
				return ((string)this.ViewState["ValidationGroup"]) ?? this.ClientID;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x000E3331 File Offset: 0x000E1531
		private bool ShouldSerializeValidationGroup()
		{
			return this.ValidationGroup != this.ClientID;
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x000E3344 File Offset: 0x000E1544
		// (set) Token: 0x06004802 RID: 18434 RVA: 0x000E334C File Offset: 0x000E154C
		public override string DataSourceID
		{
			get
			{
				return base.DataSourceID;
			}
			set
			{
				base.DataSourceID = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x000E335B File Offset: 0x000E155B
		// (set) Token: 0x06004804 RID: 18436 RVA: 0x000E3363 File Offset: 0x000E1563
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SchedulerProviderBase Provider
		{
			get
			{
				return this._appointmentProvider;
			}
			set
			{
				this._appointmentProvider = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x000E3372 File Offset: 0x000E1572
		// (set) Token: 0x06004806 RID: 18438 RVA: 0x000E33A6 File Offset: 0x000E15A6
		[DefaultValue("Integrated")]
		[Category("Data")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("The name of the custom provider to use, as configured in web.config.")]
		public string ProviderName
		{
			get
			{
				if (!this.DesignMode)
				{
					return this.Provider.Name;
				}
				return (string)(this.ViewState["ProviderName"] ?? "Integrated");
			}
			set
			{
				if (!this.DesignMode)
				{
					this.Provider = SchedulerProviderFactory.GetProvider(this, value);
					this.OnDataPropertyChanged();
					return;
				}
				this.ViewState["ProviderName"] = ((value == string.Empty) ? null : value);
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x000E33E5 File Offset: 0x000E15E5
		// (set) Token: 0x06004808 RID: 18440 RVA: 0x000E33ED File Offset: 0x000E15ED
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object ProviderContext
		{
			get
			{
				return this._providerContext;
			}
			protected internal set
			{
				this._providerContext = value;
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x000E33F6 File Offset: 0x000E15F6
		// (set) Token: 0x0600480A RID: 18442 RVA: 0x000E3416 File Offset: 0x000E1616
		[Description("DataBase field containing the primary key field")]
		[DefaultValue("")]
		[Category("Data")]
		public string DataKeyField
		{
			get
			{
				return ((string)this.ViewState["DateKeyField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DateKeyField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x000E342F File Offset: 0x000E162F
		// (set) Token: 0x0600480C RID: 18444 RVA: 0x000E344F File Offset: 0x000E164F
		[DefaultValue("")]
		[Description("DataBase field containing the appointment subject field")]
		[Category("Data")]
		public string DataSubjectField
		{
			get
			{
				return ((string)this.ViewState["DataSubjectField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataSubjectField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x000E3468 File Offset: 0x000E1668
		// (set) Token: 0x0600480E RID: 18446 RVA: 0x000E3488 File Offset: 0x000E1688
		[Category("Data")]
		[Description("DataBase field containing the appointment subject field")]
		[DefaultValue("")]
		public string DataTimeZoneIdField
		{
			get
			{
				return ((string)this.ViewState["DataTimeZoneIdField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataTimeZoneIdField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x000E34A1 File Offset: 0x000E16A1
		// (set) Token: 0x06004810 RID: 18448 RVA: 0x000E34C1 File Offset: 0x000E16C1
		[DefaultValue("")]
		[Category("Data")]
		[Description("DataBase field containing the appointment description field. Optional.")]
		public string DataDescriptionField
		{
			get
			{
				return ((string)this.ViewState["DataDescriptionField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataDescriptionField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x000E34DA File Offset: 0x000E16DA
		// (set) Token: 0x06004812 RID: 18450 RVA: 0x000E34FA File Offset: 0x000E16FA
		[Description("DataBase field containing the appointment reminder field. Optional.")]
		[DefaultValue("")]
		[Category("Data")]
		public string DataReminderField
		{
			get
			{
				return ((string)this.ViewState["DataReminderField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataReminderField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06004813 RID: 18451 RVA: 0x000E3513 File Offset: 0x000E1713
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Description("Reminder settings.")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ReminderSettings Reminders
		{
			get
			{
				return this._reminderSettings;
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x000E351B File Offset: 0x000E171B
		// (set) Token: 0x06004815 RID: 18453 RVA: 0x000E353B File Offset: 0x000E173B
		[Category("Data")]
		[DefaultValue("")]
		[Description("DataBase field containing the appointment end-time field")]
		public string DataEndField
		{
			get
			{
				return ((string)this.ViewState["DataEndField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataEndField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x000E3554 File Offset: 0x000E1754
		// (set) Token: 0x06004817 RID: 18455 RVA: 0x000E3574 File Offset: 0x000E1774
		[DefaultValue("")]
		[Description("DataBase field containing the appointment start-time field")]
		[Category("Data")]
		public string DataStartField
		{
			get
			{
				return ((string)this.ViewState["DataStartField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataStartField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06004818 RID: 18456 RVA: 0x000E358D File Offset: 0x000E178D
		// (set) Token: 0x06004819 RID: 18457 RVA: 0x000E35AD File Offset: 0x000E17AD
		[Description("DataBase field containing the recurrence rule field")]
		[DefaultValue("")]
		[Category("Data")]
		public string DataRecurrenceField
		{
			get
			{
				return ((string)this.ViewState["DataRecurrenceField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataRecurrenceField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x000E35C6 File Offset: 0x000E17C6
		// (set) Token: 0x0600481B RID: 18459 RVA: 0x000E35E6 File Offset: 0x000E17E6
		[DefaultValue("")]
		[Description("DataBase field containing the recurrence parent field")]
		[Category("Data")]
		public string DataRecurrenceParentKeyField
		{
			get
			{
				return ((string)this.ViewState["DataRecurrenceParentKeyField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataRecurrenceParentKeyField"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x000E35FF File Offset: 0x000E17FF
		// (set) Token: 0x0600481D RID: 18461 RVA: 0x000E3620 File Offset: 0x000E1820
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("The names of database fields which should be used to populate the appointment custom attributes.")]
		[Category("Data")]
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] CustomAttributeNames
		{
			get
			{
				return (string[])(this.ViewState["CustomAttributeNames"] ?? new string[0]);
			}
			set
			{
				this.ViewState["CustomAttributeNames"] = value;
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x000E3633 File Offset: 0x000E1833
		// (set) Token: 0x0600481F RID: 18463 RVA: 0x000E3653 File Offset: 0x000E1853
		[DefaultValue("")]
		[Description("The current time zone RadScheduler ins operating in.")]
		[Category("Behavior")]
		public string TimeZoneID
		{
			get
			{
				return ((string)this.ViewState["TimeZoneId"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["TimeZoneId"] = value;
				this.LoadTimeZoneProvider();
				this.OnDataPropertyChanged();
				this.TimeZoneOffset = this._timeZoneProvider.OperationTimeZone.BaseUtcOffset;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x000E3688 File Offset: 0x000E1888
		// (set) Token: 0x06004821 RID: 18465 RVA: 0x000E36A3 File Offset: 0x000E18A3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IComparer<Appointment> AppointmentComparer
		{
			get
			{
				if (this._appointmentComparer == null)
				{
					this._appointmentComparer = new AppointmentComparer();
				}
				return this._appointmentComparer;
			}
			set
			{
				this._appointmentComparer = value;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x000E36AC File Offset: 0x000E18AC
		// (set) Token: 0x06004823 RID: 18467 RVA: 0x000E36D1 File Offset: 0x000E18D1
		[DefaultValue(3000)]
		[Description(">The maximum recurrence candidates limit.")]
		[Category("Data")]
		public int MaximumRecurrenceCandidates
		{
			get
			{
				return (int)(this.ViewState["MaximumRecurrenceCandidates"] ?? 3000);
			}
			set
			{
				this.ViewState["MaximumRecurrenceCandidates"] = value;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x000E36E9 File Offset: 0x000E18E9
		// (set) Token: 0x06004825 RID: 18469 RVA: 0x000E370A File Offset: 0x000E190A
		[Description("Enables creating and editing of recurring appointments.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool EnableRecurrenceSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableRecurrenceSupport"] ?? true);
			}
			set
			{
				this.ViewState["EnableRecurrenceSupport"] = value;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x000E3722 File Offset: 0x000E1922
		// (set) Token: 0x06004827 RID: 18471 RVA: 0x000E3743 File Offset: 0x000E1943
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Enables the editing of the description field of appointments.")]
		[Category("Behavior")]
		[ClientPropertyName("_enableDescriptionField")]
		public bool EnableDescriptionField
		{
			get
			{
				return (bool)(this.ViewState["EnableDescriptionField"] ?? false);
			}
			set
			{
				this.ViewState["EnableDescriptionField"] = value;
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06004828 RID: 18472 RVA: 0x000E375B File Offset: 0x000E195B
		[Description("The web service to be used for binding this instance of RadScheduler.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SchedulerWebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x000E3763 File Offset: 0x000E1963
		// (set) Token: 0x0600482A RID: 18474 RVA: 0x000E3784 File Offset: 0x000E1984
		[DefaultValue(true)]
		[ClientPropertyName("allowEdit")]
		[Category("Behavior")]
		[Description("A value indicating whether appointments editing is allowed.")]
		[ClientControlProperty]
		public bool AllowEdit
		{
			get
			{
				return (bool)(this.ViewState["AllowEdit"] ?? true);
			}
			set
			{
				this.ViewState["AllowEdit"] = value;
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x000E379C File Offset: 0x000E199C
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x000E37BD File Offset: 0x000E19BD
		[Description("A value indicating whether appointments deleting is allowed.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("allowDelete")]
		public bool AllowDelete
		{
			get
			{
				return (bool)(this.ViewState["AllowDelete"] ?? true);
			}
			set
			{
				this.ViewState["AllowDelete"] = value;
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x000E37D5 File Offset: 0x000E19D5
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x000E37F6 File Offset: 0x000E19F6
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientPropertyName("allowInsert")]
		[Description("A value indicating whether appointments inserting is allowed.")]
		[ClientControlProperty]
		public bool AllowInsert
		{
			get
			{
				return (bool)(this.ViewState["AllowInsert"] ?? true);
			}
			set
			{
				this.ViewState["AllowInsert"] = value;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x000E380E File Offset: 0x000E1A0E
		// (set) Token: 0x06004830 RID: 18480 RVA: 0x000E382D File Offset: 0x000E1A2D
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(SchedulerAppointmentContainer))]
		public ITemplate AppointmentTemplate
		{
			get
			{
				if (this._appointmentTemplate == null && !this.DesignMode)
				{
					return new AppointmentTemplate(this);
				}
				return this._appointmentTemplate;
			}
			set
			{
				this._appointmentTemplate = value;
			}
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x000E3836 File Offset: 0x000E1A36
		// (set) Token: 0x06004832 RID: 18482 RVA: 0x000E3855 File Offset: 0x000E1A55
		[Browsable(false)]
		[TemplateContainer(typeof(SchedulerFormContainer), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate InlineInsertTemplate
		{
			get
			{
				if (this._inlineInsertTemplate == null && !this.DesignMode)
				{
					return new InlineInsertTemplate(this);
				}
				return this._inlineInsertTemplate;
			}
			set
			{
				this._inlineInsertTemplate = value;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x000E385E File Offset: 0x000E1A5E
		// (set) Token: 0x06004834 RID: 18484 RVA: 0x000E387D File Offset: 0x000E1A7D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Browsable(false)]
		[TemplateContainer(typeof(SchedulerFormContainer), BindingDirection.TwoWay)]
		public ITemplate InlineEditTemplate
		{
			get
			{
				if (this._inlineEditTemplate == null && !this.DesignMode)
				{
					return new InlineEditTemplate(this);
				}
				return this._inlineEditTemplate;
			}
			set
			{
				this._inlineEditTemplate = value;
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x000E3886 File Offset: 0x000E1A86
		// (set) Token: 0x06004836 RID: 18486 RVA: 0x000E38AB File Offset: 0x000E1AAB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SchedulerFormContainer), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[Browsable(false)]
		public ITemplate AdvancedInsertTemplate
		{
			get
			{
				if (this._advancedInsertTemplate == null && !this.DesignMode)
				{
					return new AdvancedInsertTemplate(this, base.RuntimeSkin);
				}
				return this._advancedInsertTemplate;
			}
			set
			{
				this._advancedInsertTemplate = value;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06004837 RID: 18487 RVA: 0x000E38B4 File Offset: 0x000E1AB4
		// (set) Token: 0x06004838 RID: 18488 RVA: 0x000E38D9 File Offset: 0x000E1AD9
		[Browsable(false)]
		[TemplateContainer(typeof(SchedulerFormContainer), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate AdvancedEditTemplate
		{
			get
			{
				if (this._advancedEditTemplate == null && !this.DesignMode)
				{
					return new AdvancedEditTemplate(this, base.RuntimeSkin);
				}
				return this._advancedEditTemplate;
			}
			set
			{
				this._advancedEditTemplate = value;
			}
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x000E38E2 File Offset: 0x000E1AE2
		// (set) Token: 0x0600483A RID: 18490 RVA: 0x000E3900 File Offset: 0x000E1B00
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SchedulerResourceContainer))]
		[Browsable(false)]
		public ITemplate ResourceHeaderTemplate
		{
			get
			{
				if (this._resourceHeaderTemplate == null && !this.DesignMode)
				{
					return new ResourceHeaderTemplate();
				}
				return this._resourceHeaderTemplate;
			}
			set
			{
				this._resourceHeaderTemplate = value;
			}
		}

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x0600483B RID: 18491 RVA: 0x000E3909 File Offset: 0x000E1B09
		// (remove) Token: 0x0600483C RID: 18492 RVA: 0x000E391C File Offset: 0x000E1B1C
		[Category("Action")]
		public event AppointmentCommandEventHandler AppointmentCommand
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentCommandEvent, value);
			}
		}

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x0600483D RID: 18493 RVA: 0x000E392F File Offset: 0x000E1B2F
		// (remove) Token: 0x0600483E RID: 18494 RVA: 0x000E3942 File Offset: 0x000E1B42
		[Description("Fired when an appointment context menu item is clicked, before processing default commands.")]
		[Category("Action")]
		public event AppointmentContextMenuItemClickingEventHandler AppointmentContextMenuItemClicking
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentContextMenuItemClickingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentContextMenuItemClickingEvent, value);
			}
		}

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600483F RID: 18495 RVA: 0x000E3955 File Offset: 0x000E1B55
		// (remove) Token: 0x06004840 RID: 18496 RVA: 0x000E3968 File Offset: 0x000E1B68
		[Category("Action")]
		[Description("Fired when an appointment context menu item is clicked.")]
		public event AppointmentContextMenuItemClickedEventHandler AppointmentContextMenuItemClicked
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentContextMenuItemClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentContextMenuItemClickedEvent, value);
			}
		}

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06004841 RID: 18497 RVA: 0x000E397B File Offset: 0x000E1B7B
		// (remove) Token: 0x06004842 RID: 18498 RVA: 0x000E398E File Offset: 0x000E1B8E
		[Description("Fired when a time slot context menu item is clicked, before processing default commands.")]
		[Category("Action")]
		public event TimeSlotContextMenuItemClickingEventHandler TimeSlotContextMenuItemClicking
		{
			add
			{
				base.Events.AddHandler(RadScheduler.TimeSlotContextMenuItemClickingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.TimeSlotContextMenuItemClickingEvent, value);
			}
		}

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06004843 RID: 18499 RVA: 0x000E39A1 File Offset: 0x000E1BA1
		// (remove) Token: 0x06004844 RID: 18500 RVA: 0x000E39B4 File Offset: 0x000E1BB4
		[Description("Fired when a time slot context menu item is clicked.")]
		[Category("Action")]
		public event TimeSlotContextMenuItemClickedEventHandler TimeSlotContextMenuItemClicked
		{
			add
			{
				base.Events.AddHandler(RadScheduler.TimeSlotContextMenuItemClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.TimeSlotContextMenuItemClickedEvent, value);
			}
		}

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06004845 RID: 18501 RVA: 0x000E39C7 File Offset: 0x000E1BC7
		// (remove) Token: 0x06004846 RID: 18502 RVA: 0x000E39DA File Offset: 0x000E1BDA
		[Category("Data")]
		public event AppointmentInsertEventHandler AppointmentInsert
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentInsertEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentInsertEvent, value);
			}
		}

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06004847 RID: 18503 RVA: 0x000E39ED File Offset: 0x000E1BED
		// (remove) Token: 0x06004848 RID: 18504 RVA: 0x000E3A00 File Offset: 0x000E1C00
		[Category("Data")]
		public event AppointmentUpdateEventHandler AppointmentUpdate
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentUpdateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentUpdateEvent, value);
			}
		}

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06004849 RID: 18505 RVA: 0x000E3A13 File Offset: 0x000E1C13
		// (remove) Token: 0x0600484A RID: 18506 RVA: 0x000E3A26 File Offset: 0x000E1C26
		[Category("Data")]
		public event AppointmentDeleteEventHandler AppointmentDelete
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentDeleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentDeleteEvent, value);
			}
		}

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x0600484B RID: 18507 RVA: 0x000E3A39 File Offset: 0x000E1C39
		// (remove) Token: 0x0600484C RID: 18508 RVA: 0x000E3A4C File Offset: 0x000E1C4C
		[Category("Data")]
		public event AppointmentCreatedEventHandler AppointmentCreated
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentCreatedEvent, value);
			}
		}

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x0600484D RID: 18509 RVA: 0x000E3A5F File Offset: 0x000E1C5F
		// (remove) Token: 0x0600484E RID: 18510 RVA: 0x000E3A72 File Offset: 0x000E1C72
		[Category("Data")]
		public event AppointmentDataBoundEventHandler AppointmentDataBound
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentDataBoundEvent, value);
			}
		}

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x0600484F RID: 18511 RVA: 0x000E3A85 File Offset: 0x000E1C85
		// (remove) Token: 0x06004850 RID: 18512 RVA: 0x000E3A98 File Offset: 0x000E1C98
		[Category("Action")]
		public event AppointmentClickEventHandler AppointmentClick
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentClickEvent, value);
			}
		}

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06004851 RID: 18513 RVA: 0x000E3AAB File Offset: 0x000E1CAB
		// (remove) Token: 0x06004852 RID: 18514 RVA: 0x000E3ABE File Offset: 0x000E1CBE
		[Category("Action")]
		public event SchedulerNavigationCommandEventHandler NavigationCommand
		{
			add
			{
				base.Events.AddHandler(RadScheduler.SchedulerNavigationCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.SchedulerNavigationCommandEvent, value);
			}
		}

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06004853 RID: 18515 RVA: 0x000E3AD1 File Offset: 0x000E1CD1
		// (remove) Token: 0x06004854 RID: 18516 RVA: 0x000E3AE4 File Offset: 0x000E1CE4
		[Category("Action")]
		public event SchedulerNavigationCompleteEventHandler NavigationComplete
		{
			add
			{
				base.Events.AddHandler(RadScheduler.SchedulerNavigationCompleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.SchedulerNavigationCompleteEvent, value);
			}
		}

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06004855 RID: 18517 RVA: 0x000E3AF7 File Offset: 0x000E1CF7
		// (remove) Token: 0x06004856 RID: 18518 RVA: 0x000E3B0A File Offset: 0x000E1D0A
		[Category("Action")]
		public event SchedulerFormCreatingEventHandler FormCreating
		{
			add
			{
				base.Events.AddHandler(RadScheduler.FormCreatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.FormCreatingEvent, value);
			}
		}

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06004857 RID: 18519 RVA: 0x000E3B1D File Offset: 0x000E1D1D
		// (remove) Token: 0x06004858 RID: 18520 RVA: 0x000E3B30 File Offset: 0x000E1D30
		[Category("Action")]
		public event SchedulerFormCreatedEventHandler FormCreated
		{
			add
			{
				base.Events.AddHandler(RadScheduler.FormCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.FormCreatedEvent, value);
			}
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06004859 RID: 18521 RVA: 0x000E3B43 File Offset: 0x000E1D43
		// (remove) Token: 0x0600485A RID: 18522 RVA: 0x000E3B56 File Offset: 0x000E1D56
		[Category("Action")]
		public event AppointmentCancelingEditEventHandler AppointmentCancelingEdit
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentCancelingEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentCancelingEditEvent, value);
			}
		}

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x0600485B RID: 18523 RVA: 0x000E3B69 File Offset: 0x000E1D69
		// (remove) Token: 0x0600485C RID: 18524 RVA: 0x000E3B7C File Offset: 0x000E1D7C
		[Category("Action")]
		public event TimeSlotCreatedEventHandler TimeSlotCreated
		{
			add
			{
				base.Events.AddHandler(RadScheduler.TimeSlotCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.TimeSlotCreatedEvent, value);
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x0600485D RID: 18525 RVA: 0x000E3B8F File Offset: 0x000E1D8F
		// (remove) Token: 0x0600485E RID: 18526 RVA: 0x000E3BA2 File Offset: 0x000E1DA2
		[Category("Action")]
		public event ResourceHeaderCreatedEventHandler ResourceHeaderCreated
		{
			add
			{
				base.Events.AddHandler(RadScheduler.ResourceHeaderCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.ResourceHeaderCreatedEvent, value);
			}
		}

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x0600485F RID: 18527 RVA: 0x000E3BB5 File Offset: 0x000E1DB5
		// (remove) Token: 0x06004860 RID: 18528 RVA: 0x000E3BC8 File Offset: 0x000E1DC8
		[Category("Data")]
		public event OccurrenceDeleteEventHandler OccurrenceDelete
		{
			add
			{
				base.Events.AddHandler(RadScheduler.OccurrenceDeleteEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.OccurrenceDeleteEvent, value);
			}
		}

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06004861 RID: 18529 RVA: 0x000E3BDB File Offset: 0x000E1DDB
		// (remove) Token: 0x06004862 RID: 18530 RVA: 0x000E3BEE File Offset: 0x000E1DEE
		[Category("Data")]
		public event RecurrenceExceptionCreatedEventHandler RecurrenceExceptionCreated
		{
			add
			{
				base.Events.AddHandler(RadScheduler.RecurrenceExceptionCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.RecurrenceExceptionCreatedEvent, value);
			}
		}

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06004863 RID: 18531 RVA: 0x000E3C01 File Offset: 0x000E1E01
		// (remove) Token: 0x06004864 RID: 18532 RVA: 0x000E3C14 File Offset: 0x000E1E14
		[Category("Data")]
		public event ResourcesPopulatingEventHandler ResourcesPopulating
		{
			add
			{
				base.Events.AddHandler(RadScheduler.ResourcesPopulatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.ResourcesPopulatingEvent, value);
			}
		}

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06004865 RID: 18533 RVA: 0x000E3C27 File Offset: 0x000E1E27
		// (remove) Token: 0x06004866 RID: 18534 RVA: 0x000E3C3A File Offset: 0x000E1E3A
		[Category("Data")]
		public event AppointmentsPopulatingEventHandler AppointmentsPopulating
		{
			add
			{
				base.Events.AddHandler(RadScheduler.AppointmentsPopulatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.AppointmentsPopulatingEvent, value);
			}
		}

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06004867 RID: 18535 RVA: 0x000E3C4D File Offset: 0x000E1E4D
		// (remove) Token: 0x06004868 RID: 18536 RVA: 0x000E3C60 File Offset: 0x000E1E60
		[Category("Action")]
		public event ReminderSnoozeEventHandler ReminderSnooze
		{
			add
			{
				base.Events.AddHandler(RadScheduler.ReminderSnoozeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.ReminderSnoozeEvent, value);
			}
		}

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06004869 RID: 18537 RVA: 0x000E3C73 File Offset: 0x000E1E73
		// (remove) Token: 0x0600486A RID: 18538 RVA: 0x000E3C86 File Offset: 0x000E1E86
		[Category("Action")]
		public event ReminderDismissEventHandler ReminderDismiss
		{
			add
			{
				base.Events.AddHandler(RadScheduler.ReminderDismissEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadScheduler.ReminderDismissEvent, value);
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x000E3C99 File Offset: 0x000E1E99
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x000E3CC8 File Offset: 0x000E1EC8
		[ClientPropertyName("appointmentClick")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientAppointmentClick
		{
			get
			{
				if (this.ViewState["OnClientAppointmentClick"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientAppointmentClick"];
			}
			set
			{
				this.ViewState["OnClientAppointmentClick"] = value;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x000E3CDB File Offset: 0x000E1EDB
		// (set) Token: 0x0600486E RID: 18542 RVA: 0x000E3CFB File Offset: 0x000E1EFB
		[ClientPropertyName("appointmentInserting")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientAppointmentInserting
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentInserting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentInserting"] = value;
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x000E3D0E File Offset: 0x000E1F0E
		// (set) Token: 0x06004870 RID: 18544 RVA: 0x000E3D3D File Offset: 0x000E1F3D
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("appointmentDoubleClick")]
		[Category("Client-side events")]
		public string OnClientAppointmentDoubleClick
		{
			get
			{
				if (this.ViewState["OnClientAppointmentDoubleClick"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientAppointmentDoubleClick"];
			}
			set
			{
				this.ViewState["OnClientAppointmentDoubleClick"] = value;
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x000E3D50 File Offset: 0x000E1F50
		// (set) Token: 0x06004872 RID: 18546 RVA: 0x000E3D7F File Offset: 0x000E1F7F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("appointmentResizeStart")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientAppointmentResizeStart
		{
			get
			{
				if (this.ViewState["OnClientAppointmentResizeStart"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientAppointmentResizeStart"];
			}
			set
			{
				this.ViewState["OnClientAppointmentResizeStart"] = value;
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x000E3D92 File Offset: 0x000E1F92
		// (set) Token: 0x06004874 RID: 18548 RVA: 0x000E3DC1 File Offset: 0x000E1FC1
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("appointmentResizeEnd")]
		public string OnClientAppointmentResizeEnd
		{
			get
			{
				if (this.ViewState["OnClientAppointmentResizeEnd"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientAppointmentResizeEnd"];
			}
			set
			{
				this.ViewState["OnClientAppointmentResizeEnd"] = value;
			}
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x000E3DD4 File Offset: 0x000E1FD4
		// (set) Token: 0x06004876 RID: 18550 RVA: 0x000E3E03 File Offset: 0x000E2003
		[Category("Client-side events")]
		[ClientPropertyName("appointmentResizing")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientAppointmentResizing
		{
			get
			{
				if (this.ViewState["OnClientAppointmentResizing"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientAppointmentResizing"];
			}
			set
			{
				this.ViewState["OnClientAppointmentResizing"] = value;
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x000E3E16 File Offset: 0x000E2016
		// (set) Token: 0x06004878 RID: 18552 RVA: 0x000E3E45 File Offset: 0x000E2045
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("appointmentDeleting")]
		[ClientControlEvent]
		public string OnClientAppointmentDeleting
		{
			get
			{
				if (this.ViewState["OnClientAppointmentDeleting"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientAppointmentDeleting"];
			}
			set
			{
				this.ViewState["OnClientAppointmentDeleting"] = value;
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x000E3E58 File Offset: 0x000E2058
		// (set) Token: 0x0600487A RID: 18554 RVA: 0x000E3E78 File Offset: 0x000E2078
		[ClientPropertyName("appointmentEditing")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientAppointmentEditing
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentEditing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentEditing"] = value;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x000E3E8B File Offset: 0x000E208B
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x000E3EAB File Offset: 0x000E20AB
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("appointmentMoveStart")]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when an appointment is about to be moved.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientAppointmentMoveStart
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentMoveStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentMoveStart"] = value;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x000E3EBE File Offset: 0x000E20BE
		// (set) Token: 0x0600487E RID: 18558 RVA: 0x000E3EDE File Offset: 0x000E20DE
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("appointmentMoving")]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when an appointment is being moved.")]
		public string OnClientAppointmentMoving
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentMoving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentMoving"] = value;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x000E3EF1 File Offset: 0x000E20F1
		// (set) Token: 0x06004880 RID: 18560 RVA: 0x000E3F11 File Offset: 0x000E2111
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when an appointment has been moved.")]
		[ClientPropertyName("appointmentMoveEnd")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientAppointmentMoveEnd
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentMoveEnd"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentMoveEnd"] = value;
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x000E3F24 File Offset: 0x000E2124
		// (set) Token: 0x06004882 RID: 18562 RVA: 0x000E3F44 File Offset: 0x000E2144
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("timeSlotClick")]
		public string OnClientTimeSlotClick
		{
			get
			{
				return (string)(this.ViewState["OnClientTimeSlotClick"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTimeSlotClick"] = value;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x000E3F57 File Offset: 0x000E2157
		// (set) Token: 0x06004884 RID: 18564 RVA: 0x000E3F77 File Offset: 0x000E2177
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("recurrenceActionDialogShowing")]
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when the recurrence action confirmation dialog is about to be shown.")]
		public string OnClientRecurrenceActionDialogShowing
		{
			get
			{
				return (string)(this.ViewState["OnClientRecurrenceActionDialogShowing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRecurrenceActionDialogShowing"] = value;
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x000E3F8A File Offset: 0x000E218A
		// (set) Token: 0x06004886 RID: 18566 RVA: 0x000E3FAA File Offset: 0x000E21AA
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when the recurrence action confirmation dialog has been closed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("recurrenceActionDialogClosed")]
		[Category("Client-side events")]
		public string OnClientRecurrenceActionDialogClosed
		{
			get
			{
				return (string)(this.ViewState["OnClientRecurrenceActionDialogClosed"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRecurrenceActionDialogClosed"] = value;
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x000E3FBD File Offset: 0x000E21BD
		// (set) Token: 0x06004888 RID: 18568 RVA: 0x000E3FDD File Offset: 0x000E21DD
		[Description("The name of the JavaScript function called when an edit/insert form has been created.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("formCreated")]
		public string OnClientFormCreated
		{
			get
			{
				return (string)(this.ViewState["OnClientFormCreated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientFormCreated"] = value;
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x000E3FF0 File Offset: 0x000E21F0
		// (set) Token: 0x0600488A RID: 18570 RVA: 0x000E4010 File Offset: 0x000E2210
		[Description("The name of the JavaScript function called when an appointment has been right-clicked.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("appointmentContextMenu")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientAppointmentContextMenu
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentContextMenu"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentContextMenu"] = value;
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x000E4023 File Offset: 0x000E2223
		// (set) Token: 0x0600488C RID: 18572 RVA: 0x000E4043 File Offset: 0x000E2243
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("timeSlotContextMenu")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when an empty time slot has been right-clicked.")]
		public string OnClientTimeSlotContextMenu
		{
			get
			{
				return (string)(this.ViewState["OnClientTimeSlotContextMenu"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTimeSlotContextMenu"] = value;
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x000E4056 File Offset: 0x000E2256
		// (set) Token: 0x0600488E RID: 18574 RVA: 0x000E4076 File Offset: 0x000E2276
		[Description("The name of the JavaScript function called when the scheduler is about to request appointments from the Web Service.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("appointmentsPopulating")]
		public string OnClientAppointmentsPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentsPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentsPopulating"] = value;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000E4089 File Offset: 0x000E2289
		// (set) Token: 0x06004890 RID: 18576 RVA: 0x000E40A9 File Offset: 0x000E22A9
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("appointmentsPopulated")]
		[Description("The name of the JavaScript function called when the scheduler has received appointments from the Web Service.")]
		public string OnClientAppointmentsPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentsPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentsPopulated"] = value;
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06004891 RID: 18577 RVA: 0x000E40BC File Offset: 0x000E22BC
		// (set) Token: 0x06004892 RID: 18578 RVA: 0x000E40DC File Offset: 0x000E22DC
		[ClientPropertyName("appointmentDataBound")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when an appointment is received from the Web Service and is about to be rendered.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientAppointmentDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentDataBound"] = value;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06004893 RID: 18579 RVA: 0x000E40EF File Offset: 0x000E22EF
		// (set) Token: 0x06004894 RID: 18580 RVA: 0x000E410F File Offset: 0x000E230F
		[ClientPropertyName("appointmentSerialized")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when an appointment has been serialized to a data object and is about to be sent to the Web Service.")]
		public string OnClientAppointmentSerialized
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentSerialized"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentSerialized"] = value;
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x000E4122 File Offset: 0x000E2322
		// (set) Token: 0x06004896 RID: 18582 RVA: 0x000E4142 File Offset: 0x000E2342
		[ClientPropertyName("appointmentCreated")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when the scheduler has rendered an appointment.")]
		[ClientControlEvent]
		public string OnClientAppointmentCreated
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentCreated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentCreated"] = value;
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x000E4155 File Offset: 0x000E2355
		// (set) Token: 0x06004898 RID: 18584 RVA: 0x000E4175 File Offset: 0x000E2375
		[Description("The name of the JavaScript function called when the scheduler is about to request resources from the Web Service.")]
		[ClientControlEvent]
		[ClientPropertyName("resourcesPopulating")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientResourcesPopulating
		{
			get
			{
				return (string)(this.ViewState["OnClientResourcesPopulating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientResourcesPopulating"] = value;
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06004899 RID: 18585 RVA: 0x000E4188 File Offset: 0x000E2388
		// (set) Token: 0x0600489A RID: 18586 RVA: 0x000E41A8 File Offset: 0x000E23A8
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("resourcesPopulated")]
		[Description("The name of the JavaScript function called when the scheduler has received resources from the Web Service.")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientResourcesPopulated
		{
			get
			{
				return (string)(this.ViewState["OnClientResourcesPopulated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientResourcesPopulated"] = value;
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x0600489B RID: 18587 RVA: 0x000E41BB File Offset: 0x000E23BB
		// (set) Token: 0x0600489C RID: 18588 RVA: 0x000E41DB File Offset: 0x000E23DB
		[ClientPropertyName("dataBound")]
		[Description("The name of the JavaScript function called when the scheduler has been populated with data.")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDataBound"] = value;
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x0600489D RID: 18589 RVA: 0x000E41EE File Offset: 0x000E23EE
		// (set) Token: 0x0600489E RID: 18590 RVA: 0x000E420E File Offset: 0x000E240E
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("requestSuccess")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when a request to the Web Service has succeeded.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientRequestSuccess
		{
			get
			{
				return (string)(this.ViewState["OnClientRequestSuccess"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRequestSuccess"] = value;
			}
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x0600489F RID: 18591 RVA: 0x000E4221 File Offset: 0x000E2421
		// (set) Token: 0x060048A0 RID: 18592 RVA: 0x000E4241 File Offset: 0x000E2441
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("requestFailed")]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when a request to the Web Service has failed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x000E4254 File Offset: 0x000E2454
		// (set) Token: 0x060048A2 RID: 18594 RVA: 0x000E4274 File Offset: 0x000E2474
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when an appointment is about to be stored via Web Service call.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("appointmentWebServiceInserting")]
		[Category("Client-side events")]
		public string OnClientAppointmentWebServiceInserting
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentWebServiceInserting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentWebServiceInserting"] = value;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x060048A3 RID: 18595 RVA: 0x000E4287 File Offset: 0x000E2487
		// (set) Token: 0x060048A4 RID: 18596 RVA: 0x000E42A7 File Offset: 0x000E24A7
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when an appointment is about to be deleted via Web Service call.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("appointmentWebServiceDeleting")]
		public string OnClientAppointmentWebServiceDeleting
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentWebServiceDeleting"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentWebServiceDeleting"] = value;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x060048A5 RID: 18597 RVA: 0x000E42BA File Offset: 0x000E24BA
		// (set) Token: 0x060048A6 RID: 18598 RVA: 0x000E42DA File Offset: 0x000E24DA
		[DefaultValue("")]
		[ClientPropertyName("appointmentWebServiceUpdating")]
		[Description("The name of the JavaScript function called when an appointment is about to be updated via Web Service call.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientAppointmentWebServiceUpdating
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentWebServiceUpdating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentWebServiceUpdating"] = value;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x000E42ED File Offset: 0x000E24ED
		// (set) Token: 0x060048A8 RID: 18600 RVA: 0x000E430D File Offset: 0x000E250D
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when a recurrence exception is about to be created via Web Service call.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("recurrenceExceptionCreating")]
		[Category("Client-side events")]
		public string OnClientRecurrenceExceptionCreating
		{
			get
			{
				return (string)(this.ViewState["OnClientRecurrenceExceptionCreating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRecurrenceExceptionCreating"] = value;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x000E4320 File Offset: 0x000E2520
		// (set) Token: 0x060048AA RID: 18602 RVA: 0x000E4340 File Offset: 0x000E2540
		[Description("The name of the JavaScript function called when recurrence exceptions are about to be cleared via Web Service call.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("recurrenceExceptionsRemoving")]
		public string OnClientRecurrenceExceptionsRemoving
		{
			get
			{
				return (string)(this.ViewState["OnClientRecurrenceExceptionsRemoving"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRecurrenceExceptionsRemoving"] = value;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x060048AB RID: 18603 RVA: 0x000E4353 File Offset: 0x000E2553
		// (set) Token: 0x060048AC RID: 18604 RVA: 0x000E4373 File Offset: 0x000E2573
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when the scheduler is about to execute a navigation command.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("navigationCommand")]
		[Category("Client-side events")]
		public string OnClientNavigationCommand
		{
			get
			{
				return (string)(this.ViewState["OnClientNavigationCommand"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNavigationCommand"] = value;
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x000E4386 File Offset: 0x000E2586
		// (set) Token: 0x060048AE RID: 18606 RVA: 0x000E43A6 File Offset: 0x000E25A6
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the JavaScript function called when a navigation command has been completed.")]
		[ClientPropertyName("navigationComplete")]
		public string OnClientNavigationComplete
		{
			get
			{
				return (string)(this.ViewState["OnClientNavigationComplete"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientNavigationComplete"] = value;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x060048AF RID: 18607 RVA: 0x000E43B9 File Offset: 0x000E25B9
		// (set) Token: 0x060048B0 RID: 18608 RVA: 0x000E43D9 File Offset: 0x000E25D9
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("appointmentContextMenuItemClicking")]
		[DefaultValue("")]
		[Description("The name of the JavaScript function called when an apointment context menu item is clicked, before RadScheduler processes the click event.")]
		public string OnClientAppointmentContextMenuItemClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentContextMenuItemClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentContextMenuItemClicking"] = value;
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x060048B1 RID: 18609 RVA: 0x000E43EC File Offset: 0x000E25EC
		// (set) Token: 0x060048B2 RID: 18610 RVA: 0x000E440C File Offset: 0x000E260C
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when an apointment context menu item is clicked, after RadScheduler has processed the event.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("appointmentContextMenuItemClicked")]
		public string OnClientAppointmentContextMenuItemClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientAppointmentContextMenuItemClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientAppointmentContextMenuItemClicked"] = value;
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x060048B3 RID: 18611 RVA: 0x000E441F File Offset: 0x000E261F
		// (set) Token: 0x060048B4 RID: 18612 RVA: 0x000E443F File Offset: 0x000E263F
		[Description("The name of the JavaScript function called when a time slot context menu item is clicked, before RadScheduler processes the click event.")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("timeSlotContextMenuItemClicking")]
		public string OnClientTimeSlotContextMenuItemClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientTimeSlotContextMenuItemClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTimeSlotContextMenuItemClicking"] = value;
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x000E4452 File Offset: 0x000E2652
		// (set) Token: 0x060048B6 RID: 18614 RVA: 0x000E4472 File Offset: 0x000E2672
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("timeSlotContextMenuItemClicked")]
		[Category("Client-side events")]
		[Description("The name of the JavaScript function called when a time slot context menu item is clicked, after RadScheduler has processed the event.")]
		public string OnClientTimeSlotContextMenuItemClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientTimeSlotContextMenuItemClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientTimeSlotContextMenuItemClicked"] = value;
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x060048B7 RID: 18615 RVA: 0x000E4485 File Offset: 0x000E2685
		// (set) Token: 0x060048B8 RID: 18616 RVA: 0x000E44A5 File Offset: 0x000E26A5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("reminderTriggering")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Description("The name of the JavaScript function called when an appointment reminder is about to be triggered.")]
		[Category("Client-side events")]
		public string OnClientReminderTriggering
		{
			get
			{
				return (string)(this.ViewState["OnClientReminderTriggering"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReminderTriggering"] = value;
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x000E44B8 File Offset: 0x000E26B8
		// (set) Token: 0x060048BA RID: 18618 RVA: 0x000E44D8 File Offset: 0x000E26D8
		[Description("The name of the JavaScript function called when an appointment reminder is about to be snoozed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("reminderSnoozing")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientReminderSnoozing
		{
			get
			{
				return (string)(this.ViewState["OnClientReminderSnoozing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReminderSnoozing"] = value;
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x060048BB RID: 18619 RVA: 0x000E44EB File Offset: 0x000E26EB
		// (set) Token: 0x060048BC RID: 18620 RVA: 0x000E450B File Offset: 0x000E270B
		[ClientControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("reminderDismissing")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the JavaScript function called when an appointment reminder is about to be dismissed.")]
		[Category("Client-side events")]
		public string OnClientReminderDismissing
		{
			get
			{
				return (string)(this.ViewState["OnClientReminderDismissing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReminderDismissing"] = value;
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x000E451E File Offset: 0x000E271E
		public static string ExportToICalendar(Appointment appointment)
		{
			return RadScheduler.ExportToICalendar(appointment, TimeSpan.Zero);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x000E452B File Offset: 0x000E272B
		public static string ExportToICalendar(Appointment appointment, TimeSpan timeZoneOffset)
		{
			return ICalUtil.Export(appointment, true, timeZoneOffset);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x000E4535 File Offset: 0x000E2735
		public static string ExportToICalendar(AppointmentCollection appointments)
		{
			return RadScheduler.ExportToICalendar(appointments, TimeSpan.Zero);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x000E4542 File Offset: 0x000E2742
		public static string ExportToICalendar(IEnumerable<Appointment> appointments)
		{
			return RadScheduler.ExportToICalendar(appointments, TimeSpan.Zero);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x000E454F File Offset: 0x000E274F
		public static string ExportToICalendar(AppointmentCollection appointments, TimeSpan timeZoneOffset)
		{
			return ICalUtil.Export(appointments, true, timeZoneOffset);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x000E4559 File Offset: 0x000E2759
		public static string ExportToICalendar(AppointmentCollection appointments, bool hasTimeZones)
		{
			return ICalUtil.ExportWithTimeZones(appointments, true, hasTimeZones);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x000E4563 File Offset: 0x000E2763
		public static string ExportToICalendar(Appointment appointment, bool hasTimeZones)
		{
			return ICalUtil.ExportWithTimeZones(appointment, true, hasTimeZones);
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x000E4570 File Offset: 0x000E2770
		public static string ExportToICalendar(IEnumerable<Appointment> appointments, TimeSpan timeZoneOffset)
		{
			AppointmentCollection appointmentCollection = new AppointmentCollection();
			appointmentCollection.AddRange(appointments);
			return ICalUtil.Export(appointmentCollection, true, timeZoneOffset);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x000E4592 File Offset: 0x000E2792
		public virtual Appointment CreateAppointment()
		{
			return this.AppointmentFactory.CreateAppointment();
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x000E45A0 File Offset: 0x000E27A0
		public DateTime UtcDayStart(DateTime utcDate)
		{
			return this.DisplayToUtc(this.UtcToDisplay(utcDate).Date);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x000E45C2 File Offset: 0x000E27C2
		public void ShowInlineEditForm(Appointment appointmentToEdit)
		{
			this.ShowInlineEditForm(appointmentToEdit, false);
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x000E45CC File Offset: 0x000E27CC
		public void ShowInlineEditForm(Appointment appointmentToEdit, bool editSeries)
		{
			this.EditingRecurringSeries = editSeries;
			this.SwitchToEditMode(appointmentToEdit, editSeries, false);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x000E45DE File Offset: 0x000E27DE
		public void ShowAdvancedEditForm(Appointment appointmentToEdit)
		{
			this.ShowAdvancedEditForm(appointmentToEdit, false);
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000E45E8 File Offset: 0x000E27E8
		public void ShowAdvancedEditForm(Appointment appointmentToEdit, bool editSeries)
		{
			this.EditingRecurringSeries = editSeries;
			this.SwitchToEditMode(appointmentToEdit, editSeries, true);
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000E45FA File Offset: 0x000E27FA
		public void ShowInlineInsertForm(DateTime showAt)
		{
			this.SwitchToInsertMode(showAt, showAt.AddMinutes((double)(this.MinutesPerRow * this.NumberOfHoveredRows)), false);
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000E461C File Offset: 0x000E281C
		public void ShowInlineInsertForm(ISchedulerTimeSlot timeSlot)
		{
			Appointment appointmentToInsert = this.CreateAppointment();
			this.ActiveModel.HandleInsert(timeSlot, null, appointmentToInsert);
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x000E463E File Offset: 0x000E283E
		public void ShowAdvancedInsertForm(DateTime showAt)
		{
			this.SwitchToInsertMode(showAt, showAt.AddMinutes((double)(this.MinutesPerRow * this.NumberOfHoveredRows)), true);
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000E465D File Offset: 0x000E285D
		public void ShowAlldayInlineInsertForm(DateTime showAt)
		{
			this.SwitchToInsertMode(showAt, showAt.AddDays(1.0), false);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x000E4677 File Offset: 0x000E2877
		public ISchedulerTimeSlot GetTimeSlotFromIndex(string index)
		{
			this.EnsureChildControls();
			return this.ActiveModel.GetSlotByIndex(index);
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x000E468B File Offset: 0x000E288B
		public void HideEditForm()
		{
			this.ActiveFormMode = SchedulerFormMode.Hidden;
			this.ClearChildControls();
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x000E469C File Offset: 0x000E289C
		public virtual DateTime UtcToDisplay(DateTime utcDate)
		{
			utcDate = DateTime.SpecifyKind(utcDate, DateTimeKind.Utc);
			if (this.TimeZonesEnabled)
			{
				return this.TimeZonesProvider.UtcToLocal(utcDate);
			}
			return new DateTime(utcDate.Add(this.TimeZoneOffset).Ticks, DateTimeKind.Unspecified);
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x000E46E4 File Offset: 0x000E28E4
		public virtual DateTime DisplayToUtc(DateTime displayDate)
		{
			displayDate = DateTime.SpecifyKind(displayDate, DateTimeKind.Unspecified);
			if (this.TimeZonesEnabled)
			{
				return this.TimeZonesProvider.LocalToUtc(displayDate);
			}
			return new DateTime(displayDate.Add(-this.TimeZoneOffset).Ticks, DateTimeKind.Utc);
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x000E472F File Offset: 0x000E292F
		public void InsertAppointment(Appointment appointmentToInsert)
		{
			this.AppointmentController.InsertAppointment(new SchedulerInfo(this), appointmentToInsert);
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x000E4743 File Offset: 0x000E2943
		public void UpdateAppointment(Appointment appointmentToUpdate)
		{
			this.UpdateAppointment(appointmentToUpdate, appointmentToUpdate);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x000E474D File Offset: 0x000E294D
		public void UpdateAppointment(Appointment appointmentToUpdate, Appointment originalAppointment)
		{
			this.AppointmentController.UpdateAppointment(new SchedulerInfo(this), originalAppointment, appointmentToUpdate);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x000E4762 File Offset: 0x000E2962
		public Appointment PrepareToEdit(Appointment appointmentToEdit, bool editSeries)
		{
			return this.AppointmentController.PrepareToEdit(appointmentToEdit, editSeries);
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x000E4771 File Offset: 0x000E2971
		public void DeleteAppointment(Appointment appointmentToDelete, bool deleteSeries)
		{
			this.AppointmentController.DeleteAppointment(new SchedulerInfo(this), appointmentToDelete, deleteSeries);
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x000E4786 File Offset: 0x000E2986
		public void RemoveRecurrenceExceptions(Appointment master)
		{
			this.AppointmentController.RemoveRecurrenceExceptions(new SchedulerInfo(this), master);
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x000E479A File Offset: 0x000E299A
		public void DismissAppointmentReminder(Appointment appointmentToUpdate, Appointment originalAppointment)
		{
			this.AppointmentController.DismissAppointmentReminder(new SchedulerInfo(this), appointmentToUpdate, originalAppointment);
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x000E47B0 File Offset: 0x000E29B0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowDelete", this.AllowDelete, true);
			base.DescribeProperty<bool>(descriptor, "allowEdit", this.AllowEdit, true);
			base.DescribeProperty<bool>(descriptor, "allowInsert", this.AllowInsert, true);
			base.DescribeProperty<AppointmentStyleMode>(descriptor, "appointmentStyleMode", this.AppointmentStyleMode, AppointmentStyleMode.Auto);
			base.DescribeProperty<bool>(descriptor, "_defaultAdvancedFormRendered", this.DefaultAdvancedFormRendered, false);
			base.DescribeProperty<bool>(descriptor, "displayDeleteConfirmation", this.DisplayDeleteConfirmation, true);
			base.DescribeProperty<bool>(descriptor, "displayRecurrenceActionDialogOnMove", this.DisplayRecurrenceActionDialogOnMove, false);
			base.DescribeProperty<bool>(descriptor, "_enableDescriptionField", this.EnableDescriptionField, false);
			base.DescribeProperty<DayOfWeek>(descriptor, "firstDayOfWeek", this.FirstDayOfWeek, DayOfWeek.Sunday);
			base.DescribeProperty<string>(descriptor, "groupBy", this.GroupBy, "");
			if (this.ShouldSerializeHoursPanelTimeFormat())
			{
				base.DescribeProperty<string>(descriptor, "hoursPanelTimeFormat", this.HoursPanelTimeFormat, null);
			}
			base.DescribeProperty<DayOfWeek>(descriptor, "lastDayOfWeek", this.LastDayOfWeek, DayOfWeek.Saturday);
			base.DescribeProperty<int>(descriptor, "minimumInlineFormHeight", this.MinimumInlineFormHeight, 50);
			base.DescribeProperty<int>(descriptor, "minimumInlineFormWidth", this.MinimumInlineFormWidth, 250);
			base.DescribeProperty<int>(descriptor, "minutesPerRow", this.MinutesPerRow, 30);
			base.DescribeProperty<int>(descriptor, "numberOfHoveredRows", this.NumberOfHoveredRows, 2);
			base.DescribeProperty<OverflowBehavior>(descriptor, "overflowBehavior", this.OverflowBehavior, OverflowBehavior.Scroll);
			base.DescribeProperty<string>(descriptor, "postBackReference", this.PostBackReference, null);
			base.DescribeProperty<bool>(descriptor, "readOnly", this.ReadOnly, false);
			base.DescribeProperty<string>(descriptor, "rowHeight", this.RowHeight.ToString(CultureInfo.InvariantCulture), "25px");
			base.DescribeProperty<int>(descriptor, "scrollLeft", this.ScrollLeft, 0);
			base.DescribeProperty<int>(descriptor, "scrollTop", this.ScrollTop, 0);
			base.DescribeProperty<SchedulerViewType>(descriptor, "selectedView", this.SelectedView, SchedulerViewType.DayView);
			base.DescribeProperty<bool>(descriptor, "shouldPostbackOnAppointmentContextMenuItemClick", this.ShouldPostbackOnAppointmentContextMenuItemClick, false);
			base.DescribeProperty<bool>(descriptor, "shouldPostbackOnClick", this.ShouldPostbackOnClick, true);
			base.DescribeProperty<bool>(descriptor, "_shouldPostbackOnReminderSnooze", this.ShouldPostbackOnReminderSnooze, false);
			base.DescribeProperty<bool>(descriptor, "shouldPostbackOnTimeSlotContextMenuItemClick", this.ShouldPostbackOnTimeSlotContextMenuItemClick, false);
			base.DescribeProperty<bool>(descriptor, "shouldUseClientInlineEditForm", this.ShouldUseClientInlineEditForm, true);
			base.DescribeProperty<bool>(descriptor, "shouldUseClientInlineInsertForm", this.ShouldUseClientInlineInsertForm, true);
			base.DescribeProperty<bool>(descriptor, "showAllDayRow", this.ShowAllDayRow, true);
			base.DescribeProperty<bool>(descriptor, "showFullTime", this.ShowFullTime, false);
			base.DescribeProperty<bool>(descriptor, "_startEditingInAdvancedForm", this.StartEditingInAdvancedForm, true);
			base.DescribeProperty<bool>(descriptor, "_startInsertingInAdvancedForm", this.StartInsertingInAdvancedForm, false);
			base.DescribeProperty<int>(descriptor, "timeLabelRowSpan", this.TimeLabelRowSpan, 2);
			base.DescribeProperty<string>(descriptor, "_uniqueId", this.UniqueID, null);
			base.DescribeProperty<bool>(descriptor, "_useHorizontalScrolling", this.UseHorizontalScrolling, false);
			if (this.ShouldSerializeValidationGroup())
			{
				base.DescribeProperty<string>(descriptor, "validationGroup", this.ValidationGroup, null);
			}
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x000E4AB0 File Offset: 0x000E2CB0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentClick", this.OnClientAppointmentClick);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentContextMenu", this.OnClientAppointmentContextMenu);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentContextMenuItemClicked", this.OnClientAppointmentContextMenuItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentContextMenuItemClicking", this.OnClientAppointmentContextMenuItemClicking);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentCreated", this.OnClientAppointmentCreated);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentDataBound", this.OnClientAppointmentDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentDeleting", this.OnClientAppointmentDeleting);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentDoubleClick", this.OnClientAppointmentDoubleClick);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentEditing", this.OnClientAppointmentEditing);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentInserting", this.OnClientAppointmentInserting);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentMoveEnd", this.OnClientAppointmentMoveEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentMoveStart", this.OnClientAppointmentMoveStart);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentMoving", this.OnClientAppointmentMoving);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentResizeEnd", this.OnClientAppointmentResizeEnd);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentResizeStart", this.OnClientAppointmentResizeStart);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentResizing", this.OnClientAppointmentResizing);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentSerialized", this.OnClientAppointmentSerialized);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentsPopulated", this.OnClientAppointmentsPopulated);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentsPopulating", this.OnClientAppointmentsPopulating);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentWebServiceDeleting", this.OnClientAppointmentWebServiceDeleting);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentWebServiceInserting", this.OnClientAppointmentWebServiceInserting);
			RadDataBoundControl.DescribeEvent(descriptor, "appointmentWebServiceUpdating", this.OnClientAppointmentWebServiceUpdating);
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.OnClientDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "formCreated", this.OnClientFormCreated);
			RadDataBoundControl.DescribeEvent(descriptor, "navigationCommand", this.OnClientNavigationCommand);
			RadDataBoundControl.DescribeEvent(descriptor, "navigationComplete", this.OnClientNavigationComplete);
			RadDataBoundControl.DescribeEvent(descriptor, "recurrenceActionDialogClosed", this.OnClientRecurrenceActionDialogClosed);
			RadDataBoundControl.DescribeEvent(descriptor, "recurrenceActionDialogShowing", this.OnClientRecurrenceActionDialogShowing);
			RadDataBoundControl.DescribeEvent(descriptor, "recurrenceExceptionCreating", this.OnClientRecurrenceExceptionCreating);
			RadDataBoundControl.DescribeEvent(descriptor, "recurrenceExceptionsRemoving", this.OnClientRecurrenceExceptionsRemoving);
			RadDataBoundControl.DescribeEvent(descriptor, "reminderDismissing", this.OnClientReminderDismissing);
			RadDataBoundControl.DescribeEvent(descriptor, "reminderSnoozing", this.OnClientReminderSnoozing);
			RadDataBoundControl.DescribeEvent(descriptor, "reminderTriggering", this.OnClientReminderTriggering);
			RadDataBoundControl.DescribeEvent(descriptor, "requestFailed", this.OnClientRequestFailed);
			RadDataBoundControl.DescribeEvent(descriptor, "requestSuccess", this.OnClientRequestSuccess);
			RadDataBoundControl.DescribeEvent(descriptor, "resourcesPopulated", this.OnClientResourcesPopulated);
			RadDataBoundControl.DescribeEvent(descriptor, "resourcesPopulating", this.OnClientResourcesPopulating);
			RadDataBoundControl.DescribeEvent(descriptor, "timeSlotClick", this.OnClientTimeSlotClick);
			RadDataBoundControl.DescribeEvent(descriptor, "timeSlotContextMenu", this.OnClientTimeSlotContextMenu);
			RadDataBoundControl.DescribeEvent(descriptor, "timeSlotContextMenuItemClicked", this.OnClientTimeSlotContextMenuItemClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "timeSlotContextMenuItemClicking", this.OnClientTimeSlotContextMenuItemClicking);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x000E4D80 File Offset: 0x000E2F80
		// Note: this type is marked as 'beforefieldinit'.
		static RadScheduler()
		{
			RadScheduler.AppointmentCommandEvent = new object();
			RadScheduler.AppointmentCreatedEvent = new object();
			RadScheduler.AppointmentContextMenuItemClickingEvent = new object();
			RadScheduler.AppointmentContextMenuItemClickedEvent = new object();
			RadScheduler.TimeSlotContextMenuItemClickingEvent = new object();
			RadScheduler.TimeSlotContextMenuItemClickedEvent = new object();
			RadScheduler.AppointmentDataBoundEvent = new object();
			RadScheduler.AppointmentInsertEvent = new object();
			RadScheduler.AppointmentUpdateEvent = new object();
			RadScheduler.AppointmentDeleteEvent = new object();
			RadScheduler.AppointmentClickEvent = new object();
			RadScheduler.SchedulerNavigationCommandEvent = new object();
			RadScheduler.SchedulerNavigationCompleteEvent = new object();
			RadScheduler.FormCreatingEvent = new object();
			RadScheduler.FormCreatedEvent = new object();
			RadScheduler.AppointmentCancelingEditEvent = new object();
			RadScheduler.TimeSlotCreatedEvent = new object();
			RadScheduler.OccurrenceDeleteEvent = new object();
			RadScheduler.RecurrenceExceptionCreatedEvent = new object();
			RadScheduler.ResourceHeaderCreatedEvent = new object();
			RadScheduler.ResourcesPopulatingEvent = new object();
			RadScheduler.AppointmentsPopulatingEvent = new object();
			RadScheduler.ReminderSnoozeEvent = new object();
			RadScheduler.ReminderDismissEvent = new object();
			RadScheduler.PdfExportingEvent = new object();
		}

		// Token: 0x04001235 RID: 4661
		private const int CELL_BORDER_ADJUSTMENT = 1;

		// Token: 0x04001236 RID: 4662
		internal const string JavaScriptDateFormat = "yyyy/MM/dd HH:mm";

		// Token: 0x04001237 RID: 4663
		internal const string BlankNavigateUrl = "#";

		// Token: 0x04001238 RID: 4664
		private const string ShortHoursPanelTimeFormat = "htt";

		// Token: 0x04001239 RID: 4665
		private const string LongHoursPanelTimeFormat = "h:mmtt";

		// Token: 0x0400123A RID: 4666
		private string _adjustedRowHeight = string.Empty;

		// Token: 0x0400123B RID: 4667
		private SchedulerProviderBase _appointmentProvider;

		// Token: 0x0400123C RID: 4668
		private AppointmentController _appointmentController;

		// Token: 0x0400123D RID: 4669
		private IComparer<Appointment> _appointmentComparer;

		// Token: 0x0400123E RID: 4670
		private object _providerContext;

		// Token: 0x0400123F RID: 4671
		private IAppointmentFactory _appointmentFactory;

		// Token: 0x04001240 RID: 4672
		private TimeZoneProviderBase _timeZoneProvider;

		// Token: 0x04001241 RID: 4673
		private AppointmentCollection _appointments;

		// Token: 0x04001242 RID: 4674
		private ResourceTypeCollection _resourceTypes;

		// Token: 0x04001243 RID: 4675
		private ResourceStyleMappingCollection _resourceStyles;

		// Token: 0x04001244 RID: 4676
		private ResourceCollection _resources;

		// Token: 0x04001245 RID: 4677
		private SchedulerFormMode _activeFormMode;

		// Token: 0x04001246 RID: 4678
		private DateTime? _visibleRangeStart;

		// Token: 0x04001247 RID: 4679
		private DateTime? _visibleRangeEnd;

		// Token: 0x04001248 RID: 4680
		private SchedulerStrings _localization;

		// Token: 0x04001249 RID: 4681
		private AdvancedFormSettings _advancedFormSettings;

		// Token: 0x0400124A RID: 4682
		private TimelineViewSettings _timelineViewSettings;

		// Token: 0x0400124B RID: 4683
		private WeekViewSettings _weekViewSettings;

		// Token: 0x0400124C RID: 4684
		private DayViewSettings _dayViewSettings;

		// Token: 0x0400124D RID: 4685
		private MultiDayViewSettings _multiDayViewSettings;

		// Token: 0x0400124E RID: 4686
		private MonthViewSettings _monthViewSettings;

		// Token: 0x0400124F RID: 4687
		private AgendaViewSettings _agendaViewSettings;

		// Token: 0x04001250 RID: 4688
		private YearViewSettings _yearViewSettings;

		// Token: 0x04001251 RID: 4689
		private ContextMenuSettings _appointmentContextMenuSettings;

		// Token: 0x04001252 RID: 4690
		private ContextMenuSettings _timeSlotContextMenuSettings;

		// Token: 0x04001253 RID: 4691
		private ReminderSettings _reminderSettings;

		// Token: 0x04001254 RID: 4692
		private bool _dataPropertyChanged;

		// Token: 0x04001255 RID: 4693
		private ITemplate _appointmentTemplate;

		// Token: 0x04001256 RID: 4694
		private ITemplate _inlineInsertTemplate;

		// Token: 0x04001257 RID: 4695
		private ITemplate _inlineEditTemplate;

		// Token: 0x04001258 RID: 4696
		private ITemplate _advancedInsertTemplate;

		// Token: 0x04001259 RID: 4697
		private ITemplate _advancedEditTemplate;

		// Token: 0x0400125A RID: 4698
		private ITemplate _resourceHeaderTemplate;

		// Token: 0x04001266 RID: 4710
		private static readonly object SchedulerNavigationCommandEvent;

		// Token: 0x04001267 RID: 4711
		private static readonly object SchedulerNavigationCompleteEvent;

		// Token: 0x04001274 RID: 4724
		private SchedulerWebServiceSettings _webServiceSettings;

		// Token: 0x04001275 RID: 4725
		private RadSchedulerContextMenuCollection _appointmentContextMenus;

		// Token: 0x04001276 RID: 4726
		private RadSchedulerContextMenuCollection _timeSlotContextMenus;

		// Token: 0x04001277 RID: 4727
		private ReminderDialog _reminderDialog;

		// Token: 0x04001278 RID: 4728
		private SchedulerExportSettings _exportSettings;

		// Token: 0x04001279 RID: 4729
		private IList<Appointment> _callbackAppointments = new List<Appointment>();

		// Token: 0x0400127A RID: 4730
		internal bool _updateOperationCanceled;

		// Token: 0x0400127B RID: 4731
		private bool _ignoreDataSourceViewChanged;

		// Token: 0x0400127C RID: 4732
		private bool _shouldBindAppointmentControls;

		// Token: 0x0400127D RID: 4733
		private readonly AutoResetEvent _resourceTypeLoaded = new AutoResetEvent(false);

		// Token: 0x020007F2 RID: 2034
		private class SchedulerStyle : Style
		{
			// Token: 0x060048E1 RID: 18657 RVA: 0x000E4E87 File Offset: 0x000E3087
			public SchedulerStyle(StateBag bag) : base(bag)
			{
				base.Height = Unit.Pixel(400);
			}
		}

		// Token: 0x020007F3 RID: 2035
		internal static class Defaults
		{
			// Token: 0x0400128D RID: 4749
			internal const int RowHeaderWidth = 52;

			// Token: 0x0400128E RID: 4750
			internal const string RowHeaderWidthFormatted = "52px";

			// Token: 0x0400128F RID: 4751
			internal static readonly TimeSpan DayStartTime = new TimeSpan(8, 0, 0);

			// Token: 0x04001290 RID: 4752
			internal static readonly TimeSpan DayEndTime = new TimeSpan(18, 0, 0);

			// Token: 0x04001291 RID: 4753
			internal static readonly TimeSpan WorkDayStartTime = new TimeSpan(8, 0, 0);

			// Token: 0x04001292 RID: 4754
			internal static readonly TimeSpan WorkDayEndTime = new TimeSpan(17, 0, 0);
		}
	}
}

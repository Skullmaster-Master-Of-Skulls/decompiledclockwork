using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A1F RID: 6687
	public class SchedulerWebServiceSettings : WebServiceSettings
	{
		// Token: 0x17004E93 RID: 20115
		// (get) Token: 0x060103A1 RID: 66465 RVA: 0x003A0A1C File Offset: 0x0039EC1C
		// (set) Token: 0x060103A2 RID: 66466 RVA: 0x003A0A24 File Offset: 0x0039EC24
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Method
		{
			get
			{
				return this.GetAppointmentsMethod;
			}
			set
			{
				base.ViewState["GetAppointmentsMethod"] = value;
			}
		}

		// Token: 0x17004E94 RID: 20116
		// (get) Token: 0x060103A3 RID: 66467 RVA: 0x003A0A37 File Offset: 0x0039EC37
		// (set) Token: 0x060103A4 RID: 66468 RVA: 0x003A0A58 File Offset: 0x0039EC58
		[Category("Behavior")]
		[DefaultValue(AppointmentUpdateMode.Batch)]
		[Description("Specifies the update mode for appointments in web service scenarios.")]
		public AppointmentUpdateMode UpdateMode
		{
			get
			{
				return (AppointmentUpdateMode)(base.ViewState["AppointmentUpdateMode"] ?? AppointmentUpdateMode.Batch);
			}
			set
			{
				base.ViewState["AppointmentUpdateMode"] = value;
			}
		}

		// Token: 0x17004E95 RID: 20117
		// (get) Token: 0x060103A5 RID: 66469 RVA: 0x003A0A70 File Offset: 0x0039EC70
		// (set) Token: 0x060103A6 RID: 66470 RVA: 0x003A0A90 File Offset: 0x0039EC90
		[DefaultValue("GetAppointments")]
		[Description("Specifies the web service method name to be used to populate the appointments.")]
		[Category("Behavior")]
		public string GetAppointmentsMethod
		{
			get
			{
				return (string)(base.ViewState["GetAppointmentsMethod"] ?? "GetAppointments");
			}
			set
			{
				base.ViewState["GetAppointmentsMethod"] = value;
			}
		}

		// Token: 0x17004E96 RID: 20118
		// (get) Token: 0x060103A7 RID: 66471 RVA: 0x003A0AA3 File Offset: 0x0039ECA3
		// (set) Token: 0x060103A8 RID: 66472 RVA: 0x003A0AC3 File Offset: 0x0039ECC3
		[Category("Behavior")]
		[DefaultValue("DeleteAppointment")]
		[Description("Specifies the web service method name to be used to delete appointments.")]
		public string DeleteAppointmentMethod
		{
			get
			{
				return (string)(base.ViewState["DeleteAppointmentMethod"] ?? "DeleteAppointment");
			}
			set
			{
				base.ViewState["DeleteAppointmentMethod"] = value;
			}
		}

		// Token: 0x17004E97 RID: 20119
		// (get) Token: 0x060103A9 RID: 66473 RVA: 0x003A0AD6 File Offset: 0x0039ECD6
		// (set) Token: 0x060103AA RID: 66474 RVA: 0x003A0AF6 File Offset: 0x0039ECF6
		[Category("Behavior")]
		[Description("Specifies the web service method name to be used to insert appointments.")]
		[DefaultValue("InsertAppointment")]
		public string InsertAppointmentMethod
		{
			get
			{
				return (string)(base.ViewState["InsertAppointmentMethod"] ?? "InsertAppointment");
			}
			set
			{
				base.ViewState["InsertAppointmentMethod"] = value;
			}
		}

		// Token: 0x17004E98 RID: 20120
		// (get) Token: 0x060103AB RID: 66475 RVA: 0x003A0B09 File Offset: 0x0039ED09
		// (set) Token: 0x060103AC RID: 66476 RVA: 0x003A0B29 File Offset: 0x0039ED29
		[Category("Behavior")]
		[Description("Specifies the web service method name to be used to update appointments.")]
		[DefaultValue("UpdateAppointment")]
		public string UpdateAppointmentMethod
		{
			get
			{
				return (string)(base.ViewState["UpdateAppointmentMethod"] ?? "UpdateAppointment");
			}
			set
			{
				base.ViewState["UpdateAppointmentMethod"] = value;
			}
		}

		// Token: 0x17004E99 RID: 20121
		// (get) Token: 0x060103AD RID: 66477 RVA: 0x003A0B3C File Offset: 0x0039ED3C
		// (set) Token: 0x060103AE RID: 66478 RVA: 0x003A0B5C File Offset: 0x0039ED5C
		[Description("Specifies the web service method name to be used to get the resources list.")]
		[Category("Behavior")]
		[DefaultValue("GetResources")]
		public string GetResourcesMethod
		{
			get
			{
				return (string)(base.ViewState["GetResourcesMethod"] ?? "GetResources");
			}
			set
			{
				base.ViewState["GetResourcesMethod"] = value;
			}
		}

		// Token: 0x17004E9A RID: 20122
		// (get) Token: 0x060103AF RID: 66479 RVA: 0x003A0B6F File Offset: 0x0039ED6F
		// (set) Token: 0x060103B0 RID: 66480 RVA: 0x003A0B8F File Offset: 0x0039ED8F
		[Description("Specifies the web service method name to create recurrence exceptions.")]
		[Category("Behavior")]
		[DefaultValue("CreateRecurrenceException")]
		public string CreateRecurrenceExceptionMethod
		{
			get
			{
				return (string)(base.ViewState["CreateRecurrenceExceptionMethod"] ?? "CreateRecurrenceException");
			}
			set
			{
				base.ViewState["CreateRecurrenceExceptionMethod"] = value;
			}
		}

		// Token: 0x17004E9B RID: 20123
		// (get) Token: 0x060103B1 RID: 66481 RVA: 0x003A0BA2 File Offset: 0x0039EDA2
		// (set) Token: 0x060103B2 RID: 66482 RVA: 0x003A0BC2 File Offset: 0x0039EDC2
		[Category("Behavior")]
		[Description("Specifies the web service method name to remove the recurrence exceptions of a given appointment.")]
		[DefaultValue("RemoveRecurrenceExceptions")]
		public string RemoveRecurrenceExceptionsMethod
		{
			get
			{
				return (string)(base.ViewState["RemoveRecurrenceExceptionsMethod"] ?? "RemoveRecurrenceExceptions");
			}
			set
			{
				base.ViewState["RemoveRecurrenceExceptionsMethod"] = value;
			}
		}

		// Token: 0x17004E9C RID: 20124
		// (get) Token: 0x060103B3 RID: 66483 RVA: 0x003A0BD5 File Offset: 0x0039EDD5
		// (set) Token: 0x060103B4 RID: 66484 RVA: 0x003A0BF6 File Offset: 0x0039EDF6
		[DefaultValue(SchedulerResourcePopulationMode.ClientSide)]
		[Category("Behavior")]
		[Description("Specifies the mode that will be used to retrieve resources when using Web Service data binding.")]
		public SchedulerResourcePopulationMode ResourcePopulationMode
		{
			get
			{
				return (SchedulerResourcePopulationMode)(base.ViewState["ResourcePopulationMode"] ?? SchedulerResourcePopulationMode.ClientSide);
			}
			set
			{
				base.ViewState["ResourcePopulationMode"] = value;
			}
		}

		// Token: 0x17004E9D RID: 20125
		// (get) Token: 0x060103B5 RID: 66485 RVA: 0x003A0C0E File Offset: 0x0039EE0E
		internal bool IsOData
		{
			get
			{
				return !string.IsNullOrEmpty(this.ODataSettings.ODataDataSourceID);
			}
		}

		// Token: 0x17004E9E RID: 20126
		// (get) Token: 0x060103B6 RID: 66486 RVA: 0x003A0C23 File Offset: 0x0039EE23
		[Description("OData settings")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SchedulerODataSettings ODataSettings
		{
			get
			{
				if (this._odataSettings == null)
				{
					this._odataSettings = new SchedulerODataSettings();
				}
				return this._odataSettings;
			}
		}

		// Token: 0x060103B7 RID: 66487 RVA: 0x003A0C3E File Offset: 0x0039EE3E
		public SchedulerWebServiceSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x060103B8 RID: 66488 RVA: 0x003A0C48 File Offset: 0x0039EE48
		public SchedulerWebServiceSettings(StateBag viewState) : this("WebServiceSettings", viewState)
		{
		}

		// Token: 0x060103B9 RID: 66489 RVA: 0x003A0C56 File Offset: 0x0039EE56
		internal override void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			if (!this.IsOData)
			{
				this.DescribeSchedulerWebServiceSettings(serializer, propertyName, descriptor);
				return;
			}
			this.ODataSettings.Describe(this, propertyName, serializer, descriptor);
		}

		// Token: 0x060103BA RID: 66490 RVA: 0x003A0C7C File Offset: 0x0039EE7C
		private void DescribeSchedulerWebServiceSettings(JavaScriptSerializer serializer, string propertyName, IScriptDescriptor descriptor)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new SchedulerWebServiceSettingsConverter()
			});
			SchedulerWebServiceSettingsConverter schedulerWebServiceSettingsConverter = new SchedulerWebServiceSettingsConverter();
			IDictionary<string, object> dictionary = schedulerWebServiceSettingsConverter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddProperty(propertyName, serializer.Serialize(this));
			}
		}

		// Token: 0x0400492A RID: 18730
		private SchedulerODataSettings _odataSettings;
	}
}

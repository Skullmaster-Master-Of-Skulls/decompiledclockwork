using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.Scheduler.OData;

namespace Telerik.Web.UI
{
	// Token: 0x02000E67 RID: 3687
	[RequiredScript(typeof(OData))]
	public class SchedulerODataSettings : ISchedulerData
	{
		// Token: 0x17002C33 RID: 11315
		// (get) Token: 0x06008BD8 RID: 35800 RVA: 0x001FCB73 File Offset: 0x001FAD73
		// (set) Token: 0x06008BD9 RID: 35801 RVA: 0x001FCB7B File Offset: 0x001FAD7B
		[Description("Gets or sets the id of the ODataDataSource control which has been used")]
		[DefaultValue("")]
		public string ODataDataSourceID
		{
			get
			{
				return this._oDataDataSourceID;
			}
			set
			{
				this._oDataDataSourceID = value;
			}
		}

		// Token: 0x17002C34 RID: 11316
		// (get) Token: 0x06008BDA RID: 35802 RVA: 0x001FCB84 File Offset: 0x001FAD84
		// (set) Token: 0x06008BDB RID: 35803 RVA: 0x001FCB8C File Offset: 0x001FAD8C
		[Description("Gets or sets the data recurrence parent field")]
		[DefaultValue("")]
		public string DataRecurrenceParentKeyField
		{
			get
			{
				return this._dataRecurrenceParentKeyField;
			}
			set
			{
				this._dataRecurrenceParentKeyField = value;
			}
		}

		// Token: 0x17002C35 RID: 11317
		// (get) Token: 0x06008BDC RID: 35804 RVA: 0x001FCB95 File Offset: 0x001FAD95
		// (set) Token: 0x06008BDD RID: 35805 RVA: 0x001FCB9D File Offset: 0x001FAD9D
		[Description("Gets or sets the data recurrence field")]
		[DefaultValue("")]
		public string DataRecurrenceField
		{
			get
			{
				return this._dataRecurrenceField;
			}
			set
			{
				this._dataRecurrenceField = value;
			}
		}

		// Token: 0x17002C36 RID: 11318
		// (get) Token: 0x06008BDE RID: 35806 RVA: 0x001FCBA6 File Offset: 0x001FADA6
		// (set) Token: 0x06008BDF RID: 35807 RVA: 0x001FCBAE File Offset: 0x001FADAE
		[Description("Gets or sets the data start field")]
		[DefaultValue("")]
		public string DataStartField
		{
			get
			{
				return this._dataStartField;
			}
			set
			{
				this._dataStartField = value;
			}
		}

		// Token: 0x17002C37 RID: 11319
		// (get) Token: 0x06008BE0 RID: 35808 RVA: 0x001FCBB7 File Offset: 0x001FADB7
		// (set) Token: 0x06008BE1 RID: 35809 RVA: 0x001FCBBF File Offset: 0x001FADBF
		[DefaultValue("")]
		[Description("Gets or sets the data end field")]
		public string DataEndField
		{
			get
			{
				return this._dataEndField;
			}
			set
			{
				this._dataEndField = value;
			}
		}

		// Token: 0x17002C38 RID: 11320
		// (get) Token: 0x06008BE2 RID: 35810 RVA: 0x001FCBC8 File Offset: 0x001FADC8
		// (set) Token: 0x06008BE3 RID: 35811 RVA: 0x001FCBD0 File Offset: 0x001FADD0
		[DefaultValue("")]
		[Description("Gets or sets the data description field")]
		public string DataDescriptionField
		{
			get
			{
				return this._dataDescriptionField;
			}
			set
			{
				this._dataDescriptionField = value;
			}
		}

		// Token: 0x17002C39 RID: 11321
		// (get) Token: 0x06008BE4 RID: 35812 RVA: 0x001FCBD9 File Offset: 0x001FADD9
		// (set) Token: 0x06008BE5 RID: 35813 RVA: 0x001FCBE1 File Offset: 0x001FADE1
		[DefaultValue("")]
		[Description("Gets or sets the data subject field")]
		public string DataSubjectField
		{
			get
			{
				return this._dataSubjectField;
			}
			set
			{
				this._dataSubjectField = value;
			}
		}

		// Token: 0x17002C3A RID: 11322
		// (get) Token: 0x06008BE6 RID: 35814 RVA: 0x001FCBEA File Offset: 0x001FADEA
		// (set) Token: 0x06008BE7 RID: 35815 RVA: 0x001FCBF2 File Offset: 0x001FADF2
		[Description("Gets or sets the data key field")]
		[DefaultValue("")]
		public string DataKeyField
		{
			get
			{
				return this._dataKeyField;
			}
			set
			{
				this._dataKeyField = value;
			}
		}

		// Token: 0x17002C3B RID: 11323
		// (get) Token: 0x06008BE8 RID: 35816 RVA: 0x001FCBFB File Offset: 0x001FADFB
		// (set) Token: 0x06008BE9 RID: 35817 RVA: 0x001FCC03 File Offset: 0x001FAE03
		[DefaultValue("")]
		[Description("Gets or sets the initial container name field")]
		public string DataModelID
		{
			get
			{
				return this._dataModelId;
			}
			set
			{
				this._dataModelId = value;
			}
		}

		// Token: 0x06008BEA RID: 35818 RVA: 0x001FCC0C File Offset: 0x001FAE0C
		public SchedulerODataSettings()
		{
			this.DataRecurrenceField = "";
			this.DataRecurrenceParentKeyField = "";
			this.DataStartField = "";
			this.DataEndField = "";
			this.DataDescriptionField = "";
			this.DataSubjectField = "";
			this.DataKeyField = "";
			this.DataModelID = "";
			this.ODataDataSourceID = "";
		}

		// Token: 0x17002C3C RID: 11324
		// (get) Token: 0x06008BEB RID: 35819 RVA: 0x001FCC82 File Offset: 0x001FAE82
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ODataResourceTypeCollection ResourceTypes
		{
			get
			{
				if (this._resourceTypes == null)
				{
					this._resourceTypes = new ODataResourceTypeCollection();
				}
				return this._resourceTypes;
			}
		}

		// Token: 0x06008BEC RID: 35820 RVA: 0x001FCCA0 File Offset: 0x001FAEA0
		internal virtual void Describe(SchedulerWebServiceSettings settings, string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ODataSettingsConverter()
			});
			descriptor.AddProperty(propertyName, serializer.Serialize(settings));
		}

		// Token: 0x04002728 RID: 10024
		public const string SchedulerODataScriptName = "Telerik.Web.UI.Scheduler.ClientRendering.OData.SchedulerODataSettings.js";

		// Token: 0x04002729 RID: 10025
		private string _dataRecurrenceParentKeyField;

		// Token: 0x0400272A RID: 10026
		private string _dataRecurrenceField;

		// Token: 0x0400272B RID: 10027
		private string _dataStartField;

		// Token: 0x0400272C RID: 10028
		private string _dataEndField;

		// Token: 0x0400272D RID: 10029
		private string _dataDescriptionField;

		// Token: 0x0400272E RID: 10030
		private string _dataSubjectField;

		// Token: 0x0400272F RID: 10031
		private string _dataKeyField;

		// Token: 0x04002730 RID: 10032
		private string _dataModelId;

		// Token: 0x04002731 RID: 10033
		private string _oDataDataSourceID;

		// Token: 0x04002732 RID: 10034
		private ODataResourceTypeCollection _resourceTypes;
	}
}

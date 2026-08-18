using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;

namespace Telerik.Web.UI.DataSourceSettings
{
	// Token: 0x02000105 RID: 261
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataSourceControlSettings : StateManager
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00026D7A File Offset: 0x00024F7A
		internal bool ShouldSerializeDataSourceID
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataSourceID);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00026D8A File Offset: 0x00024F8A
		internal bool ShouldSerializeDataKeyNames
		{
			get
			{
				return this.DataKeyNames.Length > 0;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00026D97 File Offset: 0x00024F97
		internal bool ShouldSerializeDataFields
		{
			get
			{
				return this.DataFields.Length > 0;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00026DA4 File Offset: 0x00024FA4
		internal bool ShouldSerializeDataMember
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataMember);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00026DB4 File Offset: 0x00024FB4
		internal bool ShouldSerializeDataModelID
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataModelID);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00026DC4 File Offset: 0x00024FC4
		internal bool ShouldSerializeUpdateMethod
		{
			get
			{
				return !string.IsNullOrEmpty(this.UpdateMethod);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00026DD4 File Offset: 0x00024FD4
		internal bool ShouldSerializeInsertMethod
		{
			get
			{
				return !string.IsNullOrEmpty(this.InsertMethod);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00026DE4 File Offset: 0x00024FE4
		internal bool ShouldSerializeDeleteMethod
		{
			get
			{
				return !string.IsNullOrEmpty(this.DeleteMethod);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00026DF4 File Offset: 0x00024FF4
		internal bool ShouldSerializeSelectMethod
		{
			get
			{
				return !string.IsNullOrEmpty(this.SelectMethod);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00026E04 File Offset: 0x00025004
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00026E24 File Offset: 0x00025024
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Gets or set the server side data source control ID to which the ClientSideDataSouce is bound.")]
		public virtual string DataSourceID
		{
			get
			{
				return (base.ViewState["DataSourceID"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataSourceID"] = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00026E38 File Offset: 0x00025038
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00026E70 File Offset: 0x00025070
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(DataSourceStringArrayConverter))]
		[DefaultValue(null)]
		[Category("Data")]
		[Description("Comma delimited list of data key Names")]
		public virtual string[] DataKeyNames
		{
			get
			{
				object obj = base.ViewState["DataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				base.ViewState["DataKeyNames"] = ((value != null) ? value.Clone() : null);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00026E90 File Offset: 0x00025090
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00026EC8 File Offset: 0x000250C8
		[TypeConverter(typeof(DataSourceStringArrayConverter))]
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[Category("Data")]
		[Description("Comma delimited list of data fields")]
		public virtual string[] DataFields
		{
			get
			{
				object obj = base.ViewState["DataFields"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				base.ViewState["DataFields"] = ((value != null) ? value.Clone() : null);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00026EE8 File Offset: 0x000250E8
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00026F16 File Offset: 0x00025116
		[NotifyParentProperty(true)]
		[Category("Data editing")]
		[DefaultValue(false)]
		public bool AllowAutomaticUpdates
		{
			get
			{
				object obj = base.ViewState["_aau"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["_aau"] = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00026F30 File Offset: 0x00025130
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00026F5E File Offset: 0x0002515E
		[NotifyParentProperty(true)]
		[Category("Data editing")]
		[DefaultValue(false)]
		public bool AllowAutomaticInserts
		{
			get
			{
				object obj = base.ViewState["_aai"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["_aai"] = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00026F78 File Offset: 0x00025178
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00026FA6 File Offset: 0x000251A6
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Data editing")]
		public bool AllowAutomaticDeletes
		{
			get
			{
				object obj = base.ViewState["_aad"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["_aad"] = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00026FBE File Offset: 0x000251BE
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00026FDE File Offset: 0x000251DE
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string DataMember
		{
			get
			{
				return (string)(base.ViewState["DataMember"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMember"] = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00026FF1 File Offset: 0x000251F1
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00027011 File Offset: 0x00025211
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataModelID
		{
			get
			{
				return (string)(base.ViewState["DataModelID"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataModelID"] = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00027024 File Offset: 0x00025224
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00027044 File Offset: 0x00025244
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the method to call in order to update data")]
		[NotifyParentProperty(true)]
		public virtual string UpdateMethod
		{
			get
			{
				return (string)(base.ViewState["UpdateMethod"] ?? string.Empty);
			}
			set
			{
				base.ViewState["UpdateMethod"] = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00027057 File Offset: 0x00025257
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00027077 File Offset: 0x00025277
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the method to call in order to insert data")]
		public virtual string InsertMethod
		{
			get
			{
				return (string)(base.ViewState["InsertMethod"] ?? string.Empty);
			}
			set
			{
				base.ViewState["InsertMethod"] = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002708A File Offset: 0x0002528A
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x000270AA File Offset: 0x000252AA
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to delete data")]
		public virtual string DeleteMethod
		{
			get
			{
				return (string)(base.ViewState["DeleteMethod"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DeleteMethod"] = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000270BD File Offset: 0x000252BD
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x000270DD File Offset: 0x000252DD
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of the method to call in order to retrieve data")]
		[Category("Data")]
		public virtual string SelectMethod
		{
			get
			{
				return (string)(base.ViewState["SelectMethod"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectMethod"] = value;
			}
		}
	}
}

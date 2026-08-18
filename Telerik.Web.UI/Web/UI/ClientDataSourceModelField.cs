using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000111 RID: 273
	public class ClientDataSourceModelField : StateManager
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000277B7 File Offset: 0x000259B7
		internal bool ShouldSerializeFieldName
		{
			get
			{
				return !string.IsNullOrEmpty(this.FieldName);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000277C7 File Offset: 0x000259C7
		internal bool ShouldSerializeOriginalFieldName
		{
			get
			{
				return !string.IsNullOrEmpty(this.OriginalFieldName);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000277D7 File Offset: 0x000259D7
		internal bool ShouldSerializeParseFunctionName
		{
			get
			{
				return !string.IsNullOrEmpty(this.ParseFunctionName);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x000277E7 File Offset: 0x000259E7
		internal bool ShouldSerializeDataType
		{
			get
			{
				return this.DataType != ClientDataSourceModelFieldType.String;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x000277F5 File Offset: 0x000259F5
		internal bool ShouldSerializeDefaultValue
		{
			get
			{
				return this.DefaultValue != null && this.DefaultValue.ToString() != string.Empty;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00027816 File Offset: 0x00025A16
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00027836 File Offset: 0x00025A36
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the field from the model")]
		[NotifyParentProperty(true)]
		public virtual string FieldName
		{
			get
			{
				return (base.ViewState["FieldName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["FieldName"] = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002784C File Offset: 0x00025A4C
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00027886 File Offset: 0x00025A86
		[DefaultValue(true)]
		public virtual bool IgnoreCase
		{
			get
			{
				bool? flag = base.ViewState["IgnoreCase"] as bool?;
				return flag == null || flag.Value;
			}
			set
			{
				base.ViewState["IgnoreCase"] = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0002789E File Offset: 0x00025A9E
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x000278BE File Offset: 0x00025ABE
		[DefaultValue("")]
		[Description("Gets or sets the name of the orginal field from the data.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual string OriginalFieldName
		{
			get
			{
				return (base.ViewState["OriginalFieldName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OriginalFieldName"] = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000278D1 File Offset: 0x00025AD1
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x000278F1 File Offset: 0x00025AF1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of function which will parse the field value. If not set default parsers will be used.")]
		[Category("Behavior")]
		public virtual string ParseFunctionName
		{
			get
			{
				return (base.ViewState["ParseFunctionName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ParseFunctionName"] = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00027904 File Offset: 0x00025B04
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x00027925 File Offset: 0x00025B25
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets field from the Model is editable")]
		public virtual bool Editable
		{
			get
			{
				return (bool)(base.ViewState["Editable"] ?? true);
			}
			set
			{
				base.ViewState["Editable"] = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002793D File Offset: 0x00025B3D
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0002795E File Offset: 0x00025B5E
		[DefaultValue(true)]
		[Description("Gets or sets field from the Model is nullable")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool Nullable
		{
			get
			{
				return (bool)(base.ViewState["Nullable"] ?? true);
			}
			set
			{
				base.ViewState["Nullable"] = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00027978 File Offset: 0x00025B78
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x000279B1 File Offset: 0x00025BB1
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets the default value of the field from the model")]
		[TypeConverter(typeof(ModelFieldDefaultValueTypeConverter))]
		[DefaultValue("")]
		public virtual object DefaultValue
		{
			get
			{
				object obj = base.ViewState["DefaultValue"];
				if (obj == null)
				{
					return string.Empty;
				}
				ModelFieldDefaultValueWrapper modelFieldDefaultValueWrapper = obj as ModelFieldDefaultValueWrapper;
				if (modelFieldDefaultValueWrapper != null)
				{
					return modelFieldDefaultValueWrapper.DefaultValue;
				}
				return obj;
			}
			set
			{
				base.ViewState["DefaultValue"] = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000279C4 File Offset: 0x00025BC4
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x000279ED File Offset: 0x00025BED
		[Description("Gets or sets the corresponding client-side type of the field from the model")]
		[Category("Behavior")]
		[DefaultValue(ClientDataSourceModelFieldType.String)]
		[NotifyParentProperty(true)]
		public virtual ClientDataSourceModelFieldType DataType
		{
			get
			{
				object obj = base.ViewState["DataType"];
				if (obj != null)
				{
					return (ClientDataSourceModelFieldType)obj;
				}
				return ClientDataSourceModelFieldType.String;
			}
			set
			{
				base.ViewState["DataType"] = value;
			}
		}
	}
}

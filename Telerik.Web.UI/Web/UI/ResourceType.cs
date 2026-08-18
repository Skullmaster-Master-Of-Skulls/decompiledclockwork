using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020012F1 RID: 4849
	[DefaultProperty("Name")]
	public class ResourceType : StateManager, IEquatable<ResourceType>
	{
		// Token: 0x0600CBA4 RID: 52132 RVA: 0x002D8104 File Offset: 0x002D6304
		public ResourceType()
		{
		}

		// Token: 0x0600CBA5 RID: 52133 RVA: 0x002D810C File Offset: 0x002D630C
		public ResourceType(string resourceTypeName)
		{
			this.Name = resourceTypeName;
		}

		// Token: 0x0600CBA6 RID: 52134 RVA: 0x002D811B File Offset: 0x002D631B
		public ResourceType(string resourceTypeName, bool allowMultipleResourceValues) : this(resourceTypeName)
		{
			this.AllowMultipleValues = allowMultipleResourceValues;
		}

		// Token: 0x170041AE RID: 16814
		// (get) Token: 0x0600CBA7 RID: 52135 RVA: 0x002D812B File Offset: 0x002D632B
		// (set) Token: 0x0600CBA8 RID: 52136 RVA: 0x002D815A File Offset: 0x002D635A
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.ResourceTypeDataFieldConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		public string KeyField
		{
			get
			{
				if (base.ViewState["KeyField"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["KeyField"];
			}
			set
			{
				base.ViewState["KeyField"] = value;
			}
		}

		// Token: 0x170041AF RID: 16815
		// (get) Token: 0x0600CBA9 RID: 52137 RVA: 0x002D816D File Offset: 0x002D636D
		// (set) Token: 0x0600CBAA RID: 52138 RVA: 0x002D8175 File Offset: 0x002D6375
		[Browsable(false)]
		public object DataSource { get; set; }

		// Token: 0x170041B0 RID: 16816
		// (get) Token: 0x0600CBAB RID: 52139 RVA: 0x002D817E File Offset: 0x002D637E
		// (set) Token: 0x0600CBAC RID: 52140 RVA: 0x002D81AD File Offset: 0x002D63AD
		[DefaultValue("")]
		public string Name
		{
			get
			{
				if (base.ViewState["Name"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Name"];
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x170041B1 RID: 16817
		// (get) Token: 0x0600CBAD RID: 52141 RVA: 0x002D81C0 File Offset: 0x002D63C0
		// (set) Token: 0x0600CBAE RID: 52142 RVA: 0x002D81EF File Offset: 0x002D63EF
		[TypeConverter("Telerik.Web.Design.ResourceTypeDataFieldConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[DefaultValue("")]
		public string TextField
		{
			get
			{
				if (base.ViewState["TextField"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["TextField"];
			}
			set
			{
				base.ViewState["TextField"] = value;
			}
		}

		// Token: 0x170041B2 RID: 16818
		// (get) Token: 0x0600CBAF RID: 52143 RVA: 0x002D8202 File Offset: 0x002D6402
		// (set) Token: 0x0600CBB0 RID: 52144 RVA: 0x002D8231 File Offset: 0x002D6431
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.ResourceForeignKeyDataFieldConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		public string ForeignKeyField
		{
			get
			{
				if (base.ViewState["ForeignKeyField"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ForeignKeyField"];
			}
			set
			{
				base.ViewState["ForeignKeyField"] = value;
			}
		}

		// Token: 0x170041B3 RID: 16819
		// (get) Token: 0x0600CBB1 RID: 52145 RVA: 0x002D8244 File Offset: 0x002D6444
		// (set) Token: 0x0600CBB2 RID: 52146 RVA: 0x002D8273 File Offset: 0x002D6473
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.ResourceTypeDataSourceIDConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		public string DataSourceID
		{
			get
			{
				if (base.ViewState["DataSourceID"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["DataSourceID"];
			}
			set
			{
				base.ViewState["DataSourceID"] = value;
			}
		}

		// Token: 0x170041B4 RID: 16820
		// (get) Token: 0x0600CBB3 RID: 52147 RVA: 0x002D8286 File Offset: 0x002D6486
		// (set) Token: 0x0600CBB4 RID: 52148 RVA: 0x002D82A7 File Offset: 0x002D64A7
		[DefaultValue(false)]
		public bool AllowMultipleValues
		{
			get
			{
				return (bool)(base.ViewState["AllowMultipleValues"] ?? false);
			}
			set
			{
				base.ViewState["AllowMultipleValues"] = value;
			}
		}

		// Token: 0x0600CBB5 RID: 52149 RVA: 0x002D82C0 File Offset: 0x002D64C0
		public override bool Equals(object obj)
		{
			ResourceType resourceType = obj as ResourceType;
			return !(resourceType == null) && this.Equals(resourceType);
		}

		// Token: 0x0600CBB6 RID: 52150 RVA: 0x002D82E6 File Offset: 0x002D64E6
		public bool Equals(ResourceType resType)
		{
			return !(resType == null) && this.Name == resType.Name && this.AllowMultipleValues == resType.AllowMultipleValues;
		}

		// Token: 0x0600CBB7 RID: 52151 RVA: 0x002D8316 File Offset: 0x002D6516
		public static bool operator ==(ResourceType o1, ResourceType o2)
		{
			if (o1 != null)
			{
				return o1.Equals(o2);
			}
			return o2 == null;
		}

		// Token: 0x0600CBB8 RID: 52152 RVA: 0x002D8327 File Offset: 0x002D6527
		public static bool operator !=(ResourceType o1, ResourceType o2)
		{
			if (o1 != null)
			{
				return !o1.Equals(o2);
			}
			return o2 != null;
		}

		// Token: 0x0600CBB9 RID: 52153 RVA: 0x002D8340 File Offset: 0x002D6540
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.AllowMultipleValues.GetHashCode();
		}
	}
}

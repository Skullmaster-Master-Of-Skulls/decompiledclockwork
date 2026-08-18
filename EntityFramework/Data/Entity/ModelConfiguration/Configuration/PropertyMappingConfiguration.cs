using System;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C8 RID: 456
	public class PropertyMappingConfiguration
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x0004113D File Offset: 0x0003F33D
		internal PropertyMappingConfiguration(PrimitivePropertyConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0004114C File Offset: 0x0003F34C
		internal PrimitivePropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00041154 File Offset: 0x0003F354
		public PropertyMappingConfiguration HasColumnName(string columnName)
		{
			this.Configuration.ColumnName = columnName;
			return this;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00041163 File Offset: 0x0003F363
		public PropertyMappingConfiguration HasColumnAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this.Configuration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x0400041F RID: 1055
		private readonly PrimitivePropertyConfiguration _configuration;
	}
}

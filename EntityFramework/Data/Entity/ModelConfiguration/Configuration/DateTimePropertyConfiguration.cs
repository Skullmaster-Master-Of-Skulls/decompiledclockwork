using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D4 RID: 2004
	public class DateTimePropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x06005B02 RID: 23298 RVA: 0x001878DE File Offset: 0x00185ADE
		internal DateTimePropertyConfiguration(DateTimePropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x001878E7 File Offset: 0x00185AE7
		public new DateTimePropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x001878F1 File Offset: 0x00185AF1
		public new DateTimePropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x001878FB File Offset: 0x00185AFB
		public new DateTimePropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x00187906 File Offset: 0x00185B06
		public new DateTimePropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x00187910 File Offset: 0x00185B10
		public new DateTimePropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x0018791B File Offset: 0x00185B1B
		public new DateTimePropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x00187926 File Offset: 0x00185B26
		public new DateTimePropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x00187932 File Offset: 0x00185B32
		public new DateTimePropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x0018793D File Offset: 0x00185B3D
		public new DateTimePropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x00187948 File Offset: 0x00185B48
		public DateTimePropertyConfiguration HasPrecision(byte value)
		{
			this.Configuration.Precision = new byte?(value);
			return this;
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06005B0D RID: 23309 RVA: 0x0018795C File Offset: 0x00185B5C
		internal new DateTimePropertyConfiguration Configuration
		{
			get
			{
				return (DateTimePropertyConfiguration)base.Configuration;
			}
		}
	}
}

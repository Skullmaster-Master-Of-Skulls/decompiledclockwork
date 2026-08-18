using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D5 RID: 2005
	public class DecimalPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x06005B0E RID: 23310 RVA: 0x00187969 File Offset: 0x00185B69
		internal DecimalPropertyConfiguration(DecimalPropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x00187972 File Offset: 0x00185B72
		public new DecimalPropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x0018797C File Offset: 0x00185B7C
		public new DecimalPropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x00187986 File Offset: 0x00185B86
		public new DecimalPropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x00187991 File Offset: 0x00185B91
		public new DecimalPropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x0018799B File Offset: 0x00185B9B
		public new DecimalPropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06005B14 RID: 23316 RVA: 0x001879A6 File Offset: 0x00185BA6
		public new DecimalPropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x001879B1 File Offset: 0x00185BB1
		public new DecimalPropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x001879BD File Offset: 0x00185BBD
		public new DecimalPropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x001879C8 File Offset: 0x00185BC8
		public new DecimalPropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x001879D3 File Offset: 0x00185BD3
		public DecimalPropertyConfiguration HasPrecision(byte precision, byte scale)
		{
			this.Configuration.Precision = new byte?(precision);
			this.Configuration.Scale = new byte?(scale);
			return this;
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x001879F8 File Offset: 0x00185BF8
		internal new DecimalPropertyConfiguration Configuration
		{
			get
			{
				return (DecimalPropertyConfiguration)base.Configuration;
			}
		}
	}
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D3 RID: 2003
	public class BinaryPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x06005AF2 RID: 23282 RVA: 0x0018782A File Offset: 0x00185A2A
		internal BinaryPropertyConfiguration(BinaryPropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x00187833 File Offset: 0x00185A33
		public new BinaryPropertyConfiguration IsMaxLength()
		{
			base.IsMaxLength();
			return this;
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0018783D File Offset: 0x00185A3D
		public new BinaryPropertyConfiguration HasMaxLength(int? value)
		{
			base.HasMaxLength(value);
			return this;
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x00187848 File Offset: 0x00185A48
		public new BinaryPropertyConfiguration IsFixedLength()
		{
			base.IsFixedLength();
			return this;
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x00187852 File Offset: 0x00185A52
		public new BinaryPropertyConfiguration IsVariableLength()
		{
			base.IsVariableLength();
			return this;
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x0018785C File Offset: 0x00185A5C
		public new BinaryPropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x00187866 File Offset: 0x00185A66
		public new BinaryPropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06005AF9 RID: 23289 RVA: 0x00187870 File Offset: 0x00185A70
		public new BinaryPropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06005AFA RID: 23290 RVA: 0x0018787B File Offset: 0x00185A7B
		public new BinaryPropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x00187885 File Offset: 0x00185A85
		public new BinaryPropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x00187890 File Offset: 0x00185A90
		public new BinaryPropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x0018789B File Offset: 0x00185A9B
		public new BinaryPropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x001878A7 File Offset: 0x00185AA7
		public new BinaryPropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x001878B2 File Offset: 0x00185AB2
		public new BinaryPropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x001878BD File Offset: 0x00185ABD
		public BinaryPropertyConfiguration IsRowVersion()
		{
			this.Configuration.IsRowVersion = new bool?(true);
			return this;
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005B01 RID: 23297 RVA: 0x001878D1 File Offset: 0x00185AD1
		internal new BinaryPropertyConfiguration Configuration
		{
			get
			{
				return (BinaryPropertyConfiguration)base.Configuration;
			}
		}
	}
}

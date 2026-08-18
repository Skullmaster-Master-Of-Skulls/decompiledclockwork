using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D6 RID: 2006
	public class StringPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x06005B1A RID: 23322 RVA: 0x00187A05 File Offset: 0x00185C05
		internal StringPropertyConfiguration(StringPropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x00187A0E File Offset: 0x00185C0E
		public new StringPropertyConfiguration IsMaxLength()
		{
			base.IsMaxLength();
			return this;
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x00187A18 File Offset: 0x00185C18
		public new StringPropertyConfiguration HasMaxLength(int? value)
		{
			base.HasMaxLength(value);
			return this;
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x00187A23 File Offset: 0x00185C23
		public new StringPropertyConfiguration IsFixedLength()
		{
			base.IsFixedLength();
			return this;
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x00187A2D File Offset: 0x00185C2D
		public new StringPropertyConfiguration IsVariableLength()
		{
			base.IsVariableLength();
			return this;
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x00187A37 File Offset: 0x00185C37
		public new StringPropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x00187A41 File Offset: 0x00185C41
		public new StringPropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x00187A4B File Offset: 0x00185C4B
		public new StringPropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x00187A56 File Offset: 0x00185C56
		public new StringPropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x00187A60 File Offset: 0x00185C60
		public new StringPropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x00187A6B File Offset: 0x00185C6B
		public new StringPropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x00187A76 File Offset: 0x00185C76
		public new StringPropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x00187A82 File Offset: 0x00185C82
		public new StringPropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x00187A8D File Offset: 0x00185C8D
		public new StringPropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x00187A98 File Offset: 0x00185C98
		public StringPropertyConfiguration IsUnicode()
		{
			this.IsUnicode(new bool?(true));
			return this;
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x00187AA8 File Offset: 0x00185CA8
		public StringPropertyConfiguration IsUnicode(bool? unicode)
		{
			this.Configuration.IsUnicode = unicode;
			return this;
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005B2A RID: 23338 RVA: 0x00187AB7 File Offset: 0x00185CB7
		internal new StringPropertyConfiguration Configuration
		{
			get
			{
				return (StringPropertyConfiguration)base.Configuration;
			}
		}
	}
}

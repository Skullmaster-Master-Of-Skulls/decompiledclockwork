using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D1 RID: 2001
	public class PrimitivePropertyConfiguration
	{
		// Token: 0x06005ADC RID: 23260 RVA: 0x001875FB File Offset: 0x001857FB
		internal PrimitivePropertyConfiguration(PrimitivePropertyConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x0018760A File Offset: 0x0018580A
		internal PrimitivePropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x00187612 File Offset: 0x00185812
		public PrimitivePropertyConfiguration IsOptional()
		{
			this.Configuration.IsNullable = new bool?(true);
			return this;
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x00187626 File Offset: 0x00185826
		public PrimitivePropertyConfiguration IsRequired()
		{
			this.Configuration.IsNullable = new bool?(false);
			return this;
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0018763A File Offset: 0x0018583A
		public PrimitivePropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			if (databaseGeneratedOption != null && !Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
			{
				throw new ArgumentOutOfRangeException("databaseGeneratedOption");
			}
			this.Configuration.DatabaseGeneratedOption = databaseGeneratedOption;
			return this;
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x00187674 File Offset: 0x00185874
		public PrimitivePropertyConfiguration IsConcurrencyToken()
		{
			this.IsConcurrencyToken(new bool?(true));
			return this;
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x00187684 File Offset: 0x00185884
		public PrimitivePropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			this.Configuration.ConcurrencyMode = ((concurrencyToken == null) ? null : new ConcurrencyMode?(concurrencyToken.Value ? ConcurrencyMode.Fixed : ConcurrencyMode.None));
			return this;
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x001876C3 File Offset: 0x001858C3
		public PrimitivePropertyConfiguration HasColumnType(string columnType)
		{
			this.Configuration.ColumnType = columnType;
			return this;
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x001876D2 File Offset: 0x001858D2
		public PrimitivePropertyConfiguration HasColumnName(string columnName)
		{
			this.Configuration.ColumnName = columnName;
			return this;
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x001876E1 File Offset: 0x001858E1
		public PrimitivePropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this.Configuration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x001876FD File Offset: 0x001858FD
		public PrimitivePropertyConfiguration HasParameterName(string parameterName)
		{
			this.Configuration.ParameterName = parameterName;
			return this;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x0018770C File Offset: 0x0018590C
		public PrimitivePropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			if (columnOrder != null && columnOrder.Value < 0)
			{
				throw new ArgumentOutOfRangeException("columnOrder");
			}
			this.Configuration.ColumnOrder = columnOrder;
			return this;
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x00187739 File Offset: 0x00185939
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x00187741 File Offset: 0x00185941
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x0018774A File Offset: 0x0018594A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x00187752 File Offset: 0x00185952
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002436 RID: 9270
		private readonly PrimitivePropertyConfiguration _configuration;
	}
}

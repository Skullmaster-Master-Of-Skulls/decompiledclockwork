using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007B7 RID: 1975
	public class PrimitiveColumnConfiguration
	{
		// Token: 0x0600595A RID: 22874 RVA: 0x00180F50 File Offset: 0x0017F150
		internal PrimitiveColumnConfiguration(PrimitivePropertyConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x0600595B RID: 22875 RVA: 0x00180F5F File Offset: 0x0017F15F
		internal PrimitivePropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00180F67 File Offset: 0x0017F167
		public PrimitiveColumnConfiguration IsOptional()
		{
			this.Configuration.IsNullable = new bool?(true);
			return this;
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00180F7B File Offset: 0x0017F17B
		public PrimitiveColumnConfiguration IsRequired()
		{
			this.Configuration.IsNullable = new bool?(false);
			return this;
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00180F8F File Offset: 0x0017F18F
		public PrimitiveColumnConfiguration HasColumnType(string columnType)
		{
			this.Configuration.ColumnType = columnType;
			return this;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x00180F9E File Offset: 0x0017F19E
		public PrimitiveColumnConfiguration HasColumnOrder(int? columnOrder)
		{
			if (columnOrder != null && columnOrder.Value < 0)
			{
				throw new ArgumentOutOfRangeException("columnOrder");
			}
			this.Configuration.ColumnOrder = columnOrder;
			return this;
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x00180FCB File Offset: 0x0017F1CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00180FD3 File Offset: 0x0017F1D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00180FDC File Offset: 0x0017F1DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00180FE4 File Offset: 0x0017F1E4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040023B6 RID: 9142
		private readonly PrimitivePropertyConfiguration _configuration;
	}
}

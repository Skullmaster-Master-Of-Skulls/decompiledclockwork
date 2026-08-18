using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007B8 RID: 1976
	public abstract class LengthColumnConfiguration : PrimitiveColumnConfiguration
	{
		// Token: 0x06005964 RID: 22884 RVA: 0x00180FEC File Offset: 0x0017F1EC
		internal LengthColumnConfiguration(LengthPropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005965 RID: 22885 RVA: 0x00180FF5 File Offset: 0x0017F1F5
		internal new LengthPropertyConfiguration Configuration
		{
			get
			{
				return (LengthPropertyConfiguration)base.Configuration;
			}
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x00181004 File Offset: 0x0017F204
		public LengthColumnConfiguration IsMaxLength()
		{
			this.Configuration.IsMaxLength = new bool?(true);
			this.Configuration.MaxLength = null;
			return this;
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x00181038 File Offset: 0x0017F238
		public LengthColumnConfiguration HasMaxLength(int? value)
		{
			this.Configuration.MaxLength = value;
			this.Configuration.IsMaxLength = null;
			return this;
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00181066 File Offset: 0x0017F266
		public LengthColumnConfiguration IsFixedLength()
		{
			this.Configuration.IsFixedLength = new bool?(true);
			return this;
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0018107A File Offset: 0x0017F27A
		public LengthColumnConfiguration IsVariableLength()
		{
			this.Configuration.IsFixedLength = new bool?(false);
			return this;
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x0018108E File Offset: 0x0017F28E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00181096 File Offset: 0x0017F296
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x0018109F File Offset: 0x0017F29F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x001810A7 File Offset: 0x0017F2A7
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

using System;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D2 RID: 2002
	public abstract class LengthPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x06005AEC RID: 23276 RVA: 0x0018775A File Offset: 0x0018595A
		internal LengthPropertyConfiguration(LengthPropertyConfiguration configuration) : base(configuration)
		{
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x00187764 File Offset: 0x00185964
		public LengthPropertyConfiguration IsMaxLength()
		{
			this.Configuration.IsMaxLength = new bool?(true);
			this.Configuration.MaxLength = null;
			return this;
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x00187798 File Offset: 0x00185998
		public LengthPropertyConfiguration HasMaxLength(int? value)
		{
			this.Configuration.MaxLength = value;
			this.Configuration.IsMaxLength = null;
			this.Configuration.IsFixedLength = new bool?(this.Configuration.IsFixedLength ?? false);
			return this;
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x001877F5 File Offset: 0x001859F5
		public LengthPropertyConfiguration IsFixedLength()
		{
			this.Configuration.IsFixedLength = new bool?(true);
			return this;
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x00187809 File Offset: 0x00185A09
		public LengthPropertyConfiguration IsVariableLength()
		{
			this.Configuration.IsFixedLength = new bool?(false);
			return this;
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005AF1 RID: 23281 RVA: 0x0018781D File Offset: 0x00185A1D
		internal new LengthPropertyConfiguration Configuration
		{
			get
			{
				return (LengthPropertyConfiguration)base.Configuration;
			}
		}
	}
}

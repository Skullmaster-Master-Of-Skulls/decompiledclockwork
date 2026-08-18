using System;
using System.ComponentModel;

namespace System.Linq
{
	// Token: 0x02000172 RID: 370
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x00030AF5 File Offset: 0x0002ECF5
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00030AFE File Offset: 0x0002ECFE
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x040007B2 RID: 1970
		private bool replaced;
	}
}

using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00003DBE File Offset: 0x00001FBE
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003DC7 File Offset: 0x00001FC7
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

		// Token: 0x040000E4 RID: 228
		private bool replaced;
	}
}

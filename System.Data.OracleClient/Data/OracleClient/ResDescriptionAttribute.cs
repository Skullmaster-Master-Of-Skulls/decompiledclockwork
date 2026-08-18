using System;
using System.ComponentModel;

namespace System.Data.OracleClient
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00054A54 File Offset: 0x00053E54
		public ResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00054A74 File Offset: 0x00053E74
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = Res.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool replaced;
	}
}

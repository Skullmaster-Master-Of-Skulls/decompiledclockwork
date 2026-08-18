using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000060E1 File Offset: 0x000042E1
		public EntityResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000060EA File Offset: 0x000042EA
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = EntityRes.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x040000AA RID: 170
		private bool replaced;
	}
}

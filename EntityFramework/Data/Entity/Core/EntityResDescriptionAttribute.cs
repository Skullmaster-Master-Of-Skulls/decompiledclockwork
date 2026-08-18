using System;
using System.ComponentModel;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core
{
	// Token: 0x0200039E RID: 926
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class EntityResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x0009D9BB File Offset: 0x0009BBBB
		public override string Description
		{
			get
			{
				if (!this._replaced)
				{
					this._replaced = true;
					base.DescriptionValue = EntityRes.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0009D9E3 File Offset: 0x0009BBE3
		public EntityResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x04000BD3 RID: 3027
		private bool _replaced;
	}
}

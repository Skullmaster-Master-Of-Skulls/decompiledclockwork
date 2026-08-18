using System;
using System.ComponentModel;

namespace System.Xml.Linq
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000B705 File Offset: 0x00009905
		public ResDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B70E File Offset: 0x0000990E
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

		// Token: 0x040000C4 RID: 196
		private bool replaced;
	}
}

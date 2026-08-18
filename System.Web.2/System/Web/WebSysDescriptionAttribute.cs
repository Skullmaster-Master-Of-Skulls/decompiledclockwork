using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200010E RID: 270
	[AttributeUsage(AttributeTargets.All)]
	internal class WebSysDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0002E448 File Offset: 0x0002C648
		internal WebSysDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0002E451 File Offset: 0x0002C651
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

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0002E479 File Offset: 0x0002C679
		public override object TypeId
		{
			get
			{
				return typeof(DescriptionAttribute);
			}
		}

		// Token: 0x0400064A RID: 1610
		private bool replaced;
	}
}

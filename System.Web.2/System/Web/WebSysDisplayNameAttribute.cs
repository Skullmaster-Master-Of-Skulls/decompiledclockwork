using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200010F RID: 271
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event)]
	internal sealed class WebSysDisplayNameAttribute : DisplayNameAttribute
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x0002E485 File Offset: 0x0002C685
		internal WebSysDisplayNameAttribute(string DisplayName) : base(DisplayName)
		{
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0002E48E File Offset: 0x0002C68E
		public override string DisplayName
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DisplayNameValue = SR.GetString(base.DisplayName);
				}
				return base.DisplayName;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0002E4B6 File Offset: 0x0002C6B6
		public override object TypeId
		{
			get
			{
				return typeof(DisplayNameAttribute);
			}
		}

		// Token: 0x0400064B RID: 1611
		private bool replaced;
	}
}

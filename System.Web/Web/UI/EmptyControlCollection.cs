using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003E8 RID: 1000
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class EmptyControlCollection : ControlCollection
	{
		// Token: 0x0600306D RID: 12397 RVA: 0x000D58B8 File Offset: 0x000D48B8
		public EmptyControlCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000D58C4 File Offset: 0x000D48C4
		private void ThrowNotSupportedException()
		{
			throw new HttpException(SR.GetString("Control_does_not_allow_children", new object[]
			{
				base.Owner.GetType().ToString()
			}));
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000D58FB File Offset: 0x000D48FB
		public override void Add(Control child)
		{
			this.ThrowNotSupportedException();
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000D5903 File Offset: 0x000D4903
		public override void AddAt(int index, Control child)
		{
			this.ThrowNotSupportedException();
		}
	}
}

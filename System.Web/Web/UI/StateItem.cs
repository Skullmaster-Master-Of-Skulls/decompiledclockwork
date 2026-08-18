using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x02000467 RID: 1127
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateItem
	{
		// Token: 0x06003558 RID: 13656 RVA: 0x000E6990 File Offset: 0x000E5990
		internal StateItem(object initialValue)
		{
			this.value = initialValue;
			this.isDirty = false;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000E69A6 File Offset: 0x000E59A6
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x000E69AE File Offset: 0x000E59AE
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				this.isDirty = value;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x000E69B7 File Offset: 0x000E59B7
		// (set) Token: 0x0600355C RID: 13660 RVA: 0x000E69BF File Offset: 0x000E59BF
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400252D RID: 9517
		private object value;

		// Token: 0x0400252E RID: 9518
		private bool isDirty;
	}
}

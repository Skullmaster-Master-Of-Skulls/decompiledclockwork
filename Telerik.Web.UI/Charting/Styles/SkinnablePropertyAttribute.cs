using System;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017BC RID: 6076
	public sealed class SkinnablePropertyAttribute : Attribute
	{
		// Token: 0x0600EC72 RID: 60530 RVA: 0x0035E7DF File Offset: 0x0035C9DF
		public SkinnablePropertyAttribute()
		{
			this.isSkinnable = true;
		}

		// Token: 0x1700478F RID: 18319
		// (get) Token: 0x0600EC73 RID: 60531 RVA: 0x0035E7EE File Offset: 0x0035C9EE
		public bool IsSkinnable
		{
			get
			{
				return this.isSkinnable;
			}
		}

		// Token: 0x0400442F RID: 17455
		private bool isSkinnable;
	}
}

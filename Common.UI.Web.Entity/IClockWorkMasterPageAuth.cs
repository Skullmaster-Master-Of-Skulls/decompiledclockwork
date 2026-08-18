using System;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x02000009 RID: 9
	public interface IClockWorkMasterPageAuth
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30
		bool IsExemptFromAuthentication { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001F RID: 31
		// (remove) Token: 0x06000020 RID: 32
		event EventHandler<IsExemptFromAuthenticationEventArgs> OnGetIsExemptFromAuthenticationEventArgs;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33
		bool IsExemptFromRequiredSessionFormCheck { get; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000022 RID: 34
		// (remove) Token: 0x06000023 RID: 35
		event EventHandler<IsExemptFromRequiredSessionFormCheckEventArgs> OnGetIsExemptFromRequiredSessionFormCheck;
	}
}

using System;

namespace System.Web
{
	// Token: 0x02000021 RID: 33
	public interface ISubscriptionToken
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000ED RID: 237
		bool IsActive { get; }

		// Token: 0x060000EE RID: 238
		void Unsubscribe();
	}
}

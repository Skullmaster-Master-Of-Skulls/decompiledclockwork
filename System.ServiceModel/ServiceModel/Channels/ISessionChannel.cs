using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000715 RID: 1813
	[__DynamicallyInvokable]
	public interface ISessionChannel<TSession> where TSession : ISession
	{
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060044E4 RID: 17636
		[__DynamicallyInvokable]
		TSession Session { [__DynamicallyInvokable] get; }
	}
}

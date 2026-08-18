using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000AE RID: 174
	public interface IConfigSystem
	{
		// Token: 0x060006EC RID: 1772
		void Init(Type typeConfigHost, params object[] hostInitParams);

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006ED RID: 1773
		IInternalConfigHost Host { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006EE RID: 1774
		IInternalConfigRoot Root { get; }
	}
}

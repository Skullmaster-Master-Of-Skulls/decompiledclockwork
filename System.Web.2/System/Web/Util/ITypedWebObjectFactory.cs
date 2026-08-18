using System;

namespace System.Web.Util
{
	// Token: 0x02000208 RID: 520
	internal interface ITypedWebObjectFactory : IWebObjectFactory
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001987 RID: 6535
		Type InstantiatedType { get; }
	}
}

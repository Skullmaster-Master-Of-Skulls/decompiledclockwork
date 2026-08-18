using System;

namespace System.Web.Http.Services
{
	// Token: 0x020000A0 RID: 160
	public interface IDecorator<out T>
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003D2 RID: 978
		T Inner { get; }
	}
}

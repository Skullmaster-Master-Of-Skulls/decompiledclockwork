using System;

namespace System.Web.Hosting
{
	// Token: 0x02000795 RID: 1941
	public interface ISuspendibleRegisteredObject : IRegisteredObject
	{
		// Token: 0x06005C9B RID: 23707
		Action Suspend();
	}
}

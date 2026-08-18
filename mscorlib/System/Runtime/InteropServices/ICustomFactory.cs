using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000523 RID: 1315
	[ComVisible(true)]
	public interface ICustomFactory
	{
		// Token: 0x060032E6 RID: 13030
		MarshalByRefObject CreateInstance(Type serverType);
	}
}

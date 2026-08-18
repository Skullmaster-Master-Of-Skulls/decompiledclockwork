using System;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x0200079F RID: 1951
	internal sealed class __TransparentProxy
	{
		// Token: 0x06004587 RID: 17799 RVA: 0x000EC8B0 File Offset: 0x000EB8B0
		private __TransparentProxy()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Constructor"));
		}

		// Token: 0x04002291 RID: 8849
		private RealProxy _rp;

		// Token: 0x04002292 RID: 8850
		private object _stubData;

		// Token: 0x04002293 RID: 8851
		private IntPtr _pMT;

		// Token: 0x04002294 RID: 8852
		private IntPtr _pInterfaceMT;

		// Token: 0x04002295 RID: 8853
		private IntPtr _stub;
	}
}

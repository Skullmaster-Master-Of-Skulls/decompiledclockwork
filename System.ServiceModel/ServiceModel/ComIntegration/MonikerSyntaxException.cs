using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200023E RID: 574
	internal class MonikerSyntaxException : COMException
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x0003E889 File Offset: 0x0003CA89
		internal MonikerSyntaxException(string message) : base(message, HR.MK_E_SYNTAX)
		{
		}
	}
}

using System;
using System.ComponentModel;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000451 RID: 1105
	internal class WbemException : Win32Exception
	{
		// Token: 0x06002AF3 RID: 10995 RVA: 0x000A87E5 File Offset: 0x000A69E5
		internal WbemException(WbemNative.WbemStatus hr) : base((int)hr)
		{
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000A87EE File Offset: 0x000A69EE
		internal WbemException(int hr) : base(hr)
		{
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000A87F7 File Offset: 0x000A69F7
		internal WbemException(int hr, string message) : base(hr, message)
		{
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000A8804 File Offset: 0x000A6A04
		internal static void Throw(WbemNative.WbemStatus hr)
		{
			if (hr <= WbemNative.WbemStatus.WBEM_E_INVALID_PARAMETER)
			{
				if (hr == WbemNative.WbemStatus.WBEM_E_NOT_FOUND)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemInstanceNotFoundException());
				}
				if (hr == WbemNative.WbemStatus.WBEM_E_INVALID_PARAMETER)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemInvalidParameterException());
				}
			}
			else
			{
				if (hr == WbemNative.WbemStatus.WBEM_E_NOT_SUPPORTED)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemNotSupportedException());
				}
				if (hr == WbemNative.WbemStatus.WBEM_E_INVALID_METHOD)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemInvalidMethodException());
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemException(hr));
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000A888D File Offset: 0x000A6A8D
		internal static void ThrowIfFail(int hr)
		{
			if (hr < 0)
			{
				WbemException.Throw((WbemNative.WbemStatus)hr);
			}
		}
	}
}

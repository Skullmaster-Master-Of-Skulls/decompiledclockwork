using System;

namespace System.DirectoryServices.Protocols
{
	// Token: 0x0200007A RID: 122
	internal class ErrorChecking
	{
		// Token: 0x06000295 RID: 661 RVA: 0x0000D6B8 File Offset: 0x0000C6B8
		public static void CheckAndSetLdapError(int error)
		{
			if (error == 0)
			{
				return;
			}
			if (Utility.IsResultCode((ResultCode)error))
			{
				string message = OperationErrorMappings.MapResultCode(error);
				throw new DirectoryOperationException(null, message);
			}
			if (Utility.IsLdapError((LdapError)error))
			{
				string message = LdapErrorMappings.MapResultCode(error);
				throw new LdapException(error, message);
			}
			throw new LdapException(error);
		}
	}
}

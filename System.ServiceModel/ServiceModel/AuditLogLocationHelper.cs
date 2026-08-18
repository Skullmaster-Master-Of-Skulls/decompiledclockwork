using System;
using System.ComponentModel;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000CA RID: 202
	internal static class AuditLogLocationHelper
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00014E3C File Offset: 0x0001303C
		public static bool IsDefined(AuditLogLocation auditLogLocation)
		{
			if (auditLogLocation == AuditLogLocation.Security && !SecurityAuditHelper.IsSecurityAuditSupported)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PlatformNotSupportedException(SR.GetString("SecurityAuditPlatformNotSupported")));
			}
			return auditLogLocation == AuditLogLocation.Default || auditLogLocation == AuditLogLocation.Application || auditLogLocation == AuditLogLocation.Security;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00014E70 File Offset: 0x00013070
		public static void Validate(AuditLogLocation value)
		{
			if (!AuditLogLocationHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(AuditLogLocation)));
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace System.ServiceModel
{
	// Token: 0x020000C8 RID: 200
	internal static class AuditLevelHelper
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00014DFF File Offset: 0x00012FFF
		public static bool IsDefined(AuditLevel auditLevel)
		{
			return auditLevel == AuditLevel.None || auditLevel == AuditLevel.Success || auditLevel == AuditLevel.Failure || auditLevel == AuditLevel.SuccessOrFailure;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00014E12 File Offset: 0x00013012
		public static void Validate(AuditLevel value)
		{
			if (!AuditLevelHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(AuditLevel)));
			}
		}
	}
}

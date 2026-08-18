using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E7 RID: 487
	internal static class DigestTraceRecordHelper
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00046408 File Offset: 0x00044608
		internal static bool ShouldTraceDigest
		{
			get
			{
				if (!DigestTraceRecordHelper._initialized)
				{
					DigestTraceRecordHelper.InitializeShouldTraceDigest();
				}
				return DigestTraceRecordHelper._shouldTraceDigest;
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004641B File Offset: 0x0004461B
		private static void InitializeShouldTraceDigest()
		{
			if (DiagnosticUtility.DiagnosticTrace != null && DiagnosticUtility.DiagnosticTrace.TraceSource != null && DiagnosticUtility.DiagnosticTrace.ShouldLogPii && DiagnosticUtility.ShouldTraceVerbose)
			{
				DigestTraceRecordHelper._shouldTraceDigest = true;
			}
			DigestTraceRecordHelper._initialized = true;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004644F File Offset: 0x0004464F
		internal static void TraceDigest(MemoryStream logStream, HashAlgorithm hash)
		{
			if (DigestTraceRecordHelper.ShouldTraceDigest)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 786432, SR.GetString("TraceCodeIdentityModel"), new DigestTraceRecord("DigestTrace", logStream, hash), null, null);
			}
		}

		// Token: 0x04000E3D RID: 3645
		private const string DigestTrace = "DigestTrace";

		// Token: 0x04000E3E RID: 3646
		private static bool _shouldTraceDigest;

		// Token: 0x04000E3F RID: 3647
		private static bool _initialized;
	}
}

using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Runtime
{
	// Token: 0x02000610 RID: 1552
	public static class GCSettings
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600381D RID: 14365 RVA: 0x000BC294 File Offset: 0x000BB294
		// (set) Token: 0x0600381E RID: 14366 RVA: 0x000BC29B File Offset: 0x000BB29B
		public static GCLatencyMode LatencyMode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (GCLatencyMode)GC.nativeGetGCLatencyMode();
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLatencyMode.Batch || value > GCLatencyMode.LowLatency)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				GC.nativeSetGCLatencyMode((int)value);
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000BC2BB File Offset: 0x000BB2BB
		public static bool IsServerGC
		{
			get
			{
				return GC.nativeIsServerGC();
			}
		}
	}
}

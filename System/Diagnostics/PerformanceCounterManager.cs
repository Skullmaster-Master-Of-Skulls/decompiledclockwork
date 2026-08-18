using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200076E RID: 1902
	[ComVisible(true)]
	[Guid("82840BE1-D273-11D2-B94A-00600893B17A")]
	[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class PerformanceCounterManager : ICollectData
	{
		// Token: 0x06003A9E RID: 15006 RVA: 0x000F97BE File Offset: 0x000F87BE
		[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public PerformanceCounterManager()
		{
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000F97C6 File Offset: 0x000F87C6
		[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		void ICollectData.CollectData(int callIdx, IntPtr valueNamePtr, IntPtr dataPtr, int totalBytes, out IntPtr res)
		{
			res = (IntPtr)(-1);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000F97D5 File Offset: 0x000F87D5
		[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		void ICollectData.CloseData()
		{
		}
	}
}

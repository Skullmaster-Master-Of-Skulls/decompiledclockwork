using System;

namespace System.Web.Util
{
	// Token: 0x02000224 RID: 548
	internal static class SystemInfo
	{
		// Token: 0x06001A43 RID: 6723 RVA: 0x00052544 File Offset: 0x00050744
		internal static int GetNumProcessCPUs()
		{
			if (SystemInfo._trueNumberOfProcessors == 0)
			{
				UnsafeNativeMethods.SYSTEM_INFO system_INFO;
				UnsafeNativeMethods.GetSystemInfo(out system_INFO);
				if (system_INFO.dwNumberOfProcessors == 1U)
				{
					SystemInfo._trueNumberOfProcessors = 1;
				}
				else
				{
					IntPtr invalid_HANDLE_VALUE = UnsafeNativeMethods.INVALID_HANDLE_VALUE;
					IntPtr value;
					IntPtr intPtr;
					if (UnsafeNativeMethods.GetProcessAffinityMask(invalid_HANDLE_VALUE, out value, out intPtr) == 0)
					{
						SystemInfo._trueNumberOfProcessors = 1;
					}
					else
					{
						int num = 0;
						if (IntPtr.Size == 4)
						{
							for (uint num2 = (uint)((int)value); num2 != 0U; num2 >>= 1)
							{
								if ((num2 & 1U) == 1U)
								{
									num++;
								}
							}
						}
						else
						{
							for (ulong num3 = (ulong)((long)value); num3 != 0UL; num3 >>= 1)
							{
								if ((num3 & 1UL) == 1UL)
								{
									num++;
								}
							}
						}
						SystemInfo._trueNumberOfProcessors = num;
					}
				}
			}
			return SystemInfo._trueNumberOfProcessors;
		}

		// Token: 0x0400181A RID: 6170
		private static int _trueNumberOfProcessors;
	}
}

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x0200015E RID: 350
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x060015D4 RID: 5588
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemAlloc(IntPtr cb);

		// Token: 0x060015D5 RID: 5589
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ole32.dll")]
		internal static extern void CoTaskMemFree(IntPtr handle);

		// Token: 0x060015D6 RID: 5590
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetUserDefaultLCID();

		// Token: 0x060015D7 RID: 5591
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern void ZeroMemory(IntPtr dest, IntPtr length);

		// Token: 0x060015D8 RID: 5592 RVA: 0x002466A8 File Offset: 0x00245AA8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal unsafe static IntPtr InterlockedExchangePointer(IntPtr lpAddress, IntPtr lpValue)
		{
			IntPtr intPtr = *(IntPtr*)lpAddress.ToPointer();
			IntPtr intPtr2;
			do
			{
				intPtr2 = intPtr;
				intPtr = Interlocked.CompareExchange(ref *(IntPtr*)lpAddress.ToPointer(), lpValue, intPtr2);
			}
			while (intPtr != intPtr2);
			return intPtr;
		}

		// Token: 0x060015D9 RID: 5593
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameExW", SetLastError = true)]
		internal static extern int GetComputerNameEx(int nameType, StringBuilder nameBuffer, ref int bufferSize);

		// Token: 0x060015DA RID: 5594
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetCurrentProcessId();

		// Token: 0x060015DB RID: 5595
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPTStr)] [In] string moduleName);

		// Token: 0x060015DC RID: 5596
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress(IntPtr HModule, [MarshalAs(UnmanagedType.LPStr)] [In] string funcName);

		// Token: 0x060015DD RID: 5597
		[DllImport("kernel32.dll")]
		internal static extern void GetSystemTimeAsFileTime(out long lpSystemTimeAsFileTime);

		// Token: 0x060015DE RID: 5598
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalAlloc(int flags, IntPtr countOfBytes);

		// Token: 0x060015DF RID: 5599
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x060015E0 RID: 5600
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SysAllocStringLen(string src, int len);

		// Token: 0x060015E1 RID: 5601
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern void SysFreeString(IntPtr bstr);

		// Token: 0x060015E2 RID: 5602
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		private static extern void SetErrorInfo(int dwReserved, IntPtr pIErrorInfo);

		// Token: 0x060015E3 RID: 5603
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int ReleaseSemaphore(IntPtr handle, int releaseCount, IntPtr previousCount);

		// Token: 0x060015E4 RID: 5604
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int WaitForMultipleObjectsEx(uint nCount, IntPtr lpHandles, bool bWaitAll, uint dwMilliseconds, bool bAlertable);

		// Token: 0x060015E5 RID: 5605
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll")]
		internal static extern int WaitForSingleObjectEx(IntPtr lpHandles, uint dwMilliseconds, bool bAlertable);

		// Token: 0x060015E6 RID: 5606
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ole32.dll", PreserveSig = false)]
		internal static extern void PropVariantClear(IntPtr pObject);

		// Token: 0x060015E7 RID: 5607
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll", PreserveSig = false)]
		internal static extern void VariantClear(IntPtr pObject);

		// Token: 0x0200015F RID: 351
		internal sealed class Wrapper
		{
			// Token: 0x060015E8 RID: 5608 RVA: 0x002466E8 File Offset: 0x00245AE8
			private Wrapper()
			{
			}

			// Token: 0x060015E9 RID: 5609 RVA: 0x00246708 File Offset: 0x00245B08
			internal static void ClearErrorInfo()
			{
				SafeNativeMethods.SetErrorInfo(0, ADP.PtrZero);
			}
		}
	}
}

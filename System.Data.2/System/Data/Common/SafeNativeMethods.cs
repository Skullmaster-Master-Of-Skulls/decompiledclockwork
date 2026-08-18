using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x0200030E RID: 782
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x06003166 RID: 12646
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemAlloc(IntPtr cb);

		// Token: 0x06003167 RID: 12647
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ole32.dll")]
		internal static extern void CoTaskMemFree(IntPtr handle);

		// Token: 0x06003168 RID: 12648
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetUserDefaultLCID();

		// Token: 0x06003169 RID: 12649
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern void ZeroMemory(IntPtr dest, IntPtr length);

		// Token: 0x0600316A RID: 12650 RVA: 0x00132F9C File Offset: 0x0013239C
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

		// Token: 0x0600316B RID: 12651
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameExW", SetLastError = true)]
		internal static extern int GetComputerNameEx(int nameType, StringBuilder nameBuffer, ref int bufferSize);

		// Token: 0x0600316C RID: 12652
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetCurrentProcessId();

		// Token: 0x0600316D RID: 12653
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPTStr)] [In] string moduleName);

		// Token: 0x0600316E RID: 12654
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress(IntPtr HModule, [MarshalAs(UnmanagedType.LPStr)] [In] string funcName);

		// Token: 0x0600316F RID: 12655
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalAlloc(int flags, IntPtr countOfBytes);

		// Token: 0x06003170 RID: 12656
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06003171 RID: 12657
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SysAllocStringLen(string src, int len);

		// Token: 0x06003172 RID: 12658
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll")]
		internal static extern void SysFreeString(IntPtr bstr);

		// Token: 0x06003173 RID: 12659
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		private static extern void SetErrorInfo(int dwReserved, IntPtr pIErrorInfo);

		// Token: 0x06003174 RID: 12660
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int ReleaseSemaphore(IntPtr handle, int releaseCount, IntPtr previousCount);

		// Token: 0x06003175 RID: 12661
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int WaitForMultipleObjectsEx(uint nCount, IntPtr lpHandles, bool bWaitAll, uint dwMilliseconds, bool bAlertable);

		// Token: 0x06003176 RID: 12662
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll")]
		internal static extern int WaitForSingleObjectEx(IntPtr lpHandles, uint dwMilliseconds, bool bAlertable);

		// Token: 0x06003177 RID: 12663
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("ole32.dll", PreserveSig = false)]
		internal static extern void PropVariantClear(IntPtr pObject);

		// Token: 0x06003178 RID: 12664
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("oleaut32.dll", PreserveSig = false)]
		internal static extern void VariantClear(IntPtr pObject);

		// Token: 0x02000442 RID: 1090
		internal sealed class Wrapper
		{
			// Token: 0x06003658 RID: 13912 RVA: 0x00149BF0 File Offset: 0x00148FF0
			private Wrapper()
			{
			}

			// Token: 0x06003659 RID: 13913 RVA: 0x00149C04 File Offset: 0x00149004
			internal static void ClearErrorInfo()
			{
				SafeNativeMethods.SetErrorInfo(0, ADP.PtrZero);
			}
		}
	}
}

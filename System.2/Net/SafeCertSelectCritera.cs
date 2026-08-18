using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F7 RID: 503
	internal sealed class SafeCertSelectCritera : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x000646D0 File Offset: 0x000628D0
		internal int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000646D4 File Offset: 0x000628D4
		private IntPtr AllocBuffer(int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			this.unmanagedMemoryList.Add(intPtr);
			return intPtr;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000646F8 File Offset: 0x000628F8
		private IntPtr AllocString(string str)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAnsi(str);
			this.unmanagedMemoryList.Add(intPtr);
			return intPtr;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006471C File Offset: 0x0006291C
		internal SafeCertSelectCritera() : base(true)
		{
			UnsafeNclNativeMethods.NativePKI.CERT_SELECT_CRITERIA cert_SELECT_CRITERIA = default(UnsafeNclNativeMethods.NativePKI.CERT_SELECT_CRITERIA);
			this.unmanagedMemoryList = new List<IntPtr>();
			IntPtr intPtr = this.AllocBuffer(2 * Marshal.SizeOf(cert_SELECT_CRITERIA));
			base.SetHandle(intPtr);
			cert_SELECT_CRITERIA.dwType = 1U;
			cert_SELECT_CRITERIA.cPara = 1U;
			IntPtr intPtr2 = this.AllocString("1.3.6.1.5.5.7.3.2");
			IntPtr intPtr3 = this.AllocBuffer(Marshal.SizeOf(intPtr2));
			Marshal.WriteIntPtr(intPtr3, intPtr2);
			cert_SELECT_CRITERIA.ppPara = intPtr3;
			Marshal.StructureToPtr(cert_SELECT_CRITERIA, intPtr, false);
			cert_SELECT_CRITERIA = default(UnsafeNclNativeMethods.NativePKI.CERT_SELECT_CRITERIA);
			cert_SELECT_CRITERIA.dwType = 2U;
			cert_SELECT_CRITERIA.cPara = 1U;
			UnsafeNclNativeMethods.NativePKI.CERT_EXTENSION cert_EXTENSION = default(UnsafeNclNativeMethods.NativePKI.CERT_EXTENSION);
			cert_EXTENSION.pszObjId = IntPtr.Zero;
			cert_EXTENSION.fCritical = 0U;
			cert_EXTENSION.Value.cbData = 1U;
			IntPtr intPtr4 = this.AllocBuffer(Marshal.SizeOf(128));
			Marshal.WriteByte(intPtr4, 128);
			cert_EXTENSION.Value.pbData = intPtr4;
			IntPtr intPtr5 = this.AllocBuffer(Marshal.SizeOf(cert_EXTENSION));
			Marshal.StructureToPtr(cert_EXTENSION, intPtr5, false);
			intPtr3 = this.AllocBuffer(Marshal.SizeOf(intPtr5));
			Marshal.WriteIntPtr(intPtr3, intPtr5);
			cert_SELECT_CRITERIA.ppPara = intPtr3;
			Marshal.StructureToPtr(cert_SELECT_CRITERIA, intPtr + Marshal.SizeOf(cert_SELECT_CRITERIA), false);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00064880 File Offset: 0x00062A80
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000648AC File Offset: 0x00062AAC
		protected override bool ReleaseHandle()
		{
			try
			{
				foreach (IntPtr hglobal in this.unmanagedMemoryList)
				{
					Marshal.FreeHGlobal(hglobal);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x04001549 RID: 5449
		private const string szOID_PKIX_KP_CLIENT_AUTH = "1.3.6.1.5.5.7.3.2";

		// Token: 0x0400154A RID: 5450
		private const int CERT_SELECT_BY_ENHKEY_USAGE = 1;

		// Token: 0x0400154B RID: 5451
		private const int CERT_SELECT_BY_KEY_USAGE = 2;

		// Token: 0x0400154C RID: 5452
		private const byte CERT_DIGITAL_SIGNATURE_KEY_USAGE = 128;

		// Token: 0x0400154D RID: 5453
		private const int criteriaCount = 2;

		// Token: 0x0400154E RID: 5454
		private List<IntPtr> unmanagedMemoryList;
	}
}

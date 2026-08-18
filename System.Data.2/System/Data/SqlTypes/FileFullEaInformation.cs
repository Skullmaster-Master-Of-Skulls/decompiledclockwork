using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200015F RID: 351
	internal class FileFullEaInformation : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x000A2F94 File Offset: 0x000A2394
		public FileFullEaInformation(byte[] transactionContext) : base(true)
		{
			this.m_cbBuffer = 0;
			this.InitializeEaBuffer(transactionContext);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x000A2FC4 File Offset: 0x000A23C4
		protected override bool ReleaseHandle()
		{
			this.m_cbBuffer = 0;
			if (this.handle == IntPtr.Zero)
			{
				return true;
			}
			Marshal.FreeHGlobal(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000A3004 File Offset: 0x000A2404
		public int Length
		{
			get
			{
				return this.m_cbBuffer;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000A3018 File Offset: 0x000A2418
		private void InitializeEaBuffer(byte[] transactionContext)
		{
			if (transactionContext.Length >= 65535)
			{
				throw ADP.ArgumentOutOfRange("transactionContext");
			}
			UnsafeNativeMethods.FILE_FULL_EA_INFORMATION file_FULL_EA_INFORMATION;
			file_FULL_EA_INFORMATION.nextEntryOffset = 0U;
			file_FULL_EA_INFORMATION.flags = 0;
			file_FULL_EA_INFORMATION.EaName = 0;
			file_FULL_EA_INFORMATION.EaNameLength = (byte)this.EA_NAME_STRING.Length;
			file_FULL_EA_INFORMATION.EaValueLength = (ushort)transactionContext.Length;
			this.m_cbBuffer = Marshal.SizeOf(file_FULL_EA_INFORMATION) + (int)file_FULL_EA_INFORMATION.EaNameLength + (int)file_FULL_EA_INFORMATION.EaValueLength;
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				intPtr = Marshal.AllocHGlobal(this.m_cbBuffer);
				if (intPtr != IntPtr.Zero)
				{
					base.SetHandle(intPtr);
				}
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.StructureToPtr(file_FULL_EA_INFORMATION, ptr, false);
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				byte[] bytes = asciiencoding.GetBytes(this.EA_NAME_STRING);
				int num = Marshal.OffsetOf(typeof(UnsafeNativeMethods.FILE_FULL_EA_INFORMATION), "EaName").ToInt32();
				int num2 = 0;
				while (num < this.m_cbBuffer && num2 < (int)file_FULL_EA_INFORMATION.EaNameLength)
				{
					Marshal.WriteByte(ptr, num, bytes[num2]);
					num2++;
					num++;
				}
				Marshal.WriteByte(ptr, num, 0);
				num++;
				int num3 = 0;
				while (num < this.m_cbBuffer && num3 < (int)file_FULL_EA_INFORMATION.EaValueLength)
				{
					Marshal.WriteByte(ptr, num, transactionContext[num3]);
					num3++;
					num++;
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x04000DE2 RID: 3554
		private string EA_NAME_STRING = "Filestream_Transaction_Tag";

		// Token: 0x04000DE3 RID: 3555
		private int m_cbBuffer;
	}
}

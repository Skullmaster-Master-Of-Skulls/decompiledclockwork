using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200034E RID: 846
	internal class FileFullEaInformation : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002D47 RID: 11591 RVA: 0x002CD498 File Offset: 0x002CC898
		public FileFullEaInformation(byte[] transactionContext) : base(true)
		{
			this.m_cbBuffer = 0;
			this.InitializeEaBuffer(transactionContext);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x002CD4C8 File Offset: 0x002CC8C8
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

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x002CD508 File Offset: 0x002CC908
		public int Length
		{
			get
			{
				return this.m_cbBuffer;
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x002CD528 File Offset: 0x002CC928
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

		// Token: 0x04001CF9 RID: 7417
		private string EA_NAME_STRING = "Filestream_Transaction_Tag";

		// Token: 0x04001CFA RID: 7418
		private int m_cbBuffer;
	}
}

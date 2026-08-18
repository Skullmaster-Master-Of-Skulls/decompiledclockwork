using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200032F RID: 815
	internal sealed class SNILoadHandle : SafeHandle
	{
		// Token: 0x06002A7A RID: 10874 RVA: 0x002BEC58 File Offset: 0x002BE058
		private SNILoadHandle() : base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._sniStatus = SNINativeMethodWrapper.SNIInitialize();
				uint num = 0U;
				SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE, ref num);
				this._encryptionOption = ((num == 0U) ? EncryptionOptions.NOT_SUP : EncryptionOptions.OFF);
				this.handle = (IntPtr)1;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x002BECF8 File Offset: 0x002BE0F8
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x002BED18 File Offset: 0x002BE118
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				if (this._sniStatus == 0U)
				{
					LocalDBAPI.ReleaseDLLHandles();
					SNINativeMethodWrapper.SNITerminate();
				}
				this.handle = IntPtr.Zero;
			}
			return true;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x002BED58 File Offset: 0x002BE158
		public uint SNIStatus
		{
			get
			{
				return this._sniStatus;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x002BED78 File Offset: 0x002BE178
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x002BED98 File Offset: 0x002BE198
		private static void ReadDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.ReadAsyncCallback(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x002BEDD8 File Offset: 0x002BE1D8
		private static void WriteDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.WriteAsyncCallback(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x04001C01 RID: 7169
		internal static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x04001C02 RID: 7170
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate ReadAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.ReadDispatcher);

		// Token: 0x04001C03 RID: 7171
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate WriteAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.WriteDispatcher);

		// Token: 0x04001C04 RID: 7172
		private readonly uint _sniStatus = uint.MaxValue;

		// Token: 0x04001C05 RID: 7173
		private readonly EncryptionOptions _encryptionOption;
	}
}

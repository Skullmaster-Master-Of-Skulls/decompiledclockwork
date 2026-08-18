using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000228 RID: 552
	internal sealed class SNILoadHandle : SafeHandle
	{
		// Token: 0x06002238 RID: 8760 RVA: 0x000ED208 File Offset: 0x000EC608
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
				if (this._sniStatus == 0U)
				{
					SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE, ref num);
				}
				this._encryptionOption = ((num == 0U) ? EncryptionOptions.NOT_SUP : EncryptionOptions.OFF);
				this.handle = (IntPtr)1;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000ED2A8 File Offset: 0x000EC6A8
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000ED2C8 File Offset: 0x000EC6C8
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

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000ED308 File Offset: 0x000EC708
		public uint SNIStatus
		{
			get
			{
				return this._sniStatus;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x000ED31C File Offset: 0x000EC71C
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000ED330 File Offset: 0x000EC730
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

		// Token: 0x0600223E RID: 8766 RVA: 0x000ED370 File Offset: 0x000EC770
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

		// Token: 0x040014AE RID: 5294
		internal static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x040014AF RID: 5295
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate ReadAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.ReadDispatcher);

		// Token: 0x040014B0 RID: 5296
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate WriteAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.WriteDispatcher);

		// Token: 0x040014B1 RID: 5297
		private readonly uint _sniStatus = uint.MaxValue;

		// Token: 0x040014B2 RID: 5298
		private readonly EncryptionOptions _encryptionOption;
	}
}

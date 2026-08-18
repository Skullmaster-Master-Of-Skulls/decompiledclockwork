using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;

namespace System.Data.OracleClient
{
	// Token: 0x02000036 RID: 54
	internal sealed class OciEnlistContext : SafeHandle
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0005B9A4 File Offset: 0x0005ADA4
		internal OciEnlistContext(byte[] userName, byte[] password, byte[] serverName, OciServiceContextHandle serviceContextHandle, OciErrorHandle errorHandle) : base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._serviceContextHandle = serviceContextHandle;
				int num = 0;
				try
				{
					num = TracedNativeMethods.OraMTSEnlCtxGet(userName, password, serverName, this._serviceContextHandle, errorHandle, out this.handle);
				}
				catch (DllNotFoundException inner)
				{
					throw ADP.DistribTxRequiresOracleServicesForMTS(inner);
				}
				if (num != 0)
				{
					OracleException.Check(errorHandle, num);
				}
				serviceContextHandle.AddRef();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0005BA44 File Offset: 0x0005AE44
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0005BA64 File Offset: 0x0005AE64
		internal void Join(OracleInternalConnection internalConnection, Transaction indigoTransaction)
		{
			IDtcTransaction oletxTransaction = ADP.GetOletxTransaction(indigoTransaction);
			int num = TracedNativeMethods.OraMTSJoinTxn(this, oletxTransaction);
			if (num != 0)
			{
				OracleException.Check(num, internalConnection);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0005BA94 File Offset: 0x0005AE94
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				TracedNativeMethods.OraMTSEnlCtxRel(handle);
			}
			if (this._serviceContextHandle != null)
			{
				this._serviceContextHandle.Release();
				this._serviceContextHandle = null;
			}
			return true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0005BAE4 File Offset: 0x0005AEE4
		internal static void SafeDispose(ref OciEnlistContext ociEnlistContext)
		{
			if (ociEnlistContext != null)
			{
				ociEnlistContext.Dispose();
			}
			ociEnlistContext = null;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0005BB04 File Offset: 0x0005AF04
		internal static IntPtr HandleValueToTrace(OciEnlistContext handle)
		{
			return handle.DangerousGetHandle();
		}

		// Token: 0x0400031B RID: 795
		private OciServiceContextHandle _serviceContextHandle;
	}
}

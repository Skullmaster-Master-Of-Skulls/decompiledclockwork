using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000229 RID: 553
	internal sealed class SNIHandle : SafeHandle
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x000ED3C8 File Offset: 0x000EC7C8
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, string serverName, byte[] spnBuffer, bool ignoreSniOpenTimeout, int timeout, out byte[] instanceName, bool flushCache, bool fSync, bool fParallel, TransparentNetworkResolutionState transparentNetworkResolutionState, int totalTimeout) : base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._fSync = fSync;
				instanceName = new byte[256];
				if (ignoreSniOpenTimeout)
				{
					timeout = -1;
				}
				this._status = SNINativeMethodWrapper.SNIOpenSyncEx(myInfo, serverName, ref this.handle, spnBuffer, instanceName, flushCache, fSync, timeout, fParallel, (int)transparentNetworkResolutionState, totalTimeout, ADP.IsAzureSqlServerEndpoint(serverName));
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000ED454 File Offset: 0x000EC854
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, SNIHandle parent) : base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._status = SNINativeMethodWrapper.SNIOpenMarsSession(myInfo, parent, ref this.handle, parent._fSync);
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000ED4B4 File Offset: 0x000EC8B4
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000ED4D4 File Offset: 0x000EC8D4
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			return !(IntPtr.Zero != handle) || SNINativeMethodWrapper.SNIClose(handle) == 0U;
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x000ED50C File Offset: 0x000EC90C
		internal uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x040014B3 RID: 5299
		private readonly uint _status = uint.MaxValue;

		// Token: 0x040014B4 RID: 5300
		private readonly bool _fSync;
	}
}

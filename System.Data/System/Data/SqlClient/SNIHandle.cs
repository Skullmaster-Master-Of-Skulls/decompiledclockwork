using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000330 RID: 816
	internal sealed class SNIHandle : SafeHandle
	{
		// Token: 0x06002A82 RID: 10882 RVA: 0x002BEE38 File Offset: 0x002BE238
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, string serverName, byte[] spnBuffer, bool ignoreSniOpenTimeout, int timeout, out byte[] instanceName, bool flushCache, bool fSync, bool fParallel) : base(IntPtr.Zero, true)
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
				this._status = SNINativeMethodWrapper.SNIOpenSyncEx(myInfo, serverName, ref this.handle, spnBuffer, instanceName, flushCache, fSync, timeout, fParallel);
			}
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x002BEEB8 File Offset: 0x002BE2B8
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, string serverName, SNIHandle parent) : base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._status = SNINativeMethodWrapper.SNIOpen(myInfo, serverName, parent, ref this.handle, parent._fSync);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x002BEF18 File Offset: 0x002BE318
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x002BEF38 File Offset: 0x002BE338
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			return !(IntPtr.Zero != handle) || SNINativeMethodWrapper.SNIClose(handle) == 0U;
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x002BEF78 File Offset: 0x002BE378
		internal uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x04001C06 RID: 7174
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04001C07 RID: 7175
		private readonly bool _fSync;
	}
}

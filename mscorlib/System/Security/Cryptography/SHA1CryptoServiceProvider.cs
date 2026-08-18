using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008AB RID: 2219
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		// Token: 0x0600509F RID: 20639 RVA: 0x0011FED8 File Offset: 0x0011EED8
		public SHA1CryptoServiceProvider()
		{
			SafeHashHandle invalidHandle = SafeHashHandle.InvalidHandle;
			Utils._CreateHash(Utils.StaticProvHandle, 32772, ref invalidHandle);
			this._safeHashHandle = invalidHandle;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x0011FF09 File Offset: 0x0011EF09
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0011FF34 File Offset: 0x0011EF34
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			SafeHashHandle invalidHandle = SafeHashHandle.InvalidHandle;
			Utils._CreateHash(Utils.StaticProvHandle, 32772, ref invalidHandle);
			this._safeHashHandle = invalidHandle;
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0011FF7F File Offset: 0x0011EF7F
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils._HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0011FF8F File Offset: 0x0011EF8F
		protected override byte[] HashFinal()
		{
			return Utils._EndHash(this._safeHashHandle);
		}

		// Token: 0x04002976 RID: 10614
		private SafeHashHandle _safeHashHandle;
	}
}

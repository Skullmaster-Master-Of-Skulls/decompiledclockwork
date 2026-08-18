using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000890 RID: 2192
	[ComVisible(true)]
	public sealed class MD5CryptoServiceProvider : MD5
	{
		// Token: 0x06004FC7 RID: 20423 RVA: 0x001157CC File Offset: 0x001147CC
		public MD5CryptoServiceProvider()
		{
			if (Utils.FipsAlgorithmPolicy == 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			SafeHashHandle invalidHandle = SafeHashHandle.InvalidHandle;
			Utils._CreateHash(Utils.StaticProvHandle, 32771, ref invalidHandle);
			this._safeHashHandle = invalidHandle;
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x00115815 File Offset: 0x00114815
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x00115840 File Offset: 0x00114840
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			SafeHashHandle invalidHandle = SafeHashHandle.InvalidHandle;
			Utils._CreateHash(Utils.StaticProvHandle, 32771, ref invalidHandle);
			this._safeHashHandle = invalidHandle;
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0011588B File Offset: 0x0011488B
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils._HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0011589B File Offset: 0x0011489B
		protected override byte[] HashFinal()
		{
			return Utils._EndHash(this._safeHashHandle);
		}

		// Token: 0x04002918 RID: 10520
		private SafeHashHandle _safeHashHandle;
	}
}

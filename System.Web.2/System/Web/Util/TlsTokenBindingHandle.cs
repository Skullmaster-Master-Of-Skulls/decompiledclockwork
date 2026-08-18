using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.Hosting;

namespace System.Web.Util
{
	// Token: 0x020001CA RID: 458
	internal sealed class TlsTokenBindingHandle : HeapAllocHandle
	{
		// Token: 0x0600175C RID: 5980 RVA: 0x000495F4 File Offset: 0x000477F4
		internal TlsTokenBindingHandle(IntPtr mgdContext)
		{
			int hresult = UnsafeIISMethods.MgdGetTlsTokenBindingIdentifiers(mgdContext, ref this.handle, out this._providedTokenBlob, out this._providedTokenBlobSize, out this._referredTokenBlob, out this._referredtokenBlobSize);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00049632 File Offset: 0x00047832
		public byte[] GetProvidedToken()
		{
			return this.GetTokenImpl(this._providedTokenBlob, this._providedTokenBlobSize);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00049646 File Offset: 0x00047846
		public byte[] GetReferredToken()
		{
			return this.GetTokenImpl(this._referredTokenBlob, this._referredtokenBlobSize);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0004965C File Offset: 0x0004785C
		private byte[] GetTokenImpl(IntPtr blob, uint blobSize)
		{
			if (blob == IntPtr.Zero || blobSize == 0U)
			{
				return null;
			}
			byte[] array = new byte[blobSize];
			int length = array.Length;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				Marshal.Copy(blob, array, 0, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return array;
		}

		// Token: 0x04001703 RID: 5891
		private readonly IntPtr _providedTokenBlob;

		// Token: 0x04001704 RID: 5892
		private readonly uint _providedTokenBlobSize;

		// Token: 0x04001705 RID: 5893
		private readonly IntPtr _referredTokenBlob;

		// Token: 0x04001706 RID: 5894
		private readonly uint _referredtokenBlobSize;
	}
}

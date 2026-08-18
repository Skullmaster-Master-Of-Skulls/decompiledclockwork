using System;

namespace System.Web.Hosting
{
	// Token: 0x020007EF RID: 2031
	internal sealed class TlsTokenBindingInfo : ITlsTokenBindingInfo
	{
		// Token: 0x060060E9 RID: 24809 RVA: 0x0014E37B File Offset: 0x0014C57B
		internal TlsTokenBindingInfo(byte[] providedTokenBindingId, byte[] referredTokenBindingId)
		{
			this._providedTokenBindingId = providedTokenBindingId;
			this._referredTokenBindingId = referredTokenBindingId;
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x0014E391 File Offset: 0x0014C591
		public byte[] GetProvidedTokenBindingId()
		{
			if (this._providedTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._providedTokenBindingId.Clone();
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0014E3AD File Offset: 0x0014C5AD
		public byte[] GetReferredTokenBindingId()
		{
			if (this._referredTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._referredTokenBindingId.Clone();
		}

		// Token: 0x0400326B RID: 12907
		private readonly byte[] _providedTokenBindingId;

		// Token: 0x0400326C RID: 12908
		private readonly byte[] _referredTokenBindingId;
	}
}

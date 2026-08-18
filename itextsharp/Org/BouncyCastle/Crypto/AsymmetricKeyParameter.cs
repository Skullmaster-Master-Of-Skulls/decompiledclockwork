using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200011C RID: 284
	public class AsymmetricKeyParameter : ICipherParameters
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x00037DEE File Offset: 0x00036DEE
		public AsymmetricKeyParameter(bool privateKey)
		{
			this.privateKey = privateKey;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00037DFD File Offset: 0x00036DFD
		public bool IsPrivate
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00037E08 File Offset: 0x00036E08
		public override bool Equals(object obj)
		{
			AsymmetricKeyParameter asymmetricKeyParameter = obj as AsymmetricKeyParameter;
			return asymmetricKeyParameter != null && this.Equals(asymmetricKeyParameter);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00037E28 File Offset: 0x00036E28
		protected bool Equals(AsymmetricKeyParameter other)
		{
			return this.privateKey == other.privateKey;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00037E38 File Offset: 0x00036E38
		public override int GetHashCode()
		{
			return this.privateKey.GetHashCode();
		}

		// Token: 0x04000878 RID: 2168
		private readonly bool privateKey;
	}
}

using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001FD RID: 509
	public class RequestedProofToken
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x00047545 File Offset: 0x00045745
		public RequestedProofToken(string computedKeyAlgorithm)
		{
			if (string.IsNullOrEmpty(computedKeyAlgorithm))
			{
				DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("computedKeyAlgorithm");
			}
			this._computedKeyAlgorithm = computedKeyAlgorithm;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0004756C File Offset: 0x0004576C
		public RequestedProofToken(byte[] secret)
		{
			this._keys = new ProtectedKey(secret);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00047580 File Offset: 0x00045780
		public RequestedProofToken(byte[] secret, EncryptingCredentials wrappingCredentials)
		{
			this._keys = new ProtectedKey(secret, wrappingCredentials);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00047595 File Offset: 0x00045795
		public RequestedProofToken(ProtectedKey protectedKey)
		{
			if (protectedKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("protectedKey");
			}
			this._keys = protectedKey;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x000475B7 File Offset: 0x000457B7
		public string ComputedKeyAlgorithm
		{
			get
			{
				return this._computedKeyAlgorithm;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000475BF File Offset: 0x000457BF
		public ProtectedKey ProtectedKey
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x04000E7E RID: 3710
		private string _computedKeyAlgorithm;

		// Token: 0x04000E7F RID: 3711
		private ProtectedKey _keys;
	}
}

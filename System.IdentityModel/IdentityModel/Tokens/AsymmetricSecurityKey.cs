using System;
using System.Security.Cryptography;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000112 RID: 274
	public abstract class AsymmetricSecurityKey : SecurityKey
	{
		// Token: 0x0600077D RID: 1917
		public abstract AsymmetricAlgorithm GetAsymmetricAlgorithm(string algorithm, bool privateKey);

		// Token: 0x0600077E RID: 1918
		public abstract HashAlgorithm GetHashAlgorithmForSignature(string algorithm);

		// Token: 0x0600077F RID: 1919
		public abstract AsymmetricSignatureDeformatter GetSignatureDeformatter(string algorithm);

		// Token: 0x06000780 RID: 1920
		public abstract AsymmetricSignatureFormatter GetSignatureFormatter(string algorithm);

		// Token: 0x06000781 RID: 1921
		public abstract bool HasPrivateKey();
	}
}

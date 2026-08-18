using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000167 RID: 359
	public abstract class SecurityKey
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B4E RID: 2894
		public abstract int KeySize { get; }

		// Token: 0x06000B4F RID: 2895
		public abstract byte[] DecryptKey(string algorithm, byte[] keyData);

		// Token: 0x06000B50 RID: 2896
		public abstract byte[] EncryptKey(string algorithm, byte[] keyData);

		// Token: 0x06000B51 RID: 2897
		public abstract bool IsAsymmetricAlgorithm(string algorithm);

		// Token: 0x06000B52 RID: 2898
		public abstract bool IsSupportedAlgorithm(string algorithm);

		// Token: 0x06000B53 RID: 2899
		public abstract bool IsSymmetricAlgorithm(string algorithm);
	}
}

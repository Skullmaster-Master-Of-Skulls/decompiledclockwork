using System;
using System.IdentityModel;

namespace System.ServiceModel.Security
{
	// Token: 0x020002BB RID: 699
	internal static class Psha1DerivedKeyGeneratorHelper
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00053F68 File Offset: 0x00052168
		internal static byte[] GenerateDerivedKey(byte[] key, byte[] label, byte[] nonce, int derivedKeySize, int position)
		{
			Psha1DerivedKeyGenerator psha1DerivedKeyGenerator = new Psha1DerivedKeyGenerator(key);
			return psha1DerivedKeyGenerator.GenerateDerivedKey(label, nonce, derivedKeySize, position);
		}
	}
}

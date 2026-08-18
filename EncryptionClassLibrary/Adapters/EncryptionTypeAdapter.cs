using System;

namespace EncryptionClassLibrary.Adapters
{
	// Token: 0x02000016 RID: 22
	public static class EncryptionTypeAdapter
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000599C File Offset: 0x00003B9C
		public static EncryptionType ParseEncryptionType(this string encryptionType)
		{
			bool flag = string.IsNullOrWhiteSpace(encryptionType);
			EncryptionType result;
			if (flag)
			{
				result = EncryptionType.TripleDES_192bit;
			}
			else
			{
				string text = encryptionType.ToLower().Trim();
				string a = text;
				EncryptionType encryptionType2;
				if (!(a == "tripledes_128bit"))
				{
					if (!(a == "tripledes_192bit"))
					{
						if (!(a == "tripledes_192bit_randomiv"))
						{
							if (!(a == "aes_256bit"))
							{
								encryptionType2 = EncryptionType.TripleDES_192bit;
							}
							else
							{
								encryptionType2 = EncryptionType.AES_256bit;
							}
						}
						else
						{
							encryptionType2 = EncryptionType.TripleDES_192bit_RandomIv;
						}
					}
					else
					{
						encryptionType2 = EncryptionType.TripleDES_192bit;
					}
				}
				else
				{
					encryptionType2 = EncryptionType.TripleDES_128bit;
				}
				result = encryptionType2;
			}
			return result;
		}

		// Token: 0x04000031 RID: 49
		public const EncryptionType DefaultEncryptionType = EncryptionType.TripleDES_192bit;
	}
}

using System;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x02000109 RID: 265
	internal sealed class PemUtilities
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x00036BDC File Offset: 0x00035BDC
		internal static bool ParseDekAlgName(string dekAlgName, out string baseAlg, out string mode)
		{
			baseAlg = dekAlgName;
			mode = "ECB";
			if (dekAlgName == "DES-EDE" || dekAlgName == "DES-EDE3")
			{
				return true;
			}
			int num = dekAlgName.LastIndexOf('-');
			if (num < 0)
			{
				return false;
			}
			baseAlg = dekAlgName.Substring(0, num);
			mode = dekAlgName.Substring(num + 1);
			return true;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00036C34 File Offset: 0x00035C34
		internal static byte[] Crypt(bool encrypt, byte[] bytes, char[] password, string dekAlgName, byte[] iv)
		{
			string text;
			string text2;
			if (!PemUtilities.ParseDekAlgName(dekAlgName, out text, out text2))
			{
				throw new EncryptionException("Unknown DEK algorithm: " + dekAlgName);
			}
			string a;
			if ((a = text2) != null)
			{
				string text3;
				if (!(a == "CBC") && !(a == "ECB"))
				{
					if (!(a == "CFB") && !(a == "OFB"))
					{
						goto IL_6D;
					}
					text3 = "NoPadding";
				}
				else
				{
					text3 = "PKCS5Padding";
				}
				byte[] array = iv;
				string key;
				if ((key = text) != null)
				{
					if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a34-1 == null)
					{
						<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a34-1 = new Dictionary<string, int>(10)
						{
							{
								"AES-128",
								0
							},
							{
								"AES-192",
								1
							},
							{
								"AES-256",
								2
							},
							{
								"BF",
								3
							},
							{
								"DES",
								4
							},
							{
								"DES-EDE",
								5
							},
							{
								"DES-EDE3",
								6
							},
							{
								"RC2",
								7
							},
							{
								"RC2-40",
								8
							},
							{
								"RC2-64",
								9
							}
						};
					}
					int num;
					if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a34-1.TryGetValue(key, out num))
					{
						string text4;
						switch (num)
						{
						case 0:
						case 1:
						case 2:
							text4 = "AES";
							if (array.Length > 8)
							{
								array = new byte[8];
								Array.Copy(iv, 0, array, 0, array.Length);
							}
							break;
						case 3:
							text4 = "BLOWFISH";
							break;
						case 4:
							text4 = "DES";
							break;
						case 5:
						case 6:
							text4 = "DESede";
							break;
						case 7:
						case 8:
						case 9:
							text4 = "RC2";
							break;
						default:
							goto IL_1A7;
						}
						string algorithm = string.Concat(new string[]
						{
							text4,
							"/",
							text2,
							"/",
							text3
						});
						IBufferedCipher cipher = CipherUtilities.GetCipher(algorithm);
						ICipherParameters parameters = PemUtilities.GetCipherParameters(password, text, array);
						if (text2 != "ECB")
						{
							parameters = new ParametersWithIV(parameters, iv);
						}
						cipher.Init(encrypt, parameters);
						return cipher.DoFinal(bytes);
					}
				}
				IL_1A7:
				throw new EncryptionException("Unknown DEK algorithm: " + dekAlgName);
			}
			IL_6D:
			throw new EncryptionException("Unknown DEK algorithm: " + dekAlgName);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00036E6C File Offset: 0x00035E6C
		private static ICipherParameters GetCipherParameters(char[] password, string baseAlg, byte[] salt)
		{
			if (baseAlg != null)
			{
				if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a35-1 == null)
				{
					<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a35-1 = new Dictionary<string, int>(10)
					{
						{
							"AES-128",
							0
						},
						{
							"AES-192",
							1
						},
						{
							"AES-256",
							2
						},
						{
							"BF",
							3
						},
						{
							"DES",
							4
						},
						{
							"DES-EDE",
							5
						},
						{
							"DES-EDE3",
							6
						},
						{
							"RC2",
							7
						},
						{
							"RC2-40",
							8
						},
						{
							"RC2-64",
							9
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a35-1.TryGetValue(baseAlg, out num))
				{
					int keySize;
					string algorithm;
					switch (num)
					{
					case 0:
						keySize = 128;
						algorithm = "AES128";
						break;
					case 1:
						keySize = 192;
						algorithm = "AES192";
						break;
					case 2:
						keySize = 256;
						algorithm = "AES256";
						break;
					case 3:
						keySize = 128;
						algorithm = "BLOWFISH";
						break;
					case 4:
						keySize = 64;
						algorithm = "DES";
						break;
					case 5:
						keySize = 128;
						algorithm = "DESEDE";
						break;
					case 6:
						keySize = 192;
						algorithm = "DESEDE3";
						break;
					case 7:
						keySize = 128;
						algorithm = "RC2";
						break;
					case 8:
						keySize = 40;
						algorithm = "RC2";
						break;
					case 9:
						keySize = 64;
						algorithm = "RC2";
						break;
					default:
						goto IL_166;
					}
					OpenSslPbeParametersGenerator openSslPbeParametersGenerator = new OpenSslPbeParametersGenerator();
					openSslPbeParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(password), salt);
					return openSslPbeParametersGenerator.GenerateDerivedParameters(algorithm, keySize);
				}
			}
			IL_166:
			return null;
		}
	}
}

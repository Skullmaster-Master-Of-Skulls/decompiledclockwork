using System;
using System.Security.Cryptography;
using System.Text;
using \u0003;
using \u0005;
using OracleInternal.I18N;
using OracleInternal.Secure.Encryption;

namespace OracleInternal.Secure.Logon
{
	// Token: 0x02000344 RID: 836
	internal sealed class O5LogonHelper
	{
		// Token: 0x06001D57 RID: 7511 RVA: 0x0011FA20 File Offset: 0x0011DC20
		static O5LogonHelper()
		{
			O5LogonHelper.\u0019 = Encoding.ASCII.GetBytes(global::\u0005.\u0001.\u0001(305));
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0011FA70 File Offset: 0x0011DC70
		internal static bool DoLogonProcessing(int verifierType, byte[] salt, byte logonCompatibility, string noQuotesUser, string noQuotesPwd, byte[] noQuotesPwdNetBytes, byte[] encryptedSK, byte[] pbkdf2_csk_salt, int pbkdf2_vgen_count, int auth_pbkdf2_sder_count, bool bSvrCSMultibyte, out byte[] encryptedKB, out byte[] encryptedPassword, out byte[] newKey, out byte[] confounder, out byte[] pbkdf2_speedy_key)
		{
			return O5LogonHelper.\u0001(verifierType, salt, logonCompatibility, noQuotesUser, noQuotesPwd, noQuotesPwdNetBytes, encryptedSK, pbkdf2_csk_salt, pbkdf2_vgen_count, auth_pbkdf2_sder_count, bSvrCSMultibyte, out encryptedKB, out encryptedPassword, out newKey, out confounder, out pbkdf2_speedy_key);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0011FAA0 File Offset: 0x0011DCA0
		internal static void ProcessNewPassword(string pwdEncStr, byte[] noQuotesPwdNetBytes, byte[] encKey, ref byte[] password, ref byte[] confounder)
		{
			O5LogonHelper.\u0001(pwdEncStr, noQuotesPwdNetBytes, encKey, ref password, ref confounder);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0011FAB0 File Offset: 0x0011DCB0
		internal static byte[] EvaluateServerResponse(string cipherStr, byte[] key, byte[] msgHex)
		{
			return O5LogonHelper.\u0001(cipherStr, key, msgHex);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0011FABC File Offset: 0x0011DCBC
		internal static byte[] EncryptOraAuthJDWPValue(bool bExternalAuthentication, ushort serverOne, byte[] key, byte[] valueToEncrypt)
		{
			return O5LogonHelper.\u0001(bExternalAuthentication, serverOne, key, valueToEncrypt);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0011FAC8 File Offset: 0x0011DCC8
		private static bool \u0001(int \u0002, byte[] \u0003, byte \u0004, string \u0005, string \u0006, byte[] \u0007, byte[] \u0008, byte[] \u000E, int \u000F, int \u0010, bool \u0011, out byte[] \u0012, out byte[] \u0013, out byte[] \u0014, out byte[] \u0015, out byte[] \u0016)
		{
			bool result = true;
			byte[] array = null;
			int num = 0;
			int num2 = 0;
			int num3 = 16;
			string u = null;
			string u2 = global::\u0005.\u0001.\u0001(338);
			\u0012 = null;
			\u0013 = null;
			\u0014 = null;
			\u0015 = null;
			\u0016 = null;
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				try
				{
					int num4;
					if (2361 != \u0002)
					{
						if (6949 == \u0002)
						{
							num4 = 24;
							num = 24;
							num2 = 96;
							if ((\u0004 & 2) != 0)
							{
								u = global::\u0005.\u0001.\u0001(367);
							}
							else
							{
								u = global::\u0005.\u0001.\u0001(338);
							}
							HashAlgorithm hashAlgorithm = null;
							array = new byte[num4];
							try
							{
								hashAlgorithm = new SHA1CryptoServiceProvider();
								string @string = Encoding.ASCII.GetString(\u0003);
								byte[] array2 = O5LogonHelper.\u0001(@string);
								byte[] bytes = Encoding.UTF8.GetBytes(\u0006);
								byte[] array3 = new byte[bytes.Length + array2.Length];
								Array.Copy(bytes, 0, array3, 0, bytes.Length);
								Array.Copy(array2, 0, array3, bytes.Length, array2.Length);
								byte[] array4 = hashAlgorithm.ComputeHash(array3);
								Array.Copy(array4, 0, array, 0, array4.Length);
								goto IL_285;
							}
							finally
							{
								if (hashAlgorithm != null)
								{
									hashAlgorithm.Clear();
									hashAlgorithm.Dispose();
								}
							}
						}
						if (18453 == \u0002)
						{
							num4 = 32;
							num = 32;
							u = global::\u0005.\u0001.\u0001(367);
							string string2 = Encoding.ASCII.GetString(\u0003);
							byte[] array5 = O5LogonHelper.\u0001(string2);
							byte[] array6 = new byte[array5.Length + O5LogonHelper.\u0019.Length];
							Array.Copy(array5, array6, array5.Length);
							Array.Copy(O5LogonHelper.\u0019, 0, array6, array5.Length, O5LogonHelper.\u0019.Length);
							SHA512CryptoServiceProvider sha512CryptoServiceProvider = null;
							\u0003.\u0001 u3 = null;
							array = new byte[num4];
							try
							{
								byte[] bytes2 = Encoding.UTF8.GetBytes(\u0006);
								u3 = new \u0003.\u0001(new HMACSHA512(bytes2), array6, \u000F);
								\u0016 = u3.\u0001(64);
								sha512CryptoServiceProvider = new SHA512CryptoServiceProvider();
								byte[] array7 = new byte[\u0016.Length + array5.Length];
								Array.Copy(\u0016, 0, array7, 0, \u0016.Length);
								Array.Copy(array5, 0, array7, \u0016.Length, array5.Length);
								byte[] sourceArray = sha512CryptoServiceProvider.ComputeHash(array7);
								Array.Copy(sourceArray, 0, array, 0, num4);
								goto IL_285;
							}
							finally
							{
								if (sha512CryptoServiceProvider != null)
								{
									sha512CryptoServiceProvider.Clear();
									sha512CryptoServiceProvider.Dispose();
								}
								if (u3 != null)
								{
									u3.\u0002();
								}
							}
						}
						return false;
					}
					num4 = 16;
					num = 16;
					num2 = 64;
					u = global::\u0005.\u0001.\u0001(367);
					WorkBench workBench = new WorkBench();
					byte[] sourceArray2 = workBench.\u0001(\u0005, \u0006, \u0011);
					array = new byte[num4];
					Array.Copy(sourceArray2, 0, array, 0, 8);
					for (int i = 8; i < 16; i++)
					{
						array[i] = 0;
					}
					IL_285:
					byte[] array8 = O5LogonHelper.\u0001(u, array, \u0008);
					if (array8 != null && array8.Length < num)
					{
						array8 = new byte[num];
						rngcryptoServiceProvider.GetBytes(array8);
					}
					byte[] array9 = new byte[array8.Length];
					rngcryptoServiceProvider.GetBytes(array9);
					byte[] u4 = O5LogonHelper.\u0002(u, array, array9);
					if (\u0012 == null || \u0012.Length != num2)
					{
						\u0012 = new byte[\u0008.Length];
						rngcryptoServiceProvider.GetBytes(\u0012);
					}
					O5LogonHelper.\u0001(u4, \u0012, 0);
					\u0014 = O5LogonHelper.\u0001(\u0002, array8, O5LogonHelper.\u0016, array9, O5LogonHelper.\u0016, \u000E, \u0010, \u0004);
					if (\u0014.Length != num)
					{
						\u0014 = new byte[num];
						rngcryptoServiceProvider.GetBytes(\u0014);
					}
					\u0015 = new byte[num3];
					rngcryptoServiceProvider.GetBytes(\u0015);
					byte[] array10 = new byte[num3 + \u0007.Length];
					Array.Copy(\u0015, 0, array10, 0, num3);
					Array.Copy(\u0007, 0, array10, num3, \u0007.Length);
					byte[] array11 = O5LogonHelper.\u0002(u2, \u0014, array10);
					if (array11 != null)
					{
						\u0013 = new byte[array11.Length * 2];
						O5LogonHelper.\u0001(array11, \u0013, 0);
					}
					if (18453 == \u0002)
					{
						byte[] array12 = new byte[num3];
						rngcryptoServiceProvider.GetBytes(array12);
						byte[] array13 = new byte[num3 + \u0016.Length];
						Array.Copy(array12, 0, array13, 0, num3);
						Array.Copy(\u0016, 0, array13, num3, \u0016.Length);
						byte[] array14 = O5LogonHelper.\u0002(u, \u0014, array13);
						if (array14 != null)
						{
							\u0016 = new byte[array14.Length * 2];
							O5LogonHelper.\u0001(array14, \u0016, 0);
						}
					}
				}
				catch (Exception)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0011FF68 File Offset: 0x0011E168
		private static byte[] \u0001(int \u0002, byte[] \u0003, int \u0004, byte[] \u0005, int \u0006, byte[] \u0007, int \u0008, byte \u000E)
		{
			byte[] array = null;
			byte[] array2 = null;
			MD5CryptoServiceProvider md5CryptoServiceProvider = null;
			\u0003.\u0001 u = null;
			try
			{
				int num;
				if (\u0002 != 2361)
				{
					if (\u0002 != 6949)
					{
						if (\u0002 != 18453)
						{
							array = new byte[0];
							return array;
						}
						num = 32;
						byte[] array3 = new byte[\u0003.Length + \u0005.Length];
						Array.Copy(\u0005, 0, array3, 0, \u0005.Length);
						Array.Copy(\u0003, 0, array3, \u0005.Length, \u0003.Length);
						array2 = new byte[array3.Length * 2];
						O5LogonHelper.\u0001(array3, array2, 0);
					}
					else
					{
						num = 24;
						if ((\u000E & 32) != 0)
						{
							byte[] array4 = new byte[num * 2];
							Array.Copy(\u0005, 0, array4, 0, num);
							Array.Copy(\u0003, 0, array4, num, num);
							array2 = new byte[array4.Length * 2];
							O5LogonHelper.\u0001(array4, array2, 0);
						}
						else
						{
							md5CryptoServiceProvider = new MD5CryptoServiceProvider();
							md5CryptoServiceProvider.Initialize();
							byte[] array5 = new byte[num];
							for (int i = 0; i < num; i++)
							{
								array5[i] = (\u0003[i + \u0004] ^ \u0005[i + \u0006]);
							}
							array = new byte[24];
							byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(array5, 0, 16);
							Array.Copy(sourceArray, 0, array, 0, 16);
							md5CryptoServiceProvider.Initialize();
							sourceArray = md5CryptoServiceProvider.ComputeHash(array5, 16, 8);
							Array.Copy(sourceArray, 0, array, 16, 8);
						}
					}
				}
				else
				{
					num = 16;
					if ((\u000E & 32) != 0)
					{
						byte[] array6 = new byte[num * 2];
						Array.Copy(\u0005, 0, array6, 0, \u0005.Length / 2);
						Array.Copy(\u0003, 0, array6, \u0005.Length / 2, \u0003.Length / 2);
						array2 = new byte[array6.Length * 2];
						O5LogonHelper.\u0001(array6, array2, 0);
					}
					else
					{
						md5CryptoServiceProvider = new MD5CryptoServiceProvider();
						md5CryptoServiceProvider.Initialize();
						byte[] array5 = new byte[num];
						for (int i = 0; i < num; i++)
						{
							array5[i] = (\u0003[i + \u0004] ^ \u0005[i + \u0006]);
						}
						array = md5CryptoServiceProvider.ComputeHash(array5);
					}
				}
				if (array2 != null && (\u000E & 32) != 0)
				{
					string @string = Encoding.ASCII.GetString(\u0007);
					\u0007 = O5LogonHelper.\u0001(@string);
					u = new \u0003.\u0001(new HMACSHA512(array2), \u0007, \u0008);
					array = u.\u0001(num);
				}
			}
			finally
			{
				if (md5CryptoServiceProvider != null)
				{
					md5CryptoServiceProvider.Clear();
					md5CryptoServiceProvider.Dispose();
				}
				if (u != null)
				{
					u.\u0002();
				}
			}
			return array;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x001201C8 File Offset: 0x0011E3C8
		private static byte[] \u0001(string \u0002, byte[] \u0003, byte[] \u0004)
		{
			if (\u0003 == null)
			{
				return new byte[0];
			}
			byte[] result = new byte[0];
			AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
			try
			{
				if (\u0002.Contains(global::\u0005.\u0001.\u0001(392)))
				{
					aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				}
				else
				{
					aesCryptoServiceProvider.Padding = PaddingMode.None;
				}
				aesCryptoServiceProvider.Mode = CipherMode.CBC;
				aesCryptoServiceProvider.KeySize = \u0003.Length * 8;
				aesCryptoServiceProvider.BlockSize = O5LogonHelper.\u0017;
				aesCryptoServiceProvider.Key = \u0003;
				aesCryptoServiceProvider.IV = O5LogonHelper.\u0018;
				ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor();
				byte[] array = O5LogonHelper.\u0001(Conv.GetInstance(871).ConvertBytesToString(\u0004, 0, \u0004.Length, null, true));
				result = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			}
			catch
			{
			}
			finally
			{
				aesCryptoServiceProvider.Clear();
				aesCryptoServiceProvider.Dispose();
			}
			return result;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x001202A0 File Offset: 0x0011E4A0
		private static byte[] \u0002(string \u0002, byte[] \u0003, byte[] \u0004)
		{
			if (\u0003 == null)
			{
				return new byte[0];
			}
			byte[] result = null;
			AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
			try
			{
				if (\u0002.Contains(global::\u0005.\u0001.\u0001(392)))
				{
					aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				}
				else if (\u0002.Contains(global::\u0005.\u0001.\u0001(409)))
				{
					aesCryptoServiceProvider.Padding = PaddingMode.Zeros;
				}
				else
				{
					aesCryptoServiceProvider.Padding = PaddingMode.None;
				}
				aesCryptoServiceProvider.Mode = CipherMode.CBC;
				aesCryptoServiceProvider.KeySize = \u0003.Length * 8;
				aesCryptoServiceProvider.BlockSize = O5LogonHelper.\u0017;
				aesCryptoServiceProvider.Key = \u0003;
				aesCryptoServiceProvider.IV = O5LogonHelper.\u0018;
				ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateEncryptor();
				result = cryptoTransform.TransformFinalBlock(\u0004, 0, \u0004.Length);
			}
			catch
			{
			}
			finally
			{
				aesCryptoServiceProvider.Clear();
				aesCryptoServiceProvider.Dispose();
			}
			return result;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00120370 File Offset: 0x0011E570
		private static void \u0001(string \u0002, byte[] \u0003, byte[] \u0004, ref byte[] \u0005, ref byte[] \u0006)
		{
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			int num = 16;
			if (\u0006 == null)
			{
				\u0006 = new byte[num];
				rngcryptoServiceProvider.GetBytes(\u0006);
			}
			byte[] array = new byte[num + \u0003.Length];
			Array.Copy(\u0006, 0, array, 0, num);
			Array.Copy(\u0003, 0, array, num, \u0003.Length);
			byte[] array2 = O5LogonHelper.\u0002(\u0002, \u0004, array);
			if (array2 != null)
			{
				O5LogonHelper.\u0001(array2, \u0005, 0);
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x001203DC File Offset: 0x0011E5DC
		private static byte[] \u0001(bool \u0002, ushort \u0003, byte[] \u0004, byte[] \u0005)
		{
			byte[] array5;
			if (\u0002)
			{
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				byte[] array = new byte[16];
				RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
				rngcryptoServiceProvider.GetBytes(array);
				tripleDESCryptoServiceProvider.IV = new byte[tripleDESCryptoServiceProvider.BlockSize / 8];
				tripleDESCryptoServiceProvider.Key = array;
				tripleDESCryptoServiceProvider.Padding = PaddingMode.Zeros;
				tripleDESCryptoServiceProvider.Mode = CipherMode.CBC;
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
				byte[] array2 = cryptoTransform.TransformFinalBlock(\u0005, 0, \u0005.Length);
				long[] array3 = new long[4];
				byte[] array4 = new byte[16];
				array5 = new byte[(array2.Length + array4.Length + 1) * 2];
				O5LogonHelper.\u0001(array2, array5, 0);
				array3[0] = (long)((ulong)BitConverter.ToUInt32(array, 0));
				array3[1] = (long)((ulong)BitConverter.ToUInt32(array, 4));
				array3[2] = (long)((ulong)BitConverter.ToUInt32(array, 8));
				array3[3] = (long)((ulong)BitConverter.ToUInt32(array, 12));
				if (BitConverter.IsLittleEndian && \u0003 == 256)
				{
					array3[0] ^= (long)((ulong)-1866822634);
					array3[1] ^= 1571793352L;
					array3[2] ^= (long)((ulong)-275225009);
					array3[3] ^= (long)((ulong)-18461189);
				}
				else
				{
					array3[0] ^= 378321552L;
					array3[1] ^= (long)((ulong)-928403619);
					array3[2] ^= 1332123887L;
					array3[3] ^= (long)((ulong)-78780674);
				}
				Array.Copy(BitConverter.GetBytes(array3[0]), 0, array4, 0, 4);
				Array.Copy(BitConverter.GetBytes(array3[1]), 0, array4, 4, 4);
				Array.Copy(BitConverter.GetBytes(array3[2]), 0, array4, 8, 4);
				Array.Copy(BitConverter.GetBytes(array3[3]), 0, array4, 12, 4);
				O5LogonHelper.\u0001(array4, array5, array2.Length * 2);
				array5[array5.Length - 2] = O5LogonHelper.\u0001(0);
				array5[array5.Length - 1] = O5LogonHelper.\u0001(2);
			}
			else
			{
				byte[] array2 = O5LogonHelper.\u0002(global::\u0005.\u0001.\u0001(418), \u0004, \u0005);
				array5 = new byte[(array2.Length + 1) * 2];
				O5LogonHelper.\u0001(array2, array5, 0);
				array5[array2.Length * 2] = O5LogonHelper.\u0001(0);
				array5[array2.Length * 2 + 1] = O5LogonHelper.\u0001(1);
			}
			return array5;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00120644 File Offset: 0x0011E844
		private static byte \u0001(byte \u0002)
		{
			\u0002 &= 15;
			return (\u0002 < 10) ? (\u0002 + 48) : (\u0002 - 10 + 65);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00120660 File Offset: 0x0011E860
		private static void \u0001(byte[] \u0002, byte[] \u0003, int \u0004)
		{
			for (int i = 0; i < \u0002.Length; i++)
			{
				\u0003[\u0004 + i * 2] = O5LogonHelper.\u0001((byte)((\u0002[i] & 240) >> 4));
				\u0003[\u0004 + i * 2 + 1] = O5LogonHelper.\u0001(\u0002[i] & 15);
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x001206A8 File Offset: 0x0011E8A8
		private static byte[] \u0001(string \u0002)
		{
			byte[] array = new byte[\u0002.Length / 2];
			for (int i = 0; i < \u0002.Length / 2; i++)
			{
				byte b = Convert.ToByte(\u0002.Substring(2 * i, 1), 16);
				byte b2 = Convert.ToByte(\u0002.Substring(2 * i + 1, 1), 16);
				int num = (int)b2 | (int)b << 4;
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0012070C File Offset: 0x0011E90C
		private static string \u0001(byte[] \u0002)
		{
			StringBuilder stringBuilder = new StringBuilder(\u0002.Length * 2);
			if (\u0002 != null)
			{
				for (int i = 0; i < \u0002.Length; i++)
				{
					if (i == 0)
					{
						stringBuilder.Append(global::\u0005.\u0001.\u0001(439));
					}
					stringBuilder.AppendFormat(global::\u0005.\u0001.\u0001(444), \u0002[i]);
				}
				stringBuilder.Append(global::\u0005.\u0001.\u0001(453));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001F9C RID: 8092
		private const byte \u0001 = 2;

		// Token: 0x04001F9D RID: 8093
		private const byte \u0002 = 32;

		// Token: 0x04001F9E RID: 8094
		private const string \u0003 = "AUTH_PBKDF2_SPEEDY_KEY";

		// Token: 0x04001F9F RID: 8095
		private const byte \u0004 = 1;

		// Token: 0x04001FA0 RID: 8096
		private const byte \u0005 = 2;

		// Token: 0x04001FA1 RID: 8097
		private const long \u0006 = 2428144662L;

		// Token: 0x04001FA2 RID: 8098
		private const long \u0007 = 1571793352L;

		// Token: 0x04001FA3 RID: 8099
		private const long \u0008 = 4019742287L;

		// Token: 0x04001FA4 RID: 8100
		private const long \u000E = 4276506107L;

		// Token: 0x04001FA5 RID: 8101
		private const long \u000F = 378321552L;

		// Token: 0x04001FA6 RID: 8102
		private const long \u0010 = 3366563677L;

		// Token: 0x04001FA7 RID: 8103
		private const long \u0011 = 1332123887L;

		// Token: 0x04001FA8 RID: 8104
		private const long \u0012 = 4216186622L;

		// Token: 0x04001FA9 RID: 8105
		internal const int \u0013 = 2361;

		// Token: 0x04001FAA RID: 8106
		internal const int \u0014 = 6949;

		// Token: 0x04001FAB RID: 8107
		internal const int \u0015 = 18453;

		// Token: 0x04001FAC RID: 8108
		private static int \u0016 = 16;

		// Token: 0x04001FAD RID: 8109
		private static int \u0017 = 128;

		// Token: 0x04001FAE RID: 8110
		private static byte[] \u0018 = new byte[O5LogonHelper.\u0017 / 8];

		// Token: 0x04001FAF RID: 8111
		internal static byte[] \u0019 = null;
	}
}

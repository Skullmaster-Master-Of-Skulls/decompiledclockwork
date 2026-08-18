using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using OracleInternal.Common;
using OracleInternal.Secure.Network;

namespace OracleInternal.Network
{
	// Token: 0x0200013F RID: 319
	internal class SEPS
	{
		// Token: 0x06000CAE RID: 3246
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CryptDecodeObject(uint dwCertEncodingType, string lpszStructType, IntPtr pbEncoded, uint cbEncoded, uint dwFlags, IntPtr pvStructInfo, ref uint pcbStructInfo);

		// Token: 0x06000CAF RID: 3247
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CryptDecodeObject(uint dwCertEncodingType, uint lpszStructType, IntPtr pbEncoded, uint cbEncoded, uint dwFlags, IntPtr pvStructInfo, ref uint pcbStructInfo);

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0008C1A4 File Offset: 0x0008A3A4
		internal static void GetSEPSUandP(string ConnectString, out string Userid, out string PW, out string WP, out string WF)
		{
			SqlNetOraConfig sqlNetOraConfig = new SqlNetOraConfig();
			string text = null;
			string password = null;
			byte[] array = null;
			bool bTraceLevelNetwork = ProviderConfig.m_bTraceLevelNetwork;
			string text2;
			WP = (text2 = null);
			string text3;
			WF = (text3 = text2);
			string text4;
			PW = (text4 = text3);
			Userid = text4;
			try
			{
				Hashtable walletLocation = SqlNetOraConfig.WalletLocation;
				if (walletLocation != null)
				{
					string text5 = ((string)walletLocation["METHOD"]).ToUpperInvariant();
					if (text5 != null && text5 == "FILE")
					{
						text = (string)walletLocation["DIRECTORY"];
					}
				}
				if (text == null)
				{
					throw new NetworkException(-6400);
				}
				if (sqlNetOraConfig != null)
				{
					password = sqlNetOraConfig["WALLET_PASSWORD"];
				}
				array = WalletReader.ReadWallet(text, ref password);
			}
			catch (NetworkException ex)
			{
				if (bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
					{
						"SEPS: Wallet config invalid."
					});
				}
				throw ex;
			}
			catch (Exception inner)
			{
				if (bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
					{
						"SEPS: Open wallet failed."
					});
				}
				throw new NetworkException(-6400, inner);
			}
			WP = text;
			WF = "cwallet.sso";
			byte[] array2;
			byte[] salt;
			int sha1iterations;
			SEPS.PbeAlgorithmType algType;
			if (SEPS.ExtractDataAndKeyParametersFromPFX(array, (uint)array.Length, out array2, out salt, out sha1iterations, out algType))
			{
				byte[] encodedBags;
				try
				{
					ICryptoTransform transform = SEPS.GeneratePbeSHA1Decryptor(password, salt, sha1iterations, algType);
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
					cryptoStream.Write(array2, 0, array2.Length);
					encodedBags = memoryStream.ToArray();
					cryptoStream.Close();
				}
				catch (Exception ex2)
				{
					if (bTraceLevelNetwork)
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Error, new string[]
						{
							"SEPS: SHA1Decryptor failure: " + ex2
						});
					}
					throw new NetworkException(12578, ex2);
				}
				SortedList listOfSecretBags = SEPS.GetListOfSecretBags(encodedBags);
				if (listOfSecretBags.Count > 0)
				{
					string text6 = null;
					for (int i = 0; i < listOfSecretBags.Count; i++)
					{
						string text7 = (string)listOfSecretBags.GetKey(i);
						string text8 = (string)listOfSecretBags.GetByIndex(i);
						if (text7.StartsWith("oracle.security.client.connect_string") && string.Equals(text8, ConnectString) && i + 2 < listOfSecretBags.Count)
						{
							text6 = text7.Substring(37, text7.Length - 37);
						}
						if (text6 != null && string.Equals("oracle.security.client.username" + text6, text7))
						{
							Userid = text8;
						}
						if (text6 != null && string.Equals("oracle.security.client.password" + text6, text7))
						{
							PW = text8;
						}
					}
				}
				if (bTraceLevelNetwork)
				{
					if (listOfSecretBags.Count == 0)
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
						{
							"SEPS: No entries found"
						});
					}
					if (string.IsNullOrEmpty(Userid))
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
						{
							"SEPS userid is null."
						});
					}
					if (string.IsNullOrEmpty(PW))
					{
						Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Sqlnet, new string[]
						{
							"SEPS password is null."
						});
					}
				}
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
		private static SortedList GetListOfSecretBags(byte[] EncodedBags)
		{
			SortedList sortedList = new SortedList();
			bool flag = false;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			IntPtr ptr = IntPtr.Zero;
			IntPtr zero4 = IntPtr.Zero;
			SEPS.CRYPT_SEQUENCE_OF_ANY crypt_SEQUENCE_OF_ANY = new SEPS.CRYPT_SEQUENCE_OF_ANY();
			new SEPS.CRYPT_SEQUENCE_OF_ANY();
			SEPS.CRYPT_CONTENT_INFO_SEQUENCE_OF_ANY crypt_CONTENT_INFO_SEQUENCE_OF_ANY = new SEPS.CRYPT_CONTENT_INFO_SEQUENCE_OF_ANY();
			SEPS.CRYPTOAPI_BLOB cryptoapi_BLOB = new SEPS.CRYPTOAPI_BLOB();
			int num = 0;
			try
			{
				if (EncodedBags[1] == 130)
				{
					int num2 = (int)EncodedBags[2] * 256 + (int)EncodedBags[3];
					if (EncodedBags.Length < num2 + 4)
					{
						int num3 = num2 + 4 - EncodedBags.Length;
						byte[] array = new byte[EncodedBags.Length + num3];
						EncodedBags.CopyTo(array, 0);
						flag = SEPS.AllocAndDecode(34U, array, out zero);
					}
					else
					{
						flag = SEPS.AllocAndDecode(34U, EncodedBags, out zero);
					}
				}
				if (!flag)
				{
					throw new ApplicationException();
				}
				Marshal.PtrToStructure(zero, crypt_SEQUENCE_OF_ANY);
				int num4 = 0;
				while ((long)num4 < (long)((ulong)crypt_SEQUENCE_OF_ANY.cValue))
				{
					ptr = (IntPtr)(crypt_SEQUENCE_OF_ANY.rgValue.ToInt64() + (long)(num4 * Marshal.SizeOf(cryptoapi_BLOB)));
					Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
					if (!SEPS.AllocAndDecode(34U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero2))
					{
						throw new ApplicationException();
					}
					Marshal.PtrToStructure(zero2, cryptoapi_BLOB);
					Marshal.PtrToStructure(cryptoapi_BLOB.pData, cryptoapi_BLOB);
					byte[] array2 = new byte[cryptoapi_BLOB.cbData];
					Marshal.Copy(cryptoapi_BLOB.pData, array2, 0, array2.Length);
					if (SEPS.DecodeOID(array2) == "1.2.840.113549.1.12.10.1.5")
					{
						Marshal.PtrToStructure(zero2, cryptoapi_BLOB);
						ptr = (IntPtr)(cryptoapi_BLOB.pData.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
						Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
						byte[] array3 = new byte[cryptoapi_BLOB.cbData];
						Marshal.Copy(cryptoapi_BLOB.pData, array3, 0, array3.Length);
						byte[] pbByteEncoded = SEPS.ASNDecodeData(array3);
						if (!SEPS.AllocAndDecode(33U, pbByteEncoded, out zero4))
						{
							throw new ApplicationException();
						}
						Marshal.PtrToStructure(zero4, crypt_CONTENT_INFO_SEQUENCE_OF_ANY);
						if (Marshal.PtrToStringAnsi(crypt_CONTENT_INFO_SEQUENCE_OF_ANY.pszObjId) == "1.2.840.113549.1.16.12.12")
						{
							num++;
							if (!SEPS.AllocAndDecode(34U, crypt_CONTENT_INFO_SEQUENCE_OF_ANY.rgValue, crypt_CONTENT_INFO_SEQUENCE_OF_ANY.cValue, out zero3))
							{
								throw new ApplicationException();
							}
							Marshal.PtrToStructure(zero3, cryptoapi_BLOB);
							Marshal.PtrToStructure(cryptoapi_BLOB.pData, cryptoapi_BLOB);
							array3 = new byte[cryptoapi_BLOB.cbData];
							Marshal.Copy(cryptoapi_BLOB.pData, array3, 0, array3.Length);
							string key = SEPS.ASNDecodeUTF8String(array3);
							Marshal.PtrToStructure(zero3, cryptoapi_BLOB);
							ptr = (IntPtr)(cryptoapi_BLOB.pData.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
							Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
							array3 = new byte[cryptoapi_BLOB.cbData];
							Marshal.Copy(cryptoapi_BLOB.pData, array3, 0, array3.Length);
							string value = SEPS.ASNDecodeUTF8String(array3);
							sortedList.Add(key, value);
							if (zero3 != IntPtr.Zero)
							{
								Marshal.FreeHGlobal(zero3);
							}
							zero3 = IntPtr.Zero;
						}
						if (zero4 != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(zero4);
						}
						zero4 = IntPtr.Zero;
					}
					if (zero2 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(zero2);
					}
					zero2 = IntPtr.Zero;
					num4++;
				}
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(zero);
				}
			}
			catch (Exception arg)
			{
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Error, new string[]
					{
						"SEPS: GetListOfSecretBags failure: " + arg
					});
				}
			}
			return sortedList;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0008C834 File Offset: 0x0008AA34
		private static bool ExtractDataAndKeyParametersFromPFX(byte[] pbPKCS12, uint dwPKCS12, out byte[] EncryptedData, out byte[] Salt, out int dwIterationCount, out SEPS.PbeAlgorithmType AlgType)
		{
			bool result = false;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			IntPtr zero4 = IntPtr.Zero;
			IntPtr zero5 = IntPtr.Zero;
			IntPtr zero6 = IntPtr.Zero;
			IntPtr ptr = IntPtr.Zero;
			SEPS.CRYPT_SEQUENCE_OF_ANY crypt_SEQUENCE_OF_ANY = new SEPS.CRYPT_SEQUENCE_OF_ANY();
			SEPS.CRYPT_SEQUENCE_OF_ANY crypt_SEQUENCE_OF_ANY2 = new SEPS.CRYPT_SEQUENCE_OF_ANY();
			SEPS.CRYPTOAPI_BLOB cryptoapi_BLOB = new SEPS.CRYPTOAPI_BLOB();
			SEPS.CRYPT_CONTENT_INFO crypt_CONTENT_INFO = new SEPS.CRYPT_CONTENT_INFO();
			new SEPS.CRYPT_CONTENT_INFO_SEQUENCE_OF_ANY();
			SEPS.CRYPTOAPI_BLOB cryptoapi_BLOB2 = new SEPS.CRYPTOAPI_BLOB();
			EncryptedData = null;
			Salt = null;
			dwIterationCount = 0;
			AlgType = SEPS.PbeAlgorithmType.pbeSHA_3DES_CBC;
			try
			{
				if (!SEPS.AllocAndDecode(34U, pbPKCS12, out zero))
				{
					throw new ApplicationException();
				}
				Marshal.PtrToStructure(zero, crypt_SEQUENCE_OF_ANY);
				if (crypt_SEQUENCE_OF_ANY.cValue < 2U)
				{
					throw new ApplicationException();
				}
				ptr = (IntPtr)(crypt_SEQUENCE_OF_ANY.rgValue.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
				Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
				if (!SEPS.AllocAndDecode(33U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero4))
				{
					throw new ApplicationException();
				}
				Marshal.FreeHGlobal(zero);
				zero = IntPtr.Zero;
				Marshal.PtrToStructure(zero4, crypt_CONTENT_INFO);
				if (Marshal.PtrToStringAnsi(crypt_CONTENT_INFO.pszObjId) != "1.2.840.113549.1.7.1")
				{
					throw new ApplicationException();
				}
				if (!SEPS.AllocAndDecode(25U, crypt_CONTENT_INFO.Content.pData, crypt_CONTENT_INFO.Content.cbData, out zero6))
				{
					throw new ApplicationException();
				}
				Marshal.FreeHGlobal(zero4);
				zero4 = IntPtr.Zero;
				Marshal.PtrToStructure(zero6, cryptoapi_BLOB2);
				if (!SEPS.AllocAndDecode(34U, cryptoapi_BLOB2.pData, cryptoapi_BLOB2.cbData, out zero))
				{
					throw new ApplicationException();
				}
				Marshal.FreeHGlobal(zero6);
				zero6 = IntPtr.Zero;
				Marshal.PtrToStructure(zero, crypt_SEQUENCE_OF_ANY);
				ptr = crypt_SEQUENCE_OF_ANY.rgValue;
				bool flag = false;
				int num = 0;
				while ((long)num < (long)((ulong)crypt_SEQUENCE_OF_ANY.cValue))
				{
					Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
					if (!SEPS.AllocAndDecode(33U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero4))
					{
						throw new ApplicationException();
					}
					Marshal.FreeHGlobal(zero);
					zero = IntPtr.Zero;
					Marshal.PtrToStructure(zero4, crypt_CONTENT_INFO);
					if (Marshal.PtrToStringAnsi(crypt_CONTENT_INFO.pszObjId) == "1.2.840.113549.1.7.6")
					{
						flag = true;
						break;
					}
					ptr = (IntPtr)(ptr.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
					num++;
				}
				if (!flag)
				{
					throw new ApplicationException();
				}
				if (!SEPS.AllocAndDecode(34U, crypt_CONTENT_INFO.Content.pData, crypt_CONTENT_INFO.Content.cbData, out zero))
				{
					throw new ApplicationException();
				}
				Marshal.FreeHGlobal(zero4);
				zero4 = IntPtr.Zero;
				Marshal.PtrToStructure(zero, crypt_SEQUENCE_OF_ANY);
				ptr = (IntPtr)(crypt_SEQUENCE_OF_ANY.rgValue.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
				Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
				if (!SEPS.AllocAndDecode(34U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero2))
				{
					throw new ApplicationException();
				}
				Marshal.FreeHGlobal(zero);
				zero = IntPtr.Zero;
				Marshal.PtrToStructure(zero2, crypt_SEQUENCE_OF_ANY);
				Marshal.PtrToStructure(crypt_SEQUENCE_OF_ANY.rgValue, cryptoapi_BLOB);
				byte[] array = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pData, array, 0, array.Length);
				if (SEPS.DecodeOID(array) != "1.2.840.113549.1.7.1")
				{
					throw new ApplicationException();
				}
				ptr = (IntPtr)(crypt_SEQUENCE_OF_ANY.rgValue.ToInt64() + (long)(2 * Marshal.SizeOf(cryptoapi_BLOB)));
				Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
				byte[] array2 = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pData, array2, 0, array2.Length);
				EncryptedData = SEPS.ASNDecodeData(array2);
				ptr = (IntPtr)(crypt_SEQUENCE_OF_ANY.rgValue.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
				Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
				if (!SEPS.AllocAndDecode(34U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero))
				{
					throw new ApplicationException();
				}
				Marshal.PtrToStructure(zero, cryptoapi_BLOB);
				Marshal.PtrToStructure(cryptoapi_BLOB.pData, cryptoapi_BLOB);
				array = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pData, array, 0, array.Length);
				string a = SEPS.DecodeOID(array);
				if (a != "1.2.840.113549.1.12.1.3" && a != "1.2.840.113549.1.12.1.6")
				{
					throw new ApplicationException();
				}
				if (a == "1.2.840.113549.1.12.1.3")
				{
					AlgType = SEPS.PbeAlgorithmType.pbeSHA_3DES_CBC;
				}
				else if (a == "1.2.840.113549.1.12.1.6")
				{
					AlgType = SEPS.PbeAlgorithmType.pbeSHA_RC2_40_CBC;
				}
				Marshal.PtrToStructure(zero, cryptoapi_BLOB);
				ptr = (IntPtr)(cryptoapi_BLOB.pData.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
				Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
				if (!SEPS.AllocAndDecode(34U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero3))
				{
					throw new ApplicationException();
				}
				Marshal.PtrToStructure(zero3, crypt_SEQUENCE_OF_ANY2);
				Marshal.PtrToStructure(crypt_SEQUENCE_OF_ANY2.rgValue, cryptoapi_BLOB);
				if (!SEPS.AllocAndDecode(25U, cryptoapi_BLOB.pData, cryptoapi_BLOB.cbData, out zero6))
				{
					throw new ApplicationException();
				}
				Marshal.PtrToStructure(zero6, cryptoapi_BLOB);
				Salt = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pData, Salt, 0, Salt.Length);
				Marshal.PtrToStructure(zero3, crypt_SEQUENCE_OF_ANY2);
				ptr = (IntPtr)(crypt_SEQUENCE_OF_ANY2.rgValue.ToInt64() + (long)Marshal.SizeOf(cryptoapi_BLOB));
				Marshal.PtrToStructure(ptr, cryptoapi_BLOB);
				array2 = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pData, array2, 0, array2.Length);
				byte[] pbData = SEPS.ASNDecodeInteger(array2);
				dwIterationCount = SEPS.CalculateInteger(pbData);
				result = true;
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelNetwork)
				{
					Trace.Write(OracleTraceLevel.Network, OracleTraceTag.Error, new string[]
					{
						"SEPS: ExtractDataAndKeyParametersFromPFX failure." + ex
					});
				}
				throw new NetworkException(12578, ex);
			}
			return result;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0008CDE0 File Offset: 0x0008AFE0
		public static void FillBuffer(byte[] From, byte[] To)
		{
			int num = 0;
			while (num + From.Length < To.Length)
			{
				From.CopyTo(To, num);
				num += From.Length;
			}
			Array.Copy(From, 0, To, num, To.Length - num);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0008CE18 File Offset: 0x0008B018
		public static ICryptoTransform GeneratePbeSHA1Decryptor(string password, byte[] salt, int sha1iterations, SEPS.PbeAlgorithmType AlgType)
		{
			byte[] array = new byte[64];
			byte[] array2 = new byte[64];
			byte[] array3 = new byte[64];
			int num = 24;
			if (AlgType == SEPS.PbeAlgorithmType.pbeSHA_3DES_CBC)
			{
				num = 24;
			}
			else if (AlgType == SEPS.PbeAlgorithmType.pbeSHA_RC2_40_CBC)
			{
				num = 5;
			}
			byte[] array4 = new byte[num];
			byte[] array5 = new byte[8];
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)array.Length))
			{
				array[(int)((UIntPtr)num2)] = 1;
				num2 += 1U;
			}
			num2 = 0U;
			while ((ulong)num2 < (ulong)((long)array2.Length))
			{
				array2[(int)((UIntPtr)num2)] = 2;
				num2 += 1U;
			}
			byte[] array6 = new byte[Encoding.Unicode.GetBytes(password).Length + 2];
			Encoding.Unicode.GetBytes(password).CopyTo(array6, 0);
			for (int i = 0; i < array6.Length; i += 2)
			{
				byte b = array6[i];
				array6[i] = array6[i + 1];
				array6[i + 1] = b;
			}
			uint num3 = (uint)(((long)array6.Length + 64L - 1L) / 64L * 64L);
			uint num4 = (uint)(((long)salt.Length + 64L - 1L) / 64L * 64L);
			byte[] array7 = new byte[num3];
			byte[] array8 = new byte[num4];
			SEPS.FillBuffer(array6, array7);
			SEPS.FillBuffer(salt, array8);
			SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array9 = new byte[64U + num3 + num4];
			array.CopyTo(array9, 0);
			array8.CopyTo(array9, array.Length);
			array7.CopyTo(array9, array.Length + array8.Length);
			byte[] array10 = sha1CryptoServiceProvider.ComputeHash(array9);
			sha1CryptoServiceProvider.Dispose();
			for (int j = 0; j < sha1iterations - 1; j++)
			{
				sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
				array10 = sha1CryptoServiceProvider.ComputeHash(array10);
				sha1CryptoServiceProvider.Dispose();
			}
			if (AlgType == SEPS.PbeAlgorithmType.pbeSHA_RC2_40_CBC)
			{
				Array.Copy(array10, 0, array4, 0, 5);
			}
			else if (AlgType == SEPS.PbeAlgorithmType.pbeSHA_3DES_CBC)
			{
				Array.Copy(array10, 0, array4, 0, 20);
				SEPS.FillBuffer(array10, array3);
				ushort num5 = 1;
				ushort num6 = 1;
				num2 = 64U;
				do
				{
					num2 -= 1U;
					num5 += (ushort)array7[(int)((UIntPtr)num2)];
					num5 += (ushort)array3[(int)((UIntPtr)num2)];
					array7[(int)((UIntPtr)num2)] = (byte)num5;
					num5 = (ushort)(num5 >> 8);
					num6 += (ushort)array8[(int)((UIntPtr)num2)];
					num6 += (ushort)array3[(int)((UIntPtr)num2)];
					array8[(int)((UIntPtr)num2)] = (byte)num6;
					num6 = (ushort)(num6 >> 8);
				}
				while (num2 > 0U);
				sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
				array9 = new byte[64U + num3 + num4];
				array.CopyTo(array9, 0);
				array8.CopyTo(array9, array.Length);
				array7.CopyTo(array9, array.Length + array8.Length);
				array10 = sha1CryptoServiceProvider.ComputeHash(array9);
				sha1CryptoServiceProvider.Dispose();
				for (int k = 0; k < sha1iterations - 1; k++)
				{
					sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
					array10 = sha1CryptoServiceProvider.ComputeHash(array10);
					sha1CryptoServiceProvider.Dispose();
				}
				Array.Copy(array10, 0, array4, 20, 4);
			}
			sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			SEPS.FillBuffer(array6, array7);
			SEPS.FillBuffer(salt, array8);
			array9 = new byte[64U + num3 + num4];
			array2.CopyTo(array9, 0);
			array8.CopyTo(array9, array.Length);
			array7.CopyTo(array9, array.Length + array8.Length);
			array10 = sha1CryptoServiceProvider.ComputeHash(array9);
			sha1CryptoServiceProvider.Dispose();
			for (int l = 0; l < sha1iterations - 1; l++)
			{
				sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
				array10 = sha1CryptoServiceProvider.ComputeHash(array10);
				sha1CryptoServiceProvider.Dispose();
			}
			Array.Copy(array10, 0, array5, 0, 8);
			if (AlgType == SEPS.PbeAlgorithmType.pbeSHA_3DES_CBC)
			{
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				return tripleDESCryptoServiceProvider.CreateDecryptor(array4, array5);
			}
			if (AlgType == SEPS.PbeAlgorithmType.pbeSHA_RC2_40_CBC)
			{
				RC2CryptoServiceProvider rc2CryptoServiceProvider = new RC2CryptoServiceProvider();
				return rc2CryptoServiceProvider.CreateDecryptor(array4, array5);
			}
			return null;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0008D17C File Offset: 0x0008B37C
		private static bool AllocAndDecode(uint lpszStructType, byte[] pbByteEncoded, out IntPtr pvStructInfo)
		{
			uint cb = 0U;
			pvStructInfo = IntPtr.Zero;
			IntPtr intPtr = Marshal.AllocHGlobal(pbByteEncoded.Length);
			uint num = (uint)pbByteEncoded.Length;
			Marshal.Copy(pbByteEncoded, 0, intPtr, (int)num);
			bool flag = SEPS.CryptDecodeObject(65537U, lpszStructType, intPtr, num, 0U, pvStructInfo, ref cb);
			if (flag)
			{
				pvStructInfo = Marshal.AllocHGlobal((int)cb);
				if (pvStructInfo != IntPtr.Zero)
				{
					flag = SEPS.CryptDecodeObject(65537U, lpszStructType, intPtr, num, 0U, pvStructInfo, ref cb);
				}
			}
			Marshal.GetLastWin32Error();
			return flag;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0008D204 File Offset: 0x0008B404
		private static bool AllocAndDecode(string lpszStructType, byte[] pbByteEncoded, out IntPtr pvStructInfo)
		{
			uint cb = 0U;
			pvStructInfo = IntPtr.Zero;
			IntPtr intPtr = Marshal.AllocHGlobal(pbByteEncoded.Length);
			uint num = (uint)pbByteEncoded.Length;
			Marshal.Copy(pbByteEncoded, 0, intPtr, (int)num);
			bool flag = SEPS.CryptDecodeObject(65537U, lpszStructType, intPtr, num, 0U, pvStructInfo, ref cb);
			if (flag)
			{
				pvStructInfo = Marshal.AllocHGlobal((int)cb);
				if (pvStructInfo != IntPtr.Zero)
				{
					flag = SEPS.CryptDecodeObject(65537U, lpszStructType, intPtr, num, 0U, pvStructInfo, ref cb);
				}
			}
			return flag;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0008D288 File Offset: 0x0008B488
		private static bool AllocAndDecode(uint lpszStructType, IntPtr pbEncoded, uint cbEncoded, out IntPtr pvStructInfo)
		{
			uint cb = 0U;
			pvStructInfo = IntPtr.Zero;
			bool flag = SEPS.CryptDecodeObject(65537U, lpszStructType, pbEncoded, cbEncoded, 0U, pvStructInfo, ref cb);
			if (flag)
			{
				pvStructInfo = Marshal.AllocHGlobal((int)cb);
				if (pvStructInfo != IntPtr.Zero)
				{
					flag = SEPS.CryptDecodeObject(65537U, lpszStructType, pbEncoded, cbEncoded, 0U, pvStructInfo, ref cb);
				}
			}
			return flag;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
		private static bool AllocAndDecode(string lpszStructType, IntPtr pbEncoded, uint cbEncoded, out IntPtr pvStructInfo)
		{
			uint cb = 0U;
			pvStructInfo = IntPtr.Zero;
			bool flag = SEPS.CryptDecodeObject(65537U, lpszStructType, pbEncoded, cbEncoded, 0U, pvStructInfo, ref cb);
			if (flag)
			{
				pvStructInfo = Marshal.AllocHGlobal((int)cb);
				if (pvStructInfo != IntPtr.Zero)
				{
					flag = SEPS.CryptDecodeObject(65537U, lpszStructType, pbEncoded, cbEncoded, 0U, pvStructInfo, ref cb);
				}
			}
			return flag;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0008D360 File Offset: 0x0008B560
		public static string DecodeOID(byte[] pbEncodedOID)
		{
			int num = 2;
			string str = "";
			int i;
			for (i = 3; i < pbEncodedOID.Length; i++)
			{
				if ((pbEncodedOID[i] & 128) == 0)
				{
					num++;
				}
			}
			int[] array = new int[num];
			array[0] = Convert.ToInt32(pbEncodedOID[2]) / 40;
			array[1] = Convert.ToInt32(pbEncodedOID[2]) % 40;
			i = 2;
			int num2 = 3;
			while (i < array.Length)
			{
				bool flag = true;
				int num3 = 0;
				do
				{
					int num4 = Convert.ToInt32(pbEncodedOID[num2++]);
					if ((num4 & 128) == 128)
					{
						num3 = (num3 + (num4 & 127)) * 128;
					}
					else
					{
						num3 += num4;
						flag = false;
					}
				}
				while (flag);
				array[i] = num3;
				i++;
			}
			for (i = 0; i < array.Length - 1; i++)
			{
				str = str + Convert.ToString(array[i], 10) + ".";
			}
			return str + Convert.ToString(array[array.Length - 1], 10);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0008D458 File Offset: 0x0008B658
		public static byte[] ASNDecodeData(byte[] pbData)
		{
			uint num = 0U;
			if (pbData[0] != 128 && pbData[0] != 160)
			{
				return null;
			}
			int num3;
			if ((pbData[1] & 128) > 0)
			{
				uint num2 = (uint)(pbData[1] & 127);
				if (num2 > 4U)
				{
					return null;
				}
				num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					num |= (uint)((uint)pbData[num3 + 2] << (int)((num2 - 1U - (uint)num3) * 8U));
					num3++;
				}
			}
			else
			{
				num = (uint)pbData[1];
				num3 = 0;
			}
			byte[] array;
			if (pbData[num3 + 2] == 0)
			{
				num -= 1U;
				array = new byte[num];
				Array.Copy(pbData, num3 + 3, array, 0, (int)num);
			}
			else
			{
				array = new byte[num];
				Array.Copy(pbData, num3 + 2, array, 0, (int)num);
			}
			return array;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0008D4F8 File Offset: 0x0008B6F8
		public static string ASNDecodeUTF8String(byte[] pbData)
		{
			uint num = 0U;
			if (pbData[0] != 12)
			{
				return null;
			}
			int i;
			if ((pbData[1] & 128) > 0)
			{
				uint num2 = (uint)(pbData[1] & 127);
				if (num2 > 4U)
				{
					return null;
				}
				i = 0;
				while ((long)i < (long)((ulong)num2))
				{
					num |= (uint)((uint)pbData[i + 2] << (int)((num2 - 1U - (uint)i) * 8U));
					i++;
				}
			}
			else
			{
				num = (uint)pbData[1];
				i = 0;
			}
			byte[] array;
			if (pbData[i + 2] == 0)
			{
				num -= 1U;
				array = new byte[num];
				Array.Copy(pbData, i + 3, array, 0, (int)num);
			}
			else
			{
				array = new byte[num];
				Array.Copy(pbData, i + 2, array, 0, (int)num);
			}
			string text = "";
			for (i = 0; i < array.Length; i++)
			{
				text += Convert.ToChar(array[i]).ToString();
			}
			return text.ToString();
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0008D5C0 File Offset: 0x0008B7C0
		public static byte[] ASNDecodeInteger(byte[] pbData)
		{
			uint num = 0U;
			if (pbData[0] != 2)
			{
				return null;
			}
			int num3;
			if ((pbData[1] & 128) > 0)
			{
				uint num2 = (uint)(pbData[1] & 127);
				if (num2 > 4U)
				{
					return null;
				}
				num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					num |= (uint)((uint)pbData[num3 + 2] << (int)((num2 - 1U - (uint)num3) * 8U));
					num3++;
				}
			}
			else
			{
				num = (uint)pbData[1];
				num3 = 0;
			}
			byte[] array;
			if (pbData[num3 + 2] == 0)
			{
				num -= 1U;
				array = new byte[num];
				Array.Copy(pbData, num3 + 3, array, 0, (int)num);
			}
			else
			{
				array = new byte[num];
				Array.Copy(pbData, num3 + 2, array, 0, (int)num);
			}
			return array;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0008D654 File Offset: 0x0008B854
		public static int CalculateInteger(byte[] pbData)
		{
			int num = 0;
			for (int i = 0; i < pbData.Length; i++)
			{
				num <<= 8;
				num |= (int)pbData[i];
			}
			return num;
		}

		// Token: 0x04000DAE RID: 3502
		private const uint X509_ASN_ENCODING = 1U;

		// Token: 0x04000DAF RID: 3503
		private const uint PKCS_7_ASN_ENCODING = 65536U;

		// Token: 0x04000DB0 RID: 3504
		private const uint X509_SEQUENCE_OF_ANY = 34U;

		// Token: 0x04000DB1 RID: 3505
		private const uint PKCS_CONTENT_INFO = 33U;

		// Token: 0x04000DB2 RID: 3506
		private const uint PKCS_CONTENT_INFO_SEQUENCE_OF_ANY = 23U;

		// Token: 0x04000DB3 RID: 3507
		private const uint X509_OCTET_STRING = 25U;

		// Token: 0x04000DB4 RID: 3508
		private const uint X509_OBJECT_IDENTIFIER = 73U;

		// Token: 0x04000DB5 RID: 3509
		private const uint ENCODING = 65537U;

		// Token: 0x04000DB6 RID: 3510
		private const uint HASH_BLOCK_SIZE = 64U;

		// Token: 0x04000DB7 RID: 3511
		private const string pkcs_12_secretBag = "1.2.840.113549.1.12.10.1.5";

		// Token: 0x04000DB8 RID: 3512
		private const string oracleOID = "1.2.840.113549.1.16.12.12";

		// Token: 0x04000DB9 RID: 3513
		private const string pbeWithSHAAnd3_KeyTripleDES_CBC = "1.2.840.113549.1.12.1.3";

		// Token: 0x04000DBA RID: 3514
		private const string pbeWithSHAAnd40BitRC2_CBC = "1.2.840.113549.1.12.1.6";

		// Token: 0x04000DBB RID: 3515
		private const string szOID_PKCS_7_DATA = "1.2.840.113549.1.7.1";

		// Token: 0x04000DBC RID: 3516
		private const string szOID_PKCS_7_ENCRYPTED = "1.2.840.113549.1.7.6";

		// Token: 0x04000DBD RID: 3517
		private const string WALLETFILENAME = "cwallet.sso";

		// Token: 0x02000140 RID: 320
		[StructLayout(LayoutKind.Sequential)]
		private class CRYPT_SEQUENCE_OF_ANY
		{
			// Token: 0x04000DBE RID: 3518
			public uint cValue;

			// Token: 0x04000DBF RID: 3519
			public IntPtr rgValue;
		}

		// Token: 0x02000141 RID: 321
		[StructLayout(LayoutKind.Sequential)]
		private class CRYPTOAPI_BLOB
		{
			// Token: 0x04000DC0 RID: 3520
			public uint cbData;

			// Token: 0x04000DC1 RID: 3521
			public IntPtr pData;
		}

		// Token: 0x02000142 RID: 322
		[StructLayout(LayoutKind.Sequential)]
		private class CRYPT_CONTENT_INFO
		{
			// Token: 0x04000DC2 RID: 3522
			public IntPtr pszObjId;

			// Token: 0x04000DC3 RID: 3523
			public SEPS.CRYPTOAPI_BLOB Content;
		}

		// Token: 0x02000143 RID: 323
		[StructLayout(LayoutKind.Sequential)]
		private class CRYPT_CONTENT_INFO_SEQUENCE_OF_ANY
		{
			// Token: 0x04000DC4 RID: 3524
			public IntPtr pszObjId;

			// Token: 0x04000DC5 RID: 3525
			public uint cValue;

			// Token: 0x04000DC6 RID: 3526
			public IntPtr rgValue;
		}

		// Token: 0x02000144 RID: 324
		internal enum PbeAlgorithmType
		{
			// Token: 0x04000DC8 RID: 3528
			pbeSHA_3DES_CBC,
			// Token: 0x04000DC9 RID: 3529
			pbeSHA_RC2_40_CBC
		}
	}
}

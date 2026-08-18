using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Infralution.Licensing
{
	// Token: 0x02000019 RID: 25
	public class EncryptedLicenseProvider : LicenseProvider
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003974 File Offset: 0x00001B74
		public static void SetParameters(string licenseParameters)
		{
			XmlReader xmlReader = new XmlTextReader(licenseParameters, XmlNodeType.Element, null);
			while (xmlReader.Read())
			{
				bool flag = xmlReader.IsStartElement();
				if (flag)
				{
					bool flag2 = xmlReader.LocalName == "RSAKeyValue";
					if (flag2)
					{
						EncryptedLicenseProvider._rsaParameters = xmlReader.ReadOuterXml();
					}
					bool flag3 = xmlReader.LocalName == "DesignSignature";
					if (flag3)
					{
						string s = xmlReader.ReadElementString();
						EncryptedLicenseProvider._designSignature = Convert.FromBase64String(s);
					}
					bool flag4 = xmlReader.LocalName == "RuntimeSignature";
					if (flag4)
					{
						string s2 = xmlReader.ReadElementString();
						EncryptedLicenseProvider._runtimeSignature = Convert.FromBase64String(s2);
					}
				}
			}
			xmlReader.Close();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003A2C File Offset: 0x00001C2C
		public string GenerateLicenseParameters(string password)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(1024);
			string data = rsacryptoServiceProvider.ToXmlString(false);
			byte[] encryptionKey = EncryptedLicenseProvider.GetEncryptionKey(password);
			byte[] inArray = rsacryptoServiceProvider.SignData(encryptionKey, new SHA1CryptoServiceProvider());
			byte[] sourceArray = new DESCryptoServiceProvider
			{
				Key = EncryptedLicenseProvider._desKey,
				IV = encryptionKey
			}.CreateEncryptor().TransformFinalBlock(encryptionKey, 0, encryptionKey.Length);
			byte[] array = new byte[EncryptedLicenseProvider.ArraySize(8)];
			Array.Copy(sourceArray, 0, array, 0, 7);
			byte[] inArray2 = rsacryptoServiceProvider.SignData(array, new SHA1CryptoServiceProvider());
			MemoryStream memoryStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.ASCII);
			xmlTextWriter.WriteStartElement("LicenseParameters");
			xmlTextWriter.WriteRaw(data);
			xmlTextWriter.WriteElementString("DesignSignature", Convert.ToBase64String(inArray));
			xmlTextWriter.WriteElementString("RuntimeSignature", Convert.ToBase64String(inArray2));
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Close();
			string @string = Encoding.ASCII.GetString(memoryStream.ToArray());
			memoryStream.Close();
			return @string;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B40 File Offset: 0x00001D40
		public virtual string GenerateKey(string password, string productInfo, ushort serialNo)
		{
			byte[] array = new byte[]
			{
				62,
				126,
				142,
				55,
				68,
				165,
				193,
				63
			};
			byte[] encryptionKey = EncryptedLicenseProvider.GetEncryptionKey(password);
			byte[] bytes = Encoding.UTF8.GetBytes(productInfo);
			byte[] bytes2 = BitConverter.GetBytes(serialNo);
			byte[] array2 = new byte[EncryptedLicenseProvider.ArraySize(bytes.Length + bytes2.Length)];
			byte[] publicKeyToken = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();
			byte[] array3 = new byte[]
			{
				62,
				126,
				142,
				55,
				68,
				165,
				193,
				63
			};
			bytes2.CopyTo(array2, 0);
			bytes.CopyTo(array2, 2);
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			descryptoServiceProvider.Key = EncryptedLicenseProvider._desKey;
			descryptoServiceProvider.IV = encryptionKey;
			byte[] array4 = descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(array2, 0, array2.Length);
			byte[] array5 = new byte[EncryptedLicenseProvider.ArraySize(7 + array4.Length)];
			encryptionKey.CopyTo(array5, 0);
			array4.CopyTo(array5, 7);
			descryptoServiceProvider.IV = EncryptedLicenseProvider._desIV;
			byte[] data = descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(array5, 0, array5.Length);
			return EncryptedLicenseProvider.ToHex(data);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C50 File Offset: 0x00001E50
		public virtual EncryptedLicense InstallLicense(Type type, string licenseKey)
		{
			EncryptedLicense encryptedLicense = this.LoadLicense(LicenseManager.CurrentContext, type, licenseKey);
			bool flag = encryptedLicense != null;
			if (flag)
			{
				string licenseFilePath = this.GetLicenseFilePath(LicenseManager.CurrentContext, type);
				this.WriteKeyToFile(licenseFilePath, licenseKey);
			}
			return encryptedLicense;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C94 File Offset: 0x00001E94
		public virtual EncryptedLicense ValidateLicenseKey(string licenseKey)
		{
			return this.LoadLicense(LicenseManager.CurrentContext, null, licenseKey);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			string licenseKey = this.GetLicenseKey(context, type);
			License license = this.LoadLicense(context, type, licenseKey);
			bool flag = license == null && allowExceptions;
			if (!flag)
			{
				return license;
			}
			bool flag2 = instance == null;
			if (flag2)
			{
				throw new LicenseException(type);
			}
			throw new LicenseException(type, instance);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003D00 File Offset: 0x00001F00
		internal static EncryptedLicense SystemLicense
		{
			get
			{
				bool flag = EncryptedLicenseProvider._systemLicense == null;
				if (flag)
				{
					EncryptedLicenseProvider.SetParameters("<LicenseParameters><RSAKeyValue><Modulus>u0Uz9OHGLVyLPZul6xeJDmFonpRo7dxI+26vxpm5vU0XHYp/7TQzqOcJVnSW1U6fIDHYynKIwfV/AzwVRV6K1dJB+Ag+bfDExQgJSniEVJq88wXz0iyyhklOx69F37Fglvz4m5p8xvG95KPrKkNHju3dp7gKr/XdfHeqO5MipEE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue><DesignSignature>Rafnrs1FVMy497Y2Bq38LFw3t2vvR2g6qWhC8BCD5FH1Rs7ArcnuQ093AalWqdhZPPvVvEbVltiFOKM0Ycr58J1uXxAUOXtd54wKE2IdfsfsbLiCXarUteFsKdmRO5dylEupq/oyGKaDHKm6PpDKKMgkofQ4Z1M7kq7pVa0gZUk=</DesignSignature><RuntimeSignature>pn47clfpjjV4wUG5YGHPZHyZFaJwEdHVGX8vh4ifSeHtMFxtLDdZg/YFgNKqRAr337bdFgz6YgWjfpBmP6lGB1ydKcT24aF/6DplaPoJiuovJrkE38iOeVLiP4vBd/7tuYc7KObCdenro/02Ur/4j6UL4UxBQsbjVUjuJM3jd80=</RuntimeSignature></LicenseParameters>");
					EncryptedLicenseProvider encryptedLicenseProvider = new EncryptedLicenseProvider();
					EncryptedLicenseProvider._systemLicense = (encryptedLicenseProvider.GetLicense(LicenseManager.CurrentContext, typeof(EncryptedLicenseProvider), null, false) as EncryptedLicense);
				}
				return EncryptedLicenseProvider._systemLicense;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D58 File Offset: 0x00001F58
		internal EncryptedLicense GetLicense(string licenseKey, string password)
		{
			EncryptedLicense result;
			try
			{
				byte[] array = EncryptedLicenseProvider.FromHex(licenseKey);
				byte[] array2 = new byte[]
				{
					62,
					126,
					142,
					55,
					68,
					165,
					193,
					63
				};
				byte[] publicKeyToken = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();
				DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
				descryptoServiceProvider.Key = EncryptedLicenseProvider._desKey;
				descryptoServiceProvider.IV = EncryptedLicenseProvider._desIV;
				byte[] array3 = descryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
				byte[] array4 = new byte[EncryptedLicenseProvider.ArraySize(8)];
				byte[] array5 = new byte[EncryptedLicenseProvider.ArraySize(array3.Length - 7)];
				Array.Copy(array3, 0, array4, 0, 7);
				Array.Copy(array3, 7, array5, 0, array5.Length);
				byte[] encryptionKey = EncryptedLicenseProvider.GetEncryptionKey(password);
				bool flag = !EncryptedLicenseProvider.ArrayEqual(array4, encryptionKey);
				if (flag)
				{
					result = null;
				}
				else
				{
					descryptoServiceProvider.IV = array4;
					byte[] array6 = descryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array5, 0, array5.Length);
					byte[] array7 = new byte[EncryptedLicenseProvider.ArraySize(array6.Length - 2)];
					Array.Copy(array6, 2, array7, 0, array7.Length);
					ushort serialNo = BitConverter.ToUInt16(array6, 0);
					string @string = Encoding.UTF8.GetString(array7);
					result = new EncryptedLicense(licenseKey, serialNo, @string);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003EA8 File Offset: 0x000020A8
		private static int ArraySize(int length)
		{
			return length;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003EBC File Offset: 0x000020BC
		private static string Strip(string value, string characters)
		{
			bool flag = value == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char value2 in value)
				{
					bool flag2 = characters.IndexOf(value2, 0) < 0;
					if (flag2)
					{
						stringBuilder.Append(value2);
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003F28 File Offset: 0x00002128
		private static string ToHex(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				bool flag = i > 0 && i % 2 == 0;
				if (flag)
				{
					stringBuilder.Append("-");
				}
				stringBuilder.Append(data[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003F94 File Offset: 0x00002194
		private static byte[] FromHex(string hex)
		{
			string text = EncryptedLicenseProvider.Strip(hex, "\t\r\n -");
			bool flag = text == null || text.Length % 2 != 0;
			if (flag)
			{
				throw new FormatException("Invalid hexadecimal string");
			}
			byte[] array = new byte[EncryptedLicenseProvider.ArraySize(text.Length / 2)];
			int i = 0;
			int num = 0;
			while (i < text.Length)
			{
				string s = text.Substring(i, 2);
				array[num] = byte.Parse(s, NumberStyles.HexNumber);
				i += 2;
				num++;
			}
			return array;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004024 File Offset: 0x00002224
		private static bool ArrayEqual(byte[] a1, byte[] a2)
		{
			bool flag = a1 == a2;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = a1 == null || a2 == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = a1.Length != a2.Length;
					if (flag3)
					{
						result = false;
					}
					else
					{
						for (int i = 0; i < a1.Length; i++)
						{
							bool flag4 = a1[i] != a2[i];
							if (flag4)
							{
								return false;
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000409C File Offset: 0x0000229C
		private static byte[] GetEncryptionKey(string password)
		{
			byte[] key = new byte[]
			{
				242,
				161,
				3,
				157,
				99,
				135,
				53,
				94
			};
			byte[] iv = new byte[]
			{
				171,
				184,
				148,
				126,
				29,
				229,
				209,
				51
			};
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			descryptoServiceProvider.Key = key;
			descryptoServiceProvider.IV = iv;
			bool flag = password.Length < 8;
			if (flag)
			{
				password = password.PadRight(8, '*');
			}
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			byte[] sourceArray = descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = new byte[EncryptedLicenseProvider.ArraySize(8)];
			Array.Copy(sourceArray, 0, array, 0, 7);
			return array;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004140 File Offset: 0x00002340
		private EncryptedLicense LoadLicense(LicenseContext context, Type type, string licenseKey)
		{
			bool flag = EncryptedLicenseProvider._rsaParameters == null || EncryptedLicenseProvider._designSignature == null || EncryptedLicenseProvider._runtimeSignature == null;
			if (flag)
			{
				throw new InvalidOperationException("EncryptedLicenseProvider.SetParameters must be called prior to using the EncryptedLicenseProvider");
			}
			bool flag2 = licenseKey == null;
			EncryptedLicense result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				try
				{
					byte[] array = EncryptedLicenseProvider.FromHex(licenseKey);
					DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
					descryptoServiceProvider.Key = EncryptedLicenseProvider._desKey;
					descryptoServiceProvider.IV = EncryptedLicenseProvider._desIV;
					byte[] array2 = descryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
					byte[] array3 = new byte[EncryptedLicenseProvider.ArraySize(8)];
					byte[] array4 = new byte[EncryptedLicenseProvider.ArraySize(array2.Length - 7)];
					Array.Copy(array2, 0, array3, 0, 7);
					Array.Copy(array2, 7, array4, 0, array4.Length);
					RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
					rsacryptoServiceProvider.FromXmlString(EncryptedLicenseProvider._rsaParameters);
					descryptoServiceProvider.IV = array3;
					byte[] array5 = descryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array4, 0, array4.Length);
					byte[] array6 = new byte[EncryptedLicenseProvider.ArraySize(array5.Length - 2)];
					Array.Copy(array5, 2, array6, 0, array6.Length);
					ushort serialNo = BitConverter.ToUInt16(array5, 0);
					string @string = Encoding.UTF8.GetString(array6);
					bool flag3 = context.UsageMode == LicenseUsageMode.Designtime && type != null;
					if (flag3)
					{
						byte[] sourceArray = descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(array3, 0, array3.Length);
						byte[] array7 = new byte[EncryptedLicenseProvider.ArraySize(8)];
						Array.Copy(sourceArray, 0, array7, 0, 7);
						descryptoServiceProvider.IV = array7;
						array4 = descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(array5, 0, array5.Length);
						array2 = new byte[EncryptedLicenseProvider.ArraySize(7 + array4.Length)];
						array7.CopyTo(array2, 0);
						array4.CopyTo(array2, 7);
						descryptoServiceProvider.IV = EncryptedLicenseProvider._desIV;
						array = descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(array2, 0, array2.Length);
						string key = EncryptedLicenseProvider.ToHex(array);
						context.SetSavedLicenseKey(type, key);
					}
					result = new EncryptedLicense(licenseKey, serialNo, @string);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004364 File Offset: 0x00002564
		protected virtual string ReadKeyFromFile(string licenseFile)
		{
			string result = null;
			bool flag = File.Exists(licenseFile);
			if (flag)
			{
				Stream stream = new FileStream(licenseFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				StreamReader streamReader = new StreamReader(stream);
				result = streamReader.ReadLine();
				streamReader.Close();
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000043A8 File Offset: 0x000025A8
		protected virtual void WriteKeyToFile(string licenseFile, string licenseKey)
		{
			Stream stream = new FileStream(licenseFile, FileMode.Create, FileAccess.Write, FileShare.None);
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.WriteLine(licenseKey);
			streamWriter.Close();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000043D8 File Offset: 0x000025D8
		protected virtual string GetLicenseKey(LicenseContext context, Type type)
		{
			string text = null;
			bool flag = context.UsageMode == LicenseUsageMode.Runtime;
			if (flag)
			{
				text = context.GetSavedLicenseKey(type, null);
			}
			bool flag2 = text == null;
			if (flag2)
			{
				text = this.ReadKeyFromFile(this.GetLicenseFilePath(context, type));
			}
			return text;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004420 File Offset: 0x00002620
		protected virtual string GetLicenseDirectory(LicenseContext context, Type type)
		{
			string text = null;
			bool flag = context != null;
			if (flag)
			{
				ITypeResolutionService typeResolutionService = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
				bool flag2 = typeResolutionService != null;
				if (flag2)
				{
					text = typeResolutionService.GetPathOfAssembly(type.Assembly.GetName());
				}
			}
			bool flag3 = text == null;
			if (flag3)
			{
				text = type.Assembly.CodeBase;
				bool flag4 = text.StartsWith("file:///");
				if (flag4)
				{
					text = text.Replace("file:///", "");
				}
				else
				{
					text = type.Module.FullyQualifiedName;
				}
			}
			return Path.GetDirectoryName(text);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000044C4 File Offset: 0x000026C4
		protected virtual string GetLicenseFilePath(LicenseContext context, Type type)
		{
			string licenseDirectory = this.GetLicenseDirectory(context, type);
			return string.Format("{0}\\{1}.lic", licenseDirectory, type.FullName);
		}

		// Token: 0x04000031 RID: 49
		private static string _rsaParameters;

		// Token: 0x04000032 RID: 50
		private static byte[] _designSignature;

		// Token: 0x04000033 RID: 51
		private static byte[] _runtimeSignature;

		// Token: 0x04000034 RID: 52
		private static byte[] _desKey = new byte[]
		{
			146,
			21,
			56,
			161,
			18,
			237,
			179,
			194
		};

		// Token: 0x04000035 RID: 53
		private static byte[] _desIV = new byte[]
		{
			173,
			63,
			198,
			17,
			71,
			144,
			221,
			161
		};

		// Token: 0x04000036 RID: 54
		private const int keyLength = 7;

		// Token: 0x04000037 RID: 55
		private const string _systemParameters = "<LicenseParameters><RSAKeyValue><Modulus>u0Uz9OHGLVyLPZul6xeJDmFonpRo7dxI+26vxpm5vU0XHYp/7TQzqOcJVnSW1U6fIDHYynKIwfV/AzwVRV6K1dJB+Ag+bfDExQgJSniEVJq88wXz0iyyhklOx69F37Fglvz4m5p8xvG95KPrKkNHju3dp7gKr/XdfHeqO5MipEE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue><DesignSignature>Rafnrs1FVMy497Y2Bq38LFw3t2vvR2g6qWhC8BCD5FH1Rs7ArcnuQ093AalWqdhZPPvVvEbVltiFOKM0Ycr58J1uXxAUOXtd54wKE2IdfsfsbLiCXarUteFsKdmRO5dylEupq/oyGKaDHKm6PpDKKMgkofQ4Z1M7kq7pVa0gZUk=</DesignSignature><RuntimeSignature>pn47clfpjjV4wUG5YGHPZHyZFaJwEdHVGX8vh4ifSeHtMFxtLDdZg/YFgNKqRAr337bdFgz6YgWjfpBmP6lGB1ydKcT24aF/6DplaPoJiuovJrkE38iOeVLiP4vBd/7tuYc7KObCdenro/02Ur/4j6UL4UxBQsbjVUjuJM3jd80=</RuntimeSignature></LicenseParameters>";

		// Token: 0x04000038 RID: 56
		private static EncryptedLicense _systemLicense;
	}
}

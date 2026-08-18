using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Win32;

namespace System.Configuration
{
	// Token: 0x02000084 RID: 132
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public sealed class RsaProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x00019EE8 File Offset: 0x000180E8
		public override XmlNode Decrypt(XmlNode encryptedNode)
		{
			XmlDocument xmlDocument = new XmlDocument();
			RSACryptoServiceProvider cryptoServiceProvider = this.GetCryptoServiceProvider(false, true);
			xmlDocument.PreserveWhitespace = true;
			ProtectedConfigurationProvider.LoadXml(xmlDocument, encryptedNode.OuterXml);
			EncryptedXml encryptedXml = new FipsAwareEncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping(this._KeyName, cryptoServiceProvider);
			encryptedXml.DecryptDocument();
			cryptoServiceProvider.Clear();
			return xmlDocument.DocumentElement;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00019F40 File Offset: 0x00018140
		public override XmlNode Encrypt(XmlNode node)
		{
			RSACryptoServiceProvider cryptoServiceProvider = this.GetCryptoServiceProvider(false, false);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			ProtectedConfigurationProvider.LoadXml(xmlDocument, "<foo>" + node.OuterXml + "</foo>");
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			XmlElement documentElement = xmlDocument.DocumentElement;
			byte[] cipherValue;
			EncryptedData encryptedData;
			EncryptedKey encryptedKey;
			using (SymmetricAlgorithm symAlgorithmProvider = this.GetSymAlgorithmProvider())
			{
				cipherValue = encryptedXml.EncryptData(documentElement, symAlgorithmProvider, true);
				encryptedData = new EncryptedData();
				encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
				encryptedData.EncryptionMethod = this.GetSymEncryptionMethod();
				encryptedData.KeyInfo = new KeyInfo();
				encryptedKey = new EncryptedKey();
				encryptedKey.EncryptionMethod = new EncryptionMethod(this.UseOAEP ? "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p" : "http://www.w3.org/2001/04/xmlenc#rsa-1_5");
				encryptedKey.KeyInfo = new KeyInfo();
				encryptedKey.CipherData = new CipherData();
				encryptedKey.CipherData.CipherValue = EncryptedXml.EncryptKey(symAlgorithmProvider.Key, cryptoServiceProvider, this.UseOAEP);
			}
			KeyInfoName keyInfoName = new KeyInfoName();
			keyInfoName.Value = this._KeyName;
			encryptedKey.KeyInfo.AddClause(keyInfoName);
			KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
			encryptedData.KeyInfo.AddClause(clause);
			encryptedData.CipherData = new CipherData();
			encryptedData.CipherData.CipherValue = cipherValue;
			EncryptedXml.ReplaceElement(documentElement, encryptedData, true);
			cryptoServiceProvider.Clear();
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.NodeType == XmlNodeType.Element)
						{
							return xmlNode2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001A158 File Offset: 0x00018358
		public void AddKey(int keySize, bool exportable)
		{
			RSACryptoServiceProvider cryptoServiceProvider = this.GetCryptoServiceProvider(exportable, false);
			cryptoServiceProvider.KeySize = keySize;
			cryptoServiceProvider.PersistKeyInCsp = true;
			cryptoServiceProvider.Clear();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A184 File Offset: 0x00018384
		public void DeleteKey()
		{
			RSACryptoServiceProvider cryptoServiceProvider = this.GetCryptoServiceProvider(false, true);
			cryptoServiceProvider.PersistKeyInCsp = false;
			cryptoServiceProvider.Clear();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001A1A8 File Offset: 0x000183A8
		public void ImportKey(string xmlFileName, bool exportable)
		{
			RSACryptoServiceProvider cryptoServiceProvider = this.GetCryptoServiceProvider(exportable, false);
			cryptoServiceProvider.FromXmlString(File.ReadAllText(xmlFileName));
			cryptoServiceProvider.PersistKeyInCsp = true;
			cryptoServiceProvider.Clear();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001A1D8 File Offset: 0x000183D8
		public void ExportKey(string xmlFileName, bool includePrivateParameters)
		{
			RSACryptoServiceProvider cryptoServiceProvider = this.GetCryptoServiceProvider(false, false);
			string contents = cryptoServiceProvider.ToXmlString(includePrivateParameters);
			File.WriteAllText(xmlFileName, contents);
			cryptoServiceProvider.Clear();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001A203 File Offset: 0x00018403
		public string KeyContainerName
		{
			get
			{
				return this._KeyContainerName;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001A20B File Offset: 0x0001840B
		public string CspProviderName
		{
			get
			{
				return this._CspProviderName;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001A213 File Offset: 0x00018413
		public bool UseMachineContainer
		{
			get
			{
				return this._UseMachineContainer;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001A21B File Offset: 0x0001841B
		public bool UseOAEP
		{
			get
			{
				return this._UseOAEP;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001A223 File Offset: 0x00018423
		public bool UseFIPS
		{
			get
			{
				return this._UseFIPS;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001A22C File Offset: 0x0001842C
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			this._KeyName = "Rsa Key";
			this._KeyContainerName = configurationValues["keyContainerName"];
			configurationValues.Remove("keyContainerName");
			if (this._KeyContainerName == null || this._KeyContainerName.Length < 1)
			{
				this._KeyContainerName = "NetFrameworkConfigurationKey";
			}
			this._CspProviderName = configurationValues["cspProviderName"];
			configurationValues.Remove("cspProviderName");
			this._UseMachineContainer = RsaProtectedConfigurationProvider.GetBooleanValue(configurationValues, "useMachineContainer", true);
			this._UseOAEP = RsaProtectedConfigurationProvider.GetBooleanValue(configurationValues, "useOAEP", true);
			this._UseFIPS = RsaProtectedConfigurationProvider.GetBooleanValue(configurationValues, "useFIPS", true);
			if (configurationValues.Count > 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Unrecognized_initialization_value", new object[]
				{
					configurationValues.GetKey(0)
				}));
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001A304 File Offset: 0x00018504
		public RSAParameters RsaPublicKey
		{
			get
			{
				return this.GetCryptoServiceProvider(false, false).ExportParameters(false);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001A314 File Offset: 0x00018514
		private RSACryptoServiceProvider GetCryptoServiceProvider(bool exportable, bool keyMustExist)
		{
			RSACryptoServiceProvider result;
			try
			{
				CspParameters cspParameters = new CspParameters();
				cspParameters.KeyContainerName = this.KeyContainerName;
				cspParameters.KeyNumber = 1;
				cspParameters.ProviderType = 1;
				if (this.CspProviderName != null && this.CspProviderName.Length > 0)
				{
					cspParameters.ProviderName = this.CspProviderName;
				}
				if (this.UseMachineContainer)
				{
					cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
				}
				if (!exportable && !keyMustExist)
				{
					cspParameters.Flags |= CspProviderFlags.UseNonExportableKey;
				}
				if (keyMustExist)
				{
					cspParameters.Flags |= CspProviderFlags.UseExistingKey;
				}
				result = new RSACryptoServiceProvider(2048, cspParameters);
			}
			catch
			{
				this.ThrowBetterException(keyMustExist);
				throw;
			}
			return result;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001A3C8 File Offset: 0x000185C8
		private void ThrowBetterException(bool keyMustExist)
		{
			SafeCryptContextHandle safeCryptContextHandle = null;
			try
			{
				int num = UnsafeNativeMethods.CryptAcquireContext(out safeCryptContextHandle, this.KeyContainerName, this.CspProviderName, 1U, this.UseMachineContainer ? 32U : 0U);
				if (num == 0)
				{
					int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
					if (hrforLastWin32Error != -2146893802 || keyMustExist)
					{
						if (hrforLastWin32Error - -2147024891 <= 1 || hrforLastWin32Error == -2146893802)
						{
							throw new ConfigurationErrorsException(SR.GetString("Key_container_doesnt_exist_or_access_denied"));
						}
						Marshal.ThrowExceptionForHR(hrforLastWin32Error);
					}
				}
			}
			finally
			{
				if (safeCryptContextHandle != null && !safeCryptContextHandle.IsInvalid)
				{
					safeCryptContextHandle.Dispose();
				}
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001A460 File Offset: 0x00018660
		private static bool GetBooleanValue(NameValueCollection configurationValues, string valueName, bool defaultValue)
		{
			string text = configurationValues[valueName];
			if (text == null)
			{
				return defaultValue;
			}
			configurationValues.Remove(valueName);
			if (text == "true")
			{
				return true;
			}
			if (text == "false")
			{
				return false;
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_invalid_boolean_attribute", new object[]
			{
				valueName
			}));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001A4B8 File Offset: 0x000186B8
		private SymmetricAlgorithm GetSymAlgorithmProvider()
		{
			SymmetricAlgorithm result;
			if (this.UseFIPS)
			{
				result = new AesCryptoServiceProvider();
			}
			else
			{
				result = new TripleDESCryptoServiceProvider();
			}
			return result;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001A4DC File Offset: 0x000186DC
		private EncryptionMethod GetSymEncryptionMethod()
		{
			if (!this.UseFIPS)
			{
				return new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#tripledes-cbc");
			}
			return new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
		}

		// Token: 0x040002DF RID: 735
		private const string DefaultRsaKeyContainerName = "NetFrameworkConfigurationKey";

		// Token: 0x040002E0 RID: 736
		private string _KeyName;

		// Token: 0x040002E1 RID: 737
		private string _KeyContainerName;

		// Token: 0x040002E2 RID: 738
		private string _CspProviderName;

		// Token: 0x040002E3 RID: 739
		private bool _UseMachineContainer;

		// Token: 0x040002E4 RID: 740
		private bool _UseOAEP;

		// Token: 0x040002E5 RID: 741
		private bool _UseFIPS;

		// Token: 0x040002E6 RID: 742
		private const uint PROV_Rsa_FULL = 1U;

		// Token: 0x040002E7 RID: 743
		private const uint CRYPT_MACHINE_KEYSET = 32U;
	}
}

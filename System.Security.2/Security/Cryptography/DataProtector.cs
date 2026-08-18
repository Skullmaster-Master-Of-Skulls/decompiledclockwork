using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200001D RID: 29
	public abstract class DataProtector
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004EEC File Offset: 0x000030EC
		protected DataProtector(string applicationName, string primaryPurpose, string[] specificPurposes)
		{
			if (string.IsNullOrWhiteSpace(applicationName))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_DataProtector_InvalidAppNameOrPurpose"), "applicationName");
			}
			if (string.IsNullOrWhiteSpace(primaryPurpose))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_DataProtector_InvalidAppNameOrPurpose"), "primaryPurpose");
			}
			if (specificPurposes != null)
			{
				foreach (string value in specificPurposes)
				{
					if (string.IsNullOrWhiteSpace(value))
					{
						throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_DataProtector_InvalidAppNameOrPurpose"), "specificPurposes");
					}
				}
			}
			this.m_applicationName = applicationName;
			this.m_primaryPurpose = primaryPurpose;
			List<string> list = new List<string>();
			if (specificPurposes != null)
			{
				list.AddRange(specificPurposes);
			}
			this.m_specificPurposes = list;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004F92 File Offset: 0x00003192
		protected string ApplicationName
		{
			get
			{
				return this.m_applicationName;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004F9A File Offset: 0x0000319A
		protected virtual bool PrependHashedPurposeToPlaintext
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004FA0 File Offset: 0x000031A0
		protected virtual byte[] GetHashedPurpose()
		{
			if (this.m_hashedPurpose == null)
			{
				using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create("System.Security.Cryptography.Sha256Cng"))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(new CryptoStream(new MemoryStream(), hashAlgorithm, CryptoStreamMode.Write), new UTF8Encoding(false, true)))
					{
						binaryWriter.Write(this.ApplicationName);
						binaryWriter.Write(this.PrimaryPurpose);
						foreach (string value in this.SpecificPurposes)
						{
							binaryWriter.Write(value);
						}
					}
					this.m_hashedPurpose = hashAlgorithm.Hash;
				}
			}
			return this.m_hashedPurpose;
		}

		// Token: 0x060000C5 RID: 197
		public abstract bool IsReprotectRequired(byte[] encryptedData);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000507C File Offset: 0x0000327C
		protected string PrimaryPurpose
		{
			get
			{
				return this.m_primaryPurpose;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005084 File Offset: 0x00003284
		protected IEnumerable<string> SpecificPurposes
		{
			get
			{
				return this.m_specificPurposes;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000508C File Offset: 0x0000328C
		public static DataProtector Create(string providerClass, string applicationName, string primaryPurpose, params string[] specificPurposes)
		{
			if (providerClass == null)
			{
				throw new ArgumentNullException("providerClass");
			}
			return (DataProtector)CryptoConfig.CreateFromName(providerClass, new object[]
			{
				applicationName,
				primaryPurpose,
				specificPurposes
			});
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000050BC File Offset: 0x000032BC
		public byte[] Protect(byte[] userData)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			if (this.PrependHashedPurposeToPlaintext)
			{
				byte[] hashedPurpose = this.GetHashedPurpose();
				byte[] array = new byte[userData.Length + hashedPurpose.Length];
				Array.Copy(hashedPurpose, 0, array, 0, hashedPurpose.Length);
				Array.Copy(userData, 0, array, hashedPurpose.Length, userData.Length);
				userData = array;
			}
			return this.ProviderProtect(userData);
		}

		// Token: 0x060000CA RID: 202
		protected abstract byte[] ProviderProtect(byte[] userData);

		// Token: 0x060000CB RID: 203
		protected abstract byte[] ProviderUnprotect(byte[] encryptedData);

		// Token: 0x060000CC RID: 204 RVA: 0x00005118 File Offset: 0x00003318
		public byte[] Unprotect(byte[] encryptedData)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (!this.PrependHashedPurposeToPlaintext)
			{
				return this.ProviderUnprotect(encryptedData);
			}
			byte[] array = this.ProviderUnprotect(encryptedData);
			byte[] hashedPurpose = this.GetHashedPurpose();
			if (!SignedXml.CryptographicEquals(hashedPurpose, array, hashedPurpose.Length))
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_DataProtector_InvalidPurpose"));
			}
			byte[] array2 = new byte[array.Length - hashedPurpose.Length];
			Array.Copy(array, hashedPurpose.Length, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x04000389 RID: 905
		private string m_applicationName;

		// Token: 0x0400038A RID: 906
		private string m_primaryPurpose;

		// Token: 0x0400038B RID: 907
		private IEnumerable<string> m_specificPurposes;

		// Token: 0x0400038C RID: 908
		private volatile byte[] m_hashedPurpose;
	}
}

using System;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200007E RID: 126
	internal sealed class Signature
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00010746 File Offset: 0x0000E946
		public Signature(SignedXml signedXml, SignedInfo signedInfo)
		{
			this.signedXml = signedXml;
			this.signedInfo = signedInfo;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00010772 File Offset: 0x0000E972
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0001077A File Offset: 0x0000E97A
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00010783 File Offset: 0x0000E983
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0001078B File Offset: 0x0000E98B
		public SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				return this.keyIdentifier;
			}
			set
			{
				this.keyIdentifier = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00010794 File Offset: 0x0000E994
		public SignedInfo SignedInfo
		{
			get
			{
				return this.signedInfo;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001079C File Offset: 0x0000E99C
		public ISignatureValueSecurityElement SignatureValue
		{
			get
			{
				return this.signatureValueElement;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public byte[] GetSignatureBytes()
		{
			return this.signatureValueElement.Value;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
		{
			reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.Signature, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			this.Id = reader.GetAttribute(dictionaryManager.UtilityDictionary.IdAttribute, null);
			reader.Read();
			this.signedInfo.ReadFrom(reader, this.signedXml.TransformFactory, dictionaryManager);
			this.signatureValueElement.ReadFrom(reader, dictionaryManager);
			if (this.signedXml.SecurityTokenSerializer.CanReadKeyIdentifier(reader))
			{
				this.keyIdentifier = this.signedXml.SecurityTokenSerializer.ReadKeyIdentifier(reader);
			}
			reader.ReadEndElement();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001085D File Offset: 0x0000EA5D
		public void SetSignatureValue(byte[] signatureValue)
		{
			this.signatureValueElement.Value = signatureValue;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001086C File Offset: 0x0000EA6C
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.Signature, dictionaryManager.XmlSignatureDictionary.Namespace);
			if (this.id != null)
			{
				writer.WriteAttributeString(dictionaryManager.UtilityDictionary.IdAttribute, null, this.id);
			}
			this.signedInfo.WriteTo(writer, dictionaryManager);
			this.signatureValueElement.WriteTo(writer, dictionaryManager);
			if (this.keyIdentifier != null)
			{
				this.signedXml.SecurityTokenSerializer.WriteKeyIdentifier(writer, this.keyIdentifier);
			}
			writer.WriteEndElement();
		}

		// Token: 0x040003A2 RID: 930
		private SignedXml signedXml;

		// Token: 0x040003A3 RID: 931
		private string id;

		// Token: 0x040003A4 RID: 932
		private SecurityKeyIdentifier keyIdentifier;

		// Token: 0x040003A5 RID: 933
		private string prefix = "";

		// Token: 0x040003A6 RID: 934
		private readonly Signature.SignatureValueElement signatureValueElement = new Signature.SignatureValueElement();

		// Token: 0x040003A7 RID: 935
		private readonly SignedInfo signedInfo;

		// Token: 0x0200023B RID: 571
		private sealed class SignatureValueElement : ISignatureValueSecurityElement, ISecurityElement
		{
			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x06001220 RID: 4640 RVA: 0x00002434 File Offset: 0x00000634
			public bool HasId
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004F9BA File Offset: 0x0004DBBA
			// (set) Token: 0x06001222 RID: 4642 RVA: 0x0004F9C2 File Offset: 0x0004DBC2
			public string Id
			{
				get
				{
					return this.id;
				}
				set
				{
					this.id = value;
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x06001223 RID: 4643 RVA: 0x0004F9CB File Offset: 0x0004DBCB
			// (set) Token: 0x06001224 RID: 4644 RVA: 0x0004F9D3 File Offset: 0x0004DBD3
			internal byte[] Value
			{
				get
				{
					return this.signatureValue;
				}
				set
				{
					this.signatureValue = value;
					this.signatureText = null;
				}
			}

			// Token: 0x06001225 RID: 4645 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
			public void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
			{
				reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.SignatureValue, dictionaryManager.XmlSignatureDictionary.Namespace);
				this.prefix = reader.Prefix;
				this.Id = reader.GetAttribute("Id", null);
				reader.Read();
				this.signatureText = reader.ReadString();
				this.signatureValue = Convert.FromBase64String(this.signatureText.Trim());
				reader.ReadEndElement();
			}

			// Token: 0x06001226 RID: 4646 RVA: 0x0004FA5C File Offset: 0x0004DC5C
			public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
			{
				writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.SignatureValue, dictionaryManager.XmlSignatureDictionary.Namespace);
				if (this.id != null)
				{
					writer.WriteAttributeString(dictionaryManager.UtilityDictionary.IdAttribute, null, this.id);
				}
				if (this.signatureText != null)
				{
					writer.WriteString(this.signatureText);
				}
				else
				{
					writer.WriteBase64(this.signatureValue, 0, this.signatureValue.Length);
				}
				writer.WriteEndElement();
			}

			// Token: 0x06001227 RID: 4647 RVA: 0x0004FADC File Offset: 0x0004DCDC
			byte[] ISignatureValueSecurityElement.GetSignatureValue()
			{
				return this.Value;
			}

			// Token: 0x04000F5F RID: 3935
			private string id;

			// Token: 0x04000F60 RID: 3936
			private string prefix = "";

			// Token: 0x04000F61 RID: 3937
			private byte[] signatureValue;

			// Token: 0x04000F62 RID: 3938
			private string signatureText;
		}
	}
}

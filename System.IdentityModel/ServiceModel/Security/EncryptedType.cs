using System;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200000A RID: 10
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal abstract class EncryptedType : ISecurityElement
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000026DB File Offset: 0x000008DB
		protected EncryptedType()
		{
			this.encryptionMethod.Init();
			this.state = EncryptedType.EncryptionState.New;
			this.tokenSerializer = new KeyInfoSerializer(false);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002701 File Offset: 0x00000901
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002709 File Offset: 0x00000909
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000271F File Offset: 0x0000091F
		public string EncryptionMethod
		{
			get
			{
				return this.encryptionMethod.algorithm;
			}
			set
			{
				this.encryptionMethod.algorithm = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000272D File Offset: 0x0000092D
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000273A File Offset: 0x0000093A
		public XmlDictionaryString EncryptionMethodDictionaryString
		{
			get
			{
				return this.encryptionMethod.algorithmDictionaryString;
			}
			set
			{
				this.encryptionMethod.algorithmDictionaryString = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002434 File Offset: 0x00000634
		public bool HasId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002748 File Offset: 0x00000948
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002750 File Offset: 0x00000950
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002759 File Offset: 0x00000959
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002761 File Offset: 0x00000961
		public bool ShouldReadXmlReferenceKeyInfoClause
		{
			get
			{
				return this.shouldReadXmlReferenceKeyInfoClause;
			}
			set
			{
				this.shouldReadXmlReferenceKeyInfoClause = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000276A File Offset: 0x0000096A
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002772 File Offset: 0x00000972
		public string WsuId
		{
			get
			{
				return this.wsuId;
			}
			set
			{
				this.wsuId = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000277B File Offset: 0x0000097B
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002783 File Offset: 0x00000983
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000278C File Offset: 0x0000098C
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002794 File Offset: 0x00000994
		public string MimeType
		{
			get
			{
				return this.mimeType;
			}
			set
			{
				this.mimeType = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000279D File Offset: 0x0000099D
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000027A5 File Offset: 0x000009A5
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65
		protected abstract XmlDictionaryString OpeningElementName { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027AE File Offset: 0x000009AE
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000027B6 File Offset: 0x000009B6
		protected EncryptedType.EncryptionState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000027BF File Offset: 0x000009BF
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000027C7 File Offset: 0x000009C7
		public SecurityTokenSerializer SecurityTokenSerializer
		{
			get
			{
				return this.tokenSerializer;
			}
			set
			{
				this.tokenSerializer = (value ?? new KeyInfoSerializer(false));
			}
		}

		// Token: 0x06000046 RID: 70
		protected abstract void ForceEncryption();

		// Token: 0x06000047 RID: 71 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void ReadAdditionalAttributes(XmlDictionaryReader reader)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void ReadAdditionalElements(XmlDictionaryReader reader)
		{
		}

		// Token: 0x06000049 RID: 73
		protected abstract void ReadCipherData(XmlDictionaryReader reader);

		// Token: 0x0600004A RID: 74
		protected abstract void ReadCipherData(XmlDictionaryReader reader, long maxBufferSize);

		// Token: 0x0600004B RID: 75 RVA: 0x000027DA File Offset: 0x000009DA
		public void ReadFrom(XmlReader reader)
		{
			this.ReadFrom(reader, 0L);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000027E5 File Offset: 0x000009E5
		public void ReadFrom(XmlDictionaryReader reader)
		{
			this.ReadFrom(reader, 0L);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027F0 File Offset: 0x000009F0
		public void ReadFrom(XmlReader reader, long maxBufferSize)
		{
			this.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader), maxBufferSize);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002800 File Offset: 0x00000A00
		public void ReadFrom(XmlDictionaryReader reader, long maxBufferSize)
		{
			this.ValidateReadState();
			reader.MoveToStartElement(this.OpeningElementName, EncryptedType.NamespaceUri);
			this.encoding = reader.GetAttribute(EncryptedType.EncodingAttribute, null);
			this.id = (reader.GetAttribute(XD.XmlEncryptionDictionary.Id, null) ?? SecurityUniqueId.Create().Value);
			this.wsuId = (reader.GetAttribute(XD.XmlEncryptionDictionary.Id, XD.UtilityDictionary.Namespace) ?? SecurityUniqueId.Create().Value);
			this.mimeType = reader.GetAttribute(EncryptedType.MimeTypeAttribute, null);
			this.type = reader.GetAttribute(EncryptedType.TypeAttribute, null);
			this.ReadAdditionalAttributes(reader);
			reader.Read();
			if (reader.IsStartElement(EncryptedType.EncryptionMethodElement.ElementName, EncryptedType.NamespaceUri))
			{
				this.encryptionMethod.ReadFrom(reader);
			}
			if (this.tokenSerializer.CanReadKeyIdentifier(reader))
			{
				XmlElement node = null;
				XmlDictionaryReader reader2;
				if (this.ShouldReadXmlReferenceKeyInfoClause)
				{
					XmlDocument xmlDocument = new XmlDocument();
					node = (xmlDocument.ReadNode(reader) as XmlElement);
					reader2 = XmlDictionaryReader.CreateDictionaryReader(new XmlNodeReader(node));
				}
				else
				{
					reader2 = reader;
				}
				try
				{
					this.KeyIdentifier = this.tokenSerializer.ReadKeyIdentifier(reader2);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex) || !this.ShouldReadXmlReferenceKeyInfoClause)
					{
						throw;
					}
					this.keyIdentifier = this.ReadGenericXmlSecurityKeyIdentifier(XmlDictionaryReader.CreateDictionaryReader(new XmlNodeReader(node)), ex);
				}
			}
			reader.ReadStartElement(EncryptedType.CipherDataElementName, EncryptedType.NamespaceUri);
			reader.ReadStartElement(EncryptedType.CipherValueElementName, EncryptedType.NamespaceUri);
			if (maxBufferSize == 0L)
			{
				this.ReadCipherData(reader);
			}
			else
			{
				this.ReadCipherData(reader, maxBufferSize);
			}
			reader.ReadEndElement();
			reader.ReadEndElement();
			this.ReadAdditionalElements(reader);
			reader.ReadEndElement();
			this.State = EncryptedType.EncryptionState.Read;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029BC File Offset: 0x00000BBC
		private SecurityKeyIdentifier ReadGenericXmlSecurityKeyIdentifier(XmlDictionaryReader localReader, Exception previousException)
		{
			if (!localReader.IsStartElement(XD.XmlSignatureDictionary.KeyInfo, XD.XmlSignatureDictionary.Namespace))
			{
				return null;
			}
			localReader.ReadStartElement(XD.XmlSignatureDictionary.KeyInfo, XD.XmlSignatureDictionary.Namespace);
			SecurityKeyIdentifier securityKeyIdentifier = new SecurityKeyIdentifier();
			if (localReader.IsStartElement())
			{
				string attribute = localReader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement referenceXml = xmlDocument.ReadNode(localReader) as XmlElement;
				SecurityKeyIdentifierClause securityKeyIdentifierClause = new GenericXmlSecurityKeyIdentifierClause(referenceXml);
				if (!string.IsNullOrEmpty(attribute))
				{
					securityKeyIdentifierClause.Id = attribute;
				}
				securityKeyIdentifier.Add(securityKeyIdentifierClause);
			}
			if (securityKeyIdentifier.Count == 0)
			{
				throw previousException;
			}
			localReader.ReadEndElement();
			return securityKeyIdentifier;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void WriteAdditionalAttributes(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void WriteAdditionalElements(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
		}

		// Token: 0x06000052 RID: 82
		protected abstract void WriteCipherData(XmlDictionaryWriter writer);

		// Token: 0x06000053 RID: 83 RVA: 0x00002A70 File Offset: 0x00000C70
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			this.ValidateWriteState();
			writer.WriteStartElement("e", this.OpeningElementName, EncryptedType.NamespaceUri);
			if (this.id != null && this.id.Length != 0)
			{
				writer.WriteAttributeString(XD.XmlEncryptionDictionary.Id, null, this.Id);
			}
			if (this.type != null)
			{
				writer.WriteAttributeString(EncryptedType.TypeAttribute, null, this.Type);
			}
			if (this.mimeType != null)
			{
				writer.WriteAttributeString(EncryptedType.MimeTypeAttribute, null, this.MimeType);
			}
			if (this.encoding != null)
			{
				writer.WriteAttributeString(EncryptedType.EncodingAttribute, null, this.Encoding);
			}
			this.WriteAdditionalAttributes(writer, dictionaryManager);
			if (this.encryptionMethod.algorithm != null)
			{
				this.encryptionMethod.WriteTo(writer);
			}
			if (this.KeyIdentifier != null)
			{
				this.tokenSerializer.WriteKeyIdentifier(writer, this.KeyIdentifier);
			}
			writer.WriteStartElement(EncryptedType.CipherDataElementName, EncryptedType.NamespaceUri);
			writer.WriteStartElement(EncryptedType.CipherValueElementName, EncryptedType.NamespaceUri);
			this.WriteCipherData(writer);
			writer.WriteEndElement();
			writer.WriteEndElement();
			this.WriteAdditionalElements(writer, dictionaryManager);
			writer.WriteEndElement();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BA2 File Offset: 0x00000DA2
		private void ValidateReadState()
		{
			if (this.State != EncryptedType.EncryptionState.New)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("BadEncryptionState")));
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BC6 File Offset: 0x00000DC6
		private void ValidateWriteState()
		{
			if (this.State == EncryptedType.EncryptionState.EncryptionSetup)
			{
				this.ForceEncryption();
				return;
			}
			if (this.State == EncryptedType.EncryptionState.New)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("BadEncryptionState")));
			}
		}

		// Token: 0x0400005D RID: 93
		internal static readonly XmlDictionaryString NamespaceUri = XD.XmlEncryptionDictionary.Namespace;

		// Token: 0x0400005E RID: 94
		internal static readonly XmlDictionaryString EncodingAttribute = XD.XmlEncryptionDictionary.Encoding;

		// Token: 0x0400005F RID: 95
		internal static readonly XmlDictionaryString MimeTypeAttribute = XD.XmlEncryptionDictionary.MimeType;

		// Token: 0x04000060 RID: 96
		internal static readonly XmlDictionaryString TypeAttribute = XD.XmlEncryptionDictionary.Type;

		// Token: 0x04000061 RID: 97
		internal static readonly XmlDictionaryString CipherDataElementName = XD.XmlEncryptionDictionary.CipherData;

		// Token: 0x04000062 RID: 98
		internal static readonly XmlDictionaryString CipherValueElementName = XD.XmlEncryptionDictionary.CipherValue;

		// Token: 0x04000063 RID: 99
		private string encoding;

		// Token: 0x04000064 RID: 100
		private EncryptedType.EncryptionMethodElement encryptionMethod;

		// Token: 0x04000065 RID: 101
		private string id;

		// Token: 0x04000066 RID: 102
		private string wsuId;

		// Token: 0x04000067 RID: 103
		private SecurityKeyIdentifier keyIdentifier;

		// Token: 0x04000068 RID: 104
		private string mimeType;

		// Token: 0x04000069 RID: 105
		private EncryptedType.EncryptionState state;

		// Token: 0x0400006A RID: 106
		private string type;

		// Token: 0x0400006B RID: 107
		private SecurityTokenSerializer tokenSerializer;

		// Token: 0x0400006C RID: 108
		private bool shouldReadXmlReferenceKeyInfoClause;

		// Token: 0x0200021A RID: 538
		protected enum EncryptionState
		{
			// Token: 0x04000EE9 RID: 3817
			New,
			// Token: 0x04000EEA RID: 3818
			Read,
			// Token: 0x04000EEB RID: 3819
			DecryptionSetup,
			// Token: 0x04000EEC RID: 3820
			Decrypted,
			// Token: 0x04000EED RID: 3821
			EncryptionSetup,
			// Token: 0x04000EEE RID: 3822
			Encrypted
		}

		// Token: 0x0200021B RID: 539
		private struct EncryptionMethodElement
		{
			// Token: 0x060011C2 RID: 4546 RVA: 0x0004E08C File Offset: 0x0004C28C
			public void Init()
			{
				this.algorithm = null;
			}

			// Token: 0x060011C3 RID: 4547 RVA: 0x0004E098 File Offset: 0x0004C298
			public void ReadFrom(XmlDictionaryReader reader)
			{
				reader.MoveToStartElement(EncryptedType.EncryptionMethodElement.ElementName, XD.XmlEncryptionDictionary.Namespace);
				bool isEmptyElement = reader.IsEmptyElement;
				this.algorithm = reader.GetAttribute(XD.XmlSignatureDictionary.Algorithm, null);
				if (this.algorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityMessageSerializationException(SR.GetString("RequiredAttributeMissing", new object[]
					{
						XD.XmlSignatureDictionary.Algorithm.Value,
						EncryptedType.EncryptionMethodElement.ElementName.Value
					})));
				}
				reader.Read();
				if (!isEmptyElement)
				{
					while (reader.IsStartElement())
					{
						reader.Skip();
					}
					reader.ReadEndElement();
				}
			}

			// Token: 0x060011C4 RID: 4548 RVA: 0x0004E140 File Offset: 0x0004C340
			public void WriteTo(XmlDictionaryWriter writer)
			{
				writer.WriteStartElement("e", EncryptedType.EncryptionMethodElement.ElementName, XD.XmlEncryptionDictionary.Namespace);
				if (this.algorithmDictionaryString != null)
				{
					writer.WriteStartAttribute(XD.XmlSignatureDictionary.Algorithm, null);
					writer.WriteString(this.algorithmDictionaryString);
					writer.WriteEndAttribute();
				}
				else
				{
					writer.WriteAttributeString(XD.XmlSignatureDictionary.Algorithm, null, this.algorithm);
				}
				if (this.algorithm == XD.SecurityAlgorithmDictionary.RsaOaepKeyWrap.Value)
				{
					writer.WriteStartElement("", XD.XmlSignatureDictionary.DigestMethod, XD.XmlSignatureDictionary.Namespace);
					writer.WriteStartAttribute(XD.XmlSignatureDictionary.Algorithm, null);
					writer.WriteString(XD.SecurityAlgorithmDictionary.Sha1Digest);
					writer.WriteEndAttribute();
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}

			// Token: 0x04000EEF RID: 3823
			internal string algorithm;

			// Token: 0x04000EF0 RID: 3824
			internal XmlDictionaryString algorithmDictionaryString;

			// Token: 0x04000EF1 RID: 3825
			internal static readonly XmlDictionaryString ElementName = XD.XmlEncryptionDictionary.EncryptionMethod;
		}
	}
}

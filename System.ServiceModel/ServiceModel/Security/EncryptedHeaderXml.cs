using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200027F RID: 639
	internal sealed class EncryptedHeaderXml
	{
		// Token: 0x0600123C RID: 4668 RVA: 0x000436B4 File Offset: 0x000418B4
		public EncryptedHeaderXml(MessageVersion version, bool shouldReadXmlReferenceKeyInfoClause)
		{
			this.version = version;
			this.encryptedData = new EncryptedData();
			this.encryptedData.ShouldReadXmlReferenceKeyInfoClause = shouldReadXmlReferenceKeyInfoClause;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x000436DA File Offset: 0x000418DA
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x000436E2 File Offset: 0x000418E2
		public string Actor
		{
			get
			{
				return this.actor;
			}
			set
			{
				this.actor = value;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x000436EB File Offset: 0x000418EB
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x000436F8 File Offset: 0x000418F8
		public string EncryptionMethod
		{
			get
			{
				return this.encryptedData.EncryptionMethod;
			}
			set
			{
				this.encryptedData.EncryptionMethod = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00043706 File Offset: 0x00041906
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x00043713 File Offset: 0x00041913
		public XmlDictionaryString EncryptionMethodDictionaryString
		{
			get
			{
				return this.encryptedData.EncryptionMethodDictionaryString;
			}
			set
			{
				this.encryptedData.EncryptionMethodDictionaryString = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00043721 File Offset: 0x00041921
		public bool HasId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00043724 File Offset: 0x00041924
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x0004372C File Offset: 0x0004192C
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00043735 File Offset: 0x00041935
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00043742 File Offset: 0x00041942
		public SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				return this.encryptedData.KeyIdentifier;
			}
			set
			{
				this.encryptedData.KeyIdentifier = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00043750 File Offset: 0x00041950
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00043758 File Offset: 0x00041958
		public bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
			set
			{
				this.mustUnderstand = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00043761 File Offset: 0x00041961
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00043769 File Offset: 0x00041969
		public bool Relay
		{
			get
			{
				return this.relay;
			}
			set
			{
				this.relay = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00043772 File Offset: 0x00041972
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x0004377F File Offset: 0x0004197F
		public SecurityTokenSerializer SecurityTokenSerializer
		{
			get
			{
				return this.encryptedData.SecurityTokenSerializer;
			}
			set
			{
				this.encryptedData.SecurityTokenSerializer = value;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004378D File Offset: 0x0004198D
		public byte[] GetDecryptedBuffer()
		{
			return this.encryptedData.GetDecryptedBuffer();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004379C File Offset: 0x0004199C
		public void ReadFrom(XmlDictionaryReader reader, long maxBufferSize)
		{
			reader.MoveToStartElement(EncryptedHeaderXml.ElementName, EncryptedHeaderXml.NamespaceUri);
			bool flag;
			MessageHeader.GetHeaderAttributes(reader, this.version, out this.actor, out this.mustUnderstand, out this.relay, out flag);
			this.id = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
			reader.ReadStartElement();
			this.encryptedData.ReadFrom(reader, maxBufferSize);
			reader.ReadEndElement();
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00043812 File Offset: 0x00041A12
		public void SetUpDecryption(SymmetricAlgorithm algorithm)
		{
			this.encryptedData.SetUpDecryption(algorithm);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00043820 File Offset: 0x00041A20
		public void SetUpEncryption(SymmetricAlgorithm algorithm, MemoryStream source)
		{
			this.encryptedData.SetUpEncryption(algorithm, new ArraySegment<byte>(source.GetBuffer(), 0, (int)source.Length));
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00043841 File Offset: 0x00041A41
		public void WriteHeaderElement(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement("k", EncryptedHeaderXml.ElementName, EncryptedHeaderXml.NamespaceUri);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00043858 File Offset: 0x00041A58
		public void WriteHeaderId(XmlDictionaryWriter writer)
		{
			writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, this.id);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00043889 File Offset: 0x00041A89
		public void WriteHeaderContents(XmlDictionaryWriter writer)
		{
			this.encryptedData.WriteTo(writer, ServiceModelDictionaryManager.Instance);
		}

		// Token: 0x040019E3 RID: 6627
		internal static readonly XmlDictionaryString ElementName = XD.SecurityXXX2005Dictionary.EncryptedHeader;

		// Token: 0x040019E4 RID: 6628
		internal static readonly XmlDictionaryString NamespaceUri = XD.SecurityXXX2005Dictionary.Namespace;

		// Token: 0x040019E5 RID: 6629
		private const string Prefix = "k";

		// Token: 0x040019E6 RID: 6630
		private string id;

		// Token: 0x040019E7 RID: 6631
		private bool mustUnderstand;

		// Token: 0x040019E8 RID: 6632
		private bool relay;

		// Token: 0x040019E9 RID: 6633
		private string actor;

		// Token: 0x040019EA RID: 6634
		private MessageVersion version;

		// Token: 0x040019EB RID: 6635
		private EncryptedData encryptedData;
	}
}

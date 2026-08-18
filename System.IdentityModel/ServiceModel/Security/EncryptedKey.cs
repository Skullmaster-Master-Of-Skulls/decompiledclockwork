using System;
using System.IdentityModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000008 RID: 8
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal sealed class EncryptedKey : EncryptedType
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000248F File Offset: 0x0000068F
		public string CarriedKeyName
		{
			get
			{
				return this.carriedKeyName;
			}
			set
			{
				this.carriedKeyName = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000024A0 File Offset: 0x000006A0
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
			set
			{
				this.recipient = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024A9 File Offset: 0x000006A9
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024B1 File Offset: 0x000006B1
		public ReferenceList ReferenceList
		{
			get
			{
				return this.referenceList;
			}
			set
			{
				this.referenceList = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024BA File Offset: 0x000006BA
		protected override XmlDictionaryString OpeningElementName
		{
			get
			{
				return EncryptedKey.ElementName;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024C1 File Offset: 0x000006C1
		protected override void ForceEncryption()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024C3 File Offset: 0x000006C3
		public byte[] GetWrappedKey()
		{
			if (base.State == EncryptedType.EncryptionState.New)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadEncryptionState")));
			}
			return this.wrappedKey;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024F0 File Offset: 0x000006F0
		public void SetUpKeyWrap(byte[] wrappedKey)
		{
			if (base.State != EncryptedType.EncryptionState.New)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadEncryptionState")));
			}
			if (wrappedKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedKey");
			}
			this.wrappedKey = wrappedKey;
			base.State = EncryptedType.EncryptionState.Encrypted;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002540 File Offset: 0x00000740
		protected override void ReadAdditionalAttributes(XmlDictionaryReader reader)
		{
			this.recipient = reader.GetAttribute(EncryptedKey.RecipientAttribute, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002554 File Offset: 0x00000754
		protected override void ReadAdditionalElements(XmlDictionaryReader reader)
		{
			if (reader.IsStartElement(ReferenceList.ElementName, EncryptedType.NamespaceUri))
			{
				this.referenceList = new ReferenceList();
				this.referenceList.ReadFrom(reader);
			}
			if (reader.IsStartElement(EncryptedKey.CarriedKeyElementName, EncryptedType.NamespaceUri))
			{
				reader.ReadStartElement(EncryptedKey.CarriedKeyElementName, EncryptedType.NamespaceUri);
				this.carriedKeyName = reader.ReadString();
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025BE File Offset: 0x000007BE
		protected override void ReadCipherData(XmlDictionaryReader reader)
		{
			this.wrappedKey = reader.ReadContentAsBase64();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025CC File Offset: 0x000007CC
		protected override void ReadCipherData(XmlDictionaryReader reader, long maxBufferSize)
		{
			this.wrappedKey = SecurityUtils.ReadContentAsBase64(reader, maxBufferSize);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025DB File Offset: 0x000007DB
		protected override void WriteAdditionalAttributes(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			if (this.recipient != null)
			{
				writer.WriteAttributeString(EncryptedKey.RecipientAttribute, null, this.recipient);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F8 File Offset: 0x000007F8
		protected override void WriteAdditionalElements(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			if (this.carriedKeyName != null)
			{
				writer.WriteStartElement(EncryptedKey.CarriedKeyElementName, EncryptedType.NamespaceUri);
				writer.WriteString(this.carriedKeyName);
				writer.WriteEndElement();
			}
			if (this.referenceList != null)
			{
				this.referenceList.WriteTo(writer, dictionaryManager);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002644 File Offset: 0x00000844
		protected override void WriteCipherData(XmlDictionaryWriter writer)
		{
			writer.WriteBase64(this.wrappedKey, 0, this.wrappedKey.Length);
		}

		// Token: 0x04000056 RID: 86
		internal static readonly XmlDictionaryString CarriedKeyElementName = XD.XmlEncryptionDictionary.CarriedKeyName;

		// Token: 0x04000057 RID: 87
		internal static readonly XmlDictionaryString ElementName = XD.XmlEncryptionDictionary.EncryptedKey;

		// Token: 0x04000058 RID: 88
		internal static readonly XmlDictionaryString RecipientAttribute = XD.XmlEncryptionDictionary.Recipient;

		// Token: 0x04000059 RID: 89
		private string carriedKeyName;

		// Token: 0x0400005A RID: 90
		private string recipient;

		// Token: 0x0400005B RID: 91
		private ReferenceList referenceList;

		// Token: 0x0400005C RID: 92
		private byte[] wrappedKey;
	}
}

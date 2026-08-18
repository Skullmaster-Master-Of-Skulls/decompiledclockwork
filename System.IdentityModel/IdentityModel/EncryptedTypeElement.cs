using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200003A RID: 58
	internal abstract class EncryptedTypeElement
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00008F51 File Offset: 0x00007151
		public EncryptedTypeElement(SecurityTokenSerializer keyInfoSerializer)
		{
			this._cipherData = new CipherDataElement();
			this._encryptionMethod = new EncryptionMethodElement();
			this._keyInfo = new KeyInfo(keyInfoSerializer);
			this._properties = new List<string>();
			this._keyInfoSerializer = keyInfoSerializer;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008F8D File Offset: 0x0000718D
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00008FA4 File Offset: 0x000071A4
		public string Algorithm
		{
			get
			{
				if (this.EncryptionMethod == null)
				{
					return null;
				}
				return this.EncryptionMethod.Algorithm;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.EncryptionMethod.Algorithm = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008FC5 File Offset: 0x000071C5
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00008FCD File Offset: 0x000071CD
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("value");
				}
				this._id = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008FE9 File Offset: 0x000071E9
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00008FF1 File Offset: 0x000071F1
		public EncryptionMethodElement EncryptionMethod
		{
			get
			{
				return this._encryptionMethod;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._encryptionMethod = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000900D File Offset: 0x0000720D
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00009015 File Offset: 0x00007215
		public CipherDataElement CipherData
		{
			get
			{
				return this._cipherData;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._cipherData = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00009031 File Offset: 0x00007231
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000903E File Offset: 0x0000723E
		public SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				return this._keyInfo.KeyIdentifier;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._keyInfo.KeyIdentifier = value;
			}
		}

		// Token: 0x06000223 RID: 547
		public abstract void ReadExtensions(XmlDictionaryReader reader);

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000905F File Offset: 0x0000725F
		public SecurityTokenSerializer TokenSerializer
		{
			get
			{
				return this._keyInfoSerializer;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009067 File Offset: 0x00007267
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000906F File Offset: 0x0000726F
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ThrowHelperArgumentNullOrEmptyString("value");
				}
				this._type = value;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000908C File Offset: 0x0000728C
		public virtual void ReadXml(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			this._id = reader.GetAttribute("Id", null);
			this._type = reader.GetAttribute("Type", null);
			this._mimeType = reader.GetAttribute("MimeType", null);
			this._encoding = reader.GetAttribute("Encoding", null);
			reader.ReadStartElement();
			reader.MoveToContent();
			if (reader.IsStartElement("EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#"))
			{
				this._encryptionMethod.ReadXml(reader);
			}
			reader.MoveToContent();
			if (reader.IsStartElement(XD.XmlSignatureDictionary.KeyInfo.Value, XD.XmlSignatureDictionary.Namespace.Value))
			{
				this._keyInfo = new KeyInfo(this._keyInfoSerializer);
				if (this._keyInfoSerializer.CanReadKeyIdentifier(reader))
				{
					this._keyInfo.KeyIdentifier = this._keyInfoSerializer.ReadKeyIdentifier(reader);
				}
				else
				{
					this._keyInfo.ReadXml(reader);
				}
			}
			reader.MoveToContent();
			this._cipherData.ReadXml(reader);
			this.ReadExtensions(reader);
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x04000144 RID: 324
		private KeyInfo _keyInfo;

		// Token: 0x04000145 RID: 325
		private EncryptionMethodElement _encryptionMethod;

		// Token: 0x04000146 RID: 326
		private CipherDataElement _cipherData;

		// Token: 0x04000147 RID: 327
		private List<string> _properties;

		// Token: 0x04000148 RID: 328
		private SecurityTokenSerializer _keyInfoSerializer;

		// Token: 0x04000149 RID: 329
		private string _id;

		// Token: 0x0400014A RID: 330
		private string _type;

		// Token: 0x0400014B RID: 331
		private string _mimeType;

		// Token: 0x0400014C RID: 332
		private string _encoding;
	}
}

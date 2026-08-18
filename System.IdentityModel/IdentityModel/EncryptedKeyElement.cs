using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000039 RID: 57
	internal class EncryptedKeyElement : EncryptedTypeElement
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00008C94 File Offset: 0x00006E94
		public EncryptedKeyElement(SecurityTokenSerializer keyInfoSerializer) : base(keyInfoSerializer)
		{
			this._keyReferences = new List<string>();
			this._dataReferences = new List<string>();
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008CB3 File Offset: 0x00006EB3
		public string CarriedName
		{
			get
			{
				return this._carriedName;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008CBB File Offset: 0x00006EBB
		public IList<string> DataReferences
		{
			get
			{
				return this._dataReferences;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00008CC3 File Offset: 0x00006EC3
		public IList<string> KeyReferences
		{
			get
			{
				return this._keyReferences;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008CCC File Offset: 0x00006ECC
		public override void ReadExtensions(XmlDictionaryReader reader)
		{
			reader.MoveToContent();
			if (reader.IsStartElement("ReferenceList", "http://www.w3.org/2001/04/xmlenc#"))
			{
				reader.ReadStartElement();
				if (reader.IsStartElement("DataReference", "http://www.w3.org/2001/04/xmlenc#"))
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("DataReference", "http://www.w3.org/2001/04/xmlenc#"))
						{
							string attribute = reader.GetAttribute("URI");
							if (!string.IsNullOrEmpty(attribute))
							{
								this._dataReferences.Add(attribute);
							}
							reader.Skip();
						}
						else
						{
							if (reader.IsStartElement("KeyReference", "http://www.w3.org/2001/04/xmlenc#"))
							{
								throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4189"));
							}
							string text = reader.ReadOuterXml();
							if (DiagnosticUtility.ShouldTraceWarning)
							{
								TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID8024", new object[]
								{
									reader.Name,
									reader.NamespaceURI,
									text
								}), new object[0]);
							}
						}
					}
				}
				else
				{
					if (!reader.IsStartElement("KeyReference", "http://www.w3.org/2001/04/xmlenc#"))
					{
						throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4191"));
					}
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("KeyReference", "http://www.w3.org/2001/04/xmlenc#"))
						{
							string attribute2 = reader.GetAttribute("URI");
							if (!string.IsNullOrEmpty(attribute2))
							{
								this._keyReferences.Add(attribute2);
							}
							reader.Skip();
						}
						else
						{
							if (reader.IsStartElement("DataReference", "http://www.w3.org/2001/04/xmlenc#"))
							{
								throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4190"));
							}
							string text2 = reader.ReadOuterXml();
							if (DiagnosticUtility.ShouldTraceWarning)
							{
								TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID8024", new object[]
								{
									reader.Name,
									reader.NamespaceURI,
									text2
								}), new object[0]);
							}
						}
					}
				}
				reader.MoveToContent();
				if (reader.IsStartElement("CarriedKeyName", "http://www.w3.org/2001/04/xmlenc#"))
				{
					reader.ReadStartElement();
					this._carriedName = reader.ReadString();
					reader.ReadEndElement();
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008ED0 File Offset: 0x000070D0
		public override void ReadXml(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (!reader.IsStartElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#"))
			{
				throw DiagnosticUtility.ThrowHelperXml(reader, SR.GetString("ID4187"));
			}
			this._recipient = reader.GetAttribute("Recipient", null);
			base.ReadXml(reader);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008F33 File Offset: 0x00007133
		public EncryptedKeyIdentifierClause GetClause()
		{
			return new EncryptedKeyIdentifierClause(base.CipherData.CipherValue, base.Algorithm, base.KeyIdentifier);
		}

		// Token: 0x04000140 RID: 320
		private string _carriedName;

		// Token: 0x04000141 RID: 321
		private string _recipient;

		// Token: 0x04000142 RID: 322
		private List<string> _keyReferences;

		// Token: 0x04000143 RID: 323
		private List<string> _dataReferences;
	}
}

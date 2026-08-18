using System;
using System.Diagnostics;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200004B RID: 75
	internal class KeyInfo
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000B81A File Offset: 0x00009A1A
		internal static void ResetReadDepth()
		{
			KeyInfo.t_keyInfoReadDepth = 0;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B822 File Offset: 0x00009A22
		public KeyInfo(SecurityTokenSerializer keyInfoSerializer)
		{
			this._keyInfoSerializer = keyInfoSerializer;
			this._ski = new SecurityKeyIdentifier();
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B83C File Offset: 0x00009A3C
		public string RetrievalMethod
		{
			get
			{
				return this._retrieval;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B844 File Offset: 0x00009A44
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B84C File Offset: 0x00009A4C
		public SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				return this._ski;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._ski = value;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B868 File Offset: 0x00009A68
		public virtual void ReadXml(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			KeyInfo.t_keyInfoReadDepth++;
			try
			{
				if (!LocalAppContextSwitches.AllowUnlimitedXmlRecursion && KeyInfo.t_keyInfoReadDepth > 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4194", new object[]
					{
						KeyInfo.t_keyInfoReadDepth,
						8
					})));
				}
				reader.MoveToContent();
				if (reader.IsStartElement(XD.XmlSignatureDictionary.KeyInfo.Value, XD.XmlSignatureDictionary.Namespace.Value))
				{
					reader.ReadStartElement();
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("RetrievalMethod", XD.XmlSignatureDictionary.Namespace.Value))
						{
							string attribute = reader.GetAttribute(XD.XmlSignatureDictionary.URI.Value);
							if (!string.IsNullOrEmpty(attribute))
							{
								this._retrieval = attribute;
							}
							reader.Skip();
						}
						else if (this._keyInfoSerializer.CanReadKeyIdentifierClause(reader))
						{
							this._ski.Add(this._keyInfoSerializer.ReadKeyIdentifierClause(reader));
						}
						else if (reader.IsStartElement())
						{
							string text = reader.ReadOuterXml();
							if (DiagnosticUtility.ShouldTraceWarning)
							{
								TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID8023", new object[]
								{
									reader.Name,
									reader.NamespaceURI,
									text
								}), new object[0]);
							}
						}
						reader.MoveToContent();
					}
					reader.MoveToContent();
					reader.ReadEndElement();
				}
			}
			finally
			{
				KeyInfo.t_keyInfoReadDepth--;
			}
		}

		// Token: 0x04000296 RID: 662
		private const int MaxKeyInfoReadDepth = 8;

		// Token: 0x04000297 RID: 663
		[ThreadStatic]
		private static int t_keyInfoReadDepth;

		// Token: 0x04000298 RID: 664
		private SecurityTokenSerializer _keyInfoSerializer;

		// Token: 0x04000299 RID: 665
		private SecurityKeyIdentifier _ski;

		// Token: 0x0400029A RID: 666
		private string _retrieval;
	}
}

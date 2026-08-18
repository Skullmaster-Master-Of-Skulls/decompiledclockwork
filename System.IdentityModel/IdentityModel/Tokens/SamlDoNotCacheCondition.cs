using System;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200015A RID: 346
	public class SamlDoNotCacheCondition : SamlCondition
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002FE24 File Offset: 0x0002E024
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002FE2C File Offset: 0x0002E02C
		public override void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002FE38 File Offset: 0x0002E038
		public override void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			if (!reader.IsStartElement(samlDictionary.DoNotCacheCondition, samlDictionary.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
				{
					samlDictionary.DoNotCacheCondition.Value
				})));
			}
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
				return;
			}
			reader.MoveToContent();
			reader.Read();
			reader.ReadEndElement();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		public override void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.DoNotCacheCondition, samlDictionary.Namespace);
			writer.WriteEndElement();
		}

		// Token: 0x04000BC9 RID: 3017
		private bool isReadOnly;
	}
}

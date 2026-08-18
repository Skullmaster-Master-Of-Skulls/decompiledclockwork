using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000151 RID: 337
	public class SamlAudienceRestrictionCondition : SamlCondition
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x0002DF48 File Offset: 0x0002C148
		public SamlAudienceRestrictionCondition(IEnumerable<Uri> audiences)
		{
			if (audiences == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("audiences"));
			}
			foreach (Uri uri in audiences)
			{
				if (uri == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
					{
						XD.SamlDictionary.Audience.Value
					}));
				}
				this.audiences.Add(uri);
			}
			this.CheckObjectValidity();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002DFFC File Offset: 0x0002C1FC
		public SamlAudienceRestrictionCondition()
		{
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002E00F File Offset: 0x0002C20F
		public IList<Uri> Audiences
		{
			get
			{
				return this.audiences;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002E017 File Offset: 0x0002C217
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002E01F File Offset: 0x0002C21F
		public override void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.audiences.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002E03B File Offset: 0x0002C23B
		private void CheckObjectValidity()
		{
			if (this.audiences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAudienceRestrictionShouldHaveOneAudience")));
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002E064 File Offset: 0x0002C264
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
			reader.MoveToContent();
			reader.Read();
			while (reader.IsStartElement())
			{
				if (!reader.IsStartElement(samlDictionary.Audience, samlDictionary.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
					{
						samlDictionary.AudienceRestrictionCondition.Value
					})));
				}
				reader.MoveToContent();
				string text = reader.ReadString();
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAudienceRestrictionInvalidAudienceValueOnRead")));
				}
				this.audiences.Add(new Uri(text));
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			if (this.audiences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAudienceRestrictionShouldHaveOneAudienceOnRead")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002E194 File Offset: 0x0002C394
		public override void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
		{
			this.CheckObjectValidity();
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (samlSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("samlSerializer"));
			}
			SamlDictionary samlDictionary = samlSerializer.DictionaryManager.SamlDictionary;
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.AudienceRestrictionCondition, samlDictionary.Namespace);
			for (int i = 0; i < this.audiences.Count; i++)
			{
				writer.WriteElementString(samlDictionary.Audience, samlDictionary.Namespace, this.audiences[i].AbsoluteUri);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BA7 RID: 2983
		private readonly ImmutableCollection<Uri> audiences = new ImmutableCollection<Uri>();

		// Token: 0x04000BA8 RID: 2984
		private bool isReadOnly;
	}
}

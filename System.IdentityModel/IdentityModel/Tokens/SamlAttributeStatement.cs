using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000150 RID: 336
	public class SamlAttributeStatement : SamlSubjectStatement
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x0002DB65 File Offset: 0x0002BD65
		public SamlAttributeStatement()
		{
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002DB78 File Offset: 0x0002BD78
		public SamlAttributeStatement(SamlSubject samlSubject, IEnumerable<SamlAttribute> attributes) : base(samlSubject)
		{
			if (attributes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("attributes"));
			}
			foreach (SamlAttribute samlAttribute in attributes)
			{
				if (samlAttribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
					{
						XD.SamlDictionary.Attribute.Value
					}));
				}
				this.attributes.Add(samlAttribute);
			}
			this.CheckObjectValidity();
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002DC28 File Offset: 0x0002BE28
		public IList<SamlAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002DC30 File Offset: 0x0002BE30
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002DC38 File Offset: 0x0002BE38
		public override void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				foreach (SamlAttribute samlAttribute in this.attributes)
				{
					samlAttribute.MakeReadOnly();
				}
				this.attributes.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		private void CheckObjectValidity()
		{
			if (base.SamlSubject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLSubjectStatementRequiresSubject")));
			}
			if (this.attributes.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeShouldHaveOneValue")));
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002DCF8 File Offset: 0x0002BEF8
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
			if (!reader.IsStartElement(samlDictionary.Subject, samlDictionary.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeStatementMissingSubjectOnRead")));
			}
			SamlSubject samlSubject = new SamlSubject();
			samlSubject.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
			base.SamlSubject = samlSubject;
			while (reader.IsStartElement() && reader.IsStartElement(samlDictionary.Attribute, samlDictionary.Namespace))
			{
				SamlAttribute samlAttribute = samlSerializer.LoadAttribute(reader, keyInfoSerializer, outOfBandTokenResolver);
				if (samlAttribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLUnableToLoadAttribute")));
				}
				this.attributes.Add(samlAttribute);
			}
			if (this.attributes.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeStatementMissingAttributeOnRead")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002DE1C File Offset: 0x0002C01C
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.AttributeStatement, samlDictionary.Namespace);
			base.SamlSubject.WriteXml(writer, samlSerializer, keyInfoSerializer);
			for (int i = 0; i < this.attributes.Count; i++)
			{
				this.attributes[i].WriteXml(writer, samlSerializer, keyInfoSerializer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002DEC8 File Offset: 0x0002C0C8
		protected override void AddClaimsToList(IList<Claim> claims)
		{
			if (claims == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claims");
			}
			for (int i = 0; i < this.attributes.Count; i++)
			{
				if (this.attributes[i] != null)
				{
					ReadOnlyCollection<Claim> readOnlyCollection = this.attributes[i].ExtractClaims();
					if (readOnlyCollection != null)
					{
						for (int j = 0; j < readOnlyCollection.Count; j++)
						{
							if (readOnlyCollection[j] != null)
							{
								claims.Add(readOnlyCollection[j]);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000BA5 RID: 2981
		private readonly ImmutableCollection<SamlAttribute> attributes = new ImmutableCollection<SamlAttribute>();

		// Token: 0x04000BA6 RID: 2982
		private bool isReadOnly;
	}
}

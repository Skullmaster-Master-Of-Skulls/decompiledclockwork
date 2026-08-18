using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200015B RID: 347
	public class SamlEvidence
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x0002FF5C File Offset: 0x0002E15C
		public SamlEvidence(IEnumerable<string> assertionIdReferences) : this(assertionIdReferences, null)
		{
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002FF66 File Offset: 0x0002E166
		public SamlEvidence(IEnumerable<SamlAssertion> assertions) : this(null, assertions)
		{
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002FF70 File Offset: 0x0002E170
		public SamlEvidence(IEnumerable<string> assertionIdReferences, IEnumerable<SamlAssertion> assertions)
		{
			this.assertionIdReferences = new ImmutableCollection<string>();
			this.assertions = new ImmutableCollection<SamlAssertion>();
			base..ctor();
			if (assertionIdReferences == null && assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEvidenceShouldHaveOneAssertion"));
			}
			if (assertionIdReferences != null)
			{
				foreach (string text in assertionIdReferences)
				{
					if (string.IsNullOrEmpty(text))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
						{
							XD.SamlDictionary.AssertionIdReference.Value
						}));
					}
					this.assertionIdReferences.Add(text);
				}
			}
			if (assertions != null)
			{
				foreach (SamlAssertion samlAssertion in assertions)
				{
					if (samlAssertion == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLEntityCannotBeNullOrEmpty", new object[]
						{
							XD.SamlDictionary.Assertion.Value
						}));
					}
					this.assertions.Add(samlAssertion);
				}
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0003009C File Offset: 0x0002E29C
		public SamlEvidence()
		{
			this.assertionIdReferences = new ImmutableCollection<string>();
			this.assertions = new ImmutableCollection<SamlAssertion>();
			base..ctor();
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000300BA File Offset: 0x0002E2BA
		public IList<string> AssertionIdReferences
		{
			get
			{
				return this.assertionIdReferences;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000300C2 File Offset: 0x0002E2C2
		public IList<SamlAssertion> Assertions
		{
			get
			{
				return this.assertions;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000300CA File Offset: 0x0002E2CA
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000300D4 File Offset: 0x0002E2D4
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				foreach (SamlAssertion samlAssertion in this.assertions)
				{
					samlAssertion.MakeReadOnly();
				}
				this.assertionIdReferences.MakeReadOnly();
				this.assertions.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00030148 File Offset: 0x0002E348
		private void CheckObjectValidity()
		{
			if (this.assertions.Count == 0 && this.assertionIdReferences.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLEvidenceShouldHaveOneAssertion")));
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00030180 File Offset: 0x0002E380
		public virtual void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver)
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
				if (reader.IsStartElement(samlDictionary.AssertionIdReference, samlDictionary.Namespace))
				{
					reader.MoveToContent();
					this.assertionIdReferences.Add(reader.ReadString());
					reader.ReadEndElement();
				}
				else
				{
					if (!reader.IsStartElement(samlDictionary.Assertion, samlDictionary.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
						{
							samlDictionary.Evidence.Value
						})));
					}
					SamlAssertion samlAssertion = new SamlAssertion();
					samlAssertion.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
					this.assertions.Add(samlAssertion);
				}
			}
			if (this.assertionIdReferences.Count == 0 && this.assertions.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLEvidenceShouldHaveOneAssertionOnRead")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000302C0 File Offset: 0x0002E4C0
		public virtual void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Evidence.Value, samlDictionary.Namespace.Value);
			for (int i = 0; i < this.assertionIdReferences.Count; i++)
			{
				writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.AssertionIdReference, samlDictionary.Namespace);
				writer.WriteString(this.assertionIdReferences[i]);
				writer.WriteEndElement();
			}
			for (int j = 0; j < this.assertions.Count; j++)
			{
				this.assertions[j].WriteXml(writer, samlSerializer, keyInfoSerializer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BCA RID: 3018
		private readonly ImmutableCollection<string> assertionIdReferences;

		// Token: 0x04000BCB RID: 3019
		private readonly ImmutableCollection<SamlAssertion> assertions;

		// Token: 0x04000BCC RID: 3020
		private bool isReadOnly;
	}
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200014B RID: 331
	public class SamlAdvice
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x0002BBF1 File Offset: 0x00029DF1
		public SamlAdvice() : this(null, null)
		{
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002BBFB File Offset: 0x00029DFB
		public SamlAdvice(IEnumerable<string> references) : this(references, null)
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002BC05 File Offset: 0x00029E05
		public SamlAdvice(IEnumerable<SamlAssertion> assertions) : this(null, assertions)
		{
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002BC10 File Offset: 0x00029E10
		public SamlAdvice(IEnumerable<string> references, IEnumerable<SamlAssertion> assertions)
		{
			if (references != null)
			{
				foreach (string text in references)
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002BD20 File Offset: 0x00029F20
		public IList<string> AssertionIdReferences
		{
			get
			{
				return this.assertionIdReferences;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002BD28 File Offset: 0x00029F28
		public IList<SamlAssertion> Assertions
		{
			get
			{
				return this.assertions;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002BD30 File Offset: 0x00029F30
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002BD38 File Offset: 0x00029F38
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.assertionIdReferences.MakeReadOnly();
				foreach (SamlAssertion samlAssertion in this.assertions)
				{
					samlAssertion.MakeReadOnly();
				}
				this.assertions.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002BDAC File Offset: 0x00029FAC
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
			if (reader.IsEmptyElement)
			{
				reader.MoveToContent();
				reader.Read();
				return;
			}
			reader.MoveToContent();
			reader.Read();
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement(samlDictionary.AssertionIdReference, samlDictionary.Namespace))
				{
					reader.MoveToContent();
					this.assertionIdReferences.Add(reader.ReadString());
					reader.MoveToContent();
					reader.ReadEndElement();
				}
				else
				{
					if (!reader.IsStartElement(samlDictionary.Assertion, samlDictionary.Namespace))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLBadSchema", new object[]
						{
							samlDictionary.Advice.Value
						})));
					}
					SamlAssertion samlAssertion = new SamlAssertion();
					samlAssertion.ReadXml(reader, samlSerializer, keyInfoSerializer, outOfBandTokenResolver);
					this.assertions.Add(samlAssertion);
				}
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		public virtual void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer)
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Advice, samlDictionary.Namespace);
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

		// Token: 0x04000B81 RID: 2945
		private readonly ImmutableCollection<string> assertionIdReferences = new ImmutableCollection<string>();

		// Token: 0x04000B82 RID: 2946
		private readonly ImmutableCollection<SamlAssertion> assertions = new ImmutableCollection<SamlAssertion>();

		// Token: 0x04000B83 RID: 2947
		private bool isReadOnly;
	}
}

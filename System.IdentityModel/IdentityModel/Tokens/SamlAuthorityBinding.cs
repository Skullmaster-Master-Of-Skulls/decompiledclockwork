using System;
using System.IdentityModel.Selectors;
using System.Runtime.Serialization;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000154 RID: 340
	[DataContract]
	public class SamlAuthorityBinding
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x0002EC58 File Offset: 0x0002CE58
		public SamlAuthorityBinding(XmlQualifiedName authorityKind, string binding, string location)
		{
			this.AuthorityKind = authorityKind;
			this.Binding = binding;
			this.Location = location;
			this.CheckObjectValidity();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00004469 File Offset: 0x00002669
		public SamlAuthorityBinding()
		{
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002EC7B File Offset: 0x0002CE7B
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0002EC84 File Offset: 0x0002CE84
		[DataMember]
		public XmlQualifiedName AuthorityKind
		{
			get
			{
				return this.authorityKind;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				if (string.IsNullOrEmpty(value.Name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAuthorityKindMissingName"));
				}
				this.authorityKind = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002ECFA File Offset: 0x0002CEFA
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0002ED04 File Offset: 0x0002CF04
		[DataMember]
		public string Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAuthorityBindingRequiresBinding"));
				}
				this.binding = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002ED57 File Offset: 0x0002CF57
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0002ED60 File Offset: 0x0002CF60
		[DataMember]
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAuthorityBindingRequiresLocation"));
				}
				this.location = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0002EDB3 File Offset: 0x0002CFB3
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002EDBB File Offset: 0x0002CFBB
		public void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002EDC4 File Offset: 0x0002CFC4
		private void CheckObjectValidity()
		{
			if (this.authorityKind == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingMissingAuthorityKind")));
			}
			if (string.IsNullOrEmpty(this.authorityKind.Name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityKindMissingName")));
			}
			if (string.IsNullOrEmpty(this.binding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingRequiresBinding")));
			}
			if (string.IsNullOrEmpty(this.location))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingRequiresLocation")));
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002EE74 File Offset: 0x0002D074
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
			string attribute = reader.GetAttribute(samlDictionary.AuthorityKind, null);
			if (string.IsNullOrEmpty(attribute))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingMissingAuthorityKindOnRead")));
			}
			string[] array = attribute.Split(new char[]
			{
				':'
			});
			if (array.Length > 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingInvalidAuthorityKind")));
			}
			string prefix;
			string name;
			if (array.Length == 2)
			{
				prefix = array[0];
				name = array[1];
			}
			else
			{
				prefix = string.Empty;
				name = array[0];
			}
			string ns = reader.LookupNamespace(prefix);
			this.authorityKind = new XmlQualifiedName(name, ns);
			this.binding = reader.GetAttribute(samlDictionary.Binding, null);
			if (string.IsNullOrEmpty(this.binding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingMissingBindingOnRead")));
			}
			this.location = reader.GetAttribute(samlDictionary.Location, null);
			if (string.IsNullOrEmpty(this.location))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAuthorityBindingMissingLocationOnRead")));
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

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002EFF4 File Offset: 0x0002D1F4
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.AuthorityBinding, samlDictionary.Namespace);
			string text = null;
			if (!string.IsNullOrEmpty(this.authorityKind.Namespace))
			{
				writer.WriteAttributeString(string.Empty, samlDictionary.NamespaceAttributePrefix.Value, null, this.authorityKind.Namespace);
				text = writer.LookupPrefix(this.authorityKind.Namespace);
			}
			writer.WriteStartAttribute(samlDictionary.AuthorityKind, null);
			if (string.IsNullOrEmpty(text))
			{
				writer.WriteString(this.authorityKind.Name);
			}
			else
			{
				writer.WriteString(text + ":" + this.authorityKind.Name);
			}
			writer.WriteEndAttribute();
			writer.WriteStartAttribute(samlDictionary.Location, null);
			writer.WriteString(this.location);
			writer.WriteEndAttribute();
			writer.WriteStartAttribute(samlDictionary.Binding, null);
			writer.WriteString(this.binding);
			writer.WriteEndAttribute();
			writer.WriteEndElement();
		}

		// Token: 0x04000BB4 RID: 2996
		private XmlQualifiedName authorityKind;

		// Token: 0x04000BB5 RID: 2997
		private string binding;

		// Token: 0x04000BB6 RID: 2998
		private string location;

		// Token: 0x04000BB7 RID: 2999
		[DataMember]
		private bool isReadOnly;
	}
}

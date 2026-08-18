using System;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200014A RID: 330
	public class SamlAction
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x0002B981 File Offset: 0x00029B81
		public SamlAction(string action) : this(action, null)
		{
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002B98B File Offset: 0x00029B8B
		public SamlAction(string action, string ns)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("action", SR.GetString("SAMLActionNameRequired"));
			}
			this.action = action;
			this.ns = ns;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00004469 File Offset: 0x00002669
		public SamlAction()
		{
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002B9C3 File Offset: 0x00029BC3
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x0002B9CC File Offset: 0x00029BCC
		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("SAMLActionNameRequired"));
				}
				this.action = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002BA24 File Offset: 0x00029C24
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0002BA2C File Offset: 0x00029C2C
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.ns = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002BA57 File Offset: 0x00029C57
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002BA5F File Offset: 0x00029C5F
		public void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002BA68 File Offset: 0x00029C68
		private void CheckObjectValidity()
		{
			if (string.IsNullOrEmpty(this.action))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLActionNameRequired")));
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002BA94 File Offset: 0x00029C94
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
			if (reader.IsStartElement(samlDictionary.Action, samlDictionary.Namespace))
			{
				this.ns = reader.GetAttribute(samlDictionary.ActionNamespaceAttribute, null);
				reader.MoveToContent();
				this.action = reader.ReadString();
				if (string.IsNullOrEmpty(this.action))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLActionNameRequiredOnRead")));
				}
				reader.MoveToContent();
				reader.ReadEndElement();
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002BB4C File Offset: 0x00029D4C
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Action, samlDictionary.Namespace);
			if (this.ns != null)
			{
				writer.WriteStartAttribute(samlDictionary.ActionNamespaceAttribute, null);
				writer.WriteString(this.ns);
				writer.WriteEndAttribute();
			}
			writer.WriteString(this.action);
			writer.WriteEndElement();
		}

		// Token: 0x04000B7E RID: 2942
		private string ns;

		// Token: 0x04000B7F RID: 2943
		private string action;

		// Token: 0x04000B80 RID: 2944
		private bool isReadOnly;
	}
}

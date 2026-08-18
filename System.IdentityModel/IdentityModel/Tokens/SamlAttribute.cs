using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200014E RID: 334
	public class SamlAttribute
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x0002D2D9 File Offset: 0x0002B4D9
		public SamlAttribute()
		{
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
		public SamlAttribute(string attributeNamespace, string attributeName, IEnumerable<string> attributeValues)
		{
			if (string.IsNullOrEmpty(attributeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeNameAttributeRequired"));
			}
			if (string.IsNullOrEmpty(attributeNamespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeNamespaceAttributeRequired"));
			}
			if (attributeValues == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attributeValues");
			}
			this.name = StringUtil.OptimizeString(attributeName);
			this.nameSpace = StringUtil.OptimizeString(attributeNamespace);
			this.claimType = (string.IsNullOrEmpty(this.nameSpace) ? this.name : (this.nameSpace + "/" + this.name));
			foreach (string text in attributeValues)
			{
				if (text == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeValueCannotBeNull"));
				}
				this.attributeValues.Add(text);
			}
			if (this.attributeValues.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeShouldHaveOneValue"));
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002D434 File Offset: 0x0002B634
		public SamlAttribute(Claim claim)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			if (!(claim.Resource is string))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SamlAttributeClaimResourceShouldBeAString"));
			}
			if (claim.Right != Rights.PossessProperty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SamlAttributeClaimRightShouldBePossessProperty"));
			}
			int num = claim.ClaimType.LastIndexOf('/');
			if (num == -1 || num == 0 || num == claim.ClaimType.Length - 1)
			{
				this.nameSpace = string.Empty;
				this.name = claim.ClaimType;
			}
			else
			{
				this.nameSpace = StringUtil.OptimizeString(claim.ClaimType.Substring(0, num));
				this.name = StringUtil.OptimizeString(claim.ClaimType.Substring(num + 1, claim.ClaimType.Length - (num + 1)));
			}
			this.claimType = claim.ClaimType;
			this.attributeValues.Add(claim.Resource as string);
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002D559 File Offset: 0x0002B759
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0002D564 File Offset: 0x0002B764
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeNameAttributeRequired"));
				}
				this.name = StringUtil.OptimizeString(value);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002D5BC File Offset: 0x0002B7BC
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
		public string Namespace
		{
			get
			{
				return this.nameSpace;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeNamespaceAttributeRequired"));
				}
				this.nameSpace = StringUtil.OptimizeString(value);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002D61C File Offset: 0x0002B81C
		public IList<string> AttributeValues
		{
			get
			{
				return this.attributeValues;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002D624 File Offset: 0x0002B824
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0002D62C File Offset: 0x0002B82C
		public string OriginalIssuer
		{
			get
			{
				return this.originalIssuer;
			}
			set
			{
				if (value == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4251"));
				}
				this.originalIssuer = StringUtil.OptimizeString(value);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002D661 File Offset: 0x0002B861
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0002D66C File Offset: 0x0002B86C
		public string AttributeValueXsiType
		{
			get
			{
				return this.attributeValueXsiType;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				int num = value.IndexOf('#');
				if (num == -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				string text = value.Substring(0, num);
				if (text.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				string text2 = value.Substring(num + 1);
				if (text2.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				this.attributeValueXsiType = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002D720 File Offset: 0x0002B920
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002D728 File Offset: 0x0002B928
		public void MakeReadOnly()
		{
			if (!this.isReadOnly)
			{
				this.attributeValues.MakeReadOnly();
				this.isReadOnly = true;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002D744 File Offset: 0x0002B944
		public virtual ReadOnlyCollection<Claim> ExtractClaims()
		{
			if (this.claims == null)
			{
				List<Claim> list = new List<Claim>(this.attributeValues.Count);
				for (int i = 0; i < this.attributeValues.Count; i++)
				{
					if (this.attributeValues[i] == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeValueCannotBeNull"));
					}
					list.Add(new Claim(this.claimType, this.attributeValues[i], Rights.PossessProperty));
				}
				this.claims = list;
			}
			return this.claims.AsReadOnly();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002D7D8 File Offset: 0x0002B9D8
		private void CheckObjectValidity()
		{
			if (string.IsNullOrEmpty(this.name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeNameAttributeRequired")));
			}
			if (string.IsNullOrEmpty(this.nameSpace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeNamespaceAttributeRequired")));
			}
			if (this.attributeValues.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeShouldHaveOneValue")));
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002D85C File Offset: 0x0002BA5C
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
			this.name = reader.GetAttribute(samlDictionary.AttributeName, null);
			if (string.IsNullOrEmpty(this.name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeMissingNameAttributeOnRead")));
			}
			this.nameSpace = reader.GetAttribute(samlDictionary.AttributeNamespace, null);
			if (string.IsNullOrEmpty(this.nameSpace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeMissingNamespaceAttributeOnRead")));
			}
			this.claimType = (string.IsNullOrEmpty(this.nameSpace) ? this.name : (this.nameSpace + "/" + this.name));
			reader.MoveToContent();
			reader.Read();
			while (reader.IsStartElement(samlDictionary.AttributeValue, samlDictionary.Namespace))
			{
				string item = reader.ReadString();
				this.attributeValues.Add(item);
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			if (this.attributeValues.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SAMLAttributeShouldHaveOneValue")));
			}
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
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
			writer.WriteStartElement(samlDictionary.PreferredPrefix.Value, samlDictionary.Attribute, samlDictionary.Namespace);
			writer.WriteStartAttribute(samlDictionary.AttributeName, null);
			writer.WriteString(this.name);
			writer.WriteEndAttribute();
			writer.WriteStartAttribute(samlDictionary.AttributeNamespace, null);
			writer.WriteString(this.nameSpace);
			writer.WriteEndAttribute();
			for (int i = 0; i < this.attributeValues.Count; i++)
			{
				if (this.attributeValues[i] == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SAMLAttributeValueCannotBeNull"));
				}
				writer.WriteElementString(samlDictionary.PreferredPrefix.Value, samlDictionary.AttributeValue, samlDictionary.Namespace, this.attributeValues[i]);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000B9D RID: 2973
		private string name;

		// Token: 0x04000B9E RID: 2974
		private string nameSpace;

		// Token: 0x04000B9F RID: 2975
		private readonly ImmutableCollection<string> attributeValues = new ImmutableCollection<string>();

		// Token: 0x04000BA0 RID: 2976
		private string originalIssuer;

		// Token: 0x04000BA1 RID: 2977
		private string attributeValueXsiType = "http://www.w3.org/2001/XMLSchema#string";

		// Token: 0x04000BA2 RID: 2978
		private List<Claim> claims;

		// Token: 0x04000BA3 RID: 2979
		private string claimType;

		// Token: 0x04000BA4 RID: 2980
		private bool isReadOnly;
	}
}

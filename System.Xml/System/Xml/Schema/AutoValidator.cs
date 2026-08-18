using System;

namespace System.Xml.Schema
{
	// Token: 0x02000184 RID: 388
	internal class AutoValidator : BaseValidator
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x00057CFA File Offset: 0x00056CFA
		public AutoValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, ValidationEventHandler eventHandler) : base(reader, schemaCollection, eventHandler)
		{
			this.schemaInfo = new SchemaInfo();
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00057D10 File Offset: 0x00056D10
		public override bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00057D14 File Offset: 0x00056D14
		public override void Validate()
		{
			switch (this.DetectValidationType())
			{
			case ValidationType.Auto:
			case ValidationType.DTD:
				break;
			case ValidationType.XDR:
				this.reader.Validator = new XdrValidator(this);
				this.reader.Validator.Validate();
				return;
			case ValidationType.Schema:
				this.reader.Validator = new XsdValidator(this);
				this.reader.Validator.Validate();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00057D86 File Offset: 0x00056D86
		public override void CompleteValidation()
		{
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00057D88 File Offset: 0x00056D88
		public override object FindId(string name)
		{
			return null;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00057D8C File Offset: 0x00056D8C
		private ValidationType DetectValidationType()
		{
			if (this.reader.Schemas != null && this.reader.Schemas.Count > 0)
			{
				XmlSchemaCollectionEnumerator enumerator = this.reader.Schemas.GetEnumerator();
				while (enumerator.MoveNext())
				{
					XmlSchemaCollectionNode currentNode = enumerator.CurrentNode;
					SchemaInfo schemaInfo = currentNode.SchemaInfo;
					if (schemaInfo.SchemaType == SchemaType.XSD)
					{
						return ValidationType.Schema;
					}
					if (schemaInfo.SchemaType == SchemaType.XDR)
					{
						return ValidationType.XDR;
					}
				}
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				SchemaType schemaType = base.SchemaNames.SchemaTypeFromRoot(this.reader.LocalName, this.reader.NamespaceURI);
				if (schemaType == SchemaType.XSD)
				{
					return ValidationType.Schema;
				}
				if (schemaType == SchemaType.XDR)
				{
					return ValidationType.XDR;
				}
				int attributeCount = this.reader.AttributeCount;
				for (int i = 0; i < attributeCount; i++)
				{
					this.reader.MoveToAttribute(i);
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						if (XdrBuilder.IsXdrSchema(this.reader.Value))
						{
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
					else
					{
						if (Ref.Equal(namespaceURI, base.SchemaNames.NsXsi))
						{
							this.reader.MoveToElement();
							return ValidationType.Schema;
						}
						if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
						{
							this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
				}
				if (attributeCount > 0)
				{
					this.reader.MoveToElement();
				}
			}
			return ValidationType.Auto;
		}

		// Token: 0x04000C79 RID: 3193
		private const string x_schema = "x-schema";
	}
}

using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200035A RID: 858
	internal sealed class DocumentationElement : SchemaElement
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x00092BA6 File Offset: 0x00090DA6
		public DocumentationElement(SchemaElement parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00092BBB File Offset: 0x00090DBB
		public Documentation MetadataDocumentation
		{
			get
			{
				this._metdataDocumentation.SetReadOnly();
				return this._metdataDocumentation;
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00092BCE File Offset: 0x00090DCE
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Summary"))
			{
				this.HandleSummaryElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "LongDescription"))
			{
				this.HandleLongDescriptionElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00092C0C File Offset: 0x00090E0C
		protected override bool HandleText(XmlReader reader)
		{
			string value = reader.Value;
			if (!string.IsNullOrWhiteSpace(value))
			{
				base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, Strings.InvalidDocumentationBothTextAndStructure);
			}
			return true;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00092C38 File Offset: 0x00090E38
		private void HandleSummaryElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.Summary = textElement.Value;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00092C64 File Offset: 0x00090E64
		private void HandleLongDescriptionElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.LongDescription = textElement.Value;
		}

		// Token: 0x04000A78 RID: 2680
		private readonly Documentation _metdataDocumentation = new Documentation();
	}
}

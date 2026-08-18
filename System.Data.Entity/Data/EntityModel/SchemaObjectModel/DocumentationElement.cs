using System;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E2 RID: 738
	internal sealed class DocumentationElement : SchemaElement
	{
		// Token: 0x06002C55 RID: 11349 RVA: 0x000A877B File Offset: 0x000A697B
		public DocumentationElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000A878F File Offset: 0x000A698F
		public Documentation MetadataDocumentation
		{
			get
			{
				this._metdataDocumentation.SetReadOnly();
				return this._metdataDocumentation;
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000A87A2 File Offset: 0x000A69A2
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

		// Token: 0x06002C58 RID: 11352 RVA: 0x000A87E0 File Offset: 0x000A69E0
		protected override bool HandleText(XmlReader reader)
		{
			string value = reader.Value;
			if (!StringUtil.IsNullOrEmptyOrWhiteSpace(value))
			{
				base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, Strings.InvalidDocumentationBothTextAndStructure);
			}
			return true;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000A880C File Offset: 0x000A6A0C
		private void HandleSummaryElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.Summary = textElement.Value;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000A8838 File Offset: 0x000A6A38
		private void HandleLongDescriptionElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.LongDescription = textElement.Value;
		}

		// Token: 0x04001301 RID: 4865
		private Documentation _metdataDocumentation = new Documentation();
	}
}

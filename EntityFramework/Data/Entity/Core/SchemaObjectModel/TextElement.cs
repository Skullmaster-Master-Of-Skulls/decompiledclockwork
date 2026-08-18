using System;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000395 RID: 917
	internal sealed class TextElement : SchemaElement
	{
		// Token: 0x0600211A RID: 8474 RVA: 0x0009B90A File Offset: 0x00099B0A
		public TextElement(SchemaElement parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0009B914 File Offset: 0x00099B14
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x0009B91C File Offset: 0x00099B1C
		public string Value { get; private set; }

		// Token: 0x0600211D RID: 8477 RVA: 0x0009B925 File Offset: 0x00099B25
		protected override bool HandleText(XmlReader reader)
		{
			this.TextElementTextHandler(reader);
			return true;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0009B930 File Offset: 0x00099B30
		private void TextElementTextHandler(XmlReader reader)
		{
			string value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (string.IsNullOrEmpty(this.Value))
			{
				this.Value = value;
				return;
			}
			this.Value += value;
		}
	}
}

using System;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000318 RID: 792
	internal sealed class TextElement : SchemaElement
	{
		// Token: 0x06002ED8 RID: 11992 RVA: 0x000A9632 File Offset: 0x000A7832
		public TextElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000B0F6D File Offset: 0x000AF16D
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x000B0F75 File Offset: 0x000AF175
		public string Value
		{
			get
			{
				return this._value;
			}
			private set
			{
				this._value = value;
			}
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000B0F7E File Offset: 0x000AF17E
		protected override bool HandleText(XmlReader reader)
		{
			this.TextElementTextHandler(reader);
			return true;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000B0F88 File Offset: 0x000AF188
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

		// Token: 0x04001443 RID: 5187
		private string _value;
	}
}

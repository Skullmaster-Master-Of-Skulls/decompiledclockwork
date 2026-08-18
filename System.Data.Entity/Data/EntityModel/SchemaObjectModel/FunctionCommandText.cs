using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002EE RID: 750
	internal sealed class FunctionCommandText : SchemaElement
	{
		// Token: 0x06002CD0 RID: 11472 RVA: 0x000A9632 File Offset: 0x000A7832
		public FunctionCommandText(Function parentElement) : base(parentElement)
		{
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000AA326 File Offset: 0x000A8526
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000AA32E File Offset: 0x000A852E
		protected override bool HandleText(XmlReader reader)
		{
			this._commandText = reader.Value;
			return true;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000AA33D File Offset: 0x000A853D
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._commandText))
			{
				base.AddError(ErrorCode.EmptyCommandText, EdmSchemaErrorSeverity.Error, Strings.EmptyCommandText);
			}
		}

		// Token: 0x040013B6 RID: 5046
		private string _commandText;
	}
}

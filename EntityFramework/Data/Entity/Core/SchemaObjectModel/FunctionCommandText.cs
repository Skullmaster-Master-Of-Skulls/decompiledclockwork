using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000368 RID: 872
	internal sealed class FunctionCommandText : SchemaElement
	{
		// Token: 0x06001F4F RID: 8015 RVA: 0x000953CC File Offset: 0x000935CC
		public FunctionCommandText(Function parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x000953D6 File Offset: 0x000935D6
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x000953DE File Offset: 0x000935DE
		protected override bool HandleText(XmlReader reader)
		{
			this._commandText = reader.Value;
			return true;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x000953ED File Offset: 0x000935ED
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this._commandText))
			{
				base.AddError(ErrorCode.EmptyCommandText, EdmSchemaErrorSeverity.Error, Strings.EmptyCommandText);
			}
		}

		// Token: 0x04000B3A RID: 2874
		private string _commandText;
	}
}

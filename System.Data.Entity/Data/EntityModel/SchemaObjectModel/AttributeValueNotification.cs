using System;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000311 RID: 785
	// (Invoke) Token: 0x06002E8E RID: 11918
	internal delegate void AttributeValueNotification(string token, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}

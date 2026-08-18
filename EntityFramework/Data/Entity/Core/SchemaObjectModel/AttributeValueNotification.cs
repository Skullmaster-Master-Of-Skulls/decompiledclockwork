using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200038F RID: 911
	// (Invoke) Token: 0x060020EA RID: 8426
	internal delegate void AttributeValueNotification(string token, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}

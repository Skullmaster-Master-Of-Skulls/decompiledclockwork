using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000390 RID: 912
	// (Invoke) Token: 0x060020EE RID: 8430
	internal delegate DbProviderManifest ProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}

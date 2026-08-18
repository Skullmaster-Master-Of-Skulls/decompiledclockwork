using System;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000312 RID: 786
	// (Invoke) Token: 0x06002E92 RID: 11922
	internal delegate DbProviderManifest ProviderManifestNeeded(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError);
}

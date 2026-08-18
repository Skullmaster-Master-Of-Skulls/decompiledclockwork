using System;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000101 RID: 257
	public abstract class JavaScriptTypeResolver
	{
		// Token: 0x06000DC0 RID: 3520
		public abstract Type ResolveType(string id);

		// Token: 0x06000DC1 RID: 3521
		public abstract string ResolveTypeId(Type type);
	}
}

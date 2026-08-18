using System;
using System.Collections.Generic;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000104 RID: 260
	internal class DictionaryTypeResolver : JavaScriptTypeResolver
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00031039 File Offset: 0x0002F239
		public override Type ResolveType(string id)
		{
			return typeof(Dictionary<string, object>);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00031045 File Offset: 0x0002F245
		public override string ResolveTypeId(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.AssemblyQualifiedName;
		}
	}
}

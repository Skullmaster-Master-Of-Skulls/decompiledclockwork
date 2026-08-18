using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000353 RID: 851
	internal sealed class MetadataFunctionGroup : MetadataMember
	{
		// Token: 0x06003199 RID: 12697 RVA: 0x000C2D75 File Offset: 0x000C0F75
		internal MetadataFunctionGroup(string name, IList<EdmFunction> functionMetadata) : base(MetadataMemberClass.FunctionGroup, name)
		{
			this.FunctionMetadata = functionMetadata;
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000C2D86 File Offset: 0x000C0F86
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataFunctionGroup.FunctionGroupClassName;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000C2D8D File Offset: 0x000C0F8D
		internal static string FunctionGroupClassName
		{
			get
			{
				return Strings.LocalizedFunction;
			}
		}

		// Token: 0x04001593 RID: 5523
		internal readonly IList<EdmFunction> FunctionMetadata;
	}
}

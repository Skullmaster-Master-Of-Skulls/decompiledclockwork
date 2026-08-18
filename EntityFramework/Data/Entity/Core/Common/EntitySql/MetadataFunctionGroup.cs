using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200025F RID: 607
	internal sealed class MetadataFunctionGroup : MetadataMember
	{
		// Token: 0x060014F0 RID: 5360 RVA: 0x0006304E File Offset: 0x0006124E
		internal MetadataFunctionGroup(string name, IList<EdmFunction> functionMetadata) : base(MetadataMemberClass.FunctionGroup, name)
		{
			this.FunctionMetadata = functionMetadata;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0006305F File Offset: 0x0006125F
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataFunctionGroup.FunctionGroupClassName;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00063066 File Offset: 0x00061266
		internal static string FunctionGroupClassName
		{
			get
			{
				return Strings.LocalizedFunction;
			}
		}

		// Token: 0x0400073C RID: 1852
		internal readonly IList<EdmFunction> FunctionMetadata;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200025B RID: 603
	internal sealed class InlineFunctionGroup : MetadataMember
	{
		// Token: 0x060014E6 RID: 5350 RVA: 0x00062FCE File Offset: 0x000611CE
		internal InlineFunctionGroup(string name, IList<InlineFunctionInfo> functionMetadata) : base(MetadataMemberClass.InlineFunctionGroup, name)
		{
			this.FunctionMetadata = functionMetadata;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00062FDF File Offset: 0x000611DF
		internal override string MetadataMemberClassName
		{
			get
			{
				return InlineFunctionGroup.InlineFunctionGroupClassName;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00062FE6 File Offset: 0x000611E6
		internal static string InlineFunctionGroupClassName
		{
			get
			{
				return Strings.LocalizedInlineFunction;
			}
		}

		// Token: 0x04000737 RID: 1847
		internal readonly IList<InlineFunctionInfo> FunctionMetadata;
	}
}

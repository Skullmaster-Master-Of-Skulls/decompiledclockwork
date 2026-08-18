using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000354 RID: 852
	internal sealed class InlineFunctionGroup : MetadataMember
	{
		// Token: 0x0600319C RID: 12700 RVA: 0x000C2D94 File Offset: 0x000C0F94
		internal InlineFunctionGroup(string name, IList<InlineFunctionInfo> functionMetadata) : base(MetadataMemberClass.InlineFunctionGroup, name)
		{
			this.FunctionMetadata = functionMetadata;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000C2DA5 File Offset: 0x000C0FA5
		internal override string MetadataMemberClassName
		{
			get
			{
				return InlineFunctionGroup.InlineFunctionGroupClassName;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000C2DAC File Offset: 0x000C0FAC
		internal static string InlineFunctionGroupClassName
		{
			get
			{
				return Strings.LocalizedInlineFunction;
			}
		}

		// Token: 0x04001594 RID: 5524
		internal readonly IList<InlineFunctionInfo> FunctionMetadata;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003BA RID: 954
	internal sealed class FunctionImportNormalizedEntityTypeMapping
	{
		// Token: 0x060022F4 RID: 8948 RVA: 0x000A3298 File Offset: 0x000A1498
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "parent")]
		internal FunctionImportNormalizedEntityTypeMapping(FunctionImportStructuralTypeMappingKB parent, List<FunctionImportEntityTypeMappingCondition> columnConditions, BitArray impliedEntityTypes)
		{
			this.ColumnConditions = new ReadOnlyCollection<FunctionImportEntityTypeMappingCondition>(columnConditions.ToList<FunctionImportEntityTypeMappingCondition>());
			this.ImpliedEntityTypes = impliedEntityTypes;
			this.ComplementImpliedEntityTypes = new BitArray(this.ImpliedEntityTypes).Not();
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A32D0 File Offset: 0x000A14D0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Values={0}, Types={1}", new object[]
			{
				StringUtil.ToCommaSeparatedString(this.ColumnConditions),
				StringUtil.ToCommaSeparatedString(this.ImpliedEntityTypes)
			});
		}

		// Token: 0x04000C40 RID: 3136
		internal readonly ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> ColumnConditions;

		// Token: 0x04000C41 RID: 3137
		internal readonly BitArray ImpliedEntityTypes;

		// Token: 0x04000C42 RID: 3138
		internal readonly BitArray ComplementImpliedEntityTypes;
	}
}

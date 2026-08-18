using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000225 RID: 549
	internal sealed class FunctionImportNormalizedEntityTypeMapping
	{
		// Token: 0x060023AF RID: 9135 RVA: 0x000812AC File Offset: 0x0007F4AC
		internal FunctionImportNormalizedEntityTypeMapping(FunctionImportStructuralTypeMappingKB parent, List<FunctionImportEntityTypeMappingCondition> columnConditions, BitArray impliedEntityTypes)
		{
			EntityUtil.CheckArgumentNull<FunctionImportStructuralTypeMappingKB>(parent, "parent");
			EntityUtil.CheckArgumentNull<List<FunctionImportEntityTypeMappingCondition>>(columnConditions, "discriminatorValues");
			EntityUtil.CheckArgumentNull<BitArray>(impliedEntityTypes, "impliedEntityTypes");
			this.ColumnConditions = new ReadOnlyCollection<FunctionImportEntityTypeMappingCondition>(columnConditions.ToList<FunctionImportEntityTypeMappingCondition>());
			this.ImpliedEntityTypes = impliedEntityTypes;
			this.ComplementImpliedEntityTypes = new BitArray(this.ImpliedEntityTypes).Not();
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00081311 File Offset: 0x0007F511
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Values={0}, Types={1}", new object[]
			{
				StringUtil.ToCommaSeparatedString(this.ColumnConditions),
				StringUtil.ToCommaSeparatedString(this.ImpliedEntityTypes)
			});
		}

		// Token: 0x04000FCF RID: 4047
		internal readonly ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> ColumnConditions;

		// Token: 0x04000FD0 RID: 4048
		internal readonly BitArray ImpliedEntityTypes;

		// Token: 0x04000FD1 RID: 4049
		internal readonly BitArray ComplementImpliedEntityTypes;
	}
}

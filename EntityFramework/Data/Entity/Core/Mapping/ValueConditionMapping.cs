using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000012 RID: 18
	public class ValueConditionMapping : ConditionPropertyMapping
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public ValueConditionMapping(EdmProperty propertyOrColumn, object value) : base(Check.NotNull<EdmProperty>(propertyOrColumn, "propertyOrColumn"), Check.NotNull<object>(value, "value"), null)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004BE2 File Offset: 0x00002DE2
		public new object Value
		{
			get
			{
				return base.Value;
			}
		}
	}
}

using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000011 RID: 17
	public class IsNullConditionMapping : ConditionPropertyMapping
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004B7A File Offset: 0x00002D7A
		public IsNullConditionMapping(EdmProperty propertyOrColumn, bool isNull) : base(Check.NotNull<EdmProperty>(propertyOrColumn, "propertyOrColumn"), null, new bool?(isNull))
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004B94 File Offset: 0x00002D94
		public new bool IsNull
		{
			get
			{
				return base.IsNull.Value;
			}
		}
	}
}

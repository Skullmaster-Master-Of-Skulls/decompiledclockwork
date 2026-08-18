using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000490 RID: 1168
	internal class KeyConstraint<TCellRelation, TSlot> : InternalBase where TCellRelation : CellRelation
	{
		// Token: 0x06002B29 RID: 11049 RVA: 0x000D0BB8 File Offset: 0x000CEDB8
		internal KeyConstraint(TCellRelation relation, IEnumerable<TSlot> keySlots, IEqualityComparer<TSlot> comparer)
		{
			this.m_relation = relation;
			this.m_keySlots = new Set<TSlot>(keySlots, comparer).MakeReadOnly();
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000D0BD9 File Offset: 0x000CEDD9
		protected TCellRelation CellRelation
		{
			get
			{
				return this.m_relation;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000D0BE1 File Offset: 0x000CEDE1
		protected Set<TSlot> KeySlots
		{
			get
			{
				return this.m_keySlots;
			}
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000D0BEC File Offset: 0x000CEDEC
		internal override void ToCompactString(StringBuilder builder)
		{
			string format = "Key (V{0}) - ";
			object[] array = new object[1];
			object[] array2 = array;
			int num = 0;
			TCellRelation relation = this.m_relation;
			array2[num] = relation.CellNumber;
			StringUtil.FormatStringBuilder(builder, format, array);
			StringUtil.ToSeparatedStringSorted(builder, this.KeySlots, ", ");
		}

		// Token: 0x04000FF3 RID: 4083
		private readonly TCellRelation m_relation;

		// Token: 0x04000FF4 RID: 4084
		private readonly Set<TSlot> m_keySlots;
	}
}

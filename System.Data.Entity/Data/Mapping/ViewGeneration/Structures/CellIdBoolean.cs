using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A3 RID: 675
	internal class CellIdBoolean : TrueFalseLiteral
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x0009BB5F File Offset: 0x00099D5F
		internal CellIdBoolean(CqlIdentifiers identifiers, int index)
		{
			this.m_index = index;
			this.m_slotName = identifiers.GetFromVariable(index);
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x0009BB7B File Offset: 0x00099D7B
		internal string SlotName
		{
			get
			{
				return this.m_slotName;
			}
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0009BB84 File Offset: 0x00099D84
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, this.SlotName);
			builder.Append(qualifiedName);
			return builder;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0009BBA7 File Offset: 0x00099DA7
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return row.Property(this.SlotName);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0009BBB5 File Offset: 0x00099DB5
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.AsEsql(builder, blockAlias, skipIsNotNull);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0009BBC0 File Offset: 0x00099DC0
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			builder.Append("NOT(");
			builder = this.AsUserString(builder, blockAlias, skipIsNotNull);
			builder.Append(")");
			return builder;
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0009BBE8 File Offset: 0x00099DE8
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			int numBoolSlots = requiredSlots.Length - projectedSlotMap.Count;
			int num = projectedSlotMap.BoolIndexToSlot(this.m_index, numBoolSlots);
			requiredSlots[num] = true;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0009BC14 File Offset: 0x00099E14
		protected override bool IsEqualTo(BoolLiteral right)
		{
			CellIdBoolean cellIdBoolean = right as CellIdBoolean;
			return cellIdBoolean != null && this.m_index == cellIdBoolean.m_index;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0009BC3C File Offset: 0x00099E3C
		public override int GetHashCode()
		{
			return this.m_index.GetHashCode();
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return this;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0009BC57 File Offset: 0x00099E57
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.SlotName);
		}

		// Token: 0x04001243 RID: 4675
		private readonly int m_index;

		// Token: 0x04001244 RID: 4676
		private readonly string m_slotName;
	}
}

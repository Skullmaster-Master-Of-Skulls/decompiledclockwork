using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000275 RID: 629
	internal abstract class CqlBlock : InternalBase
	{
		// Token: 0x0600263B RID: 9787 RVA: 0x00091DEA File Offset: 0x0008FFEA
		protected CqlBlock(SlotInfo[] slotInfos, List<CqlBlock> children, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum)
		{
			this.m_slots = new ReadOnlyCollection<SlotInfo>(slotInfos);
			this.m_children = new ReadOnlyCollection<CqlBlock>(children);
			this.m_whereClause = whereClause;
			this.m_blockAlias = identifiers.GetBlockAlias(blockAliasNum);
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00091E20 File Offset: 0x00090020
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x00091E28 File Offset: 0x00090028
		internal ReadOnlyCollection<SlotInfo> Slots
		{
			get
			{
				return this.m_slots;
			}
			set
			{
				this.m_slots = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00091E31 File Offset: 0x00090031
		protected ReadOnlyCollection<CqlBlock> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00091E39 File Offset: 0x00090039
		protected BoolExpression WhereClause
		{
			get
			{
				return this.m_whereClause;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00091E41 File Offset: 0x00090041
		internal string CqlAlias
		{
			get
			{
				return this.m_blockAlias;
			}
		}

		// Token: 0x06002641 RID: 9793
		internal abstract StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel);

		// Token: 0x06002642 RID: 9794
		internal abstract DbExpression AsCqt(bool isTopLevel);

		// Token: 0x06002643 RID: 9795 RVA: 0x00091E4C File Offset: 0x0009004C
		internal QualifiedSlot QualifySlotWithBlockAlias(int slotNum)
		{
			SlotInfo slotInfo = this.m_slots[slotNum];
			return new QualifiedSlot(this, slotInfo.SlotValue);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00091E72 File Offset: 0x00090072
		internal ProjectedSlot SlotValue(int slotNum)
		{
			return this.m_slots[slotNum].SlotValue;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00091E85 File Offset: 0x00090085
		internal MemberPath MemberPath(int slotNum)
		{
			return this.m_slots[slotNum].OutputMember;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00091E98 File Offset: 0x00090098
		internal bool IsProjected(int slotNum)
		{
			return this.m_slots[slotNum].IsProjected;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00091EAC File Offset: 0x000900AC
		protected void GenerateProjectionEsql(StringBuilder builder, string blockAlias, bool addNewLineAfterEachSlot, int indentLevel, bool isTopLevel)
		{
			bool flag = true;
			foreach (SlotInfo slotInfo in this.Slots)
			{
				if (slotInfo.IsRequiredByParent)
				{
					if (!flag)
					{
						builder.Append(", ");
					}
					if (addNewLineAfterEachSlot)
					{
						StringUtil.IndentNewLine(builder, indentLevel + 1);
					}
					slotInfo.AsEsql(builder, blockAlias, indentLevel);
					if (!isTopLevel && (!(slotInfo.SlotValue is QualifiedSlot) || slotInfo.IsEnforcedNotNull))
					{
						builder.Append(" AS ").Append(slotInfo.CqlFieldAlias);
					}
					flag = false;
				}
			}
			if (addNewLineAfterEachSlot)
			{
				StringUtil.IndentNewLine(builder, indentLevel);
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00091F64 File Offset: 0x00090164
		protected DbExpression GenerateProjectionCqt(DbExpression row, bool isTopLevel)
		{
			if (isTopLevel)
			{
				return (from slot in this.Slots
				where slot.IsRequiredByParent
				select slot).Single<SlotInfo>().AsCqt(row);
			}
			return DbExpressionBuilder.NewRow(from slot in this.Slots
			where slot.IsRequiredByParent
			select new KeyValuePair<string, DbExpression>(slot.CqlFieldAlias, slot.AsCqt(row)));
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00091FFC File Offset: 0x000901FC
		internal void SetJoinTreeContext(IList<string> parentQualifiers, string leafQualifier)
		{
			this.m_joinTreeContext = new CqlBlock.JoinTreeContext(parentQualifiers, leafQualifier);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0009200B File Offset: 0x0009020B
		internal DbExpression GetInput(DbExpression row)
		{
			if (this.m_joinTreeContext == null)
			{
				return row;
			}
			return this.m_joinTreeContext.FindInput(row);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00092024 File Offset: 0x00090224
		internal override void ToCompactString(StringBuilder builder)
		{
			for (int i = 0; i < this.m_slots.Count; i++)
			{
				StringUtil.FormatStringBuilder(builder, "{0}: ", new object[]
				{
					i
				});
				this.m_slots[i].ToCompactString(builder);
				builder.Append(' ');
			}
			this.m_whereClause.ToCompactString(builder);
		}

		// Token: 0x040011BB RID: 4539
		private ReadOnlyCollection<SlotInfo> m_slots;

		// Token: 0x040011BC RID: 4540
		private readonly ReadOnlyCollection<CqlBlock> m_children;

		// Token: 0x040011BD RID: 4541
		private readonly BoolExpression m_whereClause;

		// Token: 0x040011BE RID: 4542
		private readonly string m_blockAlias;

		// Token: 0x040011BF RID: 4543
		private CqlBlock.JoinTreeContext m_joinTreeContext;

		// Token: 0x020005A0 RID: 1440
		private sealed class JoinTreeContext
		{
			// Token: 0x06004047 RID: 16455 RVA: 0x000EC920 File Offset: 0x000EAB20
			internal JoinTreeContext(IList<string> parentQualifiers, string leafQualifier)
			{
				this.m_parentQualifiers = parentQualifiers;
				this.m_indexInParentQualifiers = parentQualifiers.Count;
				this.m_leafQualifier = leafQualifier;
			}

			// Token: 0x06004048 RID: 16456 RVA: 0x000EC944 File Offset: 0x000EAB44
			internal DbExpression FindInput(DbExpression row)
			{
				DbExpression instance = row;
				for (int i = this.m_parentQualifiers.Count - 1; i >= this.m_indexInParentQualifiers; i--)
				{
					instance = instance.Property(this.m_parentQualifiers[i]);
				}
				return instance.Property(this.m_leafQualifier);
			}

			// Token: 0x04001CD6 RID: 7382
			private readonly IList<string> m_parentQualifiers;

			// Token: 0x04001CD7 RID: 7383
			private readonly int m_indexInParentQualifiers;

			// Token: 0x04001CD8 RID: 7384
			private readonly string m_leafQualifier;
		}
	}
}

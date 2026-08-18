using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000426 RID: 1062
	internal abstract class CqlBlock : InternalBase
	{
		// Token: 0x06002720 RID: 10016 RVA: 0x000BDE48 File Offset: 0x000BC048
		protected CqlBlock(SlotInfo[] slotInfos, List<CqlBlock> children, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum)
		{
			this.m_slots = new ReadOnlyCollection<SlotInfo>(slotInfos);
			this.m_children = new ReadOnlyCollection<CqlBlock>(children);
			this.m_whereClause = whereClause;
			this.m_blockAlias = identifiers.GetBlockAlias(blockAliasNum);
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000BDE7E File Offset: 0x000BC07E
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x000BDE86 File Offset: 0x000BC086
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

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000BDE8F File Offset: 0x000BC08F
		protected ReadOnlyCollection<CqlBlock> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000BDE97 File Offset: 0x000BC097
		protected BoolExpression WhereClause
		{
			get
			{
				return this.m_whereClause;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000BDE9F File Offset: 0x000BC09F
		internal string CqlAlias
		{
			get
			{
				return this.m_blockAlias;
			}
		}

		// Token: 0x06002726 RID: 10022
		internal abstract StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel);

		// Token: 0x06002727 RID: 10023
		internal abstract DbExpression AsCqt(bool isTopLevel);

		// Token: 0x06002728 RID: 10024 RVA: 0x000BDEA8 File Offset: 0x000BC0A8
		internal QualifiedSlot QualifySlotWithBlockAlias(int slotNum)
		{
			SlotInfo slotInfo = this.m_slots[slotNum];
			return new QualifiedSlot(this, slotInfo.SlotValue);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000BDECE File Offset: 0x000BC0CE
		internal ProjectedSlot SlotValue(int slotNum)
		{
			return this.m_slots[slotNum].SlotValue;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000BDEE1 File Offset: 0x000BC0E1
		internal MemberPath MemberPath(int slotNum)
		{
			return this.m_slots[slotNum].OutputMember;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000BDEF4 File Offset: 0x000BC0F4
		internal bool IsProjected(int slotNum)
		{
			return this.m_slots[slotNum].IsProjected;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000BDF08 File Offset: 0x000BC108
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

		// Token: 0x0600272D RID: 10029 RVA: 0x000BDFF4 File Offset: 0x000BC1F4
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

		// Token: 0x0600272E RID: 10030 RVA: 0x000BE08F File Offset: 0x000BC28F
		internal void SetJoinTreeContext(IList<string> parentQualifiers, string leafQualifier)
		{
			this.m_joinTreeContext = new CqlBlock.JoinTreeContext(parentQualifiers, leafQualifier);
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000BE09E File Offset: 0x000BC29E
		internal DbExpression GetInput(DbExpression row)
		{
			if (this.m_joinTreeContext == null)
			{
				return row;
			}
			return this.m_joinTreeContext.FindInput(row);
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000BE0B8 File Offset: 0x000BC2B8
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

		// Token: 0x04000EB0 RID: 3760
		private ReadOnlyCollection<SlotInfo> m_slots;

		// Token: 0x04000EB1 RID: 3761
		private readonly ReadOnlyCollection<CqlBlock> m_children;

		// Token: 0x04000EB2 RID: 3762
		private readonly BoolExpression m_whereClause;

		// Token: 0x04000EB3 RID: 3763
		private readonly string m_blockAlias;

		// Token: 0x04000EB4 RID: 3764
		private CqlBlock.JoinTreeContext m_joinTreeContext;

		// Token: 0x02000427 RID: 1063
		private sealed class JoinTreeContext
		{
			// Token: 0x06002733 RID: 10035 RVA: 0x000BE11F File Offset: 0x000BC31F
			internal JoinTreeContext(IList<string> parentQualifiers, string leafQualifier)
			{
				this.m_parentQualifiers = parentQualifiers;
				this.m_indexInParentQualifiers = parentQualifiers.Count;
				this.m_leafQualifier = leafQualifier;
			}

			// Token: 0x06002734 RID: 10036 RVA: 0x000BE144 File Offset: 0x000BC344
			internal DbExpression FindInput(DbExpression row)
			{
				DbExpression instance = row;
				for (int i = this.m_parentQualifiers.Count - 1; i >= this.m_indexInParentQualifiers; i--)
				{
					instance = instance.Property(this.m_parentQualifiers[i]);
				}
				return instance.Property(this.m_leafQualifier);
			}

			// Token: 0x04000EB7 RID: 3767
			private readonly IList<string> m_parentQualifiers;

			// Token: 0x04000EB8 RID: 3768
			private readonly int m_indexInParentQualifiers;

			// Token: 0x04000EB9 RID: 3769
			private readonly string m_leafQualifier;
		}
	}
}

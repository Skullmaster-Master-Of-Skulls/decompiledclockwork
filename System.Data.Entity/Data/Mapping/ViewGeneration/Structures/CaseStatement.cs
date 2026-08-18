using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200029E RID: 670
	internal sealed class CaseStatement : InternalBase
	{
		// Token: 0x060027D5 RID: 10197 RVA: 0x0009A686 File Offset: 0x00098886
		internal CaseStatement(MemberPath memberPath)
		{
			this.m_memberPath = memberPath;
			this.m_clauses = new List<CaseStatement.WhenThen>();
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x0009A6A0 File Offset: 0x000988A0
		internal MemberPath MemberPath
		{
			get
			{
				return this.m_memberPath;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x0009A6A8 File Offset: 0x000988A8
		internal List<CaseStatement.WhenThen> Clauses
		{
			get
			{
				return this.m_clauses;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0009A6B0 File Offset: 0x000988B0
		internal ProjectedSlot ElseValue
		{
			get
			{
				return this.m_elseValue;
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0009A6B8 File Offset: 0x000988B8
		internal CaseStatement DeepQualify(CqlBlock block)
		{
			CaseStatement caseStatement = new CaseStatement(this.m_memberPath);
			foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
			{
				CaseStatement.WhenThen item = whenThen.ReplaceWithQualifiedSlot(block);
				caseStatement.m_clauses.Add(item);
			}
			if (this.m_elseValue != null)
			{
				caseStatement.m_elseValue = this.m_elseValue.DeepQualify(block);
			}
			caseStatement.m_simplified = this.m_simplified;
			return caseStatement;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0009A74C File Offset: 0x0009894C
		internal void AddWhenThen(BoolExpression condition, ProjectedSlot value)
		{
			condition.ExpensiveSimplify();
			this.m_clauses.Add(new CaseStatement.WhenThen(condition, value));
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x0009A768 File Offset: 0x00098968
		internal bool DependsOnMemberValue
		{
			get
			{
				if (this.m_elseValue is MemberProjectedSlot)
				{
					return true;
				}
				foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
				{
					if (whenThen.Value is MemberProjectedSlot)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x0009A7D8 File Offset: 0x000989D8
		internal IEnumerable<EdmType> InstantiatedTypes
		{
			get
			{
				foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
				{
					EdmType edmType;
					if (this.TryGetInstantiatedType(whenThen.Value, out edmType))
					{
						yield return edmType;
					}
				}
				List<CaseStatement.WhenThen>.Enumerator enumerator = default(List<CaseStatement.WhenThen>.Enumerator);
				EdmType edmType2;
				if (this.TryGetInstantiatedType(this.m_elseValue, out edmType2))
				{
					yield return edmType2;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x0009A7F8 File Offset: 0x000989F8
		private bool TryGetInstantiatedType(ProjectedSlot slot, out EdmType type)
		{
			type = null;
			ConstantProjectedSlot constantProjectedSlot = slot as ConstantProjectedSlot;
			if (constantProjectedSlot != null)
			{
				TypeConstant typeConstant = constantProjectedSlot.CellConstant as TypeConstant;
				if (typeConstant != null)
				{
					type = typeConstant.EdmType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x0009A82C File Offset: 0x00098A2C
		internal void Simplify()
		{
			if (this.m_simplified)
			{
				return;
			}
			List<CaseStatement.WhenThen> list = new List<CaseStatement.WhenThen>();
			bool flag = false;
			foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
			{
				ConstantProjectedSlot constantProjectedSlot = whenThen.Value as ConstantProjectedSlot;
				if (constantProjectedSlot != null && (constantProjectedSlot.CellConstant.IsNull() || constantProjectedSlot.CellConstant.IsUndefined()))
				{
					flag = true;
				}
				else
				{
					list.Add(whenThen);
					if (whenThen.Condition.IsTrue)
					{
						break;
					}
				}
			}
			if (flag && list.Count == 0)
			{
				this.m_elseValue = new ConstantProjectedSlot(Constant.Null, this.m_memberPath);
			}
			if (list.Count > 0 && !flag)
			{
				int index = list.Count - 1;
				this.m_elseValue = list[index].Value;
				list.RemoveAt(index);
			}
			this.m_clauses = list;
			this.m_simplified = true;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0009A930 File Offset: 0x00098B30
		internal StringBuilder AsEsql(StringBuilder builder, IEnumerable<WithRelationship> withRelationships, string blockAlias, int indentLevel)
		{
			if (this.Clauses.Count == 0)
			{
				CaseStatement.CaseSlotValueAsEsql(builder, this.ElseValue, this.MemberPath, blockAlias, withRelationships, indentLevel);
				return builder;
			}
			builder.Append("CASE");
			foreach (CaseStatement.WhenThen whenThen in this.Clauses)
			{
				StringUtil.IndentNewLine(builder, indentLevel + 2);
				builder.Append("WHEN ");
				whenThen.Condition.AsEsql(builder, blockAlias);
				builder.Append(" THEN ");
				CaseStatement.CaseSlotValueAsEsql(builder, whenThen.Value, this.MemberPath, blockAlias, withRelationships, indentLevel + 2);
			}
			if (this.ElseValue != null)
			{
				StringUtil.IndentNewLine(builder, indentLevel + 2);
				builder.Append("ELSE ");
				CaseStatement.CaseSlotValueAsEsql(builder, this.ElseValue, this.MemberPath, blockAlias, withRelationships, indentLevel + 2);
			}
			StringUtil.IndentNewLine(builder, indentLevel + 1);
			builder.Append("END");
			return builder;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0009AA48 File Offset: 0x00098C48
		internal DbExpression AsCqt(DbExpression row, IEnumerable<WithRelationship> withRelationships)
		{
			List<DbExpression> list = new List<DbExpression>();
			List<DbExpression> list2 = new List<DbExpression>();
			foreach (CaseStatement.WhenThen whenThen in this.Clauses)
			{
				list.Add(whenThen.Condition.AsCqt(row));
				list2.Add(CaseStatement.CaseSlotValueAsCqt(row, whenThen.Value, this.MemberPath, withRelationships));
			}
			DbExpression dbExpression = (this.ElseValue != null) ? CaseStatement.CaseSlotValueAsCqt(row, this.ElseValue, this.MemberPath, withRelationships) : Constant.Null.AsCqt(row, this.MemberPath);
			if (this.Clauses.Count > 0)
			{
				return DbExpressionBuilder.Case(list, list2, dbExpression);
			}
			return dbExpression;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x0009AB18 File Offset: 0x00098D18
		private static StringBuilder CaseSlotValueAsEsql(StringBuilder builder, ProjectedSlot slot, MemberPath outputMember, string blockAlias, IEnumerable<WithRelationship> withRelationships, int indentLevel)
		{
			slot.AsEsql(builder, outputMember, blockAlias, 1);
			CaseStatement.WithRelationshipsClauseAsEsql(builder, withRelationships, blockAlias, indentLevel, slot);
			return builder;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0009AB34 File Offset: 0x00098D34
		private static void WithRelationshipsClauseAsEsql(StringBuilder builder, IEnumerable<WithRelationship> withRelationships, string blockAlias, int indentLevel, ProjectedSlot slot)
		{
			bool first = true;
			CaseStatement.WithRelationshipsClauseAsCql(delegate(WithRelationship withRelationship)
			{
				if (first)
				{
					builder.Append(" WITH ");
					first = false;
				}
				withRelationship.AsEsql(builder, blockAlias, indentLevel);
			}, withRelationships, slot);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0009AB78 File Offset: 0x00098D78
		private static DbExpression CaseSlotValueAsCqt(DbExpression row, ProjectedSlot slot, MemberPath outputMember, IEnumerable<WithRelationship> withRelationships)
		{
			DbExpression slotValueExpr = slot.AsCqt(row, outputMember);
			return CaseStatement.WithRelationshipsClauseAsCqt(row, slotValueExpr, withRelationships, slot);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0009AB9C File Offset: 0x00098D9C
		private static DbExpression WithRelationshipsClauseAsCqt(DbExpression row, DbExpression slotValueExpr, IEnumerable<WithRelationship> withRelationships, ProjectedSlot slot)
		{
			List<DbRelatedEntityRef> relatedEntityRefs = new List<DbRelatedEntityRef>();
			CaseStatement.WithRelationshipsClauseAsCql(delegate(WithRelationship withRelationship)
			{
				relatedEntityRefs.Add(withRelationship.AsCqt(row));
			}, withRelationships, slot);
			if (relatedEntityRefs.Count > 0)
			{
				DbNewInstanceExpression dbNewInstanceExpression = slotValueExpr as DbNewInstanceExpression;
				return DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression((EntityType)dbNewInstanceExpression.ResultType.EdmType, dbNewInstanceExpression.Arguments, relatedEntityRefs);
			}
			return slotValueExpr;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0009AC0C File Offset: 0x00098E0C
		private static void WithRelationshipsClauseAsCql(Action<WithRelationship> emitWithRelationship, IEnumerable<WithRelationship> withRelationships, ProjectedSlot slot)
		{
			if (withRelationships != null && withRelationships.Count<WithRelationship>() > 0)
			{
				ConstantProjectedSlot constantProjectedSlot = slot as ConstantProjectedSlot;
				TypeConstant typeConstant = constantProjectedSlot.CellConstant as TypeConstant;
				EdmType edmType = typeConstant.EdmType;
				foreach (WithRelationship withRelationship in withRelationships)
				{
					if (withRelationship.FromEndEntityType.IsAssignableFrom(edmType))
					{
						emitWithRelationship(withRelationship);
					}
				}
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0009AC8C File Offset: 0x00098E8C
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.AppendLine("CASE");
			foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
			{
				builder.Append(" WHEN ");
				whenThen.Condition.ToCompactString(builder);
				builder.Append(" THEN ");
				whenThen.Value.ToCompactString(builder);
				builder.AppendLine();
			}
			if (this.m_elseValue != null)
			{
				builder.Append(" ELSE ");
				this.m_elseValue.ToCompactString(builder);
				builder.AppendLine();
			}
			builder.Append(" END AS ");
			this.m_memberPath.ToCompactString(builder);
		}

		// Token: 0x04001231 RID: 4657
		private readonly MemberPath m_memberPath;

		// Token: 0x04001232 RID: 4658
		private List<CaseStatement.WhenThen> m_clauses;

		// Token: 0x04001233 RID: 4659
		private ProjectedSlot m_elseValue;

		// Token: 0x04001234 RID: 4660
		private bool m_simplified;

		// Token: 0x020005D3 RID: 1491
		internal sealed class WhenThen : InternalBase
		{
			// Token: 0x0600414B RID: 16715 RVA: 0x000EED03 File Offset: 0x000ECF03
			internal WhenThen(BoolExpression condition, ProjectedSlot value)
			{
				this.m_condition = condition;
				this.m_value = value;
			}

			// Token: 0x17000B54 RID: 2900
			// (get) Token: 0x0600414C RID: 16716 RVA: 0x000EED19 File Offset: 0x000ECF19
			internal BoolExpression Condition
			{
				get
				{
					return this.m_condition;
				}
			}

			// Token: 0x17000B55 RID: 2901
			// (get) Token: 0x0600414D RID: 16717 RVA: 0x000EED21 File Offset: 0x000ECF21
			internal ProjectedSlot Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x0600414E RID: 16718 RVA: 0x000EED2C File Offset: 0x000ECF2C
			internal CaseStatement.WhenThen ReplaceWithQualifiedSlot(CqlBlock block)
			{
				ProjectedSlot value = this.m_value.DeepQualify(block);
				return new CaseStatement.WhenThen(this.m_condition, value);
			}

			// Token: 0x0600414F RID: 16719 RVA: 0x000EED52 File Offset: 0x000ECF52
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("WHEN ");
				this.m_condition.ToCompactString(builder);
				builder.Append("THEN ");
				this.m_value.ToCompactString(builder);
			}

			// Token: 0x04001D64 RID: 7524
			private readonly BoolExpression m_condition;

			// Token: 0x04001D65 RID: 7525
			private readonly ProjectedSlot m_value;
		}
	}
}

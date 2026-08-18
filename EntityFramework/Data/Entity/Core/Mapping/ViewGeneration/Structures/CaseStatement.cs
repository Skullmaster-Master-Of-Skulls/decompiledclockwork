using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000462 RID: 1122
	internal sealed class CaseStatement : InternalBase
	{
		// Token: 0x06002924 RID: 10532 RVA: 0x000C74B4 File Offset: 0x000C56B4
		internal CaseStatement(MemberPath memberPath)
		{
			this.m_memberPath = memberPath;
			this.m_clauses = new List<CaseStatement.WhenThen>();
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000C74CE File Offset: 0x000C56CE
		internal MemberPath MemberPath
		{
			get
			{
				return this.m_memberPath;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000C74D6 File Offset: 0x000C56D6
		internal List<CaseStatement.WhenThen> Clauses
		{
			get
			{
				return this.m_clauses;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000C74DE File Offset: 0x000C56DE
		internal ProjectedSlot ElseValue
		{
			get
			{
				return this.m_elseValue;
			}
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000C74E8 File Offset: 0x000C56E8
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

		// Token: 0x06002929 RID: 10537 RVA: 0x000C757C File Offset: 0x000C577C
		internal void AddWhenThen(BoolExpression condition, ProjectedSlot value)
		{
			condition.ExpensiveSimplify();
			this.m_clauses.Add(new CaseStatement.WhenThen(condition, value));
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000C7598 File Offset: 0x000C5798
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

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000C77E4 File Offset: 0x000C59E4
		internal IEnumerable<EdmType> InstantiatedTypes
		{
			get
			{
				foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
				{
					EdmType type;
					if (CaseStatement.TryGetInstantiatedType(whenThen.Value, out type))
					{
						yield return type;
					}
				}
				EdmType elseType;
				if (CaseStatement.TryGetInstantiatedType(this.m_elseValue, out elseType))
				{
					yield return elseType;
				}
				yield break;
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000C7804 File Offset: 0x000C5A04
		private static bool TryGetInstantiatedType(ProjectedSlot slot, out EdmType type)
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

		// Token: 0x0600292D RID: 10541 RVA: 0x000C7838 File Offset: 0x000C5A38
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
				this.m_elseValue = new ConstantProjectedSlot(Constant.Null);
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

		// Token: 0x0600292E RID: 10542 RVA: 0x000C7934 File Offset: 0x000C5B34
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

		// Token: 0x0600292F RID: 10543 RVA: 0x000C7A4C File Offset: 0x000C5C4C
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

		// Token: 0x06002930 RID: 10544 RVA: 0x000C7B18 File Offset: 0x000C5D18
		private static StringBuilder CaseSlotValueAsEsql(StringBuilder builder, ProjectedSlot slot, MemberPath outputMember, string blockAlias, IEnumerable<WithRelationship> withRelationships, int indentLevel)
		{
			slot.AsEsql(builder, outputMember, blockAlias, 1);
			CaseStatement.WithRelationshipsClauseAsEsql(builder, withRelationships, blockAlias, indentLevel, slot);
			return builder;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000C7B78 File Offset: 0x000C5D78
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

		// Token: 0x06002932 RID: 10546 RVA: 0x000C7BBC File Offset: 0x000C5DBC
		private static DbExpression CaseSlotValueAsCqt(DbExpression row, ProjectedSlot slot, MemberPath outputMember, IEnumerable<WithRelationship> withRelationships)
		{
			DbExpression slotValueExpr = slot.AsCqt(row, outputMember);
			return CaseStatement.WithRelationshipsClauseAsCqt(row, slotValueExpr, withRelationships, slot);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000C7C00 File Offset: 0x000C5E00
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

		// Token: 0x06002934 RID: 10548 RVA: 0x000C7C70 File Offset: 0x000C5E70
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

		// Token: 0x06002935 RID: 10549 RVA: 0x000C7CF4 File Offset: 0x000C5EF4
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

		// Token: 0x04000F56 RID: 3926
		private readonly MemberPath m_memberPath;

		// Token: 0x04000F57 RID: 3927
		private List<CaseStatement.WhenThen> m_clauses;

		// Token: 0x04000F58 RID: 3928
		private ProjectedSlot m_elseValue;

		// Token: 0x04000F59 RID: 3929
		private bool m_simplified;

		// Token: 0x02000463 RID: 1123
		internal sealed class WhenThen : InternalBase
		{
			// Token: 0x06002936 RID: 10550 RVA: 0x000C7DC4 File Offset: 0x000C5FC4
			internal WhenThen(BoolExpression condition, ProjectedSlot value)
			{
				this.m_condition = condition;
				this.m_value = value;
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06002937 RID: 10551 RVA: 0x000C7DDA File Offset: 0x000C5FDA
			internal BoolExpression Condition
			{
				get
				{
					return this.m_condition;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06002938 RID: 10552 RVA: 0x000C7DE2 File Offset: 0x000C5FE2
			internal ProjectedSlot Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x06002939 RID: 10553 RVA: 0x000C7DEC File Offset: 0x000C5FEC
			internal CaseStatement.WhenThen ReplaceWithQualifiedSlot(CqlBlock block)
			{
				ProjectedSlot value = this.m_value.DeepQualify(block);
				return new CaseStatement.WhenThen(this.m_condition, value);
			}

			// Token: 0x0600293A RID: 10554 RVA: 0x000C7E12 File Offset: 0x000C6012
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("WHEN ");
				this.m_condition.ToCompactString(builder);
				builder.Append("THEN ");
				this.m_value.ToCompactString(builder);
			}

			// Token: 0x04000F5A RID: 3930
			private readonly BoolExpression m_condition;

			// Token: 0x04000F5B RID: 3931
			private readonly ProjectedSlot m_value;
		}
	}
}

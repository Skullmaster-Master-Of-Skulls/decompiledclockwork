using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B2 RID: 690
	internal sealed class NegatedConstant : Constant
	{
		// Token: 0x0600291B RID: 10523 RVA: 0x0009F4E4 File Offset: 0x0009D6E4
		internal NegatedConstant(IEnumerable<Constant> values)
		{
			this.m_negatedDomain = new Set<Constant>(values, Constant.EqualityComparer);
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x0009F4FD File Offset: 0x0009D6FD
		internal IEnumerable<Constant> Elements
		{
			get
			{
				return this.m_negatedDomain;
			}
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0009F505 File Offset: 0x0009D705
		internal bool Contains(Constant constant)
		{
			return this.m_negatedDomain.Contains(constant);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0009F513 File Offset: 0x0009D713
		internal override bool IsNotNull()
		{
			return this == Constant.NotNull || (this.m_negatedDomain.Count == 1 && this.m_negatedDomain.Contains(Constant.Null));
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0009F53F File Offset: 0x0009D73F
		internal override bool HasNotNull()
		{
			return this.m_negatedDomain.Contains(Constant.Null);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x0009F554 File Offset: 0x0009D754
		public override int GetHashCode()
		{
			int num = 0;
			foreach (Constant obj in this.m_negatedDomain)
			{
				num ^= Constant.EqualityComparer.GetHashCode(obj);
			}
			return num;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x0009F5B4 File Offset: 0x0009D7B4
		protected override bool IsEqualTo(Constant right)
		{
			NegatedConstant negatedConstant = right as NegatedConstant;
			return negatedConstant != null && this.m_negatedDomain.SetEquals(negatedConstant.m_negatedDomain);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x00006174 File Offset: 0x00004374
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			return null;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x00006174 File Offset: 0x00004374
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x0009F5DE File Offset: 0x0009D7DE
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, constants, outputMember, skipIsNotNull, false);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x0009F5F0 File Offset: 0x0009D7F0
		internal DbExpression AsCqt(DbExpression row, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			DbExpression cqt = null;
			this.AsCql(delegate
			{
				cqt = DbExpressionBuilder.True;
			}, delegate
			{
				cqt = outputMember.AsCqt(row).IsNull().Not();
			}, delegate(Constant constant)
			{
				DbExpression dbExpression = outputMember.AsCqt(row).NotEqual(constant.AsCqt(row, outputMember));
				if (cqt != null)
				{
					cqt = cqt.And(dbExpression);
					return;
				}
				cqt = dbExpression;
			}, constants, outputMember, skipIsNotNull);
			return cqt;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x0009F651 File Offset: 0x0009D851
		internal StringBuilder AsUserString(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, constants, outputMember, skipIsNotNull, true);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x0009F664 File Offset: 0x0009D864
		private void AsCql(Action trueLiteral, Action varIsNotNull, Action<Constant> varNotEqualsTo, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			bool isNullable = outputMember.IsNullable;
			Set<Constant> set = new Set<Constant>(this.Elements, Constant.EqualityComparer);
			foreach (Constant constant in constants)
			{
				if (!constant.Equals(this))
				{
					set.Remove(constant);
				}
			}
			if (set.Count == 0)
			{
				trueLiteral();
				return;
			}
			bool flag = set.Contains(Constant.Null);
			set.Remove(Constant.Null);
			if (flag || (isNullable && !skipIsNotNull))
			{
				varIsNotNull();
			}
			foreach (Constant obj in set)
			{
				varNotEqualsTo(obj);
			}
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x0009F748 File Offset: 0x0009D948
		private StringBuilder ToStringHelper(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull, bool userString)
		{
			bool anyAdded = false;
			this.AsCql(delegate
			{
				builder.Append("true");
			}, delegate
			{
				if (userString)
				{
					outputMember.ToCompactString(builder, blockAlias);
					builder.Append(" is not NULL");
				}
				else
				{
					outputMember.AsEsql(builder, blockAlias);
					builder.Append(" IS NOT NULL");
				}
				anyAdded = true;
			}, delegate(Constant constant)
			{
				if (anyAdded)
				{
					builder.Append(" AND ");
				}
				anyAdded = true;
				if (userString)
				{
					outputMember.ToCompactString(builder, blockAlias);
					builder.Append(" <>");
					constant.ToCompactString(builder);
					return;
				}
				outputMember.AsEsql(builder, blockAlias);
				builder.Append(" <>");
				constant.AsEsql(builder, outputMember, blockAlias);
			}, constants, outputMember, skipIsNotNull);
			return builder;
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x0009F7BC File Offset: 0x0009D9BC
		internal override string ToUserString()
		{
			if (this.IsNotNull())
			{
				return Strings.ViewGen_NotNull;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (Constant constant in this.m_negatedDomain)
			{
				if (this.m_negatedDomain.Count <= 1 || !constant.IsNull())
				{
					if (!flag)
					{
						stringBuilder.Append(Strings.ViewGen_CommaBlank);
					}
					flag = false;
					stringBuilder.Append(constant.ToUserString());
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append(Strings.ViewGen_NegatedCellConstant(stringBuilder.ToString()));
			return stringBuilder2.ToString();
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0009F874 File Offset: 0x0009DA74
		internal override void ToCompactString(StringBuilder builder)
		{
			if (this.IsNotNull())
			{
				builder.Append("NOT_NULL");
				return;
			}
			builder.Append("NOT(");
			StringUtil.ToCommaSeparatedStringSorted(builder, this.m_negatedDomain);
			builder.Append(")");
		}

		// Token: 0x04001279 RID: 4729
		private readonly Set<Constant> m_negatedDomain;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000482 RID: 1154
	internal sealed class NegatedConstant : Constant
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x000CDEB8 File Offset: 0x000CC0B8
		internal NegatedConstant(IEnumerable<Constant> values)
		{
			this.m_negatedDomain = new Set<Constant>(values, Constant.EqualityComparer);
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x000CDED1 File Offset: 0x000CC0D1
		internal IEnumerable<Constant> Elements
		{
			get
			{
				return this.m_negatedDomain;
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000CDED9 File Offset: 0x000CC0D9
		internal bool Contains(Constant constant)
		{
			return this.m_negatedDomain.Contains(constant);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000CDEE7 File Offset: 0x000CC0E7
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000CDEEA File Offset: 0x000CC0EA
		internal override bool IsNotNull()
		{
			return object.ReferenceEquals(this, Constant.NotNull) || (this.m_negatedDomain.Count == 1 && this.m_negatedDomain.Contains(Constant.Null));
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000CDF1B File Offset: 0x000CC11B
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000CDF1E File Offset: 0x000CC11E
		internal override bool HasNotNull()
		{
			return this.m_negatedDomain.Contains(Constant.Null);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000CDF30 File Offset: 0x000CC130
		public override int GetHashCode()
		{
			int num = 0;
			foreach (Constant obj in this.m_negatedDomain)
			{
				num ^= Constant.EqualityComparer.GetHashCode(obj);
			}
			return num;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000CDF90 File Offset: 0x000CC190
		protected override bool IsEqualTo(Constant right)
		{
			NegatedConstant negatedConstant = right as NegatedConstant;
			return negatedConstant != null && this.m_negatedDomain.SetEquals(negatedConstant.m_negatedDomain);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000CDFBA File Offset: 0x000CC1BA
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			return null;
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000CDFBD File Offset: 0x000CC1BD
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000CDFC0 File Offset: 0x000CC1C0
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, constants, outputMember, skipIsNotNull, false);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000CE060 File Offset: 0x000CC260
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

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000CE0C1 File Offset: 0x000CC2C1
		internal StringBuilder AsUserString(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, constants, outputMember, skipIsNotNull, true);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000CE0D4 File Offset: 0x000CC2D4
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

		// Token: 0x06002ABB RID: 10939 RVA: 0x000CE2F4 File Offset: 0x000CC4F4
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

		// Token: 0x06002ABC RID: 10940 RVA: 0x000CE368 File Offset: 0x000CC568
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

		// Token: 0x06002ABD RID: 10941 RVA: 0x000CE420 File Offset: 0x000CC620
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

		// Token: 0x04000FB4 RID: 4020
		private readonly Set<Constant> m_negatedDomain;
	}
}

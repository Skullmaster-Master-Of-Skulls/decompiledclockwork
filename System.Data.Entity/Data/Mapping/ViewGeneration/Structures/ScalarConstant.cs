using System;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B9 RID: 697
	internal sealed class ScalarConstant : Constant
	{
		// Token: 0x06002981 RID: 10625 RVA: 0x000A12C4 File Offset: 0x0009F4C4
		internal ScalarConstant(object value)
		{
			this.m_scalar = value;
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000A12D3 File Offset: 0x0009F4D3
		internal object Value
		{
			get
			{
				return this.m_scalar;
			}
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsNotNull()
		{
			return false;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool HasNotNull()
		{
			return false;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000A12DC File Offset: 0x0009F4DC
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			EdmType edmType = modelTypeUsage.EdmType;
			if (BuiltInTypeKind.PrimitiveType == edmType.BuiltInTypeKind)
			{
				PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)edmType).PrimitiveTypeKind;
				if (primitiveTypeKind == PrimitiveTypeKind.Boolean)
				{
					bool flag = (bool)this.m_scalar;
					string value = StringUtil.FormatInvariant("{0}", new object[]
					{
						flag
					});
					builder.Append(value);
					return builder;
				}
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					bool flag2;
					if (!TypeHelpers.TryGetIsUnicode(modelTypeUsage, out flag2))
					{
						flag2 = true;
					}
					if (flag2)
					{
						builder.Append('N');
					}
					this.AppendEscapedScalar(builder);
					return builder;
				}
			}
			else if (BuiltInTypeKind.EnumType == edmType.BuiltInTypeKind)
			{
				EnumMember enumMember = (EnumMember)this.m_scalar;
				builder.Append(enumMember.Name);
				return builder;
			}
			builder.Append("CAST(");
			this.AppendEscapedScalar(builder);
			builder.Append(" AS ");
			CqlWriter.AppendEscapedTypeName(builder, edmType);
			builder.Append(')');
			return builder;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000A13CC File Offset: 0x0009F5CC
		private StringBuilder AppendEscapedScalar(StringBuilder builder)
		{
			string text = StringUtil.FormatInvariant("{0}", new object[]
			{
				this.m_scalar
			});
			if (text.Contains("'"))
			{
				text = text.Replace("'", "''");
			}
			StringUtil.FormatStringBuilder(builder, "'{0}'", new object[]
			{
				text
			});
			return builder;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000A1428 File Offset: 0x0009F628
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			return modelTypeUsage.Constant(this.m_scalar);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000A1450 File Offset: 0x0009F650
		protected override bool IsEqualTo(Constant right)
		{
			ScalarConstant scalarConstant = right as ScalarConstant;
			return scalarConstant != null && ByValueEqualityComparer.Default.Equals(this.m_scalar, scalarConstant.m_scalar);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000A147F File Offset: 0x0009F67F
		public override int GetHashCode()
		{
			return this.m_scalar.GetHashCode();
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000A148C File Offset: 0x0009F68C
		internal override string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000A14AC File Offset: 0x0009F6AC
		internal override void ToCompactString(StringBuilder builder)
		{
			EnumMember enumMember = this.m_scalar as EnumMember;
			if (enumMember != null)
			{
				builder.Append(enumMember.Name);
				return;
			}
			builder.Append(StringUtil.FormatInvariant("'{0}'", new object[]
			{
				this.m_scalar
			}));
		}

		// Token: 0x04001284 RID: 4740
		private readonly object m_scalar;
	}
}

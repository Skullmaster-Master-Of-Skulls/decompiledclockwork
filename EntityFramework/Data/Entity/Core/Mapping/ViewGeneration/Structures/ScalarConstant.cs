using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000485 RID: 1157
	internal sealed class ScalarConstant : Constant
	{
		// Token: 0x06002ADC RID: 10972 RVA: 0x000CF110 File Offset: 0x000CD310
		internal ScalarConstant(object value)
		{
			this.m_scalar = value;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000CF11F File Offset: 0x000CD31F
		internal object Value
		{
			get
			{
				return this.m_scalar;
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000CF127 File Offset: 0x000CD327
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000CF12A File Offset: 0x000CD32A
		internal override bool IsNotNull()
		{
			return false;
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000CF12D File Offset: 0x000CD32D
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000CF130 File Offset: 0x000CD330
		internal override bool HasNotNull()
		{
			return false;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000CF134 File Offset: 0x000CD334
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

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000CF228 File Offset: 0x000CD428
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

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000CF288 File Offset: 0x000CD488
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			return modelTypeUsage.Constant(this.m_scalar);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000CF2B0 File Offset: 0x000CD4B0
		protected override bool IsEqualTo(Constant right)
		{
			ScalarConstant scalarConstant = right as ScalarConstant;
			return scalarConstant != null && ByValueEqualityComparer.Default.Equals(this.m_scalar, scalarConstant.m_scalar);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000CF2DF File Offset: 0x000CD4DF
		public override int GetHashCode()
		{
			return this.m_scalar.GetHashCode();
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000CF2EC File Offset: 0x000CD4EC
		internal override string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000CF30C File Offset: 0x000CD50C
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

		// Token: 0x04000FBB RID: 4027
		private readonly object m_scalar;
	}
}

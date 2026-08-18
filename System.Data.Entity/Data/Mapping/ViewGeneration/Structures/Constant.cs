using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A1 RID: 673
	internal abstract class Constant : InternalBase
	{
		// Token: 0x060027FE RID: 10238
		internal abstract bool IsNull();

		// Token: 0x060027FF RID: 10239
		internal abstract bool IsNotNull();

		// Token: 0x06002800 RID: 10240
		internal abstract bool IsUndefined();

		// Token: 0x06002801 RID: 10241
		internal abstract bool HasNotNull();

		// Token: 0x06002802 RID: 10242
		internal abstract StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias);

		// Token: 0x06002803 RID: 10243
		internal abstract DbExpression AsCqt(DbExpression row, MemberPath outputMember);

		// Token: 0x06002804 RID: 10244 RVA: 0x0009B128 File Offset: 0x00099328
		public override bool Equals(object obj)
		{
			Constant constant = obj as Constant;
			return constant != null && this.IsEqualTo(constant);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0009B148 File Offset: 0x00099348
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002806 RID: 10246
		protected abstract bool IsEqualTo(Constant right);

		// Token: 0x06002807 RID: 10247
		internal abstract string ToUserString();

		// Token: 0x06002808 RID: 10248 RVA: 0x0009B150 File Offset: 0x00099350
		internal static void ConstantsToUserString(StringBuilder builder, Set<Constant> constants)
		{
			bool flag = true;
			foreach (Constant constant in constants)
			{
				if (!flag)
				{
					builder.Append(Strings.ViewGen_CommaBlank);
				}
				flag = false;
				string value = constant.ToUserString();
				builder.Append(value);
			}
		}

		// Token: 0x0400123C RID: 4668
		internal static readonly IEqualityComparer<Constant> EqualityComparer = new Constant.CellConstantComparer();

		// Token: 0x0400123D RID: 4669
		internal static readonly Constant Null = Constant.NullConstant.Instance;

		// Token: 0x0400123E RID: 4670
		internal static readonly Constant NotNull = new NegatedConstant(new Constant[]
		{
			Constant.NullConstant.Instance
		});

		// Token: 0x0400123F RID: 4671
		internal static readonly Constant Undefined = Constant.UndefinedConstant.Instance;

		// Token: 0x04001240 RID: 4672
		internal static readonly Constant AllOtherConstants = Constant.AllOtherConstantsConstant.Instance;

		// Token: 0x020005D7 RID: 1495
		private class CellConstantComparer : IEqualityComparer<Constant>
		{
			// Token: 0x0600415D RID: 16733 RVA: 0x000EEF8F File Offset: 0x000ED18F
			public bool Equals(Constant left, Constant right)
			{
				return left == right || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x0600415E RID: 16734 RVA: 0x000EEFA6 File Offset: 0x000ED1A6
			public int GetHashCode(Constant key)
			{
				EntityUtil.CheckArgumentNull<Constant>(key, "key");
				return key.GetHashCode();
			}
		}

		// Token: 0x020005D8 RID: 1496
		private sealed class NullConstant : Constant
		{
			// Token: 0x06004160 RID: 16736 RVA: 0x000EEFBA File Offset: 0x000ED1BA
			private NullConstant()
			{
			}

			// Token: 0x06004161 RID: 16737 RVA: 0x00017938 File Offset: 0x00015B38
			internal override bool IsNull()
			{
				return true;
			}

			// Token: 0x06004162 RID: 16738 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x06004163 RID: 16739 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsUndefined()
			{
				return false;
			}

			// Token: 0x06004164 RID: 16740 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x06004165 RID: 16741 RVA: 0x000EEFC4 File Offset: 0x000ED1C4
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				EdmType edmType = Helper.GetModelTypeUsage(outputMember.LeafEdmMember).EdmType;
				builder.Append("CAST(NULL AS ");
				CqlWriter.AppendEscapedTypeName(builder, edmType);
				builder.Append(')');
				return builder;
			}

			// Token: 0x06004166 RID: 16742 RVA: 0x000EF000 File Offset: 0x000ED200
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				EdmType edmType = Helper.GetModelTypeUsage(outputMember.LeafEdmMember).EdmType;
				return TypeUsage.Create(edmType).Null();
			}

			// Token: 0x06004167 RID: 16743 RVA: 0x000173E2 File Offset: 0x000155E2
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x06004168 RID: 16744 RVA: 0x0005AF88 File Offset: 0x00059188
			protected override bool IsEqualTo(Constant right)
			{
				return this == right;
			}

			// Token: 0x06004169 RID: 16745 RVA: 0x000EF029 File Offset: 0x000ED229
			internal override string ToUserString()
			{
				return Strings.ViewGen_Null;
			}

			// Token: 0x0600416A RID: 16746 RVA: 0x000EF030 File Offset: 0x000ED230
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("NULL");
			}

			// Token: 0x04001D71 RID: 7537
			internal static readonly Constant Instance = new Constant.NullConstant();
		}

		// Token: 0x020005D9 RID: 1497
		private sealed class UndefinedConstant : Constant
		{
			// Token: 0x0600416C RID: 16748 RVA: 0x000EEFBA File Offset: 0x000ED1BA
			private UndefinedConstant()
			{
			}

			// Token: 0x0600416D RID: 16749 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsNull()
			{
				return false;
			}

			// Token: 0x0600416E RID: 16750 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x0600416F RID: 16751 RVA: 0x00017938 File Offset: 0x00015B38
			internal override bool IsUndefined()
			{
				return true;
			}

			// Token: 0x06004170 RID: 16752 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x06004171 RID: 16753 RVA: 0x00006174 File Offset: 0x00004374
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				return null;
			}

			// Token: 0x06004172 RID: 16754 RVA: 0x00006174 File Offset: 0x00004374
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				return null;
			}

			// Token: 0x06004173 RID: 16755 RVA: 0x000173E2 File Offset: 0x000155E2
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x06004174 RID: 16756 RVA: 0x0005AF88 File Offset: 0x00059188
			protected override bool IsEqualTo(Constant right)
			{
				return this == right;
			}

			// Token: 0x06004175 RID: 16757 RVA: 0x00006174 File Offset: 0x00004374
			internal override string ToUserString()
			{
				return null;
			}

			// Token: 0x06004176 RID: 16758 RVA: 0x000EF04A File Offset: 0x000ED24A
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("?");
			}

			// Token: 0x04001D72 RID: 7538
			internal static readonly Constant Instance = new Constant.UndefinedConstant();
		}

		// Token: 0x020005DA RID: 1498
		private sealed class AllOtherConstantsConstant : Constant
		{
			// Token: 0x06004178 RID: 16760 RVA: 0x000EEFBA File Offset: 0x000ED1BA
			private AllOtherConstantsConstant()
			{
			}

			// Token: 0x06004179 RID: 16761 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsNull()
			{
				return false;
			}

			// Token: 0x0600417A RID: 16762 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x0600417B RID: 16763 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsUndefined()
			{
				return false;
			}

			// Token: 0x0600417C RID: 16764 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x0600417D RID: 16765 RVA: 0x00006174 File Offset: 0x00004374
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				return null;
			}

			// Token: 0x0600417E RID: 16766 RVA: 0x00006174 File Offset: 0x00004374
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				return null;
			}

			// Token: 0x0600417F RID: 16767 RVA: 0x000173E2 File Offset: 0x000155E2
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x06004180 RID: 16768 RVA: 0x0005AF88 File Offset: 0x00059188
			protected override bool IsEqualTo(Constant right)
			{
				return this == right;
			}

			// Token: 0x06004181 RID: 16769 RVA: 0x00006174 File Offset: 0x00004374
			internal override string ToUserString()
			{
				return null;
			}

			// Token: 0x06004182 RID: 16770 RVA: 0x000EF064 File Offset: 0x000ED264
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("AllOtherConstants");
			}

			// Token: 0x04001D73 RID: 7539
			internal static readonly Constant Instance = new Constant.AllOtherConstantsConstant();
		}
	}
}

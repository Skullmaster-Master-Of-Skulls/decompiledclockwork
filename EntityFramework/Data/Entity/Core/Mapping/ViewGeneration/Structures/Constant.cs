using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200046B RID: 1131
	internal abstract class Constant : InternalBase
	{
		// Token: 0x0600298C RID: 10636
		internal abstract bool IsNull();

		// Token: 0x0600298D RID: 10637
		internal abstract bool IsNotNull();

		// Token: 0x0600298E RID: 10638
		internal abstract bool IsUndefined();

		// Token: 0x0600298F RID: 10639
		internal abstract bool HasNotNull();

		// Token: 0x06002990 RID: 10640
		internal abstract StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias);

		// Token: 0x06002991 RID: 10641
		internal abstract DbExpression AsCqt(DbExpression row, MemberPath outputMember);

		// Token: 0x06002992 RID: 10642 RVA: 0x000C973C File Offset: 0x000C793C
		public override bool Equals(object obj)
		{
			Constant constant = obj as Constant;
			return constant != null && this.IsEqualTo(constant);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000C975C File Offset: 0x000C795C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002994 RID: 10644
		protected abstract bool IsEqualTo(Constant right);

		// Token: 0x06002995 RID: 10645
		internal abstract string ToUserString();

		// Token: 0x06002996 RID: 10646 RVA: 0x000C9764 File Offset: 0x000C7964
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

		// Token: 0x04000F79 RID: 3961
		internal static readonly IEqualityComparer<Constant> EqualityComparer = new Constant.CellConstantComparer();

		// Token: 0x04000F7A RID: 3962
		internal static readonly Constant Null = Constant.NullConstant.Instance;

		// Token: 0x04000F7B RID: 3963
		internal static readonly Constant NotNull = new NegatedConstant(new Constant[]
		{
			Constant.NullConstant.Instance
		});

		// Token: 0x04000F7C RID: 3964
		internal static readonly Constant Undefined = Constant.UndefinedConstant.Instance;

		// Token: 0x04000F7D RID: 3965
		internal static readonly Constant AllOtherConstants = Constant.AllOtherConstantsConstant.Instance;

		// Token: 0x0200046C RID: 1132
		private class CellConstantComparer : IEqualityComparer<Constant>
		{
			// Token: 0x06002999 RID: 10649 RVA: 0x000C9827 File Offset: 0x000C7A27
			public bool Equals(Constant left, Constant right)
			{
				return object.ReferenceEquals(left, right) || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x0600299A RID: 10650 RVA: 0x000C9843 File Offset: 0x000C7A43
			public int GetHashCode(Constant key)
			{
				return key.GetHashCode();
			}
		}

		// Token: 0x0200046D RID: 1133
		private sealed class NullConstant : Constant
		{
			// Token: 0x0600299C RID: 10652 RVA: 0x000C9853 File Offset: 0x000C7A53
			private NullConstant()
			{
			}

			// Token: 0x0600299D RID: 10653 RVA: 0x000C985B File Offset: 0x000C7A5B
			internal override bool IsNull()
			{
				return true;
			}

			// Token: 0x0600299E RID: 10654 RVA: 0x000C985E File Offset: 0x000C7A5E
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x0600299F RID: 10655 RVA: 0x000C9861 File Offset: 0x000C7A61
			internal override bool IsUndefined()
			{
				return false;
			}

			// Token: 0x060029A0 RID: 10656 RVA: 0x000C9864 File Offset: 0x000C7A64
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x060029A1 RID: 10657 RVA: 0x000C9868 File Offset: 0x000C7A68
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				EdmType edmType = Helper.GetModelTypeUsage(outputMember.LeafEdmMember).EdmType;
				builder.Append("CAST(NULL AS ");
				CqlWriter.AppendEscapedTypeName(builder, edmType);
				builder.Append(')');
				return builder;
			}

			// Token: 0x060029A2 RID: 10658 RVA: 0x000C98A4 File Offset: 0x000C7AA4
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				EdmType edmType = Helper.GetModelTypeUsage(outputMember.LeafEdmMember).EdmType;
				return TypeUsage.Create(edmType).Null();
			}

			// Token: 0x060029A3 RID: 10659 RVA: 0x000C98CD File Offset: 0x000C7ACD
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x060029A4 RID: 10660 RVA: 0x000C98D0 File Offset: 0x000C7AD0
			protected override bool IsEqualTo(Constant right)
			{
				return object.ReferenceEquals(this, right);
			}

			// Token: 0x060029A5 RID: 10661 RVA: 0x000C98D9 File Offset: 0x000C7AD9
			internal override string ToUserString()
			{
				return Strings.ViewGen_Null;
			}

			// Token: 0x060029A6 RID: 10662 RVA: 0x000C98E0 File Offset: 0x000C7AE0
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("NULL");
			}

			// Token: 0x04000F7E RID: 3966
			internal static readonly Constant Instance = new Constant.NullConstant();
		}

		// Token: 0x0200046E RID: 1134
		private sealed class UndefinedConstant : Constant
		{
			// Token: 0x060029A8 RID: 10664 RVA: 0x000C98FA File Offset: 0x000C7AFA
			private UndefinedConstant()
			{
			}

			// Token: 0x060029A9 RID: 10665 RVA: 0x000C9902 File Offset: 0x000C7B02
			internal override bool IsNull()
			{
				return false;
			}

			// Token: 0x060029AA RID: 10666 RVA: 0x000C9905 File Offset: 0x000C7B05
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x060029AB RID: 10667 RVA: 0x000C9908 File Offset: 0x000C7B08
			internal override bool IsUndefined()
			{
				return true;
			}

			// Token: 0x060029AC RID: 10668 RVA: 0x000C990B File Offset: 0x000C7B0B
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x060029AD RID: 10669 RVA: 0x000C990E File Offset: 0x000C7B0E
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060029AE RID: 10670 RVA: 0x000C9915 File Offset: 0x000C7B15
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060029AF RID: 10671 RVA: 0x000C991C File Offset: 0x000C7B1C
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x060029B0 RID: 10672 RVA: 0x000C991F File Offset: 0x000C7B1F
			protected override bool IsEqualTo(Constant right)
			{
				return object.ReferenceEquals(this, right);
			}

			// Token: 0x060029B1 RID: 10673 RVA: 0x000C9928 File Offset: 0x000C7B28
			internal override string ToUserString()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060029B2 RID: 10674 RVA: 0x000C992F File Offset: 0x000C7B2F
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("?");
			}

			// Token: 0x04000F7F RID: 3967
			internal static readonly Constant Instance = new Constant.UndefinedConstant();
		}

		// Token: 0x0200046F RID: 1135
		private sealed class AllOtherConstantsConstant : Constant
		{
			// Token: 0x060029B4 RID: 10676 RVA: 0x000C9949 File Offset: 0x000C7B49
			private AllOtherConstantsConstant()
			{
			}

			// Token: 0x060029B5 RID: 10677 RVA: 0x000C9951 File Offset: 0x000C7B51
			internal override bool IsNull()
			{
				return false;
			}

			// Token: 0x060029B6 RID: 10678 RVA: 0x000C9954 File Offset: 0x000C7B54
			internal override bool IsNotNull()
			{
				return false;
			}

			// Token: 0x060029B7 RID: 10679 RVA: 0x000C9957 File Offset: 0x000C7B57
			internal override bool IsUndefined()
			{
				return false;
			}

			// Token: 0x060029B8 RID: 10680 RVA: 0x000C995A File Offset: 0x000C7B5A
			internal override bool HasNotNull()
			{
				return false;
			}

			// Token: 0x060029B9 RID: 10681 RVA: 0x000C995D File Offset: 0x000C7B5D
			internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060029BA RID: 10682 RVA: 0x000C9964 File Offset: 0x000C7B64
			internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060029BB RID: 10683 RVA: 0x000C996B File Offset: 0x000C7B6B
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x060029BC RID: 10684 RVA: 0x000C996E File Offset: 0x000C7B6E
			protected override bool IsEqualTo(Constant right)
			{
				return object.ReferenceEquals(this, right);
			}

			// Token: 0x060029BD RID: 10685 RVA: 0x000C9977 File Offset: 0x000C7B77
			internal override string ToUserString()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060029BE RID: 10686 RVA: 0x000C997E File Offset: 0x000C7B7E
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("AllOtherConstants");
			}

			// Token: 0x04000F80 RID: 3968
			internal static readonly Constant Instance = new Constant.AllOtherConstantsConstant();
		}
	}
}

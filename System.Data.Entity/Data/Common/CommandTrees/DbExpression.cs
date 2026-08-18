using System;
using System.ComponentModel;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;
using System.Data.Spatial;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003EF RID: 1007
	public abstract class DbExpression
	{
		// Token: 0x060035E5 RID: 13797 RVA: 0x000D0118 File Offset: 0x000CE318
		internal DbExpression(DbExpressionKind kind, TypeUsage type)
		{
			DbExpression.CheckExpressionKind(kind);
			this._kind = kind;
			if (!TypeSemantics.IsNullable(type))
			{
				type = type.ShallowCopy(new FacetValues
				{
					Nullable = new bool?(true)
				});
			}
			this._type = type;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x000D0165 File Offset: 0x000CE365
		public TypeUsage ResultType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000D016D File Offset: 0x000CE36D
		public DbExpressionKind ExpressionKind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x060035E8 RID: 13800
		public abstract void Accept(DbExpressionVisitor visitor);

		// Token: 0x060035E9 RID: 13801
		public abstract TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor);

		// Token: 0x060035EA RID: 13802 RVA: 0x000A1177 File Offset: 0x0009F377
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0009B148 File Offset: 0x00099348
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000D0175 File Offset: 0x000CE375
		public static DbExpression FromBinary(byte[] value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Binary);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000D0187 File Offset: 0x000CE387
		public static implicit operator DbExpression(byte[] value)
		{
			return DbExpression.FromBinary(value);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000D018F File Offset: 0x000CE38F
		public static DbExpression FromBoolean(bool? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Boolean);
			}
			if (!value.Value)
			{
				return DbExpressionBuilder.False;
			}
			return DbExpressionBuilder.True;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000D01B5 File Offset: 0x000CE3B5
		public static implicit operator DbExpression(bool? value)
		{
			return DbExpression.FromBoolean(value);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000D01BD File Offset: 0x000CE3BD
		public static DbExpression FromByte(byte? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Byte);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000D01E0 File Offset: 0x000CE3E0
		public static implicit operator DbExpression(byte? value)
		{
			return DbExpression.FromByte(value);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000D01E8 File Offset: 0x000CE3E8
		public static DbExpression FromDateTime(DateTime? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.DateTime);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000D020B File Offset: 0x000CE40B
		public static implicit operator DbExpression(DateTime? value)
		{
			return DbExpression.FromDateTime(value);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000D0213 File Offset: 0x000CE413
		public static DbExpression FromDateTimeOffset(DateTimeOffset? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.DateTimeOffset);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000D0237 File Offset: 0x000CE437
		public static implicit operator DbExpression(DateTimeOffset? value)
		{
			return DbExpression.FromDateTimeOffset(value);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000D023F File Offset: 0x000CE43F
		public static DbExpression FromDecimal(decimal? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Decimal);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000D0262 File Offset: 0x000CE462
		public static implicit operator DbExpression(decimal? value)
		{
			return DbExpression.FromDecimal(value);
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000D026A File Offset: 0x000CE46A
		public static DbExpression FromDouble(double? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Double);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000D028D File Offset: 0x000CE48D
		public static implicit operator DbExpression(double? value)
		{
			return DbExpression.FromDouble(value);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000D0295 File Offset: 0x000CE495
		public static DbExpression FromGeography(DbGeography value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Geography);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000D02A8 File Offset: 0x000CE4A8
		public static implicit operator DbExpression(DbGeography value)
		{
			return DbExpression.FromGeography(value);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000D02B0 File Offset: 0x000CE4B0
		public static DbExpression FromGeometry(DbGeometry value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Geometry);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000D02C3 File Offset: 0x000CE4C3
		public static implicit operator DbExpression(DbGeometry value)
		{
			return DbExpression.FromGeometry(value);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000D02CB File Offset: 0x000CE4CB
		public static DbExpression FromGuid(Guid? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Guid);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000D02EE File Offset: 0x000CE4EE
		public static implicit operator DbExpression(Guid? value)
		{
			return DbExpression.FromGuid(value);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000D02F6 File Offset: 0x000CE4F6
		public static DbExpression FromInt16(short? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int16);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000D031A File Offset: 0x000CE51A
		public static implicit operator DbExpression(short? value)
		{
			return DbExpression.FromInt16(value);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000D0322 File Offset: 0x000CE522
		public static DbExpression FromInt32(int? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int32);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000D0346 File Offset: 0x000CE546
		public static implicit operator DbExpression(int? value)
		{
			return DbExpression.FromInt32(value);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000D034E File Offset: 0x000CE54E
		public static DbExpression FromInt64(long? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int64);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000D0372 File Offset: 0x000CE572
		public static implicit operator DbExpression(long? value)
		{
			return DbExpression.FromInt64(value);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000D037A File Offset: 0x000CE57A
		public static DbExpression FromSingle(float? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Single);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000D039D File Offset: 0x000CE59D
		public static implicit operator DbExpression(float? value)
		{
			return DbExpression.FromSingle(value);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000D03A5 File Offset: 0x000CE5A5
		public static DbExpression FromString(string value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.String);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000D03B8 File Offset: 0x000CE5B8
		public static implicit operator DbExpression(string value)
		{
			return DbExpression.FromString(value);
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000D03C0 File Offset: 0x000CE5C0
		internal string Print()
		{
			return new ExpressionPrinter().Print(this);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000D03CD File Offset: 0x000CE5CD
		internal static void CheckExpressionKind(DbExpressionKind kind)
		{
			if (kind < DbExpressionKind.All || DbExpressionKind.Lambda < kind)
			{
				throw EntityUtil.InvalidEnumerationValue(typeof(DbExpressionKind), (int)kind);
			}
		}

		// Token: 0x040017F1 RID: 6129
		private readonly TypeUsage _type;

		// Token: 0x040017F2 RID: 6130
		private readonly DbExpressionKind _kind;
	}
}

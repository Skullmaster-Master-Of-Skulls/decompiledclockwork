using System;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Globalization;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000DC RID: 220
	public abstract class DbExpression
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x000250CE File Offset: 0x000232CE
		internal DbExpression()
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000250D8 File Offset: 0x000232D8
		internal DbExpression(DbExpressionKind kind, TypeUsage type, bool forceNullable = true)
		{
			DbExpression.CheckExpressionKind(kind);
			this._kind = kind;
			if (forceNullable && !TypeSemantics.IsNullable(type))
			{
				type = type.ShallowCopy(new FacetValues
				{
					Nullable = new bool?(true)
				});
			}
			this._type = type;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0002512A File Offset: 0x0002332A
		public virtual TypeUsage ResultType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00025132 File Offset: 0x00023332
		public virtual DbExpressionKind ExpressionKind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x060005AF RID: 1455
		public abstract void Accept(DbExpressionVisitor visitor);

		// Token: 0x060005B0 RID: 1456
		public abstract TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor);

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002513A File Offset: 0x0002333A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00025143 File Offset: 0x00023343
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002514B File Offset: 0x0002334B
		public static DbExpression FromBinary(byte[] value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Binary);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002515D File Offset: 0x0002335D
		public static implicit operator DbExpression(byte[] value)
		{
			return DbExpression.FromBinary(value);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00025165 File Offset: 0x00023365
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

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002518B File Offset: 0x0002338B
		public static implicit operator DbExpression(bool? value)
		{
			return DbExpression.FromBoolean(value);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00025193 File Offset: 0x00023393
		public static DbExpression FromByte(byte? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Byte);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000251B6 File Offset: 0x000233B6
		public static implicit operator DbExpression(byte? value)
		{
			return DbExpression.FromByte(value);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000251BE File Offset: 0x000233BE
		public static DbExpression FromDateTime(DateTime? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.DateTime);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000251E1 File Offset: 0x000233E1
		public static implicit operator DbExpression(DateTime? value)
		{
			return DbExpression.FromDateTime(value);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000251E9 File Offset: 0x000233E9
		public static DbExpression FromDateTimeOffset(DateTimeOffset? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.DateTimeOffset);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002520D File Offset: 0x0002340D
		public static implicit operator DbExpression(DateTimeOffset? value)
		{
			return DbExpression.FromDateTimeOffset(value);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00025215 File Offset: 0x00023415
		public static DbExpression FromDecimal(decimal? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Decimal);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00025238 File Offset: 0x00023438
		public static implicit operator DbExpression(decimal? value)
		{
			return DbExpression.FromDecimal(value);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00025240 File Offset: 0x00023440
		public static DbExpression FromDouble(double? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Double);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00025263 File Offset: 0x00023463
		public static implicit operator DbExpression(double? value)
		{
			return DbExpression.FromDouble(value);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0002526B File Offset: 0x0002346B
		public static DbExpression FromGeography(DbGeography value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Geography);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002527E File Offset: 0x0002347E
		public static implicit operator DbExpression(DbGeography value)
		{
			return DbExpression.FromGeography(value);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00025286 File Offset: 0x00023486
		public static DbExpression FromGeometry(DbGeometry value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Geometry);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00025299 File Offset: 0x00023499
		public static implicit operator DbExpression(DbGeometry value)
		{
			return DbExpression.FromGeometry(value);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000252A1 File Offset: 0x000234A1
		public static DbExpression FromGuid(Guid? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Guid);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000252C4 File Offset: 0x000234C4
		public static implicit operator DbExpression(Guid? value)
		{
			return DbExpression.FromGuid(value);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000252CC File Offset: 0x000234CC
		public static DbExpression FromInt16(short? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int16);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000252F0 File Offset: 0x000234F0
		public static implicit operator DbExpression(short? value)
		{
			return DbExpression.FromInt16(value);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000252F8 File Offset: 0x000234F8
		public static DbExpression FromInt32(int? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int32);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002531C File Offset: 0x0002351C
		public static implicit operator DbExpression(int? value)
		{
			return DbExpression.FromInt32(value);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00025324 File Offset: 0x00023524
		public static DbExpression FromInt64(long? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Int64);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00025348 File Offset: 0x00023548
		public static implicit operator DbExpression(long? value)
		{
			return DbExpression.FromInt64(value);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00025350 File Offset: 0x00023550
		public static DbExpression FromSingle(float? value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.Single);
			}
			return DbExpressionBuilder.Constant(value.Value);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00025373 File Offset: 0x00023573
		public static implicit operator DbExpression(float? value)
		{
			return DbExpression.FromSingle(value);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0002537B File Offset: 0x0002357B
		public static DbExpression FromString(string value)
		{
			if (value == null)
			{
				return DbExpressionBuilder.CreatePrimitiveNullExpression(PrimitiveTypeKind.String);
			}
			return DbExpressionBuilder.Constant(value);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0002538E File Offset: 0x0002358E
		public static implicit operator DbExpression(string value)
		{
			return DbExpression.FromString(value);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00025398 File Offset: 0x00023598
		internal static void CheckExpressionKind(DbExpressionKind kind)
		{
			if (kind < DbExpressionKind.All || DbExpressionKindHelper.Last < kind)
			{
				string name = typeof(DbExpressionKind).Name;
				string paramName = name;
				object p = name;
				int num = (int)kind;
				throw new ArgumentOutOfRangeException(paramName, Strings.ADP_InvalidEnumerationValue(p, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x040001BC RID: 444
		private readonly TypeUsage _type;

		// Token: 0x040001BD RID: 445
		private readonly DbExpressionKind _kind;
	}
}

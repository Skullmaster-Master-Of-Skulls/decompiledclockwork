using System;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x02000207 RID: 519
	public class DbCommandDefinition
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x0004EA66 File Offset: 0x0004CC66
		protected internal DbCommandDefinition(DbCommand prototype, Func<DbCommand, DbCommand> cloneMethod)
		{
			Check.NotNull<DbCommand>(prototype, "prototype");
			Check.NotNull<Func<DbCommand, DbCommand>>(cloneMethod, "cloneMethod");
			this._prototype = prototype;
			this._cloneMethod = cloneMethod;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004EA94 File Offset: 0x0004CC94
		protected DbCommandDefinition()
		{
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0004EA9C File Offset: 0x0004CC9C
		public virtual DbCommand CreateCommand()
		{
			return this._cloneMethod(this._prototype);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004EAB0 File Offset: 0x0004CCB0
		internal static void PopulateParameterFromTypeUsage(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			parameter.IsNullable = TypeSemantics.IsNullable(type);
			DbType dbType;
			if (Helper.IsPrimitiveType(type.EdmType) && DbCommandDefinition.TryGetDbTypeFromPrimitiveType((PrimitiveType)type.EdmType, out dbType))
			{
				DbType dbType2 = dbType;
				if (dbType2 <= DbType.Decimal)
				{
					if (dbType2 == DbType.Binary)
					{
						DbCommandDefinition.PopulateBinaryParameter(parameter, type, dbType, isOutParam);
						return;
					}
					switch (dbType2)
					{
					case DbType.DateTime:
						break;
					case DbType.Decimal:
						DbCommandDefinition.PopulateDecimalParameter(parameter, type, dbType);
						return;
					default:
						goto IL_86;
					}
				}
				else
				{
					switch (dbType2)
					{
					case DbType.String:
						DbCommandDefinition.PopulateStringParameter(parameter, type, isOutParam);
						return;
					case DbType.Time:
						break;
					default:
						if (dbType2 != DbType.DateTimeOffset)
						{
							goto IL_86;
						}
						break;
					}
				}
				DbCommandDefinition.PopulateDateTimeParameter(parameter, type, dbType);
				return;
				IL_86:
				parameter.DbType = dbType;
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004EB4C File Offset: 0x0004CD4C
		internal static bool TryGetDbTypeFromPrimitiveType(PrimitiveType type, out DbType dbType)
		{
			switch (type.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				dbType = DbType.Binary;
				return true;
			case PrimitiveTypeKind.Boolean:
				dbType = DbType.Boolean;
				return true;
			case PrimitiveTypeKind.Byte:
				dbType = DbType.Byte;
				return true;
			case PrimitiveTypeKind.DateTime:
				dbType = DbType.DateTime;
				return true;
			case PrimitiveTypeKind.Decimal:
				dbType = DbType.Decimal;
				return true;
			case PrimitiveTypeKind.Double:
				dbType = DbType.Double;
				return true;
			case PrimitiveTypeKind.Guid:
				dbType = DbType.Guid;
				return true;
			case PrimitiveTypeKind.Single:
				dbType = DbType.Single;
				return true;
			case PrimitiveTypeKind.SByte:
				dbType = DbType.SByte;
				return true;
			case PrimitiveTypeKind.Int16:
				dbType = DbType.Int16;
				return true;
			case PrimitiveTypeKind.Int32:
				dbType = DbType.Int32;
				return true;
			case PrimitiveTypeKind.Int64:
				dbType = DbType.Int64;
				return true;
			case PrimitiveTypeKind.String:
				dbType = DbType.String;
				return true;
			case PrimitiveTypeKind.Time:
				dbType = DbType.Time;
				return true;
			case PrimitiveTypeKind.DateTimeOffset:
				dbType = DbType.DateTimeOffset;
				return true;
			default:
				dbType = DbType.AnsiString;
				return false;
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0004EBFC File Offset: 0x0004CDFC
		private static void PopulateBinaryParameter(DbParameter parameter, TypeUsage type, DbType dbType, bool isOutParam)
		{
			parameter.DbType = dbType;
			DbCommandDefinition.SetParameterSize(parameter, type, isOutParam);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0004EC10 File Offset: 0x0004CE10
		private static void PopulateDecimalParameter(DbParameter parameter, TypeUsage type, DbType dbType)
		{
			parameter.DbType = dbType;
			byte precision;
			if (TypeHelpers.TryGetPrecision(type, out precision))
			{
				((IDbDataParameter)parameter).Precision = precision;
			}
			byte scale;
			if (TypeHelpers.TryGetScale(type, out scale))
			{
				((IDbDataParameter)parameter).Scale = scale;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0004EC48 File Offset: 0x0004CE48
		private static void PopulateDateTimeParameter(DbParameter parameter, TypeUsage type, DbType dbType)
		{
			parameter.DbType = dbType;
			byte precision;
			if (TypeHelpers.TryGetPrecision(type, out precision))
			{
				((IDbDataParameter)parameter).Precision = precision;
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0004EC70 File Offset: 0x0004CE70
		private static void PopulateStringParameter(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			bool flag = true;
			bool flag2 = false;
			if (!TypeHelpers.TryGetIsFixedLength(type, out flag2))
			{
				flag2 = false;
			}
			if (!TypeHelpers.TryGetIsUnicode(type, out flag))
			{
				flag = true;
			}
			if (flag2)
			{
				parameter.DbType = (flag ? DbType.StringFixedLength : DbType.AnsiStringFixedLength);
			}
			else
			{
				parameter.DbType = (flag ? DbType.String : DbType.AnsiString);
			}
			DbCommandDefinition.SetParameterSize(parameter, type, isOutParam);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		private static void SetParameterSize(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			Facet facet;
			if (type.Facets.TryGetValue("MaxLength", true, out facet) && facet.Value != null)
			{
				if (!Helper.IsUnboundedFacetValue(facet))
				{
					parameter.Size = (int)facet.Value;
					return;
				}
				if (isOutParam)
				{
					parameter.Size = int.MaxValue;
				}
			}
		}

		// Token: 0x04000575 RID: 1397
		private readonly DbCommand _prototype;

		// Token: 0x04000576 RID: 1398
		private readonly Func<DbCommand, DbCommand> _cloneMethod;
	}
}

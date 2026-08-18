using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common
{
	// Token: 0x0200032A RID: 810
	public class DbCommandDefinition
	{
		// Token: 0x06002F95 RID: 12181 RVA: 0x000B407C File Offset: 0x000B227C
		internal static DbCommandDefinition CreateCommandDefinition(DbCommand prototype)
		{
			EntityUtil.CheckArgumentNull<DbCommand>(prototype, "prototype");
			ICloneable cloneable = prototype as ICloneable;
			if (cloneable == null)
			{
				throw EntityUtil.CannotCloneStoreProvider();
			}
			DbCommand prototype2 = (DbCommand)cloneable.Clone();
			return new DbCommandDefinition(prototype2);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000B40B7 File Offset: 0x000B22B7
		protected DbCommandDefinition(DbCommand prototype)
		{
			EntityUtil.CheckArgumentNull<DbCommand>(prototype, "prototype");
			this._prototype = (prototype as ICloneable);
			if (this._prototype == null)
			{
				throw EntityUtil.CannotCloneStoreProvider();
			}
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x00002050 File Offset: 0x00000250
		protected DbCommandDefinition()
		{
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000B40E5 File Offset: 0x000B22E5
		public virtual DbCommand CreateCommand()
		{
			return (DbCommand)this._prototype.Clone();
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000B40F8 File Offset: 0x000B22F8
		internal static void PopulateParameterFromTypeUsage(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			EntityUtil.CheckArgumentNull<DbParameter>(parameter, "parameter");
			EntityUtil.CheckArgumentNull<TypeUsage>(type, "type");
			parameter.IsNullable = TypeSemantics.IsNullable(type);
			DbType dbType;
			if (Helper.IsPrimitiveType(type.EdmType) && DbCommandDefinition.TryGetDbTypeFromPrimitiveType((PrimitiveType)type.EdmType, out dbType))
			{
				if (dbType <= DbType.Decimal)
				{
					if (dbType == DbType.Binary)
					{
						DbCommandDefinition.PopulateBinaryParameter(parameter, type, dbType, isOutParam);
						return;
					}
					if (dbType != DbType.DateTime)
					{
						if (dbType != DbType.Decimal)
						{
							goto IL_8D;
						}
						DbCommandDefinition.PopulateDecimalParameter(parameter, type, dbType);
						return;
					}
				}
				else
				{
					if (dbType == DbType.String)
					{
						DbCommandDefinition.PopulateStringParameter(parameter, type, isOutParam);
						return;
					}
					if (dbType != DbType.Time && dbType != DbType.DateTimeOffset)
					{
						goto IL_8D;
					}
				}
				DbCommandDefinition.PopulateDateTimeParameter(parameter, type, dbType);
				return;
				IL_8D:
				parameter.DbType = dbType;
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000B419C File Offset: 0x000B239C
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

		// Token: 0x06002F9B RID: 12187 RVA: 0x000B424C File Offset: 0x000B244C
		private static void PopulateBinaryParameter(DbParameter parameter, TypeUsage type, DbType dbType, bool isOutParam)
		{
			parameter.DbType = dbType;
			DbCommandDefinition.SetParameterSize(parameter, type, isOutParam);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000B4260 File Offset: 0x000B2460
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

		// Token: 0x06002F9D RID: 12189 RVA: 0x000B4298 File Offset: 0x000B2498
		private static void PopulateDateTimeParameter(DbParameter parameter, TypeUsage type, DbType dbType)
		{
			parameter.DbType = dbType;
			byte precision;
			if (TypeHelpers.TryGetPrecision(type, out precision))
			{
				((IDbDataParameter)parameter).Precision = precision;
			}
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000B42C0 File Offset: 0x000B24C0
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

		// Token: 0x06002F9F RID: 12191 RVA: 0x000B4314 File Offset: 0x000B2514
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

		// Token: 0x0400147D RID: 5245
		private readonly ICloneable _prototype;
	}
}

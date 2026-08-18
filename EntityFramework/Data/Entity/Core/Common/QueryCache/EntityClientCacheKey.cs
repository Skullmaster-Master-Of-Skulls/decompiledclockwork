using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Internal;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002F8 RID: 760
	internal sealed class EntityClientCacheKey : QueryCacheKey
	{
		// Token: 0x06001AC8 RID: 6856 RVA: 0x0008584C File Offset: 0x00083A4C
		internal EntityClientCacheKey(EntityCommand entityCommand)
		{
			this._commandType = entityCommand.CommandType;
			this._eSqlStatement = entityCommand.CommandText;
			this._parametersToken = EntityClientCacheKey.GetParametersToken(entityCommand);
			this._parameterCount = entityCommand.Parameters.Count;
			this._hashCode = (this._commandType.GetHashCode() ^ this._eSqlStatement.GetHashCode() ^ this._parametersToken.GetHashCode());
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000858C4 File Offset: 0x00083AC4
		public override bool Equals(object otherObject)
		{
			if (typeof(EntityClientCacheKey) != otherObject.GetType())
			{
				return false;
			}
			EntityClientCacheKey entityClientCacheKey = (EntityClientCacheKey)otherObject;
			return this._commandType == entityClientCacheKey._commandType && this._parameterCount == entityClientCacheKey._parameterCount && this.Equals(entityClientCacheKey._eSqlStatement, this._eSqlStatement) && this.Equals(entityClientCacheKey._parametersToken, this._parametersToken);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00085935 File Offset: 0x00083B35
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00085940 File Offset: 0x00083B40
		private static string GetTypeUsageToken(TypeUsage type)
		{
			string result;
			if (object.ReferenceEquals(type, DbTypeMap.AnsiString))
			{
				result = "AnsiString";
			}
			else if (object.ReferenceEquals(type, DbTypeMap.AnsiStringFixedLength))
			{
				result = "AnsiStringFixedLength";
			}
			else if (object.ReferenceEquals(type, DbTypeMap.String))
			{
				result = "String";
			}
			else if (object.ReferenceEquals(type, DbTypeMap.StringFixedLength))
			{
				result = "StringFixedLength";
			}
			else if (object.ReferenceEquals(type, DbTypeMap.Xml))
			{
				result = "String";
			}
			else if (TypeSemantics.IsEnumerationType(type))
			{
				result = type.EdmType.FullName;
			}
			else
			{
				result = type.EdmType.Name;
			}
			return result;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000859DC File Offset: 0x00083BDC
		private static string GetParametersToken(EntityCommand entityCommand)
		{
			if (entityCommand.Parameters == null || entityCommand.Parameters.Count == 0)
			{
				return "@@0";
			}
			Dictionary<string, TypeUsage> parameterTypeUsage = entityCommand.GetParameterTypeUsage();
			if (1 == parameterTypeUsage.Count)
			{
				return "@@1:" + entityCommand.Parameters[0].ParameterName + ":" + EntityClientCacheKey.GetTypeUsageToken(parameterTypeUsage[entityCommand.Parameters[0].ParameterName]);
			}
			StringBuilder stringBuilder = new StringBuilder(entityCommand.Parameters.Count * 20);
			stringBuilder.Append("@@");
			stringBuilder.Append(entityCommand.Parameters.Count);
			stringBuilder.Append(":");
			string value = "";
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in parameterTypeUsage)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append(":");
				stringBuilder.Append(EntityClientCacheKey.GetTypeUsageToken(keyValuePair.Value));
				value = ";";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00085B14 File Offset: 0x00083D14
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				Enum.GetName(typeof(CommandType), this._commandType),
				this._eSqlStatement,
				this._parametersToken
			});
		}

		// Token: 0x04000949 RID: 2377
		private readonly CommandType _commandType;

		// Token: 0x0400094A RID: 2378
		private readonly string _eSqlStatement;

		// Token: 0x0400094B RID: 2379
		private readonly string _parametersToken;

		// Token: 0x0400094C RID: 2380
		private readonly int _parameterCount;

		// Token: 0x0400094D RID: 2381
		private readonly int _hashCode;
	}
}

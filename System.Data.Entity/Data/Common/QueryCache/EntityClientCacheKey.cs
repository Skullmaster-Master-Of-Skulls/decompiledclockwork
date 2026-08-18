using System;
using System.Collections.Generic;
using System.Data.Common.Internal;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003E1 RID: 993
	internal sealed class EntityClientCacheKey : QueryCacheKey
	{
		// Token: 0x06003548 RID: 13640 RVA: 0x000CF670 File Offset: 0x000CD870
		internal EntityClientCacheKey(EntityCommand entityCommand)
		{
			this._commandType = entityCommand.CommandType;
			this._eSqlStatement = entityCommand.CommandText;
			this._parametersToken = EntityClientCacheKey.GetParametersToken(entityCommand);
			this._parameterCount = entityCommand.Parameters.Count;
			this._hashCode = (this._commandType.GetHashCode() ^ this._eSqlStatement.GetHashCode() ^ this._parametersToken.GetHashCode());
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000CF6E8 File Offset: 0x000CD8E8
		public override bool Equals(object otherObject)
		{
			if (typeof(EntityClientCacheKey) != otherObject.GetType())
			{
				return false;
			}
			EntityClientCacheKey entityClientCacheKey = (EntityClientCacheKey)otherObject;
			return this._commandType == entityClientCacheKey._commandType && this._parameterCount == entityClientCacheKey._parameterCount && this.Equals(entityClientCacheKey._eSqlStatement, this._eSqlStatement) && this.Equals(entityClientCacheKey._parametersToken, this._parametersToken);
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000CF759 File Offset: 0x000CD959
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000CF764 File Offset: 0x000CD964
		private static string GetTypeUsageToken(TypeUsage type)
		{
			string result;
			if (type == DbTypeMap.AnsiString)
			{
				result = "AnsiString";
			}
			else if (type == DbTypeMap.AnsiStringFixedLength)
			{
				result = "AnsiStringFixedLength";
			}
			else if (type == DbTypeMap.String)
			{
				result = "String";
			}
			else if (type == DbTypeMap.StringFixedLength)
			{
				result = "StringFixedLength";
			}
			else if (type == DbTypeMap.Xml)
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

		// Token: 0x0600354C RID: 13644 RVA: 0x000CF7E8 File Offset: 0x000CD9E8
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

		// Token: 0x0600354D RID: 13645 RVA: 0x000CF920 File Offset: 0x000CDB20
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				Enum.GetName(typeof(CommandType), this._commandType),
				this._eSqlStatement,
				this._parametersToken
			});
		}

		// Token: 0x04001798 RID: 6040
		private readonly CommandType _commandType;

		// Token: 0x04001799 RID: 6041
		private readonly string _eSqlStatement;

		// Token: 0x0400179A RID: 6042
		private readonly string _parametersToken;

		// Token: 0x0400179B RID: 6043
		private readonly int _parameterCount;

		// Token: 0x0400179C RID: 6044
		private readonly int _hashCode;
	}
}

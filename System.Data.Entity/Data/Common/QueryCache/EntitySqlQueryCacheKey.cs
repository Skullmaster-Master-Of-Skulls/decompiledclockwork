using System;
using System.Data.Objects;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003DC RID: 988
	internal sealed class EntitySqlQueryCacheKey : QueryCacheKey
	{
		// Token: 0x0600352A RID: 13610 RVA: 0x000CF034 File Offset: 0x000CD234
		internal EntitySqlQueryCacheKey(string defaultContainerName, string eSqlStatement, int parameterCount, string parametersToken, string includePathsToken, MergeOption mergeOption, Type resultType)
		{
			this._defaultContainer = defaultContainerName;
			this._eSqlStatement = eSqlStatement;
			this._parameterCount = parameterCount;
			this._parametersToken = parametersToken;
			this._includePathsToken = includePathsToken;
			this._mergeOption = mergeOption;
			this._resultType = resultType;
			int num = this._eSqlStatement.GetHashCode() ^ this._mergeOption.GetHashCode();
			if (this._parametersToken != null)
			{
				num ^= this._parametersToken.GetHashCode();
			}
			if (this._includePathsToken != null)
			{
				num ^= this._includePathsToken.GetHashCode();
			}
			if (this._defaultContainer != null)
			{
				num ^= this._defaultContainer.GetHashCode();
			}
			this._hashCode = num;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000CF0E4 File Offset: 0x000CD2E4
		public override bool Equals(object otherObject)
		{
			if (typeof(EntitySqlQueryCacheKey) != otherObject.GetType())
			{
				return false;
			}
			EntitySqlQueryCacheKey entitySqlQueryCacheKey = (EntitySqlQueryCacheKey)otherObject;
			return this._parameterCount == entitySqlQueryCacheKey._parameterCount && this._mergeOption == entitySqlQueryCacheKey._mergeOption && this.Equals(entitySqlQueryCacheKey._defaultContainer, this._defaultContainer) && this.Equals(entitySqlQueryCacheKey._eSqlStatement, this._eSqlStatement) && this.Equals(entitySqlQueryCacheKey._includePathsToken, this._includePathsToken) && this.Equals(entitySqlQueryCacheKey._parametersToken, this._parametersToken) && object.Equals(entitySqlQueryCacheKey._resultType, this._resultType);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000CF190 File Offset: 0x000CD390
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000CF198 File Offset: 0x000CD398
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				this._defaultContainer,
				this._eSqlStatement,
				this._parametersToken,
				this._includePathsToken,
				Enum.GetName(typeof(MergeOption), this._mergeOption)
			});
		}

		// Token: 0x0400177E RID: 6014
		private readonly int _hashCode;

		// Token: 0x0400177F RID: 6015
		private string _defaultContainer;

		// Token: 0x04001780 RID: 6016
		private readonly string _eSqlStatement;

		// Token: 0x04001781 RID: 6017
		private readonly string _parametersToken;

		// Token: 0x04001782 RID: 6018
		private readonly int _parameterCount;

		// Token: 0x04001783 RID: 6019
		private readonly string _includePathsToken;

		// Token: 0x04001784 RID: 6020
		private readonly MergeOption _mergeOption;

		// Token: 0x04001785 RID: 6021
		private readonly Type _resultType;
	}
}

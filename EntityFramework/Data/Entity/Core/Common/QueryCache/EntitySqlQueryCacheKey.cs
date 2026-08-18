using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002F9 RID: 761
	internal sealed class EntitySqlQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06001ACE RID: 6862 RVA: 0x00085B64 File Offset: 0x00083D64
		internal EntitySqlQueryCacheKey(string defaultContainerName, string eSqlStatement, int parameterCount, string parametersToken, string includePathsToken, MergeOption mergeOption, bool streaming, Type resultType)
		{
			this._defaultContainer = defaultContainerName;
			this._eSqlStatement = eSqlStatement;
			this._parameterCount = parameterCount;
			this._parametersToken = parametersToken;
			this._includePathsToken = includePathsToken;
			this._mergeOption = mergeOption;
			this._streaming = streaming;
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

		// Token: 0x06001ACF RID: 6863 RVA: 0x00085C1C File Offset: 0x00083E1C
		public override bool Equals(object otherObject)
		{
			if (typeof(EntitySqlQueryCacheKey) != otherObject.GetType())
			{
				return false;
			}
			EntitySqlQueryCacheKey entitySqlQueryCacheKey = (EntitySqlQueryCacheKey)otherObject;
			return this._parameterCount == entitySqlQueryCacheKey._parameterCount && this._mergeOption == entitySqlQueryCacheKey._mergeOption && this._streaming == entitySqlQueryCacheKey._streaming && this.Equals(entitySqlQueryCacheKey._defaultContainer, this._defaultContainer) && this.Equals(entitySqlQueryCacheKey._eSqlStatement, this._eSqlStatement) && this.Equals(entitySqlQueryCacheKey._includePathsToken, this._includePathsToken) && this.Equals(entitySqlQueryCacheKey._parametersToken, this._parametersToken) && object.Equals(entitySqlQueryCacheKey._resultType, this._resultType);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00085CD6 File Offset: 0x00083ED6
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00085CE0 File Offset: 0x00083EE0
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

		// Token: 0x0400094E RID: 2382
		private readonly int _hashCode;

		// Token: 0x0400094F RID: 2383
		private readonly string _defaultContainer;

		// Token: 0x04000950 RID: 2384
		private readonly string _eSqlStatement;

		// Token: 0x04000951 RID: 2385
		private readonly string _parametersToken;

		// Token: 0x04000952 RID: 2386
		private readonly int _parameterCount;

		// Token: 0x04000953 RID: 2387
		private readonly string _includePathsToken;

		// Token: 0x04000954 RID: 2388
		private readonly MergeOption _mergeOption;

		// Token: 0x04000955 RID: 2389
		private readonly Type _resultType;

		// Token: 0x04000956 RID: 2390
		private readonly bool _streaming;
	}
}

using System;
using System.Data.Objects;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003D9 RID: 985
	internal sealed class LinqQueryCacheKey : QueryCacheKey
	{
		// Token: 0x0600351C RID: 13596 RVA: 0x000CECB4 File Offset: 0x000CCEB4
		internal LinqQueryCacheKey(string expressionKey, int parameterCount, string parametersToken, string includePathsToken, MergeOption mergeOption, bool useCSharpNullComparisonBehavior, Type resultType)
		{
			this._expressionKey = expressionKey;
			this._parameterCount = parameterCount;
			this._parametersToken = parametersToken;
			this._includePathsToken = includePathsToken;
			this._mergeOption = mergeOption;
			this._resultType = resultType;
			this._useCSharpNullComparisonBehavior = useCSharpNullComparisonBehavior;
			int num = this._expressionKey.GetHashCode() ^ this._mergeOption.GetHashCode();
			if (this._parametersToken != null)
			{
				num ^= this._parametersToken.GetHashCode();
			}
			if (this._includePathsToken != null)
			{
				num ^= this._includePathsToken.GetHashCode();
			}
			num ^= this._useCSharpNullComparisonBehavior.GetHashCode();
			this._hashCode = num;
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000CED5C File Offset: 0x000CCF5C
		public override bool Equals(object otherObject)
		{
			if (typeof(LinqQueryCacheKey) != otherObject.GetType())
			{
				return false;
			}
			LinqQueryCacheKey linqQueryCacheKey = (LinqQueryCacheKey)otherObject;
			return this._parameterCount == linqQueryCacheKey._parameterCount && this._mergeOption == linqQueryCacheKey._mergeOption && this.Equals(linqQueryCacheKey._expressionKey, this._expressionKey) && this.Equals(linqQueryCacheKey._includePathsToken, this._includePathsToken) && this.Equals(linqQueryCacheKey._parametersToken, this._parametersToken) && object.Equals(linqQueryCacheKey._resultType, this._resultType) && object.Equals(linqQueryCacheKey._useCSharpNullComparisonBehavior, this._useCSharpNullComparisonBehavior);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000CEE11 File Offset: 0x000CD011
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000CEE1C File Offset: 0x000CD01C
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				this._expressionKey,
				this._parametersToken,
				this._includePathsToken,
				Enum.GetName(typeof(MergeOption), this._mergeOption),
				this._useCSharpNullComparisonBehavior.ToString()
			});
		}

		// Token: 0x04001773 RID: 6003
		private readonly int _hashCode;

		// Token: 0x04001774 RID: 6004
		private readonly string _expressionKey;

		// Token: 0x04001775 RID: 6005
		private readonly string _parametersToken;

		// Token: 0x04001776 RID: 6006
		private readonly int _parameterCount;

		// Token: 0x04001777 RID: 6007
		private readonly string _includePathsToken;

		// Token: 0x04001778 RID: 6008
		private readonly MergeOption _mergeOption;

		// Token: 0x04001779 RID: 6009
		private readonly Type _resultType;

		// Token: 0x0400177A RID: 6010
		private readonly bool _useCSharpNullComparisonBehavior;
	}
}

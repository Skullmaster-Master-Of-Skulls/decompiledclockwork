using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002FA RID: 762
	internal sealed class LinqQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00085D40 File Offset: 0x00083F40
		internal LinqQueryCacheKey(string expressionKey, int parameterCount, string parametersToken, string includePathsToken, MergeOption mergeOption, bool streaming, bool useCSharpNullComparisonBehavior, Type resultType)
		{
			this._expressionKey = expressionKey;
			this._parameterCount = parameterCount;
			this._parametersToken = parametersToken;
			this._includePathsToken = includePathsToken;
			this._mergeOption = mergeOption;
			this._streaming = streaming;
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

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00085DF0 File Offset: 0x00083FF0
		public override bool Equals(object otherObject)
		{
			if (typeof(LinqQueryCacheKey) != otherObject.GetType())
			{
				return false;
			}
			LinqQueryCacheKey linqQueryCacheKey = (LinqQueryCacheKey)otherObject;
			return this._parameterCount == linqQueryCacheKey._parameterCount && this._mergeOption == linqQueryCacheKey._mergeOption && this._streaming == linqQueryCacheKey._streaming && this.Equals(linqQueryCacheKey._expressionKey, this._expressionKey) && this.Equals(linqQueryCacheKey._includePathsToken, this._includePathsToken) && this.Equals(linqQueryCacheKey._parametersToken, this._parametersToken) && object.Equals(linqQueryCacheKey._resultType, this._resultType) && object.Equals(linqQueryCacheKey._useCSharpNullComparisonBehavior, this._useCSharpNullComparisonBehavior);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00085EB6 File Offset: 0x000840B6
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00085EC0 File Offset: 0x000840C0
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

		// Token: 0x04000957 RID: 2391
		private readonly int _hashCode;

		// Token: 0x04000958 RID: 2392
		private readonly string _expressionKey;

		// Token: 0x04000959 RID: 2393
		private readonly string _parametersToken;

		// Token: 0x0400095A RID: 2394
		private readonly int _parameterCount;

		// Token: 0x0400095B RID: 2395
		private readonly string _includePathsToken;

		// Token: 0x0400095C RID: 2396
		private readonly MergeOption _mergeOption;

		// Token: 0x0400095D RID: 2397
		private readonly Type _resultType;

		// Token: 0x0400095E RID: 2398
		private readonly bool _streaming;

		// Token: 0x0400095F RID: 2399
		private readonly bool _useCSharpNullComparisonBehavior;
	}
}

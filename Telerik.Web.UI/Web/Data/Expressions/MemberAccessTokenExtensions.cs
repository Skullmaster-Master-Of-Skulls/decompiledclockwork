using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BBD RID: 7101
	internal static class MemberAccessTokenExtensions
	{
		// Token: 0x06011285 RID: 70277 RVA: 0x003C884C File Offset: 0x003C6A4C
		public static Expression CreateMemberAccessExpression(this IMemberAccessToken token, Expression instance)
		{
			MemberInfo memberInfoForType = token.GetMemberInfoForType(instance.Type);
			if (memberInfoForType == null)
			{
				throw new ArgumentException(MemberAccessTokenExtensions.FormatInvalidTokenErrorMessage(token, instance.Type));
			}
			IndexerToken indexerToken = token as IndexerToken;
			if (indexerToken != null)
			{
				IEnumerable<Expression> indexerArguments = indexerToken.GetIndexerArguments();
				return Expression.Call(instance, (MethodInfo)memberInfoForType, indexerArguments);
			}
			return Expression.MakeMemberAccess(instance, memberInfoForType);
		}

		// Token: 0x06011286 RID: 70278 RVA: 0x003C88B8 File Offset: 0x003C6AB8
		private static string FormatInvalidTokenErrorMessage(IMemberAccessToken token, Type type)
		{
			PropertyToken propertyToken = token as PropertyToken;
			string text;
			string text2;
			if (propertyToken != null)
			{
				text = "property or field";
				text2 = propertyToken.PropertyName;
			}
			else
			{
				text = "indexer with arguments";
				IEnumerable<string> source = from a in ((IndexerToken)token).Arguments
				where a != null
				select a.ToString();
				text2 = string.Join(",", source.ToArray<string>());
			}
			return string.Format(CultureInfo.CurrentCulture, "Invalid {0} - '{1}' for type: {2}", new object[]
			{
				text,
				text2,
				type.GetTypeName()
			});
		}

		// Token: 0x06011287 RID: 70279 RVA: 0x003C897A File Offset: 0x003C6B7A
		private static IEnumerable<Expression> GetIndexerArguments(this IndexerToken indexerToken)
		{
			return from a in indexerToken.Arguments
			select Expression.Constant(a);
		}

		// Token: 0x06011288 RID: 70280 RVA: 0x003C89A4 File Offset: 0x003C6BA4
		private static MemberInfo GetMemberInfoForType(this IMemberAccessToken token, Type targetType)
		{
			PropertyToken propertyToken = token as PropertyToken;
			if (propertyToken != null)
			{
				return MemberAccessTokenExtensions.GetMemberInfoFromPropertyToken(propertyToken, targetType);
			}
			IndexerToken indexerToken = token as IndexerToken;
			if (indexerToken != null)
			{
				return MemberAccessTokenExtensions.GetMemberInfoFromIndexerToken(indexerToken, targetType);
			}
			throw new InvalidOperationException(token.GetType().GetTypeName() + " is not supported");
		}

		// Token: 0x06011289 RID: 70281 RVA: 0x003C89EF File Offset: 0x003C6BEF
		private static MemberInfo GetMemberInfoFromPropertyToken(PropertyToken token, Type targetType)
		{
			return targetType.FindPropertyOrField(token.PropertyName);
		}

		// Token: 0x0601128A RID: 70282 RVA: 0x003C8A08 File Offset: 0x003C6C08
		private static MemberInfo GetMemberInfoFromIndexerToken(IndexerToken token, Type targetType)
		{
			PropertyInfo indexerPropertyInfo = targetType.GetIndexerPropertyInfo((from a in token.Arguments
			select a.GetType()).ToArray<Type>());
			if (indexerPropertyInfo != null)
			{
				return indexerPropertyInfo.GetGetMethod();
			}
			return null;
		}
	}
}

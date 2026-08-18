using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x02000059 RID: 89
	public interface IExpressionBinder
	{
		// Token: 0x06000357 RID: 855
		bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result);

		// Token: 0x06000358 RID: 856
		MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, ConcurrentDictionary<ExpressionRequest, int> typePairCount);
	}
}

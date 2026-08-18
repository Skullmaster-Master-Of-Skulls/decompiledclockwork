using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C4 RID: 1220
	internal static class QueryDataModel
	{
		// Token: 0x06002E2B RID: 11819 RVA: 0x000B3EFC File Offset: 0x000B20FC
		static QueryDataModel()
		{
			QueryDataModel.axes = new QueryAxis[]
			{
				new QueryAxis(QueryAxisType.None, AxisDirection.Forward, QueryNodeType.Any, QueryNodeType.Any),
				new QueryAxis(QueryAxisType.Ancestor, AxisDirection.Reverse, QueryNodeType.Element, QueryNodeType.Ancestor),
				new QueryAxis(QueryAxisType.AncestorOrSelf, AxisDirection.Reverse, QueryNodeType.Element, QueryNodeType.All),
				new QueryAxis(QueryAxisType.Attribute, AxisDirection.Forward, QueryNodeType.Attribute, QueryNodeType.Attribute),
				new QueryAxis(QueryAxisType.Child, AxisDirection.Forward, QueryNodeType.Element, QueryNodeType.ChildNodes),
				new QueryAxis(QueryAxisType.Descendant, AxisDirection.Forward, QueryNodeType.Element, QueryNodeType.ChildNodes),
				new QueryAxis(QueryAxisType.DescendantOrSelf, AxisDirection.Forward, QueryNodeType.Element, QueryNodeType.All),
				new QueryAxis(QueryAxisType.Following, AxisDirection.Forward, QueryNodeType.Element, QueryNodeType.ChildNodes),
				new QueryAxis(QueryAxisType.FollowingSibling, AxisDirection.Forward, QueryNodeType.Element, QueryNodeType.ChildNodes),
				new QueryAxis(QueryAxisType.Namespace, AxisDirection.Forward, QueryNodeType.Namespace, QueryNodeType.Namespace),
				new QueryAxis(QueryAxisType.Parent, AxisDirection.Reverse, QueryNodeType.Element, QueryNodeType.Ancestor),
				new QueryAxis(QueryAxisType.Preceding, AxisDirection.Reverse, QueryNodeType.Element, QueryNodeType.ChildNodes),
				new QueryAxis(QueryAxisType.PrecedingSibling, AxisDirection.Reverse, QueryNodeType.Element, QueryNodeType.All),
				new QueryAxis(QueryAxisType.Self, AxisDirection.Forward, QueryNodeType.Element, QueryNodeType.All)
			};
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000B4037 File Offset: 0x000B2237
		internal static bool IsAttribute(string ns)
		{
			return string.CompareOrdinal("http://www.w3.org/2000/xmlns/", ns) != 0;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000B4047 File Offset: 0x000B2247
		internal static QueryAxis GetAxis(QueryAxisType type)
		{
			return QueryDataModel.axes[(int)type];
		}

		// Token: 0x04002537 RID: 9527
		internal static QueryAxis[] axes;

		// Token: 0x04002538 RID: 9528
		internal static string Wildcard = "*";
	}
}

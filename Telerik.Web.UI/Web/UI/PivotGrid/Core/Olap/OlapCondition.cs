using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x0200070B RID: 1803
	[DataContract]
	public abstract class OlapCondition : Condition
	{
		// Token: 0x06003FEA RID: 16362
		internal abstract IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options);

		// Token: 0x06003FEB RID: 16363 RVA: 0x000C9F84 File Offset: 0x000C8184
		internal static object GetEffectiveValue(object value)
		{
			MemberDistinctValue memberDistinctValue = value as MemberDistinctValue;
			if (memberDistinctValue == null)
			{
				return value;
			}
			return memberDistinctValue.UniqueName;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000C9FA4 File Offset: 0x000C81A4
		internal static OlapExpression GetMembersExpressionForHierarchy(OlapFieldInfo info)
		{
			OlapExpression olapExpression = new OlapIdentifierExpression(info.Name, false);
			if (info.SupportsMembersFunction)
			{
				olapExpression = new OlapMemberFuntionExpression("Children", olapExpression);
			}
			return olapExpression;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000C9FD3 File Offset: 0x000C81D3
		internal static OlapExpression GetDimensionExpression(OlapExpressionOptions options)
		{
			if (options.DimensionExpression != null)
			{
				return options.DimensionExpression;
			}
			if (options.MemberInfo != null)
			{
				return OlapCondition.GetMembersExpressionForHierarchy(options.MemberInfo);
			}
			return OlapCondition.GetMembersExpressionForHierarchy(options.HierarchyInfo);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000CA008 File Offset: 0x000C8208
		internal static object TransformConditionValueToDistinctItem(object item)
		{
			string text = item as string;
			if (text != null)
			{
				return new MemberDistinctValue(text);
			}
			return item;
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000CA028 File Offset: 0x000C8228
		internal static object TransformDistinctItemToConditionValue(object item)
		{
			MemberDistinctValue memberDistinctValue = item as MemberDistinctValue;
			if (memberDistinctValue == null)
			{
				return item;
			}
			return memberDistinctValue.UniqueName;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000CA048 File Offset: 0x000C8248
		internal static object GetDistinctItemFromValue(object value, IEnumerable<object> distinctItems)
		{
			if (value == null)
			{
				return value;
			}
			if (value is MemberDistinctValue || distinctItems == null)
			{
				return value;
			}
			foreach (object obj in distinctItems)
			{
				MemberDistinctValue memberDistinctValue = obj as MemberDistinctValue;
				if (memberDistinctValue != null && memberDistinctValue.UniqueName.Equals(value.ToString()))
				{
					return obj;
				}
			}
			return value;
		}
	}
}

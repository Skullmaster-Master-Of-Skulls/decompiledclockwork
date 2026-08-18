using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;
using Telerik.Web.UI.PivotGrid.Core.ReportFilter;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CFC RID: 3324
	internal class MdxQueryBuilder
	{
		// Token: 0x06007C04 RID: 31748 RVA: 0x001C7D9A File Offset: 0x001C5F9A
		public MdxQueryBuilder(string cubeName, IOlapPivotConfiguration pivotConfiguration)
		{
			if (string.IsNullOrEmpty(cubeName))
			{
				throw new ArgumentNullException("cubeName", "Value cannot be null or empty.");
			}
			if (pivotConfiguration == null)
			{
				throw new ArgumentNullException("pivotConfiguration");
			}
			this.cubeName = cubeName;
			this.pivotConfiguration = pivotConfiguration;
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x001C7DD8 File Offset: 0x001C5FD8
		private IList<OlapExpression> GetDescriptionSetExpressions()
		{
			List<OlapExpression> list = new List<OlapExpression>();
			OlapSetExpression groupDescriptionSetExpression = MdxQueryBuilder.GetGroupDescriptionSetExpression(this.pivotConfiguration.PivotColumnGroupDescriptions);
			OlapSetExpression aggregateDescriptionSetExpression = MdxQueryBuilder.GetAggregateDescriptionSetExpression(this.pivotConfiguration.PivotAggregateDescriptions);
			OlapSetExpression groupDescriptionSetExpression2 = MdxQueryBuilder.GetGroupDescriptionSetExpression(this.pivotConfiguration.PivotRowGroupDescriptions);
			if (groupDescriptionSetExpression != null && aggregateDescriptionSetExpression != null)
			{
				OlapFunctionExpression item = new OlapFunctionExpression("CrossJoin", new OlapSetExpression[]
				{
					groupDescriptionSetExpression,
					aggregateDescriptionSetExpression
				});
				list.Add(item);
			}
			else if (groupDescriptionSetExpression != null)
			{
				list.Add(groupDescriptionSetExpression);
			}
			else if (aggregateDescriptionSetExpression != null)
			{
				list.Add(aggregateDescriptionSetExpression);
			}
			if (groupDescriptionSetExpression2 != null)
			{
				list.Add(groupDescriptionSetExpression2);
			}
			return list;
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x001C7E70 File Offset: 0x001C6070
		private static OlapSetExpression GetGroupDescriptionSetExpression(IList<OlapGroupDescription> descriptions)
		{
			if (descriptions.Count == 0)
			{
				return null;
			}
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (OlapGroupDescription dimensionDescription in descriptions)
			{
				OlapExpression expressionForDimension = MdxQueryBuilder.GetExpressionForDimension(dimensionDescription);
				list.Add(expressionForDimension);
			}
			if (descriptions.Count == 1)
			{
				return new OlapSetExpression(list);
			}
			OlapFunctionExpression olapFunctionExpression = new OlapFunctionExpression("CrossJoin", list);
			return new OlapSetExpression(new OlapFunctionExpression[]
			{
				olapFunctionExpression
			});
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x001C7F04 File Offset: 0x001C6104
		private static OlapSetExpression GetAggregateDescriptionSetExpression(IList<OlapAggregateDescription> descriptions)
		{
			if (descriptions.Count == 0)
			{
				return null;
			}
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (OlapAggregateDescription olapAggregateDescription in descriptions)
			{
				OlapIdentifierExpression item = new OlapIdentifierExpression(olapAggregateDescription.MemberName, false);
				list.Add(item);
			}
			return new OlapSetExpression(list);
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x001C7F70 File Offset: 0x001C6170
		public string BuildQuery()
		{
			IEnumerable<OlapSelectQueryAxisClauseExpression> axisExpressions = this.GetAxisExpressions();
			OlapExpression compositeFilterExpression = this.GetCompositeFilterExpression();
			OlapExpression node;
			if (compositeFilterExpression == null)
			{
				node = new OlapSelectClauseExpression(axisExpressions, new OlapIdentifierExpression(this.cubeName));
			}
			else
			{
				OlapWrapperExpression from = new OlapWrapperExpression(new OlapExpression[]
				{
					compositeFilterExpression
				}, OlapWrapperExpressionType.Parenthesis);
				node = new OlapSelectClauseExpression(axisExpressions, from);
			}
			return OlapExpressionStringBuilder.ExpressionNodeToString(node);
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x001C7FCC File Offset: 0x001C61CC
		private IEnumerable<OlapSelectQueryAxisClauseExpression> GetAxisExpressions()
		{
			string[] dimensionProperties = new string[]
			{
				"PARENT_UNIQUE_NAME",
				"HIERARCHY_UNIQUE_NAME",
				"KEY0",
				"KEY1",
				"KEY2",
				"KEY3",
				"KEY4"
			};
			IList<OlapExpression> descriptionSetExpressions = this.GetDescriptionSetExpressions();
			List<OlapSelectQueryAxisClauseExpression> list = new List<OlapSelectQueryAxisClauseExpression>();
			foreach (OlapExpression setExpression in descriptionSetExpressions)
			{
				OlapSelectQueryAxisClauseExpression item = new OlapSelectQueryAxisClauseExpression(setExpression, dimensionProperties, true);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x001C807C File Offset: 0x001C627C
		private OlapExpression GetCompositeFilterExpression()
		{
			Stack<OlapExpression> separateFilterExpressions = this.GetSeparateFilterExpressions();
			return this.BuildCompositeFilterExpression(separateFilterExpressions);
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x001C809C File Offset: 0x001C629C
		private Stack<OlapExpression> GetSeparateFilterExpressions()
		{
			Stack<OlapExpression> stack = new Stack<OlapExpression>();
			this.AddGroupFilterExpressions(stack);
			this.AddReportFilterExpressions(stack);
			return stack;
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x001C80C0 File Offset: 0x001C62C0
		private OlapExpression BuildCompositeFilterExpression(Stack<OlapExpression> expressions)
		{
			if (expressions.Count == 0)
			{
				return null;
			}
			OlapExpression setExpression = expressions.Pop();
			OlapSelectQueryAxisClauseExpression olapSelectQueryAxisClauseExpression = new OlapSelectQueryAxisClauseExpression(setExpression, new List<string>(), false);
			OlapSelectClauseExpression olapSelectClauseExpression = new OlapSelectClauseExpression(new OlapSelectQueryAxisClauseExpression[]
			{
				olapSelectQueryAxisClauseExpression
			}, new OlapIdentifierExpression(this.cubeName));
			OlapSelectClauseExpression olapSelectClauseExpression2 = olapSelectClauseExpression;
			while (expressions.Count > 0)
			{
				OlapExpression setExpression2 = expressions.Pop();
				olapSelectQueryAxisClauseExpression = new OlapSelectQueryAxisClauseExpression(setExpression2, new List<string>(), false);
				OlapWrapperExpression from = new OlapWrapperExpression(new OlapSelectClauseExpression[]
				{
					olapSelectClauseExpression2
				}, OlapWrapperExpressionType.Parenthesis);
				olapSelectClauseExpression2 = new OlapSelectClauseExpression(new OlapSelectQueryAxisClauseExpression[]
				{
					olapSelectQueryAxisClauseExpression
				}, from);
			}
			return olapSelectClauseExpression2;
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x001C8160 File Offset: 0x001C6360
		private void AddReportFilterExpressions(Stack<OlapExpression> expressions)
		{
			foreach (OlapFilterDescription olapFilterDescription in this.pivotConfiguration.PivotFilterDescriptions)
			{
				IHierarchyFilterDescription hierarchyFilterDescription = olapFilterDescription;
				if (hierarchyFilterDescription == null || hierarchyFilterDescription.IgnoreChildren)
				{
					IEnumerable<OlapExpression> expressions2 = olapFilterDescription.GetExpressions();
					using (IEnumerator<OlapExpression> enumerator2 = expressions2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							OlapExpression item = enumerator2.Current;
							expressions.Push(item);
						}
						continue;
					}
				}
				foreach (OlapFilterDescriptionBase olapFilterDescriptionBase in hierarchyFilterDescription.Levels.OfType<OlapFilterDescriptionBase>())
				{
					IEnumerable<OlapExpression> expressions3 = olapFilterDescriptionBase.GetExpressions();
					foreach (OlapExpression item2 in expressions3)
					{
						expressions.Push(item2);
					}
				}
			}
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x001C8290 File Offset: 0x001C6490
		private void AddGroupFilterExpressions(Stack<OlapExpression> expressions)
		{
			IEnumerable<OlapExpression> groupFilterExpressions = this.GetGroupFilterExpressions(this.pivotConfiguration.PivotRowGroupDescriptions);
			IEnumerable<OlapExpression> groupFilterExpressions2 = this.GetGroupFilterExpressions(this.pivotConfiguration.PivotColumnGroupDescriptions);
			foreach (OlapExpression item in groupFilterExpressions)
			{
				expressions.Push(item);
			}
			foreach (OlapExpression item2 in groupFilterExpressions2)
			{
				expressions.Push(item2);
			}
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x001C8340 File Offset: 0x001C6540
		private IEnumerable<OlapExpression> GetGroupFilterExpressions(IEnumerable<OlapGroupDescription> descriptions)
		{
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (OlapGroupDescription olapGroupDescription in descriptions)
			{
				IHierarchyGroupDescription hierarchyGroupDescription = olapGroupDescription;
				if (hierarchyGroupDescription == null || hierarchyGroupDescription.IgnoreChildren)
				{
					IEnumerable<OlapExpression> groupFilterExpressionsForNoHierarchy = this.GetGroupFilterExpressionsForNoHierarchy(olapGroupDescription, descriptions);
					list.AddRange(groupFilterExpressionsForNoHierarchy);
				}
				else
				{
					IEnumerable<OlapExpression> groupFilterExpressionsForHierarchy = this.GetGroupFilterExpressionsForHierarchy(hierarchyGroupDescription, olapGroupDescription, descriptions);
					list.AddRange(groupFilterExpressionsForHierarchy);
				}
			}
			return list;
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x001C83C0 File Offset: 0x001C65C0
		private IEnumerable<OlapExpression> GetGroupFilterExpressionsForHierarchy(IHierarchyGroupDescription hierarchicalDescription, OlapGroupDescription description, IEnumerable<OlapGroupDescription> descriptions)
		{
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (OlapGroupDescriptionBase olapGroupDescriptionBase in hierarchicalDescription.Levels.OfType<OlapGroupDescriptionBase>())
			{
				OlapValueGroupFilter olapValueGroupFilter = olapGroupDescriptionBase.GroupFilter as OlapValueGroupFilter;
				if (olapValueGroupFilter != null)
				{
					OlapExpression hierarchicalGroupFilterSetExpression = MdxQueryBuilder.GetHierarchicalGroupFilterSetExpression(description, olapGroupDescriptionBase, descriptions);
					OlapAggregateDescription olapAggregateDescription = this.pivotConfiguration.PivotAggregateDescriptions[olapValueGroupFilter.AggregateIndex];
					OlapExpressionOptions options = new OlapExpressionOptions
					{
						HierarchyInfo = olapGroupDescriptionBase.FieldInfo,
						MemberInfo = olapAggregateDescription.FieldInfo
					};
					options.DimensionExpression = hierarchicalGroupFilterSetExpression;
					OlapExpression expression = olapValueGroupFilter.GetExpression(options);
					if (expression != null)
					{
						list.Add(expression);
					}
				}
				OlapLabelGroupFilter olapLabelGroupFilter = olapGroupDescriptionBase.GroupFilter as OlapLabelGroupFilter;
				if (olapLabelGroupFilter != null)
				{
					OlapExpressionOptions options2 = new OlapExpressionOptions
					{
						HierarchyInfo = description.FieldInfo,
						MemberInfo = olapGroupDescriptionBase.FieldInfo
					};
					options2.UseHierarchyAsAccess = true;
					IEnumerable<OlapExpression> expressions = olapLabelGroupFilter.GetExpressions(options2);
					if (expressions != null)
					{
						list.AddRange(expressions);
					}
				}
			}
			return list;
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x001C84E8 File Offset: 0x001C66E8
		private IEnumerable<OlapExpression> GetGroupFilterExpressionsForNoHierarchy(OlapGroupDescription description, IEnumerable<OlapGroupDescription> descriptions)
		{
			OlapValueGroupFilter olapValueGroupFilter = description.GroupFilter as OlapValueGroupFilter;
			IEnumerable<OlapExpression> result = new List<OlapExpression>();
			if (olapValueGroupFilter != null)
			{
				OlapExpression groupFilterSetExpression = MdxQueryBuilder.GetGroupFilterSetExpression(description, descriptions);
				OlapAggregateDescription olapAggregateDescription = this.pivotConfiguration.PivotAggregateDescriptions[olapValueGroupFilter.AggregateIndex];
				OlapExpressionOptions options = new OlapExpressionOptions
				{
					HierarchyInfo = description.FieldInfo,
					MemberInfo = olapAggregateDescription.FieldInfo
				};
				options.DimensionExpression = groupFilterSetExpression;
				OlapExpression expression = olapValueGroupFilter.GetExpression(options);
				if (expression != null)
				{
					result = new OlapExpression[]
					{
						expression
					};
				}
			}
			OlapLabelGroupFilter olapLabelGroupFilter = description.GroupFilter as OlapLabelGroupFilter;
			if (olapLabelGroupFilter != null)
			{
				OlapExpressionOptions options2 = new OlapExpressionOptions
				{
					HierarchyInfo = description.FieldInfo,
					MemberInfo = description.FieldInfo
				};
				result = olapLabelGroupFilter.GetExpressions(options2);
			}
			return result;
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x001C85B8 File Offset: 0x001C67B8
		private static OlapExpression GetFilterSetExpression(OlapGroupDescriptionBase description)
		{
			IHierarchyGroupDescription hierarchyGroupDescription = description as IHierarchyGroupDescription;
			if (hierarchyGroupDescription == null || hierarchyGroupDescription.IgnoreChildren)
			{
				return MdxQueryBuilder.GetExpressionForDimension(description, "Children");
			}
			return MdxQueryBuilder.GetExpressionForDimension(hierarchyGroupDescription.Levels.Last<IGroupDescription>() as OlapGroupDescriptionBase, "Children");
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x001C8600 File Offset: 0x001C6800
		private static OlapExpression GetGroupFilterSetExpression(OlapGroupDescription description, IEnumerable<OlapGroupDescription> allDescriptions)
		{
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (OlapGroupDescription olapGroupDescription in allDescriptions)
			{
				OlapExpression filterSetExpression = MdxQueryBuilder.GetFilterSetExpression(olapGroupDescription);
				if (filterSetExpression != null)
				{
					list.Add(filterSetExpression);
				}
				if (olapGroupDescription == description)
				{
					break;
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count > 1)
			{
				return new OlapFunctionExpression("CrossJoin", list);
			}
			return list[0];
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x001C8684 File Offset: 0x001C6884
		private static OlapExpression GetHierarchicalGroupFilterSetExpression(OlapGroupDescription description, OlapGroupDescriptionBase levelDescription, IEnumerable<OlapGroupDescription> allDescriptions)
		{
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (OlapGroupDescription olapGroupDescription in allDescriptions)
			{
				if (olapGroupDescription == description)
				{
					break;
				}
				OlapExpression filterSetExpression = MdxQueryBuilder.GetFilterSetExpression(olapGroupDescription);
				if (filterSetExpression != null)
				{
					list.Add(filterSetExpression);
				}
			}
			OlapExpression filterSetExpression2 = MdxQueryBuilder.GetFilterSetExpression(levelDescription);
			if (filterSetExpression2 != null)
			{
				list.Add(filterSetExpression2);
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count > 1)
			{
				return new OlapFunctionExpression("CrossJoin", list);
			}
			return list[0];
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x001C871C File Offset: 0x001C691C
		private static OlapExpression GetExpressionForDimension(OlapGroupDescriptionBase dimensionDescription)
		{
			return MdxQueryBuilder.GetExpressionForDimension(dimensionDescription, "AllMembers");
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x001C872C File Offset: 0x001C692C
		private static OlapExpression GetExpressionForDimension(OlapGroupDescriptionBase dimensionDescription, string membersFunctionName)
		{
			string memberWithBrackets = MemberNameHelper.GetMemberWithBrackets(dimensionDescription.MemberName);
			OlapExpression olapExpression = new OlapIdentifierExpression(memberWithBrackets, false);
			if (dimensionDescription.FieldInfo.SupportsMembersFunction)
			{
				olapExpression = new OlapMemberFuntionExpression(membersFunctionName, olapExpression);
			}
			return new OlapFunctionExpression("Hierarchize", new OlapExpression[]
			{
				olapExpression
			});
		}

		// Token: 0x04002203 RID: 8707
		private IOlapPivotConfiguration pivotConfiguration;

		// Token: 0x04002204 RID: 8708
		private string cubeName;
	}
}

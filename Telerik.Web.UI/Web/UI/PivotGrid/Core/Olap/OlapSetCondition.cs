using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x0200070F RID: 1807
	[DataContract]
	public class OlapSetCondition : OlapCondition, ISetCondition, ITransformingCondition
	{
		// Token: 0x0600402F RID: 16431 RVA: 0x000CA755 File Offset: 0x000C8955
		public OlapSetCondition()
		{
			this.condition = SetComparison.Includes;
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x000CA764 File Offset: 0x000C8964
		public override bool IsActive
		{
			get
			{
				return this.Items.Count > 0;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x000CA774 File Offset: 0x000C8974
		// (set) Token: 0x06004032 RID: 16434 RVA: 0x000CA77C File Offset: 0x000C897C
		[DataMember]
		public SetComparison Comparison
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					this.condition = value;
					base.OnPropertyChanged("Comparison");
				}
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x000CA799 File Offset: 0x000C8999
		[DataMember]
		public SetConditionHashCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new SetConditionHashCollection();
				}
				return this.items;
			}
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x000CA7B4 File Offset: 0x000C89B4
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapSetCondition();
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000CA7BC File Offset: 0x000C89BC
		protected override void CloneCore(Cloneable source)
		{
			OlapSetCondition olapSetCondition = source as OlapSetCondition;
			if (olapSetCondition != null)
			{
				this.Comparison = olapSetCondition.Comparison;
				if (olapSetCondition.items != null)
				{
					this.items = new SetConditionHashCollection(olapSetCondition.items);
				}
			}
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x000CA7F8 File Offset: 0x000C89F8
		internal override IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options)
		{
			if (options.HierarchyInfo == null)
			{
				return new List<OlapExpression>();
			}
			OlapExpression membersExpressionForHierarchy = OlapCondition.GetMembersExpressionForHierarchy(options.HierarchyInfo);
			if (options.MemberInfo != null)
			{
				membersExpressionForHierarchy = OlapCondition.GetMembersExpressionForHierarchy(options.MemberInfo);
			}
			List<OlapExpression> expressionsForSetItems = this.GetExpressionsForSetItems();
			OlapSetExpression olapSetExpression = new OlapSetExpression(expressionsForSetItems);
			if (this.Comparison == SetComparison.DoesNotInclude)
			{
				olapSetExpression = new OlapSetExpression(new OlapBinaryExpression[]
				{
					new OlapBinaryExpression(membersExpressionForHierarchy, olapSetExpression, OlapExpressionOperator.Except)
				});
			}
			return new OlapSetExpression[]
			{
				olapSetExpression
			};
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000CA878 File Offset: 0x000C8A78
		private List<OlapExpression> GetExpressionsForSetItems()
		{
			List<OlapExpression> list = new List<OlapExpression>();
			foreach (object obj in this.Items)
			{
				MemberDistinctValue memberDistinctValue = obj as MemberDistinctValue;
				if (memberDistinctValue != null)
				{
					list.Add(new OlapIdentifierExpression(memberDistinctValue.UniqueName, false));
				}
				else
				{
					list.Add(new OlapIdentifierExpression(obj.ToString(), false));
				}
			}
			return list;
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000CA900 File Offset: 0x000C8B00
		object ITransformingCondition.TransformConditionValueToDistinctItem(object item)
		{
			return OlapCondition.TransformConditionValueToDistinctItem(item);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000CA908 File Offset: 0x000C8B08
		object ITransformingCondition.TransformDistinctItemToConditionValue(object item)
		{
			return OlapCondition.TransformDistinctItemToConditionValue(item);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000CA910 File Offset: 0x000C8B10
		object ITransformingCondition.GetDistinctItemFromValue(object value, IEnumerable<object> distinctItems)
		{
			return OlapCondition.GetDistinctItemFromValue(value, distinctItems);
		}

		// Token: 0x04001107 RID: 4359
		private SetComparison condition;

		// Token: 0x04001108 RID: 4360
		private SetConditionHashCollection items;
	}
}

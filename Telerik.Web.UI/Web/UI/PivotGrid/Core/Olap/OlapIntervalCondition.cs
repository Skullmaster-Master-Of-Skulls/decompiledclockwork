using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x0200070D RID: 1805
	[DataContract]
	public class OlapIntervalCondition : OlapCondition, IIntervalCondition, ITransformingCondition
	{
		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x000CA327 File Offset: 0x000C8527
		public override bool IsActive
		{
			get
			{
				return this.From != null && this.To != null;
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x000CA33F File Offset: 0x000C853F
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x000CA347 File Offset: 0x000C8547
		[DataMember]
		public bool IgnoreCase { get; set; }

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000CA350 File Offset: 0x000C8550
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x000CA358 File Offset: 0x000C8558
		[DataMember]
		public object From
		{
			get
			{
				return this.from;
			}
			set
			{
				if (this.from != value)
				{
					this.from = value;
					base.OnPropertyChanged("From");
				}
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000CA375 File Offset: 0x000C8575
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x000CA37D File Offset: 0x000C857D
		[DataMember]
		public object To
		{
			get
			{
				return this.to;
			}
			set
			{
				if (this.to != value)
				{
					this.to = value;
					base.OnPropertyChanged("To");
				}
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x000CA39A File Offset: 0x000C859A
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x000CA3A2 File Offset: 0x000C85A2
		[DataMember]
		public IntervalComparison Condition
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
					base.OnPropertyChanged("Condition");
				}
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000CA3BF File Offset: 0x000C85BF
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000CA3C7 File Offset: 0x000C85C7
		object IIntervalCondition.From
		{
			get
			{
				return this.From;
			}
			set
			{
				this.From = value;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000CA3D0 File Offset: 0x000C85D0
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000CA3D8 File Offset: 0x000C85D8
		object IIntervalCondition.To
		{
			get
			{
				return this.To;
			}
			set
			{
				this.To = value;
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000CA3E1 File Offset: 0x000C85E1
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000CA3E9 File Offset: 0x000C85E9
		IntervalComparison IIntervalCondition.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = value;
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000CA3F2 File Offset: 0x000C85F2
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x000CA3FA File Offset: 0x000C85FA
		bool IIntervalCondition.IgnoreCase
		{
			get
			{
				return this.IgnoreCase;
			}
			set
			{
				this.IgnoreCase = value;
			}
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000CA403 File Offset: 0x000C8603
		internal override IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options)
		{
			if (options.HierarchyInfo == null)
			{
				return new List<OlapExpression>();
			}
			if (options.MemberInfo != null && options.MemberInfo.IsMeasure)
			{
				return this.GetExpressionForMeasure(options);
			}
			return this.GetExpressionForHierarchyOrLevel();
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x000CA43C File Offset: 0x000C863C
		private IEnumerable<OlapExpression> GetExpressionForHierarchyOrLevel()
		{
			OlapConstantExpression left = new OlapConstantExpression(OlapCondition.GetEffectiveValue(this.From));
			OlapConstantExpression right = new OlapConstantExpression(OlapCondition.GetEffectiveValue(this.To));
			if (this.Condition == IntervalComparison.IsBetween)
			{
				OlapBinaryExpression olapBinaryExpression = new OlapBinaryExpression(left, right, OlapExpressionOperator.Range);
				return new OlapBinaryExpression[]
				{
					olapBinaryExpression
				};
			}
			return new List<OlapExpression>();
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000CA490 File Offset: 0x000C8690
		private IEnumerable<OlapExpression> GetExpressionForMeasure(OlapExpressionOptions options)
		{
			OlapExpression left = new OlapIdentifierExpression(options.HierarchyInfo.Name, false);
			if (options.MemberInfo != null)
			{
				left = new OlapIdentifierExpression(options.MemberInfo.Name, false);
			}
			OlapExpression membersExpressionForHierarchy = OlapCondition.GetMembersExpressionForHierarchy(options.HierarchyInfo);
			OlapConstantExpression right = new OlapConstantExpression(OlapCondition.GetEffectiveValue(this.From));
			OlapConstantExpression right2 = new OlapConstantExpression(OlapCondition.GetEffectiveValue(this.To));
			if (this.Condition == IntervalComparison.IsBetween)
			{
				OlapBinaryExpression left2 = new OlapBinaryExpression(left, right, OlapExpressionOperator.IsGreaterThanOrEqualTo);
				OlapBinaryExpression right3 = new OlapBinaryExpression(left, right2, OlapExpressionOperator.IsLessThanOrEqualTo);
				OlapExpression olapExpression = new OlapBinaryExpression(left2, right3, OlapExpressionOperator.And);
				olapExpression = new OlapFunctionExpression("Filter", new OlapExpression[]
				{
					membersExpressionForHierarchy,
					olapExpression
				});
				return new OlapExpression[]
				{
					olapExpression
				};
			}
			OlapBinaryExpression left3 = new OlapBinaryExpression(left, right, OlapExpressionOperator.IsLessThan);
			OlapBinaryExpression right4 = new OlapBinaryExpression(left, right2, OlapExpressionOperator.IsGreaterThan);
			OlapExpression olapExpression2 = new OlapBinaryExpression(left3, right4, OlapExpressionOperator.Or);
			olapExpression2 = new OlapFunctionExpression("Filter", new OlapExpression[]
			{
				membersExpressionForHierarchy,
				olapExpression2
			});
			return new OlapExpression[]
			{
				olapExpression2
			};
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000CA5A8 File Offset: 0x000C87A8
		protected override void CloneCore(Cloneable source)
		{
			OlapIntervalCondition olapIntervalCondition = source as OlapIntervalCondition;
			if (olapIntervalCondition != null)
			{
				this.From = olapIntervalCondition.From;
				this.To = olapIntervalCondition.To;
				this.Condition = olapIntervalCondition.Condition;
				this.IgnoreCase = olapIntervalCondition.IgnoreCase;
			}
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000CA5EF File Offset: 0x000C87EF
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapIntervalCondition();
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000CA5F6 File Offset: 0x000C87F6
		object ITransformingCondition.TransformConditionValueToDistinctItem(object item)
		{
			return OlapCondition.TransformConditionValueToDistinctItem(item);
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000CA5FE File Offset: 0x000C87FE
		object ITransformingCondition.TransformDistinctItemToConditionValue(object item)
		{
			return OlapCondition.TransformDistinctItemToConditionValue(item);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000CA606 File Offset: 0x000C8806
		object ITransformingCondition.GetDistinctItemFromValue(object value, IEnumerable<object> distinctItems)
		{
			return OlapCondition.GetDistinctItemFromValue(value, distinctItems);
		}

		// Token: 0x04001101 RID: 4353
		private object from;

		// Token: 0x04001102 RID: 4354
		private object to;

		// Token: 0x04001103 RID: 4355
		private IntervalComparison condition;
	}
}

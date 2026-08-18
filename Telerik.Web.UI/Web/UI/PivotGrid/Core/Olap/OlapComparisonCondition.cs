using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x0200070C RID: 1804
	[DataContract]
	public class OlapComparisonCondition : OlapCondition, IComparisonCondition, ITransformingCondition
	{
		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x000CA0D0 File Offset: 0x000C82D0
		public override bool IsActive
		{
			get
			{
				return this.Than != null;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x000CA0DE File Offset: 0x000C82DE
		// (set) Token: 0x06003FF4 RID: 16372 RVA: 0x000CA0E6 File Offset: 0x000C82E6
		[DataMember]
		public bool IgnoreCase { get; set; }

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x000CA0EF File Offset: 0x000C82EF
		// (set) Token: 0x06003FF6 RID: 16374 RVA: 0x000CA0F7 File Offset: 0x000C82F7
		[DataMember]
		public object Than
		{
			get
			{
				return this.than;
			}
			set
			{
				if (this.than != value)
				{
					this.than = value;
					base.OnPropertyChanged("Than");
				}
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000CA114 File Offset: 0x000C8314
		// (set) Token: 0x06003FF8 RID: 16376 RVA: 0x000CA11C File Offset: 0x000C831C
		[DataMember]
		public Comparison Condition
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

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x000CA139 File Offset: 0x000C8339
		// (set) Token: 0x06003FFA RID: 16378 RVA: 0x000CA141 File Offset: 0x000C8341
		object IComparisonCondition.Than
		{
			get
			{
				return this.Than;
			}
			set
			{
				this.Than = value;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x000CA14A File Offset: 0x000C834A
		// (set) Token: 0x06003FFC RID: 16380 RVA: 0x000CA152 File Offset: 0x000C8352
		Comparison IComparisonCondition.Condition
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

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x000CA15B File Offset: 0x000C835B
		// (set) Token: 0x06003FFE RID: 16382 RVA: 0x000CA163 File Offset: 0x000C8363
		bool IComparisonCondition.IgnoreCase
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

		// Token: 0x06003FFF RID: 16383 RVA: 0x000CA16C File Offset: 0x000C836C
		protected override void CloneCore(Cloneable source)
		{
			OlapComparisonCondition olapComparisonCondition = source as OlapComparisonCondition;
			if (olapComparisonCondition != null)
			{
				this.Than = olapComparisonCondition.Than;
				this.Condition = olapComparisonCondition.Condition;
				this.IgnoreCase = olapComparisonCondition.IgnoreCase;
			}
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000CA1A7 File Offset: 0x000C83A7
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapComparisonCondition();
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000CA1B0 File Offset: 0x000C83B0
		internal override IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options)
		{
			if (options.HierarchyInfo == null)
			{
				return new List<OlapExpression>();
			}
			OlapExpression memberAccess = OlapComparisonCondition.GetMemberAccess(options);
			OlapExpression dimensionExpression = OlapCondition.GetDimensionExpression(options);
			OlapExpression valueExpression = this.GetValueExpression();
			OlapExpressionOperator expressionOperator = OlapComparisonCondition.ToExpressionOperator(this.Condition);
			OlapExpression olapExpression = new OlapBinaryExpression(memberAccess, valueExpression, expressionOperator);
			olapExpression = new OlapFunctionExpression("Filter", new OlapExpression[]
			{
				dimensionExpression,
				olapExpression
			});
			return new OlapExpression[]
			{
				olapExpression
			};
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000CA228 File Offset: 0x000C8428
		private OlapExpression GetValueExpression()
		{
			object effectiveValue = OlapCondition.GetEffectiveValue(this.Than);
			return new OlapConstantExpression(effectiveValue);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x000CA24C File Offset: 0x000C844C
		private static OlapExpression GetMemberAccess(OlapExpressionOptions options)
		{
			OlapExpression olapExpression = new OlapIdentifierExpression(options.HierarchyInfo.Name, false);
			if (options.MemberInfo != null && !options.UseHierarchyAsAccess)
			{
				olapExpression = new OlapIdentifierExpression(options.MemberInfo.Name, false);
				if (!options.MemberInfo.IsMeasure)
				{
					olapExpression = new OlapMemberFuntionExpression("CurrentMember", olapExpression);
				}
			}
			else if (!options.HierarchyInfo.IsMeasure)
			{
				olapExpression = new OlapMemberFuntionExpression("CurrentMember", olapExpression);
			}
			return olapExpression;
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000CA2C8 File Offset: 0x000C84C8
		private static OlapExpressionOperator ToExpressionOperator(Comparison comparison)
		{
			switch (comparison)
			{
			case Comparison.Equals:
				return OlapExpressionOperator.Equals;
			case Comparison.DoesNotEqual:
				return OlapExpressionOperator.DoesNotEqual;
			case Comparison.IsGreaterThan:
				return OlapExpressionOperator.IsGreaterThan;
			case Comparison.IsGreaterThanOrEqualTo:
				return OlapExpressionOperator.IsGreaterThanOrEqualTo;
			case Comparison.IsLessThan:
				return OlapExpressionOperator.IsLessThan;
			case Comparison.IsLessThanOrEqualTo:
				return OlapExpressionOperator.IsLessThanOrEqualTo;
			default:
				return OlapExpressionOperator.Equals;
			}
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x000CA306 File Offset: 0x000C8506
		object ITransformingCondition.TransformConditionValueToDistinctItem(object item)
		{
			return OlapCondition.TransformConditionValueToDistinctItem(item);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000CA30E File Offset: 0x000C850E
		object ITransformingCondition.TransformDistinctItemToConditionValue(object item)
		{
			return OlapCondition.TransformDistinctItemToConditionValue(item);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000CA316 File Offset: 0x000C8516
		object ITransformingCondition.GetDistinctItemFromValue(object value, IEnumerable<object> distinctItems)
		{
			return OlapCondition.GetDistinctItemFromValue(value, distinctItems);
		}

		// Token: 0x040010FE RID: 4350
		private object than;

		// Token: 0x040010FF RID: 4351
		private Comparison condition;
	}
}

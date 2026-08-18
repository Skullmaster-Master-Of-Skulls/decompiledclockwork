using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000710 RID: 1808
	[DataContract]
	public class OlapTextCondition : OlapCondition, ITextCondition
	{
		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000CA921 File Offset: 0x000C8B21
		// (set) Token: 0x0600403D RID: 16445 RVA: 0x000CA924 File Offset: 0x000C8B24
		bool ITextCondition.IgnoreCase
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x000CA926 File Offset: 0x000C8B26
		public override bool IsActive
		{
			get
			{
				return this.pattern != null;
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x000CA934 File Offset: 0x000C8B34
		// (set) Token: 0x06004040 RID: 16448 RVA: 0x000CA93C File Offset: 0x000C8B3C
		[DataMember]
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (this.pattern != value)
				{
					this.pattern = value;
					base.OnPropertyChanged("Pattern");
				}
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000CA95E File Offset: 0x000C8B5E
		// (set) Token: 0x06004042 RID: 16450 RVA: 0x000CA966 File Offset: 0x000C8B66
		[DataMember]
		public TextComparison Comparison
		{
			get
			{
				return this.comparison;
			}
			set
			{
				if (this.comparison != value)
				{
					this.comparison = value;
					base.OnPropertyChanged("Comparison");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x000CA990 File Offset: 0x000C8B90
		protected override void CloneCore(Cloneable source)
		{
			OlapTextCondition olapTextCondition = source as OlapTextCondition;
			if (olapTextCondition != null)
			{
				this.Pattern = olapTextCondition.Pattern;
				this.Comparison = olapTextCondition.Comparison;
			}
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x000CA9BF File Offset: 0x000C8BBF
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapTextCondition();
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x000CA9C8 File Offset: 0x000C8BC8
		internal override IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options)
		{
			if (options.HierarchyInfo == null)
			{
				return new List<OlapExpression>();
			}
			OlapExpression olapExpression = new OlapIdentifierExpression(options.HierarchyInfo.Name, false);
			if (options.MemberInfo != null && !options.UseHierarchyAsAccess)
			{
				olapExpression = new OlapIdentifierExpression(options.MemberInfo.Name, false);
			}
			OlapExpression dimensionExpression = OlapCondition.GetDimensionExpression(options);
			olapExpression = new OlapMemberFuntionExpression("CurrentMember", olapExpression);
			olapExpression = new OlapMemberFuntionExpression("MEMBER_CAPTION", olapExpression);
			OlapExpression olapExpression2 = new OlapConstantExpression(this.Pattern);
			olapExpression2 = new OlapWrapperExpression(new OlapExpression[]
			{
				olapExpression2
			}, OlapWrapperExpressionType.Quotes);
			OlapFunctionExpression stringFunction = new OlapFunctionExpression("InStr", new OlapExpression[]
			{
				olapExpression,
				olapExpression2
			});
			OlapExpression olapExpression3 = this.GetExpressionForTextComparison(stringFunction);
			olapExpression3 = new OlapFunctionExpression("Filter", new OlapExpression[]
			{
				dimensionExpression,
				olapExpression3
			});
			return new OlapExpression[]
			{
				olapExpression3
			};
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x000CAAB8 File Offset: 0x000C8CB8
		private OlapExpression GetExpressionForTextComparison(OlapExpression stringFunction)
		{
			OlapConstantExpression right = new OlapConstantExpression(0);
			if (this.Comparison == TextComparison.DoesNotContain)
			{
				return new OlapBinaryExpression(stringFunction, right, OlapExpressionOperator.Equals);
			}
			return new OlapBinaryExpression(stringFunction, right, OlapExpressionOperator.IsGreaterThan);
		}

		// Token: 0x04001109 RID: 4361
		private string pattern;

		// Token: 0x0400110A RID: 4362
		private TextComparison comparison;
	}
}

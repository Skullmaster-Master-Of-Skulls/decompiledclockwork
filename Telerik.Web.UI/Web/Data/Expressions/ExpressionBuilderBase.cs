using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BAA RID: 7082
	internal abstract class ExpressionBuilderBase
	{
		// Token: 0x06011212 RID: 70162 RVA: 0x003C7392 File Offset: 0x003C5592
		protected ExpressionBuilderBase(Type itemType)
		{
			this.itemType = itemType;
			this.options = new ExpressionBuilderOptions();
		}

		// Token: 0x17005396 RID: 21398
		// (get) Token: 0x06011213 RID: 70163 RVA: 0x003C73AC File Offset: 0x003C55AC
		public ExpressionBuilderOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x17005397 RID: 21399
		// (get) Token: 0x06011214 RID: 70164 RVA: 0x003C73B4 File Offset: 0x003C55B4
		protected internal Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x17005398 RID: 21400
		// (get) Token: 0x06011215 RID: 70165 RVA: 0x003C73BC File Offset: 0x003C55BC
		// (set) Token: 0x06011216 RID: 70166 RVA: 0x003C73E2 File Offset: 0x003C55E2
		protected internal ParameterExpression ParameterExpression
		{
			get
			{
				if (this.parameterExpression == null)
				{
					this.parameterExpression = Expression.Parameter(this.ItemType, "item");
				}
				return this.parameterExpression;
			}
			set
			{
				this.parameterExpression = value;
			}
		}

		// Token: 0x04004CB1 RID: 19633
		private readonly ExpressionBuilderOptions options;

		// Token: 0x04004CB2 RID: 19634
		private readonly Type itemType;

		// Token: 0x04004CB3 RID: 19635
		private ParameterExpression parameterExpression;
	}
}

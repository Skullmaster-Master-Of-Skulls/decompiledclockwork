using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200072B RID: 1835
	[DataContract]
	public abstract class QueryableAggregateDescription : QueryableAggregateDescriptionBase
	{
		// Token: 0x0600411D RID: 16669 RVA: 0x000CCC61 File Offset: 0x000CAE61
		internal QueryableAggregateDescription()
		{
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x000CCC69 File Offset: 0x000CAE69
		// (set) Token: 0x0600411F RID: 16671 RVA: 0x000CCC8A File Offset: 0x000CAE8A
		[DataMember]
		public virtual string FunctionName
		{
			get
			{
				if (string.IsNullOrEmpty(this.functionName))
				{
					this.functionName = this.GenerateFunctionName();
				}
				return this.functionName;
			}
			set
			{
				this.functionName = value;
				base.OnPropertyChanged("FunctionName");
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x000CCC9E File Offset: 0x000CAE9E
		protected virtual Type ExtensionMethodsType
		{
			get
			{
				return typeof(Enumerable);
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06004121 RID: 16673
		protected abstract string AggregateMethodName { get; }

		// Token: 0x06004122 RID: 16674
		protected internal abstract Expression CreateAggregateExpression(Expression enumerableExpression, string aggregatedValueName);

		// Token: 0x06004123 RID: 16675
		protected internal abstract Expression CreateAggregateValueExpression(ParameterExpression itemExpression);

		// Token: 0x06004124 RID: 16676
		protected abstract string GenerateFunctionName();

		// Token: 0x04001145 RID: 4421
		private string functionName;
	}
}

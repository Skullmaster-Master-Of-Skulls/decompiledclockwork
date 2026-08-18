using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B9E RID: 7070
	public abstract class AggregateFunction
	{
		// Token: 0x17005387 RID: 21383
		// (get) Token: 0x060111C4 RID: 70084 RVA: 0x003C6454 File Offset: 0x003C4654
		// (set) Token: 0x060111C5 RID: 70085 RVA: 0x003C645C File Offset: 0x003C465C
		public string Caption { get; set; }

		// Token: 0x17005388 RID: 21384
		// (get) Token: 0x060111C6 RID: 70086 RVA: 0x003C6465 File Offset: 0x003C4665
		// (set) Token: 0x060111C7 RID: 70087 RVA: 0x003C6486 File Offset: 0x003C4686
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
			}
		}

		// Token: 0x17005389 RID: 21385
		// (get) Token: 0x060111C8 RID: 70088 RVA: 0x003C648F File Offset: 0x003C468F
		// (set) Token: 0x060111C9 RID: 70089 RVA: 0x003C6497 File Offset: 0x003C4697
		public virtual string ResultFormatString { get; set; }

		// Token: 0x060111CA RID: 70090
		public abstract Expression CreateAggregateExpression(Expression enumerableExpression);

		// Token: 0x060111CB RID: 70091 RVA: 0x003C64A0 File Offset: 0x003C46A0
		protected virtual string GenerateFunctionName()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
			{
				base.GetType().Name,
				this.GetHashCode()
			});
		}

		// Token: 0x04004CA2 RID: 19618
		private string functionName;
	}
}

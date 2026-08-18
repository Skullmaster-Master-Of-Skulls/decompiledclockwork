using System;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200006C RID: 108
	public class SourceInjectedQueryInspector
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00009568 File Offset: 0x00007768
		public SourceInjectedQueryInspector()
		{
			this.SourceResult = delegate(Expression e, object o)
			{
			};
			this.DestResult = delegate(object o)
			{
			};
			this.StartQueryExecuteInterceptor = delegate(Type t, Expression e)
			{
			};
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000095EA File Offset: 0x000077EA
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000095F2 File Offset: 0x000077F2
		public Action<Expression, object> SourceResult { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000095FB File Offset: 0x000077FB
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00009603 File Offset: 0x00007803
		public Action<object> DestResult { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000960C File Offset: 0x0000780C
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00009614 File Offset: 0x00007814
		public Action<Type, Expression> StartQueryExecuteInterceptor { get; set; }
	}
}

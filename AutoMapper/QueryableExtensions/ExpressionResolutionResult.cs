using System;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x02000057 RID: 87
	public class ExpressionResolutionResult
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000085F5 File Offset: 0x000067F5
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000085FD File Offset: 0x000067FD
		public Expression ResolutionExpression { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00008606 File Offset: 0x00006806
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000860E File Offset: 0x0000680E
		public Type Type { get; private set; }

		// Token: 0x0600034C RID: 844 RVA: 0x00008617 File Offset: 0x00006817
		public ExpressionResolutionResult(Expression resolutionExpression, Type type)
		{
			this.ResolutionExpression = resolutionExpression;
			this.Type = type;
		}
	}
}

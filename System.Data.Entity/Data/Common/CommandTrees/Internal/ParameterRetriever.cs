using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x0200043A RID: 1082
	internal sealed class ParameterRetriever : BasicCommandTreeVisitor
	{
		// Token: 0x06003A1F RID: 14879 RVA: 0x000DDD11 File Offset: 0x000DBF11
		private ParameterRetriever()
		{
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000DDD24 File Offset: 0x000DBF24
		internal static ReadOnlyCollection<DbParameterReferenceExpression> GetParameters(DbCommandTree tree)
		{
			ParameterRetriever parameterRetriever = new ParameterRetriever();
			parameterRetriever.VisitCommandTree(tree);
			return parameterRetriever.paramMappings.Values.ToList<DbParameterReferenceExpression>().AsReadOnly();
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000DDD53 File Offset: 0x000DBF53
		public override void Visit(DbParameterReferenceExpression expression)
		{
			this.paramMappings[expression.ParameterName] = expression;
		}

		// Token: 0x04001871 RID: 6257
		private readonly Dictionary<string, DbParameterReferenceExpression> paramMappings = new Dictionary<string, DbParameterReferenceExpression>();
	}
}

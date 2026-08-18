using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x0200012D RID: 301
	internal sealed class ParameterRetriever : BasicCommandTreeVisitor
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x00033FDF File Offset: 0x000321DF
		private ParameterRetriever()
		{
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00033FF4 File Offset: 0x000321F4
		internal static ReadOnlyCollection<DbParameterReferenceExpression> GetParameters(DbCommandTree tree)
		{
			ParameterRetriever parameterRetriever = new ParameterRetriever();
			parameterRetriever.VisitCommandTree(tree);
			return new ReadOnlyCollection<DbParameterReferenceExpression>(parameterRetriever.paramMappings.Values.ToList<DbParameterReferenceExpression>());
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00034023 File Offset: 0x00032223
		public override void Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			this.paramMappings[expression.ParameterName] = expression;
		}

		// Token: 0x040002A4 RID: 676
		private readonly Dictionary<string, DbParameterReferenceExpression> paramMappings = new Dictionary<string, DbParameterReferenceExpression>();
	}
}

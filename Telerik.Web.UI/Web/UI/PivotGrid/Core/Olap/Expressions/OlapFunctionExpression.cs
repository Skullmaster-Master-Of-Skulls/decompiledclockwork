using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000701 RID: 1793
	internal class OlapFunctionExpression : OlapExpression
	{
		// Token: 0x06003FB5 RID: 16309 RVA: 0x000C9C1B File Offset: 0x000C7E1B
		internal OlapFunctionExpression(string name, IEnumerable<OlapExpression> arguments)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			this.Name = name;
			this.arguments = arguments.ToList<OlapExpression>();
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000C9C57 File Offset: 0x000C7E57
		public IEnumerable<OlapExpression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x000C9C5F File Offset: 0x000C7E5F
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.Function;
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x000C9C62 File Offset: 0x000C7E62
		// (set) Token: 0x06003FB9 RID: 16313 RVA: 0x000C9C6A File Offset: 0x000C7E6A
		public string Name { get; private set; }

		// Token: 0x06003FBA RID: 16314 RVA: 0x000C9C73 File Offset: 0x000C7E73
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitFunction(this);
		}

		// Token: 0x040010EA RID: 4330
		private readonly IEnumerable<OlapExpression> arguments;
	}
}

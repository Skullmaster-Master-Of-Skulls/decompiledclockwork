using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000519 RID: 1305
	internal abstract class XPathLiteralExpr : XPathExpr
	{
		// Token: 0x06003180 RID: 12672 RVA: 0x000BE371 File Offset: 0x000BC571
		internal XPathLiteralExpr(XPathExprType type, ValueDataType returnType) : base(type, returnType)
		{
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000BE37B File Offset: 0x000BC57B
		internal override bool IsLiteral
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003182 RID: 12674
		internal abstract object Literal { get; }
	}
}

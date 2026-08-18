using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000510 RID: 1296
	internal class XPathExpr
	{
		// Token: 0x06003159 RID: 12633 RVA: 0x000BE097 File Offset: 0x000BC297
		internal XPathExpr(XPathExprType type, ValueDataType returnType, XPathExprList subExpr) : this(type, returnType)
		{
			this.subExpr = subExpr;
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000BE0A8 File Offset: 0x000BC2A8
		internal XPathExpr(XPathExprType type, ValueDataType returnType)
		{
			this.type = type;
			this.returnType = returnType;
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000BE0BE File Offset: 0x000BC2BE
		internal virtual bool IsLiteral
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000BE0C1 File Offset: 0x000BC2C1
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x000BE0C9 File Offset: 0x000BC2C9
		internal bool Negate
		{
			get
			{
				return this.negate;
			}
			set
			{
				this.negate = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000BE0D2 File Offset: 0x000BC2D2
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x000BE0DA File Offset: 0x000BC2DA
		internal ValueDataType ReturnType
		{
			get
			{
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000BE0E3 File Offset: 0x000BC2E3
		internal int SubExprCount
		{
			get
			{
				if (this.subExpr != null)
				{
					return this.subExpr.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000BE0FA File Offset: 0x000BC2FA
		internal XPathExprList SubExpr
		{
			get
			{
				if (this.subExpr == null)
				{
					this.subExpr = new XPathExprList();
				}
				return this.subExpr;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000BE115 File Offset: 0x000BC315
		internal XPathExprType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000BE11D File Offset: 0x000BC31D
		// (set) Token: 0x06003164 RID: 12644 RVA: 0x000BE125 File Offset: 0x000BC325
		internal bool TypecastRequired
		{
			get
			{
				return this.castRequired;
			}
			set
			{
				this.castRequired = value;
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000BE12E File Offset: 0x000BC32E
		internal void Add(XPathExpr expr)
		{
			this.SubExpr.Add(expr);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000BE13C File Offset: 0x000BC33C
		internal void AddBooleanExpression(XPathExprType boolExprType, XPathExpr expr)
		{
			if (boolExprType == expr.Type)
			{
				XPathExprList xpathExprList = expr.SubExpr;
				for (int i = 0; i < xpathExprList.Count; i++)
				{
					this.AddBooleanExpression(boolExprType, xpathExprList[i]);
				}
				return;
			}
			this.Add(expr);
		}

		// Token: 0x0400265A RID: 9818
		private ValueDataType returnType;

		// Token: 0x0400265B RID: 9819
		private XPathExprList subExpr;

		// Token: 0x0400265C RID: 9820
		private XPathExprType type;

		// Token: 0x0400265D RID: 9821
		private bool negate;

		// Token: 0x0400265E RID: 9822
		private bool castRequired;
	}
}

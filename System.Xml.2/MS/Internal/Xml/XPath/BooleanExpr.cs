using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000B RID: 11
	internal sealed class BooleanExpr : ValueQuery
	{
		// Token: 0x06000033 RID: 51 RVA: 0x0000261C File Offset: 0x0000081C
		public BooleanExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Boolean)
			{
				opnd1 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Boolean)
			{
				opnd2 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd2);
			}
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
			this.isOr = (op == Operator.Op.OR);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000266B File Offset: 0x0000086B
		private BooleanExpr(BooleanExpr other) : base(other)
		{
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
			this.isOr = other.isOr;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026A2 File Offset: 0x000008A2
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026BC File Offset: 0x000008BC
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			object obj = this.opnd1.Evaluate(nodeIterator);
			if ((bool)obj == this.isOr)
			{
				return obj;
			}
			return this.opnd2.Evaluate(nodeIterator);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026F2 File Offset: 0x000008F2
		public override XPathNodeIterator Clone()
		{
			return new BooleanExpr(this);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000026FA File Offset: 0x000008FA
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002700 File Offset: 0x00000900
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", (this.isOr ? Operator.Op.OR : Operator.Op.AND).ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000064 RID: 100
		private Query opnd1;

		// Token: 0x04000065 RID: 101
		private Query opnd2;

		// Token: 0x04000066 RID: 102
		private bool isOr;
	}
}

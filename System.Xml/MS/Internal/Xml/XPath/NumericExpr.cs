using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200014F RID: 335
	internal sealed class NumericExpr : ValueQuery
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x000510FC File Offset: 0x000500FC
		public NumericExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Number)
			{
				opnd1 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Number)
			{
				opnd2 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd2);
			}
			this.op = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00051148 File Offset: 0x00050148
		private NumericExpr(NumericExpr other) : base(other)
		{
			this.op = other.op;
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0005117F File Offset: 0x0005017F
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00051199 File Offset: 0x00050199
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return NumericExpr.GetValue(this.op, XmlConvert.ToXPathDouble(this.opnd1.Evaluate(nodeIterator)), XmlConvert.ToXPathDouble(this.opnd2.Evaluate(nodeIterator)));
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000511D0 File Offset: 0x000501D0
		private static double GetValue(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.PLUS:
				return n1 + n2;
			case Operator.Op.MINUS:
				return n1 - n2;
			case Operator.Op.MUL:
				return n1 * n2;
			case Operator.Op.MOD:
				return n1 % n2;
			case Operator.Op.DIV:
				return n1 / n2;
			default:
				return 0.0;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0005121A File Offset: 0x0005021A
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0005121D File Offset: 0x0005021D
		public override XPathNodeIterator Clone()
		{
			return new NumericExpr(this);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00051228 File Offset: 0x00050228
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", this.op.ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000BA2 RID: 2978
		private Operator.Op op;

		// Token: 0x04000BA3 RID: 2979
		private Query opnd1;

		// Token: 0x04000BA4 RID: 2980
		private Query opnd2;
	}
}

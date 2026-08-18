using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200012A RID: 298
	internal sealed class BooleanExpr : ValueQuery
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x0004DD98 File Offset: 0x0004CD98
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

		// Token: 0x06001185 RID: 4485 RVA: 0x0004DDE7 File Offset: 0x0004CDE7
		private BooleanExpr(BooleanExpr other) : base(other)
		{
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
			this.isOr = other.isOr;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0004DE1E File Offset: 0x0004CE1E
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0004DE38 File Offset: 0x0004CE38
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			object obj = this.opnd1.Evaluate(nodeIterator);
			if ((bool)obj == this.isOr)
			{
				return obj;
			}
			return this.opnd2.Evaluate(nodeIterator);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0004DE6E File Offset: 0x0004CE6E
		public override XPathNodeIterator Clone()
		{
			return new BooleanExpr(this);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004DE76 File Offset: 0x0004CE76
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004DE7C File Offset: 0x0004CE7C
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", (this.isOr ? Operator.Op.OR : Operator.Op.AND).ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000B3E RID: 2878
		private Query opnd1;

		// Token: 0x04000B3F RID: 2879
		private Query opnd2;

		// Token: 0x04000B40 RID: 2880
		private bool isOr;
	}
}

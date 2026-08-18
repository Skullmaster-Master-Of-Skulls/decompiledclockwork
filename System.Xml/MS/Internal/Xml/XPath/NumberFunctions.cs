using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200014E RID: 334
	internal sealed class NumberFunctions : ValueQuery
	{
		// Token: 0x06001293 RID: 4755 RVA: 0x00050E6B File Offset: 0x0004FE6B
		public NumberFunctions(Function.FunctionType ftype, Query arg)
		{
			this.arg = arg;
			this.ftype = ftype;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00050E81 File Offset: 0x0004FE81
		private NumberFunctions(NumberFunctions other) : base(other)
		{
			this.arg = Query.Clone(other.arg);
			this.ftype = other.ftype;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00050EA7 File Offset: 0x0004FEA7
		public override void SetXsltContext(XsltContext context)
		{
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00050EBD File Offset: 0x0004FEBD
		internal static double Number(bool arg)
		{
			if (!arg)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00050ED5 File Offset: 0x0004FED5
		internal static double Number(string arg)
		{
			return XmlConvert.ToXPathDouble(arg);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00050EE0 File Offset: 0x0004FEE0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType functionType = this.ftype;
			if (functionType == Function.FunctionType.FuncNumber)
			{
				return this.Number(nodeIterator);
			}
			switch (functionType)
			{
			case Function.FunctionType.FuncSum:
				return this.Sum(nodeIterator);
			case Function.FunctionType.FuncFloor:
				return this.Floor(nodeIterator);
			case Function.FunctionType.FuncCeiling:
				return this.Ceiling(nodeIterator);
			case Function.FunctionType.FuncRound:
				return this.Round(nodeIterator);
			default:
				return null;
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00050F58 File Offset: 0x0004FF58
		private double Number(XPathNodeIterator nodeIterator)
		{
			if (this.arg == null)
			{
				return XmlConvert.ToXPathDouble(nodeIterator.Current.Value);
			}
			object obj = this.arg.Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				return (double)obj;
			case XPathResultType.String:
				return NumberFunctions.Number((string)obj);
			case XPathResultType.Boolean:
				return NumberFunctions.Number((bool)obj);
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this.arg.Advance();
				if (xpathNavigator != null)
				{
					return NumberFunctions.Number(xpathNavigator.Value);
				}
				break;
			}
			case (XPathResultType)4:
				return NumberFunctions.Number(((XPathNavigator)obj).Value);
			}
			return double.NaN;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00051004 File Offset: 0x00050004
		private double Sum(XPathNodeIterator nodeIterator)
		{
			double num = 0.0;
			this.arg.Evaluate(nodeIterator);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.arg.Advance()) != null)
			{
				num += NumberFunctions.Number(xpathNavigator.Value);
			}
			return num;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00051048 File Offset: 0x00050048
		private double Floor(XPathNodeIterator nodeIterator)
		{
			return Math.Floor((double)this.arg.Evaluate(nodeIterator));
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00051060 File Offset: 0x00050060
		private double Ceiling(XPathNodeIterator nodeIterator)
		{
			return Math.Ceiling((double)this.arg.Evaluate(nodeIterator));
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00051078 File Offset: 0x00050078
		private double Round(XPathNodeIterator nodeIterator)
		{
			double value = XmlConvert.ToXPathDouble(this.arg.Evaluate(nodeIterator));
			return XmlConvert.XPathRound(value);
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0005109D File Offset: 0x0005009D
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000510A0 File Offset: 0x000500A0
		public override XPathNodeIterator Clone()
		{
			return new NumberFunctions(this);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000510A8 File Offset: 0x000500A8
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", this.ftype.ToString());
			if (this.arg != null)
			{
				this.arg.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000BA0 RID: 2976
		private Query arg;

		// Token: 0x04000BA1 RID: 2977
		private Function.FunctionType ftype;
	}
}

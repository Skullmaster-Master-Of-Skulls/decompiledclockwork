using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002C RID: 44
	internal sealed class NumberFunctions : ValueQuery
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00005814 File Offset: 0x00003A14
		public NumberFunctions(Function.FunctionType ftype, Query arg)
		{
			this.arg = arg;
			this.ftype = ftype;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000582A File Offset: 0x00003A2A
		private NumberFunctions(NumberFunctions other) : base(other)
		{
			this.arg = Query.Clone(other.arg);
			this.ftype = other.ftype;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005850 File Offset: 0x00003A50
		public override void SetXsltContext(XsltContext context)
		{
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005866 File Offset: 0x00003A66
		internal static double Number(bool arg)
		{
			if (!arg)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000587E File Offset: 0x00003A7E
		internal static double Number(string arg)
		{
			return XmlConvert.ToXPathDouble(arg);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005888 File Offset: 0x00003A88
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

		// Token: 0x06000151 RID: 337 RVA: 0x00005900 File Offset: 0x00003B00
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

		// Token: 0x06000152 RID: 338 RVA: 0x000059AC File Offset: 0x00003BAC
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

		// Token: 0x06000153 RID: 339 RVA: 0x000059F0 File Offset: 0x00003BF0
		private double Floor(XPathNodeIterator nodeIterator)
		{
			return Math.Floor((double)this.arg.Evaluate(nodeIterator));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005A08 File Offset: 0x00003C08
		private double Ceiling(XPathNodeIterator nodeIterator)
		{
			return Math.Ceiling((double)this.arg.Evaluate(nodeIterator));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005A20 File Offset: 0x00003C20
		private double Round(XPathNodeIterator nodeIterator)
		{
			double value = XmlConvert.ToXPathDouble(this.arg.Evaluate(nodeIterator));
			return XmlConvert.XPathRound(value);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005A45 File Offset: 0x00003C45
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005A48 File Offset: 0x00003C48
		public override XPathNodeIterator Clone()
		{
			return new NumberFunctions(this);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005A50 File Offset: 0x00003C50
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

		// Token: 0x040000A7 RID: 167
		private Query arg;

		// Token: 0x040000A8 RID: 168
		private Function.FunctionType ftype;
	}
}

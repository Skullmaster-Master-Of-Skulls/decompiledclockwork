using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200012B RID: 299
	internal sealed class BooleanFunctions : ValueQuery
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x0004DED9 File Offset: 0x0004CED9
		public BooleanFunctions(Function.FunctionType funcType, Query arg)
		{
			this.arg = arg;
			this.funcType = funcType;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004DEEF File Offset: 0x0004CEEF
		private BooleanFunctions(BooleanFunctions other) : base(other)
		{
			this.arg = Query.Clone(other.arg);
			this.funcType = other.funcType;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0004DF15 File Offset: 0x0004CF15
		public override void SetXsltContext(XsltContext context)
		{
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004DF2C File Offset: 0x0004CF2C
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType functionType = this.funcType;
			switch (functionType)
			{
			case Function.FunctionType.FuncBoolean:
				return this.toBoolean(nodeIterator);
			case Function.FunctionType.FuncNumber:
				break;
			case Function.FunctionType.FuncTrue:
				return true;
			case Function.FunctionType.FuncFalse:
				return false;
			case Function.FunctionType.FuncNot:
				return this.Not(nodeIterator);
			default:
				if (functionType == Function.FunctionType.FuncLang)
				{
					return this.Lang(nodeIterator);
				}
				break;
			}
			return false;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004DF9E File Offset: 0x0004CF9E
		internal static bool toBoolean(double number)
		{
			return number != 0.0 && !double.IsNaN(number);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004DFB7 File Offset: 0x0004CFB7
		internal static bool toBoolean(string str)
		{
			return str.Length > 0;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0004DFC4 File Offset: 0x0004CFC4
		internal bool toBoolean(XPathNodeIterator nodeIterator)
		{
			object obj = this.arg.Evaluate(nodeIterator);
			if (obj is XPathNodeIterator)
			{
				return this.arg.Advance() != null;
			}
			if (obj is string)
			{
				return BooleanFunctions.toBoolean((string)obj);
			}
			if (obj is double)
			{
				return BooleanFunctions.toBoolean((double)obj);
			}
			return !(obj is bool) || (bool)obj;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0004E030 File Offset: 0x0004D030
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0004E033 File Offset: 0x0004D033
		private bool Not(XPathNodeIterator nodeIterator)
		{
			return !(bool)this.arg.Evaluate(nodeIterator);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004E04C File Offset: 0x0004D04C
		private bool Lang(XPathNodeIterator nodeIterator)
		{
			string text = this.arg.Evaluate(nodeIterator).ToString();
			string xmlLang = nodeIterator.Current.XmlLang;
			return xmlLang.StartsWith(text, StringComparison.OrdinalIgnoreCase) && (xmlLang.Length == text.Length || xmlLang[text.Length] == '-');
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004E0A3 File Offset: 0x0004D0A3
		public override XPathNodeIterator Clone()
		{
			return new BooleanFunctions(this);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0004E0AC File Offset: 0x0004D0AC
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", this.funcType.ToString());
			if (this.arg != null)
			{
				this.arg.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000B41 RID: 2881
		private Query arg;

		// Token: 0x04000B42 RID: 2882
		private Function.FunctionType funcType;
	}
}

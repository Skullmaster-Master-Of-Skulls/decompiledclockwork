using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000C RID: 12
	internal sealed class BooleanFunctions : ValueQuery
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002761 File Offset: 0x00000961
		public BooleanFunctions(Function.FunctionType funcType, Query arg)
		{
			this.arg = arg;
			this.funcType = funcType;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002777 File Offset: 0x00000977
		private BooleanFunctions(BooleanFunctions other) : base(other)
		{
			this.arg = Query.Clone(other.arg);
			this.funcType = other.funcType;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000279D File Offset: 0x0000099D
		public override void SetXsltContext(XsltContext context)
		{
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027B4 File Offset: 0x000009B4
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

		// Token: 0x0600003E RID: 62 RVA: 0x00002826 File Offset: 0x00000A26
		internal static bool toBoolean(double number)
		{
			return number != 0.0 && !double.IsNaN(number);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000283F File Offset: 0x00000A3F
		internal static bool toBoolean(string str)
		{
			return str.Length > 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000284C File Offset: 0x00000A4C
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028B5 File Offset: 0x00000AB5
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028B8 File Offset: 0x00000AB8
		private bool Not(XPathNodeIterator nodeIterator)
		{
			return !(bool)this.arg.Evaluate(nodeIterator);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028D0 File Offset: 0x00000AD0
		private bool Lang(XPathNodeIterator nodeIterator)
		{
			string text = this.arg.Evaluate(nodeIterator).ToString();
			string xmlLang = nodeIterator.Current.XmlLang;
			return xmlLang.StartsWith(text, StringComparison.OrdinalIgnoreCase) && (xmlLang.Length == text.Length || xmlLang[text.Length] == '-');
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002927 File Offset: 0x00000B27
		public override XPathNodeIterator Clone()
		{
			return new BooleanFunctions(this);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002930 File Offset: 0x00000B30
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

		// Token: 0x04000067 RID: 103
		private Query arg;

		// Token: 0x04000068 RID: 104
		private Function.FunctionType funcType;
	}
}

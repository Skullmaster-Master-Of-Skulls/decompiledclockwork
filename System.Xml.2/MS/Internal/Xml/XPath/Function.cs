using System;
using System.Collections;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000021 RID: 33
	internal class Function : AstNode
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004462 File Offset: 0x00002662
		public Function(Function.FunctionType ftype, ArrayList argumentList)
		{
			this.functionType = ftype;
			this.argumentList = new ArrayList(argumentList);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000447D File Offset: 0x0000267D
		public Function(string prefix, string name, ArrayList argumentList)
		{
			this.functionType = Function.FunctionType.FuncUserDefined;
			this.prefix = prefix;
			this.name = name;
			this.argumentList = new ArrayList(argumentList);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000044A7 File Offset: 0x000026A7
		public Function(Function.FunctionType ftype)
		{
			this.functionType = ftype;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000044B6 File Offset: 0x000026B6
		public Function(Function.FunctionType ftype, AstNode arg)
		{
			this.functionType = ftype;
			this.argumentList = new ArrayList();
			this.argumentList.Add(arg);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000044DD File Offset: 0x000026DD
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Function;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000044E0 File Offset: 0x000026E0
		public override XPathResultType ReturnType
		{
			get
			{
				return Function.ReturnTypes[(int)this.functionType];
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000044EE File Offset: 0x000026EE
		public Function.FunctionType TypeOfFunction
		{
			get
			{
				return this.functionType;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000044F6 File Offset: 0x000026F6
		public ArrayList ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000044FE File Offset: 0x000026FE
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004506 File Offset: 0x00002706
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400008C RID: 140
		private Function.FunctionType functionType;

		// Token: 0x0400008D RID: 141
		private ArrayList argumentList;

		// Token: 0x0400008E RID: 142
		private string name;

		// Token: 0x0400008F RID: 143
		private string prefix;

		// Token: 0x04000090 RID: 144
		internal static XPathResultType[] ReturnTypes = new XPathResultType[]
		{
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.NodeSet,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Number,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Any
		};

		// Token: 0x020002FC RID: 764
		public enum FunctionType
		{
			// Token: 0x040013DF RID: 5087
			FuncLast,
			// Token: 0x040013E0 RID: 5088
			FuncPosition,
			// Token: 0x040013E1 RID: 5089
			FuncCount,
			// Token: 0x040013E2 RID: 5090
			FuncID,
			// Token: 0x040013E3 RID: 5091
			FuncLocalName,
			// Token: 0x040013E4 RID: 5092
			FuncNameSpaceUri,
			// Token: 0x040013E5 RID: 5093
			FuncName,
			// Token: 0x040013E6 RID: 5094
			FuncString,
			// Token: 0x040013E7 RID: 5095
			FuncBoolean,
			// Token: 0x040013E8 RID: 5096
			FuncNumber,
			// Token: 0x040013E9 RID: 5097
			FuncTrue,
			// Token: 0x040013EA RID: 5098
			FuncFalse,
			// Token: 0x040013EB RID: 5099
			FuncNot,
			// Token: 0x040013EC RID: 5100
			FuncConcat,
			// Token: 0x040013ED RID: 5101
			FuncStartsWith,
			// Token: 0x040013EE RID: 5102
			FuncContains,
			// Token: 0x040013EF RID: 5103
			FuncSubstringBefore,
			// Token: 0x040013F0 RID: 5104
			FuncSubstringAfter,
			// Token: 0x040013F1 RID: 5105
			FuncSubstring,
			// Token: 0x040013F2 RID: 5106
			FuncStringLength,
			// Token: 0x040013F3 RID: 5107
			FuncNormalize,
			// Token: 0x040013F4 RID: 5108
			FuncTranslate,
			// Token: 0x040013F5 RID: 5109
			FuncLang,
			// Token: 0x040013F6 RID: 5110
			FuncSum,
			// Token: 0x040013F7 RID: 5111
			FuncFloor,
			// Token: 0x040013F8 RID: 5112
			FuncCeiling,
			// Token: 0x040013F9 RID: 5113
			FuncRound,
			// Token: 0x040013FA RID: 5114
			FuncUserDefined
		}
	}
}

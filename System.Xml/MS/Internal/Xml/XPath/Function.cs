using System;
using System.Collections;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000141 RID: 321
	internal class Function : AstNode
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x0004FC42 File Offset: 0x0004EC42
		public Function(Function.FunctionType ftype, ArrayList argumentList)
		{
			this.functionType = ftype;
			this.argumentList = new ArrayList(argumentList);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004FC5D File Offset: 0x0004EC5D
		public Function(string prefix, string name, ArrayList argumentList)
		{
			this.functionType = Function.FunctionType.FuncUserDefined;
			this.prefix = prefix;
			this.name = name;
			this.argumentList = new ArrayList(argumentList);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004FC87 File Offset: 0x0004EC87
		public Function(Function.FunctionType ftype)
		{
			this.functionType = ftype;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0004FC96 File Offset: 0x0004EC96
		public Function(Function.FunctionType ftype, AstNode arg)
		{
			this.functionType = ftype;
			this.argumentList = new ArrayList();
			this.argumentList.Add(arg);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004FCBD File Offset: 0x0004ECBD
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Function;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004FCC0 File Offset: 0x0004ECC0
		public override XPathResultType ReturnType
		{
			get
			{
				return Function.ReturnTypes[(int)this.functionType];
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004FCCE File Offset: 0x0004ECCE
		public Function.FunctionType TypeOfFunction
		{
			get
			{
				return this.functionType;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004FCD6 File Offset: 0x0004ECD6
		public ArrayList ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004FCDE File Offset: 0x0004ECDE
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004FCE6 File Offset: 0x0004ECE6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000B66 RID: 2918
		private Function.FunctionType functionType;

		// Token: 0x04000B67 RID: 2919
		private ArrayList argumentList;

		// Token: 0x04000B68 RID: 2920
		private string name;

		// Token: 0x04000B69 RID: 2921
		private string prefix;

		// Token: 0x04000B6A RID: 2922
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

		// Token: 0x02000142 RID: 322
		public enum FunctionType
		{
			// Token: 0x04000B6C RID: 2924
			FuncLast,
			// Token: 0x04000B6D RID: 2925
			FuncPosition,
			// Token: 0x04000B6E RID: 2926
			FuncCount,
			// Token: 0x04000B6F RID: 2927
			FuncID,
			// Token: 0x04000B70 RID: 2928
			FuncLocalName,
			// Token: 0x04000B71 RID: 2929
			FuncNameSpaceUri,
			// Token: 0x04000B72 RID: 2930
			FuncName,
			// Token: 0x04000B73 RID: 2931
			FuncString,
			// Token: 0x04000B74 RID: 2932
			FuncBoolean,
			// Token: 0x04000B75 RID: 2933
			FuncNumber,
			// Token: 0x04000B76 RID: 2934
			FuncTrue,
			// Token: 0x04000B77 RID: 2935
			FuncFalse,
			// Token: 0x04000B78 RID: 2936
			FuncNot,
			// Token: 0x04000B79 RID: 2937
			FuncConcat,
			// Token: 0x04000B7A RID: 2938
			FuncStartsWith,
			// Token: 0x04000B7B RID: 2939
			FuncContains,
			// Token: 0x04000B7C RID: 2940
			FuncSubstringBefore,
			// Token: 0x04000B7D RID: 2941
			FuncSubstringAfter,
			// Token: 0x04000B7E RID: 2942
			FuncSubstring,
			// Token: 0x04000B7F RID: 2943
			FuncStringLength,
			// Token: 0x04000B80 RID: 2944
			FuncNormalize,
			// Token: 0x04000B81 RID: 2945
			FuncTranslate,
			// Token: 0x04000B82 RID: 2946
			FuncLang,
			// Token: 0x04000B83 RID: 2947
			FuncSum,
			// Token: 0x04000B84 RID: 2948
			FuncFloor,
			// Token: 0x04000B85 RID: 2949
			FuncCeiling,
			// Token: 0x04000B86 RID: 2950
			FuncRound,
			// Token: 0x04000B87 RID: 2951
			FuncUserDefined
		}
	}
}

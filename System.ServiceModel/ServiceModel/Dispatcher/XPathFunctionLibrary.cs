using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200049F RID: 1183
	internal class XPathFunctionLibrary : IFunctionLibrary
	{
		// Token: 0x06002D43 RID: 11587 RVA: 0x000B0942 File Offset: 0x000AEB42
		internal XPathFunctionLibrary()
		{
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000B094C File Offset: 0x000AEB4C
		public QueryFunction Bind(string functionName, string functionNamespace, XPathExprList args)
		{
			if (functionName == "concat" && args.Count > 4)
			{
				ConcatFunction concatFunction = new ConcatFunction(args.Count);
				if (concatFunction.Bind(functionName, args))
				{
					return concatFunction;
				}
			}
			else
			{
				for (int i = 0; i < XPathFunctionLibrary.functionTable.Length; i++)
				{
					if (XPathFunctionLibrary.functionTable[i].Bind(functionName, args))
					{
						return XPathFunctionLibrary.functionTable[i];
					}
				}
			}
			return null;
		}

		// Token: 0x040024C9 RID: 9417
		private static XPathFunction[] functionTable = new XPathFunction[]
		{
			new XPathFunction(XPathFunctionID.Boolean, "boolean", ValueDataType.Boolean, new ValueDataType[1]),
			new XPathFunction(XPathFunctionID.False, "false", ValueDataType.Boolean),
			new XPathFunction(XPathFunctionID.True, "true", ValueDataType.Boolean),
			new XPathFunction(XPathFunctionID.Not, "not", ValueDataType.Boolean, new ValueDataType[]
			{
				ValueDataType.Boolean
			}),
			new XPathFunction(XPathFunctionID.Lang, "lang", ValueDataType.Boolean, new ValueDataType[]
			{
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.Number, "number", ValueDataType.Double, new ValueDataType[1]),
			new XPathFunction(XPathFunctionID.NumberDefault, "number", ValueDataType.Double),
			new XPathFunction(XPathFunctionID.Sum, "sum", ValueDataType.Double, new ValueDataType[]
			{
				ValueDataType.Sequence
			}),
			new XPathFunction(XPathFunctionID.Floor, "floor", ValueDataType.Double, new ValueDataType[]
			{
				ValueDataType.Double
			}),
			new XPathFunction(XPathFunctionID.Ceiling, "ceiling", ValueDataType.Double, new ValueDataType[]
			{
				ValueDataType.Double
			}),
			new XPathFunction(XPathFunctionID.Round, "round", ValueDataType.Double, new ValueDataType[]
			{
				ValueDataType.Double
			}),
			new XPathFunction(XPathFunctionID.String, "string", ValueDataType.String, new ValueDataType[1]),
			new XPathFunction(XPathFunctionID.StringDefault, "string", ValueDataType.String, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.ConcatTwo, "concat", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.ConcatThree, "concat", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.ConcatFour, "concat", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String,
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.StartsWith, "starts-with", ValueDataType.Boolean, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.NormalizeSpace, "normalize-space", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.NormalizeSpaceDefault, "normalize-space", ValueDataType.String, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.Contains, "contains", ValueDataType.Boolean, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.SubstringBefore, "substring-before", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.SubstringAfter, "substring-after", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.Substring, "substring", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.Double
			}),
			new XPathFunction(XPathFunctionID.SubstringLimit, "substring", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.Double,
				ValueDataType.Double
			}),
			new XPathFunction(XPathFunctionID.StringLength, "string-length", ValueDataType.Double, new ValueDataType[]
			{
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.StringLengthDefault, "string-length", ValueDataType.Double, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.Translate, "translate", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.String,
				ValueDataType.String,
				ValueDataType.String
			}),
			new XPathFunction(XPathFunctionID.Last, "last", ValueDataType.Double, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.Position, "position", ValueDataType.Double, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.Count, "count", ValueDataType.Double, new ValueDataType[]
			{
				ValueDataType.Sequence
			}),
			new XPathFunction(XPathFunctionID.LocalName, "local-name", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.Sequence
			}),
			new XPathFunction(XPathFunctionID.LocalNameDefault, "local-name", ValueDataType.String, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.Name, "name", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.Sequence
			}),
			new XPathFunction(XPathFunctionID.NameDefault, "name", ValueDataType.String, QueryFunctionFlag.UsesContextNode),
			new XPathFunction(XPathFunctionID.NamespaceUri, "namespace-uri", ValueDataType.String, new ValueDataType[]
			{
				ValueDataType.Sequence
			}),
			new XPathFunction(XPathFunctionID.NamespaceUriDefault, "namespace-uri", ValueDataType.String, QueryFunctionFlag.UsesContextNode)
		};
	}
}

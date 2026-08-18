using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200053A RID: 1338
	internal class XPathParser
	{
		// Token: 0x0600327C RID: 12924 RVA: 0x000C28AC File Offset: 0x000C0AAC
		internal XPathParser(string xpath, XmlNamespaceManager namespaces, IFunctionLibrary[] functionLibraries)
		{
			this.functionLibraries = functionLibraries;
			this.namespaces = namespaces;
			this.lexer = new XPathLexer(xpath);
			this.context = (namespaces as XsltContext);
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000C28DA File Offset: 0x000C0ADA
		private XPathExpr EnsureReturnsNodeSet(XPathExpr expr)
		{
			if (expr.ReturnType != ValueDataType.Sequence)
			{
				this.ThrowError(QueryCompileError.InvalidFunction);
			}
			return expr;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000C28F0 File Offset: 0x000C0AF0
		private XPathToken NextToken()
		{
			if (this.readToken != null)
			{
				XPathToken result = this.readToken;
				this.readToken = null;
				return result;
			}
			while (this.lexer.MoveNext())
			{
				if (XPathTokenID.Whitespace != this.lexer.Token.TokenID)
				{
					return this.lexer.Token;
				}
			}
			return null;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000C2948 File Offset: 0x000C0B48
		private XPathToken NextToken(XPathTokenID id)
		{
			XPathToken xpathToken = this.NextToken();
			if (xpathToken != null)
			{
				if (id == xpathToken.TokenID)
				{
					return xpathToken;
				}
				this.readToken = xpathToken;
			}
			return null;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000C2974 File Offset: 0x000C0B74
		private XPathToken NextToken(XPathTokenID id, QueryCompileError error)
		{
			XPathToken xpathToken = this.NextToken(id);
			if (xpathToken == null)
			{
				this.ThrowError(error);
			}
			return xpathToken;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000C2994 File Offset: 0x000C0B94
		private XPathToken NextTokenClass(XPathTokenID tokenClass)
		{
			XPathToken xpathToken = this.NextToken();
			if (xpathToken != null)
			{
				if ((xpathToken.TokenID & tokenClass) != XPathTokenID.Unknown)
				{
					return xpathToken;
				}
				this.readToken = xpathToken;
			}
			return null;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000C29C0 File Offset: 0x000C0BC0
		private NodeQName QualifyName(string prefix, string name)
		{
			if (this.namespaces != null && prefix != null && prefix.Length > 0)
			{
				prefix = this.namespaces.NameTable.Add(prefix);
				string text = this.namespaces.LookupNamespace(prefix);
				if (text == null)
				{
					this.ThrowError(QueryCompileError.NoNamespaceForPrefix);
				}
				return new NodeQName(name, text);
			}
			return new NodeQName(name);
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000C2A1C File Offset: 0x000C0C1C
		internal XPathExpr Parse()
		{
			XPathExpr xpathExpr = this.ParseExpression();
			if (xpathExpr == null)
			{
				this.ThrowError(QueryCompileError.InvalidExpression);
			}
			XPathToken xpathToken = this.NextToken();
			if (xpathToken != null)
			{
				this.ThrowError(QueryCompileError.UnexpectedToken);
			}
			return xpathExpr;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000C2A50 File Offset: 0x000C0C50
		private XPathExprList ParseAbsolutePath()
		{
			XPathExprList xpathExprList = null;
			XPathToken xpathToken = this.NextToken();
			if (xpathToken != null)
			{
				XPathTokenID tokenID = xpathToken.TokenID;
				if (tokenID != XPathTokenID.Slash)
				{
					if (tokenID != XPathTokenID.DblSlash)
					{
						this.PushToken(xpathToken);
					}
					else
					{
						xpathExprList = new XPathExprList();
						xpathExprList.Add(new XPathStepExpr(new NodeSelectCriteria(QueryAxisType.Child, NodeQName.Empty, QueryNodeType.Root)));
						xpathExprList.Add(new XPathStepExpr(new NodeSelectCriteria(QueryAxisType.DescendantOrSelf, NodeQName.Empty, QueryNodeType.All)));
					}
				}
				else
				{
					xpathExprList = new XPathExprList();
					xpathExprList.Add(new XPathStepExpr(new NodeSelectCriteria(QueryAxisType.Child, NodeQName.Empty, QueryNodeType.Root)));
				}
			}
			if (xpathExprList != null)
			{
				this.ParseRelativePath(xpathExprList);
			}
			return xpathExprList;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000C2AEC File Offset: 0x000C0CEC
		private XPathExpr ParseAdditiveExpression()
		{
			XPathExpr xpathExpr = this.ParseMultiplicativeExpression();
			if (xpathExpr != null)
			{
				MathOperator mathOperator;
				do
				{
					mathOperator = MathOperator.None;
					XPathToken xpathToken = this.NextToken();
					if (xpathToken != null)
					{
						XPathTokenID tokenID = xpathToken.TokenID;
						if (tokenID != XPathTokenID.Plus)
						{
							if (tokenID != XPathTokenID.Minus)
							{
								this.PushToken(xpathToken);
							}
							else
							{
								mathOperator = MathOperator.Minus;
							}
						}
						else
						{
							mathOperator = MathOperator.Plus;
						}
						if (mathOperator != MathOperator.None)
						{
							XPathExpr xpathExpr2 = this.ParseMultiplicativeExpression();
							if (xpathExpr2 == null)
							{
								this.ThrowError(QueryCompileError.InvalidExpression);
							}
							xpathExpr = new XPathMathExpr(mathOperator, xpathExpr, xpathExpr2);
						}
					}
				}
				while (mathOperator != MathOperator.None);
			}
			return xpathExpr;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000C2B5C File Offset: 0x000C0D5C
		private XPathExpr ParseAndExpression()
		{
			XPathExpr xpathExpr = this.ParseEqualityExpression();
			if (xpathExpr != null && this.NextToken(XPathTokenID.And) != null)
			{
				XPathExpr xpathExpr2 = new XPathExpr(XPathExprType.And, ValueDataType.Boolean);
				xpathExpr2.AddBooleanExpression(XPathExprType.And, xpathExpr);
				do
				{
					xpathExpr = this.ParseEqualityExpression();
					if (xpathExpr == null)
					{
						this.ThrowError(QueryCompileError.InvalidExpression);
					}
					xpathExpr2.AddBooleanExpression(XPathExprType.And, xpathExpr);
				}
				while (this.NextToken(XPathTokenID.And) != null);
				return xpathExpr2;
			}
			return xpathExpr;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000C2BBC File Offset: 0x000C0DBC
		private QueryAxisType ParseAxisSpecifier()
		{
			if (this.NextToken(XPathTokenID.AtSign) != null)
			{
				return QueryAxisType.Attribute;
			}
			QueryAxisType result = QueryAxisType.None;
			XPathToken xpathToken;
			if ((xpathToken = this.NextTokenClass(XPathTokenID.Axis)) != null)
			{
				XPathTokenID tokenID = xpathToken.TokenID;
				switch (tokenID)
				{
				case XPathTokenID.Attribute:
					result = QueryAxisType.Attribute;
					break;
				case XPathTokenID.Child:
					result = QueryAxisType.Child;
					break;
				case XPathTokenID.Descendant:
					result = QueryAxisType.Descendant;
					break;
				case XPathTokenID.DescendantOrSelf:
					result = QueryAxisType.DescendantOrSelf;
					break;
				default:
					if (tokenID != XPathTokenID.Self)
					{
						this.ThrowError(QueryCompileError.UnsupportedAxis);
					}
					else
					{
						result = QueryAxisType.Self;
					}
					break;
				}
				this.NextToken(XPathTokenID.DblColon, QueryCompileError.InvalidAxisSpecifier);
			}
			return result;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000C2C40 File Offset: 0x000C0E40
		private XPathExpr ParseEqualityExpression()
		{
			XPathExpr xpathExpr = this.ParseRelationalExpression();
			if (xpathExpr != null)
			{
				RelationOperator relationOperator;
				do
				{
					relationOperator = RelationOperator.None;
					XPathToken xpathToken = this.NextToken();
					if (xpathToken != null)
					{
						XPathTokenID tokenID = xpathToken.TokenID;
						if (tokenID != XPathTokenID.Eq)
						{
							if (tokenID != XPathTokenID.Neq)
							{
								this.PushToken(xpathToken);
							}
							else
							{
								relationOperator = RelationOperator.Ne;
							}
						}
						else
						{
							relationOperator = RelationOperator.Eq;
						}
						if (relationOperator != RelationOperator.None)
						{
							XPathExpr xpathExpr2 = this.ParseRelationalExpression();
							if (xpathExpr2 == null)
							{
								this.ThrowError(QueryCompileError.InvalidExpression);
							}
							xpathExpr = new XPathRelationExpr(relationOperator, xpathExpr, xpathExpr2);
						}
					}
				}
				while (relationOperator != RelationOperator.None);
			}
			return xpathExpr;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000C2CAE File Offset: 0x000C0EAE
		private XPathExpr ParseExpression()
		{
			return this.ParseOrExpression();
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000C2CB8 File Offset: 0x000C0EB8
		private XPathExpr ParseFilterExpression()
		{
			XPathExpr xpathExpr = this.ParsePrimaryExpression();
			if (xpathExpr == null)
			{
				return null;
			}
			XPathExpr xpathExpr2 = new XPathExpr(XPathExprType.Filter, xpathExpr.ReturnType);
			xpathExpr2.Add(xpathExpr);
			XPathExpr xpathExpr3 = this.ParsePredicateExpression();
			if (xpathExpr3 != null)
			{
				this.EnsureReturnsNodeSet(xpathExpr);
				xpathExpr2.Add(xpathExpr3);
				while ((xpathExpr3 = this.ParsePredicateExpression()) != null)
				{
					xpathExpr2.Add(xpathExpr3);
				}
				return xpathExpr2;
			}
			return xpathExpr;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000C2D18 File Offset: 0x000C0F18
		private XPathExpr ParseFunctionExpression()
		{
			XPathToken xpathToken = this.NextToken(XPathTokenID.Function);
			if (xpathToken == null)
			{
				return null;
			}
			NodeQName nodeQName = this.QualifyName(xpathToken.Prefix, xpathToken.Name);
			this.NextToken(XPathTokenID.LParen, QueryCompileError.InvalidFunction);
			XPathExprList xpathExprList = new XPathExprList();
			XPathExpr expr;
			while ((expr = this.ParseExpression()) != null)
			{
				xpathExprList.Add(expr);
				if (this.NextToken(XPathTokenID.Comma) == null)
				{
					break;
				}
			}
			XPathExpr xpathExpr = null;
			if (this.functionLibraries != null)
			{
				for (int i = 0; i < this.functionLibraries.Length; i++)
				{
					QueryFunction function;
					if ((function = this.functionLibraries[i].Bind(nodeQName.Name, nodeQName.Namespace, xpathExprList)) != null)
					{
						xpathExpr = new XPathFunctionExpr(function, xpathExprList);
						break;
					}
				}
			}
			if (xpathExpr == null && this.context != null)
			{
				XPathResultType[] array = new XPathResultType[xpathExprList.Count];
				for (int j = 0; j < xpathExprList.Count; j++)
				{
					array[j] = XPathXsltFunctionExpr.ConvertTypeToXslt(xpathExprList[j].ReturnType);
				}
				string prefix = this.context.LookupPrefix(nodeQName.Namespace);
				IXsltContextFunction xsltContextFunction = this.context.ResolveFunction(prefix, nodeQName.Name, array);
				if (xsltContextFunction != null)
				{
					xpathExpr = new XPathXsltFunctionExpr(this.context, xsltContextFunction, xpathExprList);
				}
			}
			if (xpathExpr == null)
			{
				this.ThrowError(QueryCompileError.UnsupportedFunction);
			}
			this.NextToken(XPathTokenID.RParen, QueryCompileError.InvalidFunction);
			return xpathExpr;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000C2E74 File Offset: 0x000C1074
		internal XPathExpr ParseLocationPath()
		{
			XPathExprList xpathExprList = this.ParseAbsolutePath();
			if (xpathExprList == null)
			{
				xpathExprList = this.ParseRelativePath();
			}
			if (xpathExprList != null)
			{
				return new XPathExpr(XPathExprType.LocationPath, ValueDataType.Sequence, xpathExprList);
			}
			return null;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000C2EA0 File Offset: 0x000C10A0
		private XPathExpr ParseLiteralExpression()
		{
			XPathToken xpathToken;
			if ((xpathToken = this.NextToken(XPathTokenID.Literal)) != null)
			{
				return new XPathStringExpr(xpathToken.Name);
			}
			return null;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000C2ECC File Offset: 0x000C10CC
		private XPathExpr ParseMultiplicativeExpression()
		{
			XPathExpr xpathExpr = this.ParseUnaryExpression();
			if (xpathExpr != null)
			{
				MathOperator mathOperator;
				do
				{
					mathOperator = MathOperator.None;
					XPathToken xpathToken = this.NextToken();
					if (xpathToken != null)
					{
						XPathTokenID tokenID = xpathToken.TokenID;
						if (tokenID != XPathTokenID.Multiply)
						{
							if (tokenID != XPathTokenID.Mod)
							{
								if (tokenID != XPathTokenID.Div)
								{
									this.PushToken(xpathToken);
								}
								else
								{
									mathOperator = MathOperator.Div;
								}
							}
							else
							{
								mathOperator = MathOperator.Mod;
							}
						}
						else
						{
							mathOperator = MathOperator.Multiply;
						}
						if (mathOperator != MathOperator.None)
						{
							XPathExpr xpathExpr2 = this.ParseUnaryExpression();
							if (xpathExpr2 == null)
							{
								this.ThrowError(QueryCompileError.InvalidExpression);
							}
							xpathExpr = new XPathMathExpr(mathOperator, xpathExpr, xpathExpr2);
						}
					}
				}
				while (mathOperator != MathOperator.None);
			}
			return xpathExpr;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000C2F48 File Offset: 0x000C1148
		private NodeSelectCriteria ParseNodeTest(QueryAxisType axisType)
		{
			QueryAxis axis = QueryDataModel.GetAxis(axisType);
			NodeQName qname = NodeQName.Empty;
			XPathToken xpathToken;
			if ((xpathToken = this.NextTokenClass(XPathTokenID.NameTest)) != null)
			{
				XPathTokenID tokenID = xpathToken.TokenID;
				if (tokenID != XPathTokenID.NameTest)
				{
					if (tokenID != XPathTokenID.Wildcard)
					{
						if (tokenID != XPathTokenID.NameWildcard)
						{
							this.ThrowError(QueryCompileError.UnexpectedToken);
						}
						else
						{
							qname = this.QualifyName(xpathToken.Prefix, QueryDataModel.Wildcard);
						}
					}
					else
					{
						qname = new NodeQName(QueryDataModel.Wildcard, QueryDataModel.Wildcard);
					}
				}
				else
				{
					qname = this.QualifyName(xpathToken.Prefix, xpathToken.Name);
				}
			}
			QueryNodeType queryNodeType = QueryNodeType.Any;
			if (qname.IsEmpty)
			{
				if ((xpathToken = this.NextTokenClass(XPathTokenID.NodeType)) == null)
				{
					return null;
				}
				switch (xpathToken.TokenID)
				{
				case XPathTokenID.Comment:
					queryNodeType = QueryNodeType.Comment;
					break;
				case XPathTokenID.Text:
					queryNodeType = QueryNodeType.Text;
					break;
				case XPathTokenID.Processing:
					queryNodeType = QueryNodeType.Processing;
					break;
				case XPathTokenID.Node:
					queryNodeType = QueryNodeType.All;
					break;
				default:
					this.ThrowError(QueryCompileError.UnsupportedNodeTest);
					break;
				}
				if ((axis.ValidNodeTypes & queryNodeType) == QueryNodeType.Any)
				{
					this.ThrowError(QueryCompileError.InvalidNodeType);
				}
				this.NextToken(XPathTokenID.LParen, QueryCompileError.InvalidNodeTest);
				this.NextToken(XPathTokenID.RParen, QueryCompileError.InvalidNodeTest);
			}
			else
			{
				queryNodeType = axis.PrincipalNodeType;
			}
			return new NodeSelectCriteria(axisType, qname, queryNodeType);
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000C307C File Offset: 0x000C127C
		private XPathExpr ParseNumberExpression()
		{
			XPathToken xpathToken;
			if ((xpathToken = this.NextTokenClass(XPathTokenID.Number)) != null)
			{
				return new XPathNumberExpr(xpathToken.Number);
			}
			return null;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000C30A8 File Offset: 0x000C12A8
		private XPathExpr ParseOrExpression()
		{
			XPathExpr xpathExpr = this.ParseAndExpression();
			if (xpathExpr != null && this.NextToken(XPathTokenID.Or) != null)
			{
				XPathExpr xpathExpr2 = new XPathExpr(XPathExprType.Or, ValueDataType.Boolean);
				xpathExpr2.AddBooleanExpression(XPathExprType.Or, xpathExpr);
				do
				{
					xpathExpr = this.ParseAndExpression();
					if (xpathExpr == null)
					{
						this.ThrowError(QueryCompileError.InvalidExpression);
					}
					xpathExpr2.AddBooleanExpression(XPathExprType.Or, xpathExpr);
				}
				while (this.NextToken(XPathTokenID.Or) != null);
				return xpathExpr2;
			}
			return xpathExpr;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000C3108 File Offset: 0x000C1308
		private XPathExpr ParsePathExpression()
		{
			XPathExpr xpathExpr = this.ParseLocationPath();
			if (xpathExpr != null)
			{
				return xpathExpr;
			}
			XPathExpr xpathExpr2 = this.ParseFilterExpression();
			if (xpathExpr2 != null)
			{
				if (this.NextToken(XPathTokenID.Slash) != null)
				{
					this.EnsureReturnsNodeSet(xpathExpr2);
					XPathExprList xpathExprList = this.ParseRelativePath();
					if (xpathExprList == null)
					{
						this.ThrowError(QueryCompileError.InvalidLocationPath);
					}
					XPathExpr expr = new XPathExpr(XPathExprType.RelativePath, ValueDataType.Sequence, xpathExprList);
					xpathExpr = new XPathExpr(XPathExprType.Path, ValueDataType.Sequence);
					xpathExpr.Add(xpathExpr2);
					xpathExpr.Add(expr);
				}
				else if (this.NextToken(XPathTokenID.DblSlash) != null)
				{
					this.EnsureReturnsNodeSet(xpathExpr2);
					XPathExprList xpathExprList2 = this.ParseRelativePath();
					if (xpathExprList2 == null)
					{
						this.ThrowError(QueryCompileError.InvalidLocationPath);
					}
					XPathExpr expr2 = new XPathExpr(XPathExprType.RelativePath, ValueDataType.Sequence, xpathExprList2);
					xpathExpr = new XPathExpr(XPathExprType.Path, ValueDataType.Sequence);
					xpathExpr.Add(xpathExpr2);
					xpathExpr.Add(new XPathStepExpr(new NodeSelectCriteria(QueryAxisType.DescendantOrSelf, NodeQName.Empty, QueryNodeType.All)));
					xpathExpr.Add(expr2);
				}
				else
				{
					xpathExpr = xpathExpr2;
				}
			}
			return xpathExpr;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000C31E4 File Offset: 0x000C13E4
		private XPathExprList ParsePredicates()
		{
			XPathExprList xpathExprList = null;
			XPathExpr xpathExpr = this.ParsePredicateExpression();
			if (xpathExpr != null)
			{
				xpathExprList = new XPathExprList();
				xpathExprList.Add(xpathExpr);
				while ((xpathExpr = this.ParsePredicateExpression()) != null)
				{
					xpathExprList.Add(xpathExpr);
				}
			}
			return xpathExprList;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000C3220 File Offset: 0x000C1420
		private XPathExpr ParsePredicateExpression()
		{
			XPathExpr xpathExpr = null;
			if (this.NextToken(XPathTokenID.LBracket) != null)
			{
				xpathExpr = this.ParseExpression();
				if (xpathExpr == null)
				{
					this.ThrowError(QueryCompileError.InvalidPredicate);
				}
				this.NextToken(XPathTokenID.RBracket, QueryCompileError.InvalidPredicate);
			}
			return xpathExpr;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000C3260 File Offset: 0x000C1460
		private XPathExpr ParsePrimaryExpression()
		{
			XPathExpr xpathExpr = this.ParseVariableExpression();
			if (xpathExpr == null && this.NextToken(XPathTokenID.LParen) != null)
			{
				xpathExpr = this.ParseExpression();
				if (xpathExpr == null || this.NextToken(XPathTokenID.RParen) == null)
				{
					this.ThrowError(QueryCompileError.InvalidExpression);
				}
			}
			if (xpathExpr == null)
			{
				xpathExpr = this.ParseLiteralExpression();
			}
			if (xpathExpr == null)
			{
				xpathExpr = this.ParseNumberExpression();
			}
			if (xpathExpr == null)
			{
				xpathExpr = this.ParseFunctionExpression();
			}
			return xpathExpr;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000C32C4 File Offset: 0x000C14C4
		private XPathExprList ParseRelativePath()
		{
			XPathExprList xpathExprList = new XPathExprList();
			if (this.ParseRelativePath(xpathExprList))
			{
				return xpathExprList;
			}
			return null;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000C32E4 File Offset: 0x000C14E4
		private bool ParseRelativePath(XPathExprList path)
		{
			XPathStepExpr xpathStepExpr = this.ParseStep();
			if (xpathStepExpr == null)
			{
				return false;
			}
			path.Add(xpathStepExpr);
			for (;;)
			{
				if (this.NextToken(XPathTokenID.Slash) != null)
				{
					xpathStepExpr = this.ParseStep();
				}
				else
				{
					if (this.NextToken(XPathTokenID.DblSlash) == null)
					{
						break;
					}
					xpathStepExpr = new XPathStepExpr(new NodeSelectCriteria(QueryAxisType.DescendantOrSelf, NodeQName.Empty, QueryNodeType.All));
					path.Add(xpathStepExpr);
					xpathStepExpr = this.ParseStep();
				}
				if (xpathStepExpr == null)
				{
					this.ThrowError(QueryCompileError.InvalidLocationPath);
				}
				path.Add(xpathStepExpr);
			}
			return true;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000C3360 File Offset: 0x000C1560
		private XPathExpr ParseRelationalExpression()
		{
			XPathExpr xpathExpr = this.ParseAdditiveExpression();
			if (xpathExpr != null)
			{
				RelationOperator relationOperator;
				do
				{
					relationOperator = RelationOperator.None;
					XPathToken xpathToken = this.NextToken();
					if (xpathToken != null)
					{
						switch (xpathToken.TokenID)
						{
						case XPathTokenID.Gt:
							relationOperator = RelationOperator.Gt;
							break;
						case XPathTokenID.Gte:
							relationOperator = RelationOperator.Ge;
							break;
						case XPathTokenID.Lt:
							relationOperator = RelationOperator.Lt;
							break;
						case XPathTokenID.Lte:
							relationOperator = RelationOperator.Le;
							break;
						default:
							this.PushToken(xpathToken);
							break;
						}
						if (relationOperator != RelationOperator.None)
						{
							XPathExpr xpathExpr2 = this.ParseAdditiveExpression();
							if (xpathExpr2 == null)
							{
								this.ThrowError(QueryCompileError.InvalidExpression);
							}
							xpathExpr = new XPathRelationExpr(relationOperator, xpathExpr, xpathExpr2);
						}
					}
				}
				while (relationOperator != RelationOperator.None);
			}
			return xpathExpr;
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000C33E4 File Offset: 0x000C15E4
		private XPathStepExpr ParseStep()
		{
			QueryAxisType queryAxisType = this.ParseAxisSpecifier();
			bool flag = false;
			NodeSelectCriteria nodeSelectCriteria;
			if (queryAxisType != QueryAxisType.None)
			{
				nodeSelectCriteria = this.ParseNodeTest(queryAxisType);
			}
			else if (this.NextToken(XPathTokenID.Period) != null)
			{
				nodeSelectCriteria = new NodeSelectCriteria(QueryAxisType.Self, NodeQName.Empty, QueryNodeType.All);
				flag = true;
			}
			else if (this.NextToken(XPathTokenID.DblPeriod) != null)
			{
				nodeSelectCriteria = new NodeSelectCriteria(QueryAxisType.Parent, NodeQName.Empty, QueryNodeType.Ancestor);
				flag = true;
			}
			else if ((nodeSelectCriteria = this.ParseNodeTest(QueryAxisType.Child)) == null)
			{
				return null;
			}
			if (nodeSelectCriteria == null)
			{
				this.ThrowError(QueryCompileError.InvalidLocationStep);
			}
			XPathExprList predicates = null;
			if (!flag)
			{
				predicates = this.ParsePredicates();
			}
			return new XPathStepExpr(nodeSelectCriteria, predicates);
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000C347C File Offset: 0x000C167C
		private XPathExpr ParseUnaryExpression()
		{
			bool flag = false;
			bool flag2 = false;
			while (this.NextToken(XPathTokenID.Minus) != null)
			{
				flag2 = true;
				flag = !flag;
			}
			XPathExpr xpathExpr = this.ParseUnionExpression();
			if (xpathExpr != null)
			{
				if (flag2 && xpathExpr.ReturnType != ValueDataType.Double)
				{
					xpathExpr.ReturnType = ValueDataType.Double;
					xpathExpr.TypecastRequired = true;
				}
				xpathExpr.Negate = flag;
			}
			return xpathExpr;
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000C34D0 File Offset: 0x000C16D0
		internal XPathExpr ParseUnionExpression()
		{
			XPathExpr xpathExpr = this.ParsePathExpression();
			if (xpathExpr != null && this.NextToken(XPathTokenID.Pipe) != null)
			{
				this.EnsureReturnsNodeSet(xpathExpr);
				XPathExpr xpathExpr2 = this.ParseUnionExpression();
				if (xpathExpr2 == null)
				{
					this.ThrowError(QueryCompileError.CouldNotParseExpression);
				}
				this.EnsureReturnsNodeSet(xpathExpr2);
				return new XPathConjunctExpr(XPathExprType.Union, ValueDataType.Sequence, xpathExpr, xpathExpr2);
			}
			return xpathExpr;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000C3520 File Offset: 0x000C1720
		internal XPathExpr ParseVariableExpression()
		{
			XPathExpr result = null;
			if (this.context != null)
			{
				XPathToken xpathToken = this.NextToken(XPathTokenID.Variable);
				if (xpathToken != null)
				{
					NodeQName nodeQName = this.QualifyName(xpathToken.Prefix, xpathToken.Name);
					string prefix = this.context.LookupPrefix(nodeQName.Namespace);
					IXsltContextVariable xsltContextVariable = this.context.ResolveVariable(prefix, nodeQName.Name);
					if (xsltContextVariable != null)
					{
						result = new XPathXsltVariableExpr(this.context, xsltContextVariable);
					}
				}
			}
			return result;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000C3594 File Offset: 0x000C1794
		private void PushToken(XPathToken token)
		{
			this.readToken = token;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000C359D File Offset: 0x000C179D
		internal void ThrowError(QueryCompileError error)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(error, this.lexer.ConsumedSubstring()));
		}

		// Token: 0x04002720 RID: 10016
		private IFunctionLibrary[] functionLibraries;

		// Token: 0x04002721 RID: 10017
		private XPathLexer lexer;

		// Token: 0x04002722 RID: 10018
		private XmlNamespaceManager namespaces;

		// Token: 0x04002723 RID: 10019
		private XPathToken readToken;

		// Token: 0x04002724 RID: 10020
		private XsltContext context;

		// Token: 0x02000C52 RID: 3154
		internal struct QName
		{
			// Token: 0x060077A0 RID: 30624 RVA: 0x001BF1FA File Offset: 0x001BD3FA
			internal QName(string prefix, string name)
			{
				this.prefix = prefix;
				this.name = name;
			}

			// Token: 0x17001B53 RID: 6995
			// (get) Token: 0x060077A1 RID: 30625 RVA: 0x001BF20A File Offset: 0x001BD40A
			internal string Prefix
			{
				get
				{
					return this.prefix;
				}
			}

			// Token: 0x17001B54 RID: 6996
			// (get) Token: 0x060077A2 RID: 30626 RVA: 0x001BF212 File Offset: 0x001BD412
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x04004470 RID: 17520
			private string prefix;

			// Token: 0x04004471 RID: 17521
			private string name;
		}
	}
}

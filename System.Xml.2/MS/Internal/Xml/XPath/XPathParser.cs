using System;
using System.Collections;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000048 RID: 72
	internal class XPathParser
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00008547 File Offset: 0x00006747
		private XPathParser(XPathScanner scanner)
		{
			this.scanner = scanner;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008558 File Offset: 0x00006758
		public static AstNode ParseXPathExpresion(string xpathExpresion)
		{
			XPathScanner xpathScanner = new XPathScanner(xpathExpresion);
			XPathParser xpathParser = new XPathParser(xpathScanner);
			AstNode result = xpathParser.ParseExpresion(null);
			if (xpathScanner.Kind != XPathScanner.LexKind.Eof)
			{
				throw XPathException.Create("Xp_InvalidToken", xpathScanner.SourceText);
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008598 File Offset: 0x00006798
		public static AstNode ParseXPathPattern(string xpathPattern)
		{
			XPathScanner xpathScanner = new XPathScanner(xpathPattern);
			XPathParser xpathParser = new XPathParser(xpathScanner);
			AstNode result = xpathParser.ParsePattern(null);
			if (xpathScanner.Kind != XPathScanner.LexKind.Eof)
			{
				throw XPathException.Create("Xp_InvalidToken", xpathScanner.SourceText);
			}
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000085D8 File Offset: 0x000067D8
		private AstNode ParseExpresion(AstNode qyInput)
		{
			int num = this.parseDepth + 1;
			this.parseDepth = num;
			if (num > 200)
			{
				throw XPathException.Create("Xp_QueryTooComplex");
			}
			AstNode result = this.ParseOrExpr(qyInput);
			this.parseDepth--;
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008620 File Offset: 0x00006820
		private AstNode ParseOrExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseAndExpr(qyInput);
			while (this.TestOp("or"))
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.OR, astNode, this.ParseAndExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000865C File Offset: 0x0000685C
		private AstNode ParseAndExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseEqualityExpr(qyInput);
			while (this.TestOp("and"))
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.AND, astNode, this.ParseEqualityExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008698 File Offset: 0x00006898
		private AstNode ParseEqualityExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseRelationalExpr(qyInput);
			for (;;)
			{
				Operator.Op op = (this.scanner.Kind == XPathScanner.LexKind.Eq) ? Operator.Op.EQ : ((this.scanner.Kind == XPathScanner.LexKind.Ne) ? Operator.Op.NE : Operator.Op.INVALID);
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseRelationalExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000086F0 File Offset: 0x000068F0
		private AstNode ParseRelationalExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseAdditiveExpr(qyInput);
			for (;;)
			{
				Operator.Op op = (this.scanner.Kind == XPathScanner.LexKind.Lt) ? Operator.Op.LT : ((this.scanner.Kind == XPathScanner.LexKind.Le) ? Operator.Op.LE : ((this.scanner.Kind == XPathScanner.LexKind.Gt) ? Operator.Op.GT : ((this.scanner.Kind == XPathScanner.LexKind.Ge) ? Operator.Op.GE : Operator.Op.INVALID)));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseAdditiveExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000876C File Offset: 0x0000696C
		private AstNode ParseAdditiveExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseMultiplicativeExpr(qyInput);
			for (;;)
			{
				Operator.Op op = (this.scanner.Kind == XPathScanner.LexKind.Plus) ? Operator.Op.PLUS : ((this.scanner.Kind == XPathScanner.LexKind.Minus) ? Operator.Op.MINUS : Operator.Op.INVALID);
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseMultiplicativeExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000087C4 File Offset: 0x000069C4
		private AstNode ParseMultiplicativeExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseUnaryExpr(qyInput);
			for (;;)
			{
				Operator.Op op = (this.scanner.Kind == XPathScanner.LexKind.Star) ? Operator.Op.MUL : (this.TestOp("div") ? Operator.Op.DIV : (this.TestOp("mod") ? Operator.Op.MOD : Operator.Op.INVALID));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseUnaryExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000882C File Offset: 0x00006A2C
		private AstNode ParseUnaryExpr(AstNode qyInput)
		{
			bool flag = false;
			while (this.scanner.Kind == XPathScanner.LexKind.Minus)
			{
				this.NextLex();
				flag = !flag;
			}
			if (flag)
			{
				return new Operator(Operator.Op.MUL, this.ParseUnionExpr(qyInput), new Operand(-1.0));
			}
			return this.ParseUnionExpr(qyInput);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008880 File Offset: 0x00006A80
		private AstNode ParseUnionExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParsePathExpr(qyInput);
			while (this.scanner.Kind == XPathScanner.LexKind.Union)
			{
				this.NextLex();
				AstNode astNode2 = this.ParsePathExpr(qyInput);
				this.CheckNodeSet(astNode.ReturnType);
				this.CheckNodeSet(astNode2.ReturnType);
				astNode = new Operator(Operator.Op.UNION, astNode, astNode2);
			}
			return astNode;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000088D8 File Offset: 0x00006AD8
		private static bool IsNodeType(XPathScanner scaner)
		{
			return scaner.Prefix.Length == 0 && (scaner.Name == "node" || scaner.Name == "text" || scaner.Name == "processing-instruction" || scaner.Name == "comment");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000893C File Offset: 0x00006B3C
		private AstNode ParsePathExpr(AstNode qyInput)
		{
			AstNode astNode;
			if (XPathParser.IsPrimaryExpr(this.scanner))
			{
				astNode = this.ParseFilterExpr(qyInput);
				if (this.scanner.Kind == XPathScanner.LexKind.Slash)
				{
					this.NextLex();
					astNode = this.ParseRelativeLocationPath(astNode);
				}
				else if (this.scanner.Kind == XPathScanner.LexKind.SlashSlash)
				{
					this.NextLex();
					astNode = this.ParseRelativeLocationPath(new Axis(Axis.AxisType.DescendantOrSelf, astNode));
				}
			}
			else
			{
				astNode = this.ParseLocationPath(null);
			}
			return astNode;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000089AC File Offset: 0x00006BAC
		private AstNode ParseFilterExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParsePrimaryExpr(qyInput);
			while (this.scanner.Kind == XPathScanner.LexKind.LBracket)
			{
				astNode = new Filter(astNode, this.ParsePredicate(astNode));
			}
			return astNode;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000089E4 File Offset: 0x00006BE4
		private AstNode ParsePredicate(AstNode qyInput)
		{
			this.CheckNodeSet(qyInput.ReturnType);
			this.PassToken(XPathScanner.LexKind.LBracket);
			AstNode result = this.ParseExpresion(qyInput);
			this.PassToken(XPathScanner.LexKind.RBracket);
			return result;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008A18 File Offset: 0x00006C18
		private AstNode ParseLocationPath(AstNode qyInput)
		{
			if (this.scanner.Kind == XPathScanner.LexKind.Slash)
			{
				this.NextLex();
				AstNode astNode = new Root();
				if (XPathParser.IsStep(this.scanner.Kind))
				{
					astNode = this.ParseRelativeLocationPath(astNode);
				}
				return astNode;
			}
			if (this.scanner.Kind == XPathScanner.LexKind.SlashSlash)
			{
				this.NextLex();
				return this.ParseRelativeLocationPath(new Axis(Axis.AxisType.DescendantOrSelf, new Root()));
			}
			return this.ParseRelativeLocationPath(qyInput);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008A8C File Offset: 0x00006C8C
		private AstNode ParseRelativeLocationPath(AstNode qyInput)
		{
			AstNode astNode = qyInput;
			for (;;)
			{
				astNode = this.ParseStep(astNode);
				if (XPathScanner.LexKind.SlashSlash == this.scanner.Kind)
				{
					this.NextLex();
					astNode = new Axis(Axis.AxisType.DescendantOrSelf, astNode);
				}
				else
				{
					if (XPathScanner.LexKind.Slash != this.scanner.Kind)
					{
						break;
					}
					this.NextLex();
				}
			}
			return astNode;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008ADA File Offset: 0x00006CDA
		private static bool IsStep(XPathScanner.LexKind lexKind)
		{
			return lexKind == XPathScanner.LexKind.Dot || lexKind == XPathScanner.LexKind.DotDot || lexKind == XPathScanner.LexKind.At || lexKind == XPathScanner.LexKind.Axe || lexKind == XPathScanner.LexKind.Star || lexKind == XPathScanner.LexKind.Name;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008AFC File Offset: 0x00006CFC
		private AstNode ParseStep(AstNode qyInput)
		{
			AstNode astNode;
			if (XPathScanner.LexKind.Dot == this.scanner.Kind)
			{
				this.NextLex();
				astNode = new Axis(Axis.AxisType.Self, qyInput);
			}
			else if (XPathScanner.LexKind.DotDot == this.scanner.Kind)
			{
				this.NextLex();
				astNode = new Axis(Axis.AxisType.Parent, qyInput);
			}
			else
			{
				Axis.AxisType axisType = Axis.AxisType.Child;
				XPathScanner.LexKind kind = this.scanner.Kind;
				if (kind != XPathScanner.LexKind.At)
				{
					if (kind == XPathScanner.LexKind.Axe)
					{
						axisType = this.GetAxis(this.scanner);
						this.NextLex();
					}
				}
				else
				{
					axisType = Axis.AxisType.Attribute;
					this.NextLex();
				}
				XPathNodeType nodeType = (axisType == Axis.AxisType.Attribute) ? XPathNodeType.Attribute : XPathNodeType.Element;
				astNode = this.ParseNodeTest(qyInput, axisType, nodeType);
				while (XPathScanner.LexKind.LBracket == this.scanner.Kind)
				{
					astNode = new Filter(astNode, this.ParsePredicate(astNode));
				}
			}
			return astNode;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008BB8 File Offset: 0x00006DB8
		private AstNode ParseNodeTest(AstNode qyInput, Axis.AxisType axisType, XPathNodeType nodeType)
		{
			XPathScanner.LexKind kind = this.scanner.Kind;
			string prefix;
			string text;
			if (kind != XPathScanner.LexKind.Star)
			{
				if (kind != XPathScanner.LexKind.Name)
				{
					throw XPathException.Create("Xp_NodeSetExpected", this.scanner.SourceText);
				}
				if (this.scanner.CanBeFunction && XPathParser.IsNodeType(this.scanner))
				{
					prefix = string.Empty;
					text = string.Empty;
					nodeType = ((this.scanner.Name == "comment") ? XPathNodeType.Comment : ((this.scanner.Name == "text") ? XPathNodeType.Text : ((this.scanner.Name == "node") ? XPathNodeType.All : ((this.scanner.Name == "processing-instruction") ? XPathNodeType.ProcessingInstruction : XPathNodeType.Root))));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					if (nodeType == XPathNodeType.ProcessingInstruction && this.scanner.Kind != XPathScanner.LexKind.RParens)
					{
						this.CheckToken(XPathScanner.LexKind.String);
						text = this.scanner.StringValue;
						this.NextLex();
					}
					this.PassToken(XPathScanner.LexKind.RParens);
				}
				else
				{
					prefix = this.scanner.Prefix;
					text = this.scanner.Name;
					this.NextLex();
					if (text == "*")
					{
						text = string.Empty;
					}
				}
			}
			else
			{
				prefix = string.Empty;
				text = string.Empty;
				this.NextLex();
			}
			return new Axis(axisType, qyInput, prefix, text, nodeType);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008D28 File Offset: 0x00006F28
		private static bool IsPrimaryExpr(XPathScanner scanner)
		{
			return scanner.Kind == XPathScanner.LexKind.String || scanner.Kind == XPathScanner.LexKind.Number || scanner.Kind == XPathScanner.LexKind.Dollar || scanner.Kind == XPathScanner.LexKind.LParens || (scanner.Kind == XPathScanner.LexKind.Name && scanner.CanBeFunction && !XPathParser.IsNodeType(scanner));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008D7C File Offset: 0x00006F7C
		private AstNode ParsePrimaryExpr(AstNode qyInput)
		{
			AstNode astNode = null;
			XPathScanner.LexKind kind = this.scanner.Kind;
			if (kind <= XPathScanner.LexKind.LParens)
			{
				if (kind != XPathScanner.LexKind.Dollar)
				{
					if (kind == XPathScanner.LexKind.LParens)
					{
						this.NextLex();
						astNode = this.ParseExpresion(qyInput);
						if (astNode.Type != AstNode.AstType.ConstantOperand)
						{
							astNode = new Group(astNode);
						}
						this.PassToken(XPathScanner.LexKind.RParens);
					}
				}
				else
				{
					this.NextLex();
					this.CheckToken(XPathScanner.LexKind.Name);
					astNode = new Variable(this.scanner.Name, this.scanner.Prefix);
					this.NextLex();
				}
			}
			else if (kind != XPathScanner.LexKind.Number)
			{
				if (kind != XPathScanner.LexKind.Name)
				{
					if (kind == XPathScanner.LexKind.String)
					{
						astNode = new Operand(this.scanner.StringValue);
						this.NextLex();
					}
				}
				else if (this.scanner.CanBeFunction && !XPathParser.IsNodeType(this.scanner))
				{
					astNode = this.ParseMethod(null);
				}
			}
			else
			{
				astNode = new Operand(this.scanner.NumberValue);
				this.NextLex();
			}
			return astNode;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008E78 File Offset: 0x00007078
		private AstNode ParseMethod(AstNode qyInput)
		{
			ArrayList arrayList = new ArrayList();
			string name = this.scanner.Name;
			string prefix = this.scanner.Prefix;
			this.PassToken(XPathScanner.LexKind.Name);
			this.PassToken(XPathScanner.LexKind.LParens);
			if (this.scanner.Kind != XPathScanner.LexKind.RParens)
			{
				for (;;)
				{
					arrayList.Add(this.ParseExpresion(qyInput));
					if (this.scanner.Kind == XPathScanner.LexKind.RParens)
					{
						break;
					}
					this.PassToken(XPathScanner.LexKind.Comma);
				}
			}
			this.PassToken(XPathScanner.LexKind.RParens);
			if (prefix.Length == 0)
			{
				XPathParser.ParamInfo paramInfo = (XPathParser.ParamInfo)XPathParser.functionTable[name];
				if (paramInfo != null)
				{
					int num = arrayList.Count;
					if (num < paramInfo.Minargs)
					{
						throw XPathException.Create("Xp_InvalidNumArgs", name, this.scanner.SourceText);
					}
					if (paramInfo.FType == Function.FunctionType.FuncConcat)
					{
						for (int i = 0; i < num; i++)
						{
							AstNode astNode = (AstNode)arrayList[i];
							if (astNode.ReturnType != XPathResultType.String)
							{
								astNode = new Function(Function.FunctionType.FuncString, astNode);
							}
							arrayList[i] = astNode;
						}
					}
					else
					{
						if (paramInfo.Maxargs < num)
						{
							throw XPathException.Create("Xp_InvalidNumArgs", name, this.scanner.SourceText);
						}
						if (paramInfo.ArgTypes.Length < num)
						{
							num = paramInfo.ArgTypes.Length;
						}
						for (int j = 0; j < num; j++)
						{
							AstNode astNode2 = (AstNode)arrayList[j];
							if (paramInfo.ArgTypes[j] != XPathResultType.Any && paramInfo.ArgTypes[j] != astNode2.ReturnType)
							{
								switch (paramInfo.ArgTypes[j])
								{
								case XPathResultType.Number:
									astNode2 = new Function(Function.FunctionType.FuncNumber, astNode2);
									break;
								case XPathResultType.String:
									astNode2 = new Function(Function.FunctionType.FuncString, astNode2);
									break;
								case XPathResultType.Boolean:
									astNode2 = new Function(Function.FunctionType.FuncBoolean, astNode2);
									break;
								case XPathResultType.NodeSet:
									if (!(astNode2 is Variable) && (!(astNode2 is Function) || astNode2.ReturnType != XPathResultType.Any))
									{
										throw XPathException.Create("Xp_InvalidArgumentType", name, this.scanner.SourceText);
									}
									break;
								}
								arrayList[j] = astNode2;
							}
						}
					}
					return new Function(paramInfo.FType, arrayList);
				}
			}
			return new Function(prefix, name, arrayList);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000090A4 File Offset: 0x000072A4
		private AstNode ParsePattern(AstNode qyInput)
		{
			AstNode astNode = this.ParseLocationPathPattern(qyInput);
			while (this.scanner.Kind == XPathScanner.LexKind.Union)
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.UNION, astNode, this.ParseLocationPathPattern(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000090E4 File Offset: 0x000072E4
		private AstNode ParseLocationPathPattern(AstNode qyInput)
		{
			AstNode astNode = null;
			XPathScanner.LexKind kind = this.scanner.Kind;
			if (kind != XPathScanner.LexKind.Slash)
			{
				if (kind != XPathScanner.LexKind.SlashSlash)
				{
					if (kind == XPathScanner.LexKind.Name)
					{
						if (this.scanner.CanBeFunction)
						{
							astNode = this.ParseIdKeyPattern(qyInput);
							if (astNode != null)
							{
								XPathScanner.LexKind kind2 = this.scanner.Kind;
								if (kind2 != XPathScanner.LexKind.Slash)
								{
									if (kind2 != XPathScanner.LexKind.SlashSlash)
									{
										return astNode;
									}
									this.NextLex();
									astNode = new Axis(Axis.AxisType.DescendantOrSelf, astNode);
								}
								else
								{
									this.NextLex();
								}
							}
						}
					}
				}
				else
				{
					this.NextLex();
					astNode = new Axis(Axis.AxisType.DescendantOrSelf, new Root());
				}
			}
			else
			{
				this.NextLex();
				astNode = new Root();
				if (this.scanner.Kind == XPathScanner.LexKind.Eof || this.scanner.Kind == XPathScanner.LexKind.Union)
				{
					return astNode;
				}
			}
			return this.ParseRelativePathPattern(astNode);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000091A4 File Offset: 0x000073A4
		private AstNode ParseIdKeyPattern(AstNode qyInput)
		{
			ArrayList arrayList = new ArrayList();
			if (this.scanner.Prefix.Length == 0)
			{
				if (this.scanner.Name == "id")
				{
					XPathParser.ParamInfo paramInfo = (XPathParser.ParamInfo)XPathParser.functionTable["id"];
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					this.CheckToken(XPathScanner.LexKind.String);
					arrayList.Add(new Operand(this.scanner.StringValue));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.RParens);
					return new Function(paramInfo.FType, arrayList);
				}
				if (this.scanner.Name == "key")
				{
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					this.CheckToken(XPathScanner.LexKind.String);
					arrayList.Add(new Operand(this.scanner.StringValue));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.Comma);
					this.CheckToken(XPathScanner.LexKind.String);
					arrayList.Add(new Operand(this.scanner.StringValue));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.RParens);
					return new Function("", "key", arrayList);
				}
			}
			return null;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000092D4 File Offset: 0x000074D4
		private AstNode ParseRelativePathPattern(AstNode qyInput)
		{
			AstNode astNode = this.ParseStepPattern(qyInput);
			if (XPathScanner.LexKind.SlashSlash == this.scanner.Kind)
			{
				this.NextLex();
				astNode = this.ParseRelativePathPattern(new Axis(Axis.AxisType.DescendantOrSelf, astNode));
			}
			else if (XPathScanner.LexKind.Slash == this.scanner.Kind)
			{
				this.NextLex();
				astNode = this.ParseRelativePathPattern(astNode);
			}
			return astNode;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000932C File Offset: 0x0000752C
		private AstNode ParseStepPattern(AstNode qyInput)
		{
			Axis.AxisType axisType = Axis.AxisType.Child;
			XPathScanner.LexKind kind = this.scanner.Kind;
			if (kind != XPathScanner.LexKind.At)
			{
				if (kind == XPathScanner.LexKind.Axe)
				{
					axisType = this.GetAxis(this.scanner);
					if (axisType != Axis.AxisType.Child && axisType != Axis.AxisType.Attribute)
					{
						throw XPathException.Create("Xp_InvalidToken", this.scanner.SourceText);
					}
					this.NextLex();
				}
			}
			else
			{
				axisType = Axis.AxisType.Attribute;
				this.NextLex();
			}
			XPathNodeType nodeType = (axisType == Axis.AxisType.Attribute) ? XPathNodeType.Attribute : XPathNodeType.Element;
			AstNode astNode = this.ParseNodeTest(qyInput, axisType, nodeType);
			while (XPathScanner.LexKind.LBracket == this.scanner.Kind)
			{
				astNode = new Filter(astNode, this.ParsePredicate(astNode));
			}
			return astNode;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000093C1 File Offset: 0x000075C1
		private void CheckToken(XPathScanner.LexKind t)
		{
			if (this.scanner.Kind != t)
			{
				throw XPathException.Create("Xp_InvalidToken", this.scanner.SourceText);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000093E7 File Offset: 0x000075E7
		private void PassToken(XPathScanner.LexKind t)
		{
			this.CheckToken(t);
			this.NextLex();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000093F6 File Offset: 0x000075F6
		private void NextLex()
		{
			this.scanner.NextLex();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009404 File Offset: 0x00007604
		private bool TestOp(string op)
		{
			return this.scanner.Kind == XPathScanner.LexKind.Name && this.scanner.Prefix.Length == 0 && this.scanner.Name.Equals(op);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000943A File Offset: 0x0000763A
		private void CheckNodeSet(XPathResultType t)
		{
			if (t != XPathResultType.NodeSet && t != XPathResultType.Any)
			{
				throw XPathException.Create("Xp_NodeSetExpected", this.scanner.SourceText);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000945C File Offset: 0x0000765C
		private static Hashtable CreateFunctionTable()
		{
			return new Hashtable(36)
			{
				{
					"last",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLast, 0, 0, XPathParser.temparray1)
				},
				{
					"position",
					new XPathParser.ParamInfo(Function.FunctionType.FuncPosition, 0, 0, XPathParser.temparray1)
				},
				{
					"name",
					new XPathParser.ParamInfo(Function.FunctionType.FuncName, 0, 1, XPathParser.temparray2)
				},
				{
					"namespace-uri",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNameSpaceUri, 0, 1, XPathParser.temparray2)
				},
				{
					"local-name",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLocalName, 0, 1, XPathParser.temparray2)
				},
				{
					"count",
					new XPathParser.ParamInfo(Function.FunctionType.FuncCount, 1, 1, XPathParser.temparray2)
				},
				{
					"id",
					new XPathParser.ParamInfo(Function.FunctionType.FuncID, 1, 1, XPathParser.temparray3)
				},
				{
					"string",
					new XPathParser.ParamInfo(Function.FunctionType.FuncString, 0, 1, XPathParser.temparray3)
				},
				{
					"concat",
					new XPathParser.ParamInfo(Function.FunctionType.FuncConcat, 2, 100, XPathParser.temparray4)
				},
				{
					"starts-with",
					new XPathParser.ParamInfo(Function.FunctionType.FuncStartsWith, 2, 2, XPathParser.temparray5)
				},
				{
					"contains",
					new XPathParser.ParamInfo(Function.FunctionType.FuncContains, 2, 2, XPathParser.temparray5)
				},
				{
					"substring-before",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstringBefore, 2, 2, XPathParser.temparray5)
				},
				{
					"substring-after",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstringAfter, 2, 2, XPathParser.temparray5)
				},
				{
					"substring",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstring, 2, 3, XPathParser.temparray6)
				},
				{
					"string-length",
					new XPathParser.ParamInfo(Function.FunctionType.FuncStringLength, 0, 1, XPathParser.temparray4)
				},
				{
					"normalize-space",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNormalize, 0, 1, XPathParser.temparray4)
				},
				{
					"translate",
					new XPathParser.ParamInfo(Function.FunctionType.FuncTranslate, 3, 3, XPathParser.temparray7)
				},
				{
					"boolean",
					new XPathParser.ParamInfo(Function.FunctionType.FuncBoolean, 1, 1, XPathParser.temparray3)
				},
				{
					"not",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNot, 1, 1, XPathParser.temparray8)
				},
				{
					"true",
					new XPathParser.ParamInfo(Function.FunctionType.FuncTrue, 0, 0, XPathParser.temparray8)
				},
				{
					"false",
					new XPathParser.ParamInfo(Function.FunctionType.FuncFalse, 0, 0, XPathParser.temparray8)
				},
				{
					"lang",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLang, 1, 1, XPathParser.temparray4)
				},
				{
					"number",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNumber, 0, 1, XPathParser.temparray3)
				},
				{
					"sum",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSum, 1, 1, XPathParser.temparray2)
				},
				{
					"floor",
					new XPathParser.ParamInfo(Function.FunctionType.FuncFloor, 1, 1, XPathParser.temparray9)
				},
				{
					"ceiling",
					new XPathParser.ParamInfo(Function.FunctionType.FuncCeiling, 1, 1, XPathParser.temparray9)
				},
				{
					"round",
					new XPathParser.ParamInfo(Function.FunctionType.FuncRound, 1, 1, XPathParser.temparray9)
				}
			};
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009710 File Offset: 0x00007910
		private static Hashtable CreateAxesTable()
		{
			return new Hashtable(13)
			{
				{
					"ancestor",
					Axis.AxisType.Ancestor
				},
				{
					"ancestor-or-self",
					Axis.AxisType.AncestorOrSelf
				},
				{
					"attribute",
					Axis.AxisType.Attribute
				},
				{
					"child",
					Axis.AxisType.Child
				},
				{
					"descendant",
					Axis.AxisType.Descendant
				},
				{
					"descendant-or-self",
					Axis.AxisType.DescendantOrSelf
				},
				{
					"following",
					Axis.AxisType.Following
				},
				{
					"following-sibling",
					Axis.AxisType.FollowingSibling
				},
				{
					"namespace",
					Axis.AxisType.Namespace
				},
				{
					"parent",
					Axis.AxisType.Parent
				},
				{
					"preceding",
					Axis.AxisType.Preceding
				},
				{
					"preceding-sibling",
					Axis.AxisType.PrecedingSibling
				},
				{
					"self",
					Axis.AxisType.Self
				}
			};
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009808 File Offset: 0x00007A08
		private Axis.AxisType GetAxis(XPathScanner scaner)
		{
			object obj = XPathParser.AxesTable[scaner.Name];
			if (obj == null)
			{
				throw XPathException.Create("Xp_InvalidToken", this.scanner.SourceText);
			}
			return (Axis.AxisType)obj;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009848 File Offset: 0x00007A48
		// Note: this type is marked as 'beforefieldinit'.
		static XPathParser()
		{
			XPathResultType[] array = new XPathResultType[3];
			array[0] = XPathResultType.String;
			XPathParser.temparray6 = array;
			XPathParser.temparray7 = new XPathResultType[]
			{
				XPathResultType.String,
				XPathResultType.String,
				XPathResultType.String
			};
			XPathParser.temparray8 = new XPathResultType[]
			{
				XPathResultType.Boolean
			};
			XPathParser.temparray9 = new XPathResultType[1];
			XPathParser.functionTable = XPathParser.CreateFunctionTable();
			XPathParser.AxesTable = XPathParser.CreateAxesTable();
		}

		// Token: 0x040000E3 RID: 227
		private XPathScanner scanner;

		// Token: 0x040000E4 RID: 228
		private int parseDepth;

		// Token: 0x040000E5 RID: 229
		private const int MaxParseDepth = 200;

		// Token: 0x040000E6 RID: 230
		private static readonly XPathResultType[] temparray1 = new XPathResultType[0];

		// Token: 0x040000E7 RID: 231
		private static readonly XPathResultType[] temparray2 = new XPathResultType[]
		{
			XPathResultType.NodeSet
		};

		// Token: 0x040000E8 RID: 232
		private static readonly XPathResultType[] temparray3 = new XPathResultType[]
		{
			XPathResultType.Any
		};

		// Token: 0x040000E9 RID: 233
		private static readonly XPathResultType[] temparray4 = new XPathResultType[]
		{
			XPathResultType.String
		};

		// Token: 0x040000EA RID: 234
		private static readonly XPathResultType[] temparray5 = new XPathResultType[]
		{
			XPathResultType.String,
			XPathResultType.String
		};

		// Token: 0x040000EB RID: 235
		private static readonly XPathResultType[] temparray6;

		// Token: 0x040000EC RID: 236
		private static readonly XPathResultType[] temparray7;

		// Token: 0x040000ED RID: 237
		private static readonly XPathResultType[] temparray8;

		// Token: 0x040000EE RID: 238
		private static readonly XPathResultType[] temparray9;

		// Token: 0x040000EF RID: 239
		private static Hashtable functionTable;

		// Token: 0x040000F0 RID: 240
		private static Hashtable AxesTable;

		// Token: 0x02000302 RID: 770
		private class ParamInfo
		{
			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000ECADA File Offset: 0x000EACDA
			public Function.FunctionType FType
			{
				get
				{
					return this.ftype;
				}
			}

			// Token: 0x17000A13 RID: 2579
			// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000ECAE2 File Offset: 0x000EACE2
			public int Minargs
			{
				get
				{
					return this.minargs;
				}
			}

			// Token: 0x17000A14 RID: 2580
			// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000ECAEA File Offset: 0x000EACEA
			public int Maxargs
			{
				get
				{
					return this.maxargs;
				}
			}

			// Token: 0x17000A15 RID: 2581
			// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000ECAF2 File Offset: 0x000EACF2
			public XPathResultType[] ArgTypes
			{
				get
				{
					return this.argTypes;
				}
			}

			// Token: 0x06002D90 RID: 11664 RVA: 0x000ECAFA File Offset: 0x000EACFA
			internal ParamInfo(Function.FunctionType ftype, int minargs, int maxargs, XPathResultType[] argTypes)
			{
				this.ftype = ftype;
				this.minargs = minargs;
				this.maxargs = maxargs;
				this.argTypes = argTypes;
			}

			// Token: 0x04001418 RID: 5144
			private Function.FunctionType ftype;

			// Token: 0x04001419 RID: 5145
			private int minargs;

			// Token: 0x0400141A RID: 5146
			private int maxargs;

			// Token: 0x0400141B RID: 5147
			private XPathResultType[] argTypes;
		}
	}
}

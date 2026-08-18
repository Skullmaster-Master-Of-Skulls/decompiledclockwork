using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020001AA RID: 426
	internal sealed class ExpressionParser
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x002546E8 File Offset: 0x00253AE8
		internal ExpressionParser(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00254748 File Offset: 0x00253B48
		internal void LoadExpression(string data)
		{
			int num;
			if (data == null)
			{
				num = 0;
				this.text = new char[num + 1];
			}
			else
			{
				num = data.Length;
				this.text = new char[num + 1];
				data.CopyTo(0, this.text, 0, num);
			}
			this.text[num] = '\0';
			if (this.expression != null)
			{
				this.expression = null;
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x002547A8 File Offset: 0x00253BA8
		internal void StartScan()
		{
			this.op = 0;
			this.pos = 0;
			this.start = 0;
			this.topOperator = 0;
			this.ops[this.topOperator++] = new OperatorInfo(Nodes.Noop, 0, 0);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x002547F8 File Offset: 0x00253BF8
		internal ExpressionNode Parse()
		{
			this.expression = null;
			this.StartScan();
			int num = 0;
			while (this.token != Tokens.EOS)
			{
				OperatorInfo operatorInfo;
				for (;;)
				{
					this.Scan();
					switch (this.token)
					{
					case Tokens.Name:
					case Tokens.Numeric:
					case Tokens.Decimal:
					case Tokens.Float:
					case Tokens.StringConst:
					case Tokens.Date:
					case Tokens.Parent:
					{
						ExpressionNode expressionNode = null;
						if (this.prevOperand != 0)
						{
							goto Block_5;
						}
						if (this.topOperator > 0)
						{
							operatorInfo = this.ops[this.topOperator - 1];
							if (operatorInfo.type == Nodes.Binop && operatorInfo.op == 5 && this.token != Tokens.Parent)
							{
								goto Block_9;
							}
						}
						this.prevOperand = 1;
						Tokens tokens = this.token;
						switch (tokens)
						{
						case Tokens.Name:
							operatorInfo = this.ops[this.topOperator - 1];
							expressionNode = new NameNode(this._table, this.text, this.start, this.pos);
							break;
						case Tokens.Numeric:
						{
							string constant = new string(this.text, this.start, this.pos - this.start);
							expressionNode = new ConstNode(this._table, ValueType.Numeric, constant);
							break;
						}
						case Tokens.Decimal:
						{
							string constant = new string(this.text, this.start, this.pos - this.start);
							expressionNode = new ConstNode(this._table, ValueType.Decimal, constant);
							break;
						}
						case Tokens.Float:
						{
							string constant = new string(this.text, this.start, this.pos - this.start);
							expressionNode = new ConstNode(this._table, ValueType.Float, constant);
							break;
						}
						case Tokens.BinaryConst:
							break;
						case Tokens.StringConst:
						{
							string constant = new string(this.text, this.start + 1, this.pos - this.start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Str, constant);
							break;
						}
						case Tokens.Date:
						{
							string constant = new string(this.text, this.start + 1, this.pos - this.start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Date, constant);
							break;
						}
						default:
							if (tokens == Tokens.Parent)
							{
								string relationName;
								try
								{
									this.Scan();
									if (this.token == Tokens.LeftParen)
									{
										this.ScanToken(Tokens.Name);
										relationName = NameNode.ParseName(this.text, this.start, this.pos);
										this.ScanToken(Tokens.RightParen);
										this.ScanToken(Tokens.Dot);
									}
									else
									{
										relationName = null;
										this.CheckToken(Tokens.Dot);
									}
								}
								catch (Exception e)
								{
									if (!ADP.IsCatchableExceptionType(e))
									{
										throw;
									}
									throw ExprException.LookupArgument();
								}
								this.ScanToken(Tokens.Name);
								string columnName = NameNode.ParseName(this.text, this.start, this.pos);
								operatorInfo = this.ops[this.topOperator - 1];
								expressionNode = new LookupNode(this._table, columnName, relationName);
							}
							break;
						}
						this.NodePush(expressionNode);
						continue;
					}
					case Tokens.ListSeparator:
					{
						if (this.prevOperand == 0)
						{
							goto Block_23;
						}
						this.BuildExpression(3);
						operatorInfo = this.ops[this.topOperator - 1];
						if (operatorInfo.type != Nodes.Call)
						{
							goto Block_24;
						}
						ExpressionNode argument = this.NodePop();
						FunctionNode functionNode = (FunctionNode)this.NodePop();
						functionNode.AddArgument(argument);
						this.NodePush(functionNode);
						this.prevOperand = 0;
						continue;
					}
					case Tokens.LeftParen:
						num++;
						if (this.prevOperand == 0)
						{
							operatorInfo = this.ops[this.topOperator - 1];
							if (operatorInfo.type == Nodes.Binop && operatorInfo.op == 5)
							{
								ExpressionNode expressionNode = new FunctionNode(this._table, "In");
								this.NodePush(expressionNode);
								this.ops[this.topOperator++] = new OperatorInfo(Nodes.Call, 0, 2);
								continue;
							}
							this.ops[this.topOperator++] = new OperatorInfo(Nodes.Paren, 0, 2);
							continue;
						}
						else
						{
							this.BuildExpression(22);
							this.prevOperand = 0;
							ExpressionNode expressionNode2 = this.NodePeek();
							if (expressionNode2 == null || expressionNode2.GetType() != typeof(NameNode))
							{
								goto IL_40A;
							}
							NameNode nameNode = (NameNode)this.NodePop();
							ExpressionNode expressionNode = new FunctionNode(this._table, nameNode.name);
							Aggregate aggregate = (Aggregate)((FunctionNode)expressionNode).Aggregate;
							if (aggregate != Aggregate.None)
							{
								expressionNode = this.ParseAggregateArgument((FunctionId)aggregate);
								this.NodePush(expressionNode);
								this.prevOperand = 2;
								continue;
							}
							this.NodePush(expressionNode);
							this.ops[this.topOperator++] = new OperatorInfo(Nodes.Call, 0, 2);
							continue;
						}
						break;
					case Tokens.RightParen:
						if (this.prevOperand != 0)
						{
							this.BuildExpression(3);
						}
						if (this.topOperator <= 1)
						{
							goto Block_18;
						}
						this.topOperator--;
						operatorInfo = this.ops[this.topOperator];
						if (this.prevOperand == 0 && operatorInfo.type != Nodes.Call)
						{
							goto Block_20;
						}
						if (operatorInfo.type == Nodes.Call)
						{
							if (this.prevOperand != 0)
							{
								ExpressionNode argument2 = this.NodePop();
								FunctionNode functionNode2 = (FunctionNode)this.NodePop();
								functionNode2.AddArgument(argument2);
								functionNode2.Check();
								this.NodePush(functionNode2);
							}
						}
						else
						{
							ExpressionNode expressionNode = this.NodePop();
							expressionNode = new UnaryNode(this._table, 0, expressionNode);
							this.NodePush(expressionNode);
						}
						this.prevOperand = 2;
						num--;
						continue;
					case Tokens.ZeroOp:
						if (this.prevOperand != 0)
						{
							goto Block_28;
						}
						this.ops[this.topOperator++] = new OperatorInfo(Nodes.Zop, this.op, 24);
						this.prevOperand = 2;
						continue;
					case Tokens.UnaryOp:
						goto IL_642;
					case Tokens.BinaryOp:
						if (this.prevOperand != 0)
						{
							this.prevOperand = 0;
							this.BuildExpression(Operators.Priority(this.op));
							this.ops[this.topOperator++] = new OperatorInfo(Nodes.Binop, this.op, Operators.Priority(this.op));
							continue;
						}
						if (this.op == 15)
						{
							this.op = 2;
							goto IL_642;
						}
						if (this.op == 16)
						{
							this.op = 1;
							goto IL_642;
						}
						goto IL_5E3;
					case Tokens.Dot:
					{
						ExpressionNode expressionNode3 = this.NodePeek();
						if (expressionNode3 != null && expressionNode3.GetType() == typeof(NameNode))
						{
							this.Scan();
							if (this.token == Tokens.Name)
							{
								NameNode nameNode2 = (NameNode)this.NodePop();
								string name = nameNode2.name + "." + NameNode.ParseName(this.text, this.start, this.pos);
								this.NodePush(new NameNode(this._table, name));
								continue;
							}
						}
						break;
					}
					case Tokens.EOS:
						goto IL_79;
					}
					goto Block_1;
					IL_642:
					this.ops[this.topOperator++] = new OperatorInfo(Nodes.Unop, this.op, Operators.Priority(this.op));
				}
				IL_79:
				if (this.prevOperand == 0)
				{
					if (this.topNode != 0)
					{
						operatorInfo = this.ops[this.topOperator - 1];
						throw ExprException.MissingOperand(operatorInfo);
					}
					continue;
				}
				else
				{
					this.BuildExpression(3);
					if (this.topOperator != 1)
					{
						throw ExprException.MissingRightParen();
					}
					continue;
				}
				Block_1:
				goto IL_756;
				Block_5:
				throw ExprException.MissingOperator(new string(this.text, this.start, this.pos - this.start));
				Block_9:
				throw ExprException.InWithoutParentheses();
				IL_40A:
				throw ExprException.SyntaxError();
				Block_18:
				throw ExprException.TooManyRightParentheses();
				Block_20:
				throw ExprException.MissingOperand(operatorInfo);
				Block_23:
				throw ExprException.MissingOperandBefore(",");
				Block_24:
				throw ExprException.SyntaxError();
				IL_5E3:
				throw ExprException.MissingOperandBefore(Operators.ToString(this.op));
				Block_28:
				throw ExprException.MissingOperator(new string(this.text, this.start, this.pos - this.start));
				IL_756:
				throw ExprException.UnknownToken(new string(this.text, this.start, this.pos - this.start), this.start + 1);
			}
			this.expression = this.NodeStack[0];
			return this.expression;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00254FC8 File Offset: 0x002543C8
		private ExpressionNode ParseAggregateArgument(FunctionId aggregate)
		{
			this.Scan();
			string columnName;
			bool flag;
			string relationName;
			try
			{
				if (this.token != Tokens.Child)
				{
					if (this.token != Tokens.Name)
					{
						throw ExprException.AggregateArgument();
					}
					columnName = NameNode.ParseName(this.text, this.start, this.pos);
					this.ScanToken(Tokens.RightParen);
					return new AggregateNode(this._table, aggregate, columnName);
				}
				else
				{
					flag = (this.token == Tokens.Child);
					this.prevOperand = 1;
					this.Scan();
					if (this.token == Tokens.LeftParen)
					{
						this.ScanToken(Tokens.Name);
						relationName = NameNode.ParseName(this.text, this.start, this.pos);
						this.ScanToken(Tokens.RightParen);
						this.ScanToken(Tokens.Dot);
					}
					else
					{
						relationName = null;
						this.CheckToken(Tokens.Dot);
					}
					this.ScanToken(Tokens.Name);
					columnName = NameNode.ParseName(this.text, this.start, this.pos);
					this.ScanToken(Tokens.RightParen);
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				throw ExprException.AggregateArgument();
			}
			return new AggregateNode(this._table, aggregate, columnName, !flag, relationName);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x002550F8 File Offset: 0x002544F8
		private ExpressionNode NodePop()
		{
			return this.NodeStack[--this.topNode];
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00255128 File Offset: 0x00254528
		private ExpressionNode NodePeek()
		{
			if (this.topNode <= 0)
			{
				return null;
			}
			return this.NodeStack[this.topNode - 1];
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00255158 File Offset: 0x00254558
		private void NodePush(ExpressionNode node)
		{
			if (this.topNode >= 98)
			{
				throw ExprException.ExpressionTooComplex();
			}
			this.NodeStack[this.topNode++] = node;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00255198 File Offset: 0x00254598
		private void BuildExpression(int pri)
		{
			OperatorInfo operatorInfo;
			for (;;)
			{
				operatorInfo = this.ops[this.topOperator - 1];
				if (operatorInfo.priority < pri)
				{
					break;
				}
				this.topOperator--;
				ExpressionNode node;
				switch (operatorInfo.type)
				{
				case Nodes.Unop:
				{
					ExpressionNode right = this.NodePop();
					int num = operatorInfo.op;
					switch (num)
					{
					case 1:
					case 2:
					case 3:
						break;
					default:
						if (num == 25)
						{
							goto Block_5;
						}
						break;
					}
					node = new UnaryNode(this._table, operatorInfo.op, right);
					goto IL_16C;
				}
				case Nodes.UnopSpec:
				case Nodes.BinopSpec:
					return;
				case Nodes.Binop:
				{
					ExpressionNode right = this.NodePop();
					ExpressionNode left = this.NodePop();
					switch (operatorInfo.op)
					{
					case 4:
					case 6:
					case 22:
					case 23:
					case 24:
					case 25:
						goto IL_D1;
					}
					if (operatorInfo.op == 14)
					{
						node = new LikeNode(this._table, operatorInfo.op, left, right);
						goto IL_16C;
					}
					node = new BinaryNode(this._table, operatorInfo.op, left, right);
					goto IL_16C;
				}
				case Nodes.Zop:
					node = new ZeroOpNode(operatorInfo.op);
					goto IL_16C;
				}
				return;
				IL_16C:
				this.NodePush(node);
			}
			return;
			IL_D1:
			throw ExprException.UnsupportedOperator(operatorInfo.op);
			Block_5:
			throw ExprException.UnsupportedOperator(operatorInfo.op);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00255328 File Offset: 0x00254728
		internal void CheckToken(Tokens token)
		{
			if (this.token != token)
			{
				throw ExprException.UnknownToken(token, this.token, this.pos);
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00255358 File Offset: 0x00254758
		internal Tokens Scan()
		{
			char[] array = this.text;
			this.token = Tokens.None;
			char c;
			char c2;
			for (;;)
			{
				this.start = this.pos;
				this.op = 0;
				c = array[this.pos++];
				c2 = c;
				if (c2 > '/')
				{
					goto IL_AE;
				}
				if (c2 == '\0')
				{
					goto IL_104;
				}
				switch (c2)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_311;
				default:
					switch (c2)
					{
					case ' ':
						goto IL_111;
					case '#':
						goto IL_136;
					case '%':
						goto IL_26E;
					case '&':
						goto IL_283;
					case '\'':
						goto IL_148;
					case '(':
						goto IL_11C;
					case ')':
						goto IL_129;
					case '*':
						goto IL_244;
					case '+':
						goto IL_21A;
					case '-':
						goto IL_22F;
					case '/':
						goto IL_259;
					}
					goto Block_4;
				}
				IL_111:
				this.ScanWhite();
			}
			Block_4:
			goto IL_311;
			IL_AE:
			if (c2 <= '[')
			{
				switch (c2)
				{
				case '<':
					this.token = Tokens.BinaryOp;
					this.ScanWhite();
					if (array[this.pos] == '=')
					{
						this.pos++;
						this.op = 11;
						goto IL_3E5;
					}
					if (array[this.pos] == '>')
					{
						this.pos++;
						this.op = 12;
						goto IL_3E5;
					}
					this.op = 9;
					goto IL_3E5;
				case '=':
					this.token = Tokens.BinaryOp;
					this.op = 7;
					goto IL_3E5;
				case '>':
					this.token = Tokens.BinaryOp;
					this.ScanWhite();
					if (array[this.pos] == '=')
					{
						this.pos++;
						this.op = 10;
						goto IL_3E5;
					}
					this.op = 8;
					goto IL_3E5;
				default:
					if (c2 != '[')
					{
						goto IL_311;
					}
					this.ScanName(']', this.Escape, "]\\");
					this.CheckToken(Tokens.Name);
					goto IL_3E5;
				}
			}
			else
			{
				switch (c2)
				{
				case '^':
					this.token = Tokens.BinaryOp;
					this.op = 24;
					goto IL_3E5;
				case '_':
					goto IL_311;
				case '`':
					this.ScanName('`', '`', "`");
					this.CheckToken(Tokens.Name);
					goto IL_3E5;
				default:
					switch (c2)
					{
					case '|':
						this.token = Tokens.BinaryOp;
						this.op = 23;
						goto IL_3E5;
					case '}':
						goto IL_311;
					case '~':
						this.token = Tokens.BinaryOp;
						this.op = 25;
						goto IL_3E5;
					default:
						goto IL_311;
					}
					break;
				}
			}
			IL_104:
			this.token = Tokens.EOS;
			goto IL_3E5;
			IL_11C:
			this.token = Tokens.LeftParen;
			goto IL_3E5;
			IL_129:
			this.token = Tokens.RightParen;
			goto IL_3E5;
			IL_136:
			this.ScanDate();
			this.CheckToken(Tokens.Date);
			goto IL_3E5;
			IL_148:
			this.ScanString('\'');
			this.CheckToken(Tokens.StringConst);
			goto IL_3E5;
			IL_21A:
			this.token = Tokens.BinaryOp;
			this.op = 15;
			goto IL_3E5;
			IL_22F:
			this.token = Tokens.BinaryOp;
			this.op = 16;
			goto IL_3E5;
			IL_244:
			this.token = Tokens.BinaryOp;
			this.op = 17;
			goto IL_3E5;
			IL_259:
			this.token = Tokens.BinaryOp;
			this.op = 18;
			goto IL_3E5;
			IL_26E:
			this.token = Tokens.BinaryOp;
			this.op = 20;
			goto IL_3E5;
			IL_283:
			this.token = Tokens.BinaryOp;
			this.op = 22;
			goto IL_3E5;
			IL_311:
			if (c == this.ListSeparator)
			{
				this.token = Tokens.ListSeparator;
			}
			else if (c == '.')
			{
				if (this.prevOperand == 0)
				{
					this.ScanNumeric();
				}
				else
				{
					this.token = Tokens.Dot;
				}
			}
			else if (c == '0' && (array[this.pos] == 'x' || array[this.pos] == 'X'))
			{
				this.ScanBinaryConstant();
				this.token = Tokens.BinaryConst;
			}
			else if (this.IsDigit(c))
			{
				this.ScanNumeric();
			}
			else
			{
				this.ScanReserved();
				if (this.token == Tokens.None)
				{
					if (this.IsAlphaNumeric(c))
					{
						this.ScanName();
						if (this.token != Tokens.None)
						{
							this.CheckToken(Tokens.Name);
							goto IL_3E5;
						}
					}
					this.token = Tokens.Unknown;
					throw ExprException.UnknownToken(new string(array, this.start, this.pos - this.start), this.start + 1);
				}
			}
			IL_3E5:
			return this.token;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00255758 File Offset: 0x00254B58
		private void ScanNumeric()
		{
			char[] array = this.text;
			bool flag = false;
			bool flag2 = false;
			while (this.IsDigit(array[this.pos]))
			{
				this.pos++;
			}
			if (array[this.pos] == this.DecimalSeparator)
			{
				flag = true;
				this.pos++;
			}
			while (this.IsDigit(array[this.pos]))
			{
				this.pos++;
			}
			if (array[this.pos] == this.ExponentL || array[this.pos] == this.ExponentU)
			{
				flag2 = true;
				this.pos++;
				if (array[this.pos] == '-' || array[this.pos] == '+')
				{
					this.pos++;
				}
				while (this.IsDigit(array[this.pos]))
				{
					this.pos++;
				}
			}
			if (flag2)
			{
				this.token = Tokens.Float;
				return;
			}
			if (flag)
			{
				this.token = Tokens.Decimal;
				return;
			}
			this.token = Tokens.Numeric;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00255868 File Offset: 0x00254C68
		private void ScanName()
		{
			char[] array = this.text;
			while (this.IsAlphaNumeric(array[this.pos]))
			{
				this.pos++;
			}
			this.token = Tokens.Name;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x002558A8 File Offset: 0x00254CA8
		private void ScanName(char chEnd, char esc, string charsToEscape)
		{
			char[] array = this.text;
			do
			{
				if (array[this.pos] == esc && this.pos + 1 < array.Length && charsToEscape.IndexOf(array[this.pos + 1]) >= 0)
				{
					this.pos++;
				}
				this.pos++;
			}
			while (this.pos < array.Length && array[this.pos] != chEnd);
			if (this.pos >= array.Length)
			{
				throw ExprException.InvalidNameBracketing(new string(array, this.start, this.pos - 1 - this.start));
			}
			this.pos++;
			this.token = Tokens.Name;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00255968 File Offset: 0x00254D68
		private void ScanDate()
		{
			char[] array = this.text;
			do
			{
				this.pos++;
			}
			while (this.pos < array.Length && array[this.pos] != '#');
			if (this.pos < array.Length && array[this.pos] == '#')
			{
				this.token = Tokens.Date;
				this.pos++;
				return;
			}
			if (this.pos >= array.Length)
			{
				throw ExprException.InvalidDate(new string(array, this.start, this.pos - 1 - this.start));
			}
			throw ExprException.InvalidDate(new string(array, this.start, this.pos - this.start));
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00255A18 File Offset: 0x00254E18
		private void ScanBinaryConstant()
		{
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00255A28 File Offset: 0x00254E28
		private void ScanReserved()
		{
			char[] array = this.text;
			if (this.IsAlpha(array[this.pos]))
			{
				this.ScanName();
				string @string = new string(array, this.start, this.pos - this.start);
				CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
				int num = 0;
				int num2 = ExpressionParser.reservedwords.Length - 1;
				int num3;
				for (;;)
				{
					num3 = (num + num2) / 2;
					int num4 = compareInfo.Compare(ExpressionParser.reservedwords[num3].word, @string, CompareOptions.IgnoreCase);
					if (num4 == 0)
					{
						break;
					}
					if (num4 < 0)
					{
						num = num3 + 1;
					}
					else
					{
						num2 = num3 - 1;
					}
					if (num > num2)
					{
						return;
					}
				}
				this.token = ExpressionParser.reservedwords[num3].token;
				this.op = ExpressionParser.reservedwords[num3].op;
				return;
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00255B08 File Offset: 0x00254F08
		private void ScanString(char escape)
		{
			char[] array = this.text;
			while (this.pos < array.Length)
			{
				char c = array[this.pos++];
				if (c == escape && this.pos < array.Length && array[this.pos] == escape)
				{
					this.pos++;
				}
				else if (c == escape)
				{
					break;
				}
			}
			if (this.pos >= array.Length)
			{
				throw ExprException.InvalidString(new string(array, this.start, this.pos - 1 - this.start));
			}
			this.token = Tokens.StringConst;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00255BA8 File Offset: 0x00254FA8
		internal void ScanToken(Tokens token)
		{
			this.Scan();
			this.CheckToken(token);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00255BC8 File Offset: 0x00254FC8
		private void ScanWhite()
		{
			char[] array = this.text;
			while (this.pos < array.Length && this.IsWhiteSpace(array[this.pos]))
			{
				this.pos++;
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00255C08 File Offset: 0x00255008
		private bool IsWhiteSpace(char ch)
		{
			return ch <= ' ' && ch != '\0';
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00255C28 File Offset: 0x00255028
		private bool IsAlphaNumeric(char ch)
		{
			switch (ch)
			{
			case '$':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return ch > '\u007f';
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00255DA8 File Offset: 0x002551A8
		private bool IsDigit(char ch)
		{
			switch (ch)
			{
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00255DF8 File Offset: 0x002551F8
		private bool IsAlpha(char ch)
		{
			switch (ch)
			{
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return false;
		}

		// Token: 0x04000D81 RID: 3457
		private const int Empty = 0;

		// Token: 0x04000D82 RID: 3458
		private const int Scalar = 1;

		// Token: 0x04000D83 RID: 3459
		private const int Expr = 2;

		// Token: 0x04000D84 RID: 3460
		private const int MaxPredicates = 100;

		// Token: 0x04000D85 RID: 3461
		private static readonly ExpressionParser.ReservedWords[] reservedwords = new ExpressionParser.ReservedWords[]
		{
			new ExpressionParser.ReservedWords("And", Tokens.BinaryOp, 26),
			new ExpressionParser.ReservedWords("Between", Tokens.BinaryOp, 6),
			new ExpressionParser.ReservedWords("Child", Tokens.Child, 0),
			new ExpressionParser.ReservedWords("False", Tokens.ZeroOp, 34),
			new ExpressionParser.ReservedWords("In", Tokens.BinaryOp, 5),
			new ExpressionParser.ReservedWords("Is", Tokens.BinaryOp, 13),
			new ExpressionParser.ReservedWords("Like", Tokens.BinaryOp, 14),
			new ExpressionParser.ReservedWords("Not", Tokens.UnaryOp, 3),
			new ExpressionParser.ReservedWords("Null", Tokens.ZeroOp, 32),
			new ExpressionParser.ReservedWords("Or", Tokens.BinaryOp, 27),
			new ExpressionParser.ReservedWords("Parent", Tokens.Parent, 0),
			new ExpressionParser.ReservedWords("True", Tokens.ZeroOp, 33)
		};

		// Token: 0x04000D86 RID: 3462
		private char Escape = '\\';

		// Token: 0x04000D87 RID: 3463
		private char DecimalSeparator = '.';

		// Token: 0x04000D88 RID: 3464
		private char ListSeparator = ',';

		// Token: 0x04000D89 RID: 3465
		private char ExponentL = 'e';

		// Token: 0x04000D8A RID: 3466
		private char ExponentU = 'E';

		// Token: 0x04000D8B RID: 3467
		internal char[] text;

		// Token: 0x04000D8C RID: 3468
		internal int pos;

		// Token: 0x04000D8D RID: 3469
		internal int start;

		// Token: 0x04000D8E RID: 3470
		internal Tokens token;

		// Token: 0x04000D8F RID: 3471
		internal int op;

		// Token: 0x04000D90 RID: 3472
		internal OperatorInfo[] ops = new OperatorInfo[100];

		// Token: 0x04000D91 RID: 3473
		internal int topOperator;

		// Token: 0x04000D92 RID: 3474
		internal int topNode;

		// Token: 0x04000D93 RID: 3475
		private readonly DataTable _table;

		// Token: 0x04000D94 RID: 3476
		internal ExpressionNode[] NodeStack = new ExpressionNode[100];

		// Token: 0x04000D95 RID: 3477
		internal int prevOperand;

		// Token: 0x04000D96 RID: 3478
		internal ExpressionNode expression;

		// Token: 0x020001AB RID: 427
		private struct ReservedWords
		{
			// Token: 0x060018B2 RID: 6322 RVA: 0x00256068 File Offset: 0x00255468
			internal ReservedWords(string word, Tokens token, int op)
			{
				this.word = word;
				this.token = token;
				this.op = op;
			}

			// Token: 0x04000D97 RID: 3479
			internal readonly string word;

			// Token: 0x04000D98 RID: 3480
			internal readonly Tokens token;

			// Token: 0x04000D99 RID: 3481
			internal readonly int op;
		}
	}
}

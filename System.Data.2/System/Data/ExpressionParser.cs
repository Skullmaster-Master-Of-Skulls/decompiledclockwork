using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000ED RID: 237
	internal sealed class ExpressionParser
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0007CA54 File Offset: 0x0007BE54
		internal ExpressionParser(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0007CAB0 File Offset: 0x0007BEB0
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

		// Token: 0x06000F85 RID: 3973 RVA: 0x0007CB10 File Offset: 0x0007BF10
		internal void StartScan()
		{
			this.op = 0;
			this.pos = 0;
			this.start = 0;
			this.topOperator = 0;
			OperatorInfo[] array = this.ops;
			int num = this.topOperator;
			this.topOperator = num + 1;
			array[num] = new OperatorInfo(Nodes.Noop, 0, 0);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0007CB5C File Offset: 0x0007BF5C
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
					int num2;
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
								OperatorInfo[] array = this.ops;
								num2 = this.topOperator;
								this.topOperator = num2 + 1;
								array[num2] = new OperatorInfo(Nodes.Call, 0, 2);
								continue;
							}
							OperatorInfo[] array2 = this.ops;
							num2 = this.topOperator;
							this.topOperator = num2 + 1;
							array2[num2] = new OperatorInfo(Nodes.Paren, 0, 2);
							continue;
						}
						else
						{
							this.BuildExpression(22);
							this.prevOperand = 0;
							ExpressionNode expressionNode2 = this.NodePeek();
							if (expressionNode2 == null || expressionNode2.GetType() != typeof(NameNode))
							{
								goto IL_40F;
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
							OperatorInfo[] array3 = this.ops;
							num2 = this.topOperator;
							this.topOperator = num2 + 1;
							array3[num2] = new OperatorInfo(Nodes.Call, 0, 2);
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
					{
						if (this.prevOperand != 0)
						{
							goto Block_28;
						}
						OperatorInfo[] array4 = this.ops;
						num2 = this.topOperator;
						this.topOperator = num2 + 1;
						array4[num2] = new OperatorInfo(Nodes.Zop, this.op, 24);
						this.prevOperand = 2;
						continue;
					}
					case Tokens.UnaryOp:
						goto IL_645;
					case Tokens.BinaryOp:
						if (this.prevOperand != 0)
						{
							this.prevOperand = 0;
							this.BuildExpression(Operators.Priority(this.op));
							OperatorInfo[] array5 = this.ops;
							num2 = this.topOperator;
							this.topOperator = num2 + 1;
							array5[num2] = new OperatorInfo(Nodes.Binop, this.op, Operators.Priority(this.op));
							continue;
						}
						if (this.op == 15)
						{
							this.op = 2;
							goto IL_645;
						}
						if (this.op == 16)
						{
							this.op = 1;
							goto IL_645;
						}
						goto IL_5E8;
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
						goto IL_7A;
					}
					goto Block_1;
					IL_645:
					OperatorInfo[] array6 = this.ops;
					num2 = this.topOperator;
					this.topOperator = num2 + 1;
					array6[num2] = new OperatorInfo(Nodes.Unop, this.op, Operators.Priority(this.op));
				}
				IL_7A:
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
				goto IL_75D;
				Block_5:
				throw ExprException.MissingOperator(new string(this.text, this.start, this.pos - this.start));
				Block_9:
				throw ExprException.InWithoutParentheses();
				IL_40F:
				throw ExprException.SyntaxError();
				Block_18:
				throw ExprException.TooManyRightParentheses();
				Block_20:
				throw ExprException.MissingOperand(operatorInfo);
				Block_23:
				throw ExprException.MissingOperandBefore(",");
				Block_24:
				throw ExprException.SyntaxError();
				IL_5E8:
				throw ExprException.MissingOperandBefore(Operators.ToString(this.op));
				Block_28:
				throw ExprException.MissingOperator(new string(this.text, this.start, this.pos - this.start));
				IL_75D:
				throw ExprException.UnknownToken(new string(this.text, this.start, this.pos - this.start), this.start + 1);
			}
			this.expression = this.NodeStack[0];
			return this.expression;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0007D330 File Offset: 0x0007C730
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

		// Token: 0x06000F88 RID: 3976 RVA: 0x0007D45C File Offset: 0x0007C85C
		private ExpressionNode NodePop()
		{
			ExpressionNode[] nodeStack = this.NodeStack;
			int num = this.topNode - 1;
			this.topNode = num;
			return nodeStack[num];
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0007D484 File Offset: 0x0007C884
		private ExpressionNode NodePeek()
		{
			if (this.topNode <= 0)
			{
				return null;
			}
			return this.NodeStack[this.topNode - 1];
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0007D4AC File Offset: 0x0007C8AC
		private void NodePush(ExpressionNode node)
		{
			if (this.topNode >= 98)
			{
				throw ExprException.ExpressionTooComplex();
			}
			ExpressionNode[] nodeStack = this.NodeStack;
			int num = this.topNode;
			this.topNode = num + 1;
			nodeStack[num] = node;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0007D4E4 File Offset: 0x0007C8E4
		private void BuildExpression(int pri)
		{
			OperatorInfo operatorInfo;
			for (;;)
			{
				operatorInfo = this.ops[this.topOperator - 1];
				if (operatorInfo.priority < pri)
				{
					return;
				}
				this.topOperator--;
				ExpressionNode node;
				switch (operatorInfo.type)
				{
				case Nodes.Unop:
				{
					ExpressionNode right = this.NodePop();
					int num = operatorInfo.op;
					if (num != 1 && num != 3 && num == 25)
					{
						goto Block_6;
					}
					node = new UnaryNode(this._table, operatorInfo.op, right);
					goto IL_163;
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
						goto IL_D4;
					}
					if (operatorInfo.op == 14)
					{
						node = new LikeNode(this._table, operatorInfo.op, left, right);
						goto IL_163;
					}
					node = new BinaryNode(this._table, operatorInfo.op, left, right);
					goto IL_163;
				}
				case Nodes.Zop:
					node = new ZeroOpNode(operatorInfo.op);
					goto IL_163;
				}
				break;
				IL_163:
				this.NodePush(node);
			}
			return;
			IL_D4:
			throw ExprException.UnsupportedOperator(operatorInfo.op);
			Block_6:
			throw ExprException.UnsupportedOperator(operatorInfo.op);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0007D660 File Offset: 0x0007CA60
		internal void CheckToken(Tokens token)
		{
			if (this.token != token)
			{
				throw ExprException.UnknownToken(token, this.token, this.pos);
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0007D68C File Offset: 0x0007CA8C
		internal Tokens Scan()
		{
			char[] array = this.text;
			this.token = Tokens.None;
			char c;
			for (;;)
			{
				this.start = this.pos;
				this.op = 0;
				char[] array2 = array;
				int num = this.pos;
				this.pos = num + 1;
				c = array2[num];
				if (c > '>')
				{
					goto IL_CD;
				}
				if (c > '\r')
				{
					switch (c)
					{
					case ' ':
						goto IL_111;
					case '!':
					case '"':
					case '$':
					case ',':
					case '.':
						goto IL_311;
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
					goto Block_5;
				}
				if (c != '\0')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_111;
					}
					break;
				}
				goto IL_104;
				IL_111:
				this.ScanWhite();
			}
			goto IL_311;
			Block_5:
			switch (c)
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
				goto IL_311;
			}
			IL_CD:
			if (c <= '^')
			{
				if (c == '[')
				{
					this.ScanName(']', this.Escape, "]\\");
					this.CheckToken(Tokens.Name);
					goto IL_3E5;
				}
				if (c != '^')
				{
					goto IL_311;
				}
				this.token = Tokens.BinaryOp;
				this.op = 24;
				goto IL_3E5;
			}
			else
			{
				if (c == '`')
				{
					this.ScanName('`', '`', "`");
					this.CheckToken(Tokens.Name);
					goto IL_3E5;
				}
				if (c == '|')
				{
					this.token = Tokens.BinaryOp;
					this.op = 23;
					goto IL_3E5;
				}
				if (c != '~')
				{
					goto IL_311;
				}
				this.token = Tokens.BinaryOp;
				this.op = 25;
				goto IL_3E5;
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

		// Token: 0x06000F8E RID: 3982 RVA: 0x0007DA84 File Offset: 0x0007CE84
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

		// Token: 0x06000F8F RID: 3983 RVA: 0x0007DB90 File Offset: 0x0007CF90
		private void ScanName()
		{
			char[] array = this.text;
			while (this.IsAlphaNumeric(array[this.pos]))
			{
				this.pos++;
			}
			this.token = Tokens.Name;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0007DBCC File Offset: 0x0007CFCC
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

		// Token: 0x06000F91 RID: 3985 RVA: 0x0007DC80 File Offset: 0x0007D080
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

		// Token: 0x06000F92 RID: 3986 RVA: 0x0007DD30 File Offset: 0x0007D130
		private void ScanBinaryConstant()
		{
			char[] array = this.text;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0007DD44 File Offset: 0x0007D144
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

		// Token: 0x06000F94 RID: 3988 RVA: 0x0007DE0C File Offset: 0x0007D20C
		private void ScanString(char escape)
		{
			char[] array = this.text;
			while (this.pos < array.Length)
			{
				char[] array2 = array;
				int num = this.pos;
				this.pos = num + 1;
				char c = array2[num];
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

		// Token: 0x06000F95 RID: 3989 RVA: 0x0007DEA4 File Offset: 0x0007D2A4
		internal void ScanToken(Tokens token)
		{
			this.Scan();
			this.CheckToken(token);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0007DEC0 File Offset: 0x0007D2C0
		private void ScanWhite()
		{
			char[] array = this.text;
			while (this.pos < array.Length && this.IsWhiteSpace(array[this.pos]))
			{
				this.pos++;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0007DF00 File Offset: 0x0007D300
		private bool IsWhiteSpace(char ch)
		{
			return ch <= ' ' && ch > '\0';
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0007DF18 File Offset: 0x0007D318
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

		// Token: 0x06000F99 RID: 3993 RVA: 0x0007E098 File Offset: 0x0007D498
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

		// Token: 0x06000F9A RID: 3994 RVA: 0x0007E0DC File Offset: 0x0007D4DC
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

		// Token: 0x040004B3 RID: 1203
		private const int Empty = 0;

		// Token: 0x040004B4 RID: 1204
		private const int Scalar = 1;

		// Token: 0x040004B5 RID: 1205
		private const int Expr = 2;

		// Token: 0x040004B6 RID: 1206
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

		// Token: 0x040004B7 RID: 1207
		private char Escape = '\\';

		// Token: 0x040004B8 RID: 1208
		private char DecimalSeparator = '.';

		// Token: 0x040004B9 RID: 1209
		private char ListSeparator = ',';

		// Token: 0x040004BA RID: 1210
		private char ExponentL = 'e';

		// Token: 0x040004BB RID: 1211
		private char ExponentU = 'E';

		// Token: 0x040004BC RID: 1212
		internal char[] text;

		// Token: 0x040004BD RID: 1213
		internal int pos;

		// Token: 0x040004BE RID: 1214
		internal int start;

		// Token: 0x040004BF RID: 1215
		internal Tokens token;

		// Token: 0x040004C0 RID: 1216
		internal int op;

		// Token: 0x040004C1 RID: 1217
		internal OperatorInfo[] ops = new OperatorInfo[100];

		// Token: 0x040004C2 RID: 1218
		internal int topOperator;

		// Token: 0x040004C3 RID: 1219
		internal int topNode;

		// Token: 0x040004C4 RID: 1220
		private readonly DataTable _table;

		// Token: 0x040004C5 RID: 1221
		private const int MaxPredicates = 100;

		// Token: 0x040004C6 RID: 1222
		internal ExpressionNode[] NodeStack = new ExpressionNode[100];

		// Token: 0x040004C7 RID: 1223
		internal int prevOperand;

		// Token: 0x040004C8 RID: 1224
		internal ExpressionNode expression;

		// Token: 0x02000351 RID: 849
		private struct ReservedWords
		{
			// Token: 0x0600340E RID: 13326 RVA: 0x0013FFF8 File Offset: 0x0013F3F8
			internal ReservedWords(string word, Tokens token, int op)
			{
				this.word = word;
				this.token = token;
				this.op = op;
			}

			// Token: 0x04001EE2 RID: 7906
			internal readonly string word;

			// Token: 0x04001EE3 RID: 7907
			internal readonly Tokens token;

			// Token: 0x04001EE4 RID: 7908
			internal readonly int op;
		}
	}
}

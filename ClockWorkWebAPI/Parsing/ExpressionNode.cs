using System;

namespace ClockWorkWebAPI.Parsing
{
	// Token: 0x0200004A RID: 74
	public class ExpressionNode
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x00019D59 File Offset: 0x00017F59
		public ExpressionNode(string new_expression)
		{
			this.Expression = new_expression;
			this.LeftChild = null;
			this.RightChild = null;
			this.ParseExpression();
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00019D80 File Offset: 0x00017F80
		public string Text
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.Op);
				string result;
				if (flag)
				{
					result = this.Expression;
				}
				else
				{
					result = this.Op;
				}
				return result;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00019DB0 File Offset: 0x00017FB0
		public string ShowStack(int indent)
		{
			string text = "";
			bool flag = string.IsNullOrEmpty(this.Op);
			if (flag)
			{
				text = new string(' ', indent) + this.Expression;
			}
			else
			{
				text = text + new string(' ', indent) + this.Op;
				bool flag2 = this.LeftChild != null;
				if (flag2)
				{
					text = text + Environment.NewLine + this.LeftChild.ShowStack(indent + 2);
				}
				bool flag3 = this.RightChild != null;
				if (flag3)
				{
					text = text + Environment.NewLine + this.RightChild.ShowStack(indent + 2);
				}
			}
			return text;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00019E58 File Offset: 0x00018058
		public string RebuildExpression()
		{
			bool flag = this.Op == "( )";
			string result;
			if (flag)
			{
				result = "(" + this.LeftChild.RebuildExpression() + ")";
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(this.Op);
				if (flag2)
				{
					result = this.Expression;
				}
				else
				{
					bool flag3 = this.RightChild == null;
					if (flag3)
					{
						result = this.Op + this.LeftChild.RebuildExpression();
					}
					else
					{
						result = this.LeftChild.RebuildExpression() + this.Op + this.RightChild.RebuildExpression();
					}
				}
			}
			return result;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00019F0C File Offset: 0x0001810C
		private void ParseExpression()
		{
			bool flag = this.Expression.Length == 0;
			if (flag)
			{
				this.Expression = "0";
			}
			else
			{
				bool flag2 = true;
				ExpressionNode.Precedence precedence = ExpressionNode.Precedence.None;
				int num = 0;
				int num2 = -1;
				for (int i = 0; i <= this.Expression.Length - 1; i++)
				{
					bool flag3 = false;
					string text = this.Expression.Substring(i, 1);
					bool flag4 = text == " ";
					if (!flag4)
					{
						bool flag5 = text == "(";
						if (flag5)
						{
							num++;
							flag3 = true;
						}
						else
						{
							bool flag6 = text == ")";
							if (flag6)
							{
								num--;
								flag3 = false;
								bool flag7 = num < 0;
								if (flag7)
								{
									throw new Exception("Parse error. Too many )s in expression '" + this.Expression + "'");
								}
							}
							else
							{
								bool flag8 = num == 0;
								if (flag8)
								{
									bool flag9 = text == "^" || text == "*" || text == "/" || text == "\\" || text == "%" || text == "+" || text == "-";
									if (flag9)
									{
										flag3 = true;
										string text2 = text;
										string text3 = text2;
										uint num3 = <PrivateImplementationDetails>.ComputeStringHash(text3);
										if (num3 <= 705468254U)
										{
											if (num3 != 537692064U)
											{
												if (num3 != 671913016U)
												{
													if (num3 == 705468254U)
													{
														if (text3 == "/")
														{
															goto IL_264;
														}
													}
												}
												else if (text3 == "-")
												{
													goto IL_2A6;
												}
											}
											else if (text3 == "%")
											{
												bool flag10 = precedence >= ExpressionNode.Precedence.Modulus;
												if (flag10)
												{
													precedence = ExpressionNode.Precedence.Modulus;
													num2 = i;
												}
											}
										}
										else if (num3 <= 789356349U)
										{
											if (num3 != 772578730U)
											{
												if (num3 == 789356349U)
												{
													if (text3 == "*")
													{
														goto IL_264;
													}
												}
											}
											else if (text3 == "+")
											{
												goto IL_2A6;
											}
										}
										else if (num3 != 3641448411U)
										{
											if (num3 == 3675003649U)
											{
												if (text3 == "^")
												{
													bool flag11 = precedence >= ExpressionNode.Precedence.Power;
													if (flag11)
													{
														precedence = ExpressionNode.Precedence.Power;
														num2 = i;
													}
												}
											}
										}
										else if (text3 == "\\")
										{
											bool flag12 = precedence >= ExpressionNode.Precedence.IntDiv;
											if (flag12)
											{
												precedence = ExpressionNode.Precedence.IntDiv;
												num2 = i;
											}
										}
										IL_2C2:
										goto IL_2C3;
										IL_264:
										bool flag13 = precedence >= ExpressionNode.Precedence.Times;
										if (flag13)
										{
											precedence = ExpressionNode.Precedence.Times;
											num2 = i;
										}
										goto IL_2C2;
										IL_2A6:
										bool flag14 = !flag2 && precedence >= ExpressionNode.Precedence.Plus;
										if (flag14)
										{
											precedence = ExpressionNode.Precedence.Plus;
											num2 = i;
										}
									}
									IL_2C3:;
								}
							}
						}
					}
					flag2 = flag3;
				}
				bool flag15 = num != 0;
				if (flag15)
				{
					throw new Exception("Parse error. Missing ) in expression '" + this.Expression + "'");
				}
				bool flag16 = precedence < ExpressionNode.Precedence.None;
				if (flag16)
				{
					string new_expression = this.Expression.Substring(0, num2);
					string new_expression2 = this.Expression.Substring(num2 + 1);
					this.Op = this.Expression.Substring(num2, 1);
					this.LeftChild = new ExpressionNode(new_expression);
					this.RightChild = new ExpressionNode(new_expression2);
				}
				else
				{
					bool flag17 = this.Expression.StartsWith("(") && this.Expression.EndsWith(")");
					if (flag17)
					{
						this.Op = "( )";
						this.LeftChild = new ExpressionNode(this.Expression.Substring(1, this.Expression.Length - 2));
					}
					else
					{
						bool flag18 = this.Expression.StartsWith("-");
						if (flag18)
						{
							this.Op = "-";
							this.LeftChild = new ExpressionNode(this.Expression.Substring(1, this.Expression.Length - 1));
						}
						else
						{
							bool flag19 = this.Expression.StartsWith("+");
							if (flag19)
							{
								this.Op = "+";
								this.LeftChild = new ExpressionNode(this.Expression.Substring(1, this.Expression.Length - 1));
							}
							else
							{
								bool flag20 = this.Expression.Length > 5 && this.Expression.EndsWith(")");
								if (flag20)
								{
									int num4 = this.Expression.IndexOf("(");
									bool flag21 = num4 > 0;
									if (flag21)
									{
										this.Op = this.Expression.Substring(0, num4);
										string new_expression3 = this.Expression.Substring(num4 + 1, this.Expression.Length - num4 - 2);
										this.LeftChild = new ExpressionNode(new_expression3);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001A40C File Offset: 0x0001860C
		public bool Evaluate(int[] cids)
		{
			bool flag = string.IsNullOrEmpty(this.Op);
			if (!flag)
			{
				string text = this.Op.ToLower();
				string text2 = text;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 789356349U)
				{
					if (num <= 671913016U)
					{
						if (num != 101484020U)
						{
							if (num != 537692064U)
							{
								if (num == 671913016U)
								{
									if (text2 == "-")
									{
										return Array.IndexOf<int>(cids, int.Parse(this.Expression)) < 0;
									}
								}
							}
							else if (text2 == "%")
							{
								return false;
							}
						}
						else if (text2 == "( )")
						{
							return this.LeftChild.Evaluate(cids);
						}
					}
					else if (num != 705468254U)
					{
						if (num != 772578730U)
						{
							if (num == 789356349U)
							{
								if (text2 == "*")
								{
									return this.LeftChild.Evaluate(cids) && this.RightChild.Evaluate(cids);
								}
							}
						}
						else if (text2 == "+")
						{
							return false;
						}
					}
					else if (text2 == "/")
					{
						return this.LeftChild.Evaluate(cids) || this.RightChild.Evaluate(cids);
					}
				}
				else if (num <= 3561304609U)
				{
					if (num != 2633446552U)
					{
						if (num != 2974667336U)
						{
							if (num == 3561304609U)
							{
								if (text2 == "sqr")
								{
									return false;
								}
							}
						}
						else if (text2 == "factorial")
						{
							return false;
						}
					}
					else if (text2 == "tan")
					{
						return false;
					}
				}
				else if (num <= 3675003649U)
				{
					if (num != 3641448411U)
					{
						if (num == 3675003649U)
						{
							if (text2 == "^")
							{
								return false;
							}
						}
					}
					else if (text2 == "\\")
					{
						return false;
					}
				}
				else if (num != 3761252941U)
				{
					if (num == 4220379804U)
					{
						if (text2 == "cos")
						{
							return false;
						}
					}
				}
				else if (text2 == "sin")
				{
					return false;
				}
				throw new Exception("Evaluate error. Unknown function '" + this.Op + "'");
			}
			return Array.IndexOf<int>(cids, int.Parse(this.Expression)) >= 0;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001A700 File Offset: 0x00018900
		private double Factorial(double N)
		{
			bool flag = (double)((long)N) != N;
			if (flag)
			{
				throw new Exception("Factorial error. Argument " + N.ToString() + " must be an integer");
			}
			double num = 1.0;
			while (N > 1.0)
			{
				num *= N;
				N -= 1.0;
			}
			return num;
		}

		// Token: 0x040001D5 RID: 469
		public string Expression;

		// Token: 0x040001D6 RID: 470
		public ExpressionNode LeftChild;

		// Token: 0x040001D7 RID: 471
		public ExpressionNode RightChild;

		// Token: 0x040001D8 RID: 472
		public string Op;

		// Token: 0x020000A5 RID: 165
		private enum Precedence
		{
			// Token: 0x040003C1 RID: 961
			None = 11,
			// Token: 0x040003C2 RID: 962
			Unary = 10,
			// Token: 0x040003C3 RID: 963
			Power = 9,
			// Token: 0x040003C4 RID: 964
			Times = 8,
			// Token: 0x040003C5 RID: 965
			Div = 7,
			// Token: 0x040003C6 RID: 966
			IntDiv = 6,
			// Token: 0x040003C7 RID: 967
			Modulus = 5,
			// Token: 0x040003C8 RID: 968
			Plus = 4
		}
	}
}

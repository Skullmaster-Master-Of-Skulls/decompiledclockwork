using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Resources;

namespace System.Web.Query.Dynamic
{
	// Token: 0x0200003F RID: 63
	internal class ExpressionParser
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public ExpressionParser(ParameterExpression[] parameters, string expression, object[] values)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (ExpressionParser.keywords == null)
			{
				ExpressionParser.keywords = ExpressionParser.CreateKeywords();
			}
			this.symbols = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.literals = new Dictionary<Expression, string>();
			if (parameters != null)
			{
				this.ProcessParameters(parameters);
			}
			if (values != null)
			{
				this.ProcessValues(values);
			}
			this.text = expression;
			this.textLen = this.text.Length;
			this.SetTextPos(0);
			this.NextToken();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E27C File Offset: 0x0000C47C
		private void ProcessParameters(ParameterExpression[] parameters)
		{
			foreach (ParameterExpression parameterExpression in parameters)
			{
				if (!string.IsNullOrEmpty(parameterExpression.Name))
				{
					this.AddSymbol(parameterExpression.Name, parameterExpression);
				}
			}
			if (parameters.Length == 1 && string.IsNullOrEmpty(parameters[0].Name))
			{
				this.it = parameters[0];
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		private void ProcessValues(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				object obj = values[i];
				if (i == values.Length - 1 && obj is IDictionary<string, object>)
				{
					this.externals = (IDictionary<string, object>)obj;
				}
				else
				{
					this.AddSymbol("@" + i.ToString(CultureInfo.InvariantCulture), obj);
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000E332 File Offset: 0x0000C532
		private void AddSymbol(string name, object value)
		{
			if (this.symbols.ContainsKey(name))
			{
				throw this.ParseError(AtlasWeb.ExpressionParser_DuplicateIdentifier, new object[]
				{
					name
				});
			}
			this.symbols.Add(name, value);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000E368 File Offset: 0x0000C568
		public Expression Parse(Type resultType)
		{
			int pos = this.token.pos;
			Expression expression = this.ParseExpression();
			if (resultType != null && (expression = this.PromoteExpression(expression, resultType, true)) == null)
			{
				throw this.ParseError(pos, AtlasWeb.ExpressionParser_ExpressionTypeMismatch, new object[]
				{
					ExpressionParser.GetTypeName(resultType)
				});
			}
			this.ValidateToken(ExpressionParser.TokenId.End, AtlasWeb.ExpressionParser_SyntaxError);
			return expression;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		public IEnumerable<DynamicOrdering> ParseOrdering()
		{
			List<DynamicOrdering> list = new List<DynamicOrdering>();
			for (;;)
			{
				Expression selector = this.ParseExpression();
				bool ascending = true;
				if (this.TokenIdentifierIs("asc") || this.TokenIdentifierIs("ascending"))
				{
					this.NextToken();
				}
				else if (this.TokenIdentifierIs("desc") || this.TokenIdentifierIs("descending"))
				{
					this.NextToken();
					ascending = false;
				}
				list.Add(new DynamicOrdering
				{
					Selector = selector,
					Ascending = ascending
				});
				if (this.token.id != ExpressionParser.TokenId.Comma)
				{
					break;
				}
				this.NextToken();
			}
			this.ValidateToken(ExpressionParser.TokenId.End, AtlasWeb.ExpressionParser_SyntaxError);
			return list;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000E468 File Offset: 0x0000C668
		private Expression ParseExpression()
		{
			int pos = this.token.pos;
			Expression expression = this.ParseLogicalOr();
			if (this.token.id == ExpressionParser.TokenId.Question)
			{
				this.NextToken();
				Expression expr = this.ParseExpression();
				this.ValidateToken(ExpressionParser.TokenId.Colon, AtlasWeb.ExpressionParser_ColonExpected);
				this.NextToken();
				Expression expr2 = this.ParseExpression();
				expression = this.GenerateConditional(expression, expr, expr2, pos);
			}
			return expression;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		private Expression ParseLogicalOr()
		{
			Expression expression = this.ParseLogicalAnd();
			while (this.token.id == ExpressionParser.TokenId.DoubleBar || this.TokenIdentifierIs("or"))
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseLogicalAnd();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.ILogicalSignatures), token.text, ref expression, ref right, token.pos);
				expression = Expression.OrElse(expression, right);
			}
			return expression;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E53C File Offset: 0x0000C73C
		private Expression ParseLogicalAnd()
		{
			Expression expression = this.ParseComparison();
			while (this.token.id == ExpressionParser.TokenId.DoubleAmphersand || this.TokenIdentifierIs("and"))
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseComparison();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.ILogicalSignatures), token.text, ref expression, ref right, token.pos);
				expression = Expression.AndAlso(expression, right);
			}
			return expression;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		private Expression ParseComparison()
		{
			Expression expression = this.ParseAdditive();
			while (this.token.id == ExpressionParser.TokenId.Equal || this.token.id == ExpressionParser.TokenId.DoubleEqual || this.token.id == ExpressionParser.TokenId.ExclamationEqual || this.token.id == ExpressionParser.TokenId.LessGreater || this.token.id == ExpressionParser.TokenId.GreaterThan || this.token.id == ExpressionParser.TokenId.GreaterThanEqual || this.token.id == ExpressionParser.TokenId.LessThan || this.token.id == ExpressionParser.TokenId.LessThanEqual)
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseAdditive();
				bool flag = token.id == ExpressionParser.TokenId.Equal || token.id == ExpressionParser.TokenId.DoubleEqual || token.id == ExpressionParser.TokenId.ExclamationEqual || token.id == ExpressionParser.TokenId.LessGreater;
				if (flag && !expression.Type.IsValueType && !expression2.Type.IsValueType)
				{
					if (expression.Type != expression2.Type)
					{
						if (expression.Type.IsAssignableFrom(expression2.Type))
						{
							expression2 = Expression.Convert(expression2, expression.Type);
						}
						else
						{
							if (!expression2.Type.IsAssignableFrom(expression.Type))
							{
								throw this.IncompatibleOperandsError(token.text, expression, expression2, token.pos);
							}
							expression = Expression.Convert(expression, expression2.Type);
						}
					}
				}
				else if (ExpressionParser.IsEnumType(expression.Type) || ExpressionParser.IsEnumType(expression2.Type))
				{
					if (expression.Type != expression2.Type)
					{
						Expression expression3;
						if ((expression3 = this.PromoteExpression(expression2, expression.Type, true)) != null)
						{
							expression2 = expression3;
						}
						else
						{
							if ((expression3 = this.PromoteExpression(expression, expression2.Type, true)) == null)
							{
								throw this.IncompatibleOperandsError(token.text, expression, expression2, token.pos);
							}
							expression = expression3;
						}
					}
				}
				else
				{
					this.CheckAndPromoteOperands(flag ? typeof(ExpressionParser.IEqualitySignatures) : typeof(ExpressionParser.IRelationalSignatures), token.text, ref expression, ref expression2, token.pos);
				}
				switch (token.id)
				{
				case ExpressionParser.TokenId.LessThan:
					expression = this.GenerateLessThan(expression, expression2);
					break;
				case ExpressionParser.TokenId.Equal:
				case ExpressionParser.TokenId.DoubleEqual:
					expression = this.GenerateEqual(expression, expression2);
					break;
				case ExpressionParser.TokenId.GreaterThan:
					expression = this.GenerateGreaterThan(expression, expression2);
					break;
				case ExpressionParser.TokenId.ExclamationEqual:
				case ExpressionParser.TokenId.LessGreater:
					expression = this.GenerateNotEqual(expression, expression2);
					break;
				case ExpressionParser.TokenId.LessThanEqual:
					expression = this.GenerateLessThanEqual(expression, expression2);
					break;
				case ExpressionParser.TokenId.GreaterThanEqual:
					expression = this.GenerateGreaterThanEqual(expression, expression2);
					break;
				}
			}
			return expression;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E858 File Offset: 0x0000CA58
		private Expression ParseAdditive()
		{
			Expression expression = this.ParseMultiplicative();
			while (this.token.id == ExpressionParser.TokenId.Plus || this.token.id == ExpressionParser.TokenId.Minus || this.token.id == ExpressionParser.TokenId.Amphersand)
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseMultiplicative();
				ExpressionParser.TokenId id = token.id;
				if (id != ExpressionParser.TokenId.Amphersand)
				{
					if (id != ExpressionParser.TokenId.Plus)
					{
						if (id != ExpressionParser.TokenId.Minus)
						{
							continue;
						}
						this.CheckAndPromoteOperands(typeof(ExpressionParser.ISubtractSignatures), token.text, ref expression, ref expression2, token.pos);
						expression = this.GenerateSubtract(expression, expression2);
						continue;
					}
					else if (!(expression.Type == typeof(string)) && !(expression2.Type == typeof(string)))
					{
						this.CheckAndPromoteOperands(typeof(ExpressionParser.IAddSignatures), token.text, ref expression, ref expression2, token.pos);
						expression = this.GenerateAdd(expression, expression2);
						continue;
					}
				}
				expression = this.GenerateStringConcat(expression, expression2);
			}
			return expression;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E968 File Offset: 0x0000CB68
		private Expression ParseMultiplicative()
		{
			Expression expression = this.ParseUnary();
			while (this.token.id == ExpressionParser.TokenId.Asterisk || this.token.id == ExpressionParser.TokenId.Slash || this.token.id == ExpressionParser.TokenId.Percent || this.TokenIdentifierIs("mod"))
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseUnary();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.IArithmeticSignatures), token.text, ref expression, ref right, token.pos);
				ExpressionParser.TokenId id = token.id;
				if (id <= ExpressionParser.TokenId.Percent)
				{
					if (id == ExpressionParser.TokenId.Identifier || id == ExpressionParser.TokenId.Percent)
					{
						expression = Expression.Modulo(expression, right);
					}
				}
				else if (id != ExpressionParser.TokenId.Asterisk)
				{
					if (id == ExpressionParser.TokenId.Slash)
					{
						expression = Expression.Divide(expression, right);
					}
				}
				else
				{
					expression = Expression.Multiply(expression, right);
				}
			}
			return expression;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EA34 File Offset: 0x0000CC34
		private Expression ParseUnary()
		{
			if (this.token.id != ExpressionParser.TokenId.Minus && this.token.id != ExpressionParser.TokenId.Exclamation && !this.TokenIdentifierIs("not"))
			{
				return this.ParsePrimary();
			}
			ExpressionParser.Token token = this.token;
			this.NextToken();
			if (token.id == ExpressionParser.TokenId.Minus && (this.token.id == ExpressionParser.TokenId.IntegerLiteral || this.token.id == ExpressionParser.TokenId.RealLiteral))
			{
				this.token.text = "-" + this.token.text;
				this.token.pos = token.pos;
				return this.ParsePrimary();
			}
			Expression expression = this.ParseUnary();
			if (token.id == ExpressionParser.TokenId.Minus)
			{
				this.CheckAndPromoteOperand(typeof(ExpressionParser.INegationSignatures), token.text, ref expression, token.pos);
				expression = Expression.Negate(expression);
			}
			else
			{
				this.CheckAndPromoteOperand(typeof(ExpressionParser.INotSignatures), token.text, ref expression, token.pos);
				expression = Expression.Not(expression);
			}
			return expression;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000EB40 File Offset: 0x0000CD40
		private Expression ParsePrimary()
		{
			Expression expression = this.ParsePrimaryStart();
			for (;;)
			{
				if (this.token.id == ExpressionParser.TokenId.Dot)
				{
					this.NextToken();
					expression = this.ParseMemberAccess(null, expression);
				}
				else
				{
					if (this.token.id != ExpressionParser.TokenId.OpenBracket)
					{
						break;
					}
					expression = this.ParseElementAccess(expression);
				}
			}
			return expression;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000EB90 File Offset: 0x0000CD90
		private Expression ParsePrimaryStart()
		{
			switch (this.token.id)
			{
			case ExpressionParser.TokenId.Identifier:
				return this.ParseIdentifier();
			case ExpressionParser.TokenId.StringLiteral:
				return this.ParseStringLiteral();
			case ExpressionParser.TokenId.IntegerLiteral:
				return this.ParseIntegerLiteral();
			case ExpressionParser.TokenId.RealLiteral:
				return this.ParseRealLiteral();
			case ExpressionParser.TokenId.OpenParen:
				return this.ParseParenExpression();
			}
			throw this.ParseError(AtlasWeb.ExpressionParser_ExpressionExpected, new object[0]);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000EC08 File Offset: 0x0000CE08
		private Expression ParseStringLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.StringLiteral);
			char c = this.token.text[0];
			string text = this.token.text.Substring(1, this.token.text.Length - 2);
			int startIndex = 0;
			for (;;)
			{
				int num = text.IndexOf(c, startIndex);
				if (num < 0)
				{
					break;
				}
				text = text.Remove(num, 1);
				startIndex = num + 1;
			}
			if (c != '\'')
			{
				this.NextToken();
				return this.CreateLiteral(text, text);
			}
			if (text.Length != 1)
			{
				throw this.ParseError(AtlasWeb.ExpressionParser_InvalidCharacterLiteral, new object[0]);
			}
			this.NextToken();
			return this.CreateLiteral(text[0], text);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		private Expression ParseIntegerLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.IntegerLiteral);
			string text = this.token.text;
			if (text[0] != '-')
			{
				ulong num;
				if (!ulong.TryParse(text, out num))
				{
					throw this.ParseError(AtlasWeb.ExpressionParser_InvalidIntegerLiteral, new object[]
					{
						text
					});
				}
				this.NextToken();
				if (num <= 2147483647UL)
				{
					return this.CreateLiteral((int)num, text);
				}
				if (num <= (ulong)-1)
				{
					return this.CreateLiteral((uint)num, text);
				}
				if (num <= 9223372036854775807UL)
				{
					return this.CreateLiteral((long)num, text);
				}
				return this.CreateLiteral(num, text);
			}
			else
			{
				long num2;
				if (!long.TryParse(text, out num2))
				{
					throw this.ParseError(AtlasWeb.ExpressionParser_InvalidIntegerLiteral, new object[]
					{
						text
					});
				}
				this.NextToken();
				if (num2 >= -2147483648L && num2 <= 2147483647L)
				{
					return this.CreateLiteral((int)num2, text);
				}
				return this.CreateLiteral(num2, text);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		private Expression ParseRealLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.RealLiteral);
			string text = this.token.text;
			object obj = null;
			char c = text[text.Length - 1];
			double num2;
			if (c == 'F' || c == 'f')
			{
				float num;
				if (float.TryParse(text.Substring(0, text.Length - 1), out num))
				{
					obj = num;
				}
			}
			else if (double.TryParse(text, out num2))
			{
				obj = num2;
			}
			if (obj == null)
			{
				throw this.ParseError(AtlasWeb.ExpressionParser_InvalidRealLiteral, new object[]
				{
					text
				});
			}
			this.NextToken();
			return this.CreateLiteral(obj, text);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000EE4C File Offset: 0x0000D04C
		private Expression CreateLiteral(object value, string text)
		{
			ConstantExpression constantExpression = Expression.Constant(value);
			this.literals[constantExpression] = text;
			return constantExpression;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000EE70 File Offset: 0x0000D070
		private Expression ParseParenExpression()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, AtlasWeb.ExpressionParser_OpenParenExpected);
			this.NextToken();
			Expression result = this.ParseExpression();
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, AtlasWeb.ExpressionParser_CloseParenOrOperatorExpected);
			this.NextToken();
			return result;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000EEAC File Offset: 0x0000D0AC
		private Expression ParseIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier);
			object obj;
			if (ExpressionParser.keywords.TryGetValue(this.token.text, out obj))
			{
				if (obj is Type)
				{
					return this.ParseTypeAccess((Type)obj);
				}
				if (obj == ExpressionParser.keywordIt)
				{
					return this.ParseIt();
				}
				if (obj == ExpressionParser.keywordIif)
				{
					return this.ParseIif();
				}
				if (obj == ExpressionParser.keywordNew)
				{
					return this.ParseNew();
				}
				this.NextToken();
				return (Expression)obj;
			}
			else
			{
				if (this.symbols.TryGetValue(this.token.text, out obj) || (this.externals != null && this.externals.TryGetValue(this.token.text, out obj)))
				{
					Expression expression = obj as Expression;
					if (expression == null)
					{
						expression = Expression.Constant(obj);
					}
					else
					{
						LambdaExpression lambdaExpression = expression as LambdaExpression;
						if (lambdaExpression != null)
						{
							return this.ParseLambdaInvocation(lambdaExpression);
						}
					}
					this.NextToken();
					return expression;
				}
				if (this.it != null)
				{
					return this.ParseMemberAccess(null, this.it);
				}
				throw this.ParseError(AtlasWeb.ExpressionParser_UnknownIdentifier, new object[]
				{
					this.token.text
				});
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		private Expression ParseIt()
		{
			if (this.it == null)
			{
				throw this.ParseError(AtlasWeb.ExpressionParser_NoItInScope, new object[0]);
			}
			this.NextToken();
			return this.it;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		private Expression ParseIif()
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			if (array.Length != 3)
			{
				throw this.ParseError(pos, AtlasWeb.ExpressionParser_IifRequiresThreeArgs, new object[0]);
			}
			return this.GenerateConditional(array[0], array[1], array[2], pos);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000F040 File Offset: 0x0000D240
		private Expression GenerateConditional(Expression test, Expression expr1, Expression expr2, int errorPos)
		{
			if (test.Type != typeof(bool))
			{
				throw this.ParseError(errorPos, AtlasWeb.ExpressionParser_FirstExprMustBeBool, new object[0]);
			}
			if (expr1.Type != expr2.Type)
			{
				Expression expression = (expr2 != ExpressionParser.nullLiteral) ? this.PromoteExpression(expr1, expr2.Type, true) : null;
				Expression expression2 = (expr1 != ExpressionParser.nullLiteral) ? this.PromoteExpression(expr2, expr1.Type, true) : null;
				if (expression != null && expression2 == null)
				{
					expr1 = expression;
				}
				else if (expression2 != null && expression == null)
				{
					expr2 = expression2;
				}
				else
				{
					string text = (expr1 != ExpressionParser.nullLiteral) ? expr1.Type.Name : "null";
					string text2 = (expr2 != ExpressionParser.nullLiteral) ? expr2.Type.Name : "null";
					if (expression != null && expression2 != null)
					{
						throw this.ParseError(errorPos, AtlasWeb.ExpressionParser_BothTypesConvertToOther, new object[]
						{
							text,
							text2
						});
					}
					throw this.ParseError(errorPos, AtlasWeb.ExpressionParser_NeitherTypeConvertsToOther, new object[]
					{
						text,
						text2
					});
				}
			}
			return Expression.Condition(test, expr1, expr2);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000F154 File Offset: 0x0000D354
		private Expression ParseNew()
		{
			this.NextToken();
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, AtlasWeb.ExpressionParser_OpenParenExpected);
			this.NextToken();
			List<DynamicProperty> list = new List<DynamicProperty>();
			List<Expression> list2 = new List<Expression>();
			int pos;
			for (;;)
			{
				pos = this.token.pos;
				Expression expression = this.ParseExpression();
				string name;
				if (this.TokenIdentifierIs("as"))
				{
					this.NextToken();
					name = this.GetIdentifier();
					this.NextToken();
				}
				else
				{
					MemberExpression memberExpression = expression as MemberExpression;
					if (memberExpression == null)
					{
						break;
					}
					name = memberExpression.Member.Name;
				}
				list2.Add(expression);
				list.Add(new DynamicProperty(name, expression.Type));
				if (this.token.id != ExpressionParser.TokenId.Comma)
				{
					goto IL_C2;
				}
				this.NextToken();
			}
			throw this.ParseError(pos, AtlasWeb.ExpressionParser_MissingAsClause, new object[0]);
			IL_C2:
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, AtlasWeb.ExpressionParser_CloseParenOrCommaExpected);
			this.NextToken();
			Type type = DynamicExpression.CreateClass(list);
			MemberBinding[] array = new MemberBinding[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Expression.Bind(type.GetProperty(list[i].Name), list2[i]);
			}
			return Expression.MemberInit(Expression.New(type), array);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F28C File Offset: 0x0000D48C
		private Expression ParseLambdaInvocation(LambdaExpression lambda)
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			if (this.FindMethod(lambda.Type, "Invoke", false, array, out methodBase) != 1)
			{
				throw this.ParseError(pos, AtlasWeb.ExpressionParser_ArgsIncompatibleWithLambda, new object[0]);
			}
			return Expression.Invoke(lambda, array);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		private Expression ParseTypeAccess(Type type)
		{
			int pos = this.token.pos;
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.Question)
			{
				if (!type.IsValueType || ExpressionParser.IsNullableType(type))
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_TypeHasNoNullableForm, new object[]
					{
						ExpressionParser.GetTypeName(type)
					});
				}
				type = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
				this.NextToken();
			}
			if (this.token.id != ExpressionParser.TokenId.OpenParen)
			{
				this.ValidateToken(ExpressionParser.TokenId.Dot, AtlasWeb.ExpressionParser_DotOrOpenParenExpected);
				this.NextToken();
				return this.ParseMemberAccess(type, null);
			}
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			int num = this.FindBestMethod(type.GetConstructors(), array, out methodBase);
			if (num != 0)
			{
				if (num != 1)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_AmbiguousConstructorInvocation, new object[]
					{
						ExpressionParser.GetTypeName(type)
					});
				}
				return Expression.New((ConstructorInfo)methodBase, array);
			}
			else
			{
				if (array.Length == 1)
				{
					return this.GenerateConversion(array[0], type, pos);
				}
				throw this.ParseError(pos, AtlasWeb.ExpressionParser_NoMatchingConstructor, new object[]
				{
					ExpressionParser.GetTypeName(type)
				});
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F400 File Offset: 0x0000D600
		private Expression GenerateConversion(Expression expr, Type type, int errorPos)
		{
			Type type2 = expr.Type;
			if (type2 == type)
			{
				return expr;
			}
			if (type2.IsValueType && type.IsValueType)
			{
				if ((ExpressionParser.IsNullableType(type2) || ExpressionParser.IsNullableType(type)) && ExpressionParser.GetNonNullableType(type2) == ExpressionParser.GetNonNullableType(type))
				{
					return Expression.Convert(expr, type);
				}
				if ((ExpressionParser.IsNumericType(type2) || ExpressionParser.IsEnumType(type2)) && (ExpressionParser.IsNumericType(type) || ExpressionParser.IsEnumType(type)))
				{
					return Expression.ConvertChecked(expr, type);
				}
			}
			if (type2.IsAssignableFrom(type) || type.IsAssignableFrom(type2) || type2.IsInterface || type.IsInterface)
			{
				return Expression.Convert(expr, type);
			}
			throw this.ParseError(errorPos, AtlasWeb.ExpressionParser_CannotConvertValue, new object[]
			{
				ExpressionParser.GetTypeName(type2),
				ExpressionParser.GetTypeName(type)
			});
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		private Expression ParseMemberAccess(Type type, Expression instance)
		{
			if (instance != null)
			{
				type = instance.Type;
			}
			int pos = this.token.pos;
			string identifier = this.GetIdentifier();
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.OpenParen)
			{
				if (instance != null && type != typeof(string))
				{
					Type type2 = ExpressionParser.FindGenericType(typeof(IEnumerable<>), type);
					if (type2 != null)
					{
						Type elementType = type2.GetGenericArguments()[0];
						return this.ParseAggregate(instance, elementType, identifier, pos);
					}
				}
				Expression[] array = this.ParseArgumentList();
				MethodBase methodBase;
				int num = this.FindMethod(type, identifier, instance == null, array, out methodBase);
				if (num == 0)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_NoApplicableMethod, new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				}
				if (num != 1)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_AmbiguousMethodInvocation, new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				}
				MethodInfo methodInfo = (MethodInfo)methodBase;
				if (!ExpressionParser.IsPredefinedType(methodInfo.DeclaringType))
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_MethodsAreInaccessible, new object[]
					{
						ExpressionParser.GetTypeName(methodInfo.DeclaringType)
					});
				}
				if (methodInfo.ReturnType == typeof(void))
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_MethodIsVoid, new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(methodInfo.DeclaringType)
					});
				}
				return Expression.Call(instance, methodInfo, array);
			}
			else
			{
				MemberInfo memberInfo = this.FindPropertyOrField(type, identifier, instance == null);
				if (memberInfo == null)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_UnknownPropertyOrField, new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				}
				if (!(memberInfo is PropertyInfo))
				{
					return Expression.Field(instance, (FieldInfo)memberInfo);
				}
				return Expression.Property(instance, (PropertyInfo)memberInfo);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		private static Type FindGenericType(Type generic, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == generic)
				{
					return type;
				}
				if (generic.IsInterface)
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = ExpressionParser.FindGenericType(generic, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000F720 File Offset: 0x0000D920
		private Expression ParseAggregate(Expression instance, Type elementType, string methodName, int errorPos)
		{
			ParameterExpression parameterExpression = this.it;
			ParameterExpression parameterExpression2 = Expression.Parameter(elementType, "");
			this.it = parameterExpression2;
			Expression[] array = this.ParseArgumentList();
			this.it = parameterExpression;
			MethodBase methodBase;
			if (this.FindMethod(typeof(ExpressionParser.IEnumerableSignatures), methodName, false, array, out methodBase) != 1)
			{
				throw this.ParseError(errorPos, AtlasWeb.ExpressionParser_NoApplicableAggregate, new object[]
				{
					methodName
				});
			}
			Type[] typeArguments;
			if (methodBase.Name == "Min" || methodBase.Name == "Max")
			{
				typeArguments = new Type[]
				{
					elementType,
					array[0].Type
				};
			}
			else
			{
				typeArguments = new Type[]
				{
					elementType
				};
			}
			if (array.Length == 0)
			{
				array = new Expression[]
				{
					instance
				};
			}
			else
			{
				array = new Expression[]
				{
					instance,
					DynamicExpression.Lambda(array[0], new ParameterExpression[]
					{
						parameterExpression2
					})
				};
			}
			return Expression.Call(typeof(Enumerable), methodBase.Name, typeArguments, array);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000F818 File Offset: 0x0000DA18
		private Expression[] ParseArgumentList()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, AtlasWeb.ExpressionParser_OpenParenExpected);
			this.NextToken();
			Expression[] result = (this.token.id != ExpressionParser.TokenId.CloseParen) ? this.ParseArguments() : new Expression[0];
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, AtlasWeb.ExpressionParser_CloseParenOrCommaExpected);
			this.NextToken();
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000F86C File Offset: 0x0000DA6C
		private Expression[] ParseArguments()
		{
			List<Expression> list = new List<Expression>();
			for (;;)
			{
				list.Add(this.ParseExpression());
				if (this.token.id != ExpressionParser.TokenId.Comma)
				{
					break;
				}
				this.NextToken();
			}
			return list.ToArray();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		private Expression ParseElementAccess(Expression expr)
		{
			int pos = this.token.pos;
			this.ValidateToken(ExpressionParser.TokenId.OpenBracket, AtlasWeb.ExpressionParser_OpenParenExpected);
			this.NextToken();
			Expression[] array = this.ParseArguments();
			this.ValidateToken(ExpressionParser.TokenId.CloseBracket, AtlasWeb.ExpressionParser_CloseBracketOrCommaExpected);
			this.NextToken();
			if (expr.Type.IsArray)
			{
				if (expr.Type.GetArrayRank() != 1 || array.Length != 1)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_CannotIndexMultipleDimensionalArray, new object[0]);
				}
				Expression expression = this.PromoteExpression(array[0], typeof(int), true);
				if (expression == null)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_InvalidIndex, new object[0]);
				}
				return Expression.ArrayIndex(expr, expression);
			}
			else
			{
				MethodBase methodBase;
				int num = this.FindIndexer(expr.Type, array, out methodBase);
				if (num == 0)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_NoApplicableIndexer, new object[]
					{
						ExpressionParser.GetTypeName(expr.Type)
					});
				}
				if (num != 1)
				{
					throw this.ParseError(pos, AtlasWeb.ExpressionParser_AmbiguousIndexerInvocation, new object[]
					{
						ExpressionParser.GetTypeName(expr.Type)
					});
				}
				return Expression.Call(expr, (MethodInfo)methodBase, array);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		private static bool IsPredefinedType(Type type)
		{
			foreach (Type left in ExpressionParser.predefinedTypes)
			{
				if (left == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000F9F1 File Offset: 0x0000DBF1
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000FA12 File Offset: 0x0000DC12
		private static Type GetNonNullableType(Type type)
		{
			if (!ExpressionParser.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000FA28 File Offset: 0x0000DC28
		private static string GetTypeName(Type type)
		{
			Type nonNullableType = ExpressionParser.GetNonNullableType(type);
			string text = nonNullableType.Name;
			if (type != nonNullableType)
			{
				text += "?";
			}
			return text;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000FA59 File Offset: 0x0000DC59
		private static bool IsNumericType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) != 0;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000FA64 File Offset: 0x0000DC64
		private static bool IsSignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 2;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000FA6F File Offset: 0x0000DC6F
		private static bool IsUnsignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 3;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		private static int GetNumericTypeKind(Type type)
		{
			type = ExpressionParser.GetNonNullableType(type);
			if (type.IsEnum)
			{
				return 0;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Char:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return 1;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return 2;
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		private static bool IsEnumType(Type type)
		{
			return ExpressionParser.GetNonNullableType(type).IsEnum;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		private void CheckAndPromoteOperand(Type signatures, string opName, ref Expression expr, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				expr
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1)
			{
				throw this.ParseError(errorPos, AtlasWeb.ExpressionParser_IncompatibleOperand, new object[]
				{
					opName,
					ExpressionParser.GetTypeName(array[0].Type)
				});
			}
			expr = array[0];
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		private void CheckAndPromoteOperands(Type signatures, string opName, ref Expression left, ref Expression right, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				left,
				right
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1)
			{
				throw this.IncompatibleOperandsError(opName, left, right, errorPos);
			}
			left = array[0];
			right = array[1];
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000FB98 File Offset: 0x0000DD98
		private Exception IncompatibleOperandsError(string opName, Expression left, Expression right, int pos)
		{
			return this.ParseError(pos, AtlasWeb.ExpressionParser_IncompatibleOperands, new object[]
			{
				opName,
				ExpressionParser.GetTypeName(left.Type),
				ExpressionParser.GetTypeName(right.Type)
			});
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		private MemberInfo FindPropertyOrField(Type type, string memberName, bool staticAccess)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type type2 in ExpressionParser.SelfAndBaseTypes(type))
			{
				MemberInfo[] array = type2.FindMembers(MemberTypes.Field | MemberTypes.Property, bindingAttr, Type.FilterNameIgnoreCase, memberName);
				if (array.Length != 0)
				{
					return array[0];
				}
			}
			return null;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000FC40 File Offset: 0x0000DE40
		private int FindMethod(Type type, string methodName, bool staticAccess, Expression[] args, out MethodBase method)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type type2 in ExpressionParser.SelfAndBaseTypes(type))
			{
				MemberInfo[] source = type2.FindMembers(MemberTypes.Method, bindingAttr, Type.FilterNameIgnoreCase, methodName);
				int num = this.FindBestMethod(source.Cast<MethodBase>(), args, out method);
				if (num != 0)
				{
					return num;
				}
			}
			method = null;
			return 0;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		private int FindIndexer(Type type, Expression[] args, out MethodBase method)
		{
			foreach (Type type2 in ExpressionParser.SelfAndBaseTypes(type))
			{
				MemberInfo[] defaultMembers = type2.GetDefaultMembers();
				if (defaultMembers.Length != 0)
				{
					IEnumerable<MethodBase> methods = from p in defaultMembers.OfType<PropertyInfo>()
					select p.GetGetMethod() into m
					where m != null
					select m;
					int num = this.FindBestMethod(methods, args, out method);
					if (num != 0)
					{
						return num;
					}
				}
			}
			method = null;
			return 0;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000FD80 File Offset: 0x0000DF80
		private static IEnumerable<Type> SelfAndBaseTypes(Type type)
		{
			if (type.IsInterface)
			{
				List<Type> list = new List<Type>();
				ExpressionParser.AddInterface(list, type);
				return list;
			}
			return ExpressionParser.SelfAndBaseClasses(type);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000FDAA File Offset: 0x0000DFAA
		private static IEnumerable<Type> SelfAndBaseClasses(Type type)
		{
			while (type != null)
			{
				yield return type;
				type = type.BaseType;
			}
			yield break;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		private static void AddInterface(List<Type> types, Type type)
		{
			if (!types.Contains(type))
			{
				types.Add(type);
				foreach (Type type2 in type.GetInterfaces())
				{
					ExpressionParser.AddInterface(types, type2);
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000FDFC File Offset: 0x0000DFFC
		private int FindBestMethod(IEnumerable<MethodBase> methods, Expression[] args, out MethodBase method)
		{
			ExpressionParser.MethodData[] applicable = (from m in methods
			select new ExpressionParser.MethodData
			{
				MethodBase = m,
				Parameters = m.GetParameters()
			} into m
			where this.IsApplicable(m, args)
			select m).ToArray<ExpressionParser.MethodData>();
			if (applicable.Length > 1)
			{
				applicable = (from m in applicable
				where applicable.All((ExpressionParser.MethodData n) => m == n || ExpressionParser.IsBetterThan(args, m, n))
				select m).ToArray<ExpressionParser.MethodData>();
			}
			if (applicable.Length == 1)
			{
				ExpressionParser.MethodData methodData = applicable[0];
				for (int i = 0; i < args.Length; i++)
				{
					args[i] = methodData.Args[i];
				}
				method = methodData.MethodBase;
			}
			else
			{
				method = null;
			}
			return applicable.Length;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		private bool IsApplicable(ExpressionParser.MethodData method, Expression[] args)
		{
			if (method.Parameters.Length != args.Length)
			{
				return false;
			}
			Expression[] array = new Expression[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				ParameterInfo parameterInfo = method.Parameters[i];
				if (parameterInfo.IsOut)
				{
					return false;
				}
				Expression expression = this.PromoteExpression(args[i], parameterInfo.ParameterType, false);
				if (expression == null)
				{
					return false;
				}
				array[i] = expression;
			}
			method.Args = array;
			return true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000FF40 File Offset: 0x0000E140
		private Expression PromoteExpression(Expression expr, Type type, bool exact)
		{
			if (expr.Type == type)
			{
				return expr;
			}
			if (expr is ConstantExpression)
			{
				ConstantExpression constantExpression = (ConstantExpression)expr;
				string name;
				if (constantExpression == ExpressionParser.nullLiteral)
				{
					if (!type.IsValueType || ExpressionParser.IsNullableType(type))
					{
						return Expression.Constant(null, type);
					}
				}
				else if (this.literals.TryGetValue(constantExpression, out name))
				{
					Type nonNullableType = ExpressionParser.GetNonNullableType(type);
					object obj = null;
					switch (Type.GetTypeCode(constantExpression.Type))
					{
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
						obj = ExpressionParser.ParseNumber(name, nonNullableType);
						break;
					case TypeCode.Double:
						if (nonNullableType == typeof(decimal))
						{
							obj = ExpressionParser.ParseNumber(name, nonNullableType);
						}
						break;
					case TypeCode.String:
						obj = ExpressionParser.ParseEnum(name, nonNullableType);
						break;
					}
					if (obj != null)
					{
						return Expression.Constant(obj, type);
					}
				}
			}
			if (!ExpressionParser.IsCompatibleWith(expr.Type, type))
			{
				return null;
			}
			if (type.IsValueType || exact)
			{
				return Expression.Convert(expr, type);
			}
			return expr;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001004C File Offset: 0x0000E24C
		private static object ParseNumber(string text, Type type)
		{
			switch (Type.GetTypeCode(ExpressionParser.GetNonNullableType(type)))
			{
			case TypeCode.SByte:
			{
				sbyte b;
				if (sbyte.TryParse(text, out b))
				{
					return b;
				}
				break;
			}
			case TypeCode.Byte:
			{
				byte b2;
				if (byte.TryParse(text, out b2))
				{
					return b2;
				}
				break;
			}
			case TypeCode.Int16:
			{
				short num;
				if (short.TryParse(text, out num))
				{
					return num;
				}
				break;
			}
			case TypeCode.UInt16:
			{
				ushort num2;
				if (ushort.TryParse(text, out num2))
				{
					return num2;
				}
				break;
			}
			case TypeCode.Int32:
			{
				int num3;
				if (int.TryParse(text, out num3))
				{
					return num3;
				}
				break;
			}
			case TypeCode.UInt32:
			{
				uint num4;
				if (uint.TryParse(text, out num4))
				{
					return num4;
				}
				break;
			}
			case TypeCode.Int64:
			{
				long num5;
				if (long.TryParse(text, out num5))
				{
					return num5;
				}
				break;
			}
			case TypeCode.UInt64:
			{
				ulong num6;
				if (ulong.TryParse(text, out num6))
				{
					return num6;
				}
				break;
			}
			case TypeCode.Single:
			{
				float num7;
				if (float.TryParse(text, out num7))
				{
					return num7;
				}
				break;
			}
			case TypeCode.Double:
			{
				double num8;
				if (double.TryParse(text, out num8))
				{
					return num8;
				}
				break;
			}
			case TypeCode.Decimal:
			{
				decimal num9;
				if (decimal.TryParse(text, out num9))
				{
					return num9;
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010170 File Offset: 0x0000E370
		private static object ParseEnum(string name, Type type)
		{
			if (type.IsEnum)
			{
				MemberInfo[] array = type.FindMembers(MemberTypes.Field, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, Type.FilterNameIgnoreCase, name);
				if (array.Length != 0)
				{
					return ((FieldInfo)array[0]).GetValue(null);
				}
			}
			return null;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000101AC File Offset: 0x0000E3AC
		private static bool IsCompatibleWith(Type source, Type target)
		{
			if (source == target)
			{
				return true;
			}
			if (!target.IsValueType)
			{
				return target.IsAssignableFrom(source);
			}
			Type nonNullableType = ExpressionParser.GetNonNullableType(source);
			Type nonNullableType2 = ExpressionParser.GetNonNullableType(target);
			if (nonNullableType != source && nonNullableType2 == target)
			{
				return false;
			}
			TypeCode typeCode = nonNullableType.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType);
			TypeCode typeCode2 = nonNullableType2.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType2);
			switch (typeCode)
			{
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Byte:
				if (typeCode2 - TypeCode.Byte <= 9)
				{
					return true;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt16:
				if (typeCode2 - TypeCode.UInt16 <= 7)
				{
					return true;
				}
				break;
			case TypeCode.Int32:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt32:
				if (typeCode2 - TypeCode.UInt32 <= 5)
				{
					return true;
				}
				break;
			case TypeCode.Int64:
				if (typeCode2 == TypeCode.Int64 || typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.UInt64:
				if (typeCode2 - TypeCode.UInt64 <= 3)
				{
					return true;
				}
				break;
			case TypeCode.Single:
				if (typeCode2 - TypeCode.Single <= 1)
				{
					return true;
				}
				break;
			default:
				if (nonNullableType == nonNullableType2)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00010328 File Offset: 0x0000E528
		private static bool IsBetterThan(Expression[] args, ExpressionParser.MethodData m1, ExpressionParser.MethodData m2)
		{
			bool result = false;
			for (int i = 0; i < args.Length; i++)
			{
				int num = ExpressionParser.CompareConversions(args[i].Type, m1.Parameters[i].ParameterType, m2.Parameters[i].ParameterType);
				if (num < 0)
				{
					return false;
				}
				if (num > 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001037C File Offset: 0x0000E57C
		private static int CompareConversions(Type s, Type t1, Type t2)
		{
			if (t1 == t2)
			{
				return 0;
			}
			if (s == t1)
			{
				return 1;
			}
			if (s == t2)
			{
				return -1;
			}
			bool flag = ExpressionParser.IsCompatibleWith(t1, t2);
			bool flag2 = ExpressionParser.IsCompatibleWith(t2, t1);
			if (flag && !flag2)
			{
				return 1;
			}
			if (flag2 && !flag)
			{
				return -1;
			}
			if (ExpressionParser.IsSignedIntegralType(t1) && ExpressionParser.IsUnsignedIntegralType(t2))
			{
				return 1;
			}
			if (ExpressionParser.IsSignedIntegralType(t2) && ExpressionParser.IsUnsignedIntegralType(t1))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000103EF File Offset: 0x0000E5EF
		private Expression GenerateEqual(Expression left, Expression right)
		{
			return Expression.Equal(left, right);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000103F8 File Offset: 0x0000E5F8
		private Expression GenerateNotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00010401 File Offset: 0x0000E601
		private Expression GenerateGreaterThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThan(left, right);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001043F File Offset: 0x0000E63F
		private Expression GenerateGreaterThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001047D File Offset: 0x0000E67D
		private Expression GenerateLessThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThan(left, right);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000104BB File Offset: 0x0000E6BB
		private Expression GenerateLessThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThanOrEqual(left, right);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000104FC File Offset: 0x0000E6FC
		private Expression GenerateAdd(Expression left, Expression right)
		{
			if (left.Type == typeof(string) && right.Type == typeof(string))
			{
				return this.GenerateStaticMethodCall("Concat", left, right);
			}
			return Expression.Add(left, right);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001054C File Offset: 0x0000E74C
		private Expression GenerateSubtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00010558 File Offset: 0x0000E758
		private Expression GenerateStringConcat(Expression left, Expression right)
		{
			if (left.Type.IsValueType)
			{
				left = Expression.Convert(left, typeof(object));
			}
			if (right.Type.IsValueType)
			{
				right = Expression.Convert(right, typeof(object));
			}
			return Expression.Call(null, typeof(string).GetMethod("Concat", new Type[]
			{
				typeof(object),
				typeof(object)
			}), new Expression[]
			{
				left,
				right
			});
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000105EB File Offset: 0x0000E7EB
		private MethodInfo GetStaticMethod(string methodName, Expression left, Expression right)
		{
			return left.Type.GetMethod(methodName, new Type[]
			{
				left.Type,
				right.Type
			});
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00010611 File Offset: 0x0000E811
		private Expression GenerateStaticMethodCall(string methodName, Expression left, Expression right)
		{
			return Expression.Call(null, this.GetStaticMethod(methodName, left, right), new Expression[]
			{
				left,
				right
			});
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010630 File Offset: 0x0000E830
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00010664 File Offset: 0x0000E864
		private void NextChar()
		{
			if (this.textPos < this.textLen)
			{
				this.textPos++;
			}
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000106B8 File Offset: 0x0000E8B8
		private void NextToken()
		{
			while (char.IsWhiteSpace(this.ch))
			{
				this.NextChar();
			}
			int num = this.textPos;
			char c = this.ch;
			ExpressionParser.TokenId id;
			if (c <= '[')
			{
				switch (c)
				{
				case '!':
					this.NextChar();
					if (this.ch == '=')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.ExclamationEqual;
						goto IL_421;
					}
					id = ExpressionParser.TokenId.Exclamation;
					goto IL_421;
				case '"':
				case '\'':
				{
					char c2 = this.ch;
					for (;;)
					{
						this.NextChar();
						while (this.textPos < this.textLen && this.ch != c2)
						{
							this.NextChar();
						}
						if (this.textPos == this.textLen)
						{
							break;
						}
						this.NextChar();
						if (this.ch != c2)
						{
							goto Block_16;
						}
					}
					throw this.ParseError(this.textPos, AtlasWeb.ExpressionParser_UnterminatedStringLiteral, new object[0]);
					Block_16:
					id = ExpressionParser.TokenId.StringLiteral;
					goto IL_421;
				}
				case '#':
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
				case ';':
					break;
				case '%':
					this.NextChar();
					id = ExpressionParser.TokenId.Percent;
					goto IL_421;
				case '&':
					this.NextChar();
					if (this.ch == '&')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.DoubleAmphersand;
						goto IL_421;
					}
					id = ExpressionParser.TokenId.Amphersand;
					goto IL_421;
				case '(':
					this.NextChar();
					id = ExpressionParser.TokenId.OpenParen;
					goto IL_421;
				case ')':
					this.NextChar();
					id = ExpressionParser.TokenId.CloseParen;
					goto IL_421;
				case '*':
					this.NextChar();
					id = ExpressionParser.TokenId.Asterisk;
					goto IL_421;
				case '+':
					this.NextChar();
					id = ExpressionParser.TokenId.Plus;
					goto IL_421;
				case ',':
					this.NextChar();
					id = ExpressionParser.TokenId.Comma;
					goto IL_421;
				case '-':
					this.NextChar();
					id = ExpressionParser.TokenId.Minus;
					goto IL_421;
				case '.':
					this.NextChar();
					id = ExpressionParser.TokenId.Dot;
					goto IL_421;
				case '/':
					this.NextChar();
					id = ExpressionParser.TokenId.Slash;
					goto IL_421;
				case ':':
					this.NextChar();
					id = ExpressionParser.TokenId.Colon;
					goto IL_421;
				case '<':
					this.NextChar();
					if (this.ch == '=')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.LessThanEqual;
						goto IL_421;
					}
					if (this.ch == '>')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.LessGreater;
						goto IL_421;
					}
					id = ExpressionParser.TokenId.LessThan;
					goto IL_421;
				case '=':
					this.NextChar();
					if (this.ch == '=')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.DoubleEqual;
						goto IL_421;
					}
					id = ExpressionParser.TokenId.Equal;
					goto IL_421;
				case '>':
					this.NextChar();
					if (this.ch == '=')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.GreaterThanEqual;
						goto IL_421;
					}
					id = ExpressionParser.TokenId.GreaterThan;
					goto IL_421;
				case '?':
					this.NextChar();
					id = ExpressionParser.TokenId.Question;
					goto IL_421;
				default:
					if (c == '[')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.OpenBracket;
						goto IL_421;
					}
					break;
				}
			}
			else
			{
				if (c == ']')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.CloseBracket;
					goto IL_421;
				}
				if (c == '|')
				{
					this.NextChar();
					if (this.ch == '|')
					{
						this.NextChar();
						id = ExpressionParser.TokenId.DoubleBar;
						goto IL_421;
					}
					id = ExpressionParser.TokenId.Bar;
					goto IL_421;
				}
			}
			if (ExpressionParser.IsIdentifierStart(this.ch) || this.ch == '@' || this.ch == '_')
			{
				do
				{
					this.NextChar();
				}
				while (ExpressionParser.IsIdentifierPart(this.ch) || this.ch == '_');
				id = ExpressionParser.TokenId.Identifier;
			}
			else if (char.IsDigit(this.ch))
			{
				id = ExpressionParser.TokenId.IntegerLiteral;
				do
				{
					this.NextChar();
				}
				while (char.IsDigit(this.ch));
				if (this.ch == '.')
				{
					id = ExpressionParser.TokenId.RealLiteral;
					this.NextChar();
					this.ValidateDigit();
					do
					{
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
				}
				if (this.ch == 'E' || this.ch == 'e')
				{
					id = ExpressionParser.TokenId.RealLiteral;
					this.NextChar();
					if (this.ch == '+' || this.ch == '-')
					{
						this.NextChar();
					}
					this.ValidateDigit();
					do
					{
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
				}
				if (this.ch == 'F' || this.ch == 'f')
				{
					this.NextChar();
				}
			}
			else
			{
				if (this.textPos != this.textLen)
				{
					throw this.ParseError(this.textPos, AtlasWeb.ExpressionParser_InvalidCharacter, new object[]
					{
						this.ch
					});
				}
				id = ExpressionParser.TokenId.End;
			}
			IL_421:
			this.token.id = id;
			this.token.text = this.text.Substring(num, this.textPos - num);
			this.token.pos = num;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010B1D File Offset: 0x0000ED1D
		private static bool IsIdentifierStart(char ch)
		{
			return (1 << (int)char.GetUnicodeCategory(ch) & 543) != 0;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00010B33 File Offset: 0x0000ED33
		private static bool IsIdentifierPart(char ch)
		{
			return (1 << (int)char.GetUnicodeCategory(ch) & 295807) != 0;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00010B49 File Offset: 0x0000ED49
		private bool TokenIdentifierIs(string id)
		{
			return this.token.id == ExpressionParser.TokenId.Identifier && string.Equals(id, this.token.text, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00010B70 File Offset: 0x0000ED70
		private string GetIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier, AtlasWeb.ExpressionParser_IdentifierExpected);
			string text = this.token.text;
			if (text.Length > 1 && text[0] == '@')
			{
				text = text.Substring(1);
			}
			return text;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00010BB2 File Offset: 0x0000EDB2
		private void ValidateDigit()
		{
			if (!char.IsDigit(this.ch))
			{
				throw this.ParseError(this.textPos, AtlasWeb.ExpressionParser_DigitExpected, new object[0]);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010BD9 File Offset: 0x0000EDD9
		private void ValidateToken(ExpressionParser.TokenId t, string errorMessage)
		{
			if (this.token.id != t)
			{
				throw this.ParseError(errorMessage, new object[0]);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010BF7 File Offset: 0x0000EDF7
		private void ValidateToken(ExpressionParser.TokenId t)
		{
			if (this.token.id != t)
			{
				throw this.ParseError(AtlasWeb.ExpressionParser_SyntaxError, new object[0]);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010C19 File Offset: 0x0000EE19
		private Exception ParseError(string format, params object[] args)
		{
			return this.ParseError(this.token.pos, format, args);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010C2E File Offset: 0x0000EE2E
		private Exception ParseError(int pos, string format, params object[] args)
		{
			return new ParseException(string.Format(CultureInfo.CurrentCulture, format, args), pos);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010C44 File Offset: 0x0000EE44
		private static Dictionary<string, object> CreateKeywords()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("true", ExpressionParser.trueLiteral);
			dictionary.Add("false", ExpressionParser.falseLiteral);
			dictionary.Add("null", ExpressionParser.nullLiteral);
			dictionary.Add(ExpressionParser.keywordIt, ExpressionParser.keywordIt);
			dictionary.Add(ExpressionParser.keywordIif, ExpressionParser.keywordIif);
			dictionary.Add(ExpressionParser.keywordNew, ExpressionParser.keywordNew);
			foreach (Type type in ExpressionParser.predefinedTypes)
			{
				dictionary.Add(type.Name, type);
			}
			return dictionary;
		}

		// Token: 0x040000E7 RID: 231
		private static readonly Type[] predefinedTypes = new Type[]
		{
			typeof(object),
			typeof(bool),
			typeof(char),
			typeof(string),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(DateTimeOffset),
			typeof(TimeSpan),
			typeof(Guid),
			typeof(Math),
			typeof(Convert)
		};

		// Token: 0x040000E8 RID: 232
		private static readonly Expression trueLiteral = Expression.Constant(true);

		// Token: 0x040000E9 RID: 233
		private static readonly Expression falseLiteral = Expression.Constant(false);

		// Token: 0x040000EA RID: 234
		private static readonly Expression nullLiteral = Expression.Constant(null);

		// Token: 0x040000EB RID: 235
		private static readonly string keywordIt = "it";

		// Token: 0x040000EC RID: 236
		private static readonly string keywordIif = "iif";

		// Token: 0x040000ED RID: 237
		private static readonly string keywordNew = "new";

		// Token: 0x040000EE RID: 238
		private static Dictionary<string, object> keywords;

		// Token: 0x040000EF RID: 239
		private Dictionary<string, object> symbols;

		// Token: 0x040000F0 RID: 240
		private IDictionary<string, object> externals;

		// Token: 0x040000F1 RID: 241
		private Dictionary<Expression, string> literals;

		// Token: 0x040000F2 RID: 242
		private ParameterExpression it;

		// Token: 0x040000F3 RID: 243
		private string text;

		// Token: 0x040000F4 RID: 244
		private int textPos;

		// Token: 0x040000F5 RID: 245
		private int textLen;

		// Token: 0x040000F6 RID: 246
		private char ch;

		// Token: 0x040000F7 RID: 247
		private ExpressionParser.Token token;

		// Token: 0x02000145 RID: 325
		private struct Token
		{
			// Token: 0x04000499 RID: 1177
			public ExpressionParser.TokenId id;

			// Token: 0x0400049A RID: 1178
			public string text;

			// Token: 0x0400049B RID: 1179
			public int pos;
		}

		// Token: 0x02000146 RID: 326
		private enum TokenId
		{
			// Token: 0x0400049D RID: 1181
			Unknown,
			// Token: 0x0400049E RID: 1182
			End,
			// Token: 0x0400049F RID: 1183
			Identifier,
			// Token: 0x040004A0 RID: 1184
			StringLiteral,
			// Token: 0x040004A1 RID: 1185
			IntegerLiteral,
			// Token: 0x040004A2 RID: 1186
			RealLiteral,
			// Token: 0x040004A3 RID: 1187
			Exclamation,
			// Token: 0x040004A4 RID: 1188
			Percent,
			// Token: 0x040004A5 RID: 1189
			Amphersand,
			// Token: 0x040004A6 RID: 1190
			OpenParen,
			// Token: 0x040004A7 RID: 1191
			CloseParen,
			// Token: 0x040004A8 RID: 1192
			Asterisk,
			// Token: 0x040004A9 RID: 1193
			Plus,
			// Token: 0x040004AA RID: 1194
			Comma,
			// Token: 0x040004AB RID: 1195
			Minus,
			// Token: 0x040004AC RID: 1196
			Dot,
			// Token: 0x040004AD RID: 1197
			Slash,
			// Token: 0x040004AE RID: 1198
			Colon,
			// Token: 0x040004AF RID: 1199
			LessThan,
			// Token: 0x040004B0 RID: 1200
			Equal,
			// Token: 0x040004B1 RID: 1201
			GreaterThan,
			// Token: 0x040004B2 RID: 1202
			Question,
			// Token: 0x040004B3 RID: 1203
			OpenBracket,
			// Token: 0x040004B4 RID: 1204
			CloseBracket,
			// Token: 0x040004B5 RID: 1205
			Bar,
			// Token: 0x040004B6 RID: 1206
			ExclamationEqual,
			// Token: 0x040004B7 RID: 1207
			DoubleAmphersand,
			// Token: 0x040004B8 RID: 1208
			LessThanEqual,
			// Token: 0x040004B9 RID: 1209
			LessGreater,
			// Token: 0x040004BA RID: 1210
			DoubleEqual,
			// Token: 0x040004BB RID: 1211
			GreaterThanEqual,
			// Token: 0x040004BC RID: 1212
			DoubleBar
		}

		// Token: 0x02000147 RID: 327
		private interface ILogicalSignatures
		{
			// Token: 0x06000F7F RID: 3967
			void F(bool x, bool y);

			// Token: 0x06000F80 RID: 3968
			void F(bool? x, bool? y);
		}

		// Token: 0x02000148 RID: 328
		private interface IArithmeticSignatures
		{
			// Token: 0x06000F81 RID: 3969
			void F(int x, int y);

			// Token: 0x06000F82 RID: 3970
			void F(uint x, uint y);

			// Token: 0x06000F83 RID: 3971
			void F(long x, long y);

			// Token: 0x06000F84 RID: 3972
			void F(ulong x, ulong y);

			// Token: 0x06000F85 RID: 3973
			void F(float x, float y);

			// Token: 0x06000F86 RID: 3974
			void F(double x, double y);

			// Token: 0x06000F87 RID: 3975
			void F(decimal x, decimal y);

			// Token: 0x06000F88 RID: 3976
			void F(int? x, int? y);

			// Token: 0x06000F89 RID: 3977
			void F(uint? x, uint? y);

			// Token: 0x06000F8A RID: 3978
			void F(long? x, long? y);

			// Token: 0x06000F8B RID: 3979
			void F(ulong? x, ulong? y);

			// Token: 0x06000F8C RID: 3980
			void F(float? x, float? y);

			// Token: 0x06000F8D RID: 3981
			void F(double? x, double? y);

			// Token: 0x06000F8E RID: 3982
			void F(decimal? x, decimal? y);
		}

		// Token: 0x02000149 RID: 329
		private interface IRelationalSignatures : ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06000F8F RID: 3983
			void F(string x, string y);

			// Token: 0x06000F90 RID: 3984
			void F(char x, char y);

			// Token: 0x06000F91 RID: 3985
			void F(DateTime x, DateTime y);

			// Token: 0x06000F92 RID: 3986
			void F(DateTimeOffset x, DateTimeOffset y);

			// Token: 0x06000F93 RID: 3987
			void F(TimeSpan x, TimeSpan y);

			// Token: 0x06000F94 RID: 3988
			void F(char? x, char? y);

			// Token: 0x06000F95 RID: 3989
			void F(DateTime? x, DateTime? y);

			// Token: 0x06000F96 RID: 3990
			void F(DateTimeOffset? x, DateTimeOffset? y);

			// Token: 0x06000F97 RID: 3991
			void F(TimeSpan? x, TimeSpan? y);
		}

		// Token: 0x0200014A RID: 330
		private interface IEqualitySignatures : ExpressionParser.IRelationalSignatures, ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06000F98 RID: 3992
			void F(bool x, bool y);

			// Token: 0x06000F99 RID: 3993
			void F(bool? x, bool? y);

			// Token: 0x06000F9A RID: 3994
			void F(Guid x, Guid y);

			// Token: 0x06000F9B RID: 3995
			void F(Guid? x, Guid? y);
		}

		// Token: 0x0200014B RID: 331
		private interface IAddSignatures : ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06000F9C RID: 3996
			void F(DateTime x, TimeSpan y);

			// Token: 0x06000F9D RID: 3997
			void F(DateTimeOffset x, TimeSpan y);

			// Token: 0x06000F9E RID: 3998
			void F(TimeSpan x, TimeSpan y);

			// Token: 0x06000F9F RID: 3999
			void F(DateTime? x, TimeSpan? y);

			// Token: 0x06000FA0 RID: 4000
			void F(DateTimeOffset? x, TimeSpan? y);

			// Token: 0x06000FA1 RID: 4001
			void F(TimeSpan? x, TimeSpan? y);
		}

		// Token: 0x0200014C RID: 332
		private interface ISubtractSignatures : ExpressionParser.IAddSignatures, ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06000FA2 RID: 4002
			void F(DateTime x, DateTime y);

			// Token: 0x06000FA3 RID: 4003
			void F(DateTimeOffset x, DateTimeOffset y);

			// Token: 0x06000FA4 RID: 4004
			void F(DateTime? x, DateTime? y);

			// Token: 0x06000FA5 RID: 4005
			void F(DateTimeOffset? x, DateTimeOffset? y);
		}

		// Token: 0x0200014D RID: 333
		private interface INegationSignatures
		{
			// Token: 0x06000FA6 RID: 4006
			void F(int x);

			// Token: 0x06000FA7 RID: 4007
			void F(long x);

			// Token: 0x06000FA8 RID: 4008
			void F(float x);

			// Token: 0x06000FA9 RID: 4009
			void F(double x);

			// Token: 0x06000FAA RID: 4010
			void F(decimal x);

			// Token: 0x06000FAB RID: 4011
			void F(int? x);

			// Token: 0x06000FAC RID: 4012
			void F(long? x);

			// Token: 0x06000FAD RID: 4013
			void F(float? x);

			// Token: 0x06000FAE RID: 4014
			void F(double? x);

			// Token: 0x06000FAF RID: 4015
			void F(decimal? x);
		}

		// Token: 0x0200014E RID: 334
		private interface INotSignatures
		{
			// Token: 0x06000FB0 RID: 4016
			void F(bool x);

			// Token: 0x06000FB1 RID: 4017
			void F(bool? x);
		}

		// Token: 0x0200014F RID: 335
		private interface IEnumerableSignatures
		{
			// Token: 0x06000FB2 RID: 4018
			void Where(bool predicate);

			// Token: 0x06000FB3 RID: 4019
			void Any();

			// Token: 0x06000FB4 RID: 4020
			void Any(bool predicate);

			// Token: 0x06000FB5 RID: 4021
			void All(bool predicate);

			// Token: 0x06000FB6 RID: 4022
			void Count();

			// Token: 0x06000FB7 RID: 4023
			void Count(bool predicate);

			// Token: 0x06000FB8 RID: 4024
			void Min(object selector);

			// Token: 0x06000FB9 RID: 4025
			void Max(object selector);

			// Token: 0x06000FBA RID: 4026
			void Sum(int selector);

			// Token: 0x06000FBB RID: 4027
			void Sum(int? selector);

			// Token: 0x06000FBC RID: 4028
			void Sum(long selector);

			// Token: 0x06000FBD RID: 4029
			void Sum(long? selector);

			// Token: 0x06000FBE RID: 4030
			void Sum(float selector);

			// Token: 0x06000FBF RID: 4031
			void Sum(float? selector);

			// Token: 0x06000FC0 RID: 4032
			void Sum(double selector);

			// Token: 0x06000FC1 RID: 4033
			void Sum(double? selector);

			// Token: 0x06000FC2 RID: 4034
			void Sum(decimal selector);

			// Token: 0x06000FC3 RID: 4035
			void Sum(decimal? selector);

			// Token: 0x06000FC4 RID: 4036
			void Average(int selector);

			// Token: 0x06000FC5 RID: 4037
			void Average(int? selector);

			// Token: 0x06000FC6 RID: 4038
			void Average(long selector);

			// Token: 0x06000FC7 RID: 4039
			void Average(long? selector);

			// Token: 0x06000FC8 RID: 4040
			void Average(float selector);

			// Token: 0x06000FC9 RID: 4041
			void Average(float? selector);

			// Token: 0x06000FCA RID: 4042
			void Average(double selector);

			// Token: 0x06000FCB RID: 4043
			void Average(double? selector);

			// Token: 0x06000FCC RID: 4044
			void Average(decimal selector);

			// Token: 0x06000FCD RID: 4045
			void Average(decimal? selector);
		}

		// Token: 0x02000150 RID: 336
		private class MethodData
		{
			// Token: 0x040004BD RID: 1213
			public MethodBase MethodBase;

			// Token: 0x040004BE RID: 1214
			public ParameterInfo[] Parameters;

			// Token: 0x040004BF RID: 1215
			public Expression[] Args;
		}
	}
}

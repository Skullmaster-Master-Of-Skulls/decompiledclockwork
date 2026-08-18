using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02000377 RID: 887
	internal class ExpressionParser
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x0005EB24 File Offset: 0x0005CD24
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

		// Token: 0x06001E50 RID: 7760 RVA: 0x0005EBAC File Offset: 0x0005CDAC
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

		// Token: 0x06001E51 RID: 7761 RVA: 0x0005EC08 File Offset: 0x0005CE08
		private void ProcessValues(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				object obj = values[i];
				IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
				if (i == values.Length - 1 && dictionary != null)
				{
					this.externals = dictionary;
				}
				else
				{
					this.AddSymbol("@" + i.ToString(CultureInfo.InvariantCulture), obj);
				}
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0005EC60 File Offset: 0x0005CE60
		private void AddSymbol(string name, object value)
		{
			if (this.symbols.ContainsKey(name))
			{
				throw this.ParseError("The identifier '{0}' was defined more than once", new object[]
				{
					name
				});
			}
			this.symbols.Add(name, value);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0005ECA0 File Offset: 0x0005CEA0
		public Expression Parse(Type resultType)
		{
			int pos = this.token.pos;
			Expression expression = this.ParseExpression();
			if (resultType != null && (expression = this.PromoteExpression(expression, resultType, true)) == null)
			{
				throw this.ParseError(pos, "Expression of type '{0}' expected", new object[]
				{
					ExpressionParser.GetTypeName(resultType)
				});
			}
			this.ValidateToken(ExpressionParser.TokenId.End, "Syntax error");
			return expression;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0005ED04 File Offset: 0x0005CF04
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
			this.ValidateToken(ExpressionParser.TokenId.End, "Syntax error");
			return list;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0005EDA4 File Offset: 0x0005CFA4
		private Expression ParseExpression()
		{
			int pos = this.token.pos;
			Expression expression = this.ParseLogicalOr();
			if (this.token.id == ExpressionParser.TokenId.Question)
			{
				this.NextToken();
				Expression expr = this.ParseExpression();
				this.ValidateToken(ExpressionParser.TokenId.Colon, "':' expected");
				this.NextToken();
				Expression expr2 = this.ParseExpression();
				expression = this.GenerateConditional(expression, expr, expr2, pos);
			}
			return expression;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0005EE08 File Offset: 0x0005D008
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

		// Token: 0x06001E57 RID: 7767 RVA: 0x0005EE7C File Offset: 0x0005D07C
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

		// Token: 0x06001E58 RID: 7768 RVA: 0x0005EEF0 File Offset: 0x0005D0F0
		private Expression ParseComparison()
		{
			Expression expression = this.ParseAdditive();
			while (this.token.id == ExpressionParser.TokenId.Equal || this.token.id == ExpressionParser.TokenId.DoubleEqual || this.token.id == ExpressionParser.TokenId.ExclamationEqual || this.token.id == ExpressionParser.TokenId.LessGreater || this.token.id == ExpressionParser.TokenId.GreaterThan || this.token.id == ExpressionParser.TokenId.GreaterThanEqual || this.token.id == ExpressionParser.TokenId.LessThan || this.token.id == ExpressionParser.TokenId.LessThanEqual)
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseAdditive();
				bool flag = token.id == ExpressionParser.TokenId.Equal || token.id == ExpressionParser.TokenId.DoubleEqual || token.id == ExpressionParser.TokenId.ExclamationEqual || token.id == ExpressionParser.TokenId.LessGreater;
				if ((flag && !expression.Type.IsValueType && !expression2.Type.IsValueType) || (expression.Type == typeof(Guid) && expression2.Type == typeof(Guid)))
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
				else if (expression.Type == typeof(Guid) && expression2.Type == typeof(Guid))
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

		// Token: 0x06001E59 RID: 7769 RVA: 0x0005F27C File Offset: 0x0005D47C
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
					switch (id)
					{
					case ExpressionParser.TokenId.Plus:
						if (!(expression.Type == typeof(string)) && !(expression2.Type == typeof(string)))
						{
							this.CheckAndPromoteOperands(typeof(ExpressionParser.IAddSignatures), token.text, ref expression, ref expression2, token.pos);
							expression = this.GenerateAdd(expression, expression2);
							continue;
						}
						break;
					case ExpressionParser.TokenId.Comma:
						continue;
					case ExpressionParser.TokenId.Minus:
						this.CheckAndPromoteOperands(typeof(ExpressionParser.ISubtractSignatures), token.text, ref expression, ref expression2, token.pos);
						expression = this.GenerateSubtract(expression, expression2);
						continue;
					default:
						continue;
					}
				}
				expression = this.GenerateStringConcat(expression, expression2);
			}
			return expression;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0005F39C File Offset: 0x0005D59C
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

		// Token: 0x06001E5B RID: 7771 RVA: 0x0005F46C File Offset: 0x0005D66C
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

		// Token: 0x06001E5C RID: 7772 RVA: 0x0005F580 File Offset: 0x0005D780
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

		// Token: 0x06001E5D RID: 7773 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
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
			throw this.ParseError("Expression expected", new object[0]);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0005F648 File Offset: 0x0005D848
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
				throw this.ParseError("Character literal must contain exactly one character", new object[0]);
			}
			this.NextToken();
			return this.CreateLiteral(text[0], text);
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		private Expression ParseIntegerLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.IntegerLiteral);
			string text = this.token.text;
			if (text[0] != '-')
			{
				ulong num;
				if (!ulong.TryParse(text, out num))
				{
					throw this.ParseError("Invalid integer literal '{0}'", new object[]
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
					throw this.ParseError("Invalid integer literal '{0}'", new object[]
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

		// Token: 0x06001E60 RID: 7776 RVA: 0x0005F7F8 File Offset: 0x0005D9F8
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
				throw this.ParseError("Invalid real literal '{0}'", new object[]
				{
					text
				});
			}
			this.NextToken();
			return this.CreateLiteral(obj, text);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0005F894 File Offset: 0x0005DA94
		private Expression CreateLiteral(object value, string text)
		{
			ConstantExpression constantExpression = Expression.Constant(value);
			this.literals.Add(constantExpression, text);
			return constantExpression;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		private Expression ParseParenExpression()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			Expression result = this.ParseExpression();
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or operator expected");
			this.NextToken();
			return result;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0005F8F4 File Offset: 0x0005DAF4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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
				throw this.ParseError("Unknown identifier '{0}'", new object[]
				{
					this.token.text
				});
			}
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0005FA10 File Offset: 0x0005DC10
		private Expression ParseIt()
		{
			if (this.it == null)
			{
				throw this.ParseError("No 'it' is in scope", new object[0]);
			}
			this.NextToken();
			return this.it;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0005FA38 File Offset: 0x0005DC38
		private Expression ParseIif()
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			if (array.Length != 3)
			{
				throw this.ParseError(pos, "The 'iif' function requires three arguments", new object[0]);
			}
			return this.GenerateConditional(array[0], array[1], array[2], pos);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0005FA88 File Offset: 0x0005DC88
		private Expression GenerateConditional(Expression test, Expression expr1, Expression expr2, int errorPos)
		{
			if (test.Type != typeof(bool))
			{
				throw this.ParseError(errorPos, "The first expression must be of type 'Boolean'", new object[0]);
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
						throw this.ParseError(errorPos, "Both of the types '{0}' and '{1}' convert to the other", new object[]
						{
							text,
							text2
						});
					}
					throw this.ParseError(errorPos, "Neither of the types '{0}' and '{1}' converts to the other", new object[]
					{
						text,
						text2
					});
				}
			}
			return Expression.Condition(test, expr1, expr2);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0005FBB0 File Offset: 0x0005DDB0
		private Expression ParseNew()
		{
			this.NextToken();
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
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
					goto IL_BC;
				}
				this.NextToken();
			}
			throw this.ParseError(pos, "Expression is missing an 'as' clause", new object[0]);
			IL_BC:
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or ',' expected");
			this.NextToken();
			Type type = DynamicExpression.CreateClass(list);
			MemberBinding[] array = new MemberBinding[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Expression.Bind(type.GetProperty(list[i].Name), list2[i]);
			}
			return Expression.MemberInit(Expression.New(type), array);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0005FCE8 File Offset: 0x0005DEE8
		private Expression ParseLambdaInvocation(LambdaExpression lambda)
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			if (this.FindMethod(lambda.Type, "Invoke", false, array, out methodBase) != 1)
			{
				throw this.ParseError(pos, "Argument list incompatible with lambda expression", new object[0]);
			}
			return Expression.Invoke(lambda, array);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0005FD40 File Offset: 0x0005DF40
		private Expression ParseTypeAccess(Type type)
		{
			int pos = this.token.pos;
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.Question)
			{
				if (!type.IsValueType || ExpressionParser.IsNullableType(type))
				{
					throw this.ParseError(pos, "Type '{0}' has no nullable form", new object[]
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
				this.ValidateToken(ExpressionParser.TokenId.Dot, "'.' or '(' expected");
				this.NextToken();
				return this.ParseMemberAccess(type, null);
			}
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			switch (this.FindBestMethod(type.GetConstructors(), array, out methodBase))
			{
			case 0:
				if (array.Length == 1)
				{
					return this.GenerateConversion(array[0], type, pos);
				}
				throw this.ParseError(pos, "No matching constructor in type '{0}'", new object[]
				{
					ExpressionParser.GetTypeName(type)
				});
			case 1:
				return Expression.New((ConstructorInfo)methodBase, array);
			default:
				throw this.ParseError(pos, "Ambiguous invocation of '{0}' constructor", new object[]
				{
					ExpressionParser.GetTypeName(type)
				});
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0005FE7C File Offset: 0x0005E07C
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
				if (((ExpressionParser.IsNumericType(type2) || ExpressionParser.IsEnumType(type2)) && ExpressionParser.IsNumericType(type)) || ExpressionParser.IsEnumType(type))
				{
					return Expression.ConvertChecked(expr, type);
				}
			}
			if (type2.IsAssignableFrom(type) || type.IsAssignableFrom(type2) || type2.IsInterface || type.IsInterface)
			{
				return Expression.Convert(expr, type);
			}
			throw this.ParseError(errorPos, "A value of type '{0}' cannot be converted to type '{1}'", new object[]
			{
				ExpressionParser.GetTypeName(type2),
				ExpressionParser.GetTypeName(type)
			});
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0005FF50 File Offset: 0x0005E150
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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
				switch (this.FindMethod(type, identifier, instance == null, array, out methodBase))
				{
				case 0:
					throw this.ParseError(pos, "No applicable method '{0}' exists in type '{1}'", new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				case 1:
				{
					MethodInfo methodInfo = (MethodInfo)methodBase;
					if (!ExpressionParser.IsPredefinedType(methodInfo.DeclaringType))
					{
						throw this.ParseError(pos, "Methods on type '{0}' are not accessible", new object[]
						{
							ExpressionParser.GetTypeName(methodInfo.DeclaringType)
						});
					}
					if (methodInfo.ReturnType == typeof(void))
					{
						throw this.ParseError(pos, "Method '{0}' in type '{1}' does not return a value", new object[]
						{
							identifier,
							ExpressionParser.GetTypeName(methodInfo.DeclaringType)
						});
					}
					return Expression.Call(instance, methodInfo, array);
				}
				default:
					throw this.ParseError(pos, "Ambiguous invocation of method '{0}' in type '{1}'", new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				}
			}
			else
			{
				MemberInfo memberInfo = this.FindPropertyOrField(type, identifier, instance == null);
				if (memberInfo == null)
				{
					throw this.ParseError(pos, "No property or field '{0}' exists in type '{1}'", new object[]
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

		// Token: 0x06001E6C RID: 7788 RVA: 0x00060140 File Offset: 0x0005E340
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

		// Token: 0x06001E6D RID: 7789 RVA: 0x000601C8 File Offset: 0x0005E3C8
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
				throw this.ParseError(errorPos, "No applicable aggregate method '{0}' exists", new object[]
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
					Expression.Lambda(array[0], new ParameterExpression[]
					{
						parameterExpression2
					})
				};
			}
			return Expression.Call(typeof(Enumerable), methodBase.Name, typeArguments, array);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000602E0 File Offset: 0x0005E4E0
		private Expression[] ParseArgumentList()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			Expression[] result = (this.token.id != ExpressionParser.TokenId.CloseParen) ? this.ParseArguments() : new Expression[0];
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or ',' expected");
			this.NextToken();
			return result;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00060334 File Offset: 0x0005E534
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

		// Token: 0x06001E70 RID: 7792 RVA: 0x00060370 File Offset: 0x0005E570
		private Expression ParseElementAccess(Expression expr)
		{
			int pos = this.token.pos;
			this.ValidateToken(ExpressionParser.TokenId.OpenBracket, "'(' expected");
			this.NextToken();
			Expression[] array = this.ParseArguments();
			this.ValidateToken(ExpressionParser.TokenId.CloseBracket, "']' or ',' expected");
			this.NextToken();
			if (expr.Type.IsArray)
			{
				if (expr.Type.GetArrayRank() != 1 || array.Length != 1)
				{
					throw this.ParseError(pos, "Indexing of multi-dimensional arrays is not supported", new object[0]);
				}
				Expression expression = this.PromoteExpression(array[0], typeof(int), true);
				if (expression == null)
				{
					throw this.ParseError(pos, "Array index must be an integer expression", new object[0]);
				}
				return Expression.ArrayIndex(expr, expression);
			}
			else
			{
				MethodBase methodBase;
				switch (this.FindIndexer(expr.Type, array, out methodBase))
				{
				case 0:
					throw this.ParseError(pos, "No applicable indexer exists in type '{0}'", new object[]
					{
						ExpressionParser.GetTypeName(expr.Type)
					});
				case 1:
					return Expression.Call(expr, (MethodInfo)methodBase, array);
				default:
					throw this.ParseError(pos, "Ambiguous invocation of indexer in type '{0}'", new object[]
					{
						ExpressionParser.GetTypeName(expr.Type)
					});
				}
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00060498 File Offset: 0x0005E698
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

		// Token: 0x06001E72 RID: 7794 RVA: 0x000604CD File Offset: 0x0005E6CD
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000604EE File Offset: 0x0005E6EE
		private static Type GetNonNullableType(Type type)
		{
			if (!ExpressionParser.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00060504 File Offset: 0x0005E704
		private static string GetTypeName(Type type)
		{
			Type nonNullableType = ExpressionParser.GetNonNullableType(type);
			string text = nonNullableType.Name;
			if (type != nonNullableType)
			{
				text += '?';
			}
			return text;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00060537 File Offset: 0x0005E737
		private static bool IsNumericType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) != 0;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00060545 File Offset: 0x0005E745
		private static bool IsSignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 2;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00060550 File Offset: 0x0005E750
		private static bool IsUnsignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 3;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0006055C File Offset: 0x0005E75C
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

		// Token: 0x06001E79 RID: 7801 RVA: 0x000605C3 File Offset: 0x0005E7C3
		private static bool IsEnumType(Type type)
		{
			return ExpressionParser.GetNonNullableType(type).IsEnum;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000605D0 File Offset: 0x0005E7D0
		private void CheckAndPromoteOperand(Type signatures, string opName, ref Expression expr, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				expr
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1)
			{
				throw this.ParseError(errorPos, "Operator '{0}' incompatible with operand type '{1}'", new object[]
				{
					opName,
					ExpressionParser.GetTypeName(array[0].Type)
				});
			}
			expr = array[0];
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00060630 File Offset: 0x0005E830
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

		// Token: 0x06001E7C RID: 7804 RVA: 0x00060680 File Offset: 0x0005E880
		private Exception IncompatibleOperandsError(string opName, Expression left, Expression right, int pos)
		{
			return this.ParseError(pos, "Operator '{0}' incompatible with operand types '{1}' and '{2}'", new object[]
			{
				opName,
				ExpressionParser.GetTypeName(left.Type),
				ExpressionParser.GetTypeName(right.Type)
			});
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000606C4 File Offset: 0x0005E8C4
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
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

		// Token: 0x06001E7E RID: 7806 RVA: 0x00060738 File Offset: 0x0005E938
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

		// Token: 0x06001E7F RID: 7807 RVA: 0x000607D4 File Offset: 0x0005E9D4
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

		// Token: 0x06001E80 RID: 7808 RVA: 0x00060890 File Offset: 0x0005EA90
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

		// Token: 0x06001E81 RID: 7809 RVA: 0x000609A4 File Offset: 0x0005EBA4
		private static IEnumerable<Type> SelfAndBaseClasses(Type type)
		{
			while (type != null)
			{
				yield return type;
				type = type.BaseType;
			}
			yield break;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000609C4 File Offset: 0x0005EBC4
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

		// Token: 0x06001E83 RID: 7811 RVA: 0x00060AAC File Offset: 0x0005ECAC
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

		// Token: 0x06001E84 RID: 7812 RVA: 0x00060B8C File Offset: 0x0005ED8C
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

		// Token: 0x06001E85 RID: 7813 RVA: 0x00060BF4 File Offset: 0x0005EDF4
		private Expression PromoteExpression(Expression expr, Type type, bool exact)
		{
			if (expr.Type == type)
			{
				return expr;
			}
			ConstantExpression constantExpression = expr as ConstantExpression;
			if (constantExpression != null)
			{
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

		// Token: 0x06001E86 RID: 7814 RVA: 0x00060CFC File Offset: 0x0005EEFC
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

		// Token: 0x06001E87 RID: 7815 RVA: 0x00060E20 File Offset: 0x0005F020
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

		// Token: 0x06001E88 RID: 7816 RVA: 0x00060E5C File Offset: 0x0005F05C
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
				switch (typeCode2)
				{
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
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
				switch (typeCode2)
				{
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
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
				switch (typeCode2)
				{
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int64:
				switch (typeCode2)
				{
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt64:
				switch (typeCode2)
				{
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Single:
				switch (typeCode2)
				{
				case TypeCode.Single:
				case TypeCode.Double:
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

		// Token: 0x06001E89 RID: 7817 RVA: 0x000610A8 File Offset: 0x0005F2A8
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

		// Token: 0x06001E8A RID: 7818 RVA: 0x000610FC File Offset: 0x0005F2FC
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

		// Token: 0x06001E8B RID: 7819 RVA: 0x0006116F File Offset: 0x0005F36F
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private Expression GenerateEqual(Expression left, Expression right)
		{
			return Expression.Equal(left, right);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00061178 File Offset: 0x0005F378
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private Expression GenerateNotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00061181 File Offset: 0x0005F381
		private Expression GenerateGreaterThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThan(left, right);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000611BF File Offset: 0x0005F3BF
		private Expression GenerateGreaterThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000611FD File Offset: 0x0005F3FD
		private Expression GenerateLessThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThan(left, right);
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0006123B File Offset: 0x0005F43B
		private Expression GenerateLessThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThanOrEqual(left, right);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0006127C File Offset: 0x0005F47C
		private Expression GenerateAdd(Expression left, Expression right)
		{
			if (left.Type == typeof(string) && right.Type == typeof(string))
			{
				return this.GenerateStaticMethodCall("Concat", left, right);
			}
			return Expression.Add(left, right);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x000612CC File Offset: 0x0005F4CC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private Expression GenerateSubtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000612D8 File Offset: 0x0005F4D8
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private Expression GenerateStringConcat(Expression left, Expression right)
		{
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

		// Token: 0x06001E94 RID: 7828 RVA: 0x00061334 File Offset: 0x0005F534
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private MethodInfo GetStaticMethod(string methodName, Expression left, Expression right)
		{
			return left.Type.GetMethod(methodName, new Type[]
			{
				left.Type,
				right.Type
			});
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00061368 File Offset: 0x0005F568
		private Expression GenerateStaticMethodCall(string methodName, Expression left, Expression right)
		{
			return Expression.Call(null, this.GetStaticMethod(methodName, left, right), new Expression[]
			{
				left,
				right
			});
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00061394 File Offset: 0x0005F594
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000613C8 File Offset: 0x0005F5C8
		private void NextChar()
		{
			if (this.textPos < this.textLen)
			{
				this.textPos++;
			}
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0006141C File Offset: 0x0005F61C
		private void NextToken()
		{
			while (char.IsWhiteSpace(this.ch))
			{
				this.NextChar();
			}
			int num = this.textPos;
			char c = this.ch;
			ExpressionParser.TokenId id;
			switch (c)
			{
			case '!':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.ExclamationEqual;
					goto IL_435;
				}
				id = ExpressionParser.TokenId.Exclamation;
				goto IL_435;
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
						goto Block_14;
					}
				}
				throw this.ParseError(this.textPos, "Unterminated string literal", new object[0]);
				Block_14:
				id = ExpressionParser.TokenId.StringLiteral;
				goto IL_435;
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
				goto IL_435;
			case '&':
				this.NextChar();
				if (this.ch == '&')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.DoubleAmphersand;
					goto IL_435;
				}
				id = ExpressionParser.TokenId.Amphersand;
				goto IL_435;
			case '(':
				this.NextChar();
				id = ExpressionParser.TokenId.OpenParen;
				goto IL_435;
			case ')':
				this.NextChar();
				id = ExpressionParser.TokenId.CloseParen;
				goto IL_435;
			case '*':
				this.NextChar();
				id = ExpressionParser.TokenId.Asterisk;
				goto IL_435;
			case '+':
				this.NextChar();
				id = ExpressionParser.TokenId.Plus;
				goto IL_435;
			case ',':
				this.NextChar();
				id = ExpressionParser.TokenId.Comma;
				goto IL_435;
			case '-':
				this.NextChar();
				id = ExpressionParser.TokenId.Minus;
				goto IL_435;
			case '.':
				this.NextChar();
				id = ExpressionParser.TokenId.Dot;
				goto IL_435;
			case '/':
				this.NextChar();
				id = ExpressionParser.TokenId.Slash;
				goto IL_435;
			case ':':
				this.NextChar();
				id = ExpressionParser.TokenId.Colon;
				goto IL_435;
			case '<':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.LessThanEqual;
					goto IL_435;
				}
				if (this.ch == '>')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.LessGreater;
					goto IL_435;
				}
				id = ExpressionParser.TokenId.LessThan;
				goto IL_435;
			case '=':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.DoubleEqual;
					goto IL_435;
				}
				id = ExpressionParser.TokenId.Equal;
				goto IL_435;
			case '>':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.GreaterThanEqual;
					goto IL_435;
				}
				id = ExpressionParser.TokenId.GreaterThan;
				goto IL_435;
			case '?':
				this.NextChar();
				id = ExpressionParser.TokenId.Question;
				goto IL_435;
			default:
				switch (c)
				{
				case '[':
					this.NextChar();
					id = ExpressionParser.TokenId.OpenBracket;
					goto IL_435;
				case '\\':
					break;
				case ']':
					this.NextChar();
					id = ExpressionParser.TokenId.CloseBracket;
					goto IL_435;
				default:
					if (c == '|')
					{
						this.NextChar();
						if (this.ch == '|')
						{
							this.NextChar();
							id = ExpressionParser.TokenId.DoubleBar;
							goto IL_435;
						}
						id = ExpressionParser.TokenId.Bar;
						goto IL_435;
					}
					break;
				}
				break;
			}
			if (char.IsLetter(this.ch) || this.ch == '@' || this.ch == '_')
			{
				do
				{
					this.NextChar();
				}
				while (char.IsLetterOrDigit(this.ch) || this.ch == '_');
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
				if (this.ch.ToString() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
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
					throw this.ParseError(this.textPos, "Syntax error '{0}'", new object[]
					{
						this.ch
					});
				}
				id = ExpressionParser.TokenId.End;
			}
			IL_435:
			this.token.id = id;
			this.token.text = this.text.Substring(num, this.textPos - num);
			this.token.pos = num;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00061895 File Offset: 0x0005FA95
		private bool TokenIdentifierIs(string id)
		{
			return this.token.id == ExpressionParser.TokenId.Identifier && string.Equals(id, this.token.text, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000618BC File Offset: 0x0005FABC
		private string GetIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier, "Identifier expected");
			string text = this.token.text;
			if (text.Length > 1 && text[0] == '@')
			{
				text = text.Substring(1);
			}
			return text;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000618FE File Offset: 0x0005FAFE
		private void ValidateDigit()
		{
			if (!char.IsDigit(this.ch))
			{
				throw this.ParseError(this.textPos, "Digit expected", new object[0]);
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00061925 File Offset: 0x0005FB25
		private void ValidateToken(ExpressionParser.TokenId t, string errorMessage)
		{
			if (this.token.id != t)
			{
				throw this.ParseError(errorMessage, new object[0]);
			}
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00061943 File Offset: 0x0005FB43
		private void ValidateToken(ExpressionParser.TokenId t)
		{
			if (this.token.id != t)
			{
				throw this.ParseError("Syntax error", new object[0]);
			}
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00061965 File Offset: 0x0005FB65
		private Exception ParseError(string format, params object[] args)
		{
			return this.ParseError(this.token.pos, format, args);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0006197A File Offset: 0x0005FB7A
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private Exception ParseError(int pos, string format, params object[] args)
		{
			return new ParseException(string.Format(CultureInfo.CurrentCulture, format, args), pos);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00061990 File Offset: 0x0005FB90
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

		// Token: 0x04000787 RID: 1927
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
			typeof(TimeSpan),
			typeof(Guid),
			typeof(Math),
			typeof(Convert)
		};

		// Token: 0x04000788 RID: 1928
		private static readonly Expression trueLiteral = Expression.Constant(true);

		// Token: 0x04000789 RID: 1929
		private static readonly Expression falseLiteral = Expression.Constant(false);

		// Token: 0x0400078A RID: 1930
		private static readonly Expression nullLiteral = Expression.Constant(null);

		// Token: 0x0400078B RID: 1931
		private static readonly string keywordIt = "it";

		// Token: 0x0400078C RID: 1932
		private static readonly string keywordIif = "iif";

		// Token: 0x0400078D RID: 1933
		private static readonly string keywordNew = "new";

		// Token: 0x0400078E RID: 1934
		private static Dictionary<string, object> keywords;

		// Token: 0x0400078F RID: 1935
		private Dictionary<string, object> symbols;

		// Token: 0x04000790 RID: 1936
		private IDictionary<string, object> externals;

		// Token: 0x04000791 RID: 1937
		private Dictionary<Expression, string> literals;

		// Token: 0x04000792 RID: 1938
		private ParameterExpression it;

		// Token: 0x04000793 RID: 1939
		private string text;

		// Token: 0x04000794 RID: 1940
		private int textPos;

		// Token: 0x04000795 RID: 1941
		private int textLen;

		// Token: 0x04000796 RID: 1942
		private char ch;

		// Token: 0x04000797 RID: 1943
		private ExpressionParser.Token token;

		// Token: 0x02000378 RID: 888
		private struct Token
		{
			// Token: 0x0400079B RID: 1947
			public ExpressionParser.TokenId id;

			// Token: 0x0400079C RID: 1948
			public string text;

			// Token: 0x0400079D RID: 1949
			public int pos;
		}

		// Token: 0x02000379 RID: 889
		private enum TokenId
		{
			// Token: 0x0400079F RID: 1951
			Unknown,
			// Token: 0x040007A0 RID: 1952
			End,
			// Token: 0x040007A1 RID: 1953
			Identifier,
			// Token: 0x040007A2 RID: 1954
			StringLiteral,
			// Token: 0x040007A3 RID: 1955
			IntegerLiteral,
			// Token: 0x040007A4 RID: 1956
			RealLiteral,
			// Token: 0x040007A5 RID: 1957
			Exclamation,
			// Token: 0x040007A6 RID: 1958
			Percent,
			// Token: 0x040007A7 RID: 1959
			Amphersand,
			// Token: 0x040007A8 RID: 1960
			OpenParen,
			// Token: 0x040007A9 RID: 1961
			CloseParen,
			// Token: 0x040007AA RID: 1962
			Asterisk,
			// Token: 0x040007AB RID: 1963
			Plus,
			// Token: 0x040007AC RID: 1964
			Comma,
			// Token: 0x040007AD RID: 1965
			Minus,
			// Token: 0x040007AE RID: 1966
			Dot,
			// Token: 0x040007AF RID: 1967
			Slash,
			// Token: 0x040007B0 RID: 1968
			Colon,
			// Token: 0x040007B1 RID: 1969
			LessThan,
			// Token: 0x040007B2 RID: 1970
			Equal,
			// Token: 0x040007B3 RID: 1971
			GreaterThan,
			// Token: 0x040007B4 RID: 1972
			Question,
			// Token: 0x040007B5 RID: 1973
			OpenBracket,
			// Token: 0x040007B6 RID: 1974
			CloseBracket,
			// Token: 0x040007B7 RID: 1975
			Bar,
			// Token: 0x040007B8 RID: 1976
			ExclamationEqual,
			// Token: 0x040007B9 RID: 1977
			DoubleAmphersand,
			// Token: 0x040007BA RID: 1978
			LessThanEqual,
			// Token: 0x040007BB RID: 1979
			LessGreater,
			// Token: 0x040007BC RID: 1980
			DoubleEqual,
			// Token: 0x040007BD RID: 1981
			GreaterThanEqual,
			// Token: 0x040007BE RID: 1982
			DoubleBar
		}

		// Token: 0x0200037A RID: 890
		private interface ILogicalSignatures
		{
			// Token: 0x06001EA5 RID: 7845
			void F(bool x, bool y);

			// Token: 0x06001EA6 RID: 7846
			void F(bool? x, bool? y);
		}

		// Token: 0x0200037B RID: 891
		private interface IArithmeticSignatures
		{
			// Token: 0x06001EA7 RID: 7847
			void F(int x, int y);

			// Token: 0x06001EA8 RID: 7848
			void F(uint x, uint y);

			// Token: 0x06001EA9 RID: 7849
			void F(long x, long y);

			// Token: 0x06001EAA RID: 7850
			void F(ulong x, ulong y);

			// Token: 0x06001EAB RID: 7851
			void F(float x, float y);

			// Token: 0x06001EAC RID: 7852
			void F(double x, double y);

			// Token: 0x06001EAD RID: 7853
			void F(decimal x, decimal y);

			// Token: 0x06001EAE RID: 7854
			void F(int? x, int? y);

			// Token: 0x06001EAF RID: 7855
			void F(uint? x, uint? y);

			// Token: 0x06001EB0 RID: 7856
			void F(long? x, long? y);

			// Token: 0x06001EB1 RID: 7857
			void F(ulong? x, ulong? y);

			// Token: 0x06001EB2 RID: 7858
			void F(float? x, float? y);

			// Token: 0x06001EB3 RID: 7859
			void F(double? x, double? y);

			// Token: 0x06001EB4 RID: 7860
			void F(decimal? x, decimal? y);
		}

		// Token: 0x0200037C RID: 892
		private interface IRelationalSignatures : ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06001EB5 RID: 7861
			void F(string x, string y);

			// Token: 0x06001EB6 RID: 7862
			void F(char x, char y);

			// Token: 0x06001EB7 RID: 7863
			void F(DateTime x, DateTime y);

			// Token: 0x06001EB8 RID: 7864
			void F(TimeSpan x, TimeSpan y);

			// Token: 0x06001EB9 RID: 7865
			void F(char? x, char? y);

			// Token: 0x06001EBA RID: 7866
			void F(DateTime? x, DateTime? y);

			// Token: 0x06001EBB RID: 7867
			void F(TimeSpan? x, TimeSpan? y);
		}

		// Token: 0x0200037D RID: 893
		private interface IEqualitySignatures : ExpressionParser.IRelationalSignatures, ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06001EBC RID: 7868
			void F(bool x, bool y);

			// Token: 0x06001EBD RID: 7869
			void F(bool? x, bool? y);
		}

		// Token: 0x0200037E RID: 894
		private interface IAddSignatures : ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06001EBE RID: 7870
			void F(DateTime x, TimeSpan y);

			// Token: 0x06001EBF RID: 7871
			void F(TimeSpan x, TimeSpan y);

			// Token: 0x06001EC0 RID: 7872
			void F(DateTime? x, TimeSpan? y);

			// Token: 0x06001EC1 RID: 7873
			void F(TimeSpan? x, TimeSpan? y);
		}

		// Token: 0x0200037F RID: 895
		private interface ISubtractSignatures : ExpressionParser.IAddSignatures, ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x06001EC2 RID: 7874
			void F(DateTime x, DateTime y);

			// Token: 0x06001EC3 RID: 7875
			void F(DateTime? x, DateTime? y);
		}

		// Token: 0x02000380 RID: 896
		private interface INegationSignatures
		{
			// Token: 0x06001EC4 RID: 7876
			void F(int x);

			// Token: 0x06001EC5 RID: 7877
			void F(long x);

			// Token: 0x06001EC6 RID: 7878
			void F(float x);

			// Token: 0x06001EC7 RID: 7879
			void F(double x);

			// Token: 0x06001EC8 RID: 7880
			void F(decimal x);

			// Token: 0x06001EC9 RID: 7881
			void F(int? x);

			// Token: 0x06001ECA RID: 7882
			void F(long? x);

			// Token: 0x06001ECB RID: 7883
			void F(float? x);

			// Token: 0x06001ECC RID: 7884
			void F(double? x);

			// Token: 0x06001ECD RID: 7885
			void F(decimal? x);
		}

		// Token: 0x02000381 RID: 897
		private interface INotSignatures
		{
			// Token: 0x06001ECE RID: 7886
			void F(bool x);

			// Token: 0x06001ECF RID: 7887
			void F(bool? x);
		}

		// Token: 0x02000382 RID: 898
		private interface IEnumerableSignatures
		{
			// Token: 0x06001ED0 RID: 7888
			void Where(bool predicate);

			// Token: 0x06001ED1 RID: 7889
			void Any();

			// Token: 0x06001ED2 RID: 7890
			void Any(bool predicate);

			// Token: 0x06001ED3 RID: 7891
			void All(bool predicate);

			// Token: 0x06001ED4 RID: 7892
			void Count();

			// Token: 0x06001ED5 RID: 7893
			void Count(bool predicate);

			// Token: 0x06001ED6 RID: 7894
			void Min(object selector);

			// Token: 0x06001ED7 RID: 7895
			void Max(object selector);

			// Token: 0x06001ED8 RID: 7896
			void Sum(int selector);

			// Token: 0x06001ED9 RID: 7897
			void Sum(int? selector);

			// Token: 0x06001EDA RID: 7898
			void Sum(long selector);

			// Token: 0x06001EDB RID: 7899
			void Sum(long? selector);

			// Token: 0x06001EDC RID: 7900
			void Sum(float selector);

			// Token: 0x06001EDD RID: 7901
			void Sum(float? selector);

			// Token: 0x06001EDE RID: 7902
			void Sum(double selector);

			// Token: 0x06001EDF RID: 7903
			void Sum(double? selector);

			// Token: 0x06001EE0 RID: 7904
			void Sum(decimal selector);

			// Token: 0x06001EE1 RID: 7905
			void Sum(decimal? selector);

			// Token: 0x06001EE2 RID: 7906
			void Average(int selector);

			// Token: 0x06001EE3 RID: 7907
			void Average(int? selector);

			// Token: 0x06001EE4 RID: 7908
			void Average(long selector);

			// Token: 0x06001EE5 RID: 7909
			void Average(long? selector);

			// Token: 0x06001EE6 RID: 7910
			void Average(float selector);

			// Token: 0x06001EE7 RID: 7911
			void Average(float? selector);

			// Token: 0x06001EE8 RID: 7912
			void Average(double selector);

			// Token: 0x06001EE9 RID: 7913
			void Average(double? selector);

			// Token: 0x06001EEA RID: 7914
			void Average(decimal selector);

			// Token: 0x06001EEB RID: 7915
			void Average(decimal? selector);
		}

		// Token: 0x02000383 RID: 899
		private class MethodData
		{
			// Token: 0x040007BF RID: 1983
			public MethodBase MethodBase;

			// Token: 0x040007C0 RID: 1984
			public ParameterInfo[] Parameters;

			// Token: 0x040007C1 RID: 1985
			public Expression[] Args;
		}
	}
}

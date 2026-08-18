using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Conditions
{
	// Token: 0x02000039 RID: 57
	public class ConditionParser
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003A8D File Offset: 0x00001C8D
		private ConditionParser(SimpleStringReader stringReader, ConfigurationItemFactory configurationItemFactory)
		{
			this.configurationItemFactory = configurationItemFactory;
			this.tokenizer = new ConditionTokenizer(stringReader);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public static ConditionExpression ParseExpression(string expressionText)
		{
			return ConditionParser.ParseExpression(expressionText, ConfigurationItemFactory.Default);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static ConditionExpression ParseExpression(string expressionText, ConfigurationItemFactory configurationItemFactories)
		{
			if (expressionText == null)
			{
				return null;
			}
			ConditionParser conditionParser = new ConditionParser(new SimpleStringReader(expressionText), configurationItemFactories);
			ConditionExpression result = conditionParser.ParseExpression();
			if (!conditionParser.tokenizer.IsEOF())
			{
				throw new ConditionParseException("Unexpected token: " + conditionParser.tokenizer.TokenValue);
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003B08 File Offset: 0x00001D08
		internal static ConditionExpression ParseExpression(SimpleStringReader stringReader, ConfigurationItemFactory configurationItemFactories)
		{
			ConditionParser conditionParser = new ConditionParser(stringReader, configurationItemFactories);
			return conditionParser.ParseExpression();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003B28 File Offset: 0x00001D28
		private ConditionMethodExpression ParsePredicate(string functionName)
		{
			List<ConditionExpression> list = new List<ConditionExpression>();
			while (!this.tokenizer.IsEOF() && this.tokenizer.TokenType != ConditionTokenType.RightParen)
			{
				list.Add(this.ParseExpression());
				if (this.tokenizer.TokenType != ConditionTokenType.Comma)
				{
					break;
				}
				this.tokenizer.GetNextToken();
			}
			this.tokenizer.Expect(ConditionTokenType.RightParen);
			ConditionMethodExpression result;
			try
			{
				MethodInfo methodInfo = this.configurationItemFactory.ConditionMethods.CreateInstance(functionName);
				result = new ConditionMethodExpression(functionName, methodInfo, list);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Cannot resolve function '{0}'", new object[]
				{
					functionName
				});
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				throw new ConditionParseException("Cannot resolve function '" + functionName + "'", ex);
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003BF8 File Offset: 0x00001DF8
		private ConditionExpression ParseLiteralExpression()
		{
			if (this.tokenizer.IsToken(ConditionTokenType.LeftParen))
			{
				this.tokenizer.GetNextToken();
				ConditionExpression result = this.ParseExpression();
				this.tokenizer.Expect(ConditionTokenType.RightParen);
				return result;
			}
			if (this.tokenizer.IsToken(ConditionTokenType.Minus))
			{
				this.tokenizer.GetNextToken();
				if (!this.tokenizer.IsNumber())
				{
					throw new ConditionParseException("Number expected, got " + this.tokenizer.TokenType);
				}
				string tokenValue = this.tokenizer.TokenValue;
				this.tokenizer.GetNextToken();
				if (tokenValue.IndexOf('.') >= 0)
				{
					return new ConditionLiteralExpression(-double.Parse(tokenValue, CultureInfo.InvariantCulture));
				}
				return new ConditionLiteralExpression(-int.Parse(tokenValue, CultureInfo.InvariantCulture));
			}
			else if (this.tokenizer.IsNumber())
			{
				string tokenValue2 = this.tokenizer.TokenValue;
				this.tokenizer.GetNextToken();
				if (tokenValue2.IndexOf('.') >= 0)
				{
					return new ConditionLiteralExpression(double.Parse(tokenValue2, CultureInfo.InvariantCulture));
				}
				return new ConditionLiteralExpression(int.Parse(tokenValue2, CultureInfo.InvariantCulture));
			}
			else
			{
				if (this.tokenizer.TokenType == ConditionTokenType.String)
				{
					ConditionExpression result2 = new ConditionLayoutExpression(Layout.FromString(this.tokenizer.StringTokenValue, this.configurationItemFactory));
					this.tokenizer.GetNextToken();
					return result2;
				}
				if (this.tokenizer.TokenType == ConditionTokenType.Keyword)
				{
					string text = this.tokenizer.EatKeyword();
					if (string.Compare(text, "level", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLevelExpression();
					}
					if (string.Compare(text, "logger", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLoggerNameExpression();
					}
					if (string.Compare(text, "message", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionMessageExpression();
					}
					if (string.Compare(text, "loglevel", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.tokenizer.Expect(ConditionTokenType.Dot);
						return new ConditionLiteralExpression(LogLevel.FromString(this.tokenizer.EatKeyword()));
					}
					if (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLiteralExpression(true);
					}
					if (string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLiteralExpression(false);
					}
					if (string.Compare(text, "null", StringComparison.OrdinalIgnoreCase) == 0)
					{
						return new ConditionLiteralExpression(null);
					}
					if (this.tokenizer.TokenType == ConditionTokenType.LeftParen)
					{
						this.tokenizer.GetNextToken();
						return this.ParsePredicate(text);
					}
				}
				throw new ConditionParseException("Unexpected token: " + this.tokenizer.TokenValue);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003E80 File Offset: 0x00002080
		private ConditionExpression ParseBooleanRelation()
		{
			ConditionExpression conditionExpression = this.ParseLiteralExpression();
			if (this.tokenizer.IsToken(ConditionTokenType.EqualTo))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Equal);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.NotEqual))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.NotEqual);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.LessThan))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Less);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.GreaterThan))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Greater);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.LessThanOrEqualTo))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.LessOrEqual);
			}
			if (this.tokenizer.IsToken(ConditionTokenType.GreaterThanOrEqualTo))
			{
				this.tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.GreaterOrEqual);
			}
			return conditionExpression;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003F83 File Offset: 0x00002183
		private ConditionExpression ParseBooleanPredicate()
		{
			if (this.tokenizer.IsKeyword("not") || this.tokenizer.IsToken(ConditionTokenType.Not))
			{
				this.tokenizer.GetNextToken();
				return new ConditionNotExpression(this.ParseBooleanPredicate());
			}
			return this.ParseBooleanRelation();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003FC4 File Offset: 0x000021C4
		private ConditionExpression ParseBooleanAnd()
		{
			ConditionExpression conditionExpression = this.ParseBooleanPredicate();
			while (this.tokenizer.IsKeyword("and") || this.tokenizer.IsToken(ConditionTokenType.And))
			{
				this.tokenizer.GetNextToken();
				conditionExpression = new ConditionAndExpression(conditionExpression, this.ParseBooleanPredicate());
			}
			return conditionExpression;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004014 File Offset: 0x00002214
		private ConditionExpression ParseBooleanOr()
		{
			ConditionExpression conditionExpression = this.ParseBooleanAnd();
			while (this.tokenizer.IsKeyword("or") || this.tokenizer.IsToken(ConditionTokenType.Or))
			{
				this.tokenizer.GetNextToken();
				conditionExpression = new ConditionOrExpression(conditionExpression, this.ParseBooleanAnd());
			}
			return conditionExpression;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004064 File Offset: 0x00002264
		private ConditionExpression ParseBooleanExpression()
		{
			return this.ParseBooleanOr();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000406C File Offset: 0x0000226C
		private ConditionExpression ParseExpression()
		{
			return this.ParseBooleanExpression();
		}

		// Token: 0x0400003E RID: 62
		private readonly ConditionTokenizer tokenizer;

		// Token: 0x0400003F RID: 63
		private readonly ConfigurationItemFactory configurationItemFactory;
	}
}

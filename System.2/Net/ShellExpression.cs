using System;

namespace System.Net
{
	// Token: 0x0200020B RID: 523
	internal struct ShellExpression
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x00066987 File Offset: 0x00064B87
		internal ShellExpression(string pattern)
		{
			this.pattern = null;
			this.match = null;
			this.Parse(pattern);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000669A0 File Offset: 0x00064BA0
		internal bool IsMatch(string target)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool result = false;
			for (;;)
			{
				if (!flag)
				{
					if (num2 > target.Length)
					{
						return result;
					}
					switch (this.pattern[num])
					{
					case ShellExpression.ShExpTokens.End:
						if (num2 == target.Length)
						{
							goto Block_10;
						}
						flag = true;
						break;
					case ShellExpression.ShExpTokens.Start:
						if (num2 != 0)
						{
							return result;
						}
						this.match[num++] = 0;
						break;
					case ShellExpression.ShExpTokens.AugmentedQuestion:
						if (num2 == target.Length || target[num2] == '.')
						{
							this.match[num++] = num2;
						}
						else
						{
							num2 = (this.match[num++] = num2 + 1);
						}
						break;
					case ShellExpression.ShExpTokens.AugmentedAsterisk:
						if (num2 == target.Length || target[num2] == '.')
						{
							flag = true;
						}
						else
						{
							num2 = (this.match[num++] = num2 + 1);
						}
						break;
					case ShellExpression.ShExpTokens.AugmentedDot:
						if (num2 == target.Length)
						{
							this.match[num++] = num2;
						}
						else if (target[num2] == '.')
						{
							num2 = (this.match[num++] = num2 + 1);
						}
						else
						{
							flag = true;
						}
						break;
					case ShellExpression.ShExpTokens.Question:
						if (num2 == target.Length)
						{
							flag = true;
						}
						else
						{
							num2 = (this.match[num++] = num2 + 1);
						}
						break;
					case ShellExpression.ShExpTokens.Asterisk:
						num2 = (this.match[num++] = target.Length);
						break;
					default:
						if (num2 < target.Length && this.pattern[num] == (ShellExpression.ShExpTokens)char.ToLowerInvariant(target[num2]))
						{
							num2 = (this.match[num++] = num2 + 1);
						}
						else
						{
							flag = true;
						}
						break;
					}
				}
				else
				{
					switch (this.pattern[--num])
					{
					case ShellExpression.ShExpTokens.End:
					case ShellExpression.ShExpTokens.Start:
						return result;
					case ShellExpression.ShExpTokens.AugmentedQuestion:
					case ShellExpression.ShExpTokens.Asterisk:
						if (this.match[num] != this.match[num - 1])
						{
							int[] array = this.match;
							int num3 = num++;
							int num4 = array[num3] - 1;
							array[num3] = num4;
							num2 = num4;
							flag = false;
						}
						break;
					}
				}
			}
			Block_10:
			result = true;
			return result;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00066BC4 File Offset: 0x00064DC4
		private void Parse(string patString)
		{
			this.pattern = new ShellExpression.ShExpTokens[patString.Length + 2];
			this.match = null;
			int num = 0;
			this.pattern[num++] = ShellExpression.ShExpTokens.Start;
			for (int i = 0; i < patString.Length; i++)
			{
				char c = patString[i];
				if (c != '*')
				{
					if (c != '?')
					{
						if (c != '^')
						{
							this.pattern[num++] = (ShellExpression.ShExpTokens)char.ToLowerInvariant(patString[i]);
						}
						else
						{
							if (i >= patString.Length - 1)
							{
								this.pattern = null;
								if (Logging.On)
								{
									Logging.PrintWarning(Logging.Web, SR.GetString("net_log_shell_expression_pattern_format_warning", new object[]
									{
										patString
									}));
								}
								throw new FormatException(SR.GetString("net_format_shexp", new object[]
								{
									patString
								}));
							}
							i++;
							char c2 = patString[i];
							if (c2 != '*')
							{
								if (c2 != '.')
								{
									if (c2 != '?')
									{
										this.pattern = null;
										if (Logging.On)
										{
											Logging.PrintWarning(Logging.Web, SR.GetString("net_log_shell_expression_pattern_format_warning", new object[]
											{
												patString
											}));
										}
										throw new FormatException(SR.GetString("net_format_shexp", new object[]
										{
											patString
										}));
									}
									this.pattern[num++] = ShellExpression.ShExpTokens.AugmentedQuestion;
								}
								else
								{
									this.pattern[num++] = ShellExpression.ShExpTokens.AugmentedDot;
								}
							}
							else
							{
								this.pattern[num++] = ShellExpression.ShExpTokens.AugmentedAsterisk;
							}
						}
					}
					else
					{
						this.pattern[num++] = ShellExpression.ShExpTokens.Question;
					}
				}
				else
				{
					this.pattern[num++] = ShellExpression.ShExpTokens.Asterisk;
				}
			}
			this.pattern[num++] = ShellExpression.ShExpTokens.End;
			this.match = new int[num];
		}

		// Token: 0x0400156A RID: 5482
		private ShellExpression.ShExpTokens[] pattern;

		// Token: 0x0400156B RID: 5483
		private int[] match;

		// Token: 0x02000759 RID: 1881
		private enum ShExpTokens
		{
			// Token: 0x04003227 RID: 12839
			Asterisk = -1,
			// Token: 0x04003228 RID: 12840
			Question = -2,
			// Token: 0x04003229 RID: 12841
			AugmentedDot = -3,
			// Token: 0x0400322A RID: 12842
			AugmentedAsterisk = -4,
			// Token: 0x0400322B RID: 12843
			AugmentedQuestion = -5,
			// Token: 0x0400322C RID: 12844
			Start = -6,
			// Token: 0x0400322D RID: 12845
			End = -7
		}
	}
}

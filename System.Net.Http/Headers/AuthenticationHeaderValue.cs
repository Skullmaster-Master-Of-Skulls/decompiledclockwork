using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000023 RID: 35
	[__DynamicallyInvokable]
	public class AuthenticationHeaderValue : ICloneable
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006C5A File Offset: 0x00004E5A
		[__DynamicallyInvokable]
		public string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006C62 File Offset: 0x00004E62
		[__DynamicallyInvokable]
		public string Parameter
		{
			[__DynamicallyInvokable]
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006C6A File Offset: 0x00004E6A
		[__DynamicallyInvokable]
		public AuthenticationHeaderValue(string scheme) : this(scheme, null)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006C74 File Offset: 0x00004E74
		[__DynamicallyInvokable]
		public AuthenticationHeaderValue(string scheme, string parameter)
		{
			HeaderUtilities.CheckValidToken(scheme, "scheme");
			this.scheme = scheme;
			this.parameter = parameter;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006C95 File Offset: 0x00004E95
		private AuthenticationHeaderValue(AuthenticationHeaderValue source)
		{
			this.scheme = source.scheme;
			this.parameter = source.parameter;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006CB5 File Offset: 0x00004EB5
		private AuthenticationHeaderValue()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006CBD File Offset: 0x00004EBD
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.parameter))
			{
				return this.scheme;
			}
			return this.scheme + " " + this.parameter;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006CEC File Offset: 0x00004EEC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			AuthenticationHeaderValue authenticationHeaderValue = obj as AuthenticationHeaderValue;
			if (authenticationHeaderValue == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.parameter) && string.IsNullOrEmpty(authenticationHeaderValue.parameter))
			{
				return string.Compare(this.scheme, authenticationHeaderValue.scheme, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(this.scheme, authenticationHeaderValue.scheme, StringComparison.OrdinalIgnoreCase) == 0 && string.CompareOrdinal(this.parameter, authenticationHeaderValue.parameter) == 0;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006D60 File Offset: 0x00004F60
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = this.scheme.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.parameter))
			{
				num ^= this.parameter.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006D9C File Offset: 0x00004F9C
		[__DynamicallyInvokable]
		public static AuthenticationHeaderValue Parse(string input)
		{
			int num = 0;
			return (AuthenticationHeaderValue)GenericHeaderParser.SingleValueAuthenticationParser.ParseValue(input, null, ref num);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006DC0 File Offset: 0x00004FC0
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out AuthenticationHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueAuthenticationParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (AuthenticationHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006DF0 File Offset: 0x00004FF0
		internal static int GetAuthenticationLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			int tokenLength = HttpRuleParser.GetTokenLength(input, startIndex);
			if (tokenLength == 0)
			{
				return 0;
			}
			AuthenticationHeaderValue authenticationHeaderValue = new AuthenticationHeaderValue();
			authenticationHeaderValue.scheme = input.Substring(startIndex, tokenLength);
			int num = startIndex + tokenLength;
			int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, num);
			num += whitespaceLength;
			if (num == input.Length || input[num] == ',')
			{
				parsedValue = authenticationHeaderValue;
				return num - startIndex;
			}
			if (whitespaceLength == 0)
			{
				return 0;
			}
			int num2 = num;
			int num3 = num;
			if (!AuthenticationHeaderValue.TrySkipFirstBlob(input, ref num, ref num3))
			{
				return 0;
			}
			if (num < input.Length && !AuthenticationHeaderValue.TryGetParametersEndIndex(input, ref num, ref num3))
			{
				return 0;
			}
			authenticationHeaderValue.parameter = input.Substring(num2, num3 - num2 + 1);
			parsedValue = authenticationHeaderValue;
			return num - startIndex;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006EAC File Offset: 0x000050AC
		private static bool TrySkipFirstBlob(string input, ref int current, ref int parameterEndIndex)
		{
			while (current < input.Length && input[current] != ',')
			{
				if (input[current] == '"')
				{
					int num = 0;
					if (HttpRuleParser.GetQuotedStringLength(input, current, out num) != HttpParseResult.Parsed)
					{
						return false;
					}
					current += num;
					parameterEndIndex = current - 1;
				}
				else
				{
					int whitespaceLength = HttpRuleParser.GetWhitespaceLength(input, current);
					if (whitespaceLength == 0)
					{
						parameterEndIndex = current;
						current++;
					}
					else
					{
						current += whitespaceLength;
					}
				}
			}
			return true;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006F1C File Offset: 0x0000511C
		private static bool TryGetParametersEndIndex(string input, ref int parseEndIndex, ref int parameterEndIndex)
		{
			int num = parseEndIndex;
			for (;;)
			{
				num++;
				bool flag = false;
				num = HeaderUtilities.GetNextNonEmptyOrWhitespaceIndex(input, num, true, out flag);
				if (num == input.Length)
				{
					break;
				}
				int tokenLength = HttpRuleParser.GetTokenLength(input, num);
				if (tokenLength == 0)
				{
					return false;
				}
				num += tokenLength;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
				if (num == input.Length || input[num] != '=')
				{
					return true;
				}
				num++;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
				int valueLength = NameValueHeaderValue.GetValueLength(input, num);
				if (valueLength == 0)
				{
					return false;
				}
				num += valueLength;
				parameterEndIndex = num - 1;
				num += HttpRuleParser.GetWhitespaceLength(input, num);
				parseEndIndex = num;
				if (num >= input.Length || input[num] != ',')
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006FC2 File Offset: 0x000051C2
		object ICloneable.Clone()
		{
			return new AuthenticationHeaderValue(this);
		}

		// Token: 0x040000D5 RID: 213
		private string scheme;

		// Token: 0x040000D6 RID: 214
		private string parameter;
	}
}

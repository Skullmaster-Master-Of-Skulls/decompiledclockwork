using System;

namespace System.Net.Http.Headers
{
	// Token: 0x0200002B RID: 43
	[__DynamicallyInvokable]
	public class EntityTagHeaderValue : ICloneable
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000914F File Offset: 0x0000734F
		[__DynamicallyInvokable]
		public string Tag
		{
			[__DynamicallyInvokable]
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00009157 File Offset: 0x00007357
		[__DynamicallyInvokable]
		public bool IsWeak
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isWeak;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000915F File Offset: 0x0000735F
		[__DynamicallyInvokable]
		public static EntityTagHeaderValue Any
		{
			[__DynamicallyInvokable]
			get
			{
				if (EntityTagHeaderValue.any == null)
				{
					EntityTagHeaderValue.any = new EntityTagHeaderValue();
					EntityTagHeaderValue.any.tag = "*";
					EntityTagHeaderValue.any.isWeak = false;
				}
				return EntityTagHeaderValue.any;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009191 File Offset: 0x00007391
		[__DynamicallyInvokable]
		public EntityTagHeaderValue(string tag) : this(tag, false)
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000919C File Offset: 0x0000739C
		[__DynamicallyInvokable]
		public EntityTagHeaderValue(string tag, bool isWeak)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "tag");
			}
			int num = 0;
			if (HttpRuleParser.GetQuotedStringLength(tag, 0, out num) != HttpParseResult.Parsed || num != tag.Length)
			{
				throw new FormatException(SR.net_http_headers_invalid_etag_name);
			}
			this.tag = tag;
			this.isWeak = isWeak;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000091F6 File Offset: 0x000073F6
		private EntityTagHeaderValue(EntityTagHeaderValue source)
		{
			this.tag = source.tag;
			this.isWeak = source.isWeak;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009216 File Offset: 0x00007416
		private EntityTagHeaderValue()
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000921E File Offset: 0x0000741E
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.isWeak)
			{
				return "W/" + this.tag;
			}
			return this.tag;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009240 File Offset: 0x00007440
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			EntityTagHeaderValue entityTagHeaderValue = obj as EntityTagHeaderValue;
			return entityTagHeaderValue != null && this.isWeak == entityTagHeaderValue.isWeak && string.CompareOrdinal(this.tag, entityTagHeaderValue.tag) == 0;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000927D File Offset: 0x0000747D
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.tag.GetHashCode() ^ this.isWeak.GetHashCode();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009298 File Offset: 0x00007498
		[__DynamicallyInvokable]
		public static EntityTagHeaderValue Parse(string input)
		{
			int num = 0;
			return (EntityTagHeaderValue)GenericHeaderParser.SingleValueEntityTagParser.ParseValue(input, null, ref num);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000092BC File Offset: 0x000074BC
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out EntityTagHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueEntityTagParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (EntityTagHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000092EC File Offset: 0x000074EC
		internal static int GetEntityTagLength(string input, int startIndex, out EntityTagHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			bool flag = false;
			int num = startIndex;
			char c = input[startIndex];
			if (c == '*')
			{
				parsedValue = EntityTagHeaderValue.Any;
				num++;
			}
			else
			{
				if (c == 'W' || c == 'w')
				{
					num++;
					if (num + 2 >= input.Length || input[num] != '/')
					{
						return 0;
					}
					flag = true;
					num++;
					num += HttpRuleParser.GetWhitespaceLength(input, num);
				}
				int startIndex2 = num;
				int num2 = 0;
				if (HttpRuleParser.GetQuotedStringLength(input, num, out num2) != HttpParseResult.Parsed)
				{
					return 0;
				}
				parsedValue = new EntityTagHeaderValue();
				if (num2 == input.Length)
				{
					parsedValue.tag = input;
					parsedValue.isWeak = false;
				}
				else
				{
					parsedValue.tag = input.Substring(startIndex2, num2);
					parsedValue.isWeak = flag;
				}
				num += num2;
			}
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			return num - startIndex;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000093C5 File Offset: 0x000075C5
		object ICloneable.Clone()
		{
			if (this == EntityTagHeaderValue.any)
			{
				return EntityTagHeaderValue.any;
			}
			return new EntityTagHeaderValue(this);
		}

		// Token: 0x04000105 RID: 261
		private static EntityTagHeaderValue any;

		// Token: 0x04000106 RID: 262
		private string tag;

		// Token: 0x04000107 RID: 263
		private bool isWeak;
	}
}

using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000040 RID: 64
	[__DynamicallyInvokable]
	public class RangeConditionHeaderValue : ICloneable
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000D698 File Offset: 0x0000B898
		[__DynamicallyInvokable]
		public DateTimeOffset? Date
		{
			[__DynamicallyInvokable]
			get
			{
				return this.date;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		[__DynamicallyInvokable]
		public EntityTagHeaderValue EntityTag
		{
			[__DynamicallyInvokable]
			get
			{
				return this.entityTag;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		[__DynamicallyInvokable]
		public RangeConditionHeaderValue(DateTimeOffset date)
		{
			this.date = new DateTimeOffset?(date);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		[__DynamicallyInvokable]
		public RangeConditionHeaderValue(EntityTagHeaderValue entityTag)
		{
			if (entityTag == null)
			{
				throw new ArgumentNullException("entityTag");
			}
			this.entityTag = entityTag;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D6D9 File Offset: 0x0000B8D9
		[__DynamicallyInvokable]
		public RangeConditionHeaderValue(string entityTag) : this(new EntityTagHeaderValue(entityTag))
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D6E7 File Offset: 0x0000B8E7
		private RangeConditionHeaderValue(RangeConditionHeaderValue source)
		{
			this.entityTag = source.entityTag;
			this.date = source.date;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D707 File Offset: 0x0000B907
		private RangeConditionHeaderValue()
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D70F File Offset: 0x0000B90F
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.entityTag == null)
			{
				return HttpRuleParser.DateToString(this.date.Value);
			}
			return this.entityTag.ToString();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D738 File Offset: 0x0000B938
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			RangeConditionHeaderValue rangeConditionHeaderValue = obj as RangeConditionHeaderValue;
			if (rangeConditionHeaderValue == null)
			{
				return false;
			}
			if (this.entityTag == null)
			{
				return rangeConditionHeaderValue.date != null && this.date.Value == rangeConditionHeaderValue.date.Value;
			}
			return this.entityTag.Equals(rangeConditionHeaderValue.entityTag);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D798 File Offset: 0x0000B998
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.entityTag == null)
			{
				return this.date.Value.GetHashCode();
			}
			return this.entityTag.GetHashCode();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		[__DynamicallyInvokable]
		public static RangeConditionHeaderValue Parse(string input)
		{
			int num = 0;
			return (RangeConditionHeaderValue)GenericHeaderParser.RangeConditionParser.ParseValue(input, null, ref num);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out RangeConditionHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.RangeConditionParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (RangeConditionHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D828 File Offset: 0x0000BA28
		internal static int GetRangeConditionLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex + 1 >= input.Length)
			{
				return 0;
			}
			DateTimeOffset minValue = DateTimeOffset.MinValue;
			EntityTagHeaderValue entityTagHeaderValue = null;
			char c = input[startIndex];
			char c2 = input[startIndex + 1];
			int num;
			if (c == '"' || ((c == 'w' || c == 'W') && c2 == '/'))
			{
				int entityTagLength = EntityTagHeaderValue.GetEntityTagLength(input, startIndex, out entityTagHeaderValue);
				if (entityTagLength == 0)
				{
					return 0;
				}
				num = startIndex + entityTagLength;
				if (num != input.Length)
				{
					return 0;
				}
			}
			else
			{
				if (!HttpRuleParser.TryStringToDate(input.Substring(startIndex), out minValue))
				{
					return 0;
				}
				num = input.Length;
			}
			RangeConditionHeaderValue rangeConditionHeaderValue = new RangeConditionHeaderValue();
			if (entityTagHeaderValue == null)
			{
				rangeConditionHeaderValue.date = new DateTimeOffset?(minValue);
			}
			else
			{
				rangeConditionHeaderValue.entityTag = entityTagHeaderValue;
			}
			parsedValue = rangeConditionHeaderValue;
			return num - startIndex;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D8E1 File Offset: 0x0000BAE1
		object ICloneable.Clone()
		{
			return new RangeConditionHeaderValue(this);
		}

		// Token: 0x04000170 RID: 368
		private DateTimeOffset? date;

		// Token: 0x04000171 RID: 369
		private EntityTagHeaderValue entityTag;
	}
}

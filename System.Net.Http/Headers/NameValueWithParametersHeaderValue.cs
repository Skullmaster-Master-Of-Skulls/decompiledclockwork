using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003B RID: 59
	[__DynamicallyInvokable]
	public class NameValueWithParametersHeaderValue : NameValueHeaderValue, ICloneable
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000CF16 File Offset: 0x0000B116
		[__DynamicallyInvokable]
		public ICollection<NameValueHeaderValue> Parameters
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ObjectCollection<NameValueHeaderValue>();
				}
				return this.parameters;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000CF31 File Offset: 0x0000B131
		[__DynamicallyInvokable]
		public NameValueWithParametersHeaderValue(string name) : base(name)
		{
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000CF3A File Offset: 0x0000B13A
		[__DynamicallyInvokable]
		public NameValueWithParametersHeaderValue(string name, string value) : base(name, value)
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000CF44 File Offset: 0x0000B144
		internal NameValueWithParametersHeaderValue()
		{
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000CF4C File Offset: 0x0000B14C
		[__DynamicallyInvokable]
		protected NameValueWithParametersHeaderValue(NameValueWithParametersHeaderValue source) : base(source)
		{
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add((NameValueHeaderValue)((ICloneable)nameValueHeaderValue).Clone());
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			bool flag = base.Equals(obj);
			if (flag)
			{
				NameValueWithParametersHeaderValue nameValueWithParametersHeaderValue = obj as NameValueWithParametersHeaderValue;
				return nameValueWithParametersHeaderValue != null && HeaderUtilities.AreEqualCollections<NameValueHeaderValue>(this.parameters, nameValueWithParametersHeaderValue.parameters);
			}
			return false;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000CFEF File Offset: 0x0000B1EF
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ NameValueHeaderValue.GetHashCode(this.parameters);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000D003 File Offset: 0x0000B203
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return base.ToString() + NameValueHeaderValue.ToString(this.parameters, ';', true);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000D020 File Offset: 0x0000B220
		[__DynamicallyInvokable]
		public new static NameValueWithParametersHeaderValue Parse(string input)
		{
			int num = 0;
			return (NameValueWithParametersHeaderValue)GenericHeaderParser.SingleValueNameValueWithParametersParser.ParseValue(input, null, ref num);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D044 File Offset: 0x0000B244
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out NameValueWithParametersHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.SingleValueNameValueWithParametersParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (NameValueWithParametersHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000D074 File Offset: 0x0000B274
		internal static int GetNameValueWithParametersLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			NameValueHeaderValue nameValueHeaderValue = null;
			int nameValueLength = NameValueHeaderValue.GetNameValueLength(input, startIndex, NameValueWithParametersHeaderValue.nameValueCreator, out nameValueHeaderValue);
			if (nameValueLength == 0)
			{
				return 0;
			}
			int num = startIndex + nameValueLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			NameValueWithParametersHeaderValue nameValueWithParametersHeaderValue = nameValueHeaderValue as NameValueWithParametersHeaderValue;
			if (num >= input.Length || input[num] != ';')
			{
				parsedValue = nameValueWithParametersHeaderValue;
				return num - startIndex;
			}
			num++;
			int nameValueListLength = NameValueHeaderValue.GetNameValueListLength(input, num, ';', nameValueWithParametersHeaderValue.Parameters);
			if (nameValueListLength == 0)
			{
				return 0;
			}
			parsedValue = nameValueWithParametersHeaderValue;
			return num + nameValueListLength - startIndex;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000D101 File Offset: 0x0000B301
		private static NameValueHeaderValue CreateNameValue()
		{
			return new NameValueWithParametersHeaderValue();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000D108 File Offset: 0x0000B308
		object ICloneable.Clone()
		{
			return new NameValueWithParametersHeaderValue(this);
		}

		// Token: 0x04000165 RID: 357
		private static readonly Func<NameValueHeaderValue> nameValueCreator = new Func<NameValueHeaderValue>(NameValueWithParametersHeaderValue.CreateNameValue);

		// Token: 0x04000166 RID: 358
		private ICollection<NameValueHeaderValue> parameters;
	}
}

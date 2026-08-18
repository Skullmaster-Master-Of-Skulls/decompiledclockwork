using System;
using System.Collections;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000030 RID: 48
	internal abstract class HttpHeaderParser
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A4D5 File Offset: 0x000086D5
		public bool SupportsMultipleValues
		{
			get
			{
				return this.supportsMultipleValues;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A4DD File Offset: 0x000086DD
		public string Separator
		{
			get
			{
				return this.separator;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A4E5 File Offset: 0x000086E5
		public virtual IEqualityComparer Comparer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A4E8 File Offset: 0x000086E8
		protected HttpHeaderParser(bool supportsMultipleValues)
		{
			this.supportsMultipleValues = supportsMultipleValues;
			if (supportsMultipleValues)
			{
				this.separator = ", ";
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A505 File Offset: 0x00008705
		protected HttpHeaderParser(bool supportsMultipleValues, string separator)
		{
			this.supportsMultipleValues = supportsMultipleValues;
			this.separator = separator;
		}

		// Token: 0x06000279 RID: 633
		public abstract bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue);

		// Token: 0x0600027A RID: 634 RVA: 0x0000A51C File Offset: 0x0000871C
		public object ParseValue(string value, object storeValue, ref int index)
		{
			object result = null;
			if (!this.TryParseValue(value, storeValue, ref index, out result))
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					(value == null) ? "<null>" : value.Substring(index)
				}));
			}
			return result;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A569 File Offset: 0x00008769
		public virtual string ToString(object value)
		{
			return value.ToString();
		}

		// Token: 0x04000139 RID: 313
		internal const string DefaultSeparator = ", ";

		// Token: 0x0400013A RID: 314
		private bool supportsMultipleValues;

		// Token: 0x0400013B RID: 315
		private string separator;
	}
}

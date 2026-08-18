using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000781 RID: 1921
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDay : ISoapXsd
	{
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004477 RID: 17527 RVA: 0x000EA926 File Offset: 0x000E9926
		public static string XsdType
		{
			get
			{
				return "gDay";
			}
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000EA92D File Offset: 0x000E992D
		public string GetXsdType()
		{
			return SoapDay.XsdType;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x000EA934 File Offset: 0x000E9934
		public SoapDay()
		{
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000EA947 File Offset: 0x000E9947
		public SoapDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x000EA961 File Offset: 0x000E9961
		// (set) Token: 0x0600447C RID: 17532 RVA: 0x000EA969 File Offset: 0x000E9969
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000EA972 File Offset: 0x000E9972
		public override string ToString()
		{
			return this._value.ToString("---dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000EA989 File Offset: 0x000E9989
		public static SoapDay Parse(string value)
		{
			return new SoapDay(DateTime.ParseExact(value, SoapDay.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x0400224F RID: 8783
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002250 RID: 8784
		private static string[] formats = new string[]
		{
			"---dd",
			"---ddzzz"
		};
	}
}

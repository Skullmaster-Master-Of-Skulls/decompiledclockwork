using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200077C RID: 1916
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapTime : ISoapXsd
	{
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x000EA2E0 File Offset: 0x000E92E0
		public static string XsdType
		{
			get
			{
				return "time";
			}
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000EA2E7 File Offset: 0x000E92E7
		public string GetXsdType()
		{
			return SoapTime.XsdType;
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x000EA2EE File Offset: 0x000E92EE
		public SoapTime()
		{
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x000EA301 File Offset: 0x000E9301
		public SoapTime(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x000EA31B File Offset: 0x000E931B
		// (set) Token: 0x06004446 RID: 17478 RVA: 0x000EA324 File Offset: 0x000E9324
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = new DateTime(1, 1, 1, value.Hour, value.Minute, value.Second, value.Millisecond);
			}
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x000EA35B File Offset: 0x000E935B
		public override string ToString()
		{
			return this._value.ToString("HH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x06004448 RID: 17480 RVA: 0x000EA374 File Offset: 0x000E9374
		public static SoapTime Parse(string value)
		{
			string s = value;
			if (value.EndsWith("Z", StringComparison.Ordinal))
			{
				s = value.Substring(0, value.Length - 1) + "-00:00";
			}
			return new SoapTime(DateTime.ParseExact(s, SoapTime.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x04002242 RID: 8770
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002243 RID: 8771
		private static string[] formats = new string[]
		{
			"HH:mm:ss.fffffffzzz",
			"HH:mm:ss.ffff",
			"HH:mm:ss.ffffzzz",
			"HH:mm:ss.fff",
			"HH:mm:ss.fffzzz",
			"HH:mm:ss.ff",
			"HH:mm:ss.ffzzz",
			"HH:mm:ss.f",
			"HH:mm:ss.fzzz",
			"HH:mm:ss",
			"HH:mm:sszzz",
			"HH:mm:ss.fffff",
			"HH:mm:ss.fffffzzz",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.ffffffzzz",
			"HH:mm:ss.fffffff",
			"HH:mm:ss.ffffffff",
			"HH:mm:ss.ffffffffzzz",
			"HH:mm:ss.fffffffff",
			"HH:mm:ss.fffffffffzzz",
			"HH:mm:ss.fffffffff",
			"HH:mm:ss.fffffffffzzz"
		};
	}
}

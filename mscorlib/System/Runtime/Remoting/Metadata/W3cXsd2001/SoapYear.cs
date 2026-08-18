using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200077F RID: 1919
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYear : ISoapXsd
	{
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x000EA74A File Offset: 0x000E974A
		public static string XsdType
		{
			get
			{
				return "gYear";
			}
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x000EA751 File Offset: 0x000E9751
		public string GetXsdType()
		{
			return SoapYear.XsdType;
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x000EA758 File Offset: 0x000E9758
		public SoapYear()
		{
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000EA76B File Offset: 0x000E976B
		public SoapYear(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x000EA785 File Offset: 0x000E9785
		public SoapYear(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000EA7A6 File Offset: 0x000E97A6
		// (set) Token: 0x06004468 RID: 17512 RVA: 0x000EA7AE File Offset: 0x000E97AE
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

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x000EA7B7 File Offset: 0x000E97B7
		// (set) Token: 0x0600446A RID: 17514 RVA: 0x000EA7BF File Offset: 0x000E97BF
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x000EA7C8 File Offset: 0x000E97C8
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x000EA800 File Offset: 0x000E9800
		public static SoapYear Parse(string value)
		{
			int sign = 0;
			if (value[0] == '-')
			{
				sign = -1;
			}
			return new SoapYear(DateTime.ParseExact(value, SoapYear.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
		}

		// Token: 0x0400224A RID: 8778
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400224B RID: 8779
		private int _sign;

		// Token: 0x0400224C RID: 8780
		private static string[] formats = new string[]
		{
			"yyyy",
			"'+'yyyy",
			"'-'yyyy",
			"yyyyzzz",
			"'+'yyyyzzz",
			"'-'yyyyzzz"
		};
	}
}

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000780 RID: 1920
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x000EA87E File Offset: 0x000E987E
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x000EA885 File Offset: 0x000E9885
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x000EA88C File Offset: 0x000E988C
		public SoapMonthDay()
		{
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x000EA89F File Offset: 0x000E989F
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x000EA8B9 File Offset: 0x000E98B9
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x000EA8C1 File Offset: 0x000E98C1
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

		// Token: 0x06004474 RID: 17524 RVA: 0x000EA8CA File Offset: 0x000E98CA
		public override string ToString()
		{
			return this._value.ToString("'--'MM'-'dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000EA8E1 File Offset: 0x000E98E1
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x0400224D RID: 8781
		private DateTime _value = DateTime.MinValue;

		// Token: 0x0400224E RID: 8782
		private static string[] formats = new string[]
		{
			"--MM-dd",
			"--MM-ddzzz"
		};
	}
}

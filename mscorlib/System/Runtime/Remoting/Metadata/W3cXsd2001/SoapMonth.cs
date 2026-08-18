using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000782 RID: 1922
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonth : ISoapXsd
	{
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000EA9CE File Offset: 0x000E99CE
		public static string XsdType
		{
			get
			{
				return "gMonth";
			}
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x000EA9D5 File Offset: 0x000E99D5
		public string GetXsdType()
		{
			return SoapMonth.XsdType;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x000EA9DC File Offset: 0x000E99DC
		public SoapMonth()
		{
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000EA9EF File Offset: 0x000E99EF
		public SoapMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x000EAA09 File Offset: 0x000E9A09
		// (set) Token: 0x06004485 RID: 17541 RVA: 0x000EAA11 File Offset: 0x000E9A11
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

		// Token: 0x06004486 RID: 17542 RVA: 0x000EAA1A File Offset: 0x000E9A1A
		public override string ToString()
		{
			return this._value.ToString("--MM--", CultureInfo.InvariantCulture);
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x000EAA31 File Offset: 0x000E9A31
		public static SoapMonth Parse(string value)
		{
			return new SoapMonth(DateTime.ParseExact(value, SoapMonth.formats, CultureInfo.InvariantCulture, DateTimeStyles.None));
		}

		// Token: 0x04002251 RID: 8785
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002252 RID: 8786
		private static string[] formats = new string[]
		{
			"--MM--",
			"--MM--zzz"
		};
	}
}

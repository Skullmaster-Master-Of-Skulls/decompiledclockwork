using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000791 RID: 1937
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x000EB5FE File Offset: 0x000EA5FE
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000EB605 File Offset: 0x000EA605
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000EB60C File Offset: 0x000EA60C
		public SoapIdrefs()
		{
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x000EB614 File Offset: 0x000EA614
		public SoapIdrefs(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x000EB623 File Offset: 0x000EA623
		// (set) Token: 0x06004508 RID: 17672 RVA: 0x000EB62B File Offset: 0x000EA62B
		public string Value
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

		// Token: 0x06004509 RID: 17673 RVA: 0x000EB634 File Offset: 0x000EA634
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x000EB641 File Offset: 0x000EA641
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		// Token: 0x04002264 RID: 8804
		private string _value;
	}
}

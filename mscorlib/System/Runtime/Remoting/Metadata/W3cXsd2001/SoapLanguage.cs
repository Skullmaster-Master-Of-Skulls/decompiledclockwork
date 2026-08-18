using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200078F RID: 1935
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapLanguage : ISoapXsd
	{
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x000EB568 File Offset: 0x000EA568
		public static string XsdType
		{
			get
			{
				return "language";
			}
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x000EB56F File Offset: 0x000EA56F
		public string GetXsdType()
		{
			return SoapLanguage.XsdType;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x000EB576 File Offset: 0x000EA576
		public SoapLanguage()
		{
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000EB57E File Offset: 0x000EA57E
		public SoapLanguage(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x000EB58D File Offset: 0x000EA58D
		// (set) Token: 0x060044F8 RID: 17656 RVA: 0x000EB595 File Offset: 0x000EA595
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

		// Token: 0x060044F9 RID: 17657 RVA: 0x000EB59E File Offset: 0x000EA59E
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000EB5AB File Offset: 0x000EA5AB
		public static SoapLanguage Parse(string value)
		{
			return new SoapLanguage(value);
		}

		// Token: 0x04002262 RID: 8802
		private string _value;
	}
}

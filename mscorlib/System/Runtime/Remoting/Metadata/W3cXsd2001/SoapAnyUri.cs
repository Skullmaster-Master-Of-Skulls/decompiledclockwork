using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200078A RID: 1930
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x000EB1A8 File Offset: 0x000EA1A8
		public static string XsdType
		{
			get
			{
				return "anyURI";
			}
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000EB1AF File Offset: 0x000EA1AF
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000EB1B6 File Offset: 0x000EA1B6
		public SoapAnyUri()
		{
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x000EB1BE File Offset: 0x000EA1BE
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x000EB1CD File Offset: 0x000EA1CD
		// (set) Token: 0x060044C8 RID: 17608 RVA: 0x000EB1D5 File Offset: 0x000EA1D5
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

		// Token: 0x060044C9 RID: 17609 RVA: 0x000EB1DE File Offset: 0x000EA1DE
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x000EB1E6 File Offset: 0x000EA1E6
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		// Token: 0x0400225B RID: 8795
		private string _value;
	}
}

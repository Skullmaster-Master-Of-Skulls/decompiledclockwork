using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000794 RID: 1940
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000EB6DF File Offset: 0x000EA6DF
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000EB6E6 File Offset: 0x000EA6E6
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000EB6ED File Offset: 0x000EA6ED
		public SoapNmtokens()
		{
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000EB6F5 File Offset: 0x000EA6F5
		public SoapNmtokens(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x000EB704 File Offset: 0x000EA704
		// (set) Token: 0x06004520 RID: 17696 RVA: 0x000EB70C File Offset: 0x000EA70C
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

		// Token: 0x06004521 RID: 17697 RVA: 0x000EB715 File Offset: 0x000EA715
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000EB722 File Offset: 0x000EA722
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		// Token: 0x04002267 RID: 8807
		private string _value;
	}
}

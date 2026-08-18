using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000797 RID: 1943
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x000EB7C0 File Offset: 0x000EA7C0
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x000EB7C7 File Offset: 0x000EA7C7
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000EB7CE File Offset: 0x000EA7CE
		public SoapIdref()
		{
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000EB7D6 File Offset: 0x000EA7D6
		public SoapIdref(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x000EB7E5 File Offset: 0x000EA7E5
		// (set) Token: 0x06004538 RID: 17720 RVA: 0x000EB7ED File Offset: 0x000EA7ED
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

		// Token: 0x06004539 RID: 17721 RVA: 0x000EB7F6 File Offset: 0x000EA7F6
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x000EB803 File Offset: 0x000EA803
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		// Token: 0x0400226A RID: 8810
		private string _value;
	}
}

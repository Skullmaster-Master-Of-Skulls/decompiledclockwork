using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000795 RID: 1941
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x000EB72A File Offset: 0x000EA72A
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x000EB731 File Offset: 0x000EA731
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x000EB738 File Offset: 0x000EA738
		public SoapNcName()
		{
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x000EB740 File Offset: 0x000EA740
		public SoapNcName(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x000EB74F File Offset: 0x000EA74F
		// (set) Token: 0x06004528 RID: 17704 RVA: 0x000EB757 File Offset: 0x000EA757
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

		// Token: 0x06004529 RID: 17705 RVA: 0x000EB760 File Offset: 0x000EA760
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000EB76D File Offset: 0x000EA76D
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		// Token: 0x04002268 RID: 8808
		private string _value;
	}
}

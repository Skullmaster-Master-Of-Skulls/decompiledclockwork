using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000793 RID: 1939
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x000EB694 File Offset: 0x000EA694
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x000EB69B File Offset: 0x000EA69B
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000EB6A2 File Offset: 0x000EA6A2
		public SoapNmtoken()
		{
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000EB6AA File Offset: 0x000EA6AA
		public SoapNmtoken(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x000EB6B9 File Offset: 0x000EA6B9
		// (set) Token: 0x06004518 RID: 17688 RVA: 0x000EB6C1 File Offset: 0x000EA6C1
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

		// Token: 0x06004519 RID: 17689 RVA: 0x000EB6CA File Offset: 0x000EA6CA
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000EB6D7 File Offset: 0x000EA6D7
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		// Token: 0x04002266 RID: 8806
		private string _value;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200078C RID: 1932
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x000EB2F5 File Offset: 0x000EA2F5
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x000EB2FC File Offset: 0x000EA2FC
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x000EB303 File Offset: 0x000EA303
		public SoapNotation()
		{
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x000EB30B File Offset: 0x000EA30B
		public SoapNotation(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000EB31A File Offset: 0x000EA31A
		// (set) Token: 0x060044DE RID: 17630 RVA: 0x000EB322 File Offset: 0x000EA322
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

		// Token: 0x060044DF RID: 17631 RVA: 0x000EB32B File Offset: 0x000EA32B
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x000EB333 File Offset: 0x000EA333
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		// Token: 0x0400225F RID: 8799
		private string _value;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000796 RID: 1942
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x000EB775 File Offset: 0x000EA775
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x000EB77C File Offset: 0x000EA77C
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000EB783 File Offset: 0x000EA783
		public SoapId()
		{
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x000EB78B File Offset: 0x000EA78B
		public SoapId(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x000EB79A File Offset: 0x000EA79A
		// (set) Token: 0x06004530 RID: 17712 RVA: 0x000EB7A2 File Offset: 0x000EA7A2
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

		// Token: 0x06004531 RID: 17713 RVA: 0x000EB7AB File Offset: 0x000EA7AB
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x000EB7B8 File Offset: 0x000EA7B8
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		// Token: 0x04002269 RID: 8809
		private string _value;
	}
}

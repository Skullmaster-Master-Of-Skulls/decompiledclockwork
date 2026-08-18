using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000790 RID: 1936
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x000EB5B3 File Offset: 0x000EA5B3
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000EB5BA File Offset: 0x000EA5BA
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000EB5C1 File Offset: 0x000EA5C1
		public SoapName()
		{
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000EB5C9 File Offset: 0x000EA5C9
		public SoapName(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x000EB5D8 File Offset: 0x000EA5D8
		// (set) Token: 0x06004500 RID: 17664 RVA: 0x000EB5E0 File Offset: 0x000EA5E0
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

		// Token: 0x06004501 RID: 17665 RVA: 0x000EB5E9 File Offset: 0x000EA5E9
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000EB5F6 File Offset: 0x000EA5F6
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		// Token: 0x04002263 RID: 8803
		private string _value;
	}
}

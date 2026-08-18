using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x0200074B RID: 1867
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		// Token: 0x060042B3 RID: 17075 RVA: 0x000E2A7B File Offset: 0x000E1A7B
		public bool IsInteropXmlElement()
		{
			return (this._explicitlySet & SoapFieldAttribute.ExplicitlySet.XmlElementName) != SoapFieldAttribute.ExplicitlySet.None;
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000E2A8B File Offset: 0x000E1A8B
		// (set) Token: 0x060042B5 RID: 17077 RVA: 0x000E2AB9 File Offset: 0x000E1AB9
		public string XmlElementName
		{
			get
			{
				if (this._xmlElementName == null && this.ReflectInfo != null)
				{
					this._xmlElementName = ((FieldInfo)this.ReflectInfo).Name;
				}
				return this._xmlElementName;
			}
			set
			{
				this._xmlElementName = value;
				this._explicitlySet |= SoapFieldAttribute.ExplicitlySet.XmlElementName;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x000E2AD0 File Offset: 0x000E1AD0
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x000E2AD8 File Offset: 0x000E1AD8
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x04002182 RID: 8578
		private SoapFieldAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x04002183 RID: 8579
		private string _xmlElementName;

		// Token: 0x04002184 RID: 8580
		private int _order;

		// Token: 0x0200074C RID: 1868
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x04002186 RID: 8582
			None = 0,
			// Token: 0x04002187 RID: 8583
			XmlElementName = 1
		}
	}
}

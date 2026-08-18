using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000798 RID: 1944
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x000EB80B File Offset: 0x000EA80B
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000EB812 File Offset: 0x000EA812
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000EB819 File Offset: 0x000EA819
		public SoapEntity()
		{
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000EB821 File Offset: 0x000EA821
		public SoapEntity(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x000EB830 File Offset: 0x000EA830
		// (set) Token: 0x06004540 RID: 17728 RVA: 0x000EB838 File Offset: 0x000EA838
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

		// Token: 0x06004541 RID: 17729 RVA: 0x000EB841 File Offset: 0x000EA841
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x000EB84E File Offset: 0x000EA84E
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		// Token: 0x0400226B RID: 8811
		private string _value;
	}
}

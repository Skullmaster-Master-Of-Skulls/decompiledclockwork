using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002ED RID: 749
	[AttributeUsage(AttributeTargets.Field)]
	public class SoapEnumAttribute : Attribute
	{
		// Token: 0x060022FE RID: 8958 RVA: 0x000A45DF File Offset: 0x000A35DF
		public SoapEnumAttribute()
		{
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000A45E7 File Offset: 0x000A35E7
		public SoapEnumAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000A45F6 File Offset: 0x000A35F6
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x000A460C File Offset: 0x000A360C
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040014E3 RID: 5347
		private string name;
	}
}

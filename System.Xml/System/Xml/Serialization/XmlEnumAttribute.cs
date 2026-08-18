using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200030D RID: 781
	[AttributeUsage(AttributeTargets.Field)]
	public class XmlEnumAttribute : Attribute
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x000ADF79 File Offset: 0x000ACF79
		public XmlEnumAttribute()
		{
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000ADF81 File Offset: 0x000ACF81
		public XmlEnumAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000ADF90 File Offset: 0x000ACF90
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x000ADF98 File Offset: 0x000ACF98
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0400157F RID: 5503
		private string name;
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x02000384 RID: 900
	public class DynamicFieldConversionFieldInfoAttribute : Attribute
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public DynamicFieldConversionFieldInfoAttribute()
		{
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0001F867 File Offset: 0x0001DA67
		public DynamicFieldConversionFieldInfoAttribute(eControlCode controlCode)
		{
			this.ControlCode = controlCode;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0001F879 File Offset: 0x0001DA79
		public DynamicFieldConversionFieldInfoAttribute(eControlCode controlCode, int setting3Value)
		{
			this.ControlCode = controlCode;
			this.Setting3Value = new int?(setting3Value);
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0001F898 File Offset: 0x0001DA98
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		public eControlCode ControlCode { get; set; }

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0001F8A9 File Offset: 0x0001DAA9
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0001F8B1 File Offset: 0x0001DAB1
		public int? Setting3Value { get; set; }
	}
}

using System;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls
{
	// Token: 0x02000777 RID: 1911
	public class CustomControlBaseAttribute : Attribute
	{
		// Token: 0x0600274A RID: 10058 RVA: 0x00009924 File Offset: 0x00007B24
		public CustomControlBaseAttribute()
		{
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x00012653 File Offset: 0x00010853
		public CustomControlBaseAttribute(eCustomControlType controlType)
		{
			this.ControlType = controlType;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00012665 File Offset: 0x00010865
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x0001266D File Offset: 0x0001086D
		public eCustomControlType ControlType { get; set; }
	}
}

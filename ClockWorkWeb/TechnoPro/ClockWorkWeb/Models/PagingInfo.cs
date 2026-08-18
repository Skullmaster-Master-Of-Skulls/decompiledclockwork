using System;

namespace TechnoPro.ClockWorkWeb.Models
{
	// Token: 0x0200010C RID: 268
	public class PagingInfo
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0003A4C2 File Offset: 0x000386C2
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0003A4CA File Offset: 0x000386CA
		public int TotalItems { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0003A4D3 File Offset: 0x000386D3
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0003A4DB File Offset: 0x000386DB
		public int ItemsPerPage { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0003A4E4 File Offset: 0x000386E4
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0003A4EC File Offset: 0x000386EC
		public int CurrentPage { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0003A4F8 File Offset: 0x000386F8
		public int TotalPages
		{
			get
			{
				return (int)Math.Ceiling(this.TotalItems / this.ItemsPerPage);
			}
		}
	}
}

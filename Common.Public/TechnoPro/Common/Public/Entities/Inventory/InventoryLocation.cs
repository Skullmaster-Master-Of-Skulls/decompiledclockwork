using System;
using System.Text;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200031D RID: 797
	public class InventoryLocation : BusinessBase<int>
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0001D868 File Offset: 0x0001BA68
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0000E258 File Offset: 0x0000C458
		public int LocationId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0001D880 File Offset: 0x0001BA80
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x0001D888 File Offset: 0x0001BA88
		public string Campus { get; set; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0001D891 File Offset: 0x0001BA91
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0001D899 File Offset: 0x0001BA99
		public string Building { get; set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0001D8A2 File Offset: 0x0001BAA2
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x0001D8AA File Offset: 0x0001BAAA
		public string RoomNumber { get; set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0001D8B3 File Offset: 0x0001BAB3
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x0001D8BB File Offset: 0x0001BABB
		public string Seat { get; set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x0001D8CC File Offset: 0x0001BACC
		public string Notes { get; set; }

		// Token: 0x060018DE RID: 6366 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = !string.IsNullOrEmpty(this.Campus);
			if (flag)
			{
				stringBuilder.AppendFormat("Campus='{0}'", this.Campus);
			}
			bool flag2 = !string.IsNullOrEmpty(this.Building);
			if (flag2)
			{
				stringBuilder.AppendFormat((stringBuilder.Length > 0) ? " ,Building='{0}'" : "Building='{0}'", this.Building);
			}
			bool flag3 = !string.IsNullOrEmpty(this.RoomNumber);
			if (flag3)
			{
				stringBuilder.AppendFormat((stringBuilder.Length > 0) ? " ,RoomNumber='{0}'" : "RoomNumber='{0}'", this.RoomNumber);
			}
			bool flag4 = !string.IsNullOrEmpty(this.Seat);
			if (flag4)
			{
				stringBuilder.AppendFormat((stringBuilder.Length > 0) ? " ,Seat='{0}'" : "Seat='{0}'", this.Seat);
			}
			bool flag5 = stringBuilder.Length == 0 && !string.IsNullOrEmpty(this.Notes);
			if (flag5)
			{
				stringBuilder.AppendFormat("Notes='{0}'", this.Notes);
			}
			return stringBuilder.ToString();
		}
	}
}

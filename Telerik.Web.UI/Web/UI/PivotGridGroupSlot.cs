using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF5 RID: 3573
	[Serializable]
	public class PivotGridGroupSlot
	{
		// Token: 0x060084BC RID: 33980 RVA: 0x001E47E0 File Offset: 0x001E29E0
		public PivotGridGroupSlot(int slot, int level)
		{
			this.Slot = slot;
			this.Level = level;
		}

		// Token: 0x170029FB RID: 10747
		// (get) Token: 0x060084BD RID: 33981 RVA: 0x001E47F6 File Offset: 0x001E29F6
		// (set) Token: 0x060084BE RID: 33982 RVA: 0x001E47FE File Offset: 0x001E29FE
		public int Slot { get; set; }

		// Token: 0x170029FC RID: 10748
		// (get) Token: 0x060084BF RID: 33983 RVA: 0x001E4807 File Offset: 0x001E2A07
		// (set) Token: 0x060084C0 RID: 33984 RVA: 0x001E480F File Offset: 0x001E2A0F
		public int Level { get; set; }

		// Token: 0x060084C1 RID: 33985 RVA: 0x001E4818 File Offset: 0x001E2A18
		public override bool Equals(object obj)
		{
			PivotGridGroupSlot pivotGridGroupSlot = obj as PivotGridGroupSlot;
			if (pivotGridGroupSlot != null)
			{
				return this.Slot == pivotGridGroupSlot.Slot && this.Level == pivotGridGroupSlot.Level;
			}
			return base.Equals(obj);
		}

		// Token: 0x060084C2 RID: 33986 RVA: 0x001E4858 File Offset: 0x001E2A58
		public override int GetHashCode()
		{
			return this.Slot.GetHashCode() * this.Level.GetHashCode();
		}
	}
}

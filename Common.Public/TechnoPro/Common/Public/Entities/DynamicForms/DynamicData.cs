using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000370 RID: 880
	[Serializable]
	public class DynamicData : BusinessBase<int>, IDynamicDataHoldingObject, ICloneable
	{
		// Token: 0x06001B3E RID: 6974 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public DynamicData()
		{
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0001F304 File Offset: 0x0001D504
		public DynamicData(DynamicData item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.DataId = item.DataId;
				this.Value = item.Value;
				this.ValueId = item.ValueId;
				this.Field = ((item.Field == null) ? null : item.Field.Clone());
				this.SecondaryValue = item.SecondaryValue;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0001F374 File Offset: 0x0001D574
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int DataId
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

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0001F38C File Offset: 0x0001D58C
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0001F394 File Offset: 0x0001D594
		public object Value { get; set; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0001F39D File Offset: 0x0001D59D
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x0001F3A5 File Offset: 0x0001D5A5
		public int ValueId { get; set; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0001F3AE File Offset: 0x0001D5AE
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0001F3B6 File Offset: 0x0001D5B6
		public DynamicField Field { get; set; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0001F3BF File Offset: 0x0001D5BF
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x0001F3C7 File Offset: 0x0001D5C7
		public object SecondaryValue { get; set; }

		// Token: 0x06001B4A RID: 6986 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		public DynamicData GetDynamicData()
		{
			return this;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
		public DynamicData Clone()
		{
			return new DynamicData(this);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}

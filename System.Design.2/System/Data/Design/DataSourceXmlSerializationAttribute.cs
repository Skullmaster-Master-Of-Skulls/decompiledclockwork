using System;

namespace System.Data.Design
{
	// Token: 0x0200022A RID: 554
	internal abstract class DataSourceXmlSerializationAttribute : Attribute
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x000760B6 File Offset: 0x000742B6
		internal DataSourceXmlSerializationAttribute()
		{
			this.specialWay = false;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000760C5 File Offset: 0x000742C5
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x000760CD File Offset: 0x000742CD
		public Type ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000760D6 File Offset: 0x000742D6
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x000760DE File Offset: 0x000742DE
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

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x000760E7 File Offset: 0x000742E7
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x000760EF File Offset: 0x000742EF
		public bool SpecialWay
		{
			get
			{
				return this.specialWay;
			}
			set
			{
				this.specialWay = value;
			}
		}

		// Token: 0x04000ADF RID: 2783
		private bool specialWay;

		// Token: 0x04000AE0 RID: 2784
		private Type itemType;

		// Token: 0x04000AE1 RID: 2785
		private string name;
	}
}

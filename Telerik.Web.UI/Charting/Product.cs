using System;

namespace Telerik.Charting
{
	// Token: 0x020016F4 RID: 5876
	public class Product
	{
		// Token: 0x170045AC RID: 17836
		// (get) Token: 0x0600E43C RID: 58428 RVA: 0x0032AC03 File Offset: 0x00328E03
		// (set) Token: 0x0600E43D RID: 58429 RVA: 0x0032AC0B File Offset: 0x00328E0B
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x170045AD RID: 17837
		// (get) Token: 0x0600E43E RID: 58430 RVA: 0x0032AC14 File Offset: 0x00328E14
		// (set) Token: 0x0600E43F RID: 58431 RVA: 0x0032AC1C File Offset: 0x00328E1C
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170045AE RID: 17838
		// (get) Token: 0x0600E440 RID: 58432 RVA: 0x0032AC25 File Offset: 0x00328E25
		// (set) Token: 0x0600E441 RID: 58433 RVA: 0x0032AC2D File Offset: 0x00328E2D
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				this._amount = value;
			}
		}

		// Token: 0x0600E442 RID: 58434 RVA: 0x0032AC36 File Offset: 0x00328E36
		public Product(int idIn)
		{
			this.ID = idIn;
		}

		// Token: 0x0600E443 RID: 58435 RVA: 0x0032AC50 File Offset: 0x00328E50
		public Product(int idIn, string name, int val)
		{
			this._id = idIn;
			this._name = name;
			this._amount = val;
		}

		// Token: 0x040041E6 RID: 16870
		private int _id;

		// Token: 0x040041E7 RID: 16871
		private string _name = "";

		// Token: 0x040041E8 RID: 16872
		private int _amount;
	}
}

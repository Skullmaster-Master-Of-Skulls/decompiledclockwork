using System;
using System.Drawing;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200030A RID: 778
	public class ProductBarCode : BusinessBase<string>, IDisposable
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0001D160 File Offset: 0x0001B360
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string BarCodeId
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

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0001D178 File Offset: 0x0001B378
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x0001D180 File Offset: 0x0001B380
		public Image BarCodeImage { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0001D189 File Offset: 0x0001B389
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x0001D191 File Offset: 0x0001B391
		public string BarCodeDescription { get; set; }

		// Token: 0x06001824 RID: 6180 RVA: 0x0001D19C File Offset: 0x0001B39C
		~ProductBarCode()
		{
			this.Dispose(false);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
		private void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				bool flag2 = this.BarCodeImage != null;
				if (flag2)
				{
					this.BarCodeImage.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001415 RID: 5141
		protected bool disposed = false;
	}
}

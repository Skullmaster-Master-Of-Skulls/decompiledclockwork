using System;

namespace TechnoPro.Common.Public
{
	// Token: 0x020000BB RID: 187
	internal class CacheItem
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000E034 File Offset: 0x0000C234
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000E03C File Offset: 0x0000C23C
		public object ItemValue { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000E045 File Offset: 0x0000C245
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000E04D File Offset: 0x0000C24D
		public DateTime CreatedDate { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000E056 File Offset: 0x0000C256
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000E05E File Offset: 0x0000C25E
		public DateTime ExpirationDate { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000E067 File Offset: 0x0000C267
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000E06F File Offset: 0x0000C26F
		public TimeSpan SlidingExpirationTime { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000E078 File Offset: 0x0000C278
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000E080 File Offset: 0x0000C280
		public DateTime LastAccessTime { get; set; }

		// Token: 0x060004DA RID: 1242 RVA: 0x0000E089 File Offset: 0x0000C289
		public CacheItem(object itemValue)
		{
			this.LastAccessTime = DateTime.Now;
			this.ExpirationDate = DateTime.MaxValue;
			this.CreatedDate = DateTime.Now;
			this.ItemValue = itemValue;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		public CacheItem(object itemValue, TimeSpan expirationTime)
		{
			this.LastAccessTime = DateTime.Now;
			this.CreatedDate = DateTime.Now;
			this.ItemValue = itemValue;
			this.ExpirationDate = this.CreatedDate.Add(expirationTime);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000E10C File Offset: 0x0000C30C
		public CacheItem(object itemValue, TimeSpan expirationTime, bool slidingExpiration)
		{
			this.LastAccessTime = DateTime.Now;
			this.ExpirationDate = DateTime.MaxValue;
			this.CreatedDate = DateTime.Now;
			this.ItemValue = itemValue;
			if (slidingExpiration)
			{
				this.SlidingExpirationTime = expirationTime;
			}
			else
			{
				this.ExpirationDate = this.CreatedDate.Add(expirationTime);
			}
		}
	}
}

using System;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x0200087F RID: 2175
	internal class CacheKey
	{
		// Token: 0x06006660 RID: 26208 RVA: 0x00168DAC File Offset: 0x00166FAC
		internal CacheKey(string key, bool isPublic)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._key = key;
			if (isPublic)
			{
				this._bits = 32;
				return;
			}
			if (key[0] == "a"[0])
			{
				this._bits |= 64;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06006661 RID: 26209 RVA: 0x00168E04 File Offset: 0x00167004
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x06006662 RID: 26210 RVA: 0x00168E0C File Offset: 0x0016700C
		internal bool IsOutputCache
		{
			get
			{
				return (this._bits & 64) > 0;
			}
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06006663 RID: 26211 RVA: 0x00168E1A File Offset: 0x0016701A
		internal bool IsPublic
		{
			get
			{
				return (this._bits & 32) > 0;
			}
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x00168E28 File Offset: 0x00167028
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				if (!this.IsPublic || AppSettings.UseLegacyCacheKeyHash)
				{
					this._hashCode = this._key.GetHashCode();
				}
				else
				{
					this._hashCode = MarvinHash.ComputeHash32(this._key, MarvinHash.DefaultSeed);
				}
			}
			return this._hashCode;
		}

		// Token: 0x040034BA RID: 13498
		protected const byte BitPublic = 32;

		// Token: 0x040034BB RID: 13499
		protected const byte BitOutputCache = 64;

		// Token: 0x040034BC RID: 13500
		protected string _key;

		// Token: 0x040034BD RID: 13501
		protected byte _bits;

		// Token: 0x040034BE RID: 13502
		private int _hashCode;
	}
}

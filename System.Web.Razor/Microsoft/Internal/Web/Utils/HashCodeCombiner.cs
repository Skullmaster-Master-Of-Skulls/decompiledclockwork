using System;
using System.Collections;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000003 RID: 3
	internal class HashCodeCombiner
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002225 File Offset: 0x00000425
		public int CombinedHash
		{
			get
			{
				return this._combinedHash64.GetHashCode();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002234 File Offset: 0x00000434
		public HashCodeCombiner Add(IEnumerable e)
		{
			if (e == null)
			{
				this.Add(0);
			}
			else
			{
				int num = 0;
				foreach (object o in e)
				{
					this.Add(o);
					num++;
				}
				this.Add(num);
			}
			return this;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022A0 File Offset: 0x000004A0
		public HashCodeCombiner Add(int i)
		{
			this._combinedHash64 = ((this._combinedHash64 << 5) + this._combinedHash64 ^ (long)i);
			return this;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022BC File Offset: 0x000004BC
		public HashCodeCombiner Add(object o)
		{
			int i = (o != null) ? o.GetHashCode() : 0;
			this.Add(i);
			return this;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022DF File Offset: 0x000004DF
		public static HashCodeCombiner Start()
		{
			return new HashCodeCombiner();
		}

		// Token: 0x04000004 RID: 4
		private long _combinedHash64 = 5381L;
	}
}

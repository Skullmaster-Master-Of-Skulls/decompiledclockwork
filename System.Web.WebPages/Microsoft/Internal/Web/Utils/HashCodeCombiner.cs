using System;
using System.Collections;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000005 RID: 5
	internal class HashCodeCombiner
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002573 File Offset: 0x00000773
		public int CombinedHash
		{
			get
			{
				return this._combinedHash64.GetHashCode();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002580 File Offset: 0x00000780
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

		// Token: 0x0600001F RID: 31 RVA: 0x000025EC File Offset: 0x000007EC
		public HashCodeCombiner Add(int i)
		{
			this._combinedHash64 = ((this._combinedHash64 << 5) + this._combinedHash64 ^ (long)i);
			return this;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002608 File Offset: 0x00000808
		public HashCodeCombiner Add(object o)
		{
			int i = (o != null) ? o.GetHashCode() : 0;
			this.Add(i);
			return this;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000262B File Offset: 0x0000082B
		public static HashCodeCombiner Start()
		{
			return new HashCodeCombiner();
		}

		// Token: 0x04000005 RID: 5
		private long _combinedHash64 = 5381L;
	}
}

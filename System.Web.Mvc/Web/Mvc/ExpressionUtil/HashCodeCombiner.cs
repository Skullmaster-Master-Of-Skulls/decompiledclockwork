using System;
using System.Collections;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000AF RID: 175
	internal class HashCodeCombiner
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000DAA9 File Offset: 0x0000BCA9
		public int CombinedHash
		{
			get
			{
				return this._combinedHash64.GetHashCode();
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000DAB6 File Offset: 0x0000BCB6
		public void AddFingerprint(ExpressionFingerprint fingerprint)
		{
			if (fingerprint != null)
			{
				fingerprint.AddToHashCodeCombiner(this);
				return;
			}
			this.AddInt32(0);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000DACC File Offset: 0x0000BCCC
		public void AddEnumerable(IEnumerable e)
		{
			if (e == null)
			{
				this.AddInt32(0);
				return;
			}
			int num = 0;
			foreach (object o in e)
			{
				this.AddObject(o);
				num++;
			}
			this.AddInt32(num);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public void AddInt32(int i)
		{
			this._combinedHash64 = ((this._combinedHash64 << 5) + this._combinedHash64 ^ (long)i);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public void AddObject(object o)
		{
			int i = (o != null) ? o.GetHashCode() : 0;
			this.AddInt32(i);
		}

		// Token: 0x0400014F RID: 335
		private long _combinedHash64 = 5381L;
	}
}

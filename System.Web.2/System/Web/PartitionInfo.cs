using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000E3 RID: 227
	internal class PartitionInfo : IDisposable, IPartitionInfo
	{
		// Token: 0x06000E37 RID: 3639 RVA: 0x00028728 File Offset: 0x00026928
		internal PartitionInfo(ResourcePool rpool)
		{
			this._rpool = rpool;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00028737 File Offset: 0x00026937
		internal object RetrieveResource()
		{
			return this._rpool.RetrieveResource();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00028744 File Offset: 0x00026944
		internal void StoreResource(IDisposable o)
		{
			this._rpool.StoreResource(o);
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00028752 File Offset: 0x00026952
		protected virtual string TracingPartitionString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00028759 File Offset: 0x00026959
		string IPartitionInfo.GetTracingPartitionString()
		{
			return this.TracingPartitionString;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00028764 File Offset: 0x00026964
		public void Dispose()
		{
			if (this._rpool == null)
			{
				return;
			}
			lock (this)
			{
				if (this._rpool != null)
				{
					this._rpool.Dispose();
					this._rpool = null;
				}
			}
		}

		// Token: 0x04000557 RID: 1367
		private ResourcePool _rpool;
	}
}

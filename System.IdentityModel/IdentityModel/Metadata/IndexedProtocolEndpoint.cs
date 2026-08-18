using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F5 RID: 245
	public class IndexedProtocolEndpoint : ProtocolEndpoint
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001A9F2 File Offset: 0x00018BF2
		public IndexedProtocolEndpoint()
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001A9FA File Offset: 0x00018BFA
		public IndexedProtocolEndpoint(int index, Uri binding, Uri location) : base(binding, location)
		{
			this._index = index;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001AA0B File Offset: 0x00018C0B
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001AA13 File Offset: 0x00018C13
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001AA1C File Offset: 0x00018C1C
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0001AA24 File Offset: 0x00018C24
		public bool? IsDefault
		{
			get
			{
				return this._isDefault;
			}
			set
			{
				this._isDefault = value;
			}
		}

		// Token: 0x04000A71 RID: 2673
		private int _index;

		// Token: 0x04000A72 RID: 2674
		private bool? _isDefault;
	}
}

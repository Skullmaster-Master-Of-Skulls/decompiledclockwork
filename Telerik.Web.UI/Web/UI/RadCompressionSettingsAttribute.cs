using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001830 RID: 6192
	public class RadCompressionSettingsAttribute : Attribute
	{
		// Token: 0x170048C2 RID: 18626
		// (get) Token: 0x0600F0C6 RID: 61638 RVA: 0x0036BA69 File Offset: 0x00369C69
		// (set) Token: 0x0600F0C7 RID: 61639 RVA: 0x0036BA71 File Offset: 0x00369C71
		public CompressionType HttpCompression
		{
			get
			{
				return this._httpCompression;
			}
			set
			{
				this._httpCompression = value;
			}
		}

		// Token: 0x170048C3 RID: 18627
		// (get) Token: 0x0600F0C8 RID: 61640 RVA: 0x0036BA7A File Offset: 0x00369C7A
		// (set) Token: 0x0600F0C9 RID: 61641 RVA: 0x0036BA82 File Offset: 0x00369C82
		public CompressionType StateCompression
		{
			get
			{
				return this._stateCompression;
			}
			set
			{
				this._stateCompression = value;
			}
		}

		// Token: 0x170048C4 RID: 18628
		// (get) Token: 0x0600F0CA RID: 61642 RVA: 0x0036BA8B File Offset: 0x00369C8B
		// (set) Token: 0x0600F0CB RID: 61643 RVA: 0x0036BA93 File Offset: 0x00369C93
		public bool EnablePostbackCompression
		{
			get
			{
				return this._commpressRegularPostbacks;
			}
			set
			{
				this._commpressRegularPostbacks = value;
			}
		}

		// Token: 0x04004554 RID: 17748
		private CompressionType _httpCompression = CompressionType.GZip;

		// Token: 0x04004555 RID: 17749
		private CompressionType _stateCompression = CompressionType.GZip;

		// Token: 0x04004556 RID: 17750
		private bool _commpressRegularPostbacks;
	}
}

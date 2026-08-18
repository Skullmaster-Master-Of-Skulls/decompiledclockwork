using System;
using Telerik.Web.UI.Captcha;

namespace Telerik.Web.UI
{
	// Token: 0x020016CE RID: 5838
	internal class CaptchaImageHelper
	{
		// Token: 0x17004515 RID: 17685
		// (get) Token: 0x0600E153 RID: 57683 RVA: 0x003215AF File Offset: 0x0031F7AF
		public string Guid
		{
			get
			{
				return this._guid;
			}
		}

		// Token: 0x17004516 RID: 17686
		// (get) Token: 0x0600E154 RID: 57684 RVA: 0x003215B7 File Offset: 0x0031F7B7
		public string IsStoredInCache
		{
			get
			{
				return this._isStoredInCache;
			}
		}

		// Token: 0x0600E155 RID: 57685 RVA: 0x003215BF File Offset: 0x0031F7BF
		public CaptchaImageHelper(string guid, string isStoredInCache)
		{
			this._guid = guid;
			this._isStoredInCache = isStoredInCache;
		}

		// Token: 0x0600E156 RID: 57686 RVA: 0x003215D8 File Offset: 0x0031F7D8
		internal CaptchaImage GetCaptchaImage()
		{
			CaptchaImageStorage storage;
			if (this.IsStoredInCache == "false")
			{
				storage = CaptchaImageStorage.Session;
			}
			else if (this.IsStoredInCache == "cust")
			{
				storage = CaptchaImageStorage.Custom;
			}
			else
			{
				storage = CaptchaImageStorage.Cache;
			}
			return CachingProviderFactory.GetProviderByStorageType(storage).Provider.Load(this.Guid);
		}

		// Token: 0x04004149 RID: 16713
		private readonly string _guid;

		// Token: 0x0400414A RID: 16714
		private readonly string _isStoredInCache;
	}
}

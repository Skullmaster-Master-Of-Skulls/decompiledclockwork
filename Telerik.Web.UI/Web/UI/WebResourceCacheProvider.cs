using System;
using System.Configuration.Provider;

namespace Telerik.Web.UI
{
	// Token: 0x02000E79 RID: 3705
	public abstract class WebResourceCacheProvider : ProviderBase
	{
		// Token: 0x06008C77 RID: 35959
		public abstract void Initialize();

		// Token: 0x17002C65 RID: 11365
		// (get) Token: 0x06008C78 RID: 35960 RVA: 0x001FE38E File Offset: 0x001FC58E
		// (set) Token: 0x06008C79 RID: 35961 RVA: 0x001FE396 File Offset: 0x001FC596
		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			protected set
			{
				this._isInitialized = value;
			}
		}

		// Token: 0x06008C7A RID: 35962
		public abstract void Store(string resourceUid, string output);

		// Token: 0x06008C7B RID: 35963
		public abstract void Associate(string pageKey, string resourceUid);

		// Token: 0x06008C7C RID: 35964
		public abstract bool AreAssociated(string pageKey, string resourceUid);

		// Token: 0x06008C7D RID: 35965
		public abstract string Get(string resourceUid);

		// Token: 0x06008C7E RID: 35966
		public abstract bool Exists(string resourceUid);

		// Token: 0x06008C7F RID: 35967
		public abstract void Invalidate(string pageKey);

		// Token: 0x06008C80 RID: 35968
		public abstract void Invalidate();

		// Token: 0x04002773 RID: 10099
		private bool _isInitialized;
	}
}

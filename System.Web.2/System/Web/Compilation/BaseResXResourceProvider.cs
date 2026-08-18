using System;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x02000847 RID: 2119
	internal abstract class BaseResXResourceProvider : IResourceProvider
	{
		// Token: 0x060064AD RID: 25773 RVA: 0x00160B99 File Offset: 0x0015ED99
		public virtual object GetObject(string resourceKey, CultureInfo culture)
		{
			this.EnsureResourceManager();
			if (this._resourceManager == null)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentUICulture;
			}
			return this._resourceManager.GetObject(resourceKey, culture);
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x060064AE RID: 25774 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual IResourceReader ResourceReader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060064AF RID: 25775
		protected abstract ResourceManager CreateResourceManager();

		// Token: 0x060064B0 RID: 25776 RVA: 0x00160BC2 File Offset: 0x0015EDC2
		private void EnsureResourceManager()
		{
			if (this._resourceManager != null)
			{
				return;
			}
			this._resourceManager = this.CreateResourceManager();
		}

		// Token: 0x040033F5 RID: 13301
		private ResourceManager _resourceManager;
	}
}

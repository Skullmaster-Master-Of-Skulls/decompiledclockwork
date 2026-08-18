using System;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B6 RID: 438
	[ProvideProperty("Language", typeof(object))]
	[ProvideProperty("LoadLanguage", typeof(object))]
	[ProvideProperty("Localizable", typeof(object))]
	[Obsolete("This class has been deprecated. Use CodeDomLocalizationProvider instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
	public class LocalizationExtenderProvider : IExtenderProvider, IDisposable
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0005A750 File Offset: 0x00058950
		public LocalizationExtenderProvider(ISite serviceProvider, IComponent baseComponent)
		{
			this.serviceProvider = serviceProvider;
			this.baseComponent = baseComponent;
			if (serviceProvider != null)
			{
				IExtenderProviderService extenderProviderService = (IExtenderProviderService)serviceProvider.GetService(typeof(IExtenderProviderService));
				if (extenderProviderService != null)
				{
					extenderProviderService.AddExtenderProvider(this);
				}
			}
			this.language = CultureInfo.InvariantCulture;
			ResourceManager resourceManager = new ResourceManager(baseComponent.GetType());
			if (resourceManager != null)
			{
				ResourceSet resourceSet = resourceManager.GetResourceSet(this.language, true, false);
				if (resourceSet != null)
				{
					object @object = resourceSet.GetObject("$this.Localizable");
					if (@object is bool)
					{
						this.defaultLocalizable = (bool)@object;
						this.localizable = this.defaultLocalizable;
					}
				}
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0005A7EC File Offset: 0x000589EC
		private CultureInfo ThreadDefaultLanguage
		{
			get
			{
				object obj = LocalizationExtenderProvider.localizationLock;
				lock (obj)
				{
					if (this.defaultLanguage != null)
					{
						return this.defaultLanguage;
					}
					LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("_Thread_Default_Language");
					if (namedDataSlot == null)
					{
						return null;
					}
					this.defaultLanguage = (CultureInfo)Thread.GetData(namedDataSlot);
					if (this.defaultLanguage == null)
					{
						this.defaultLanguage = Application.CurrentCulture;
						Thread.SetData(namedDataSlot, this.defaultLanguage);
					}
				}
				return this.defaultLanguage;
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0005A880 File Offset: 0x00058A80
		[DesignOnly(true)]
		[Localizable(true)]
		[SRDescription("ParentControlDesignerLanguageDescr")]
		public CultureInfo GetLanguage(object o)
		{
			return this.language;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0005A888 File Offset: 0x00058A88
		[DesignOnly(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CultureInfo GetLoadLanguage(object o)
		{
			if (this.loadLanguage == null)
			{
				this.loadLanguage = CultureInfo.InvariantCulture;
			}
			return this.loadLanguage;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0005A8A3 File Offset: 0x00058AA3
		[DesignOnly(true)]
		[Localizable(true)]
		[SRDescription("ParentControlDesignerLocalizableDescr")]
		public bool GetLocalizable(object o)
		{
			return this.localizable;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0005A8AC File Offset: 0x00058AAC
		public void SetLanguage(object o, CultureInfo language)
		{
			if (language == null)
			{
				language = CultureInfo.InvariantCulture;
			}
			if (this.language.Equals(language))
			{
				return;
			}
			bool flag = language.Equals(CultureInfo.InvariantCulture);
			CultureInfo threadDefaultLanguage = this.ThreadDefaultLanguage;
			this.language = language;
			if (!flag)
			{
				this.SetLocalizable(null, true);
			}
			if (this.serviceProvider != null)
			{
				IDesignerLoaderService designerLoaderService = (IDesignerLoaderService)this.serviceProvider.GetService(typeof(IDesignerLoaderService));
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					if (designerHost.Loading)
					{
						this.loadLanguage = language;
						return;
					}
					bool flag2 = false;
					if (designerLoaderService != null)
					{
						flag2 = designerLoaderService.Reload();
					}
					if (!flag2)
					{
						IUIService iuiservice = (IUIService)this.serviceProvider.GetService(typeof(IUIService));
						if (iuiservice != null)
						{
							iuiservice.ShowMessage(SR.GetString("LocalizerManualReload"));
						}
					}
				}
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005A98E File Offset: 0x00058B8E
		public void SetLocalizable(object o, bool localizable)
		{
			this.localizable = localizable;
			if (!localizable)
			{
				this.SetLanguage(null, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005A9A6 File Offset: 0x00058BA6
		public bool ShouldSerializeLanguage(object o)
		{
			return this.language != null && this.language != CultureInfo.InvariantCulture;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0005A9C2 File Offset: 0x00058BC2
		private bool ShouldSerializeLocalizable(object o)
		{
			return this.localizable != this.defaultLocalizable;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0005A9D5 File Offset: 0x00058BD5
		private void ResetLocalizable(object o)
		{
			this.SetLocalizable(null, this.defaultLocalizable);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005A9E4 File Offset: 0x00058BE4
		public void ResetLanguage(object o)
		{
			this.SetLanguage(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005A9F2 File Offset: 0x00058BF2
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005A9FC File Offset: 0x00058BFC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.serviceProvider != null)
			{
				IExtenderProviderService extenderProviderService = (IExtenderProviderService)this.serviceProvider.GetService(typeof(IExtenderProviderService));
				if (extenderProviderService != null)
				{
					extenderProviderService.RemoveExtenderProvider(this);
				}
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0005AA39 File Offset: 0x00058C39
		public bool CanExtend(object o)
		{
			return o.Equals(this.baseComponent);
		}

		// Token: 0x04000943 RID: 2371
		private IServiceProvider serviceProvider;

		// Token: 0x04000944 RID: 2372
		private IComponent baseComponent;

		// Token: 0x04000945 RID: 2373
		private bool localizable;

		// Token: 0x04000946 RID: 2374
		private bool defaultLocalizable;

		// Token: 0x04000947 RID: 2375
		private CultureInfo language;

		// Token: 0x04000948 RID: 2376
		private CultureInfo loadLanguage;

		// Token: 0x04000949 RID: 2377
		private CultureInfo defaultLanguage;

		// Token: 0x0400094A RID: 2378
		private const string KeyThreadDefaultLanguage = "_Thread_Default_Language";

		// Token: 0x0400094B RID: 2379
		private static object localizationLock = new object();
	}
}

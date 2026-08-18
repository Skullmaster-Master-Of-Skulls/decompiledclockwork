using System;
using System.Collections;
using System.Design;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001F2 RID: 498
	public sealed class CodeDomLocalizationProvider : IDisposable, IDesignerSerializationProvider
	{
		// Token: 0x060012C7 RID: 4807 RVA: 0x0006DC0B File Offset: 0x0006BE0B
		public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._model = model;
			this.Initialize(provider);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0006DC30 File Offset: 0x0006BE30
		public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model, CultureInfo[] supportedCultures)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (supportedCultures == null)
			{
				throw new ArgumentNullException("supportedCultures");
			}
			this._model = model;
			this._supportedCultures = (CultureInfo[])supportedCultures.Clone();
			this.Initialize(provider);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0006DC7E File Offset: 0x0006BE7E
		public void Dispose()
		{
			if (this._providerService != null && this._extender != null)
			{
				this._providerService.RemoveExtenderProvider(this._extender);
				this._providerService = null;
				this._extender = null;
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0006DCB0 File Offset: 0x0006BEB0
		private void Initialize(IServiceProvider provider)
		{
			this._providerService = (provider.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService);
			if (this._providerService == null)
			{
				throw new NotSupportedException(SR.GetString("LocalizationProviderMissingService", new object[]
				{
					typeof(IExtenderProviderService).Name
				}));
			}
			this._extender = new CodeDomLocalizationProvider.LanguageExtenders(provider, this._supportedCultures);
			this._providerService.AddExtenderProvider(this._extender);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0006DD2C File Offset: 0x0006BF2C
		private object GetCodeDomSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (currentSerializer == null)
			{
				return null;
			}
			if (typeof(ResourceManager).IsAssignableFrom(objectType))
			{
				return null;
			}
			CodeDomLocalizationModel codeDomLocalizationModel = CodeDomLocalizationModel.None;
			object obj = manager.Context[typeof(CodeDomLocalizationModel)];
			if (obj != null)
			{
				codeDomLocalizationModel = (CodeDomLocalizationModel)obj;
			}
			if (codeDomLocalizationModel != CodeDomLocalizationModel.None)
			{
				return new LocalizationCodeDomSerializer(codeDomLocalizationModel, currentSerializer);
			}
			return null;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0006DD80 File Offset: 0x0006BF80
		private object GetMemberCodeDomSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			CodeDomLocalizationModel codeDomLocalizationModel = this._model;
			if (!typeof(PropertyDescriptor).IsAssignableFrom(objectType))
			{
				return null;
			}
			if (currentSerializer == null)
			{
				return null;
			}
			if (currentSerializer is ResourcePropertyMemberCodeDomSerializer)
			{
				return null;
			}
			if (this._extender == null || !this._extender.GetLocalizable(null))
			{
				return null;
			}
			PropertyDescriptor propertyDescriptor = manager.Context[typeof(PropertyDescriptor)] as PropertyDescriptor;
			if (propertyDescriptor == null || !propertyDescriptor.IsLocalizable)
			{
				codeDomLocalizationModel = CodeDomLocalizationModel.None;
			}
			if (this._memberSerializers == null)
			{
				this._memberSerializers = new Hashtable();
			}
			if (this._nopMemberSerializers == null)
			{
				this._nopMemberSerializers = new Hashtable();
			}
			object obj;
			if (codeDomLocalizationModel == CodeDomLocalizationModel.None)
			{
				obj = this._nopMemberSerializers[currentSerializer];
			}
			else
			{
				obj = this._memberSerializers[currentSerializer];
			}
			if (obj == null)
			{
				obj = new ResourcePropertyMemberCodeDomSerializer((MemberCodeDomSerializer)currentSerializer, this._extender, codeDomLocalizationModel);
				if (codeDomLocalizationModel == CodeDomLocalizationModel.None)
				{
					this._nopMemberSerializers[currentSerializer] = obj;
				}
				else
				{
					this._memberSerializers[currentSerializer] = obj;
				}
			}
			return obj;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0006DE74 File Offset: 0x0006C074
		object IDesignerSerializationProvider.GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (serializerType == typeof(CodeDomSerializer))
			{
				return this.GetCodeDomSerializer(manager, currentSerializer, objectType, serializerType);
			}
			if (serializerType == typeof(MemberCodeDomSerializer))
			{
				return this.GetMemberCodeDomSerializer(manager, currentSerializer, objectType, serializerType);
			}
			return null;
		}

		// Token: 0x04000A36 RID: 2614
		private IExtenderProviderService _providerService;

		// Token: 0x04000A37 RID: 2615
		private CodeDomLocalizationModel _model;

		// Token: 0x04000A38 RID: 2616
		private CultureInfo[] _supportedCultures;

		// Token: 0x04000A39 RID: 2617
		private CodeDomLocalizationProvider.LanguageExtenders _extender;

		// Token: 0x04000A3A RID: 2618
		private Hashtable _memberSerializers;

		// Token: 0x04000A3B RID: 2619
		private Hashtable _nopMemberSerializers;

		// Token: 0x020004B4 RID: 1204
		[ProvideProperty("Language", typeof(IComponent))]
		[ProvideProperty("LoadLanguage", typeof(IComponent))]
		[ProvideProperty("Localizable", typeof(IComponent))]
		internal class LanguageExtenders : IExtenderProvider
		{
			// Token: 0x06002C09 RID: 11273 RVA: 0x00106A24 File Offset: 0x00104C24
			public LanguageExtenders(IServiceProvider serviceProvider, CultureInfo[] supportedCultures)
			{
				this._serviceProvider = serviceProvider;
				this._host = (serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost);
				this._language = CultureInfo.InvariantCulture;
				if (supportedCultures != null)
				{
					this._supportedCultures = new TypeConverter.StandardValuesCollection(supportedCultures);
				}
			}

			// Token: 0x1700094C RID: 2380
			// (get) Token: 0x06002C0A RID: 11274 RVA: 0x00106A73 File Offset: 0x00104C73
			internal TypeConverter.StandardValuesCollection SupportedCultures
			{
				get
				{
					return this._supportedCultures;
				}
			}

			// Token: 0x1700094D RID: 2381
			// (get) Token: 0x06002C0B RID: 11275 RVA: 0x00106A7B File Offset: 0x00104C7B
			private CultureInfo ThreadDefaultLanguage
			{
				get
				{
					if (this._defaultLanguage == null)
					{
						this._defaultLanguage = Application.CurrentCulture;
					}
					return this._defaultLanguage;
				}
			}

			// Token: 0x06002C0C RID: 11276 RVA: 0x00106A98 File Offset: 0x00104C98
			private void BroadcastGlobalChange(IComponent comp)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					IComponentChangeService componentChangeService = site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
					IContainer container = site.GetService(typeof(IContainer)) as IContainer;
					if (componentChangeService != null && container != null)
					{
						foreach (object obj in container.Components)
						{
							IComponent component = (IComponent)obj;
							componentChangeService.OnComponentChanging(component, null);
							componentChangeService.OnComponentChanged(component, null, null, null);
						}
					}
				}
			}

			// Token: 0x06002C0D RID: 11277 RVA: 0x00106B40 File Offset: 0x00104D40
			private void CheckRoot()
			{
				if (this._host != null && this._host.RootComponent != this._lastRoot)
				{
					this._lastRoot = this._host.RootComponent;
					this._language = CultureInfo.InvariantCulture;
					this._loadLanguage = null;
					this._localizable = false;
				}
			}

			// Token: 0x06002C0E RID: 11278 RVA: 0x00106B92 File Offset: 0x00104D92
			[DesignOnly(true)]
			[TypeConverter(typeof(CodeDomLocalizationProvider.LanguageCultureInfoConverter))]
			[Category("Design")]
			[SRDescription("LocalizationProviderLanguageDescr")]
			public CultureInfo GetLanguage(IComponent o)
			{
				this.CheckRoot();
				return this._language;
			}

			// Token: 0x06002C0F RID: 11279 RVA: 0x00106BA0 File Offset: 0x00104DA0
			[DesignOnly(true)]
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public CultureInfo GetLoadLanguage(IComponent o)
			{
				this.CheckRoot();
				if (this._loadLanguage == null)
				{
					this._loadLanguage = CultureInfo.InvariantCulture;
				}
				return this._loadLanguage;
			}

			// Token: 0x06002C10 RID: 11280 RVA: 0x00106BC1 File Offset: 0x00104DC1
			[DesignOnly(true)]
			[Category("Design")]
			[SRDescription("LocalizationProviderLocalizableDescr")]
			public bool GetLocalizable(IComponent o)
			{
				this.CheckRoot();
				return this._localizable;
			}

			// Token: 0x06002C11 RID: 11281 RVA: 0x00106BD0 File Offset: 0x00104DD0
			public void SetLanguage(IComponent o, CultureInfo language)
			{
				this.CheckRoot();
				if (language == null)
				{
					language = CultureInfo.InvariantCulture;
				}
				bool flag = language.Equals(CultureInfo.InvariantCulture);
				if (this._language.Equals(language))
				{
					return;
				}
				this._language = language;
				if (!flag)
				{
					this.SetLocalizable(o, true);
				}
				if (this._serviceProvider != null && this._host != null)
				{
					IDesignerLoaderService designerLoaderService = this._serviceProvider.GetService(typeof(IDesignerLoaderService)) as IDesignerLoaderService;
					if (this._host.Loading)
					{
						this._loadLanguage = language;
						return;
					}
					bool flag2 = false;
					if (designerLoaderService != null)
					{
						flag2 = designerLoaderService.Reload();
					}
					if (!flag2)
					{
						IUIService iuiservice = (IUIService)this._serviceProvider.GetService(typeof(IUIService));
						if (iuiservice != null)
						{
							iuiservice.ShowMessage(SR.GetString("LocalizationProviderManualReload"));
						}
					}
				}
			}

			// Token: 0x06002C12 RID: 11282 RVA: 0x00106C98 File Offset: 0x00104E98
			public void SetLocalizable(IComponent o, bool localizable)
			{
				this.CheckRoot();
				if (localizable != this._localizable)
				{
					this._localizable = localizable;
					if (!localizable)
					{
						this.SetLanguage(o, CultureInfo.InvariantCulture);
					}
					if (this._host != null && !this._host.Loading)
					{
						this.BroadcastGlobalChange(o);
					}
				}
			}

			// Token: 0x06002C13 RID: 11283 RVA: 0x00106CE6 File Offset: 0x00104EE6
			private bool ShouldSerializeLanguage(IComponent o)
			{
				return this._language != null && this._language != CultureInfo.InvariantCulture;
			}

			// Token: 0x06002C14 RID: 11284 RVA: 0x00106D02 File Offset: 0x00104F02
			private bool ShouldSerializeLocalizable(IComponent o)
			{
				return this._localizable;
			}

			// Token: 0x06002C15 RID: 11285 RVA: 0x00106D0A File Offset: 0x00104F0A
			private void ResetLocalizable(IComponent o)
			{
				this.SetLocalizable(o, false);
			}

			// Token: 0x06002C16 RID: 11286 RVA: 0x00106D14 File Offset: 0x00104F14
			private void ResetLanguage(IComponent o)
			{
				this.SetLanguage(o, CultureInfo.InvariantCulture);
			}

			// Token: 0x06002C17 RID: 11287 RVA: 0x00106D22 File Offset: 0x00104F22
			public bool CanExtend(object o)
			{
				this.CheckRoot();
				return this._host != null && o == this._host.RootComponent;
			}

			// Token: 0x04001E88 RID: 7816
			private IServiceProvider _serviceProvider;

			// Token: 0x04001E89 RID: 7817
			private IDesignerHost _host;

			// Token: 0x04001E8A RID: 7818
			private IComponent _lastRoot;

			// Token: 0x04001E8B RID: 7819
			private TypeConverter.StandardValuesCollection _supportedCultures;

			// Token: 0x04001E8C RID: 7820
			private bool _localizable;

			// Token: 0x04001E8D RID: 7821
			private CultureInfo _language;

			// Token: 0x04001E8E RID: 7822
			private CultureInfo _loadLanguage;

			// Token: 0x04001E8F RID: 7823
			private CultureInfo _defaultLanguage;
		}

		// Token: 0x020004B5 RID: 1205
		internal sealed class LanguageCultureInfoConverter : CultureInfoConverter
		{
			// Token: 0x06002C18 RID: 11288 RVA: 0x00106D42 File Offset: 0x00104F42
			protected override string GetCultureName(CultureInfo culture)
			{
				return culture.DisplayName;
			}

			// Token: 0x06002C19 RID: 11289 RVA: 0x00106D4C File Offset: 0x00104F4C
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = null;
				if (context.PropertyDescriptor != null)
				{
					ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = context.PropertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
					if (extenderProvidedPropertyAttribute != null)
					{
						CodeDomLocalizationProvider.LanguageExtenders languageExtenders = extenderProvidedPropertyAttribute.Provider as CodeDomLocalizationProvider.LanguageExtenders;
						if (languageExtenders != null)
						{
							standardValuesCollection = languageExtenders.SupportedCultures;
						}
					}
				}
				if (standardValuesCollection == null)
				{
					standardValuesCollection = base.GetStandardValues(context);
				}
				return standardValuesCollection;
			}
		}
	}
}

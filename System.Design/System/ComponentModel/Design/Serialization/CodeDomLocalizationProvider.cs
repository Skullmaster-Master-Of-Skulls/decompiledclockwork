using System;
using System.Collections;
using System.Design;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000589 RID: 1417
	public sealed class CodeDomLocalizationProvider : IDisposable, IDesignerSerializationProvider
	{
		// Token: 0x0600324B RID: 12875 RVA: 0x0011CBC2 File Offset: 0x0011BBC2
		public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._model = model;
			this.Initialize(provider);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x0011CBE8 File Offset: 0x0011BBE8
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

		// Token: 0x0600324D RID: 12877 RVA: 0x0011CC36 File Offset: 0x0011BC36
		public void Dispose()
		{
			if (this._providerService != null && this._extender != null)
			{
				this._providerService.RemoveExtenderProvider(this._extender);
				this._providerService = null;
				this._extender = null;
			}
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x0011CC68 File Offset: 0x0011BC68
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

		// Token: 0x0600324F RID: 12879 RVA: 0x0011CCE8 File Offset: 0x0011BCE8
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

		// Token: 0x06003250 RID: 12880 RVA: 0x0011CD3C File Offset: 0x0011BD3C
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

		// Token: 0x06003251 RID: 12881 RVA: 0x0011CE30 File Offset: 0x0011BE30
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

		// Token: 0x04002169 RID: 8553
		private IExtenderProviderService _providerService;

		// Token: 0x0400216A RID: 8554
		private CodeDomLocalizationModel _model;

		// Token: 0x0400216B RID: 8555
		private CultureInfo[] _supportedCultures;

		// Token: 0x0400216C RID: 8556
		private CodeDomLocalizationProvider.LanguageExtenders _extender;

		// Token: 0x0400216D RID: 8557
		private Hashtable _memberSerializers;

		// Token: 0x0400216E RID: 8558
		private Hashtable _nopMemberSerializers;

		// Token: 0x0200058A RID: 1418
		[ProvideProperty("Language", typeof(IComponent))]
		[ProvideProperty("LoadLanguage", typeof(IComponent))]
		[ProvideProperty("Localizable", typeof(IComponent))]
		internal class LanguageExtenders : IExtenderProvider
		{
			// Token: 0x06003252 RID: 12882 RVA: 0x0011CE68 File Offset: 0x0011BE68
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

			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x06003253 RID: 12883 RVA: 0x0011CEB7 File Offset: 0x0011BEB7
			internal TypeConverter.StandardValuesCollection SupportedCultures
			{
				get
				{
					return this._supportedCultures;
				}
			}

			// Token: 0x1700095B RID: 2395
			// (get) Token: 0x06003254 RID: 12884 RVA: 0x0011CEBF File Offset: 0x0011BEBF
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

			// Token: 0x06003255 RID: 12885 RVA: 0x0011CEDC File Offset: 0x0011BEDC
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

			// Token: 0x06003256 RID: 12886 RVA: 0x0011CF88 File Offset: 0x0011BF88
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

			// Token: 0x06003257 RID: 12887 RVA: 0x0011CFDA File Offset: 0x0011BFDA
			[DesignOnly(true)]
			[TypeConverter(typeof(CodeDomLocalizationProvider.LanguageCultureInfoConverter))]
			[Category("Design")]
			[SRDescription("LocalizationProviderLanguageDescr")]
			public CultureInfo GetLanguage(IComponent o)
			{
				this.CheckRoot();
				return this._language;
			}

			// Token: 0x06003258 RID: 12888 RVA: 0x0011CFE8 File Offset: 0x0011BFE8
			[Browsable(false)]
			[DesignOnly(true)]
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

			// Token: 0x06003259 RID: 12889 RVA: 0x0011D009 File Offset: 0x0011C009
			[DesignOnly(true)]
			[Category("Design")]
			[SRDescription("LocalizationProviderLocalizableDescr")]
			public bool GetLocalizable(IComponent o)
			{
				this.CheckRoot();
				return this._localizable;
			}

			// Token: 0x0600325A RID: 12890 RVA: 0x0011D018 File Offset: 0x0011C018
			public void SetLanguage(IComponent o, CultureInfo language)
			{
				this.CheckRoot();
				if (language == null)
				{
					language = CultureInfo.InvariantCulture;
				}
				bool flag = language.Equals(CultureInfo.InvariantCulture);
				CultureInfo threadDefaultLanguage = this.ThreadDefaultLanguage;
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

			// Token: 0x0600325B RID: 12891 RVA: 0x0011D0E8 File Offset: 0x0011C0E8
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

			// Token: 0x0600325C RID: 12892 RVA: 0x0011D136 File Offset: 0x0011C136
			private bool ShouldSerializeLanguage(IComponent o)
			{
				return this._language != null && this._language != CultureInfo.InvariantCulture;
			}

			// Token: 0x0600325D RID: 12893 RVA: 0x0011D152 File Offset: 0x0011C152
			private bool ShouldSerializeLocalizable(IComponent o)
			{
				return this._localizable;
			}

			// Token: 0x0600325E RID: 12894 RVA: 0x0011D15A File Offset: 0x0011C15A
			private void ResetLocalizable(IComponent o)
			{
				this.SetLocalizable(o, false);
			}

			// Token: 0x0600325F RID: 12895 RVA: 0x0011D164 File Offset: 0x0011C164
			private void ResetLanguage(IComponent o)
			{
				this.SetLanguage(o, CultureInfo.InvariantCulture);
			}

			// Token: 0x06003260 RID: 12896 RVA: 0x0011D172 File Offset: 0x0011C172
			public bool CanExtend(object o)
			{
				this.CheckRoot();
				return this._host != null && o == this._host.RootComponent;
			}

			// Token: 0x0400216F RID: 8559
			private IServiceProvider _serviceProvider;

			// Token: 0x04002170 RID: 8560
			private IDesignerHost _host;

			// Token: 0x04002171 RID: 8561
			private IComponent _lastRoot;

			// Token: 0x04002172 RID: 8562
			private TypeConverter.StandardValuesCollection _supportedCultures;

			// Token: 0x04002173 RID: 8563
			private bool _localizable;

			// Token: 0x04002174 RID: 8564
			private CultureInfo _language;

			// Token: 0x04002175 RID: 8565
			private CultureInfo _loadLanguage;

			// Token: 0x04002176 RID: 8566
			private CultureInfo _defaultLanguage;
		}

		// Token: 0x0200058B RID: 1419
		internal sealed class LanguageCultureInfoConverter : CultureInfoConverter
		{
			// Token: 0x06003261 RID: 12897 RVA: 0x0011D194 File Offset: 0x0011C194
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

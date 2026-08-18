using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C5 RID: 453
	public class IdentityConfiguration
	{
		// Token: 0x06000E8F RID: 3727 RVA: 0x00041F1C File Offset: 0x0004011C
		public IdentityConfiguration()
		{
			SystemIdentityModelSection systemIdentityModelSection = SystemIdentityModelSection.Current;
			IdentityConfigurationElement element = (systemIdentityModelSection != null) ? systemIdentityModelSection.IdentityConfigurationElements.GetElement("") : null;
			this.LoadConfiguration(element);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00041F7F File Offset: 0x0004017F
		public IdentityConfiguration(X509Certificate2 serviceCertificate) : this()
		{
			this.ServiceCertificate = serviceCertificate;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00041F90 File Offset: 0x00040190
		public IdentityConfiguration(bool loadConfig)
		{
			if (!loadConfig)
			{
				this.LoadConfiguration(null);
				return;
			}
			SystemIdentityModelSection systemIdentityModelSection = SystemIdentityModelSection.Current;
			if (systemIdentityModelSection == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7027"));
			}
			IdentityConfigurationElement element = systemIdentityModelSection.IdentityConfigurationElements.GetElement("");
			this.LoadConfiguration(element);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0004200B File Offset: 0x0004020B
		public IdentityConfiguration(bool loadConfig, X509Certificate2 serviceCertificate) : this(loadConfig)
		{
			this.ServiceCertificate = serviceCertificate;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0004201C File Offset: 0x0004021C
		public IdentityConfiguration(string identityConfigurationName)
		{
			if (identityConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identityConfigurationName");
			}
			SystemIdentityModelSection systemIdentityModelSection = SystemIdentityModelSection.Current;
			if (systemIdentityModelSection == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7027"));
			}
			this._identityConfigurationName = identityConfigurationName;
			IdentityConfigurationElement element = systemIdentityModelSection.IdentityConfigurationElements.GetElement(identityConfigurationName);
			this.LoadConfiguration(element);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000420A2 File Offset: 0x000402A2
		public IdentityConfiguration(string identityConfigurationName, X509Certificate2 serviceCertificate) : this(identityConfigurationName)
		{
			this.ServiceCertificate = serviceCertificate;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x000420B2 File Offset: 0x000402B2
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x000420BF File Offset: 0x000402BF
		public AudienceRestriction AudienceRestriction
		{
			get
			{
				return this._serviceHandlerConfiguration.AudienceRestriction;
			}
			set
			{
				this._serviceHandlerConfiguration.AudienceRestriction = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000420CD File Offset: 0x000402CD
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x000420DA File Offset: 0x000402DA
		public IdentityModelCaches Caches
		{
			get
			{
				return this._serviceHandlerConfiguration.Caches;
			}
			set
			{
				this._serviceHandlerConfiguration.Caches = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000420E8 File Offset: 0x000402E8
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x000420F5 File Offset: 0x000402F5
		public X509CertificateValidationMode CertificateValidationMode
		{
			get
			{
				return this._serviceHandlerConfiguration.CertificateValidationMode;
			}
			set
			{
				this._serviceHandlerConfiguration.CertificateValidationMode = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00042103 File Offset: 0x00040303
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00042110 File Offset: 0x00040310
		public X509CertificateValidator CertificateValidator
		{
			get
			{
				return this._serviceHandlerConfiguration.CertificateValidator;
			}
			set
			{
				this._serviceHandlerConfiguration.CertificateValidator = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0004211E File Offset: 0x0004031E
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x00042126 File Offset: 0x00040326
		public ClaimsAuthenticationManager ClaimsAuthenticationManager
		{
			get
			{
				return this._claimsAuthenticationManager;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._claimsAuthenticationManager = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00042142 File Offset: 0x00040342
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x0004214A File Offset: 0x0004034A
		public ClaimsAuthorizationManager ClaimsAuthorizationManager
		{
			get
			{
				return this._claimsAuthorizationManager;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._claimsAuthorizationManager = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00042166 File Offset: 0x00040366
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x00042173 File Offset: 0x00040373
		public bool DetectReplayedTokens
		{
			get
			{
				return this._serviceHandlerConfiguration.DetectReplayedTokens;
			}
			set
			{
				this._serviceHandlerConfiguration.DetectReplayedTokens = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00042181 File Offset: 0x00040381
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00042189 File Offset: 0x00040389
		public virtual bool IsInitialized
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

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00042194 File Offset: 0x00040394
		private static SecurityTokenResolver GetServiceTokenResolver(IdentityConfigurationElement element)
		{
			SecurityTokenResolver result;
			try
			{
				result = CustomTypeElement.Resolve<SecurityTokenResolver>(element.ServiceTokenResolver);
			}
			catch (ArgumentException inner)
			{
				throw DiagnosticUtility.ThrowHelperConfigurationError(element, "serviceTokenResolver", inner);
			}
			return result;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000421D0 File Offset: 0x000403D0
		private static SecurityTokenResolver GetIssuerTokenResolver(IdentityConfigurationElement element)
		{
			SecurityTokenResolver result;
			try
			{
				result = CustomTypeElement.Resolve<SecurityTokenResolver>(element.IssuerTokenResolver);
			}
			catch (ArgumentException inner)
			{
				throw DiagnosticUtility.ThrowHelperConfigurationError(element, "issuerTokenResolver", inner);
			}
			return result;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0004220C File Offset: 0x0004040C
		private static ClaimsAuthenticationManager GetClaimsAuthenticationManager(IdentityConfigurationElement element)
		{
			ClaimsAuthenticationManager result;
			try
			{
				result = CustomTypeElement.Resolve<ClaimsAuthenticationManager>(element.ClaimsAuthenticationManager);
			}
			catch (ArgumentException inner)
			{
				throw DiagnosticUtility.ThrowHelperConfigurationError(element, "claimsAuthenticationManager", inner);
			}
			return result;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00042248 File Offset: 0x00040448
		private static IssuerNameRegistry GetIssuerNameRegistry(IssuerNameRegistryElement element)
		{
			IssuerNameRegistry result;
			try
			{
				Type customType = string.IsNullOrEmpty(element.Type) ? IdentityConfiguration.DefaultIssuerNameRegistryType : Type.GetType(element.Type);
				result = TypeResolveHelper.Resolve<IssuerNameRegistry>(element, customType);
			}
			catch (ArgumentException inner)
			{
				throw DiagnosticUtility.ThrowHelperConfigurationError(element, "issuerNameRegistry", inner);
			}
			return result;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000422A0 File Offset: 0x000404A0
		public virtual void Initialize()
		{
			if (this.IsInitialized)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID7009"));
			}
			SecurityTokenHandlerCollection securityTokenHandlers = this.SecurityTokenHandlers;
			if (this._serviceHandlerConfiguration != securityTokenHandlers.Configuration)
			{
				TraceUtility.TraceString(TraceEventType.Information, SR.GetString("ID4283"), new object[0]);
				this.IsInitialized = true;
				return;
			}
			if (this.ServiceCertificate != null)
			{
				SecurityTokenResolver securityTokenResolver = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(new ReadOnlyCollection<SecurityToken>(new SecurityToken[]
				{
					new X509SecurityToken(this.ServiceCertificate)
				}), false);
				SecurityTokenResolver serviceTokenResolver = this.SecurityTokenHandlers.Configuration.ServiceTokenResolver;
				if (serviceTokenResolver != null && serviceTokenResolver != EmptySecurityTokenResolver.Instance)
				{
					this.SecurityTokenHandlers.Configuration.ServiceTokenResolver = new AggregateTokenResolver(new SecurityTokenResolver[]
					{
						securityTokenResolver,
						serviceTokenResolver
					});
				}
				else
				{
					this.SecurityTokenHandlers.Configuration.ServiceTokenResolver = securityTokenResolver;
				}
			}
			SecurityTokenResolver issuerTokenResolver = this.IssuerTokenResolver;
			if (this.IssuerTokenResolver == SecurityTokenHandlerConfiguration.DefaultIssuerTokenResolver && this.KnownIssuerCertificates != null)
			{
				int count = this.KnownIssuerCertificates.Count;
				if (count > 0)
				{
					SecurityToken[] array = new SecurityToken[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new X509SecurityToken(this.KnownIssuerCertificates[i]);
					}
					SecurityTokenResolver securityTokenResolver2 = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(new ReadOnlyCollection<SecurityToken>(array), false);
					this.IssuerTokenResolver = new AggregateTokenResolver(new SecurityTokenResolver[]
					{
						securityTokenResolver2,
						issuerTokenResolver
					});
				}
			}
			if (this.CertificateValidationMode != X509CertificateValidationMode.Custom)
			{
				securityTokenHandlers.Configuration.CertificateValidator = X509Util.CreateCertificateValidator(securityTokenHandlers.Configuration.CertificateValidationMode, securityTokenHandlers.Configuration.RevocationMode, securityTokenHandlers.Configuration.TrustedStoreLocation);
			}
			else if (securityTokenHandlers.Configuration.CertificateValidator == SecurityTokenHandlerConfiguration.DefaultCertificateValidator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4280")));
			}
			this.IsInitialized = true;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0004246C File Offset: 0x0004066C
		protected void LoadConfiguration(IdentityConfigurationElement element)
		{
			if (element != null)
			{
				if (element.ClaimsAuthenticationManager.IsConfigured)
				{
					this._claimsAuthenticationManager = IdentityConfiguration.GetClaimsAuthenticationManager(element);
				}
				if (element.ClaimsAuthorizationManager.IsConfigured)
				{
					this._claimsAuthorizationManager = CustomTypeElement.Resolve<ClaimsAuthorizationManager>(element.ClaimsAuthorizationManager);
				}
				this._serviceHandlerConfiguration = this.LoadHandlerConfiguration(element);
			}
			this._securityTokenHandlerCollectionManager = this.LoadHandlers(element);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000424D0 File Offset: 0x000406D0
		protected SecurityTokenHandlerCollectionManager LoadHandlers(IdentityConfigurationElement serviceElement)
		{
			SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager = SecurityTokenHandlerCollectionManager.CreateEmptySecurityTokenHandlerCollectionManager();
			if (serviceElement != null)
			{
				if (serviceElement.SecurityTokenHandlerSets.Count > 0)
				{
					foreach (object obj in serviceElement.SecurityTokenHandlerSets)
					{
						SecurityTokenHandlerElementCollection securityTokenHandlerElementCollection = (SecurityTokenHandlerElementCollection)obj;
						try
						{
							SecurityTokenHandlerConfiguration securityTokenHandlerConfiguration;
							if (string.IsNullOrEmpty(securityTokenHandlerElementCollection.Name) || StringComparer.Ordinal.Equals(securityTokenHandlerElementCollection.Name, ""))
							{
								if (securityTokenHandlerElementCollection.SecurityTokenHandlerConfiguration.IsConfigured)
								{
									this._serviceHandlerConfiguration = this.LoadHandlerConfiguration(serviceElement);
									securityTokenHandlerConfiguration = this.LoadHandlerConfiguration(this._serviceHandlerConfiguration, securityTokenHandlerElementCollection.SecurityTokenHandlerConfiguration);
								}
								else
								{
									securityTokenHandlerConfiguration = this.LoadHandlerConfiguration(serviceElement);
								}
								this._serviceHandlerConfiguration = securityTokenHandlerConfiguration;
							}
							else if (securityTokenHandlerElementCollection.SecurityTokenHandlerConfiguration.IsConfigured)
							{
								securityTokenHandlerConfiguration = this.LoadHandlerConfiguration(null, securityTokenHandlerElementCollection.SecurityTokenHandlerConfiguration);
							}
							else
							{
								securityTokenHandlerConfiguration = new SecurityTokenHandlerConfiguration();
							}
							SecurityTokenHandlerCollection securityTokenHandlerCollection = new SecurityTokenHandlerCollection(securityTokenHandlerConfiguration);
							securityTokenHandlerCollectionManager[securityTokenHandlerElementCollection.Name] = securityTokenHandlerCollection;
							foreach (object obj2 in securityTokenHandlerElementCollection)
							{
								CustomTypeElement customTypeElement = (CustomTypeElement)obj2;
								securityTokenHandlerCollection.Add(CustomTypeElement.Resolve<SecurityTokenHandler>(customTypeElement));
							}
						}
						catch (ArgumentException inner)
						{
							throw DiagnosticUtility.ThrowHelperConfigurationError(serviceElement, securityTokenHandlerElementCollection.Name, inner);
						}
					}
				}
				if (!securityTokenHandlerCollectionManager.ContainsKey(""))
				{
					securityTokenHandlerCollectionManager[""] = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection(this._serviceHandlerConfiguration);
				}
			}
			else
			{
				this._serviceHandlerConfiguration = new SecurityTokenHandlerConfiguration();
				this._serviceHandlerConfiguration.MaxClockSkew = this._serviceMaxClockSkew;
				if (!securityTokenHandlerCollectionManager.ContainsKey(""))
				{
					securityTokenHandlerCollectionManager[""] = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection(this._serviceHandlerConfiguration);
				}
			}
			return securityTokenHandlerCollectionManager;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000426E0 File Offset: 0x000408E0
		protected SecurityTokenHandlerConfiguration LoadHandlerConfiguration(IdentityConfigurationElement element)
		{
			SecurityTokenHandlerConfiguration securityTokenHandlerConfiguration = new SecurityTokenHandlerConfiguration();
			try
			{
				if (element.ElementInformation.Properties["maximumClockSkew"].ValueOrigin != PropertyValueOrigin.Default)
				{
					securityTokenHandlerConfiguration.MaxClockSkew = element.MaximumClockSkew;
				}
				else
				{
					securityTokenHandlerConfiguration.MaxClockSkew = this._serviceMaxClockSkew;
				}
			}
			catch (ArgumentException inner)
			{
				throw DiagnosticUtility.ThrowHelperConfigurationError(element, "maximumClockSkew", inner);
			}
			if (element.AudienceUris.IsConfigured)
			{
				securityTokenHandlerConfiguration.AudienceRestriction.AudienceMode = element.AudienceUris.Mode;
				foreach (object obj in element.AudienceUris)
				{
					AudienceUriElement audienceUriElement = (AudienceUriElement)obj;
					securityTokenHandlerConfiguration.AudienceRestriction.AllowedAudienceUris.Add(new Uri(audienceUriElement.Value, UriKind.RelativeOrAbsolute));
				}
			}
			if (element.Caches.IsConfigured)
			{
				if (element.Caches.TokenReplayCache.IsConfigured)
				{
					securityTokenHandlerConfiguration.Caches.TokenReplayCache = CustomTypeElement.Resolve<TokenReplayCache>(element.Caches.TokenReplayCache);
				}
				if (element.Caches.SessionSecurityTokenCache.IsConfigured)
				{
					securityTokenHandlerConfiguration.Caches.SessionSecurityTokenCache = CustomTypeElement.Resolve<SessionSecurityTokenCache>(element.Caches.SessionSecurityTokenCache);
				}
			}
			if (element.CertificateValidation.IsConfigured)
			{
				securityTokenHandlerConfiguration.RevocationMode = element.CertificateValidation.RevocationMode;
				securityTokenHandlerConfiguration.CertificateValidationMode = element.CertificateValidation.CertificateValidationMode;
				securityTokenHandlerConfiguration.TrustedStoreLocation = element.CertificateValidation.TrustedStoreLocation;
				if (element.CertificateValidation.CertificateValidator.IsConfigured)
				{
					securityTokenHandlerConfiguration.CertificateValidator = CustomTypeElement.Resolve<X509CertificateValidator>(element.CertificateValidation.CertificateValidator);
				}
			}
			if (element.IssuerNameRegistry.IsConfigured)
			{
				securityTokenHandlerConfiguration.IssuerNameRegistry = IdentityConfiguration.GetIssuerNameRegistry(element.IssuerNameRegistry);
			}
			if (element.IssuerTokenResolver.IsConfigured)
			{
				securityTokenHandlerConfiguration.IssuerTokenResolver = IdentityConfiguration.GetIssuerTokenResolver(element);
			}
			securityTokenHandlerConfiguration.SaveBootstrapContext = element.SaveBootstrapContext;
			if (element.ServiceTokenResolver.IsConfigured)
			{
				securityTokenHandlerConfiguration.ServiceTokenResolver = IdentityConfiguration.GetServiceTokenResolver(element);
			}
			if (element.TokenReplayDetection.IsConfigured)
			{
				securityTokenHandlerConfiguration.DetectReplayedTokens = element.TokenReplayDetection.Enabled;
				securityTokenHandlerConfiguration.TokenReplayCacheExpirationPeriod = element.TokenReplayDetection.ExpirationPeriod;
			}
			return securityTokenHandlerConfiguration;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00042928 File Offset: 0x00040B28
		protected SecurityTokenHandlerConfiguration LoadHandlerConfiguration(SecurityTokenHandlerConfiguration baseConfiguration, SecurityTokenHandlerConfigurationElement element)
		{
			SecurityTokenHandlerConfiguration securityTokenHandlerConfiguration = (baseConfiguration == null) ? new SecurityTokenHandlerConfiguration() : baseConfiguration;
			if (element.AudienceUris.IsConfigured)
			{
				securityTokenHandlerConfiguration.AudienceRestriction.AudienceMode = AudienceUriMode.Always;
				securityTokenHandlerConfiguration.AudienceRestriction.AllowedAudienceUris.Clear();
				securityTokenHandlerConfiguration.AudienceRestriction.AudienceMode = element.AudienceUris.Mode;
				foreach (object obj in element.AudienceUris)
				{
					AudienceUriElement audienceUriElement = (AudienceUriElement)obj;
					securityTokenHandlerConfiguration.AudienceRestriction.AllowedAudienceUris.Add(new Uri(audienceUriElement.Value, UriKind.RelativeOrAbsolute));
				}
			}
			if (element.Caches.IsConfigured)
			{
				if (element.Caches.TokenReplayCache.IsConfigured)
				{
					securityTokenHandlerConfiguration.Caches.TokenReplayCache = CustomTypeElement.Resolve<TokenReplayCache>(element.Caches.TokenReplayCache);
				}
				if (element.Caches.SessionSecurityTokenCache.IsConfigured)
				{
					securityTokenHandlerConfiguration.Caches.SessionSecurityTokenCache = CustomTypeElement.Resolve<SessionSecurityTokenCache>(element.Caches.SessionSecurityTokenCache);
				}
			}
			if (element.CertificateValidation.IsConfigured)
			{
				securityTokenHandlerConfiguration.RevocationMode = element.CertificateValidation.RevocationMode;
				securityTokenHandlerConfiguration.CertificateValidationMode = element.CertificateValidation.CertificateValidationMode;
				securityTokenHandlerConfiguration.TrustedStoreLocation = element.CertificateValidation.TrustedStoreLocation;
				if (element.CertificateValidation.CertificateValidator.IsConfigured)
				{
					securityTokenHandlerConfiguration.CertificateValidator = CustomTypeElement.Resolve<X509CertificateValidator>(element.CertificateValidation.CertificateValidator);
				}
			}
			if (element.IssuerNameRegistry.IsConfigured)
			{
				securityTokenHandlerConfiguration.IssuerNameRegistry = IdentityConfiguration.GetIssuerNameRegistry(element.IssuerNameRegistry);
			}
			if (element.IssuerTokenResolver.IsConfigured)
			{
				securityTokenHandlerConfiguration.IssuerTokenResolver = CustomTypeElement.Resolve<SecurityTokenResolver>(element.IssuerTokenResolver);
			}
			try
			{
				if (element.ElementInformation.Properties["maximumClockSkew"].ValueOrigin != PropertyValueOrigin.Default)
				{
					securityTokenHandlerConfiguration.MaxClockSkew = element.MaximumClockSkew;
				}
			}
			catch (ArgumentException inner)
			{
				throw DiagnosticUtility.ThrowHelperConfigurationError(element, "maximumClockSkew", inner);
			}
			if (element.ElementInformation.Properties["saveBootstrapContext"].ValueOrigin != PropertyValueOrigin.Default)
			{
				securityTokenHandlerConfiguration.SaveBootstrapContext = element.SaveBootstrapContext;
			}
			if (element.ServiceTokenResolver.IsConfigured)
			{
				securityTokenHandlerConfiguration.ServiceTokenResolver = CustomTypeElement.Resolve<SecurityTokenResolver>(element.ServiceTokenResolver);
			}
			if (element.TokenReplayDetection.IsConfigured)
			{
				securityTokenHandlerConfiguration.DetectReplayedTokens = element.TokenReplayDetection.Enabled;
				securityTokenHandlerConfiguration.TokenReplayCacheExpirationPeriod = element.TokenReplayDetection.ExpirationPeriod;
			}
			return securityTokenHandlerConfiguration;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00042BAC File Offset: 0x00040DAC
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00042BB9 File Offset: 0x00040DB9
		public TimeSpan MaxClockSkew
		{
			get
			{
				return this._serviceHandlerConfiguration.MaxClockSkew;
			}
			set
			{
				this._serviceHandlerConfiguration.MaxClockSkew = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00042BC7 File Offset: 0x00040DC7
		public string Name
		{
			get
			{
				return this._identityConfigurationName;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00042BCF File Offset: 0x00040DCF
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x00042BDC File Offset: 0x00040DDC
		public IssuerNameRegistry IssuerNameRegistry
		{
			get
			{
				return this._serviceHandlerConfiguration.IssuerNameRegistry;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._serviceHandlerConfiguration.IssuerNameRegistry = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00042BFD File Offset: 0x00040DFD
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x00042C05 File Offset: 0x00040E05
		public X509Certificate2 ServiceCertificate
		{
			get
			{
				return this._serviceCertificate;
			}
			set
			{
				this._serviceCertificate = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00042C0E File Offset: 0x00040E0E
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x00042C16 File Offset: 0x00040E16
		internal List<X509Certificate2> KnownIssuerCertificates
		{
			get
			{
				return this.knownCertificates;
			}
			set
			{
				this.knownCertificates = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00042C1F File Offset: 0x00040E1F
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00042C2C File Offset: 0x00040E2C
		public SecurityTokenResolver IssuerTokenResolver
		{
			get
			{
				return this._serviceHandlerConfiguration.IssuerTokenResolver;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._serviceHandlerConfiguration.IssuerTokenResolver = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00042C4D File Offset: 0x00040E4D
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00042C5A File Offset: 0x00040E5A
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this._serviceHandlerConfiguration.RevocationMode;
			}
			set
			{
				this._serviceHandlerConfiguration.RevocationMode = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00042C68 File Offset: 0x00040E68
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00042C75 File Offset: 0x00040E75
		public SecurityTokenResolver ServiceTokenResolver
		{
			get
			{
				return this._serviceHandlerConfiguration.ServiceTokenResolver;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._serviceHandlerConfiguration.ServiceTokenResolver = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00042C96 File Offset: 0x00040E96
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00042CA3 File Offset: 0x00040EA3
		public bool SaveBootstrapContext
		{
			get
			{
				return this._serviceHandlerConfiguration.SaveBootstrapContext;
			}
			set
			{
				this._serviceHandlerConfiguration.SaveBootstrapContext = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00042CB1 File Offset: 0x00040EB1
		public SecurityTokenHandlerCollectionManager SecurityTokenHandlerCollectionManager
		{
			get
			{
				return this._securityTokenHandlerCollectionManager;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00042CB9 File Offset: 0x00040EB9
		public SecurityTokenHandlerCollection SecurityTokenHandlers
		{
			get
			{
				return this._securityTokenHandlerCollectionManager[""];
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00042CCB File Offset: 0x00040ECB
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00042CD8 File Offset: 0x00040ED8
		public TimeSpan TokenReplayCacheExpirationPeriod
		{
			get
			{
				return this._serviceHandlerConfiguration.TokenReplayCacheExpirationPeriod;
			}
			set
			{
				this._serviceHandlerConfiguration.TokenReplayCacheExpirationPeriod = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00042CE6 File Offset: 0x00040EE6
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00042CF3 File Offset: 0x00040EF3
		public StoreLocation TrustedStoreLocation
		{
			get
			{
				return this._serviceHandlerConfiguration.TrustedStoreLocation;
			}
			set
			{
				this._serviceHandlerConfiguration.TrustedStoreLocation = value;
			}
		}

		// Token: 0x04000D68 RID: 3432
		public const string DefaultServiceName = "";

		// Token: 0x04000D69 RID: 3433
		public static readonly TimeSpan DefaultMaxClockSkew = new TimeSpan(0, 5, 0);

		// Token: 0x04000D6A RID: 3434
		internal const string DefaultMaxClockSkewString = "00:05:00";

		// Token: 0x04000D6B RID: 3435
		public static readonly X509CertificateValidationMode DefaultCertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

		// Token: 0x04000D6C RID: 3436
		public static readonly Type DefaultIssuerNameRegistryType = typeof(ConfigurationBasedIssuerNameRegistry);

		// Token: 0x04000D6D RID: 3437
		public static readonly X509RevocationMode DefaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04000D6E RID: 3438
		public static readonly StoreLocation DefaultTrustedStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04000D6F RID: 3439
		private ClaimsAuthenticationManager _claimsAuthenticationManager = new ClaimsAuthenticationManager();

		// Token: 0x04000D70 RID: 3440
		private ClaimsAuthorizationManager _claimsAuthorizationManager = new ClaimsAuthorizationManager();

		// Token: 0x04000D71 RID: 3441
		private bool _isInitialized;

		// Token: 0x04000D72 RID: 3442
		private SecurityTokenHandlerCollectionManager _securityTokenHandlerCollectionManager;

		// Token: 0x04000D73 RID: 3443
		private string _identityConfigurationName = "";

		// Token: 0x04000D74 RID: 3444
		private TimeSpan _serviceMaxClockSkew = IdentityConfiguration.DefaultMaxClockSkew;

		// Token: 0x04000D75 RID: 3445
		private SecurityTokenHandlerConfiguration _serviceHandlerConfiguration;

		// Token: 0x04000D76 RID: 3446
		private X509Certificate2 _serviceCertificate;

		// Token: 0x04000D77 RID: 3447
		private List<X509Certificate2> knownCertificates;
	}
}

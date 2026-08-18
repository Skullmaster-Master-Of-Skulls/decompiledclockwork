using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Security
{
	// Token: 0x02000373 RID: 883
	[ComVisible(false)]
	public class WSTrustChannelFactory : ChannelFactory<IWSTrustChannelContract>
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x00078660 File Offset: 0x00076860
		public WSTrustChannelFactory()
		{
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0007867E File Offset: 0x0007687E
		public WSTrustChannelFactory(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0007869D File Offset: 0x0007689D
		public WSTrustChannelFactory(Binding binding) : base(binding)
		{
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000786BC File Offset: 0x000768BC
		public WSTrustChannelFactory(ServiceEndpoint endpoint) : base(endpoint)
		{
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000786DB File Offset: 0x000768DB
		public WSTrustChannelFactory(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000786FB File Offset: 0x000768FB
		public WSTrustChannelFactory(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0007871B File Offset: 0x0007691B
		public WSTrustChannelFactory(Binding binding, string remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0007873B File Offset: 0x0007693B
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x00078744 File Offset: 0x00076944
		public TrustVersion TrustVersion
		{
			get
			{
				return this._trustVersion;
			}
			set
			{
				object factoryLock = this._factoryLock;
				lock (factoryLock)
				{
					if (this._locked)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3287"));
					}
					this._trustVersion = value;
				}
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x000787A0 File Offset: 0x000769A0
		// (set) Token: 0x060020A7 RID: 8359 RVA: 0x000787A8 File Offset: 0x000769A8
		public SecurityTokenHandlerCollectionManager SecurityTokenHandlerCollectionManager
		{
			get
			{
				return this._securityTokenHandlerCollectionManager;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				object factoryLock = this._factoryLock;
				lock (factoryLock)
				{
					if (this._locked)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3287"));
					}
					this._securityTokenHandlerCollectionManager = value;
				}
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00078814 File Offset: 0x00076A14
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x0007881C File Offset: 0x00076A1C
		public SecurityTokenResolver SecurityTokenResolver
		{
			get
			{
				return this._securityTokenResolver;
			}
			set
			{
				object factoryLock = this._factoryLock;
				lock (factoryLock)
				{
					if (this._locked)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3287"));
					}
					this._securityTokenResolver = value;
				}
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00078878 File Offset: 0x00076A78
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x00078880 File Offset: 0x00076A80
		public SecurityTokenResolver UseKeyTokenResolver
		{
			get
			{
				return this._useKeyTokenResolver;
			}
			set
			{
				object factoryLock = this._factoryLock;
				lock (factoryLock)
				{
					if (this._locked)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3287"));
					}
					this._useKeyTokenResolver = value;
				}
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x000788DC File Offset: 0x00076ADC
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x000788E4 File Offset: 0x00076AE4
		public WSTrustRequestSerializer WSTrustRequestSerializer
		{
			get
			{
				return this._wsTrustRequestSerializer;
			}
			set
			{
				object factoryLock = this._factoryLock;
				lock (factoryLock)
				{
					if (this._locked)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3287"));
					}
					this._wsTrustRequestSerializer = value;
				}
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x00078940 File Offset: 0x00076B40
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x00078948 File Offset: 0x00076B48
		public WSTrustResponseSerializer WSTrustResponseSerializer
		{
			get
			{
				return this._wsTrustResponseSerializer;
			}
			set
			{
				object factoryLock = this._factoryLock;
				lock (factoryLock)
				{
					if (this._locked)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3287"));
					}
					this._wsTrustResponseSerializer = value;
				}
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000789A4 File Offset: 0x00076BA4
		public override IWSTrustChannelContract CreateChannel(EndpointAddress address, Uri via)
		{
			IWSTrustChannelContract innerChannel = base.CreateChannel(address, via);
			WSTrustChannelFactory.WSTrustChannelLockedProperties lockedProperties = this.GetLockedProperties();
			return this.CreateTrustChannel(innerChannel, lockedProperties.TrustVersion, lockedProperties.Context, lockedProperties.RequestSerializer, lockedProperties.ResponseSerializer);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000789E0 File Offset: 0x00076BE0
		protected virtual WSTrustChannel CreateTrustChannel(IWSTrustChannelContract innerChannel, TrustVersion trustVersion, WSTrustSerializationContext context, WSTrustRequestSerializer requestSerializer, WSTrustResponseSerializer responseSerializer)
		{
			return new WSTrustChannel(this, innerChannel, trustVersion, context, requestSerializer, responseSerializer);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000789F0 File Offset: 0x00076BF0
		private WSTrustChannelFactory.WSTrustChannelLockedProperties GetLockedProperties()
		{
			object factoryLock = this._factoryLock;
			WSTrustChannelFactory.WSTrustChannelLockedProperties lockedProperties;
			lock (factoryLock)
			{
				if (this._lockedProperties == null)
				{
					WSTrustChannelFactory.WSTrustChannelLockedProperties wstrustChannelLockedProperties = new WSTrustChannelFactory.WSTrustChannelLockedProperties();
					wstrustChannelLockedProperties.TrustVersion = this.GetTrustVersion();
					wstrustChannelLockedProperties.Context = this.CreateSerializationContext();
					wstrustChannelLockedProperties.RequestSerializer = this.GetRequestSerializer(wstrustChannelLockedProperties.TrustVersion);
					wstrustChannelLockedProperties.ResponseSerializer = this.GetResponseSerializer(wstrustChannelLockedProperties.TrustVersion);
					this._lockedProperties = wstrustChannelLockedProperties;
					this._locked = true;
				}
				lockedProperties = this._lockedProperties;
			}
			return lockedProperties;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00078A8C File Offset: 0x00076C8C
		private WSTrustRequestSerializer GetRequestSerializer(TrustVersion trustVersion)
		{
			if (this._wsTrustRequestSerializer != null)
			{
				return this._wsTrustRequestSerializer;
			}
			if (trustVersion == TrustVersion.WSTrust13)
			{
				return new WSTrust13RequestSerializer();
			}
			if (trustVersion == TrustVersion.WSTrustFeb2005)
			{
				return new WSTrustFeb2005RequestSerializer();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3137", new object[]
			{
				trustVersion.ToString()
			})));
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00078AEC File Offset: 0x00076CEC
		private WSTrustResponseSerializer GetResponseSerializer(TrustVersion trustVersion)
		{
			if (this._wsTrustResponseSerializer != null)
			{
				return this._wsTrustResponseSerializer;
			}
			if (trustVersion == TrustVersion.WSTrust13)
			{
				return new WSTrust13ResponseSerializer();
			}
			if (trustVersion == TrustVersion.WSTrustFeb2005)
			{
				return new WSTrustFeb2005ResponseSerializer();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3137", new object[]
			{
				trustVersion.ToString()
			})));
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00078B4C File Offset: 0x00076D4C
		private TrustVersion GetTrustVersion()
		{
			TrustVersion trustVersion = this._trustVersion;
			if (trustVersion == null)
			{
				BindingElementCollection bindingElementCollection = base.Endpoint.Binding.CreateBindingElements();
				SecurityBindingElement securityBindingElement = bindingElementCollection.Find<SecurityBindingElement>();
				if (securityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3269")));
				}
				trustVersion = securityBindingElement.MessageSecurityVersion.TrustVersion;
			}
			return trustVersion;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00078BA8 File Offset: 0x00076DA8
		protected virtual WSTrustSerializationContext CreateSerializationContext()
		{
			SecurityTokenResolver securityTokenResolver = this._securityTokenResolver;
			if (securityTokenResolver == null)
			{
				ClientCredentials credentials = base.Credentials;
				if (credentials.ClientCertificate != null && credentials.ClientCertificate.Certificate != null)
				{
					securityTokenResolver = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(new List<SecurityToken>
					{
						new X509SecurityToken(credentials.ClientCertificate.Certificate)
					}.AsReadOnly(), false);
				}
			}
			if (securityTokenResolver == null)
			{
				securityTokenResolver = EmptySecurityTokenResolver.Instance;
			}
			SecurityTokenResolver useKeyTokenResolver = this._useKeyTokenResolver ?? EmptySecurityTokenResolver.Instance;
			return new WSTrustSerializationContext(this._securityTokenHandlerCollectionManager, securityTokenResolver, useKeyTokenResolver);
		}

		// Token: 0x04001F18 RID: 7960
		private object _factoryLock = new object();

		// Token: 0x04001F19 RID: 7961
		private bool _locked;

		// Token: 0x04001F1A RID: 7962
		private WSTrustChannelFactory.WSTrustChannelLockedProperties _lockedProperties;

		// Token: 0x04001F1B RID: 7963
		private TrustVersion _trustVersion;

		// Token: 0x04001F1C RID: 7964
		private SecurityTokenResolver _securityTokenResolver;

		// Token: 0x04001F1D RID: 7965
		private SecurityTokenResolver _useKeyTokenResolver;

		// Token: 0x04001F1E RID: 7966
		private SecurityTokenHandlerCollectionManager _securityTokenHandlerCollectionManager = SecurityTokenHandlerCollectionManager.CreateDefaultSecurityTokenHandlerCollectionManager();

		// Token: 0x04001F1F RID: 7967
		private WSTrustRequestSerializer _wsTrustRequestSerializer;

		// Token: 0x04001F20 RID: 7968
		private WSTrustResponseSerializer _wsTrustResponseSerializer;

		// Token: 0x02000B95 RID: 2965
		private class WSTrustChannelLockedProperties
		{
			// Token: 0x0400412D RID: 16685
			public TrustVersion TrustVersion;

			// Token: 0x0400412E RID: 16686
			public WSTrustSerializationContext Context;

			// Token: 0x0400412F RID: 16687
			public WSTrustRequestSerializer RequestSerializer;

			// Token: 0x04004130 RID: 16688
			public WSTrustResponseSerializer ResponseSerializer;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E0 RID: 480
	internal class ChannelCredentials : IChannelCredentials, IDisposable
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x00036BA6 File Offset: 0x00034DA6
		internal ChannelCredentials(IProvideChannelBuilderSettings channelBuilderSettings)
		{
			this.channelBuilderSettings = channelBuilderSettings;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00036BB8 File Offset: 0x00034DB8
		internal static ComProxy Create(IntPtr outer, IProvideChannelBuilderSettings channelBuilderSettings)
		{
			if (channelBuilderSettings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotCreateChannelOption")));
			}
			ChannelCredentials channelCredentials = null;
			ComProxy comProxy = null;
			ComProxy result;
			try
			{
				channelCredentials = new ChannelCredentials(channelBuilderSettings);
				comProxy = ComProxy.Create(outer, channelCredentials, channelCredentials);
				result = comProxy;
			}
			finally
			{
				if (comProxy == null && channelCredentials != null)
				{
					((IDisposable)channelCredentials).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00036C18 File Offset: 0x00034E18
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00036C1C File Offset: 0x00034E1C
		void IChannelCredentials.SetWindowsCredential(string domain, string userName, string password, int impersonationLevel, bool allowNtlm)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				NetworkCredential clientCredential = null;
				if (!string.IsNullOrEmpty(domain) || !string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password))
				{
					if (string.IsNullOrEmpty(userName))
					{
						userName = "";
					}
					SecurityUtils.PrepareNetworkCredential();
					clientCredential = new NetworkCredential(userName, password, domain);
				}
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.Windows.AllowedImpersonationLevel = (TokenImpersonationLevel)impersonationLevel;
				clientCredentials.Windows.AllowNtlm = allowNtlm;
				clientCredentials.Windows.ClientCredential = clientCredential;
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00036CE0 File Offset: 0x00034EE0
		void IChannelCredentials.SetUserNameCredential(string userName, string password)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.UserName.UserName = userName;
				clientCredentials.UserName.Password = password;
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00036D58 File Offset: 0x00034F58
		void IChannelCredentials.SetServiceCertificateAuthentication(string storeLocation, string revocationMode, string certificationValidationMode)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				StoreLocation trustedStoreLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocation);
				X509RevocationMode revocationMode2 = (X509RevocationMode)Enum.Parse(typeof(X509RevocationMode), revocationMode);
				X509CertificateValidationMode certificateValidationMode = X509CertificateValidationMode.ChainTrust;
				if (!string.IsNullOrEmpty(certificationValidationMode))
				{
					certificateValidationMode = (X509CertificateValidationMode)Enum.Parse(typeof(X509CertificateValidationMode), certificationValidationMode);
				}
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.ServiceCertificate.Authentication.TrustedStoreLocation = trustedStoreLocation;
				clientCredentials.ServiceCertificate.Authentication.RevocationMode = revocationMode2;
				clientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = certificateValidationMode;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00036E44 File Offset: 0x00035044
		void IChannelCredentials.SetClientCertificateFromStore(string storeLocation, string storeName, string findType, object findValue)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				StoreLocation storeLocation2 = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocation);
				StoreName storeName2 = (StoreName)Enum.Parse(typeof(StoreName), storeName);
				X509FindType findType2 = (X509FindType)Enum.Parse(typeof(X509FindType), findType);
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.ClientCertificate.SetCertificate(storeLocation2, storeName2, findType2, findValue);
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00036F00 File Offset: 0x00035100
		void IChannelCredentials.SetClientCertificateFromStoreByName(string subjectName, string storeLocation, string storeName)
		{
			((IChannelCredentials)this).SetClientCertificateFromStore(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName.ToString("G"), subjectName);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00036F1C File Offset: 0x0003511C
		void IChannelCredentials.SetClientCertificateFromFile(string fileName, string password, string keyStorageFlags)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				X509Certificate2 certificate;
				if (!string.IsNullOrEmpty(keyStorageFlags))
				{
					X509KeyStorageFlags keyStorageFlags2 = (X509KeyStorageFlags)Enum.Parse(typeof(X509KeyStorageFlags), keyStorageFlags);
					certificate = new X509Certificate2(fileName, password, keyStorageFlags2);
				}
				else
				{
					certificate = new X509Certificate2(fileName, password);
				}
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.ClientCertificate.Certificate = certificate;
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00036FC0 File Offset: 0x000351C0
		void IChannelCredentials.SetDefaultServiceCertificateFromStore(string storeLocation, string storeName, string findType, object findValue)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				StoreLocation storeLocation2 = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocation);
				StoreName storeName2 = (StoreName)Enum.Parse(typeof(StoreName), storeName);
				X509FindType findType2 = (X509FindType)Enum.Parse(typeof(X509FindType), findType);
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.ServiceCertificate.SetDefaultCertificate(storeLocation2, storeName2, findType2, findValue);
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003707C File Offset: 0x0003527C
		void IChannelCredentials.SetDefaultServiceCertificateFromStoreByName(string subjectName, string storeLocation, string storeName)
		{
			((IChannelCredentials)this).SetDefaultServiceCertificateFromStore(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName.ToString("G"), subjectName);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00037098 File Offset: 0x00035298
		void IChannelCredentials.SetDefaultServiceCertificateFromFile(string fileName, string password, string keyStorageFlags)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				X509Certificate2 defaultCertificate;
				if (!string.IsNullOrEmpty(keyStorageFlags))
				{
					X509KeyStorageFlags keyStorageFlags2 = (X509KeyStorageFlags)Enum.Parse(typeof(X509KeyStorageFlags), keyStorageFlags);
					defaultCertificate = new X509Certificate2(fileName, password, keyStorageFlags2);
				}
				else
				{
					defaultCertificate = new X509Certificate2(fileName, password);
				}
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.ServiceCertificate.DefaultCertificate = defaultCertificate;
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003713C File Offset: 0x0003533C
		void IChannelCredentials.SetIssuedToken(string localIssuerAddres, string localIssuerBindingType, string localIssuerBinding)
		{
			IProvideChannelBuilderSettings obj = this.channelBuilderSettings;
			lock (obj)
			{
				Binding localIssuerBinding2 = ConfigLoader.LookupBinding(localIssuerBindingType, localIssuerBinding);
				KeyedByTypeCollection<IEndpointBehavior> behaviors = this.channelBuilderSettings.Behaviors;
				ClientCredentials clientCredentials = behaviors.Find<ClientCredentials>();
				if (clientCredentials == null)
				{
					clientCredentials = new ClientCredentials();
					behaviors.Add(clientCredentials);
				}
				clientCredentials.IssuedToken.LocalIssuerAddress = new EndpointAddress(localIssuerAddres);
				clientCredentials.IssuedToken.LocalIssuerBinding = localIssuerBinding2;
			}
		}

		// Token: 0x040017C0 RID: 6080
		protected IProvideChannelBuilderSettings channelBuilderSettings;
	}
}

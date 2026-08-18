using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000838 RID: 2104
	internal static class HttpTransportSecurityHelpers
	{
		// Token: 0x06004E8D RID: 20109 RVA: 0x0011E554 File Offset: 0x0011C754
		public static bool AddIdentityMapping(Uri via, EndpointAddress target)
		{
			string absoluteUri = via.AbsoluteUri;
			EndpointIdentity identity = target.Identity;
			string text;
			if (identity != null && !(identity is X509CertificateEndpointIdentity))
			{
				text = SecurityUtils.GetSpnFromIdentity(identity, target);
			}
			else
			{
				text = SecurityUtils.GetSpnFromTarget(target);
			}
			Dictionary<string, int> obj = HttpTransportSecurityHelpers.targetNameCounter;
			lock (obj)
			{
				int num = 0;
				if (HttpTransportSecurityHelpers.targetNameCounter.TryGetValue(absoluteUri, out num))
				{
					if (!AuthenticationManager.CustomTargetNameDictionary.ContainsKey(absoluteUri) || AuthenticationManager.CustomTargetNameDictionary[absoluteUri] != text)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpTargetNameDictionaryConflict", new object[]
						{
							absoluteUri,
							text
						})));
					}
					HttpTransportSecurityHelpers.targetNameCounter[absoluteUri] = num + 1;
				}
				else
				{
					if (AuthenticationManager.CustomTargetNameDictionary.ContainsKey(absoluteUri) && AuthenticationManager.CustomTargetNameDictionary[absoluteUri] != text)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpTargetNameDictionaryConflict", new object[]
						{
							absoluteUri,
							text
						})));
					}
					AuthenticationManager.CustomTargetNameDictionary[absoluteUri] = text;
					HttpTransportSecurityHelpers.targetNameCounter.Add(absoluteUri, 1);
				}
			}
			return true;
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x0011E688 File Offset: 0x0011C888
		public static void RemoveIdentityMapping(Uri via, EndpointAddress target, bool validateState)
		{
			string absoluteUri = via.AbsoluteUri;
			EndpointIdentity identity = target.Identity;
			string text;
			if (identity != null && !(identity is X509CertificateEndpointIdentity))
			{
				text = SecurityUtils.GetSpnFromIdentity(identity, target);
			}
			else
			{
				text = SecurityUtils.GetSpnFromTarget(target);
			}
			Dictionary<string, int> obj = HttpTransportSecurityHelpers.targetNameCounter;
			lock (obj)
			{
				int num = HttpTransportSecurityHelpers.targetNameCounter[absoluteUri];
				if (num == 1)
				{
					HttpTransportSecurityHelpers.targetNameCounter.Remove(absoluteUri);
				}
				else
				{
					HttpTransportSecurityHelpers.targetNameCounter[absoluteUri] = num - 1;
				}
				if (validateState && (!AuthenticationManager.CustomTargetNameDictionary.ContainsKey(absoluteUri) || AuthenticationManager.CustomTargetNameDictionary[absoluteUri] != text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpTargetNameDictionaryConflict", new object[]
					{
						absoluteUri,
						text
					})));
				}
			}
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x0011E768 File Offset: 0x0011C968
		public static void AddServerCertMapping(HttpWebRequest request, EndpointAddress to)
		{
			X509CertificateEndpointIdentity x509CertificateEndpointIdentity = to.Identity as X509CertificateEndpointIdentity;
			if (x509CertificateEndpointIdentity != null)
			{
				HttpTransportSecurityHelpers.AddServerCertMapping(request, x509CertificateEndpointIdentity.Certificates[0].Thumbprint);
			}
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x0011E79C File Offset: 0x0011C99C
		private static void AddServerCertMapping(HttpWebRequest request, string thumbprint)
		{
			Dictionary<HttpWebRequest, string> obj = HttpTransportSecurityHelpers.serverCertMap;
			lock (obj)
			{
				if (!HttpTransportSecurityHelpers.serverCertValidationCallbackInstalled)
				{
					HttpTransportSecurityHelpers.chainedServerCertValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
					ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpTransportSecurityHelpers.OnValidateServerCertificate);
					HttpTransportSecurityHelpers.serverCertValidationCallbackInstalled = true;
				}
				HttpTransportSecurityHelpers.serverCertMap.Add(request, thumbprint);
			}
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x0011E80C File Offset: 0x0011CA0C
		private static bool OnValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			HttpWebRequest httpWebRequest = sender as HttpWebRequest;
			if (httpWebRequest != null)
			{
				Dictionary<HttpWebRequest, string> obj = HttpTransportSecurityHelpers.serverCertMap;
				string text;
				lock (obj)
				{
					HttpTransportSecurityHelpers.serverCertMap.TryGetValue(httpWebRequest, out text);
				}
				if (text != null)
				{
					try
					{
						HttpTransportSecurityHelpers.ValidateServerCertificate(certificate, text);
					}
					catch (SecurityNegotiationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						return false;
					}
				}
			}
			if (HttpTransportSecurityHelpers.chainedServerCertValidationCallback == null)
			{
				return sslPolicyErrors == SslPolicyErrors.None;
			}
			return HttpTransportSecurityHelpers.chainedServerCertValidationCallback(sender, certificate, chain, sslPolicyErrors);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x0011E8A4 File Offset: 0x0011CAA4
		public static void RemoveServerCertMapping(HttpWebRequest request)
		{
			Dictionary<HttpWebRequest, string> obj = HttpTransportSecurityHelpers.serverCertMap;
			lock (obj)
			{
				HttpTransportSecurityHelpers.serverCertMap.Remove(request);
			}
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x0011E8EC File Offset: 0x0011CAEC
		private static void ValidateServerCertificate(X509Certificate certificate, string thumbprint)
		{
			string certHashString = certificate.GetCertHashString();
			if (!thumbprint.Equals(certHashString))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("HttpsServerCertThumbprintMismatch", new object[]
				{
					certificate.Subject,
					certHashString,
					thumbprint
				})));
			}
		}

		// Token: 0x040030EE RID: 12526
		private static Dictionary<string, int> targetNameCounter = new Dictionary<string, int>();

		// Token: 0x040030EF RID: 12527
		private static Dictionary<HttpWebRequest, string> serverCertMap = new Dictionary<HttpWebRequest, string>();

		// Token: 0x040030F0 RID: 12528
		private static RemoteCertificateValidationCallback chainedServerCertValidationCallback = null;

		// Token: 0x040030F1 RID: 12529
		private static bool serverCertValidationCallbackInstalled = false;
	}
}

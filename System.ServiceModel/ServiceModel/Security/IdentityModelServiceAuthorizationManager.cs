using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000359 RID: 857
	internal class IdentityModelServiceAuthorizationManager : ServiceAuthorizationManager
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x00075A00 File Offset: 0x00073C00
		protected override ReadOnlyCollection<IAuthorizationPolicy> GetAuthorizationPolicies(OperationContext operationContext)
		{
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = base.GetAuthorizationPolicies(operationContext);
			if (authorizationPolicies == null)
			{
				return IdentityModelServiceAuthorizationManager.AnonymousAuthorizationPolicy;
			}
			ServiceCredentials serviceCredentials = IdentityModelServiceAuthorizationManager.GetServiceCredentials();
			AuthorizationPolicy authorizationPolicy = IdentityModelServiceAuthorizationManager.TransformAuthorizationPolicies(authorizationPolicies, serviceCredentials.IdentityConfiguration.SecurityTokenHandlers, true);
			if (authorizationPolicy == null || authorizationPolicy.IdentityCollection.Count == 0)
			{
				return IdentityModelServiceAuthorizationManager.AnonymousAuthorizationPolicy;
			}
			return new List<IAuthorizationPolicy>
			{
				authorizationPolicy
			}.AsReadOnly();
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00075A60 File Offset: 0x00073C60
		internal static AuthorizationPolicy TransformAuthorizationPolicies(ReadOnlyCollection<IAuthorizationPolicy> baseAuthorizationPolicies, SecurityTokenHandlerCollection securityTokenHandlerCollection, bool includeTransportTokens)
		{
			List<ClaimsIdentity> list = new List<ClaimsIdentity>();
			List<IAuthorizationPolicy> list2 = new List<IAuthorizationPolicy>();
			foreach (IAuthorizationPolicy authorizationPolicy in baseAuthorizationPolicies)
			{
				if (!(authorizationPolicy is SctAuthorizationPolicy) && !(authorizationPolicy is EndpointAuthorizationPolicy))
				{
					AuthorizationPolicy authorizationPolicy2 = authorizationPolicy as AuthorizationPolicy;
					if (authorizationPolicy2 != null)
					{
						list.AddRange(authorizationPolicy2.IdentityCollection);
					}
					else
					{
						list2.Add(authorizationPolicy);
					}
				}
			}
			if (includeTransportTokens && OperationContext.Current != null && OperationContext.Current.IncomingMessageProperties != null && OperationContext.Current.IncomingMessageProperties.Security != null && OperationContext.Current.IncomingMessageProperties.Security.TransportToken != null)
			{
				SecurityToken securityToken = OperationContext.Current.IncomingMessageProperties.Security.TransportToken.SecurityToken;
				ReadOnlyCollection<IAuthorizationPolicy> securityTokenPolicies = OperationContext.Current.IncomingMessageProperties.Security.TransportToken.SecurityTokenPolicies;
				bool flag = true;
				foreach (IAuthorizationPolicy authorizationPolicy3 in securityTokenPolicies)
				{
					if (authorizationPolicy3 is AuthorizationPolicy)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					ReadOnlyCollection<ClaimsIdentity> transportTokenIdentities = IdentityModelServiceAuthorizationManager.GetTransportTokenIdentities(securityToken);
					list.AddRange(transportTokenIdentities);
					IdentityModelServiceAuthorizationManager.EliminateTransportTokenPolicy(securityToken, transportTokenIdentities, list2);
				}
			}
			if (list2.Count > 0)
			{
				list.AddRange(IdentityModelServiceAuthorizationManager.ConvertToIDFxIdentities(list2, securityTokenHandlerCollection));
			}
			AuthorizationPolicy result;
			if (list.Count == 0)
			{
				result = new AuthorizationPolicy(new ClaimsIdentity());
			}
			else
			{
				result = new AuthorizationPolicy(list.AsReadOnly());
			}
			return result;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00075C08 File Offset: 0x00073E08
		private static ReadOnlyCollection<ClaimsIdentity> GetTransportTokenIdentities(SecurityToken transportToken)
		{
			if (transportToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("transportToken");
			}
			ServiceCredentials serviceCredentials = IdentityModelServiceAuthorizationManager.GetServiceCredentials();
			List<ClaimsIdentity> list = new List<ClaimsIdentity>();
			WindowsSecurityToken windowsSecurityToken = transportToken as WindowsSecurityToken;
			if (windowsSecurityToken != null)
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity(windowsSecurityToken.WindowsIdentity.Token, "Windows");
				IdentityModelServiceAuthorizationManager.AddAuthenticationMethod(windowsIdentity, "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/windows");
				IdentityModelServiceAuthorizationManager.AddAuthenticationInstantClaim(windowsIdentity, XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated));
				list.Add(windowsIdentity);
			}
			else
			{
				list.AddRange(serviceCredentials.IdentityConfiguration.SecurityTokenHandlers.ValidateToken(transportToken));
			}
			return list.AsReadOnly();
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00075C9C File Offset: 0x00073E9C
		private static void EliminateTransportTokenPolicy(SecurityToken transportToken, IEnumerable<ClaimsIdentity> tranportTokenIdentities, List<IAuthorizationPolicy> baseAuthorizationPolicies)
		{
			if (transportToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("transportToken");
			}
			if (tranportTokenIdentities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tranportTokenIdentities");
			}
			if (baseAuthorizationPolicies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseAuthorizationPolicy");
			}
			if (baseAuthorizationPolicies.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("baseAuthorizationPolicy", SR.GetString("ID0020"));
			}
			IAuthorizationPolicy authorizationPolicy = null;
			foreach (IAuthorizationPolicy authorizationPolicy2 in baseAuthorizationPolicies)
			{
				if (IdentityModelServiceAuthorizationManager.DoesPolicyMatchTransportToken(transportToken, tranportTokenIdentities, authorizationPolicy2))
				{
					authorizationPolicy = authorizationPolicy2;
					break;
				}
			}
			if (authorizationPolicy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4271", new object[]
				{
					transportToken
				}));
			}
			baseAuthorizationPolicies.Remove(authorizationPolicy);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00075D78 File Offset: 0x00073F78
		private static bool DoesPolicyMatchTransportToken(SecurityToken transportToken, IEnumerable<ClaimsIdentity> tranportTokenIdentities, IAuthorizationPolicy authPolicy)
		{
			if (transportToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("transportToken");
			}
			if (tranportTokenIdentities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tranportTokenIdentities");
			}
			if (authPolicy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authPolicy");
			}
			X509SecurityToken x509SecurityToken = transportToken as X509SecurityToken;
			System.IdentityModel.Policy.AuthorizationContext authorizationContext = System.IdentityModel.Policy.AuthorizationContext.CreateDefaultAuthorizationContext(new List<IAuthorizationPolicy>
			{
				authPolicy
			});
			foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
			{
				if (x509SecurityToken != null)
				{
					if (claimSet.ContainsClaim(new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Thumbprint, x509SecurityToken.Certificate.GetCertHash(), Rights.PossessProperty)))
					{
						return true;
					}
				}
				else
				{
					foreach (ClaimsIdentity claimsIdentity in tranportTokenIdentities)
					{
						if (claimSet.ContainsClaim(new System.IdentityModel.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Name, claimsIdentity.Name, Rights.PossessProperty), new ClaimStringValueComparer()))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00075EA4 File Offset: 0x000740A4
		private static ReadOnlyCollection<ClaimsIdentity> ConvertToIDFxIdentities(IList<IAuthorizationPolicy> authorizationPolicies, SecurityTokenHandlerCollection securityTokenHandlerCollection)
		{
			if (authorizationPolicies == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorizationPolicies");
			}
			if (securityTokenHandlerCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandlerCollection");
			}
			List<ClaimsIdentity> list = new List<ClaimsIdentity>();
			SecurityTokenSpecification securityTokenSpecification = null;
			System.IdentityModel.Policy.AuthorizationContext authorizationContext = null;
			if (OperationContext.Current != null && OperationContext.Current.IncomingMessageProperties != null && OperationContext.Current.IncomingMessageProperties.Security != null)
			{
				SecurityMessageProperty security = OperationContext.Current.IncomingMessageProperties.Security;
				foreach (SecurityTokenSpecification securityTokenSpecification2 in new SecurityTokenSpecificationEnumerable(security))
				{
					if (securityTokenSpecification2.SecurityToken is KerberosReceiverSecurityToken)
					{
						securityTokenSpecification = securityTokenSpecification2;
						authorizationContext = System.IdentityModel.Policy.AuthorizationContext.CreateDefaultAuthorizationContext(securityTokenSpecification.SecurityTokenPolicies);
						break;
					}
				}
			}
			bool flag = false;
			foreach (IAuthorizationPolicy authorizationPolicy in authorizationPolicies)
			{
				bool flag2 = false;
				if (securityTokenSpecification != null && !flag)
				{
					if (securityTokenSpecification.SecurityTokenPolicies.Contains(authorizationPolicy))
					{
						flag = true;
					}
					else
					{
						System.IdentityModel.Policy.AuthorizationContext authorizationContext2 = System.IdentityModel.Policy.AuthorizationContext.CreateDefaultAuthorizationContext(new List<IAuthorizationPolicy>
						{
							authorizationPolicy
						});
						if (authorizationContext2.ClaimSets.Count == 1)
						{
							bool flag3 = true;
							foreach (System.IdentityModel.Claims.Claim claim in authorizationContext2.ClaimSets[0])
							{
								if (!authorizationContext.ClaimSets[0].ContainsClaim(claim))
								{
									flag3 = false;
									break;
								}
							}
							flag = flag3;
						}
					}
					if (flag)
					{
						SecurityTokenHandler securityTokenHandler = securityTokenHandlerCollection[securityTokenSpecification.SecurityToken];
						if (securityTokenHandler != null && securityTokenHandler.CanValidateToken)
						{
							list.AddRange(securityTokenHandler.ValidateToken(securityTokenSpecification.SecurityToken));
							flag2 = true;
						}
					}
				}
				if (!flag2)
				{
					System.IdentityModel.Policy.AuthorizationContext authorizationContext3 = System.IdentityModel.Policy.AuthorizationContext.CreateDefaultAuthorizationContext(new List<IAuthorizationPolicy>
					{
						authorizationPolicy
					});
					list.Add(IdentityModelServiceAuthorizationManager.ConvertToIDFxIdentity(authorizationContext3.ClaimSets, securityTokenHandlerCollection.Configuration));
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000760E8 File Offset: 0x000742E8
		private static ClaimsIdentity ConvertToIDFxIdentity(IList<ClaimSet> claimSets, SecurityTokenHandlerConfiguration securityTokenHandlerConfiguration)
		{
			if (claimSets == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimSets");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimSet claimSet in claimSets)
			{
				WindowsClaimSet windowsClaimSet = claimSet as WindowsClaimSet;
				if (windowsClaimSet != null)
				{
					claimsIdentity = IdentityModelServiceAuthorizationManager.MergeClaims(claimsIdentity, new WindowsIdentity(windowsClaimSet.WindowsIdentity.Token, "Negotiate"));
					IdentityModelServiceAuthorizationManager.AddAuthenticationMethod(claimsIdentity, "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/windows");
					IdentityModelServiceAuthorizationManager.AddAuthenticationInstantClaim(claimsIdentity, XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated));
				}
				else
				{
					claimsIdentity = IdentityModelServiceAuthorizationManager.MergeClaims(claimsIdentity, ClaimsConversionHelper.CreateClaimsIdentityFromClaimSet(claimSet));
					IdentityModelServiceAuthorizationManager.AddAuthenticationInstantClaim(claimsIdentity, XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated));
				}
			}
			return claimsIdentity;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000761AC File Offset: 0x000743AC
		private static ServiceCredentials GetServiceCredentials()
		{
			ServiceCredentials result = null;
			if (OperationContext.Current != null && OperationContext.Current.Host != null && OperationContext.Current.Host.Description != null && OperationContext.Current.Host.Description.Behaviors != null)
			{
				result = OperationContext.Current.Host.Description.Behaviors.Find<ServiceCredentials>();
			}
			return result;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00076210 File Offset: 0x00074410
		private static void AddAuthenticationMethod(ClaimsIdentity claimsIdentity, string authenticationMethod)
		{
			if (claimsIdentity.Claims.FirstOrDefault((System.Security.Claims.Claim claim) => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod") == null)
			{
				claimsIdentity.AddClaim(new System.Security.Claims.Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", authenticationMethod));
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0007625C File Offset: 0x0007445C
		private static void AddAuthenticationInstantClaim(ClaimsIdentity claimsIdentity, string authenticationInstant)
		{
			string issuer = "LOCAL AUTHORITY";
			if (claimsIdentity.Claims.FirstOrDefault((System.Security.Claims.Claim claim) => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant") == null)
			{
				claimsIdentity.AddClaim(new System.Security.Claims.Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", authenticationInstant, "http://www.w3.org/2001/XMLSchema#dateTime", issuer));
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000762B4 File Offset: 0x000744B4
		internal static ClaimsIdentity MergeClaims(ClaimsIdentity identity1, ClaimsIdentity identity2)
		{
			if (identity1 == null && identity2 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4268"));
			}
			if (identity1 == null)
			{
				return identity2;
			}
			if (identity2 == null)
			{
				return identity1;
			}
			WindowsIdentity windowsIdentity = identity1 as WindowsIdentity;
			if (windowsIdentity != null)
			{
				windowsIdentity.AddClaims(identity2.Claims);
				return windowsIdentity;
			}
			windowsIdentity = (identity2 as WindowsIdentity);
			if (windowsIdentity != null)
			{
				windowsIdentity.AddClaims(identity1.Claims);
				return windowsIdentity;
			}
			identity1.AddClaims(identity2.Claims);
			return identity1;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00076324 File Offset: 0x00074524
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			if (operationContext == null)
			{
				return false;
			}
			string text = string.Empty;
			if (!string.IsNullOrEmpty(operationContext.IncomingMessageHeaders.Action))
			{
				text = operationContext.IncomingMessageHeaders.Action;
			}
			else
			{
				HttpRequestMessageProperty httpRequestMessageProperty = operationContext.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
				if (httpRequestMessageProperty != null)
				{
					text = httpRequestMessageProperty.Method;
				}
			}
			Uri to = operationContext.IncomingMessageHeaders.To;
			ServiceCredentials serviceCredentials = IdentityModelServiceAuthorizationManager.GetServiceCredentials();
			if (serviceCredentials == null || string.IsNullOrEmpty(text) || to == null)
			{
				return false;
			}
			ClaimsPrincipal claimsPrincipal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["ClaimsPrincipal"] as ClaimsPrincipal;
			claimsPrincipal = serviceCredentials.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate(to.AbsoluteUri, claimsPrincipal);
			operationContext.ServiceSecurityContext.AuthorizationContext.Properties["ClaimsPrincipal"] = claimsPrincipal;
			if (claimsPrincipal == null || claimsPrincipal.Identities == null)
			{
				return false;
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 458752, SR.GetString("TraceAuthorize"), new AuthorizeTraceRecord(claimsPrincipal, to.AbsoluteUri, text));
			}
			bool flag = serviceCredentials.IdentityConfiguration.ClaimsAuthorizationManager.CheckAccess(new System.Security.Claims.AuthorizationContext(claimsPrincipal, to.AbsoluteUri, text));
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				if (flag)
				{
					TraceUtility.TraceString(TraceEventType.Information, SR.GetString("TraceOnAuthorizeRequestSucceed"), new object[0]);
				}
				else
				{
					TraceUtility.TraceString(TraceEventType.Information, SR.GetString("TraceOnAuthorizeRequestFailed"), new object[0]);
				}
			}
			return flag;
		}

		// Token: 0x04001EE8 RID: 7912
		protected static readonly ReadOnlyCollection<IAuthorizationPolicy> AnonymousAuthorizationPolicy = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy>
		{
			new AuthorizationPolicy(new ClaimsIdentity())
		});
	}
}

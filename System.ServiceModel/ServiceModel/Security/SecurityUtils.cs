using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Security;
using System.Runtime;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Win32;

namespace System.ServiceModel.Security
{
	// Token: 0x02000351 RID: 849
	internal static class SecurityUtils
	{
		// Token: 0x06001EBC RID: 7868 RVA: 0x00071C90 File Offset: 0x0006FE90
		public static ChannelBinding GetChannelBindingFromMessage(Message message)
		{
			if (message == null)
			{
				return null;
			}
			ChannelBindingMessageProperty channelBindingMessageProperty = null;
			ChannelBindingMessageProperty.TryGet(message, out channelBindingMessageProperty);
			ChannelBinding result = null;
			if (channelBindingMessageProperty != null)
			{
				result = channelBindingMessageProperty.ChannelBinding;
			}
			return result;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00071CBA File Offset: 0x0006FEBA
		internal static bool IsOsGreaterThanXP()
		{
			return (Environment.OSVersion.Version.Major >= 5 && Environment.OSVersion.Version.Minor > 1) || Environment.OSVersion.Version.Major > 5;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		internal static bool IsOSGreaterThanOrEqualToWin7()
		{
			Version version = new Version(6, 1, 0, 0);
			return Environment.OSVersion.Version.Major >= version.Major && Environment.OSVersion.Version.Minor >= version.Minor;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00071D40 File Offset: 0x0006FF40
		internal static bool IsCurrentlyTimeEffective(DateTime effectiveTime, DateTime expirationTime, TimeSpan maxClockSkew)
		{
			DateTime dateTime = (effectiveTime < DateTime.MinValue.Add(maxClockSkew)) ? effectiveTime : effectiveTime.Subtract(maxClockSkew);
			DateTime dateTime2 = (expirationTime > DateTime.MaxValue.Subtract(maxClockSkew)) ? expirationTime : expirationTime.Add(maxClockSkew);
			DateTime utcNow = DateTime.UtcNow;
			return dateTime.ToUniversalTime() <= utcNow && utcNow < dateTime2.ToUniversalTime();
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00071DB5 File Offset: 0x0006FFB5
		internal static X509SecurityTokenAuthenticator NonValidatingX509Authenticator
		{
			get
			{
				if (SecurityUtils.nonValidatingX509Authenticator == null)
				{
					SecurityUtils.nonValidatingX509Authenticator = new X509SecurityTokenAuthenticator(X509CertificateValidator.None);
				}
				return SecurityUtils.nonValidatingX509Authenticator;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00071DD2 File Offset: 0x0006FFD2
		public static SecurityIdentifier AdministratorsSid
		{
			get
			{
				if (SecurityUtils.administratorsSid == null)
				{
					SecurityUtils.administratorsSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
				}
				return SecurityUtils.administratorsSid;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x00071DF3 File Offset: 0x0006FFF3
		internal static IIdentity AnonymousIdentity
		{
			get
			{
				if (SecurityUtils.anonymousIdentity == null)
				{
					SecurityUtils.anonymousIdentity = SecurityUtils.CreateIdentity(string.Empty);
				}
				return SecurityUtils.anonymousIdentity;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00071E10 File Offset: 0x00070010
		public static DateTime MaxUtcDateTime
		{
			get
			{
				return new DateTime(DateTime.MaxValue.Ticks - 864000000000L, DateTimeKind.Utc);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x00071E3C File Offset: 0x0007003C
		public static DateTime MinUtcDateTime
		{
			get
			{
				return new DateTime(DateTime.MinValue.Ticks + 864000000000L, DateTimeKind.Utc);
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00071E66 File Offset: 0x00070066
		internal static IIdentity CreateIdentity(string name, string authenticationType)
		{
			return new GenericIdentity(name, authenticationType);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00071E6F File Offset: 0x0007006F
		internal static IIdentity CreateIdentity(string name)
		{
			return new GenericIdentity(name);
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00071E77 File Offset: 0x00070077
		internal static EndpointIdentity CreateWindowsIdentity()
		{
			return SecurityUtils.CreateWindowsIdentity(false);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00071E80 File Offset: 0x00070080
		internal static EndpointIdentity CreateWindowsIdentity(NetworkCredential serverCredential)
		{
			if (serverCredential != null && !SecurityUtils.NetworkCredentialHelper.IsDefault(serverCredential))
			{
				string upnName;
				if (serverCredential.Domain != null && serverCredential.Domain.Length > 0)
				{
					upnName = serverCredential.UserName + "@" + serverCredential.Domain;
				}
				else
				{
					upnName = serverCredential.UserName;
				}
				return EndpointIdentity.CreateUpnIdentity(upnName);
			}
			return SecurityUtils.CreateWindowsIdentity();
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00071EDC File Offset: 0x000700DC
		private static bool IsSystemAccount(WindowsIdentity self)
		{
			SecurityIdentifier user = self.User;
			return !(user == null) && (user.IsWellKnown(WellKnownSidType.LocalSystemSid) || user.IsWellKnown(WellKnownSidType.NetworkServiceSid) || user.IsWellKnown(WellKnownSidType.LocalServiceSid) || self.User.Value.StartsWith("S-1-5-82", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00071F34 File Offset: 0x00070134
		internal static EndpointIdentity CreateWindowsIdentity(bool spnOnly)
		{
			EndpointIdentity result = null;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				bool flag = SecurityUtils.IsSystemAccount(current);
				if (spnOnly || flag)
				{
					result = EndpointIdentity.CreateSpnIdentity(string.Format(CultureInfo.InvariantCulture, "host/{0}", new object[]
					{
						DnsCache.MachineName
					}));
				}
				else
				{
					result = new UpnEndpointIdentity(SecurityUtils.CloneWindowsIdentityIfNecessary(current));
				}
			}
			return result;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00071FA4 File Offset: 0x000701A4
		[SecuritySafeCritical]
		internal static WindowsIdentity CloneWindowsIdentityIfNecessary(WindowsIdentity wid)
		{
			return SecurityUtils.CloneWindowsIdentityIfNecessary(wid, null);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00071FB0 File Offset: 0x000701B0
		[SecuritySafeCritical]
		internal static WindowsIdentity CloneWindowsIdentityIfNecessary(WindowsIdentity wid, string authType)
		{
			if (wid != null)
			{
				IntPtr intPtr = SecurityUtils.UnsafeGetWindowsIdentityToken(wid);
				if (intPtr != IntPtr.Zero)
				{
					return SecurityUtils.UnsafeCreateWindowsIdentityFromToken(intPtr, authType);
				}
			}
			return wid;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00071FDD File Offset: 0x000701DD
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static IntPtr UnsafeGetWindowsIdentityToken(WindowsIdentity wid)
		{
			return wid.Token;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00071FE8 File Offset: 0x000701E8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private static string UnsafeGetCurrentUserSidAsString()
		{
			string value;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				value = current.User.Value;
			}
			return value;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00072024 File Offset: 0x00070224
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true, UnmanagedCode = true)]
		private static WindowsIdentity UnsafeCreateWindowsIdentityFromToken(IntPtr token, string authType)
		{
			if (authType != null)
			{
				return new WindowsIdentity(token, authType);
			}
			return new WindowsIdentity(token);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00072038 File Offset: 0x00070238
		internal static bool AllowsImpersonation(WindowsIdentity windowsIdentity, TokenImpersonationLevel impersonationLevel)
		{
			if (windowsIdentity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windowsIdentity");
			}
			TokenImpersonationLevelHelper.Validate(impersonationLevel);
			if (impersonationLevel == TokenImpersonationLevel.Identification)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("impersonationLevel"));
			}
			bool result = true;
			switch (windowsIdentity.ImpersonationLevel)
			{
			case TokenImpersonationLevel.None:
			case TokenImpersonationLevel.Anonymous:
			case TokenImpersonationLevel.Identification:
				result = false;
				break;
			case TokenImpersonationLevel.Impersonation:
				if (impersonationLevel == TokenImpersonationLevel.Delegation)
				{
					result = false;
				}
				break;
			case TokenImpersonationLevel.Delegation:
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x000720AB File Offset: 0x000702AB
		internal static byte[] CombinedHashLabel
		{
			get
			{
				if (SecurityUtils.combinedHashLabel == null)
				{
					SecurityUtils.combinedHashLabel = Encoding.UTF8.GetBytes("AUTH-HASH");
				}
				return SecurityUtils.combinedHashLabel;
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000720D0 File Offset: 0x000702D0
		internal static T GetSecurityKey<T>(SecurityToken token) where T : SecurityKey
		{
			T t = default(T);
			if (token.SecurityKeys != null)
			{
				for (int i = 0; i < token.SecurityKeys.Count; i++)
				{
					T t2 = token.SecurityKeys[i] as T;
					if (t2 != null)
					{
						if (t != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MultipleMatchingCryptosFound", new object[]
							{
								typeof(T).ToString()
							})));
						}
						t = t2;
					}
				}
			}
			return t;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0007215F File Offset: 0x0007035F
		internal static bool HasSymmetricSecurityKey(SecurityToken token)
		{
			return SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(token) != null;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0007216C File Offset: 0x0007036C
		internal static void EnsureExpectedSymmetricMatch(SecurityToken t1, SecurityToken t2, Message message)
		{
			if (t1 == null || t2 == null || t1 == t2)
			{
				return;
			}
			SymmetricSecurityKey securityKey = SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(t1);
			SymmetricSecurityKey securityKey2 = SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(t2);
			if (securityKey == null || securityKey2 == null || !CryptoHelper.IsEqual(securityKey.GetSymmetricKey(), securityKey2.GetSymmetricKey()))
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenNotExpectedInSecurityHeader", new object[]
				{
					t2
				})), message);
			}
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000721CC File Offset: 0x000703CC
		internal static SymmetricAlgorithm GetSymmetricAlgorithm(string algorithm, SecurityToken token)
		{
			SymmetricSecurityKey securityKey = SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(token);
			if (securityKey != null && securityKey.IsSupportedAlgorithm(algorithm))
			{
				return securityKey.GetSymmetricAlgorithm(algorithm);
			}
			return null;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000721F8 File Offset: 0x000703F8
		internal static KeyedHashAlgorithm GetKeyedHashAlgorithm(string algorithm, SecurityToken token)
		{
			SymmetricSecurityKey securityKey = SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(token);
			if (securityKey != null && securityKey.IsSupportedAlgorithm(algorithm))
			{
				return securityKey.GetKeyedHashAlgorithm(algorithm);
			}
			return null;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00072224 File Offset: 0x00070424
		internal static ReadOnlyCollection<SecurityKey> CreateSymmetricSecurityKeys(byte[] key)
		{
			return new List<SecurityKey>(1)
			{
				new InMemorySymmetricSecurityKey(key)
			}.AsReadOnly();
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0007224C File Offset: 0x0007044C
		internal static byte[] DecryptKey(SecurityToken unwrappingToken, string encryptionMethod, byte[] wrappedKey, out SecurityKey unwrappingSecurityKey)
		{
			unwrappingSecurityKey = null;
			if (unwrappingToken.SecurityKeys != null)
			{
				for (int i = 0; i < unwrappingToken.SecurityKeys.Count; i++)
				{
					if (unwrappingToken.SecurityKeys[i].IsSupportedAlgorithm(encryptionMethod))
					{
						unwrappingSecurityKey = unwrappingToken.SecurityKeys[i];
						break;
					}
				}
			}
			if (unwrappingSecurityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("CannotFindMatchingCrypto", new object[]
				{
					encryptionMethod
				})));
			}
			return unwrappingSecurityKey.DecryptKey(encryptionMethod, wrappedKey);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000722D0 File Offset: 0x000704D0
		internal static byte[] EncryptKey(SecurityToken wrappingToken, string encryptionMethod, byte[] keyToWrap)
		{
			SecurityKey securityKey = null;
			if (wrappingToken.SecurityKeys != null)
			{
				for (int i = 0; i < wrappingToken.SecurityKeys.Count; i++)
				{
					if (wrappingToken.SecurityKeys[i].IsSupportedAlgorithm(encryptionMethod))
					{
						securityKey = wrappingToken.SecurityKeys[i];
						break;
					}
				}
			}
			if (securityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("CannotFindMatchingCrypto", new object[]
				{
					encryptionMethod
				}));
			}
			return securityKey.EncryptKey(encryptionMethod, keyToWrap);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0007234C File Offset: 0x0007054C
		internal static byte[] ReadContentAsBase64(XmlDictionaryReader reader, long maxBufferSize)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			byte[][] array = new byte[32][];
			int num = 384;
			int num2 = 0;
			int num3 = 0;
			byte[] array2;
			for (;;)
			{
				array2 = new byte[num];
				array[num2++] = array2;
				int i;
				int num4;
				for (i = 0; i < array2.Length; i += num4)
				{
					num4 = reader.ReadContentAsBase64(array2, i, array2.Length - i);
					if (num4 == 0)
					{
						break;
					}
				}
				if ((long)num3 > maxBufferSize - (long)i)
				{
					break;
				}
				num3 += i;
				if (i < array2.Length)
				{
					goto IL_A4;
				}
				num *= 2;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QuotaExceededException(SR.GetString("BufferQuotaExceededReadingBase64", new object[]
			{
				maxBufferSize
			})));
			IL_A4:
			array2 = new byte[num3];
			int num5 = 0;
			for (int j = 0; j < num2 - 1; j++)
			{
				Buffer.BlockCopy(array[j], 0, array2, num5, array[j].Length);
				num5 += array[j].Length;
			}
			Buffer.BlockCopy(array[num2 - 1], 0, array2, num5, num3 - num5);
			return array2;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0007244C File Offset: 0x0007064C
		internal static byte[] GenerateDerivedKey(SecurityToken tokenToDerive, string derivationAlgorithm, byte[] label, byte[] nonce, int keySize, int offset)
		{
			SymmetricSecurityKey securityKey = SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(tokenToDerive);
			if (securityKey == null || !securityKey.IsSupportedAlgorithm(derivationAlgorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("CannotFindMatchingCrypto", new object[]
				{
					derivationAlgorithm
				})));
			}
			return securityKey.GenerateDerivedKey(derivationAlgorithm, label, nonce, keySize, offset);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000724A0 File Offset: 0x000706A0
		internal static string GetSpnFromIdentity(EndpointIdentity identity, EndpointAddress target)
		{
			bool flag = false;
			string result = null;
			if (identity != null)
			{
				if (ClaimTypes.Spn.Equals(identity.IdentityClaim.ClaimType))
				{
					result = (string)identity.IdentityClaim.Resource;
					flag = true;
				}
				else if (ClaimTypes.Upn.Equals(identity.IdentityClaim.ClaimType))
				{
					result = (string)identity.IdentityClaim.Resource;
					flag = true;
				}
				else if (ClaimTypes.Dns.Equals(identity.IdentityClaim.ClaimType))
				{
					result = string.Format(CultureInfo.InvariantCulture, "host/{0}", new object[]
					{
						(string)identity.IdentityClaim.Resource
					});
					flag = true;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("CannotDetermineSPNBasedOnAddress", new object[]
				{
					target
				})));
			}
			return result;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00072579 File Offset: 0x00070779
		internal static string GetSpnFromTarget(EndpointAddress target)
		{
			if (target == null)
			{
				throw Fx.AssertAndThrow("target should not be null - expecting an EndpointAddress");
			}
			return string.Format(CultureInfo.InvariantCulture, "host/{0}", new object[]
			{
				target.Uri.DnsSafeHost
			});
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000725B4 File Offset: 0x000707B4
		internal static bool IsSupportedAlgorithm(string algorithm, SecurityToken token)
		{
			if (token.SecurityKeys == null)
			{
				return false;
			}
			for (int i = 0; i < token.SecurityKeys.Count; i++)
			{
				if (token.SecurityKeys[i].IsSupportedAlgorithm(algorithm))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000725F8 File Offset: 0x000707F8
		internal static Claim GetPrimaryIdentityClaim(ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			return SecurityUtils.GetPrimaryIdentityClaim(AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies));
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00072608 File Offset: 0x00070808
		internal static Claim GetPrimaryIdentityClaim(AuthorizationContext authContext)
		{
			if (authContext != null)
			{
				for (int i = 0; i < authContext.ClaimSets.Count; i++)
				{
					ClaimSet claimSet = authContext.ClaimSets[i];
					using (IEnumerator<Claim> enumerator = claimSet.FindClaims(null, Rights.Identity).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							return enumerator.Current;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00072684 File Offset: 0x00070884
		internal static int GetServiceAddressAndViaHash(EndpointAddress sr)
		{
			if (sr == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sr");
			}
			return sr.GetHashCode();
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000726A8 File Offset: 0x000708A8
		internal static string GenerateId()
		{
			return SecurityUniqueId.Create().Value;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000726C4 File Offset: 0x000708C4
		internal static string GenerateIdWithPrefix(string prefix)
		{
			return SecurityUniqueId.Create(prefix).Value;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000726DF File Offset: 0x000708DF
		internal static UniqueId GenerateUniqueId()
		{
			return new UniqueId();
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000726E8 File Offset: 0x000708E8
		internal static string GetPrimaryDomain()
		{
			string primaryDomain;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				primaryDomain = SecurityUtils.GetPrimaryDomain(SecurityUtils.IsSystemAccount(current));
			}
			return primaryDomain;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00072724 File Offset: 0x00070924
		internal static string GetPrimaryDomain(bool isSystemAccount)
		{
			if (!SecurityUtils.computedDomain)
			{
				try
				{
					if (isSystemAccount)
					{
						SecurityUtils.currentDomain = Domain.GetComputerDomain().Name;
					}
					else
					{
						SecurityUtils.currentDomain = Domain.GetCurrentDomain().Name;
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				}
				finally
				{
					SecurityUtils.computedDomain = true;
				}
			}
			return SecurityUtils.currentDomain;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0007279C File Offset: 0x0007099C
		internal static void EnsureCertificateCanDoKeyExchange(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			bool flag = false;
			Exception innerException = null;
			if (certificate.HasPrivateKey)
			{
				try
				{
					flag = SecurityUtils.CanKeyDoKeyExchange(certificate);
				}
				catch (SecurityException ex)
				{
					innerException = ex;
				}
				catch (CryptographicException ex2)
				{
					innerException = ex2;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SslCertMayNotDoKeyExchange", new object[]
				{
					certificate.SubjectName.Name
				}), innerException));
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0007282C File Offset: 0x00070A2C
		[SecuritySafeCritical]
		private static bool CanKeyDoKeyExchange(X509Certificate2 certificate)
		{
			bool flag = false;
			if (!LocalAppContextSwitches.DisableCngCertificates)
			{
				X509KeyUsageExtension x509KeyUsageExtension = null;
				for (int i = 0; i < certificate.Extensions.Count; i++)
				{
					x509KeyUsageExtension = (certificate.Extensions[i] as X509KeyUsageExtension);
					if (x509KeyUsageExtension != null)
					{
						break;
					}
				}
				if (x509KeyUsageExtension == null || !x509KeyUsageExtension.Critical)
				{
					return true;
				}
				flag = ((x509KeyUsageExtension.KeyUsages & (X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature)) > X509KeyUsageFlags.None);
			}
			if (!flag)
			{
				CspKeyContainerInfo keyContainerInfo = SecurityUtils.GetKeyContainerInfo(certificate);
				flag = (keyContainerInfo != null && keyContainerInfo.KeyNumber == KeyNumber.Exchange);
			}
			return flag;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x000728A8 File Offset: 0x00070AA8
		[SecurityCritical]
		[KeyContainerPermission(SecurityAction.Assert, Flags = KeyContainerPermissionFlags.Open)]
		private static CspKeyContainerInfo GetKeyContainerInfo(X509Certificate2 certificate)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = certificate.PrivateKey as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return rsacryptoServiceProvider.CspKeyContainerInfo;
			}
			return null;
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x000728CC File Offset: 0x00070ACC
		internal static string GetCertificateId(X509Certificate2 certificate)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			SecurityUtils.AppendCertificateIdentityName(stringBuilder, certificate);
			return stringBuilder.ToString();
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000728F4 File Offset: 0x00070AF4
		internal static ReadOnlyCollection<IAuthorizationPolicy> CreatePrincipalNameAuthorizationPolicies(string principalName)
		{
			if (principalName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("principalName");
			}
			Claim item;
			Claim item2;
			if (principalName.Contains("@") || principalName.Contains("\\"))
			{
				item = new Claim(ClaimTypes.Upn, principalName, Rights.Identity);
				item2 = Claim.CreateUpnClaim(principalName);
			}
			else
			{
				item = new Claim(ClaimTypes.Spn, principalName, Rights.Identity);
				item2 = Claim.CreateSpnClaim(principalName);
			}
			List<Claim> list = new List<Claim>(2);
			list.Add(item);
			list.Add(item2);
			return new List<IAuthorizationPolicy>(1)
			{
				new UnconditionalPolicy(SecurityUtils.CreateIdentity(principalName), new DefaultClaimSet(ClaimSet.Anonymous, list))
			}.AsReadOnly();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0007299E File Offset: 0x00070B9E
		internal static string GetIdentityNamesFromPolicies(IList<IAuthorizationPolicy> authPolicies)
		{
			return SecurityUtils.GetIdentityNamesFromContext(AuthorizationContext.CreateDefaultAuthorizationContext(authPolicies));
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000729AC File Offset: 0x00070BAC
		internal static string GetIdentityNamesFromContext(AuthorizationContext authContext)
		{
			if (authContext == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			for (int i = 0; i < authContext.ClaimSets.Count; i++)
			{
				ClaimSet claimSet = authContext.ClaimSets[i];
				WindowsClaimSet windowsClaimSet = claimSet as WindowsClaimSet;
				if (windowsClaimSet != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					SecurityUtils.AppendIdentityName(stringBuilder, windowsClaimSet.WindowsIdentity);
				}
				else
				{
					X509CertificateClaimSet x509CertificateClaimSet = claimSet as X509CertificateClaimSet;
					if (x509CertificateClaimSet != null)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(", ");
						}
						SecurityUtils.AppendCertificateIdentityName(stringBuilder, x509CertificateClaimSet.X509Certificate);
					}
				}
			}
			if (stringBuilder.Length <= 0)
			{
				List<IIdentity> list = null;
				object obj;
				if (authContext.Properties.TryGetValue("Identities", out obj))
				{
					list = (obj as List<IIdentity>);
				}
				if (list != null)
				{
					for (int j = 0; j < list.Count; j++)
					{
						IIdentity identity = list[j];
						if (identity != null)
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(", ");
							}
							SecurityUtils.AppendIdentityName(stringBuilder, identity);
						}
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00072AD0 File Offset: 0x00070CD0
		internal static void AppendCertificateIdentityName(StringBuilder str, X509Certificate2 certificate)
		{
			string text = certificate.SubjectName.Name;
			if (string.IsNullOrEmpty(text))
			{
				text = certificate.GetNameInfo(X509NameType.DnsName, false);
				if (string.IsNullOrEmpty(text))
				{
					text = certificate.GetNameInfo(X509NameType.SimpleName, false);
					if (string.IsNullOrEmpty(text))
					{
						text = certificate.GetNameInfo(X509NameType.EmailName, false);
						if (string.IsNullOrEmpty(text))
						{
							text = certificate.GetNameInfo(X509NameType.UpnName, false);
						}
					}
				}
			}
			str.Append(string.IsNullOrEmpty(text) ? "<x509>" : text);
			str.Append("; ");
			str.Append(certificate.Thumbprint);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00072B60 File Offset: 0x00070D60
		internal static void AppendIdentityName(StringBuilder str, IIdentity identity)
		{
			string text = null;
			try
			{
				text = identity.Name;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			str.Append(string.IsNullOrEmpty(text) ? "<null>" : text);
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				if (windowsIdentity.User != null)
				{
					str.Append("; ");
					str.Append(windowsIdentity.User.ToString());
					return;
				}
			}
			else
			{
				WindowsSidIdentity windowsSidIdentity = identity as WindowsSidIdentity;
				if (windowsSidIdentity != null)
				{
					str.Append("; ");
					str.Append(windowsSidIdentity.SecurityIdentifier.ToString());
				}
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00072C0C File Offset: 0x00070E0C
		[SecurityCritical]
		internal static string AppendWindowsAuthenticationInfo(string inputString, NetworkCredential credential, AuthenticationLevel authenticationLevel, TokenImpersonationLevel impersonationLevel)
		{
			if (SecurityUtils.IsDefaultNetworkCredential(credential))
			{
				string text = SecurityUtils.UnsafeGetCurrentUserSidAsString();
				return string.Concat(new string[]
				{
					inputString,
					"\0",
					text,
					"\0",
					AuthenticationLevelHelper.ToString(authenticationLevel),
					"\0",
					TokenImpersonationLevelHelper.ToString(impersonationLevel)
				});
			}
			return string.Concat(new string[]
			{
				inputString,
				"\0",
				SecurityUtils.NetworkCredentialHelper.UnsafeGetDomain(credential),
				"\0",
				SecurityUtils.NetworkCredentialHelper.UnsafeGetUsername(credential),
				"\0",
				SecurityUtils.NetworkCredentialHelper.UnsafeGetPassword(credential),
				"\0",
				AuthenticationLevelHelper.ToString(authenticationLevel),
				"\0",
				TokenImpersonationLevelHelper.ToString(impersonationLevel)
			});
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00072CCC File Offset: 0x00070ECC
		internal static string GetIdentityName(IIdentity identity)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			SecurityUtils.AppendIdentityName(stringBuilder, identity);
			return stringBuilder.ToString();
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00072CF1 File Offset: 0x00070EF1
		internal static bool IsChannelBindingDisabled
		{
			[SecuritySafeCritical]
			get
			{
				return (SecurityUtils.GetSuppressChannelBindingValue() & 1) != 0;
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00072D00 File Offset: 0x00070F00
		[SecurityCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Lsa")]
		internal static int GetSuppressChannelBindingValue()
		{
			int result = 0;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa", false))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("SuppressChannelBindingInfo");
						if (value != null)
						{
							result = (int)value;
						}
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00072D74 File Offset: 0x00070F74
		internal static bool IsSecurityBindingSuitableForChannelBinding(TransportSecurityBindingElement securityBindingElement)
		{
			return securityBindingElement != null && (SecurityUtils.AreSecurityTokenParametersSuitableForChannelBinding(securityBindingElement.EndpointSupportingTokenParameters.Endorsing) || SecurityUtils.AreSecurityTokenParametersSuitableForChannelBinding(securityBindingElement.EndpointSupportingTokenParameters.Signed) || SecurityUtils.AreSecurityTokenParametersSuitableForChannelBinding(securityBindingElement.EndpointSupportingTokenParameters.SignedEncrypted) || SecurityUtils.AreSecurityTokenParametersSuitableForChannelBinding(securityBindingElement.EndpointSupportingTokenParameters.SignedEndorsing));
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00072DD8 File Offset: 0x00070FD8
		internal static bool AreSecurityTokenParametersSuitableForChannelBinding(Collection<SecurityTokenParameters> tokenParameters)
		{
			if (tokenParameters == null)
			{
				return false;
			}
			foreach (SecurityTokenParameters securityTokenParameters in tokenParameters)
			{
				if (securityTokenParameters is SspiSecurityTokenParameters || securityTokenParameters is KerberosSecurityTokenParameters)
				{
					return true;
				}
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = securityTokenParameters as SecureConversationSecurityTokenParameters;
				if (secureConversationSecurityTokenParameters != null)
				{
					return SecurityUtils.IsSecurityBindingSuitableForChannelBinding(secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement as TransportSecurityBindingElement);
				}
			}
			return false;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00072E54 File Offset: 0x00071054
		internal static void ThrowIfNegotiationFault(Message message, EndpointAddress target)
		{
			if (message.IsFault)
			{
				MessageFault messageFault = MessageFault.CreateFault(message, 16384);
				Exception ex = new FaultException(messageFault, message.Headers.Action);
				if (messageFault.Code != null && messageFault.Code.IsReceiverFault && messageFault.Code.SubCode != null)
				{
					FaultCode subCode = messageFault.Code.SubCode;
					if (subCode.Name == "ServerTooBusy" && subCode.Namespace == "http://schemas.microsoft.com/ws/2006/05/security")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ServerTooBusyException(SR.GetString("SecurityServerTooBusy", new object[]
						{
							target
						}), ex));
					}
					if (subCode.Name == "EndpointUnavailable" && subCode.Namespace == message.Version.Addressing.Namespace)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("SecurityEndpointNotFound", new object[]
						{
							target
						}), ex));
					}
				}
				throw TraceUtility.ThrowHelperError(ex, message);
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00072F68 File Offset: 0x00071168
		internal static bool IsSecurityFault(MessageFault fault, SecurityStandardsManager standardsManager)
		{
			if (fault.Code.IsSenderFault)
			{
				FaultCode subCode = fault.Code.SubCode;
				if (subCode != null)
				{
					return subCode.Namespace == standardsManager.SecurityVersion.HeaderNamespace.Value || subCode.Namespace == standardsManager.SecureConversationDriver.Namespace.Value || subCode.Namespace == standardsManager.TrustDriver.Namespace.Value || subCode.Namespace == "http://schemas.microsoft.com/ws/2006/05/security";
				}
			}
			return false;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00072FFC File Offset: 0x000711FC
		internal static Exception CreateSecurityFaultException(Message unverifiedMessage)
		{
			MessageFault fault = MessageFault.CreateFault(unverifiedMessage, 16384);
			return SecurityUtils.CreateSecurityFaultException(fault);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0007301C File Offset: 0x0007121C
		internal static Exception CreateSecurityFaultException(MessageFault fault)
		{
			FaultException innerException = FaultException.CreateFault(fault, new Type[]
			{
				typeof(string),
				typeof(object)
			});
			return new MessageSecurityException(SR.GetString("UnsecuredMessageFaultReceived"), innerException);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00073060 File Offset: 0x00071260
		internal static MessageFault CreateSecurityContextNotFoundFault(SecurityStandardsManager standardsManager, string action)
		{
			SecureConversationDriver secureConversationDriver = standardsManager.SecureConversationDriver;
			FaultCode subCode = new FaultCode(secureConversationDriver.BadContextTokenFaultCode.Value, secureConversationDriver.Namespace.Value);
			FaultReason reason;
			if (action != null)
			{
				reason = new FaultReason(SR.GetString("BadContextTokenOrActionFaultReason", new object[]
				{
					action
				}), CultureInfo.CurrentCulture);
			}
			else
			{
				reason = new FaultReason(SR.GetString("BadContextTokenFaultReason"), CultureInfo.CurrentCulture);
			}
			FaultCode code = FaultCode.CreateSenderFaultCode(subCode);
			return MessageFault.CreateFault(code, reason);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000730D8 File Offset: 0x000712D8
		internal static MessageFault CreateSecurityMessageFault(Exception e, SecurityStandardsManager standardsManager)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			FaultException ex = null;
			while (e != null)
			{
				if (e is SecurityTokenValidationException)
				{
					if (e is SecurityContextTokenValidationException)
					{
						return SecurityUtils.CreateSecurityContextNotFoundFault(SecurityStandardsManager.DefaultInstance, null);
					}
					flag = true;
					flag2 = true;
					break;
				}
				else
				{
					if (e is SecurityTokenException)
					{
						flag = true;
						flag3 = true;
						break;
					}
					if (e is MessageSecurityException)
					{
						MessageSecurityException ex2 = (MessageSecurityException)e;
						if (ex2.Fault != null)
						{
							return ex2.Fault;
						}
						flag = true;
					}
					else if (e is FaultException)
					{
						ex = (FaultException)e;
						break;
					}
					e = e.InnerException;
				}
			}
			if (!flag && ex == null)
			{
				return null;
			}
			SecurityVersion securityVersion = standardsManager.SecurityVersion;
			FaultCode subCode;
			FaultReason reason;
			if (flag2)
			{
				subCode = new FaultCode(securityVersion.FailedAuthenticationFaultCode.Value, securityVersion.HeaderNamespace.Value);
				reason = new FaultReason(SR.GetString("FailedAuthenticationFaultReason"), CultureInfo.CurrentCulture);
			}
			else if (flag3)
			{
				subCode = new FaultCode(securityVersion.InvalidSecurityTokenFaultCode.Value, securityVersion.HeaderNamespace.Value);
				reason = new FaultReason(SR.GetString("InvalidSecurityTokenFaultReason"), CultureInfo.CurrentCulture);
			}
			else
			{
				if (ex != null)
				{
					return MessageFault.CreateFault(ex.Code, ex.Reason);
				}
				subCode = new FaultCode(securityVersion.InvalidSecurityFaultCode.Value, securityVersion.HeaderNamespace.Value);
				reason = new FaultReason(SR.GetString("InvalidSecurityFaultReason"), CultureInfo.CurrentCulture);
			}
			FaultCode code = FaultCode.CreateSenderFaultCode(subCode);
			return MessageFault.CreateFault(code, reason);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00073243 File Offset: 0x00071443
		internal static bool IsCompositeDuplexBinding(BindingContext context)
		{
			return context.Binding.Elements.Find<CompositeDuplexBindingElement>() != null || context.Binding.Elements.Find<InternalDuplexBindingElement>() != null;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0007326C File Offset: 0x0007146C
		internal static void ErasePasswordInUsernameTokenIfPresent(SecurityMessageProperty messageProperty)
		{
			if (messageProperty == null)
			{
				return;
			}
			if (messageProperty.TransportToken != null)
			{
				UserNameSecurityToken userNameSecurityToken = messageProperty.TransportToken.SecurityToken as UserNameSecurityToken;
				if (userNameSecurityToken != null && !messageProperty.TransportToken.SecurityToken.GetType().IsSubclassOf(typeof(UserNameSecurityToken)))
				{
					messageProperty.TransportToken = new SecurityTokenSpecification(new UserNameSecurityToken(userNameSecurityToken.UserName, null, userNameSecurityToken.Id), messageProperty.TransportToken.SecurityTokenPolicies);
				}
			}
			if (messageProperty.ProtectionToken != null)
			{
				UserNameSecurityToken userNameSecurityToken2 = messageProperty.ProtectionToken.SecurityToken as UserNameSecurityToken;
				if (userNameSecurityToken2 != null && !messageProperty.ProtectionToken.SecurityToken.GetType().IsSubclassOf(typeof(UserNameSecurityToken)))
				{
					messageProperty.ProtectionToken = new SecurityTokenSpecification(new UserNameSecurityToken(userNameSecurityToken2.UserName, null, userNameSecurityToken2.Id), messageProperty.ProtectionToken.SecurityTokenPolicies);
				}
			}
			if (messageProperty.HasIncomingSupportingTokens)
			{
				for (int i = 0; i < messageProperty.IncomingSupportingTokens.Count; i++)
				{
					SupportingTokenSpecification supportingTokenSpecification = messageProperty.IncomingSupportingTokens[i];
					UserNameSecurityToken userNameSecurityToken3 = supportingTokenSpecification.SecurityToken as UserNameSecurityToken;
					if (userNameSecurityToken3 != null && !supportingTokenSpecification.SecurityToken.GetType().IsSubclassOf(typeof(UserNameSecurityToken)))
					{
						messageProperty.IncomingSupportingTokens[i] = new SupportingTokenSpecification(new UserNameSecurityToken(userNameSecurityToken3.UserName, null, userNameSecurityToken3.Id), supportingTokenSpecification.SecurityTokenPolicies, supportingTokenSpecification.SecurityTokenAttachmentMode, supportingTokenSpecification.SecurityTokenParameters);
					}
				}
			}
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000733DC File Offset: 0x000715DC
		[SecuritySafeCritical]
		internal static void FixNetworkCredential(ref NetworkCredential credential)
		{
			SecurityUtils.FixNetworkCredential(ref credential, ServiceModelAppSettings.EnableLegacyUpnUsernameFix);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000733EC File Offset: 0x000715EC
		[SecuritySafeCritical]
		internal static void FixNetworkCredential(ref NetworkCredential credential, bool enableLegacyUpnUsernameFix)
		{
			if (credential == null)
			{
				return;
			}
			string text = SecurityUtils.NetworkCredentialHelper.UnsafeGetUsername(credential);
			string value = SecurityUtils.NetworkCredentialHelper.UnsafeGetDomain(credential);
			if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(value))
			{
				string[] array = text.Split(new char[]
				{
					'\\'
				});
				string[] array2 = text.Split(new char[]
				{
					'@'
				});
				if (array.Length == 2 && array2.Length == 1)
				{
					if (!string.IsNullOrEmpty(array[0]) && !string.IsNullOrEmpty(array[1]))
					{
						credential = new NetworkCredential(array[1], SecurityUtils.NetworkCredentialHelper.UnsafeGetPassword(credential), array[0]);
						return;
					}
				}
				else if (enableLegacyUpnUsernameFix && array.Length == 1 && array2.Length == 2 && !string.IsNullOrEmpty(array2[0]) && !string.IsNullOrEmpty(array2[1]))
				{
					credential = new NetworkCredential(array2[0], SecurityUtils.NetworkCredentialHelper.UnsafeGetPassword(credential), array2[1]);
				}
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000734B4 File Offset: 0x000716B4
		internal static void PrepareNetworkCredential()
		{
			if (SecurityUtils.dummyNetworkCredential == null)
			{
				SecurityUtils.PrepareNetworkCredentialWorker();
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000734C4 File Offset: 0x000716C4
		private static void PrepareNetworkCredentialWorker()
		{
			object obj = SecurityUtils.dummyNetworkCredentialLock;
			lock (obj)
			{
				SecurityUtils.dummyNetworkCredential = new NetworkCredential("dummy", "dummy");
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00073514 File Offset: 0x00071714
		internal static void ResetAllCertificates(X509Certificate2Collection certificates)
		{
			if (certificates != null)
			{
				for (int i = 0; i < certificates.Count; i++)
				{
					SecurityUtils.ResetCertificate(certificates[i]);
				}
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00073541 File Offset: 0x00071741
		[SecuritySafeCritical]
		internal static void ResetCertificate(X509Certificate2 certificate)
		{
			certificate.Reset();
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00073549 File Offset: 0x00071749
		internal static bool IsDefaultNetworkCredential(NetworkCredential credential)
		{
			return SecurityUtils.NetworkCredentialHelper.IsDefault(credential);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00073551 File Offset: 0x00071751
		internal static void OpenTokenProviderIfRequired(SecurityTokenProvider tokenProvider, TimeSpan timeout)
		{
			SecurityUtils.OpenCommunicationObject(tokenProvider as ICommunicationObject, timeout);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0007355F File Offset: 0x0007175F
		internal static IAsyncResult BeginOpenTokenProviderIfRequired(SecurityTokenProvider tokenProvider, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SecurityUtils.OpenCommunicationObjectAsyncResult(tokenProvider, timeout, callback, state);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0007356A File Offset: 0x0007176A
		internal static void EndOpenTokenProviderIfRequired(IAsyncResult result)
		{
			SecurityUtils.OpenCommunicationObjectAsyncResult.End(result);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00073572 File Offset: 0x00071772
		internal static IAsyncResult BeginCloseTokenProviderIfRequired(SecurityTokenProvider tokenProvider, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SecurityUtils.CloseCommunicationObjectAsyncResult(tokenProvider, timeout, callback, state);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0007357D File Offset: 0x0007177D
		internal static void EndCloseTokenProviderIfRequired(IAsyncResult result)
		{
			SecurityUtils.CloseCommunicationObjectAsyncResult.End(result);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x00073585 File Offset: 0x00071785
		internal static void CloseTokenProviderIfRequired(SecurityTokenProvider tokenProvider, TimeSpan timeout)
		{
			SecurityUtils.CloseCommunicationObject(tokenProvider, false, timeout);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0007358F File Offset: 0x0007178F
		internal static void CloseTokenProviderIfRequired(SecurityTokenProvider tokenProvider, bool aborted, TimeSpan timeout)
		{
			SecurityUtils.CloseCommunicationObject(tokenProvider, aborted, timeout);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00073599 File Offset: 0x00071799
		internal static void AbortTokenProviderIfRequired(SecurityTokenProvider tokenProvider)
		{
			SecurityUtils.CloseCommunicationObject(tokenProvider, true, TimeSpan.Zero);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000735A7 File Offset: 0x000717A7
		internal static void OpenTokenAuthenticatorIfRequired(SecurityTokenAuthenticator tokenAuthenticator, TimeSpan timeout)
		{
			SecurityUtils.OpenCommunicationObject(tokenAuthenticator as ICommunicationObject, timeout);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000735B5 File Offset: 0x000717B5
		internal static void CloseTokenAuthenticatorIfRequired(SecurityTokenAuthenticator tokenAuthenticator, TimeSpan timeout)
		{
			SecurityUtils.CloseTokenAuthenticatorIfRequired(tokenAuthenticator, false, timeout);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000735BF File Offset: 0x000717BF
		internal static void CloseTokenAuthenticatorIfRequired(SecurityTokenAuthenticator tokenAuthenticator, bool aborted, TimeSpan timeout)
		{
			SecurityUtils.CloseCommunicationObject(tokenAuthenticator, aborted, timeout);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000735C9 File Offset: 0x000717C9
		internal static IAsyncResult BeginOpenTokenAuthenticatorIfRequired(SecurityTokenAuthenticator tokenAuthenticator, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SecurityUtils.OpenCommunicationObjectAsyncResult(tokenAuthenticator, timeout, callback, state);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000735D4 File Offset: 0x000717D4
		internal static void EndOpenTokenAuthenticatorIfRequired(IAsyncResult result)
		{
			SecurityUtils.OpenCommunicationObjectAsyncResult.End(result);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000735DC File Offset: 0x000717DC
		internal static IAsyncResult BeginCloseTokenAuthenticatorIfRequired(SecurityTokenAuthenticator tokenAuthenticator, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SecurityUtils.CloseCommunicationObjectAsyncResult(tokenAuthenticator, timeout, callback, state);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000735E7 File Offset: 0x000717E7
		internal static void EndCloseTokenAuthenticatorIfRequired(IAsyncResult result)
		{
			SecurityUtils.CloseCommunicationObjectAsyncResult.End(result);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000735EF File Offset: 0x000717EF
		internal static void AbortTokenAuthenticatorIfRequired(SecurityTokenAuthenticator tokenAuthenticator)
		{
			SecurityUtils.CloseCommunicationObject(tokenAuthenticator, true, TimeSpan.Zero);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000735FD File Offset: 0x000717FD
		private static void OpenCommunicationObject(ICommunicationObject obj, TimeSpan timeout)
		{
			if (obj != null)
			{
				obj.Open(timeout);
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0007360C File Offset: 0x0007180C
		private static void CloseCommunicationObject(object obj, bool aborted, TimeSpan timeout)
		{
			if (obj != null)
			{
				ICommunicationObject communicationObject = obj as ICommunicationObject;
				if (communicationObject != null)
				{
					if (aborted)
					{
						try
						{
							communicationObject.Abort();
							return;
						}
						catch (CommunicationException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
							return;
						}
					}
					communicationObject.Close(timeout);
					return;
				}
				if (obj is IDisposable)
				{
					((IDisposable)obj).Dispose();
				}
			}
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00073668 File Offset: 0x00071868
		internal static void MatchRstWithEndpointFilter(Message rst, IMessageFilterTable<EndpointAddress> endpointFilterTable, Uri listenUri)
		{
			if (endpointFilterTable == null)
			{
				return;
			}
			Collection<EndpointAddress> results = new Collection<EndpointAddress>();
			if (!endpointFilterTable.GetMatchingValues(rst, results))
			{
				throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("RequestSecurityTokenDoesNotMatchEndpointFilters", new object[]
				{
					listenUri
				})), rst);
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000736AC File Offset: 0x000718AC
		internal static bool ShouldMatchRstWithEndpointFilter(SecurityBindingElement sbe)
		{
			foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
			{
				if (securityTokenParameters.HasAsymmetricKey)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00073704 File Offset: 0x00071904
		internal static SecurityStandardsManager CreateSecurityStandardsManager(MessageSecurityVersion securityVersion, SecurityTokenManager tokenManager)
		{
			SecurityTokenSerializer tokenSerializer = tokenManager.CreateSecurityTokenSerializer(securityVersion.SecurityTokenVersion);
			return new SecurityStandardsManager(securityVersion, tokenSerializer);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00073728 File Offset: 0x00071928
		internal static SecurityStandardsManager CreateSecurityStandardsManager(SecurityTokenRequirement requirement, SecurityTokenManager tokenManager)
		{
			MessageSecurityTokenVersion property = requirement.GetProperty<MessageSecurityTokenVersion>(ServiceModelSecurityTokenRequirement.MessageSecurityVersionProperty);
			if (property == MessageSecurityTokenVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10)
			{
				return SecurityUtils.CreateSecurityStandardsManager(MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, tokenManager);
			}
			if (property == MessageSecurityTokenVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005)
			{
				return SecurityUtils.CreateSecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11, tokenManager);
			}
			if (property == MessageSecurityTokenVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005BasicSecurityProfile10)
			{
				return SecurityUtils.CreateSecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, tokenManager);
			}
			if (property == MessageSecurityTokenVersion.WSSecurity10WSTrust13WSSecureConversation13BasicSecurityProfile10)
			{
				return SecurityUtils.CreateSecurityStandardsManager(MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10, tokenManager);
			}
			if (property == MessageSecurityTokenVersion.WSSecurity11WSTrust13WSSecureConversation13)
			{
				return SecurityUtils.CreateSecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12, tokenManager);
			}
			if (property == MessageSecurityTokenVersion.WSSecurity11WSTrust13WSSecureConversation13BasicSecurityProfile10)
			{
				return SecurityUtils.CreateSecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10, tokenManager);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000737C8 File Offset: 0x000719C8
		internal static SecurityStandardsManager CreateSecurityStandardsManager(MessageSecurityVersion securityVersion, SecurityTokenSerializer securityTokenSerializer)
		{
			if (securityVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("securityVersion"));
			}
			if (securityTokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenSerializer");
			}
			return new SecurityStandardsManager(securityVersion, securityTokenSerializer);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000737FC File Offset: 0x000719FC
		private static bool TryCreateIdentity(ClaimSet claimSet, string claimType, out EndpointIdentity identity)
		{
			identity = null;
			using (IEnumerator<Claim> enumerator = claimSet.FindClaims(claimType, null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Claim identity2 = enumerator.Current;
					identity = EndpointIdentity.CreateIdentity(identity2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00073858 File Offset: 0x00071A58
		internal static EndpointIdentity GetServiceCertificateIdentity(X509Certificate2 certificate)
		{
			EndpointIdentity result;
			using (X509CertificateClaimSet x509CertificateClaimSet = new X509CertificateClaimSet(certificate))
			{
				EndpointIdentity endpointIdentity;
				if (!SecurityUtils.TryCreateIdentity(x509CertificateClaimSet, ClaimTypes.Dns, out endpointIdentity))
				{
					SecurityUtils.TryCreateIdentity(x509CertificateClaimSet, ClaimTypes.Rsa, out endpointIdentity);
				}
				result = endpointIdentity;
			}
			return result;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000738A8 File Offset: 0x00071AA8
		[SecuritySafeCritical]
		internal static NetworkCredential GetNetworkCredentialsCopy(NetworkCredential networkCredential)
		{
			NetworkCredential result;
			if (networkCredential != null && !SecurityUtils.NetworkCredentialHelper.IsDefault(networkCredential))
			{
				result = new NetworkCredential(SecurityUtils.NetworkCredentialHelper.UnsafeGetUsername(networkCredential), SecurityUtils.NetworkCredentialHelper.UnsafeGetPassword(networkCredential), SecurityUtils.NetworkCredentialHelper.UnsafeGetDomain(networkCredential));
			}
			else
			{
				result = networkCredential;
			}
			return result;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000738DD File Offset: 0x00071ADD
		internal static NetworkCredential GetNetworkCredentialOrDefault(NetworkCredential credential)
		{
			if (SecurityUtils.NetworkCredentialHelper.IsNullOrEmpty(credential))
			{
				return CredentialCache.DefaultNetworkCredentials;
			}
			return credential;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000738F0 File Offset: 0x00071AF0
		public static bool CanReadPrivateKey(X509Certificate2 certificate)
		{
			if (!certificate.HasPrivateKey)
			{
				return false;
			}
			bool result;
			try
			{
				using (RSA rsaprivateKey = CngLightup.GetRSAPrivateKey(certificate))
				{
					if (rsaprivateKey != null)
					{
						return true;
					}
				}
				using (DSA dsaprivateKey = CngLightup.GetDSAPrivateKey(certificate))
				{
					if (dsaprivateKey != null)
					{
						return true;
					}
				}
				using (ECDsa ecdsaPrivateKey = CngLightup.GetECDsaPrivateKey(certificate))
				{
					if (ecdsaPrivateKey != null)
					{
						return true;
					}
				}
				if (certificate.PrivateKey != null)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (CryptographicException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000739A0 File Offset: 0x00071BA0
		internal static SafeFreeCredentials GetCredentialsHandle(string package, NetworkCredential credential, bool isServer, params string[] additionalPackages)
		{
			CredentialUse intent = isServer ? CredentialUse.Inbound : CredentialUse.Outbound;
			SafeFreeCredentials result;
			if (credential == null || SecurityUtils.NetworkCredentialHelper.IsDefault(credential))
			{
				AuthIdentityEx authIdentityEx = new AuthIdentityEx(null, null, null, additionalPackages);
				result = SspiWrapper.AcquireCredentialsHandle(package, intent, ref authIdentityEx);
			}
			else
			{
				SecurityUtils.FixNetworkCredential(ref credential);
				AuthIdentityEx authIdentityEx2 = new AuthIdentityEx(credential.UserName, credential.Password, credential.Domain, new string[0]);
				result = SspiWrapper.AcquireCredentialsHandle(package, intent, ref authIdentityEx2);
			}
			return result;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00073A08 File Offset: 0x00071C08
		internal static SafeFreeCredentials GetCredentialsHandle(Binding binding, KeyedByTypeCollection<IEndpointBehavior> behaviors)
		{
			ClientCredentials clientCredentials = (behaviors == null) ? null : behaviors.Find<ClientCredentials>();
			return SecurityUtils.GetCredentialsHandle(binding, clientCredentials);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00073A2C File Offset: 0x00071C2C
		internal static SafeFreeCredentials GetCredentialsHandle(Binding binding, ClientCredentials clientCredentials)
		{
			SecurityBindingElement sbe = (binding == null) ? null : binding.CreateBindingElements().Find<SecurityBindingElement>();
			return SecurityUtils.GetCredentialsHandle(sbe, clientCredentials);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00073A54 File Offset: 0x00071C54
		internal static SafeFreeCredentials GetCredentialsHandle(SecurityBindingElement sbe, BindingContext context)
		{
			ClientCredentials clientCredentials = (context == null) ? null : context.BindingParameters.Find<ClientCredentials>();
			return SecurityUtils.GetCredentialsHandle(sbe, clientCredentials);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00073A7C File Offset: 0x00071C7C
		internal static SafeFreeCredentials GetCredentialsHandle(SecurityBindingElement sbe, ClientCredentials clientCredentials)
		{
			if (sbe == null)
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe, true))
			{
				if (securityTokenParameters is SecureConversationSecurityTokenParameters)
				{
					SafeFreeCredentials credentialsHandle = SecurityUtils.GetCredentialsHandle(((SecureConversationSecurityTokenParameters)securityTokenParameters).BootstrapSecurityBindingElement, clientCredentials);
					if (credentialsHandle != null)
					{
						return credentialsHandle;
					}
				}
				else if (securityTokenParameters is IssuedSecurityTokenParameters)
				{
					SafeFreeCredentials credentialsHandle2 = SecurityUtils.GetCredentialsHandle(((IssuedSecurityTokenParameters)securityTokenParameters).IssuerBinding, clientCredentials);
					if (credentialsHandle2 != null)
					{
						return credentialsHandle2;
					}
				}
				else
				{
					if (securityTokenParameters is SspiSecurityTokenParameters)
					{
						flag = true;
						break;
					}
					if (securityTokenParameters is KerberosSecurityTokenParameters)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag && !flag2)
			{
				return null;
			}
			NetworkCredential credential = null;
			if (clientCredentials != null)
			{
				credential = SecurityUtils.GetNetworkCredentialOrDefault(clientCredentials.Windows.ClientCredential);
			}
			if (flag2)
			{
				return SecurityUtils.GetCredentialsHandle("Kerberos", credential, false, new string[0]);
			}
			if (clientCredentials == null || clientCredentials.Windows.AllowNtlm)
			{
				return SecurityUtils.GetCredentialsHandle("Negotiate", credential, false, new string[0]);
			}
			if (SecurityUtils.IsOsGreaterThanXP())
			{
				return SecurityUtils.GetCredentialsHandle("Negotiate", credential, false, new string[]
				{
					"!NTLM"
				});
			}
			return SecurityUtils.GetCredentialsHandle("Kerberos", credential, false, new string[0]);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00073BCC File Offset: 0x00071DCC
		internal static byte[] CloneBuffer(byte[] buffer)
		{
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(buffer.Length);
			Buffer.BlockCopy(buffer, 0, array, 0, buffer.Length);
			return array;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00073BF4 File Offset: 0x00071DF4
		internal static X509Certificate2 GetCertificateFromStore(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue, EndpointAddress target)
		{
			X509Certificate2 certificateFromStoreCore = SecurityUtils.GetCertificateFromStoreCore(storeName, storeLocation, findType, findValue, target, true);
			if (certificateFromStoreCore == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotFindCert", new object[]
				{
					storeName,
					storeLocation,
					findType,
					findValue
				})));
			}
			return certificateFromStoreCore;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00073C51 File Offset: 0x00071E51
		internal static bool TryGetCertificateFromStore(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue, EndpointAddress target, out X509Certificate2 certificate)
		{
			certificate = SecurityUtils.GetCertificateFromStoreCore(storeName, storeLocation, findType, findValue, target, false);
			return certificate != null;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00073C68 File Offset: 0x00071E68
		private static X509Certificate2 GetCertificateFromStoreCore(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue, EndpointAddress target, bool throwIfMultipleOrNoMatch)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			X509CertificateStore x509CertificateStore = new X509CertificateStore(storeName, storeLocation);
			X509Certificate2Collection x509Certificate2Collection = null;
			X509Certificate2 result;
			try
			{
				x509CertificateStore.Open(OpenFlags.ReadOnly);
				x509Certificate2Collection = x509CertificateStore.Find(findType, findValue, false);
				if (x509Certificate2Collection.Count == 1)
				{
					result = new X509Certificate2(x509Certificate2Collection[0]);
				}
				else
				{
					if (throwIfMultipleOrNoMatch)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SecurityUtils.CreateCertificateLoadException(storeName, storeLocation, findType, findValue, target, x509Certificate2Collection.Count));
					}
					result = null;
				}
			}
			finally
			{
				SecurityUtils.ResetAllCertificates(x509Certificate2Collection);
				x509CertificateStore.Close();
			}
			return result;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00073D00 File Offset: 0x00071F00
		private static Exception CreateCertificateLoadException(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue, EndpointAddress target, int certCount)
		{
			if (certCount == 0)
			{
				if (target == null)
				{
					return new InvalidOperationException(SR.GetString("CannotFindCert", new object[]
					{
						storeName,
						storeLocation,
						findType,
						findValue
					}));
				}
				return new InvalidOperationException(SR.GetString("CannotFindCertForTarget", new object[]
				{
					storeName,
					storeLocation,
					findType,
					findValue,
					target
				}));
			}
			else
			{
				if (target == null)
				{
					return new InvalidOperationException(SR.GetString("FoundMultipleCerts", new object[]
					{
						storeName,
						storeLocation,
						findType,
						findValue
					}));
				}
				return new InvalidOperationException(SR.GetString("FoundMultipleCertsForTarget", new object[]
				{
					storeName,
					storeLocation,
					findType,
					findValue,
					target
				}));
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00073E04 File Offset: 0x00072004
		public static SecurityBindingElement GetIssuerSecurityBindingElement(ServiceModelSecurityTokenRequirement requirement)
		{
			SecurityBindingElement secureConversationSecurityBindingElement = requirement.SecureConversationSecurityBindingElement;
			if (secureConversationSecurityBindingElement != null)
			{
				return secureConversationSecurityBindingElement;
			}
			Binding issuerBinding = requirement.IssuerBinding;
			if (issuerBinding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("IssuerBindingNotPresentInTokenRequirement", new object[]
				{
					requirement
				}));
			}
			BindingElementCollection bindingElementCollection = issuerBinding.CreateBindingElements();
			return bindingElementCollection.Find<SecurityBindingElement>();
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00073E54 File Offset: 0x00072054
		public static int GetMaxNegotiationBufferSize(BindingContext bindingContext)
		{
			TransportBindingElement transportBindingElement = bindingContext.RemainingBindingElements.Find<TransportBindingElement>();
			int result;
			if (transportBindingElement is ConnectionOrientedTransportBindingElement)
			{
				result = ((ConnectionOrientedTransportBindingElement)transportBindingElement).MaxBufferSize;
			}
			else if (transportBindingElement is HttpTransportBindingElement)
			{
				result = ((HttpTransportBindingElement)transportBindingElement).MaxBufferSize;
			}
			else
			{
				result = 65536;
			}
			return result;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00073EA0 File Offset: 0x000720A0
		public static bool TryCreateKeyFromIntrinsicKeyClause(SecurityKeyIdentifierClause keyIdentifierClause, SecurityTokenResolver resolver, out SecurityKey key)
		{
			key = null;
			if (keyIdentifierClause.CanCreateKey)
			{
				key = keyIdentifierClause.CreateKey();
				return true;
			}
			if (keyIdentifierClause is EncryptedKeyIdentifierClause)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = (EncryptedKeyIdentifierClause)keyIdentifierClause;
				for (int i = 0; i < encryptedKeyIdentifierClause.EncryptingKeyIdentifier.Count; i++)
				{
					SecurityKey securityKey = null;
					if (resolver.TryResolveSecurityKey(encryptedKeyIdentifierClause.EncryptingKeyIdentifier[i], out securityKey))
					{
						byte[] encryptedKey = encryptedKeyIdentifierClause.GetEncryptedKey();
						string encryptionMethod = encryptedKeyIdentifierClause.EncryptionMethod;
						byte[] symmetricKey = securityKey.DecryptKey(encryptionMethod, encryptedKey);
						key = new InMemorySymmetricSecurityKey(symmetricKey, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00073F28 File Offset: 0x00072128
		public static WrappedKeySecurityToken CreateTokenFromEncryptedKeyClause(EncryptedKeyIdentifierClause keyClause, SecurityToken unwrappingToken)
		{
			SecurityKeyIdentifier encryptingKeyIdentifier = keyClause.EncryptingKeyIdentifier;
			byte[] encryptedKey = keyClause.GetEncryptedKey();
			SecurityKey securityKey = unwrappingToken.SecurityKeys[0];
			string encryptionMethod = keyClause.EncryptionMethod;
			byte[] keyToWrap = securityKey.DecryptKey(encryptionMethod, encryptedKey);
			return new WrappedKeySecurityToken(SecurityUtils.GenerateId(), keyToWrap, encryptionMethod, unwrappingToken, encryptingKeyIdentifier, encryptedKey, securityKey);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00073F72 File Offset: 0x00072172
		public static void ValidateAnonymityConstraint(WindowsIdentity identity, bool allowUnauthenticatedCallers)
		{
			if (!allowUnauthenticatedCallers && identity.User.IsWellKnown(WellKnownSidType.AnonymousSid))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityTokenValidationException(SR.GetString("AnonymousLogonsAreNotAllowed")));
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00073FA0 File Offset: 0x000721A0
		private static bool ComputeSslCipherStrengthRequirementFlag()
		{
			if (Environment.OSVersion.Version.Major > 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor > 2))
			{
				return false;
			}
			if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1)
			{
				return Environment.OSVersion.ServicePack == string.Empty || string.Equals(Environment.OSVersion.ServicePack, "Service Pack 1", StringComparison.OrdinalIgnoreCase) || string.Equals(Environment.OSVersion.ServicePack, "Service Pack 2", StringComparison.OrdinalIgnoreCase);
			}
			return Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor != 2 || (Environment.OSVersion.ServicePack == string.Empty || string.Equals(Environment.OSVersion.ServicePack, "Service Pack 1", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000740A7 File Offset: 0x000722A7
		public static bool ShouldValidateSslCipherStrength()
		{
			if (!SecurityUtils.isSslValidationRequirementDetermined)
			{
				SecurityUtils.shouldValidateSslCipherStrength = SecurityUtils.ComputeSslCipherStrengthRequirementFlag();
				Thread.MemoryBarrier();
				SecurityUtils.isSslValidationRequirementDetermined = true;
			}
			return SecurityUtils.shouldValidateSslCipherStrength;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000740D4 File Offset: 0x000722D4
		public static void ValidateSslCipherStrength(int keySizeInBits)
		{
			if (SecurityUtils.ShouldValidateSslCipherStrength() && keySizeInBits < SecurityUtils.MinimumSslCipherStrength)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("SslCipherKeyTooSmall", new object[]
				{
					keySizeInBits,
					SecurityUtils.MinimumSslCipherStrength
				})));
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00074126 File Offset: 0x00072326
		public static bool TryCreateX509CertificateFromRawData(byte[] rawData, out X509Certificate2 certificate)
		{
			if (rawData == null || rawData.Length == 0)
			{
				certificate = null;
				return false;
			}
			X509Helper.VerifyNotPfx(rawData);
			certificate = new X509Certificate2(rawData);
			return certificate.Handle != IntPtr.Zero;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00074154 File Offset: 0x00072354
		internal static string GetKeyDerivationAlgorithm(SecureConversationVersion version)
		{
			string result;
			if (version == SecureConversationVersion.WSSecureConversationFeb2005)
			{
				result = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";
			}
			else
			{
				if (version != SecureConversationVersion.WSSecureConversation13)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				result = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";
			}
			return result;
		}

		// Token: 0x04001EB4 RID: 7860
		public const string Principal = "Principal";

		// Token: 0x04001EB5 RID: 7861
		public const string Identities = "Identities";

		// Token: 0x04001EB6 RID: 7862
		private static bool computedDomain;

		// Token: 0x04001EB7 RID: 7863
		private static string currentDomain;

		// Token: 0x04001EB8 RID: 7864
		private static byte[] combinedHashLabel;

		// Token: 0x04001EB9 RID: 7865
		private static IIdentity anonymousIdentity;

		// Token: 0x04001EBA RID: 7866
		private static NetworkCredential dummyNetworkCredential;

		// Token: 0x04001EBB RID: 7867
		private static object dummyNetworkCredentialLock = new object();

		// Token: 0x04001EBC RID: 7868
		private static X509SecurityTokenAuthenticator nonValidatingX509Authenticator;

		// Token: 0x04001EBD RID: 7869
		private static SecurityIdentifier administratorsSid;

		// Token: 0x04001EBE RID: 7870
		private const int WindowsServerMajorNumber = 5;

		// Token: 0x04001EBF RID: 7871
		private const int WindowsServerMinorNumber = 2;

		// Token: 0x04001EC0 RID: 7872
		private const int XPMajorNumber = 5;

		// Token: 0x04001EC1 RID: 7873
		private const int XPMinorNumber = 1;

		// Token: 0x04001EC2 RID: 7874
		private const string ServicePack1 = "Service Pack 1";

		// Token: 0x04001EC3 RID: 7875
		private const string ServicePack2 = "Service Pack 2";

		// Token: 0x04001EC4 RID: 7876
		private static volatile bool shouldValidateSslCipherStrength;

		// Token: 0x04001EC5 RID: 7877
		private static volatile bool isSslValidationRequirementDetermined = false;

		// Token: 0x04001EC6 RID: 7878
		private static readonly int MinimumSslCipherStrength = 128;

		// Token: 0x04001EC7 RID: 7879
		public const string AuthTypeNTLM = "NTLM";

		// Token: 0x04001EC8 RID: 7880
		public const string AuthTypeNegotiate = "Negotiate";

		// Token: 0x04001EC9 RID: 7881
		public const string AuthTypeKerberos = "Kerberos";

		// Token: 0x04001ECA RID: 7882
		public const string AuthTypeAnonymous = "";

		// Token: 0x04001ECB RID: 7883
		public const string AuthTypeCertMap = "SSL/PCT";

		// Token: 0x04001ECC RID: 7884
		public const string AuthTypeBasic = "Basic";

		// Token: 0x04001ECD RID: 7885
		private const string suppressChannelBindingRegistryKey = "System\\CurrentControlSet\\Control\\Lsa";

		// Token: 0x02000B80 RID: 2944
		private class OpenCommunicationObjectAsyncResult : AsyncResult
		{
			// Token: 0x060072DA RID: 29402 RVA: 0x001ACE9C File Offset: 0x001AB09C
			public OpenCommunicationObjectAsyncResult(object obj, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = (obj as ICommunicationObject);
				bool flag = false;
				if (this.communicationObject == null)
				{
					flag = true;
				}
				else
				{
					if (SecurityUtils.OpenCommunicationObjectAsyncResult.onOpen == null)
					{
						SecurityUtils.OpenCommunicationObjectAsyncResult.onOpen = Fx.ThunkCallback(new AsyncCallback(SecurityUtils.OpenCommunicationObjectAsyncResult.OnOpen));
					}
					IAsyncResult asyncResult = this.communicationObject.BeginOpen(timeout, SecurityUtils.OpenCommunicationObjectAsyncResult.onOpen, this);
					if (asyncResult.CompletedSynchronously)
					{
						this.communicationObject.EndOpen(asyncResult);
						flag = true;
					}
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x060072DB RID: 29403 RVA: 0x001ACF1C File Offset: 0x001AB11C
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SecurityUtils.OpenCommunicationObjectAsyncResult>(result);
			}

			// Token: 0x060072DC RID: 29404 RVA: 0x001ACF28 File Offset: 0x001AB128
			private static void OnOpen(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecurityUtils.OpenCommunicationObjectAsyncResult openCommunicationObjectAsyncResult = (SecurityUtils.OpenCommunicationObjectAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					openCommunicationObjectAsyncResult.communicationObject.EndOpen(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				openCommunicationObjectAsyncResult.Complete(false, exception);
			}

			// Token: 0x04004106 RID: 16646
			private ICommunicationObject communicationObject;

			// Token: 0x04004107 RID: 16647
			private static AsyncCallback onOpen;
		}

		// Token: 0x02000B81 RID: 2945
		private class CloseCommunicationObjectAsyncResult : AsyncResult
		{
			// Token: 0x060072DD RID: 29405 RVA: 0x001ACF84 File Offset: 0x001AB184
			public CloseCommunicationObjectAsyncResult(object obj, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = (obj as ICommunicationObject);
				bool flag = false;
				if (this.communicationObject == null)
				{
					IDisposable disposable = obj as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					flag = true;
				}
				else
				{
					if (SecurityUtils.CloseCommunicationObjectAsyncResult.onClose == null)
					{
						SecurityUtils.CloseCommunicationObjectAsyncResult.onClose = Fx.ThunkCallback(new AsyncCallback(SecurityUtils.CloseCommunicationObjectAsyncResult.OnClose));
					}
					IAsyncResult asyncResult = this.communicationObject.BeginClose(timeout, SecurityUtils.CloseCommunicationObjectAsyncResult.onClose, this);
					if (asyncResult.CompletedSynchronously)
					{
						this.communicationObject.EndClose(asyncResult);
						flag = true;
					}
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x060072DE RID: 29406 RVA: 0x001AD014 File Offset: 0x001AB214
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SecurityUtils.CloseCommunicationObjectAsyncResult>(result);
			}

			// Token: 0x060072DF RID: 29407 RVA: 0x001AD020 File Offset: 0x001AB220
			private static void OnClose(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecurityUtils.CloseCommunicationObjectAsyncResult closeCommunicationObjectAsyncResult = (SecurityUtils.CloseCommunicationObjectAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					closeCommunicationObjectAsyncResult.communicationObject.EndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				closeCommunicationObjectAsyncResult.Complete(false, exception);
			}

			// Token: 0x04004108 RID: 16648
			private ICommunicationObject communicationObject;

			// Token: 0x04004109 RID: 16649
			private static AsyncCallback onClose;
		}

		// Token: 0x02000B82 RID: 2946
		private static class NetworkCredentialHelper
		{
			// Token: 0x060072E0 RID: 29408 RVA: 0x001AD07C File Offset: 0x001AB27C
			[SecuritySafeCritical]
			internal static bool IsNullOrEmpty(NetworkCredential credential)
			{
				return credential == null || (string.IsNullOrEmpty(SecurityUtils.NetworkCredentialHelper.UnsafeGetUsername(credential)) && string.IsNullOrEmpty(SecurityUtils.NetworkCredentialHelper.UnsafeGetDomain(credential)) && string.IsNullOrEmpty(SecurityUtils.NetworkCredentialHelper.UnsafeGetPassword(credential)));
			}

			// Token: 0x060072E1 RID: 29409 RVA: 0x001AD0AA File Offset: 0x001AB2AA
			[SecuritySafeCritical]
			internal static bool IsDefault(NetworkCredential credential)
			{
				return SecurityUtils.NetworkCredentialHelper.UnsafeGetDefaultNetworkCredentials().Equals(credential);
			}

			// Token: 0x060072E2 RID: 29410 RVA: 0x001AD0B7 File Offset: 0x001AB2B7
			[SecurityCritical]
			[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
			internal static string UnsafeGetUsername(NetworkCredential credential)
			{
				return credential.UserName;
			}

			// Token: 0x060072E3 RID: 29411 RVA: 0x001AD0BF File Offset: 0x001AB2BF
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			internal static string UnsafeGetPassword(NetworkCredential credential)
			{
				return credential.Password;
			}

			// Token: 0x060072E4 RID: 29412 RVA: 0x001AD0C7 File Offset: 0x001AB2C7
			[SecurityCritical]
			[EnvironmentPermission(SecurityAction.Assert, Read = "USERDOMAIN")]
			internal static string UnsafeGetDomain(NetworkCredential credential)
			{
				return credential.Domain;
			}

			// Token: 0x060072E5 RID: 29413 RVA: 0x001AD0CF File Offset: 0x001AB2CF
			[SecurityCritical]
			[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
			private static NetworkCredential UnsafeGetDefaultNetworkCredentials()
			{
				return CredentialCache.DefaultNetworkCredentials;
			}
		}
	}
}

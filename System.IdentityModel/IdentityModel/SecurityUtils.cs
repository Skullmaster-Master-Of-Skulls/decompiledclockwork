using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Claims;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Win32;

namespace System.IdentityModel
{
	// Token: 0x02000077 RID: 119
	internal static class SecurityUtils
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000F117 File Offset: 0x0000D317
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000F134 File Offset: 0x0000D334
		public static DateTime MaxUtcDateTime
		{
			get
			{
				return new DateTime(DateTime.MaxValue.Ticks - 864000000000L, DateTimeKind.Utc);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000F160 File Offset: 0x0000D360
		public static DateTime MinUtcDateTime
		{
			get
			{
				return new DateTime(DateTime.MinValue.Ticks + 864000000000L, DateTimeKind.Utc);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000F18A File Offset: 0x0000D38A
		internal static IIdentity CreateIdentity(string name, string authenticationType)
		{
			return new GenericIdentity(name, authenticationType);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000F193 File Offset: 0x0000D393
		internal static IIdentity CreateIdentity(string name)
		{
			return new GenericIdentity(name);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000F19B File Offset: 0x0000D39B
		internal static byte[] CloneBuffer(byte[] buffer)
		{
			return SecurityUtils.CloneBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		internal static byte[] CloneBuffer(byte[] buffer, int offset, int len)
		{
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(len);
			Buffer.BlockCopy(buffer, offset, array, 0, len);
			return array;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F1CC File Offset: 0x0000D3CC
		internal static ReadOnlyCollection<SecurityKey> CreateSymmetricSecurityKeys(byte[] key)
		{
			return new List<SecurityKey>(1)
			{
				new InMemorySymmetricSecurityKey(key)
			}.AsReadOnly();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
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

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F26E File Offset: 0x0000D46E
		internal static bool MatchesBuffer(byte[] src, byte[] dst)
		{
			return SecurityUtils.MatchesBuffer(src, 0, dst, 0);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F27C File Offset: 0x0000D47C
		internal static bool MatchesBuffer(byte[] src, int srcOffset, byte[] dst, int dstOffset)
		{
			if (dstOffset < 0 || srcOffset < 0)
			{
				return false;
			}
			if (src == null || srcOffset >= src.Length)
			{
				return false;
			}
			if (dst == null || dstOffset >= dst.Length)
			{
				return false;
			}
			if (src.Length - srcOffset != dst.Length - dstOffset)
			{
				return false;
			}
			int i = srcOffset;
			int num = dstOffset;
			while (i < src.Length)
			{
				if (src[i] != dst[num])
				{
					return false;
				}
				i++;
				num++;
			}
			return true;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		internal static string GetCertificateId(X509Certificate2 certificate)
		{
			string text = certificate.SubjectName.Name;
			if (string.IsNullOrEmpty(text))
			{
				text = certificate.Thumbprint;
			}
			return text;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000F301 File Offset: 0x0000D501
		[SecuritySafeCritical]
		internal static void ResetCertificate(X509Certificate2 certificate)
		{
			certificate.Reset();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F30C File Offset: 0x0000D50C
		internal static bool IsCurrentlyTimeEffective(DateTime effectiveTime, DateTime expirationTime, TimeSpan maxClockSkew)
		{
			DateTime dateTime = (effectiveTime < DateTime.MinValue.Add(maxClockSkew)) ? effectiveTime : effectiveTime.Subtract(maxClockSkew);
			DateTime dateTime2 = (expirationTime > DateTime.MaxValue.Subtract(maxClockSkew)) ? expirationTime : expirationTime.Add(maxClockSkew);
			DateTime utcNow = DateTime.UtcNow;
			return dateTime.ToUniversalTime() <= utcNow && utcNow < dateTime2.ToUniversalTime();
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000F384 File Offset: 0x0000D584
		internal static bool RequiresFipsCompliance
		{
			[SecuritySafeCritical]
			get
			{
				if (SecurityUtils.fipsAlgorithmPolicy == -1)
				{
					if (Environment.OSVersion.Version.Major >= 6)
					{
						bool flag2;
						bool flag = CAPI.BCryptGetFipsAlgorithmMode(out flag2) == 0;
						if (flag && flag2)
						{
							SecurityUtils.fipsAlgorithmPolicy = 1;
						}
						else
						{
							SecurityUtils.fipsAlgorithmPolicy = 0;
						}
					}
					else
					{
						SecurityUtils.fipsAlgorithmPolicy = SecurityUtils.GetFipsAlgorithmPolicyKeyFromRegistry();
						if (SecurityUtils.fipsAlgorithmPolicy != 1)
						{
							SecurityUtils.fipsAlgorithmPolicy = 0;
						}
					}
				}
				return SecurityUtils.fipsAlgorithmPolicy == 1;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		[SecurityCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Lsa")]
		private static int GetFipsAlgorithmPolicyKeyFromRegistry()
		{
			int result = -1;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa", false))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("FIPSAlgorithmPolicy");
					if (value != null)
					{
						result = (int)value;
					}
				}
			}
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F444 File Offset: 0x0000D644
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static long GetMaxXmlTransformsPerReference()
		{
			if (!SecurityUtils.s_readMaxTransformsPerReference)
			{
				SecurityUtils.s_maxTransformsPerReference = SecurityUtils.GetNetFxSecurityRegistryValue("SignedXmlMaxTransformsPerReference", SecurityUtils.s_maxTransformsPerReference);
				Thread.MemoryBarrier();
				SecurityUtils.s_readMaxTransformsPerReference = true;
			}
			return SecurityUtils.s_maxTransformsPerReference;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F471 File Offset: 0x0000D671
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static long GetMaxXmlReferencesPerSignedInfo()
		{
			if (!SecurityUtils.s_readMaxReferencesPerSignedInfo)
			{
				SecurityUtils.s_maxReferencesPerSignedInfo = SecurityUtils.GetNetFxSecurityRegistryValue("SignedXmlMaxReferencesPerSignedInfo", SecurityUtils.s_maxReferencesPerSignedInfo);
				Thread.MemoryBarrier();
				SecurityUtils.s_readMaxReferencesPerSignedInfo = true;
			}
			return SecurityUtils.s_maxReferencesPerSignedInfo;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		private static long GetNetFxSecurityRegistryValue(string regValueName, long defaultValue)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\Security", false))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue(regValueName);
						if (value != null)
						{
							RegistryValueKind valueKind = registryKey.GetValueKind(regValueName);
							if (valueKind == RegistryValueKind.DWord || valueKind == RegistryValueKind.QWord)
							{
								return Convert.ToInt64(value, CultureInfo.InvariantCulture);
							}
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return defaultValue;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F51C File Offset: 0x0000D71C
		internal static bool CollectionContainsCertificate(X509Certificate2Collection collection, X509Certificate2 certificate)
		{
			if (collection == null || certificate == null || certificate.Handle == IntPtr.Zero)
			{
				return false;
			}
			byte[] rawData = certificate.RawData;
			for (int i = 0; i < collection.Count; i++)
			{
				if (!(collection[i].Handle == IntPtr.Zero))
				{
					byte[] rawData2 = collection[i].RawData;
					if (CryptoHelper.IsEqual(rawData2, rawData))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F58C File Offset: 0x0000D78C
		internal static AuthorizationContext CreateDefaultAuthorizationContext(IList<IAuthorizationPolicy> authorizationPolicies)
		{
			AuthorizationContext authorizationContext;
			if (authorizationPolicies != null && authorizationPolicies.Count == 1 && authorizationPolicies[0] is UnconditionalPolicy)
			{
				authorizationContext = new SecurityUtils.SimpleAuthorizationContext(authorizationPolicies);
			}
			else
			{
				if (authorizationPolicies == null || authorizationPolicies.Count <= 0)
				{
					return DefaultAuthorizationContext.Empty;
				}
				DefaultEvaluationContext defaultEvaluationContext = new DefaultEvaluationContext();
				object[] array = new object[authorizationPolicies.Count];
				object obj = new object();
				int generation;
				do
				{
					generation = defaultEvaluationContext.Generation;
					for (int i = 0; i < authorizationPolicies.Count; i++)
					{
						if (array[i] != obj)
						{
							IAuthorizationPolicy authorizationPolicy = authorizationPolicies[i];
							if (authorizationPolicy == null)
							{
								array[i] = obj;
							}
							else if (authorizationPolicy.Evaluate(defaultEvaluationContext, ref array[i]))
							{
								array[i] = obj;
								if (DiagnosticUtility.ShouldTraceVerbose)
								{
									TraceUtility.TraceEvent(TraceEventType.Verbose, 786435, SR.GetString("AuthorizationPolicyEvaluated", new object[]
									{
										authorizationPolicy.Id
									}));
								}
							}
						}
					}
				}
				while (generation < defaultEvaluationContext.Generation);
				authorizationContext = new DefaultAuthorizationContext(defaultEvaluationContext);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 786434, SR.GetString("AuthorizationContextCreated", new object[]
				{
					authorizationContext.Id
				}));
			}
			return authorizationContext;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		internal static string ClaimSetToString(ClaimSet claimSet)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("ClaimSet [");
			for (int i = 0; i < claimSet.Count; i++)
			{
				Claim claim = claimSet[i];
				if (claim != null)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendLine(claim.ToString());
				}
			}
			string arg = "] by ";
			ClaimSet claimSet2 = claimSet;
			do
			{
				claimSet2 = claimSet2.Issuer;
				stringBuilder.AppendFormat("{0}{1}", arg, (claimSet2 == claimSet) ? "Self" : ((claimSet2.Count <= 0) ? "Unknown" : claimSet2[0].ToString()));
				arg = " -> ";
			}
			while (claimSet2.Issuer != claimSet2);
			return stringBuilder.ToString();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F758 File Offset: 0x0000D958
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

		// Token: 0x06000411 RID: 1041 RVA: 0x0000F788 File Offset: 0x0000D988
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
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new LimitExceededException(SR.GetString("BufferQuotaExceededReadingBase64", new object[]
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

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F888 File Offset: 0x0000DA88
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
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityMessageSerializationException(SR.GetString("CannotFindMatchingCrypto", new object[]
				{
					encryptionMethod
				})));
			}
			return unwrappingSecurityKey.DecryptKey(encryptionMethod, wrappedKey);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F90B File Offset: 0x0000DB0B
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

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F93C File Offset: 0x0000DB3C
		internal static byte[] DecodeHexString(string hexString)
		{
			hexString = hexString.Trim();
			int i = 0;
			int num = hexString.Length;
			if (num >= 2 && hexString[0] == '0' && (hexString[1] == 'x' || hexString[1] == 'X'))
			{
				num = hexString.Length - 2;
				i = 2;
			}
			if (num < 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("InvalidHexString")));
			}
			bool flag;
			byte[] array;
			if (num >= 3 && hexString[i + 2] == ' ')
			{
				if (num % 3 != 2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("InvalidHexString")));
				}
				flag = true;
				array = DiagnosticUtility.Utility.AllocateByteArray(num / 3 + 1);
			}
			else
			{
				if (num % 2 != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("InvalidHexString")));
				}
				flag = false;
				array = DiagnosticUtility.Utility.AllocateByteArray(num / 2);
			}
			int num2 = 0;
			while (i < hexString.Length)
			{
				int num3 = SecurityUtils.ConvertHexDigit(hexString[i]);
				int num4 = SecurityUtils.ConvertHexDigit(hexString[i + 1]);
				array[num2] = (byte)(num4 | num3 << 4);
				if (flag)
				{
					i++;
				}
				i += 2;
				num2++;
			}
			return array;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FA68 File Offset: 0x0000DC68
		private static int ConvertHexDigit(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (int)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (int)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (int)(val - 'A' + '\n');
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("InvalidHexString")));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FAC1 File Offset: 0x0000DCC1
		internal static ReadOnlyCollection<IAuthorizationPolicy> CreateAuthorizationPolicies(ClaimSet claimSet)
		{
			return SecurityUtils.CreateAuthorizationPolicies(claimSet, SecurityUtils.MaxUtcDateTime);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		internal static ReadOnlyCollection<IAuthorizationPolicy> CreateAuthorizationPolicies(ClaimSet claimSet, DateTime expirationTime)
		{
			return new List<IAuthorizationPolicy>(1)
			{
				new UnconditionalPolicy(claimSet, expirationTime)
			}.AsReadOnly();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000FAF7 File Offset: 0x0000DCF7
		internal static string GenerateId()
		{
			return SecurityUniqueId.Create().Value;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000FB04 File Offset: 0x0000DD04
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

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FB48 File Offset: 0x0000DD48
		internal static IIdentity CloneIdentityIfNecessary(IIdentity identity)
		{
			if (identity != null)
			{
				WindowsIdentity windowsIdentity = identity as WindowsIdentity;
				if (windowsIdentity != null)
				{
					return SecurityUtils.CloneWindowsIdentityIfNecessary(windowsIdentity);
				}
			}
			return identity;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000FB6A File Offset: 0x0000DD6A
		[SecuritySafeCritical]
		internal static WindowsIdentity CloneWindowsIdentityIfNecessary(WindowsIdentity wid)
		{
			return SecurityUtils.CloneWindowsIdentityIfNecessary(wid, wid.AuthenticationType);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000FB78 File Offset: 0x0000DD78
		[SecuritySafeCritical]
		internal static WindowsIdentity CloneWindowsIdentityIfNecessary(WindowsIdentity wid, string authenticationType)
		{
			if (wid != null)
			{
				IntPtr intPtr = SecurityUtils.UnsafeGetWindowsIdentityToken(wid);
				if (intPtr != IntPtr.Zero)
				{
					return SecurityUtils.UnsafeCreateWindowsIdentityFromToken(intPtr, authenticationType);
				}
			}
			return wid;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000FBA5 File Offset: 0x0000DDA5
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static IntPtr UnsafeGetWindowsIdentityToken(WindowsIdentity wid)
		{
			return wid.Token;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FBAD File Offset: 0x0000DDAD
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true, UnmanagedCode = true)]
		private static WindowsIdentity UnsafeCreateWindowsIdentityFromToken(IntPtr token, string authenticationType)
		{
			if (authenticationType != null)
			{
				return new WindowsIdentity(token, authenticationType);
			}
			return new WindowsIdentity(token);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		internal static ClaimSet CloneClaimSetIfNecessary(ClaimSet claimSet)
		{
			if (claimSet != null)
			{
				WindowsClaimSet windowsClaimSet = claimSet as WindowsClaimSet;
				if (windowsClaimSet != null)
				{
					return windowsClaimSet.Clone();
				}
			}
			return claimSet;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		internal static ReadOnlyCollection<ClaimSet> CloneClaimSetsIfNecessary(ReadOnlyCollection<ClaimSet> claimSets)
		{
			if (claimSets != null)
			{
				bool flag = false;
				for (int i = 0; i < claimSets.Count; i++)
				{
					if (claimSets[i] is WindowsClaimSet)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					List<ClaimSet> list = new List<ClaimSet>(claimSets.Count);
					for (int j = 0; j < claimSets.Count; j++)
					{
						list.Add(SecurityUtils.CloneClaimSetIfNecessary(claimSets[j]));
					}
					return list.AsReadOnly();
				}
			}
			return claimSets;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000FC53 File Offset: 0x0000DE53
		internal static void DisposeClaimSetIfNecessary(ClaimSet claimSet)
		{
			if (claimSet != null)
			{
				SecurityUtils.DisposeIfNecessary(claimSet as WindowsClaimSet);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FC64 File Offset: 0x0000DE64
		internal static void DisposeClaimSetsIfNecessary(ReadOnlyCollection<ClaimSet> claimSets)
		{
			if (claimSets != null)
			{
				for (int i = 0; i < claimSets.Count; i++)
				{
					SecurityUtils.DisposeIfNecessary(claimSets[i] as WindowsClaimSet);
				}
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000FC98 File Offset: 0x0000DE98
		internal static ReadOnlyCollection<IAuthorizationPolicy> CloneAuthorizationPoliciesIfNecessary(ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (authorizationPolicies != null && authorizationPolicies.Count > 0)
			{
				bool flag = false;
				for (int i = 0; i < authorizationPolicies.Count; i++)
				{
					UnconditionalPolicy unconditionalPolicy = authorizationPolicies[i] as UnconditionalPolicy;
					if (unconditionalPolicy != null && unconditionalPolicy.IsDisposable)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>(authorizationPolicies.Count);
					for (int j = 0; j < authorizationPolicies.Count; j++)
					{
						UnconditionalPolicy unconditionalPolicy2 = authorizationPolicies[j] as UnconditionalPolicy;
						if (unconditionalPolicy2 != null)
						{
							list.Add(unconditionalPolicy2.Clone());
						}
						else
						{
							list.Add(authorizationPolicies[j]);
						}
					}
					return list.AsReadOnly();
				}
			}
			return authorizationPolicies;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FD44 File Offset: 0x0000DF44
		public static void DisposeAuthorizationPoliciesIfNecessary(ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (authorizationPolicies != null && authorizationPolicies.Count > 0)
			{
				for (int i = 0; i < authorizationPolicies.Count; i++)
				{
					SecurityUtils.DisposeIfNecessary(authorizationPolicies[i] as UnconditionalPolicy);
				}
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FD7F File Offset: 0x0000DF7F
		public static void DisposeIfNecessary(IDisposable obj)
		{
			if (obj != null)
			{
				obj.Dispose();
			}
		}

		// Token: 0x04000380 RID: 896
		public const string Identities = "Identities";

		// Token: 0x04000381 RID: 897
		private static int fipsAlgorithmPolicy = -1;

		// Token: 0x04000382 RID: 898
		public const int WindowsVistaMajorNumber = 6;

		// Token: 0x04000383 RID: 899
		private static IIdentity anonymousIdentity;

		// Token: 0x04000384 RID: 900
		public const string AuthTypeNTLM = "NTLM";

		// Token: 0x04000385 RID: 901
		public const string AuthTypeNegotiate = "Negotiate";

		// Token: 0x04000386 RID: 902
		public const string AuthTypeKerberos = "Kerberos";

		// Token: 0x04000387 RID: 903
		public const string AuthTypeAnonymous = "";

		// Token: 0x04000388 RID: 904
		public const string AuthTypeCertMap = "SSL/PCT";

		// Token: 0x04000389 RID: 905
		public const string AuthTypeBasic = "Basic";

		// Token: 0x0400038A RID: 906
		private const string fipsPolicyRegistryKey = "System\\CurrentControlSet\\Control\\Lsa";

		// Token: 0x0400038B RID: 907
		private static bool s_readMaxTransformsPerReference = false;

		// Token: 0x0400038C RID: 908
		private static long s_maxTransformsPerReference = 10L;

		// Token: 0x0400038D RID: 909
		private static bool s_readMaxReferencesPerSignedInfo = false;

		// Token: 0x0400038E RID: 910
		private static long s_maxReferencesPerSignedInfo = 100L;

		// Token: 0x0200023A RID: 570
		private class SimpleAuthorizationContext : AuthorizationContext
		{
			// Token: 0x0600121B RID: 4635 RVA: 0x0004F904 File Offset: 0x0004DB04
			public SimpleAuthorizationContext(IList<IAuthorizationPolicy> authorizationPolicies)
			{
				this.policy = (UnconditionalPolicy)authorizationPolicies[0];
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				if (this.policy.PrimaryIdentity != null && this.policy.PrimaryIdentity != SecurityUtils.AnonymousIdentity)
				{
					dictionary.Add("Identities", new List<IIdentity>
					{
						this.policy.PrimaryIdentity
					});
				}
				this.properties = dictionary;
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004F978 File Offset: 0x0004DB78
			public override string Id
			{
				get
				{
					if (this.id == null)
					{
						this.id = SecurityUniqueId.Create();
					}
					return this.id.Value;
				}
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004F998 File Offset: 0x0004DB98
			public override ReadOnlyCollection<ClaimSet> ClaimSets
			{
				get
				{
					return this.policy.Issuances;
				}
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004F9A5 File Offset: 0x0004DBA5
			public override DateTime ExpirationTime
			{
				get
				{
					return this.policy.ExpirationTime;
				}
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004F9B2 File Offset: 0x0004DBB2
			public override IDictionary<string, object> Properties
			{
				get
				{
					return this.properties;
				}
			}

			// Token: 0x04000F5C RID: 3932
			private SecurityUniqueId id;

			// Token: 0x04000F5D RID: 3933
			private UnconditionalPolicy policy;

			// Token: 0x04000F5E RID: 3934
			private IDictionary<string, object> properties;
		}
	}
}

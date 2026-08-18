using System;
using System.Collections;
using System.Deployment.Internal;
using System.Deployment.Internal.CodeSigning;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace System.Security.Policy
{
	// Token: 0x02000100 RID: 256
	internal class TrustManager : IApplicationTrustManager, ISecurityEncodable
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000CE80 File Offset: 0x0000B080
		[HostProtection(SecurityAction.LinkDemand, UI = true)]
		public ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext trustManagerContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			ApplicationSecurityInfo info = new ApplicationSecurityInfo(activationContext);
			ApplicationTrustExtraInfo applicationTrustExtraInfo = new ApplicationTrustExtraInfo();
			HostContextInternal hostContextInternal = new HostContextInternal(trustManagerContext);
			ICMS cms = (ICMS)InternalActivationContextHelper.GetDeploymentComponentManifest(activationContext);
			ParsedData parsedData = new ParsedData();
			if (TrustManager.ParseManifest(cms, parsedData))
			{
				applicationTrustExtraInfo.RequestsShellIntegration = parsedData.RequestsShellIntegration;
			}
			string deploymentUrl = TrustManager.GetDeploymentUrl(info);
			string zoneNameFromDeploymentUrl = TrustManager.GetZoneNameFromDeploymentUrl(deploymentUrl);
			MemoryStream ms;
			if (!TrustManager.ExtractManifestContent(cms, out ms))
			{
				return TrustManager.BlockingPrompt(activationContext, parsedData, deploymentUrl, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, TrustManager.AppRequestsBeyondDefaultTrust(info));
			}
			bool flag;
			bool flag2;
			bool flag3;
			TrustManager.AnalyzeCertificate(parsedData, ms, out flag, out flag2, out flag3);
			ICMS cms2 = (ICMS)InternalActivationContextHelper.GetApplicationComponentManifest(activationContext);
			ParsedData parsedData2 = new ParsedData();
			MemoryStream ms2;
			if (TrustManager.ParseManifest(cms2, parsedData2) && parsedData2.UseManifestForTrust && TrustManager.ExtractManifestContent(cms2, out ms2))
			{
				bool flag4;
				bool flag5;
				bool flag6;
				TrustManager.AnalyzeCertificate(parsedData, ms2, out flag4, out flag5, out flag6);
				flag = flag4;
				flag2 = flag5;
				flag3 = flag6;
				parsedData.AppName = parsedData2.AppName;
				parsedData.AppPublisher = parsedData2.AppPublisher;
				parsedData.SupportUrl = parsedData2.SupportUrl;
			}
			if (flag)
			{
				TrustManager.PromptsAllowed promptsAllowed = TrustManager.GetPromptsAllowed(hostContextInternal, zoneNameFromDeploymentUrl, parsedData);
				if (promptsAllowed == TrustManager.PromptsAllowed.None)
				{
					return TrustManager.CreateApplicationTrust(activationContext, info, applicationTrustExtraInfo, false, false);
				}
				return TrustManager.BlockingPrompt(activationContext, parsedData, deploymentUrl, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, TrustManager.AppRequestsBeyondDefaultTrust(info));
			}
			else
			{
				if (flag3)
				{
					parsedData.AuthenticodedPublisher = null;
					parsedData.Certificate = null;
				}
				ArrayList matchingTrusts;
				if (!hostContextInternal.IgnorePersistedDecision && TrustManager.SearchPreviousTrustedVersion(activationContext, out matchingTrusts) && TrustManager.ExistingTrustApplicable(info, matchingTrusts))
				{
					if (applicationTrustExtraInfo.RequestsShellIntegration && !TrustManager.SomePreviousTrustedVersionRequiresShellIntegration(matchingTrusts) && !flag2)
					{
						switch (TrustManager.GetPromptsAllowed(hostContextInternal, zoneNameFromDeploymentUrl, parsedData))
						{
						case TrustManager.PromptsAllowed.All:
							return TrustManager.BasicInstallPrompt(activationContext, parsedData, deploymentUrl, hostContextInternal, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, TrustManager.AppRequestsBeyondDefaultTrust(info));
						case TrustManager.PromptsAllowed.BlockingOnly:
							return TrustManager.BlockingPrompt(activationContext, parsedData, deploymentUrl, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, TrustManager.AppRequestsBeyondDefaultTrust(info));
						case TrustManager.PromptsAllowed.None:
							return TrustManager.CreateApplicationTrust(activationContext, info, applicationTrustExtraInfo, false, false);
						}
					}
					return TrustManager.CreateApplicationTrust(activationContext, info, applicationTrustExtraInfo, true, hostContextInternal.Persist);
				}
				bool flag7 = TrustManager.AppRequestsBeyondDefaultTrust(info);
				if (!flag7 || flag2)
				{
					if (flag2)
					{
						return TrustManager.CreateApplicationTrust(activationContext, info, applicationTrustExtraInfo, true, hostContextInternal.Persist);
					}
					switch (TrustManager.GetPromptsAllowed(hostContextInternal, zoneNameFromDeploymentUrl, parsedData))
					{
					case TrustManager.PromptsAllowed.All:
					case TrustManager.PromptsAllowed.None:
						return TrustManager.BasicInstallPrompt(activationContext, parsedData, deploymentUrl, hostContextInternal, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, false);
					case TrustManager.PromptsAllowed.BlockingOnly:
						return TrustManager.BlockingPrompt(activationContext, parsedData, deploymentUrl, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, flag7);
					}
				}
				TrustManager.PromptsAllowed promptsAllowed = TrustManager.GetPromptsAllowed(hostContextInternal, zoneNameFromDeploymentUrl, parsedData);
				if (promptsAllowed == TrustManager.PromptsAllowed.BlockingOnly)
				{
					return TrustManager.BlockingPrompt(activationContext, parsedData, deploymentUrl, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl, true);
				}
				if (promptsAllowed == TrustManager.PromptsAllowed.None)
				{
					return TrustManager.CreateApplicationTrust(activationContext, info, applicationTrustExtraInfo, false, false);
				}
				return TrustManager.HighRiskPrompt(activationContext, parsedData, deploymentUrl, hostContextInternal, info, applicationTrustExtraInfo, zoneNameFromDeploymentUrl);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000D124 File Offset: 0x0000B324
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
			securityElement.AddAttribute("class", SecurityElement.Escape(base.GetType().AssemblyQualifiedName));
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000D168 File Offset: 0x0000B368
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (!string.Equals(element.Tag, "IApplicationTrustManager", StringComparison.Ordinal))
			{
				throw new ArgumentException(SR.GetString("TrustManagerBadXml", new object[]
				{
					"IApplicationTrustManager"
				}));
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		private static string DefaultBrowserExePath
		{
			get
			{
				string result;
				try
				{
					string text = null;
					new RegistryPermission(PermissionState.Unrestricted).Assert();
					try
					{
						RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command");
						if (registryKey != null)
						{
							string text2 = (string)registryKey.GetValue(string.Empty);
							registryKey.Close();
							if (text2 != null)
							{
								text2 = text2.Trim();
								if (text2.Length != 0)
								{
									if (text2[0] == '"')
									{
										int num = text2.IndexOf('"', 1);
										if (num != -1)
										{
											text = text2.Substring(1, num - 1);
										}
									}
									else
									{
										int num2 = text2.IndexOf(' ');
										if (num2 != -1)
										{
											text = text2.Substring(0, num2);
										}
										else
										{
											text = text2;
										}
									}
								}
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					result = text;
				}
				catch (Exception ex)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D280 File Offset: 0x0000B480
		private static void ProcessSignerInfo(SignedCmiManifest2 signedManifest, out bool distrustedPublisher, out bool noCertificate)
		{
			distrustedPublisher = false;
			noCertificate = false;
			int errorCode = signedManifest.AuthenticodeSignerInfo.ErrorCode;
			if (errorCode == -2146762479 || errorCode == -2146885616)
			{
				distrustedPublisher = true;
				return;
			}
			if (errorCode == -2146762748)
			{
				return;
			}
			noCertificate = true;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		private static bool AnalyzeCertificate(ParsedData parsedData, MemoryStream ms, out bool distrustedPublisher, out bool trustedPublisher, out bool noCertificate)
		{
			distrustedPublisher = false;
			trustedPublisher = false;
			noCertificate = false;
			SignedCmiManifest2 signedCmiManifest = null;
			XmlDocument xmlDocument = null;
			try
			{
				xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.Load(ms);
				signedCmiManifest = new SignedCmiManifest2(xmlDocument, true);
				signedCmiManifest.Verify(CmiManifestVerifyFlags.None);
			}
			catch (Exception ex)
			{
				if (!(ex is CryptographicException))
				{
					return false;
				}
				if (signedCmiManifest.StrongNameSignerInfo != null && signedCmiManifest.StrongNameSignerInfo.ErrorCode != -2146869232)
				{
					if (signedCmiManifest.AuthenticodeSignerInfo == null)
					{
						return false;
					}
					TrustManager.ProcessSignerInfo(signedCmiManifest, out distrustedPublisher, out noCertificate);
					return true;
				}
				else
				{
					try
					{
						signedCmiManifest = new SignedCmiManifest2(xmlDocument, false);
						signedCmiManifest.Verify(CmiManifestVerifyFlags.None);
					}
					catch (Exception ex2)
					{
						if (!(ex2 is CryptographicException))
						{
							return false;
						}
						if (signedCmiManifest.AuthenticodeSignerInfo != null)
						{
							TrustManager.ProcessSignerInfo(signedCmiManifest, out distrustedPublisher, out noCertificate);
							return true;
						}
						return false;
					}
				}
			}
			finally
			{
				if (signedCmiManifest != null && signedCmiManifest.AuthenticodeSignerInfo != null && signedCmiManifest.AuthenticodeSignerInfo.SignerChain != null)
				{
					parsedData.Certificate = signedCmiManifest.AuthenticodeSignerInfo.SignerChain.ChainElements[0].Certificate;
					parsedData.AuthenticodedPublisher = parsedData.Certificate.GetNameInfo(X509NameType.SimpleName, false);
				}
			}
			if (signedCmiManifest == null || signedCmiManifest.AuthenticodeSignerInfo == null)
			{
				noCertificate = true;
			}
			else
			{
				trustedPublisher = true;
			}
			return true;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000D414 File Offset: 0x0000B614
		private static bool AppRequestsBeyondDefaultTrust(ApplicationSecurityInfo info)
		{
			bool result;
			try
			{
				PermissionSet standardSandbox = SecurityManager.GetStandardSandbox(info.ApplicationEvidence);
				PermissionSet requestedPermissionSet = TrustManager.GetRequestedPermissionSet(info);
				if (standardSandbox == null && requestedPermissionSet != null)
				{
					result = true;
				}
				else if (standardSandbox != null && requestedPermissionSet == null)
				{
					result = false;
				}
				else
				{
					result = !requestedPermissionSet.IsSubsetOf(standardSandbox);
				}
			}
			catch (Exception)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D46C File Offset: 0x0000B66C
		private static ApplicationTrust BasicInstallPrompt(ActivationContext activationContext, ParsedData parsedData, string deploymentUrl, HostContextInternal hostContextInternal, ApplicationSecurityInfo info, ApplicationTrustExtraInfo appTrustExtraInfo, string zoneName, bool permissionElevationRequired)
		{
			TrustManagerPromptOptions options = TrustManager.CompletePromptOptions(permissionElevationRequired ? TrustManagerPromptOptions.RequiresPermissions : TrustManagerPromptOptions.None, appTrustExtraInfo, zoneName, info);
			DialogResult dialogResult;
			try
			{
				TrustManagerPromptUIThread trustManagerPromptUIThread = new TrustManagerPromptUIThread(string.IsNullOrEmpty(parsedData.AppName) ? info.ApplicationId.Name : parsedData.AppName, TrustManager.DefaultBrowserExePath, parsedData.SupportUrl, TrustManager.GetHostFromDeploymentUrl(deploymentUrl), parsedData.AuthenticodedPublisher, parsedData.Certificate, options);
				dialogResult = trustManagerPromptUIThread.ShowDialog();
			}
			catch (Exception ex)
			{
				dialogResult = DialogResult.No;
			}
			return TrustManager.CreateApplicationTrust(activationContext, info, appTrustExtraInfo, dialogResult == DialogResult.OK, hostContextInternal.Persist && dialogResult == DialogResult.OK);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D510 File Offset: 0x0000B710
		private static TrustManagerPromptOptions CompletePromptOptions(TrustManagerPromptOptions options, ApplicationTrustExtraInfo appTrustExtraInfo, string zoneName, ApplicationSecurityInfo info)
		{
			if (appTrustExtraInfo.RequestsShellIntegration)
			{
				options |= TrustManagerPromptOptions.AddsShortcut;
			}
			if (zoneName != null)
			{
				if (string.Compare(zoneName, "Internet", true, CultureInfo.InvariantCulture) == 0)
				{
					options |= TrustManagerPromptOptions.InternetSource;
				}
				else if (string.Compare(zoneName, "TrustedSites", true, CultureInfo.InvariantCulture) == 0)
				{
					options |= TrustManagerPromptOptions.TrustedSitesSource;
				}
				else if (string.Compare(zoneName, "UntrustedSites", true, CultureInfo.InvariantCulture) == 0)
				{
					options |= TrustManagerPromptOptions.UntrustedSitesSource;
				}
				else if (string.Compare(zoneName, "LocalIntranet", true, CultureInfo.InvariantCulture) == 0)
				{
					options |= TrustManagerPromptOptions.LocalNetworkSource;
				}
				else if (string.Compare(zoneName, "MyComputer", true, CultureInfo.InvariantCulture) == 0)
				{
					options |= TrustManagerPromptOptions.LocalComputerSource;
				}
			}
			if (info != null)
			{
				PermissionSet defaultRequestSet = info.DefaultRequestSet;
				if (defaultRequestSet != null && defaultRequestSet.IsUnrestricted())
				{
					options |= TrustManagerPromptOptions.WillHaveFullTrust;
				}
			}
			return options;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		private static ApplicationTrust CreateApplicationTrust(ActivationContext activationContext, ApplicationSecurityInfo info, ApplicationTrustExtraInfo appTrustExtraInfo, bool trust, bool persist)
		{
			return new ApplicationTrust(activationContext.Identity)
			{
				ExtraInfo = appTrustExtraInfo,
				IsApplicationTrustedToRun = trust,
				DefaultGrantSet = new PolicyStatement(info.DefaultRequestSet, PolicyStatementAttribute.Nothing),
				Persist = persist
			};
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D61C File Offset: 0x0000B81C
		private static bool ExistingTrustApplicable(ApplicationSecurityInfo info, ArrayList matchingTrusts)
		{
			int i = 0;
			while (i < matchingTrusts.Count)
			{
				ApplicationTrust applicationTrust = (ApplicationTrust)matchingTrusts[i];
				if (!applicationTrust.IsApplicationTrustedToRun)
				{
					matchingTrusts.RemoveAt(i);
				}
				PermissionSet requestedPermissionSet = TrustManager.GetRequestedPermissionSet(info);
				PermissionSet permissionSet = applicationTrust.DefaultGrantSet.PermissionSet;
				if (permissionSet == null && requestedPermissionSet != null)
				{
					matchingTrusts.RemoveAt(i);
				}
				else if (permissionSet != null && requestedPermissionSet == null)
				{
					i++;
					continue;
				}
				if (requestedPermissionSet.IsSubsetOf(permissionSet))
				{
					i++;
				}
				else
				{
					matchingTrusts.RemoveAt(i);
				}
			}
			return matchingTrusts.Count > 0;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		private unsafe static bool ExtractManifestContent(ICMS cms, out MemoryStream ms)
		{
			ms = new MemoryStream();
			bool result;
			try
			{
				IStream stream = cms as IStream;
				if (stream == null)
				{
					result = false;
				}
				else
				{
					byte[] array = new byte[4096];
					int num = 4096;
					do
					{
						stream.Read(array, num, new IntPtr((void*)(&num)));
						ms.Write(array, 0, num);
					}
					while (num == 4096);
					ms.Position = 0L;
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000D718 File Offset: 0x0000B918
		private static bool IsInternetZone(string zoneName)
		{
			return string.Compare(zoneName, "Internet", true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000D730 File Offset: 0x0000B930
		private static TrustManager.PromptingLevel GetDefaultPromptingLevel(string zoneName)
		{
			TrustManager.PromptingLevel result;
			if (!(zoneName == "Internet") && !(zoneName == "LocalIntranet") && !(zoneName == "MyComputer") && !(zoneName == "TrustedSites"))
			{
				if (!(zoneName == "UntrustedSites"))
				{
					result = TrustManager.PromptingLevel.Disabled;
				}
				else
				{
					result = TrustManager.PromptingLevel.Disabled;
				}
			}
			else
			{
				result = TrustManager.PromptingLevel.Prompt;
			}
			return result;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D78C File Offset: 0x0000B98C
		private static string GetDeploymentUrl(ApplicationSecurityInfo info)
		{
			Evidence applicationEvidence = info.ApplicationEvidence;
			Url hostEvidence = applicationEvidence.GetHostEvidence<Url>();
			if (hostEvidence != null)
			{
				return hostEvidence.Value;
			}
			return null;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		private static PermissionSet GetRequestedPermissionSet(ApplicationSecurityInfo info)
		{
			PermissionSet defaultRequestSet = info.DefaultRequestSet;
			PermissionSet result = null;
			if (defaultRequestSet != null)
			{
				result = defaultRequestSet.Copy();
			}
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		private static string GetHostFromDeploymentUrl(string deploymentUrl)
		{
			if (deploymentUrl == null)
			{
				return string.Empty;
			}
			string text = null;
			try
			{
				Uri uri = new Uri(deploymentUrl);
				if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
				{
					text = uri.Host;
				}
				if (string.IsNullOrEmpty(text))
				{
					text = uri.AbsolutePath;
					int num = -1;
					if (string.IsNullOrEmpty(uri.Host) && text.StartsWith("/"))
					{
						text = text.TrimStart(new char[]
						{
							'/'
						});
						num = text.IndexOf('/');
					}
					else if (uri.LocalPath.Length > 2 && (uri.LocalPath[1] == ':' || uri.LocalPath.StartsWith("\\\\")))
					{
						text = uri.LocalPath;
						num = text.LastIndexOf('\\');
					}
					if (num != -1)
					{
						text = text.Remove(num);
					}
				}
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000D8DC File Offset: 0x0000BADC
		private static TrustManager.PromptsAllowed GetPromptsAllowed(HostContextInternal hostContextInternal, string zoneName, ParsedData parsedData)
		{
			if (hostContextInternal.NoPrompt)
			{
				return TrustManager.PromptsAllowed.None;
			}
			TrustManager.PromptingLevel zonePromptingLevel = TrustManager.GetZonePromptingLevel(zoneName);
			if (zonePromptingLevel == TrustManager.PromptingLevel.Disabled || (zonePromptingLevel == TrustManager.PromptingLevel.PromptOnlyForAuthenticode && parsedData.AuthenticodedPublisher == null))
			{
				return TrustManager.PromptsAllowed.BlockingOnly;
			}
			return TrustManager.PromptsAllowed.All;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000D90C File Offset: 0x0000BB0C
		private static string GetZoneNameFromDeploymentUrl(string deploymentUrl)
		{
			Zone zone = Zone.CreateFromUrl(deploymentUrl);
			if (zone == null || zone.SecurityZone == SecurityZone.NoZone)
			{
				return "UntrustedSites";
			}
			switch (zone.SecurityZone)
			{
			case SecurityZone.MyComputer:
				return "MyComputer";
			case SecurityZone.Intranet:
				return "LocalIntranet";
			case SecurityZone.Trusted:
				return "TrustedSites";
			case SecurityZone.Internet:
				return "Internet";
			case SecurityZone.Untrusted:
				return "UntrustedSites";
			default:
				return "UntrustedSites";
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D978 File Offset: 0x0000BB78
		private static TrustManager.PromptingLevel GetZonePromptingLevel(string zoneName)
		{
			TrustManager.PromptingLevel result;
			try
			{
				string text = null;
				new RegistryPermission(PermissionState.Unrestricted).Assert();
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\.NETFramework\\Security\\TrustManager\\PromptingLevel"))
					{
						if (registryKey != null)
						{
							text = (string)registryKey.GetValue(zoneName);
						}
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (string.IsNullOrEmpty(text))
				{
					result = TrustManager.GetDefaultPromptingLevel(zoneName);
				}
				else if (string.Compare(text, "Enabled", true, CultureInfo.InvariantCulture) == 0)
				{
					result = TrustManager.PromptingLevel.Prompt;
				}
				else if (string.Compare(text, "Disabled", true, CultureInfo.InvariantCulture) == 0)
				{
					result = TrustManager.PromptingLevel.Disabled;
				}
				else if (string.Compare(text, "AuthenticodeRequired", true, CultureInfo.InvariantCulture) == 0)
				{
					result = TrustManager.PromptingLevel.PromptOnlyForAuthenticode;
				}
				else
				{
					result = TrustManager.GetDefaultPromptingLevel(zoneName);
				}
			}
			catch (Exception ex)
			{
				result = TrustManager.GetDefaultPromptingLevel(zoneName);
			}
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000DA58 File Offset: 0x0000BC58
		private static ApplicationTrust HighRiskPrompt(ActivationContext activationContext, ParsedData parsedData, string deploymentUrl, HostContextInternal hostContextInternal, ApplicationSecurityInfo info, ApplicationTrustExtraInfo appTrustExtraInfo, string zoneName)
		{
			TrustManagerPromptOptions options = TrustManager.CompletePromptOptions(TrustManagerPromptOptions.RequiresPermissions, appTrustExtraInfo, zoneName, info);
			DialogResult dialogResult;
			try
			{
				TrustManagerPromptUIThread trustManagerPromptUIThread = new TrustManagerPromptUIThread(string.IsNullOrEmpty(parsedData.AppName) ? info.ApplicationId.Name : parsedData.AppName, TrustManager.DefaultBrowserExePath, parsedData.SupportUrl, TrustManager.GetHostFromDeploymentUrl(deploymentUrl), parsedData.AuthenticodedPublisher, parsedData.Certificate, options);
				dialogResult = trustManagerPromptUIThread.ShowDialog();
			}
			catch (Exception ex)
			{
				dialogResult = DialogResult.No;
			}
			return TrustManager.CreateApplicationTrust(activationContext, info, appTrustExtraInfo, dialogResult == DialogResult.OK, hostContextInternal.Persist && dialogResult == DialogResult.OK);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		private static ApplicationTrust BlockingPrompt(ActivationContext activationContext, ParsedData parsedData, string deploymentUrl, ApplicationSecurityInfo info, ApplicationTrustExtraInfo appTrustExtraInfo, string zoneName, bool permissionElevationRequired)
		{
			TrustManagerPromptOptions options = TrustManager.CompletePromptOptions(permissionElevationRequired ? (TrustManagerPromptOptions.StopApp | TrustManagerPromptOptions.RequiresPermissions) : TrustManagerPromptOptions.StopApp, appTrustExtraInfo, zoneName, info);
			try
			{
				TrustManagerPromptUIThread trustManagerPromptUIThread = new TrustManagerPromptUIThread(string.IsNullOrEmpty(parsedData.AppName) ? info.ApplicationId.Name : parsedData.AppName, TrustManager.DefaultBrowserExePath, parsedData.SupportUrl, TrustManager.GetHostFromDeploymentUrl(deploymentUrl), parsedData.AuthenticodedPublisher, parsedData.Certificate, options);
				trustManagerPromptUIThread.ShowDialog();
			}
			catch (Exception ex)
			{
			}
			return TrustManager.CreateApplicationTrust(activationContext, info, appTrustExtraInfo, false, false);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000DB80 File Offset: 0x0000BD80
		private static bool ParseManifest(ICMS cms, ParsedData parsedData)
		{
			try
			{
				if (cms != null && cms.MetadataSectionEntry != null)
				{
					IMetadataSectionEntry metadataSectionEntry = cms.MetadataSectionEntry as IMetadataSectionEntry;
					if (metadataSectionEntry != null)
					{
						IDescriptionMetadataEntry descriptionData = metadataSectionEntry.DescriptionData;
						if (descriptionData != null)
						{
							parsedData.SupportUrl = descriptionData.SupportUrl;
							parsedData.AppName = descriptionData.Product;
							parsedData.AppPublisher = descriptionData.Publisher;
						}
						IDeploymentMetadataEntry deploymentData = metadataSectionEntry.DeploymentData;
						if (deploymentData != null)
						{
							parsedData.RequestsShellIntegration = ((deploymentData.DeploymentFlags & 32U) > 0U);
						}
						if ((metadataSectionEntry.ManifestFlags & 8U) != 0U)
						{
							parsedData.UseManifestForTrust = true;
						}
						else
						{
							parsedData.UseManifestForTrust = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000DC28 File Offset: 0x0000BE28
		private static bool SomePreviousTrustedVersionRequiresShellIntegration(ArrayList matchingTrusts)
		{
			foreach (object obj in matchingTrusts)
			{
				ApplicationTrust applicationTrust = (ApplicationTrust)obj;
				ApplicationTrustExtraInfo applicationTrustExtraInfo = applicationTrust.ExtraInfo as ApplicationTrustExtraInfo;
				if (applicationTrustExtraInfo != null && applicationTrustExtraInfo.RequestsShellIntegration)
				{
					return true;
				}
				if (applicationTrustExtraInfo == null && applicationTrust.DefaultGrantSet.PermissionSet.IsUnrestricted())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		private static bool SearchPreviousTrustedVersion(ActivationContext activationContext, out ArrayList matchingTrusts)
		{
			matchingTrusts = null;
			ApplicationTrustCollection userApplicationTrusts = ApplicationSecurityManager.UserApplicationTrusts;
			foreach (ApplicationTrust applicationTrust in userApplicationTrusts)
			{
				IDefinitionAppId definition = IsolationInterop.AppIdAuthority.TextToDefinition(0U, applicationTrust.ApplicationIdentity.FullName);
				IDefinitionAppId definition2 = IsolationInterop.AppIdAuthority.TextToDefinition(0U, activationContext.Identity.FullName);
				if (IsolationInterop.AppIdAuthority.AreDefinitionsEqual(1U, definition, definition2))
				{
					if (matchingTrusts == null)
					{
						matchingTrusts = new ArrayList();
					}
					matchingTrusts.Add(applicationTrust);
				}
			}
			return matchingTrusts != null;
		}

		// Token: 0x04000442 RID: 1090
		public const string PromptingLevelKeyName = "Software\\Microsoft\\.NETFramework\\Security\\TrustManager\\PromptingLevel";

		// Token: 0x02000549 RID: 1353
		private enum PromptingLevel
		{
			// Token: 0x04003818 RID: 14360
			Disabled,
			// Token: 0x04003819 RID: 14361
			Prompt,
			// Token: 0x0400381A RID: 14362
			PromptOnlyForAuthenticode
		}

		// Token: 0x0200054A RID: 1354
		private enum PromptsAllowed
		{
			// Token: 0x0400381C RID: 14364
			All,
			// Token: 0x0400381D RID: 14365
			BlockingOnly,
			// Token: 0x0400381E RID: 14366
			None
		}
	}
}

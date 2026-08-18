using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ClockWorkLogger;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Text;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x02000005 RID: 5
	[ClockWorkServerJobExecuting("Check server certificate")]
	public class ClockWorkServerCertificateJob : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002D33 File Offset: 0x00000F33
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002D3B File Offset: 0x00000F3B
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string JobName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D44 File Offset: 0x00000F44
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002D50 File Offset: 0x00000F50
		public ClockWorkServerJobRunningResult Run()
		{
			CWLogger.Logger.Info("{0}:: ********* Job Run start *********", this.JobName);
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.OpenExistingOnly);
			X509Certificate2 certificate = x509Store.GetCertificate(this.ServerInstance.X509FindType, this.ServerInstance.X509FindValue, false);
			x509Store.Close();
			X509Certificate2 x509Certificate = null;
			if (certificate != null && this.CheckForNewRealCertificate(certificate, out x509Certificate) && x509Certificate != null)
			{
				CWLogger.Logger.Info("{0}:: A new real certificate '{1}' was found and will replace the current one '{2}'", this.JobName, x509Certificate.Thumbprint, certificate.Thumbprint);
				this.SwitchCertificates(certificate, x509Certificate);
			}
			else if (!this.ValidCertificate(certificate))
			{
				CWLogger.Logger.Info("{0}:: Current certificate '{1}' is about to expired or is already expired and need to be replaced", this.JobName, ((certificate != null) ? certificate.Thumbprint : null) ?? "NULL");
				this.SwitchCertificates(certificate);
			}
			else
			{
				CWLogger.Logger.Info("{0}:: Current certificate '{1}' is valid", this.JobName, ((certificate != null) ? certificate.Thumbprint : null) ?? "NULL");
			}
			CWLogger.Logger.Info("{0}:: *********** Job Run end **********", this.JobName);
			return new ClockWorkServerJobRunningResult
			{
				JobName = this.JobName,
				Status = eClockWorkServerJobResult.Success,
				Message = string.Empty
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E80 File Offset: 0x00001080
		private bool CheckForNewRealCertificate(X509Certificate2 cert, out X509Certificate2 foundCert)
		{
			foundCert = null;
			if (!cert.IsSelfSigned())
			{
				return false;
			}
			string text = new RegistryHelper().ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application",
				this.ServerInstance.InstanceName,
				"ClockWorkServerJobs",
				this.JobName,
				"OldCertificateSubjectDistinguishedName"
			});
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.OpenExistingOnly);
			X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, text, true);
			x509Store.Close();
			if (x509Certificate2Collection != null && x509Certificate2Collection.Count > 0)
			{
				foundCert = x509Certificate2Collection.Cast<X509Certificate2>().FirstOrDefault((X509Certificate2 cer) => !cer.Subject.Equals(cer.Issuer, StringComparison.OrdinalIgnoreCase) && !cer.Thumbprint.Trim().Equals(cert.Thumbprint.Trim(), StringComparison.OrdinalIgnoreCase) && cer.NotAfter > DateTime.Today.AddDays(7.0) && cer.NotBefore < DateTime.Now && cer.Verify());
			}
			return foundCert != null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F48 File Offset: 0x00001148
		private void SwitchCertificates(X509Certificate2 oldCert, X509Certificate2 newCert)
		{
			CWLogger.Logger.Info("{0}:: Switching ClockWorkServer current certificate '{1}' to use new certificate '{2}'", this.JobName, (oldCert != null) ? oldCert.Thumbprint : null, (newCert != null) ? newCert.Thumbprint : null);
			CWLogger.Logger.Info("{0}:: Assigning permissions ...", this.JobName);
			newCert.AddAccessToCertificate("NETWORK SERVICE");
			CWLogger.Logger.Info("{0}:: User NETWORK SERVICE was added", this.JobName);
			newCert.AddAccessToCertificate("IIS_IUSRS");
			CWLogger.Logger.Info("{0}:: User IIS_IUSRS was added", this.JobName);
			this.ServerInstance.X509FindType = "FindByThumbprint";
			this.ServerInstance.X509FindValue = ((newCert != null) ? newCert.Thumbprint : null);
			Dictionary<string, string> codes = new Dictionary<string, string>
			{
				{
					"certificatefindtype",
					this.ServerInstance.X509FindType
				},
				{
					"certificatefindvalue",
					this.ServerInstance.X509FindValue
				}
			};
			string envConfigPath = Path.Combine(this.ServerInstance.InstallationPath, "EnvironmentConfigs");
			this.SaveBehaviorsConfig(envConfigPath, codes);
			this.ClockWorkServerUseNewCertificate(newCert);
			CWLogger.Logger.Info("{0}:: ClockWorkServer is using new certificate thumbprint='{1}'", this.JobName, newCert.Thumbprint);
			if (newCert.IsSelfSigned())
			{
				if (!oldCert.IsSelfSigned())
				{
					this.RememberOldCertificate(oldCert);
					return;
				}
			}
			else
			{
				this.ForgetCertificateCheck();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003090 File Offset: 0x00001290
		private void ClockWorkServerUseNewCertificate(X509Certificate2 cert)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, this.ServerInstance.X509FindType, new string[]
			{
				"ClockWorkServer Application",
				this.ServerInstance.VirtualDirectory,
				"x509FindType"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, this.ServerInstance.X509FindValue, new string[]
			{
				"ClockWorkServer Application",
				this.ServerInstance.VirtualDirectory,
				"x509FindValue"
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003118 File Offset: 0x00001318
		private void RememberOldCertificate(X509Certificate2 oldCert)
		{
			new RegistryHelper().WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, oldCert.Subject, new string[]
			{
				"ClockWorkServer Application",
				this.ServerInstance.InstanceName,
				"ClockWorkServerJobs",
				this.JobName,
				"OldCertificateSubjectDistinguishedName"
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003170 File Offset: 0x00001370
		private void ForgetCertificateCheck()
		{
			new RegistryHelper().WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, string.Empty, new string[]
			{
				"ClockWorkServer Application",
				this.ServerInstance.InstanceName,
				"ClockWorkServerJobs",
				this.JobName,
				"OldCertificateSubjectDistinguishedName"
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031C8 File Offset: 0x000013C8
		private void SwitchCertificates(X509Certificate2 oldCert)
		{
			X509Certificate2 x509Certificate = null;
			if (oldCert != null)
			{
				CWLogger.Logger.Info("{0}:: Switching current certificate '{1}' ...", this.JobName, oldCert.Thumbprint);
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, oldCert.Subject, true);
				x509Store.Close();
				if (x509Certificate2Collection != null && x509Certificate2Collection.Count > 1)
				{
					x509Certificate = x509Certificate2Collection.Cast<X509Certificate2>().FirstOrDefault(delegate(X509Certificate2 cer)
					{
						if (!cer.Subject.Equals(cer.Issuer, StringComparison.OrdinalIgnoreCase))
						{
							string text = cer.Thumbprint.Trim();
							string thumbprint = oldCert.Thumbprint;
							if (!text.Equals((thumbprint != null) ? thumbprint.Trim() : null, StringComparison.OrdinalIgnoreCase) && cer.NotAfter > DateTime.Today.AddDays(7.0) && cer.NotBefore < DateTime.Now)
							{
								return cer.Verify();
							}
						}
						return false;
					});
					if (x509Certificate != null)
					{
						CWLogger.Logger.Info("{0}:: Current certificate '{1}' is about to expired or is already expired, a new certificate '{2}' with the same subject name was found and will be used as a replacement", this.JobName, oldCert.Thumbprint, x509Certificate.Thumbprint);
					}
				}
			}
			if (x509Certificate == null)
			{
				CWLogger.Logger.Info("{0}:: None real certificate was found to replace current one, a new self signed one will be created and use it", this.JobName);
				x509Certificate = this.CreateNewSelfSignedCertificate();
			}
			if (x509Certificate != null)
			{
				this.SwitchCertificates(oldCert, x509Certificate);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000032B8 File Offset: 0x000014B8
		private void SaveBehaviorsConfig(string envConfigPath, IDictionary<string, string> codes)
		{
			string path = Path.Combine(envConfigPath, "system.serviceModel.behaviors.pattern.config");
			if (File.Exists(path))
			{
				string text = File.ReadAllText(path);
				text = text.ReplaceCodes(codes);
				File.WriteAllText(Path.Combine(envConfigPath, "system.serviceModel.behaviors.config"), text);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000032FC File Offset: 0x000014FC
		private X509Certificate2 CreateNewSelfSignedCertificate()
		{
			X509Certificate2 result;
			try
			{
				CWLogger.Logger.Info("{0}:: *********** New self-signed certificate **********", this.JobName);
				X509Certificate2 x509Certificate = X509CertificateAdapter.CreateSelfSignedCertificate(DateTime.Today, DateTime.Today.AddYears(3), string.Format("ClockWork self-signed certificate on {0}", DateTime.Today.ToString("MMM, yyyy")));
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.ReadWrite);
				x509Store.Add(x509Certificate);
				x509Store.Close();
				CWLogger.Logger.Info("{0}:: Added to Personal Certificate store", this.JobName);
				X509Store x509Store2 = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
				x509Store2.Open(OpenFlags.ReadWrite);
				x509Store2.Add(x509Certificate);
				x509Store2.Close();
				CWLogger.Logger.Info("{0}:: Added to Trusted Root Certificate Authorities store", this.JobName);
				CWLogger.Logger.Info("{0}:: ******** End of new self-signed certificate *********", this.JobName);
				result = x509Certificate;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("{0}: {1}", ex.ToString(), this.JobName), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003404 File Offset: 0x00001604
		private bool ValidCertificate(X509Certificate2 cert)
		{
			if (cert == null)
			{
				return false;
			}
			string expirationDateString = cert.GetExpirationDateString();
			DateTime t;
			return !string.IsNullOrEmpty(expirationDateString) && DateTime.TryParse(expirationDateString, out t) && t > DateTime.Now.AddDays(2.0);
		}
	}
}

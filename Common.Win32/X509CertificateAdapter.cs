using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using ClockWorkLogger;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000011 RID: 17
	public static class X509CertificateAdapter
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00003FAC File Offset: 0x000021AC
		public static X509Certificate2 CreateSelfSignedCertificate(DateTime startDate, DateTime endDate, string friendlyName = "ClockWork Self Signed Certificate")
		{
			X509Certificate2 result;
			try
			{
				string text = "techno09";
				result = new X509Certificate2(Certificate.CreateSelfSignCertificatePfx(string.Format("CN = {0}", Environment.FullComputerName), startDate, endDate, text), text, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet)
				{
					FriendlyName = friendlyName
				};
			}
			catch (CryptographicException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("X509CertificateAdapter::CreateSelfSignedCertificate: {0}", ex.ToString()), ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004018 File Offset: 0x00002218
		public static void Install(this X509Certificate2 certificate, StoreName storeName, StoreLocation storeLocation)
		{
			try
			{
				X509Store x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadWrite);
				x509Store.Add(certificate);
				x509Store.Close();
			}
			catch (CryptographicException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("X509CertificateAdapter::Install: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004070 File Offset: 0x00002270
		public static void Remove(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object value)
		{
			try
			{
				X509Store x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadWrite);
				X509Certificate2Collection certificates = x509Store.Certificates.Find(findType, value, false);
				try
				{
					x509Store.RemoveRange(certificates);
				}
				catch
				{
				}
				x509Store.Close();
			}
			catch (CryptographicException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("X509CertificateAdapter::Remove: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000040EC File Offset: 0x000022EC
		public static void RemoveByThumbprint(StoreName storeName, StoreLocation storeLocation, string cerThumbprint)
		{
			try
			{
				X509Store x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadWrite);
				X509Certificate2Collection certificates = x509Store.Certificates.Find(X509FindType.FindByThumbprint, cerThumbprint, false);
				try
				{
					x509Store.RemoveRange(certificates);
				}
				catch
				{
				}
				x509Store.Close();
			}
			catch (CryptographicException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("X509CertificateAdapter::Remove: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004168 File Offset: 0x00002368
		public static void RemoveByBySubjectDistinguishedName(StoreName storeName, StoreLocation storeLocation, string cerSubjectName)
		{
			try
			{
				X509Store x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadWrite);
				X509Certificate2Collection certificates = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, cerSubjectName, false);
				try
				{
					x509Store.RemoveRange(certificates);
				}
				catch
				{
				}
				x509Store.Close();
			}
			catch (CryptographicException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("X509CertificateAdapter::Remove: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000041E4 File Offset: 0x000023E4
		public static string ToBase64String(this X509Certificate2 certificate)
		{
			return Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000041F2 File Offset: 0x000023F2
		public static void ImportFromBase64String(this X509Certificate2 certificate, string base64Cert)
		{
			certificate.Import(Convert.FromBase64String(base64Cert));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004200 File Offset: 0x00002400
		public static void AddAccessToCertificate(this X509Certificate2 certificate, string user)
		{
			try
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = certificate.PrivateKey as RSACryptoServiceProvider;
				if (rsacryptoServiceProvider != null)
				{
					FileInfo fileInfo = new FileInfo(X509CertificateAdapter.FindKeyLocation(rsacryptoServiceProvider.CspKeyContainerInfo.UniqueKeyContainerName) + "\\" + rsacryptoServiceProvider.CspKeyContainerInfo.UniqueKeyContainerName);
					FileSecurity accessControl = fileInfo.GetAccessControl();
					NTAccount identity = new NTAccount(user);
					accessControl.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.FullControl, AccessControlType.Allow));
					fileInfo.SetAccessControl(accessControl);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("X509CertificateAdapter::AddAccessToCertificate: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000429C File Offset: 0x0000249C
		public static bool IsSelfSigned(this X509Certificate2 certificate)
		{
			return certificate != null && certificate.Subject.Equals(certificate.Issuer, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000042B8 File Offset: 0x000024B8
		private static string FindKeyLocation(string keyFileName)
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Microsoft\\Crypto\\RSA\\MachineKeys";
			string[] array = Directory.GetFiles(text, keyFileName);
			if (array.Length != 0)
			{
				return text;
			}
			array = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Crypto\\RSA\\");
			if (array.Length != 0)
			{
				foreach (string text2 in array)
				{
					array = Directory.GetFiles(text2, keyFileName);
					if (array.Length != 0)
					{
						return text2;
					}
				}
			}
			return "Private key exists but is not accessible";
		}
	}
}

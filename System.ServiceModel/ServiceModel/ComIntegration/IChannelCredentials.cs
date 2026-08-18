using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000223 RID: 547
	[Guid("181b448c-c17c-4b17-ac6d-06699b93198f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	[ComImport]
	public interface IChannelCredentials
	{
		// Token: 0x06001088 RID: 4232
		void SetWindowsCredential(string domain, string userName, string password, int impersonationLevel, bool allowNtlm);

		// Token: 0x06001089 RID: 4233
		void SetUserNameCredential(string userName, string password);

		// Token: 0x0600108A RID: 4234
		void SetClientCertificateFromStore(string storeLocation, string storeName, string findType, object findValue);

		// Token: 0x0600108B RID: 4235
		void SetClientCertificateFromStoreByName(string subjectName, string storeLocation, string storeName);

		// Token: 0x0600108C RID: 4236
		void SetClientCertificateFromFile(string fileName, string password, string keyStorageFlags);

		// Token: 0x0600108D RID: 4237
		void SetDefaultServiceCertificateFromStore(string storeLocation, string storeName, string findType, object findValue);

		// Token: 0x0600108E RID: 4238
		void SetDefaultServiceCertificateFromStoreByName(string subjectName, string storeLocation, string storeName);

		// Token: 0x0600108F RID: 4239
		void SetDefaultServiceCertificateFromFile(string fileName, string password, string keyStorageFlags);

		// Token: 0x06001090 RID: 4240
		void SetServiceCertificateAuthentication(string storeLocation, string revocationMode, string certificationValidationMode);

		// Token: 0x06001091 RID: 4241
		void SetIssuedToken(string localIssuerAddres, string localIssuerBindingType, string localIssuerBinding);
	}
}

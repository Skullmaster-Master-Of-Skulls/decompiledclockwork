using System;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200046A RID: 1130
	public class PkixAttrCertPathValidator
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x000EA23C File Offset: 0x000E923C
		public virtual PkixCertPathValidatorResult Validate(PkixCertPath certPath, PkixParameters pkixParams)
		{
			IX509Selector targetConstraints = pkixParams.GetTargetConstraints();
			if (!(targetConstraints is X509AttrCertStoreSelector))
			{
				throw new ArgumentException("TargetConstraints must be an instance of " + typeof(X509AttrCertStoreSelector).FullName, "pkixParams");
			}
			IX509AttributeCertificate attributeCert = ((X509AttrCertStoreSelector)targetConstraints).AttributeCert;
			PkixCertPath holderCertPath = Rfc3281CertPathUtilities.ProcessAttrCert1(attributeCert, pkixParams);
			PkixCertPathValidatorResult result = Rfc3281CertPathUtilities.ProcessAttrCert2(certPath, pkixParams);
			X509Certificate x509Certificate = (X509Certificate)certPath.Certificates[0];
			Rfc3281CertPathUtilities.ProcessAttrCert3(x509Certificate, pkixParams);
			Rfc3281CertPathUtilities.ProcessAttrCert4(x509Certificate, pkixParams);
			Rfc3281CertPathUtilities.ProcessAttrCert5(attributeCert, pkixParams);
			Rfc3281CertPathUtilities.ProcessAttrCert7(attributeCert, certPath, holderCertPath, pkixParams);
			Rfc3281CertPathUtilities.AdditionalChecks(attributeCert, pkixParams);
			DateTime validCertDateFromValidityModel;
			try
			{
				validCertDateFromValidityModel = PkixCertPathValidatorUtilities.GetValidCertDateFromValidityModel(pkixParams, null, -1);
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Could not get validity date from attribute certificate.", cause);
			}
			Rfc3281CertPathUtilities.CheckCrls(attributeCert, pkixParams, x509Certificate, validCertDateFromValidityModel, certPath.Certificates);
			return result;
		}
	}
}

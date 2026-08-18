using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.IsisMtt;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000009 RID: 9
	public class PkixCertPathValidatorUtilities
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002D88 File Offset: 0x00001D88
		internal static TrustAnchor FindTrustAnchor(X509Certificate cert, ISet trustAnchors)
		{
			IEnumerator enumerator = trustAnchors.GetEnumerator();
			TrustAnchor trustAnchor = null;
			AsymmetricKeyParameter asymmetricKeyParameter = null;
			Exception ex = null;
			X509CertStoreSelector x509CertStoreSelector = new X509CertStoreSelector();
			try
			{
				x509CertStoreSelector.Subject = PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert);
				goto IL_C4;
			}
			catch (IOException innerException)
			{
				throw new Exception("Cannot set subject search criteria for trust anchor.", innerException);
			}
			IL_35:
			trustAnchor = (TrustAnchor)enumerator.Current;
			if (trustAnchor.TrustedCert != null)
			{
				if (x509CertStoreSelector.Match(trustAnchor.TrustedCert))
				{
					asymmetricKeyParameter = trustAnchor.TrustedCert.GetPublicKey();
				}
				else
				{
					trustAnchor = null;
				}
			}
			else
			{
				if (trustAnchor.CAName != null && trustAnchor.CAPublicKey != null)
				{
					try
					{
						X509Name issuerPrincipal = PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert);
						X509Name other = new X509Name(trustAnchor.CAName);
						if (issuerPrincipal.Equivalent(other, true))
						{
							asymmetricKeyParameter = trustAnchor.CAPublicKey;
						}
						else
						{
							trustAnchor = null;
						}
						goto IL_AF;
					}
					catch (InvalidParameterException)
					{
						trustAnchor = null;
						goto IL_AF;
					}
				}
				trustAnchor = null;
			}
			IL_AF:
			if (asymmetricKeyParameter != null)
			{
				try
				{
					cert.Verify(asymmetricKeyParameter);
				}
				catch (Exception ex2)
				{
					ex = ex2;
					trustAnchor = null;
				}
			}
			IL_C4:
			if (enumerator.MoveNext() && trustAnchor == null)
			{
				goto IL_35;
			}
			if (trustAnchor == null && ex != null)
			{
				throw new Exception("TrustAnchor found but certificate validation failed.", ex);
			}
			return trustAnchor;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002EA4 File Offset: 0x00001EA4
		internal static void AddAdditionalStoresFromAltNames(X509Certificate cert, PkixParameters pkixParams)
		{
			if (cert.GetIssuerAlternativeNames() != null)
			{
				foreach (object obj in cert.GetIssuerAlternativeNames())
				{
					IList list = (IList)obj;
					if (list[0].Equals(6))
					{
						string location = (string)list[1];
						PkixCertPathValidatorUtilities.AddAdditionalStoreFromLocation(location, pkixParams);
					}
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F04 File Offset: 0x00001F04
		internal static DateTime GetValidDate(PkixParameters paramsPKIX)
		{
			DateTimeObject date = paramsPKIX.Date;
			if (date == null)
			{
				return DateTime.UtcNow;
			}
			return date.Value;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F27 File Offset: 0x00001F27
		internal static X509Name GetIssuerPrincipal(object cert)
		{
			if (cert is X509Certificate)
			{
				return ((X509Certificate)cert).IssuerDN;
			}
			return ((IX509AttributeCertificate)cert).Issuer.GetPrincipals()[0];
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F4F File Offset: 0x00001F4F
		internal static bool IsSelfIssued(X509Certificate cert)
		{
			return cert.SubjectDN.Equivalent(cert.IssuerDN, true);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F64 File Offset: 0x00001F64
		internal static AlgorithmIdentifier GetAlgorithmIdentifier(AsymmetricKeyParameter key)
		{
			AlgorithmIdentifier algorithmID;
			try
			{
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(key);
				algorithmID = subjectPublicKeyInfo.AlgorithmID;
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Subject public key cannot be decoded.", cause);
			}
			return algorithmID;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FA0 File Offset: 0x00001FA0
		internal static ICollection FindCrls(X509CrlStoreSelector crlSelect, IList crlStores)
		{
			ISet set = new HashSet();
			Exception ex = null;
			bool flag = false;
			foreach (object obj in crlStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					foreach (object obj2 in ix509Store.GetMatches(crlSelect))
					{
						X509Crl o = (X509Crl)obj2;
						set.Add(o);
					}
					flag = true;
				}
				catch (Exception innerException)
				{
					ex = new Exception("Exception searching in X.509 CRL store.", innerException);
				}
			}
			if (!flag && ex != null)
			{
				throw ex;
			}
			return set;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000307C File Offset: 0x0000207C
		internal static bool IsAnyPolicy(ISet policySet)
		{
			return policySet == null || policySet.Contains(PkixCertPathValidatorUtilities.ANY_POLICY) || policySet.Count == 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000309C File Offset: 0x0000209C
		internal static void AddAdditionalStoreFromLocation(string location, PkixParameters pkixParams)
		{
			if (pkixParams.IsAdditionalLocationsEnabled)
			{
				try
				{
					if (location.StartsWith("ldap://"))
					{
						location = location.Substring(7);
						int num = location.IndexOf('/');
						if (num != -1)
						{
							"ldap://" + location.Substring(0, num);
							location.Substring(num);
						}
						else
						{
							"ldap://" + location;
						}
						throw Platform.CreateNotImplementedException("LDAP cert/CRL stores");
					}
				}
				catch (Exception)
				{
					throw new Exception("Exception adding X.509 stores.");
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003128 File Offset: 0x00002128
		private static BigInteger GetSerialNumber(object cert)
		{
			if (cert is X509Certificate)
			{
				return ((X509Certificate)cert).SerialNumber;
			}
			return ((X509V2AttributeCertificate)cert).SerialNumber;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000314C File Offset: 0x0000214C
		internal static ISet GetQualifierSet(Asn1Sequence qualifiers)
		{
			ISet set = new HashSet();
			if (qualifiers == null)
			{
				return set;
			}
			foreach (object obj in qualifiers)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				try
				{
					set.Add(PolicyQualifierInfo.GetInstance(asn1Encodable.ToAsn1Object()));
				}
				catch (IOException cause)
				{
					throw new PkixCertPathValidatorException("Policy qualifier info cannot be decoded.", cause);
				}
			}
			return set;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031D4 File Offset: 0x000021D4
		internal static PkixPolicyNode RemovePolicyNode(PkixPolicyNode validPolicyTree, IList[] policyNodes, PkixPolicyNode _node)
		{
			PkixPolicyNode parent = _node.Parent;
			if (validPolicyTree == null)
			{
				return null;
			}
			if (parent == null)
			{
				for (int i = 0; i < policyNodes.Length; i++)
				{
					policyNodes[i] = new ArrayList();
				}
				return null;
			}
			parent.RemoveChild(_node);
			PkixCertPathValidatorUtilities.RemovePolicyNodeRecurse(policyNodes, _node);
			return validPolicyTree;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003218 File Offset: 0x00002218
		private static void RemovePolicyNodeRecurse(IList[] policyNodes, PkixPolicyNode _node)
		{
			policyNodes[_node.Depth].Remove(_node);
			if (_node.HasChildren)
			{
				foreach (object obj in _node.Children)
				{
					PkixPolicyNode node = (PkixPolicyNode)obj;
					PkixCertPathValidatorUtilities.RemovePolicyNodeRecurse(policyNodes, node);
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003288 File Offset: 0x00002288
		internal static void PrepareNextCertB1(int i, IList[] policyNodes, string id_p, IDictionary m_idp, X509Certificate cert)
		{
			bool flag = false;
			foreach (object obj in policyNodes[i])
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				if (pkixPolicyNode.ValidPolicy.Equals(id_p))
				{
					flag = true;
					pkixPolicyNode.ExpectedPolicies = (ISet)m_idp[id_p];
					break;
				}
			}
			if (!flag)
			{
				foreach (object obj2 in policyNodes[i])
				{
					PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj2;
					if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(pkixPolicyNode2.ValidPolicy))
					{
						ISet policyQualifiers = null;
						Asn1Sequence asn1Sequence = null;
						try
						{
							asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(cert, X509Extensions.CertificatePolicies));
						}
						catch (Exception innerException)
						{
							throw new Exception("Certificate policies cannot be decoded.", innerException);
						}
						IEnumerator enumerator2 = asn1Sequence.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							PolicyInformation policyInformation = null;
							try
							{
								policyInformation = PolicyInformation.GetInstance(enumerator2.Current);
							}
							catch (Exception innerException2)
							{
								throw new Exception("Policy information cannot be decoded.", innerException2);
							}
							if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(policyInformation.PolicyIdentifier.Id))
							{
								try
								{
									policyQualifiers = PkixCertPathValidatorUtilities.GetQualifierSet(policyInformation.PolicyQualifiers);
									break;
								}
								catch (PkixCertPathValidatorException cause)
								{
									throw new PkixCertPathValidatorException("Policy qualifier info set could not be built.", cause);
								}
							}
						}
						bool critical = false;
						ISet criticalExtensionOids = cert.GetCriticalExtensionOids();
						if (criticalExtensionOids != null)
						{
							critical = criticalExtensionOids.Contains(X509Extensions.CertificatePolicies.Id);
						}
						PkixPolicyNode parent = pkixPolicyNode2.Parent;
						if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(parent.ValidPolicy))
						{
							PkixPolicyNode pkixPolicyNode3 = new PkixPolicyNode(new ArrayList(), i, (ISet)m_idp[id_p], parent, policyQualifiers, id_p, critical);
							parent.AddChild(pkixPolicyNode3);
							policyNodes[i].Add(pkixPolicyNode3);
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000344C File Offset: 0x0000244C
		internal static PkixPolicyNode PrepareNextCertB2(int i, IList[] policyNodes, string id_p, PkixPolicyNode validPolicyTree)
		{
			int num = 0;
			foreach (object obj in new ArrayList(policyNodes[i]))
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)obj;
				if (pkixPolicyNode.ValidPolicy.Equals(id_p))
				{
					PkixPolicyNode parent = pkixPolicyNode.Parent;
					parent.RemoveChild(pkixPolicyNode);
					policyNodes[i].RemoveAt(num);
					for (int j = i - 1; j >= 0; j--)
					{
						IList list = policyNodes[j];
						for (int k = 0; k < list.Count; k++)
						{
							PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)list[k];
							if (!pkixPolicyNode2.HasChildren)
							{
								validPolicyTree = PkixCertPathValidatorUtilities.RemovePolicyNode(validPolicyTree, policyNodes, pkixPolicyNode2);
								if (validPolicyTree == null)
								{
									break;
								}
							}
						}
					}
				}
				else
				{
					num++;
				}
			}
			return validPolicyTree;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000352C File Offset: 0x0000252C
		internal static void GetCertStatus(DateTime validDate, X509Crl crl, object cert, CertStatus certStatus)
		{
			X509Crl x509Crl = null;
			try
			{
				x509Crl = new X509Crl(CertificateList.GetInstance((Asn1Sequence)Asn1Object.FromByteArray(crl.GetEncoded())));
			}
			catch (Exception innerException)
			{
				throw new Exception("Bouncy Castle X509Crl could not be created.", innerException);
			}
			X509CrlEntry revokedCertificate = x509Crl.GetRevokedCertificate(PkixCertPathValidatorUtilities.GetSerialNumber(cert));
			if (revokedCertificate == null)
			{
				return;
			}
			X509Name issuerPrincipal = PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert);
			if (issuerPrincipal.Equivalent(revokedCertificate.GetCertificateIssuer(), true) || issuerPrincipal.Equivalent(crl.IssuerDN, true))
			{
				DerEnumerated derEnumerated = null;
				if (revokedCertificate.HasExtensions)
				{
					try
					{
						derEnumerated = DerEnumerated.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(revokedCertificate, X509Extensions.ReasonCode));
					}
					catch (Exception innerException2)
					{
						new Exception("Reason code CRL entry extension could not be decoded.", innerException2);
					}
				}
				if (validDate.Ticks >= revokedCertificate.RevocationDate.Ticks || derEnumerated == null || derEnumerated.Value.TestBit(0) || derEnumerated.Value.TestBit(1) || derEnumerated.Value.TestBit(2) || derEnumerated.Value.TestBit(8))
				{
					if (derEnumerated != null)
					{
						certStatus.Status = derEnumerated.Value.SignValue;
					}
					else
					{
						certStatus.Status = 0;
					}
					certStatus.RevocationDate = new DateTimeObject(revokedCertificate.RevocationDate);
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003674 File Offset: 0x00002674
		internal static AsymmetricKeyParameter GetNextWorkingKey(IList certs, int index)
		{
			X509Certificate x509Certificate = (X509Certificate)certs[index];
			AsymmetricKeyParameter publicKey = x509Certificate.GetPublicKey();
			if (!(publicKey is DsaPublicKeyParameters))
			{
				return publicKey;
			}
			DsaPublicKeyParameters dsaPublicKeyParameters = (DsaPublicKeyParameters)publicKey;
			if (dsaPublicKeyParameters.Parameters != null)
			{
				return dsaPublicKeyParameters;
			}
			for (int i = index + 1; i < certs.Count; i++)
			{
				X509Certificate x509Certificate2 = (X509Certificate)certs[i];
				publicKey = x509Certificate2.GetPublicKey();
				if (!(publicKey is DsaPublicKeyParameters))
				{
					throw new PkixCertPathValidatorException("DSA parameters cannot be inherited from previous certificate.");
				}
				DsaPublicKeyParameters dsaPublicKeyParameters2 = (DsaPublicKeyParameters)publicKey;
				if (dsaPublicKeyParameters2.Parameters != null)
				{
					DsaParameters parameters = dsaPublicKeyParameters2.Parameters;
					try
					{
						return new DsaPublicKeyParameters(dsaPublicKeyParameters.Y, parameters);
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message);
					}
				}
			}
			throw new PkixCertPathValidatorException("DSA parameters cannot be inherited from previous certificate.");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003744 File Offset: 0x00002744
		internal static DateTime GetValidCertDateFromValidityModel(PkixParameters paramsPkix, PkixCertPath certPath, int index)
		{
			if (paramsPkix.ValidityModel != 1)
			{
				return PkixCertPathValidatorUtilities.GetValidDate(paramsPkix);
			}
			if (index <= 0)
			{
				return PkixCertPathValidatorUtilities.GetValidDate(paramsPkix);
			}
			if (index - 1 == 0)
			{
				DerGeneralizedTime derGeneralizedTime = null;
				try
				{
					X509Certificate x509Certificate = (X509Certificate)certPath.Certificates[index - 1];
					Asn1OctetString extensionValue = x509Certificate.GetExtensionValue(IsisMttObjectIdentifiers.IdIsisMttATDateOfCertGen);
					derGeneralizedTime = DerGeneralizedTime.GetInstance(extensionValue);
				}
				catch (ArgumentException)
				{
					throw new Exception("Date of cert gen extension could not be read.");
				}
				if (derGeneralizedTime != null)
				{
					try
					{
						return derGeneralizedTime.ToDateTime();
					}
					catch (ArgumentException innerException)
					{
						throw new Exception("Date from date of cert gen extension could not be parsed.", innerException);
					}
				}
			}
			return ((X509Certificate)certPath.Certificates[index - 1]).NotBefore;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000037FC File Offset: 0x000027FC
		internal static ICollection FindCertificates(X509CertStoreSelector certSelect, IList certStores)
		{
			ISet set = new HashSet();
			foreach (object obj in certStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					foreach (object obj2 in ix509Store.GetMatches(certSelect))
					{
						X509Certificate o = (X509Certificate)obj2;
						set.Add(o);
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("Problem while picking certificates from X.509 store.", innerException);
				}
			}
			return set;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000038C4 File Offset: 0x000028C4
		internal static void GetCrlIssuersFromDistributionPoint(DistributionPoint dp, ICollection issuerPrincipals, X509CrlStoreSelector selector, PkixParameters pkixParams)
		{
			IList list = new ArrayList();
			if (dp.CrlIssuer != null)
			{
				GeneralName[] names = dp.CrlIssuer.GetNames();
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].TagNo == 4)
					{
						try
						{
							list.Add(X509Name.GetInstance(names[i].Name.ToAsn1Object()));
						}
						catch (IOException innerException)
						{
							throw new Exception("CRL issuer information from distribution point cannot be decoded.", innerException);
						}
					}
				}
			}
			else
			{
				if (dp.DistributionPointName == null)
				{
					throw new Exception("CRL issuer is omitted from distribution point but no distributionPoint field present.");
				}
				foreach (object obj in issuerPrincipals)
				{
					list.Add((X509Name)obj);
				}
			}
			selector.Issuers = list;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003980 File Offset: 0x00002980
		internal static ISet GetCompleteCrls(DistributionPoint dp, object cert, DateTime currentDate, PkixParameters paramsPKIX)
		{
			X509CrlStoreSelector x509CrlStoreSelector = new X509CrlStoreSelector();
			try
			{
				ISet set = new HashSet();
				if (cert is X509V2AttributeCertificate)
				{
					set.Add(((X509V2AttributeCertificate)cert).Issuer.GetPrincipals()[0]);
				}
				else
				{
					set.Add(PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert));
				}
				PkixCertPathValidatorUtilities.GetCrlIssuersFromDistributionPoint(dp, set, x509CrlStoreSelector, paramsPKIX);
			}
			catch (Exception innerException)
			{
				new Exception("Could not get issuer information from distribution point.", innerException);
			}
			if (cert is X509Certificate)
			{
				x509CrlStoreSelector.CertificateChecking = (X509Certificate)cert;
			}
			else if (cert is X509V2AttributeCertificate)
			{
				x509CrlStoreSelector.AttrCertChecking = (IX509AttributeCertificate)cert;
			}
			if (paramsPKIX.Date != null)
			{
				x509CrlStoreSelector.DateAndTime = paramsPKIX.Date;
			}
			else
			{
				x509CrlStoreSelector.DateAndTime = new DateTimeObject(currentDate);
			}
			x509CrlStoreSelector.CompleteCrlEnabled = true;
			ISet set2 = new HashSet();
			try
			{
				set2.AddAll(PkixCertPathValidatorUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX.GetStores()));
				set2.AddAll(PkixCertPathValidatorUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX.GetAdditionalStores()));
			}
			catch (Exception innerException2)
			{
				throw new Exception("Could not search for CRLs.", innerException2);
			}
			if (!set2.IsEmpty)
			{
				return set2;
			}
			if (cert is IX509AttributeCertificate)
			{
				IX509AttributeCertificate ix509AttributeCertificate = (IX509AttributeCertificate)cert;
				throw new Exception("No CRLs found for issuer \"" + ix509AttributeCertificate.Issuer.GetPrincipals()[0] + "\"");
			}
			X509Certificate x509Certificate = (X509Certificate)cert;
			throw new Exception("No CRLs found for issuer \"" + x509Certificate.IssuerDN + "\"");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003AEC File Offset: 0x00002AEC
		internal static ISet GetDeltaCrls(DateTime currentDate, PkixParameters paramsPKIX, X509Crl completeCRL)
		{
			X509CrlStoreSelector x509CrlStoreSelector = new X509CrlStoreSelector();
			if (paramsPKIX.Date != null)
			{
				x509CrlStoreSelector.DateAndTime = paramsPKIX.Date;
			}
			else
			{
				x509CrlStoreSelector.DateAndTime = new DateTimeObject(currentDate);
			}
			try
			{
				x509CrlStoreSelector.Issuers = new ArrayList
				{
					completeCRL.IssuerDN
				};
			}
			catch (IOException innerException)
			{
				new Exception("Cannot extract issuer from CRL.", innerException);
			}
			BigInteger bigInteger = null;
			try
			{
				Asn1Object extensionValue = PkixCertPathValidatorUtilities.GetExtensionValue(completeCRL, X509Extensions.CrlNumber);
				if (extensionValue != null)
				{
					bigInteger = DerInteger.GetInstance(extensionValue).PositiveValue;
				}
			}
			catch (Exception innerException2)
			{
				throw new Exception("CRL number extension could not be extracted from CRL.", innerException2);
			}
			byte[] issuingDistributionPoint = null;
			try
			{
				Asn1Object extensionValue2 = PkixCertPathValidatorUtilities.GetExtensionValue(completeCRL, X509Extensions.IssuingDistributionPoint);
				if (extensionValue2 != null)
				{
					issuingDistributionPoint = extensionValue2.GetDerEncoded();
				}
			}
			catch (Exception innerException3)
			{
				throw new Exception("Issuing distribution point extension value could not be read.", innerException3);
			}
			x509CrlStoreSelector.MinCrlNumber = ((bigInteger == null) ? null : bigInteger.Add(BigInteger.One));
			x509CrlStoreSelector.IssuingDistributionPoint = issuingDistributionPoint;
			x509CrlStoreSelector.IssuingDistributionPointEnabled = true;
			x509CrlStoreSelector.MaxBaseCrlNumber = bigInteger;
			ISet set = new HashSet();
			try
			{
				set.AddAll(PkixCertPathValidatorUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX.GetAdditionalStores()));
				set.AddAll(PkixCertPathValidatorUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX.GetStores()));
			}
			catch (Exception innerException4)
			{
				throw new Exception("Could not search for delta CRLs.", innerException4);
			}
			ISet set2 = new HashSet();
			foreach (object obj in set)
			{
				X509Crl x509Crl = (X509Crl)obj;
				if (PkixCertPathValidatorUtilities.isDeltaCrl(x509Crl))
				{
					set2.Add(x509Crl);
				}
			}
			return set2;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003CAC File Offset: 0x00002CAC
		private static bool isDeltaCrl(X509Crl crl)
		{
			ISet criticalExtensionOids = crl.GetCriticalExtensionOids();
			return criticalExtensionOids.Contains(X509Extensions.DeltaCrlIndicator.Id);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003CD0 File Offset: 0x00002CD0
		internal static ICollection FindCertificates(X509AttrCertStoreSelector certSelect, IList certStores)
		{
			ISet set = new HashSet();
			foreach (object obj in certStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					foreach (object obj2 in ix509Store.GetMatches(certSelect))
					{
						X509V2AttributeCertificate o = (X509V2AttributeCertificate)obj2;
						set.Add(o);
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("Problem while picking certificates from X.509 store.", innerException);
				}
			}
			return set;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003D98 File Offset: 0x00002D98
		internal static void AddAdditionalStoresFromCrlDistributionPoint(CrlDistPoint crldp, PkixParameters pkixParams)
		{
			if (crldp != null)
			{
				DistributionPoint[] array = null;
				try
				{
					array = crldp.GetDistributionPoints();
				}
				catch (Exception innerException)
				{
					throw new Exception("Distribution points could not be read.", innerException);
				}
				for (int i = 0; i < array.Length; i++)
				{
					DistributionPointName distributionPointName = array[i].DistributionPointName;
					if (distributionPointName != null && distributionPointName.PointType == 0)
					{
						GeneralName[] names = GeneralNames.GetInstance(distributionPointName.Name).GetNames();
						for (int j = 0; j < names.Length; j++)
						{
							if (names[j].TagNo == 6)
							{
								string @string = DerIA5String.GetInstance(names[j].Name).GetString();
								PkixCertPathValidatorUtilities.AddAdditionalStoreFromLocation(@string, pkixParams);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003E48 File Offset: 0x00002E48
		internal static bool ProcessCertD1i(int index, IList[] policyNodes, DerObjectIdentifier pOid, ISet pq)
		{
			IList list = policyNodes[index - 1];
			for (int i = 0; i < list.Count; i++)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)list[i];
				ISet expectedPolicies = pkixPolicyNode.ExpectedPolicies;
				if (expectedPolicies.Contains(pOid.Id))
				{
					ISet set = new HashSet();
					set.Add(pOid.Id);
					PkixPolicyNode pkixPolicyNode2 = new PkixPolicyNode(new ArrayList(), index, set, pkixPolicyNode, pq, pOid.Id, false);
					pkixPolicyNode.AddChild(pkixPolicyNode2);
					policyNodes[index].Add(pkixPolicyNode2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003ED0 File Offset: 0x00002ED0
		internal static void ProcessCertD1ii(int index, IList[] policyNodes, DerObjectIdentifier _poid, ISet _pq)
		{
			IList list = policyNodes[index - 1];
			for (int i = 0; i < list.Count; i++)
			{
				PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)list[i];
				if (PkixCertPathValidatorUtilities.ANY_POLICY.Equals(pkixPolicyNode.ValidPolicy))
				{
					ISet set = new HashSet();
					set.Add(_poid.Id);
					PkixPolicyNode pkixPolicyNode2 = new PkixPolicyNode(new ArrayList(), index, set, pkixPolicyNode, _pq, _poid.Id, false);
					pkixPolicyNode.AddChild(pkixPolicyNode2);
					policyNodes[index].Add(pkixPolicyNode2);
					return;
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003F50 File Offset: 0x00002F50
		internal static ICollection FindIssuerCerts(X509Certificate cert, PkixBuilderParameters pkixParams)
		{
			X509CertStoreSelector x509CertStoreSelector = new X509CertStoreSelector();
			ISet set = new HashSet();
			try
			{
				x509CertStoreSelector.Subject = cert.IssuerDN;
			}
			catch (IOException innerException)
			{
				throw new Exception("Subject criteria for certificate selector to find issuer certificate could not be set.", innerException);
			}
			try
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(PkixCertPathValidatorUtilities.FindCertificates(x509CertStoreSelector, pkixParams.GetStores()));
				arrayList.AddRange(PkixCertPathValidatorUtilities.FindCertificates(x509CertStoreSelector, pkixParams.GetAdditionalStores()));
				foreach (object obj in arrayList)
				{
					X509Certificate o = (X509Certificate)obj;
					set.Add(o);
				}
			}
			catch (Exception innerException2)
			{
				throw new Exception("Issuer certificate cannot be searched.", innerException2);
			}
			return set;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004028 File Offset: 0x00003028
		internal static Asn1Object GetExtensionValue(IX509Extension ext, DerObjectIdentifier oid)
		{
			Asn1OctetString extensionValue = ext.GetExtensionValue(oid);
			if (extensionValue == null)
			{
				return null;
			}
			return X509ExtensionUtilities.FromExtensionValue(extensionValue);
		}

		// Token: 0x0400000A RID: 10
		internal static readonly string ANY_POLICY = "2.5.29.32.0";

		// Token: 0x0400000B RID: 11
		internal static readonly string CRL_NUMBER = X509Extensions.CrlNumber.Id;

		// Token: 0x0400000C RID: 12
		internal static readonly int KEY_CERT_SIGN = 5;

		// Token: 0x0400000D RID: 13
		internal static readonly int CRL_SIGN = 6;

		// Token: 0x0400000E RID: 14
		internal static readonly string[] crlReasons = new string[]
		{
			"unspecified",
			"keyCompromise",
			"cACompromise",
			"affiliationChanged",
			"superseded",
			"cessationOfOperation",
			"certificateHold",
			"unknown",
			"removeFromCRL",
			"privilegeWithdrawn",
			"aACompromise"
		};
	}
}

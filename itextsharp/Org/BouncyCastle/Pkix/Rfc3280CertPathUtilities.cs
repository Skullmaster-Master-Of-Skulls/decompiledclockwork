using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000231 RID: 561
	public class Rfc3280CertPathUtilities
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x0007E0AC File Offset: 0x0007D0AC
		internal static void ProcessCrlB2(DistributionPoint dp, object cert, X509Crl crl)
		{
			IssuingDistributionPoint issuingDistributionPoint = null;
			try
			{
				issuingDistributionPoint = IssuingDistributionPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(crl, X509Extensions.IssuingDistributionPoint));
			}
			catch (Exception innerException)
			{
				throw new Exception("0 Issuing distribution point extension could not be decoded.", innerException);
			}
			if (issuingDistributionPoint != null)
			{
				if (issuingDistributionPoint.DistributionPoint != null)
				{
					DistributionPointName distributionPointName = IssuingDistributionPoint.GetInstance(issuingDistributionPoint).DistributionPoint;
					IList list = new ArrayList();
					if (distributionPointName.PointType == 0)
					{
						GeneralName[] names = GeneralNames.GetInstance(distributionPointName.Name).GetNames();
						for (int i = 0; i < names.Length; i++)
						{
							list.Add(names[i]);
						}
					}
					if (distributionPointName.PointType == 1)
					{
						Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
						try
						{
							foreach (object obj in Asn1Sequence.GetInstance(Asn1Object.FromByteArray(crl.IssuerDN.GetEncoded())))
							{
								asn1EncodableVector.Add(new Asn1Encodable[]
								{
									(Asn1Encodable)obj
								});
							}
						}
						catch (IOException innerException2)
						{
							throw new Exception("Could not read CRL issuer.", innerException2);
						}
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							distributionPointName.Name
						});
						list.Add(new GeneralName(X509Name.GetInstance(new DerSequence(asn1EncodableVector))));
					}
					bool flag = false;
					if (dp.DistributionPointName != null)
					{
						distributionPointName = dp.DistributionPointName;
						GeneralName[] array = null;
						if (distributionPointName.PointType == 0)
						{
							array = GeneralNames.GetInstance(distributionPointName.Name).GetNames();
						}
						if (distributionPointName.PointType == 1)
						{
							if (dp.CrlIssuer != null)
							{
								array = dp.CrlIssuer.GetNames();
							}
							else
							{
								array = new GeneralName[1];
								try
								{
									array[0] = new GeneralName(PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert));
								}
								catch (IOException innerException3)
								{
									throw new Exception("Could not read certificate issuer.", innerException3);
								}
							}
							for (int j = 0; j < array.Length; j++)
							{
								IEnumerator enumerator2 = Asn1Sequence.GetInstance(array[j].Name.ToAsn1Object()).GetEnumerator();
								Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									asn1EncodableVector2.Add(new Asn1Encodable[]
									{
										(Asn1Encodable)obj2
									});
								}
								asn1EncodableVector2.Add(new Asn1Encodable[]
								{
									distributionPointName.Name
								});
								array[j] = new GeneralName(X509Name.GetInstance(new DerSequence(asn1EncodableVector2)));
							}
						}
						if (array != null)
						{
							for (int k = 0; k < array.Length; k++)
							{
								if (list.Contains(array[k]))
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							throw new Exception("No match for certificate CRL issuing distribution point name to cRLIssuer CRL distribution point.");
						}
					}
					else
					{
						if (dp.CrlIssuer == null)
						{
							throw new Exception("Either the cRLIssuer or the distributionPoint field must be contained in DistributionPoint.");
						}
						GeneralName[] names2 = dp.CrlIssuer.GetNames();
						for (int l = 0; l < names2.Length; l++)
						{
							if (list.Contains(names2[l]))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							throw new Exception("No match for certificate CRL issuing distribution point name to cRLIssuer CRL distribution point.");
						}
					}
				}
				BasicConstraints basicConstraints = null;
				try
				{
					basicConstraints = BasicConstraints.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue((IX509Extension)cert, X509Extensions.BasicConstraints));
				}
				catch (Exception innerException4)
				{
					throw new Exception("Basic constraints extension could not be decoded.", innerException4);
				}
				if (issuingDistributionPoint.OnlyContainsUserCerts && basicConstraints != null && basicConstraints.IsCA())
				{
					throw new Exception("CA Cert CRL only contains user certificates.");
				}
				if (issuingDistributionPoint.OnlyContainsCACerts && (basicConstraints == null || !basicConstraints.IsCA()))
				{
					throw new Exception("End CRL only contains CA certificates.");
				}
				if (issuingDistributionPoint.OnlyContainsAttributeCerts)
				{
					throw new Exception("onlyContainsAttributeCerts boolean is asserted.");
				}
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0007E438 File Offset: 0x0007D438
		internal static void ProcessCertBC(PkixCertPath certPath, int index, PkixNameConstraintValidator nameConstraintValidator)
		{
			IList certificates = certPath.Certificates;
			X509Certificate x509Certificate = (X509Certificate)certificates[index];
			int count = certificates.Count;
			int num = count - index;
			if (!PkixCertPathValidatorUtilities.IsSelfIssued(x509Certificate) || num >= count)
			{
				X509Name subjectDN = x509Certificate.SubjectDN;
				Asn1InputStream asn1InputStream = new Asn1InputStream(subjectDN.GetEncoded());
				Asn1Sequence instance;
				try
				{
					instance = Asn1Sequence.GetInstance(asn1InputStream.ReadObject());
				}
				catch (Exception cause)
				{
					throw new PkixCertPathValidatorException("Exception extracting subject name when checking subtrees.", cause, certPath, index);
				}
				try
				{
					nameConstraintValidator.CheckPermittedDN(instance);
					nameConstraintValidator.CheckExcludedDN(instance);
				}
				catch (PkixNameConstraintValidatorException cause2)
				{
					throw new PkixCertPathValidatorException("Subtree check for certificate subject failed.", cause2, certPath, index);
				}
				GeneralNames generalNames = null;
				try
				{
					generalNames = GeneralNames.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(x509Certificate, X509Extensions.SubjectAlternativeName));
				}
				catch (Exception cause3)
				{
					throw new PkixCertPathValidatorException("Subject alternative name extension could not be decoded.", cause3, certPath, index);
				}
				ArrayList values = X509Name.GetInstance(instance).GetValues(X509Name.EmailAddress);
				foreach (object obj in values)
				{
					string name = (string)obj;
					GeneralName name2 = new GeneralName(1, name);
					try
					{
						nameConstraintValidator.checkPermitted(name2);
						nameConstraintValidator.checkExcluded(name2);
					}
					catch (PkixNameConstraintValidatorException cause4)
					{
						throw new PkixCertPathValidatorException("Subtree check for certificate subject alternative email failed.", cause4, certPath, index);
					}
				}
				if (generalNames != null)
				{
					GeneralName[] array = null;
					try
					{
						array = generalNames.GetNames();
					}
					catch (Exception cause5)
					{
						throw new PkixCertPathValidatorException("Subject alternative name contents could not be decoded.", cause5, certPath, index);
					}
					foreach (GeneralName name3 in array)
					{
						try
						{
							nameConstraintValidator.checkPermitted(name3);
							nameConstraintValidator.checkExcluded(name3);
						}
						catch (PkixNameConstraintValidatorException cause6)
						{
							throw new PkixCertPathValidatorException("Subtree check for certificate subject alternative name failed.", cause6, certPath, index);
						}
					}
				}
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0007E62C File Offset: 0x0007D62C
		internal static void PrepareNextCertA(PkixCertPath certPath, int index)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.PolicyMappings));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Policy mappings extension could not be decoded.", cause, certPath, index);
			}
			if (asn1Sequence != null)
			{
				Asn1Sequence asn1Sequence2 = asn1Sequence;
				for (int i = 0; i < asn1Sequence2.Count; i++)
				{
					DerObjectIdentifier derObjectIdentifier = null;
					DerObjectIdentifier derObjectIdentifier2 = null;
					try
					{
						Asn1Sequence instance = Asn1Sequence.GetInstance(asn1Sequence2[i]);
						derObjectIdentifier = DerObjectIdentifier.GetInstance(instance[0]);
						derObjectIdentifier2 = DerObjectIdentifier.GetInstance(instance[1]);
					}
					catch (Exception cause2)
					{
						throw new PkixCertPathValidatorException("Policy mappings extension contents could not be decoded.", cause2, certPath, index);
					}
					if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(derObjectIdentifier.Id))
					{
						throw new PkixCertPathValidatorException("IssuerDomainPolicy is anyPolicy", null, certPath, index);
					}
					if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(derObjectIdentifier2.Id))
					{
						throw new PkixCertPathValidatorException("SubjectDomainPolicy is anyPolicy,", null, certPath, index);
					}
				}
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0007E73C File Offset: 0x0007D73C
		internal static PkixPolicyNode ProcessCertD(PkixCertPath certPath, int index, ISet acceptablePolicies, PkixPolicyNode validPolicyTree, IList[] policyNodes, int inhibitAnyPolicy)
		{
			IList certificates = certPath.Certificates;
			X509Certificate x509Certificate = (X509Certificate)certificates[index];
			int count = certificates.Count;
			int num = count - index;
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(x509Certificate, X509Extensions.CertificatePolicies));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Could not read certificate policies extension from certificate.", cause, certPath, index);
			}
			if (asn1Sequence != null && validPolicyTree != null)
			{
				ISet set = new HashSet();
				foreach (object obj in asn1Sequence)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					PolicyInformation instance = PolicyInformation.GetInstance(asn1Encodable.ToAsn1Object());
					DerObjectIdentifier policyIdentifier = instance.PolicyIdentifier;
					set.Add(policyIdentifier.Id);
					if (!Rfc3280CertPathUtilities.ANY_POLICY.Equals(policyIdentifier.Id))
					{
						ISet pq = null;
						try
						{
							pq = PkixCertPathValidatorUtilities.GetQualifierSet(instance.PolicyQualifiers);
						}
						catch (PkixCertPathValidatorException cause2)
						{
							throw new PkixCertPathValidatorException("Policy qualifier info set could not be build.", cause2, certPath, index);
						}
						if (!PkixCertPathValidatorUtilities.ProcessCertD1i(num, policyNodes, policyIdentifier, pq))
						{
							PkixCertPathValidatorUtilities.ProcessCertD1ii(num, policyNodes, policyIdentifier, pq);
						}
					}
				}
				if (acceptablePolicies.IsEmpty || acceptablePolicies.Contains(Rfc3280CertPathUtilities.ANY_POLICY))
				{
					acceptablePolicies.Clear();
					acceptablePolicies.AddAll(set);
				}
				else
				{
					ISet set2 = new HashSet();
					foreach (object o in acceptablePolicies)
					{
						if (set.Contains(o))
						{
							set2.Add(o);
						}
					}
					acceptablePolicies.Clear();
					acceptablePolicies.AddAll(set2);
				}
				if (inhibitAnyPolicy > 0 || (num < count && PkixCertPathValidatorUtilities.IsSelfIssued(x509Certificate)))
				{
					foreach (object obj2 in asn1Sequence)
					{
						Asn1Encodable asn1Encodable2 = (Asn1Encodable)obj2;
						PolicyInformation instance2 = PolicyInformation.GetInstance(asn1Encodable2.ToAsn1Object());
						if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(instance2.PolicyIdentifier.Id))
						{
							ISet qualifierSet = PkixCertPathValidatorUtilities.GetQualifierSet(instance2.PolicyQualifiers);
							IList list = policyNodes[num - 1];
							for (int i = 0; i < list.Count; i++)
							{
								PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)list[i];
								foreach (object obj3 in pkixPolicyNode.ExpectedPolicies)
								{
									string text;
									if (obj3 is string)
									{
										text = (string)obj3;
									}
									else
									{
										if (!(obj3 is DerObjectIdentifier))
										{
											continue;
										}
										text = ((DerObjectIdentifier)obj3).Id;
									}
									bool flag = false;
									foreach (object obj4 in pkixPolicyNode.Children)
									{
										PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj4;
										if (text.Equals(pkixPolicyNode2.ValidPolicy))
										{
											flag = true;
										}
									}
									if (!flag)
									{
										ISet set3 = new HashSet();
										set3.Add(text);
										PkixPolicyNode pkixPolicyNode3 = new PkixPolicyNode(new ArrayList(), num, set3, pkixPolicyNode, qualifierSet, text, false);
										pkixPolicyNode.AddChild(pkixPolicyNode3);
										policyNodes[num].Add(pkixPolicyNode3);
									}
								}
							}
							break;
						}
					}
				}
				PkixPolicyNode pkixPolicyNode4 = validPolicyTree;
				for (int j = num - 1; j >= 0; j--)
				{
					IList list2 = policyNodes[j];
					for (int k = 0; k < list2.Count; k++)
					{
						PkixPolicyNode pkixPolicyNode5 = (PkixPolicyNode)list2[k];
						if (!pkixPolicyNode5.HasChildren)
						{
							pkixPolicyNode4 = PkixCertPathValidatorUtilities.RemovePolicyNode(pkixPolicyNode4, policyNodes, pkixPolicyNode5);
							if (pkixPolicyNode4 == null)
							{
								break;
							}
						}
					}
				}
				ISet criticalExtensionOids = x509Certificate.GetCriticalExtensionOids();
				if (criticalExtensionOids != null)
				{
					bool isCritical = criticalExtensionOids.Contains(X509Extensions.CertificatePolicies.Id);
					IList list3 = policyNodes[num];
					for (int l = 0; l < list3.Count; l++)
					{
						PkixPolicyNode pkixPolicyNode6 = (PkixPolicyNode)list3[l];
						pkixPolicyNode6.IsCritical = isCritical;
					}
				}
				return pkixPolicyNode4;
			}
			return null;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0007EBD4 File Offset: 0x0007DBD4
		internal static void ProcessCrlB1(DistributionPoint dp, object cert, X509Crl crl)
		{
			Asn1Object extensionValue = PkixCertPathValidatorUtilities.GetExtensionValue(crl, X509Extensions.IssuingDistributionPoint);
			bool flag = false;
			if (extensionValue != null && IssuingDistributionPoint.GetInstance(extensionValue).IsIndirectCrl)
			{
				flag = true;
			}
			byte[] encoded = crl.IssuerDN.GetEncoded();
			bool flag2 = false;
			if (dp.CrlIssuer != null)
			{
				GeneralName[] names = dp.CrlIssuer.GetNames();
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].TagNo == 4)
					{
						try
						{
							if (Arrays.AreEqual(names[i].Name.ToAsn1Object().GetEncoded(), encoded))
							{
								flag2 = true;
							}
						}
						catch (IOException innerException)
						{
							throw new Exception("CRL issuer information from distribution point cannot be decoded.", innerException);
						}
					}
				}
				if (flag2 && !flag)
				{
					throw new Exception("Distribution point contains cRLIssuer field but CRL is not indirect.");
				}
				if (!flag2)
				{
					throw new Exception("CRL issuer of CRL does not match CRL issuer of distribution point.");
				}
			}
			else if (crl.IssuerDN.Equivalent(PkixCertPathValidatorUtilities.GetIssuerPrincipal(cert), true))
			{
				flag2 = true;
			}
			if (!flag2)
			{
				throw new Exception("Cannot find matching CRL issuer for certificate.");
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0007ECC8 File Offset: 0x0007DCC8
		internal static ReasonsMask ProcessCrlD(X509Crl crl, DistributionPoint dp)
		{
			IssuingDistributionPoint issuingDistributionPoint = null;
			try
			{
				issuingDistributionPoint = IssuingDistributionPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(crl, X509Extensions.IssuingDistributionPoint));
			}
			catch (Exception innerException)
			{
				throw new Exception("issuing distribution point extension could not be decoded.", innerException);
			}
			if (issuingDistributionPoint != null && issuingDistributionPoint.OnlySomeReasons != null && dp.Reasons != null)
			{
				return new ReasonsMask(dp.Reasons.IntValue).Intersect(new ReasonsMask(issuingDistributionPoint.OnlySomeReasons.IntValue));
			}
			if ((issuingDistributionPoint == null || issuingDistributionPoint.OnlySomeReasons == null) && dp.Reasons == null)
			{
				return ReasonsMask.AllReasons;
			}
			ReasonsMask reasonsMask;
			if (dp.Reasons == null)
			{
				reasonsMask = ReasonsMask.AllReasons;
			}
			else
			{
				reasonsMask = new ReasonsMask(dp.Reasons.IntValue);
			}
			ReasonsMask mask;
			if (issuingDistributionPoint == null)
			{
				mask = ReasonsMask.AllReasons;
			}
			else
			{
				mask = new ReasonsMask(issuingDistributionPoint.OnlySomeReasons.IntValue);
			}
			return reasonsMask.Intersect(mask);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0007EDA4 File Offset: 0x0007DDA4
		internal static ISet ProcessCrlF(X509Crl crl, object cert, X509Certificate defaultCRLSignCert, AsymmetricKeyParameter defaultCRLSignKey, PkixParameters paramsPKIX, IList certPathCerts)
		{
			X509CertStoreSelector x509CertStoreSelector = new X509CertStoreSelector();
			try
			{
				x509CertStoreSelector.Subject = crl.IssuerDN;
			}
			catch (IOException innerException)
			{
				throw new Exception("Subject criteria for certificate selector to find issuer certificate for CRL could not be set.", innerException);
			}
			ArrayList arrayList = new ArrayList();
			try
			{
				arrayList.AddRange(PkixCertPathValidatorUtilities.FindCertificates(x509CertStoreSelector, paramsPKIX.GetStores()));
				arrayList.AddRange(PkixCertPathValidatorUtilities.FindCertificates(x509CertStoreSelector, paramsPKIX.GetAdditionalStores()));
			}
			catch (Exception innerException2)
			{
				throw new Exception("Issuer certificate for CRL cannot be searched.", innerException2);
			}
			arrayList.Add(defaultCRLSignCert);
			IEnumerator enumerator = arrayList.GetEnumerator();
			IList list = new ArrayList();
			IList list2 = new ArrayList();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				X509Certificate x509Certificate = (X509Certificate)obj;
				if (x509Certificate.Equals(defaultCRLSignCert))
				{
					list.Add(x509Certificate);
					list2.Add(defaultCRLSignKey);
				}
				else
				{
					try
					{
						PkixCertPathBuilder pkixCertPathBuilder = new PkixCertPathBuilder();
						x509CertStoreSelector = new X509CertStoreSelector();
						x509CertStoreSelector.Certificate = x509Certificate;
						PkixParameters pkixParameters = (PkixParameters)paramsPKIX.Clone();
						pkixParameters.SetTargetCertConstraints(x509CertStoreSelector);
						PkixBuilderParameters instance = PkixBuilderParameters.GetInstance(pkixParameters);
						if (certPathCerts.Contains(x509Certificate))
						{
							instance.IsRevocationEnabled = false;
						}
						else
						{
							instance.IsRevocationEnabled = true;
						}
						IList certificates = pkixCertPathBuilder.Build(instance).CertPath.Certificates;
						list.Add(x509Certificate);
						list2.Add(PkixCertPathValidatorUtilities.GetNextWorkingKey(certificates, 0));
					}
					catch (PkixCertPathBuilderException innerException3)
					{
						throw new Exception("Internal error.", innerException3);
					}
					catch (PkixCertPathValidatorException innerException4)
					{
						throw new Exception("Public key of issuer certificate of CRL could not be retrieved.", innerException4);
					}
				}
			}
			ISet set = new HashSet();
			Exception ex = null;
			for (int i = 0; i < list.Count; i++)
			{
				X509Certificate x509Certificate2 = (X509Certificate)list[i];
				bool[] keyUsage = x509Certificate2.GetKeyUsage();
				if (keyUsage != null && (keyUsage.Length < 7 || !keyUsage[Rfc3280CertPathUtilities.CRL_SIGN]))
				{
					ex = new Exception("Issuer certificate key usage extension does not permit CRL signing.");
				}
				else
				{
					set.Add(list2[i]);
				}
			}
			if (set.Count == 0 && ex == null)
			{
				throw new Exception("Cannot find a valid issuer certificate.");
			}
			if (set.Count == 0 && ex != null)
			{
				throw ex;
			}
			return set;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0007EFD8 File Offset: 0x0007DFD8
		internal static AsymmetricKeyParameter ProcessCrlG(X509Crl crl, ISet keys)
		{
			Exception innerException = null;
			foreach (object obj in keys)
			{
				AsymmetricKeyParameter asymmetricKeyParameter = (AsymmetricKeyParameter)obj;
				try
				{
					crl.Verify(asymmetricKeyParameter);
					return asymmetricKeyParameter;
				}
				catch (Exception ex)
				{
					innerException = ex;
				}
			}
			throw new Exception("Cannot verify CRL.", innerException);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0007F058 File Offset: 0x0007E058
		internal static X509Crl ProcessCrlH(ISet deltaCrls, AsymmetricKeyParameter key)
		{
			Exception ex = null;
			foreach (object obj in deltaCrls)
			{
				X509Crl x509Crl = (X509Crl)obj;
				try
				{
					x509Crl.Verify(key);
					return x509Crl;
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
			}
			if (ex != null)
			{
				throw new Exception("Cannot verify delta CRL.", ex);
			}
			return null;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0007F0DC File Offset: 0x0007E0DC
		private static void CheckCrl(DistributionPoint dp, PkixParameters paramsPKIX, X509Certificate cert, DateTime validDate, X509Certificate defaultCRLSignCert, AsymmetricKeyParameter defaultCRLSignKey, CertStatus certStatus, ReasonsMask reasonMask, IList certPathCerts)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (validDate.Ticks > utcNow.Ticks)
			{
				throw new Exception("Validation time is in future.");
			}
			ISet completeCrls = PkixCertPathValidatorUtilities.GetCompleteCrls(dp, cert, utcNow, paramsPKIX);
			bool flag = false;
			Exception ex = null;
			IEnumerator enumerator = completeCrls.GetEnumerator();
			while (enumerator.MoveNext() && certStatus.Status == 11 && !reasonMask.IsAllReasons)
			{
				try
				{
					X509Crl x509Crl = (X509Crl)enumerator.Current;
					ReasonsMask reasonsMask = Rfc3280CertPathUtilities.ProcessCrlD(x509Crl, dp);
					if (reasonsMask.HasNewReasons(reasonMask))
					{
						ISet keys = Rfc3280CertPathUtilities.ProcessCrlF(x509Crl, cert, defaultCRLSignCert, defaultCRLSignKey, paramsPKIX, certPathCerts);
						AsymmetricKeyParameter key = Rfc3280CertPathUtilities.ProcessCrlG(x509Crl, keys);
						X509Crl x509Crl2 = null;
						if (paramsPKIX.IsUseDeltasEnabled)
						{
							ISet deltaCrls = PkixCertPathValidatorUtilities.GetDeltaCrls(utcNow, paramsPKIX, x509Crl);
							x509Crl2 = Rfc3280CertPathUtilities.ProcessCrlH(deltaCrls, key);
						}
						if (paramsPKIX.ValidityModel != 1 && cert.NotAfter.Ticks < x509Crl.ThisUpdate.Ticks)
						{
							throw new Exception("No valid CRL for current time found.");
						}
						Rfc3280CertPathUtilities.ProcessCrlB1(dp, cert, x509Crl);
						Rfc3280CertPathUtilities.ProcessCrlB2(dp, cert, x509Crl);
						Rfc3280CertPathUtilities.ProcessCrlC(x509Crl2, x509Crl, paramsPKIX);
						Rfc3280CertPathUtilities.ProcessCrlI(validDate, x509Crl2, cert, certStatus, paramsPKIX);
						Rfc3280CertPathUtilities.ProcessCrlJ(validDate, x509Crl, cert, certStatus);
						if (certStatus.Status == 8)
						{
							certStatus.Status = 11;
						}
						reasonMask.AddReasons(reasonsMask);
						ISet set = x509Crl.GetCriticalExtensionOids();
						if (set != null)
						{
							set = new HashSet(set);
							set.Remove(X509Extensions.IssuingDistributionPoint.Id);
							set.Remove(X509Extensions.DeltaCrlIndicator.Id);
							if (!set.IsEmpty)
							{
								throw new Exception("CRL contains unsupported critical extensions.");
							}
						}
						if (x509Crl2 != null)
						{
							set = x509Crl2.GetCriticalExtensionOids();
							if (set != null)
							{
								set = new HashSet(set);
								set.Remove(X509Extensions.IssuingDistributionPoint.Id);
								set.Remove(X509Extensions.DeltaCrlIndicator.Id);
								if (!set.IsEmpty)
								{
									throw new Exception("Delta CRL contains unsupported critical extension.");
								}
							}
						}
						flag = true;
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
			}
			if (!flag)
			{
				throw ex;
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0007F2F8 File Offset: 0x0007E2F8
		protected static void CheckCrls(PkixParameters paramsPKIX, X509Certificate cert, DateTime validDate, X509Certificate sign, AsymmetricKeyParameter workingPublicKey, IList certPathCerts)
		{
			Exception ex = null;
			CrlDistPoint crlDistPoint = null;
			try
			{
				crlDistPoint = CrlDistPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(cert, X509Extensions.CrlDistributionPoints));
			}
			catch (Exception innerException)
			{
				throw new Exception("CRL distribution point extension could not be read.", innerException);
			}
			try
			{
				PkixCertPathValidatorUtilities.AddAdditionalStoresFromCrlDistributionPoint(crlDistPoint, paramsPKIX);
			}
			catch (Exception innerException2)
			{
				throw new Exception("No additional CRL locations could be decoded from CRL distribution point extension.", innerException2);
			}
			CertStatus certStatus = new CertStatus();
			ReasonsMask reasonsMask = new ReasonsMask();
			bool flag = false;
			if (crlDistPoint != null)
			{
				DistributionPoint[] array = null;
				try
				{
					array = crlDistPoint.GetDistributionPoints();
				}
				catch (Exception innerException3)
				{
					throw new Exception("Distribution points could not be read.", innerException3);
				}
				if (array != null)
				{
					int num = 0;
					while (num < array.Length && certStatus.Status == 11 && !reasonsMask.IsAllReasons)
					{
						PkixParameters paramsPKIX2 = (PkixParameters)paramsPKIX.Clone();
						try
						{
							Rfc3280CertPathUtilities.CheckCrl(array[num], paramsPKIX2, cert, validDate, sign, workingPublicKey, certStatus, reasonsMask, certPathCerts);
							flag = true;
						}
						catch (Exception ex2)
						{
							ex = ex2;
						}
						num++;
					}
				}
			}
			if (certStatus.Status == 11 && !reasonsMask.IsAllReasons)
			{
				try
				{
					Asn1Object name = null;
					try
					{
						name = new Asn1InputStream(cert.IssuerDN.GetEncoded()).ReadObject();
					}
					catch (Exception innerException4)
					{
						throw new Exception("Issuer from certificate for CRL could not be reencoded.", innerException4);
					}
					DistributionPoint dp = new DistributionPoint(new DistributionPointName(0, new GeneralNames(new GeneralName(4, name))), null, null);
					PkixParameters paramsPKIX3 = (PkixParameters)paramsPKIX.Clone();
					Rfc3280CertPathUtilities.CheckCrl(dp, paramsPKIX3, cert, validDate, sign, workingPublicKey, certStatus, reasonsMask, certPathCerts);
					flag = true;
				}
				catch (Exception ex3)
				{
					ex = ex3;
				}
			}
			if (!flag)
			{
				throw ex;
			}
			if (certStatus.Status != 11)
			{
				string str = certStatus.RevocationDate.Value.ToString("G", CultureInfo.CreateSpecificCulture("en-us"));
				string text = "Certificate revocation after " + str;
				text = text + ", reason: " + Rfc3280CertPathUtilities.CrlReasons[certStatus.Status];
				throw new Exception(text);
			}
			if (!reasonsMask.IsAllReasons && certStatus.Status == 11)
			{
				certStatus.Status = 12;
			}
			if (certStatus.Status == 12)
			{
				throw new Exception("Certificate status could not be determined.");
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0007F53C File Offset: 0x0007E53C
		internal static PkixPolicyNode PrepareCertB(PkixCertPath certPath, int index, IList[] policyNodes, PkixPolicyNode validPolicyTree, int policyMapping)
		{
			IList certificates = certPath.Certificates;
			X509Certificate x509Certificate = (X509Certificate)certificates[index];
			int count = certificates.Count;
			int num = count - index;
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(x509Certificate, X509Extensions.PolicyMappings));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Policy mappings extension could not be decoded.", cause, certPath, index);
			}
			PkixPolicyNode pkixPolicyNode = validPolicyTree;
			if (asn1Sequence != null)
			{
				Asn1Sequence asn1Sequence2 = asn1Sequence;
				IDictionary dictionary = new Hashtable();
				ISet set = new HashSet();
				for (int i = 0; i < asn1Sequence2.Count; i++)
				{
					Asn1Sequence asn1Sequence3 = (Asn1Sequence)asn1Sequence2[i];
					string id = ((DerObjectIdentifier)asn1Sequence3[0]).Id;
					string id2 = ((DerObjectIdentifier)asn1Sequence3[1]).Id;
					if (!dictionary.Contains(id))
					{
						dictionary[id] = new HashSet
						{
							id2
						};
						set.Add(id);
					}
					else
					{
						ISet set2 = (ISet)dictionary[id];
						set2.Add(id2);
					}
				}
				foreach (object obj in set)
				{
					string text = (string)obj;
					if (policyMapping > 0)
					{
						bool flag = false;
						foreach (object obj2 in policyNodes[num])
						{
							PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj2;
							if (pkixPolicyNode2.ValidPolicy.Equals(text))
							{
								flag = true;
								pkixPolicyNode2.ExpectedPolicies = (ISet)dictionary[text];
								break;
							}
						}
						if (!flag)
						{
							foreach (object obj3 in policyNodes[num])
							{
								PkixPolicyNode pkixPolicyNode3 = (PkixPolicyNode)obj3;
								if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(pkixPolicyNode3.ValidPolicy))
								{
									ISet policyQualifiers = null;
									Asn1Sequence asn1Sequence4 = null;
									try
									{
										asn1Sequence4 = (Asn1Sequence)PkixCertPathValidatorUtilities.GetExtensionValue(x509Certificate, X509Extensions.CertificatePolicies);
									}
									catch (Exception cause2)
									{
										throw new PkixCertPathValidatorException("Certificate policies extension could not be decoded.", cause2, certPath, index);
									}
									foreach (object obj4 in asn1Sequence4)
									{
										Asn1Encodable asn1Encodable = (Asn1Encodable)obj4;
										PolicyInformation policyInformation = null;
										try
										{
											policyInformation = PolicyInformation.GetInstance(asn1Encodable.ToAsn1Object());
										}
										catch (Exception cause3)
										{
											throw new PkixCertPathValidatorException("Policy information could not be decoded.", cause3, certPath, index);
										}
										if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(policyInformation.PolicyIdentifier.Id))
										{
											try
											{
												policyQualifiers = PkixCertPathValidatorUtilities.GetQualifierSet(policyInformation.PolicyQualifiers);
												break;
											}
											catch (PkixCertPathValidatorException cause4)
											{
												throw new PkixCertPathValidatorException("Policy qualifier info set could not be decoded.", cause4, certPath, index);
											}
										}
									}
									bool critical = false;
									ISet criticalExtensionOids = x509Certificate.GetCriticalExtensionOids();
									if (criticalExtensionOids != null)
									{
										critical = criticalExtensionOids.Contains(X509Extensions.CertificatePolicies.Id);
									}
									PkixPolicyNode parent = pkixPolicyNode3.Parent;
									if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(parent.ValidPolicy))
									{
										PkixPolicyNode pkixPolicyNode4 = new PkixPolicyNode(new ArrayList(), num, (ISet)dictionary[text], parent, policyQualifiers, text, critical);
										parent.AddChild(pkixPolicyNode4);
										policyNodes[num].Add(pkixPolicyNode4);
										break;
									}
									break;
								}
							}
						}
					}
					else if (policyMapping <= 0)
					{
						foreach (object obj5 in new ArrayList(policyNodes[num]))
						{
							PkixPolicyNode pkixPolicyNode5 = (PkixPolicyNode)obj5;
							if (pkixPolicyNode5.ValidPolicy.Equals(text))
							{
								PkixPolicyNode parent2 = pkixPolicyNode5.Parent;
								parent2.RemoveChild(pkixPolicyNode5);
								for (int j = num - 1; j >= 0; j--)
								{
									IList list = policyNodes[j];
									for (int k = 0; k < list.Count; k++)
									{
										PkixPolicyNode pkixPolicyNode6 = (PkixPolicyNode)list[k];
										if (!pkixPolicyNode6.HasChildren)
										{
											pkixPolicyNode = PkixCertPathValidatorUtilities.RemovePolicyNode(pkixPolicyNode, policyNodes, pkixPolicyNode6);
											if (pkixPolicyNode == null)
											{
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return pkixPolicyNode;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0007F944 File Offset: 0x0007E944
		internal static ISet[] ProcessCrlA1ii(DateTime currentDate, PkixParameters paramsPKIX, X509Certificate cert, X509Crl crl)
		{
			ISet set = new HashSet();
			ISet set2 = new HashSet();
			X509CrlStoreSelector x509CrlStoreSelector = new X509CrlStoreSelector();
			x509CrlStoreSelector.CertificateChecking = cert;
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
					crl.IssuerDN
				};
			}
			catch (IOException ex)
			{
				throw new Exception("Cannot extract issuer from CRL." + ex, ex);
			}
			x509CrlStoreSelector.CompleteCrlEnabled = true;
			try
			{
				set.AddAll(PkixCertPathValidatorUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX.GetAdditionalStores()));
				set.AddAll(PkixCertPathValidatorUtilities.FindCrls(x509CrlStoreSelector, paramsPKIX.GetStores()));
			}
			catch (Exception innerException)
			{
				throw new Exception("Exception obtaining complete CRLs.", innerException);
			}
			if (paramsPKIX.IsUseDeltasEnabled)
			{
				try
				{
					set2.AddAll(PkixCertPathValidatorUtilities.GetDeltaCrls(currentDate, paramsPKIX, crl));
				}
				catch (Exception innerException2)
				{
					throw new Exception("Exception obtaining delta CRLs.", innerException2);
				}
			}
			return new ISet[]
			{
				set,
				set2
			};
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0007FA60 File Offset: 0x0007EA60
		internal static ISet ProcessCrlA1i(DateTime currentDate, PkixParameters paramsPKIX, X509Certificate cert, X509Crl crl)
		{
			ISet set = new HashSet();
			if (paramsPKIX.IsUseDeltasEnabled)
			{
				CrlDistPoint crlDistPoint = null;
				try
				{
					crlDistPoint = CrlDistPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(cert, X509Extensions.FreshestCrl));
				}
				catch (Exception innerException)
				{
					throw new Exception("Freshest CRL extension could not be decoded from certificate.", innerException);
				}
				if (crlDistPoint == null)
				{
					try
					{
						crlDistPoint = CrlDistPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(crl, X509Extensions.FreshestCrl));
					}
					catch (Exception innerException2)
					{
						throw new Exception("Freshest CRL extension could not be decoded from CRL.", innerException2);
					}
				}
				if (crlDistPoint != null)
				{
					try
					{
						PkixCertPathValidatorUtilities.AddAdditionalStoresFromCrlDistributionPoint(crlDistPoint, paramsPKIX);
					}
					catch (Exception innerException3)
					{
						throw new Exception("No new delta CRL locations could be added from Freshest CRL extension.", innerException3);
					}
					try
					{
						set.AddAll(PkixCertPathValidatorUtilities.GetDeltaCrls(currentDate, paramsPKIX, crl));
					}
					catch (Exception innerException4)
					{
						throw new Exception("Exception obtaining delta CRLs.", innerException4);
					}
				}
			}
			return set;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0007FB30 File Offset: 0x0007EB30
		internal static void ProcessCertF(PkixCertPath certPath, int index, PkixPolicyNode validPolicyTree, int explicitPolicy)
		{
			if (explicitPolicy <= 0 && validPolicyTree == null)
			{
				throw new PkixCertPathValidatorException("No valid policy tree found when one expected.", null, certPath, index);
			}
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0007FB48 File Offset: 0x0007EB48
		internal static void ProcessCertA(PkixCertPath certPath, PkixParameters paramsPKIX, int index, AsymmetricKeyParameter workingPublicKey, X509Name workingIssuerName, X509Certificate sign)
		{
			IList certificates = certPath.Certificates;
			X509Certificate x509Certificate = (X509Certificate)certificates[index];
			try
			{
				x509Certificate.Verify(workingPublicKey);
			}
			catch (GeneralSecurityException cause)
			{
				throw new PkixCertPathValidatorException("Could not validate certificate signature.", cause, certPath, index);
			}
			try
			{
				x509Certificate.CheckValidity(PkixCertPathValidatorUtilities.GetValidCertDateFromValidityModel(paramsPKIX, certPath, index));
			}
			catch (CertificateExpiredException ex)
			{
				throw new PkixCertPathValidatorException("Could not validate certificate: " + ex.Message, ex, certPath, index);
			}
			catch (CertificateNotYetValidException ex2)
			{
				throw new PkixCertPathValidatorException("Could not validate certificate: " + ex2.Message, ex2, certPath, index);
			}
			catch (Exception cause2)
			{
				throw new PkixCertPathValidatorException("Could not validate time of certificate.", cause2, certPath, index);
			}
			if (paramsPKIX.IsRevocationEnabled)
			{
				try
				{
					Rfc3280CertPathUtilities.CheckCrls(paramsPKIX, x509Certificate, PkixCertPathValidatorUtilities.GetValidCertDateFromValidityModel(paramsPKIX, certPath, index), sign, workingPublicKey, certificates);
				}
				catch (Exception ex3)
				{
					Exception ex4 = ex3.InnerException;
					if (ex4 == null)
					{
						ex4 = ex3;
					}
					throw new PkixCertPathValidatorException(ex3.Message, ex4, certPath, index);
				}
			}
			X509Name issuerPrincipal = PkixCertPathValidatorUtilities.GetIssuerPrincipal(x509Certificate);
			if (!issuerPrincipal.Equivalent(workingIssuerName, true))
			{
				throw new PkixCertPathValidatorException(string.Concat(new object[]
				{
					"IssuerName(",
					issuerPrincipal,
					") does not match SubjectName(",
					workingIssuerName,
					") of signing certificate."
				}), null, certPath, index);
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0007FCB0 File Offset: 0x0007ECB0
		internal static int PrepareNextCertI1(PkixCertPath certPath, int index, int explicitPolicy)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.PolicyConstraints));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Policy constraints extension cannot be decoded.", cause, certPath, index);
			}
			if (asn1Sequence != null)
			{
				IEnumerator enumerator = asn1Sequence.GetEnumerator();
				while (enumerator.MoveNext())
				{
					try
					{
						Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(enumerator.Current);
						if (instance.TagNo == 0)
						{
							int intValue = DerInteger.GetInstance(instance).Value.IntValue;
							if (intValue < explicitPolicy)
							{
								return intValue;
							}
							break;
						}
					}
					catch (ArgumentException cause2)
					{
						throw new PkixCertPathValidatorException("Policy constraints extension contents cannot be decoded.", cause2, certPath, index);
					}
				}
			}
			return explicitPolicy;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0007FD78 File Offset: 0x0007ED78
		internal static int PrepareNextCertI2(PkixCertPath certPath, int index, int policyMapping)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.PolicyConstraints));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Policy constraints extension cannot be decoded.", cause, certPath, index);
			}
			if (asn1Sequence != null)
			{
				IEnumerator enumerator = asn1Sequence.GetEnumerator();
				while (enumerator.MoveNext())
				{
					try
					{
						Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(enumerator.Current);
						if (instance.TagNo == 1)
						{
							int intValue = DerInteger.GetInstance(instance).Value.IntValue;
							if (intValue < policyMapping)
							{
								return intValue;
							}
							break;
						}
					}
					catch (ArgumentException cause2)
					{
						throw new PkixCertPathValidatorException("Policy constraints extension contents cannot be decoded.", cause2, certPath, index);
					}
				}
			}
			return policyMapping;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0007FE40 File Offset: 0x0007EE40
		internal static void PrepareNextCertG(PkixCertPath certPath, int index, PkixNameConstraintValidator nameConstraintValidator)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			NameConstraints nameConstraints = null;
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.NameConstraints));
				if (instance != null)
				{
					nameConstraints = new NameConstraints(instance);
				}
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Name constraints extension could not be decoded.", cause, certPath, index);
			}
			if (nameConstraints != null)
			{
				Asn1Sequence permittedSubtrees = nameConstraints.PermittedSubtrees;
				if (permittedSubtrees != null)
				{
					try
					{
						nameConstraintValidator.IntersectPermittedSubtree(permittedSubtrees);
					}
					catch (Exception cause2)
					{
						throw new PkixCertPathValidatorException("Permitted subtrees cannot be build from name constraints extension.", cause2, certPath, index);
					}
				}
				Asn1Sequence excludedSubtrees = nameConstraints.ExcludedSubtrees;
				if (excludedSubtrees != null)
				{
					IEnumerator enumerator = excludedSubtrees.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							GeneralSubtree instance2 = GeneralSubtree.GetInstance(obj);
							nameConstraintValidator.AddExcludedSubtree(instance2);
						}
					}
					catch (Exception cause3)
					{
						throw new PkixCertPathValidatorException("Excluded subtrees cannot be build from name constraints extension.", cause3, certPath, index);
					}
				}
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0007FF2C File Offset: 0x0007EF2C
		internal static int PrepareNextCertJ(PkixCertPath certPath, int index, int inhibitAnyPolicy)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			DerInteger derInteger = null;
			try
			{
				derInteger = DerInteger.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.InhibitAnyPolicy));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Inhibit any-policy extension cannot be decoded.", cause, certPath, index);
			}
			if (derInteger != null)
			{
				int intValue = derInteger.Value.IntValue;
				if (intValue < inhibitAnyPolicy)
				{
					return intValue;
				}
			}
			return inhibitAnyPolicy;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0007FF9C File Offset: 0x0007EF9C
		internal static void PrepareNextCertK(PkixCertPath certPath, int index)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			BasicConstraints basicConstraints = null;
			try
			{
				basicConstraints = BasicConstraints.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.BasicConstraints));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Basic constraints extension cannot be decoded.", cause, certPath, index);
			}
			if (basicConstraints == null)
			{
				throw new PkixCertPathValidatorException("Intermediate certificate lacks BasicConstraints");
			}
			if (!basicConstraints.IsCA())
			{
				throw new PkixCertPathValidatorException("Not a CA certificate");
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00080014 File Offset: 0x0007F014
		internal static int PrepareNextCertL(PkixCertPath certPath, int index, int maxPathLength)
		{
			IList certificates = certPath.Certificates;
			X509Certificate cert = (X509Certificate)certificates[index];
			if (PkixCertPathValidatorUtilities.IsSelfIssued(cert))
			{
				return maxPathLength;
			}
			if (maxPathLength <= 0)
			{
				throw new PkixCertPathValidatorException("Max path length not greater than zero", null, certPath, index);
			}
			return maxPathLength - 1;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00080054 File Offset: 0x0007F054
		internal static int PrepareNextCertM(PkixCertPath certPath, int index, int maxPathLength)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			BasicConstraints basicConstraints = null;
			try
			{
				basicConstraints = BasicConstraints.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.BasicConstraints));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Basic constraints extension cannot be decoded.", cause, certPath, index);
			}
			if (basicConstraints != null)
			{
				BigInteger pathLenConstraint = basicConstraints.PathLenConstraint;
				if (pathLenConstraint != null)
				{
					int intValue = pathLenConstraint.IntValue;
					if (intValue < maxPathLength)
					{
						return intValue;
					}
				}
			}
			return maxPathLength;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000800CC File Offset: 0x0007F0CC
		internal static void PrepareNextCertN(PkixCertPath certPath, int index)
		{
			IList certificates = certPath.Certificates;
			X509Certificate x509Certificate = (X509Certificate)certificates[index];
			bool[] keyUsage = x509Certificate.GetKeyUsage();
			if (keyUsage != null && !keyUsage[Rfc3280CertPathUtilities.KEY_CERT_SIGN])
			{
				throw new PkixCertPathValidatorException("Issuer certificate keyusage extension is critical and does not permit key signing.", null, certPath, index);
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00080110 File Offset: 0x0007F110
		internal static void PrepareNextCertO(PkixCertPath certPath, int index, ISet criticalExtensions, IList pathCheckers)
		{
			IList certificates = certPath.Certificates;
			X509Certificate cert = (X509Certificate)certificates[index];
			IEnumerator enumerator = pathCheckers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					((PkixCertPathChecker)enumerator.Current).Check(cert, criticalExtensions);
				}
				catch (PkixCertPathValidatorException ex)
				{
					throw new PkixCertPathValidatorException(ex.Message, ex.InnerException, certPath, index);
				}
			}
			if (!criticalExtensions.IsEmpty)
			{
				throw new PkixCertPathValidatorException("Certificate has unsupported critical extension.", null, certPath, index);
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00080194 File Offset: 0x0007F194
		internal static int PrepareNextCertH1(PkixCertPath certPath, int index, int explicitPolicy)
		{
			IList certificates = certPath.Certificates;
			X509Certificate cert = (X509Certificate)certificates[index];
			if (!PkixCertPathValidatorUtilities.IsSelfIssued(cert) && explicitPolicy != 0)
			{
				return explicitPolicy - 1;
			}
			return explicitPolicy;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000801C8 File Offset: 0x0007F1C8
		internal static int PrepareNextCertH2(PkixCertPath certPath, int index, int policyMapping)
		{
			IList certificates = certPath.Certificates;
			X509Certificate cert = (X509Certificate)certificates[index];
			if (!PkixCertPathValidatorUtilities.IsSelfIssued(cert) && policyMapping != 0)
			{
				return policyMapping - 1;
			}
			return policyMapping;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000801FC File Offset: 0x0007F1FC
		internal static int PrepareNextCertH3(PkixCertPath certPath, int index, int inhibitAnyPolicy)
		{
			IList certificates = certPath.Certificates;
			X509Certificate cert = (X509Certificate)certificates[index];
			if (!PkixCertPathValidatorUtilities.IsSelfIssued(cert) && inhibitAnyPolicy != 0)
			{
				return inhibitAnyPolicy - 1;
			}
			return inhibitAnyPolicy;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0008022D File Offset: 0x0007F22D
		internal static int WrapupCertA(int explicitPolicy, X509Certificate cert)
		{
			if (!PkixCertPathValidatorUtilities.IsSelfIssued(cert) && explicitPolicy != 0)
			{
				explicitPolicy--;
			}
			return explicitPolicy;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00080240 File Offset: 0x0007F240
		internal static int WrapupCertB(PkixCertPath certPath, int index, int explicitPolicy)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.PolicyConstraints));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Policy constraints could no be decoded.", cause, certPath, index);
			}
			if (asn1Sequence != null)
			{
				foreach (object obj in asn1Sequence)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
					int tagNo = asn1TaggedObject.TagNo;
					if (tagNo == 0)
					{
						int intValue;
						try
						{
							intValue = DerInteger.GetInstance(asn1TaggedObject).Value.IntValue;
						}
						catch (Exception cause2)
						{
							throw new PkixCertPathValidatorException("Policy constraints requireExplicitPolicy field could no be decoded.", cause2, certPath, index);
						}
						if (intValue == 0)
						{
							return 0;
						}
					}
				}
			}
			return explicitPolicy;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00080300 File Offset: 0x0007F300
		internal static void WrapupCertF(PkixCertPath certPath, int index, IList pathCheckers, ISet criticalExtensions)
		{
			IList certificates = certPath.Certificates;
			X509Certificate cert = (X509Certificate)certificates[index];
			IEnumerator enumerator = pathCheckers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					((PkixCertPathChecker)enumerator.Current).Check(cert, criticalExtensions);
				}
				catch (PkixCertPathValidatorException cause)
				{
					throw new PkixCertPathValidatorException("Additional certificate path checker failed.", cause, certPath, index);
				}
			}
			if (!criticalExtensions.IsEmpty)
			{
				throw new PkixCertPathValidatorException("Certificate has unsupported critical extension", null, certPath, index);
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0008037C File Offset: 0x0007F37C
		internal static PkixPolicyNode WrapupCertG(PkixCertPath certPath, PkixParameters paramsPKIX, ISet userInitialPolicySet, int index, IList[] policyNodes, PkixPolicyNode validPolicyTree, ISet acceptablePolicies)
		{
			int count = certPath.Certificates.Count;
			PkixPolicyNode result;
			if (validPolicyTree == null)
			{
				if (paramsPKIX.IsExplicitPolicyRequired)
				{
					throw new PkixCertPathValidatorException("Explicit policy requested but none available.", null, certPath, index);
				}
				result = null;
			}
			else if (PkixCertPathValidatorUtilities.IsAnyPolicy(userInitialPolicySet))
			{
				if (paramsPKIX.IsExplicitPolicyRequired)
				{
					if (acceptablePolicies.IsEmpty)
					{
						throw new PkixCertPathValidatorException("Explicit policy requested but none available.", null, certPath, index);
					}
					ISet set = new HashSet();
					foreach (IList list in policyNodes)
					{
						for (int j = 0; j < list.Count; j++)
						{
							PkixPolicyNode pkixPolicyNode = (PkixPolicyNode)list[j];
							if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(pkixPolicyNode.ValidPolicy))
							{
								foreach (object o in pkixPolicyNode.Children)
								{
									set.Add(o);
								}
							}
						}
					}
					foreach (object obj in set)
					{
						PkixPolicyNode pkixPolicyNode2 = (PkixPolicyNode)obj;
						string validPolicy = pkixPolicyNode2.ValidPolicy;
						acceptablePolicies.Contains(validPolicy);
					}
					if (validPolicyTree != null)
					{
						for (int k = count - 1; k >= 0; k--)
						{
							IList list2 = policyNodes[k];
							for (int l = 0; l < list2.Count; l++)
							{
								PkixPolicyNode pkixPolicyNode3 = (PkixPolicyNode)list2[l];
								if (!pkixPolicyNode3.HasChildren)
								{
									validPolicyTree = PkixCertPathValidatorUtilities.RemovePolicyNode(validPolicyTree, policyNodes, pkixPolicyNode3);
								}
							}
						}
					}
				}
				result = validPolicyTree;
			}
			else
			{
				ISet set2 = new HashSet();
				foreach (IList list3 in policyNodes)
				{
					for (int n = 0; n < list3.Count; n++)
					{
						PkixPolicyNode pkixPolicyNode4 = (PkixPolicyNode)list3[n];
						if (Rfc3280CertPathUtilities.ANY_POLICY.Equals(pkixPolicyNode4.ValidPolicy))
						{
							foreach (object obj2 in pkixPolicyNode4.Children)
							{
								PkixPolicyNode pkixPolicyNode5 = (PkixPolicyNode)obj2;
								if (!Rfc3280CertPathUtilities.ANY_POLICY.Equals(pkixPolicyNode5.ValidPolicy))
								{
									set2.Add(pkixPolicyNode5);
								}
							}
						}
					}
				}
				foreach (object obj3 in set2)
				{
					PkixPolicyNode pkixPolicyNode6 = (PkixPolicyNode)obj3;
					string validPolicy2 = pkixPolicyNode6.ValidPolicy;
					if (!userInitialPolicySet.Contains(validPolicy2))
					{
						validPolicyTree = PkixCertPathValidatorUtilities.RemovePolicyNode(validPolicyTree, policyNodes, pkixPolicyNode6);
					}
				}
				if (validPolicyTree != null)
				{
					for (int num = count - 1; num >= 0; num--)
					{
						IList list4 = policyNodes[num];
						for (int num2 = 0; num2 < list4.Count; num2++)
						{
							PkixPolicyNode pkixPolicyNode7 = (PkixPolicyNode)list4[num2];
							if (!pkixPolicyNode7.HasChildren)
							{
								validPolicyTree = PkixCertPathValidatorUtilities.RemovePolicyNode(validPolicyTree, policyNodes, pkixPolicyNode7);
							}
						}
					}
				}
				result = validPolicyTree;
			}
			return result;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000806B4 File Offset: 0x0007F6B4
		internal static void ProcessCrlC(X509Crl deltaCRL, X509Crl completeCRL, PkixParameters pkixParams)
		{
			if (deltaCRL == null)
			{
				return;
			}
			IssuingDistributionPoint objA = null;
			try
			{
				objA = IssuingDistributionPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(completeCRL, X509Extensions.IssuingDistributionPoint));
			}
			catch (Exception innerException)
			{
				throw new Exception("000 Issuing distribution point extension could not be decoded.", innerException);
			}
			if (pkixParams.IsUseDeltasEnabled)
			{
				if (!deltaCRL.IssuerDN.Equivalent(completeCRL.IssuerDN, true))
				{
					throw new Exception("Complete CRL issuer does not match delta CRL issuer.");
				}
				IssuingDistributionPoint objB = null;
				try
				{
					objB = IssuingDistributionPoint.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(deltaCRL, X509Extensions.IssuingDistributionPoint));
				}
				catch (Exception innerException2)
				{
					throw new Exception("Issuing distribution point extension from delta CRL could not be decoded.", innerException2);
				}
				if (!object.Equals(objA, objB))
				{
					throw new Exception("Issuing distribution point extension from delta CRL and complete CRL does not match.");
				}
				Asn1Object asn1Object = null;
				try
				{
					asn1Object = PkixCertPathValidatorUtilities.GetExtensionValue(completeCRL, X509Extensions.AuthorityKeyIdentifier);
				}
				catch (Exception innerException3)
				{
					throw new Exception("Authority key identifier extension could not be extracted from complete CRL.", innerException3);
				}
				Asn1Object asn1Object2 = null;
				try
				{
					asn1Object2 = PkixCertPathValidatorUtilities.GetExtensionValue(deltaCRL, X509Extensions.AuthorityKeyIdentifier);
				}
				catch (Exception innerException4)
				{
					throw new Exception("Authority key identifier extension could not be extracted from delta CRL.", innerException4);
				}
				if (asn1Object == null)
				{
					throw new Exception("CRL authority key identifier is null.");
				}
				if (asn1Object2 == null)
				{
					throw new Exception("Delta CRL authority key identifier is null.");
				}
				if (!asn1Object.Equals(asn1Object2))
				{
					throw new Exception("Delta CRL authority key identifier does not match complete CRL authority key identifier.");
				}
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000807F4 File Offset: 0x0007F7F4
		internal static void ProcessCrlI(DateTime validDate, X509Crl deltacrl, object cert, CertStatus certStatus, PkixParameters pkixParams)
		{
			if (pkixParams.IsUseDeltasEnabled && deltacrl != null)
			{
				PkixCertPathValidatorUtilities.GetCertStatus(validDate, deltacrl, cert, certStatus);
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0008080B File Offset: 0x0007F80B
		internal static void ProcessCrlJ(DateTime validDate, X509Crl completecrl, object cert, CertStatus certStatus)
		{
			if (certStatus.Status == 11)
			{
				PkixCertPathValidatorUtilities.GetCertStatus(validDate, completecrl, cert, certStatus);
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00080820 File Offset: 0x0007F820
		internal static PkixPolicyNode ProcessCertE(PkixCertPath certPath, int index, PkixPolicyNode validPolicyTree)
		{
			IList certificates = certPath.Certificates;
			X509Certificate ext = (X509Certificate)certificates[index];
			Asn1Sequence asn1Sequence = null;
			try
			{
				asn1Sequence = Asn1Sequence.GetInstance(PkixCertPathValidatorUtilities.GetExtensionValue(ext, X509Extensions.CertificatePolicies));
			}
			catch (Exception cause)
			{
				throw new PkixCertPathValidatorException("Could not read certificate policies extension from certificate.", cause, certPath, index);
			}
			if (asn1Sequence == null)
			{
				validPolicyTree = null;
			}
			return validPolicyTree;
		}

		// Token: 0x04000F36 RID: 3894
		internal static readonly string ANY_POLICY = "2.5.29.32.0";

		// Token: 0x04000F37 RID: 3895
		internal static readonly int KEY_CERT_SIGN = 5;

		// Token: 0x04000F38 RID: 3896
		internal static readonly int CRL_SIGN = 6;

		// Token: 0x04000F39 RID: 3897
		internal static readonly string[] CrlReasons = new string[]
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

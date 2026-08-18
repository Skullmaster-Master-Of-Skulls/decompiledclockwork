using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000053 RID: 83
	internal static class SignedXmlDebugLog
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000DE4F File Offset: 0x0000C04F
		private static bool InformationLoggingEnabled
		{
			get
			{
				if (!SignedXmlDebugLog.s_haveInformationLogging)
				{
					SignedXmlDebugLog.s_informationLogging = SignedXmlDebugLog.s_traceSource.Switch.ShouldTrace(TraceEventType.Information);
					SignedXmlDebugLog.s_haveInformationLogging = true;
				}
				return SignedXmlDebugLog.s_informationLogging;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000DE80 File Offset: 0x0000C080
		private static bool VerboseLoggingEnabled
		{
			get
			{
				if (!SignedXmlDebugLog.s_haveVerboseLogging)
				{
					SignedXmlDebugLog.s_verboseLogging = SignedXmlDebugLog.s_traceSource.Switch.ShouldTrace(TraceEventType.Verbose);
					SignedXmlDebugLog.s_haveVerboseLogging = true;
				}
				return SignedXmlDebugLog.s_verboseLogging;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		private static string FormatBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return "(null)";
			}
			StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000DF08 File Offset: 0x0000C108
		private static string GetKeyName(object key)
		{
			ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = key as ICspAsymmetricAlgorithm;
			X509Certificate x509Certificate = key as X509Certificate;
			X509Certificate2 x509Certificate2 = key as X509Certificate2;
			string text;
			if (cspAsymmetricAlgorithm != null && cspAsymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
				{
					cspAsymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName
				});
			}
			else if (x509Certificate2 != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
				{
					x509Certificate2.GetNameInfo(X509NameType.SimpleName, false)
				});
			}
			else if (x509Certificate != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
				{
					x509Certificate.Subject
				});
			}
			else
			{
				text = key.GetHashCode().ToString("x8", CultureInfo.InvariantCulture);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", new object[]
			{
				key.GetType().Name,
				text
			});
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		private static string GetObjectId(object o)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", new object[]
			{
				o.GetType().Name,
				o.GetHashCode().ToString("x8", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E03C File Offset: 0x0000C23C
		private static string GetOidName(Oid oid)
		{
			string text = oid.FriendlyName;
			if (string.IsNullOrEmpty(text))
			{
				text = oid.Value;
			}
			return text;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E060 File Offset: 0x0000C260
		internal static void LogBeginCanonicalization(SignedXml signedXml, Transform canonicalizationTransform)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_BeginCanonicalization"), new object[]
				{
					canonicalizationTransform.Algorithm,
					canonicalizationTransform.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCanonicalization, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_CanonicalizationSettings"), new object[]
				{
					canonicalizationTransform.Resolver.GetType(),
					canonicalizationTransform.BaseURI
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCanonicalization, data2);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		internal static void LogBeginCheckSignatureFormat(SignedXml signedXml, Func<SignedXml, bool> formatValidator)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				MethodInfo method = formatValidator.Method;
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_CheckSignatureFormat"), new object[]
				{
					method.Module.Assembly.FullName,
					method.DeclaringType.FullName,
					method.Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCheckSignatureFormat, data);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E160 File Offset: 0x0000C360
		internal static void LogBeginCheckSignedInfo(SignedXml signedXml, SignedInfo signedInfo)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_CheckSignedInfo"), new object[]
				{
					(signedInfo.Id != null) ? signedInfo.Id : "(null)"
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCheckSignedInfo, data);
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		internal static void LogBeginSignatureComputation(SignedXml signedXml, XmlElement context)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureComputation, SecurityResources.GetResourceString("Log_BeginSignatureComputation"));
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_XmlContext"), new object[]
				{
					(context != null) ? context.OuterXml : "(null)"
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureComputation, data);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E218 File Offset: 0x0000C418
		internal static void LogBeginSignatureVerification(SignedXml signedXml, XmlElement context)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureVerification, SecurityResources.GetResourceString("Log_BeginSignatureVerification"));
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_XmlContext"), new object[]
				{
					(context != null) ? context.OuterXml : "(null)"
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureVerification, data);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E280 File Offset: 0x0000C480
		internal static void LogCanonicalizedOutput(SignedXml signedXml, Transform canonicalizationTransform)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				using (StreamReader streamReader = new StreamReader(canonicalizationTransform.GetOutput(typeof(Stream)) as Stream))
				{
					string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_CanonicalizedOutput"), new object[]
					{
						streamReader.ReadToEnd()
					});
					SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.CanonicalizedData, data);
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		internal static void LogFormatValidationResult(SignedXml signedXml, bool result)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = result ? SecurityResources.GetResourceString("Log_FormatValidationSuccessful") : SecurityResources.GetResourceString("Log_FormatValidationNotSuccessful");
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.FormatValidationResult, data);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E334 File Offset: 0x0000C534
		internal static void LogUnsafeCanonicalizationMethod(SignedXml signedXml, string algorithm, IEnumerable<string> validAlgorithms)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string arg in validAlgorithms)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("\"{0}\"", arg);
				}
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_UnsafeCanonicalizationMethod"), new object[]
				{
					algorithm,
					stringBuilder.ToString()
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.UnsafeCanonicalizationMethod, data);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		internal static void LogUnsafeTransformMethod(SignedXml signedXml, string algorithm, IEnumerable<string> validC14nAlgorithms, IEnumerable<string> validTransformAlgorithms)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string arg in validC14nAlgorithms)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("\"{0}\"", arg);
				}
				foreach (string arg2 in validTransformAlgorithms)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("\"{0}\"", arg2);
				}
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_UnsafeTransformMethod"), new object[]
				{
					algorithm,
					stringBuilder.ToString()
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.UnsafeTransformMethod, data);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		internal static void LogNamespacePropagation(SignedXml signedXml, XmlNodeList namespaces)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				if (namespaces != null)
				{
					using (IEnumerator enumerator = namespaces.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_PropagatingNamespace"), new object[]
							{
								xmlAttribute.Name,
								xmlAttribute.Value
							});
							SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.NamespacePropagation, data);
						}
						return;
					}
				}
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.NamespacePropagation, SecurityResources.GetResourceString("Log_NoNamespacesPropagated"));
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E574 File Offset: 0x0000C774
		internal static Stream LogReferenceData(Reference reference, Stream data)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = new byte[4096];
				int num;
				do
				{
					num = data.Read(array, 0, array.Length);
					memoryStream.Write(array, 0, num);
				}
				while (num == array.Length);
				string data2 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_TransformedReferenceContents"), new object[]
				{
					Encoding.UTF8.GetString(memoryStream.ToArray())
				});
				SignedXmlDebugLog.WriteLine(reference, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.ReferenceData, data2);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}
			return data;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		internal static void LogSigning(SignedXml signedXml, object key, SignatureDescription signatureDescription, HashAlgorithm hash, AsymmetricSignatureFormatter asymmetricSignatureFormatter)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_SigningAsymmetric"), new object[]
				{
					SignedXmlDebugLog.GetKeyName(key),
					signatureDescription.GetType().Name,
					hash.GetType().Name,
					asymmetricSignatureFormatter.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.Signing, data);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E66C File Offset: 0x0000C86C
		internal static void LogSigning(SignedXml signedXml, KeyedHashAlgorithm key)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_SigningHmac"), new object[]
				{
					key.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.Signing, data);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		internal static void LogSigningReference(SignedXml signedXml, Reference reference)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = Utils.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = (hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name;
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_SigningReference"), new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.Uri,
					reference.Id,
					reference.Type,
					reference.DigestMethod,
					text
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.SigningReference, data);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E744 File Offset: 0x0000C944
		internal static void LogVerificationFailure(SignedXml signedXml, string failureLocation)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_VerificationFailed"), new object[]
				{
					failureLocation
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerificationFailure, data);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E784 File Offset: 0x0000C984
		internal static void LogVerificationResult(SignedXml signedXml, object key, bool verified)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string format = verified ? SecurityResources.GetResourceString("Log_VerificationWithKeySuccessful") : SecurityResources.GetResourceString("Log_VerificationWithKeyNotSuccessful");
				string data = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					SignedXmlDebugLog.GetKeyName(key)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.SignatureVerificationResult, data);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		internal static void LogVerifyKeyUsage(SignedXml signedXml, X509Certificate certificate, X509KeyUsageExtension keyUsages)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_KeyUsages"), new object[]
				{
					keyUsages.KeyUsages,
					SignedXmlDebugLog.GetOidName(keyUsages.Oid),
					SignedXmlDebugLog.GetKeyName(certificate)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E838 File Offset: 0x0000CA38
		internal static void LogVerifyReference(SignedXml signedXml, Reference reference)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_VerifyReference"), new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.Uri,
					reference.Id,
					reference.Type
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifyReference, data);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E898 File Offset: 0x0000CA98
		internal static void LogVerifyReferenceHash(SignedXml signedXml, Reference reference, byte[] actualHash, byte[] expectedHash)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = Utils.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = (hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name;
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_ReferenceHash"), new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.DigestMethod,
					text,
					SignedXmlDebugLog.FormatBytes(actualHash),
					SignedXmlDebugLog.FormatBytes(expectedHash)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifyReference, data);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E91C File Offset: 0x0000CB1C
		internal static void LogVerifySignedInfo(SignedXml signedXml, AsymmetricAlgorithm key, SignatureDescription signatureDescription, HashAlgorithm hashAlgorithm, AsymmetricSignatureDeformatter asymmetricSignatureDeformatter, byte[] actualHashValue, byte[] signatureValue)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_VerifySignedInfoAsymmetric"), new object[]
				{
					SignedXmlDebugLog.GetKeyName(key),
					signatureDescription.GetType().Name,
					hashAlgorithm.GetType().Name,
					asymmetricSignatureDeformatter.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_ActualHashValue"), new object[]
				{
					SignedXmlDebugLog.FormatBytes(actualHashValue)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_RawSignatureValue"), new object[]
				{
					SignedXmlDebugLog.FormatBytes(signatureValue)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data3);
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		internal static void LogVerifySignedInfo(SignedXml signedXml, KeyedHashAlgorithm mac, byte[] actualHashValue, byte[] signatureValue)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_VerifySignedInfoHmac"), new object[]
				{
					mac.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_ActualHashValue"), new object[]
				{
					SignedXmlDebugLog.FormatBytes(actualHashValue)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_RawSignatureValue"), new object[]
				{
					SignedXmlDebugLog.FormatBytes(signatureValue)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data3);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		internal static void LogVerifyX509Chain(SignedXml signedXml, X509Chain chain, X509Certificate certificate)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_BuildX509Chain"), new object[]
				{
					SignedXmlDebugLog.GetKeyName(certificate)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_RevocationMode"), new object[]
				{
					chain.ChainPolicy.RevocationFlag
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_RevocationFlag"), new object[]
				{
					chain.ChainPolicy.RevocationFlag
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data3);
				string data4 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_VerificationFlag"), new object[]
				{
					chain.ChainPolicy.VerificationFlags
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data4);
				string data5 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_VerificationTime"), new object[]
				{
					chain.ChainPolicy.VerificationTime
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data5);
				string data6 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_UrlTimeout"), new object[]
				{
					chain.ChainPolicy.UrlRetrievalTimeout
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data6);
			}
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if (x509ChainStatus.Status != X509ChainStatusFlags.NoError)
					{
						string data7 = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_X509ChainError"), new object[]
						{
							x509ChainStatus.Status,
							x509ChainStatus.StatusInformation
						});
						SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data7);
					}
				}
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(SecurityResources.GetResourceString("Log_CertificateChain"));
				foreach (X509ChainElement x509ChainElement in chain.ChainElements)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", new object[]
					{
						SignedXmlDebugLog.GetKeyName(x509ChainElement.Certificate)
					});
				}
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, stringBuilder.ToString());
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		internal static void LogSignedXmlRecursionLimit(SignedXml signedXml, Reference reference)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = Utils.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = (hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name;
				string data = string.Format(CultureInfo.InvariantCulture, SecurityResources.GetResourceString("Log_SignedXmlRecursionLimit"), new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.DigestMethod,
					text
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000ED69 File Offset: 0x0000CF69
		private static void WriteLine(object source, TraceEventType eventType, SignedXmlDebugLog.SignedXmlDebugEvent eventId, string data)
		{
			SignedXmlDebugLog.s_traceSource.TraceEvent(eventType, (int)eventId, "[{0}, {1}] {2}", new object[]
			{
				SignedXmlDebugLog.GetObjectId(source),
				eventId,
				data
			});
		}

		// Token: 0x0400044B RID: 1099
		private const string NullString = "(null)";

		// Token: 0x0400044C RID: 1100
		private static TraceSource s_traceSource = new TraceSource("System.Security.Cryptography.Xml.SignedXml");

		// Token: 0x0400044D RID: 1101
		private static volatile bool s_haveVerboseLogging;

		// Token: 0x0400044E RID: 1102
		private static volatile bool s_verboseLogging;

		// Token: 0x0400044F RID: 1103
		private static volatile bool s_haveInformationLogging;

		// Token: 0x04000450 RID: 1104
		private static volatile bool s_informationLogging;

		// Token: 0x020000DC RID: 220
		internal enum SignedXmlDebugEvent
		{
			// Token: 0x0400066E RID: 1646
			BeginCanonicalization,
			// Token: 0x0400066F RID: 1647
			BeginCheckSignatureFormat,
			// Token: 0x04000670 RID: 1648
			BeginCheckSignedInfo,
			// Token: 0x04000671 RID: 1649
			BeginSignatureComputation,
			// Token: 0x04000672 RID: 1650
			BeginSignatureVerification,
			// Token: 0x04000673 RID: 1651
			CanonicalizedData,
			// Token: 0x04000674 RID: 1652
			FormatValidationResult,
			// Token: 0x04000675 RID: 1653
			NamespacePropagation,
			// Token: 0x04000676 RID: 1654
			ReferenceData,
			// Token: 0x04000677 RID: 1655
			SignatureVerificationResult,
			// Token: 0x04000678 RID: 1656
			Signing,
			// Token: 0x04000679 RID: 1657
			SigningReference,
			// Token: 0x0400067A RID: 1658
			VerificationFailure,
			// Token: 0x0400067B RID: 1659
			VerifyReference,
			// Token: 0x0400067C RID: 1660
			VerifySignedInfo,
			// Token: 0x0400067D RID: 1661
			X509Verification,
			// Token: 0x0400067E RID: 1662
			UnsafeCanonicalizationMethod,
			// Token: 0x0400067F RID: 1663
			UnsafeTransformMethod
		}
	}
}

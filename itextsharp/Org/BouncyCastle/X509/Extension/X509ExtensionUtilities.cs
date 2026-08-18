using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509.Extension
{
	// Token: 0x0200022C RID: 556
	public class X509ExtensionUtilities
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x0007D900 File Offset: 0x0007C900
		public static Asn1Object FromExtensionValue(Asn1OctetString extensionValue)
		{
			return Asn1Object.FromByteArray(extensionValue.GetOctets());
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0007D910 File Offset: 0x0007C910
		public static ICollection GetIssuerAlternativeNames(X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(X509Extensions.IssuerAlternativeName);
			return X509ExtensionUtilities.GetAlternativeName(extensionValue);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0007D930 File Offset: 0x0007C930
		public static ICollection GetSubjectAlternativeNames(X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(X509Extensions.SubjectAlternativeName);
			return X509ExtensionUtilities.GetAlternativeName(extensionValue);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0007D950 File Offset: 0x0007C950
		private static ICollection GetAlternativeName(Asn1OctetString extVal)
		{
			ArrayList arrayList = new ArrayList();
			if (extVal != null)
			{
				try
				{
					Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extVal));
					foreach (object obj in instance)
					{
						GeneralName generalName = (GeneralName)obj;
						ArrayList arrayList2 = new ArrayList();
						arrayList2.Add(generalName.TagNo);
						switch (generalName.TagNo)
						{
						case 0:
						case 3:
						case 5:
							arrayList2.Add(generalName.Name.ToAsn1Object());
							break;
						case 1:
						case 2:
						case 6:
							arrayList2.Add(((IAsn1String)generalName.Name).GetString());
							break;
						case 4:
							arrayList2.Add(X509Name.GetInstance(generalName.Name).ToString());
							break;
						case 7:
							arrayList2.Add(Asn1OctetString.GetInstance(generalName.Name).GetOctets());
							break;
						case 8:
							arrayList2.Add(DerObjectIdentifier.GetInstance(generalName.Name).Id);
							break;
						default:
							throw new IOException("Bad tag number: " + generalName.TagNo);
						}
						arrayList.Add(arrayList2);
					}
				}
				catch (Exception ex)
				{
					throw new CertificateParsingException(ex.Message);
				}
			}
			return arrayList;
		}
	}
}

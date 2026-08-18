using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000004 RID: 4
	public class X509CrlEntry : X509ExtensionBase
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021AE File Offset: 0x000011AE
		public X509CrlEntry(CrlEntry c)
		{
			this.c = c;
			this.certificateIssuer = this.loadCertificateIssuer();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C9 File Offset: 0x000011C9
		public X509CrlEntry(CrlEntry c, bool isIndirect, X509Name previousCertificateIssuer)
		{
			this.c = c;
			this.isIndirect = isIndirect;
			this.previousCertificateIssuer = previousCertificateIssuer;
			this.certificateIssuer = this.loadCertificateIssuer();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F4 File Offset: 0x000011F4
		private X509Name loadCertificateIssuer()
		{
			if (!this.isIndirect)
			{
				return null;
			}
			Asn1OctetString extensionValue = this.GetExtensionValue(X509Extensions.CertificateIssuer);
			if (extensionValue == null)
			{
				return this.previousCertificateIssuer;
			}
			try
			{
				GeneralName[] names = GeneralNames.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).GetNames();
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].TagNo == 4)
					{
						return X509Name.GetInstance(names[i].Name);
					}
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002274 File Offset: 0x00001274
		public X509Name GetCertificateIssuer()
		{
			return this.certificateIssuer;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000227C File Offset: 0x0000127C
		protected override X509Extensions GetX509Extensions()
		{
			return this.c.Extensions;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000228C File Offset: 0x0000128C
		public byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022C8 File Offset: 0x000012C8
		public BigInteger SerialNumber
		{
			get
			{
				return this.c.UserCertificate.Value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022DA File Offset: 0x000012DA
		public DateTime RevocationDate
		{
			get
			{
				return this.c.RevocationDate.ToDateTime();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022EC File Offset: 0x000012EC
		public bool HasExtensions
		{
			get
			{
				return this.c.Extensions != null;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002300 File Offset: 0x00001300
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("        userCertificate: ").Append(this.SerialNumber).Append(newLine);
			stringBuilder.Append("         revocationDate: ").Append(this.RevocationDate).Append(newLine);
			stringBuilder.Append("      certificateIssuer: ").Append(this.GetCertificateIssuer()).Append(newLine);
			X509Extensions extensions = this.c.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("   crlEntryExtensions:").Append(newLine);
					for (;;)
					{
						DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
						X509Extension extension = extensions.GetExtension(derObjectIdentifier);
						if (extension.Value != null)
						{
							Asn1Object asn1Object = Asn1Object.FromByteArray(extension.Value.GetOctets());
							stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
							try
							{
								if (derObjectIdentifier.Equals(X509Extensions.ReasonCode))
								{
									stringBuilder.Append(new CrlReason(DerEnumerated.GetInstance(asn1Object)));
								}
								else if (derObjectIdentifier.Equals(X509Extensions.CertificateIssuer))
								{
									stringBuilder.Append("Certificate issuer: ").Append(GeneralNames.GetInstance((Asn1Sequence)asn1Object));
								}
								else
								{
									stringBuilder.Append(derObjectIdentifier.Id);
									stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object));
								}
								stringBuilder.Append(newLine);
								goto IL_1B0;
							}
							catch (Exception)
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append("*****").Append(newLine);
								goto IL_1B0;
							}
							goto IL_1A8;
						}
						goto IL_1A8;
						IL_1B0:
						if (!enumerator.MoveNext())
						{
							break;
						}
						continue;
						IL_1A8:
						stringBuilder.Append(newLine);
						goto IL_1B0;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000001 RID: 1
		private CrlEntry c;

		// Token: 0x04000002 RID: 2
		private bool isIndirect;

		// Token: 0x04000003 RID: 3
		private X509Name previousCertificateIssuer;

		// Token: 0x04000004 RID: 4
		private X509Name certificateIssuer;
	}
}

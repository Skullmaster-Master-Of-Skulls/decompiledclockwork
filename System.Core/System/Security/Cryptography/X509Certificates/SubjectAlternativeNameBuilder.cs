using System;
using System.Collections.Generic;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200012E RID: 302
	public sealed class SubjectAlternativeNameBuilder
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00023F2F File Offset: 0x0002212F
		public void AddEmailAddress(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentOutOfRangeException("emailAddress", SR.GetString("Arg_EmptyOrNullString"));
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeEmailAddress(emailAddress));
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00023F65 File Offset: 0x00022165
		public void AddDnsName(string dnsName)
		{
			if (string.IsNullOrEmpty(dnsName))
			{
				throw new ArgumentOutOfRangeException("dnsName", SR.GetString("Arg_EmptyOrNullString"));
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeDnsName(dnsName));
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00023F9B File Offset: 0x0002219B
		public void AddUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUri(uri));
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00023FC8 File Offset: 0x000221C8
		public void AddIpAddress(IPAddress ipAddress)
		{
			if (ipAddress == null)
			{
				throw new ArgumentNullException("ipAddress");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeIpAddress(ipAddress));
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00023FEF File Offset: 0x000221EF
		public void AddUserPrincipalName(string upn)
		{
			if (string.IsNullOrEmpty(upn))
			{
				throw new ArgumentOutOfRangeException("upn", SR.GetString("Arg_EmptyOrNullString"));
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUserPrincipalName(upn));
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00024025 File Offset: 0x00022225
		public X509Extension Build(bool critical = false)
		{
			return new X509Extension("2.5.29.17", DerEncoder.ConstructSequence(this._encodedTlvs), critical);
		}

		// Token: 0x04000742 RID: 1858
		private readonly List<byte[][]> _encodedTlvs = new List<byte[][]>();

		// Token: 0x04000743 RID: 1859
		private readonly GeneralNameEncoder _generalNameEncoder = new GeneralNameEncoder();
	}
}

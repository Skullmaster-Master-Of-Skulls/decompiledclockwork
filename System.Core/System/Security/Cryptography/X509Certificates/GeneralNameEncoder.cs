using System;
using System.Globalization;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000127 RID: 295
	internal sealed class GeneralNameEncoder
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x00023534 File Offset: 0x00021734
		internal byte[][] EncodeEmailAddress(string emailAddress)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(emailAddress.ToCharArray());
			array[0][0] = 129;
			return array;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00023558 File Offset: 0x00021758
		internal byte[][] EncodeDnsName(string dnsName)
		{
			string ascii = this._idnMapping.GetAscii(dnsName);
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(ascii.ToCharArray());
			array[0][0] = 130;
			return array;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002358C File Offset: 0x0002178C
		internal byte[][] EncodeUri(Uri uri)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(uri.AbsoluteUri.ToCharArray());
			array[0][0] = 134;
			return array;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000235B8 File Offset: 0x000217B8
		internal byte[][] EncodeIpAddress(IPAddress address)
		{
			byte[] addressBytes = address.GetAddressBytes();
			byte[][] array = DerEncoder.SegmentedEncodeOctetString(addressBytes);
			array[0][0] = 135;
			return array;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000235E0 File Offset: 0x000217E0
		internal byte[][] EncodeUserPrincipalName(string upn)
		{
			byte[][] array = DerEncoder.SegmentedEncodeUtf8String(upn.ToCharArray());
			byte[][] array2 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				array
			});
			array2[0][0] = 160;
			byte[][] array3 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid("1.3.6.1.4.1.311.20.2.3"),
				array2
			});
			array3[0][0] = 160;
			return array3;
		}

		// Token: 0x04000711 RID: 1809
		private readonly IdnMapping _idnMapping = new IdnMapping();

		// Token: 0x02000358 RID: 856
		private enum GeneralNameTag : byte
		{
			// Token: 0x04000F3E RID: 3902
			OtherName = 160,
			// Token: 0x04000F3F RID: 3903
			Rfc822Name = 129,
			// Token: 0x04000F40 RID: 3904
			DnsName,
			// Token: 0x04000F41 RID: 3905
			X400Address,
			// Token: 0x04000F42 RID: 3906
			DirectoryName,
			// Token: 0x04000F43 RID: 3907
			EdiPartyName,
			// Token: 0x04000F44 RID: 3908
			Uri,
			// Token: 0x04000F45 RID: 3909
			IpAddress,
			// Token: 0x04000F46 RID: 3910
			RegisteredId
		}
	}
}

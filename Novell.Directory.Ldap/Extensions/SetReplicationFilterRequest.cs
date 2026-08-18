using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B4 RID: 180
	public class SetReplicationFilterRequest : LdapExtendedOperation
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0001733C File Offset: 0x0001633C
		public SetReplicationFilterRequest(string serverDN, string[][] replicationFilter) : base("2.16.840.1.113719.1.27.100.35", null)
		{
			try
			{
				if (serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				asn1OctetString.encode(enc, memoryStream);
				Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf();
				if (replicationFilter == null)
				{
					asn1SequenceOf.encode(enc, memoryStream);
					this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
				}
				else
				{
					int num = 0;
					while (num < replicationFilter.Length && replicationFilter[num] != null)
					{
						Asn1Sequence asn1Sequence = new Asn1Sequence();
						asn1Sequence.add(new Asn1OctetString(replicationFilter[num][0]));
						Asn1SequenceOf asn1SequenceOf2 = new Asn1SequenceOf();
						int num2 = 1;
						while (num2 < replicationFilter[num].Length && replicationFilter[num][num2] != null)
						{
							asn1SequenceOf2.add(new Asn1OctetString(replicationFilter[num][num2]));
							num2++;
						}
						asn1Sequence.add(asn1SequenceOf2);
						asn1SequenceOf.add(asn1Sequence);
						num++;
					}
					asn1SequenceOf.encode(enc, memoryStream);
					this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
				}
			}
			catch (IOException ex)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}

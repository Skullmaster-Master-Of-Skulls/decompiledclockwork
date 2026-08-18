using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A7 RID: 167
	public class ListReplicasResponse : LdapExtendedResponse
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00016C6C File Offset: 0x00015C6C
		public virtual string[] ReplicaList
		{
			get
			{
				return this.replicaList;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00016C84 File Offset: 0x00015C84
		public ListReplicasResponse(RfcLdapMessage rfcMessage) : base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.replicaList = new string[0];
			}
			else
			{
				sbyte[] value = this.Value;
				if (value == null)
				{
					throw new IOException("No returned value");
				}
				LBERDecoder lberdecoder = new LBERDecoder();
				if (lberdecoder == null)
				{
					throw new IOException("Decoding error");
				}
				Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(value);
				if (asn1Sequence == null)
				{
					throw new IOException("Decoding error");
				}
				int num = asn1Sequence.size();
				this.replicaList = new string[num];
				for (int i = 0; i < num; i++)
				{
					Asn1OctetString asn1OctetString = (Asn1OctetString)asn1Sequence.get_Renamed(i);
					if (asn1OctetString == null)
					{
						throw new IOException("Decoding error");
					}
					this.replicaList[i] = asn1OctetString.stringValue();
					if (this.replicaList[i] == null)
					{
						throw new IOException("Decoding error");
					}
				}
			}
		}

		// Token: 0x0400035A RID: 858
		private string[] replicaList;
	}
}

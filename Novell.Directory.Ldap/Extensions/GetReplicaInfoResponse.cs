using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A0 RID: 160
	public class GetReplicaInfoResponse : LdapExtendedResponse
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x00015FA4 File Offset: 0x00014FA4
		public GetReplicaInfoResponse(RfcLdapMessage rfcMessage) : base(rfcMessage)
		{
			if (this.ResultCode == 0)
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
				MemoryStream in_Renamed = new MemoryStream(SupportClass.ToByteArray(value));
				Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer == null)
				{
					throw new IOException("Decoding error");
				}
				this.partitionID = asn1Integer.intValue();
				Asn1Integer asn1Integer2 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer2 == null)
				{
					throw new IOException("Decoding error");
				}
				this.replicaState = asn1Integer2.intValue();
				Asn1Integer asn1Integer3 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer3 == null)
				{
					throw new IOException("Decoding error");
				}
				this.modificationTime = asn1Integer3.intValue();
				Asn1Integer asn1Integer4 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer4 == null)
				{
					throw new IOException("Decoding error");
				}
				this.purgeTime = asn1Integer4.intValue();
				Asn1Integer asn1Integer5 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer5 == null)
				{
					throw new IOException("Decoding error");
				}
				this.localPartitionID = asn1Integer5.intValue();
				Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(in_Renamed);
				if (asn1OctetString == null)
				{
					throw new IOException("Decoding error");
				}
				this.partitionDN = asn1OctetString.stringValue();
				if (this.partitionDN == null)
				{
					throw new IOException("Decoding error");
				}
				Asn1Integer asn1Integer6 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer6 == null)
				{
					throw new IOException("Decoding error");
				}
				this.replicaType = asn1Integer6.intValue();
				Asn1Integer asn1Integer7 = (Asn1Integer)lberdecoder.decode(in_Renamed);
				if (asn1Integer7 == null)
				{
					throw new IOException("Decoding error");
				}
				this.flags = asn1Integer7.intValue();
			}
			else
			{
				this.partitionID = 0;
				this.replicaState = 0;
				this.modificationTime = 0;
				this.purgeTime = 0;
				this.localPartitionID = 0;
				this.partitionDN = "";
				this.replicaType = 0;
				this.flags = 0;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00016198 File Offset: 0x00015198
		public virtual int getpartitionID()
		{
			return this.partitionID;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000161B0 File Offset: 0x000151B0
		public virtual int getreplicaState()
		{
			return this.replicaState;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000161C8 File Offset: 0x000151C8
		public virtual int getmodificationTime()
		{
			return this.modificationTime;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000161E0 File Offset: 0x000151E0
		public virtual int getpurgeTime()
		{
			return this.purgeTime;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000161F8 File Offset: 0x000151F8
		public virtual int getlocalPartitionID()
		{
			return this.localPartitionID;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016210 File Offset: 0x00015210
		public virtual string getpartitionDN()
		{
			return this.partitionDN;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016228 File Offset: 0x00015228
		public virtual int getreplicaType()
		{
			return this.replicaType;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00016240 File Offset: 0x00015240
		public virtual int getflags()
		{
			return this.flags;
		}

		// Token: 0x0400034D RID: 845
		private int partitionID;

		// Token: 0x0400034E RID: 846
		private int replicaState;

		// Token: 0x0400034F RID: 847
		private int modificationTime;

		// Token: 0x04000350 RID: 848
		private int purgeTime;

		// Token: 0x04000351 RID: 849
		private int localPartitionID;

		// Token: 0x04000352 RID: 850
		private string partitionDN;

		// Token: 0x04000353 RID: 851
		private int replicaType;

		// Token: 0x04000354 RID: 852
		private int flags;
	}
}

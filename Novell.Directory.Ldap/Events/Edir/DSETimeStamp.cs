using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x02000072 RID: 114
	public class DSETimeStamp
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000136C0 File Offset: 0x000126C0
		public int Seconds
		{
			get
			{
				return this.nSeconds;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000136D8 File Offset: 0x000126D8
		public int ReplicaNumber
		{
			get
			{
				return this.replica_number;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x000136F0 File Offset: 0x000126F0
		public int Event
		{
			get
			{
				return this.nEvent;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013708 File Offset: 0x00012708
		public DSETimeStamp(Asn1Sequence dseObject)
		{
			this.nSeconds = ((Asn1Integer)dseObject.get_Renamed(0)).intValue();
			this.replica_number = ((Asn1Integer)dseObject.get_Renamed(1)).intValue();
			this.nEvent = ((Asn1Integer)dseObject.get_Renamed(2)).intValue();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00013760 File Offset: 0x00012760
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[TimeStamp (seconds={0})", this.nSeconds);
			stringBuilder.AppendFormat("(replicaNumber={0})", this.replica_number);
			stringBuilder.AppendFormat("(event={0})", this.nEvent);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001ED RID: 493
		protected int nSeconds;

		// Token: 0x040001EE RID: 494
		protected int replica_number;

		// Token: 0x040001EF RID: 495
		protected int nEvent;
	}
}

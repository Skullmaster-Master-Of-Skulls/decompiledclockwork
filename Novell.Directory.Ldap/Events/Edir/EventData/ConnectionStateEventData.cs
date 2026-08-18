using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x0200006F RID: 111
	public class ConnectionStateEventData : BaseEdirEventData
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00012FD8 File Offset: 0x00011FD8
		public string ConnectionDN
		{
			get
			{
				return this.strConnectionDN;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00012FF0 File Offset: 0x00011FF0
		public int OldFlags
		{
			get
			{
				return this.old_flags;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00013008 File Offset: 0x00012008
		public int NewFlags
		{
			get
			{
				return this.new_flags;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00013020 File Offset: 0x00012020
		public string SourceModule
		{
			get
			{
				return this.source_module;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013038 File Offset: 0x00012038
		public ConnectionStateEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.strConnectionDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.old_flags = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.new_flags = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.source_module = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000130E4 File Offset: 0x000120E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ConnectionStateEvent");
			stringBuilder.AppendFormat("(ConnectionDN={0})", this.strConnectionDN);
			stringBuilder.AppendFormat("(oldFlags={0})", this.old_flags);
			stringBuilder.AppendFormat("(newFlags={0})", this.new_flags);
			stringBuilder.AppendFormat("(SourceModule={0})", this.source_module);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001E0 RID: 480
		protected string strConnectionDN;

		// Token: 0x040001E1 RID: 481
		protected int old_flags;

		// Token: 0x040001E2 RID: 482
		protected int new_flags;

		// Token: 0x040001E3 RID: 483
		protected string source_module;
	}
}

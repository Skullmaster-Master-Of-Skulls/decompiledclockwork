using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000075 RID: 117
	public class ModuleStateEventData : BaseEdirEventData
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013EBC File Offset: 0x00012EBC
		public string ConnectionDN
		{
			get
			{
				return this.strConnectionDN;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00013ED4 File Offset: 0x00012ED4
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00013EEC File Offset: 0x00012EEC
		public string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00013F04 File Offset: 0x00012F04
		public string Description
		{
			get
			{
				return this.strDescription;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00013F1C File Offset: 0x00012F1C
		public string Source
		{
			get
			{
				return this.strSource;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00013F34 File Offset: 0x00012F34
		public ModuleStateEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.strConnectionDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strName = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strDescription = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.strSource = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00014000 File Offset: 0x00013000
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ModuleStateEvent");
			stringBuilder.AppendFormat("(connectionDN={0})", this.strConnectionDN);
			stringBuilder.AppendFormat("(flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(Name={0})", this.strName);
			stringBuilder.AppendFormat("(Description={0})", this.strDescription);
			stringBuilder.AppendFormat("(Source={0})", this.strSource);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001FE RID: 510
		protected string strConnectionDN;

		// Token: 0x040001FF RID: 511
		protected int nFlags;

		// Token: 0x04000200 RID: 512
		protected string strName;

		// Token: 0x04000201 RID: 513
		protected string strDescription;

		// Token: 0x04000202 RID: 514
		protected string strSource;
	}
}

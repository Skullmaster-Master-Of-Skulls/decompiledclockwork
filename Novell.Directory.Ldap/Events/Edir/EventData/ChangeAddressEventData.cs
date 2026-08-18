using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x0200006E RID: 110
	public class ChangeAddressEventData : BaseEdirEventData
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00012DA8 File Offset: 0x00011DA8
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00012DC0 File Offset: 0x00011DC0
		public int Proto
		{
			get
			{
				return this.nProto;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00012DD8 File Offset: 0x00011DD8
		public int AddressFamily
		{
			get
			{
				return this.address_family;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00012DF0 File Offset: 0x00011DF0
		public string Address
		{
			get
			{
				return this.strAddress;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00012E08 File Offset: 0x00011E08
		public string PstkName
		{
			get
			{
				return this.pstk_name;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00012E20 File Offset: 0x00011E20
		public string SourceModule
		{
			get
			{
				return this.source_module;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012E38 File Offset: 0x00011E38
		public ChangeAddressEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.nProto = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.address_family = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strAddress = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.pstk_name = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.source_module = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00012F28 File Offset: 0x00011F28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ChangeAddresssEvent");
			stringBuilder.AppendFormat("(flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(proto={0})", this.nProto);
			stringBuilder.AppendFormat("(addrFamily={0})", this.address_family);
			stringBuilder.AppendFormat("(address={0})", this.strAddress);
			stringBuilder.AppendFormat("(pstkName={0})", this.pstk_name);
			stringBuilder.AppendFormat("(source={0})", this.source_module);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001DA RID: 474
		protected int nFlags;

		// Token: 0x040001DB RID: 475
		protected int nProto;

		// Token: 0x040001DC RID: 476
		protected int address_family;

		// Token: 0x040001DD RID: 477
		protected string strAddress;

		// Token: 0x040001DE RID: 478
		protected string pstk_name;

		// Token: 0x040001DF RID: 479
		protected string source_module;
	}
}

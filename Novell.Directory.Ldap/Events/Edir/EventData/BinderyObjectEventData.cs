using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x0200006D RID: 109
	public class BinderyObjectEventData : BaseEdirEventData
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00012BC4 File Offset: 0x00011BC4
		public string EntryDN
		{
			get
			{
				return this.strEntryDN;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00012BDC File Offset: 0x00011BDC
		public int ValueType
		{
			get
			{
				return this.nType;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00012BF4 File Offset: 0x00011BF4
		public int EmuObjFlags
		{
			get
			{
				return this.nEmuObjFlags;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00012C0C File Offset: 0x00011C0C
		public int Security
		{
			get
			{
				return this.nSecurity;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00012C24 File Offset: 0x00011C24
		public string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00012C3C File Offset: 0x00011C3C
		public BinderyObjectEventData(EdirEventDataType eventDataType, Asn1Object message) : base(eventDataType, message)
		{
			int[] len = new int[1];
			this.strEntryDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			this.nType = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.nEmuObjFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.nSecurity = ((Asn1Integer)this.decoder.decode(this.decodedData, len)).intValue();
			this.strName = ((Asn1OctetString)this.decoder.decode(this.decodedData, len)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012D08 File Offset: 0x00011D08
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BinderyObjectEvent");
			stringBuilder.AppendFormat("(EntryDn={0})", this.strEntryDN);
			stringBuilder.AppendFormat("(Type={0})", this.nType);
			stringBuilder.AppendFormat("(EnumOldFlags={0})", this.nEmuObjFlags);
			stringBuilder.AppendFormat("(Secuirty={0})", this.nSecurity);
			stringBuilder.AppendFormat("(Name={0})", this.strName);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040001D5 RID: 469
		protected string strEntryDN;

		// Token: 0x040001D6 RID: 470
		protected int nType;

		// Token: 0x040001D7 RID: 471
		protected int nEmuObjFlags;

		// Token: 0x040001D8 RID: 472
		protected int nSecurity;

		// Token: 0x040001D9 RID: 473
		protected string strName;
	}
}

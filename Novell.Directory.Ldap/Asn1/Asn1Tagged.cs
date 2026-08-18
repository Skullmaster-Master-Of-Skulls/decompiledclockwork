using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000062 RID: 98
	[CLSCompliant(true)]
	public class Asn1Tagged : Asn1Object
	{
		// Token: 0x170000D2 RID: 210
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0001166C File Offset: 0x0001066C
		[CLSCompliant(false)]
		public virtual Asn1Object TaggedValue
		{
			set
			{
				this.content = value;
				if (!this.explicit_Renamed && value != null)
				{
					value.setIdentifier(this.getIdentifier());
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00011698 File Offset: 0x00010698
		public virtual bool Explicit
		{
			get
			{
				return this.explicit_Renamed;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000116B0 File Offset: 0x000106B0
		public Asn1Tagged(Asn1Identifier identifier, Asn1Object object_Renamed) : this(identifier, object_Renamed, true)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000116C8 File Offset: 0x000106C8
		public Asn1Tagged(Asn1Identifier identifier, Asn1Object object_Renamed, bool explicit_Renamed) : base(identifier)
		{
			this.content = object_Renamed;
			this.explicit_Renamed = explicit_Renamed;
			if (!explicit_Renamed && this.content != null)
			{
				this.content.setIdentifier(identifier);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011704 File Offset: 0x00010704
		[CLSCompliant(false)]
		public Asn1Tagged(Asn1Decoder dec, Stream in_Renamed, int len, Asn1Identifier identifier) : base(identifier)
		{
			this.content = new Asn1OctetString(dec, in_Renamed, len);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001172C File Offset: 0x0001072C
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011744 File Offset: 0x00010744
		public Asn1Object taggedValue()
		{
			return this.content;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001175C File Offset: 0x0001075C
		public override string ToString()
		{
			string result;
			if (this.explicit_Renamed)
			{
				result = base.ToString() + this.content.ToString();
			}
			else
			{
				result = this.content.ToString();
			}
			return result;
		}

		// Token: 0x040001A2 RID: 418
		private bool explicit_Renamed;

		// Token: 0x040001A3 RID: 419
		private Asn1Object content;
	}
}

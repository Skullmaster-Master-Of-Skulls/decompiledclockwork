using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000053 RID: 83
	[CLSCompliant(true)]
	public class Asn1Choice : Asn1Object
	{
		// Token: 0x170000C7 RID: 199
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00010834 File Offset: 0x0000F834
		[CLSCompliant(false)]
		protected internal virtual Asn1Object ChoiceValue
		{
			set
			{
				this.content = value;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001084C File Offset: 0x0000F84C
		public Asn1Choice(Asn1Object content) : base(null)
		{
			this.content = content;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001086C File Offset: 0x0000F86C
		protected internal Asn1Choice() : base(null)
		{
			this.content = null;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001088C File Offset: 0x0000F88C
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			this.content.encode(enc, out_Renamed);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000108A8 File Offset: 0x0000F8A8
		public Asn1Object choiceValue()
		{
			return this.content;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000108C0 File Offset: 0x0000F8C0
		public override Asn1Identifier getIdentifier()
		{
			return this.content.getIdentifier();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000108DC File Offset: 0x0000F8DC
		public override void setIdentifier(Asn1Identifier id)
		{
			this.content.setIdentifier(id);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000108F8 File Offset: 0x0000F8F8
		public override string ToString()
		{
			return this.content.ToString();
		}

		// Token: 0x04000183 RID: 387
		private Asn1Object content;
	}
}

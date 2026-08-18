using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000056 RID: 86
	[CLSCompliant(true)]
	public abstract class Asn1Numeric : Asn1Object
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00010914 File Offset: 0x0000F914
		internal Asn1Numeric(Asn1Identifier id, int value_Renamed) : base(id)
		{
			this.content = (long)value_Renamed;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010934 File Offset: 0x0000F934
		internal Asn1Numeric(Asn1Identifier id, long value_Renamed) : base(id)
		{
			this.content = value_Renamed;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010954 File Offset: 0x0000F954
		public int intValue()
		{
			return (int)this.content;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001096C File Offset: 0x0000F96C
		public long longValue()
		{
			return this.content;
		}

		// Token: 0x04000184 RID: 388
		private long content;
	}
}

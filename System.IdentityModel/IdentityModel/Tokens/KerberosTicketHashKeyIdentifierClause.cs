using System;
using System.Globalization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000128 RID: 296
	public sealed class KerberosTicketHashKeyIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x000222C1 File Offset: 0x000204C1
		public KerberosTicketHashKeyIdentifierClause(byte[] ticketHash) : this(ticketHash, null, 0)
		{
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000222CC File Offset: 0x000204CC
		public KerberosTicketHashKeyIdentifierClause(byte[] ticketHash, byte[] derivationNonce, int derivationLength) : this(ticketHash, true, derivationNonce, derivationLength)
		{
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000026A8 File Offset: 0x000008A8
		internal KerberosTicketHashKeyIdentifierClause(byte[] ticketHash, bool cloneBuffer, byte[] derivationNonce, int derivationLength) : base(null, ticketHash, cloneBuffer, derivationNonce, derivationLength)
		{
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetKerberosTicketHash()
		{
			return base.GetBuffer();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000222D8 File Offset: 0x000204D8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "KerberosTicketHashKeyIdentifierClause(Hash = {0})", new object[]
			{
				base.ToBase64String()
			});
		}
	}
}

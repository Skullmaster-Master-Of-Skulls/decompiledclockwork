using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020002E9 RID: 745
	public class PgpDataValidationException : PgpException
	{
		// Token: 0x06001B9B RID: 7067 RVA: 0x000A58F1 File Offset: 0x000A48F1
		public PgpDataValidationException()
		{
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000A58F9 File Offset: 0x000A48F9
		public PgpDataValidationException(string message) : base(message)
		{
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000A5902 File Offset: 0x000A4902
		public PgpDataValidationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}

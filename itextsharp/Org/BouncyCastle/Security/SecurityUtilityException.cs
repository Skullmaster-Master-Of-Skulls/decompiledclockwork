using System;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000540 RID: 1344
	public class SecurityUtilityException : Exception
	{
		// Token: 0x06002E33 RID: 11827 RVA: 0x0011D7B7 File Offset: 0x0011C7B7
		public SecurityUtilityException()
		{
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x0011D7BF File Offset: 0x0011C7BF
		public SecurityUtilityException(string message) : base(message)
		{
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x0011D7C8 File Offset: 0x0011C7C8
		public SecurityUtilityException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}

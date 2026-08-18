using System;

namespace Telerik.Web.Apoc
{
	// Token: 0x02001374 RID: 4980
	public class ApocException : ApplicationException
	{
		// Token: 0x0600CFE9 RID: 53225 RVA: 0x002E12BE File Offset: 0x002DF4BE
		public ApocException(Exception innerException) : base(innerException.Message, innerException)
		{
		}

		// Token: 0x0600CFEA RID: 53226 RVA: 0x002E12CD File Offset: 0x002DF4CD
		public ApocException(string message) : base(message)
		{
		}

		// Token: 0x0600CFEB RID: 53227 RVA: 0x002E12D6 File Offset: 0x002DF4D6
		public ApocException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

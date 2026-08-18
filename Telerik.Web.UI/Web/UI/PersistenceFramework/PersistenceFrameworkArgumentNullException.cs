using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x0200048B RID: 1163
	public class PersistenceFrameworkArgumentNullException : ArgumentNullException
	{
		// Token: 0x0600295F RID: 10591 RVA: 0x0008579D File Offset: 0x0008399D
		public PersistenceFrameworkArgumentNullException()
		{
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000857A5 File Offset: 0x000839A5
		public PersistenceFrameworkArgumentNullException(string message) : base(message)
		{
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000857AE File Offset: 0x000839AE
		public PersistenceFrameworkArgumentNullException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

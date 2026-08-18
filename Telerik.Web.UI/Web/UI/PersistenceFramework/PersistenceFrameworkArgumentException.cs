using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x0200048A RID: 1162
	public class PersistenceFrameworkArgumentException : PersistenceFrameworkException
	{
		// Token: 0x0600295C RID: 10588 RVA: 0x00085782 File Offset: 0x00083982
		public PersistenceFrameworkArgumentException()
		{
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0008578A File Offset: 0x0008398A
		public PersistenceFrameworkArgumentException(string message) : base(message)
		{
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00085793 File Offset: 0x00083993
		public PersistenceFrameworkArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

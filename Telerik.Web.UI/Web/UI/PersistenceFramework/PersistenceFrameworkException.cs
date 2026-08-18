using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000488 RID: 1160
	public class PersistenceFrameworkException : Exception
	{
		// Token: 0x06002956 RID: 10582 RVA: 0x0008574C File Offset: 0x0008394C
		public PersistenceFrameworkException()
		{
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x00085754 File Offset: 0x00083954
		public PersistenceFrameworkException(string message) : base(message)
		{
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0008575D File Offset: 0x0008395D
		public PersistenceFrameworkException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

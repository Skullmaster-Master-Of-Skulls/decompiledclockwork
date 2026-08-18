using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000489 RID: 1161
	public class PersistenceFrameworkStorageException : PersistenceFrameworkException
	{
		// Token: 0x06002959 RID: 10585 RVA: 0x00085767 File Offset: 0x00083967
		public PersistenceFrameworkStorageException()
		{
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x0008576F File Offset: 0x0008396F
		public PersistenceFrameworkStorageException(string message) : base(message)
		{
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x00085778 File Offset: 0x00083978
		public PersistenceFrameworkStorageException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

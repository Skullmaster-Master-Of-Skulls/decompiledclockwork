using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x0200048C RID: 1164
	public class PersistenceFrameworkInvalidOperationException : InvalidOperationException
	{
		// Token: 0x06002962 RID: 10594 RVA: 0x000857B8 File Offset: 0x000839B8
		public PersistenceFrameworkInvalidOperationException()
		{
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000857C0 File Offset: 0x000839C0
		public PersistenceFrameworkInvalidOperationException(string message) : base(message)
		{
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000857C9 File Offset: 0x000839C9
		public PersistenceFrameworkInvalidOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using System;

namespace System.Web.Mvc
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public class ModelError
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x0002369E File Offset: 0x0002189E
		public ModelError(Exception exception) : this(exception, null)
		{
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000236A8 File Offset: 0x000218A8
		public ModelError(Exception exception, string errorMessage) : this(errorMessage)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000236C6 File Offset: 0x000218C6
		public ModelError(string errorMessage)
		{
			this.ErrorMessage = (errorMessage ?? string.Empty);
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000236DE File Offset: 0x000218DE
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000236E6 File Offset: 0x000218E6
		public Exception Exception { get; private set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000236EF File Offset: 0x000218EF
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000236F7 File Offset: 0x000218F7
		public string ErrorMessage { get; private set; }
	}
}

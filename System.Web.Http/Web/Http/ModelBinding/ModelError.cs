using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class ModelError
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x0001ADAA File Offset: 0x00018FAA
		public ModelError(Exception exception) : this(exception, null)
		{
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		public ModelError(Exception exception, string errorMessage) : this(errorMessage)
		{
			if (exception == null)
			{
				throw Error.ArgumentNull("exception");
			}
			this.Exception = exception;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001ADD2 File Offset: 0x00018FD2
		public ModelError(string errorMessage)
		{
			this.ErrorMessage = (errorMessage ?? string.Empty);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001ADEA File Offset: 0x00018FEA
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		public Exception Exception { get; private set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001ADFB File Offset: 0x00018FFB
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0001AE03 File Offset: 0x00019003
		public string ErrorMessage { get; private set; }
	}
}

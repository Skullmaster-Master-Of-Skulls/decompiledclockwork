using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000658 RID: 1624
	[Serializable]
	public class ModelError
	{
		// Token: 0x06004FC1 RID: 20417 RVA: 0x00114A40 File Offset: 0x00112C40
		public ModelError(Exception exception) : this(exception, null)
		{
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00114A4A File Offset: 0x00112C4A
		public ModelError(Exception exception, string errorMessage) : this(errorMessage)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00114A68 File Offset: 0x00112C68
		public ModelError(string errorMessage)
		{
			this.ErrorMessage = (errorMessage ?? string.Empty);
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06004FC4 RID: 20420 RVA: 0x00114A80 File Offset: 0x00112C80
		// (set) Token: 0x06004FC5 RID: 20421 RVA: 0x00114A88 File Offset: 0x00112C88
		public Exception Exception { get; private set; }

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x00114A91 File Offset: 0x00112C91
		// (set) Token: 0x06004FC7 RID: 20423 RVA: 0x00114A99 File Offset: 0x00112C99
		public string ErrorMessage { get; private set; }
	}
}

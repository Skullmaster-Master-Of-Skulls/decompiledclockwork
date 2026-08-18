using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public abstract class EdmError
	{
		// Token: 0x06001EB0 RID: 7856 RVA: 0x0006C712 File Offset: 0x0006A912
		internal EdmError(string message)
		{
			EntityUtil.CheckStringArgument(message, "message");
			this._message = message;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0006C72C File Offset: 0x0006A92C
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x04000CDE RID: 3294
		private string _message;
	}
}

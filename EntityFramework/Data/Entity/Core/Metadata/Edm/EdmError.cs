using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B9 RID: 1209
	[Serializable]
	public abstract class EdmError
	{
		// Token: 0x06002C82 RID: 11394 RVA: 0x000D9323 File Offset: 0x000D7523
		internal EdmError(string message)
		{
			Check.NotEmpty(message, "message");
			this._message = message;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000D933E File Offset: 0x000D753E
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x04001069 RID: 4201
		private readonly string _message;
	}
}

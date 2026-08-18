using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200065F RID: 1631
	public class ModelValidationResult
	{
		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x00115207 File Offset: 0x00113407
		// (set) Token: 0x06005026 RID: 20518 RVA: 0x00115218 File Offset: 0x00113418
		public string MemberName
		{
			get
			{
				return this._memberName ?? string.Empty;
			}
			set
			{
				this._memberName = value;
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x00115221 File Offset: 0x00113421
		// (set) Token: 0x06005028 RID: 20520 RVA: 0x00115232 File Offset: 0x00113432
		public string Message
		{
			get
			{
				return this._message ?? string.Empty;
			}
			set
			{
				this._message = value;
			}
		}

		// Token: 0x04002AB4 RID: 10932
		private string _memberName;

		// Token: 0x04002AB5 RID: 10933
		private string _message;
	}
}

using System;

namespace System.Web.Mvc
{
	// Token: 0x0200014A RID: 330
	public class ModelValidationResult
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x000178FA File Offset: 0x00015AFA
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0001790B File Offset: 0x00015B0B
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00017914 File Offset: 0x00015B14
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x00017925 File Offset: 0x00015B25
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

		// Token: 0x04000269 RID: 617
		private string _memberName;

		// Token: 0x0400026A RID: 618
		private string _message;
	}
}

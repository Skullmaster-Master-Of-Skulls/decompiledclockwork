using System;

namespace System.Web.Http.Validation
{
	// Token: 0x02000187 RID: 391
	public class ModelValidationResult
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002183C File Offset: 0x0001FA3C
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0002184D File Offset: 0x0001FA4D
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00021856 File Offset: 0x0001FA56
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x00021867 File Offset: 0x0001FA67
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

		// Token: 0x04000302 RID: 770
		private string _memberName;

		// Token: 0x04000303 RID: 771
		private string _message;
	}
}

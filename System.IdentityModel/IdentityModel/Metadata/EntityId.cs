using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F2 RID: 242
	public class EntityId
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0001A95B File Offset: 0x00018B5B
		public EntityId() : this(null)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001A964 File Offset: 0x00018B64
		public EntityId(string id)
		{
			this._id = id;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001A973 File Offset: 0x00018B73
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001A97B File Offset: 0x00018B7B
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if (value != null && value.ToString().Length > 1024)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID3199"));
				}
				this._id = value;
			}
		}

		// Token: 0x04000A6A RID: 2666
		private const int MaximumLength = 1024;

		// Token: 0x04000A6B RID: 2667
		private string _id;
	}
}

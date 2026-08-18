using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000CD RID: 205
	[Obsolete("DataSysDescriptionAttribute has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[AttributeUsage(AttributeTargets.All)]
	public class DataSysDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000C56 RID: 3158 RVA: 0x00068BD8 File Offset: 0x00067FD8
		[Obsolete("DataSysDescriptionAttribute has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public DataSysDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00068BEC File Offset: 0x00067FEC
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = Res.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x040003A5 RID: 933
		private bool replaced;
	}
}

using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000099 RID: 153
	[Obsolete("DataSysDescriptionAttribute has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[AttributeUsage(AttributeTargets.All)]
	public class DataSysDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x001FDE88 File Offset: 0x001FD288
		[Obsolete("DataSysDescriptionAttribute has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public DataSysDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x001FDEA8 File Offset: 0x001FD2A8
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

		// Token: 0x040007C7 RID: 1991
		private bool replaced;
	}
}

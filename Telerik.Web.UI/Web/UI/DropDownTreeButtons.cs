using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000468 RID: 1128
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DropDownTreeButtons : LocalizationStrings
	{
		// Token: 0x0600287C RID: 10364 RVA: 0x000831F4 File Offset: 0x000813F4
		internal DropDownTreeButtons(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x000831FD File Offset: 0x000813FD
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x0008320A File Offset: 0x0008140A
		[Localizable(true)]
		[DefaultValue("Clear")]
		[NotifyParentProperty(true)]
		public string Clear
		{
			get
			{
				return this.GetString("Clear");
			}
			set
			{
				this.SetString("Clear", value);
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x00083218 File Offset: 0x00081418
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x00083225 File Offset: 0x00081425
		[DefaultValue("Check All")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string CheckAll
		{
			get
			{
				return this.GetString("CheckAll");
			}
			set
			{
				this.SetString("CheckAll", value);
			}
		}
	}
}

using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000066 RID: 102
	public sealed class Schedule : ConfigurationElement
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x00007386 File Offset: 0x00006386
		internal Schedule()
		{
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000738E File Offset: 0x0000638E
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000073A0 File Offset: 0x000063A0
		public TimeSpan Time
		{
			get
			{
				return (TimeSpan)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}
	}
}

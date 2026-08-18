using System;
using ClockWorkWebAPIWeb;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring
{
	// Token: 0x0200012A RID: 298
	public class TutorWrapper
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0003F6AC File Offset: 0x0003D8AC
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0003F6B4 File Offset: 0x0003D8B4
		public string Name { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0003F6BD File Offset: 0x0003D8BD
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0003F6C5 File Offset: 0x0003D8C5
		public int PersonId { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0003F6D0 File Offset: 0x0003D8D0
		public string DisplayText
		{
			get
			{
				bool flag = this.PersonId < 1;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = string.Format("{0} &nbsp; &nbsp; <a style='font-size:.8em' href='TutorProfile.aspx?tpid={1}'>profile</a> | <a style='font-size:.8em' href='TutorCalendar.aspx?tpid={1}'>availability</a>", this.Name, ClockWorkWebCore.EncodeUrlVariable(this.PersonId.ToString(), true));
				}
				return result;
			}
		}
	}
}

using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200005B RID: 91
	public class MyTutorWrapper : WrapperBase<MyTutorDTO>
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000D53C File Offset: 0x0000B73C
		public MyTutorWrapper()
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D546 File Offset: 0x0000B746
		public MyTutorWrapper(MyTutorDTO tutor) : base(tutor)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D551 File Offset: 0x0000B751
		public int PersonId
		{
			get
			{
				MyTutorDTO item = base.Item;
				return (item != null) ? item.TutorPersonId : 0;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000D568 File Offset: 0x0000B768
		public string UrlId
		{
			get
			{
				return NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(this.PersonId.ToString());
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000D590 File Offset: 0x0000B790
		public string Name
		{
			get
			{
				MyTutorDTO item = base.Item;
				bool flag = ((item != null) ? item.Tutor : null) == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = base.Item.Tutor.GetName();
				}
				return result;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D5D3 File Offset: 0x0000B7D3
		public string Summary
		{
			get
			{
				MyTutorDTO item = base.Item;
				return (((item != null) ? item.Tutor : null) == null) ? "" : (base.Item.Tutor.Specializations ?? "");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000D60C File Offset: 0x0000B80C
		public DateTime? LastMetWith
		{
			get
			{
				MyTutorDTO item = base.Item;
				return (item != null) ? new DateTime?(item.LastDateMetWith) : null;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000D638 File Offset: 0x0000B838
		public string AppointmentActivity
		{
			get
			{
				DateTime? lastMetWith = this.LastMetWith;
				bool flag = lastMetWith == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = lastMetWith.Value > DateTime.Now;
					if (flag2)
					{
						result = "Upcoming appointment on " + lastMetWith.Value.ToString("ddd MMM d, yyyy") + " - " + lastMetWith.Value.ToString("h:mm tt");
					}
					else
					{
						result = "Last appointment on " + lastMetWith.Value.ToString("ddd MMM d, yyyy") + " - " + lastMetWith.Value.ToString("h:mm tt");
					}
				}
				return result;
			}
		}
	}
}

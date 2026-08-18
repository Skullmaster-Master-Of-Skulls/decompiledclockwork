using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace ClockWorkAPI.EntityExtensions
{
	// Token: 0x02000015 RID: 21
	public class AppointmentExt
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00004BBC File Offset: 0x00003BBC
		public AppointmentExt()
		{
			this.AttendeeExts = new Dictionary<int, AttendeeExt>();
			this.AccObj = null;
			this.Ctrl = null;
			this.IsSelected = false;
			this.IsMouseOver = false;
			this.AppointmentRectangle = new Rectangle(-1, -1, 0, 0);
			this.Name = "";
			this.AppointmentCommand = AppointmentCommand.None;
			this.PERMISSIONS_DetailsVisible = true;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004C2C File Offset: 0x00003C2C
		public AppointmentExt(AppointmentDTO parent)
		{
			this.AttendeeExts = new Dictionary<int, AttendeeExt>();
			this.AccObj = null;
			this.Ctrl = null;
			this.IsSelected = false;
			this.IsMouseOver = false;
			this.parent = parent;
			this.AppointmentRectangle = new Rectangle(-1, -1, 0, 0);
			this.Name = "";
			this.AppointmentCommand = AppointmentCommand.None;
			this.OverrideColourActive = (parent.OverrideColour != null);
			if (this.OverrideColourActive)
			{
				try
				{
					this.OverrideColour = Color.FromArgb(parent.OverrideColour.Value);
				}
				catch
				{
				}
			}
			if (parent.IsPrivate)
			{
				if (this.IsWhoAmIBookedOrInAppointment(parent))
				{
					this.PERMISSIONS_DetailsVisible = true;
				}
				else
				{
					this.PERMISSIONS_DetailsVisible = false;
				}
			}
			else
			{
				this.PERMISSIONS_DetailsVisible = true;
			}
			if (parent.IsLocked)
			{
				if (this.IsWhoAmIBookedOrInAppointment(parent))
				{
					this.PERMISSIONS_Locked = false;
				}
				else
				{
					this.PERMISSIONS_Locked = true;
				}
			}
			else
			{
				this.PERMISSIONS_Locked = false;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004D98 File Offset: 0x00003D98
		private bool IsWhoAmIBookedOrInAppointment(AppointmentDTO app)
		{
			int whoAmIPid = ClientCache.CurrentInstance.whoAmIId;
			return (app.WhoBooked != null && app.WhoBooked.PersonId == whoAmIPid) || (app.Attendees != null && app.Attendees.Find((AttendeeDTO att) => att.Person.PersonId == whoAmIPid) != null);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004E08 File Offset: 0x00003E08
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004E1F File Offset: 0x00003E1F
		public AppointmentCommand AppointmentCommand { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004E28 File Offset: 0x00003E28
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004E3F File Offset: 0x00003E3F
		public Control Schedule1DayControl { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004E48 File Offset: 0x00003E48
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004E5F File Offset: 0x00003E5F
		public bool PERMISSIONS_DetailsVisible { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004E68 File Offset: 0x00003E68
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004E7F File Offset: 0x00003E7F
		public bool PERMISSIONS_Locked { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004E88 File Offset: 0x00003E88
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004E9F File Offset: 0x00003E9F
		public object Ctrl { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004EA8 File Offset: 0x00003EA8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004EBF File Offset: 0x00003EBF
		public AccessibleObject AccObj { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004EC8 File Offset: 0x00003EC8
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004EDF File Offset: 0x00003EDF
		public Rectangle AppointmentRectangle { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004EE8 File Offset: 0x00003EE8
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004EFF File Offset: 0x00003EFF
		public string Name { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004F08 File Offset: 0x00003F08
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004F1F File Offset: 0x00003F1F
		public Color BackColour { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004F28 File Offset: 0x00003F28
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004F3F File Offset: 0x00003F3F
		public Color OverrideColour { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004F48 File Offset: 0x00003F48
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004F5F File Offset: 0x00003F5F
		public bool OverrideColourActive { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004F68 File Offset: 0x00003F68
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004F7F File Offset: 0x00003F7F
		public Color ForeColour { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004F88 File Offset: 0x00003F88
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00004F9F File Offset: 0x00003F9F
		public Color OverrideForeColour { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004FA8 File Offset: 0x00003FA8
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004FBF File Offset: 0x00003FBF
		public bool DefaultColourSet { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004FC8 File Offset: 0x00003FC8
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004FDF File Offset: 0x00003FDF
		public bool IsSelected { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004FE8 File Offset: 0x00003FE8
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004FFF File Offset: 0x00003FFF
		public bool IsMouseOver { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005008 File Offset: 0x00004008
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000501F File Offset: 0x0000401F
		public int LastCalculatedY1 { get; set; }

		// Token: 0x060000AF RID: 175 RVA: 0x00005028 File Offset: 0x00004028
		public AttendeeExt GetAttendeeExt(int attendeeId)
		{
			if (this.AttendeeExts == null)
			{
				this.AttendeeExts = new Dictionary<int, AttendeeExt>();
			}
			AttendeeExt result;
			if (this.AttendeeExts.ContainsKey(attendeeId))
			{
				result = this.AttendeeExts[attendeeId];
			}
			else
			{
				AttendeeExt attendeeExt = new AttendeeExt();
				this.AttendeeExts.Add(attendeeId, attendeeExt);
				result = attendeeExt;
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005110 File Offset: 0x00004110
		public override string ToString()
		{
			string result;
			if (this.parent == null)
			{
				result = "---";
			}
			else
			{
				string appTypeDescription = this.parent.GetAppTypeDescription();
				string text = this.parent.StartDateTime.ToString("MMMM dd, yyyy (ddd) hh:mm tt");
				text = text + " - " + this.parent.EndDateTime.ToString("hh:mm tt");
				text = text + " [" + appTypeDescription + "]";
				List<AttendeeDTO> list = this.parent.Attendees.FindAll((AttendeeDTO a) => a.Person.CoreGroup == eCoreGroupDTO.Admin || a.Person.CoreGroup == eCoreGroupDTO.Staff || a.Person.CoreGroup == eCoreGroupDTO.Students);
				string str = string.Join(", ", list.ConvertAll<string>((AttendeeDTO att) => att.Person.GetName()).ToArray());
				AttendeeDTO attendeeDTO = this.parent.Attendees.Find((AttendeeDTO att) => att.Person.CoreGroup == eCoreGroupDTO.Rooms);
				string text2 = (attendeeDTO == null) ? "" : attendeeDTO.Person.FirstName;
				text = text + " {" + str + "}";
				if (!string.IsNullOrEmpty(text2))
				{
					text = text + " Room: " + text2;
				}
				string memoPlainText = Utility.GetMemoPlainText(this.parent);
				if (!string.IsNullOrEmpty(memoPlainText))
				{
					text = text + " NOTE: " + memoPlainText;
				}
				result = text;
			}
			return result;
		}

		// Token: 0x0400004C RID: 76
		private string name;

		// Token: 0x0400004D RID: 77
		private string accessibleName;

		// Token: 0x0400004E RID: 78
		private string accessibleDescription;

		// Token: 0x0400004F RID: 79
		public Dictionary<int, AttendeeExt> AttendeeExts;

		// Token: 0x04000050 RID: 80
		private AppointmentDTO parent;
	}
}

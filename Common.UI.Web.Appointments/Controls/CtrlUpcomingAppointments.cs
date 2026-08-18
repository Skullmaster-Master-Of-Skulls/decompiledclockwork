using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.ClientManager.Core.AppointmentsList;
using TechnoPro.Common.ClientManager.ICore.AppointmentsList;
using TechnoPro.Common.UI.Web.Appointments.Entity;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.Web.Appointments.Controls
{
	// Token: 0x02000006 RID: 6
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlUpcomingAppointments runat=server></{0}:CtrlUpcomingAppointments>")]
	public class CtrlUpcomingAppointments : WebControl, INamingContainer
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002233 File Offset: 0x00000433
		public override void Dispose()
		{
			if (this.grid != null)
			{
				this.grid.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000224E File Offset: 0x0000044E
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002256 File Offset: 0x00000456
		public int Pid { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x0000225F File Offset: 0x0000045F
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000226C File Offset: 0x0000046C
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000227A File Offset: 0x0000047A
		protected override void RenderContents(HtmlTextWriter output)
		{
			this.grid.RenderControl(output);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002288 File Offset: 0x00000488
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
			this.grid.AddBoundColumn("col_datetime", "Date and Time", "DisplayDateAndTime", "DisplayDateAndTime");
			this.grid.AddBoundColumn("col_location", "Location", "DisplayLocation", "DisplayLocation");
			this.grid.AddBoundColumn("col_title", "Title", "DisplayTitle", "DisplayTitle");
			this.grid.AddBoundColumn("col_who", "Who", "DisplayWho", "DisplayWho");
			this.grid.AddBoundColumn("col_action", "Action", "", "Action");
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002340 File Offset: 0x00000540
		private void InitializeControls()
		{
			this.grid.OnItemCreated += this.grid_ItemCreated;
			this.grid.OnNeedDataSource += this.grid_NeedDataSource;
			this.grid.OnItemCommand += this.grid_ItemCommand;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002392 File Offset: 0x00000592
		private void grid_ItemCommand(object sender, GridCommandEventArgs e)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002394 File Offset: 0x00000594
		private void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			List<ListAppointmentDTO> list = ((IListAppointmentClientManager)new ListAppointmentClientManager()).LoadAppointments(new List<int>
			{
				this.Pid
			}, DateTime.Now.Date.AddDays(-7.0), 365, false).ToList<ListAppointmentDTO>().FindAll((ListAppointmentDTO g) => !g.IsCancelled);
			List<UpcomingAppointmentView> list2 = new List<UpcomingAppointmentView>();
			foreach (ListAppointmentDTO listAppointmentDTO in list)
			{
				list2.Add(new UpcomingAppointmentView
				{
					DisplayDateAndTime = string.Format("<b>{0}</b><br /><span style='font-size: .8em;'><i>{1}</i></span>", listAppointmentDTO.StartDateTime.ToString("ddd MMMM d yyyy"), listAppointmentDTO.StartDateTime.ToString("h:mmtt")),
					DisplayLocation = (listAppointmentDTO.Location ?? ""),
					DisplayTitle = "Tutoring session",
					DisplayWho = ((listAppointmentDTO.Staff == null) ? "" : listAppointmentDTO.Staff.GetName())
				});
			}
			this.grid.DataSource = list2;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002392 File Offset: 0x00000592
		private void grid_ItemCreated(object sender, GridItemEventArgs e)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024DC File Offset: 0x000006DC
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.grid);
		}

		// Token: 0x04000014 RID: 20
		private CtrlWebGrid grid = new CtrlWebGrid();
	}
}

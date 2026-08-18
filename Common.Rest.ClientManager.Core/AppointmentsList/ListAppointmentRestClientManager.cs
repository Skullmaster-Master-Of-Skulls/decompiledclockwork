using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsList;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsList
{
	// Token: 0x02000082 RID: 130
	public class ListAppointmentRestClientManager : BearerTokenRestProxy<IListAppointmentClientManager>, IListAppointmentClientManager, IWebService
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0000DFBD File Offset: 0x0000C1BD
		public ListAppointmentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		public ListAppointmentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		public int CreateListAppointment(ListAppointmentDTO Appointment)
		{
			int result = base.Post<ListAppointmentDTO, int>(Appointment, "listappointment");
			Task.Run(() => this.NotifyOthersAppointmentWasCreatedAsync(Appointment));
			return result;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000E018 File Offset: 0x0000C218
		public void CancelListAppointment(int AppointmentId)
		{
			base.Post(string.Format("listappointment/cancel/appid/{0}", AppointmentId));
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false)));
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000E068 File Offset: 0x0000C268
		public void UnCancelListAppointment(int AppointmentId)
		{
			base.Post(string.Format("listappointment/uncancel/appid/{0}", AppointmentId));
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false)));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		public void MarkListAppointmentAsTentative(int Appointmentid)
		{
			base.Post(string.Format("listappointment/markastentative/appid/{0}", Appointmentid));
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(Appointmentid, false)));
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000E108 File Offset: 0x0000C308
		public void UnMarkListAppointmentAsTentative(int Appointmentid)
		{
			base.Post(string.Format("listappointment/unmarkastentative/appid/{0}", Appointmentid));
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(Appointmentid, false)));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000E158 File Offset: 0x0000C358
		public void DeleteListAppointment(int AppointmentId)
		{
			base.Delete(string.Format("listappointment/appid/{0}", AppointmentId));
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false)));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public void UpdateListAppointment(ListAppointmentDTO Appointment)
		{
			base.Put<ListAppointmentDTO>(Appointment, "listappointment");
			IEnumerable<AttendeeDTO> source = from f in this.LoadAppointmentById(Appointment.AppointmentId, false).Attendees
			where Appointment.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == f.Person.PersonId) == null
			select f;
			Appointment.Attendees.AddRange(source.ToList<AttendeeDTO>());
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(Appointment));
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000E22C File Offset: 0x0000C42C
		public void CreateAvailabilities(List<Availability2ItemDTO> Availabilities)
		{
			CreateAvailabilitiesReq createAvailabilitiesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAvailabilitiesReq>();
			createAvailabilitiesReq.Availabilities = Availabilities;
			base.Post<CreateAvailabilitiesReq>(createAvailabilitiesReq, "listappointment/listappointment");
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000E257 File Offset: 0x0000C457
		public void DeleteAvailability(List<int> AvailabilityIds)
		{
			base.Delete(string.Format("listappointment/ids/{0}", AvailabilityIds.CommaSeparatedValuesWithoutSpace<int>()));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000E270 File Offset: 0x0000C470
		public void UpdateAvailability(List<Availability2ItemDTO> Availabilities)
		{
			UpdateAvailabilityReq updateAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAvailabilityReq>();
			updateAvailabilityReq.Availabilities = Availabilities;
			base.Put<UpdateAvailabilityReq>(updateAvailabilityReq, "listappointment/availabilities");
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000E29B File Offset: 0x0000C49B
		public IList<Availability2ItemDTO> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime)
		{
			return base.GetMany<Availability2ItemDTO>(string.Format("listappointment/overlappingavailabilities/pid/{0}/range/{1}/{2}", PersonId, StartDateTime, EndDateTime), true);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		public IList<Availability2ItemDTO> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime)
		{
			return base.GetMany<Availability2ItemDTO>(string.Format("listappointment/freetimesearch/pids/{0}/range/{1}/{2}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), StartDateTime, EndDateTime), true);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000E2E5 File Offset: 0x0000C4E5
		public IList<ClosedDayDTO> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<ClosedDayDTO>(string.Format("listappointment/closeddays/pids/{0}/range/{1}/{2}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), StartDate, EndDate), true);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000E30A File Offset: 0x0000C50A
		public ClosedDayDTO IsDayClosed(int PersonId, DateTime Date)
		{
			return base.Get<ClosedDayDTO>(string.Format("listappointment/isdayclosed/pid/{0}/date/{1}", PersonId, Date), true);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000E32C File Offset: 0x0000C52C
		public void CreateClosedDay(IList<ClosedDayDTO> ClosedDays)
		{
			CreateClosedDayReq createClosedDayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateClosedDayReq>();
			createClosedDayReq.ClosedDays = ClosedDays;
			base.Post<CreateClosedDayReq>(createClosedDayReq, "listappointment/closeddays");
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000E357 File Offset: 0x0000C557
		public void DeleteClosedDay(int PersonId, DateTime Date)
		{
			base.Delete(string.Format("listappointment/closedday/pid/{0}/date/{1}", PersonId, Date));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000E375 File Offset: 0x0000C575
		public IList<Availability2ItemDTO> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays)
		{
			return base.GetMany<Availability2ItemDTO>(string.Format("listappointment/availabilities/pids/{0}/start/{1}/numdays/{2}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), StartDate, NumDays), true);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000E39A File Offset: 0x0000C59A
		public IList<ListAppointmentDTO> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment)
		{
			return base.GetMany<ListAppointmentDTO>(string.Format("listappointment/pids/{0}/start/{1}/numdays/{2}?isstudentfirstapp={3}", new object[]
			{
				PersonIds.CommaSeparatedValuesWithoutSpace<int>(),
				StartDate,
				NumDays,
				LoadIsStudentsFirstAppointment
			}), true);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		public IList<ListAppointmentOrAvailabilityDTO> LoadAppointmentsWithAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments)
		{
			return base.GetMany<ListAppointmentOrAvailabilityDTO>(string.Format("listappointment/withavailability/pids/{0}/start/{1}/numdays/{2}?isstudentsfirstappointment={3}&hidecancelled={4}", new object[]
			{
				PersonIds.CommaSeparatedValuesWithoutSpace<int>(),
				StartDate,
				NumDays,
				LoadIsStudentsFirstAppointment,
				HideCancelledAppointments
			}), true);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000E42C File Offset: 0x0000C62C
		public BinaryFileDTO PrintMedicalCalendar(DateTime StartDate, int NumDays, IList<PersonBaseDTO> Staff, eFileFormatDTO OutputFormat, bool HideCancelled)
		{
			PrintMedicalCalendarReq printMedicalCalendarReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<PrintMedicalCalendarReq>();
			BaseReportMessageReq baseReportMessageReq = printMedicalCalendarReq;
			ApplicationContext applicationContext = printMedicalCalendarReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			printMedicalCalendarReq.StartDate = StartDate;
			printMedicalCalendarReq.NumDays = NumDays;
			printMedicalCalendarReq.Staff = Staff;
			printMedicalCalendarReq.OutputFormat = OutputFormat;
			printMedicalCalendarReq.HideCancelled = HideCancelled;
			return base.Post<PrintMedicalCalendarReq, BinaryFileDTO>(printMedicalCalendarReq, "listappointment/printmedicalcalendar");
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000E48D File Offset: 0x0000C68D
		public ListAppointmentDTO LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment = false)
		{
			return base.Get<ListAppointmentDTO>(string.Format("listappointment/appid/{0}?isstudentfirstappointment={1}", AppointmentId, LoadIsStudentsFirstAppointment), true);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		public void MarkIn(int AppointmentId, bool newIn)
		{
			MarkInReq markInReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkInReq>();
			markInReq.AppointmentId = AppointmentId;
			markInReq.NewIn = newIn;
			base.Post<MarkInReq>(markInReq, "listappointment/markin");
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false)));
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000E50C File Offset: 0x0000C70C
		public void MarkNoShow(int AppointmentId, bool newNoShow)
		{
			MarkNoShowReq markNoShowReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkNoShowReq>();
			markNoShowReq.AppointmentId = AppointmentId;
			markNoShowReq.NewNoShow = newNoShow;
			base.Post<MarkNoShowReq>(markNoShowReq, "listappointment/marknoshow");
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false)));
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000E56C File Offset: 0x0000C76C
		public void MarkConfirmed(int AppointmentId, bool newConfirmed)
		{
			MarkConfirmedReq markConfirmedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkConfirmedReq>();
			markConfirmedReq.AppointmentId = AppointmentId;
			markConfirmedReq.NewConfirmed = newConfirmed;
			base.Post<MarkConfirmedReq>(markConfirmedReq, "listappointment/markconfirmed");
			Task.Run(() => this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false)));
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		public Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays)
		{
			return base.Get<LoadSingleDayAvailabilityStatusesByUserResp>(string.Format("listappointment/singledayavailabilitystatuses/pid/{0}/start/{1}/numdays/{2}", PersonId, StartDate, NumDays), true).Items;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText)
		{
			InsertOrUpdateAppointmentMemoReq insertOrUpdateAppointmentMemoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentMemoReq>();
			insertOrUpdateAppointmentMemoReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentMemoReq.MemoText = MemoText;
			base.Post<InsertOrUpdateAppointmentMemoReq>(insertOrUpdateAppointmentMemoReq, "listappointment/appointmentmemo");
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000E628 File Offset: 0x0000C828
		public void FixAvailabilityAppointmentMappings(DateTime StartDate, DateTime EndDate)
		{
			FixAvailabilityAppointmentMappingsReq fixAvailabilityAppointmentMappingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FixAvailabilityAppointmentMappingsReq>();
			fixAvailabilityAppointmentMappingsReq.StartDate = StartDate;
			fixAvailabilityAppointmentMappingsReq.EndDate = EndDate;
			base.Post<FixAvailabilityAppointmentMappingsReq>(fixAvailabilityAppointmentMappingsReq, "listappointment/fixavailabilityappointmentmappgins");
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000E65A File Offset: 0x0000C85A
		public IList<Availability2MarkerDTO> LoadAvailability2Markers()
		{
			return base.GetMany<Availability2MarkerDTO>("listappointment/availability2markers", true);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000E668 File Offset: 0x0000C868
		public int CreateAvailability2Marker(Availability2MarkerDTO Marker)
		{
			return base.Post<Availability2MarkerDTO, int>(Marker, "listappointment/availability2markers");
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E676 File Offset: 0x0000C876
		public void DeleteAvailability2Marker(int Availability2MarkerId)
		{
			base.Delete(string.Format("listappointment/availability2markers/{0}", Availability2MarkerId));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E68E File Offset: 0x0000C88E
		public void UpdateAvailability2Marker(Availability2MarkerDTO Marker)
		{
			base.Put<Availability2MarkerDTO>(Marker, "listappointment/availability2markers");
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000E69C File Offset: 0x0000C89C
		private async Task NotifyOthersAppointmentChangedOrDeletedAsync(ListAppointmentDTO Appointment)
		{
			AppointmentNotificationManager currentInstance = AppointmentNotificationManager.CurrentInstance;
			AppNotificationMessage appNotificationMessage = new AppNotificationMessage();
			appNotificationMessage.Code = eAppNotificationMessageCode.AppointmentCreateEnded;
			List<BasicAppointmentInfo> list = new List<BasicAppointmentInfo>();
			BasicAppointmentInfo basicAppointmentInfo = new BasicAppointmentInfo();
			List<int> attendeePersonIds;
			if (Appointment.Staff != null)
			{
				(attendeePersonIds = new List<int>()).Add(Appointment.Staff.PersonId);
			}
			else
			{
				attendeePersonIds = new List<int>();
			}
			basicAppointmentInfo.AttendeePersonIds = attendeePersonIds;
			basicAppointmentInfo.AppointmentId = Appointment.AppointmentId;
			basicAppointmentInfo.StartDateTime = Appointment.StartDateTime;
			basicAppointmentInfo.EndDateTime = Appointment.EndDateTime;
			list.Add(basicAppointmentInfo);
			appNotificationMessage.AppInfos = list;
			await currentInstance.NotifyAsync(appNotificationMessage, null);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		private async Task NotifyOthersAppointmentWasCreatedAsync(ListAppointmentDTO Appointment)
		{
			AppointmentNotificationManager currentInstance = AppointmentNotificationManager.CurrentInstance;
			AppNotificationMessage appNotificationMessage = new AppNotificationMessage();
			appNotificationMessage.Code = eAppNotificationMessageCode.AppointmentCreateEnded;
			List<BasicAppointmentInfo> list = new List<BasicAppointmentInfo>();
			BasicAppointmentInfo basicAppointmentInfo = new BasicAppointmentInfo();
			List<int> attendeePersonIds;
			if (Appointment.Staff != null)
			{
				(attendeePersonIds = new List<int>()).Add(Appointment.Staff.PersonId);
			}
			else
			{
				attendeePersonIds = new List<int>();
			}
			basicAppointmentInfo.AttendeePersonIds = attendeePersonIds;
			basicAppointmentInfo.AppointmentId = Appointment.AppointmentId;
			basicAppointmentInfo.StartDateTime = Appointment.StartDateTime;
			basicAppointmentInfo.EndDateTime = Appointment.EndDateTime;
			list.Add(basicAppointmentInfo);
			appNotificationMessage.AppInfos = list;
			await currentInstance.NotifyAsync(appNotificationMessage, null);
		}
	}
}

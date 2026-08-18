using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Databases;
using TechnoPro.Common.Core.AlertTrigger;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.AlertTrigger;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Startup;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Startup;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.Core.Startup
{
	// Token: 0x02000041 RID: 65
	public class ClockWorkClientStartupManager : IClockWorkClientStartupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000FF44 File Offset: 0x0000E144
		public ClockWorkClientStartupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000FF56 File Offset: 0x0000E156
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000FF5E File Offset: 0x0000E15E
		public OperationContext OpContext { get; set; }

		// Token: 0x060002AC RID: 684 RVA: 0x0000FF68 File Offset: 0x0000E168
		public ClockWorkClientStartup GetClockWorkClientStartup(int PersonId)
		{
			bool flag = PersonId < 1;
			if (flag)
			{
				PersonId = this.OpContext.WhoAmI;
			}
			bool flag2 = this.OpContext.WhoAmI != PersonId;
			if (flag2)
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				bool flag3 = peopleGroupManager.IsAdmin(this.OpContext.WhoAmI);
				bool flag4 = !flag3;
				if (flag4)
				{
					throw new PermissionDeniedException(string.Format("ClockWorkCLientStartupManager:GetClockWorkClientStartup:User {0} asking for User {1}", this.OpContext.WhoAmI.ToString(), PersonId.ToString()));
				}
			}
			ISessionManager sessionManager = new SessionManager(this.OpContext);
			DateTime? sessionChooserDefaultValue = sessionManager.GetSessionChooserDefaultValue();
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			List<PersonBase> rooms = peopleManager.LoadRooms();
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IList<DynamicFormWithExtendedInfo> screens = dynamicFormManager.LoadActiveFormsWithExtendedInfo();
			IAcademicTermManager academicTermManager = new AcademicTermManager(this.OpContext);
			IList<AcademicTerm> sessions = academicTermManager.LoadAcademicTerms(false);
			IPermissionManager permissionManager = new PermissionManager(this.OpContext);
			UserPermissionIsAllowedSet userPermissionIsAllowedSet = permissionManager.LoadUserPermissionSet(this.OpContext.WhoAmI, true);
			IAlertTriggerManager alertTriggerManager = new AlertTriggerManager(this.OpContext);
			alertTriggerManager.ClearAlertTriggersForCurrentUser();
			IAlertTriggerDefinition[] alertTriggersForCurrentUser = alertTriggerManager.GetAlertTriggersForCurrentUser();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string encryptionPassword = databaseLayer.EncryptionPassword;
			byte[] bytes = Encoding.UTF8.GetBytes(encryptionPassword);
			int num = bytes.Length / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[bytes.Length - num];
			Array.Copy(bytes, 0, array, 0, num);
			Array.Copy(bytes, num, array2, 0, array2.Length);
			ClockWorkClientStartup clockWorkClientStartup = new ClockWorkClientStartup();
			clockWorkClientStartup.SessionChooserDefaultValue = sessionChooserDefaultValue;
			clockWorkClientStartup.Rooms = rooms;
			clockWorkClientStartup.Screens = screens;
			clockWorkClientStartup.Sessions = sessions;
			clockWorkClientStartup.UserPermissionIsAllowedSet = userPermissionIsAllowedSet;
			clockWorkClientStartup.UseAlertTriggerSystem = (alertTriggersForCurrentUser != null && alertTriggersForCurrentUser.Length != 0);
			bool? flag5;
			if (alertTriggersForCurrentUser == null)
			{
				flag5 = null;
			}
			else
			{
				IAlertTriggerDefinition alertTriggerDefinition = alertTriggersForCurrentUser.FirstOrDefault((IAlertTriggerDefinition g) => g.DontAllowAppointmentBooking);
				flag5 = ((alertTriggerDefinition != null) ? new bool?(alertTriggerDefinition.DontAllowAppointmentBooking) : null);
			}
			bool? flag6 = flag5;
			clockWorkClientStartup.AnyAlertTriggerDontAllowAppointmentBookingItems = flag6.GetValueOrDefault();
			clockWorkClientStartup.ServerNonce = array;
			clockWorkClientStartup.DefaultBackGroundImage = array2;
			clockWorkClientStartup.ServerCNonce = (int)databaseLayer.Encryption.Name;
			return clockWorkClientStartup;
		}
	}
}

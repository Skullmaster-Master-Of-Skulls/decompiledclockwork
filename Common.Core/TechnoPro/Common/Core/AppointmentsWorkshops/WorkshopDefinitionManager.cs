using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AppointmentsWorkshops;
using TechnoPro.Common.DAO.Impl.AppointmentsWorkshops;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AppointmentsWorkshops
{
	// Token: 0x02000136 RID: 310
	public class WorkshopDefinitionManager : IWorkshopDefinitionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00061AB7 File Offset: 0x0005FCB7
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x00061ABF File Offset: 0x0005FCBF
		public IWorkshopDefinitionDAO dao { get; set; }

		// Token: 0x06000D6D RID: 3437 RVA: 0x00061AC8 File Offset: 0x0005FCC8
		public WorkshopDefinitionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new WorkshopDefinitionDAO(this.OpContext);
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00061AEC File Offset: 0x0005FCEC
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00061AF4 File Offset: 0x0005FCF4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000D70 RID: 3440 RVA: 0x00061B00 File Offset: 0x0005FD00
		private Forest<WorkshopDefinitionOrAppType> ToForest(IList<AppType> workshopAppTypes, IList<WorkshopDefinition> items)
		{
			Forest<WorkshopDefinitionOrAppType> forest = new Forest<WorkshopDefinitionOrAppType>();
			bool flag = items == null || items.Count < 1;
			Forest<WorkshopDefinitionOrAppType> result;
			if (flag)
			{
				result = forest;
			}
			else
			{
				foreach (WorkshopDefinition workshopDefinition in items)
				{
					AppType appTypeParent = workshopDefinition.AppTypeParent;
					int id = (appTypeParent != null) ? appTypeParent.AppTypeId : 0;
					bool flag2 = id > 0 && workshopAppTypes.FirstOrDefault((AppType a) => a.AppTypeId == id) == null;
					if (flag2)
					{
						workshopAppTypes.Add(appTypeParent);
					}
				}
				List<WorkshopDefinition> list = items.ToList<WorkshopDefinition>();
				using (IEnumerator<AppType> enumerator2 = workshopAppTypes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						AppType uniqueAppType = enumerator2.Current;
						TreeNode<WorkshopDefinitionOrAppType> treeNode = forest.AppendNode(null, new WorkshopDefinitionOrAppType
						{
							AppType = uniqueAppType
						});
						List<WorkshopDefinition> list2 = list.FindAll((WorkshopDefinition w) => w.AppTypeParent != null && w.AppTypeParent.AppTypeId == uniqueAppType.AppTypeId);
						foreach (WorkshopDefinition workshopDefinition2 in list2)
						{
							treeNode.AppendNode(new WorkshopDefinitionOrAppType
							{
								WorkshopDefinition = workshopDefinition2
							}, treeNode);
						}
					}
				}
				result = forest;
			}
			return result;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00061CA8 File Offset: 0x0005FEA8
		public IList<WorkshopDefinition> LoadWorkshopDefinitions()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_ButtonHide_Workshops);
			bool flag = settingValue_Bool;
			IList<WorkshopDefinition> result;
			if (flag)
			{
				result = new List<WorkshopDefinition>();
			}
			else
			{
				IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
				List<int> allowedAppointmentTypes = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI).ToList<int>();
				result = this.dao.LoadWorkshopDefinitions(allowedAppointmentTypes);
			}
			return result;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00061D1F File Offset: 0x0005FF1F
		public void DeleteWorkshopDefinition(int workshopEventId)
		{
			this.dao.DeleteWorkshopDefinition(workshopEventId);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00061D30 File Offset: 0x0005FF30
		public int CreateWorkshopDefinition(WorkshopDefinition workshopDefinition)
		{
			return this.dao.CreateWorkshopDefinition(workshopDefinition);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00061D4E File Offset: 0x0005FF4E
		public void UpdateWorkshopDefinition(WorkshopDefinition workshopDefinition)
		{
			this.dao.UpdateWorkshopDefinition(workshopDefinition);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00061D60 File Offset: 0x0005FF60
		public List<AppType> LoadAllWorkshopAppTypes()
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			return (from at in appointmentTypeManager.LoadAllAppTypes()
			where at.IsWorkshop
			select at).ToList<AppType>();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00061DB0 File Offset: 0x0005FFB0
		public WorkshopDefinition LoadWorkshopDefinition(int workshopDefinitionId)
		{
			return this.dao.LoadWorkshopDefinitionById(workshopDefinitionId);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00061DD0 File Offset: 0x0005FFD0
		public IList<WorkshopDefinition> LoadWorkshopDefinitionsByAppType(int appTypeId)
		{
			return this.dao.LoadWorkshopDefinitions(new List<int>
			{
				appTypeId
			});
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00061DFC File Offset: 0x0005FFFC
		public Forest<WorkshopDefinitionOrAppType> LoadAppTypesWithWorkshopDefinitions()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_ButtonHide_Workshops);
			bool flag = settingValue_Bool;
			Forest<WorkshopDefinitionOrAppType> result;
			if (flag)
			{
				CWLogger.Logger.Warn("Common.Core.AppointmentsWorkshops.WorkshopDefinitionManager:LoadAppTypesWithWorkshopDefinitions:User not allowed to view workshops");
				result = new Forest<WorkshopDefinitionOrAppType>();
			}
			else
			{
				IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
				List<int> appTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI).ToList<int>();
				IList<WorkshopDefinition> items = this.dao.LoadWorkshopDefinitions(appTypeIds);
				List<AppType> source = appointmentTypeManager.LoadAllAppTypes();
				List<AppType> list = (from g in source
				where g.IsWorkshop && appTypeIds.Contains(g.AppTypeId)
				select g).ToList<AppType>();
				list.Sort((AppType w1, AppType w2) => (w1.Description ?? "").CompareTo(w2.Description ?? ""));
				result = this.ToForest(list, items);
			}
			return result;
		}
	}
}

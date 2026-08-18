using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.DataSync;
using TechnoPro.Common.DAO.Impl.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses.ExtendedDataSyncData;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x02000108 RID: 264
	public class DataSyncCourseExtendedDataManager : IDataSyncCourseExtendedDataManager, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0004527C File Offset: 0x0004347C
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00045284 File Offset: 0x00043484
		internal IDataSyncCourseExtendedDataDAO dao { get; set; }

		// Token: 0x06000ABE RID: 2750 RVA: 0x0004528D File Offset: 0x0004348D
		public DataSyncCourseExtendedDataManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DataSyncCourseExtendedDataDAO(opContext);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000452AC File Offset: 0x000434AC
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x000452B4 File Offset: 0x000434B4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000072EA File Offset: 0x000054EA
		public CourseExtendedDataSyncDataItems LoadCourseExtendedDataSyncDataByLuCourseId(int lucid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000072EA File Offset: 0x000054EA
		public IDictionary<int, CourseExtendedDataSyncDataItems> LoadCourseExtendedDataSyncDataByLuCourseIds(int[] lucids)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000452C0 File Offset: 0x000434C0
		public IList<CourseExtendedDataSyncField> LoadCourseExtendedDataSyncFields()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<CourseExtendedDataSyncField> list = (IList<CourseExtendedDataSyncField>)cacheStorageManager["courseExtendedDataSyncFields"];
			bool flag = list != null;
			IList<CourseExtendedDataSyncField> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				list = this.dao.LoadCourseExtendedDataSyncFields();
				cacheStorageManager.Insert("courseExtendedDataSyncFields", list, TimeSpan.FromMinutes(60.0));
				result = list;
			}
			return result;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0004531D File Offset: 0x0004351D
		public void OverwriteCourseExtendedData(int lucid, CourseExtendedDataSyncDataItems dataItems)
		{
			this.dao.OverwriteCourseExtendedData(lucid, this.LoadCourseExtendedDataSyncFields(), dataItems);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00045334 File Offset: 0x00043534
		public void DeleteCourseExtendedDataSyncField(int ControlId)
		{
			this.dao.DeleteCourseExtendedDataSyncField(ControlId);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00045344 File Offset: 0x00043544
		public void UpdateCourseExtendedDataSyncField(CourseExtendedDataSyncField field)
		{
			this.dao.UpdateCourseExtendedDataSyncField(field);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00045354 File Offset: 0x00043554
		public int AddCourseExtendedDataSyncField(CourseExtendedDataSyncField field)
		{
			return this.dao.AddCourseExtendedDataSyncField(field);
		}
	}
}

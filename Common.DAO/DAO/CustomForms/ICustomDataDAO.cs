using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.Common.DAO.CustomForms
{
	// Token: 0x02000092 RID: 146
	public interface ICustomDataDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003BF RID: 959
		Task<CustomDataSet> LoadPerStudentDataAsync(int personId, params Guid[] dataInstanceIds);

		// Token: 0x060003C0 RID: 960
		Task<CustomDataSet> LoadPerSemesterDataAsync(int personId, int semesterId, params Guid[] dataInstanceIds);

		// Token: 0x060003C1 RID: 961
		Task<CustomDataSet> LoadPerDateDataAsync(int personId, int customDataPerDateId, params Guid[] dataInstanceIds);

		// Token: 0x060003C2 RID: 962
		CustomDataSet LoadPerStudentData(int personId, params Guid[] dataInstanceIds);

		// Token: 0x060003C3 RID: 963
		CustomDataSet LoadPerSemesterData(int personId, int semesterId, params Guid[] dataInstanceIds);

		// Token: 0x060003C4 RID: 964
		CustomDataSet LoadPerDateData(int personId, int customDataPerDateId, params Guid[] dataInstanceIds);

		// Token: 0x060003C5 RID: 965
		Task WritePerStudentDataAsync(int personId, params CustomDataSerialized[] serializedDatas);

		// Token: 0x060003C6 RID: 966
		Task WritePerSemesterDataAsync(int personId, int semesterId, params CustomDataSerialized[] serializedDatas);

		// Token: 0x060003C7 RID: 967
		Task WritePerDateDataAsync(int personId, int perDateId, params CustomDataSerialized[] serializedDatas);

		// Token: 0x060003C8 RID: 968
		Task ClearPerStudentDataAsync(int personId, params Guid[] dataInstanceIds);

		// Token: 0x060003C9 RID: 969
		Task ClearPerSemesterDataAsync(int personId, int semesterId, params Guid[] dataInstanceIds);

		// Token: 0x060003CA RID: 970
		Task ClearPerDateDataAsync(int personId, int perDateId, params Guid[] dataInstanceIds);
	}
}

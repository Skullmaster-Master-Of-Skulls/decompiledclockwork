using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.DAO.Impl.CustomForms;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.Core.CustomForms
{
	// Token: 0x02000114 RID: 276
	public class CustomDataManager : ICustomDataManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00052D83 File Offset: 0x00050F83
		public CustomDataManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00052D95 File Offset: 0x00050F95
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00052D9D File Offset: 0x00050F9D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00052DA8 File Offset: 0x00050FA8
		[DebuggerStepThrough]
		public Task<CustomDataSet> LoadDataAsync(CustomDataContext context, params Guid[] dataInstanceIds)
		{
			CustomDataManager.<LoadDataAsync>d__5 <LoadDataAsync>d__ = new CustomDataManager.<LoadDataAsync>d__5();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomDataSet>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.context = context;
			<LoadDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<CustomDataManager.<LoadDataAsync>d__5>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00052DFC File Offset: 0x00050FFC
		public CustomDataSet LoadData(CustomDataContext context, params Guid[] dataInstanceIds)
		{
			ICustomDataDAO customDataDAO = new CustomDataDAO(this.OpContext);
			CustomDataPerStudentContext customDataPerStudentContext = context as CustomDataPerStudentContext;
			bool flag = customDataPerStudentContext != null;
			CustomDataSet result;
			if (flag)
			{
				result = customDataDAO.LoadPerStudentData(customDataPerStudentContext.PersonId, dataInstanceIds);
			}
			else
			{
				CustomDataPerDateContext customDataPerDateContext = context as CustomDataPerDateContext;
				bool flag2 = customDataPerDateContext != null;
				if (flag2)
				{
					result = customDataDAO.LoadPerDateData(customDataPerDateContext.PersonId, customDataPerDateContext.CustomDataPerDateId, dataInstanceIds);
				}
				else
				{
					CustomDataPerSemesterContext customDataPerSemesterContext = context as CustomDataPerSemesterContext;
					bool flag3 = customDataPerSemesterContext != null;
					if (!flag3)
					{
						throw new NotImplementedException();
					}
					result = customDataDAO.LoadPerSemesterData(customDataPerSemesterContext.PersonId, customDataPerSemesterContext.SemesterId, dataInstanceIds);
				}
			}
			return result;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00052E90 File Offset: 0x00051090
		[DebuggerStepThrough]
		public Task SaveCustomFormsDataAsync(CustomDataSet dataSet, params Guid[] dataInstanceIds)
		{
			CustomDataManager.<SaveCustomFormsDataAsync>d__7 <SaveCustomFormsDataAsync>d__ = new CustomDataManager.<SaveCustomFormsDataAsync>d__7();
			<SaveCustomFormsDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveCustomFormsDataAsync>d__.<>4__this = this;
			<SaveCustomFormsDataAsync>d__.dataSet = dataSet;
			<SaveCustomFormsDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveCustomFormsDataAsync>d__.<>1__state = -1;
			<SaveCustomFormsDataAsync>d__.<>t__builder.Start<CustomDataManager.<SaveCustomFormsDataAsync>d__7>(ref <SaveCustomFormsDataAsync>d__);
			return <SaveCustomFormsDataAsync>d__.<>t__builder.Task;
		}
	}
}

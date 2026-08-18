using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Form;

namespace TechnoPro.Common.DAO.Impl.CustomForms
{
	// Token: 0x02000100 RID: 256
	public class CustomFormDAO : ICustomFormDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x0004B1C7 File Offset: 0x000493C7
		public CustomFormDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0004B1D9 File Offset: 0x000493D9
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0004B1E1 File Offset: 0x000493E1
		public OperationContext OpContext { get; set; }

		// Token: 0x0600074F RID: 1871 RVA: 0x0004B1EC File Offset: 0x000493EC
		private CustomForm GetFormFromRecord(IDataRecord record)
		{
			return new CustomForm
			{
				FormId = (Guid)record["CustomFormId"],
				Title = record["FormTitle"].ToString().Trim(),
				Xml = record["Xml"].ToString().Trim(),
				IsHidden = (record["ishidden"] != DBNull.Value && Convert.ToBoolean(record["ishidden"]))
			};
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0004B280 File Offset: 0x00049480
		[DebuggerStepThrough]
		public Task<CustomForm> LoadFormByIdAsync(Guid formId)
		{
			CustomFormDAO.<LoadFormByIdAsync>d__6 <LoadFormByIdAsync>d__ = new CustomFormDAO.<LoadFormByIdAsync>d__6();
			<LoadFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomForm>.Create();
			<LoadFormByIdAsync>d__.<>4__this = this;
			<LoadFormByIdAsync>d__.formId = formId;
			<LoadFormByIdAsync>d__.<>1__state = -1;
			<LoadFormByIdAsync>d__.<>t__builder.Start<CustomFormDAO.<LoadFormByIdAsync>d__6>(ref <LoadFormByIdAsync>d__);
			return <LoadFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0004B2CC File Offset: 0x000494CC
		public CustomForm LoadFormById(Guid formId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@formid", DbType.Guid, formId)
			};
			CustomForm result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT CustomFormId,FormTitle,Xml,IsHidden FROM CustomForm WHERE CustomFormId=@formid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetFormFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0004B360 File Offset: 0x00049560
		[DebuggerStepThrough]
		public Task<Guid> CreateFormAsync(CustomForm form)
		{
			CustomFormDAO.<CreateFormAsync>d__8 <CreateFormAsync>d__ = new CustomFormDAO.<CreateFormAsync>d__8();
			<CreateFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateFormAsync>d__.<>4__this = this;
			<CreateFormAsync>d__.form = form;
			<CreateFormAsync>d__.<>1__state = -1;
			<CreateFormAsync>d__.<>t__builder.Start<CustomFormDAO.<CreateFormAsync>d__8>(ref <CreateFormAsync>d__);
			return <CreateFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0004B3AC File Offset: 0x000495AC
		[DebuggerStepThrough]
		public Task DeleteFormAsync(Guid formId)
		{
			CustomFormDAO.<DeleteFormAsync>d__9 <DeleteFormAsync>d__ = new CustomFormDAO.<DeleteFormAsync>d__9();
			<DeleteFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFormAsync>d__.<>4__this = this;
			<DeleteFormAsync>d__.formId = formId;
			<DeleteFormAsync>d__.<>1__state = -1;
			<DeleteFormAsync>d__.<>t__builder.Start<CustomFormDAO.<DeleteFormAsync>d__9>(ref <DeleteFormAsync>d__);
			return <DeleteFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0004B3F8 File Offset: 0x000495F8
		[DebuggerStepThrough]
		public Task UpdateFormAsync(CustomForm form)
		{
			CustomFormDAO.<UpdateFormAsync>d__10 <UpdateFormAsync>d__ = new CustomFormDAO.<UpdateFormAsync>d__10();
			<UpdateFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateFormAsync>d__.<>4__this = this;
			<UpdateFormAsync>d__.form = form;
			<UpdateFormAsync>d__.<>1__state = -1;
			<UpdateFormAsync>d__.<>t__builder.Start<CustomFormDAO.<UpdateFormAsync>d__10>(ref <UpdateFormAsync>d__);
			return <UpdateFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0004B444 File Offset: 0x00049644
		[DebuggerStepThrough]
		public Task<IList<CustomForm>> LoadAllCustomFormsAsync()
		{
			CustomFormDAO.<LoadAllCustomFormsAsync>d__11 <LoadAllCustomFormsAsync>d__ = new CustomFormDAO.<LoadAllCustomFormsAsync>d__11();
			<LoadAllCustomFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<CustomForm>>.Create();
			<LoadAllCustomFormsAsync>d__.<>4__this = this;
			<LoadAllCustomFormsAsync>d__.<>1__state = -1;
			<LoadAllCustomFormsAsync>d__.<>t__builder.Start<CustomFormDAO.<LoadAllCustomFormsAsync>d__11>(ref <LoadAllCustomFormsAsync>d__);
			return <LoadAllCustomFormsAsync>d__.<>t__builder.Task;
		}
	}
}

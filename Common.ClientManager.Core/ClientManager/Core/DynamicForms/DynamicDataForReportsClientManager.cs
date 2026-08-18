using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000065 RID: 101
	public class DynamicDataForReportsClientManager : IDynamicDataForReportsClientManager, IWebService
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00010658 File Offset: 0x0000E858
		public DataTable CrossReferenceDataIntoSingleTable(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName);
			if (flag)
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferenceDataIntoSingleTableReq crossReferenceDataIntoSingleTableReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferenceDataIntoSingleTableReq>();
			crossReferenceDataIntoSingleTableReq.TableWithData = TableWithContext;
			crossReferenceDataIntoSingleTableReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicDataForReports>().CrossReferenceDataIntoSingleTable(crossReferenceDataIntoSingleTableReq).Table;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public DataTable CrossReferenceAccommodationDataTemplateOrCourseSpecific(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName);
			if (flag)
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferenceAccommodationDataTemplateOrCourseSpecificReq crossReferenceAccommodationDataTemplateOrCourseSpecificReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferenceAccommodationDataTemplateOrCourseSpecificReq>();
			crossReferenceAccommodationDataTemplateOrCourseSpecificReq.TableWithData = TableWithContext;
			crossReferenceAccommodationDataTemplateOrCourseSpecificReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicDataForReports>().CrossReferenceAccommodationDataTemplateOrCourseSpecific(crossReferenceAccommodationDataTemplateOrCourseSpecificReq).Table;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00010718 File Offset: 0x0000E918
		public DataTable CrossReferenceAccommodationDataTemplateOnly(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName);
			if (flag)
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferenceAccommodationDataTemplateOnlyReq crossReferenceAccommodationDataTemplateOnlyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferenceAccommodationDataTemplateOnlyReq>();
			crossReferenceAccommodationDataTemplateOnlyReq.TableWithData = TableWithContext;
			crossReferenceAccommodationDataTemplateOnlyReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicDataForReports>().CrossReferenceAccommodationDataTemplateOnly(crossReferenceAccommodationDataTemplateOnlyReq).Table;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00010778 File Offset: 0x0000E978
		public DataTable CrossReferencePerStudentData(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName);
			if (flag)
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferencePerStudentDataReq crossReferencePerStudentDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferencePerStudentDataReq>();
			crossReferencePerStudentDataReq.TableWithData = TableWithContext;
			crossReferencePerStudentDataReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicDataForReports>().CrossReferencePerStudentData(crossReferencePerStudentDataReq).Table;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000107D8 File Offset: 0x0000E9D8
		public DataTable CrossReferencePerAppointmentData(DataTable TableWithContext, IList<int> ControlIds)
		{
			bool flag = TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName);
			if (flag)
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferencePerAppointmentDataReq crossReferencePerAppointmentDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferencePerAppointmentDataReq>();
			crossReferencePerAppointmentDataReq.TableWithData = TableWithContext;
			crossReferencePerAppointmentDataReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicDataForReports>().CrossReferencePerAppointmentData(crossReferencePerAppointmentDataReq).Table;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00010838 File Offset: 0x0000EA38
		public DataTable LoadStudentReportInfo(int[] studentPersonIds, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds)
		{
			bool useEmail = typesToLoad.Contains(eDynamicStudentReportInfoType.Email);
			bool useAssignedAdvisor = typesToLoad.Contains(eDynamicStudentReportInfoType.AssignedAdvisor);
			bool useAccExpiry = typesToLoad.Contains(eDynamicStudentReportInfoType.AccommodationsExpiry);
			bool useAge = typesToLoad.Contains(eDynamicStudentReportInfoType.Age);
			LoadStudentReportInfoReq loadStudentReportInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentReportInfoReq>();
			loadStudentReportInfoReq.StudentPersonIds = studentPersonIds;
			loadStudentReportInfoReq.TypesToLoad = typesToLoad;
			loadStudentReportInfoReq.ControlIds = ControlIds;
			LoadStudentReportInfoResp loadStudentReportInfoResp = ClientServiceFactory.GetClientInstance<IDynamicDataForReports>().LoadStudentReportInfo(loadStudentReportInfoReq);
			bool flag = ((loadStudentReportInfoResp != null) ? loadStudentReportInfoResp.Items : null) == null;
			DataTable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = DynamicDataForReportsClientManager.ConverStudentInfoItemsToDataTable(studentPersonIds, loadStudentReportInfoResp.Items, useEmail, useAssignedAdvisor, useAccExpiry, useAge);
			}
			return result;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000108D0 File Offset: 0x0000EAD0
		[DebuggerStepThrough]
		public Task<DataTable> LoadStudentReportInfoAsync(int[] studentPersonIds, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds)
		{
			DynamicDataForReportsClientManager.<LoadStudentReportInfoAsync>d__6 <LoadStudentReportInfoAsync>d__ = new DynamicDataForReportsClientManager.<LoadStudentReportInfoAsync>d__6();
			<LoadStudentReportInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DataTable>.Create();
			<LoadStudentReportInfoAsync>d__.<>4__this = this;
			<LoadStudentReportInfoAsync>d__.studentPersonIds = studentPersonIds;
			<LoadStudentReportInfoAsync>d__.typesToLoad = typesToLoad;
			<LoadStudentReportInfoAsync>d__.ControlIds = ControlIds;
			<LoadStudentReportInfoAsync>d__.<>1__state = -1;
			<LoadStudentReportInfoAsync>d__.<>t__builder.Start<DynamicDataForReportsClientManager.<LoadStudentReportInfoAsync>d__6>(ref <LoadStudentReportInfoAsync>d__);
			return <LoadStudentReportInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001092C File Offset: 0x0000EB2C
		private static DataTable GetNewStudentInfoTable(bool useEmail, bool useAssignedAdvisor, bool useAccExpiry, bool useAge)
		{
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("personid", typeof(int));
			if (useEmail)
			{
				dataTable.Columns.Add("Email");
			}
			if (useAssignedAdvisor)
			{
				dataTable.Columns.Add("AssignedAdvisorFirst");
				dataTable.Columns.Add("AssignedAdvisorLast");
				dataTable.Columns.Add("AssignedAdvisor");
				dataTable.Columns.Add("AssignedAdvisorPhone");
				dataTable.Columns.Add("AssignedAdvisorTitle");
				dataTable.Columns.Add("AssignedAdvisorEmail");
			}
			if (useAge)
			{
				dataTable.Columns.Add("DateOfBirth", typeof(DateTime));
				dataTable.Columns.Add("Age", typeof(int));
			}
			if (useAccExpiry)
			{
				dataTable.Columns.Add("AccommodationsExpiryDate", typeof(DateTime));
			}
			return dataTable;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00010A4C File Offset: 0x0000EC4C
		private static DataTable ConverStudentInfoItemsToDataTable(int[] pids, List<StudentInfoItemBaseDTO>[] items, bool useEmail, bool useAssignedAdvisor, bool useAccExpiry, bool useAge)
		{
			DataTable newStudentInfoTable = DynamicDataForReportsClientManager.GetNewStudentInfoTable(useEmail, useAssignedAdvisor, useAccExpiry, useAge);
			foreach (int num in pids)
			{
				newStudentInfoTable.Rows.Add(new object[]
				{
					num
				});
			}
			foreach (List<StudentInfoItemBaseDTO> list in items)
			{
				bool flag = list == null || list.Count < 1;
				if (!flag)
				{
					StudentInfoItemBaseDTO studentInfoItemBaseDTO = list[0];
					bool flag2 = studentInfoItemBaseDTO is StudentInfoAgeItemDTO;
					eDynamicStudentReportInfoType eDynamicStudentReportInfoType;
					if (flag2)
					{
						eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.Age;
					}
					else
					{
						bool flag3 = studentInfoItemBaseDTO is StudentInfoEmailItemDTO;
						if (flag3)
						{
							eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.Email;
						}
						else
						{
							bool flag4 = studentInfoItemBaseDTO is StudentInfoAccExpiryItemDTO;
							if (flag4)
							{
								eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.AccommodationsExpiry;
							}
							else
							{
								bool flag5 = studentInfoItemBaseDTO is StudentInfoAssignedAdvisorItemDTO;
								if (!flag5)
								{
									throw new Exception("Can't find what to do: " + studentInfoItemBaseDTO.GetType().ToString());
								}
								eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.AssignedAdvisor;
							}
						}
					}
					foreach (StudentInfoItemBaseDTO studentInfoItemBaseDTO2 in list)
					{
						DataRow[] array = newStudentInfoTable.Select("personid=" + studentInfoItemBaseDTO2.PersonId.ToString());
						switch (eDynamicStudentReportInfoType)
						{
						case eDynamicStudentReportInfoType.Email:
						{
							StudentInfoEmailItemDTO studentInfoEmailItemDTO = (StudentInfoEmailItemDTO)studentInfoItemBaseDTO2;
							foreach (DataRow dataRow in array)
							{
								dataRow["Email"] = (studentInfoEmailItemDTO.Email ?? "");
							}
							break;
						}
						case eDynamicStudentReportInfoType.AssignedAdvisor:
						{
							StudentInfoAssignedAdvisorItemDTO studentInfoAssignedAdvisorItemDTO = (StudentInfoAssignedAdvisorItemDTO)studentInfoItemBaseDTO2;
							foreach (DataRow dataRow2 in array)
							{
								dataRow2["AssignedAdvisorFirst"] = (studentInfoAssignedAdvisorItemDTO.AdvisorFirstName ?? "");
								dataRow2["AssignedAdvisorLast"] = (studentInfoAssignedAdvisorItemDTO.AdvisorLastName ?? "");
								dataRow2["AssignedAdvisor"] = (studentInfoAssignedAdvisorItemDTO.AdvisorName ?? "");
								dataRow2["AssignedAdvisorPhone"] = (studentInfoAssignedAdvisorItemDTO.AdvisorPhone ?? "");
								dataRow2["AssignedAdvisorTitle"] = (studentInfoAssignedAdvisorItemDTO.AdvisorTitle ?? "");
								dataRow2["AssignedAdvisorEmail"] = (studentInfoAssignedAdvisorItemDTO.AdvisorEmail ?? "");
							}
							break;
						}
						case eDynamicStudentReportInfoType.Age:
						{
							StudentInfoAgeItemDTO studentInfoAgeItemDTO = (StudentInfoAgeItemDTO)studentInfoItemBaseDTO2;
							foreach (DataRow dataRow3 in array)
							{
								DataRow dataRow4 = dataRow3;
								string columnName = "DateOfBirth";
								DateTime? dateOfBirth = studentInfoAgeItemDTO.DateOfBirth;
								dataRow4[columnName] = ((dateOfBirth != null) ? dateOfBirth.GetValueOrDefault() : DBNull.Value);
								dataRow3["Age"] = studentInfoAgeItemDTO.Age;
							}
							break;
						}
						case eDynamicStudentReportInfoType.AccommodationsExpiry:
						{
							StudentInfoAccExpiryItemDTO studentInfoAccExpiryItemDTO = (StudentInfoAccExpiryItemDTO)studentInfoItemBaseDTO2;
							foreach (DataRow dataRow5 in array)
							{
								dataRow5["AccommodationsExpiryDate"] = (studentInfoAccExpiryItemDTO.AccExpiry ?? DBNull.Value);
							}
							break;
						}
						}
					}
				}
			}
			return newStudentInfoTable;
		}
	}
}

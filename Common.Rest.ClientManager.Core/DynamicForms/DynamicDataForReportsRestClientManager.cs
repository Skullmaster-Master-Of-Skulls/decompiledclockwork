using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000053 RID: 83
	public class DynamicDataForReportsRestClientManager : BearerTokenRestProxy<IDynamicDataForReportsClientManager>, IDynamicDataForReportsClientManager, IWebService
	{
		// Token: 0x0600031D RID: 797 RVA: 0x000098AF File Offset: 0x00007AAF
		public DynamicDataForReportsRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000098B9 File Offset: 0x00007AB9
		public DynamicDataForReportsRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000098C4 File Offset: 0x00007AC4
		public DataTable CrossReferenceDataIntoSingleTable(DataTable TableWithContext, IList<int> ControlIds)
		{
			if (TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName))
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferenceDataIntoSingleTableReq crossReferenceDataIntoSingleTableReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferenceDataIntoSingleTableReq>();
			crossReferenceDataIntoSingleTableReq.TableWithData = TableWithContext;
			crossReferenceDataIntoSingleTableReq.ControlIds = ControlIds;
			return base.Post<CrossReferenceDataIntoSingleTableReq, DataTable>(crossReferenceDataIntoSingleTableReq, "dynamicdataforreports/crossreferencedataintosingletable");
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009914 File Offset: 0x00007B14
		public DataTable CrossReferenceAccommodationDataTemplateOrCourseSpecific(DataTable TableWithContext, IList<int> ControlIds)
		{
			if (TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName))
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferenceAccommodationDataTemplateOrCourseSpecificReq crossReferenceAccommodationDataTemplateOrCourseSpecificReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferenceAccommodationDataTemplateOrCourseSpecificReq>();
			crossReferenceAccommodationDataTemplateOrCourseSpecificReq.TableWithData = TableWithContext;
			crossReferenceAccommodationDataTemplateOrCourseSpecificReq.ControlIds = ControlIds;
			return base.Post<CrossReferenceAccommodationDataTemplateOrCourseSpecificReq, DataTable>(crossReferenceAccommodationDataTemplateOrCourseSpecificReq, "dynamicdataforreports/crossreferenceaccommodationdatatemplateorcoursespecific");
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009964 File Offset: 0x00007B64
		public DataTable CrossReferenceAccommodationDataTemplateOnly(DataTable TableWithContext, IList<int> ControlIds)
		{
			if (TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName))
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferenceAccommodationDataTemplateOnlyReq crossReferenceAccommodationDataTemplateOnlyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferenceAccommodationDataTemplateOnlyReq>();
			crossReferenceAccommodationDataTemplateOnlyReq.TableWithData = TableWithContext;
			crossReferenceAccommodationDataTemplateOnlyReq.ControlIds = ControlIds;
			return base.Post<CrossReferenceAccommodationDataTemplateOnlyReq, DataTable>(crossReferenceAccommodationDataTemplateOnlyReq, "dynamicdataforreports/crossreferenceaccommodationdatatemplateonly");
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000099B4 File Offset: 0x00007BB4
		public DataTable CrossReferencePerStudentData(DataTable TableWithContext, IList<int> ControlIds)
		{
			if (TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName))
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferencePerStudentDataReq crossReferencePerStudentDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferencePerStudentDataReq>();
			crossReferencePerStudentDataReq.TableWithData = TableWithContext;
			crossReferencePerStudentDataReq.ControlIds = ControlIds;
			return base.Post<CrossReferencePerStudentDataReq, DataTable>(crossReferencePerStudentDataReq, "dynamicdataforreports/crossreferenceperstudentdata");
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009A04 File Offset: 0x00007C04
		public DataTable CrossReferencePerAppointmentData(DataTable TableWithContext, IList<int> ControlIds)
		{
			if (TableWithContext != null && string.IsNullOrEmpty(TableWithContext.TableName))
			{
				TableWithContext.TableName = "TableWithContext";
			}
			CrossReferencePerAppointmentDataReq crossReferencePerAppointmentDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CrossReferencePerAppointmentDataReq>();
			crossReferencePerAppointmentDataReq.TableWithData = TableWithContext;
			crossReferencePerAppointmentDataReq.ControlIds = ControlIds;
			return base.Post<CrossReferencePerAppointmentDataReq, DataTable>(crossReferencePerAppointmentDataReq, "dynamicdataforreports/crossreferenceperappointmentdata");
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009A54 File Offset: 0x00007C54
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
			LoadStudentReportInfoResp loadStudentReportInfoResp = base.Post<LoadStudentReportInfoReq, LoadStudentReportInfoResp>(loadStudentReportInfoReq, "dynamicdataforreports/studentreportinfo");
			if (((loadStudentReportInfoResp != null) ? loadStudentReportInfoResp.Items : null) == null)
			{
				return null;
			}
			return DynamicDataForReportsRestClientManager.ConverStudentInfoItemsToDataTable(studentPersonIds, loadStudentReportInfoResp.Items, useEmail, useAssignedAdvisor, useAccExpiry, useAge);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public async Task<DataTable> LoadStudentReportInfoAsync(int[] studentPersonIds, eDynamicStudentReportInfoType[] typesToLoad, IDictionary<eDynamicStudentReportInfoType, int> ControlIds)
		{
			bool useEmail = typesToLoad.Contains(eDynamicStudentReportInfoType.Email);
			bool useAssignedAdvisor = typesToLoad.Contains(eDynamicStudentReportInfoType.AssignedAdvisor);
			bool useAccExpiry = typesToLoad.Contains(eDynamicStudentReportInfoType.AccommodationsExpiry);
			bool useAge = typesToLoad.Contains(eDynamicStudentReportInfoType.Age);
			LoadStudentReportInfoReq loadStudentReportInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentReportInfoReq>();
			loadStudentReportInfoReq.StudentPersonIds = studentPersonIds;
			loadStudentReportInfoReq.TypesToLoad = typesToLoad;
			loadStudentReportInfoReq.ControlIds = ControlIds;
			LoadStudentReportInfoResp loadStudentReportInfoResp = await this.PostAsync<LoadStudentReportInfoReq, LoadStudentReportInfoResp>(loadStudentReportInfoReq, "dynamicdataforreports/studentreportinfo").ConfigureAwait(false);
			DataTable result;
			if (((loadStudentReportInfoResp != null) ? loadStudentReportInfoResp.Items : null) == null)
			{
				result = null;
			}
			else
			{
				result = DynamicDataForReportsRestClientManager.ConverStudentInfoItemsToDataTable(studentPersonIds, loadStudentReportInfoResp.Items, useEmail, useAssignedAdvisor, useAccExpiry, useAge);
			}
			return result;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00009B38 File Offset: 0x00007D38
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

		// Token: 0x06000327 RID: 807 RVA: 0x00009C40 File Offset: 0x00007E40
		private static DataTable ConverStudentInfoItemsToDataTable(int[] pids, List<StudentInfoItemBaseDTO>[] items, bool useEmail, bool useAssignedAdvisor, bool useAccExpiry, bool useAge)
		{
			DataTable newStudentInfoTable = DynamicDataForReportsRestClientManager.GetNewStudentInfoTable(useEmail, useAssignedAdvisor, useAccExpiry, useAge);
			foreach (int num in pids)
			{
				newStudentInfoTable.Rows.Add(new object[]
				{
					num
				});
			}
			foreach (List<StudentInfoItemBaseDTO> list in items)
			{
				if (list != null && list.Count >= 1)
				{
					StudentInfoItemBaseDTO studentInfoItemBaseDTO = list[0];
					eDynamicStudentReportInfoType eDynamicStudentReportInfoType;
					if (studentInfoItemBaseDTO is StudentInfoAgeItemDTO)
					{
						eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.Age;
					}
					else if (studentInfoItemBaseDTO is StudentInfoEmailItemDTO)
					{
						eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.Email;
					}
					else if (studentInfoItemBaseDTO is StudentInfoAccExpiryItemDTO)
					{
						eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.AccommodationsExpiry;
					}
					else
					{
						if (!(studentInfoItemBaseDTO is StudentInfoAssignedAdvisorItemDTO))
						{
							throw new Exception("Can't find what to do: " + studentInfoItemBaseDTO.GetType().ToString());
						}
						eDynamicStudentReportInfoType = eDynamicStudentReportInfoType.AssignedAdvisor;
					}
					foreach (StudentInfoItemBaseDTO studentInfoItemBaseDTO2 in list)
					{
						DataRow[] array = newStudentInfoTable.Select("personid=" + studentInfoItemBaseDTO2.PersonId.ToString());
						switch (eDynamicStudentReportInfoType)
						{
						case eDynamicStudentReportInfoType.Email:
						{
							StudentInfoEmailItemDTO studentInfoEmailItemDTO = (StudentInfoEmailItemDTO)studentInfoItemBaseDTO2;
							DataRow[] array2 = array;
							for (int j = 0; j < array2.Length; j++)
							{
								array2[j]["Email"] = (studentInfoEmailItemDTO.Email ?? "");
							}
							break;
						}
						case eDynamicStudentReportInfoType.AssignedAdvisor:
						{
							StudentInfoAssignedAdvisorItemDTO studentInfoAssignedAdvisorItemDTO = (StudentInfoAssignedAdvisorItemDTO)studentInfoItemBaseDTO2;
							foreach (DataRow dataRow in array)
							{
								dataRow["AssignedAdvisorFirst"] = (studentInfoAssignedAdvisorItemDTO.AdvisorFirstName ?? "");
								dataRow["AssignedAdvisorLast"] = (studentInfoAssignedAdvisorItemDTO.AdvisorLastName ?? "");
								dataRow["AssignedAdvisor"] = (studentInfoAssignedAdvisorItemDTO.AdvisorName ?? "");
								dataRow["AssignedAdvisorPhone"] = (studentInfoAssignedAdvisorItemDTO.AdvisorPhone ?? "");
								dataRow["AssignedAdvisorTitle"] = (studentInfoAssignedAdvisorItemDTO.AdvisorTitle ?? "");
								dataRow["AssignedAdvisorEmail"] = (studentInfoAssignedAdvisorItemDTO.AdvisorEmail ?? "");
							}
							break;
						}
						case eDynamicStudentReportInfoType.Age:
						{
							StudentInfoAgeItemDTO studentInfoAgeItemDTO = (StudentInfoAgeItemDTO)studentInfoItemBaseDTO2;
							foreach (DataRow dataRow2 in array)
							{
								string columnName = "DateOfBirth";
								DateTime? dateOfBirth = studentInfoAgeItemDTO.DateOfBirth;
								dataRow2[columnName] = ((dateOfBirth != null) ? dateOfBirth.GetValueOrDefault() : DBNull.Value);
								dataRow2["Age"] = studentInfoAgeItemDTO.Age;
							}
							break;
						}
						case eDynamicStudentReportInfoType.AccommodationsExpiry:
						{
							StudentInfoAccExpiryItemDTO studentInfoAccExpiryItemDTO = (StudentInfoAccExpiryItemDTO)studentInfoItemBaseDTO2;
							DataRow[] array2 = array;
							for (int j = 0; j < array2.Length; j++)
							{
								array2[j]["AccommodationsExpiryDate"] = (studentInfoAccExpiryItemDTO.AccExpiry ?? DBNull.Value);
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

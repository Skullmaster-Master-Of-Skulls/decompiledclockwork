using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x0200005E RID: 94
	public class ServiceRequestDAO : IServiceRequestDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00013CFC File Offset: 0x00011EFC
		public ServiceRequestDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00013D2C File Offset: 0x00011F2C
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00013D34 File Offset: 0x00011F34
		public OperationContext OpContext { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00013D40 File Offset: 0x00011F40
		private ServiceProviderTypeDAO serviceProviderTypeDao
		{
			get
			{
				bool flag = this._serviceProviderTypeDao == null;
				if (flag)
				{
					this._serviceProviderTypeDao = new ServiceProviderTypeDAO(this.OpContext);
				}
				return this._serviceProviderTypeDao;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00013D78 File Offset: 0x00011F78
		private PeopleDAO peopleDao
		{
			get
			{
				bool flag = this._peopleDao == null;
				if (flag)
				{
					this._peopleDao = new PeopleDAO(this.OpContext);
				}
				return this._peopleDao;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00013DB0 File Offset: 0x00011FB0
		private LookupCourseDAO lookupCourseDao
		{
			get
			{
				bool flag = this._lookupCourseDao == null;
				if (flag)
				{
					this._lookupCourseDao = new LookupCourseDAO(this.OpContext);
				}
				return this._lookupCourseDao;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00013DE8 File Offset: 0x00011FE8
		public SPRequestStatusType GetRequestStatusFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = string.Format("{0}SPRequestStatusTypeId", prefix);
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPRequestStatusType result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				string name2 = string.Format("{0}RSAssignmentIsRequired", prefix);
				SPRequestStatusType sprequestStatusType = new SPRequestStatusType
				{
					SPRequestStatusTypeId = (int)record[name],
					Title = record[string.Format("{0}RSTitle", prefix)].ToString(),
					Description = record[string.Format("{0}RSDescription", prefix)].ToString(),
					AssignmentIsRequired = (record[name2] != DBNull.Value && Convert.ToBoolean(record[name2])),
					UrgencyLevel = this.GetUrgencyLevelFromRecord(record, prefix)
				};
				result = sprequestStatusType;
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00013ECC File Offset: 0x000120CC
		public SPRequestAssignmentStatusType GetRequestAssignmentStatusFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = prefix + "SPRequestAssignmentStatusTypeId";
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPRequestAssignmentStatusType result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				string name2 = prefix + "ASAssignmentIsCompleted";
				SPRequestAssignmentStatusType sprequestAssignmentStatusType = new SPRequestAssignmentStatusType
				{
					SPRequestAssignmentStatusTypeId = (int)record[name],
					Title = record[prefix + "ASTitle"].ToString(),
					Description = record[prefix + "ASDescription"].ToString(),
					AssignmentIsCompleted = (record[name2] != DBNull.Value && Convert.ToBoolean(record[name2])),
					UrgencyLevel = this.GetUrgencyLevelFromRecord(record, prefix)
				};
				result = sprequestAssignmentStatusType;
			}
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00013FB0 File Offset: 0x000121B0
		public SPUrgencyLevelType GetUrgencyLevelFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = string.Format("{0}SPUrgencyLevelTypeId", prefix);
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPUrgencyLevelType result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				string name2 = string.Format("{0}UrgencyLevel", prefix);
				SPUrgencyLevelType spurgencyLevelType = new SPUrgencyLevelType
				{
					SPUrgencyLevelTypeId = (int)record[name],
					Title = record[string.Format("{0}UrgencyTitle", prefix)].ToString(),
					Description = record[string.Format("{0}UrgencyDescription", prefix)].ToString(),
					Urgency = ((record[name2] == DBNull.Value) ? 0 : ((int)record[name2]))
				};
				result = spurgencyLevelType;
			}
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00014088 File Offset: 0x00012288
		public SPProvider GetProviderFromRecord(IDataReader record, string prefix)
		{
			bool flag = prefix == null;
			if (flag)
			{
				prefix = "";
			}
			string name = prefix + "SPProviderId";
			bool flag2 = record == null || record[name] == DBNull.Value;
			SPProvider result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				string name2 = prefix + "address1isprimary";
				string name3 = prefix + "isactive";
				result = new SPProvider
				{
					SPProviderId = (int)record[name],
					Person = PeopleDAO.GetPersonFromReader(prefix, record, this.OpContext, null),
					Address1 = this.GetEncryptedData(record, prefix, "address1"),
					Address2 = this.GetEncryptedData(record, prefix, "address2"),
					AlternateEmail = this.GetEncryptedData(record, prefix, "AlternateEmail"),
					Specializations = this.GetEncryptedData(record, prefix, "Specializations"),
					Email = this.GetEncryptedData(record, prefix, "email"),
					ExternalId = this.GetEncryptedData(record, prefix, "externalid"),
					Note1 = this.GetEncryptedData(record, prefix, "note1"),
					Note2 = this.GetEncryptedData(record, prefix, "note2"),
					Phone1 = this.GetEncryptedData(record, prefix, "phone1"),
					Phone2 = this.GetEncryptedData(record, prefix, "phone2"),
					PhoneNote = this.GetEncryptedData(record, prefix, "phonenote"),
					UserName = this.GetEncryptedData(record, prefix, "username"),
					Address1IsPrimary = (record[name2] != DBNull.Value && Convert.ToBoolean(record[name2])),
					IsActive = (record[name3] != DBNull.Value && Convert.ToBoolean(record[name3]))
				};
			}
			return result;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00014258 File Offset: 0x00012458
		private string GetEncryptedData(IDataReader record, string prefix, string colName)
		{
			string text = prefix + colName;
			return (record[colName] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record[colName]);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000142A4 File Offset: 0x000124A4
		private SPRequestCourseAssignment GetCourseAssignmentFromRecord(IDataReader record)
		{
			bool flag = record == null || record["SPRequestEventAssignmentId"] == DBNull.Value;
			SPRequestCourseAssignment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new SPRequestCourseAssignment
				{
					SPRequestCourseAssignmentId = (int)record["SPRequestEventAssignmentId"],
					Provider = this.GetProviderFromRecord(record, "CourseAssignment"),
					Course = LookupCourseDAO.GetCourseBaseFromReader("courseassignment", record),
					DateCancelled = ((record["courseassignmentdatecancelled"] == DBNull.Value) ? null : new DateTime?((DateTime)record["courseassignmentdatecancelled"])),
					IsActive = (record["courseassignmentisactive"] != DBNull.Value && Convert.ToBoolean(record["courseassignmentisactive"])),
					Notes = ((record["courseassignmentnotes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["courseassignmentnotes"]))
				};
			}
			return result;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000143C0 File Offset: 0x000125C0
		private SPRequestEventAssignment GetEventAssignmentFromRecord(IDataReader record)
		{
			bool flag = record == null || record["SPRequestEventAssignmentId"] == DBNull.Value;
			SPRequestEventAssignment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new SPRequestEventAssignment
				{
					SPRequestEventAssignmentId = (int)record["SPRequestEventAssignmentId"],
					AssignedProvider = this.GetProviderFromRecord(record, "EventAssign"),
					IsActive = (record["eventassignmentisactive"] != DBNull.Value && Convert.ToBoolean(record["eventassignmentisactive"])),
					DateCancelled = ((record["eventassignmentdatecancelled"] == DBNull.Value) ? null : new DateTime?((DateTime)record["eventassignmentdatecancelled"])),
					Notes = ((record["eventassignmentnotes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["eventassignmentnotes"]))
				};
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000144CC File Offset: 0x000126CC
		private SPRequestWithSubItems GetFirstRequestFromReader(IDataReader reader, bool includeSubItems)
		{
			bool flag = reader == null;
			SPRequestWithSubItems result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SPRequestWithSubItems basicRequestFromRecord = this.GetBasicRequestFromRecord(reader);
				bool flag2 = basicRequestFromRecord != null && includeSubItems;
				if (flag2)
				{
					for (;;)
					{
						int num = (int)reader["SPRequestId"];
						bool flag3 = num != basicRequestFromRecord.Request.SPRequestId;
						if (flag3)
						{
							break;
						}
						this.AddCourseRequestToRequestFromRecord(ref basicRequestFromRecord, reader);
						this.AddEventRequestToRequestFromRecord(ref basicRequestFromRecord, reader);
						if (!reader.Read())
						{
							goto IL_71;
						}
					}
					return basicRequestFromRecord;
				}
				IL_71:
				result = basicRequestFromRecord;
			}
			return result;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00014550 File Offset: 0x00012750
		private void AddCourseRequestToRequestFromRecord(ref SPRequestWithSubItems request, IDataReader record)
		{
			bool flag = record == null || record["SPRequestCourseId"] == DBNull.Value;
			if (!flag)
			{
				SPRequestCourse item = new SPRequestCourse
				{
					SPRequestCourseId = (int)record["SPRequestCourseId"],
					RequestStatus = this.GetRequestStatusFromRecord(record, "Course"),
					AssignmentStatus = this.GetRequestAssignmentStatusFromRecord(record, "Course"),
					UrgencyLevel = this.GetUrgencyLevelFromRecord(record, "Course"),
					Notes = ((record["requestcoursenotes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["requestcoursenotes"])),
					IsRequired = (record["courseisrequired"] != DBNull.Value && Convert.ToBoolean(record["courseisrequired"])),
					Course = LookupCourseDAO.GetCourseBaseFromReader("", record),
					Assignment = this.GetCourseAssignmentFromRecord(record)
				};
				request.Courses.Add(item);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00014670 File Offset: 0x00012870
		private void AddEventRequestToRequestFromRecord(ref SPRequestWithSubItems request, IDataReader record)
		{
			bool flag = record == null || record["SPRequestEventId"] == DBNull.Value;
			if (!flag)
			{
				SPRequestEvent item = new SPRequestEvent
				{
					SPRequestEventId = (int)record["SPRequestEventId"],
					RequestStatus = this.GetRequestStatusFromRecord(record, "Event"),
					AssignmentStatus = this.GetRequestAssignmentStatusFromRecord(record, "event"),
					UrgencyLevel = this.GetUrgencyLevelFromRecord(record, "event"),
					StartDateTime = (DateTime)record["requesteventstartdatetime"],
					EndDateTime = (DateTime)record["requesteventenddatetime"],
					Notes = ((record["requesteventnotes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["requesteventnotes"])),
					IsRequired = (record["requesteventisrequired"] != DBNull.Value && Convert.ToBoolean(record["requesteventisrequired"])),
					Assignment = this.GetEventAssignmentFromRecord(record)
				};
				request.Events.Add(item);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000147AC File Offset: 0x000129AC
		private SPRequestWithSubItems GetBasicRequestFromRecord(IDataReader record)
		{
			bool flag = record == null;
			SPRequestWithSubItems result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int sprequestId = (int)record["SPRequestId"];
				SPRequestWithSubItems sprequestWithSubItems = new SPRequestWithSubItems
				{
					Courses = new List<SPRequestCourse>(),
					Events = new List<SPRequestEvent>(),
					Request = new SPRequest
					{
						SPRequestId = sprequestId,
						ProviderType = this.serviceProviderTypeDao.GetProviderTypeFromRecord(record),
						AssignmentStatus = this.GetRequestAssignmentStatusFromRecord(record, ""),
						RequestStatus = this.GetRequestStatusFromRecord(record, ""),
						UrgencyLevel = this.GetUrgencyLevelFromRecord(record, ""),
						DateEntered = ((record["dateentered"] == DBNull.Value) ? DateTime.Now : ((DateTime)record["dateentered"])),
						IsActive = (record["isactive"] != DBNull.Value && Convert.ToBoolean(record["isactive"])),
						Notes = ((record["notes"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["notes"])),
						SpecialInstructions = ((record["specialinstructions"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["specialinstructions"])),
						Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, null),
						WhoEntered = PeopleDAO.GetPersonFromReader("we", record, this.OpContext, null)
					}
				};
				result = sprequestWithSubItems;
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001496C File Offset: 0x00012B6C
		public IList<SPRequest> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeId)
		{
			DbParameter[] array = new DbParameter[5];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate);
			array[2] = this.DatabaseManager.GetParameter("@includeassigned", DbType.Boolean, IncludeAssigned);
			array[3] = this.DatabaseManager.GetParameter("@includeunassigned", DbType.Boolean, IncludeUnassigned);
			int num = 4;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@sptypes";
			DbType pType = DbType.String;
			object value;
			if (SPProviderTypeId != null)
			{
				value = string.Join(",", SPProviderTypeId.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC SPRequestsWithoutSubItems @startdate,@enddate,@includeassigned,@includeunassigned,@sptypes", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<SPRequest> list = new List<SPRequest>();
					while (dataReader != null)
					{
						SPRequestWithSubItems firstRequestFromReader = this.GetFirstRequestFromReader(dataReader, false);
						bool flag2 = firstRequestFromReader != null && firstRequestFromReader.Request != null;
						if (flag2)
						{
							list.Add(firstRequestFromReader.Request);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00014AD0 File Offset: 0x00012CD0
		public SPRequest LoadRequestById(int SPRequestId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@requestid", DbType.Int32, SPRequestId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC SPRequestWithSubItems @requestid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					bool flag2 = dataReader.Read();
					if (flag2)
					{
						SPRequestWithSubItems firstRequestFromReader = this.GetFirstRequestFromReader(dataReader, false);
						bool flag3 = firstRequestFromReader == null;
						if (flag3)
						{
							return null;
						}
						return firstRequestFromReader.Request;
					}
				}
			}
			return null;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00014B74 File Offset: 0x00012D74
		public SPRequestWithSubItems LoadRequestWithSubItemsById(int SPRequestId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@requestid", DbType.Int32, SPRequestId)
			};
			SPRequestWithSubItems result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC SPRequestWithSubItems @requestid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = !dataReader.Read();
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = this.GetFirstRequestFromReader(dataReader, true);
					}
				}
			}
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00014C00 File Offset: 0x00012E00
		public int CreateRequestCourse(int SPRequestId, SPRequestCourse RequestCourse)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId),
				(RequestCourse.Course == null) ? this.DatabaseManager.GetParameter("@lucourseid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@lucourseid", DbType.Int32, RequestCourse.Course.LuCourseId),
				(RequestCourse.RequestStatus == null) ? this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, RequestCourse.RequestStatus.SPRequestStatusTypeId),
				(RequestCourse.AssignmentStatus == null) ? this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, RequestCourse.AssignmentStatus.SPRequestAssignmentStatusTypeId),
				(RequestCourse.UrgencyLevel == null) ? this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, RequestCourse.UrgencyLevel.SPUrgencyLevelTypeId),
				(RequestCourse.Notes == null) ? this.DatabaseManager.GetParameter("@notes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(RequestCourse.Notes)),
				this.DatabaseManager.GetParameter("@isrequired", DbType.Boolean, RequestCourse.IsRequired),
				(RequestCourse.Assignment == null) ? this.DatabaseManager.GetParameter("@sprequestcourseassignmentid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestcourseassignmentid", DbType.Int32, RequestCourse.Assignment.SPRequestCourseAssignmentId)
			};
			return (int)this.DatabaseManager.ExecuteScalar("INSERT INTO SPRequestCourse (SPRequestId,lucourseid,SPRequestStatusTypeId,SPRequestAssignmentStatusTypeId,SPUrgencyLevelTypeId,Notes,SPRequestCourseAssignmentId,IsRequired)\r\nVALUES (@SPRequestId,@LuCourseId,@SPRequestStatusTypeId,@SPRequestAssignmentStatusTypeId,@SPUrgencyLevelTypeId,@Notes,@SPRequestCourseAssignmentId,@IsRequired);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS spcourseid", parameters);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00014E14 File Offset: 0x00013014
		public void UpdateRequest(SPRequest Request)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, Request.SPRequestId),
				(Request.Notes == null) ? this.DatabaseManager.GetParameter("@notes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Request.Notes)),
				(Request.SpecialInstructions == null) ? this.DatabaseManager.GetParameter("@specialinstructions", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@specialinstructions", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Request.SpecialInstructions)),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, Request.IsActive),
				(Request.RequestStatus == null) ? this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, Request.RequestStatus),
				(Request.AssignmentStatus == null) ? this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, Request.AssignmentStatus),
				(Request.UrgencyLevel == null) ? this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, Request.UrgencyLevel.SPUrgencyLevelTypeId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE    SPRequest SET notes=@notes,specialinstructions=@specialinstructions,isactive=@isactive,\r\n            sprequeststatustypeid=@sprequeststatustypeid,sprequestassignmentstatustypeid=@sprequestassignmentstatustypeid,spurgencyleveltypeid=@spurgencyleveltypeid\r\nWHERE sprequestid=@sprequestid", parameters);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00014FCC File Offset: 0x000131CC
		public void UpdateRequest(SPRequestWithSubItems RequestWithSubItems, bool UpdateSubItems)
		{
			SPRequest request = RequestWithSubItems.Request;
			this.UpdateRequest(request);
			if (UpdateSubItems)
			{
				bool flag = RequestWithSubItems.Courses != null;
				if (flag)
				{
					foreach (SPRequestCourse requestCourse in RequestWithSubItems.Courses)
					{
						this.UpdateRequestCourse(requestCourse);
					}
				}
				bool flag2 = RequestWithSubItems.Events != null;
				if (flag2)
				{
					foreach (SPRequestEvent requestEvent in RequestWithSubItems.Events)
					{
						this.UpdateRequestEvent(requestEvent);
					}
				}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000150A4 File Offset: 0x000132A4
		public void DeleteRequest(int SPRequestId)
		{
			DbTransaction transaction = this.DatabaseManager.BeginDbTransaction();
			try
			{
				this.DatabaseManager.ExecuteNonQueryTransaction("DELETE FROM sprequestcourseassignment WHERE sprequestcourseassignmentid IN (SELECT sprequestcourseassignmentid FROM sprequestcourse WHERE sprequestid=@sprequestid AND NOT sprequestcourseassignmentid IS NULL )", transaction, new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId)
				});
				this.DatabaseManager.ExecuteNonQueryTransaction("DELETE FROM sprequesteventassignment WHERE sprequesteventassignmentid IN (SELECT sprequesteventassignmentid FROM sprequestevent WHERE sprequestid=@sprequestid AND NOT sprequesteventassignmentid IS NULL )", transaction, new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId)
				});
				this.DatabaseManager.ExecuteNonQueryTransaction("DELETE FROM sprequestcourse WHERE sprequestid=@sprequestid", transaction, new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId)
				});
				this.DatabaseManager.ExecuteNonQueryTransaction("DELETE FROM sprequestevent WHERE sprequestid=@sprequestid", transaction, new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId)
				});
				this.DatabaseManager.ExecuteNonQueryTransaction("DELETE FROM sprequest WHERE sprequestid=@sprequestid", transaction, new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId)
				});
				this.DatabaseManager.CommitDbTransaction(transaction);
			}
			catch (DbException ex)
			{
				this.DatabaseManager.RollbackDbTransaction(transaction);
				CWLogger.Logger.Error("ServiceRequestDAO:DeleteRequest:TransactionRolledBack:{0}", ex.ToString());
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00015214 File Offset: 0x00013414
		public void UpdateRequestCourse(SPRequestCourse RequestCourse)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestcourseid", DbType.Int32, RequestCourse.SPRequestCourseId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, RequestCourse.Course.LuCourseId),
				this.DatabaseManager.GetParameter("@isrequired", DbType.Boolean, RequestCourse.IsRequired),
				(RequestCourse.RequestStatus == null) ? this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, RequestCourse.RequestStatus.SPRequestStatusTypeId),
				(RequestCourse.UrgencyLevel == null) ? this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, RequestCourse.RequestStatus.UrgencyLevel.SPUrgencyLevelTypeId),
				(RequestCourse.Notes == null) ? this.DatabaseManager.GetParameter("@notes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(RequestCourse.Notes)),
				(RequestCourse.Assignment == null) ? this.DatabaseManager.GetParameter("@sprequestcourseassignmentid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestcourseassignmentid", DbType.Int32, RequestCourse.Assignment.SPRequestCourseAssignmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE    SPRequestCourse SET lucourseid=@lucid,sprequeststatustypeid=@sprequeststatustypeid,sprequestassignmentstatustypeid=@sprequestassignmentstatustypeid,\r\n            spurgencyleveltypeid=@spurgencyleveltypeid,notes=@notes,sprequestcourseassignmentid=@sprequestcourseassignmentid,isrequired=@isrequired\r\nWHERE       sprequestcourseid=@sprequestcourseid", parameters);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000153C0 File Offset: 0x000135C0
		public void UpdateRequestEvent(SPRequestEvent RequestEvent)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequesteventid", DbType.Int32, RequestEvent.SPRequestEventId),
				this.DatabaseManager.GetParameter("@requesteventstartdatetime", DbType.DateTime, RequestEvent.StartDateTime),
				this.DatabaseManager.GetParameter("@requesteventenddatetime", DbType.DateTime, RequestEvent.EndDateTime),
				this.DatabaseManager.GetParameter("@requesteventisrequired", DbType.Boolean, RequestEvent.IsRequired),
				(RequestEvent.RequestStatus == null) ? this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, RequestEvent.RequestStatus.SPRequestStatusTypeId),
				(RequestEvent.AssignmentStatus == null) ? this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, RequestEvent.RequestStatus),
				(RequestEvent.UrgencyLevel == null) ? this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, RequestEvent.RequestStatus.UrgencyLevel.SPUrgencyLevelTypeId),
				(RequestEvent.Notes == null) ? this.DatabaseManager.GetParameter("@requesteventnotes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@requesteventnotes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(RequestEvent.Notes)),
				(RequestEvent.Assignment == null) ? this.DatabaseManager.GetParameter("@sprequesteventassignmentid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequesteventassignmentid", DbType.Int32, RequestEvent.Assignment.SPRequestEventAssignmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE    sprequestevent SET requesteventstartdatetime=@requesteventstartdatetime,requesteventenddatetime=@requesteventenddatetime,sprequeststatustypeid=@sprequeststatustypeid,\r\n            sprequestassignmentstatustypeid=@sprequestassignmentstatustypeid,spurgencyleveltypeid=@spurgencyleveltypeid,requesteventnotes=@requesteventnotes,\r\n            sprequesteventassignmentid=@sprequesteventassignmentid,requesteventisrequired=@requesteventisrequired\r\nWHERE       sprequesteventid=@sprequesteventid", parameters);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteRequestCourse(int SPRequestCourseId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00003998 File Offset: 0x00001B98
		public void DeleteRequestEvent(int SPRequestEventId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000155C4 File Offset: 0x000137C4
		public int CreateRequestEvent(int SPRequestId, SPRequestEvent RequestEvent)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestid", DbType.Int32, SPRequestId),
				this.DatabaseManager.GetParameter("@requesteventstartdatetime", DbType.DateTime, RequestEvent.StartDateTime),
				this.DatabaseManager.GetParameter("@requesteventenddatetime", DbType.DateTime, RequestEvent.EndDateTime),
				(RequestEvent.RequestStatus == null) ? this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, RequestEvent.RequestStatus.SPRequestStatusTypeId),
				(RequestEvent.AssignmentStatus == null) ? this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, RequestEvent.AssignmentStatus.SPRequestAssignmentStatusTypeId),
				(RequestEvent.UrgencyLevel == null) ? this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spurgencyleveltypeid", DbType.Int32, RequestEvent.UrgencyLevel.SPUrgencyLevelTypeId),
				(RequestEvent.Notes == null) ? this.DatabaseManager.GetParameter("@requesteventnotes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@requesteventnotes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(RequestEvent.Notes)),
				this.DatabaseManager.GetParameter("@requesteventisrequired", DbType.Boolean, RequestEvent.IsRequired),
				(RequestEvent.Assignment == null) ? this.DatabaseManager.GetParameter("@SPRequestEventAssignmentId", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@SPRequestEventAssignmentId", DbType.Int32, RequestEvent.Assignment.SPRequestEventAssignmentId)
			};
			return (int)this.DatabaseManager.ExecuteScalar("INSERT INTO SPRequestEvent (SPRequestId,requesteventstartdatetime,requesteventenddatetime,sprequeststatustypeid,SPRequestAssignmentStatusTypeId,SPUrgencyLevelTypeId,requesteventNotes,sprequesteventassignmentid,requesteventIsRequired)\r\nVALUES (@SPRequestId,@requesteventstartdatetime,@requesteventenddatetime,@SPRequestStatusTypeId,@SPRequestAssignmentStatusTypeId,@SPUrgencyLevelTypeId,@requesteventnotes,@sprequesteventassignmentid,@requesteventIsRequired);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS sprequesteventid", parameters);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000157D0 File Offset: 0x000139D0
		public int CreateRequest(SPRequestWithSubItems RequestWithSubItems, bool CreateSubItems)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spprovidertypeid", DbType.Int32, RequestWithSubItems.Request.ProviderType.SPProviderTypeId),
				this.DatabaseManager.GetParameter("@personid", DbType.Int32, RequestWithSubItems.Request.Student.PersonId),
				this.DatabaseManager.GetParameter("@whoentered", DbType.Int32, RequestWithSubItems.Request.WhoEntered.PersonId),
				(RequestWithSubItems.Request.Notes == null) ? this.DatabaseManager.GetParameter("@notes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(RequestWithSubItems.Request.Notes)),
				(RequestWithSubItems.Request.SpecialInstructions == null) ? this.DatabaseManager.GetParameter("@specialinstructions", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@specialinstructions", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(RequestWithSubItems.Request.SpecialInstructions)),
				(RequestWithSubItems.Request.RequestStatus == null) ? this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequeststatustypeid", DbType.Int32, RequestWithSubItems.Request.RequestStatus.SPRequestStatusTypeId),
				(RequestWithSubItems.Request.AssignmentStatus == null) ? this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@sprequestassignmentstatustypeid", DbType.Int32, RequestWithSubItems.Request.AssignmentStatus.SPRequestAssignmentStatusTypeId),
				(RequestWithSubItems.Request.UrgencyLevel == null) ? this.DatabaseManager.GetParameter("@SPUrgencyLevelTypeId", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@SPUrgencyLevelTypeId", DbType.Int32, RequestWithSubItems.Request.UrgencyLevel.SPUrgencyLevelTypeId),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, RequestWithSubItems.Request.IsActive)
			};
			int num = (int)this.DatabaseManager.ExecuteScalar("INSERT INTO sprequest (spprovidertypeid,personid,dateentered,whoentered,notes,specialinstructions,sprequeststatustypeid,sprequestassignmentstatustypeid,spurgencyleveltypeid,isactive)\r\nVALUES (@spprovidertypeid,@personid,getdate(),@whoentered,@notes,@specialinstructions,@sprequeststatustypeid,@sprequestassignmentstatustypeid,@spurgencyleveltypeid,@isactive); \r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS sprequestid", parameters);
			RequestWithSubItems.Request.SPRequestId = num;
			bool flag = num < 1;
			if (flag)
			{
				throw new Exception("ServiceRequestDAO:CreateRequest:Failed to create request.");
			}
			if (CreateSubItems)
			{
				foreach (SPRequestCourse sprequestCourse in RequestWithSubItems.Courses)
				{
					int sprequestCourseId = this.CreateRequestCourse(num, sprequestCourse);
					sprequestCourse.SPRequestCourseId = sprequestCourseId;
				}
				foreach (SPRequestEvent sprequestEvent in RequestWithSubItems.Events)
				{
					int sprequestEventId = this.CreateRequestEvent(num, sprequestEvent);
					sprequestEvent.SPRequestEventId = sprequestEventId;
				}
			}
			return num;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00015B14 File Offset: 0x00013D14
		public int AssignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignment CourseAssignment)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestcourseid", DbType.Int32, SPRequestCourseId),
				(CourseAssignment.Provider == null) ? this.DatabaseManager.GetParameter("@spproviderid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spproviderid", DbType.Int32, CourseAssignment.Provider.SPProviderId),
				(CourseAssignment.Course == null) ? this.DatabaseManager.GetParameter("@lucid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@lucid", DbType.Int32, CourseAssignment.Course.LuCourseId),
				(CourseAssignment.Notes == null) ? this.DatabaseManager.GetParameter("@notes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(CourseAssignment.Notes)),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, CourseAssignment.IsActive),
				(CourseAssignment.DateCancelled != null) ? this.DatabaseManager.GetParameter("@datecancelled", DbType.DateTime, CourseAssignment.DateCancelled.Value) : this.DatabaseManager.GetParameter("@datecancelled", DbType.DateTime, DBNull.Value)
			};
			return (int)this.DatabaseManager.ExecuteScalar("DECLARE @id int\r\nSET @id=(SELECT sprequestcourseid FROM sprequestcourse WHERE sprequestcourseid=@sprequestcourseid AND NOT SPRequestCourseAssignmentId IS NULL)\r\nIF @id IS NULL\r\nBEGIN\r\n    INSERT INTO sprequestcourseassignment(CourseAssignmentSPProviderId,CourseAssignmentLuCourseId,CourseAssignmentNotes,CourseAssignmentIsActive,CourseAssignmentDateCancelled)\r\n        VALUES (@spproviderid,@lucid,@notes,@isactive,@datecancelled);\r\n    SET @id=(SELECT CAST(SCOPE_IDENTITY() AS int)\r\n    UPDATE sprequestcourse SET sprequestcourseassignmentid=@id WHERE sprequestcourseid=@sprequestcourseid\r\nEND\r\nELSE \r\n    UPDATE sprequestcourseassignment SET CourseAssignmentSPProviderId=@providerid,CourseAssignmentLuCourseId=@lucid,\r\n        CourseAssignmentNotes=@notes,CourseAssignmentIsActive=@isactive,CourseAssignmentDateCancelled=@datecancelled\r\n    WHERE sprequestcourseassignmentid=@id\r\nEND\r\nSELECT @id", parameters);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public void UnAssignRequestCourse(int SPRequestCourseId)
		{
			this.DatabaseManager.ExecuteNonQuery("SET @id=(SELECT sprequestcourseassignmentid FROM sprequestcourse WHERE sprequestcourseid=@sprequestcourseid)\r\nIF NOT @id IS NULL\r\nBEGIN\r\n    UPDATE sprequestcourse SET sprequestcourseassignmentid=NULL WHERE sprequestcourseid=@sprequestcourseid\r\n    DELETE FROM sprequestcourseassignment WHERE sprequestcourseassignmentid=@id\r\nEND", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestcourseid", DbType.Int32, SPRequestCourseId)
			});
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00015CE4 File Offset: 0x00013EE4
		public int AssignRequestEvent(int SPRequestEventId, SPRequestEventAssignment EventAssignment)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequestcourseid", DbType.Int32, SPRequestEventId),
				(EventAssignment.AssignedProvider == null) ? this.DatabaseManager.GetParameter("@spproviderid", DbType.Int32, DBNull.Value) : this.DatabaseManager.GetParameter("@spproviderid", DbType.Int32, EventAssignment.AssignedProvider.SPProviderId),
				(EventAssignment.Notes == null) ? this.DatabaseManager.GetParameter("@notes", DbType.Binary, DBNull.Value) : this.DatabaseManager.GetParameter("@notes", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(EventAssignment.Notes)),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, EventAssignment.IsActive),
				(EventAssignment.DateCancelled != null) ? this.DatabaseManager.GetParameter("@datecancelled", DbType.DateTime, EventAssignment.DateCancelled.Value) : this.DatabaseManager.GetParameter("@datecancelled", DbType.DateTime, DBNull.Value)
			};
			return (int)this.DatabaseManager.ExecuteScalar("DECLARE @id int\r\nSET @id=(SELECT sprequesteventid FROM sprequestevent WHERE sprequesteventid=@sprequesteventid AND NOT SPRequesteventAssignmentId IS NULL)\r\nIF @id IS NULL\r\nBEGIN\r\n    INSERT INTO sprequesteventassignment(eventAssignmentSPProviderId,eventAssignmentNotes,eventAssignmentIsActive,eventAssignmentDateCancelled)\r\n        VALUES (@spproviderid,@notes,@isactive,@datecancelled);\r\n    SET @id=(SELECT CAST(SCOPE_IDENTITY() AS int)\r\n    UPDATE sprequestevent SET sprequesteventassignmentid=@id WHERE sprequesteventid=@sprequesteventid\r\nEND\r\nELSE \r\n    UPDATE sprequesteventassignment SET eventAssignmentSPProviderId=@providerid,\r\n        eventAssignmentNotes=@notes,eventAssignmentIsActive=@isactive,eventAssignmentDateCancelled=@datecancelled\r\n    WHERE sprequesteventassignmentid=@id\r\nEND\r\nSELECT @id", parameters);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00015E2C File Offset: 0x0001402C
		public void UnAssignRequestEvent(int SPRequestEventId)
		{
			this.DatabaseManager.ExecuteNonQuery("SET @id=(SELECT sprequesteventassignmentid FROM sprequestevent WHERE sprequesteventid=@sprequesteventid)\r\nIF NOT @id IS NULL\r\nBEGIN\r\n    UPDATE sprequestevent SET sprequesteventassignmentid=NULL WHERE sprequesteventid=@sprequesteventid\r\n    DELETE FROM sprequesteventassignment WHERE sprequesteventassignmentid=@id\r\nEND", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sprequesteventid", DbType.Int32, SPRequestEventId)
			});
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003998 File Offset: 0x00001B98
		public void AssignOrUnassignRequestEvent(int SPRequestEventId, SPRequestEventAssignment EventAssignment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00015E6C File Offset: 0x0001406C
		public void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pidnew", DbType.Int32, PersonIdNew),
				this.DatabaseManager.GetParameter("@pidold", DbType.Int32, PersonIdOld)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE serviceproviderrequests SET personid=@pidnew WHERE personid=@pidold", parameters);
		}

		// Token: 0x040000E8 RID: 232
		public DatabaseLayer DatabaseManager;

		// Token: 0x040000EA RID: 234
		private ServiceProviderTypeDAO _serviceProviderTypeDao;

		// Token: 0x040000EB RID: 235
		private PeopleDAO _peopleDao;

		// Token: 0x040000EC RID: 236
		private LookupCourseDAO _lookupCourseDao;
	}
}

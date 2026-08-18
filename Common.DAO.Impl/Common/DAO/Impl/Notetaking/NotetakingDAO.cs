using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Notetaking;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.DAO.Impl.Notetaking
{
	// Token: 0x02000084 RID: 132
	public class NotetakingDAO : INotetakingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		private LookupCourseDAO lookupCourseDao
		{
			get
			{
				bool flag = this.lcd == null;
				if (flag)
				{
					this.lcd = new LookupCourseDAO(this.OpContext);
				}
				return this.lcd;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001D71E File Offset: 0x0001B91E
		public NotetakingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0001D74F File Offset: 0x0001B94F
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0001D757 File Offset: 0x0001B957
		public OperationContext OpContext { get; set; }

		// Token: 0x06000365 RID: 869 RVA: 0x0001D760 File Offset: 0x0001B960
		private DownloadedLectureNote GetDownloadedLectureNoteFromRecord(IDataReader record)
		{
			bool flag = record == null || record["notetakerid"] is DBNull;
			DownloadedLectureNote result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int notetakerDocumentId = (record["notetakerdocumentid"] is DBNull) ? 0 : ((int)record["notetakerdocumentid"]);
				result = new DownloadedLectureNote
				{
					NotetakerDocumentId = notetakerDocumentId,
					LastDateDownloaded = (DateTime)record["datedownloaded"],
					LectureNoteDescription = new LectureNoteDescription
					{
						Comment = record["notes"].ToString(),
						DateUploaded = ((record["datecreated"] is DBNull) ? DateTime.Now : ((DateTime)record["datecreated"])),
						LectureDate = (DateTime)record["lecturedate"],
						NotetakerBaseInfo = this.GetNotetakerBaseFromRecord(record),
						CourseBaseInfo = LookupCourseDAO.GetCourseBaseFromReader("", record),
						NotetakerDocumentId = notetakerDocumentId,
						MarkedForDeletionDate = (record.ContainsColumn("DeletionDate") ? ((record["DeletionDate"] is DBNull) ? null : new DateTime?((DateTime)record["DeletionDate"])) : null),
						FileSizeInBytes = (record.ContainsColumn("sizeinbytes") ? ((record["sizeinbytes"] is DBNull) ? 0 : ((int)record["sizeinbytes"])) : 0)
					},
					LectureNoteDocument = new BinaryFile
					{
						FileName = record["docname"].ToString()
					}
				};
			}
			return result;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001D934 File Offset: 0x0001BB34
		private LectureNote GetLectureNoteFromRecord(IDataReader record)
		{
			bool flag = record == null || record["notetakerdocumentid"] == DBNull.Value;
			LectureNote result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int notetakerDocumentId = (int)record["notetakerdocumentid"];
				LectureNote lectureNote = new LectureNote();
				lectureNote.LectureNoteDescription = NotetakingDAO.GetLectureNoteDescriptionFromRecord(record, this.DatabaseManager.Encryption.GetBatchDecryptor());
				lectureNote.NotetakerDocumentId = notetakerDocumentId;
				BinaryFile lectureNoteDocument;
				if (record["binarydata"] != DBNull.Value)
				{
					BinaryFile binaryFile = new BinaryFile();
					binaryFile.ByteArray = (byte[])record["binarydata"];
					binaryFile.FileName = record["docname"].ToString();
					binaryFile.FileSize = ((record["sizeinbytes"] == DBNull.Value) ? 0 : ((int)record["sizeinbytes"]));
					lectureNoteDocument = binaryFile;
					binaryFile.Id = notetakerDocumentId.ToString();
				}
				else
				{
					lectureNoteDocument = null;
				}
				lectureNote.LectureNoteDocument = lectureNoteDocument;
				result = lectureNote;
			}
			return result;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001DA30 File Offset: 0x0001BC30
		public static LectureNoteDescription GetLectureNoteDescriptionFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null || record["notetakerdocumentid"] == DBNull.Value;
			LectureNoteDescription result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new LectureNoteDescription
				{
					NotetakerDocumentId = (int)record["notetakerdocumentid"],
					LectureDate = (DateTime)record["lecturedate"],
					DateUploaded = (DateTime)record["datecreated"],
					Comment = ((record["notes"] == DBNull.Value) ? "" : ((string)record["notes"])),
					CourseBaseInfo = LookupCourseDAO.GetCourseBaseFromReader("", record),
					NotetakerBaseInfo = NotetakingDAO.GetNotetakerBaseFromRecord(record, batchDecryptor),
					Filename = ((record["docname"] is DBNull) ? "" : record["docname"].ToString()),
					MarkedForDeletionDate = (record.ContainsColumn("DeletionDate") ? ((record["DeletionDate"] is DBNull) ? null : new DateTime?((DateTime)record["DeletionDate"])) : null),
					FileSizeInBytes = (record.ContainsColumn("sizeinbytes") ? ((record["sizeinbytes"] is DBNull) ? 0 : ((int)record["sizeinbytes"])) : 0)
				};
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		private NotetakerBase GetNotetakerBaseFromRecord(IDataReader record)
		{
			return NotetakingDAO.GetNotetakerBaseFromRecord(record, this.DatabaseManager.Encryption.GetBatchDecryptor());
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
		private static NotetakerBase GetNotetakerBaseFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null || record["serviceproviderid"] == DBNull.Value;
			NotetakerBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new NotetakerBase
				{
					ServiceProviderId = (int)record["serviceproviderid"],
					Email = ((record["email"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])record["email"])),
					FirstName = ((record["firstname"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"])),
					MiddleName = ((record["middlename"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])record["middlename"])),
					LastName = ((record["lastname"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
					Student_no = ((record["student_no"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])record["student_no"])),
					Username = ((record["altid"] == DBNull.Value) ? "" : batchDecryptor.Decrypt((byte[])record["altid"]))
				};
			}
			return result;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001DD80 File Offset: 0x0001BF80
		public NotetakerBase LoadNotetakerBaseByUsername(string username)
		{
			bool flag = username == null || username.Trim().Length < 1;
			NotetakerBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@usernameu", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(username.ToUpper())),
					this.DatabaseManager.GetParameter("@usernamel", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(username.ToLower()))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.email,sp.altid\r\nFROM        serviceproviders sp \r\nWHERE       sp.isactive=1 AND\r\n            (\r\n                sp.altid=@usernameu OR sp.altid=@usernamel\r\n            )", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return this.GetNotetakerBaseFromRecord(dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public NotetakerBase LoadNotetakerBaseByStudentNumber(string StudentNumber)
		{
			bool flag = StudentNumber == null || StudentNumber.Trim().Length < 1;
			NotetakerBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@StudentNumber0", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(StudentNumber.ToUpper())),
					this.DatabaseManager.GetParameter("@StudentNumber1", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(StudentNumber.ToLower()))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.email,sp.altid\r\nFROM        serviceproviders sp \r\nWHERE       sp.isactive=1 AND\r\n            (\r\n                sp.student_no=@StudentNumber0 OR sp.student_no=@StudentNumber1\r\n            )", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return this.GetNotetakerBaseFromRecord(dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001DF38 File Offset: 0x0001C138
		public NotetakerBase LoadNotetakerBaseById(int ServiceProviderId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, ServiceProviderId)
			};
			NotetakerBase result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.email,sp.altid\r\nFROM        serviceproviders sp \r\nWHERE       sp.serviceproviderid=@id", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetNotetakerBaseFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public NotetakerBase LoadNotetakerBaseByNotetakeeAndCourse(int NotetakeePersonId, int NotetakeeLuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, NotetakeePersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, NotetakeeLuCourseId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    spr.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.email,sp.altid\r\nFROM        serviceproviderrequests spr LEFT JOIN serviceproviders sp ON sp.serviceproviderid=spr.serviceproviderid\r\nWHERE       spr.isactive=1 AND spr.personid=@pid AND spr.lucourseid=@lucid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetNotetakerBaseFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001E054 File Offset: 0x0001C254
		public List<LectureNoteDescription> LoadLectureNoteDescriptionsByNotetakerAndCourse(int ServiceProviderId, int NotetakerLuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spid", DbType.Int32, ServiceProviderId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, NotetakerLuCourseId)
			};
			List<LectureNoteDescription> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.DeletionDate,\r\n            nd.notetakerid AS serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.altid,sp.email,\r\n            nd.notes,nd.lecturedate,nd.lucourseid,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,\r\n            luc.section,luc.timeofday,lucd2.altlookupstring AS pinstructorname,luc.instructorid AS pinstructorid,\r\n            lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.externalid AS pinstructorexternalid,\r\n            lucd2.id AS pinstructoremployeeid,\r\n            luc.subjectid\r\nFROM        notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE       nd.notetakerid=@spid AND nd.lucourseid=@lucid\r\nORDER BY    nd.lecturedate", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LectureNoteDescription> list = new List<LectureNoteDescription>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						LectureNoteDescription lectureNoteDescriptionFromRecord = NotetakingDAO.GetLectureNoteDescriptionFromRecord(dataReader, batchDecryptor);
						bool flag2 = lectureNoteDescriptionFromRecord != null;
						if (flag2)
						{
							list.Add(lectureNoteDescriptionFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001E128 File Offset: 0x0001C328
		public LectureNote LoadLectureNoteById(int NotetakerDocumentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, NotetakerDocumentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.DeletionDate,\r\n            nd.notetakerid AS serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.altid,sp.email,nd.binarydata,\r\n            nd.notes,nd.lecturedate,nd.lucourseid,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,\r\n            luc.section,luc.timeofday,lucd2.altlookupstring AS pinstructorname,luc.instructorid AS pinstructorid,\r\n            lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.externalid AS pinstructorexternalid,\r\n            lucd2.id AS pinstructoremployeeid,\r\n            luc.subjectid\r\nFROM        notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE       nd.notetakerdocumentid=@id", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetLectureNoteFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		public List<NotetakerBaseWithLookupCourseBase> LoadMatchingNotetakersWithLectureNoteUploadsByCourse(int LuCourseId, int EquivalentSettingNum)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@equivalentsettingnum", DbType.Int32, EquivalentSettingNum)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT ec.lucourseid,nd.notetakerid AS serviceproviderid,\r\n\tsp.firstname,sp.lastname,sp.middlename,sp.student_no,sp.email,sp.altid,\r\n    luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,\r\n    luc.section,luc.timeofday,lucd2.altlookupstring AS pinstructorname,luc.instructorid AS pinstructorid,\r\n    lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.externalid AS pinstructorexternalid,\r\n    lucd2.id AS pinstructoremployeeid,\r\n    luc.subjectid\r\nFROM EquivalentCoursesChooser(@lucid,@equivalentsettingnum) ec LEFT JOIN NotetakerDocument nd \r\n\t\tON nd.LUCourseId=ec.lucourseid\r\nLEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=nd.NotetakerID\r\nLEFT JOIN lucourses luc ON luc.lucourseid=ec.lucourseid\r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.notetakerid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				List<NotetakerBaseWithLookupCourseBase> list = new List<NotetakerBaseWithLookupCourseBase>();
				NotetakerBase notetakerBaseFromRecord = this.GetNotetakerBaseFromRecord(dataReader);
				LookupCourseBase courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader("", dataReader);
				bool flag2 = notetakerBaseFromRecord == null || courseBaseFromReader == null;
				if (flag2)
				{
					return null;
				}
				NotetakerBaseWithLookupCourseBase item = new NotetakerBaseWithLookupCourseBase
				{
					Notetaker = notetakerBaseFromRecord,
					Course = courseBaseFromReader
				};
				list.Add(item);
			}
			return null;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001E290 File Offset: 0x0001C490
		public List<LookupCourseBase> LoadEquivalentCourses(int LuCourseId, int EquivalentSettingNum)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@equivalentsettingnum", DbType.Int32, EquivalentSettingNum)
			};
			List<LookupCourseBase> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    ec.lucourseid,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,\r\n            luc.section,luc.timeofday,lucd2.altlookupstring AS pinstructorname,luc.instructorid AS pinstructorid,\r\n            lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.externalid AS pinstructorexternalid,\r\n            lucd2.id AS pinstructoremployeeid,\r\n            luc.subjectid\r\nFROM        EquivalentCoursesChooser(@lucid,@equivalentsettingnum) ec LEFT JOIN lucourses luc ON luc.lucourseid=ec.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.notetakerid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupCourseBase> list = new List<LookupCourseBase>();
					while (dataReader.Read())
					{
						LookupCourseBase courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader("", dataReader);
						bool flag2 = courseBaseFromReader != null;
						if (flag2)
						{
							list.Add(courseBaseFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001E354 File Offset: 0x0001C554
		public void ChangeCourseRegistrationStatus(int ServiceProviderApplicationCourseId, eRegistrationStatus NewStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@ServiceProviderApplicationCourseId", DbType.Int32, ServiceProviderApplicationCourseId),
				this.DatabaseManager.GetParameter("@registrationstatus", DbType.Int32, (int)NewStatus)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE ServiceProviderApplicationCourses SET registrationstatus=@registrationstatus WHERE ServiceProviderApplicationCourseId=@ServiceProviderApplicationCourseId", parameters);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		public NotetakerCourseRegistration RegisterNotetakerInCourse(int ServiceProviderId, int Lucid, bool? ExemptCourseFromDataSyncForStudent = null)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spid", DbType.Int32, ServiceProviderId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, Lucid),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				(ExemptCourseFromDataSyncForStudent != null) ? this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, ExemptCourseFromDataSyncForStudent.Value) : this.DatabaseManager.GetParameter("@isexempt", DbType.Boolean, DBNull.Value)
			};
			NotetakerCourseRegistration result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("IF EXISTS(SELECT ServiceProviderApplicationCourseId FROM ServiceProviderApplicationCourses WHERE lucourseid=@lucid AND ServiceProviderApplicationId IN (SELECT ServiceProviderApplicationId FROM ServiceProviderApplications WHERE ServiceProviderId=@serviceproviderid))\r\nBEGIN\r\n    UPDATE ServiceProviderApplicationCourses SET registrationstatus=NULL,exemptfromdatasync=COALESCE(@isexempt,exemptfromdatasync) WHERE lucourseid=@lucid AND ServiceProviderApplicationId IN (SELECT ServiceProviderApplicationId FROM ServiceProviderApplications WHERE ServiceProviderId=@serviceproviderid)\r\n    SELECT ServiceProviderApplicationCourseId FROM ServiceProviderApplicationCourse WHERE lucourseid=@lucid AND ServiceProviderApplicationId IN (SELECT ServiceProviderApplicationId FROM ServiceProviderApplications WHERE ServiceProviderId=@serviceproviderid))\r\nEND\r\nELSE\r\nBEGIN\r\nTODO!!!\r\n    INSERT INTO ServiceProviderApplicationCourses (personid,lucourseid,whoadded,dateadded,exemptfromdatasync) VALUES (@pid,@lucid,@whoami,getdate(),COALESCE(@isexempt,0));\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS coursesid\r\nEND", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					int num = (int)dataReader[0];
					NotetakerCourseRegistration notetakerCourseRegistration = this.LoadCourseRegistration(ServiceProviderId, Lucid);
					bool flag2 = notetakerCourseRegistration == null || ExemptCourseFromDataSyncForStudent == null;
					if (flag2)
					{
						result = notetakerCourseRegistration;
					}
					else
					{
						bool flag3 = notetakerCourseRegistration.IsExemptFromDataSync == ExemptCourseFromDataSyncForStudent.Value;
						if (flag3)
						{
							result = notetakerCourseRegistration;
						}
						else
						{
							eRegistrationStatus newStatus = ExemptCourseFromDataSyncForStudent.Value ? eRegistrationStatus.NormalAndExemptFromDataSync : eRegistrationStatus.Normal;
							this.ChangeCourseRegistrationStatus(notetakerCourseRegistration.ServiceProviderApplicationCourseId, newStatus);
							notetakerCourseRegistration = this.LoadCourseRegistration(ServiceProviderId, Lucid);
							result = notetakerCourseRegistration;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00003998 File Offset: 0x00001B98
		public NotetakerCourseRegistration LoadCourseRegistration(int ServiceProviderId, int Lucid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001E52C File Offset: 0x0001C72C
		public int CreateOrRetrieveSpAppIdForCourses(int ServiceProviderId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spid", DbType.Int32, ServiceProviderId),
				this.DatabaseManager.GetParameter("@sptype", DbType.Int32, 128)
			};
			int result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("IF EXISTS(SELECT serviceproviderid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype)\r\n\tSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderid=@spid AND serviceprovidertype=@sptype\r\n\tELSE \r\n\tBEGIN\r\nINSERT INTO serviceproviderapplications (serviceproviderid,serviceprovidertype,note1,note2,dateentered,whoentered,ispermanent,isactive,isactivecomment) \r\nSELECT @spid,@sptype,'','',getdate(),1,NULL,1,NULL  \r\nSELECT serviceproviderapplicationid FROM serviceproviderapplications WHERE serviceproviderapplicationid=@@identity\r\nEND", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read() || dataReader["serviceproviderapplicationid"] is DBNull;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (int)dataReader["serviceproviderapplicationid"];
				}
			}
			return result;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		public void AddServiceProviderApplicationCourse(int spaid, int lucid)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@spa", DbType.Int32, spaid),
				this.DatabaseManager.GetParameter("@sptype", DbType.Int32, 128),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, lucid)
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spa AND serviceprovidertype=@sptype AND lucourseid=@lucid)\r\nBEGIN\r\n    SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spa AND serviceprovidertype=@sptype AND lucourseid=@lucid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO serviceproviderapplicationcourses \r\n            (serviceproviderapplicationid,serviceprovidertype,lucourseid,datecancelled) \r\n    VALUES  (@spa,@sptype,@lucid,NULL);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS serviceproviderapplicationcourseid\r\nEND", parameters);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001E66C File Offset: 0x0001C86C
		public int CreateNotetakerAccount(SPProvider Provider)
		{
			IEncryption encryption = this.DatabaseManager.Encryption;
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@serviceproviderid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@firstname", DbType.Binary, encryption.Encrypt(Provider.Person.FirstName.Trim())),
				this.DatabaseManager.GetParameter("@middlename", DbType.Binary, encryption.Encrypt((Provider.Person.MiddleName ?? "").Trim())),
				this.DatabaseManager.GetParameter("@lastname", DbType.Binary, encryption.Encrypt(Provider.Person.LastName.Trim())),
				this.DatabaseManager.GetParameter("@student_no", DbType.Binary, encryption.Encrypt(Provider.Person.Student_no.Trim())),
				this.DatabaseManager.GetParameter("@altid", DbType.Binary, (Provider.UserName.Length > 0) ? encryption.Encrypt(Provider.UserName) : DBNull.Value),
				this.DatabaseManager.GetParameter("@specialization", DbType.Binary, encryption.Encrypt((Provider.Specializations ?? "").Trim())),
				this.DatabaseManager.GetParameter("@notes1", DbType.Binary, encryption.Encrypt((Provider.Note1 ?? "").Trim())),
				this.DatabaseManager.GetParameter("@notes2", DbType.Binary, encryption.Encrypt((Provider.Note2 ?? "").Trim())),
				this.DatabaseManager.GetParameter("@email", DbType.Binary, encryption.Encrypt((Provider.Email ?? "").Trim())),
				this.DatabaseManager.GetParameter("@phone1", DbType.Binary, encryption.Encrypt((Provider.Phone1 ?? "").Trim())),
				this.DatabaseManager.GetParameter("@phone2", DbType.Binary, encryption.Encrypt((Provider.Phone2 ?? "").Trim())),
				this.DatabaseManager.GetParameter("@phonenote", DbType.Binary, encryption.Encrypt((Provider.PhoneNote ?? "").Trim())),
				this.DatabaseManager.GetParameter("@address", DbType.Binary, encryption.Encrypt((Provider.Address1 ?? "").Trim())),
				this.DatabaseManager.GetParameter("@address2", DbType.Binary, encryption.Encrypt((Provider.Address2 ?? "").Trim())),
				this.DatabaseManager.GetParameter("@email2", DbType.Binary, encryption.Encrypt((Provider.AlternateEmail ?? "").Trim())),
				this.DatabaseManager.GetParameter("@addressactive", DbType.Boolean, Provider.Address1IsPrimary),
				this.DatabaseManager.GetParameter("@address2active", DbType.Boolean, !Provider.Address1IsPrimary)
			};
			int result;
			using (this.DatabaseManager.ExecuteQueryReader("IF NOT EXISTS(SELECT serviceproviderid FROM ServiceProviders WHERE (NOT @altid IS NULL AND altid=@altid) OR (NOT @student_no IS NULL AND student_no=@student_no))\r\nBEGIN\r\n    INSERT INTO ServiceProviders (firstname,middlename,lastname,student_no,altid,specialization,notes1,notes2,email,phone1,phone2,phonenote,address,address2,email2,addressactive,address2active)\r\n    VALUES (@firstname,@middlename,@lastname,@student_no,@altid,@specialization,@notes1,@notes2,@email,@phone1,@phone2,@phonenote,@address,@address2,@email2,@addressactive,@address2active);\r\n    SET @serviceproviderid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))\r\nEND\r\nELSE \r\n    SET @serviceproviderid=0", array))
			{
				bool flag = array[0].Value is DBNull || !(array[0].Value is int) || (int)array[0].Value < 1;
				if (flag)
				{
					CWLogger.Logger.Error("NotetakingDAO.CreateNotetakerAccount:UnableToCreateNotetakerAccount:snum={0}", (Provider == null) ? "NULL1" : ((Provider.Person == null) ? "NULL2" : (Provider.Person.Student_no ?? "NULL")));
					result = 0;
				}
				else
				{
					result = (int)array[0].Value;
				}
			}
			return result;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001EA5C File Offset: 0x0001CC5C
		public void RecordStudentDownloadedLectureNote(int PersonId, int NotetakerDocumentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@docid", DbType.Int32, NotetakerDocumentId)
			};
			this.DatabaseManager.ExecuteNonQuery("DECLARE @lucid int\r\nSET @lucid=(SELECT TOP 1 lucourseid FROM notetakerdocument WHERE notetakerdocumentid=@docid)\r\nINSERT INTO NotetakerDocumentStudentDownloads (personid,lucourseid,notetakerdocumentid) VALUES (@pid,@lucid,@docid)", parameters);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001EABC File Offset: 0x0001CCBC
		public IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistory(int PersonId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			IList<DownloadedLectureNote> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT nds.personid,nds.lucourseid,nds.notetakerdocumentid,nds.datedownloaded,\r\n         p.firstname,p.middlename,p.lastname,p.student_no,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,luc.course,luc.[section],luc.timeofday,\r\n         nd.docname,nd.sizeinbytes,nd.datecreated,nd.notetakerid,nd.notetakerid AS serviceproviderid,nd.notes,nd.lecturedate,nd.issamplenotes,nd.deletiondate,\r\n         sp.firstname,sp.lastname,sp.student_no,sp.altid,sp.email,sp.phone1,sp.phone2\r\nFROM     NotetakerDocumentStudentDownloads nds LEFT JOIN people p ON p.personid=nds.personid\r\n         LEFT JOIN lucourses luc ON luc.lucourseid=nds.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n         LEFT JOIN notetakerdocument nd ON nd.notetakerdocumentid=nds.notetakerdocumentid\r\n         LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid\r\nWHERE    nds.personid=@pid AND nds.lucourseid=@lucid\r\nORDER BY nds.datedownloaded DESC", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DownloadedLectureNote> list = new List<DownloadedLectureNote>();
					while (dataReader.Read())
					{
						DownloadedLectureNote downloadedLectureNoteFromRecord = this.GetDownloadedLectureNoteFromRecord(dataReader);
						bool flag2 = downloadedLectureNoteFromRecord != null;
						if (flag2)
						{
							list.Add(downloadedLectureNoteFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		public IList<DownloadedLectureNote> LoadStudentDownloadedLectureNoteHistoryLastDateDownloadedForEachLectureNote(int PersonId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			IList<DownloadedLectureNote> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT nds.personid,nds.lucourseid,nds.notetakerdocumentid,nds.datedownloaded,\r\n         p.firstname,p.middlename,p.lastname,p.student_no,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,luc.course,luc.[section],luc.timeofday,\r\n         nds.docname,nds.sizeinbytes,nds.datecreated,nds.notetakerid,nds.notetakerid AS serviceproviderid,nds.notes,nds.lecturedate,nds.issamplenotes,nds.deletiondate,\r\n         sp.firstname,sp.lastname,sp.student_no,sp.altid,sp.email,sp.phone1,sp.phone2\r\nFROM     NotetakingStudentDownloadHistoryMaxDatePerLectureNote nds LEFT JOIN people p ON p.personid=nds.personid\r\n         LEFT JOIN lucourses luc ON luc.lucourseid=nds.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n         LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nds.notetakerid\r\nWHERE    nds.personid=@pid AND nds.lucourseid=@lucid\r\nORDER BY nds.lecturedate DESC", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DownloadedLectureNote> list = new List<DownloadedLectureNote>();
					while (dataReader.Read())
					{
						DownloadedLectureNote downloadedLectureNoteFromRecord = this.GetDownloadedLectureNoteFromRecord(dataReader);
						bool flag2 = downloadedLectureNoteFromRecord != null;
						if (flag2)
						{
							list.Add(downloadedLectureNoteFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001EC3C File Offset: 0x0001CE3C
		public NotetakerBase LoadNotetakerBaseByEmail(string Email)
		{
			bool flag = Email == null || Email.Trim().Length < 1;
			NotetakerBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@emailu", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Email.ToUpper())),
					this.DatabaseManager.GetParameter("@emaill", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(Email.ToLower()))
				};
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sp.serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.email,sp.altid\r\nFROM        serviceproviders sp \r\nWHERE       sp.isactive=1 AND\r\n            (\r\n                sp.email=@emailu OR sp.email=@emaill\r\n            )", parameters))
				{
					bool flag2 = dataReader != null && dataReader.Read();
					if (flag2)
					{
						return this.GetNotetakerBaseFromRecord(dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001ED18 File Offset: 0x0001CF18
		public int CreateLectureNote(LectureNote lectureNote)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("notetakerdocumentid", DbType.Int32, 0),
				databaseLayer.GetParameter("@spid", DbType.Int32, lectureNote.LectureNoteDescription.NotetakerBaseInfo.ServiceProviderId),
				databaseLayer.GetParameter("@lucid", DbType.Int32, lectureNote.LectureNoteDescription.CourseBaseInfo.LuCourseId),
				databaseLayer.GetParameter("@comment", DbType.String, lectureNote.LectureNoteDescription.Comment ?? ""),
				databaseLayer.GetParameter("@docname", DbType.String, (lectureNote.LectureNoteDocument == null) ? "" : (lectureNote.LectureNoteDocument.FileName ?? "")),
				databaseLayer.GetParameter("@binarydata", DbType.Binary, (lectureNote.LectureNoteDocument == null) ? DBNull.Value : lectureNote.LectureNoteDocument.ByteArray),
				databaseLayer.GetParameter("@lecturedate", DbType.DateTime, lectureNote.LectureNoteDescription.LectureDate),
				databaseLayer.GetParameter("@sizeinbytes", DbType.Int32, (lectureNote.LectureNoteDocument == null || lectureNote.LectureNoteDocument.ByteArray == null) ? 0 : lectureNote.LectureNoteDocument.ByteArray.Length)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO notetakerdocument (docname,numpages,sizeinbytes,datecreated,binarydata,notetakerid,lucourseid,notes,lecturedate,issamplenotes) \r\nVALUES (@docname,0,@sizeinbytes,getdate(),@binarydata,@spid,@lucid,@comment,@lecturedate,0);\r\n\r\nSET @notetakerdocumentid=(SELECT CAST(SCOPE_IDENTITY() AS int) AS notetakerdocumentid)", CommandOverrideSettings.CommandOverrideSettingsTimeout180, array);
			object value = array[0].Value;
			bool flag = value == null || value is DBNull || !(value is int);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)value;
			}
			return result;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		public void UpdateLectureNote(LectureNote lectureNote)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@notetakerdocumentid", DbType.Int32, lectureNote.NotetakerDocumentId),
				databaseLayer.GetParameter("@comment", DbType.String, lectureNote.LectureNoteDescription.Comment ?? ""),
				databaseLayer.GetParameter("@docname", DbType.String, (lectureNote.LectureNoteDocument == null) ? "" : (lectureNote.LectureNoteDocument.FileName ?? "")),
				databaseLayer.GetParameter("@binarydata", DbType.Binary, (lectureNote.LectureNoteDocument == null) ? DBNull.Value : lectureNote.LectureNoteDocument.ByteArray),
				databaseLayer.GetParameter("@lecturedate", DbType.DateTime, lectureNote.LectureNoteDescription.LectureDate),
				databaseLayer.GetParameter("@sizeinbytes", DbType.Int32, (lectureNote.LectureNoteDocument == null || lectureNote.LectureNoteDocument.ByteArray == null) ? 0 : lectureNote.LectureNoteDocument.ByteArray.Length)
			};
			databaseLayer.ExecuteNonQuery("UPDATE notetakerdocument \r\nSET docname=@docname,sizeinbytes=@sizeinbytes,binarydata=@binarydata,notes=@comment,lecturedate=@lecturedate\r\nWHERE notetakerdocumentid=@notetakerdocumentid", CommandOverrideSettings.CommandOverrideSettingsTimeout180, parameters);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		public void DeleteLectureNote(int NotetakerDocumentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@notetakerdocumentid", DbType.Int32, NotetakerDocumentId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM notetakerdocument WHERE notetakerdocumentid=@notetakerdocumentid", parameters);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001F04C File Offset: 0x0001D24C
		public IList<DateTime> LoadUniqueCourseStartDatesForNotetakerAvailableCourses(int NotetakerId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@spid", DbType.Int32, NotetakerId)
			};
			IList<DateTime> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tDISTINCT spac.lucourseid INTO #t1\r\nFROM\tserviceproviderapplications spa LEFT JOIN serviceproviderapplicationcourses spac ON spac.ServiceProviderApplicationId=spa.ServiceProviderApplicationId\r\nWHERE\tspa.serviceproviderid=@spid AND spa.serviceprovidertype=128 AND (spac.registrationstatus IS NULL OR NOT spac.registrationstatus=2)\r\n\r\nSELECT DISTINCT x.startdate FROM\r\n(\r\nSELECT luc.startdate FROM #t1 LEFT JOIN lucourses luc ON luc.LUCourseID=#t1.lucourseid\r\nUNION\r\nSELECT luc.enddate AS startdate FROM #t1 LEFT JOIN lucourses luc ON luc.LUCourseID=#t1.lucourseid\r\n) x\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DateTime> list = new List<DateTime>();
					while (dataReader.Read())
					{
						bool flag2 = dataReader["startdate"] != DBNull.Value;
						if (flag2)
						{
							list.Add((DateTime)dataReader["startdate"]);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001F118 File Offset: 0x0001D318
		public IList<LookupCourseBase> LoadNotetakerAvailableCourses(int NotetakerId, DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@spid", DbType.Int32, NotetakerId),
				databaseLayer.GetParameter("@sd", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@ed", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0))
			};
			IList<LookupCourseBase> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tDISTINCT spac.lucourseid INTO #t1\r\nFROM\tserviceproviderapplications spa LEFT JOIN serviceproviderapplicationcourses spac ON spac.ServiceProviderApplicationId=spa.ServiceProviderApplicationId\r\n\t\tLEFT JOIN lucourses luc ON luc.LUCourseID=spac.lucourseid\r\nWHERE\tspa.serviceproviderid=@spid AND spa.serviceprovidertype=128 AND (spac.registrationstatus IS NULL OR NOT spac.registrationstatus=2)\r\n        AND NOT ( ( luc.enddate<@sd ) OR (luc.startdate > @ed ) )\r\n\r\nSELECT\t#t1.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS [subject],luc.course,\r\n\t\tluc.section,luc.timeofday,luc.campus,luc.subjectid\r\nFROM\t#t1 LEFT JOIN lucourses luc ON luc.lucourseid=#t1.lucourseid\r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LookupCourseBase> list = new List<LookupCourseBase>();
					while (dataReader.Read())
					{
						LookupCourseBase course = LookupCourseDAO.GetCourseBaseFromReader("", dataReader);
						bool flag2 = course != null && list.FirstOrDefault((LookupCourseBase g) => g.LuCourseId == course.LuCourseId) == null;
						if (flag2)
						{
							list.Add(course);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001F25C File Offset: 0x0001D45C
		private ServiceRequestBase GetServiceRequestBaseFromRecord(IDataReader record, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null || record["serviceproviderrequestid"] is DBNull;
			ServiceRequestBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ServiceRequestBase
				{
					ServiceProviderRequestId = (int)record["serviceproviderrequestid"],
					CourseBase = LookupCourseDAO.GetCourseBaseFromReader("", record),
					Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor),
					AssignedServiceProviderId = ((record["serviceproviderid"] is DBNull) ? 0 : ((int)record["serviceproviderid"])),
					AssignedServiceProviderCourse = LookupCourseDAO.GetCourseBaseFromReader("sp_", record),
					IsAssignedPrivate = false
				};
			}
			return result;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001F324 File Offset: 0x0001D524
		public IList<ServiceRequestBase> LoadUniqueStudentsReceivingNotes(int NotetakerId, int LuCourseId, int ServiceProviderType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@spid", DbType.Int32, NotetakerId),
				databaseLayer.GetParameter("@lucid", DbType.Int32, LuCourseId),
				databaseLayer.GetParameter("@sptype", DbType.Int32, ServiceProviderType)
			};
			IList<ServiceRequestBase> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tr.serviceproviderrequestid,r.ServiceProviderId,r.serviceproviderlucourseid,\r\n\t\tr.personid,p.lastName,p.firstName,p.middleName,p.student_no,\r\n\t\t\r\n\t\tr.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.[term],luc.[subjectid],luc.course,luc.[section],\r\n\t\tluc.timeofday,luc.campus,lucd.altLookupString AS [subject],luc.department,luc.[location],luc.coursenote,\r\n\r\n\t\tr.serviceproviderlucourseid,luc2.LUCourseID AS sp_lucourseid,luc2.startdate AS sp_startdate,luc2.enddate AS sp_enddate,luc2.duration AS sp_duration,luc2.[term] AS sp_term,\t\r\n\t\tluc2.[subjectid] AS sp_subjectid,luc2.course AS sp_course,luc2.[section] AS sp_section,\r\n\t\tluc2.timeofday AS sp_timeofday,luc2.campus AS sp_campus,lucd2.altLookupString AS sp_subject,luc2.department AS sp_department,\r\n\t\tluc2.[location] AS sp_location,luc2.coursenote AS sp_coursenote\r\nFROM\tServiceProviderRequests r LEFT JOIN people p ON p.PersonID=r.personid\r\n\t\tLEFT JOIN lucourses luc ON luc.LUCourseID=r.lucourseid\r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN lucourses luc2 ON luc2.LUCourseID=r.serviceproviderlucourseid\r\n\t\tLEFT JOIN lucoursedata lucd2 ON lucd2.luCourseDataID=luc2.SubjectID \r\nWHERE\tr.IsActive=1 AND r.serviceprovidertype=@sptype\r\n\t\tAND NOT r.ServiceProviderId IS NULL AND r.ServiceProviderId=@spid \r\n\t\tAND r.serviceproviderlucourseid=@lucid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<ServiceRequestBase> list = new List<ServiceRequestBase>();
					while (dataReader.Read())
					{
						ServiceRequestBase serviceRequestBaseFromRecord = this.GetServiceRequestBaseFromRecord(dataReader, batchDecryptor);
						bool flag2 = serviceRequestBaseFromRecord != null;
						if (flag2)
						{
							list.Add(serviceRequestBaseFromRecord);
						}
					}
					result = list.RemoveDuplicateItemsFromList(delegate(Pair<ServiceRequestBase, ServiceRequestBase> g)
					{
						ServiceRequestBase item = g.Item1;
						ServiceRequestBase item2 = g.Item2;
						PersonBase personBase = item.Student ?? new PersonBase();
						PersonBase personBase2 = item2.Student ?? new PersonBase();
						int num = (personBase.LastName ?? "").CompareTo(personBase2.LastName ?? "");
						bool flag3 = num != 0;
						int result2;
						if (flag3)
						{
							result2 = num;
						}
						else
						{
							num = (personBase.FirstName ?? "").CompareTo(personBase2.FirstName ?? "");
							bool flag4 = num != 0;
							if (flag4)
							{
								result2 = num;
							}
							else
							{
								num = (personBase.MiddleName ?? "").CompareTo(personBase2.MiddleName ?? "");
								bool flag5 = num != 0;
								if (flag5)
								{
									result2 = num;
								}
								else
								{
									num = (personBase.Student_no ?? "").CompareTo(personBase2.Student_no ?? "");
									bool flag6 = num != 0;
									if (flag6)
									{
										result2 = num;
									}
									else
									{
										result2 = ((item.CourseBase == null) ? 0 : item.CourseBase.LuCourseId).CompareTo((item2.CourseBase == null) ? 0 : item2.CourseBase.LuCourseId);
									}
								}
							}
						}
						return result2;
					});
				}
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001F43C File Offset: 0x0001D63C
		public List<LectureNoteDescription> LoadLectureNoteDescriptionsByStudentAndCourse(int StudentPersonId, int StudentLuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@studentpid", DbType.Int32, StudentPersonId),
				this.DatabaseManager.GetParameter("@studentlucid", DbType.Int32, StudentLuCourseId)
			};
			List<LectureNoteDescription> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT serviceproviderid,serviceproviderlucourseid INTO #t1 FROM ServiceProviderRequests WHERE personid=@studentpid AND NOT lucourseid IS NULL AND lucourseid=@studentlucid AND NOT serviceproviderid IS NULL AND NOT serviceproviderlucourseid IS NULL\r\n\r\nSELECT lucourseid INTO #tlucids FROM EquivalentCourses(@studentlucid)\r\n\r\n--add in possible other serviceproviders with notes\r\nINSERT INTO #t1 (serviceproviderid,serviceproviderlucourseid)\r\n\tSELECT DISTINCT notetakerid AS serviceproviderid,LUCourseId AS serviceproviderlucourseid FROM NotetakerDocument WHERE lucourseid IN (SELECT lucourseid FROM #tlucids)\r\n\r\nSELECT    nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.DeletionDate,\r\n            nd.notetakerid AS serviceproviderid,sp.firstname,sp.middlename,sp.lastname,sp.student_no,\r\n            sp.altid,sp.email,\r\n            nd.notes,nd.lecturedate,nd.lucourseid,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,\r\n            luc.section,luc.timeofday,lucd2.altlookupstring AS pinstructorname,luc.instructorid AS pinstructorid,\r\n            lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.externalid AS pinstructorexternalid,\r\n            lucd2.id AS pinstructoremployeeid,\r\n            luc.subjectid\r\nFROM        notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE       EXISTS(SELECT serviceproviderid FROM #t1 WHERE serviceproviderid=nd.NotetakerID AND serviceproviderlucourseid=nd.lucourseid)\r\nORDER BY    nd.lecturedate\r\n\r\nDROP TABLE #t1\r\nDROP TABLE #tlucids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LectureNoteDescription> list = new List<LectureNoteDescription>();
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						LectureNoteDescription lectureNoteDescriptionFromRecord = NotetakingDAO.GetLectureNoteDescriptionFromRecord(dataReader, batchDecryptor);
						bool flag2 = lectureNoteDescriptionFromRecord != null;
						if (flag2)
						{
							list.Add(lectureNoteDescriptionFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001F510 File Offset: 0x0001D710
		public bool AssignNotetaker(int studentPid, int studentLucid, int serviceProviderId, int serviceProviderLucid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@spid", DbType.Int32, serviceProviderId),
				databaseLayer.GetParameter("@lucid", DbType.Int32, studentLucid),
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@splucid", DbType.Int32, serviceProviderLucid)
			};
			bool result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("UPDATE serviceproviderrequests SET serviceproviderid=@spid,dateassigned=getdate(),serviceproviderlucourseid=@splucid WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128\r\n\r\n--load all students getting notes from this notetaker and course now\r\nSELECT\tDISTINCT r.personid\r\nFROM\tServiceProviderRequests r \r\nWHERE\tr.IsActive=1 AND r.serviceprovidertype=128\r\n\t\tAND NOT r.ServiceProviderId IS NULL AND r.ServiceProviderId=@spid \r\n\t\tAND r.serviceproviderlucourseid=@splucid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = false;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						list.Add((dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]));
					}
					CWLogger logger = CWLogger.Logger;
					string message = "NotetakingDAO:AssignNotetaker:pid={0}:lucid={1}:spid={2}:splucid={3}:pids={4}";
					object[] array = new object[5];
					array[0] = studentPid.ToString();
					array[1] = studentLucid.ToString();
					array[2] = serviceProviderId.ToString();
					array[3] = serviceProviderLucid.ToString();
					array[4] = string.Join(",", (from g in list
					select g.ToString()).ToArray<string>());
					logger.Debug(message, array);
					int[] array2 = (from g in list
					where g > 0 && g != studentPid
					select g).ToArray<int>();
					result = (array2.Length < 1);
				}
			}
			return result;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		public NotetakerBaseWithLookupCourseBase CancelNotetakerAssignment(int studentPid, int studentLucid, string why)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPid),
				databaseLayer.GetParameter("@lucid", DbType.Int32, studentLucid),
				databaseLayer.GetParameter("@note", DbType.Binary, string.IsNullOrEmpty(why) ? DBNull.Value : databaseLayer.Encryption.Encrypt(why.Trim()))
			};
			NotetakerBaseWithLookupCourseBase result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    ServiceProviderRequestID,personid,lucourseid,datetimerequesttitle,startdatetimerequest,enddatetimerequest,serviceprovidertype,\r\n            dateentered,startdate,enddate,whoentered,ServiceProviderId,ServiceProviderRequestDetailId,notes,studentrequested,\r\n            studentrequestedcancelnote,DateAssigned,SpecialInstructions,partsgroupid,partsdescription,serviceproviderlucourseid,\r\n            CAST(0 AS bit) AS stillactive \r\nINTO #t1\r\nFROM        serviceproviderrequests \r\nWHERE       isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128\r\n\r\nINSERT INTO serviceproviderrequestshistory \r\n    (ServiceProviderRequestID,personid,lucourseid,datetimerequesttitle,startdatetimerequest,enddatetimerequest,serviceprovidertype,\r\n    dateentered,startdate,enddate,whoentered,ServiceProviderId,ServiceProviderRequestDetailId,notes,studentrequested,studentrequestedcancelnote,\r\n    DateAssigned,SpecialInstructions,partsgroupid,partsdescription,serviceproviderlucourseid,stillactive) \r\n    SELECT  * FROM #t1\r\n\r\nUPDATE      serviceproviderrequests SET dateassigned=NULL,serviceproviderid=NULL,studentrequested=0,studentrequestedcancelnote=@note,\r\n            serviceproviderlucourseid=NULL WHERE isactive=1 AND personid=@pid AND lucourseid=@lucid AND serviceprovidertype=128\r\n\r\nSELECT  DISTINCT #t1.ServiceProviderId,sp.email,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.altid,\r\n        #t1.serviceproviderlucourseid AS lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n        luc.course,luc.[section],luc.timeofday,luc.campus,luc.department,luc.location,luc.coursenote\r\nFROM    #t1 LEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=#t1.ServiceProviderId\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=#t1.serviceproviderlucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new NotetakerBaseWithLookupCourseBase
					{
						Notetaker = this.GetNotetakerBaseFromRecord(dataReader),
						Course = LookupCourseDAO.GetCourseBaseFromReader("", dataReader)
					};
				}
			}
			return result;
		}

		// Token: 0x04000174 RID: 372
		private LookupCourseDAO lcd;
	}
}

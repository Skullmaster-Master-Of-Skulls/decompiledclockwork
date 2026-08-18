using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Entity.Accommodations;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.CourseRegistrations;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000D9 RID: 217
	public class AccommodationsDAO : IAccommodationsDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00037114 File Offset: 0x00035314
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0003711C File Offset: 0x0003531C
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00037128 File Offset: 0x00035328
		private DynamicFormsDAO dynamicFormsDAO
		{
			get
			{
				bool flag = this.dfd == null;
				if (flag)
				{
					this.dfd = new DynamicFormsDAO(this.OpContext);
				}
				return this.dfd;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00037160 File Offset: 0x00035360
		private DynamicDataDAO dynamicDataDao
		{
			get
			{
				bool flag = this._dynamicDataDao == null;
				if (flag)
				{
					this._dynamicDataDao = new DynamicDataDAO(this.OpContext);
				}
				return this._dynamicDataDao;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00037198 File Offset: 0x00035398
		private PersonDAO personDAO
		{
			get
			{
				bool flag = this.pd == null;
				if (flag)
				{
					this.pd = new PersonDAO(this.OpContext);
				}
				return this.pd;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000371D0 File Offset: 0x000353D0
		private LookupCourseDAO lookupCourseDAO
		{
			get
			{
				bool flag = this.ld == null;
				if (flag)
				{
					this.ld = new LookupCourseDAO(this.OpContext);
				}
				return this.ld;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00037206 File Offset: 0x00035406
		public AccommodationsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00037237 File Offset: 0x00035437
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0003723F File Offset: 0x0003543F
		public OperationContext OpContext { get; set; }

		// Token: 0x060005E5 RID: 1509 RVA: 0x00037248 File Offset: 0x00035448
		private string GetAccommodationsString(List<AccommodationData> accommodations, AccommodationListFormattingInfoDAO formattingInfo, TempCache tempCache, string counterName = null)
		{
			bool flag = tempCache == null;
			if (flag)
			{
				tempCache = new TempCache();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(formattingInfo.itemHeader);
			bool flag2 = accommodations == null || accommodations.Count < 1;
			string result;
			if (flag2)
			{
				result = formattingInfo.emptyListString;
			}
			else
			{
				List<string> list = new List<string>();
				TempCacheObject tempCacheObject = (!string.IsNullOrEmpty(counterName)) ? (tempCache.ContainsKey(counterName) ? tempCache[counterName] : tempCache.AddLocalItem(counterName, 1)) : null;
				int num = (tempCacheObject == null) ? 1 : ((int)tempCacheObject.Object);
				for (int i = 0; i < accommodations.Count; i++)
				{
					AccommodationData accommodationData = accommodations[i];
					string @string = accommodationData.GetString();
					bool flag3 = !list.Contains(@string);
					if (flag3)
					{
						list.Add(@string);
						bool flag4 = i > 0;
						if (flag4)
						{
							stringBuilder.Append(formattingInfo.itemNewline);
						}
						stringBuilder.AppendFormat("{0}{1}{2}", formattingInfo.itemPre.Replace("{ctr}", num++.ToString()), @string, formattingInfo.itemPost);
					}
				}
				stringBuilder.Append(formattingInfo.itemFooter);
				bool flag5 = tempCacheObject != null;
				if (flag5)
				{
					tempCacheObject.Object = num;
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000373B0 File Offset: 0x000355B0
		public AccommodationListFormattingInfoDAO LoadAccommodationListFormattingInfo(string bulletPrePostNewlineHeaderFooter)
		{
			AccommodationListFormattingInfoDAO accommodationListFormattingInfoDAO = new AccommodationListFormattingInfoDAO();
			bool flag = string.IsNullOrEmpty(bulletPrePostNewlineHeaderFooter);
			AccommodationListFormattingInfoDAO result;
			if (flag)
			{
				accommodationListFormattingInfoDAO.itemNewline = "\n";
				accommodationListFormattingInfoDAO.itemPre = "• ";
				result = accommodationListFormattingInfoDAO;
			}
			else
			{
				bulletPrePostNewlineHeaderFooter = bulletPrePostNewlineHeaderFooter.Replace("\\n", "\n");
				bulletPrePostNewlineHeaderFooter = bulletPrePostNewlineHeaderFooter.Replace("\\b", '\u0095'.ToString());
				string[] array = bulletPrePostNewlineHeaderFooter.Split(new char[]
				{
					'`'
				});
				bool flag2 = array.Length >= 5;
				if (flag2)
				{
					accommodationListFormattingInfoDAO.itemPre = array[0];
					accommodationListFormattingInfoDAO.itemPost = array[1];
					accommodationListFormattingInfoDAO.itemNewline = array[2];
					accommodationListFormattingInfoDAO.itemHeader = array[3];
					accommodationListFormattingInfoDAO.itemFooter = array[4];
				}
				result = accommodationListFormattingInfoDAO;
			}
			return result;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00037478 File Offset: 0x00035678
		public static AccommodationListFormattingInfoDAO GetHtmlListFormatting()
		{
			return new AccommodationListFormattingInfoDAO
			{
				itemFooter = "</ul>",
				itemHeader = "<ul>",
				itemNewline = "",
				itemPre = "<li>",
				itemPost = "</li>",
				emptyListString = "None."
			};
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000374D8 File Offset: 0x000356D8
		public string GetAccommodationsListStringAsHtml(List<AccommodationData> alist, string mailMergeCode)
		{
			AccommodationListFormattingInfoDAO htmlListFormatting = AccommodationsDAO.GetHtmlListFormatting();
			return this.GetAccommodationsListString(alist, mailMergeCode, htmlListFormatting, null, null);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000374FC File Offset: 0x000356FC
		public string GetAccommodationsListString(List<AccommodationData> alist, string mailMergeCode, AccommodationListFormattingInfoDAO formattingInfo, TempCache tempCache = null, string listCounterName = null)
		{
			bool flag = tempCache == null;
			if (flag)
			{
				tempCache = new TempCache();
			}
			bool flag2 = formattingInfo == null;
			if (flag2)
			{
				formattingInfo = this.LoadAccommodationListFormattingInfo("");
			}
			string accommodationsString;
			if (!(mailMergeCode == "accommodationsline"))
			{
				if (!(mailMergeCode == "accommodationsfr"))
				{
					accommodationsString = this.GetAccommodationsString(alist, formattingInfo, tempCache, listCounterName);
				}
				else
				{
					accommodationsString = this.GetAccommodationsString(alist, formattingInfo, tempCache, listCounterName);
				}
			}
			else
			{
				formattingInfo.itemFooter = "";
				formattingInfo.itemHeader = "";
				formattingInfo.itemNewline = ", ";
				formattingInfo.itemPre = "";
				formattingInfo.itemPost = "";
				formattingInfo.emptyListString = "";
				accommodationsString = this.GetAccommodationsString(alist, formattingInfo, tempCache, listCounterName);
			}
			return accommodationsString;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000375D4 File Offset: 0x000357D4
		public List<DynamicDataChange> LoadAccommodationChanges(int WhoAmI, int PersonId, int LuCourseId, DateTime SinceDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@sincedate", DbType.DateTime, SinceDate)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\taa.accommodationsapprovalid,aa.personid,aa.lucourseid,aa.controlid,\r\n\t\taa.newint AS valint,aa.newdatetime AS valdate,aa.newbytes AS valbytes,aa.newimage AS valimage,aa.[status],\r\n\t\taa.dateentered,aa.whoentered,p.firstname AS whoenteredfirst,p.lastname AS whoenteredlast,\r\n\t\tad.dataid,ad.controlcaption,ad.valtext,ad.valint AS valint2,ad.valbytes AS valbytes2,ad.valdate AS valdate2,ad.valimage AS valimage2,\r\n\t\tad.setting1,ad.setting2,ad.setting3,ad.setting4,ad.defaultvalue,ad.controlcode,ad.valbytesisencrypted,\r\n\t\tad.[offline],ad.expirydate,ad.altlongdescription,ad.note,ad.showonletter AS showonletter2,\r\n        a.showonletter AS showonletter1,\r\n\t\tad.recommendedbutdeclined,ad.rationale,sessiondateentered,ad.recommendedbutdeclineddetail \r\nFROM\taccommodationsapproval aa LEFT JOIN accommodationdata ad ON ad.PersonID=aa.personid AND ad.courseid=aa.lucourseid\r\n\t\tLEFT JOIN people p ON p.personid=aa.whoentered\r\n        LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE\taa.AccommodationsApprovalId IN (SELECT AccommodationsApprovalId FROM AccommodationsApproval WHERE dateentered>=@sincedate)\r\n\t\tAND aa.personid=@pid AND aa.lucourseid=@lucid\r\nORDER BY aa.controlid,aa.dateentered", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<DynamicDataChange> list = new List<DynamicDataChange>();
					DynamicFormsDAO dynamicFormsDAO = this.dynamicFormsDAO;
					PersonDAO personDAO = this.personDAO;
					object previousValue = null;
					while (dataReader.Read())
					{
						int num = (dataReader["status"] == DBNull.Value) ? 0 : ((int)dataReader["status"]);
						Person personFromReader = personDAO.GetPersonFromReader(dataReader, "whoentered", "whoenteredfirst", "", "whoenteredlast", "");
						bool flag2 = personFromReader == null;
						PersonBase whoLastChanged;
						if (flag2)
						{
							whoLastChanged = null;
						}
						else
						{
							whoLastChanged = new PersonBase
							{
								FirstName = personFromReader.FirstName,
								LastName = personFromReader.LastName,
								MiddleName = personFromReader.MiddleName,
								Student_no = personFromReader.Id,
								PersonId = personFromReader.PersonID
							};
						}
						DynamicDataChange dynamicDataChange = new DynamicDataChange
						{
							Data = this.dynamicDataDao.GetDataFromRecords(dataReader),
							ChangeAction = (eDynamicDataChangeAction)(Enum.IsDefined(typeof(eDynamicDataChangeAction), num) ? num : 0),
							LastDateOfChange = (DateTime)dataReader["dateentered"],
							WhoLastChanged = whoLastChanged,
							Context = new DynamicDataContext
							{
								PrimaryId = PersonId,
								SecondaryId = LuCourseId
							},
							Id = (int)dataReader["accommodationsapprovalid"],
							PreviousValue = previousValue
						};
						previousValue = dynamicDataChange.Data.Value;
						list.Add(dynamicDataChange);
					}
					List<DynamicDataChange> list2 = list.FindAll((DynamicDataChange d) => d.LastDateOfChange >= SinceDate);
					this.dynamicDataDao.MergeDynamicDataIntoUniqueControlIds<DynamicDataChange>(list2);
					return list2;
				}
			}
			return null;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0003785C File Offset: 0x00035A5C
		public DateTime? GetStudentAccommodationsExpiryDate(int PersonId, int AccommodationsExpiryDateControlId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@cid", DbType.Int32, AccommodationsExpiryDateControlId)
			};
			object obj = this.DatabaseManager.ExecuteScalar("SELECT controlvalue FROM datetimeinfoaccommodationps WHERE courseid=0 AND personid=@pid AND controlid=@cid", parameters);
			bool flag = obj != null && obj is DateTime;
			DateTime? result;
			if (flag)
			{
				result = new DateTime?(((DateTime)obj).Date);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000378F4 File Offset: 0x00035AF4
		public IDictionary<int, DateTime?> LoadAccommodationExpiryDatesForStudents(int[] pids, int expiryDateCid)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", (from g in pids
			select g.ToString()).ToArray<string>()));
			array[1] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, expiryDateCid);
			DbParameter[] parameters = array;
			IDictionary<int, DateTime?> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT personid,controlvalue FROM datetimeinfoaccommodationps WHERE courseid=0 AND controlid=@cid AND personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) ORDER BY personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, DateTime?> dictionary = pids.Distinct<int>().ToDictionary((int pid) => pid, (int pid) => null);
					while (dataReader.Read())
					{
						int num = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						bool flag2 = num < 1 || !dictionary.ContainsKey(num);
						if (!flag2)
						{
							dictionary[num] = ((dataReader["controlvalue"] is DBNull) ? null : new DateTime?((DateTime)dataReader["controlvalue"]));
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00037A90 File Offset: 0x00035C90
		[DebuggerStepThrough]
		public Task<IDictionary<int, DateTime?>> LoadAccommodationExpiryDatesForStudentsAsync(int[] pids, int expiryDateCid)
		{
			AccommodationsDAO.<LoadAccommodationExpiryDatesForStudentsAsync>d__29 <LoadAccommodationExpiryDatesForStudentsAsync>d__ = new AccommodationsDAO.<LoadAccommodationExpiryDatesForStudentsAsync>d__29();
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDictionary<int, DateTime?>>.Create();
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>4__this = this;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.pids = pids;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.expiryDateCid = expiryDateCid;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>1__state = -1;
			<LoadAccommodationExpiryDatesForStudentsAsync>d__.<>t__builder.Start<AccommodationsDAO.<LoadAccommodationExpiryDatesForStudentsAsync>d__29>(ref <LoadAccommodationExpiryDatesForStudentsAsync>d__);
			return <LoadAccommodationExpiryDatesForStudentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00037AE4 File Offset: 0x00035CE4
		public AccommodationData GetAccommodationDataFromRecord(IDataReader reader)
		{
			bool flag = reader == null || reader["dataid"] == DBNull.Value;
			AccommodationData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicData dataFromRecords = this.dynamicDataDao.GetDataFromRecords(reader);
				ExtendedAccommodationInfo extendedAccommodationInfoFromRecord = AccommodationsDAO.GetExtendedAccommodationInfoFromRecord(reader, this.OpContext);
				AccommodationData accommodationData = new AccommodationData
				{
					Data = dataFromRecords,
					Detail = extendedAccommodationInfoFromRecord
				};
				result = accommodationData;
			}
			return result;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00037B4C File Offset: 0x00035D4C
		internal static ExtendedAccommodationInfo GetExtendedAccommodationInfoFromRecord(IDataReader record, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			eAccommodationType eAccommodationType = eAccommodationType.Unknown;
			int num = (!record.ContainsColumn("showOnLetter1") || record["showOnLetter1"] is DBNull) ? 0 : ((int)record["showOnLetter1"]);
			int num2 = (!record.ContainsColumn("showonreport") || record["showonreport"] is DBNull) ? 0 : ((int)record["showonreport"]);
			eAccommodationGroup eAccommodationGroup = (eAccommodationGroup)num;
			bool flag = num2 > 0;
			if (flag)
			{
				eAccommodationGroup |= eAccommodationGroup.Report;
			}
			bool flag2 = (num & 1) == 1;
			if (flag2)
			{
				eAccommodationGroup |= eAccommodationGroup.Classroom;
			}
			bool flag3 = (num & 2) == 2;
			if (flag3)
			{
				eAccommodationGroup |= eAccommodationGroup.TestExam;
			}
			bool flag4 = (num & 4) == 4;
			if (flag4)
			{
				eAccommodationGroup |= eAccommodationGroup.Other;
			}
			bool flag5 = record.ContainsColumn("extratime") && record["extratime"] != DBNull.Value && Convert.ToBoolean(record["extratime"]);
			if (flag5)
			{
				eAccommodationType |= eAccommodationType.ExtraTime;
			}
			bool flag6 = record.ContainsColumn("isalone") && record["isalone"] != DBNull.Value && Convert.ToBoolean(record["isalone"]);
			if (flag6)
			{
				eAccommodationType |= eAccommodationType.AloneRoom;
			}
			bool flag7 = record.ContainsColumn("needscomputer") && record["needscomputer"] != DBNull.Value && Convert.ToBoolean(record["needscomputer"]);
			if (flag7)
			{
				eAccommodationType |= eAccommodationType.NeedsComputer;
			}
			bool flag8 = record.ContainsColumn("needsreaderscribe") && record["needsreaderscribe"] != DBNull.Value && Convert.ToBoolean(record["needsreaderscribe"]);
			if (flag8)
			{
				eAccommodationType |= eAccommodationType.NeedsReaderScribe;
			}
			bool flag9 = record.ContainsColumn("availableinallrooms") && record["availableinallrooms"] != DBNull.Value && Convert.ToBoolean(record["availableinallrooms"]);
			if (flag9)
			{
				eAccommodationType |= eAccommodationType.AvailableInAllRooms;
			}
			bool flag10 = record.ContainsColumn("isgroup") && record["isgroup"] != DBNull.Value && Convert.ToBoolean(record["isgroup"]);
			if (flag10)
			{
				eAccommodationType |= eAccommodationType.GroupRoom;
			}
			bool flag11 = record.ContainsColumn("tapedexams") && record["tapedexams"] != DBNull.Value && Convert.ToBoolean(record["tapedexams"]);
			if (flag11)
			{
				eAccommodationType |= eAccommodationType.TapedExams;
			}
			bool flag12 = record.ContainsColumn("other") && record["other"] != DBNull.Value && Convert.ToBoolean(record["other"]);
			if (flag12)
			{
				eAccommodationType |= eAccommodationType.Other;
			}
			bool flag13 = record.ContainsColumn("enlarged") && record["enlarged"] != DBNull.Value && Convert.ToBoolean(record["enlarged"]);
			if (flag13)
			{
				eAccommodationType |= eAccommodationType.EnlargedText;
			}
			bool flag14 = record.ContainsColumn("recommendedbutdeclineddetail");
			ExtendedAccommodationInfo result;
			if (flag14)
			{
				result = new ExtendedAccommodationInfo
				{
					Approved = (record["approved"] != DBNull.Value && Convert.ToBoolean(record["approved"])),
					ExpiryDate = ((record["expirydate"] == DBNull.Value) ? null : new DateTime?((DateTime)record["expirydate"])),
					Note = ((record["note"] == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])record["note"])),
					Offline = (record["offline"] != DBNull.Value && Convert.ToBoolean(record["offline"])),
					Rationale = ((record["rationale"] == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])record["rationale"])),
					RecommendedButDeclined = (record["recommendedbutdeclined"] != DBNull.Value && Convert.ToBoolean(record["recommendedbutdeclined"])),
					RecommendedButDeclinedDetail = ((record["recommendedbutdeclineddetail"] == DBNull.Value) ? "" : databaseLayer.Encryption.Decrypt((byte[])record["recommendedbutdeclineddetail"])),
					ShowOnLetter = (record["showonletter2"] != DBNull.Value && Convert.ToBoolean(record["showonletter2"])),
					SessionDateEntered = ((record["sessiondateentered"] == DBNull.Value) ? null : new DateTime?((DateTime)record["sessiondateentered"])),
					LongDescription = record["longdescription"].ToString(),
					ShortCode = record["shortcode"].ToString(),
					Group = eAccommodationGroup,
					AccommodationType = eAccommodationType
				};
			}
			else
			{
				result = new ExtendedAccommodationInfo
				{
					LongDescription = (record.ContainsColumn("longdescription") ? record["longdescription"].ToString() : ""),
					ShortCode = (record.ContainsColumn("shortcode") ? record["shortcode"].ToString() : ""),
					Group = eAccommodationGroup,
					AccommodationType = eAccommodationType
				};
			}
			return result;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000380E0 File Offset: 0x000362E0
		public IList<AccommodationData> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId, out bool IsUsingTemplateAccommodations)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @courseid int\r\n\r\nIF @lucid <= 0 OR dbo.accommodationscourseortemplate(@pid,@lucid) = 0\r\n\tSET @courseid=0\r\nELSE\r\n\tSET @courseid=@lucid\r\n\r\nSELECT    p.firstname,p.lastname,p.student_no,acc.accommodationid,acc.longdescription,acc.shortcode,acc.showonletter AS showonletter1,acc.showonemail,\r\n          acc.extratime,acc.isalone,acc.needscomputer,acc.needsreaderscribe,acc.availableinallrooms,acc.groupid,acc.isgroup,acc.tapedexams,\r\n          acc.other,acc.enlarged,acc.showonreport,\r\n          ad.controlid,ad.personid,ad.courseid,ad.showonletter AS showonletter2,ad.dataid,\r\n          ad.controlcaption,ad.valtext,ad.valint,ad.valbytes,ad.valdate,ad.valimage,ad.setting1,ad.setting2,ad.setting3,ad.setting4,\r\n          ad.defaultvalue,ad.controlcode,ad.valbytesisencrypted,ad.offline,ad.expirydate,ad.altlongdescription,ad.note,ad.approved,\r\n          ad.recommendedbutdeclined,ad.rationale,ad.sessiondateentered,ad.recommendedbutdeclineddetail,dc.setting4string\r\nFROM    accommodationdata ad LEFT JOIN accommodations acc ON acc.controlid=ad.controlid\r\n        LEFT JOIN DynamicScreenControls dsc ON dsc.controlID=ad.ControlID AND dsc.screenNum=4\r\n        LEFT JOIN people p ON p.personid=ad.personid\r\n        LEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\nWHERE   ad.personid=@pid \r\n\t\tAND ad.courseid=@courseid\r\n        AND NOT dsc.controlid IS NULL\r\n        AND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\nORDER BY dsc.ordernum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IList<AccommodationData> list = new List<AccommodationData>();
					int num = 0;
					while (dataReader.Read())
					{
						AccommodationData accommodationDataFromRecord = this.GetAccommodationDataFromRecord(dataReader);
						bool flag2 = accommodationDataFromRecord != null;
						if (flag2)
						{
							list.Add(accommodationDataFromRecord);
						}
						bool flag3 = num < 1 && dataReader["courseid"] != DBNull.Value;
						if (flag3)
						{
							num = (int)dataReader["courseid"];
						}
					}
					this.dynamicDataDao.MergeDynamicDataIntoUniqueControlIds<AccommodationData>(list);
					IsUsingTemplateAccommodations = (num == 0);
					return list;
				}
			}
			IsUsingTemplateAccommodations = true;
			return null;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000381F8 File Offset: 0x000363F8
		private IList<CourseRegistrationWithAccommodations> GetCourseRegistrationsWithAccommodationsFromReader(IDataReader reader)
		{
			IList<CourseRegistrationWithAccommodations> list = new List<CourseRegistrationWithAccommodations>();
			CourseRegistrationWithAccommodations courseRegistrationWithAccommodations = null;
			while (reader.Read())
			{
				int num = (int)reader["lucourseid"];
				bool flag = courseRegistrationWithAccommodations == null || courseRegistrationWithAccommodations.CourseReg.Course.LuCourseId != num;
				if (flag)
				{
					LookupCourse course = new LookupCourse
					{
						LuCourseId = num
					};
					LookupCourseDAO.GetMainCourseFromReader(course, "", reader);
					courseRegistrationWithAccommodations = new CourseRegistrationWithAccommodations
					{
						CourseReg = CourseRegistrationDAO.GetCourseRegistrationFromRecord0<CourseRegistration>(course, reader, this.OpContext),
						CourseOrTemplateAccommodations = new List<AccommodationData>()
					};
					list.Add(courseRegistrationWithAccommodations);
				}
				int dataId = (reader["dataid"] == DBNull.Value) ? 0 : ((int)reader["dataid"]);
				bool flag2 = dataId > 0;
				if (flag2)
				{
					bool flag3 = courseRegistrationWithAccommodations.IsUsingTemplateAccommodations == null && reader["courseid"] != DBNull.Value;
					if (flag3)
					{
						courseRegistrationWithAccommodations.IsUsingTemplateAccommodations = new bool?((int)reader["courseid"] == 0);
					}
					bool flag4 = courseRegistrationWithAccommodations.CourseOrTemplateAccommodations.FirstOrDefault((AccommodationData f) => ((f.Data == null) ? 0 : f.Data.DataId) == dataId) == null;
					if (flag4)
					{
						DynamicData dataFromRecords = this.dynamicDataDao.GetDataFromRecords(reader);
						bool flag5 = dataFromRecords != null;
						if (flag5)
						{
							courseRegistrationWithAccommodations.CourseOrTemplateAccommodations.Add(new AccommodationData
							{
								Data = dataFromRecords
							});
						}
					}
				}
			}
			foreach (CourseRegistrationWithAccommodations courseRegistrationWithAccommodations2 in list)
			{
				this.dynamicDataDao.MergeDynamicDataIntoUniqueControlIds<AccommodationData>(courseRegistrationWithAccommodations2.CourseOrTemplateAccommodations);
			}
			return list;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000383E8 File Offset: 0x000365E8
		public IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodations(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@loadaccommodations", DbType.Boolean, LoadAccommodations)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT c.coursesid,c.personid,c.lucourseid,dbo.AccommodationsCourseOrTemplate(c.personid,c.lucourseid) AS q \r\nINTO #t1 FROM courses c LEFT JOIN lucourses luc ON luc.LUCourseID=c.luCourseID\r\nWHERE   c.personid=@pid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n\t\tAND NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n\r\nSELECT    #t1.coursesid,#t1.personid,#t1.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        p.firstname,p.lastname,p.student_no,\r\n        ad.*,ad.showonletter AS showonletter2,a.showonletter AS showonletter1\r\nFROM    #t1 LEFT JOIN courses c ON c.lucourseid=#t1.lucourseid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN accommodationdata ad ON @loadaccommodations=1 AND ad.personid=@pid AND ad.courseid=#t1.q AND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\t\tLEFT JOIN DynamicScreenControls dsc ON dsc.controlID=ad.ControlID AND dsc.screenNum=4\r\n        LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE   c.personid=@pid AND NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n        AND (@loadaccommodations=0 OR NOT dsc.controlid IS NULL)\r\nORDER BY c.lucourseid,dsc.ordernum\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetCourseRegistrationsWithAccommodationsFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00003998 File Offset: 0x00001B98
		public List<DynamicDataChange> LoadAccommodationChanges(int PersonId, int LuCourseId, DateTime SinceDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000384B0 File Offset: 0x000366B0
		public void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@courseid", DbType.Int32, CourseId),
				this.DatabaseManager.GetParameter("@requiresapproval", DbType.Boolean, RequiresApproval),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO AccommodationsApproval (personid,controlid,lucourseid,newint,newdatetime,newbytes,newimage,status,whoentered,requiresapproval) \r\n    SELECT personid,controlid,courseid,valint,valdate,valbytes,valimage,4,@whoami,@requiresapproval\r\n    FROM accommodationdata WHERE personid=@pid AND courseid=@courseid;\r\nDELETE FROM maininfoaccommodationps WHERE personid=@pid AND courseid=@courseid;\r\nDELETE FROM otherinfoaccommodationps WHERE personid=@pid AND courseid=@courseid;\r\nDELETE FROM datetimeinfoaccommodationps WHERE personid=@pid AND courseid=@courseid;\r\nDELETE FROM imageinfoaccommodationps WHERE personid=@pid AND courseid=@courseid;", parameters);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0003854C File Offset: 0x0003674C
		private IList<CourseRegistrationWithAccommodations> GetStudentsRegisteredCoursesWithAccommodationsAndRequestsFromReader(IDataReader reader)
		{
			IList<CourseRegistrationWithAccommodations> list = new List<CourseRegistrationWithAccommodations>();
			CourseRegistrationWithAccommodations courseRegistrationWithAccommodations = null;
			int num = 0;
			while (reader.Read())
			{
				int num2 = (int)reader["lucourseid"];
				bool flag = courseRegistrationWithAccommodations == null || courseRegistrationWithAccommodations.CourseReg.Course.LuCourseId != num2;
				if (flag)
				{
					LookupCourse course = new LookupCourse
					{
						LuCourseId = num2
					};
					LookupCourseDAO.GetMainCourseFromReader(course, "", reader);
					courseRegistrationWithAccommodations = new CourseRegistrationWithAccommodations
					{
						CourseReg = CourseRegistrationDAO.GetCourseRegistrationFromRecord0<CourseRegistration>(course, reader, this.OpContext),
						CourseOrTemplateAccommodations = new List<AccommodationData>()
					};
					list.Add(courseRegistrationWithAccommodations);
				}
				int dataId = (reader["dataid"] == DBNull.Value) ? 0 : ((int)reader["dataid"]);
				bool flag2 = dataId > 0 && num != dataId;
				if (flag2)
				{
					bool flag3 = courseRegistrationWithAccommodations.IsUsingTemplateAccommodations == null && reader["courseid"] != DBNull.Value;
					if (flag3)
					{
						courseRegistrationWithAccommodations.IsUsingTemplateAccommodations = new bool?((int)reader["courseid"] == 0);
					}
					bool flag4 = courseRegistrationWithAccommodations.CourseOrTemplateAccommodations.FirstOrDefault((AccommodationData f) => ((f.Data == null) ? 0 : f.Data.DataId) == dataId) == null;
					if (flag4)
					{
						DynamicData dataFromRecords = this.dynamicDataDao.GetDataFromRecords(reader);
						bool flag5 = dataFromRecords != null;
						if (flag5)
						{
							courseRegistrationWithAccommodations.CourseOrTemplateAccommodations.Add(new AccommodationData
							{
								Data = dataFromRecords
							});
							num = dataId;
						}
					}
				}
			}
			foreach (CourseRegistrationWithAccommodations courseRegistrationWithAccommodations2 in list)
			{
				this.dynamicDataDao.MergeDynamicDataIntoUniqueControlIds<AccommodationData>(courseRegistrationWithAccommodations2.CourseOrTemplateAccommodations);
			}
			return list;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00038758 File Offset: 0x00036958
		public IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate),
				this.DatabaseManager.GetParameter("@loadaccommodations", DbType.Boolean, LoadAccommodations)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\tc.lucourseid,c.personid,dbo.accommodationscourseortemplate(@pid,c.luCourseID) AS courseortemplate\r\nINTO\t#t1\r\nFROM\tCourses c \r\nWHERE\t@loadaccommodations=1\r\n\t\tAND c.personID=@pid \r\n\t\tAND c.luCourseID IN (SELECT luc.luCourseID FROM LUCourses luc WHERE NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate) )\r\n\r\nSELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        p.firstname,p.lastname,p.student_no,\r\n        ad.*,ad.showonletter AS showonletter2,a.showonletter AS showonletter1,\r\n        sar.StudentCourseAccommodationRequestId,sar.status AS rstatus,sar.daterequested AS rdaterequested,sar.dateapproved AS rdateapproved,\r\n        sar.dateentered AS rdateentered,sar.note1 AS rnote1,sar.note2 AS rnote2,\r\n        sar.whoapprovedpersonid AS rapprovedpersonid,psara.firstname AS rapprovedfirstname,psara.lastname AS rapprovedlastname,psara.student_no AS rapprovedstudent_no,\r\n        sar.whoenteredpersonid AS renteredpersonid,psare.firstname AS renteredfirstname,psare.lastname AS renteredlastname,psare.student_no AS renteredstudent_no\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN accommodationdata ad ON @loadaccommodations=1 \r\n\t\t\tAND ad.personid=@pid \r\n\t\t\tAND ad.courseid=COALESCE((SELECT TOP 1 courseortemplate FROM #t1 WHERE #t1.lucourseid=c.luCourseID),0) \r\n\t\t\tAND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\t\tLEFT JOIN DynamicScreenControls dsc ON dsc.controlID=ad.ControlID AND dsc.screenNum=4 AND (@loadaccommodations=0 OR NOT dsc.controlid IS NULL)\r\n        LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\n        LEFT JOIN StudentCourseAccommodationRequest sar ON sar.personid=c.personid AND sar.lucourseid=c.lucourseid AND sar.isactive=1\r\n        LEFT JOIN people psare ON psare.personid=sar.whoenteredpersonid\r\n        LEFT JOIN people psara ON psara.personid=sar.whoapprovedpersonid\r\nWHERE   c.personid=@pid AND NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\nORDER BY c.lucourseid,dsc.ordernum\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetStudentsRegisteredCoursesWithAccommodationsAndRequestsFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00038820 File Offset: 0x00036A20
		public void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds)
		{
			string value = string.Join(",", LuCourseIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucids", DbType.String, value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE courses SET dateletterissued=getdate() WHERE personid=@pid AND lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))", parameters);
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucids", DbType.String, value),
				this.DatabaseManager.GetParameter("@whopid", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@method", DbType.Int32, 0)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO accommodationloaissued (personid,lucourseid,datetimeissued,whoissued,issuedmethod,loa) SELECT @pid,orderid AS lucid,getdate(),@whopid,@method,NULL FROM splitorderids(@lucids,',')", parameters);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0003893C File Offset: 0x00036B3C
		public void MergeAccommodations(int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId, IList<int> ControlIdsToIgnore)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[5];
			array[0] = databaseLayer.GetParameter("@sourcepid", DbType.Int32, SourcePersonId);
			array[1] = databaseLayer.GetParameter("@sourcelucid", DbType.Int32, SourceLuCourseId);
			array[2] = databaseLayer.GetParameter("@destpid", DbType.Int32, DestPersonId);
			array[3] = databaseLayer.GetParameter("@destlucid", DbType.Int32, DestLuCourseId);
			int num = 4;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@cids";
			DbType pType = DbType.String;
			object value;
			if (ControlIdsToIgnore != null)
			{
				value = string.Join(",", ControlIdsToIgnore.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("SELECT orderid AS controlid INTO #t1 FROM splitorderids(@cids,',');\r\n\r\nINSERT INTO maininfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT m.screennum,@destpid,m.controlid,m.controlvalue,@destlucid,m.flavour,m.[offline],m.expirydate,m.altlongdescription,m.note,m.showonletter,m.sessiondateentered,m.approved,m.recommendedbutdeclined,m.rationale,m.recommendedbutdeclineddetail \r\n\tFROM maininfoaccommodationps m\r\n    WHERE m.personid=@sourcepid AND m.courseid=@sourcelucid AND NOT m.controlid IN (SELECT controlid FROM #t1) AND m.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n            AND NOT EXISTS(SELECT m2.dataid FROM maininfoaccommodationps m2 WHERE m2.controlid=m.controlid AND m2.personid=@destpid AND m2.courseid=@destlucid)\r\n\r\n\t\t\t\r\nINSERT INTO otherinfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT o.screennum,@destpid,o.controlid,o.controlvalue,@destlucid,o.flavour,o.[offline],o.expirydate,o.altlongdescription,o.note,o.showonletter,o.sessiondateentered,o.approved,o.recommendedbutdeclined,o.rationale,o.recommendedbutdeclineddetail \r\n\tFROM otherinfoaccommodationps o\r\n    WHERE o.personid=@sourcepid AND o.courseid=@sourcelucid AND NOT o.controlid IN (SELECT controlid FROM #t1) AND o.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n            AND NOT EXISTS(SELECT o2.dataid FROM otherinfoaccommodationps o2 WHERE o2.controlid=o.controlid AND o2.personid=@destpid AND o2.courseid=@destlucid)\r\n\r\nINSERT INTO datetimeinfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT d.screennum,@destpid,d.controlid,d.controlvalue,@destlucid,d.flavour,d.[offline],d.expirydate,d.altlongdescription,d.note,d.showonletter,d.sessiondateentered,d.approved,d.recommendedbutdeclined,d.rationale,d.recommendedbutdeclineddetail \r\n\tFROM datetimeinfoaccommodationps d\r\n    WHERE d.personid=@sourcepid AND d.courseid=@sourcelucid AND NOT d.controlid IN (SELECT controlid FROM #t1) AND d.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n            AND NOT EXISTS(SELECT d2.dataid FROM datetimeinfoaccommodationps d2 WHERE d2.controlid=d.controlid AND d2.personid=@destpid AND d2.courseid=@destlucid)\r\n\r\nINSERT INTO imageinfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT i.screennum,@destpid,i.controlid,i.controlvalue,@destlucid,i.flavour,i.[offline],i.expirydate,i.altlongdescription,i.note,i.showonletter,i.sessiondateentered,i.approved,i.recommendedbutdeclined,i.rationale,i.recommendedbutdeclineddetail \r\n\tFROM imageinfoaccommodationps i\r\n    WHERE personid=@sourcepid AND courseid=@sourcelucid AND NOT controlid IN (SELECT controlid FROM #t1) AND controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n            AND NOT EXISTS(SELECT i2.dataid FROM imageinfoaccommodationps i2 WHERE i2.controlid=i.controlid AND i2.personid=@destpid AND i2.courseid=@destlucid)\r\n\t\r\nDROP TABLE #t1", parameters);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00038A28 File Offset: 0x00036C28
		public void ReplaceAccommodations(int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId, IList<int> ControlIdsToIgnore)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[5];
			array[0] = databaseLayer.GetParameter("@sourcepid", DbType.Int32, SourcePersonId);
			array[1] = databaseLayer.GetParameter("@sourcelucid", DbType.Int32, SourceLuCourseId);
			array[2] = databaseLayer.GetParameter("@destpid", DbType.Int32, DestPersonId);
			array[3] = databaseLayer.GetParameter("@destlucid", DbType.Int32, DestLuCourseId);
			int num = 4;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@cids";
			DbType pType = DbType.String;
			object value;
			if (ControlIdsToIgnore != null)
			{
				value = string.Join(",", ControlIdsToIgnore.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			databaseLayer.ExecuteNonQuery("SELECT orderid AS controlid INTO #t1 FROM splitorderids(@cids,',');\r\n\r\nDELETE FROM maininfoaccommodationps WHERE personid=@destpid AND courseid=@destlucid;\r\nINSERT INTO maininfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT screennum,@destpid,controlid,controlvalue,@destlucid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail FROM maininfoaccommodationps \r\n        WHERE personid=@sourcepid AND courseid=@sourcelucid AND NOT controlid IN (SELECT controlid FROM #t1) AND controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\r\nDELETE FROM otherinfoaccommodationps WHERE personid=@destpid AND courseid=@destlucid;\r\nINSERT INTO otherinfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT screennum,@destpid,controlid,controlvalue,@destlucid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail FROM otherinfoaccommodationps \r\n        WHERE personid=@sourcepid AND courseid=@sourcelucid AND NOT controlid IN (SELECT controlid FROM #t1) AND controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\r\nDELETE FROM datetimeinfoaccommodationps WHERE personid=@destpid AND courseid=@destlucid;\r\nINSERT INTO datetimeinfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT screennum,@destpid,controlid,controlvalue,@destlucid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail FROM datetimeinfoaccommodationps \r\n        WHERE personid=@sourcepid AND courseid=@sourcelucid AND NOT controlid IN (SELECT controlid FROM #t1) AND controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\r\nDELETE FROM imageinfoaccommodationps WHERE personid=@destpid AND courseid=@destlucid;\r\nINSERT INTO imageinfoaccommodationps (screennum,personid,controlid,controlvalue,courseid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail)\r\n    SELECT screennum,@destpid,controlid,controlvalue,@destlucid,flavour,offline,expirydate,altlongdescription,note,showonletter,sessiondateentered,approved,recommendedbutdeclined,rationale,recommendedbutdeclineddetail FROM imageinfoaccommodationps \r\n        WHERE personid=@sourcepid AND courseid=@sourcelucid AND NOT controlid IN (SELECT controlid FROM #t1) AND controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\r\nDROP TABLE #t1", parameters);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00038B14 File Offset: 0x00036D14
		public IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsByCourse(int PersonId, int LuCourseId, bool LoadAccommodations)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@loadaccommodations", DbType.Boolean, LoadAccommodations)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @courseortemplate int = COALESCE((SELECT dbo.accommodationscourseortemplate(@pid,@lucid)),0)\r\n\r\nSELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        p.firstname,p.lastname,p.student_no,\r\n        ad.*,ad.showonletter AS showonletter2,a.showonletter AS showonletter1\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN accommodationdata ad ON @loadaccommodations=1 AND ad.personid=@pid AND ad.courseid=@courseortemplate AND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\t\tLEFT JOIN DynamicScreenControls dsc ON dsc.controlID=ad.ControlID AND dsc.screenNum=4\r\n        LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE   c.personid=@pid AND c.lucourseid=@lucid\r\n        AND (@loadaccommodations=0 OR NOT dsc.controlid IS NULL)\r\nORDER BY c.lucourseid,dsc.ordernum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetCourseRegistrationsWithAccommodationsFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00038BC4 File Offset: 0x00036DC4
		public IList<CourseRegistrationWithAccommodations> LoadStudentsAccommodationsAndRequestsForOfflineCourse(int PersonId, bool LoadAccommodations)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, 1),
				this.DatabaseManager.GetParameter("@loadaccommodations", DbType.Boolean, LoadAccommodations)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @courseid int\r\nSET @courseid=(SELECT dbo.accommodationscourseortemplate(@pid,@lucid))\r\nSELECT    0 AS coursesid,ad.personid,ad.courseid AS lucourseid,CAST(NULL AS int) AS registrationstatus,getdate() AS dateadded,\r\n0 AS whoaddedpersonid,CAST(NULL AS varbinary(8000)) AS whoaddedfirstname,CAST(NULL AS varbinary(8000)) AS whoaddedlastname,CAST(NULL AS varbinary(8000)) AS whoaddedstudent_no,\r\nCAST(NULL AS DATETIME) AS dateletterissued,CAST(NULL AS DATETIME) AS dateletterreturned,CAST(0 AS bit) AS needsnotes,0 AS extracode,'' AS coursenote,CAST(NULL AS int) AS notetakerrequired,\r\nCAST(NULL AS DATETIME) AS datestudentlastviewed,CAST(NULL AS DATETIME) AS dateinstructorlastviewed,'' AS wholastviewed,0 AS instructorconfirmed,CAST(0 AS BIT) AS exemptfromdatasync,\r\n@lucid AS lucourseid,getdate() AS startdate,getdate() AS enddate,'' AS duration,'' AS term,0 AS subjectid,'' AS externalid,CAST(0 AS BIT) AS exemptfromdatasync\r\n        ,'' subjectcode,'Offline accommodations' AS subjectdescription\r\n        ,'' AS course,'' AS timeofday,'' AS [section]\r\n        ,'' AS campus,'' AS department,'' AS location\r\n        ,CAST(NULL AS int) AS pinstructorid,CAST(NULL AS varchar(8000)) AS pinstructorname,'' AS pinstructoremail,'' AS pinstructorphone,'' AS pinstructorusername,\r\n\t\tCAST(0 AS BIT) AS pexemptfromdatasync,'' AS pinstructoremployeeid,'' AS pinstructorexternalid\r\n        ,CAST(0 AS BIT) AS pExemptAssignmentFromDataSync\r\n        ,CAST(NULL AS int) AS p3instructorid,'' AS p3instructorname,'' AS p3instructoremail,'' AS p3instructorphone,'' AS p3instructorusername,CAST(0 AS BIT) AS p3exemptfromdatasync,\r\n\t\t'' AS p3instructoremployeeid,'' AS p3instructorexternalid\r\n        ,CAST(0 AS BIT) AS p3ExemptAssignmentFromDataSync\r\n        ,CAST(NULL AS int) AS timetableid\r\n        ,0 AS sunstartminutes,0 AS sunendminutes,0 AS monstartminutes,0 AS monendminutes,0 AS tuestartminutes,0 AS tueendminutes\r\n        ,0 AS wedstartminutes,0 AS wedendminutes,0 AS thustartminutes,0 AS thuendminutes,0 AS fristartminutes,0 AS friendminutes\r\n        ,0 AS satstartminutes,0 AS satendminutes,0 AS sunroom,0 AS monroom,0 AS tueroom,0 AS wedroom,0 AS thuroom,0 AS friroom,0 AS satroom,\r\n        p.firstname,p.lastname,p.student_no,\r\n        ad.*,ad.showonletter AS showonletter2,a.showonletter AS showonletter1,\r\n        sar.StudentCourseAccommodationRequestId,sar.status AS rstatus,sar.daterequested AS rdaterequested,sar.dateapproved AS rdateapproved,\r\n        sar.dateentered AS rdateentered,sar.note1 AS rnote1,sar.note2 AS rnote2,\r\n        sar.whoapprovedpersonid AS rapprovedpersonid,psara.firstname AS rapprovedfirstname,psara.lastname AS rapprovedlastname,psara.student_no AS rapprovedstudent_no,\r\n        sar.whoenteredpersonid AS renteredpersonid,psare.firstname AS renteredfirstname,psare.lastname AS renteredlastname,psare.student_no AS renteredstudent_no\r\nFROM    people p \r\n        LEFT JOIN accommodationdata ad ON @loadaccommodations=1 AND ad.personid=@pid AND ad.courseid=@courseid AND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\t\tLEFT JOIN DynamicScreenControls dsc ON dsc.controlID=ad.ControlID AND dsc.screenNum=4 AND (@loadaccommodations=0 OR NOT dsc.controlid IS NULL)\r\n        LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\n        LEFT JOIN StudentCourseAccommodationRequest sar ON sar.personid=p.personid AND sar.lucourseid=@lucid AND sar.isactive=1\r\n        LEFT JOIN people psare ON psare.personid=sar.whoenteredpersonid\r\n        LEFT JOIN people psara ON psara.personid=sar.whoapprovedpersonid\r\nWHERE   p.personid=@pid AND ad.courseid=@lucid\r\nORDER BY ad.courseid,dsc.ordernum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetStudentsRegisteredCoursesWithAccommodationsAndRequestsFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00038C74 File Offset: 0x00036E74
		public IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsByCourse(int PersonId, int LuCourseId, bool LoadAccommodations)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@loadaccommodations", DbType.Boolean, LoadAccommodations)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @courseortemplate int = (SELECT dbo.AccommodationsCourseOrTemplate(@pid,@lucid))\r\n\r\nSELECT    c.coursesid,c.personid,c.lucourseid,c.registrationstatus,c.dateadded,\r\nc.whoadded AS whoaddedpersonid,pc.firstname AS whoaddedfirstname,pc.lastname AS whoaddedlastname,pc.student_no AS whoaddedstudent_no,\r\nc.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,c.notetakerrequired,\r\nc.datestudentlastviewed,c.dateinstructorlastviewed,c.wholastviewed,c.instructorconfirmed,c.exemptfromdatasync,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.externalid,luc.exemptfromdatasync\r\n        ,lucd.lookupstring AS subjectcode,lucd.altlookupstring AS subjectdescription\r\n        ,luc.course,luc.timeofday,luc.[section]\r\n        ,luc.campus,luc.department,luc.location\r\n        ,luc.instructorid AS pinstructorid,lucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,lucd2.phone AS pinstructorphone,lucd2.username AS pinstructorusername,lucd2.exemptfromdatasync AS pexemptfromdatasync,lucd2.id AS pinstructoremployeeid,lucd2.externalid AS pinstructorexternalid\r\n        ,luc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync\r\n        ,lci.instructorid AS p3instructorid,lucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,lucd3.phone AS p3instructorphone,lucd3.username AS p3instructorusername,lucd3.exemptfromdatasync AS p3exemptfromdatasync,lucd3.id AS p3instructoremployeeid,lucd3.externalid AS p3instructorexternalid\r\n        ,lci.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync\r\n        ,tt.timetableid\r\n        ,tt.sunstartminutes,tt.sunendminutes,tt.monstartminutes,tt.monendminutes,tt.tuestartminutes,tt.tueendminutes\r\n        ,tt.wedstartminutes,tt.wedendminutes,tt.thustartminutes,tt.thuendminutes,tt.fristartminutes,tt.friendminutes\r\n        ,tt.satstartminutes,tt.satendminutes,tt.sunroom,tt.monroom,tt.tueroom,tt.wedroom,tt.thuroom,tt.friroom,tt.satroom,\r\n        p.firstname,p.lastname,p.student_no,\r\n        ad.*,ad.showonletter AS showonletter2,a.showonletter AS showonletter1,\r\n        sar.StudentCourseAccommodationRequestId,sar.status AS rstatus,sar.daterequested AS rdaterequested,sar.dateapproved AS rdateapproved,\r\n        sar.dateentered AS rdateentered,sar.note1 AS rnote1,sar.note2 AS rnote2,\r\n        sar.whoapprovedpersonid AS rapprovedpersonid,psara.firstname AS rapprovedfirstname,psara.lastname AS rapprovedlastname,psara.student_no AS rapprovedstudent_no,\r\n        sar.whoenteredpersonid AS renteredpersonid,psare.firstname AS renteredfirstname,psare.lastname AS renteredlastname,psare.student_no AS renteredstudent_no\r\nFROM    courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n        LEFT JOIN people p ON p.personid=c.personid\r\n        LEFT JOIN people pc ON pc.personid=c.whoadded\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor lci ON lci.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=lci.instructorid\r\n        LEFT JOIN timetable tt ON tt.timetabletype='C' AND tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN accommodationdata ad ON @loadaccommodations=1 \r\n\t\t\tAND ad.personid=@pid \r\n\t\t\tAND ad.courseid=COALESCE(@courseortemplate,0) --dbo.accommodationscourseortemplate(@pid,c.luCourseID) \r\n\t\t\tAND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\t\tLEFT JOIN DynamicScreenControls dsc ON dsc.controlID=ad.ControlID AND dsc.screenNum=4 AND (@loadaccommodations=0 OR NOT dsc.controlid IS NULL)\r\n        LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\n        LEFT JOIN StudentCourseAccommodationRequest sar ON sar.personid=c.personid AND sar.lucourseid=c.lucourseid AND sar.isactive=1\r\n        LEFT JOIN people psare ON psare.personid=sar.whoenteredpersonid\r\n        LEFT JOIN people psara ON psara.personid=sar.whoapprovedpersonid\r\nWHERE   c.personid=@pid AND c.lucourseid=@lucid\r\nORDER BY c.lucourseid,dsc.ordernum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetStudentsRegisteredCoursesWithAccommodationsAndRequestsFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00038D24 File Offset: 0x00036F24
		public IList<DynamicDataSetWithStudentName> LoadActiveStudentsWithTemplateAccommodations(DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date)
			};
			IList<DynamicDataSetWithStudentName> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("CREATE TABLE #tpids (personid INT);\r\n\r\nINSERT INTO #tpids\r\n\tEXEC ActiveStudentPids @startdate,@enddate;\r\n\r\nSELECT \tt.personid,p.lastname,p.firstname,p.middlename,p.student_no,\r\n\t\tad.*,dc.*\r\nFROM \t#tpids t LEFT JOIN people p ON p.personid=t.personid\r\n\t\tLEFT JOIN accommodationdata ad ON ad.courseid=0 AND ad.personid=t.personid AND ad.controlid IN (SELECT controlid FROM dynamicscreencontrols)\r\n\t\tLEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\n\t\tLEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=4 AND dsc.controlid=dc.controlid\r\nORDER BY t.personid,dsc.ordernum,dc.controlcaption;\r\n\r\nDROP TABLE #tpids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					DynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
					result = dynamicDataDAO.GetDataSetListWithStudentNamesFromMapper(dataReader);
				}
			}
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00038DDC File Offset: 0x00036FDC
		public IList<int> LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(int PersonId, int[] cids, int[] lucids)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			int num = 1;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@lucids";
			DbType pType = DbType.String;
			string separator = ",";
			string[] array2;
			if (lucids == null)
			{
				array2 = null;
			}
			else
			{
				array2 = (from g in lucids
				select g.ToString()).ToArray<string>();
			}
			array[num] = databaseManager.GetParameter(pName, pType, string.Join(separator, array2 ?? new string[0]));
			int num2 = 2;
			DatabaseLayer databaseManager2 = this.DatabaseManager;
			string pName2 = "@cids";
			DbType pType2 = DbType.String;
			string separator2 = ",";
			string[] array3;
			if (cids == null)
			{
				array3 = null;
			}
			else
			{
				array3 = (from g in cids
				select g.ToString()).ToArray<string>();
			}
			array[num2] = databaseManager2.GetParameter(pName2, pType2, string.Join(separator2, array3 ?? new string[0]));
			DbParameter[] parameters = array;
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS lucid,CAST(0 AS int) AS courseid INTO #t1 FROM SplitOrderIDs(@lucids,',')\r\nSELECT orderid AS cid INTO #tcids FROM SplitOrderIDs(@cids,',')\r\n\r\nUPDATE #t1 SET courseid=lucid WHERE EXISTS(SELECT dataid FROM MainInfoAccommodationPS WHERE personid=@pid AND courseid=lucid) OR EXISTS(SELECT dataid FROM OtherInfoAccommodationPS WHERE personid=@pid AND courseid=lucid) OR EXISTS(SELECT dataid FROM DateTimeInfoAccommodationPS WHERE personid=@pid AND courseid=lucid) OR EXISTS(SELECT dataid FROM imageinfoaccommodationps WHERE personid=@pid AND courseid=lucid)\r\n\r\nSELECT  DISTINCT #t1.lucid AS lucourseid\r\nFROM\t#t1 LEFT JOIN MainInfoAccommodationPS m ON m.personid=@pid AND m.courseid=#t1.courseid AND m.controlid IN (SELECT cid FROM #tcids)\r\nWHERE\tNOT m.controlvalue IS NULL AND NOT m.controlvalue=0\r\n\r\nDROP TABLE #t1\r\nDROP TABLE #tcids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = new List<int>();
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num3 = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
						bool flag2 = num3 > 0;
						if (flag2)
						{
							list.Add(num3);
						}
					}
					result = list.Distinct<int>().ToList<int>();
				}
			}
			return result;
		}

		// Token: 0x040002FE RID: 766
		private DynamicFormsDAO dfd;

		// Token: 0x040002FF RID: 767
		private DynamicDataDAO _dynamicDataDao;

		// Token: 0x04000300 RID: 768
		private PersonDAO pd;

		// Token: 0x04000301 RID: 769
		private LookupCourseDAO ld;
	}
}

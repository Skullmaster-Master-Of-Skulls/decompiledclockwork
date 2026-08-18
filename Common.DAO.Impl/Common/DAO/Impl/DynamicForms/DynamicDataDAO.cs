using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000DB RID: 219
	public class DynamicDataDAO : IDynamicDataDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000396E8 File Offset: 0x000378E8
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x000396F0 File Offset: 0x000378F0
		public OperationContext OpContext { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x000396FC File Offset: 0x000378FC
		private DynamicFieldDAO dynamicFieldDao
		{
			get
			{
				DynamicFieldDAO result;
				if ((result = this._dynamicFieldDao) == null)
				{
					result = (this._dynamicFieldDao = new DynamicFieldDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00039727 File Offset: 0x00037927
		public DynamicDataDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00039758 File Offset: 0x00037958
		private void MergeFileLists(int PersonIdNew, int PersonIdOld)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew),
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT dataid,personid,controlid,controlvalue FROM otherinfops WHERE (personid=@newpid OR personid=@oldpid) AND controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20) ORDER BY personid,controlid", parameters))
			{
				bool flag = dataReader == null;
				if (!flag)
				{
					List<DynamicData> list = new List<DynamicData>();
					List<DynamicData> itemsNew = new List<DynamicData>();
					List<int> list2 = new List<int>();
					while (dataReader.Read())
					{
						DynamicData dynamicData = new DynamicData
						{
							DataId = (int)dataReader["dataid"],
							Field = new DynamicField
							{
								ControlId = (int)dataReader["controlid"]
							},
							Value = Encoding.ASCII.GetString((byte[])dataReader["controlvalue"])
						};
						bool flag2 = !list2.Contains(dynamicData.Field.ControlId);
						if (flag2)
						{
							list2.Add(dynamicData.Field.ControlId);
						}
						int num = (int)dataReader["personid"];
						bool flag3 = num == PersonIdNew;
						if (flag3)
						{
							itemsNew.Add(dynamicData);
						}
						else
						{
							bool flag4 = num == PersonIdOld;
							if (flag4)
							{
								list.Add(dynamicData);
							}
						}
					}
					IEnumerable<DynamicData> enumerable = from g in list
					where itemsNew.FirstOrDefault((DynamicData h) => h.Field.ControlId == g.Field.ControlId) == null
					select g;
					IEnumerable<DynamicData> enumerable2 = from g in list
					where itemsNew.FirstOrDefault((DynamicData h) => h.Field.ControlId == g.Field.ControlId) != null
					select g;
					foreach (DynamicData dynamicData2 in enumerable)
					{
						int cid = dynamicData2.Field.ControlId;
						bool flag5 = true;
						IEnumerable<DynamicData> source = list;
						Func<DynamicData, bool> predicate;
						Func<DynamicData, bool> <>9__4;
						if ((predicate = <>9__4) == null)
						{
							predicate = (<>9__4 = ((DynamicData h) => h.Field.ControlId == cid));
						}
						foreach (DynamicData dynamicData3 in source.Where(predicate))
						{
							bool flag6 = flag5;
							if (flag6)
							{
								parameters = new DbParameter[]
								{
									this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew),
									this.DatabaseManager.GetParameter("@dataid", DbType.Int32, dynamicData3.DataId)
								};
								this.DatabaseManager.ExecuteNonQuery("UPDATE otherinfops SET personid=@newpid WHERE dataid=@dataid", parameters);
								flag5 = false;
							}
							else
							{
								this.DeleteItem(dynamicData3.DataId);
							}
						}
					}
					DynamicDataContext context = new DynamicDataContext
					{
						PrimaryId = PersonIdNew
					};
					IDynamicFieldDAO dynamicFieldDAO = new DynamicFieldDAO(this.OpContext);
					foreach (DynamicData dynamicData4 in enumerable2)
					{
						int cid = dynamicData4.Field.ControlId;
						List<DynamicData> list3 = (from h in list
						where h.Field.ControlId == cid
						select h).ToList<DynamicData>();
						List<DynamicData> list4 = (from g in itemsNew
						where g.Field.ControlId == cid
						select g).ToList<DynamicData>();
						bool flag7 = list3.Count > 0 && list4.Count > 0;
						if (flag7)
						{
							list4[0].Field = dynamicFieldDAO.LoadFieldsByControlIds(new List<int>
							{
								list4[0].Field.ControlId
							})[0];
							list4[0].Value = list4[0].Value.ToString() + '\t'.ToString() + list3[0].Value.ToString();
							this.SaveData(context, new List<DynamicData>
							{
								list4[0]
							}, eDynamicFormType.PerStudent);
							for (int i = 1; i < list3.Count; i++)
							{
								this.DeleteItem(list3[i].DataId);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00039C14 File Offset: 0x00037E14
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00039C54 File Offset: 0x00037E54
		public IList<DynamicData> GetDatasFromRecords(IDataReader reader)
		{
			IList<DynamicData> list = new List<DynamicData>();
			bool flag = reader == null;
			IList<DynamicData> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				while (reader.Read())
				{
					bool flag2 = reader["dataid"] == DBNull.Value;
					if (!flag2)
					{
						DynamicData dataFromRecords = this.GetDataFromRecords(reader);
						bool flag3 = dataFromRecords != null;
						if (flag3)
						{
							list.Add(dataFromRecords);
						}
					}
				}
				this.MergeDynamicDataIntoUniqueControlIds<DynamicData>(list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00039CC8 File Offset: 0x00037EC8
		public void MergeDynamicDataIntoUniqueControlIds(IList<DynamicDataSet> dataSets)
		{
			foreach (DynamicDataSet dynamicDataSet in dataSets)
			{
				this.MergeDynamicDataIntoUniqueControlIds<DynamicData>(dynamicDataSet.Data);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00039D1C File Offset: 0x00037F1C
		public void MergeDynamicDataWithStudentNameIntoUniqueControlIds(IList<DynamicDataSetWithStudentName> dataSets)
		{
			foreach (DynamicDataSetWithStudentName dynamicDataSetWithStudentName in dataSets)
			{
				this.MergeDynamicDataIntoUniqueControlIds<DynamicData>(dynamicDataSetWithStudentName.Data);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00039D70 File Offset: 0x00037F70
		public void MergeDynamicDataIntoUniqueControlIds<T>(IList<T> datas) where T : class, IDynamicDataHoldingObject
		{
			List<T> list = (from g in datas
			where true
			select g).ToList<T>();
			list.Sort((T g1, T g2) => g1.GetDynamicData().Field.ControlId.CompareTo(g2.GetDynamicData().Field.ControlId));
			int i = 0;
			List<T> list2 = new List<T>();
			while (i < list.Count)
			{
				int controlId = list[i].GetDynamicData().Field.ControlId;
				int j = i + 1;
				List<T> list3 = new List<T>();
				list3.Add(list[i]);
				while (j < list.Count)
				{
					int controlId2 = list[j].GetDynamicData().Field.ControlId;
					bool flag = controlId2 != controlId;
					if (flag)
					{
						break;
					}
					list3.Add(list[j]);
					j++;
				}
				bool flag2 = list3.Count > 1;
				if (flag2)
				{
					DynamicDataDAO.<>c__DisplayClass14_0<T> CS$<>8__locals1 = new DynamicDataDAO.<>c__DisplayClass14_0<T>();
					CS$<>8__locals1.itemToKeep = list3.FirstOrDefault((T g) => g.GetDynamicData().Value != null && g.GetDynamicData().Value is string);
					bool flag3 = CS$<>8__locals1.itemToKeep != null;
					if (flag3)
					{
						T t = list3.FirstOrDefault((T g) => g.GetDynamicData().Value != null && g.GetDynamicData().Value is int);
						bool flag4 = t != null;
						if (flag4)
						{
							CS$<>8__locals1.itemToKeep.GetDynamicData().SecondaryValue = (int)t.GetDynamicData().Value;
						}
					}
					else
					{
						DynamicDataDAO.<>c__DisplayClass14_0<T> CS$<>8__locals2 = CS$<>8__locals1;
						T itemToKeep;
						if ((itemToKeep = list3.FirstOrDefault((T g) => g.GetDynamicData().Value != null && g.GetDynamicData().Value is int)) == null)
						{
							itemToKeep = list3[0];
						}
						CS$<>8__locals2.itemToKeep = itemToKeep;
					}
					list2.AddRange(from g in list3
					where g != CS$<>8__locals1.itemToKeep
					select g);
				}
				i = j;
			}
			foreach (T item in list2)
			{
				datas.Remove(item);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00039FF0 File Offset: 0x000381F0
		private void SetDynamicDataValueFromByteArray(DynamicData dataItem, byte[] bytes, byte[] bytes2, bool defaultBytesIsEncrypted)
		{
			DynamicField field = dataItem.Field;
			eControlCode controlCode = field.ControlCode;
			eControlCode eControlCode = controlCode;
			if (eControlCode <= eControlCode.StaffComboBox)
			{
				if (eControlCode != eControlCode.Label && eControlCode != eControlCode.Picture)
				{
					if (eControlCode != eControlCode.StaffComboBox)
					{
						goto IL_DF;
					}
					string arg = this.DatabaseManager.Encryption.Decrypt(bytes);
					string arg2 = (bytes2 == null) ? "" : this.DatabaseManager.Encryption.Decrypt(bytes2);
					dataItem.Value = string.Format("{0} {1}", arg, arg2);
					return;
				}
			}
			else if (eControlCode != eControlCode.File)
			{
				if (eControlCode != eControlCode.RtfTextBox && eControlCode != eControlCode.MultiLineTextBox)
				{
					goto IL_DF;
				}
				dataItem.Value = ((field.Setting3 == 1) ? this.DatabaseManager.Encryption.Decrypt(bytes) : Encoding.ASCII.GetString(bytes));
				return;
			}
			dataItem.Value = bytes;
			return;
			IL_DF:
			dataItem.Value = (defaultBytesIsEncrypted ? this.DatabaseManager.Encryption.Decrypt(bytes) : Encoding.ASCII.GetString(bytes));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0003A108 File Offset: 0x00038308
		public DynamicData GetDataFromRecords(IDataReader reader)
		{
			DynamicField fieldFromRecord = DynamicFieldDAO.GetFieldFromRecord(reader);
			bool flag = fieldFromRecord == null || !DynamicDataDAO.ReaderContainsColumn(reader, "valint");
			DynamicData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object obj = reader["valint"];
				object obj2 = reader["valdate"];
				object obj3 = reader["valbytes"];
				object obj4 = reader["valtext"];
				string value = (obj4 == DBNull.Value) ? "" : ((string)obj4);
				DynamicData dynamicData = new DynamicData
				{
					Field = fieldFromRecord
				};
				int? num2 = (obj != DBNull.Value) ? new int?((int)obj) : null;
				dynamicData.ValueId = ((num2 != null) ? num2.Value : 0);
				bool flag2 = !DynamicDataDAO.ReaderContainsColumn(reader, "valbytesisencrypted");
				object obj5;
				if (flag2)
				{
					eControlCode controlCode = fieldFromRecord.ControlCode;
					eControlCode eControlCode = controlCode;
					if (eControlCode != eControlCode.TextBox)
					{
						if (eControlCode != eControlCode.DropList)
						{
							obj5 = false;
						}
						else
						{
							obj5 = (fieldFromRecord.Setting3 == -1);
						}
					}
					else
					{
						obj5 = (fieldFromRecord.Setting3 == 1);
					}
				}
				else
				{
					obj5 = reader["valbytesisencrypted"];
				}
				bool defaultBytesIsEncrypted = obj5 != DBNull.Value && Convert.ToBoolean(obj5);
				object obj6 = DynamicDataDAO.ReaderContainsColumn(reader, "valimage") ? reader["valimage"] : DBNull.Value;
				bool flag3 = obj2 != DBNull.Value;
				if (flag3)
				{
					dynamicData.Value = (DateTime)obj2;
				}
				else
				{
					bool flag4 = obj6 != DBNull.Value;
					if (flag4)
					{
						this.SetDynamicDataValueFromByteArray(dynamicData, (byte[])obj6, (obj3 == DBNull.Value) ? null : ((byte[])obj3), defaultBytesIsEncrypted);
					}
					else
					{
						bool flag5 = obj3 != DBNull.Value;
						if (flag5)
						{
							this.SetDynamicDataValueFromByteArray(dynamicData, (byte[])obj3, (obj6 == DBNull.Value) ? null : ((byte[])obj6), defaultBytesIsEncrypted);
						}
						else
						{
							bool flag6 = num2 != null;
							if (flag6)
							{
								int num = num2.Value;
								eControlCode controlCode2 = fieldFromRecord.ControlCode;
								eControlCode eControlCode2 = controlCode2;
								if (eControlCode2 <= eControlCode.StaffComboBox)
								{
									if (eControlCode2 != eControlCode.CheckBox)
									{
										if (eControlCode2 != eControlCode.StaffComboBox)
										{
											goto IL_368;
										}
										bool flag7 = this.staffMembersForLookup == null;
										if (flag7)
										{
											PeopleDAO peopleDAO = new PeopleDAO(this.OpContext);
											this.staffMembersForLookup = peopleDAO.LoadGroupMembers(2);
										}
										PersonBase personBase = this.staffMembersForLookup.FirstOrDefault((PersonBase g) => g.PersonId == num);
										dynamicData.Value = ((personBase == null) ? ("Staff " + num.ToString()) : personBase.GetName());
										goto IL_37E;
									}
								}
								else if (eControlCode2 != eControlCode.ListSelect && eControlCode2 != eControlCode.AccommodationCheckBox)
								{
									if (eControlCode2 != eControlCode.AccommodationDropList)
									{
										goto IL_368;
									}
									IDynamicFieldDAO dynamicFieldDAO = new DynamicFieldDAO(this.OpContext);
									List<DynamicListItem> source = dynamicFieldDAO.LoadListItems(fieldFromRecord.Setting1);
									DynamicListItem dynamicListItem = source.FirstOrDefault((DynamicListItem g) => g.LookupListId == num);
									dynamicData.Value = ((dynamicListItem != null && !string.IsNullOrEmpty(dynamicListItem.LookupText)) ? dynamicListItem.LookupText : num.ToString());
									goto IL_37E;
								}
								bool flag8 = Convert.ToBoolean(num);
								dynamicData.Value = flag8;
								goto IL_37E;
								IL_368:
								dynamicData.Value = num;
								IL_37E:;
							}
						}
					}
				}
				eControlCode controlCode3 = fieldFromRecord.ControlCode;
				eControlCode eControlCode3 = controlCode3;
				if (eControlCode3 <= eControlCode.RadioGroup)
				{
					if (eControlCode3 != eControlCode.DropList && eControlCode3 != eControlCode.RadioGroup)
					{
						goto IL_45A;
					}
				}
				else
				{
					if (eControlCode3 == eControlCode.File)
					{
						bool flag9 = !string.IsNullOrEmpty(value);
						if (flag9)
						{
							dynamicData.Value = value;
						}
						goto IL_45A;
					}
					if (eControlCode3 != eControlCode.MultiCheckBoxDropList && eControlCode3 != eControlCode.AccommodationDropList)
					{
						goto IL_45A;
					}
				}
				bool flag10 = dynamicData.Value != null && dynamicData.Value is int;
				if (flag10)
				{
					dynamicData.ValueId = (int)dynamicData.Value;
					dynamicData.Value = value;
				}
				bool flag11 = fieldFromRecord.ControlCode == eControlCode.MultiCheckBoxDropList;
				if (flag11)
				{
					dynamicData.SecondaryValue = ((obj != DBNull.Value) ? ((int)obj) : 0);
				}
				IL_45A:
				bool flag12 = PeopleDAO.ReaderContainsColumn(reader, "dataid");
				if (flag12)
				{
					dynamicData.DataId = ((reader["dataid"] is DBNull) ? 0 : ((int)reader["dataid"]));
				}
				result = dynamicData;
			}
			return result;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0003A5B8 File Offset: 0x000387B8
		public static List<DynamicDataStorageItem> GetDynamicDataStorageItemListFromRecords(IDataReader reader, OperationContext opContext)
		{
			bool flag = reader == null;
			List<DynamicDataStorageItem> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IList<DynamicDataStorageItem> list = new List<DynamicDataStorageItem>();
				while (reader.Read())
				{
					DynamicDataStorageItem dynamicDataStorageItemFromRecord = DynamicDataDAO.GetDynamicDataStorageItemFromRecord(reader, opContext);
					bool flag2 = dynamicDataStorageItemFromRecord != null;
					if (flag2)
					{
						list.Add(dynamicDataStorageItemFromRecord);
					}
				}
				result = list.ToList<DynamicDataStorageItem>();
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0003A60C File Offset: 0x0003880C
		private static DynamicDataStorageItem GetDynamicDataStorageItemFromRecord(IDataReader record, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DynamicField fieldFromRecord = DynamicFieldDAO.GetFieldFromRecord(record);
			bool flag = fieldFromRecord != null && DynamicDataDAO.ReaderContainsColumn(record, "valint");
			DynamicDataStorageItem result;
			if (flag)
			{
				DynamicDataStorageItem dynamicDataStorageItem = new DynamicDataStorageItem
				{
					Field = fieldFromRecord
				};
				object obj = record["valint"];
				object obj2 = record["valdate"];
				object obj3 = record["valbytes"];
				object obj4 = record["valtext"];
				string text = (obj4 == DBNull.Value) ? "" : ((string)obj4);
				object obj5 = DynamicDataDAO.ReaderContainsColumn(record, "valimage") ? record["valimage"] : DBNull.Value;
				dynamicDataStorageItem.IntValue = ((obj is DBNull) ? null : new int?((int)obj));
				dynamicDataStorageItem.DateTimeValue = ((obj2 is DBNull) ? null : new DateTime?((DateTime)obj2));
				eDynamicDataStorageType storageType = fieldFromRecord.StorageType;
				bool flag2 = obj3 != DBNull.Value;
				if (flag2)
				{
					byte[] array = (byte[])obj3;
					dynamicDataStorageItem.OtherValue = ((storageType == eDynamicDataStorageType.Encrypted) ? databaseLayer.Encryption.Decrypt(array) : Encoding.ASCII.GetString(array));
				}
				bool flag3 = obj5 != DBNull.Value;
				if (flag3)
				{
					byte[] array2 = (byte[])obj5;
					dynamicDataStorageItem.ImageValue = ((storageType == eDynamicDataStorageType.Encrypted) ? Encoding.ASCII.GetBytes(databaseLayer.Encryption.Decrypt(array2)) : array2);
				}
				bool flag4 = string.IsNullOrEmpty(dynamicDataStorageItem.OtherValue) && !string.IsNullOrEmpty(text);
				if (flag4)
				{
					dynamicDataStorageItem.OtherValue = text;
				}
				result = dynamicDataStorageItem;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0003A7E0 File Offset: 0x000389E0
		public List<DynamicData> GetDataListFromRecords(IDataReader reader)
		{
			bool flag = reader == null;
			List<DynamicData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IList<DynamicData> list = new List<DynamicData>();
				while (reader.Read())
				{
					DynamicData dataFromRecords = this.GetDataFromRecords(reader);
					bool flag2 = dataFromRecords != null;
					if (flag2)
					{
						list.Add(dataFromRecords);
					}
				}
				this.MergeDynamicDataIntoUniqueControlIds<DynamicData>(list);
				result = list.ToList<DynamicData>();
			}
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0003A83C File Offset: 0x00038A3C
		[DebuggerStepThrough]
		public Task<List<DynamicData>> GetDataListFromRecordsAsync(DbDataReader reader)
		{
			DynamicDataDAO.<GetDataListFromRecordsAsync>d__21 <GetDataListFromRecordsAsync>d__ = new DynamicDataDAO.<GetDataListFromRecordsAsync>d__21();
			<GetDataListFromRecordsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicData>>.Create();
			<GetDataListFromRecordsAsync>d__.<>4__this = this;
			<GetDataListFromRecordsAsync>d__.reader = reader;
			<GetDataListFromRecordsAsync>d__.<>1__state = -1;
			<GetDataListFromRecordsAsync>d__.<>t__builder.Start<DynamicDataDAO.<GetDataListFromRecordsAsync>d__21>(ref <GetDataListFromRecordsAsync>d__);
			return <GetDataListFromRecordsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0003A888 File Offset: 0x00038A88
		public List<DynamicData> GetDataListFromRecordsAndReturnStudentInfo(IDataReader reader, out PersonBase StudentInfo)
		{
			StudentInfo = null;
			bool flag = reader == null;
			List<DynamicData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IList<DynamicData> list = new List<DynamicData>();
				while (reader.Read())
				{
					bool flag2 = StudentInfo == null;
					if (flag2)
					{
						StudentInfo = PeopleDAO.GetPersonFromReader("", reader, this.OpContext, null);
					}
					DynamicData dataFromRecords = this.GetDataFromRecords(reader);
					bool flag3 = dataFromRecords != null;
					if (flag3)
					{
						list.Add(dataFromRecords);
					}
				}
				this.MergeDynamicDataIntoUniqueControlIds<DynamicData>(list);
				result = list.ToList<DynamicData>();
			}
			return result;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0003A908 File Offset: 0x00038B08
		public IList<DynamicDataSetWithStudentName> GetDataSetListWithStudentNamesFromMapper(IDataReader reader)
		{
			bool flag = reader == null;
			IList<DynamicDataSetWithStudentName> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<DynamicDataSetWithStudentName> list = new List<DynamicDataSetWithStudentName>();
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				DynamicDataSetWithStudentName dynamicDataSetWithStudentName = null;
				while (reader.Read())
				{
					int num = (reader["personid"] is DBNull) ? 0 : ((int)reader["personid"]);
					int num2 = (dynamicDataSetWithStudentName == null || dynamicDataSetWithStudentName.Student == null) ? 0 : dynamicDataSetWithStudentName.Student.PersonId;
					bool flag2 = dynamicDataSetWithStudentName == null || num2 != num;
					if (flag2)
					{
						dynamicDataSetWithStudentName = new DynamicDataSetWithStudentName
						{
							Student = PeopleDAO.GetPersonFromReader("", reader, this.OpContext, batchDecryptor),
							Data = new List<DynamicData>()
						};
						list.Add(dynamicDataSetWithStudentName);
					}
					DynamicData dataFromRecords = this.GetDataFromRecords(reader);
					bool flag3 = dataFromRecords != null;
					if (flag3)
					{
						dynamicDataSetWithStudentName.Data.Add(dataFromRecords);
					}
				}
				this.MergeDynamicDataWithStudentNameIntoUniqueControlIds(list);
				result = list;
			}
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0003AA28 File Offset: 0x00038C28
		public List<DynamicDataSet> GetDataSetListFromRecords(IDataReader reader)
		{
			bool flag = reader == null;
			List<DynamicDataSet> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<DynamicDataSet> list = new List<DynamicDataSet>();
				bool flag2 = reader.ContainsColumn("appointmentid");
				DynamicDataSet dynamicDataSet = null;
				while (reader.Read())
				{
					int num = (!reader.ContainsColumn("personid") || reader["personid"] is DBNull) ? 0 : ((int)reader["personid"]);
					int num2 = flag2 ? ((reader["appointmentid"] is DBNull) ? 0 : ((int)reader["appointmentid"])) : 0;
					int? num3;
					if (dynamicDataSet == null)
					{
						num3 = null;
					}
					else
					{
						DynamicDataContext context = dynamicDataSet.Context;
						num3 = ((context != null) ? new int?(context.PrimaryId) : null);
					}
					int? num4 = num3;
					int valueOrDefault = num4.GetValueOrDefault();
					int? num5;
					if (dynamicDataSet == null)
					{
						num5 = null;
					}
					else
					{
						DynamicDataContext context2 = dynamicDataSet.Context;
						num5 = ((context2 != null) ? new int?(context2.SecondaryId) : null);
					}
					num4 = num5;
					int valueOrDefault2 = num4.GetValueOrDefault();
					bool flag3 = dynamicDataSet == null || valueOrDefault != num || valueOrDefault2 != num2;
					if (flag3)
					{
						dynamicDataSet = new DynamicDataSet
						{
							Context = new DynamicDataContext
							{
								PrimaryId = num,
								SecondaryId = num2
							},
							Data = new List<DynamicData>()
						};
						list.Add(dynamicDataSet);
					}
					DynamicData dataFromRecords = this.GetDataFromRecords(reader);
					bool flag4 = dataFromRecords != null;
					if (flag4)
					{
						dynamicDataSet.Data.Add(dataFromRecords);
					}
				}
				this.MergeDynamicDataIntoUniqueControlIds(list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0003ABCC File Offset: 0x00038DCC
		public string GetDynamicDataSelectQuery(eDynamicFormType dataType)
		{
			return DynamicDataDAO.GetDynamicDataItemQuery(dataType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		public static string GetDynamicDataItemQuery(eDynamicFormType dataType, eDynamicDataStorageLocation location, eQueryType QueryType)
		{
			eDynamicFormType eDynamicFormType = dataType;
			eDynamicFormType eDynamicFormType2 = eDynamicFormType;
			if (eDynamicFormType2 <= eDynamicFormType.PerInstructor)
			{
				if (eDynamicFormType2 <= eDynamicFormType.PerStaff)
				{
					switch (eDynamicFormType2)
					{
					case eDynamicFormType.PerStudent:
						break;
					case eDynamicFormType.PerAppointment:
					{
						eDynamicDataStorageLocation eDynamicDataStorageLocation = location;
						eDynamicDataStorageLocation eDynamicDataStorageLocation2 = eDynamicDataStorageLocation;
						switch (eDynamicDataStorageLocation2)
						{
						case eDynamicDataStorageLocation.MainInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PAMain;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PAMain;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PA;
							}
							goto IL_4ED;
						case eDynamicDataStorageLocation.OtherInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PAOther;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PAOther;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PA;
							}
							goto IL_4ED;
						case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
							break;
						case eDynamicDataStorageLocation.DateTimeInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PADateTime;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PADateTime;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PA;
							}
							goto IL_4ED;
						default:
							if (eDynamicDataStorageLocation2 == eDynamicDataStorageLocation.ImageInfo)
							{
								switch (QueryType)
								{
								case eQueryType.InsertOrUpdate:
									return QueryStorageDynamicData.QI_PAImage;
								case eQueryType.Delete:
									return QueryStorageDynamicData.QD_PAImage;
								case eQueryType.Load:
									return QueryStorageDynamicData.QS_PA;
								}
								goto IL_4ED;
							}
							break;
						}
						throw new NotImplementedException("Unknown data location: " + location.ToString());
						IL_4ED:
						goto IL_1217;
					}
					case eDynamicFormType.Anonymous:
					{
						eDynamicDataStorageLocation eDynamicDataStorageLocation3 = location;
						eDynamicDataStorageLocation eDynamicDataStorageLocation4 = eDynamicDataStorageLocation3;
						switch (eDynamicDataStorageLocation4)
						{
						case eDynamicDataStorageLocation.MainInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_ANMain;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_ANMain;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_AN;
							}
							goto IL_7D5;
						case eDynamicDataStorageLocation.OtherInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_ANOther;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_ANOther;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_AN;
							}
							goto IL_7D5;
						case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
							break;
						case eDynamicDataStorageLocation.DateTimeInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_ANDateTime;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_ANDateTime;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_AN;
							}
							goto IL_7D5;
						default:
							if (eDynamicDataStorageLocation4 == eDynamicDataStorageLocation.ImageInfo)
							{
								switch (QueryType)
								{
								case eQueryType.InsertOrUpdate:
									return QueryStorageDynamicData.QI_ANImage;
								case eQueryType.Delete:
									return QueryStorageDynamicData.QD_ANImage;
								case eQueryType.Load:
									return QueryStorageDynamicData.QS_AN;
								}
								goto IL_7D5;
							}
							break;
						}
						throw new NotImplementedException("Unknown data location: " + location.ToString());
						IL_7D5:
						goto IL_1217;
					}
					case eDynamicFormType.Accommodation:
					{
						eDynamicDataStorageLocation eDynamicDataStorageLocation5 = location;
						eDynamicDataStorageLocation eDynamicDataStorageLocation6 = eDynamicDataStorageLocation5;
						switch (eDynamicDataStorageLocation6)
						{
						case eDynamicDataStorageLocation.MainInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_AccommodationMain;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_AccommodationMain;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_Accommodation;
							}
							goto IL_661;
						case eDynamicDataStorageLocation.OtherInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_AccommodationOther;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_AccommodationOther;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_Accommodation;
							}
							goto IL_661;
						case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
							break;
						case eDynamicDataStorageLocation.DateTimeInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_AccommodationDateTime;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_AccommodationDateTime;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_Accommodation;
							}
							goto IL_661;
						default:
							if (eDynamicDataStorageLocation6 == eDynamicDataStorageLocation.ImageInfo)
							{
								switch (QueryType)
								{
								case eQueryType.InsertOrUpdate:
									return QueryStorageDynamicData.QI_AccommodationImage;
								case eQueryType.Delete:
									return QueryStorageDynamicData.QD_AccommodationImage;
								case eQueryType.Load:
									return QueryStorageDynamicData.QS_Accommodation;
								}
								goto IL_661;
							}
							break;
						}
						throw new NotImplementedException("Unknown data location: " + location.ToString());
						IL_661:
						goto IL_1217;
					}
					case eDynamicFormType.AccommodationTemplateOnly:
					{
						eDynamicDataStorageLocation eDynamicDataStorageLocation7 = location;
						eDynamicDataStorageLocation eDynamicDataStorageLocation8 = eDynamicDataStorageLocation7;
						switch (eDynamicDataStorageLocation8)
						{
						case eDynamicDataStorageLocation.MainInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_AccommodationTemplateOnlyMain;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_AccommodationTemplateOnlyMain;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_AccommodationTemplateOnly;
							}
							goto IL_205;
						case eDynamicDataStorageLocation.OtherInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_AccommodationTemplateOnlyOther;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_AccommodationTemplateOnlyOther;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_AccommodationTemplateOnly;
							}
							goto IL_205;
						case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
							break;
						case eDynamicDataStorageLocation.DateTimeInfo:
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_AccommodationTemplateOnlyDateTime;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_AccommodationTemplateOnlyDateTime;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_AccommodationTemplateOnly;
							}
							goto IL_205;
						default:
							if (eDynamicDataStorageLocation8 == eDynamicDataStorageLocation.ImageInfo)
							{
								switch (QueryType)
								{
								case eQueryType.InsertOrUpdate:
									return QueryStorageDynamicData.QI_AccommodationTemplateOnlyImage;
								case eQueryType.Delete:
									return QueryStorageDynamicData.QD_AccommodationTemplateOnlyImage;
								case eQueryType.Load:
									return QueryStorageDynamicData.QS_AccommodationTemplateOnly;
								}
								goto IL_205;
							}
							break;
						}
						throw new NotImplementedException("Unknown data location: " + location.ToString());
						IL_205:
						goto IL_1217;
					}
					default:
						if (eDynamicFormType2 != eDynamicFormType.PerStaff)
						{
							goto IL_11FA;
						}
						break;
					}
					eDynamicDataStorageLocation eDynamicDataStorageLocation9 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation10 = eDynamicDataStorageLocation9;
					switch (eDynamicDataStorageLocation10)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PSMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PSMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PS;
						}
						goto IL_379;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PSOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PSOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PS;
						}
						goto IL_379;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PSDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PSDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PS;
						}
						goto IL_379;
					default:
						if (eDynamicDataStorageLocation10 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PSImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PSImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PS;
							}
							goto IL_379;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_379:
					goto IL_1217;
				}
				if (eDynamicFormType2 == eDynamicFormType.PerDate)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation11 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation12 = eDynamicDataStorageLocation11;
					switch (eDynamicDataStorageLocation12)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PMMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PMMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PM;
						}
						goto IL_949;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PMOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PMOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PM;
						}
						goto IL_949;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PMDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PMDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PM;
						}
						goto IL_949;
					default:
						if (eDynamicDataStorageLocation12 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PMImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PMImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PM;
							}
							goto IL_949;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_949:
					goto IL_1217;
				}
				if (eDynamicFormType2 == eDynamicFormType.PerInstructor)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation13 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation14 = eDynamicDataStorageLocation13;
					switch (eDynamicDataStorageLocation14)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PIMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PIMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PI;
						}
						goto IL_C31;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PIOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PIOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PI;
						}
						goto IL_C31;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PIDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PIDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PI;
						}
						goto IL_C31;
					default:
						if (eDynamicDataStorageLocation14 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PIImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PIImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PI;
							}
							goto IL_C31;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_C31:
					goto IL_1217;
				}
			}
			else if (eDynamicFormType2 <= eDynamicFormType.PerWaitingList)
			{
				if (eDynamicFormType2 == eDynamicFormType.PerCase)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation15 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation16 = eDynamicDataStorageLocation15;
					switch (eDynamicDataStorageLocation16)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PCMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PCMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PC;
						}
						goto IL_ABD;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PCOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PCOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PC;
						}
						goto IL_ABD;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_PCDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_PCDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_PC;
						}
						goto IL_ABD;
					default:
						if (eDynamicDataStorageLocation16 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_PCImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_PCImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_PC;
							}
							goto IL_ABD;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_ABD:
					goto IL_1217;
				}
				if (eDynamicFormType2 == eDynamicFormType.PerWaitingList)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation17 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation18 = eDynamicDataStorageLocation17;
					switch (eDynamicDataStorageLocation18)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_WlMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_WlMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Wl;
						}
						goto IL_F19;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_WlOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_WlOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Wl;
						}
						goto IL_F19;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_WlDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_WlDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Wl;
						}
						goto IL_F19;
					default:
						if (eDynamicDataStorageLocation18 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_WlImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_WlImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_Wl;
							}
							goto IL_F19;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_F19:
					goto IL_1217;
				}
			}
			else
			{
				if (eDynamicFormType2 == eDynamicFormType.PerInventory)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation19 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation20 = eDynamicDataStorageLocation19;
					switch (eDynamicDataStorageLocation20)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_InvMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_InvMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Inv;
						}
						goto IL_DA5;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_InvOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_InvOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Inv;
						}
						goto IL_DA5;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_InvDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_InvDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Inv;
						}
						goto IL_DA5;
					default:
						if (eDynamicDataStorageLocation20 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_InvImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_InvImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_Inv;
							}
							goto IL_DA5;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_DA5:
					goto IL_1217;
				}
				if (eDynamicFormType2 == eDynamicFormType.Survey)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation21 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation22 = eDynamicDataStorageLocation21;
					switch (eDynamicDataStorageLocation22)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_SurveyMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_SurveyMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Survey;
						}
						goto IL_108D;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_SurveyOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_SurveyOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Survey;
						}
						goto IL_108D;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_SurveyDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_SurveyDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_Survey;
						}
						goto IL_108D;
					default:
						if (eDynamicDataStorageLocation22 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_SurveyImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_SurveyImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_Survey;
							}
							goto IL_108D;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_108D:
					goto IL_1217;
				}
				if (eDynamicFormType2 == eDynamicFormType.OnlineForm)
				{
					eDynamicDataStorageLocation eDynamicDataStorageLocation23 = location;
					eDynamicDataStorageLocation eDynamicDataStorageLocation24 = eDynamicDataStorageLocation23;
					switch (eDynamicDataStorageLocation24)
					{
					case eDynamicDataStorageLocation.MainInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_OnlineFormMain;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_OnlineFormMain;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_OnlineForm;
						}
						goto IL_11F8;
					case eDynamicDataStorageLocation.OtherInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_OnlineFormOther;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_OnlineFormOther;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_OnlineForm;
						}
						goto IL_11F8;
					case eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo:
						break;
					case eDynamicDataStorageLocation.DateTimeInfo:
						switch (QueryType)
						{
						case eQueryType.InsertOrUpdate:
							return QueryStorageDynamicData.QI_OnlineFormDateTime;
						case eQueryType.Delete:
							return QueryStorageDynamicData.QD_OnlineFormDateTime;
						case eQueryType.Load:
							return QueryStorageDynamicData.QS_OnlineForm;
						}
						goto IL_11F8;
					default:
						if (eDynamicDataStorageLocation24 == eDynamicDataStorageLocation.ImageInfo)
						{
							switch (QueryType)
							{
							case eQueryType.InsertOrUpdate:
								return QueryStorageDynamicData.QI_OnlineFormImage;
							case eQueryType.Delete:
								return QueryStorageDynamicData.QD_OnlineFormImage;
							case eQueryType.Load:
								return QueryStorageDynamicData.QS_OnlineForm;
							}
							goto IL_11F8;
						}
						break;
					}
					throw new NotImplementedException("Unknown data location: " + location.ToString());
					IL_11F8:
					goto IL_1217;
				}
			}
			IL_11FA:
			throw new NotImplementedException("Unknown form type: " + dataType.ToString());
			IL_1217:
			return null;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0003BE14 File Offset: 0x0003A014
		private static BinaryFile GetFileFromDataRow(DataRow dr)
		{
			return new BinaryFile
			{
				FileName = dr["filename"].ToString(),
				ByteArray = (byte[])dr["filebytes"],
				Id = dr["fileid"].ToString()
			};
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0003BE74 File Offset: 0x0003A074
		private DbParameter GetDynamicDataParameter(DynamicData dataItem, out eDynamicDataStorageLocation location)
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			eControlCode controlCode = dataItem.Field.ControlCode;
			eControlCode eControlCode = controlCode;
			if (eControlCode <= eControlCode.FileList)
			{
				switch (eControlCode)
				{
				case eControlCode.TextBox:
				{
					location = eDynamicDataStorageLocation.OtherInfo;
					string text = (dataItem.Value == null) ? "" : ((string)dataItem.Value);
					bool flag = text.Length < 1;
					if (flag)
					{
						return null;
					}
					bool flag2 = dataItem.Field.Setting3 == 1;
					if (flag2)
					{
						return this.DatabaseManager.GetParameter("@val", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(text));
					}
					return this.DatabaseManager.GetParameter("@val", DbType.Binary, utf8Encoding.GetBytes(text));
				}
				case eControlCode.CheckBox:
				{
					location = eDynamicDataStorageLocation.MainInfo;
					bool flag3 = dataItem.Value != null && Convert.ToBoolean(dataItem.Value);
					return (!flag3) ? null : this.DatabaseManager.GetParameter("@val", DbType.Int32, 1);
				}
				case eControlCode.DropList:
				{
					bool flag4 = dataItem.Field.Setting3 == 0;
					if (flag4)
					{
						location = eDynamicDataStorageLocation.MainInfo;
						int num = (dataItem.Value != null && dataItem.Value is int) ? ((int)dataItem.Value) : 0;
						bool flag5 = num < 1;
						if (flag5)
						{
							return null;
						}
						return this.DatabaseManager.GetParameter("@val", DbType.Int32, num);
					}
					else
					{
						location = eDynamicDataStorageLocation.OtherInfo;
						string text = (dataItem.Value == null) ? "" : ((string)dataItem.Value);
						bool flag6 = text.Length < 1;
						if (flag6)
						{
							return null;
						}
						bool flag7 = dataItem.Field.Setting3 == -1;
						if (flag7)
						{
							return this.DatabaseManager.GetParameter("@val", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(text));
						}
						return this.DatabaseManager.GetParameter("@val", DbType.Binary, utf8Encoding.GetBytes(text));
					}
					break;
				}
				case eControlCode.RadioButton:
				case eControlCode.Label:
					break;
				case eControlCode.Date:
				{
					location = eDynamicDataStorageLocation.DateTimeInfo;
					bool flag8 = dataItem.Value == null || !(dataItem.Value is DateTime);
					if (flag8)
					{
						return null;
					}
					return this.DatabaseManager.GetParameter("@val", DbType.DateTime, (DateTime)dataItem.Value);
				}
				default:
					if (eControlCode != eControlCode.RadioGroup)
					{
						if (eControlCode == eControlCode.FileList)
						{
							location = eDynamicDataStorageLocation.OtherInfo;
							string text = (dataItem.Value == null) ? "" : ((string)dataItem.Value);
							return this.DatabaseManager.GetParameter("@val", DbType.Binary, utf8Encoding.GetBytes(text));
						}
					}
					else
					{
						location = eDynamicDataStorageLocation.MainInfo;
						int num2 = (dataItem.Value != null && dataItem.Value is int) ? ((int)dataItem.Value) : 0;
						bool flag9 = num2 < 1;
						if (flag9)
						{
							return null;
						}
						return this.DatabaseManager.GetParameter("@val", DbType.Int32, num2);
					}
					break;
				}
			}
			else if (eControlCode != eControlCode.Picture)
			{
				if (eControlCode != eControlCode.StaffComboBox)
				{
					if (eControlCode == eControlCode.RtfTextBox)
					{
						location = eDynamicDataStorageLocation.ImageInfo;
						string text = (dataItem.Value == null) ? "" : ((string)dataItem.Value);
						bool flag10 = text.Length < 1;
						if (flag10)
						{
							return null;
						}
						bool flag11 = dataItem.Field.Setting3 == 1;
						if (flag11)
						{
							return this.DatabaseManager.GetParameter("@val", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(text));
						}
						return this.DatabaseManager.GetParameter("@val", DbType.Binary, utf8Encoding.GetBytes(text));
					}
				}
				else
				{
					location = eDynamicDataStorageLocation.MainInfo;
					int num3 = (dataItem.Value != null && dataItem.Value is int) ? ((int)dataItem.Value) : 0;
					bool flag12 = num3 < 1;
					if (flag12)
					{
						return null;
					}
					return this.DatabaseManager.GetParameter("@val", DbType.Int32, num3);
				}
			}
			else
			{
				location = eDynamicDataStorageLocation.ImageInfo;
				byte[] array = (dataItem.Value == null) ? new byte[0] : ((byte[])dataItem.Value);
				bool flag13 = array.Length < 1;
				if (flag13)
				{
					return null;
				}
				return this.DatabaseManager.GetParameter("@val", DbType.Binary, array);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0003C2F0 File Offset: 0x0003A4F0
		public IList<DynamicDataSet> LoadData(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType)
		{
			string text = DynamicDataDAO.GetDynamicDataItemQuery(ScreensType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			text = text.Replace("ps.appointmentid=@appid", "ps.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appids,','))");
			bool flag = string.IsNullOrEmpty(text);
			IList<DynamicDataSet> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<DynamicDataSet> list = new List<DynamicDataSet>();
				foreach (int num in ScreenNums)
				{
					DbParameter[] array = new DbParameter[5];
					array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PrimaryId);
					array[1] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
					array[2] = this.DatabaseManager.GetParameter("@cids", DbType.String, "");
					array[3] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, num);
					array[4] = this.DatabaseManager.GetParameter("@appids", DbType.String, string.Join(",", SecondaryIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
					DbParameter[] parameters = array;
					using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(text, parameters))
					{
						bool flag2 = dataReader != null;
						if (flag2)
						{
							DynamicDataSet dynamicDataSet = null;
							while (dataReader.Read())
							{
								DynamicData dataFromRecords = this.GetDataFromRecords(dataReader);
								bool flag3 = dataFromRecords != null;
								if (flag3)
								{
									int num2 = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
									int num3 = (dynamicDataSet == null || dynamicDataSet.Context == null) ? 0 : dynamicDataSet.Context.SecondaryId;
									bool flag4 = dynamicDataSet == null || num2 != num3;
									if (flag4)
									{
										dynamicDataSet = new DynamicDataSet
										{
											Context = new DynamicDataContext
											{
												PrimaryId = PrimaryId,
												SecondaryId = num2
											},
											Data = new List<DynamicData>()
										};
										list.Add(dynamicDataSet);
									}
									dynamicDataSet.Data.Add(dataFromRecords);
								}
							}
						}
					}
				}
				this.MergeDynamicDataIntoUniqueControlIds(list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0003C574 File Offset: 0x0003A774
		[DebuggerStepThrough]
		public Task<IList<DynamicDataSet>> LoadDataAsync(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType)
		{
			DynamicDataDAO.<LoadDataAsync>d__30 <LoadDataAsync>d__ = new DynamicDataDAO.<LoadDataAsync>d__30();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicDataSet>>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.PrimaryId = PrimaryId;
			<LoadDataAsync>d__.SecondaryIds = SecondaryIds;
			<LoadDataAsync>d__.ScreenNums = ScreenNums;
			<LoadDataAsync>d__.ScreensType = ScreensType;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<DynamicDataDAO.<LoadDataAsync>d__30>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0003C5D8 File Offset: 0x0003A7D8
		public List<DynamicData> LoadData(DynamicDataContext Context, int FormNum, eDynamicFormType FormType)
		{
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(FormType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			bool flag = string.IsNullOrEmpty(dynamicDataItemQuery);
			List<DynamicData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
				bool flag3 = flag2;
				DbParameter[] parameters;
				if (flag3)
				{
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId),
						this.DatabaseManager.GetParameter("@appid", DbType.Int32, Context.SecondaryId),
						this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0),
						this.DatabaseManager.GetParameter("@cids", DbType.String, ""),
						this.DatabaseManager.GetParameter("@screennum", DbType.Int32, FormNum)
					};
				}
				else
				{
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId),
						this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0),
						this.DatabaseManager.GetParameter("@cids", DbType.String, ""),
						this.DatabaseManager.GetParameter("@screennum", DbType.Int32, FormNum)
					};
				}
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(dynamicDataItemQuery, parameters))
				{
					bool flag4 = dataReader != null;
					if (flag4)
					{
						return this.GetDataListFromRecords(dataReader);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0003C788 File Offset: 0x0003A988
		[DebuggerStepThrough]
		public Task<List<DynamicData>> LoadDataAsync(DynamicDataContext Context, int FormNum, eDynamicFormType FormType)
		{
			DynamicDataDAO.<LoadDataAsync>d__32 <LoadDataAsync>d__ = new DynamicDataDAO.<LoadDataAsync>d__32();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicData>>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.Context = Context;
			<LoadDataAsync>d__.FormNum = FormNum;
			<LoadDataAsync>d__.FormType = FormType;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<DynamicDataDAO.<LoadDataAsync>d__32>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0003C7E4 File Offset: 0x0003A9E4
		public List<DynamicData> LoadData(DynamicDataContext Context, DynamicForm Form)
		{
			return this.LoadData(Context, Form.ScreenNum, Form.FormType);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0003C80C File Offset: 0x0003AA0C
		public int UploadDocumentToDatabase(BinaryFile File, int fileTypeCode = 1000)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@filename", DbType.String, File.FileName),
				this.DatabaseManager.GetParameter("@filetypecode", DbType.Int32, fileTypeCode),
				this.DatabaseManager.GetParameter("@isencrypted", DbType.Boolean, false),
				this.DatabaseManager.GetParameter("@iscompressed", DbType.Boolean, false),
				this.DatabaseManager.GetParameter("@whouploaded", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@filebytes", DbType.Binary, File.ByteArray)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("INSERT INTO files (filebytes,filename,filetypecode,isencrypted,iscompressed,dateuploaded,whouploaded)\r\nVALUES (@filebytes,@filename,@filetypecode,@isencrypted,@iscompressed,getdate(),@whouploaded);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS fileid", parameters);
			bool flag = dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			int result;
			if (flag)
			{
				result = (int)dataTable.Rows[0][0];
			}
			else
			{
				CWLogger.Logger.Error("UploadDocumentToDatabaseError:filename={0}:filetypecode={1}:filebyteslen={2}", File.FileName, 1000, (File.ByteArray == null) ? "NULL" : File.ByteArray.Length.ToString());
				result = 0;
			}
			return result;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0003C970 File Offset: 0x0003AB70
		[DebuggerStepThrough]
		public Task<int> UploadDocumentToDatabaseAsync(BinaryFile File, int fileTypeCode = 1000)
		{
			DynamicDataDAO.<UploadDocumentToDatabaseAsync>d__35 <UploadDocumentToDatabaseAsync>d__ = new DynamicDataDAO.<UploadDocumentToDatabaseAsync>d__35();
			<UploadDocumentToDatabaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadDocumentToDatabaseAsync>d__.<>4__this = this;
			<UploadDocumentToDatabaseAsync>d__.File = File;
			<UploadDocumentToDatabaseAsync>d__.fileTypeCode = fileTypeCode;
			<UploadDocumentToDatabaseAsync>d__.<>1__state = -1;
			<UploadDocumentToDatabaseAsync>d__.<>t__builder.Start<DynamicDataDAO.<UploadDocumentToDatabaseAsync>d__35>(ref <UploadDocumentToDatabaseAsync>d__);
			return <UploadDocumentToDatabaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0003C9C4 File Offset: 0x0003ABC4
		public BinaryFile LoadFileFromDocuments(int StudentPersonId, int FileId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@fileid", DbType.Int32, FileId)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery("SELECT fileid,filename,filebytes,isencrypted,iscompressed,dateuploaded,whouploaded\r\nFROM files \r\nWHERE fileid=@fileid", parameters);
			bool flag = dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			BinaryFile result;
			if (flag)
			{
				result = DynamicDataDAO.GetFileFromDataRow(dataTable.Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0003CA54 File Offset: 0x0003AC54
		[DebuggerStepThrough]
		public Task<BinaryFile> LoadFileFromDocumentsAsync(int StudentPersonId, int FileId)
		{
			DynamicDataDAO.<LoadFileFromDocumentsAsync>d__37 <LoadFileFromDocumentsAsync>d__ = new DynamicDataDAO.<LoadFileFromDocumentsAsync>d__37();
			<LoadFileFromDocumentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<LoadFileFromDocumentsAsync>d__.<>4__this = this;
			<LoadFileFromDocumentsAsync>d__.StudentPersonId = StudentPersonId;
			<LoadFileFromDocumentsAsync>d__.FileId = FileId;
			<LoadFileFromDocumentsAsync>d__.<>1__state = -1;
			<LoadFileFromDocumentsAsync>d__.<>t__builder.Start<DynamicDataDAO.<LoadFileFromDocumentsAsync>d__37>(ref <LoadFileFromDocumentsAsync>d__);
			return <LoadFileFromDocumentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0003CAA8 File Offset: 0x0003ACA8
		public void SaveData(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType)
		{
			foreach (DynamicData dynamicData in data)
			{
				eDynamicDataStorageLocation location;
				DbParameter dynamicDataParameter = this.GetDynamicDataParameter(dynamicData, out location);
				bool flag = dynamicDataParameter != null;
				if (flag)
				{
					string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(DataType, location, eQueryType.InsertOrUpdate);
					bool flag2 = !string.IsNullOrEmpty(dynamicDataItemQuery);
					if (flag2)
					{
						bool flag3 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
						bool flag4 = flag3;
						DbParameter[] parameters;
						if (flag4)
						{
							parameters = new DbParameter[]
							{
								this.DatabaseManager.GetParameter("@pid", DbType.Int32, context.PrimaryId),
								this.DatabaseManager.GetParameter("@cid", DbType.Int32, dynamicData.Field.ControlId),
								this.DatabaseManager.GetParameter("@appid", DbType.Int32, context.SecondaryId),
								dynamicDataParameter
							};
						}
						else
						{
							parameters = new DbParameter[]
							{
								this.DatabaseManager.GetParameter("@pid", DbType.Int32, context.PrimaryId),
								this.DatabaseManager.GetParameter("@cid", DbType.Int32, dynamicData.Field.ControlId),
								this.DatabaseManager.GetParameter("@appid", DbType.Int32, context.SecondaryId),
								dynamicDataParameter
							};
						}
						this.DatabaseManager.ExecuteNonQuery(dynamicDataItemQuery, parameters);
					}
				}
				else
				{
					this.DeleteDataItem(context, dynamicData.Field.ControlId, dynamicData.Field.ControlCode, DataType, location);
				}
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0003CC78 File Offset: 0x0003AE78
		[DebuggerStepThrough]
		public Task SaveDataAsync(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType)
		{
			DynamicDataDAO.<SaveDataAsync>d__39 <SaveDataAsync>d__ = new DynamicDataDAO.<SaveDataAsync>d__39();
			<SaveDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveDataAsync>d__.<>4__this = this;
			<SaveDataAsync>d__.context = context;
			<SaveDataAsync>d__.data = data;
			<SaveDataAsync>d__.DataType = DataType;
			<SaveDataAsync>d__.<>1__state = -1;
			<SaveDataAsync>d__.<>t__builder.Start<DynamicDataDAO.<SaveDataAsync>d__39>(ref <SaveDataAsync>d__);
			return <SaveDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0003CCD4 File Offset: 0x0003AED4
		public void DeleteDataItem(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location = eDynamicDataStorageLocation.Unknown)
		{
			bool flag = location == eDynamicDataStorageLocation.Unknown;
			if (flag)
			{
				DynamicControlAttribute attribute = eControlCode.GetAttribute();
				bool flag2 = attribute != null;
				if (flag2)
				{
					location = attribute.StorageLocation;
				}
			}
			bool flag3 = location == (eDynamicDataStorageLocation.MainInfo | eDynamicDataStorageLocation.OtherInfo);
			if (flag3)
			{
				this.DeleteDataItem2(context, ControlId, eControlCode, DataType, eDynamicDataStorageLocation.MainInfo);
				this.DeleteDataItem2(context, ControlId, eControlCode, DataType, eDynamicDataStorageLocation.OtherInfo);
			}
			else
			{
				this.DeleteDataItem2(context, ControlId, eControlCode, DataType, location);
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0003CD38 File Offset: 0x0003AF38
		[DebuggerStepThrough]
		public Task DeleteDataItemAsync(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location = eDynamicDataStorageLocation.Unknown)
		{
			DynamicDataDAO.<DeleteDataItemAsync>d__41 <DeleteDataItemAsync>d__ = new DynamicDataDAO.<DeleteDataItemAsync>d__41();
			<DeleteDataItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteDataItemAsync>d__.<>4__this = this;
			<DeleteDataItemAsync>d__.context = context;
			<DeleteDataItemAsync>d__.ControlId = ControlId;
			<DeleteDataItemAsync>d__.eControlCode = eControlCode;
			<DeleteDataItemAsync>d__.DataType = DataType;
			<DeleteDataItemAsync>d__.location = location;
			<DeleteDataItemAsync>d__.<>1__state = -1;
			<DeleteDataItemAsync>d__.<>t__builder.Start<DynamicDataDAO.<DeleteDataItemAsync>d__41>(ref <DeleteDataItemAsync>d__);
			return <DeleteDataItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		private void DeleteDataItem2(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location)
		{
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(DataType, location, eQueryType.Delete);
			bool flag = !string.IsNullOrEmpty(dynamicDataItemQuery);
			if (flag)
			{
				bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
				bool flag3 = flag2;
				DbParameter[] parameters;
				if (flag3)
				{
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@pid", DbType.Int32, context.PrimaryId),
						this.DatabaseManager.GetParameter("@cid", DbType.Int32, ControlId),
						this.DatabaseManager.GetParameter("@appid", DbType.Int32, context.SecondaryId),
						this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
					};
				}
				else
				{
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@pid", DbType.Int32, context.PrimaryId),
						this.DatabaseManager.GetParameter("@cid", DbType.Int32, ControlId),
						this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
					};
				}
				this.DatabaseManager.ExecuteNonQuery(dynamicDataItemQuery, parameters);
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0003CEF0 File Offset: 0x0003B0F0
		[DebuggerStepThrough]
		private Task DeleteDataItem2Async(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location)
		{
			DynamicDataDAO.<DeleteDataItem2Async>d__43 <DeleteDataItem2Async>d__ = new DynamicDataDAO.<DeleteDataItem2Async>d__43();
			<DeleteDataItem2Async>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteDataItem2Async>d__.<>4__this = this;
			<DeleteDataItem2Async>d__.context = context;
			<DeleteDataItem2Async>d__.ControlId = ControlId;
			<DeleteDataItem2Async>d__.eControlCode = eControlCode;
			<DeleteDataItem2Async>d__.DataType = DataType;
			<DeleteDataItem2Async>d__.location = location;
			<DeleteDataItem2Async>d__.<>1__state = -1;
			<DeleteDataItem2Async>d__.<>t__builder.Start<DynamicDataDAO.<DeleteDataItem2Async>d__43>(ref <DeleteDataItem2Async>d__);
			return <DeleteDataItem2Async>d__.<>t__builder.Task;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0003CF5C File Offset: 0x0003B15C
		public IList<int> FindPerAppointmentExistingDataForAnyAppointment(int pid, IList<int> controlIds)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, pid);
			array[1] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", (from g in controlIds
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			List<int> list = new List<int>();
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT orderid AS controlid INTO #t1 FROM splitorderids(@cids,',')\r\nSELECT controlid FROM maininfopa WHERE personid=@pid AND controlid IN (SELECT controlid FROM #t1)\r\nUNION ALL\r\nSELECT controlid FROM otherinfopa WHERE personid=@pid AND controlid IN (SELECT controlid FROM #t1)\r\nUNION ALL\r\nSELECT controlid FROM datetimeinfopa WHERE personid=@pid AND controlid IN (SELECT controlid FROM #t1)\r\nUNION ALL\r\nSELECT controlid FROM imageinfopa WHERE personid=@pid AND controlid IN (SELECT controlid FROM #t1)\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					int num = (dataReader["controlid"] is DBNull) ? 0 : ((int)dataReader["controlid"]);
					bool flag2 = num < 1 || list.Contains(num);
					if (!flag2)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0003D078 File Offset: 0x0003B278
		public List<DynamicData> LoadDataByFields(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType)
		{
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(DataType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			bool flag = string.IsNullOrEmpty(dynamicDataItemQuery);
			List<DynamicData> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
				bool flag3 = flag2;
				DbParameter[] parameters;
				if (flag3)
				{
					DbParameter[] array = new DbParameter[5];
					array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId);
					array[1] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, Context.SecondaryId);
					array[2] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
					array[3] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
					array[4] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, 0);
					parameters = array;
				}
				else
				{
					DbParameter[] array2 = new DbParameter[4];
					array2[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId);
					array2[1] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
					array2[2] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
					array2[3] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, 0);
					parameters = array2;
				}
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(dynamicDataItemQuery, parameters))
				{
					bool flag4 = dataReader != null;
					if (flag4)
					{
						List<DynamicData> dataListFromRecords = this.GetDataListFromRecords(dataReader);
						dataListFromRecords.Sort((DynamicData g1, DynamicData g2) => ControlIds.IndexOf(g1.Field.ControlId).CompareTo(ControlIds.IndexOf(g2.Field.ControlId)));
						return dataListFromRecords;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0003D2B4 File Offset: 0x0003B4B4
		[DebuggerStepThrough]
		public Task<List<DynamicData>> LoadDataByFieldsAsync(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType)
		{
			DynamicDataDAO.<LoadDataByFieldsAsync>d__46 <LoadDataByFieldsAsync>d__ = new DynamicDataDAO.<LoadDataByFieldsAsync>d__46();
			<LoadDataByFieldsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicData>>.Create();
			<LoadDataByFieldsAsync>d__.<>4__this = this;
			<LoadDataByFieldsAsync>d__.Context = Context;
			<LoadDataByFieldsAsync>d__.ControlIds = ControlIds;
			<LoadDataByFieldsAsync>d__.DataType = DataType;
			<LoadDataByFieldsAsync>d__.<>1__state = -1;
			<LoadDataByFieldsAsync>d__.<>t__builder.Start<DynamicDataDAO.<LoadDataByFieldsAsync>d__46>(ref <LoadDataByFieldsAsync>d__);
			return <LoadDataByFieldsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0003D310 File Offset: 0x0003B510
		public IList<DynamicDataSet> LoadInstructorFormDataForMultipleExams(IList<int> examIds, IList<int> controlIds)
		{
			List<int> list = examIds.Distinct<int>().ToList<int>();
			IList<Chunk> list2 = list.BreakdownItemsIntoChunks(500);
			string value = string.Join(",", (from g in controlIds
			select g.ToString()).ToArray<string>());
			List<DynamicDataSet> list3 = new List<DynamicDataSet>();
			foreach (Chunk chunk in list2)
			{
				DbParameter[] array = new DbParameter[2];
				array[0] = this.DatabaseManager.GetParameter("@cids", DbType.String, value);
				array[1] = this.DatabaseManager.GetParameter("@examids", DbType.String, string.Join(",", (from g in list.GetRange(chunk.Start, chunk.End - chunk.Start + 1)
				select g.ToString()).ToArray<string>()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT orderid AS personid INTO #texamids FROM splitorderids(COALESCE(@examids,''),',');\r\nSELECT orderid AS controlid INTO #tcids FROM splitorderids(COALESCE(@cids,''),',');\r\n\r\nSELECT    ps.appointmentid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\nFROM        perinstructordata2 ps LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.appointmentid IN (SELECT personid FROM #texamids)\r\n            AND ps.controlid IN (SELECT controlid FROM #tcids)\r\nORDER BY ps.appointmentid,ps.controlid\r\n\r\nDROP TABLE #texamids\r\nDROP TABLE #tcids", parameters))
				{
					bool flag = dataReader != null;
					if (flag)
					{
						List<DynamicDataSet> dataSetListFromRecords = this.GetDataSetListFromRecords(dataReader);
						bool flag2 = dataSetListFromRecords == null || dataSetListFromRecords.Count < 1;
						if (!flag2)
						{
							foreach (DynamicDataSet dynamicDataSet in dataSetListFromRecords)
							{
								int examId = dynamicDataSet.Context.SecondaryId;
								bool flag3 = list3.All((DynamicDataSet g) => g.Context.SecondaryId != examId);
								if (flag3)
								{
									list3.Add(dynamicDataSet);
								}
							}
						}
					}
				}
			}
			return list3;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0003D544 File Offset: 0x0003B744
		public IDictionary<int, DateTime?> LoadDateTimeDynamicPerStudentDataForStudents(int[] studentPersonIds, int cid)
		{
			DbParameter[] array = new DbParameter[2];
			array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", (from g in studentPersonIds
			select g.ToString()).ToArray<string>()));
			array[1] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, cid);
			DbParameter[] parameters = array;
			IDictionary<int, DateTime?> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT personid,controlvalue FROM datetimeinfops WHERE controlid=@cid AND personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) ORDER BY personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, DateTime?> dictionary = new Dictionary<int, DateTime?>();
					while (dataReader.Read())
					{
						int num = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							DateTime? value = (dataReader["controlvalue"] is DBNull) ? null : new DateTime?((DateTime)dataReader["controlvalue"]);
							dictionary.Add(num, value);
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0003D690 File Offset: 0x0003B890
		[DebuggerStepThrough]
		public Task<IDictionary<int, DateTime?>> LoadDateTimeDynamicPerStudentDataForStudentsAsync(int[] studentPersonIds, int cid)
		{
			DynamicDataDAO.<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__49 <LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__ = new DynamicDataDAO.<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__49();
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDictionary<int, DateTime?>>.Create();
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>4__this = this;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.studentPersonIds = studentPersonIds;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.cid = cid;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>1__state = -1;
			<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>t__builder.Start<DynamicDataDAO.<LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__49>(ref <LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__);
			return <LoadDateTimeDynamicPerStudentDataForStudentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0003D6E4 File Offset: 0x0003B8E4
		private List<DynamicDataSet> LoadPerStudentDataForMultipleStudents(string queryToUse, List<int> PersonIds, List<int> ControlIds)
		{
			int i = 0;
			List<DynamicDataSet> list = new List<DynamicDataSet>();
			while (i < PersonIds.Count)
			{
				List<int> range = PersonIds.GetRange(i, Math.Min(PersonIds.Count - i, 100));
				DbParameter[] array = new DbParameter[2];
				array[0] = this.DatabaseManager.GetParameter("@pids", DbType.String, string.Join(",", range.ConvertAll<string>((int f) => f.ToString()).ToArray()));
				array[1] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(queryToUse, parameters))
				{
					bool flag = dataReader != null;
					if (flag)
					{
						List<DynamicDataSet> dataSetListFromRecords = this.GetDataSetListFromRecords(dataReader);
						bool flag2 = dataSetListFromRecords != null && dataSetListFromRecords.Count > 0;
						if (flag2)
						{
							list.AddRange(dataSetListFromRecords);
						}
					}
				}
				i += 100;
			}
			return list;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0003D838 File Offset: 0x0003BA38
		public List<DynamicDataSet> LoadPerCaseDataForMultipleStudents(List<int> PersonIds, List<int> ControlIds)
		{
			return this.LoadPerStudentDataForMultipleStudents("SELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',');\r\n\r\nSELECT    DISTINCT ps.infopcid AS personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,CAST(NULL AS varbinary(8000)) AS firstname,CAST(NULL AS varbinary(8000)) AS lastname,p.student_no,CAST(NULL AS varbinary(8000)) AS middlename,ps.uniqueid\r\nFROM        pcdata2 ps LEFT JOIN infopc p ON p.personid=ps.infopcid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.infopcid IN (SELECT personid AS infopcid FROM #t1)\r\n            AND ps.controlid IN (SELECT controlid FROM #t2)\r\nORDER BY ps.infopcid,ps.controlid\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2", PersonIds, ControlIds);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0003D858 File Offset: 0x0003BA58
		public int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@cid", DbType.Int32, cid),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, pid)
			};
			object obj = this.DatabaseManager.ExecuteScalar("SELECT COUNT(personid) AS ct FROM maininfops WHERE controlid=@cid AND controlvalue=@pid", parameters);
			bool flag = !(obj is int);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
		public List<DynamicDataSet> LoadPerStudentDataForMultipleStudents(List<int> PersonIds, List<int> ControlIds)
		{
			return this.LoadPerStudentDataForMultipleStudents("SELECT orderid AS personid INTO #t1 FROM splitorderids(@pids,',');\r\nSELECT orderid AS controlid INTO #t2 FROM splitorderids(@cids,',');\r\n\r\nSELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.uniqueid\r\nFROM        perstudentdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid IN (SELECT personid FROM #t1)\r\n            AND ps.controlid IN (SELECT controlid FROM #t2)\r\nORDER BY ps.personid,ps.controlid\r\n\r\nDROP TABLE #t1;\r\nDROP TABLE #t2", PersonIds, ControlIds);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
		public IList<PersonBase> LoadStudentByDataItem(eDynamicFormType FormType, DynamicField Field, object Value)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@valtext", DbType.String, DBNull.Value);
			DbParameter parameter2 = this.DatabaseManager.GetParameter("@valint", DbType.Int32, DBNull.Value);
			DbParameter parameter3 = this.DatabaseManager.GetParameter("@valbytes", DbType.Binary, DBNull.Value);
			DbParameter parameter4 = this.DatabaseManager.GetParameter("@valbytes2", DbType.Binary, DBNull.Value);
			DbParameter parameter5 = this.DatabaseManager.GetParameter("@valbytes3", DbType.Binary, DBNull.Value);
			DbParameter parameter6 = this.DatabaseManager.GetParameter("@valdate", DbType.DateTime, DBNull.Value);
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@cid", DbType.Int32, Field.ControlId),
				parameter,
				parameter2,
				parameter3,
				parameter4,
				parameter5,
				parameter6
			};
			if (FormType != eDynamicFormType.PerStudent && FormType != eDynamicFormType.PerStaff)
			{
				throw new NotImplementedException("Form type is not implemented for loadstudentbydataitem");
			}
			string query = "SELECT    ps2.personid,p.firstname,p.middlename,p.student_no,p.lastname\r\nFROM        perstudentdata2 ps2 LEFT JOIN people p ON p.personid=ps2.personid\r\nWHERE       ps2.controlid=@cid\r\n            AND p.isactive=1 --AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1 OR groupid=2 OR groupid=10)\r\n            AND \r\n            (\r\n                (NOT @valtext IS NULL AND ps2.valtext=@valtext)\r\n                OR\r\n                ((NOT @valbytes IS NULL AND ps2.valbytes=@valbytes) OR (NOT @valbytes2 IS NULL AND ps2.valbytes=@valbytes2) OR (NOT @valbytes3 IS NULL AND ps2.valbytes=@valbytes3))\r\n                OR\r\n                (NOT @valint IS NULL AND ps2.valint=@valint)\r\n                OR\r\n                (NOT @valdate IS NULL AND ps2.valdate=@valdate)\r\n            )";
			eControlCode controlCode = Field.ControlCode;
			eControlCode eControlCode = controlCode;
			if (eControlCode == eControlCode.TextBox)
			{
				bool flag = Field.Setting3 == 0;
				if (flag)
				{
					parameter.Value = (string)Value;
				}
				else
				{
					parameter3.Value = this.DatabaseManager.Encryption.Encrypt((string)Value);
					parameter4.Value = this.DatabaseManager.Encryption.Encrypt(((string)Value).ToLower());
					parameter5.Value = this.DatabaseManager.Encryption.Encrypt(((string)Value).ToUpper());
				}
			}
			IList<PersonBase> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(query, parameters))
			{
				bool flag2 = dataReader == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<PersonBase> list = new List<PersonBase>();
					PeopleDAO peopleDAO = new PeopleDAO(this.OpContext);
					while (dataReader.Read())
					{
						PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
						bool flag3 = personFromReader != null;
						if (flag3)
						{
							list.Add(personFromReader);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0003DB30 File Offset: 0x0003BD30
		public void CopyAllFormDataFromPerStudentToPerDate(int StudentPersonId, int ScreenNumPerStudent, int PerDateAppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNumPerStudent),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPersonId),
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, PerDateAppointmentId)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO maininfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM maininfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nINSERT INTO otherinfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM otherinfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nINSERT INTO imageinfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM imageinfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nINSERT INTO datetimeinfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM datetimeinfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);", parameters);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0003DBA8 File Offset: 0x0003BDA8
		public void MergeAllData(int PersonIdNew, int PersonIdOld)
		{
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM maininfops WHERE maininfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfops q WHERE q.personid=@newpid AND q.controlid=maininfops.controlid);\r\nUPDATE maininfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\n\r\nDELETE FROM otherinfops WHERE otherinfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfops q WHERE q.personid=@newpid AND q.controlid=otherinfops.controlid) AND NOT otherinfops.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\nUPDATE otherinfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\n\r\nDELETE FROM datetimeinfops WHERE datetimeinfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfops q WHERE q.personid=@newpid AND q.controlid=datetimeinfops.controlid);\r\nUPDATE datetimeinfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\n\r\nDELETE FROM imageinfops WHERE imageinfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfops q WHERE q.personid=@newpid AND q.controlid=imageinfops.controlid) AND NOT imageinfops.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\nUPDATE imageinfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld),
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew)
			});
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM maininfopa WHERE maininfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfopa q WHERE q.personid=@newpid AND q.appointmentid=maininfopa.appointmentid AND q.controlid=maininfopa.controlid);\r\nUPDATE maininfopa SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM otherinfopa WHERE otherinfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfopa q WHERE q.personid=@newpid AND q.appointmentid=otherinfopa.appointmentid AND q.controlid=otherinfopa.controlid);\r\nUPDATE otherinfopa SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM datetimeinfopa WHERE datetimeinfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfopa q WHERE q.personid=@newpid AND q.appointmentid=datetimeinfopa.appointmentid AND q.controlid=datetimeinfopa.controlid);\r\nUPDATE datetimeinfopa SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM imageinfopa WHERE imageinfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfopa q WHERE q.personid=@newpid AND q.appointmentid=imageinfopa.appointmentid AND q.controlid=imageinfopa.controlid);\r\nUPDATE imageinfopa SET personid=@newpid WHERE personid=@oldpid;", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld),
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew)
			});
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM maininfopm WHERE maininfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfopm q WHERE q.personid=@newpid AND q.appointmentid=maininfopm.appointmentid AND q.controlid=maininfopm.controlid);\r\nUPDATE maininfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM otherinfopm WHERE otherinfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfopm q WHERE q.personid=@newpid AND q.appointmentid=otherinfopm.appointmentid AND q.controlid=otherinfopm.controlid);\r\nUPDATE otherinfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM datetimeinfopm WHERE datetimeinfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfopm q WHERE q.personid=@newpid AND q.appointmentid=datetimeinfopm.appointmentid AND q.controlid=datetimeinfopm.controlid);\r\nUPDATE datetimeinfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM imageinfopm WHERE imageinfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfopm q WHERE q.personid=@newpid AND q.appointmentid=imageinfopm.appointmentid AND q.controlid=imageinfopm.controlid);\r\nUPDATE imageinfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nUPDATE infopm SET personid=@newpid WHERE personid=@oldpid;", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld),
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew)
			});
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM maininfoaccommodationps WHERE maininfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=maininfoaccommodationps.courseid AND q.controlid=maininfoaccommodationps.controlid);\r\nUPDATE maininfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM otherinfoaccommodationps WHERE otherinfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=otherinfoaccommodationps.courseid AND q.controlid=otherinfoaccommodationps.controlid);\r\nUPDATE otherinfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM datetimeinfoaccommodationps WHERE datetimeinfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=datetimeinfoaccommodationps.courseid AND q.controlid=datetimeinfoaccommodationps.controlid);\r\nUPDATE datetimeinfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM imageinfoaccommodationps WHERE imageinfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=imageinfoaccommodationps.courseid AND q.controlid=imageinfoaccommodationps.controlid);\r\nUPDATE imageinfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;", new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@oldpid", DbType.Int32, PersonIdOld),
				this.DatabaseManager.GetParameter("@newpid", DbType.Int32, PersonIdNew)
			});
			this.MergeFileLists(PersonIdNew, PersonIdOld);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0003DCF4 File Offset: 0x0003BEF4
		private void DeleteItem(int dataId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@dataid", DbType.Int32, dataId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM otherinfops WHERE dataid=@dataid", parameters);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0003DD38 File Offset: 0x0003BF38
		public bool DoesAtLeastOneSavedDataItemExist(DynamicDataContext Context, int ScreenNum, eDynamicFormType FormType)
		{
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(FormType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			bool flag = string.IsNullOrEmpty(dynamicDataItemQuery);
			if (flag)
			{
				throw new Exception("DoesAtLeastOneSavedDataItemExist:Sql is null");
			}
			bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
			bool flag3 = flag2;
			DbParameter[] parameters;
			if (flag3)
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId),
					this.DatabaseManager.GetParameter("@appid", DbType.Int32, Context.SecondaryId),
					this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0),
					this.DatabaseManager.GetParameter("@cids", DbType.String, ""),
					this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum)
				};
			}
			else
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId),
					this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0),
					this.DatabaseManager.GetParameter("@cids", DbType.String, ""),
					this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum)
				};
			}
			DataTable dataTable = this.DatabaseManager.ExecuteQuery(dynamicDataItemQuery, parameters);
			return dataTable.Rows.Count > 0;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0003DEBC File Offset: 0x0003C0BC
		public bool DoesAtLeastOneSavedDataItemExist(DynamicDataContext Context, IList<int> ControlIds, eDynamicFormType FormType)
		{
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(FormType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			bool flag = string.IsNullOrEmpty(dynamicDataItemQuery);
			if (flag)
			{
				throw new Exception("DoesAtLeastOneSavedDataItemExist:Sql is null");
			}
			bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
			bool flag3 = flag2;
			DbParameter[] parameters;
			if (flag3)
			{
				DbParameter[] array = new DbParameter[5];
				array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId);
				array[1] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, Context.SecondaryId);
				array[2] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
				array[3] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
				array[4] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, 0);
				parameters = array;
			}
			else
			{
				DbParameter[] array2 = new DbParameter[4];
				array2[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId);
				array2[1] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
				array2[2] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
				array2[3] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, 0);
				parameters = array2;
			}
			DataTable dataTable = this.DatabaseManager.ExecuteQuery(dynamicDataItemQuery, parameters);
			return dataTable.Rows.Count > 0;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0003E0AC File Offset: 0x0003C2AC
		public IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum),
				this.DatabaseManager.GetParameter("@personid", DbType.Int32, StudentPersonId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM appointmenticons \r\nWHERE   screennum=@screennum \r\n        AND appointmentid IN \r\n            (SELECT app.appointmentid \r\n             FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid \r\n             WHERE att.personid=@personid)", parameters);
			bool flag = ControlIdToActivate > 0;
			DataTable dataTable;
			if (flag)
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@personid", DbType.Int32, StudentPersonId),
					this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum),
					this.DatabaseManager.GetParameter("@cid", DbType.Int32, ControlIdToActivate)
				};
				dataTable = this.DatabaseManager.ExecuteQuery("SELECT a1.appointmentid FROM \r\n    (SELECT DISTINCT appointmentid FROM maininfopa WHERE controlid=@cid AND personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM otherinfopa WHERE controlid=@cid AND personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM datetimeinfopa WHERE controlid=@cid AND personid=@personid\r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM imageinfopa WHERE controlid=@cid AND personid=@personid) a1 \r\nWHERE NOT a1.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE screennum=@screennum)", parameters);
			}
			else
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@personid", DbType.Int32, StudentPersonId),
					this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum)
				};
				dataTable = this.DatabaseManager.ExecuteQuery("SELECT a1.appointmentid FROM \r\n    (SELECT DISTINCT appointmentid FROM maininfopa WHERE personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM otherinfopa WHERE personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM datetimeinfopa WHERE personid=@personid\r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM imageinfopa WHERE personid=@personid) a1 \r\nWHERE NOT a1.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE screennum=@screennum)", parameters);
			}
			List<int> list = new List<int>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag2 = IconId >= 0;
				if (flag2)
				{
					int num = (int)dataRow[0];
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@screennum", DbType.Int32, ScreenNum),
						this.DatabaseManager.GetParameter("@appointmentid", DbType.Int32, num),
						this.DatabaseManager.GetParameter("@iconnum", DbType.Int32, IconId)
					};
					this.DatabaseManager.ExecuteNonQuery("INSERT INTO appointmenticons (appointmentid,screennum,iconnum) VALUES (@appointmentid,@screennum,@iconnum)", parameters);
					list.Add(num);
				}
			}
			return list;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0003E2CC File Offset: 0x0003C4CC
		public IList<DynamicDataStorageItem> LoadDynamicDataStorageItemsByForm(DynamicDataContext Context, int FormNum, eDynamicFormType FormType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(FormType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			bool flag = string.IsNullOrEmpty(dynamicDataItemQuery);
			IList<DynamicDataStorageItem> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
				bool flag3 = flag2;
				DbParameter[] parameters;
				if (flag3)
				{
					parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, Context.PrimaryId),
						databaseLayer.GetParameter("@appid", DbType.Int32, Context.SecondaryId),
						databaseLayer.GetParameter("@cid", DbType.Int32, 0),
						databaseLayer.GetParameter("@cids", DbType.String, ""),
						databaseLayer.GetParameter("@screennum", DbType.Int32, FormNum)
					};
				}
				else
				{
					parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@pid", DbType.Int32, Context.PrimaryId),
						databaseLayer.GetParameter("@cid", DbType.Int32, 0),
						databaseLayer.GetParameter("@cids", DbType.String, ""),
						databaseLayer.GetParameter("@screennum", DbType.Int32, FormNum)
					};
				}
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(dynamicDataItemQuery, parameters))
				{
					bool flag4 = dataReader != null;
					if (flag4)
					{
						return DynamicDataDAO.GetDynamicDataStorageItemListFromRecords(dataReader, this.OpContext);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0003E464 File Offset: 0x0003C664
		public IList<PersonBase> LoadUniqueStudentsWithPerStudentDataEnteredByForm(int ScreenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			IList<PersonBase> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    DISTINCT pd.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        perstudentdata2 pd LEFT JOIN people p ON p.personid=pd.personid\r\nWHERE       pd.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)\r\n            AND p.isactive=1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PersonBase> list = new List<PersonBase>();
					while (dataReader.Read())
					{
						PersonBase student = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, null);
						bool flag2 = student != null && student.PersonId > 0 && list.Find((PersonBase s) => s.PersonId == student.PersonId) == null;
						if (flag2)
						{
							list.Add(student);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0003E568 File Offset: 0x0003C768
		public IList<DynamicDataStorageItem> LoadDynamicDataItemsByControlIds(DynamicDataContext Context, IList<int> ControlIds, eDynamicFormType FormType)
		{
			string dynamicDataItemQuery = DynamicDataDAO.GetDynamicDataItemQuery(FormType, eDynamicDataStorageLocation.MainInfo, eQueryType.Load);
			bool flag = string.IsNullOrEmpty(dynamicDataItemQuery);
			IList<DynamicDataStorageItem> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = dynamicDataItemQuery.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
				bool flag3 = flag2;
				DbParameter[] parameters;
				if (flag3)
				{
					DbParameter[] array = new DbParameter[5];
					array[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId);
					array[1] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, Context.SecondaryId);
					array[2] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
					array[3] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
					array[4] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, 0);
					parameters = array;
				}
				else
				{
					DbParameter[] array2 = new DbParameter[4];
					array2[0] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, Context.PrimaryId);
					array2[1] = this.DatabaseManager.GetParameter("@cid", DbType.Int32, 0);
					array2[2] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray()));
					array2[3] = this.DatabaseManager.GetParameter("@screennum", DbType.Int32, 0);
					parameters = array2;
				}
				using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(dynamicDataItemQuery, parameters))
				{
					bool flag4 = dataReader != null;
					if (flag4)
					{
						List<DynamicDataStorageItem> dynamicDataStorageItemListFromRecords = DynamicDataDAO.GetDynamicDataStorageItemListFromRecords(dataReader, this.OpContext);
						dynamicDataStorageItemListFromRecords.Sort((DynamicDataStorageItem g1, DynamicDataStorageItem g2) => ControlIds.IndexOf(g1.Field.ControlId).CompareTo(ControlIds.IndexOf(g2.Field.ControlId)));
						return dynamicDataStorageItemListFromRecords;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0003E7B4 File Offset: 0x0003C9B4
		private DbParameter[] GetDbParametersForSavingDynamicDataStorageItem(string sql, DynamicDataStorageItem storageItem, DynamicDataContext context, DbParameter valueParameter)
		{
			bool flag = sql.IndexOf("@appid", StringComparison.OrdinalIgnoreCase) >= 0;
			List<DbParameter> list = new List<DbParameter>();
			list.Add(this.DatabaseManager.GetParameter("@pid", DbType.Int32, context.PrimaryId));
			list.Add(this.DatabaseManager.GetParameter("@cid", DbType.Int32, storageItem.Field.ControlId));
			list.Add(this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI));
			bool flag2 = flag;
			if (flag2)
			{
				list.Add(this.DatabaseManager.GetParameter("@appid", DbType.Int32, context.SecondaryId));
			}
			bool flag3 = valueParameter != null;
			if (flag3)
			{
				list.Add(valueParameter);
			}
			return list.ToArray();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0003E898 File Offset: 0x0003CA98
		private void GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType queryType, eDynamicFormType formType, DynamicDataContext context, DynamicDataStorageItem storageItem, DbParameter valParameter, eDynamicDataStorageLocation storageLocation, out string sql, out DbParameter[] parameters)
		{
			sql = DynamicDataDAO.GetDynamicDataItemQuery(formType, storageLocation, queryType);
			parameters = this.GetDbParametersForSavingDynamicDataStorageItem(sql, storageItem, context, valParameter);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0003E8B8 File Offset: 0x0003CAB8
		private bool ShouldSkipSavingDataType(DynamicDataStorageItem storageItem, eDynamicDataStorageLocation storageLocation)
		{
			eControlCode controlCode = storageItem.Field.ControlCode;
			eControlCode eControlCode = controlCode;
			eControlCode eControlCode2 = eControlCode;
			if (eControlCode2 == eControlCode.DropList || eControlCode2 == eControlCode.AccommodationDropList)
			{
				bool flag = storageLocation == eDynamicDataStorageLocation.MainInfo && storageItem.Field.Setting3 != 0;
				if (flag)
				{
					return true;
				}
				bool flag2 = storageLocation == eDynamicDataStorageLocation.OtherInfo && storageItem.Field.Setting3 == 0;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0003E930 File Offset: 0x0003CB30
		public IList<BasicPerson> LoadAssignedAdvisorsFromPerStudentForm(int studentPersonId, int[] cids)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string value = string.Join(",", from g in (from c in cids
			where c > 0
			select c).Distinct<int>()
			select g.ToString());
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@cids", DbType.String, value)
			};
			IList<BasicPerson> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS controlid INTO #tcids FROM splitorderids(@cids,',')\r\nSELECT DISTINCT m.controlvalue AS personid,p.firstname,p.middlename,p.lastname,p.student_no \r\nFROM maininfops m LEFT JOIN people p ON p.personid=m.controlvalue WHERE m.personid= @pid AND m.controlid IN (SELECT controlid FROM #tcids)\r\nDROP TABLE #tcids", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<BasicPerson> list = new List<BasicPerson>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						BasicPerson p = PeopleDAO.GetBasicPersonFromRecord("", dataReader, batchDecryptor);
						bool flag2 = p == null || list.Any((BasicPerson g) => g.PersonId == p.PersonId);
						if (!flag2)
						{
							list.Add(p);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0003EA98 File Offset: 0x0003CC98
		public void SaveDynamicDataStorageItems(DynamicDataContext Context, IList<DynamicDataStorageItem> StorageItems, eDynamicFormType FormType)
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			foreach (DynamicDataStorageItem dynamicDataStorageItem in StorageItems)
			{
				DbParameter[] array = null;
				string text = null;
				DynamicField field = dynamicDataStorageItem.Field;
				eDynamicDataStorageType storageType = field.StorageType;
				DynamicControlAttribute attribute = field.ControlCode.GetAttribute();
				bool flag = attribute != null;
				if (flag)
				{
					bool flag2 = (attribute.StorageLocation & eDynamicDataStorageLocation.MainInfo) > eDynamicDataStorageLocation.Unknown && !this.ShouldSkipSavingDataType(dynamicDataStorageItem, attribute.StorageLocation);
					if (flag2)
					{
						bool flag3 = dynamicDataStorageItem.IntValue != null;
						if (flag3)
						{
							DbParameter parameter = this.DatabaseManager.GetParameter("@val", DbType.Int32, dynamicDataStorageItem.IntValue.Value);
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.InsertOrUpdate, FormType, Context, dynamicDataStorageItem, parameter, eDynamicDataStorageLocation.MainInfo, out text, out array);
						}
						else
						{
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.Delete, FormType, Context, dynamicDataStorageItem, null, eDynamicDataStorageLocation.MainInfo, out text, out array);
						}
						bool flag4 = !string.IsNullOrEmpty(text) && array != null;
						if (flag4)
						{
							this.DatabaseManager.ExecuteNonQuery(text, array);
						}
					}
					bool flag5 = (attribute.StorageLocation & eDynamicDataStorageLocation.DateTimeInfo) > eDynamicDataStorageLocation.Unknown && !this.ShouldSkipSavingDataType(dynamicDataStorageItem, attribute.StorageLocation);
					if (flag5)
					{
						bool flag6 = dynamicDataStorageItem.DateTimeValue != null;
						if (flag6)
						{
							DbParameter parameter = this.DatabaseManager.GetParameter("@val", DbType.DateTime, dynamicDataStorageItem.DateTimeValue.Value);
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.InsertOrUpdate, FormType, Context, dynamicDataStorageItem, parameter, eDynamicDataStorageLocation.DateTimeInfo, out text, out array);
						}
						else
						{
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.Delete, FormType, Context, dynamicDataStorageItem, null, eDynamicDataStorageLocation.DateTimeInfo, out text, out array);
						}
						bool flag7 = !string.IsNullOrEmpty(text) && array != null;
						if (flag7)
						{
							this.DatabaseManager.ExecuteNonQuery(text, array);
						}
					}
					bool flag8 = (attribute.StorageLocation & eDynamicDataStorageLocation.OtherInfo) > eDynamicDataStorageLocation.Unknown && !this.ShouldSkipSavingDataType(dynamicDataStorageItem, attribute.StorageLocation);
					if (flag8)
					{
						bool flag9 = string.IsNullOrEmpty(dynamicDataStorageItem.OtherValue);
						byte[] array2;
						if (flag9)
						{
							array2 = null;
						}
						else
						{
							array2 = (((storageType & eDynamicDataStorageType.Encrypted) > eDynamicDataStorageType.None) ? this.DatabaseManager.Encryption.Encrypt(dynamicDataStorageItem.OtherValue) : utf8Encoding.GetBytes(dynamicDataStorageItem.OtherValue));
						}
						bool flag10 = array2 != null && array2.Length != 0;
						if (flag10)
						{
							DbParameter parameter = this.DatabaseManager.GetParameter("@val", DbType.Binary, array2);
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.InsertOrUpdate, FormType, Context, dynamicDataStorageItem, parameter, eDynamicDataStorageLocation.OtherInfo, out text, out array);
						}
						else
						{
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.Delete, FormType, Context, dynamicDataStorageItem, null, eDynamicDataStorageLocation.OtherInfo, out text, out array);
						}
						bool flag11 = !string.IsNullOrEmpty(text) && array != null;
						if (flag11)
						{
							this.DatabaseManager.ExecuteNonQuery(text, array);
						}
					}
					bool flag12 = (attribute.StorageLocation & eDynamicDataStorageLocation.ImageInfo) > eDynamicDataStorageLocation.Unknown && !this.ShouldSkipSavingDataType(dynamicDataStorageItem, attribute.StorageLocation);
					if (flag12)
					{
						bool flag13 = dynamicDataStorageItem.ImageValue == null || dynamicDataStorageItem.ImageValue.Length < 1;
						byte[] array3;
						if (flag13)
						{
							array3 = null;
						}
						else
						{
							array3 = (((storageType & eDynamicDataStorageType.Encrypted) > eDynamicDataStorageType.None) ? this.DatabaseManager.Encryption.Encrypt(utf8Encoding.GetString(dynamicDataStorageItem.ImageValue)) : dynamicDataStorageItem.ImageValue);
						}
						bool flag14 = array3 != null && array3.Length != 0;
						if (flag14)
						{
							DbParameter parameter = this.DatabaseManager.GetParameter("@val", DbType.Binary, array3);
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.InsertOrUpdate, FormType, Context, dynamicDataStorageItem, parameter, eDynamicDataStorageLocation.ImageInfo, out text, out array);
						}
						else
						{
							this.GetSqlAndParametersForSavingDynamicDataStorageItem(eQueryType.Delete, FormType, Context, dynamicDataStorageItem, null, eDynamicDataStorageLocation.ImageInfo, out text, out array);
						}
						bool flag15 = !string.IsNullOrEmpty(text) && array != null;
						if (flag15)
						{
							this.DatabaseManager.ExecuteNonQuery(text, array);
						}
					}
				}
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0003EE60 File Offset: 0x0003D060
		public BinaryFile LoadFileFromImageInfo(string imageInfoTableName, int dataId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@dataid", DbType.Int32, dataId)
			};
			string query = "SELECT controlvalue FROM " + imageInfoTableName + " WHERE dataid=@dataid";
			BinaryFile result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(query, parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					byte[] array = (dataReader["controlvalue"] is DBNull) ? null : ((byte[])dataReader["controlvalue"]);
					bool flag2 = array == null || array.Length < 1;
					if (flag2)
					{
						result = null;
					}
					else
					{
						string fileName;
						byte[] array2 = array.ParseSingleFileBytes(out fileName);
						bool flag3 = array2 == null;
						if (flag3)
						{
							result = null;
						}
						else
						{
							result = new BinaryFile
							{
								ByteArray = array2,
								FileName = fileName
							};
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0003EF70 File Offset: 0x0003D170
		[DebuggerStepThrough]
		public Task<BinaryFile> LoadFileFromImageInfoAsync(string imageInfoTableName, int dataId)
		{
			DynamicDataDAO.<LoadFileFromImageInfoAsync>d__70 <LoadFileFromImageInfoAsync>d__ = new DynamicDataDAO.<LoadFileFromImageInfoAsync>d__70();
			<LoadFileFromImageInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<LoadFileFromImageInfoAsync>d__.<>4__this = this;
			<LoadFileFromImageInfoAsync>d__.imageInfoTableName = imageInfoTableName;
			<LoadFileFromImageInfoAsync>d__.dataId = dataId;
			<LoadFileFromImageInfoAsync>d__.<>1__state = -1;
			<LoadFileFromImageInfoAsync>d__.<>t__builder.Start<DynamicDataDAO.<LoadFileFromImageInfoAsync>d__70>(ref <LoadFileFromImageInfoAsync>d__);
			return <LoadFileFromImageInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0003EFC4 File Offset: 0x0003D1C4
		public IDictionary<int, int[]> LoadAllPersonIdsAndControlIdsWithDataForPerStudentData(params int[] ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@cids";
			DbType pType = DbType.String;
			object value;
			if (ControlIds != null && ControlIds.Length >= 1)
			{
				value = string.Join(",", (from g in ControlIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = DBNull.Value;
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			IDictionary<int, int[]> personIdsAndControlIdsListFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS controlid INTO #t1 FROM splitorderids(COALESCE(@cids,'0'),',')\r\n\r\nSELECT personid,controlid FROM perstudentdata2 WHERE @cids IS NULL OR controlid IN (SELECT controlid FROM #t1)\r\nORDER BY personid,controlid\r\n\r\nDROP TABLE #t1", parameters))
			{
				personIdsAndControlIdsListFromReader = this.GetPersonIdsAndControlIdsListFromReader(dataReader);
			}
			return personIdsAndControlIdsListFromReader;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0003F07C File Offset: 0x0003D27C
		private IDictionary<int, int[]> GetPersonIdsAndControlIdsListFromReader(IDataReader reader)
		{
			Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>();
			bool flag = reader == null;
			IDictionary<int, int[]> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				int num = 0;
				List<int> list = new List<int>();
				while (reader.Read())
				{
					int num2 = (reader["personid"] is DBNull) ? 0 : ((int)reader["personid"]);
					int num3 = (reader["controlid"] is DBNull) ? 0 : ((int)reader["controlid"]);
					bool flag2 = num2 < 1 || num3 < 1;
					if (!flag2)
					{
						bool flag3 = num2 != num;
						if (flag3)
						{
							bool flag4 = num > 0 && list.Count > 0;
							if (flag4)
							{
								dictionary.Add(num, list.ToArray());
							}
							num = num2;
							list = new List<int>();
						}
						list.Add(num3);
					}
				}
				bool flag5 = num > 0 && list.Count > 0;
				if (flag5)
				{
					dictionary.Add(num, list.ToArray());
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0003F194 File Offset: 0x0003D394
		public IDictionary<int, int[]> LoadAllPersonIdsAndControlIdsWithDataForTemplateOnlyAccommodations(params int[] ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@cids";
			DbType pType = DbType.String;
			object value;
			if (ControlIds != null && ControlIds.Length >= 1)
			{
				value = string.Join(",", (from g in ControlIds
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = DBNull.Value;
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			IDictionary<int, int[]> personIdsAndControlIdsListFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT orderid AS controlid INTO #t1 FROM splitorderids(COALESCE(@cids,'0'),',')\r\n\r\nSELECT personid,controlid FROM accommodationdata WHERE courseid=0 AND @cids IS NULL OR controlid IN (SELECT controlid FROM #t1)\r\nORDER BY personid,controlid\r\n\r\nDROP TABLE #t1", parameters))
			{
				personIdsAndControlIdsListFromReader = this.GetPersonIdsAndControlIdsListFromReader(dataReader);
			}
			return personIdsAndControlIdsListFromReader;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0003F24C File Offset: 0x0003D44C
		public IList<Pair<PersonBase, PersonBase>> SwapAssignedAdvisors(int ControlId, int OldAdvisorPid, int NewAdvisorPid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, ControlId),
				databaseLayer.GetParameter("@oldpid", DbType.Int32, OldAdvisorPid)
			};
			Dictionary<int, Pair<PersonBase, PersonBase>> dictionary = new Dictionary<int, Pair<PersonBase, PersonBase>>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT m.dataid,m.personid,m.controlid,m.controlvalue,\r\n         p1.firstname,p1.middlename,p1.lastname,p1.student_no,\r\n         p2.firstname AS advisor_firstname,p2.middlename AS advisor_middlename,p2.lastname AS advisor_lastname,p2.student_no AS advisor_student_no\r\nFROM     maininfops m LEFT JOIN people p1 ON p1.personid=m.personid\r\n         LEFT JOIN people p2 ON p2.personid=m.controlvalue\r\nWHERE    m.controlid=@cid AND m.controlvalue=@oldpid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", dataReader, this.OpContext, batchDecryptor);
						PersonBase personFromReader2 = PeopleDAO.GetPersonFromReader("advisor_", dataReader, this.OpContext, batchDecryptor);
						dictionary.Add((int)dataReader["dataid"], new Pair<PersonBase, PersonBase>(personFromReader, personFromReader2));
					}
				}
			}
			List<Pair<PersonBase, PersonBase>> list = new List<Pair<PersonBase, PersonBase>>();
			foreach (KeyValuePair<int, Pair<PersonBase, PersonBase>> keyValuePair in dictionary)
			{
				int key = keyValuePair.Key;
				Pair<PersonBase, PersonBase> value = keyValuePair.Value;
				parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@dataid", DbType.Int32, key),
					databaseLayer.GetParameter("@pid", DbType.Int32, value.Item1.PersonId),
					databaseLayer.GetParameter("@cid", DbType.Int32, ControlId),
					databaseLayer.GetParameter("@oldval", DbType.Int32, OldAdvisorPid),
					databaseLayer.GetParameter("@newval", DbType.Int32, NewAdvisorPid)
				};
				databaseLayer.ExecuteNonQuery("UPDATE maininfops SET controlvalue=@newval WHERE dataid=@dataid AND personid=@pid AND controlid=@cid AND controlvalue=@oldval", parameters);
				list.Add(value);
			}
			return list;
		}

		// Token: 0x04000305 RID: 773
		private DatabaseLayer DatabaseManager;

		// Token: 0x04000306 RID: 774
		private DynamicFieldDAO _dynamicFieldDao;

		// Token: 0x04000307 RID: 775
		public IList<PersonBase> staffMembersForLookup;
	}
}

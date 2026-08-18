using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000DD RID: 221
	public class DynamicFieldDAO : IDynamicFieldDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00040220 File Offset: 0x0003E420
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00040228 File Offset: 0x0003E428
		public OperationContext OpContext { get; set; }

		// Token: 0x0600065E RID: 1630 RVA: 0x00040231 File Offset: 0x0003E431
		public DynamicFieldDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00040244 File Offset: 0x0003E444
		private DynamicListItem GetListItemFromRecord(IDataReader record)
		{
			bool flag = record == null;
			DynamicListItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DynamicListItem
				{
					Children = record["children"].ToString(),
					LookupListId = (int)record["lookuplistid"],
					LookupText = record["lookuptext"].ToString(),
					LookupValue = record["lookupvalue"].ToString(),
					OrderNum = ((record["ordernum"] == DBNull.Value) ? 0 : ((int)record["ordernum"])),
					Group = this.GetListGroupFromRecord(record)
				};
			}
			return result;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00040300 File Offset: 0x0003E500
		private DynamicListGroup GetListGroupFromRecord(IDataReader record)
		{
			bool flag = record == null;
			DynamicListGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DynamicListGroup
				{
					LookupGroupId = ((record["lookupgroupid"] == DBNull.Value) ? 0 : ((int)record["lookupgroupid"])),
					ChildList = record["childlist"].ToString(),
					Description = record["description"].ToString(),
					SortBy = ((record["sortby"] == DBNull.Value) ? 0 : ((int)record["sortby"]))
				};
			}
			return result;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000403AC File Offset: 0x0003E5AC
		private List<DynamicField> GetFieldsFromRecords(IDataReader reader)
		{
			bool flag = reader != null;
			List<DynamicField> result;
			if (flag)
			{
				List<DynamicField> list = new List<DynamicField>();
				while (reader.Read())
				{
					DynamicField fieldFromRecord = DynamicFieldDAO.GetFieldFromRecord(reader);
					bool flag2 = fieldFromRecord != null;
					if (flag2)
					{
						list.Add(fieldFromRecord);
					}
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00040400 File Offset: 0x0003E600
		[DebuggerStepThrough]
		private Task<List<DynamicField>> GetFieldsFromRecordsAsync(DbDataReader reader)
		{
			DynamicFieldDAO.<GetFieldsFromRecordsAsync>d__8 <GetFieldsFromRecordsAsync>d__ = new DynamicFieldDAO.<GetFieldsFromRecordsAsync>d__8();
			<GetFieldsFromRecordsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicField>>.Create();
			<GetFieldsFromRecordsAsync>d__.<>4__this = this;
			<GetFieldsFromRecordsAsync>d__.reader = reader;
			<GetFieldsFromRecordsAsync>d__.<>1__state = -1;
			<GetFieldsFromRecordsAsync>d__.<>t__builder.Start<DynamicFieldDAO.<GetFieldsFromRecordsAsync>d__8>(ref <GetFieldsFromRecordsAsync>d__);
			return <GetFieldsFromRecordsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0004044C File Offset: 0x0003E64C
		public static DynamicField GetFieldFromRecord(IDataReader reader)
		{
			try
			{
				object obj = reader["controlid"];
				int num = (obj == DBNull.Value) ? 0 : ((int)obj);
				bool flag = num > 0;
				if (flag)
				{
					object obj2 = reader["controlcaption"];
					string originalCaption = "";
					bool flag2 = obj2 != DBNull.Value;
					string text;
					if (flag2)
					{
						text = (string)obj2;
						originalCaption = text;
						int num2 = text.IndexOf("~~");
						bool flag3 = num2 > 0;
						if (flag3)
						{
							text = text.Substring(0, num2);
						}
					}
					else
					{
						text = "";
					}
					string uniqueId = (DynamicFieldDAO.ReaderContainsColumn(reader, "uniqueid") && !(reader["uniqueid"] is DBNull)) ? ((Guid)reader["uniqueid"]).ToString().Trim() : "";
					int setting = (reader["setting1"] is DBNull) ? 0 : ((int)reader["setting1"]);
					int setting2 = (reader["setting2"] is DBNull) ? 0 : ((int)reader["setting2"]);
					int setting3 = (reader["setting3"] is DBNull) ? 0 : ((int)reader["setting3"]);
					int setting4 = (reader["setting4"] is DBNull) ? 0 : ((int)reader["setting4"]);
					string setting4String = DynamicFieldDAO.ReaderContainsColumn(reader, "setting4string") ? reader["setting4string"].ToString() : "";
					bool flag4 = DynamicFieldDAO.ReaderContainsColumn(reader, "defaultvalue") && reader["defaultvalue"] != DBNull.Value;
					int defaultValue;
					string defaultValueString;
					string mask;
					if (flag4)
					{
						defaultValue = (int)reader["defaultvalue"];
						defaultValueString = (DynamicFieldDAO.ReaderContainsColumn(reader, "defaultvaluestring") ? reader["defaultvaluestring"].ToString() : "");
						mask = (DynamicFieldDAO.ReaderContainsColumn(reader, "mask") ? reader["mask"].ToString() : "");
					}
					else
					{
						defaultValue = 0;
						defaultValueString = "";
						mask = "";
					}
					Dictionary<string, string> args = new Dictionary<string, string>();
					bool flag5 = DynamicFieldDAO.ReaderContainsColumn(reader, "controlgroup");
					if (flag5)
					{
						args = (from q in (from g in reader["controlgroup"].ToString().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
						select g.Trim() into h
						where h.Length > 0
						select h).Select(delegate(string m)
						{
							int num4 = m.IndexOf('=');
							return (num4 > 0) ? new Pair<string, string>(m.Substring(0, num4), (num4 < m.Length - 1) ? m.Substring(num4 + 1) : "") : new Pair<string, string>(m, "");
						})
						group q by q.Item1).ToDictionary((IGrouping<string, Pair<string, string>> g) => g.Key, delegate(IGrouping<string, Pair<string, string>> g)
						{
							Pair<string, string> pair = g.First<Pair<string, string>>();
							return (pair == null) ? "" : pair.Item2;
						});
					}
					object obj3 = reader["controlcode"];
					bool flag6 = obj3 == DBNull.Value;
					eControlCode controlCode;
					if (flag6)
					{
						controlCode = eControlCode.Unknown;
					}
					else
					{
						int num3 = (int)obj3;
						bool flag7 = Enum.IsDefined(typeof(eControlCode), num3);
						if (flag7)
						{
							controlCode = (eControlCode)num3;
						}
						else
						{
							controlCode = eControlCode.Unknown;
						}
					}
					return new DynamicField
					{
						UniqueId = uniqueId,
						ControlId = num,
						ControlCaption = text,
						ControlCode = controlCode,
						Args = args,
						Setting1 = setting,
						Setting2 = setting2,
						Setting3 = setting3,
						Setting4 = setting4,
						Setting4String = setting4String,
						DefaultValue = defaultValue,
						DefaultValueString = defaultValueString,
						Mask = mask,
						OriginalCaption = originalCaption,
						OrderNum = (DynamicFieldDAO.ReaderContainsColumn(reader, "ordernum") ? ((reader["ordernum"] is DBNull) ? 0 : ((int)reader["ordernum"])) : 0),
						IsReadOnly = (DynamicFieldDAO.ReaderContainsColumn(reader, "readonly") && !(reader["readonly"] is DBNull) && Convert.ToBoolean(reader["readonly"])),
						ControlName = (DynamicFieldDAO.ReaderContainsColumn(reader, "controlname") ? reader["controlname"].ToString().Trim() : ""),
						SpecialControlType = ((DynamicFieldDAO.ReaderContainsColumn(reader, "specialcontroltype") && !(reader["specialcontroltype"] is DBNull) && Enum.IsDefined(typeof(eSpecialControlType), (int)reader["specialcontroltype"])) ? ((eSpecialControlType)Enum.Parse(typeof(eSpecialControlType), reader["specialcontroltype"].ToString())) : eSpecialControlType.Unknown)
					};
				}
			}
			finally
			{
			}
			return null;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000409E0 File Offset: 0x0003EBE0
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

		// Token: 0x06000665 RID: 1637 RVA: 0x00040A20 File Offset: 0x0003EC20
		public List<DynamicField> LoadFields(int screenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    dsc.controlid,dsc.ordernum,dsc.screennum\r\n            ,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,COALESCE(dsc.controlgroup,dc.controlgroup) AS controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,s.description,s.typecode,dc.uniqueid,dc.specialcontroltype\r\nFROM        screens s LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=s.screennum\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid\r\nWHERE       s.screennum=@screennum\r\nORDER BY    dsc.ordernum,dc.controlcaption", new DbParameter[]
			{
				databaseLayer.GetParameter("@screennum", DbType.Int32, screenNum)
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetFieldsFromRecords(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		public DynamicField LoadFieldByUniqueId(Guid uniqueId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Guid, uniqueId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    dc.controlid,0 AS ordernum,0 AS screennum\r\n            ,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,'' AS description,0 AS typecode,dc.uniqueid,dc.specialcontroltype\r\nFROM        dynamiccontrols dc \r\nWHERE       dc.uniqueid=@id\r\nORDER BY    dc.controlcaption", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return DynamicFieldDAO.GetFieldFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00040B44 File Offset: 0x0003ED44
		public List<DynamicField> LoadFieldsByControlIds(List<int> ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			array[0] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    dc.controlid,0 AS screennum\r\n            ,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.[enabled],dc.[readonly],dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,'' AS [description],0 AS typecode,\r\n\t\t\tMAX(dsc.ordernum) AS ordernum,dc.uniqueid,dc.specialcontroltype\r\nFROM        dynamiccontrols dc LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=dc.controlid\r\nWHERE       dc.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))\r\nGROUP BY\tdc.controlid,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.[enabled],dc.[readonly],dc.hidecaption,dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nORDER BY    dc.controlcaption", array))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					return this.GetFieldsFromRecords(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00040C04 File Offset: 0x0003EE04
		[DebuggerStepThrough]
		public Task<List<DynamicField>> LoadFieldsByControlIdsAsync(List<int> ControlIds)
		{
			DynamicFieldDAO.<LoadFieldsByControlIdsAsync>d__14 <LoadFieldsByControlIdsAsync>d__ = new DynamicFieldDAO.<LoadFieldsByControlIdsAsync>d__14();
			<LoadFieldsByControlIdsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicField>>.Create();
			<LoadFieldsByControlIdsAsync>d__.<>4__this = this;
			<LoadFieldsByControlIdsAsync>d__.ControlIds = ControlIds;
			<LoadFieldsByControlIdsAsync>d__.<>1__state = -1;
			<LoadFieldsByControlIdsAsync>d__.<>t__builder.Start<DynamicFieldDAO.<LoadFieldsByControlIdsAsync>d__14>(ref <LoadFieldsByControlIdsAsync>d__);
			return <LoadFieldsByControlIdsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00040C50 File Offset: 0x0003EE50
		public DynamicField SearchForField(string ControlCaption, int ScreenNum)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@caption", DbType.String, ControlCaption),
				databaseLayer.GetParameter("@screennum", DbType.Int32, ScreenNum)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    dc.controlid,0 AS ordernum,0 AS screennum\r\n            ,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,'' AS description,0 AS typecode,dc.uniqueid,dc.specialcontroltype\r\nFROM        dynamiccontrols dc \r\nWHERE       dc.controlcaption=@caption\r\n            AND dc.enabled=1\r\n            AND (\r\n                @screennum=0 OR \r\n                dc.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum AND isactive=1)\r\n            )\r\nORDER BY dc.controlid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return DynamicFieldDAO.GetFieldFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00040CF4 File Offset: 0x0003EEF4
		public int CreateFieldOnForm(DynamicFieldOnForm FieldOnForm)
		{
			bool flag = FieldOnForm.ControlId < 1 || FieldOnForm.ScreenNum < 1;
			int result;
			if (flag)
			{
				CWLogger.Logger.Error("DynamicFieldDAO:CreateFieldOnForm:InvalidControlIdOrScreenNum:cid={0}:screennum={1}", FieldOnForm.ControlId.ToString(), FieldOnForm.ScreenNum.ToString());
				result = 0;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@dynamicscreencontrolid", DbType.Int32, 0),
					databaseLayer.GetParameter("@controlid", DbType.Int32, FieldOnForm.ControlId),
					databaseLayer.GetParameter("@screennum", DbType.Int32, FieldOnForm.ScreenNum),
					databaseLayer.GetParameter("@ordernum", DbType.Int32, FieldOnForm.OrderNum)
				};
				databaseLayer.ExecuteNonQuery("SET @dynamicscreencontrolid=(SELECT TOP 1 dynamicscreencontrolid FROM dynamicscreencontrols WHERE screennum=@screennum AND controlid=@controlid);\r\nIF NOT ( NOT @dynamicscreencontrolid IS NULL AND @dynamicscreencontrolid > 0 )\r\nBEGIN\r\n    INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive) VALUES (@screennum,@controlid,@ordernum,1);\r\n    SET @dynamicscreencontrolid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))\r\nEND", array);
				result = ((array[0].Value != null) ? ((int)array[0].Value) : 0);
			}
			return result;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00040E00 File Offset: 0x0003F000
		public int CreateField(DynamicField Field)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@controlname", DbType.String, Field.ControlName ?? ""),
				databaseLayer.GetParameter("@controlcaption", DbType.String, Field.ControlCaption ?? ""),
				databaseLayer.GetParameter("@controlcode", DbType.Int32, Field.ControlCode),
				databaseLayer.GetParameter("@setting1", DbType.Int32, Field.Setting1),
				databaseLayer.GetParameter("@setting2", DbType.Int32, Field.Setting2),
				databaseLayer.GetParameter("@setting3", DbType.Int32, Field.Setting3),
				databaseLayer.GetParameter("@setting4", DbType.Int32, Field.Setting4),
				databaseLayer.GetParameter("@defaultvalue", DbType.Int32, Field.DefaultValue),
				databaseLayer.GetParameter("@setting4string", DbType.String, Field.Setting4String ?? ""),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, Field.IsActive),
				databaseLayer.GetParameter("@enabled", DbType.Boolean, true),
				databaseLayer.GetParameter("@readonly", DbType.Boolean, false),
				databaseLayer.GetParameter("@defaultvaluestring", DbType.String, Field.DefaultValueString ?? ""),
				databaseLayer.GetParameter("@enforce", DbType.Int32, (int)Field.EnforceMethod),
				databaseLayer.GetParameter("@hidecaption", DbType.Boolean, Field.HideCaption),
				databaseLayer.GetParameter("@dontwraptonextline", DbType.Boolean, Field.DontWrapToNextLine),
				databaseLayer.GetParameter("@mask", DbType.String, Field.Mask ?? ""),
				databaseLayer.GetParameter("@controlgroup", DbType.String, ""),
				databaseLayer.GetParameter("@helptext", DbType.String, ""),
				databaseLayer.GetParameter("@helptextdisplaymethod", DbType.Int32, 0),
				databaseLayer.GetParameter("@actionhandlers", DbType.String, ""),
				databaseLayer.GetParameter("@fontsize", DbType.Int32, 0),
				databaseLayer.GetParameter("@uniqueid", DbType.Guid, string.IsNullOrEmpty(Field.UniqueId) ? Guid.NewGuid() : new Guid(Field.UniqueId)),
				databaseLayer.GetParameter("@specialcontroltype", DbType.Int32, (int)Field.SpecialControlType)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("INSERT INTO dynamiccontrols (controlcode,controlcaption,setting1,setting2,setting3,setting4,\r\ndefaultvalue,controlname,controlgroup,helptext,helptextdisplaymethod,mask,enforce,actionhandlers,\r\nsetting4string,enabled,readonly,hidecaption,fontsize,dontwraptonextline,defaultvaluestring,uniqueid,specialcontroltype)\r\nVALUES (@controlcode,@controlcaption,@setting1,@setting2,@setting3,@setting4,\r\n@defaultvalue,@controlname,@controlgroup,@helptext,@helptextdisplaymethod,@mask,@enforce\r\n,@actionhandlers,@setting4string,@enabled,@readonly,@hidecaption,@fontsize\r\n,@dontwraptonextline,@defaultvaluestring,@uniqueid,@specialcontroltype);\r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS controlid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					object obj = dataReader[0];
					bool flag2 = obj != null && obj != DBNull.Value;
					if (flag2)
					{
						return (int)obj;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0004113C File Offset: 0x0003F33C
		public DynamicField LoadFieldByName(string Name)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@name", DbType.String, Name)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    dc.controlid,0 AS ordernum,0 AS screennum\r\n            ,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,'' AS description,0 AS typecode,dc.uniqueid,dc.specialcontroltype\r\nFROM        dynamiccontrols dc \r\nWHERE       dc.controlname=@name\r\nORDER BY    dc.controlcaption", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return DynamicFieldDAO.GetFieldFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000411CC File Offset: 0x0003F3CC
		public List<DynamicListItem> LoadListItems(int LookupGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@lookupgroupid", DbType.Int32, LookupGroupId)
			};
			List<DynamicListItem> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT ll.lookupgroupid,lg.description,lg.sortby,lg.childlist,lg.isvisible\r\n            ,ll.lookuplistid,ll.lookuptext,ll.ordernum,ll.lookupvalue,ll.children\r\nFROM        lookuplists ll LEFT JOIN lookupgroups lg ON lg.lookupgroupid=ll.lookupgroupid\r\nWHERE       ll.lookupgroupid=@lookupgroupid AND ll.visible=1\r\nORDER BY    ll.ordernum,ll.lookuptext", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<DynamicListItem> list = new List<DynamicListItem>();
					while (dataReader.Read())
					{
						DynamicListItem listItemFromRecord = this.GetListItemFromRecord(dataReader);
						bool flag2 = listItemFromRecord != null;
						if (flag2)
						{
							list.Add(listItemFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00041284 File Offset: 0x0003F484
		[DebuggerStepThrough]
		public Task<List<DynamicListItem>> LoadListItemsAsync(int LookupGroupId)
		{
			DynamicFieldDAO.<LoadListItemsAsync>d__20 <LoadListItemsAsync>d__ = new DynamicFieldDAO.<LoadListItemsAsync>d__20();
			<LoadListItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<DynamicListItem>>.Create();
			<LoadListItemsAsync>d__.<>4__this = this;
			<LoadListItemsAsync>d__.LookupGroupId = LookupGroupId;
			<LoadListItemsAsync>d__.<>1__state = -1;
			<LoadListItemsAsync>d__.<>t__builder.Start<DynamicFieldDAO.<LoadListItemsAsync>d__20>(ref <LoadListItemsAsync>d__);
			return <LoadListItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000412D0 File Offset: 0x0003F4D0
		public void UpdateFieldName(int ControlId, string NewName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@cid", DbType.Int32, ControlId),
				databaseLayer.GetParameter("@name", DbType.String, NewName)
			};
			databaseLayer.ExecuteNonQuery("UPDATE dynamiccontrols SET controlname=@name WHERE controlid=@cid", parameters);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00041334 File Offset: 0x0003F534
		public IList<DynamicFormOrGroupOrField> LoadFormsWithGroupsAndFields(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[2];
			array[0] = databaseLayer.GetParameter("@excludenondatafields", DbType.Boolean, ExcludeNonDataHoldingControls);
			int num = 1;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@excludescreennums";
			DbType pType = DbType.String;
			object value;
			if (ScreenNumsToExclude != null)
			{
				value = string.Join(",", ScreenNumsToExclude.ToList<int>().ConvertAll<string>((int f) => f.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,\r\n            s.longdescription,s.showasbutton,s.iconindex,s.largeiconindex,s.isactive,\r\n            dsc.controlid,dsc.ordernum,\r\n            dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4,\r\n            dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers,\r\n            dc.controlname,COALESCE(dsc.controlgroup,dc.controlgroup) AS controlgroup,dc.helptext,dc.helptextdisplaymethod,\r\n            dc.enforce,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM        screens s LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=s.screennum\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid\r\nWHERE       (@excludescreennums='' OR NOT s.screennum IN (SELECT orderid AS screennum FROM splitorderids(@excludescreennums,',')))\r\n            AND\r\n            (@excludenondatafields=0 OR NOT dc.controlcode IN (SELECT controlcode FROM DynamicScreenNonDataControls))\r\n            AND NOT dc.controlid IS NULL\r\n            AND NOT dsc.screennum IS NULL\r\nORDER BY s.longdescription,s.description,dsc.ordernum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					Dictionary<int, DynamicForm> dictionary = new Dictionary<int, DynamicForm>();
					List<DynamicFormOrGroupOrField> list = new List<DynamicFormOrGroupOrField>();
					while (dataReader.Read())
					{
						int num2 = (dataReader["screennum"] == DBNull.Value) ? 0 : ((int)dataReader["screennum"]);
						int num3 = (dataReader["controlid"] == DBNull.Value) ? 0 : ((int)dataReader["controlid"]);
						bool flag2 = num2 > 0 && num3 > 0;
						if (flag2)
						{
							DynamicForm dynamicFormFromRecord;
							bool flag3 = !dictionary.TryGetValue(num2, out dynamicFormFromRecord);
							if (flag3)
							{
								dynamicFormFromRecord = DynamicFormsDAO.GetDynamicFormFromRecord(dataReader);
							}
							DynamicField fieldFromRecord = DynamicFieldDAO.GetFieldFromRecord(dataReader);
							list.Add(new DynamicFormOrGroupOrField
							{
								GroupName = (dynamicFormFromRecord.GroupName ?? ""),
								DynamicForm = dynamicFormFromRecord,
								Field = fieldFromRecord
							});
						}
					}
					dictionary.Clear();
					return list;
				}
			}
			return null;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000414F8 File Offset: 0x0003F6F8
		public IDictionary<int, ExtendedAccommodationInfo> LoadAccommodationShortCodes(params int[] ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			Dictionary<int, ExtendedAccommodationInfo> dictionary = new Dictionary<int, ExtendedAccommodationInfo>();
			bool flag = ControlIds == null || ControlIds.Length < 1;
			IDictionary<int, ExtendedAccommodationInfo> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = databaseLayer.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
				DbParameter[] parameters = array;
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT    acc.controlid,acc.accommodationid,acc.longdescription,acc.shortcode,acc.showonletter AS showonletter1,acc.showonemail,\r\n            acc.extratime,acc.isalone,acc.needscomputer,acc.needsreaderscribe,acc.availableinallrooms,acc.groupid,acc.isgroup,acc.tapedexams,\r\n            acc.other,acc.enlarged,acc.showonreport\r\nFROM    accommodations acc \r\nWHERE   acc.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))", parameters))
				{
					bool flag2 = dataReader == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						while (dataReader.Read())
						{
							int num = (dataReader["controlid"] is DBNull) ? 0 : ((int)dataReader["controlid"]);
							bool flag3 = num <= 0 || dictionary.ContainsKey(num);
							if (!flag3)
							{
								ExtendedAccommodationInfo extendedAccommodationInfoFromRecord = AccommodationsDAO.GetExtendedAccommodationInfoFromRecord(dataReader, this.OpContext);
								bool flag4 = extendedAccommodationInfoFromRecord != null;
								if (flag4)
								{
									dictionary.Add(num, extendedAccommodationInfoFromRecord);
								}
							}
						}
						result = dictionary;
					}
				}
			}
			return result;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0004164C File Offset: 0x0003F84C
		public DynamicField GetFirstFieldOnFirstPerAppointmentForm(int AppTypeId, eControlCode FieldType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@apptypeid", DbType.Int32, AppTypeId),
				databaseLayer.GetParameter("@controlcode", DbType.Int32, (int)FieldType)
			};
			DynamicField result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @screennums varchar(max)\r\nSET @screennums=COALESCE((SELECT TOP 1 perAppScreenNumsForTabs FROM AppointmentTypes WHERE AppTypeID=@apptypeid),'')\r\n\r\nDECLARE @screennum int\r\nSET @screennum=COALESCE((SELECT TOP 1 orderid AS screennum FROM splitorderids(@screennums,',')),0)\r\n\r\nSELECT    dc.controlid,0 AS ordernum,0 AS screennum\r\n            ,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.mask,dc.actionhandlers\r\n            ,dc.controlname,dc.controlgroup,dc.helptext,dc.helptextdisplaymethod\r\n            ,dc.enforce,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,'' AS description,0 AS typecode,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid \r\nWHERE dsc.screennum=@screennum AND dc.controlcode=@controlcode\r\nORDER BY dsc.ordernum", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = DynamicFieldDAO.GetFieldFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000416F4 File Offset: 0x0003F8F4
		public bool IsListItemSavedSomewhere(int LookupListId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@lookuplistid", DbType.Int32, LookupListId)
			};
			object obj = databaseLayer.ExecuteScalar("DECLARE @dataexists bit\r\n\r\nIF EXISTS(SELECT TOP 1 dataid FROM perstudentdata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid))\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM perappdata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid))\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM accommodationdata WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM perinstructordata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM perinventorydata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM perinvigilatordata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM pjadata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM pjcdata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE IF EXISTS(SELECT TOP 1 dataid FROM pmdata2 WHERE (controlcode=3 AND setting3=0 AND valint=@lookuplistid) OR (controlcode=14 AND setting4=0 AND valint=@lookuplistid) )\r\n\tSET @dataexists=1\r\nELSE\r\n\tSET @dataexists=0\r\n\t\r\nSELECT @dataexists AS dataexists", parameters);
			return obj != null && obj != DBNull.Value && obj is bool && (bool)obj;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00041768 File Offset: 0x0003F968
		public IList<DynamicListGroup> LoadAllLookupLists()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<DynamicListGroup> list = new List<DynamicListGroup>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT LookupGroupID,description,SortBy,childlist FROM LookupGroups WHERE isvisible=1 ORDER BY SortBy,description"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					DynamicListGroup listGroupFromRecord = this.GetListGroupFromRecord(dataReader);
					bool flag2 = listGroupFromRecord != null;
					if (flag2)
					{
						list.Add(listGroupFromRecord);
					}
				}
			}
			return list;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00041804 File Offset: 0x0003FA04
		public int CreateList(DynamicListGroup listGroup, IList<DynamicListItem> listItems)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@lookupgroupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@description", DbType.String, string.IsNullOrEmpty(listGroup.Description) ? "New list" : listGroup.Description),
				databaseLayer.GetParameter("@sortby", DbType.Int32, listGroup.SortBy),
				databaseLayer.GetParameter("@isvisible", DbType.Boolean, true)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO lookupgroups (description,sortby,isvisible) VALUES (@description,@sortby,@isvisible)\r\nSET @lookupgroupid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS INT))", array);
			int valueOrDefault = ((int?)array[0].Value).GetValueOrDefault();
			bool flag = valueOrDefault < 1;
			int result;
			if (flag)
			{
				result = valueOrDefault;
			}
			else
			{
				int num = 0;
				foreach (DynamicListItem dynamicListItem in listItems)
				{
					array = new DbParameter[]
					{
						databaseLayer.GetParameter("@lookupgroupid", DbType.Int32, valueOrDefault),
						databaseLayer.GetParameter("@lookuptext", DbType.String, dynamicListItem.LookupText ?? ""),
						databaseLayer.GetParameter("@ordernum", DbType.Int32, num++)
					};
					databaseLayer.ExecuteNonQuery("INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum,visible) VALUES (@lookupgroupid,@lookuptext,@ordernum,1)", array);
				}
				result = valueOrDefault;
			}
			return result;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00041980 File Offset: 0x0003FB80
		public IDictionary<int, IList<int>> LoadControlIdsByForms(params int[] screenNums)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			array[0] = databaseLayer.GetParameter("@screennums", DbType.String, string.Join(",", (from g in screenNums
			select g.ToString()).ToArray<string>()));
			DbParameter[] parameters = array;
			IDictionary<int, IList<int>> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT screennum,controlid FROM dynamicscreencontrols WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')) ORDER BY screennum,ordernum", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					Dictionary<int, IList<int>> dictionary = screenNums.ToDictionary((int g) => g, (int g) => new List<int>());
					int num = 0;
					IList<int> list = null;
					while (dataReader.Read())
					{
						int num2 = (dataReader["screennum"] is DBNull) ? 0 : ((int)dataReader["screennum"]);
						int num3 = (dataReader["controlid"] is DBNull) ? 0 : ((int)dataReader["controlid"]);
						bool flag2 = num3 < 1 || num2 < 1;
						if (!flag2)
						{
							bool flag3 = num2 != num;
							if (flag3)
							{
								num = num2;
								bool flag4 = dictionary.ContainsKey(num);
								if (flag4)
								{
									list = dictionary[num];
								}
								else
								{
									list = new List<int>();
									dictionary.Add(num, list);
								}
							}
							bool flag5 = !list.Contains(num3);
							if (flag5)
							{
								list.Add(num3);
							}
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00041B64 File Offset: 0x0003FD64
		[DebuggerStepThrough]
		public Task<IDictionary<int, IList<int>>> LoadControlIdsByFormsAsync(params int[] screenNums)
		{
			DynamicFieldDAO.<LoadControlIdsByFormsAsync>d__29 <LoadControlIdsByFormsAsync>d__ = new DynamicFieldDAO.<LoadControlIdsByFormsAsync>d__29();
			<LoadControlIdsByFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IDictionary<int, IList<int>>>.Create();
			<LoadControlIdsByFormsAsync>d__.<>4__this = this;
			<LoadControlIdsByFormsAsync>d__.screenNums = screenNums;
			<LoadControlIdsByFormsAsync>d__.<>1__state = -1;
			<LoadControlIdsByFormsAsync>d__.<>t__builder.Start<DynamicFieldDAO.<LoadControlIdsByFormsAsync>d__29>(ref <LoadControlIdsByFormsAsync>d__);
			return <LoadControlIdsByFormsAsync>d__.<>t__builder.Task;
		}
	}
}

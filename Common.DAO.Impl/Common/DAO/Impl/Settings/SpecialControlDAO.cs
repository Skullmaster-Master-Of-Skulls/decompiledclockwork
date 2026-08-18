using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x0200004D RID: 77
	public class SpecialControlDAO : ISpecialControlDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00011FC2 File Offset: 0x000101C2
		public SpecialControlDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00011FD4 File Offset: 0x000101D4
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00011FDC File Offset: 0x000101DC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000207 RID: 519 RVA: 0x00011FE8 File Offset: 0x000101E8
		private string GetStringValueFromReader(IDataReader reader)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string text = (reader["valtext"] is DBNull) ? null : ((string)reader["valtext"]);
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				byte[] array = (reader["valbytes"] is DBNull) ? null : ((byte[])reader["valbytes"]);
				bool flag2 = array != null && array.Length != 0;
				if (flag2)
				{
					result = databaseLayer.Encryption.Decrypt(array);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00012098 File Offset: 0x00010298
		public int GetSpecialControlId(eSpecialControlType SpecialControlType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@specialControlType", DbType.Int32, SpecialControlType)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT TOP 1 controlid FROM dynamiccontrols WHERE specialcontroltype=@specialcontroltype ORDER BY controlid DESC", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (dataReader[0] is DBNull) ? 0 : ((int)dataReader[0]);
				}
			}
			return 0;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00012140 File Offset: 0x00010340
		public DateTime? GetSpecialControlValueDateTime(int PersonId, eSpecialControlType SpecialControlType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@specialControlType", DbType.Int32, (int)SpecialControlType)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT TOP 1 ps.valtext,ps.valbytes,ps.valdate\r\nFROM perstudentdata2 ps\r\nWHERE ps.PersonID=@pid AND ps.SpecialControlType=@specialControlType\r\nORDER BY ps.controlID DESC", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					DateTime? result = (dataReader["valdate"] is DBNull) ? null : new DateTime?((DateTime)dataReader["valdate"]);
					bool flag2 = result != null;
					if (flag2)
					{
						return result;
					}
					string stringValueFromReader = this.GetStringValueFromReader(dataReader);
					bool flag3 = !string.IsNullOrEmpty(stringValueFromReader);
					if (flag3)
					{
						DateTime value;
						bool flag4 = DateTime.TryParse(stringValueFromReader, out value);
						if (flag4)
						{
							return new DateTime?(value);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00012268 File Offset: 0x00010468
		public bool? GetSpecialControlValueBool(int PersonId, eSpecialControlType SpecialControlType)
		{
			int? specialControlValueInt = this.GetSpecialControlValueInt(PersonId, SpecialControlType);
			bool flag = specialControlValueInt == null;
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new bool?(specialControlValueInt.Value != 0);
			}
			return result;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000122AC File Offset: 0x000104AC
		public int? GetSpecialControlValueInt(int PersonId, eSpecialControlType SpecialControlType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@specialControlType", DbType.Int32, (int)SpecialControlType)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT TOP 1 ps.valtext,ps.valint,ps.valbytes\r\nFROM perstudentdata2 ps\r\nWHERE ps.PersonID=@pid AND ps.SpecialControlType=@specialControlType\r\nORDER BY ps.controlID DESC", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					int? result = (dataReader["valint"] is DBNull) ? null : new int?((int)dataReader["valint"]);
					bool flag2 = result != null;
					if (flag2)
					{
						return result;
					}
					string stringValueFromReader = this.GetStringValueFromReader(dataReader);
					bool flag3 = !string.IsNullOrEmpty(stringValueFromReader);
					if (flag3)
					{
						int value;
						bool flag4 = int.TryParse(stringValueFromReader, out value);
						if (flag4)
						{
							return new int?(value);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000123D4 File Offset: 0x000105D4
		public string GetSpecialControlValueString(int PersonId, eSpecialControlType SpecialControlType)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@specialControlType", DbType.Int32, (int)SpecialControlType)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT TOP 1 ps.valtext,ps.valbytes,ps.valint\r\nFROM perstudentdata2 ps\r\nWHERE ps.PersonID=@pid AND ps.SpecialControlType=@specialControlType\r\nORDER BY ps.controlID DESC", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetStringValueFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001247C File Offset: 0x0001067C
		public IDictionary<eSpecialControlType, int> GetDefinedSpecialControlIds(IList<eSpecialControlType> RestrictSearchToTheseSpecialControlTypes = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[1];
			int num = 0;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@restricttypes";
			DbType pType = DbType.String;
			object value;
			if (RestrictSearchToTheseSpecialControlTypes != null)
			{
				value = string.Join(",", RestrictSearchToTheseSpecialControlTypes.ToList<eSpecialControlType>().ConvertAll<string>(delegate(eSpecialControlType g)
				{
					int num4 = (int)g;
					return num4.ToString();
				}).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseLayer2.GetParameter(pName, pType, value);
			DbParameter[] parameters = array;
			IDictionary<eSpecialControlType, int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tdc.SpecialControlType,MAX(dc.ControlID) AS controlid\r\nFROM\tDynamicControls dc \r\nWHERE   dc.SpecialControlType>0 AND\r\n\t\t(@restricttypes IS NULL OR @restricttypes='' OR dc.SpecialControlType IN (SELECT orderid AS specialcontroltype FROM splitorderids(@restricttypes,',')))\r\nGROUP BY dc.SpecialControlType ", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					Dictionary<eSpecialControlType, int> dictionary = new Dictionary<eSpecialControlType, int>();
					if (dataReader.Read())
					{
						int num2 = (dataReader["specialcontroltype"] is DBNull) ? 0 : ((int)dataReader["specialcontroltype"]);
						int num3 = (dataReader["controlid"] is DBNull) ? 0 : ((int)dataReader["controlid"]);
						bool flag2 = num3 > 0 && num2 > 0 && Enum.IsDefined(typeof(eSpecialControlType), num2);
						if (flag2)
						{
							eSpecialControlType key = (eSpecialControlType)num2;
							bool flag3 = !dictionary.ContainsKey(key);
							if (flag3)
							{
								dictionary.Add(key, num3);
							}
						}
						return dictionary;
					}
				}
				result = null;
			}
			return result;
		}
	}
}

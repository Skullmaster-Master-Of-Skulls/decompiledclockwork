using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.DAO.Impl.UserSettingsPermissions
{
	// Token: 0x02000028 RID: 40
	public class PermissionDAO : IPermissionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00007677 File Offset: 0x00005877
		public PermissionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00007689 File Offset: 0x00005889
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00007691 File Offset: 0x00005891
		public OperationContext OpContext { get; set; }

		// Token: 0x060000FF RID: 255 RVA: 0x0000769C File Offset: 0x0000589C
		private UserPermission GetUserPermissionFromRecord(IDataReader record)
		{
			bool flag = record == null;
			UserPermission result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
				int num2 = (record["groupid"] is DBNull) ? 0 : ((int)record["groupid"]);
				int num3 = (record["permissioncode"] is DBNull) ? 0 : ((int)record["permissioncode"]);
				bool flag2 = !Enum.IsDefined(typeof(UserPermissionEnum), num3);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new UserPermission
					{
						PersonOrGroupId = ((num > 0) ? num : num2),
						PermissionType = ((num > 0) ? eUserPermissionType.Person : ((num2 > 0) ? eUserPermissionType.Group : eUserPermissionType.Everyone)),
						Permission = (UserPermissionEnum)num3,
						PermissionValue = ((record["permissionvalue"] is DBNull) ? -1 : ((int)record["permissionvalue"]))
					};
				}
			}
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000077BC File Offset: 0x000059BC
		public IList<UserPermission> LoadUserPermissions(int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			IList<UserPermission> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT groupid,ordernum INTO #t1 FROM peoplegroups WHERE personid=@pid ORDER BY ordernum;\r\nDECLARE @maxGroupIdOrderNum int\r\nSET @maxGroupIdOrderNum=(SELECT MAX(ordernum) FROM #t1)\r\nIF @maxGroupIdOrderNum IS NULL \r\n    SET @maxGroupIdOrderNum=0;\r\nSET @maxGroupIdOrderNum=@maxGroupIdOrderNum+100\r\n\r\nSELECT  permissionid AS id,CAST(NULL AS int) AS groupid,personid,permissioncode,permissionvalue,CAST(0 AS int) AS ordernum \r\nFROM    permissions \r\nWHERE   personid=@pid\r\n\r\nUNION\r\n\r\nSELECT  p.permissiongroupid AS id,#t1.groupid,CAST(NULL AS int) AS personid,p.permissioncode,p.permissionvalue,#t1.ordernum\r\nFROM    permissionsgroups p LEFT JOIN #t1 ON #t1.groupid=p.groupid\r\nWHERE   #t1.groupid > 0\r\n\r\nUNION   \r\n\r\nSELECT  permissiongroupid AS id,groupid,CAST(NULL AS int) AS personid,permissioncode,permissionvalue,@maxGroupIdOrderNum AS ordernum\r\nFROM    permissionsgroups\r\nWHERE   groupid <= 0\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<UserPermission> list = new List<UserPermission>();
					while (dataReader.Read())
					{
						UserPermission userPermissionFromRecord = this.GetUserPermissionFromRecord(dataReader);
						bool flag2 = userPermissionFromRecord != null;
						if (flag2)
						{
							list.Add(userPermissionFromRecord);
						}
					}
					list.Sort(delegate(UserPermission g1, UserPermission g2)
					{
						int num = g1.PermissionType.CompareTo(g2.PermissionType);
						bool flag3 = num != 0;
						int result2;
						if (flag3)
						{
							result2 = num;
						}
						else
						{
							result2 = g1.OrderNum.CompareTo(g2.OrderNum);
						}
						return result2;
					});
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007898 File Offset: 0x00005A98
		public void LegacyLoadUserAndGroupPermissionTables(int pid, out DataTable personPermissionsTable, out DataTable groupPermissionsTable)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			string text = "SELECT permissionid,personid,permissioncode,permissionvalue FROM permissions WHERE personid=@personid";
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@personid", DbType.Int32, pid)
			};
			personPermissionsTable = databaseLayer.ExecuteQuery(text, parameters);
			text = "SELECT p.permissiongroupid,p.groupid,p.permissioncode,p.permissionvalue,g.ordernum\r\nFROM peoplegroups pg LEFT JOIN permissionsgroups p ON p.groupid=pg.groupid LEFT JOIN groups g ON g.groupid=p.groupid WHERE NOT p.permissiongroupid IS NULL AND pg.personid=" + pid.ToString();
			text = text + " UNION SELECT p.permissiongroupid,p.groupid,p.permissioncode,p.permissionvalue," + int.MaxValue.ToString() + " AS ordernum FROM permissionsgroups p WHERE p.groupid=-1";
			text = "SELECT q.* FROM (" + text + ") q ORDER BY q.ordernum";
			groupPermissionsTable = databaseLayer.ExecuteQuery(text);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007938 File Offset: 0x00005B38
		public UserOrGroupJustPermissionSet LoadJustUserPermissions(int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			UserOrGroupJustPermissionSet result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT permissionid,personid,permissioncode,permissionvalue FROM permissions WHERE personid=@pid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					UserOrGroupJustPermissionSet userOrGroupJustPermissionSet = new UserOrGroupJustPermissionSet
					{
						GeneralPermissions = new List<UserOrGroupJustPermission>(),
						PermissionType = eUserPermissionType.Person,
						PersonOrGroupId = pid,
						ScreenNumsAllowedCreateScreen = new List<int>(),
						ScreenNumsAllowedModifyScreen = new List<int>(),
						ScreenNumsAllowedViewScreen = new List<int>()
					};
					while (dataReader.Read())
					{
						int id = (dataReader["permissionid"] is DBNull) ? 0 : ((int)dataReader["permissionid"]);
						int num = (dataReader["permissioncode"] is DBNull) ? 0 : ((int)dataReader["permissioncode"]);
						bool flag2 = !Enum.IsDefined(typeof(UserPermissionEnum), num);
						if (!flag2)
						{
							int num2 = (dataReader["permissionvalue"] is DBNull) ? 0 : ((int)dataReader["permissionvalue"]);
							UserPermissionEnum permission = (UserPermissionEnum)num;
							switch (permission)
							{
							case UserPermissionEnum.ViewScreen:
							{
								bool flag3 = num2 > 0 && !userOrGroupJustPermissionSet.ScreenNumsAllowedViewScreen.Contains(num2);
								if (flag3)
								{
									userOrGroupJustPermissionSet.ScreenNumsAllowedViewScreen.Add(num2);
								}
								break;
							}
							case UserPermissionEnum.ModifyScreen:
							{
								bool flag4 = num2 > 0 && !userOrGroupJustPermissionSet.ScreenNumsAllowedModifyScreen.Contains(num2);
								if (flag4)
								{
									userOrGroupJustPermissionSet.ScreenNumsAllowedModifyScreen.Add(num2);
								}
								break;
							}
							case UserPermissionEnum.UseAdminProgram:
							case UserPermissionEnum.DeleteStudent:
								goto IL_1F7;
							case UserPermissionEnum.CreateScreen:
							{
								bool flag5 = num2 > 0 && !userOrGroupJustPermissionSet.ScreenNumsAllowedCreateScreen.Contains(num2);
								if (flag5)
								{
									userOrGroupJustPermissionSet.ScreenNumsAllowedCreateScreen.Add(num2);
								}
								break;
							}
							default:
								goto IL_1F7;
							}
							continue;
							IL_1F7:
							UserOrGroupJustPermission item = new UserOrGroupJustPermission
							{
								Id = id,
								Permission = permission,
								IsAllowed = this.GetIsAllowedFromPermission(permission, num2)
							};
							userOrGroupJustPermissionSet.GeneralPermissions.Add(item);
						}
					}
					result = userOrGroupJustPermissionSet;
				}
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public UserOrGroupJustPermissionSet LoadJustGroupPermissions(int gid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, gid)
			};
			UserOrGroupJustPermissionSet result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT permissiongroupid,groupid,permissioncode,permissionvalue FROM permissionsgroups WHERE groupid=@gid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					UserOrGroupJustPermissionSet userOrGroupJustPermissionSet = new UserOrGroupJustPermissionSet
					{
						GeneralPermissions = new List<UserOrGroupJustPermission>(),
						PermissionType = ((gid >= 0) ? eUserPermissionType.Group : eUserPermissionType.Everyone),
						PersonOrGroupId = gid,
						ScreenNumsAllowedCreateScreen = new List<int>(),
						ScreenNumsAllowedModifyScreen = new List<int>(),
						ScreenNumsAllowedViewScreen = new List<int>()
					};
					while (dataReader.Read())
					{
						int id = (dataReader["PermissionGroupID"] is DBNull) ? 0 : ((int)dataReader["PermissionGroupID"]);
						int num = (dataReader["permissioncode"] is DBNull) ? 0 : ((int)dataReader["permissioncode"]);
						bool flag2 = !Enum.IsDefined(typeof(UserPermissionEnum), num);
						if (!flag2)
						{
							int num2 = (dataReader["permissionvalue"] is DBNull) ? 0 : ((int)dataReader["permissionvalue"]);
							UserPermissionEnum permission = (UserPermissionEnum)num;
							switch (permission)
							{
							case UserPermissionEnum.ViewScreen:
							{
								bool flag3 = num2 > 0 && !userOrGroupJustPermissionSet.ScreenNumsAllowedViewScreen.Contains(num2);
								if (flag3)
								{
									userOrGroupJustPermissionSet.ScreenNumsAllowedViewScreen.Add(num2);
								}
								break;
							}
							case UserPermissionEnum.ModifyScreen:
							{
								bool flag4 = num2 > 0 && !userOrGroupJustPermissionSet.ScreenNumsAllowedModifyScreen.Contains(num2);
								if (flag4)
								{
									userOrGroupJustPermissionSet.ScreenNumsAllowedModifyScreen.Add(num2);
								}
								break;
							}
							case UserPermissionEnum.UseAdminProgram:
							case UserPermissionEnum.DeleteStudent:
								goto IL_1FE;
							case UserPermissionEnum.CreateScreen:
							{
								bool flag5 = num2 > 0 && !userOrGroupJustPermissionSet.ScreenNumsAllowedCreateScreen.Contains(num2);
								if (flag5)
								{
									userOrGroupJustPermissionSet.ScreenNumsAllowedCreateScreen.Add(num2);
								}
								break;
							}
							default:
								goto IL_1FE;
							}
							continue;
							IL_1FE:
							UserOrGroupJustPermission item = new UserOrGroupJustPermission
							{
								Id = id,
								Permission = permission,
								IsAllowed = this.GetIsAllowedFromPermission(permission, num2)
							};
							userOrGroupJustPermissionSet.GeneralPermissions.Add(item);
						}
					}
					result = userOrGroupJustPermissionSet;
				}
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007E38 File Offset: 0x00006038
		private void UpdateJustUserOrGroupPermissions(UserOrGroupJustPermissionSet permissionSet, string personOrGroupIdParamName, string deleteAllPermissionsQuery, string insertPermissionQuery)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			try
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter(personOrGroupIdParamName, DbType.Int32, permissionSet.PersonOrGroupId)
				};
				databaseLayer.ExecuteNonQueryTransaction(deleteAllPermissionsQuery, dbTransaction, parameters);
				foreach (UserOrGroupJustPermission userOrGroupJustPermission in permissionSet.GeneralPermissions.Where(delegate(UserOrGroupJustPermission g)
				{
					UserPermissionAttribute attribute = g.Permission.GetAttribute<UserPermissionAttribute>();
					bool flag = attribute == null;
					bool result;
					if (flag)
					{
						result = true;
					}
					else
					{
						UserPermissionGroupAttribute attribute2 = attribute.Group.GetAttribute<UserPermissionGroupAttribute>();
						bool flag2 = attribute2 == null;
						result = (flag2 || !attribute2.IsScreenViewModifyCreatePermissions);
					}
					return result;
				}))
				{
					parameters = new DbParameter[]
					{
						databaseLayer.GetParameter(personOrGroupIdParamName, DbType.Int32, permissionSet.PersonOrGroupId),
						databaseLayer.GetParameter("@pc", DbType.Int32, (int)userOrGroupJustPermission.Permission),
						databaseLayer.GetParameter("@val", DbType.Int32, this.GetPermissionValueFromIsAllowed(userOrGroupJustPermission.Permission, userOrGroupJustPermission.IsAllowed))
					};
					databaseLayer.ExecuteNonQueryTransaction(insertPermissionQuery, dbTransaction, parameters);
				}
				this.UpdateJustUserOrGroupScreenPermissions(permissionSet.PersonOrGroupId, UserPermissionEnum.ViewScreen, permissionSet.ScreenNumsAllowedViewScreen, databaseLayer, dbTransaction, personOrGroupIdParamName, insertPermissionQuery);
				this.UpdateJustUserOrGroupScreenPermissions(permissionSet.PersonOrGroupId, UserPermissionEnum.ModifyScreen, permissionSet.ScreenNumsAllowedModifyScreen, databaseLayer, dbTransaction, personOrGroupIdParamName, insertPermissionQuery);
				this.UpdateJustUserOrGroupScreenPermissions(permissionSet.PersonOrGroupId, UserPermissionEnum.CreateScreen, permissionSet.ScreenNumsAllowedCreateScreen, databaseLayer, dbTransaction, personOrGroupIdParamName, insertPermissionQuery);
				dbTransaction.Commit();
			}
			catch (Exception ex)
			{
				dbTransaction.Rollback();
				string message = string.Format("PermissionDAO:UpdateJustUserOrGroupPermissions:RolledBack:Err={0}", ex.ToString());
				CWLogger.Logger.Error(message);
				throw new DatabaseUpdateFailedException(message);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000800C File Offset: 0x0000620C
		private bool GetIsAllowedFromPermission(UserPermissionEnum permission, int permissionValue)
		{
			bool flag = permission < (UserPermissionEnum)0;
			bool result;
			if (flag)
			{
				result = (permissionValue == 0);
			}
			else
			{
				result = (permissionValue != 0);
			}
			return result;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008034 File Offset: 0x00006234
		private int GetPermissionValueFromIsAllowed(UserPermissionEnum permission, bool isAllowed)
		{
			bool flag = permission < (UserPermissionEnum)0;
			int result;
			if (flag)
			{
				result = (isAllowed ? 0 : -1);
			}
			else
			{
				result = (isAllowed ? 1 : 0);
			}
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008060 File Offset: 0x00006260
		private void UpdateJustUserOrGroupScreenPermissions(int personOrGroupId, UserPermissionEnum permission, IList<int> screenNumsAllowed, DatabaseLayer databaseManager, DbTransaction transaction, string personOrGroupIdParamName, string insertPermissionQuery)
		{
			bool flag = screenNumsAllowed == null;
			if (!flag)
			{
				foreach (int num in screenNumsAllowed)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						databaseManager.GetParameter(personOrGroupIdParamName, DbType.Int32, personOrGroupId),
						databaseManager.GetParameter("@pc", DbType.Int32, permission),
						databaseManager.GetParameter("@val", DbType.Int32, num)
					};
					databaseManager.ExecuteNonQueryTransaction(insertPermissionQuery, transaction, parameters);
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008108 File Offset: 0x00006308
		public void UpdateJustUserOrGroupPermissions(UserOrGroupJustPermissionSet permissionSet)
		{
			eUserPermissionType permissionType = permissionSet.PermissionType;
			eUserPermissionType eUserPermissionType = permissionType;
			if (eUserPermissionType != eUserPermissionType.Person)
			{
				if (eUserPermissionType - eUserPermissionType.Group > 1)
				{
					throw new InvalidParameterException("UpdateJustUserPermissions:Unknown permission type:" + permissionSet.PermissionType.ToString());
				}
				this.UpdateJustUserOrGroupPermissions(permissionSet, "@gid", "DELETE FROM permissionsgroups WHERE groupid=@gid", "INSERT INTO permissionsgroups (groupid,permissioncode,permissionvalue) VALUES (@gid,@pc,@val)");
			}
			else
			{
				this.UpdateJustUserOrGroupPermissions(permissionSet, "@pid", "DELETE FROM permissions WHERE personid=@pid", "INSERT INTO permissions (personid,permissioncode,permissionvalue) VALUES (@pid,@pc,@val)");
			}
		}
	}
}

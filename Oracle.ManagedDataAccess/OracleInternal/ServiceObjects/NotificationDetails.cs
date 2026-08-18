using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.I18N;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A8 RID: 424
	internal class NotificationDetails
	{
		// Token: 0x06000FDE RID: 4062 RVA: 0x000A3D58 File Offset: 0x000A1F58
		internal NotificationDetails(short csId, byte[] notifInfo)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_notificationInfoBuffer = notifInfo;
				this.m_csId = csId;
				this.m_type = OracleNotificationType.Change;
				this.m_source = OracleNotificationSource.Data;
				this.m_info = OracleNotificationInfo.Update;
				this.m_resources = new string[0];
				this.m_details = new DataTable();
				this.m_details.Columns.Add("ResourceName", typeof(string));
				this.m_details.Columns.Add("Info", typeof(OracleNotificationInfo));
				this.m_details.Columns.Add("Rowid", typeof(string));
				this.m_details.Columns.Add("QueryId", typeof(long));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000A3EA0 File Offset: 0x000A20A0
		internal void ParseNotificationInfo()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_notificationInfoBuffer != null && this.m_notificationInfoBuffer.Length > 0)
				{
					this.m_byteBuffer = new ByteBuffer(this.m_notificationInfoBuffer.Length);
					this.m_byteBuffer.m_byteBuffer = this.m_notificationInfoBuffer;
					this.m_byteBuffer.Limit = this.m_notificationInfoBuffer.Length;
					this.m_byteBuffer.GetByte();
					int @int = this.m_byteBuffer.GetInt();
					byte[] bytes = null;
					int byteOffset = -1;
					this.m_byteBuffer.GetBufferRef(out bytes, out byteOffset, @int);
					string text = Conv.GetInstance(871).ConvertBytesToString(bytes, byteOffset, @int, null, true);
					text = text.Trim("CHNF".ToCharArray());
					this.m_regIdFromServer = int.Parse(text);
					this.m_byteBuffer.GetByte();
					int int2 = this.m_byteBuffer.GetInt();
					byte[] array = null;
					this.m_byteBuffer.GetBufferRef(out array, out byteOffset, int2);
					this.m_byteBuffer.GetByte();
					this.m_byteBuffer.GetInt();
					if (this.m_byteBuffer.HasRemaining)
					{
						this.m_byteBuffer.GetShort();
						this.m_byteBuffer.GetInt();
						this.SetNotificationTypeSourceInfo(this.m_byteBuffer.GetInt());
						if (this.m_source == OracleNotificationSource.Data)
						{
							int @short = (int)this.m_byteBuffer.GetShort();
							byte[] bytes2 = null;
							this.m_byteBuffer.GetBufferRef(out bytes2, out byteOffset, @short);
							Conv.GetInstance(871).ConvertBytesToString(bytes2, byteOffset, @short, null, true);
							this.m_byteBuffer.GetLong();
							this.m_byteBuffer.GetInt();
							this.m_byteBuffer.GetShort();
							if (this.m_type == OracleNotificationType.Query)
							{
								short short2 = this.m_byteBuffer.GetShort();
								for (int i = 0; i < (int)short2; i++)
								{
									this.ReadQueryInfo(this.resourceNamesList);
								}
							}
							else if (this.m_type == OracleNotificationType.Change)
							{
								int short3 = (int)this.m_byteBuffer.GetShort();
								for (int j = 0; j < short3; j++)
								{
									this.ReadTableInfo(0L, this.resourceNamesList);
								}
							}
							this.m_resources = (string[])this.resourceNamesList.ToArray(typeof(string));
							if (this.resourceNamesList.Count > 0)
							{
								this.resourceNamesList.Clear();
							}
							if (this.m_details.Rows.Count > 0)
							{
								this.m_info = (OracleNotificationInfo)this.m_details.Rows[0][1];
							}
							else
							{
								this.m_info = OracleNotificationInfo.Error;
							}
						}
					}
					this.m_byteBuffer = null;
					this.m_notificationInfoBuffer = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000A41B0 File Offset: 0x000A23B0
		private void ReadQueryInfo(ArrayList resourceNamesList)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				long num = (long)this.m_byteBuffer.GetInt();
				long num2 = (long)this.m_byteBuffer.GetInt();
				long num3 = num | num2 << 32;
				if (!this.m_queryIdList.Contains(num3))
				{
					this.m_queryIdList.Add(num3);
				}
				this.m_byteBuffer.GetInt();
				int @short = (int)this.m_byteBuffer.GetShort();
				for (int i = 0; i < @short; i++)
				{
					this.ReadTableInfo(num3, resourceNamesList);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000A428C File Offset: 0x000A248C
		internal void ReadTableInfo(long queryId, ArrayList resourceNamesList)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				int @int = this.m_byteBuffer.GetInt();
				int @short = (int)this.m_byteBuffer.GetShort();
				byte[] bytes = null;
				int byteOffset = -1;
				this.m_byteBuffer.GetBufferRef(out bytes, out byteOffset, @short);
				string text = Conv.GetInstance((int)this.m_csId).ConvertBytesToString(bytes, byteOffset, @short, null, true);
				resourceNamesList.Add(text);
				this.m_byteBuffer.GetInt();
				int num = 0;
				if ((@int & 1) == 0)
				{
					num = (int)this.m_byteBuffer.GetShort();
				}
				if (num == 0)
				{
					if ((@int & 16) != 0)
					{
						this.m_source = OracleNotificationSource.Object;
						this.AddRowDetail(text, OracleNotificationInfo.Alter, null, queryId);
					}
					if ((@int & 32) != 0)
					{
						this.m_source = OracleNotificationSource.Object;
						this.AddRowDetail(text, OracleNotificationInfo.Drop, null, queryId);
					}
					if ((@int & 4) != 0)
					{
						this.AddRowDetail(text, OracleNotificationInfo.Update, null, queryId);
					}
					if ((@int & 2) != 0)
					{
						this.AddRowDetail(text, OracleNotificationInfo.Insert, null, queryId);
					}
					if ((@int & 8) != 0)
					{
						this.AddRowDetail(text, OracleNotificationInfo.Delete, null, queryId);
					}
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						this.ReadRowInfo(queryId, text);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000A4410 File Offset: 0x000A2610
		internal void ReadRowInfo(long queryId, string tableName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				int @int = this.m_byteBuffer.GetInt();
				int @short = (int)this.m_byteBuffer.GetShort();
				byte[] bytes = null;
				int byteOffset = -1;
				this.m_byteBuffer.GetBufferRef(out bytes, out byteOffset, @short);
				string rowid = Conv.GetInstance(871).ConvertBytesToString(bytes, byteOffset, @short, null, true);
				if ((@int & 16) != 0)
				{
					this.m_source = OracleNotificationSource.Object;
					this.AddRowDetail(tableName, OracleNotificationInfo.Alter, null, queryId);
				}
				if ((@int & 32) != 0)
				{
					this.m_source = OracleNotificationSource.Object;
					this.AddRowDetail(tableName, OracleNotificationInfo.Drop, null, queryId);
				}
				if ((@int & 2) != 0)
				{
					this.AddRowDetail(tableName, OracleNotificationInfo.Insert, rowid, queryId);
				}
				if ((@int & 8) != 0)
				{
					this.AddRowDetail(tableName, OracleNotificationInfo.Delete, rowid, queryId);
				}
				if ((@int & 4) != 0)
				{
					this.AddRowDetail(tableName, OracleNotificationInfo.Update, rowid, queryId);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000A452C File Offset: 0x000A272C
		internal void AddRowDetail(string name, OracleNotificationInfo info, string rowid, long queryid)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				DataRow dataRow = this.m_details.NewRow();
				dataRow[0] = name;
				dataRow[1] = info;
				dataRow[2] = rowid;
				dataRow[3] = queryid;
				this.m_details.Rows.Add(dataRow);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000A45E4 File Offset: 0x000A27E4
		internal void SetNotificationTypeSourceInfo(int notifId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				switch (notifId)
				{
				case 1:
					this.m_type = OracleNotificationType.Change;
					this.m_source = OracleNotificationSource.Database;
					this.m_info = OracleNotificationInfo.Startup;
					goto IL_101;
				case 2:
					this.m_type = OracleNotificationType.Change;
					this.m_source = OracleNotificationSource.Database;
					this.m_info = OracleNotificationInfo.Shutdown;
					goto IL_101;
				case 3:
					this.m_type = OracleNotificationType.Change;
					this.m_source = OracleNotificationSource.Database;
					this.m_info = OracleNotificationInfo.Shutdown_any;
					goto IL_101;
				case 4:
					this.m_type = OracleNotificationType.Change;
					this.m_source = OracleNotificationSource.Database;
					this.m_info = OracleNotificationInfo.Drop;
					goto IL_101;
				case 5:
					this.m_type = OracleNotificationType.Subscribe;
					this.m_source = OracleNotificationSource.Subscription;
					this.m_info = OracleNotificationInfo.End;
					goto IL_101;
				case 6:
					this.m_type = OracleNotificationType.Change;
					this.m_source = OracleNotificationSource.Data;
					goto IL_101;
				case 7:
					this.m_type = OracleNotificationType.Query;
					this.m_source = OracleNotificationSource.Data;
					goto IL_101;
				}
				throw new NotSupportedException("Unsupported DB Change Event...");
				IL_101:;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x04001287 RID: 4743
		private const byte TBL_OPCODE_ALLROWS = 1;

		// Token: 0x04001288 RID: 4744
		private const byte TBL_OPCODE_ALLOPS = 0;

		// Token: 0x04001289 RID: 4745
		private const byte TBL_OPCODE_INSERT = 2;

		// Token: 0x0400128A RID: 4746
		private const byte TBL_OPCODE_UPDATE = 4;

		// Token: 0x0400128B RID: 4747
		private const byte TBL_OPCODE_DELETE = 8;

		// Token: 0x0400128C RID: 4748
		private const byte TBL_OPCODE_ALTER = 16;

		// Token: 0x0400128D RID: 4749
		private const byte TBL_OPCODE_DROP = 32;

		// Token: 0x0400128E RID: 4750
		private const byte TBL_OPCODE_UNKNOWN = 64;

		// Token: 0x0400128F RID: 4751
		private byte[] m_notificationInfoBuffer;

		// Token: 0x04001290 RID: 4752
		private short m_csId;

		// Token: 0x04001291 RID: 4753
		internal OracleNotificationType m_type;

		// Token: 0x04001292 RID: 4754
		internal OracleNotificationSource m_source;

		// Token: 0x04001293 RID: 4755
		internal OracleNotificationInfo m_info;

		// Token: 0x04001294 RID: 4756
		internal string[] m_resources;

		// Token: 0x04001295 RID: 4757
		internal DataTable m_details;

		// Token: 0x04001296 RID: 4758
		internal ByteBuffer m_byteBuffer;

		// Token: 0x04001297 RID: 4759
		private ArrayList resourceNamesList = new ArrayList();

		// Token: 0x04001298 RID: 4760
		internal List<long> m_queryIdList = new List<long>();

		// Token: 0x04001299 RID: 4761
		internal int m_regIdFromServer;
	}
}

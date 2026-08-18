using System;
using System.IO;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000184 RID: 388
	internal class Notification
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0009A9C0 File Offset: 0x00098BC0
		protected internal Notification(InputBuffer ibuf, ONS oems)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				string nextString = ibuf.NextString;
				if (nextString[0] == 'o')
				{
					flag = true;
					nextString = ibuf.NextString;
					nextString = ibuf.NextString;
				}
				ibuf.skipBytes(Notification.typeheader.Length);
				this.type_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.affectedcomponentsheader.Length);
				this.affectedComponents_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.affectednodesheader.Length);
				this.affectedNodes_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.generatingcomponentheader.Length);
				this.generatingComponent_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.generatingprocessheader.Length);
				this.generatingProcess_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.generatingnodeheader.Length);
				this.generatingNode_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.idheader.Length);
				this.id_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.creationtimeheader.Length);
				nextString = ibuf.NextString;
				if (nextString != null)
				{
					try
					{
						this.creationTime_Renamed_Field = long.Parse(nextString);
						goto IL_163;
					}
					catch (FormatException ex)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
							{
								"Notification::Notification(InputBuffer, ONS) failed. -" + ex.Message
							});
						}
						throw new IOException();
					}
				}
				this.creationTime_Renamed_Field = -1L;
				IL_163:
				ibuf.skipBytes(Notification.clusteridheader.Length);
				this.clusterId_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.clusternameheader.Length);
				this.clusterName_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.instanceidheader.Length);
				this.instanceId_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.instancenameheader.Length);
				this.instanceName_Renamed_Field = ibuf.NextString;
				ibuf.skipBytes(Notification.localonlyheader.Length);
				nextString = ibuf.NextString;
				if (nextString != null)
				{
					if (string.Equals(nextString, "true", StringComparison.InvariantCultureIgnoreCase))
					{
						this.localonly = true;
					}
					else
					{
						this.localonly = false;
					}
				}
				else
				{
					this.localonly = false;
				}
				if (!flag)
				{
					string nextString2 = ibuf.NextString;
				}
				ibuf.skipBytes(Notification.numpropertiesheader.Length);
				nextString = ibuf.NextString;
				if (nextString != null)
				{
					try
					{
						num2 = int.Parse(nextString);
						goto IL_270;
					}
					catch (FormatException ex2)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
							{
								"Notification::Notification(InputBuffer, ONS) failed. -" + ex2.Message
							});
						}
						throw new IOException();
					}
				}
				num2 = 0;
				IL_270:
				if (num2 > 0)
				{
					this.properties = new PropertyList(num2, ibuf);
				}
				else
				{
					this.properties = null;
				}
				ibuf.skipBytes(Notification.contentlengthheader.Length);
				nextString = ibuf.NextString;
				if (nextString != null)
				{
					try
					{
						num = int.Parse(nextString);
						goto IL_2C3;
					}
					catch (FormatException ex3)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
						throw new IOException();
					}
				}
				num = 0;
				IL_2C3:
				ibuf.skipBytes(14);
				nextString = ibuf.NextString;
				if (nextString != null)
				{
					int num3 = 1;
					int num4 = 0;
					int num5;
					while ((num5 = nextString.IndexOf(';', num4)) != -1)
					{
						num3++;
						num4 = num5 + 1;
					}
					this.recipients = new int[num3];
					num4 = 0;
					for (int i = 0; i < num3; i++)
					{
						num5 = nextString.IndexOf(';', num4);
						string s;
						if (num5 == -1)
						{
							s = nextString.Substring(num4);
						}
						else
						{
							s = nextString.Substring(num4, num5 - num4);
						}
						try
						{
							this.recipients[i] = int.Parse(s);
						}
						catch (FormatException ex4)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex4, null);
							this.recipients[i] = -1;
						}
						num4 = num5 + 1;
					}
				}
				else
				{
					this.recipients = null;
				}
				ibuf.skipBytes(2);
				if (num > 0)
				{
					this.body_Renamed_Field = new sbyte[num];
					ibuf.getBytes(this.body_Renamed_Field, num);
				}
				else
				{
					this.body_Renamed_Field = null;
				}
				this.deliveryTime_Renamed_Field = (DateTime.Now.Ticks - 621355968000000000L) / 10000L;
			}
			catch (Exception ex5)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex5, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0009AE6C File Offset: 0x0009906C
		protected internal virtual void send(OutputBuffer obuf, ONS oems, Connection connection)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				obuf.putBytes(Notification.eventmessageline, Notification.eventmessageline.Length);
				obuf.putBytes(Notification.versionheader, Notification.versionheader.Length);
				if (connection.ServerVersion == 3)
				{
					obuf.putBytes(Notification.versionheaderid3, Notification.versionheaderid.Length);
				}
				else
				{
					obuf.putBytes(Notification.versionheaderid, Notification.versionheaderid.Length);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.typeheader, Notification.typeheader.Length);
				if (this.type_Renamed_Field != null)
				{
					obuf.putString(this.type_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.affectedcomponentsheader, Notification.affectedcomponentsheader.Length);
				if (this.affectedComponents_Renamed_Field != null)
				{
					obuf.putString(this.affectedComponents_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.affectednodesheader, Notification.affectednodesheader.Length);
				if (this.affectedNodes_Renamed_Field != null)
				{
					obuf.putString(this.affectedNodes_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.generatingcomponentheader, Notification.generatingcomponentheader.Length);
				if (this.generatingComponent_Renamed_Field != null)
				{
					obuf.putString(this.generatingComponent_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.generatingprocessheader, Notification.generatingprocessheader.Length);
				if (this.generatingProcess_Renamed_Field != null)
				{
					obuf.putString(this.generatingProcess_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.generatingnodeheader, Notification.generatingnodeheader.Length);
				if (this.generatingNode_Renamed_Field != null)
				{
					obuf.putString(this.generatingNode_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.idheader, Notification.idheader.Length);
				if (this.id_Renamed_Field != null)
				{
					obuf.putString(this.id_Renamed_Field);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.creationtimeheader, Notification.creationtimeheader.Length);
				sbyte[] array = SupportClass.ToSByteArray(SupportClass.ToByteArray(this.creationTime_Renamed_Field.ToString()));
				obuf.putBytes(array, array.Length);
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.clusteridheader, Notification.clusteridheader.Length);
				if (oems.clusterid != null)
				{
					obuf.putString(oems.clusterid);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.clusternameheader, Notification.clusternameheader.Length);
				if (oems.clustername != null)
				{
					obuf.putString(oems.clustername);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.instanceidheader, Notification.instanceidheader.Length);
				if (oems.instanceid != null)
				{
					obuf.putString(oems.instanceid);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.instancenameheader, Notification.instancenameheader.Length);
				if (oems.instancename != null)
				{
					obuf.putString(oems.instancename);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.localonlyheader, Notification.localonlyheader.Length);
				if (this.localonly)
				{
					obuf.putBytes(Notification.truestring, Notification.truestring.Length);
				}
				else
				{
					obuf.putBytes(Notification.falsestring, Notification.falsestring.Length);
				}
				obuf.putBytes(Notification.crlf, 2);
				if (connection.ServerVersion == 3)
				{
					obuf.putBytes(Notification.clusteronlyheader, Notification.clusteronlyheader.Length);
					obuf.putBytes(Notification.falsestring, Notification.falsestring.Length);
					obuf.putBytes(Notification.crlf, 2);
				}
				obuf.putBytes(Notification.numpropertiesheader, Notification.numpropertiesheader.Length);
				if (this.properties != null)
				{
					array = SupportClass.ToSByteArray(SupportClass.ToByteArray(this.properties.num().ToString()));
					obuf.putBytes(array, array.Length);
					obuf.putBytes(Notification.crlf, 2);
					this.properties.write(obuf);
				}
				else
				{
					obuf.putByte(48);
					obuf.putBytes(Notification.crlf, 2);
				}
				if (this.body_Renamed_Field != null)
				{
					obuf.putBytes(Notification.contentlengthheader, Notification.contentlengthheader.Length);
					array = SupportClass.ToSByteArray(SupportClass.ToByteArray(this.body_Renamed_Field.Length.ToString()));
					obuf.putBytes(array, array.Length);
					obuf.putBytes(Notification.crlf, 2);
				}
				else
				{
					obuf.putBytes(Notification.contentlengthheader, Notification.contentlengthheader.Length);
					obuf.putByte(48);
					obuf.putBytes(Notification.crlf, 2);
				}
				obuf.putBytes(Notification.crlf, 2);
				if (this.body_Renamed_Field != null)
				{
					obuf.putBytes(this.body_Renamed_Field, this.body_Renamed_Field.Length);
				}
				obuf.flush();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0009B34C File Offset: 0x0009954C
		public virtual string type()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.type_Renamed_Field;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0009B384 File Offset: 0x00099584
		public virtual sbyte[] body()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.body_Renamed_Field;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0009B3BC File Offset: 0x000995BC
		static Notification()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				Notification.crlf = new sbyte[2];
				Notification.crlf[0] = 13;
				Notification.crlf[1] = 10;
				Notification.eventmessageline = new sbyte[22];
				Array.Copy(SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("POST /event HTTP/1.1").ToString())), 0, Notification.eventmessageline, 0, 20);
				Array.Copy(Notification.crlf, 0, Notification.eventmessageline, 20, 2);
				Notification.poststring = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("POST ").ToString()));
				Notification.headerseparator = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder(": ").ToString()));
				Notification.versionheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("Version: ").ToString()));
				Notification.versionheaderid = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder(Convert.ToString(4)).ToString()));
				Notification.typeheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("eventType: ").ToString()));
				Notification.affectedcomponentsheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("affectedComponents: ").ToString()));
				Notification.affectednodesheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("affectedNodes: ").ToString()));
				Notification.generatingcomponentheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("generatingComponent: ").ToString()));
				Notification.generatingnodeheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("generatingNode: ").ToString()));
				Notification.generatingprocessheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("generatingProcess: ").ToString()));
				Notification.idheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("eventId: ").ToString()));
				Notification.clusteridheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("clusterId: ").ToString()));
				Notification.clusternameheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("clusterName: ").ToString()));
				Notification.instanceidheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("instanceId: ").ToString()));
				Notification.instancenameheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("instanceName: ").ToString()));
				Notification.creationtimeheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("creationTime: ").ToString()));
				Notification.contentlengthheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("Content-Length: ").ToString()));
				Notification.numpropertiesheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("numberOfProperties: ").ToString()));
				Notification.localonlyheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("LocalOnly: ").ToString()));
				Notification.stampheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("stamp: ").ToString()));
				Notification.hostname = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("hostName: ").ToString()));
				Notification.truestring = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("true").ToString()));
				Notification.falsestring = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("false").ToString()));
				Notification.versionheaderid3 = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("3").ToString()));
				Notification.clusteronlyheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("ClusterOnly: ").ToString()));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x0400113C RID: 4412
		protected internal static sbyte[] eventmessageline;

		// Token: 0x0400113D RID: 4413
		protected internal static sbyte[] poststring;

		// Token: 0x0400113E RID: 4414
		protected internal static sbyte[] headerseparator;

		// Token: 0x0400113F RID: 4415
		protected internal static sbyte[] crlf;

		// Token: 0x04001140 RID: 4416
		protected internal static sbyte[] versionheader;

		// Token: 0x04001141 RID: 4417
		protected internal static sbyte[] versionheaderid;

		// Token: 0x04001142 RID: 4418
		protected internal static sbyte[] typeheader;

		// Token: 0x04001143 RID: 4419
		protected internal static sbyte[] affectedcomponentsheader;

		// Token: 0x04001144 RID: 4420
		protected internal static sbyte[] affectednodesheader;

		// Token: 0x04001145 RID: 4421
		protected internal static sbyte[] generatingcomponentheader;

		// Token: 0x04001146 RID: 4422
		protected internal static sbyte[] generatingnodeheader;

		// Token: 0x04001147 RID: 4423
		protected internal static sbyte[] generatingprocessheader;

		// Token: 0x04001148 RID: 4424
		protected internal static sbyte[] idheader;

		// Token: 0x04001149 RID: 4425
		protected internal static sbyte[] clusteridheader;

		// Token: 0x0400114A RID: 4426
		protected internal static sbyte[] clusternameheader;

		// Token: 0x0400114B RID: 4427
		protected internal static sbyte[] instanceidheader;

		// Token: 0x0400114C RID: 4428
		protected internal static sbyte[] instancenameheader;

		// Token: 0x0400114D RID: 4429
		protected internal static sbyte[] creationtimeheader;

		// Token: 0x0400114E RID: 4430
		protected internal static sbyte[] contentlengthheader;

		// Token: 0x0400114F RID: 4431
		protected internal static sbyte[] numpropertiesheader;

		// Token: 0x04001150 RID: 4432
		protected internal static sbyte[] localonlyheader;

		// Token: 0x04001151 RID: 4433
		protected internal static sbyte[] stampheader;

		// Token: 0x04001152 RID: 4434
		protected internal static sbyte[] hostname;

		// Token: 0x04001153 RID: 4435
		protected internal static sbyte[] truestring;

		// Token: 0x04001154 RID: 4436
		protected internal static sbyte[] falsestring;

		// Token: 0x04001155 RID: 4437
		protected internal static sbyte[] versionheaderid3;

		// Token: 0x04001156 RID: 4438
		protected internal static sbyte[] clusteronlyheader;

		// Token: 0x04001157 RID: 4439
		private string type_Renamed_Field;

		// Token: 0x04001158 RID: 4440
		private string affectedComponents_Renamed_Field;

		// Token: 0x04001159 RID: 4441
		private string affectedNodes_Renamed_Field;

		// Token: 0x0400115A RID: 4442
		private sbyte[] body_Renamed_Field;

		// Token: 0x0400115B RID: 4443
		protected internal bool localonly;

		// Token: 0x0400115C RID: 4444
		protected internal long deliveryTime_Renamed_Field;

		// Token: 0x0400115D RID: 4445
		protected internal string generatingComponent_Renamed_Field;

		// Token: 0x0400115E RID: 4446
		protected internal string generatingNode_Renamed_Field;

		// Token: 0x0400115F RID: 4447
		protected internal string generatingProcess_Renamed_Field;

		// Token: 0x04001160 RID: 4448
		protected internal string id_Renamed_Field;

		// Token: 0x04001161 RID: 4449
		protected internal long creationTime_Renamed_Field;

		// Token: 0x04001162 RID: 4450
		protected internal string clusterId_Renamed_Field;

		// Token: 0x04001163 RID: 4451
		protected internal string clusterName_Renamed_Field;

		// Token: 0x04001164 RID: 4452
		protected internal string instanceId_Renamed_Field;

		// Token: 0x04001165 RID: 4453
		protected internal string instanceName_Renamed_Field;

		// Token: 0x04001166 RID: 4454
		protected internal ONS oems;

		// Token: 0x04001167 RID: 4455
		private PropertyList properties;

		// Token: 0x04001168 RID: 4456
		protected internal int[] recipients;
	}
}

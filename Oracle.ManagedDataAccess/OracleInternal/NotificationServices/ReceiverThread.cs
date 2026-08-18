using System;
using System.IO;
using System.Text;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200018E RID: 398
	internal class ReceiverThread : SupportClass.ThreadClass
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x0009E3E8 File Offset: 0x0009C5E8
		protected internal ReceiverThread(NodeList list, Connection co)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.lock_Renamed = new object();
			this.shutdown_Renamed_Field = false;
			base.IsBackground = true;
			this.nodeList = list;
			this.oems = list.ons;
			this.connection = co;
			this.s = null;
			this.connection.ClientReceiver = this;
			this.id_Renamed_Field = new StringBuilder("ReceiverThread[" + this.connection.Id + "]").ToString();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0009E49C File Offset: 0x0009C69C
		private bool establishConnection(bool first)
		{
			ONSTcpClient onstcpClient = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			bool result;
			try
			{
				try
				{
					onstcpClient = this.connection.connect();
				}
				catch (TimeoutException ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
					return true;
				}
				if (onstcpClient == null)
				{
					return false;
				}
				result = false;
				try
				{
					Stream stream = onstcpClient.GetStream();
					OutputBuffer outputBuffer = new OutputBuffer(stream);
					if (this.connection.ServerVersion == 3)
					{
						outputBuffer.putBytes(ReceiverThread.connectmessage3, ReceiverThread.connectmessage3.Length);
					}
					else
					{
						outputBuffer.putBytes(ReceiverThread.connectmessage, ReceiverThread.connectmessage.Length);
					}
					outputBuffer.putBytes(ReceiverThread.selfid, ReceiverThread.selfid.Length);
					outputBuffer.putString(this.oems.oraclehome);
					outputBuffer.putBytes(ReceiverThread.endconnect, ReceiverThread.endconnect.Length);
					outputBuffer.flush();
					InputBuffer inputBuffer = new InputBuffer(new BufferedStream(onstcpClient.GetStream()));
					string nextString = inputBuffer.NextString;
					string nextString2 = inputBuffer.NextString;
					if (nextString2[0] != 'V' || nextString2[1] != 'e' || nextString2[2] != 'r' || nextString2[3] != 's' || nextString2[4] != 'i' || nextString2[5] != 'o' || nextString2[6] != 'n')
					{
						onstcpClient.Close();
						return false;
					}
					int num;
					try
					{
						num = int.Parse(nextString2.Substring(9, 1));
					}
					catch (FormatException ex2)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
						onstcpClient.Close();
						return false;
					}
					if (num != this.connection.ServerVersion)
					{
						this.connection.ServerVersion = num;
						onstcpClient.Close();
						return false;
					}
					string nextString3 = inputBuffer.NextString;
					string nextString4 = inputBuffer.NextString;
					string nextString5 = inputBuffer.NextString;
					string nextString6 = inputBuffer.NextString;
					inputBuffer.skipBytes(Notification.clusteridheader.Length);
					this.oems.clusterid = inputBuffer.NextString;
					inputBuffer.skipBytes(Notification.clusternameheader.Length);
					this.oems.clustername = inputBuffer.NextString;
					inputBuffer.skipBytes(Notification.instanceidheader.Length);
					this.oems.instanceid = inputBuffer.NextString;
					inputBuffer.skipBytes(Notification.instancenameheader.Length);
					this.oems.instancename = inputBuffer.NextString;
					string nextString7 = inputBuffer.NextString;
				}
				catch (TimeoutException ex3)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
					try
					{
						onstcpClient.Close();
					}
					catch (IOException ex4)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex4, null);
					}
					onstcpClient = null;
					result = true;
				}
				catch (IOException ex5)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex5, null);
					try
					{
						onstcpClient.Close();
					}
					catch (IOException ex6)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex6, null);
					}
					onstcpClient = null;
				}
				if (onstcpClient != null)
				{
					if (!this.oems.localConn)
					{
						this.connection.sender.flushSenderQueue();
					}
					this.connection.sender.quiescent(false);
					this.connection.setClientSocket(onstcpClient);
					this.connection.sender.wakeThread();
					this.s = onstcpClient;
					if (!first || !this.oems.localConn)
					{
						this.oems.resendSubscriptions(this.connection.sender);
					}
				}
			}
			catch (Exception ex7)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex7, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0009E8C4 File Offset: 0x0009CAC4
		public override void Run()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			SubscriptionNotification subscriptionNotification = null;
			InputBuffer inputBuffer = null;
			bool flag = false;
			bool first = true;
			bool flag2 = true;
			try
			{
				if (!this.oems.localConn && this.oems.remoteIOtimeout != 0)
				{
					subscriptionNotification = new SubscriptionNotification(99, "(", false);
				}
				while (!this.shutdown_Renamed_Field && !this.connection.shutdown)
				{
					if (this.s != null)
					{
						try
						{
							Stream stream = this.s.GetStream();
							inputBuffer = new InputBuffer(stream);
							goto IL_2B5;
						}
						catch (Exception ex)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
							break;
						}
						goto Block_8;
					}
					IL_2B5:
					if (this.shutdown_Renamed_Field || this.s == null)
					{
						flag = false;
						int num = 0;
						long num2 = 0L;
						long num3 = 500L;
						while (this.s == null && !this.shutdown_Renamed_Field)
						{
							if (this.establishConnection(first))
							{
								num2 = 5000L;
								this.connection.ScanDelay = 0L;
							}
							else if (num3 == 500L && this.connection.ServerVersion == 3)
							{
								num3 = 0L;
								continue;
							}
							if (this.s == null)
							{
								if (this.connection.scanDelay != 0L)
								{
									num3 = this.connection.scanDelay;
									this.connection.ScanDelay = 0L;
								}
								else if (num2 >= 5000L || flag2)
								{
									num2 = 0L;
									this.nodeList.checkConnections(this.connection);
									if (this.shutdown_Renamed_Field)
									{
										break;
									}
								}
								num2 += num3;
								num++;
								if (num > 30 && num3 < 5000L)
								{
									num3 += 1000L;
								}
								lock (this.lock_Renamed)
								{
									try
									{
										Monitor.Wait(this.lock_Renamed, TimeSpan.FromMilliseconds((double)num3));
									}
									catch (Exception ex2)
									{
										OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
									}
									goto IL_40A;
								}
								goto IL_3F6;
							}
							goto IL_3F6;
							IL_40A:
							flag2 = false;
							continue;
							IL_3F6:
							this.nodeList.establishedConnection(this.connection);
							first = false;
							goto IL_40A;
						}
						continue;
					}
					Block_8:
					string text;
					try
					{
						text = inputBuffer.NextString;
						if (flag)
						{
							flag = false;
						}
						if (text == null || text[0] != 'P' || text[1] != 'O' || text[2] != 'S' || text[3] != 'T' || text[4] != ' ')
						{
							goto IL_2B5;
						}
					}
					catch (TimeoutException ex3)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
						if (flag || subscriptionNotification == null)
						{
							this.s = null;
							text = null;
							this.connection.closeClientSocket();
						}
						else
						{
							flag = true;
							text = null;
							this.connection.sender.send(subscriptionNotification);
						}
					}
					catch (IOException ex4)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex4, null);
						this.s = null;
						text = null;
						this.connection.closeClientSocket();
					}
					if (text == null)
					{
						goto IL_2B5;
					}
					if (text[6] == 'e' && text[7] == 'v' && text[8] == 'e' && text[9] == 'n' && text[10] == 't')
					{
						Notification notification = this.readNotificationMessage(inputBuffer);
						if (notification != null)
						{
							this.nodeList.deliver(notification);
							goto IL_2B5;
						}
						this.s = null;
						this.connection.closeClientSocket();
						goto IL_2B5;
					}
					else if (text[6] == 's' && text[7] == 't' && text[8] == 'a' && text[9] == 't' && text[10] == 'u' && text[11] == 's')
					{
						if (!this.readStatusMessage(inputBuffer))
						{
							this.s = null;
							goto IL_2B5;
						}
						goto IL_2B5;
					}
					else
					{
						if (text[6] == 'q' && text[7] == 'u' && text[8] == 'i' && text[9] == 'e' && text[10] == 's' && text[11] == 'c')
						{
							while (text != null)
							{
								try
								{
									text = inputBuffer.NextString;
								}
								catch (IOException)
								{
									this.s = null;
									text = null;
									this.connection.closeClientSocket();
								}
							}
							this.connection.sender.quiescent(true);
							goto IL_2B5;
						}
						goto IL_2B5;
					}
				}
			}
			catch (Exception ex5)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex5, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0009EDFC File Offset: 0x0009CFFC
		protected internal virtual void shutdown()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.shutdown_Renamed_Field = true;
				this.connection.closeClientSocket();
				lock (this.lock_Renamed)
				{
					try
					{
						Monitor.Pulse(this.lock_Renamed);
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
					}
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
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

		// Token: 0x06000F37 RID: 3895 RVA: 0x0009EED0 File Offset: 0x0009D0D0
		private bool readStatusMessage(InputBuffer ibuf)
		{
			bool flag = true;
			int num = -1;
			bool success = false;
			string message = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				try
				{
					for (string nextString = ibuf.NextString; nextString != null; nextString = ibuf.NextString)
					{
						int num2 = nextString.IndexOf(':');
						if (num2 != -1)
						{
							string a = nextString.Substring(0, num2);
							string text = nextString.Substring(num2 + 2);
							if (string.Equals(a, "result", StringComparison.InvariantCultureIgnoreCase))
							{
								success = string.Equals(text, "success", StringComparison.InvariantCultureIgnoreCase);
							}
							else
							{
								if (string.Equals(a, "subscriberid", StringComparison.InvariantCultureIgnoreCase))
								{
									try
									{
										num = int.Parse(text);
										goto IL_B9;
									}
									catch (FormatException ex)
									{
										OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
										num = -1;
										goto IL_B9;
									}
								}
								if (string.Equals(a, "message", StringComparison.InvariantCultureIgnoreCase))
								{
									message = text;
								}
							}
						}
						IL_B9:;
					}
				}
				catch (IOException ex2)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
					flag = false;
				}
				if (num == -1)
				{
					flag = false;
				}
				if (flag && num != 99)
				{
					this.oems.handleSubscriptionReply(num, success, message);
				}
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return flag;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0009F03C File Offset: 0x0009D23C
		private Notification readNotificationMessage(InputBuffer ibuf)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			Notification result;
			try
			{
				result = new Notification(ibuf, this.oems);
			}
			catch (IOException ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				result = null;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0009F0BC File Offset: 0x0009D2BC
		static ReceiverThread()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				ReceiverThread.connectmessage3 = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("POST /connect HTTP/1.1\r\nVersion: 3\r\nFormFactor: ").ToString()));
				ReceiverThread.connectmessage = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("POST /connect HTTP/1.1\r\nVersion: 4\r\nFormFactor: ").ToString()));
				ReceiverThread.selfid = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("\r\nSelfId: java; Home=").ToString()));
				ReceiverThread.endconnect = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("\r\n\r\n").ToString()));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x040011CB RID: 4555
		private ONS oems;

		// Token: 0x040011CC RID: 4556
		private NodeList nodeList;

		// Token: 0x040011CD RID: 4557
		private Connection connection;

		// Token: 0x040011CE RID: 4558
		private object lock_Renamed;

		// Token: 0x040011CF RID: 4559
		private bool shutdown_Renamed_Field;

		// Token: 0x040011D0 RID: 4560
		private ONSTcpClient s;

		// Token: 0x040011D1 RID: 4561
		private string id_Renamed_Field;

		// Token: 0x040011D2 RID: 4562
		private static sbyte[] connectmessage3;

		// Token: 0x040011D3 RID: 4563
		private static sbyte[] connectmessage;

		// Token: 0x040011D4 RID: 4564
		private static sbyte[] selfid;

		// Token: 0x040011D5 RID: 4565
		private static sbyte[] endconnect;
	}
}

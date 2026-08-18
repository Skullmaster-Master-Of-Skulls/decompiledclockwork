using System;
using System.Collections.Concurrent;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001AA RID: 426
	internal class NotificationHandler
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x000A476C File Offset: 0x000A296C
		internal NotificationHandler(OracleCommunication dataEndPoint)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_dataEP = dataEndPoint;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000A47F0 File Offset: 0x000A29F0
		internal void ProcessBufferedNSDataPackets()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool flag = true;
			try
			{
				this.m_indexOfArraySegmentToBeRead = 0;
				this.m_lstOfArraySegments_Count = 0;
				while (!this.m_bNoDataAvailInNetwork || flag || this.m_lstOfOraBufs.Count > 0)
				{
					if (!this.m_NSPacketDataBuffer.HasRemaining)
					{
						flag = this.ReadFromBufferedData();
					}
					if (flag)
					{
						this.UnmarshalNSDataPacket();
					}
				}
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
				if (this.m_currentOB != null)
				{
					this.m_currentOB.ReturnToPool();
					this.m_currentOB = null;
					this.m_lstOfArraySegments = null;
					this.m_lstOfArraySegments_Count = 0;
				}
				NotificationHandler.m_bufMgr.PutNotificationOraBufPool(this.m_dataEP);
				this.m_dataEnqueuedEvnt.Set();
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000A48EC File Offset: 0x000A2AEC
		private void SendProcessedNotifications()
		{
			try
			{
				for (;;)
				{
					bool flag = false;
					while (!this.m_lstOfTimeoutNotifs.IsEmpty)
					{
						NotificationInfo obj;
						this.m_lstOfTimeoutNotifs.TryDequeue(out obj);
						OracleNotificationManager.s_sendNtfDetailsToUpperLayer(obj);
						flag = true;
					}
					if (!this.m_lstOfNormalNotifs.IsEmpty)
					{
						NotificationInfo obj;
						this.m_lstOfNormalNotifs.TryDequeue(out obj);
						OracleNotificationManager.s_sendNtfDetailsToUpperLayer(obj);
						flag = true;
					}
					if (!flag)
					{
						this.m_dataEnqueuedEvnt.Reset();
						if (this.m_lstOfNormalNotifs.Count == 0 && this.m_lstOfTimeoutNotifs.Count == 0)
						{
							this.m_dataEnqueuedEvnt.Wait();
							if (this.m_lstOfNormalNotifs.Count == 0 && this.m_lstOfTimeoutNotifs.Count == 0)
							{
								break;
							}
						}
						else
						{
							this.m_dataEnqueuedEvnt.Set();
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
			}
			finally
			{
				this.m_bNotifSendingThreadActive = false;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000A49EC File Offset: 0x000A2BEC
		internal void ProcessNotification()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_dataEP.Answer();
				this.m_dataEP.Accept(null);
				this.ReadFromNetwork();
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

		// Token: 0x06000FEA RID: 4074 RVA: 0x000A4A78 File Offset: 0x000A2C78
		internal void ReadFromNetwork()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				NotificationHandler.m_bufMgr.GetNotificationOraBufPool(this.m_dataEP);
				for (;;)
				{
					OraBuf oraBuf = this.m_dataEP.OraBufPool.Get(this.m_dataEP.SDU, this.m_dataEP, true);
					this.m_dataEP.m_sessionCtx.m_readerStream.Read(oraBuf);
					if (oraBuf.the_ByteSegments_Count > 0)
					{
						if (this.m_lstOfOraBufs == null)
						{
							this.m_lstOfOraBufs = new ConcurrentQueue<OraBuf>();
							this.m_lstOfOraBufs.Enqueue(oraBuf);
							new Thread(new ThreadStart(this.ProcessBufferedNSDataPackets))
							{
								IsBackground = true
							}.Start();
						}
						else
						{
							this.m_lstOfOraBufs.Enqueue(oraBuf);
						}
						this.m_dataAvailableEvnt.Set();
					}
				}
			}
			catch (NetworkException)
			{
				this.m_bNoDataAvailInNetwork = true;
			}
			catch (Exception ex)
			{
				this.m_bNoDataAvailInNetwork = true;
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
			}
			finally
			{
				this.m_dataAvailableEvnt.Set();
				this.m_dataEnqueuedEvnt.Set();
				this.m_dataEP.Disconnect();
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000A4BD4 File Offset: 0x000A2DD4
		internal bool ReadFromBufferedData()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result = false;
			try
			{
				if (this.m_indexOfArraySegmentToBeRead == this.m_lstOfArraySegments_Count)
				{
					this.m_lstOfArraySegments_Count = 0;
					this.m_indexOfArraySegmentToBeRead = 0;
					if (this.m_currentOB != null)
					{
						this.m_currentOB.ReturnToPool();
						this.m_currentOB = null;
						this.m_lstOfArraySegments = null;
						this.m_lstOfArraySegments_Count = 0;
					}
					if (!this.m_bNoDataAvailInNetwork && this.m_lstOfOraBufs.Count == 0)
					{
						this.m_dataAvailableEvnt.Reset();
						this.m_dataAvailableEvnt.Wait();
					}
					if (!this.m_lstOfOraBufs.IsEmpty)
					{
						this.m_lstOfOraBufs.TryDequeue(out this.m_currentOB);
					}
					if (this.m_currentOB != null)
					{
						this.m_lstOfArraySegments = this.m_currentOB.the_ByteSegments;
						this.m_lstOfArraySegments_Count = this.m_currentOB.the_ByteSegments_Count;
					}
				}
				if (this.m_lstOfArraySegments_Count > 0)
				{
					result = true;
					this.m_NSPacketDataBuffer.m_byteBuffer = this.m_lstOfArraySegments[this.m_indexOfArraySegmentToBeRead].Array;
					this.m_NSPacketDataBuffer.m_position = this.m_lstOfArraySegments[this.m_indexOfArraySegmentToBeRead].Offset;
					this.m_NSPacketDataBuffer.m_limit = this.m_lstOfArraySegments[this.m_indexOfArraySegmentToBeRead].Count + this.m_lstOfArraySegments[this.m_indexOfArraySegmentToBeRead].Offset;
					this.m_indexOfArraySegmentToBeRead++;
				}
				else
				{
					result = false;
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
			return result;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000A4DA0 File Offset: 0x000A2FA0
		internal void UnmarshalNSDataPacket()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				int num = (int)this.ReadShort();
				int num2 = this.ReadInt();
				this.ReadByte();
				this.ReadInt();
				short csId = this.ReadShort();
				this.ReadByte();
				this.ReadInt();
				this.ReadShort();
				this.ReadByte();
				this.ReadInt();
				this.ReadShort();
				int num3 = (num2 - 21) / 9;
				int[] array = new int[num3];
				for (int i = 0; i < num3; i++)
				{
					this.ReadByte();
					int num4 = this.ReadInt();
					byte[] array2 = new byte[num4];
					this.ReadBuffer(array2, 0, num4);
					for (int j = 0; j < num4; j++)
					{
						if (j < 4)
						{
							array[i] |= (int)(array2[j] & byte.MaxValue) << 8 * (num4 - j - 1);
						}
					}
				}
				int num5 = 2;
				byte[] array3 = null;
				if (num >= 2)
				{
					this.ReadShort();
					if (num5 != 2)
					{
						throw new NotSupportedException("Unsupported Unknown Namespace");
					}
					int num6 = this.ReadInt();
					array3 = new byte[num6];
					this.ReadBuffer(array3, 0, num6);
				}
				if (num >= 3)
				{
					this.ReadShort();
					this.ReadInt();
					this.ReadByte();
					this.ReadInt();
					this.ReadShort();
				}
				if (num > 3)
				{
					throw new Exception("Internal Error: more than 3 Handles received from DB");
				}
				if (num5 == 2)
				{
					NotificationInfo item = new NotificationInfo(csId, num3, array, num >= 3, array3);
					if (num >= 3)
					{
						this.m_lstOfTimeoutNotifs.Enqueue(item);
					}
					else
					{
						this.m_lstOfNormalNotifs.Enqueue(item);
					}
					if (!this.m_dataEnqueuedEvnt.IsSet)
					{
						this.m_dataEnqueuedEvnt.Set();
					}
					if (!this.m_bNotifSendingThreadActive)
					{
						this.m_bNotifSendingThreadActive = true;
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
							{
								"(NOTIF)(New thread request)"
							});
						}
						ThreadPool.QueueUserWorkItem(delegate(object obj)
						{
							this.SendProcessedNotifications();
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
								{
									"(NOTIF)(current thread finished)"
								});
							}
						});
					}
				}
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

		// Token: 0x06000FED RID: 4077 RVA: 0x000A5018 File Offset: 0x000A3218
		private byte ReadByte()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			byte result = 0;
			try
			{
				if (this.m_NSPacketDataBuffer.HasRemaining)
				{
					result = this.m_NSPacketDataBuffer.GetByte();
				}
				else
				{
					bool flag = this.ReadFromBufferedData();
					if (flag)
					{
						result = this.m_NSPacketDataBuffer.GetByte();
					}
				}
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
			return result;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000A50BC File Offset: 0x000A32BC
		private short ReadShort()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			short result = 0;
			try
			{
				if (this.m_NSPacketDataBuffer.Remaining >= 2)
				{
					result = this.m_NSPacketDataBuffer.GetShort();
				}
				else
				{
					int num = (int)(this.ReadByte() & byte.MaxValue);
					int num2 = (int)(this.ReadByte() & byte.MaxValue);
					result = (short)((num << 8 | num2) & 65535);
				}
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
			return result;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000A5170 File Offset: 0x000A3370
		private int ReadInt()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result = 0;
			try
			{
				if (this.m_NSPacketDataBuffer.Remaining >= 4)
				{
					result = this.m_NSPacketDataBuffer.GetInt();
				}
				else
				{
					int num = (int)(this.ReadByte() & byte.MaxValue);
					int num2 = (int)(this.ReadByte() & byte.MaxValue);
					int num3 = (int)(this.ReadByte() & byte.MaxValue);
					int num4 = (int)(this.ReadByte() & byte.MaxValue);
					result = (num << 24 | num2 << 16 | num3 << 8 | num4);
				}
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
			return result;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000A5248 File Offset: 0x000A3448
		private void ReadBuffer(byte[] buff, int offset, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_NSPacketDataBuffer.Remaining >= length)
				{
					this.m_NSPacketDataBuffer.GetBuffer(buff, offset, length);
				}
				else
				{
					bool flag = false;
					int num = 0;
					int remaining = this.m_NSPacketDataBuffer.Remaining;
					this.m_NSPacketDataBuffer.GetBuffer(buff, offset, remaining);
					offset += remaining;
					num += remaining;
					while (!flag)
					{
						remaining = this.m_NSPacketDataBuffer.Remaining;
						int num2 = Math.Min(remaining, length - num);
						bool flag2 = true;
						if (remaining == 0)
						{
							flag2 = this.ReadFromBufferedData();
						}
						if (!flag2)
						{
							flag = true;
						}
						else
						{
							this.m_NSPacketDataBuffer.GetBuffer(buff, offset, num2);
							offset += num2;
							num += num2;
							if (num == length)
							{
								flag = true;
							}
						}
					}
				}
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

		// Token: 0x0400129F RID: 4767
		private const int NS_HEADER_SIZE = 10;

		// Token: 0x040012A0 RID: 4768
		private const int INTERRUPT_SIGNAL = -2;

		// Token: 0x040012A1 RID: 4769
		private const int NSPTCN = 1;

		// Token: 0x040012A2 RID: 4770
		private const int NSPTDA = 6;

		// Token: 0x040012A3 RID: 4771
		private int m_indexOfArraySegmentToBeRead;

		// Token: 0x040012A4 RID: 4772
		internal OracleCommunication m_dataEP;

		// Token: 0x040012A5 RID: 4773
		private ByteBuffer m_NSPacketDataBuffer = new ByteBuffer();

		// Token: 0x040012A6 RID: 4774
		private ConcurrentQueue<OraBuf> m_lstOfOraBufs;

		// Token: 0x040012A7 RID: 4775
		private ConcurrentQueue<NotificationInfo> m_lstOfNormalNotifs = new ConcurrentQueue<NotificationInfo>();

		// Token: 0x040012A8 RID: 4776
		private ConcurrentQueue<NotificationInfo> m_lstOfTimeoutNotifs = new ConcurrentQueue<NotificationInfo>();

		// Token: 0x040012A9 RID: 4777
		private bool m_bNotifSendingThreadActive;

		// Token: 0x040012AA RID: 4778
		private bool m_bNoDataAvailInNetwork;

		// Token: 0x040012AB RID: 4779
		internal OraBuf m_currentOB;

		// Token: 0x040012AC RID: 4780
		private OraArraySegment[] m_lstOfArraySegments;

		// Token: 0x040012AD RID: 4781
		private int m_lstOfArraySegments_Count;

		// Token: 0x040012AE RID: 4782
		private ManualResetEventSlim m_dataAvailableEvnt = new ManualResetEventSlim(false);

		// Token: 0x040012AF RID: 4783
		private ManualResetEventSlim m_dataEnqueuedEvnt = new ManualResetEventSlim(false);

		// Token: 0x040012B0 RID: 4784
		internal static NotificationBufferManager m_bufMgr = new NotificationBufferManager();
	}
}

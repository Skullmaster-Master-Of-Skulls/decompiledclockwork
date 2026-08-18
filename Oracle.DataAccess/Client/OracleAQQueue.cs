using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200003D RID: 61
	public class OracleAQQueue : IDisposable
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0001D891 File Offset: 0x0001C891
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0001D8B2 File Offset: 0x0001C8B2
		public OracleConnection Connection
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_connection;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_connection = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0001D8D4 File Offset: 0x0001C8D4
		public string Name
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_name;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0001D8F5 File Offset: 0x0001C8F5
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0001D916 File Offset: 0x0001C916
		public string UdtTypeName
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_udtTypeName;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_udtTypeName = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0001D938 File Offset: 0x0001C938
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0001D959 File Offset: 0x0001C959
		public string[] NotificationConsumers
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_notificationConsumers;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_eventWrapper.InvocationListLength != 0)
				{
					throw new InvalidOperationException();
				}
				this.m_notificationConsumers = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0001D98E File Offset: 0x0001C98E
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0001D9AF File Offset: 0x0001C9AF
		public OracleAQMessageType MessageType
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_messageType;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (value != OracleAQMessageType.Raw && value != OracleAQMessageType.Udt && value != OracleAQMessageType.Xml)
				{
					throw new ArgumentOutOfRangeException("MessageType");
				}
				this.m_messageType = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0001D9E8 File Offset: 0x0001C9E8
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0001DA09 File Offset: 0x0001CA09
		public OracleAQEnqueueOptions EnqueueOptions
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_aqEnqOptions;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_aqEnqOptions = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0001DA2B File Offset: 0x0001CA2B
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0001DA4C File Offset: 0x0001CA4C
		public OracleAQDequeueOptions DequeueOptions
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.m_aqDeqOptions;
			}
			set
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.m_aqDeqOptions = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0001DA6E File Offset: 0x0001CA6E
		public OracleNotificationRequest Notification
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (this.m_NTFNReq == null)
				{
					this.m_NTFNReq = new OracleNotificationRequest(false, 0L, false, false, OracleAQNotificationGroupingType.Summary, 600);
				}
				return this.m_NTFNReq;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000288 RID: 648 RVA: 0x0001DAB0 File Offset: 0x0001CAB0
		// (remove) Token: 0x06000289 RID: 649 RVA: 0x0001DB0C File Offset: 0x0001CB0C
		public event OracleAQMessageAvailableEventHandler MessageAvailable
		{
			add
			{
				lock (this.m_lockObj)
				{
					this.m_eventWrapper.OnMessageAvailable += value;
					if (this.m_eventWrapper.InvocationListLength == 1)
					{
						this.SubscriptionRegister();
					}
				}
			}
			remove
			{
				this.m_eventWrapper.OnMessageAvailable -= value;
				if (this.m_eventWrapper.InvocationListLength == 0)
				{
					this.SubscriptionUnRegister();
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001DB2D File Offset: 0x0001CB2D
		static OracleAQQueue()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			OracleAQQueue.m_subscriptionMap = Hashtable.Synchronized(new Hashtable());
			OracleAQQueue.s_onAQNTFNOpsCallback = new OnAQNTFNCallback(OracleAQQueue.OnAQNTFNOpsCallback_fn);
			OpsAQ.RegisterNotificationCallback(OracleAQQueue.s_onAQNTFNOpsCallback);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001DB66 File Offset: 0x0001CB66
		public OracleAQQueue(string name) : this(name, null, OracleAQMessageType.Raw, null, false)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001DB73 File Offset: 0x0001CB73
		public OracleAQQueue(string name, OracleConnection con) : this(name, con, OracleAQMessageType.Raw)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001DB7E File Offset: 0x0001CB7E
		public OracleAQQueue(string name, OracleConnection con, OracleAQMessageType messageType) : this(name, con, messageType, null)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0001DB8A File Offset: 0x0001CB8A
		public OracleAQQueue(string name, OracleConnection con, OracleAQMessageType messageType, string udtTypeName) : this(name, con, messageType, udtTypeName, true)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001DB98 File Offset: 0x0001CB98
		private OracleAQQueue(string name, OracleConnection con, OracleAQMessageType messageType, string udtTypeName, bool checkConnReference)
		{
			if (con == null && checkConnReference)
			{
				throw new ArgumentNullException("con");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"name"
				}));
			}
			if (messageType != OracleAQMessageType.Raw && messageType != OracleAQMessageType.Udt && messageType != OracleAQMessageType.Xml)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"messageType"
				}));
			}
			this.m_name = name;
			this.m_connection = con;
			this.m_messageType = messageType;
			this.m_udtTypeName = udtTypeName;
			this.Init();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001DCAC File Offset: 0x0001CCAC
		private void SetConnection(OracleConnection con)
		{
			if (this.m_isConSet && this.m_conSignature != this.m_connection.m_conSignature)
			{
				this.m_enqOptsInfo = 65535;
				this.m_deqOptsInfo = 65535;
				if (!(this.m_OCIAQEnqOptions != IntPtr.Zero) && !(this.m_OCIAQDeqOptions != IntPtr.Zero))
				{
					if (!(this.m_OCIAQMsgProperties != IntPtr.Zero))
					{
						goto IL_97;
					}
				}
				try
				{
					OpsAQ.FreeCachedDesc(ref this.m_OCIAQEnqOptions, ref this.m_OCIAQDeqOptions, ref this.m_OCIAQMsgProperties);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				IL_97:
				if (this.m_opsErrCtx != IntPtr.Zero)
				{
					try
					{
						OpsErr.FreeCtx(ref this.m_opsErrCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					this.m_opsErrCtx = IntPtr.Zero;
				}
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
						throw;
					}
				}
			}
			this.m_opsConCtx = con.m_opoConCtx.opsConCtx;
			int num = 0;
			try
			{
				num = OpsCon.AddRef(this.m_opsConCtx);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				this.m_opsConCtx = IntPtr.Zero;
				throw;
			}
			if (num <= 1)
			{
				this.m_opsConCtx = IntPtr.Zero;
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				OpsErr.AllocCtx(ref this.m_opsErrCtx, this.m_opsConCtx);
			}
			catch (Exception ex5)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex5);
				}
				throw;
			}
			this.m_conSignature = con.m_conSignature;
			this.m_isConSet = true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001DE98 File Offset: 0x0001CE98
		private void Init()
		{
			this.m_opoAQMsgPropsRefCtx = new OpoAQMsgPropsRefCtx();
			int num = 0;
			try
			{
				num = OpsAQ.AllocValCtx(out this.m_pOpoAQMsgPropsValCtx, out this.m_pOpoAQMsgValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0001DF0C File Offset: 0x0001CF0C
		internal unsafe static void OnAQNTFNOpsCallback_fn(IntPtr pSubscrhp, IntPtr pDesc, IntPtr ctx, OpoAQMsgPropsValCtx* pOpoAQMsgPropsValCtx, OpoAQMsgValCtx* pOpoAQMsgValCtx)
		{
			int num = 0;
			OpoAQMsgPropsRefCtx opoAQMsgPropsRefCtx = new OpoAQMsgPropsRefCtx();
			OpoAQNtfnDataRefCtx opoAQNtfnDataRefCtx = new OpoAQNtfnDataRefCtx();
			OracleAQNotificationType oracleAQNotificationType = OracleAQNotificationType.Regular;
			int availableMessages = 0;
			OpoAQMsgIdValCtx* ptr = null;
			int num2 = 0;
			string text = null;
			OracleAQMessageAvailableEventArgs oracleAQMessageAvailableEventArgs = new OracleAQMessageAvailableEventArgs();
			OracleAQQueue.NtfnInfo ntfnInfo = OracleAQQueue.m_subscriptionMap[ctx] as OracleAQQueue.NtfnInfo;
			if (ntfnInfo == null)
			{
				return;
			}
			lock (ntfnInfo)
			{
				if (!(OracleAQQueue.m_subscriptionMap[ctx] is OracleAQQueue.NtfnInfo))
				{
					return;
				}
				try
				{
					num = OpsAQ.ProcessNtfn(pSubscrhp, pDesc, ctx, pOpoAQMsgValCtx, pOpoAQMsgPropsValCtx, ref opoAQMsgPropsRefCtx, ref oracleAQNotificationType, ref availableMessages, out ptr, ref num2, ref opoAQNtfnDataRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					return;
				}
			}
			text = opoAQNtfnDataRefCtx.queueName;
			string consumerName = opoAQNtfnDataRefCtx.consumerName;
			if (oracleAQNotificationType == OracleAQNotificationType.Regular)
			{
				if (num == 0)
				{
					oracleAQMessageAvailableEventArgs.m_notificationType = OracleAQNotificationType.Regular;
					oracleAQMessageAvailableEventArgs.m_availableMessages = 1;
					oracleAQMessageAvailableEventArgs.m_queueName = text;
					oracleAQMessageAvailableEventArgs.m_consumerName = consumerName;
					oracleAQMessageAvailableEventArgs.m_deliveryMode = (OracleAQMessageDeliveryMode)pOpoAQMsgPropsValCtx->deliveryMode;
					oracleAQMessageAvailableEventArgs.m_messageId = new byte[1][];
					oracleAQMessageAvailableEventArgs.m_messageId[0] = new byte[pOpoAQMsgValCtx->msgIdLen];
					Marshal.Copy(pOpoAQMsgValCtx->pMsgId, oracleAQMessageAvailableEventArgs.m_messageId[0], 0, pOpoAQMsgValCtx->msgIdLen);
					oracleAQMessageAvailableEventArgs.m_correlation = opoAQMsgPropsRefCtx.correlationId;
					oracleAQMessageAvailableEventArgs.m_delay = pOpoAQMsgPropsValCtx->delay;
					oracleAQMessageAvailableEventArgs.m_exceptionQueue = opoAQMsgPropsRefCtx.exceptionQueue;
					oracleAQMessageAvailableEventArgs.m_expiration = pOpoAQMsgPropsValCtx->expiration;
					oracleAQMessageAvailableEventArgs.m_priority = pOpoAQMsgPropsValCtx->priority;
					oracleAQMessageAvailableEventArgs.m_enqueueTime = new DateTime(pOpoAQMsgPropsValCtx->year, pOpoAQMsgPropsValCtx->month, pOpoAQMsgPropsValCtx->day, pOpoAQMsgPropsValCtx->hour, pOpoAQMsgPropsValCtx->min, pOpoAQMsgPropsValCtx->sec);
					oracleAQMessageAvailableEventArgs.m_state = (OracleAQMessageState)pOpoAQMsgPropsValCtx->msgState;
					if (pOpoAQMsgPropsValCtx->origMsgIdLen > 0)
					{
						oracleAQMessageAvailableEventArgs.m_originalMessageId = new byte[pOpoAQMsgPropsValCtx->origMsgIdLen];
						Marshal.Copy(pOpoAQMsgPropsValCtx->pOrigMsgId, oracleAQMessageAvailableEventArgs.m_originalMessageId, 0, pOpoAQMsgPropsValCtx->origMsgIdLen);
					}
					if (opoAQMsgPropsRefCtx.senderId.name != null && opoAQMsgPropsRefCtx.senderId.address == null)
					{
						oracleAQMessageAvailableEventArgs.m_senderId = new OracleAQAgent(opoAQMsgPropsRefCtx.senderId.name);
					}
					else if (opoAQMsgPropsRefCtx.senderId.name != null && opoAQMsgPropsRefCtx.senderId.address != null)
					{
						oracleAQMessageAvailableEventArgs.m_senderId = new OracleAQAgent(opoAQMsgPropsRefCtx.senderId.name, opoAQMsgPropsRefCtx.senderId.address);
					}
				}
				opoAQMsgPropsRefCtx = null;
				opoAQNtfnDataRefCtx = null;
			}
			else if (OracleAQNotificationType.Timeout == oracleAQNotificationType)
			{
				oracleAQMessageAvailableEventArgs.m_notificationType = OracleAQNotificationType.Timeout;
				oracleAQMessageAvailableEventArgs.m_availableMessages = 0;
				if (text == null && ntfnInfo != null)
				{
					oracleAQMessageAvailableEventArgs.m_queueName = ntfnInfo.m_queueName;
				}
				else
				{
					oracleAQMessageAvailableEventArgs.m_queueName = text;
				}
				if (consumerName == null && ntfnInfo != null)
				{
					oracleAQMessageAvailableEventArgs.m_consumerName = ntfnInfo.m_consumerName;
				}
				else
				{
					oracleAQMessageAvailableEventArgs.m_consumerName = consumerName;
				}
			}
			else if (OracleAQNotificationType.Group == oracleAQNotificationType)
			{
				oracleAQMessageAvailableEventArgs.m_availableMessages = availableMessages;
				oracleAQMessageAvailableEventArgs.m_notificationType = OracleAQNotificationType.Group;
				if (text == null && ntfnInfo != null)
				{
					oracleAQMessageAvailableEventArgs.m_queueName = ntfnInfo.m_queueName;
				}
				else
				{
					oracleAQMessageAvailableEventArgs.m_queueName = text;
				}
				if (consumerName == null && ntfnInfo != null)
				{
					oracleAQMessageAvailableEventArgs.m_consumerName = ntfnInfo.m_consumerName;
				}
				else
				{
					oracleAQMessageAvailableEventArgs.m_consumerName = consumerName;
				}
				oracleAQMessageAvailableEventArgs.m_messageId = new byte[num2][];
				for (int i = 0; i < num2; i++)
				{
					oracleAQMessageAvailableEventArgs.m_messageId[i] = new byte[ptr[i].msgIdLen];
					Marshal.Copy(ptr[i].pMsgId, oracleAQMessageAvailableEventArgs.m_messageId[i], 0, ptr[i].msgIdLen);
				}
				try
				{
					OpsAQ.FreeMsgIdValCtxArray(ref ptr);
				}
				catch (Exception ex2)
				{
					Console.WriteLine(ex2.Message);
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
				}
			}
			if (num == 0)
			{
				OracleAQQueue.EventWrapper eventWrapper = null;
				if (ntfnInfo != null)
				{
					eventWrapper = ntfnInfo.m_eventWrapper;
				}
				if (eventWrapper != null)
				{
					eventWrapper.FireEvent(text, oracleAQMessageAvailableEventArgs);
				}
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001E31C File Offset: 0x0001D31C
		public void Enqueue(OracleAQMessage msg)
		{
			this.Enqueue(msg, this.m_aqEnqOptions);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001E32C File Offset: 0x0001D32C
		public unsafe void Enqueue(OracleAQMessage msg, OracleAQEnqueueOptions aqEnqOptions)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::Enqueue()\n"
				});
			}
			OracleXmlType oracleXmlType = null;
			int num = 0;
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature)
			{
				this.SetConnection(this.m_connection);
			}
			if (msg.Payload is OracleXmlType && ((OracleXmlType)msg.Payload).m_conSignature != this.m_conSignature)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"msg.Payload"
				}));
			}
			if (aqEnqOptions != null)
			{
				if (this.m_pOpoAQEnqOptionsValCtx == null)
				{
					try
					{
						num = OpsAQ.AllocEnqOptValCtx(out this.m_pOpoAQEnqOptionsValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
				this.GetModEnqOptDescAttribFlag(aqEnqOptions, ref this.m_enqOptsInfo);
			}
			IntPtr zero = IntPtr.Zero;
			if (msg.m_msgPropsModified)
			{
				this.m_pOpoAQMsgPropsValCtx->isNull = 0;
				this.m_pOpoAQMsgPropsValCtx->delay = msg.m_delay;
				this.m_pOpoAQMsgPropsValCtx->expiration = msg.m_expiration;
				this.m_pOpoAQMsgPropsValCtx->priority = msg.m_priority;
				if (msg.m_recipients != null)
				{
					this.m_pOpoAQMsgPropsValCtx->numRecipients = msg.m_recipients.Length;
					OpoAQAgentRefCtx[] array = new OpoAQAgentRefCtx[msg.m_recipients.Length];
					for (int i = 0; i < msg.m_recipients.Length; i++)
					{
						array[i] = default(OpoAQAgentRefCtx);
						array[i].name = msg.m_recipients[i].m_name;
						array[i].address = msg.m_recipients[i].m_address;
					}
					num = OpsAQ.PrepareAgentArray(this.m_opsConCtx, this.m_opsErrCtx, ref array, msg.m_recipients.Length, out zero);
					this.m_pOpoAQMsgPropsValCtx->pRecipients = zero;
				}
				else
				{
					this.m_pOpoAQMsgPropsValCtx->numRecipients = 0;
				}
				if (msg.m_senderId != null)
				{
					this.m_opoAQMsgPropsRefCtx.senderId.name = msg.m_senderId.Name;
					this.m_opoAQMsgPropsRefCtx.senderId.address = msg.m_senderId.Address;
				}
				else
				{
					this.m_opoAQMsgPropsRefCtx.senderId.name = null;
					this.m_opoAQMsgPropsRefCtx.senderId.address = null;
				}
				this.m_opoAQMsgPropsRefCtx.correlationId = msg.m_correlation;
				this.m_opoAQMsgPropsRefCtx.exceptionQueue = msg.m_exceptionQueue;
			}
			else
			{
				this.m_pOpoAQMsgPropsValCtx->isNull = 1;
			}
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			byte[] array2 = null;
			if (OracleAQMessageType.Raw == this.m_messageType)
			{
				this.m_pOpoAQMsgValCtx->payloadType = 1;
				if (msg.m_payload is OracleBinary)
				{
					array2 = ((OracleBinary)msg.m_payload).m_value;
					this.m_pOpoAQMsgValCtx->rawPayloadLen = array2.Length;
				}
				else if (msg.m_payload is byte[])
				{
					array2 = (byte[])msg.m_payload;
					this.m_pOpoAQMsgValCtx->rawPayloadLen = array2.Length;
				}
				else
				{
					if (msg.m_payload != null)
					{
						throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
						{
							"msg"
						}));
					}
					array2 = null;
					this.m_pOpoAQMsgValCtx->rawPayloadLen = 0;
				}
			}
			else if (OracleAQMessageType.Udt == this.m_messageType)
			{
				if (msg.m_payload is IOracleCustomType && !((INullable)msg.m_payload).IsNull)
				{
					this.SetUDTFromCustomObject((IOracleCustomType)msg.m_payload, this.m_pOpoAQMsgValCtx);
				}
				else
				{
					if (msg.m_payload != null && (!(msg.m_payload is IOracleCustomType) || !((INullable)msg.m_payload).IsNull))
					{
						throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
						{
							"msg"
						}));
					}
					this.SetNullUDTFromCustomObject(this.m_udtTypeName, this.m_pOpoAQMsgValCtx);
				}
				this.m_pOpoAQMsgValCtx->payloadType = 2;
			}
			else if (OracleAQMessageType.Xml == this.m_messageType)
			{
				try
				{
					this.m_pOpoAQMsgValCtx->payloadType = 3;
					if (msg.Payload != null)
					{
						if (msg.Payload is OracleXmlType)
						{
							num = (msg.Payload as OracleXmlType).GetOCIXMLType(out this.m_pOpoAQMsgValCtx->pXmlPayload);
						}
						else if (msg.Payload is XmlReader)
						{
							OracleXmlType oracleXmlType2 = new OracleXmlType(this.m_connection, msg.Payload as XmlReader);
							num = oracleXmlType2.GetOCIXMLType(out this.m_pOpoAQMsgValCtx->pXmlPayload);
							oracleXmlType = oracleXmlType2;
						}
						else
						{
							if (!(msg.Payload is string))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
								{
									"msg"
								}));
							}
							OracleXmlType oracleXmlType3 = new OracleXmlType(this.m_connection, msg.Payload as string);
							num = oracleXmlType3.GetOCIXMLType(out this.m_pOpoAQMsgValCtx->pXmlPayload);
							oracleXmlType = oracleXmlType3;
						}
					}
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			try
			{
				if (num == 0)
				{
					num = OpsAQ.Enqueue(this.m_opsConCtx, this.m_opsErrCtx, this.m_name, (OracleAQMessageType.Raw == this.m_messageType) ? array2 : null, (aqEnqOptions != null) ? this.m_pOpoAQEnqOptionsValCtx : null, this.m_pOpoAQMsgPropsValCtx, this.m_opoAQMsgPropsRefCtx, this.m_pOpoAQMsgValCtx, ref this.m_OCIAQEnqOptions, (aqEnqOptions != null) ? this.m_enqOptsInfo : 0, ref this.m_OCIAQMsgProperties);
				}
				if (num == 0 && OracleAQMessageType.Udt == this.m_messageType)
				{
					if (this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pUDT != IntPtr.Zero)
					{
						OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pUDT);
					}
					OpsUdt.FreeValCtx(this.m_pOpoAQMsgValCtx->pOpoUdtValCtx, true);
					this.m_pOpoAQMsgValCtx->pOpoUdtValCtx = (OpoUdtValCtx*)((void*)IntPtr.Zero);
				}
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			finally
			{
				if (aqEnqOptions != null)
				{
					this.m_enqOptsInfo = 0;
				}
				if (OracleAQMessageType.Xml == this.m_messageType)
				{
					this.m_pOpoAQMsgValCtx->pXmlPayload = IntPtr.Zero;
					this.m_pOpoAQMsgValCtx->isXmlOrUDTNull = 0;
				}
				if (oracleXmlType != null)
				{
					oracleXmlType.Dispose();
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (aqEnqOptions == null || aqEnqOptions.m_deliveryMode != OracleAQMessageDeliveryMode.Buffered)
			{
				msg.m_messageId = new byte[this.m_pOpoAQMsgValCtx->msgIdLen];
				Marshal.Copy(this.m_pOpoAQMsgValCtx->pMsgId, msg.m_messageId, 0, this.m_pOpoAQMsgValCtx->msgIdLen);
			}
			try
			{
				OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoAQMsgValCtx->pMsgIdObject);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleAQQueue::Enqueue()\n"
				});
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001EAB4 File Offset: 0x0001DAB4
		public int EnqueueArray(OracleAQMessage[] messages)
		{
			return this.EnqueueArray(messages, this.m_aqEnqOptions);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001EAC4 File Offset: 0x0001DAC4
		public unsafe int EnqueueArray(OracleAQMessage[] messages, OracleAQEnqueueOptions aqEnqOptions)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::EnqueueArray()\n"
				});
			}
			OracleXmlType[] array = null;
			OracleXmlType[] array2 = null;
			if (messages == null)
			{
				throw new ArgumentNullException("msg");
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature)
			{
				this.SetConnection(this.m_connection);
			}
			for (int i = 0; i < messages.Length; i++)
			{
				if (messages[i] == null)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"messages"
					}));
				}
				if (messages[i].Payload is OracleXmlType && ((OracleXmlType)messages[i].Payload).m_conSignature != this.m_conSignature)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"messages[ " + i + " ].Payload"
					}));
				}
			}
			int num = 0;
			int result = messages.Length;
			if (aqEnqOptions != null)
			{
				if (this.m_pOpoAQEnqOptionsValCtx == null)
				{
					try
					{
						num = OpsAQ.AllocEnqOptValCtx(out this.m_pOpoAQEnqOptionsValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
				this.GetModEnqOptDescAttribFlag(aqEnqOptions, ref this.m_enqOptsInfo);
			}
			OpoAQMsgPropsValCtx* ptr = null;
			OpoAQMsgValCtx* ptr2 = null;
			OpoAQMsgPropsRefCtx[] array3 = new OpoAQMsgPropsRefCtx[messages.Length];
			try
			{
				num = OpsAQ.AllocValCtxArray(out ptr, out ptr2, messages.Length);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			IntPtr zero = IntPtr.Zero;
			for (int j = 0; j < messages.Length; j++)
			{
				if (messages[j].m_msgPropsModified)
				{
					ptr[j].isNull = 0;
					ptr[j].delay = messages[j].m_delay;
					ptr[j].expiration = messages[j].m_expiration;
					ptr[j].priority = messages[j].m_priority;
					if (messages[j].m_recipients != null)
					{
						ptr[j].numRecipients = messages[j].m_recipients.Length;
						OpoAQAgentRefCtx[] array4 = new OpoAQAgentRefCtx[messages[j].m_recipients.Length];
						for (int k = 0; k < messages[j].m_recipients.Length; k++)
						{
							array4[k] = default(OpoAQAgentRefCtx);
							array4[k].name = messages[j].m_recipients[k].m_name;
							array4[k].address = messages[j].m_recipients[k].m_address;
						}
						try
						{
							try
							{
								num = OpsAQ.PrepareAgentArray(this.m_opsConCtx, this.m_opsErrCtx, ref array4, messages[j].m_recipients.Length, out zero);
							}
							catch (Exception ex3)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex3);
								}
								num = ErrRes.INT_ERR;
								throw;
							}
							goto IL_38C;
						}
						finally
						{
							if (num == 0)
							{
								ptr[j].pRecipients = zero;
							}
							else if (num != ErrRes.INT_ERR)
							{
								OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
							}
						}
						goto IL_379;
					}
					goto IL_379;
					IL_38C:
					array3[j] = new OpoAQMsgPropsRefCtx();
					if (messages[j].m_senderId != null)
					{
						array3[j].senderId.name = messages[j].m_senderId.Name;
						array3[j].senderId.address = messages[j].m_senderId.Address;
					}
					else
					{
						array3[j].senderId.name = null;
						array3[j].senderId.address = null;
					}
					array3[j].correlationId = messages[j].m_correlation;
					array3[j].exceptionQueue = messages[j].m_exceptionQueue;
					goto IL_438;
					IL_379:
					ptr[j].numRecipients = 0;
					goto IL_38C;
				}
				ptr[j].isNull = 1;
				IL_438:;
			}
			IntPtr[] array5 = new IntPtr[messages.Length];
			IntPtr[] array6 = new IntPtr[messages.Length];
			try
			{
				num = OpsAQ.AllocMsgPropsRefCtxArray(array6, messages.Length);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			for (int l = 0; l < messages.Length; l++)
			{
				if (ptr[l].isNull != 1)
				{
					Marshal.StructureToPtr(array3[l], array6[l], true);
				}
			}
			if (OracleAQMessageType.Raw == this.m_messageType)
			{
				for (int m = 0; m < messages.Length; m++)
				{
					ptr2[m].payloadType = 1;
					if (messages[m].m_payload != null)
					{
						byte[] array7;
						if (messages[m].m_payload is OracleBinary)
						{
							array7 = ((OracleBinary)messages[m].m_payload).m_value;
							ptr2[m].rawPayloadLen = array7.Length;
						}
						else
						{
							if (!(messages[m].m_payload is byte[]))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
								{
									"messages"
								}));
							}
							array7 = (byte[])messages[m].m_payload;
							ptr2[m].rawPayloadLen = array7.Length;
						}
						if (ptr2[m].rawPayloadLen > 0)
						{
							array5[m] = Marshal.AllocCoTaskMem(ptr2[m].rawPayloadLen);
							try
							{
								num = OpsAQ.ConvertByteArray(array5[m], array7, ptr2[m].rawPayloadLen);
								goto IL_62D;
							}
							catch (Exception ex5)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex5);
								}
								throw;
							}
						}
						array5[m] = IntPtr.Zero;
					}
					IL_62D:;
				}
			}
			else if (OracleAQMessageType.Udt == this.m_messageType)
			{
				for (int n = 0; n < messages.Length; n++)
				{
					if (messages[n].m_payload is IOracleCustomType && !((INullable)messages[n].m_payload).IsNull)
					{
						this.SetUDTFromCustomObject((IOracleCustomType)messages[n].m_payload, ptr2 + n);
					}
					else
					{
						if (messages[n].m_payload != null && (!(messages[n].m_payload is IOracleCustomType) || !((INullable)messages[n].m_payload).IsNull))
						{
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
							{
								"messages"
							}));
						}
						this.SetNullUDTFromCustomObject(this.m_udtTypeName, ptr2 + n);
					}
					ptr2[n].payloadType = 2;
				}
			}
			else if (OracleAQMessageType.Xml == this.m_messageType)
			{
				try
				{
					bool flag = false;
					if (array2 == null)
					{
						array2 = new OracleXmlType[messages.Length];
					}
					for (int num2 = 0; num2 < messages.Length; num2++)
					{
						if (messages[num2].Payload != null && messages[num2].Payload is XmlReader)
						{
							if (num2 == 0)
							{
								array2[num2] = new OracleXmlType(this.m_connection, messages[num2].Payload as XmlReader);
							}
							else
							{
								for (int num3 = num2; num3 > 0; num3--)
								{
									if (messages[num3 - 1].Payload != null && messages[num3 - 1].Payload is XmlReader && ((XmlReader)messages[num2].Payload).Equals((XmlReader)messages[num3 - 1].Payload))
									{
										array2[num2] = array2[num3 - 1];
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									array2[num2] = new OracleXmlType(this.m_connection, messages[num2].Payload as XmlReader);
								}
							}
						}
						flag = false;
					}
					for (int num4 = 0; num4 < messages.Length; num4++)
					{
						ptr2[num4].payloadType = 3;
						if (messages[num4].Payload != null)
						{
							if (messages[num4].Payload is OracleXmlType)
							{
								num = (messages[num4].Payload as OracleXmlType).GetOCIXMLType(out ptr2[num4].pXmlPayload);
							}
							else if (messages[num4].Payload is XmlReader)
							{
								num = array2[num4].GetOCIXMLType(out ptr2[num4].pXmlPayload);
							}
							else
							{
								if (!(messages[num4].Payload is string))
								{
									throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
									{
										"messages"
									}));
								}
								if (array == null)
								{
									array = new OracleXmlType[messages.Length];
								}
								OracleXmlType oracleXmlType = new OracleXmlType(this.m_connection, messages[num4].Payload as string);
								num = oracleXmlType.GetOCIXMLType(out ptr2[num4].pXmlPayload);
								array[num4] = oracleXmlType;
							}
						}
					}
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			try
			{
				if (num == 0)
				{
					num = OpsAQ.EnqueueArray(this.m_opsConCtx, this.m_opsErrCtx, this.m_name, ref result, (OracleAQMessageType.Raw == this.m_messageType) ? array5 : null, (aqEnqOptions != null) ? this.m_pOpoAQEnqOptionsValCtx : null, ptr, array6, ptr2, ref this.m_OCIAQEnqOptions, (aqEnqOptions != null) ? this.m_enqOptsInfo : 0);
				}
			}
			catch (Exception ex6)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex6);
				}
				throw;
			}
			finally
			{
				if (OracleAQMessageType.Raw == this.m_messageType)
				{
					for (int num5 = 0; num5 < messages.Length; num5++)
					{
						if (array5[num5] != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(array5[num5]);
						}
					}
				}
				if (aqEnqOptions != null)
				{
					this.m_enqOptsInfo = 0;
				}
				if (array != null)
				{
					for (int num6 = 0; num6 < array.Length; num6++)
					{
						if (array[num6] != null)
						{
							array[num6].Dispose();
						}
					}
				}
				if (array2 != null)
				{
					for (int num7 = 0; num7 < array2.Length; num7++)
					{
						if (array2[num7] != null)
						{
							array2[num7].Dispose();
						}
					}
				}
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			for (int num8 = 0; num8 < messages.Length; num8++)
			{
				if (aqEnqOptions == null || aqEnqOptions.m_deliveryMode != OracleAQMessageDeliveryMode.Buffered)
				{
					messages[num8].m_messageId = new byte[ptr2[num8].msgIdLen];
					Marshal.Copy(ptr2[num8].pMsgId, messages[num8].m_messageId, 0, ptr2[num8].msgIdLen);
				}
				try
				{
					OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, ptr2[num8].pMsgIdObject);
					if (OracleAQMessageType.Udt == this.m_messageType)
					{
						if (ptr2[num8].pOpoUdtValCtx->pUDT != IntPtr.Zero)
						{
							OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, ptr2[num8].pOpoUdtValCtx->pUDT);
						}
						OpsUdt.FreeValCtx(ptr2[num8].pOpoUdtValCtx, true);
						ptr2[num8].pOpoUdtValCtx = (OpoUdtValCtx*)((void*)IntPtr.Zero);
					}
				}
				catch (Exception ex7)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex7);
					}
					throw;
				}
			}
			try
			{
				OpsAQ.FreeValCtxArray(ref ptr, ref ptr2, messages.Length);
				OpsAQ.FreeMsgPropsRefCtxArray(array6, messages.Length);
			}
			catch (Exception ex8)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex8);
				}
				throw;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleAQQueue::EnqueueArray()\n"
				});
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0001F838 File Offset: 0x0001E838
		public OracleAQMessage Dequeue()
		{
			return this.Dequeue(this.m_aqDeqOptions);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001F848 File Offset: 0x0001E848
		public unsafe OracleAQMessage Dequeue(OracleAQDequeueOptions aqDeqOptions)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::Dequeue()\n"
				});
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature)
			{
				this.SetConnection(this.m_connection);
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			int num = 0;
			OracleAQMessage oracleAQMessage = new OracleAQMessage();
			if (aqDeqOptions != null)
			{
				if (this.m_pOpoAQDeqOptionsValCtx == null)
				{
					try
					{
						OpsAQ.AllocDeqOptValCtx(out this.m_pOpoAQDeqOptionsValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
				}
				if (this.m_opoAQDeqOptionsRefCtx == null)
				{
					this.m_opoAQDeqOptionsRefCtx = new OpoAQDeqOptionsRefCtx();
				}
				this.GetModDeqOptDescAttribFlag(aqDeqOptions, ref this.m_deqOptsInfo);
			}
			OracleUdtDescriptor oracleUdtDescriptor = null;
			if (OracleAQMessageType.Raw == this.m_messageType)
			{
				this.m_pOpoAQMsgValCtx->payloadType = 1;
			}
			else if (OracleAQMessageType.Udt == this.m_messageType)
			{
				if (this.m_udtTypeName == null || this.m_udtTypeName.Length == 0)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"UdtTypeName"
					}));
				}
				this.m_pOpoAQMsgValCtx->payloadType = 2;
				oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, this.m_udtTypeName);
				if (oracleUdtDescriptor.m_customTypeFactory == null)
				{
					object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
					oracleUdtDescriptor.DescribeCustomType(factory);
				}
				int num2 = 0;
				if ((IntPtr)((void*)this.m_pOpoAQMsgValCtx->pOpoUdtValCtx) == IntPtr.Zero)
				{
					try
					{
						num2 = OpsUdt.AllocValCtx(out this.m_pOpoAQMsgValCtx->pOpoUdtValCtx, 1);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					finally
					{
						if (num2 != 0)
						{
							OracleException.HandleError(num2, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
						}
					}
				}
				this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
				this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pTDO = oracleUdtDescriptor.m_opsDscCtx;
				this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			}
			else if (this.m_messageType == OracleAQMessageType.Xml)
			{
				this.m_pOpoAQMsgValCtx->payloadType = 3;
			}
			try
			{
				num = OpsAQ.Dequeue(this.m_opsConCtx, this.m_opsErrCtx, this.m_name, (aqDeqOptions != null) ? ((aqDeqOptions.m_messageId != null) ? aqDeqOptions.m_messageId : null) : null, (aqDeqOptions != null) ? this.m_pOpoAQDeqOptionsValCtx : null, (aqDeqOptions != null) ? this.m_opoAQDeqOptionsRefCtx : null, this.m_pOpoAQMsgPropsValCtx, ref this.m_opoAQMsgPropsRefCtx, this.m_pOpoAQMsgValCtx, ref this.m_OCIAQDeqOptions, (aqDeqOptions != null) ? this.m_deqOptsInfo : 0, ref this.m_OCIAQMsgProperties);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (aqDeqOptions != null)
			{
				this.m_deqOptsInfo = 0;
			}
			if (num == 0)
			{
				if (OracleAQMessageType.Raw == this.m_messageType)
				{
					if (this.m_pOpoAQMsgValCtx->rawPayloadLen != 0)
					{
						byte[] array = new byte[this.m_pOpoAQMsgValCtx->rawPayloadLen];
						Marshal.Copy(this.m_pOpoAQMsgValCtx->pPayloadOut, array, 0, this.m_pOpoAQMsgValCtx->rawPayloadLen);
						if (aqDeqOptions != null && aqDeqOptions.m_providerSpecificType)
						{
							OracleBinary oracleBinary = new OracleBinary(array, false);
							oracleAQMessage.m_payload = oracleBinary;
						}
						else
						{
							oracleAQMessage.m_payload = array;
						}
					}
					else
					{
						oracleAQMessage.m_payload = null;
					}
				}
				else
				{
					if (this.m_messageType == OracleAQMessageType.Xml)
					{
						try
						{
							if (this.m_pOpoAQMsgValCtx->isXmlOrUDTNull == 1)
							{
								oracleAQMessage.m_payload = OracleXmlType.Null;
							}
							else
							{
								OracleXmlType oracleXmlType = new OracleXmlType(this.m_connection, this.m_pOpoAQMsgValCtx->pXmlPayload, false, 1);
								if (aqDeqOptions != null && aqDeqOptions.m_providerSpecificType)
								{
									oracleAQMessage.m_payload = oracleXmlType;
								}
								else
								{
									XmlReader xmlReader;
									try
									{
										xmlReader = oracleXmlType.GetXmlReader();
									}
									finally
									{
										oracleXmlType.Dispose();
									}
									oracleAQMessage.m_payload = xmlReader;
								}
							}
							goto IL_5E4;
						}
						finally
						{
							this.m_pOpoAQMsgValCtx->pXmlPayload = IntPtr.Zero;
							this.m_pOpoAQMsgValCtx->isXmlOrUDTNull = 0;
						}
					}
					if (this.m_messageType == OracleAQMessageType.Udt)
					{
						if (aqDeqOptions.DequeueMode != OracleAQDequeueMode.RemoveNoData)
						{
							if (this.m_pOpoAQMsgValCtx->isXmlOrUDTNull == 0)
							{
								try
								{
									try
									{
										num = OpsUdt.GetObj(this.m_connection.m_opoConCtx.opsConCtx, this.m_pOpoAQMsgValCtx->pOpoUdtValCtx);
									}
									catch (Exception ex4)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex4);
										}
										num = ErrRes.INT_ERR;
										throw;
									}
									goto IL_4C3;
								}
								finally
								{
									if (num == 0)
									{
										this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->bIsNull = 0;
									}
									else if (num != ErrRes.INT_ERR)
									{
										OracleException.HandleError(num, this.m_connection, this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pOpsErrCtx, this);
									}
								}
							}
							this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->bIsNull = 1;
							IL_4C3:
							IOracleCustomType oracleCustomType = ((IOracleCustomTypeFactory)oracleUdtDescriptor.m_customTypeFactory).CreateObject();
							if (this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->bIsNull == 1)
							{
								Type type = oracleCustomType.GetType();
								PropertyInfo property = type.GetProperty("Null");
								if (property == null)
								{
									oracleAQMessage.m_payload = null;
								}
								else
								{
									oracleAQMessage.m_payload = property.GetValue(null, null);
								}
							}
							else
							{
								oracleCustomType.ToCustomObject(this.m_connection, (IntPtr)((void*)this.m_pOpoAQMsgValCtx->pOpoUdtValCtx));
								oracleAQMessage.m_payload = oracleCustomType;
								try
								{
									num = OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoAQMsgValCtx->pOpoUdtValCtx->pUDT);
								}
								catch (Exception ex5)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex5);
									}
									throw;
								}
								finally
								{
									if (num != 0)
									{
										OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
									}
								}
							}
							try
							{
								OpsUdt.FreeValCtx(this.m_pOpoAQMsgValCtx->pOpoUdtValCtx, true);
							}
							catch (Exception ex6)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex6);
								}
								throw;
							}
							this.m_pOpoAQMsgValCtx->pOpoUdtValCtx = (OpoUdtValCtx*)((void*)IntPtr.Zero);
							GC.KeepAlive(oracleUdtDescriptor);
						}
						else
						{
							oracleAQMessage.m_payload = null;
						}
					}
				}
				IL_5E4:
				oracleAQMessage.m_deliveryMode = (OracleAQMessageDeliveryMode)this.m_pOpoAQMsgPropsValCtx->deliveryMode;
				if (oracleAQMessage.m_deliveryMode != OracleAQMessageDeliveryMode.Buffered)
				{
					oracleAQMessage.m_messageId = new byte[this.m_pOpoAQMsgValCtx->msgIdLen];
					Marshal.Copy(this.m_pOpoAQMsgValCtx->pMsgId, oracleAQMessage.m_messageId, 0, this.m_pOpoAQMsgValCtx->msgIdLen);
				}
				if (this.m_pOpoAQMsgPropsValCtx->origMsgIdLen > 0)
				{
					oracleAQMessage.m_originalMessageId = new byte[this.m_pOpoAQMsgPropsValCtx->origMsgIdLen];
					Marshal.Copy(this.m_pOpoAQMsgPropsValCtx->pOrigMsgId, oracleAQMessage.m_originalMessageId, 0, this.m_pOpoAQMsgPropsValCtx->origMsgIdLen);
				}
				oracleAQMessage.m_correlation = this.m_opoAQMsgPropsRefCtx.correlationId;
				oracleAQMessage.m_exceptionQueue = this.m_opoAQMsgPropsRefCtx.exceptionQueue;
				if (this.m_opoAQMsgPropsRefCtx.senderId.name != null && this.m_opoAQMsgPropsRefCtx.senderId.address == null)
				{
					oracleAQMessage.m_senderId = new OracleAQAgent(this.m_opoAQMsgPropsRefCtx.senderId.name);
				}
				else if (this.m_opoAQMsgPropsRefCtx.senderId.name != null && this.m_opoAQMsgPropsRefCtx.senderId.address != null)
				{
					oracleAQMessage.m_senderId = new OracleAQAgent(this.m_opoAQMsgPropsRefCtx.senderId.name, this.m_opoAQMsgPropsRefCtx.senderId.address);
				}
				if (oracleAQMessage.m_deliveryMode == OracleAQMessageDeliveryMode.Buffered && aqDeqOptions.m_correlation != null)
				{
					oracleAQMessage.m_state = OracleAQMessageState.Ready;
				}
				else
				{
					oracleAQMessage.m_state = (OracleAQMessageState)this.m_pOpoAQMsgPropsValCtx->msgState;
				}
				oracleAQMessage.m_enqueueTime = new DateTime(this.m_pOpoAQMsgPropsValCtx->year, this.m_pOpoAQMsgPropsValCtx->month, this.m_pOpoAQMsgPropsValCtx->day, this.m_pOpoAQMsgPropsValCtx->hour, this.m_pOpoAQMsgPropsValCtx->min, this.m_pOpoAQMsgPropsValCtx->sec);
				oracleAQMessage.m_deqAttempts = this.m_pOpoAQMsgPropsValCtx->dequeueAttempts;
				oracleAQMessage.m_delay = this.m_pOpoAQMsgPropsValCtx->delay;
				oracleAQMessage.m_expiration = this.m_pOpoAQMsgPropsValCtx->expiration;
				oracleAQMessage.m_priority = this.m_pOpoAQMsgPropsValCtx->priority;
			}
			try
			{
				OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoAQMsgValCtx->pMsgIdObject);
				if (this.m_pOpoAQMsgPropsValCtx->origMsgIdLen > 0)
				{
					OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoAQMsgPropsValCtx->pOrigMsgIdObject);
				}
				if (OracleAQMessageType.Raw == this.m_messageType && oracleAQMessage.m_payload != null)
				{
					OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoAQMsgValCtx->pPayloadObject);
				}
			}
			catch (Exception ex7)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex7);
				}
				throw;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleAQQueue::Dequeue()\n"
				});
			}
			return oracleAQMessage;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0002018C File Offset: 0x0001F18C
		public OracleAQMessage[] DequeueArray(int dequeueCount)
		{
			return this.DequeueArray(dequeueCount, this.m_aqDeqOptions);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0002019C File Offset: 0x0001F19C
		public unsafe OracleAQMessage[] DequeueArray(int dequeueCount, OracleAQDequeueOptions aqDeqOptions)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::DequeueArray()\n"
				});
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature)
			{
				this.SetConnection(this.m_connection);
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (dequeueCount <= 0)
			{
				throw new ArgumentOutOfRangeException("dequeueCount");
			}
			int num = 0;
			OracleAQMessage[] array = null;
			if (aqDeqOptions != null)
			{
				if (this.m_pOpoAQDeqOptionsValCtx == null)
				{
					try
					{
						OpsAQ.AllocDeqOptValCtx(out this.m_pOpoAQDeqOptionsValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
				}
				if (this.m_opoAQDeqOptionsRefCtx == null)
				{
					this.m_opoAQDeqOptionsRefCtx = new OpoAQDeqOptionsRefCtx();
				}
				this.GetModDeqOptDescAttribFlag(aqDeqOptions, ref this.m_deqOptsInfo);
			}
			OpoAQMsgPropsValCtx* ptr = null;
			OpoAQMsgValCtx* ptr2 = null;
			OpoAQMsgPropsRefCtx[] array2 = null;
			try
			{
				OpsAQ.AllocValCtxArray(out ptr, out ptr2, 1);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			IntPtr[] array3 = null;
			OracleUdtDescriptor oracleUdtDescriptor = null;
			if (this.m_messageType == OracleAQMessageType.Raw)
			{
				ptr2->payloadType = 1;
			}
			else if (this.m_messageType == OracleAQMessageType.Udt)
			{
				if (this.m_udtTypeName == null || this.m_udtTypeName.Length == 0)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"UdtTypeName"
					}));
				}
				ptr2->payloadType = 2;
				oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, this.m_udtTypeName);
				if (oracleUdtDescriptor.m_customTypeFactory == null)
				{
					object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
					oracleUdtDescriptor.DescribeCustomType(factory);
				}
				int num2 = 0;
				ptr2->payloadType = 2;
				if ((IntPtr)((void*)ptr2->pOpoUdtValCtx) == IntPtr.Zero)
				{
					try
					{
						num2 = OpsUdt.AllocValCtx(out ptr2->pOpoUdtValCtx, 1);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
						throw;
					}
					finally
					{
						if (num2 != 0)
						{
							OracleException.HandleError(num2, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
						}
					}
				}
				ptr2->pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
				ptr2->pOpoUdtValCtx->pTDO = oracleUdtDescriptor.m_opsDscCtx;
				ptr2->pOpoUdtValCtx->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			}
			else if (this.m_messageType == OracleAQMessageType.Xml)
			{
				ptr2->payloadType = 3;
			}
			OpoAQDequeueArrayPtrs* ptr3 = null;
			OpoUdtValCtx* ptr4 = null;
			try
			{
				num = OpsAQ.DequeueArray(this.m_opsConCtx, this.m_opsErrCtx, this.m_name, ref dequeueCount, (aqDeqOptions != null) ? ((aqDeqOptions.m_messageId != null) ? aqDeqOptions.m_messageId : null) : null, (aqDeqOptions != null) ? this.m_pOpoAQDeqOptionsValCtx : null, (aqDeqOptions != null) ? this.m_opoAQDeqOptionsRefCtx : null, ref ptr, ref ptr2, ref this.m_OCIAQDeqOptions, (aqDeqOptions != null) ? this.m_deqOptsInfo : 0, out ptr3);
				if (num == 0 && dequeueCount > 0)
				{
					OpsAQ.AllocValCtxArray(out ptr, out ptr2, dequeueCount);
					array2 = new OpoAQMsgPropsRefCtx[dequeueCount];
					array3 = new IntPtr[dequeueCount];
					OpsAQ.AllocMsgPropsRefCtxArray(array3, dequeueCount);
					if (this.m_messageType == OracleAQMessageType.Raw)
					{
						ptr2->payloadType = 1;
					}
					else if (this.m_messageType == OracleAQMessageType.Udt)
					{
						ptr2->payloadType = 2;
						num = OpsUdt.AllocValCtx(out ptr4, dequeueCount);
						if (num == 0)
						{
							for (int i = 0; i < dequeueCount; i++)
							{
								ptr2[i].payloadType = 2;
								ptr2[i].pOpoUdtValCtx = ptr4 + i;
								ptr2[i].pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
								ptr2[i].pOpoUdtValCtx->pTDO = oracleUdtDescriptor.m_opsDscCtx;
								ptr2[i].pOpoUdtValCtx->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
							}
						}
					}
					else if (this.m_messageType == OracleAQMessageType.Xml)
					{
						ptr2->payloadType = 3;
					}
				}
				if (num == 0 && dequeueCount > 0)
				{
					num = OpsAQ.DequeueArrayGetInfo(this.m_opsConCtx, this.m_opsErrCtx, dequeueCount, ptr, array3, ptr2, ref ptr3);
				}
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OpsAQ.FreeDeqArrPtrs(ref ptr3);
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (aqDeqOptions != null)
			{
				this.m_deqOptsInfo = 0;
			}
			if (num == 0 && dequeueCount > 0)
			{
				if (OracleAQMessageType.Raw == this.m_messageType)
				{
					array = new OracleAQMessage[dequeueCount];
					for (int j = 0; j < dequeueCount; j++)
					{
						array2[j] = new OpoAQMsgPropsRefCtx();
						Marshal.PtrToStructure(array3[j], array2[j]);
					}
					for (int k = 0; k < dequeueCount; k++)
					{
						array[k] = new OracleAQMessage();
						if (ptr2[k].rawPayloadLen != 0)
						{
							byte[] array4 = new byte[ptr2[k].rawPayloadLen];
							Marshal.Copy(ptr2[k].pPayloadOut, array4, 0, ptr2[k].rawPayloadLen);
							if (aqDeqOptions != null && aqDeqOptions.m_providerSpecificType)
							{
								OracleBinary oracleBinary = new OracleBinary(array4, false);
								array[k].m_payload = oracleBinary;
							}
							else
							{
								array[k].m_payload = array4;
							}
						}
						else
						{
							array[k].m_payload = null;
						}
					}
				}
				else if (this.m_messageType == OracleAQMessageType.Udt)
				{
					object[] array6;
					if (aqDeqOptions.DequeueMode != OracleAQDequeueMode.RemoveNoData)
					{
						IOracleCustomType[] array5 = new IOracleCustomType[dequeueCount];
						int l = 0;
						while (l < dequeueCount)
						{
							if (ptr2[l].isXmlOrUDTNull == 0)
							{
								try
								{
									try
									{
										num = OpsUdt.GetObj(this.m_connection.m_opoConCtx.opsConCtx, ptr2[l].pOpoUdtValCtx);
									}
									catch (Exception ex5)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex5);
										}
										num = ErrRes.INT_ERR;
										throw;
									}
									goto IL_657;
								}
								finally
								{
									if (num == 0)
									{
										ptr2[l].pOpoUdtValCtx->bIsNull = 0;
									}
									else if (num != ErrRes.INT_ERR)
									{
										OracleException.HandleError(num, this.m_connection, ptr2[l].pOpoUdtValCtx->pOpsErrCtx, this);
									}
								}
								goto IL_63F;
							}
							goto IL_63F;
							IL_657:
							array5[l] = ((IOracleCustomTypeFactory)oracleUdtDescriptor.m_customTypeFactory).CreateObject();
							if (ptr2[l].pOpoUdtValCtx->bIsNull == 1)
							{
								Type type = array5[l].GetType();
								PropertyInfo property = type.GetProperty("Null");
								if (property == null)
								{
									array5[l] = null;
								}
								else
								{
									array5[l] = (IOracleCustomType)property.GetValue(null, null);
								}
							}
							else
							{
								array5[l].ToCustomObject(this.m_connection, (IntPtr)((void*)ptr2[l].pOpoUdtValCtx));
								try
								{
									OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, ptr2[l].pOpoUdtValCtx->pUDT);
								}
								catch (Exception ex6)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex6);
									}
									throw;
								}
								finally
								{
									if (num != 0)
									{
										OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
									}
								}
							}
							l++;
							continue;
							IL_63F:
							ptr2[l].pOpoUdtValCtx->bIsNull = 1;
							goto IL_657;
						}
						try
						{
							OpsAQ.FreeUdtValCtxArray(ptr4, dequeueCount);
						}
						catch (Exception ex7)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex7);
							}
							throw;
						}
						ptr2->pOpoUdtValCtx = (OpoUdtValCtx*)((void*)IntPtr.Zero);
						GC.KeepAlive(oracleUdtDescriptor);
						array6 = array5;
					}
					else
					{
						array6 = null;
					}
					array = new OracleAQMessage[dequeueCount];
					if (array6 != null)
					{
						for (int m = 0; m < dequeueCount; m++)
						{
							array[m] = new OracleAQMessage(array6[m]);
						}
					}
					else
					{
						for (int n = 0; n < dequeueCount; n++)
						{
							array[n] = new OracleAQMessage();
						}
					}
					for (int num3 = 0; num3 < dequeueCount; num3++)
					{
						array2[num3] = new OpoAQMsgPropsRefCtx();
						Marshal.PtrToStructure(array3[num3], array2[num3]);
					}
				}
				else if (this.m_messageType == OracleAQMessageType.Xml)
				{
					try
					{
						array = new OracleAQMessage[dequeueCount];
						for (int num4 = 0; num4 < dequeueCount; num4++)
						{
							array[num4] = new OracleAQMessage();
							array2[num4] = new OpoAQMsgPropsRefCtx();
							Marshal.PtrToStructure(array3[num4], array2[num4]);
							if (ptr2[num4].isXmlOrUDTNull == 1)
							{
								array[num4].m_payload = OracleXmlType.Null;
							}
							else
							{
								OracleXmlType oracleXmlType = new OracleXmlType(this.m_connection, ptr2[num4].pXmlPayload, false, 1);
								if (aqDeqOptions != null && aqDeqOptions.m_providerSpecificType)
								{
									array[num4].m_payload = oracleXmlType;
								}
								else
								{
									XmlReader xmlReader;
									try
									{
										xmlReader = oracleXmlType.GetXmlReader();
									}
									finally
									{
										oracleXmlType.Dispose();
									}
									array[num4].m_payload = xmlReader;
								}
							}
						}
					}
					finally
					{
						for (int num5 = 0; num5 < dequeueCount; num5++)
						{
							ptr2[num5].pXmlPayload = IntPtr.Zero;
							ptr2[num5].isXmlOrUDTNull = 0;
						}
					}
				}
				for (int num6 = 0; num6 < dequeueCount; num6++)
				{
					if (this.m_connection.IsDBVer11gR2OrHigher && (aqDeqOptions == null || aqDeqOptions.DeliveryMode != OracleAQMessageDeliveryMode.Buffered))
					{
						array[num6].m_messageId = new byte[ptr2[num6].msgIdLen];
						Marshal.Copy(ptr2[num6].pMsgId, array[num6].m_messageId, 0, ptr2[num6].msgIdLen);
					}
					if (ptr[num6].origMsgIdLen > 0)
					{
						array[num6].m_originalMessageId = new byte[ptr[num6].origMsgIdLen];
						Marshal.Copy(ptr[num6].pOrigMsgId, array[num6].m_originalMessageId, 0, ptr[num6].origMsgIdLen);
					}
					array[num6].m_correlation = array2[num6].correlationId;
					array[num6].m_exceptionQueue = array2[num6].exceptionQueue;
					array[num6].m_transactionGroup = array2[num6].transNo;
					if (array2[num6].senderId.name != null && array2[num6].senderId.address == null)
					{
						array[num6].m_senderId = new OracleAQAgent(array2[num6].senderId.name);
					}
					else if (array2[num6].senderId.name != null && array2[num6].senderId.address != null)
					{
						array[num6].m_senderId = new OracleAQAgent(array2[num6].senderId.name, array2[num6].senderId.address);
					}
					array[num6].m_state = (OracleAQMessageState)ptr[num6].msgState;
					array[num6].m_deliveryMode = (OracleAQMessageDeliveryMode)ptr[num6].deliveryMode;
					array[num6].m_enqueueTime = new DateTime(ptr[num6].year, ptr[num6].month, ptr[num6].day, ptr[num6].hour, ptr[num6].min, ptr[num6].sec);
					array[num6].m_deqAttempts = ptr[num6].dequeueAttempts;
					array[num6].m_delay = ptr[num6].delay;
					array[num6].m_expiration = ptr[num6].expiration;
					array[num6].m_priority = ptr[num6].priority;
					try
					{
						if (this.m_connection.IsDBVer11gR2OrHigher)
						{
							OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, ptr2[num6].pMsgIdObject);
						}
						if (ptr[num6].origMsgIdLen > 0)
						{
							OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, ptr[num6].pOrigMsgIdObject);
						}
						if (OracleAQMessageType.Raw == this.m_messageType && array[num6].m_payload != null)
						{
							OpsAQ.FreeObject(this.m_opsConCtx, this.m_opsErrCtx, ptr2[num6].pPayloadObject);
						}
					}
					catch (Exception ex8)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex8);
						}
						throw;
					}
				}
			}
			if (num == 0 && dequeueCount > 0)
			{
				try
				{
					OpsAQ.FreeValCtxArray(ref ptr, ref ptr2, dequeueCount);
					OpsAQ.FreeMsgPropsRefCtxArray(array3, dequeueCount);
				}
				catch (Exception ex9)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex9);
					}
					throw;
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleAQQueue::DequeueArray()\n"
				});
			}
			return array;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00021004 File Offset: 0x00020004
		public string Listen(string[] listenConsumers)
		{
			return this.Listen(listenConsumers, -1);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00021010 File Offset: 0x00020010
		public string Listen(string[] listenConsumers, int waitTime)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::Listen()\n"
				});
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature)
			{
				this.SetConnection(this.m_connection);
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (waitTime < -1)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"waitTime"
				}));
			}
			int num = 0;
			int num2 = (listenConsumers != null) ? listenConsumers.Length : 1;
			OpoAQAgentRefCtx[] array = new OpoAQAgentRefCtx[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i] = default(OpoAQAgentRefCtx);
				array[i].name = ((listenConsumers != null) ? listenConsumers[i] : null);
				array[i].address = this.m_name;
			}
			OpoAQAgentRefCtx opoAQAgentRefCtx = default(OpoAQAgentRefCtx);
			try
			{
				IntPtr zero = IntPtr.Zero;
				if (num == 0)
				{
					num = OpsAQ.Listen(this.m_connection.m_opoConCtx.opsConCtx, this.m_connection.m_opoConCtx.opsErrCtx, ref array, num2, waitTime, out zero);
				}
				if (num == 0)
				{
					opoAQAgentRefCtx = (OpoAQAgentRefCtx)Marshal.PtrToStructure(zero, typeof(OpoAQAgentRefCtx));
				}
				OpsAQ.FreeAQAgentCtx(ref zero);
				if (num == 25254)
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0 && num != 25254)
				{
					OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, null);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleAQQueue::Listen()\n"
				});
			}
			if (opoAQAgentRefCtx.name == null)
			{
				return "";
			}
			return opoAQAgentRefCtx.name;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0002122C File Offset: 0x0002022C
		public static OracleAQAgent Listen(OracleConnection con, OracleAQAgent[] listenConsumers)
		{
			return OracleAQQueue.Listen(con, listenConsumers, -1);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00021238 File Offset: 0x00020238
		public static OracleAQAgent Listen(OracleConnection con, OracleAQAgent[] listenConsumers, int waitTime)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) Static-OracleAQAgent::Listen()\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (listenConsumers == null)
			{
				throw new ArgumentNullException("listenConsumers");
			}
			if (waitTime < -1)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"waitTime"
				}));
			}
			if (con.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			int num = 0;
			int num2 = listenConsumers.Length;
			OpoAQAgentRefCtx[] array = new OpoAQAgentRefCtx[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i] = default(OpoAQAgentRefCtx);
				array[i].name = listenConsumers[i].m_name;
				array[i].address = listenConsumers[i].m_address;
			}
			OpoAQAgentRefCtx opoAQAgentRefCtx = default(OpoAQAgentRefCtx);
			try
			{
				IntPtr zero = IntPtr.Zero;
				if (num == 0)
				{
					num = OpsAQ.Listen(con.m_opoConCtx.opsConCtx, con.m_opoConCtx.opsErrCtx, ref array, num2, waitTime, out zero);
				}
				if (num == 0)
				{
					opoAQAgentRefCtx = (OpoAQAgentRefCtx)Marshal.PtrToStructure(zero, typeof(OpoAQAgentRefCtx));
				}
				OpsAQ.FreeAQAgentCtx(ref zero);
				if (num == 25254)
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0 && num != 25254)
				{
					OracleException.HandleError(num, con, con.m_opoConCtx.opsErrCtx, null);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  Static-OracleAQAgent::Listen()\n"
				});
			}
			return new OracleAQAgent(opoAQAgentRefCtx.name, opoAQAgentRefCtx.address);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00021410 File Offset: 0x00020410
		private unsafe void SetUDTFromCustomObject(IOracleCustomType customObj, OpoAQMsgValCtx* pOpoAQMsgValCtx)
		{
			int num = 0;
			OracleUdtDescriptor oracleUdtDescriptor = null;
			oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor2(this.m_connection, (OpoDscRefCtx)OracleUdt.GetUdtName(customObj.GetType().FullName, this.m_connection.DataSource));
			if (oracleUdtDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			if ((IntPtr)((void*)pOpoAQMsgValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					num = OpsUdt.AllocValCtx(out pOpoAQMsgValCtx->pOpoUdtValCtx, 1);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if ((IntPtr)((void*)pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num = OpsUdt.AllocValCtx(out pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					goto IL_1A7;
				}
				finally
				{
					if (num == 0)
					{
						pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if (pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx < oracleUdtDescriptor.AttributeCount)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx, pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				finally
				{
					if (num == 0)
					{
						pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			IL_1A7:
			pOpoAQMsgValCtx->pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
			pOpoAQMsgValCtx->pOpoUdtValCtx->pTDO = oracleUdtDescriptor.m_opsDscCtx;
			pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			for (int i = 0; i < oracleUdtDescriptor.AttributeCount; i++)
			{
				pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx[i].bIsNull = 1;
			}
			customObj.FromCustomObject(this.m_connection, (IntPtr)((void*)pOpoAQMsgValCtx->pOpoUdtValCtx));
			if (!((INullable)customObj).IsNull)
			{
				try
				{
					num = OpsUdt.SetData(this.m_connection.m_opoConCtx.opsConCtx, pOpoAQMsgValCtx->pOpoUdtValCtx);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			GC.KeepAlive(oracleUdtDescriptor);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00021714 File Offset: 0x00020714
		private unsafe void SetNullUDTFromCustomObject(string udtTypeName, OpoAQMsgValCtx* pOpoAQMsgValCtx)
		{
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, udtTypeName);
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			int num = 0;
			if ((IntPtr)((void*)pOpoAQMsgValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					num = OpsUdt.AllocValCtx(out pOpoAQMsgValCtx->pOpoUdtValCtx, 1);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if ((IntPtr)((void*)pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num = OpsUdt.AllocValCtx(out pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					goto IL_17D;
				}
				finally
				{
					if (num == 0)
					{
						pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if (pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx < oracleUdtDescriptor.AttributeCount)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx, pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				finally
				{
					if (num == 0)
					{
						pOpoAQMsgValCtx->pOpoUdtValCtx->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
					}
					else
					{
						OracleException.HandleError(num, this.m_connection, this.m_connection.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			IL_17D:
			pOpoAQMsgValCtx->pOpoUdtValCtx->pOpsErrCtx = this.m_connection.m_opoConCtx.opsErrCtx;
			pOpoAQMsgValCtx->pOpoUdtValCtx->pTDO = oracleUdtDescriptor.m_opsDscCtx;
			pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			pOpoAQMsgValCtx->pOpoUdtValCtx->bIsNull = 1;
			for (int i = 0; i < oracleUdtDescriptor.AttributeCount; i++)
			{
				pOpoAQMsgValCtx->pOpoUdtValCtx->pOpoUdtValCtx[i].bIsNull = 1;
			}
			GC.KeepAlive(oracleUdtDescriptor);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0002196C File Offset: 0x0002096C
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				try
				{
					if (disposing)
					{
						if (this.m_aqEnqOptions != null)
						{
							this.m_aqEnqOptions = null;
						}
						if (this.m_aqDeqOptions != null)
						{
							this.m_aqDeqOptions = null;
						}
						this.m_opoAQMsgPropsRefCtx = null;
					}
					if (this.m_pCtxNTFN != null)
					{
						try
						{
							this.SubscriptionUnRegister();
						}
						catch
						{
						}
					}
					try
					{
						OpsAQ.FreeValCtx(ref this.m_pOpoAQMsgPropsValCtx, ref this.m_pOpoAQMsgValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					try
					{
						if (this.m_pOpoAQEnqOptionsValCtx != null)
						{
							OpsAQ.FreeEnqOptValCtx(ref this.m_pOpoAQEnqOptionsValCtx);
						}
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					try
					{
						if (this.m_pOpoAQDeqOptionsValCtx != null)
						{
							OpsAQ.FreeDeqOptValCtx(ref this.m_pOpoAQDeqOptionsValCtx);
						}
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					if (!(this.m_OCIAQEnqOptions != IntPtr.Zero) && !(this.m_OCIAQDeqOptions != IntPtr.Zero))
					{
						if (!(this.m_OCIAQMsgProperties != IntPtr.Zero))
						{
							goto IL_11A;
						}
					}
					try
					{
						OpsAQ.FreeCachedDesc(ref this.m_OCIAQEnqOptions, ref this.m_OCIAQDeqOptions, ref this.m_OCIAQMsgProperties);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					IL_11A:
					if (this.m_opsErrCtx != IntPtr.Zero)
					{
						try
						{
							OpsErr.FreeCtx(ref this.m_opsErrCtx);
						}
						catch (Exception ex5)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex5);
							}
						}
					}
					if (this.m_opsConCtx != IntPtr.Zero)
					{
						try
						{
							OpsCon.RelRef(ref this.m_opsConCtx);
						}
						catch (Exception ex6)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex6);
							}
						}
					}
				}
				finally
				{
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00021BC4 File Offset: 0x00020BC4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00021BD4 File Offset: 0x00020BD4
		private void SubscriptionRegister()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::SubscriptionRegister\n"
				});
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature || this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				this.SetConnection(this.m_connection);
			}
			int num = 0;
			int num2 = 1;
			if (this.m_notificationConsumers != null)
			{
				num2 = this.m_notificationConsumers.Length;
				this.m_subscriptionName = new string[num2];
				this.m_pOCISubscription = new IntPtr[num2];
				for (int i = 0; i < num2; i++)
				{
					this.m_subscriptionName[i] = this.m_name + ":" + this.m_notificationConsumers[i];
				}
				this.m_pCtxNTFN = new IntPtr[num2];
			}
			else
			{
				this.m_subscriptionName = new string[1];
				this.m_pOCISubscription = new IntPtr[1];
				this.m_subscriptionName[0] = this.m_name;
				this.m_pCtxNTFN = new IntPtr[1];
			}
			try
			{
				num = OpsAQ.AllocSubscrHandle(this.m_opsConCtx, OracleDependency.s_opsEnvCtx, this.m_pOCISubscription, this.m_pCtxNTFN, num2);
				if (num == 0)
				{
					this.m_ntfnInfo = new OracleAQQueue.NtfnInfo[num2];
					string ntfnFormatQueueName = this.GetNtfnFormatQueueName();
					for (int j = 0; j < num2; j++)
					{
						this.m_ntfnInfo[j] = new OracleAQQueue.NtfnInfo();
						this.m_ntfnInfo[j].m_queueName = ntfnFormatQueueName;
						this.m_ntfnInfo[j].m_isNotifiedOnce = (this.m_NTFNReq != null && this.m_NTFNReq.m_bIsNotifiedOnce);
						this.m_ntfnInfo[j].m_eventWrapper = this.m_eventWrapper;
						if (this.m_notificationConsumers == null)
						{
							this.m_ntfnInfo[j].m_consumerName = null;
						}
						else
						{
							this.m_ntfnInfo[j].m_consumerName = this.m_notificationConsumers[j];
						}
						OracleAQQueue.m_subscriptionMap.Add(this.m_pCtxNTFN[j], this.m_ntfnInfo[j]);
					}
					num = OpsAQ.SubscriptionRegister(OracleDependency.s_opsEnvCtx, this.m_opsConCtx, this.m_opsErrCtx, this.m_pOCISubscription, this.m_subscriptionName, num2, (this.m_NTFNReq != null) ? (this.m_NTFNReq.m_bIsNotifiedOnce ? 1 : 0) : 0, (this.m_NTFNReq != null) ? (this.m_NTFNReq.m_bIsPersistent ? 1 : 0) : 0, (this.m_NTFNReq != null) ? ((uint)this.m_NTFNReq.m_timeout) : 0U, (uint)((this.m_NTFNReq != null) ? this.m_NTFNReq.m_groupingInterval : 600), (this.m_NTFNReq != null) ? (this.m_NTFNReq.m_bGroupingNotificationEnabled ? 1 : 0) : 0, (int)((this.m_NTFNReq != null) ? this.m_NTFNReq.m_groupingType : OracleAQNotificationGroupingType.Summary), this.m_pCtxNTFN);
				}
				if (!OracleDependency.s_Listener.bListenerStart)
				{
					lock (OracleDependency.s_Listener)
					{
						if (!OracleDependency.s_Listener.bListenerStart)
						{
							uint port = 0U;
							OpsSubscr.GetPort(OracleDependency.s_opsEnvCtx, OracleDependency.s_opsErrCtx, out port);
							OracleDependency.s_Listener.port = (int)port;
							OracleDependency.s_Listener.bListenerStart = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleAQQueue::SubscriptionRegister\n"
				});
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00021FD0 File Offset: 0x00020FD0
		private void SubscriptionUnRegister()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleAQQueue::SubscriptionUnRegister\n"
				});
			}
			if (this.m_pCtxNTFN != null && this.m_pOCISubscription != null)
			{
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (!this.m_isConSet || this.m_connection.m_conSignature != this.m_conSignature)
				{
					this.SetConnection(this.m_connection);
				}
				int num = 0;
				int num2 = this.m_pOCISubscription.Length;
				try
				{
					for (int i = 0; i < num2; i++)
					{
						OracleAQQueue.NtfnInfo ntfnInfo = OracleAQQueue.m_subscriptionMap[this.m_pCtxNTFN[i]] as OracleAQQueue.NtfnInfo;
						lock (ntfnInfo)
						{
							OracleAQQueue.m_subscriptionMap.Remove(this.m_pCtxNTFN[i]);
						}
					}
					num = OpsAQ.SubscriptionUnRegister(this.m_opsConCtx, this.m_opsErrCtx, num2, this.m_pOCISubscription);
					if (num == 0)
					{
						num = OpsAQ.FreeCtxNTFN(this.m_pCtxNTFN, num2);
						this.m_pCtxNTFN = null;
					}
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleAQQueue::SubscriptionUnRegister\n"
				});
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0002218C File Offset: 0x0002118C
		private unsafe void GetModEnqOptDescAttribFlag(OracleAQEnqueueOptions aqEnqOptions, ref int enqOptsInfo)
		{
			if (aqEnqOptions.m_deliveryMode != (OracleAQMessageDeliveryMode)this.m_pOpoAQEnqOptionsValCtx->deliveryMode)
			{
				enqOptsInfo |= 1;
				this.m_pOpoAQEnqOptionsValCtx->deliveryMode = (int)aqEnqOptions.m_deliveryMode;
			}
			if (aqEnqOptions.m_visibility != (OracleAQVisibilityMode)this.m_pOpoAQEnqOptionsValCtx->visibility)
			{
				enqOptsInfo |= 2;
				this.m_pOpoAQEnqOptionsValCtx->visibility = (int)aqEnqOptions.m_visibility;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000221F0 File Offset: 0x000211F0
		private unsafe void GetModDeqOptDescAttribFlag(OracleAQDequeueOptions aqDeqOptions, ref int deqOptsInfo)
		{
			if (!string.Equals(aqDeqOptions.m_consumerName, this.m_opoAQDeqOptionsRefCtx.consumerName))
			{
				deqOptsInfo |= 1;
				this.m_opoAQDeqOptionsRefCtx.consumerName = aqDeqOptions.m_consumerName;
			}
			if (!string.Equals(aqDeqOptions.m_correlation, this.m_opoAQDeqOptionsRefCtx.correlationId))
			{
				deqOptsInfo |= 2;
				this.m_opoAQDeqOptionsRefCtx.correlationId = aqDeqOptions.m_correlation;
			}
			if (aqDeqOptions.m_deliveryMode != (OracleAQMessageDeliveryMode)this.m_pOpoAQDeqOptionsValCtx->deliveryMode)
			{
				deqOptsInfo |= 4;
				this.m_pOpoAQDeqOptionsValCtx->deliveryMode = (int)aqDeqOptions.m_deliveryMode;
			}
			if (aqDeqOptions.m_dequeueMode != (OracleAQDequeueMode)this.m_pOpoAQDeqOptionsValCtx->deqMode)
			{
				deqOptsInfo |= 8;
				this.m_pOpoAQDeqOptionsValCtx->deqMode = (int)aqDeqOptions.m_dequeueMode;
			}
			if (!this.ArraysEqual(aqDeqOptions.m_messageId, this.m_opoAQDeqOptionsRefCtx.msgId))
			{
				deqOptsInfo |= 16;
				if (aqDeqOptions.m_messageId == null)
				{
					this.m_opoAQDeqOptionsRefCtx.msgId = null;
					this.m_pOpoAQDeqOptionsValCtx->msgIdSize = 0;
				}
				else
				{
					this.m_pOpoAQDeqOptionsValCtx->msgIdSize = aqDeqOptions.m_messageId.Length;
					this.m_opoAQDeqOptionsRefCtx.msgId = new byte[this.m_pOpoAQDeqOptionsValCtx->msgIdSize];
					Array.Copy(aqDeqOptions.m_messageId, this.m_opoAQDeqOptionsRefCtx.msgId, aqDeqOptions.m_messageId.Length);
				}
			}
			if (aqDeqOptions.m_navigationMode != (OracleAQNavigationMode)this.m_pOpoAQDeqOptionsValCtx->navigation)
			{
				deqOptsInfo |= 32;
				this.m_pOpoAQDeqOptionsValCtx->navigation = (int)aqDeqOptions.m_navigationMode;
			}
			if (aqDeqOptions.m_visibility != (OracleAQVisibilityMode)this.m_pOpoAQDeqOptionsValCtx->visibility)
			{
				deqOptsInfo |= 64;
				this.m_pOpoAQDeqOptionsValCtx->visibility = (int)aqDeqOptions.m_visibility;
			}
			if (aqDeqOptions.m_wait != this.m_pOpoAQDeqOptionsValCtx->wait)
			{
				deqOptsInfo |= 128;
				this.m_pOpoAQDeqOptionsValCtx->wait = aqDeqOptions.m_wait;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000223C4 File Offset: 0x000213C4
		private bool ArraysEqual(byte[] ba1, byte[] ba2)
		{
			if (ba1 == null && ba2 == null)
			{
				return true;
			}
			if (ba1 == null && ba2 != null)
			{
				return false;
			}
			if (ba1 != null && ba2 == null)
			{
				return false;
			}
			if (ba1.Length != ba2.Length)
			{
				return false;
			}
			for (int i = 0; i < ba1.Length; i++)
			{
				if (ba1[i] != ba2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0002240C File Offset: 0x0002140C
		private string GetNtfnFormatQueueName()
		{
			string userID = this.m_connection.m_opoConCtx.opoConRefCtx.userID;
			string str = null;
			string str2 = null;
			try
			{
				this.m_name.Trim();
				if (-1 != this.m_name.IndexOf("."))
				{
					if (-1 != this.m_name.IndexOf("\".\""))
					{
						int num = this.m_name.IndexOf("\".\"");
						str2 = this.m_name.Substring(0, num + 1);
						str = this.m_name.Substring(num + 2).ToUpper();
					}
					else if (-1 == this.m_name.IndexOf("\""))
					{
						int num2 = this.m_name.IndexOf('.');
						str2 = "\"" + this.m_name.Substring(0, num2).ToUpper() + "\"";
						str = "\"" + this.m_name.Substring(num2 + 1).ToUpper() + "\"";
					}
					else
					{
						int num3 = this.m_name.IndexOf("\"");
						int num4 = this.m_name.LastIndexOf("\"");
						bool flag = false;
						if (-1 != this.m_name.IndexOf("\"."))
						{
							int num5 = this.m_name.LastIndexOf("\".");
							if (num5 >= num4)
							{
								str2 = this.m_name.Substring(0, num5 + 1);
								str = "\"" + this.m_name.Substring(num5 + 2).ToUpper() + "\"";
							}
							else
							{
								flag = true;
							}
						}
						if (-1 != this.m_name.IndexOf(".\""))
						{
							int num6 = this.m_name.IndexOf(".\"");
							if (num6 + 1 <= num3)
							{
								str2 = "\"" + this.m_name.Substring(0, num6).ToUpper() + "\"";
								str = this.m_name.Substring(num6 + 1).ToUpper();
							}
							else if (flag)
							{
								this.GetDefaultNtfnFormatQNameAndSName(userID, ref str, ref str2);
							}
						}
					}
				}
				else if (this.m_name.StartsWith("\"") && this.m_name.EndsWith("\""))
				{
					if (userID.StartsWith("\"") && userID.EndsWith("\""))
					{
						str2 = userID;
						str = this.m_name.ToUpper();
					}
					else
					{
						str2 = "\"" + userID.ToUpper() + "\"";
						str = this.m_name.ToUpper();
					}
				}
				else
				{
					this.GetDefaultNtfnFormatQNameAndSName(userID, ref str, ref str2);
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) OracleAQQueue::GetNtfnFormatQueueName():" + ex.ToString() + "\n"
					});
				}
				this.GetDefaultNtfnFormatQNameAndSName(userID, ref str, ref str2);
			}
			return str2 + "." + str;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00022704 File Offset: 0x00021704
		private void GetDefaultNtfnFormatQNameAndSName(string schemaName, ref string qName, ref string sName)
		{
			if (schemaName.StartsWith("\"") && schemaName.EndsWith("\""))
			{
				sName = schemaName;
				qName = "\"" + this.m_name.ToUpper() + "\"";
				return;
			}
			sName = "\"" + schemaName.ToUpper() + "\"";
			qName = "\"" + this.m_name.ToUpper() + "\"";
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00022780 File Offset: 0x00021780
		~OracleAQQueue()
		{
			this.Dispose(false);
		}

		// Token: 0x04000202 RID: 514
		private const int LISTENFOREVER = -1;

		// Token: 0x04000203 RID: 515
		internal OracleConnection m_connection;

		// Token: 0x04000204 RID: 516
		internal IntPtr m_opsConCtx;

		// Token: 0x04000205 RID: 517
		internal IntPtr m_opsErrCtx;

		// Token: 0x04000206 RID: 518
		internal int m_conSignature;

		// Token: 0x04000207 RID: 519
		private IntPtr m_OCIAQEnqOptions = IntPtr.Zero;

		// Token: 0x04000208 RID: 520
		private IntPtr m_OCIAQDeqOptions = IntPtr.Zero;

		// Token: 0x04000209 RID: 521
		private IntPtr m_OCIAQMsgProperties = IntPtr.Zero;

		// Token: 0x0400020A RID: 522
		private bool m_disposed;

		// Token: 0x0400020B RID: 523
		protected string m_name;

		// Token: 0x0400020C RID: 524
		private string m_udtTypeName;

		// Token: 0x0400020D RID: 525
		private OracleAQMessageType m_messageType = OracleAQMessageType.Raw;

		// Token: 0x0400020E RID: 526
		private OracleAQEnqueueOptions m_aqEnqOptions = new OracleAQEnqueueOptions();

		// Token: 0x0400020F RID: 527
		private OracleAQDequeueOptions m_aqDeqOptions = new OracleAQDequeueOptions();

		// Token: 0x04000210 RID: 528
		private int m_enqOptsInfo = 65535;

		// Token: 0x04000211 RID: 529
		private int m_deqOptsInfo = 65535;

		// Token: 0x04000212 RID: 530
		internal unsafe OpoAQEnqOptionsValCtx* m_pOpoAQEnqOptionsValCtx;

		// Token: 0x04000213 RID: 531
		internal unsafe OpoAQDeqOptionsValCtx* m_pOpoAQDeqOptionsValCtx;

		// Token: 0x04000214 RID: 532
		internal OpoAQDeqOptionsRefCtx m_opoAQDeqOptionsRefCtx;

		// Token: 0x04000215 RID: 533
		private unsafe OpoAQMsgPropsValCtx* m_pOpoAQMsgPropsValCtx;

		// Token: 0x04000216 RID: 534
		private OpoAQMsgPropsRefCtx m_opoAQMsgPropsRefCtx;

		// Token: 0x04000217 RID: 535
		private unsafe OpoAQMsgValCtx* m_pOpoAQMsgValCtx;

		// Token: 0x04000218 RID: 536
		internal OracleAQQueue.EventWrapper m_eventWrapper = new OracleAQQueue.EventWrapper();

		// Token: 0x04000219 RID: 537
		private OracleAQQueue.NtfnInfo[] m_ntfnInfo;

		// Token: 0x0400021A RID: 538
		private static OnAQNTFNCallback s_onAQNTFNOpsCallback;

		// Token: 0x0400021B RID: 539
		private object m_lockObj = new object();

		// Token: 0x0400021C RID: 540
		private OracleNotificationRequest m_NTFNReq;

		// Token: 0x0400021D RID: 541
		private static Hashtable m_subscriptionMap;

		// Token: 0x0400021E RID: 542
		private IntPtr[] m_pCtxNTFN;

		// Token: 0x0400021F RID: 543
		private IntPtr[] m_pOCISubscription;

		// Token: 0x04000220 RID: 544
		private string[] m_subscriptionName;

		// Token: 0x04000221 RID: 545
		private string[] m_notificationConsumers;

		// Token: 0x04000222 RID: 546
		private bool m_isConSet;

		// Token: 0x0200003E RID: 62
		internal class EventWrapper
		{
			// Token: 0x14000002 RID: 2
			// (add) Token: 0x060002AB RID: 683 RVA: 0x000227B0 File Offset: 0x000217B0
			// (remove) Token: 0x060002AC RID: 684 RVA: 0x000227E8 File Offset: 0x000217E8
			internal event OracleAQMessageAvailableEventHandler OnMessageAvailable;

			// Token: 0x060002AD RID: 685 RVA: 0x0002281D File Offset: 0x0002181D
			internal void FireEvent(object sender, OracleAQMessageAvailableEventArgs e)
			{
				if (this.OnMessageAvailable != null)
				{
					this.OnMessageAvailable(sender, e);
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x060002AE RID: 686 RVA: 0x00022834 File Offset: 0x00021834
			public int InvocationListLength
			{
				get
				{
					if (this.OnMessageAvailable != null)
					{
						return this.OnMessageAvailable.GetInvocationList().Length;
					}
					return 0;
				}
			}
		}

		// Token: 0x0200003F RID: 63
		internal class NtfnInfo
		{
			// Token: 0x04000224 RID: 548
			internal OracleAQQueue.EventWrapper m_eventWrapper;

			// Token: 0x04000225 RID: 549
			internal string m_queueName;

			// Token: 0x04000226 RID: 550
			internal string m_consumerName;

			// Token: 0x04000227 RID: 551
			internal bool m_isNotifiedOnce;
		}
	}
}

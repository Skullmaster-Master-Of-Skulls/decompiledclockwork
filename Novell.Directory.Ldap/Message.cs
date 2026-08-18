using System;
using System.Threading;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004C RID: 76
	internal class Message
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000F6F8 File Offset: 0x0000E6F8
		private void InitBlock()
		{
			this.replies = new MessageVector(5, 5);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000F714 File Offset: 0x0000E714
		internal virtual int Count
		{
			get
			{
				int count = this.replies.Count;
				int result;
				if (this.complete)
				{
					result = ((count > 0) ? (count - 1) : count);
				}
				else
				{
					result = count;
				}
				return result;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000F748 File Offset: 0x0000E748
		internal virtual MessageAgent Agent
		{
			set
			{
				this.agent = value;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F760 File Offset: 0x0000E760
		internal virtual bool hasReplies()
		{
			return this.replies != null && this.replies.Count > 0;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000F78C File Offset: 0x0000E78C
		internal virtual int MessageType
		{
			get
			{
				int result;
				if (this.msg == null)
				{
					result = -1;
				}
				else
				{
					result = this.msg.Type;
				}
				return result;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000F7B4 File Offset: 0x0000E7B4
		internal virtual int MessageID
		{
			get
			{
				return this.msgId;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000F7CC File Offset: 0x0000E7CC
		internal virtual bool Complete
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000F7E4 File Offset: 0x0000E7E4
		internal virtual object waitForReply()
		{
			object result;
			if (this.replies == null)
			{
				result = null;
			}
			else
			{
				lock (this.replies)
				{
					while (this.waitForReply_Renamed_Field)
					{
						if (this.replies.Count != 0)
						{
							object obj2 = this.replies[0];
							this.replies.RemoveAt(0);
							object result2 = obj2;
							if ((this.complete || !this.acceptReplies) && this.replies.Count == 0)
							{
								this.conn.removeMessage(this);
							}
							return result2;
						}
						try
						{
							Monitor.Wait(this.replies);
						}
						catch (ThreadInterruptedException ex)
						{
						}
						if (!this.waitForReply_Renamed_Field)
						{
							break;
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000F8D0 File Offset: 0x0000E8D0
		internal virtual object Reply
		{
			get
			{
				object result;
				if (this.replies == null)
				{
					result = null;
				}
				else
				{
					object obj3;
					lock (this.replies)
					{
						if (this.replies.Count == 0)
						{
							return null;
						}
						object obj2 = this.replies[0];
						this.replies.RemoveAt(0);
						obj3 = obj2;
					}
					if (this.conn != null && (this.complete || !this.acceptReplies) && this.replies.Count == 0)
					{
						this.conn.removeMessage(this);
					}
					result = obj3;
				}
				return result;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000F980 File Offset: 0x0000E980
		internal virtual bool acceptsReplies()
		{
			return this.acceptReplies;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000F998 File Offset: 0x0000E998
		internal virtual LdapMessage Request
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F9B0 File Offset: 0x0000E9B0
		internal virtual bool BindRequest
		{
			get
			{
				return this.bindprops != null;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000F9D0 File Offset: 0x0000E9D0
		internal virtual MessageAgent MessageAgent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F9E8 File Offset: 0x0000E9E8
		internal Message(LdapMessage msg, int mslimit, Connection conn, MessageAgent agent, LdapMessageQueue queue, BindProperties bindprops)
		{
			this.InitBlock();
			this.msg = msg;
			this.conn = conn;
			this.agent = agent;
			this.queue = queue;
			this.mslimit = mslimit;
			this.msgId = msg.MessageID;
			this.bindprops = bindprops;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000FA58 File Offset: 0x0000EA58
		internal void sendMessage()
		{
			this.conn.writeMessage(this);
			if (this.mslimit != 0)
			{
				int type = this.msg.Type;
				if (type != 2 && type != 16)
				{
					this.timer = new Message.Timeout(this, this.mslimit, this);
					this.timer.IsBackground = true;
					this.timer.Start();
				}
				else
				{
					this.mslimit = 0;
				}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000FAC8 File Offset: 0x0000EAC8
		internal virtual void Abandon(LdapConstraints cons, InterThreadException informUserEx)
		{
			if (this.waitForReply_Renamed_Field)
			{
				this.acceptReplies = false;
				this.waitForReply_Renamed_Field = false;
				if (!this.complete)
				{
					try
					{
						if (this.bindprops != null)
						{
							int bindSemId;
							if (this.conn.BindSemIdClear)
							{
								bindSemId = this.msgId;
							}
							else
							{
								bindSemId = this.conn.BindSemId;
								this.conn.clearBindSemId();
							}
							this.conn.freeWriteSemaphore(bindSemId);
						}
						LdapControl[] cont = null;
						if (cons != null)
						{
							cont = cons.getControls();
						}
						LdapMessage ldapMessage = new LdapAbandonRequest(this.msgId, cont);
						this.conn.writeMessage(ldapMessage);
					}
					catch (LdapException ex)
					{
					}
					if (informUserEx == null)
					{
						this.agent.Abandon(this.msgId, null);
					}
					this.conn.removeMessage(this);
				}
				if (informUserEx != null)
				{
					this.replies.Add(new LdapResponse(informUserEx, this.conn.ActiveReferral));
					this.stopTimer();
					this.sleepersAwake();
				}
				else
				{
					this.sleepersAwake();
					this.cleanup();
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000FBE0 File Offset: 0x0000EBE0
		private void cleanup()
		{
			this.stopTimer();
			try
			{
				this.acceptReplies = false;
				if (this.conn != null)
				{
					this.conn.removeMessage(this);
				}
				if (this.replies != null)
				{
					while (this.replies.Count != 0)
					{
						object obj = this.replies[0];
						this.replies.RemoveAt(0);
					}
				}
			}
			catch (Exception ex)
			{
			}
			this.conn = null;
			this.msg = null;
			this.queue = null;
			this.bindprops = null;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000FC80 File Offset: 0x0000EC80
		~Message()
		{
			this.cleanup();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000FCB8 File Offset: 0x0000ECB8
		internal virtual void putReply(RfcLdapMessage message)
		{
			if (this.acceptReplies)
			{
				lock (this.replies)
				{
					this.replies.Add(message);
				}
				message.RequestingMessage = this.msg;
				int type = message.Type;
				if (type != 4 && type != 19 && type != 25)
				{
					this.stopTimer();
					this.acceptReplies = false;
					this.complete = true;
					if (this.bindprops != null)
					{
						int num = ((RfcResponse)message.Response).getResultCode().intValue();
						if (num != 14)
						{
							if (num == 0)
							{
								this.conn.BindProperties = this.bindprops;
							}
							int bindSemId;
							if (this.conn.BindSemIdClear)
							{
								bindSemId = this.msgId;
							}
							else
							{
								bindSemId = this.conn.BindSemId;
								this.conn.clearBindSemId();
							}
							this.conn.freeWriteSemaphore(bindSemId);
						}
					}
				}
				this.sleepersAwake();
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000FDCC File Offset: 0x0000EDCC
		internal virtual void stopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Interrupt();
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000FDF0 File Offset: 0x0000EDF0
		private void sleepersAwake()
		{
			lock (this.replies)
			{
				Monitor.Pulse(this.replies);
			}
			this.agent.sleepersAwake(false);
		}

		// Token: 0x04000169 RID: 361
		private LdapMessage msg;

		// Token: 0x0400016A RID: 362
		private Connection conn;

		// Token: 0x0400016B RID: 363
		private MessageAgent agent;

		// Token: 0x0400016C RID: 364
		private LdapMessageQueue queue;

		// Token: 0x0400016D RID: 365
		private int mslimit;

		// Token: 0x0400016E RID: 366
		private SupportClass.ThreadClass timer = null;

		// Token: 0x0400016F RID: 367
		private MessageVector replies;

		// Token: 0x04000170 RID: 368
		private int msgId;

		// Token: 0x04000171 RID: 369
		private bool acceptReplies = true;

		// Token: 0x04000172 RID: 370
		private bool waitForReply_Renamed_Field = true;

		// Token: 0x04000173 RID: 371
		private bool complete = false;

		// Token: 0x04000174 RID: 372
		private string name;

		// Token: 0x04000175 RID: 373
		private BindProperties bindprops;

		// Token: 0x0200004D RID: 77
		private sealed class Timeout : SupportClass.ThreadClass
		{
			// Token: 0x060002F7 RID: 759 RVA: 0x0000FE48 File Offset: 0x0000EE48
			private void InitBlock(Message enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000FE5C File Offset: 0x0000EE5C
			public Message Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000FE74 File Offset: 0x0000EE74
			internal Timeout(Message enclosingInstance, int interval, Message msg)
			{
				this.InitBlock(enclosingInstance);
				this.timeToWait = interval;
				this.message = msg;
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000FEA8 File Offset: 0x0000EEA8
			public override void Run()
			{
				try
				{
					Thread.Sleep(new TimeSpan((long)(10000 * this.timeToWait)));
					this.message.acceptReplies = false;
					this.message.Abandon(null, new InterThreadException("Client request timed out", null, 85, null, this.message));
				}
				catch (ThreadInterruptedException ex)
				{
				}
			}

			// Token: 0x04000176 RID: 374
			private Message enclosingInstance;

			// Token: 0x04000177 RID: 375
			private int timeToWait = 0;

			// Token: 0x04000178 RID: 376
			private Message message;
		}
	}
}

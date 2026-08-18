using System;
using System.Threading;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004E RID: 78
	internal class MessageAgent
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000FF1C File Offset: 0x0000EF1C
		private void InitBlock()
		{
			this.messages = new MessageVector(5, 5);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000FF38 File Offset: 0x0000EF38
		internal virtual object[] MessageArray
		{
			get
			{
				return this.messages.ObjectArray;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000FF54 File Offset: 0x0000EF54
		internal virtual int[] MessageIDs
		{
			get
			{
				int count = this.messages.Count;
				int[] array = new int[count];
				for (int i = 0; i < count; i++)
				{
					Message message = (Message)this.messages[i];
					array[i] = message.MessageID;
				}
				return array;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000FFA4 File Offset: 0x0000EFA4
		internal virtual string AgentName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000FFBC File Offset: 0x0000EFBC
		internal virtual int Count
		{
			get
			{
				int num = 0;
				foreach (Message message in this.messages.ToArray())
				{
					num += message.Count;
				}
				return num;
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010000 File Offset: 0x0000F000
		internal MessageAgent()
		{
			this.InitBlock();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010020 File Offset: 0x0000F020
		internal void merge(MessageAgent fromAgent)
		{
			object[] messageArray = fromAgent.MessageArray;
			for (int i = 0; i < messageArray.Length; i++)
			{
				this.messages.Add(messageArray[i]);
				((Message)messageArray[i]).Agent = this;
			}
			lock (this.messages)
			{
				if (messageArray.Length > 1)
				{
					Monitor.PulseAll(this.messages);
				}
				else if (messageArray.Length == 1)
				{
					Monitor.Pulse(this.messages);
				}
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000100B8 File Offset: 0x0000F0B8
		internal void sleepersAwake(bool all)
		{
			lock (this.messages)
			{
				if (all)
				{
					Monitor.PulseAll(this.messages);
				}
				else
				{
					Monitor.Pulse(this.messages);
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010114 File Offset: 0x0000F114
		internal bool isResponseReceived()
		{
			int count = this.messages.Count;
			int num = this.indexLastRead + 1;
			for (int i = 0; i < count; i++)
			{
				if (num == count)
				{
					num = 0;
				}
				Message message = (Message)this.messages[num];
				if (message.hasReplies())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010170 File Offset: 0x0000F170
		internal bool isResponseReceived(int msgId)
		{
			bool result;
			try
			{
				Message message = this.messages.findMessageById(msgId);
				result = message.hasReplies();
			}
			catch (FieldAccessException ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000101B8 File Offset: 0x0000F1B8
		internal void Abandon(int msgId, LdapConstraints cons)
		{
			try
			{
				Message message = this.messages.findMessageById(msgId);
				SupportClass.VectorRemoveElement(this.messages, message);
				message.Abandon(cons, null);
			}
			catch (FieldAccessException ex)
			{
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001020C File Offset: 0x0000F20C
		internal void AbandonAll()
		{
			int count = this.messages.Count;
			for (int i = 0; i < count; i++)
			{
				Message message = (Message)this.messages[i];
				SupportClass.VectorRemoveElement(this.messages, message);
				message.Abandon(null, null);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001025C File Offset: 0x0000F25C
		internal bool isComplete(int msgid)
		{
			try
			{
				Message message = this.messages.findMessageById(msgid);
				if (!message.Complete)
				{
					return false;
				}
			}
			catch (FieldAccessException ex)
			{
			}
			return true;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000102A8 File Offset: 0x0000F2A8
		internal Message getMessage(int msgid)
		{
			return this.messages.findMessageById(msgid);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000102C8 File Offset: 0x0000F2C8
		internal void sendMessage(Connection conn, LdapMessage msg, int timeOut, LdapMessageQueue queue, BindProperties bindProps)
		{
			Message message = new Message(msg, timeOut, conn, this, queue, bindProps);
			this.messages.Add(message);
			message.sendMessage();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000102F8 File Offset: 0x0000F2F8
		internal object getLdapMessage(int msgId)
		{
			return this.getLdapMessage(new Integer32(msgId));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00010318 File Offset: 0x0000F318
		internal object getLdapMessage(Integer32 msgId)
		{
			object result;
			if (this.messages.Count == 0)
			{
				result = null;
			}
			else
			{
				if (msgId != null)
				{
					try
					{
						Message message = this.messages.findMessageById(msgId.intValue);
						object obj = message.waitForReply();
						if (!message.acceptsReplies() && !message.hasReplies())
						{
							SupportClass.VectorRemoveElement(this.messages, message);
							message.Abandon(null, null);
						}
						return obj;
					}
					catch (FieldAccessException ex)
					{
						return null;
					}
				}
				lock (this.messages)
				{
					object obj;
					for (;;)
					{
						int num = this.indexLastRead + 1;
						for (int i = 0; i < this.messages.Count; i++)
						{
							if (num >= this.messages.Count)
							{
								num = 0;
							}
							Message message2 = (Message)this.messages[num];
							this.indexLastRead = num++;
							obj = message2.Reply;
							if (!message2.acceptsReplies() && !message2.hasReplies())
							{
								SupportClass.VectorRemoveElement(this.messages, message2);
								message2.Abandon(null, null);
								i--;
							}
							if (obj != null)
							{
								goto Block_12;
							}
						}
						if (this.messages.Count == 0)
						{
							goto Block_14;
						}
						try
						{
							Monitor.Wait(this.messages);
						}
						catch (ThreadInterruptedException ex2)
						{
						}
					}
					Block_12:
					return obj;
					Block_14:
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000104B4 File Offset: 0x0000F4B4
		private void debugDisplayMessages()
		{
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000104C4 File Offset: 0x0000F4C4
		static MessageAgent()
		{
			MessageAgent.nameLock = new object();
		}

		// Token: 0x04000179 RID: 377
		private MessageVector messages;

		// Token: 0x0400017A RID: 378
		private int indexLastRead = 0;

		// Token: 0x0400017B RID: 379
		private static object nameLock;

		// Token: 0x0400017C RID: 380
		private static int agentNum = 0;

		// Token: 0x0400017D RID: 381
		private string name;
	}
}

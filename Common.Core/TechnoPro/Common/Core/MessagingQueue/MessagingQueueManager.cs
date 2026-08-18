using System;
using System.Messaging;
using ClockWorkLogger;
using TechnoPro.Common.ICore.MessagingQueue;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.MessagingQueue
{
	// Token: 0x020000B3 RID: 179
	public class MessagingQueueManager<T> : IMessagingQueueManager<T>, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0002687B File Offset: 0x00024A7B
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00026883 File Offset: 0x00024A83
		public OperationContext OpContext { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0002688C File Offset: 0x00024A8C
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00026894 File Offset: 0x00024A94
		public string QueueName { get; private set; }

		// Token: 0x060006B8 RID: 1720 RVA: 0x000268A0 File Offset: 0x00024AA0
		public MessagingQueueManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.QueueName = ".\\Private$\\" + typeof(T).Name + "MessageQueue";
			bool flag = !MessageQueue.Exists(this.QueueName);
			if (flag)
			{
				MessageQueue.Create(this.QueueName);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00026900 File Offset: 0x00024B00
		public void SendMessage(T obj)
		{
			MessageQueue messageQueue = null;
			try
			{
				messageQueue = new MessageQueue(this.QueueName)
				{
					Formatter = new XmlMessageFormatter(new Type[]
					{
						typeof(T)
					})
				};
				Message obj2 = new Message
				{
					Body = obj,
					Label = typeof(T).Name
				};
				messageQueue.Send(obj2);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("MessagingQueueManager::SendMessage: {0}", ex.ToString()), ex);
				throw;
			}
			finally
			{
				bool flag = messageQueue != null;
				if (flag)
				{
					messageQueue.Close();
				}
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000269CC File Offset: 0x00024BCC
		public T ReceiveMessage()
		{
			MessageQueue messageQueue = null;
			T result;
			try
			{
				messageQueue = new MessageQueue(this.QueueName)
				{
					Formatter = new XmlMessageFormatter(new Type[]
					{
						typeof(T)
					})
				};
				Message message = messageQueue.Receive();
				result = ((message != null) ? ((T)((object)message.Body)) : default(T));
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("MessagingQueueManager::ReceiveMessage: {0}", ex.ToString()), ex);
				throw;
			}
			finally
			{
				bool flag = messageQueue != null;
				if (flag)
				{
					messageQueue.Close();
				}
			}
			return result;
		}
	}
}

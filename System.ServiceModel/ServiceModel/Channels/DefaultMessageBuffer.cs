using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C6 RID: 2502
	internal class DefaultMessageBuffer : MessageBuffer
	{
		// Token: 0x0600624A RID: 25162 RVA: 0x0016DC8C File Offset: 0x0016BE8C
		public DefaultMessageBuffer(Message message, XmlBuffer msgBuffer)
		{
			this.msgBuffer = msgBuffer;
			this.version = message.Version;
			this.isNullMessage = (message is NullMessage);
			this.properties = new KeyValuePair<string, object>[message.Properties.Count];
			((ICollection<KeyValuePair<string, object>>)message.Properties).CopyTo(this.properties, 0);
			this.understoodHeaders = new bool[message.Headers.Count];
			for (int i = 0; i < this.understoodHeaders.Length; i++)
			{
				this.understoodHeaders[i] = message.Headers.IsUnderstood(i);
			}
			if (this.version == MessageVersion.None)
			{
				this.to = message.Headers.To;
				this.action = message.Headers.Action;
			}
		}

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x0600624B RID: 25163 RVA: 0x0016DD55 File Offset: 0x0016BF55
		private object ThisLock
		{
			get
			{
				return this.msgBuffer;
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x0600624C RID: 25164 RVA: 0x0016DD5D File Offset: 0x0016BF5D
		public override int BufferSize
		{
			get
			{
				return this.msgBuffer.BufferSize;
			}
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x0016DD6C File Offset: 0x0016BF6C
		public override void Close()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.closed)
				{
					this.closed = true;
					for (int i = 0; i < this.properties.Length; i++)
					{
						IDisposable disposable = this.properties[i].Value as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x0016DDEC File Offset: 0x0016BFEC
		public override Message CreateMessage()
		{
			if (this.closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
			}
			Message message;
			if (this.isNullMessage)
			{
				message = new NullMessage();
			}
			else
			{
				message = Message.CreateMessage(this.msgBuffer.GetReader(0), int.MaxValue, this.version);
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				message.Properties.CopyProperties(this.properties);
			}
			for (int i = 0; i < this.understoodHeaders.Length; i++)
			{
				if (this.understoodHeaders[i])
				{
					message.Headers.AddUnderstood(i);
				}
			}
			if (this.to != null)
			{
				message.Headers.To = this.to;
			}
			if (this.action != null)
			{
				message.Headers.Action = this.action;
			}
			return message;
		}

		// Token: 0x04003906 RID: 14598
		private XmlBuffer msgBuffer;

		// Token: 0x04003907 RID: 14599
		private KeyValuePair<string, object>[] properties;

		// Token: 0x04003908 RID: 14600
		private bool[] understoodHeaders;

		// Token: 0x04003909 RID: 14601
		private bool closed;

		// Token: 0x0400390A RID: 14602
		private MessageVersion version;

		// Token: 0x0400390B RID: 14603
		private Uri to;

		// Token: 0x0400390C RID: 14604
		private string action;

		// Token: 0x0400390D RID: 14605
		private bool isNullMessage;
	}
}

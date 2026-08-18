using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004F RID: 79
	internal class MessageVector : ArrayList
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000104E4 File Offset: 0x0000F4E4
		internal virtual object[] ObjectArray
		{
			get
			{
				object[] result;
				lock (this)
				{
					object[] array = new object[this.Count];
					Array.Copy(this.ToArray(), 0, array, 0, this.Count);
					for (int i = 0; i < this.Count; i++)
					{
						this.ToArray()[i] = null;
					}
					result = array;
				}
				return result;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001055C File Offset: 0x0000F55C
		internal MessageVector(int cap, int incr) : base(cap)
		{
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00010574 File Offset: 0x0000F574
		internal Message findMessageById(int msgId)
		{
			lock (this)
			{
				for (int i = 0; i < this.Count; i++)
				{
					Message message;
					if ((message = (Message)this[i]) == null)
					{
						throw new FieldAccessException();
					}
					if (message.MessageID == msgId)
					{
						return message;
					}
				}
				throw new FieldAccessException();
			}
			Message result;
			return result;
		}
	}
}

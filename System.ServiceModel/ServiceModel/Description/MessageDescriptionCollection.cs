using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D0 RID: 976
	[__DynamicallyInvokable]
	public class MessageDescriptionCollection : Collection<MessageDescription>
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x0008489A File Offset: 0x00082A9A
		internal MessageDescriptionCollection()
		{
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000848A4 File Offset: 0x00082AA4
		[__DynamicallyInvokable]
		public MessageDescription Find(string action)
		{
			foreach (MessageDescription messageDescription in this)
			{
				if (messageDescription != null && action == messageDescription.Action)
				{
					return messageDescription;
				}
			}
			return null;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x00084900 File Offset: 0x00082B00
		[__DynamicallyInvokable]
		public Collection<MessageDescription> FindAll(string action)
		{
			Collection<MessageDescription> collection = new Collection<MessageDescription>();
			foreach (MessageDescription messageDescription in this)
			{
				if (messageDescription != null && action == messageDescription.Action)
				{
					collection.Add(messageDescription);
				}
			}
			return collection;
		}
	}
}

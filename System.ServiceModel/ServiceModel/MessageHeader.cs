using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200011D RID: 285
	[__DynamicallyInvokable]
	public class MessageHeader<T>
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x0001E668 File Offset: 0x0001C868
		[__DynamicallyInvokable]
		public MessageHeader()
		{
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001E670 File Offset: 0x0001C870
		[__DynamicallyInvokable]
		public MessageHeader(T content) : this(content, false, "", false)
		{
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001E680 File Offset: 0x0001C880
		[__DynamicallyInvokable]
		public MessageHeader(T content, bool mustUnderstand, string actor, bool relay)
		{
			this.content = content;
			this.mustUnderstand = mustUnderstand;
			this.actor = actor;
			this.relay = relay;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001E6A5 File Offset: 0x0001C8A5
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001E6AD File Offset: 0x0001C8AD
		[__DynamicallyInvokable]
		public string Actor
		{
			[__DynamicallyInvokable]
			get
			{
				return this.actor;
			}
			[__DynamicallyInvokable]
			set
			{
				this.actor = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001E6B6 File Offset: 0x0001C8B6
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001E6BE File Offset: 0x0001C8BE
		[__DynamicallyInvokable]
		public T Content
		{
			[__DynamicallyInvokable]
			get
			{
				return this.content;
			}
			[__DynamicallyInvokable]
			set
			{
				this.content = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001E6C7 File Offset: 0x0001C8C7
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001E6CF File Offset: 0x0001C8CF
		[__DynamicallyInvokable]
		public bool MustUnderstand
		{
			[__DynamicallyInvokable]
			get
			{
				return this.mustUnderstand;
			}
			[__DynamicallyInvokable]
			set
			{
				this.mustUnderstand = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		[__DynamicallyInvokable]
		public bool Relay
		{
			[__DynamicallyInvokable]
			get
			{
				return this.relay;
			}
			[__DynamicallyInvokable]
			set
			{
				this.relay = value;
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001E6E9 File Offset: 0x0001C8E9
		internal Type GetGenericArgument()
		{
			return typeof(T);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001E6F5 File Offset: 0x0001C8F5
		[__DynamicallyInvokable]
		public MessageHeader GetUntypedHeader(string name, string ns)
		{
			return MessageHeader.CreateHeader(name, ns, this.content, this.mustUnderstand, this.actor, this.relay);
		}

		// Token: 0x04000ABE RID: 2750
		private string actor;

		// Token: 0x04000ABF RID: 2751
		private bool mustUnderstand;

		// Token: 0x04000AC0 RID: 2752
		private bool relay;

		// Token: 0x04000AC1 RID: 2753
		private T content;
	}
}

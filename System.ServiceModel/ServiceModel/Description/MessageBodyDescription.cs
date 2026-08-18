using System;
using System.ComponentModel;

namespace System.ServiceModel.Description
{
	// Token: 0x020003CD RID: 973
	[__DynamicallyInvokable]
	public class MessageBodyDescription
	{
		// Token: 0x0600248C RID: 9356 RVA: 0x000843B4 File Offset: 0x000825B4
		[__DynamicallyInvokable]
		public MessageBodyDescription()
		{
			this.parts = new MessagePartDescriptionCollection();
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000843C8 File Offset: 0x000825C8
		internal MessageBodyDescription(MessageBodyDescription other)
		{
			this.WrapperName = other.WrapperName;
			this.WrapperNamespace = other.WrapperNamespace;
			this.parts = new MessagePartDescriptionCollection();
			foreach (MessagePartDescription messagePartDescription in other.Parts)
			{
				this.Parts.Add(messagePartDescription.Clone());
			}
			if (other.ReturnValue != null)
			{
				this.ReturnValue = other.ReturnValue.Clone();
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00084464 File Offset: 0x00082664
		internal MessageBodyDescription Clone()
		{
			return new MessageBodyDescription(this);
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x0008446C File Offset: 0x0008266C
		[__DynamicallyInvokable]
		public MessagePartDescriptionCollection Parts
		{
			[__DynamicallyInvokable]
			get
			{
				return this.parts;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00084474 File Offset: 0x00082674
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0008447C File Offset: 0x0008267C
		[DefaultValue(null)]
		[__DynamicallyInvokable]
		public MessagePartDescription ReturnValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this.returnValue;
			}
			[__DynamicallyInvokable]
			set
			{
				this.returnValue = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x00084485 File Offset: 0x00082685
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x000844A2 File Offset: 0x000826A2
		[DefaultValue(null)]
		[__DynamicallyInvokable]
		public string WrapperName
		{
			[__DynamicallyInvokable]
			get
			{
				if (!(this.wrapperName == null))
				{
					return this.wrapperName.EncodedName;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				this.wrapperName = new XmlName(value, true);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000844B1 File Offset: 0x000826B1
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x000844B9 File Offset: 0x000826B9
		[DefaultValue(null)]
		[__DynamicallyInvokable]
		public string WrapperNamespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.wrapperNs;
			}
			[__DynamicallyInvokable]
			set
			{
				this.wrapperNs = value;
			}
		}

		// Token: 0x04002079 RID: 8313
		private XmlName wrapperName;

		// Token: 0x0400207A RID: 8314
		private string wrapperNs;

		// Token: 0x0400207B RID: 8315
		private MessagePartDescriptionCollection parts;

		// Token: 0x0400207C RID: 8316
		private MessagePartDescription returnValue;
	}
}

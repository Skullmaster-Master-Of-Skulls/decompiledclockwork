using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003CF RID: 975
	internal class MessageDescriptionItems
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x00084838 File Offset: 0x00082A38
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x00084853 File Offset: 0x00082A53
		internal MessageBodyDescription Body
		{
			get
			{
				if (this.body == null)
				{
					this.body = new MessageBodyDescription();
				}
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0008485C File Offset: 0x00082A5C
		internal MessageHeaderDescriptionCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new MessageHeaderDescriptionCollection();
				}
				return this.headers;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00084877 File Offset: 0x00082A77
		internal MessagePropertyDescriptionCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new MessagePropertyDescriptionCollection();
				}
				return this.properties;
			}
		}

		// Token: 0x04002086 RID: 8326
		private MessageHeaderDescriptionCollection headers;

		// Token: 0x04002087 RID: 8327
		private MessageBodyDescription body;

		// Token: 0x04002088 RID: 8328
		private MessagePropertyDescriptionCollection properties;
	}
}

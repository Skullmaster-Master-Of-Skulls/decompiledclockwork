using System;
using System.ComponentModel;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D1 RID: 977
	[__DynamicallyInvokable]
	public class MessageHeaderDescription : MessagePartDescription
	{
		// Token: 0x060024B8 RID: 9400 RVA: 0x00084960 File Offset: 0x00082B60
		[__DynamicallyInvokable]
		public MessageHeaderDescription(string name, string ns) : base(name, ns)
		{
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0008496C File Offset: 0x00082B6C
		internal MessageHeaderDescription(MessageHeaderDescription other) : base(other)
		{
			this.MustUnderstand = other.MustUnderstand;
			this.Relay = other.Relay;
			this.Actor = other.Actor;
			this.TypedHeader = other.TypedHeader;
			this.IsUnknownHeaderCollection = other.IsUnknownHeaderCollection;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000849BC File Offset: 0x00082BBC
		internal override MessagePartDescription Clone()
		{
			return new MessageHeaderDescription(this);
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x000849C4 File Offset: 0x00082BC4
		// (set) Token: 0x060024BC RID: 9404 RVA: 0x000849CC File Offset: 0x00082BCC
		[DefaultValue(null)]
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

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x000849D5 File Offset: 0x00082BD5
		// (set) Token: 0x060024BE RID: 9406 RVA: 0x000849DD File Offset: 0x00082BDD
		[DefaultValue(false)]
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

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x000849E6 File Offset: 0x00082BE6
		// (set) Token: 0x060024C0 RID: 9408 RVA: 0x000849EE File Offset: 0x00082BEE
		[DefaultValue(false)]
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

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x000849F7 File Offset: 0x00082BF7
		// (set) Token: 0x060024C2 RID: 9410 RVA: 0x000849FF File Offset: 0x00082BFF
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool TypedHeader
		{
			[__DynamicallyInvokable]
			get
			{
				return this.typedHeader;
			}
			[__DynamicallyInvokable]
			set
			{
				this.typedHeader = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x00084A08 File Offset: 0x00082C08
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x00084A33 File Offset: 0x00082C33
		internal bool IsUnknownHeaderCollection
		{
			get
			{
				return this.isUnknownHeader || (base.Multiple && base.Type == typeof(XmlElement));
			}
			set
			{
				this.isUnknownHeader = value;
			}
		}

		// Token: 0x04002089 RID: 8329
		private bool mustUnderstand;

		// Token: 0x0400208A RID: 8330
		private bool relay;

		// Token: 0x0400208B RID: 8331
		private string actor;

		// Token: 0x0400208C RID: 8332
		private bool typedHeader;

		// Token: 0x0400208D RID: 8333
		private bool isUnknownHeader;
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003CE RID: 974
	[DebuggerDisplay("Action={action}, Direction={direction}, MessageType={messageType}")]
	[__DynamicallyInvokable]
	public class MessageDescription
	{
		// Token: 0x06002496 RID: 9366 RVA: 0x000844C2 File Offset: 0x000826C2
		[__DynamicallyInvokable]
		public MessageDescription(string action, MessageDirection direction) : this(action, direction, null)
		{
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000844CD File Offset: 0x000826CD
		internal MessageDescription(string action, MessageDirection direction, MessageDescriptionItems items)
		{
			if (!MessageDirectionHelper.IsDefined(direction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("direction"));
			}
			this.action = action;
			this.direction = direction;
			this.items = items;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00084508 File Offset: 0x00082708
		internal MessageDescription(MessageDescription other)
		{
			this.action = other.action;
			this.direction = other.direction;
			this.Items.Body = other.Items.Body.Clone();
			foreach (MessageHeaderDescription messageHeaderDescription in other.Items.Headers)
			{
				this.Items.Headers.Add(messageHeaderDescription.Clone() as MessageHeaderDescription);
			}
			foreach (MessagePropertyDescription messagePropertyDescription in other.Items.Properties)
			{
				this.Items.Properties.Add(messagePropertyDescription.Clone() as MessagePropertyDescription);
			}
			this.MessageName = other.MessageName;
			this.MessageType = other.MessageType;
			this.XsdTypeName = other.XsdTypeName;
			this.hasProtectionLevel = other.hasProtectionLevel;
			this.ProtectionLevel = other.ProtectionLevel;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00084638 File Offset: 0x00082838
		internal MessageDescription Clone()
		{
			return new MessageDescription(this);
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x00084640 File Offset: 0x00082840
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x00084648 File Offset: 0x00082848
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
			internal set
			{
				this.action = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x00084651 File Offset: 0x00082851
		[__DynamicallyInvokable]
		public MessageBodyDescription Body
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Items.Body;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x0008465E File Offset: 0x0008285E
		[__DynamicallyInvokable]
		public MessageDirection Direction
		{
			[__DynamicallyInvokable]
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x00084666 File Offset: 0x00082866
		[__DynamicallyInvokable]
		public MessageHeaderDescriptionCollection Headers
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Items.Headers;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00084673 File Offset: 0x00082873
		[__DynamicallyInvokable]
		public MessagePropertyDescriptionCollection Properties
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Items.Properties;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00084680 File Offset: 0x00082880
		internal MessageDescriptionItems Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new MessageDescriptionItems();
				}
				return this.items;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x0008469B File Offset: 0x0008289B
		// (set) Token: 0x060024A2 RID: 9378 RVA: 0x000846A3 File Offset: 0x000828A3
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
				this.hasProtectionLevel = true;
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000846D0 File Offset: 0x000828D0
		public bool ShouldSerializeProtectionLevel()
		{
			return this.HasProtectionLevel;
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x000846D8 File Offset: 0x000828D8
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000846E0 File Offset: 0x000828E0
		internal static Type TypeOfUntypedMessage
		{
			get
			{
				if (MessageDescription.typeOfUntypedMessage == null)
				{
					MessageDescription.typeOfUntypedMessage = typeof(Message);
				}
				return MessageDescription.typeOfUntypedMessage;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x00084703 File Offset: 0x00082903
		// (set) Token: 0x060024A7 RID: 9383 RVA: 0x0008470B File Offset: 0x0008290B
		internal XmlName MessageName
		{
			get
			{
				return this.messageName;
			}
			set
			{
				this.messageName = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x00084714 File Offset: 0x00082914
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x0008471C File Offset: 0x0008291C
		[DefaultValue(null)]
		[__DynamicallyInvokable]
		public Type MessageType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageType;
			}
			[__DynamicallyInvokable]
			set
			{
				this.messageType = value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x00084725 File Offset: 0x00082925
		internal bool IsTypedMessage
		{
			get
			{
				return this.messageType != null;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x00084734 File Offset: 0x00082934
		internal bool IsUntypedMessage
		{
			get
			{
				return (this.Body.ReturnValue != null && this.Body.Parts.Count == 0 && this.Body.ReturnValue.Type == MessageDescription.TypeOfUntypedMessage) || (this.Body.ReturnValue == null && this.Body.Parts.Count == 1 && this.Body.Parts[0].Type == MessageDescription.TypeOfUntypedMessage);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000847C0 File Offset: 0x000829C0
		internal bool IsVoid
		{
			get
			{
				return !this.IsTypedMessage && this.Body.Parts.Count == 0 && (this.Body.ReturnValue == null || this.Body.ReturnValue.Type == typeof(void));
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x00084817 File Offset: 0x00082A17
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x0008481F File Offset: 0x00082A1F
		internal XmlQualifiedName XsdTypeName
		{
			get
			{
				return this.xsdType;
			}
			set
			{
				this.xsdType = value;
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00084828 File Offset: 0x00082A28
		internal void ResetProtectionLevel()
		{
			this.protectionLevel = ProtectionLevel.None;
			this.hasProtectionLevel = false;
		}

		// Token: 0x0400207D RID: 8317
		private static Type typeOfUntypedMessage;

		// Token: 0x0400207E RID: 8318
		private string action;

		// Token: 0x0400207F RID: 8319
		private MessageDirection direction;

		// Token: 0x04002080 RID: 8320
		private MessageDescriptionItems items;

		// Token: 0x04002081 RID: 8321
		private XmlName messageName;

		// Token: 0x04002082 RID: 8322
		private Type messageType;

		// Token: 0x04002083 RID: 8323
		private XmlQualifiedName xsdType;

		// Token: 0x04002084 RID: 8324
		private ProtectionLevel protectionLevel;

		// Token: 0x04002085 RID: 8325
		private bool hasProtectionLevel;
	}
}

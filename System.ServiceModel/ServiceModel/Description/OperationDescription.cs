using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.Reflection;
using System.ServiceModel.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D7 RID: 983
	[DebuggerDisplay("Name={name}, IsInitiating={isInitiating}, IsTerminating={isTerminating}")]
	[__DynamicallyInvokable]
	public class OperationDescription
	{
		// Token: 0x060024E9 RID: 9449 RVA: 0x00084CB8 File Offset: 0x00082EB8
		[__DynamicallyInvokable]
		public OperationDescription(string name, ContractDescription declaringContract)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (name.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("name", SR.GetString("SFxOperationDescriptionNameCannotBeEmpty")));
			}
			this.name = new XmlName(name, true);
			if (declaringContract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("declaringContract");
			}
			this.declaringContract = declaringContract;
			this.isInitiating = true;
			this.isTerminating = false;
			this.faults = new FaultDescriptionCollection();
			this.messages = new MessageDescriptionCollection();
			this.behaviors = new KeyedByTypeCollection<IOperationBehavior>();
			this.knownTypes = new Collection<Type>();
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00084D6D File Offset: 0x00082F6D
		internal OperationDescription(string name, ContractDescription declaringContract, bool validateRpcWrapperName) : this(name, declaringContract)
		{
			this.validateRpcWrapperName = validateRpcWrapperName;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00084D7E File Offset: 0x00082F7E
		[__DynamicallyInvokable]
		public KeyedCollection<Type, IOperationBehavior> OperationBehaviors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Behaviors;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x00084D86 File Offset: 0x00082F86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public KeyedByTypeCollection<IOperationBehavior> Behaviors
		{
			get
			{
				return this.behaviors;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00084D8E File Offset: 0x00082F8E
		// (set) Token: 0x060024EE RID: 9454 RVA: 0x00084D96 File Offset: 0x00082F96
		[__DynamicallyInvokable]
		public MethodInfo TaskMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.taskMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				this.taskMethod = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00084D9F File Offset: 0x00082F9F
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x00084DA7 File Offset: 0x00082FA7
		[__DynamicallyInvokable]
		public MethodInfo SyncMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.syncMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				this.syncMethod = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00084DB0 File Offset: 0x00082FB0
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00084DB8 File Offset: 0x00082FB8
		[__DynamicallyInvokable]
		public MethodInfo BeginMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.beginMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				this.beginMethod = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00084DC1 File Offset: 0x00082FC1
		internal MethodInfo OperationMethod
		{
			get
			{
				if (this.SyncMethod == null)
				{
					return this.TaskMethod ?? this.BeginMethod;
				}
				return this.SyncMethod;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00084DE8 File Offset: 0x00082FE8
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x00084DF0 File Offset: 0x00082FF0
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

		// Token: 0x060024F6 RID: 9462 RVA: 0x00084E1D File Offset: 0x0008301D
		public bool ShouldSerializeProtectionLevel()
		{
			return this.HasProtectionLevel;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00084E25 File Offset: 0x00083025
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x00084E2D File Offset: 0x0008302D
		// (set) Token: 0x060024F9 RID: 9465 RVA: 0x00084E35 File Offset: 0x00083035
		internal bool HasNoDisposableParameters
		{
			get
			{
				return this.hasNoDisposableParameters;
			}
			set
			{
				this.hasNoDisposableParameters = value;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x00084E3E File Offset: 0x0008303E
		// (set) Token: 0x060024FB RID: 9467 RVA: 0x00084E46 File Offset: 0x00083046
		[__DynamicallyInvokable]
		public MethodInfo EndMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.endMethod;
			}
			[__DynamicallyInvokable]
			set
			{
				this.endMethod = value;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00084E4F File Offset: 0x0008304F
		// (set) Token: 0x060024FD RID: 9469 RVA: 0x00084E57 File Offset: 0x00083057
		[__DynamicallyInvokable]
		public ContractDescription DeclaringContract
		{
			[__DynamicallyInvokable]
			get
			{
				return this.declaringContract;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("DeclaringContract");
				}
				this.declaringContract = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x00084E73 File Offset: 0x00083073
		[__DynamicallyInvokable]
		public FaultDescriptionCollection Faults
		{
			[__DynamicallyInvokable]
			get
			{
				return this.faults;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x00084E7B File Offset: 0x0008307B
		[__DynamicallyInvokable]
		public bool IsOneWay
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Messages.Count == 1;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x00084E8B File Offset: 0x0008308B
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x00084E93 File Offset: 0x00083093
		[DefaultValue(false)]
		public bool IsInitiating
		{
			get
			{
				return this.isInitiating;
			}
			set
			{
				this.isInitiating = value;
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00084E9C File Offset: 0x0008309C
		internal bool IsServerInitiated()
		{
			this.EnsureInvariants();
			return this.Messages[0].Direction == MessageDirection.Output;
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x00084EB8 File Offset: 0x000830B8
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x00084EC0 File Offset: 0x000830C0
		[DefaultValue(false)]
		public bool IsTerminating
		{
			get
			{
				return this.isTerminating;
			}
			set
			{
				this.isTerminating = value;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x00084EC9 File Offset: 0x000830C9
		[__DynamicallyInvokable]
		public Collection<Type> KnownTypes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.knownTypes;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x00084ED1 File Offset: 0x000830D1
		[__DynamicallyInvokable]
		public MessageDescriptionCollection Messages
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messages;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00084ED9 File Offset: 0x000830D9
		internal XmlName XmlName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x00084EE1 File Offset: 0x000830E1
		internal string CodeName
		{
			get
			{
				return this.name.DecodedName;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x00084EEE File Offset: 0x000830EE
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name.EncodedName;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x00084EFB File Offset: 0x000830FB
		internal bool IsValidateRpcWrapperName
		{
			get
			{
				return this.validateRpcWrapperName;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x00084F03 File Offset: 0x00083103
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x00084F0B File Offset: 0x0008310B
		internal bool IsInsideTransactedReceiveScope { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x00084F14 File Offset: 0x00083114
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x00084F1C File Offset: 0x0008311C
		internal bool IsFirstReceiveOfTransactedReceiveScopeTree { get; set; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x00084F25 File Offset: 0x00083125
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x00084F2D File Offset: 0x0008312D
		internal Type TaskTResult { get; set; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x00084F36 File Offset: 0x00083136
		internal bool HasOutputParameters
		{
			get
			{
				return this.Messages.Count > 1 && this.Messages[1].Body.Parts.Count > 0;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00084F66 File Offset: 0x00083166
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00084F6E File Offset: 0x0008316E
		internal bool IsSessionOpenNotificationEnabled
		{
			get
			{
				return this.isSessionOpenNotificationEnabled;
			}
			set
			{
				this.isSessionOpenNotificationEnabled = value;
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00084F78 File Offset: 0x00083178
		internal void EnsureInvariants()
		{
			if (this.Messages.Count != 1 && this.Messages.Count != 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxOperationMustHaveOneOrTwoMessages", new object[]
				{
					this.Name
				})));
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00084FCA File Offset: 0x000831CA
		internal void ResetProtectionLevel()
		{
			this.protectionLevel = ProtectionLevel.None;
			this.hasProtectionLevel = false;
		}

		// Token: 0x0400209A RID: 8346
		internal const string SessionOpenedAction = "http://schemas.microsoft.com/2011/02/session/onopen";

		// Token: 0x0400209B RID: 8347
		private XmlName name;

		// Token: 0x0400209C RID: 8348
		private bool isInitiating;

		// Token: 0x0400209D RID: 8349
		private bool isTerminating;

		// Token: 0x0400209E RID: 8350
		private bool isSessionOpenNotificationEnabled;

		// Token: 0x0400209F RID: 8351
		private ContractDescription declaringContract;

		// Token: 0x040020A0 RID: 8352
		private FaultDescriptionCollection faults;

		// Token: 0x040020A1 RID: 8353
		private MessageDescriptionCollection messages;

		// Token: 0x040020A2 RID: 8354
		private KeyedByTypeCollection<IOperationBehavior> behaviors;

		// Token: 0x040020A3 RID: 8355
		private Collection<Type> knownTypes;

		// Token: 0x040020A4 RID: 8356
		private MethodInfo beginMethod;

		// Token: 0x040020A5 RID: 8357
		private MethodInfo endMethod;

		// Token: 0x040020A6 RID: 8358
		private MethodInfo syncMethod;

		// Token: 0x040020A7 RID: 8359
		private MethodInfo taskMethod;

		// Token: 0x040020A8 RID: 8360
		private ProtectionLevel protectionLevel;

		// Token: 0x040020A9 RID: 8361
		private bool hasProtectionLevel;

		// Token: 0x040020AA RID: 8362
		private bool validateRpcWrapperName = true;

		// Token: 0x040020AB RID: 8363
		private bool hasNoDisposableParameters;
	}
}

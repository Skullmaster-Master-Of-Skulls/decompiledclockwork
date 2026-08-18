using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.ServiceModel.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C6 RID: 966
	[DebuggerDisplay("Name={name}, Namespace={ns}, ContractType={contractType}")]
	[__DynamicallyInvokable]
	public class ContractDescription
	{
		// Token: 0x0600244D RID: 9293 RVA: 0x00083D90 File Offset: 0x00081F90
		[__DynamicallyInvokable]
		public ContractDescription(string name) : this(name, null)
		{
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00083D9C File Offset: 0x00081F9C
		[__DynamicallyInvokable]
		public ContractDescription(string name, string ns)
		{
			this.Name = name;
			if (!string.IsNullOrEmpty(ns))
			{
				NamingHelper.CheckUriParameter(ns, "ns");
			}
			this.operations = new OperationDescriptionCollection();
			this.ns = (ns ?? "http://tempuri.org/");
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x00083DEF File Offset: 0x00081FEF
		internal string CodeName
		{
			get
			{
				return this.name.DecodedName;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x00083DFC File Offset: 0x00081FFC
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x00083E04 File Offset: 0x00082004
		[DefaultValue(null)]
		[__DynamicallyInvokable]
		public string ConfigurationName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.configurationName;
			}
			[__DynamicallyInvokable]
			set
			{
				this.configurationName = value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x00083E0D File Offset: 0x0008200D
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x00083E15 File Offset: 0x00082015
		[__DynamicallyInvokable]
		public Type ContractType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.contractType;
			}
			[__DynamicallyInvokable]
			set
			{
				this.contractType = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x00083E1E File Offset: 0x0008201E
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x00083E26 File Offset: 0x00082026
		[__DynamicallyInvokable]
		public Type CallbackContractType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.callbackContractType;
			}
			[__DynamicallyInvokable]
			set
			{
				this.callbackContractType = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x00083E2F File Offset: 0x0008202F
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x00083E3C File Offset: 0x0008203C
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name.EncodedName;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("SFxContractDescriptionNameCannotBeEmpty")));
				}
				this.name = new XmlName(value, true);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x00083E90 File Offset: 0x00082090
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x00083E98 File Offset: 0x00082098
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					NamingHelper.CheckUriProperty(value, "Namespace");
				}
				this.ns = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00083EB4 File Offset: 0x000820B4
		[__DynamicallyInvokable]
		public OperationDescriptionCollection Operations
		{
			[__DynamicallyInvokable]
			get
			{
				return this.operations;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x00083EBC File Offset: 0x000820BC
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x00083EC4 File Offset: 0x000820C4
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

		// Token: 0x0600245D RID: 9309 RVA: 0x00083EF1 File Offset: 0x000820F1
		public bool ShouldSerializeProtectionLevel()
		{
			return this.HasProtectionLevel;
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00083EF9 File Offset: 0x000820F9
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x00083F01 File Offset: 0x00082101
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x00083F09 File Offset: 0x00082109
		[DefaultValue(SessionMode.Allowed)]
		public SessionMode SessionMode
		{
			get
			{
				return this.sessionMode;
			}
			set
			{
				if (!SessionModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.sessionMode = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00083F2F File Offset: 0x0008212F
		[__DynamicallyInvokable]
		public KeyedCollection<Type, IContractBehavior> ContractBehaviors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Behaviors;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x00083F37 File Offset: 0x00082137
		[EditorBrowsable(EditorBrowsableState.Never)]
		public KeyedByTypeCollection<IContractBehavior> Behaviors
		{
			get
			{
				return this.behaviors;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00083F40 File Offset: 0x00082140
		public static ContractDescription GetContract(Type contractType)
		{
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractType");
			}
			TypeLoader typeLoader = new TypeLoader();
			return typeLoader.LoadContractDescription(contractType);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00083F74 File Offset: 0x00082174
		public static ContractDescription GetContract(Type contractType, Type serviceType)
		{
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractType");
			}
			if (serviceType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceType");
			}
			TypeLoader typeLoader = new TypeLoader();
			return typeLoader.LoadContractDescription(contractType, serviceType);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00083FC4 File Offset: 0x000821C4
		public static ContractDescription GetContract(Type contractType, object serviceImplementation)
		{
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractType");
			}
			if (serviceImplementation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceImplementation");
			}
			TypeLoader typeLoader = new TypeLoader();
			Type type = serviceImplementation.GetType();
			return typeLoader.LoadContractDescription(contractType, type, serviceImplementation);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00084018 File Offset: 0x00082218
		public Collection<ContractDescription> GetInheritedContracts()
		{
			Collection<ContractDescription> collection = new Collection<ContractDescription>();
			for (int i = 0; i < this.Operations.Count; i++)
			{
				OperationDescription operationDescription = this.Operations[i];
				if (operationDescription.DeclaringContract != this)
				{
					ContractDescription declaringContract = operationDescription.DeclaringContract;
					if (!collection.Contains(declaringContract))
					{
						collection.Add(declaringContract);
					}
				}
			}
			return collection;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00084070 File Offset: 0x00082270
		internal void EnsureInvariants()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AChannelServiceEndpointSContractSNameIsNull0")));
			}
			if (this.Namespace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AChannelServiceEndpointSContractSNamespace0")));
			}
			if (this.Operations.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxContractHasZeroOperations", new object[]
				{
					this.Name
				})));
			}
			bool flag = false;
			for (int i = 0; i < this.Operations.Count; i++)
			{
				OperationDescription operationDescription = this.Operations[i];
				operationDescription.EnsureInvariants();
				if (operationDescription.IsInitiating)
				{
					flag = true;
				}
				if ((!operationDescription.IsInitiating || operationDescription.IsTerminating) && this.SessionMode != SessionMode.Required)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContractIsNotSelfConsistentItHasOneOrMore2", new object[]
					{
						this.Name
					})));
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxContractHasZeroInitiatingOperations", new object[]
				{
					this.Name
				})));
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000841A0 File Offset: 0x000823A0
		internal bool IsDuplex()
		{
			for (int i = 0; i < this.operations.Count; i++)
			{
				if (this.operations[i].IsServerInitiated())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002067 RID: 8295
		private Type callbackContractType;

		// Token: 0x04002068 RID: 8296
		private string configurationName;

		// Token: 0x04002069 RID: 8297
		private Type contractType;

		// Token: 0x0400206A RID: 8298
		private XmlName name;

		// Token: 0x0400206B RID: 8299
		private string ns;

		// Token: 0x0400206C RID: 8300
		private OperationDescriptionCollection operations;

		// Token: 0x0400206D RID: 8301
		private SessionMode sessionMode;

		// Token: 0x0400206E RID: 8302
		private KeyedByTypeCollection<IContractBehavior> behaviors = new KeyedByTypeCollection<IContractBehavior>();

		// Token: 0x0400206F RID: 8303
		private ProtectionLevel protectionLevel;

		// Token: 0x04002070 RID: 8304
		private bool hasProtectionLevel;
	}
}

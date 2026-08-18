using System;
using System.CodeDom;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x0200041D RID: 1053
	public class ServiceContractGenerationContext
	{
		// Token: 0x0600284B RID: 10315 RVA: 0x0009788C File Offset: 0x00095A8C
		public ServiceContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ContractDescription contract, CodeTypeDeclaration contractType)
		{
			if (serviceContractGenerator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceContractGenerator"));
			}
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contract"));
			}
			if (contractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contractType"));
			}
			this.serviceContractGenerator = serviceContractGenerator;
			this.contract = contract;
			this.contractType = contractType;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x00097907 File Offset: 0x00095B07
		public ServiceContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ContractDescription contract, CodeTypeDeclaration contractType, CodeTypeDeclaration duplexCallbackType) : this(serviceContractGenerator, contract, contractType)
		{
			this.duplexCallbackType = duplexCallbackType;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0009791A File Offset: 0x00095B1A
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x00097922 File Offset: 0x00095B22
		internal CodeTypeDeclaration ChannelType
		{
			get
			{
				return this.channelType;
			}
			set
			{
				this.channelType = value;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x0009792B File Offset: 0x00095B2B
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x00097933 File Offset: 0x00095B33
		internal CodeTypeReference ChannelTypeReference
		{
			get
			{
				return this.channelTypeReference;
			}
			set
			{
				this.channelTypeReference = value;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x0009793C File Offset: 0x00095B3C
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x00097944 File Offset: 0x00095B44
		internal CodeTypeDeclaration ClientType
		{
			get
			{
				return this.clientType;
			}
			set
			{
				this.clientType = value;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x0009794D File Offset: 0x00095B4D
		// (set) Token: 0x06002854 RID: 10324 RVA: 0x00097955 File Offset: 0x00095B55
		internal CodeTypeReference ClientTypeReference
		{
			get
			{
				return this.clientTypeReference;
			}
			set
			{
				this.clientTypeReference = value;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0009795E File Offset: 0x00095B5E
		public ContractDescription Contract
		{
			get
			{
				return this.contract;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x00097966 File Offset: 0x00095B66
		public CodeTypeDeclaration ContractType
		{
			get
			{
				return this.contractType;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0009796E File Offset: 0x00095B6E
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x00097976 File Offset: 0x00095B76
		internal CodeTypeReference ContractTypeReference
		{
			get
			{
				return this.contractTypeReference;
			}
			set
			{
				this.contractTypeReference = value;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x0009797F File Offset: 0x00095B7F
		public CodeTypeDeclaration DuplexCallbackType
		{
			get
			{
				return this.duplexCallbackType;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x00097987 File Offset: 0x00095B87
		// (set) Token: 0x0600285B RID: 10331 RVA: 0x0009798F File Offset: 0x00095B8F
		internal CodeTypeReference DuplexCallbackTypeReference
		{
			get
			{
				return this.duplexCallbackTypeReference;
			}
			set
			{
				this.duplexCallbackTypeReference = value;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x00097998 File Offset: 0x00095B98
		// (set) Token: 0x0600285D RID: 10333 RVA: 0x000979A0 File Offset: 0x00095BA0
		internal CodeNamespace Namespace
		{
			get
			{
				return this.codeNamespace;
			}
			set
			{
				this.codeNamespace = value;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x000979A9 File Offset: 0x00095BA9
		public Collection<OperationContractGenerationContext> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000979B1 File Offset: 0x00095BB1
		public ServiceContractGenerator ServiceContractGenerator
		{
			get
			{
				return this.serviceContractGenerator;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000979B9 File Offset: 0x00095BB9
		// (set) Token: 0x06002861 RID: 10337 RVA: 0x000979C1 File Offset: 0x00095BC1
		internal ServiceContractGenerator.CodeTypeFactory TypeFactory
		{
			get
			{
				return this.typeFactory;
			}
			set
			{
				this.typeFactory = value;
			}
		}

		// Token: 0x04002226 RID: 8742
		private readonly ServiceContractGenerator serviceContractGenerator;

		// Token: 0x04002227 RID: 8743
		private readonly ContractDescription contract;

		// Token: 0x04002228 RID: 8744
		private readonly CodeTypeDeclaration contractType;

		// Token: 0x04002229 RID: 8745
		private readonly CodeTypeDeclaration duplexCallbackType;

		// Token: 0x0400222A RID: 8746
		private readonly Collection<OperationContractGenerationContext> operations = new Collection<OperationContractGenerationContext>();

		// Token: 0x0400222B RID: 8747
		private CodeNamespace codeNamespace;

		// Token: 0x0400222C RID: 8748
		private CodeTypeDeclaration channelType;

		// Token: 0x0400222D RID: 8749
		private CodeTypeReference channelTypeReference;

		// Token: 0x0400222E RID: 8750
		private CodeTypeDeclaration clientType;

		// Token: 0x0400222F RID: 8751
		private CodeTypeReference clientTypeReference;

		// Token: 0x04002230 RID: 8752
		private CodeTypeReference contractTypeReference;

		// Token: 0x04002231 RID: 8753
		private CodeTypeReference duplexCallbackTypeReference;

		// Token: 0x04002232 RID: 8754
		private ServiceContractGenerator.CodeTypeFactory typeFactory;
	}
}

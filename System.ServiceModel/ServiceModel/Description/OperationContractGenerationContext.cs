using System;
using System.CodeDom;

namespace System.ServiceModel.Description
{
	// Token: 0x02000413 RID: 1043
	public class OperationContractGenerationContext
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x00096AC8 File Offset: 0x00094CC8
		private OperationContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ServiceContractGenerationContext contract, OperationDescription operation, CodeTypeDeclaration declaringType)
		{
			if (serviceContractGenerator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serviceContractGenerator"));
			}
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contract"));
			}
			if (declaringType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("declaringType"));
			}
			this.serviceContractGenerator = serviceContractGenerator;
			this.contract = contract;
			this.operation = operation;
			this.declaringType = declaringType;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00096B44 File Offset: 0x00094D44
		public OperationContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ServiceContractGenerationContext contract, OperationDescription operation, CodeTypeDeclaration declaringType, CodeMemberMethod syncMethod, CodeMemberMethod beginMethod, CodeMemberMethod endMethod, CodeMemberMethod taskMethod) : this(serviceContractGenerator, contract, operation, declaringType)
		{
			if (syncMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncMethod"));
			}
			if (beginMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("beginMethod"));
			}
			if (endMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endMethod"));
			}
			if (taskMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("taskMethod"));
			}
			this.syncMethod = syncMethod;
			this.beginMethod = beginMethod;
			this.endMethod = endMethod;
			this.taskMethod = taskMethod;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00096BE0 File Offset: 0x00094DE0
		public OperationContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ServiceContractGenerationContext contract, OperationDescription operation, CodeTypeDeclaration declaringType, CodeMemberMethod syncMethod, CodeMemberMethod beginMethod, CodeMemberMethod endMethod) : this(serviceContractGenerator, contract, operation, declaringType)
		{
			if (syncMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncMethod"));
			}
			if (beginMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("beginMethod"));
			}
			if (endMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endMethod"));
			}
			this.syncMethod = syncMethod;
			this.beginMethod = beginMethod;
			this.endMethod = endMethod;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00096C5C File Offset: 0x00094E5C
		public OperationContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ServiceContractGenerationContext contract, OperationDescription operation, CodeTypeDeclaration declaringType, CodeMemberMethod syncMethod, CodeMemberMethod taskMethod) : this(serviceContractGenerator, contract, operation, declaringType)
		{
			if (syncMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncMethod"));
			}
			if (taskMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("taskMethod"));
			}
			this.syncMethod = syncMethod;
			this.taskMethod = taskMethod;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00096CB6 File Offset: 0x00094EB6
		public OperationContractGenerationContext(ServiceContractGenerator serviceContractGenerator, ServiceContractGenerationContext contract, OperationDescription operation, CodeTypeDeclaration declaringType, CodeMemberMethod method) : this(serviceContractGenerator, contract, operation, declaringType)
		{
			if (method == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("method"));
			}
			this.syncMethod = method;
			this.beginMethod = null;
			this.endMethod = null;
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00096CF2 File Offset: 0x00094EF2
		public ServiceContractGenerationContext Contract
		{
			get
			{
				return this.contract;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x00096CFA File Offset: 0x00094EFA
		public CodeTypeDeclaration DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x00096D02 File Offset: 0x00094F02
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x00096D0A File Offset: 0x00094F0A
		internal CodeTypeReference DeclaringTypeReference
		{
			get
			{
				return this.declaringTypeReference;
			}
			set
			{
				this.declaringTypeReference = value;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x00096D13 File Offset: 0x00094F13
		public CodeMemberMethod BeginMethod
		{
			get
			{
				return this.beginMethod;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x00096D1B File Offset: 0x00094F1B
		public CodeMemberMethod EndMethod
		{
			get
			{
				return this.endMethod;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x00096D23 File Offset: 0x00094F23
		public CodeMemberMethod TaskMethod
		{
			get
			{
				return this.taskMethod;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x00096D2B File Offset: 0x00094F2B
		public CodeMemberMethod SyncMethod
		{
			get
			{
				return this.syncMethod;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x00096D33 File Offset: 0x00094F33
		public bool IsAsync
		{
			get
			{
				return this.beginMethod != null;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x00096D3E File Offset: 0x00094F3E
		public bool IsTask
		{
			get
			{
				return this.taskMethod != null;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x00096D49 File Offset: 0x00094F49
		internal bool IsInherited
		{
			get
			{
				return this.declaringType != this.contract.ContractType && this.declaringType != this.contract.DuplexCallbackType;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x00096D76 File Offset: 0x00094F76
		public OperationDescription Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x00096D7E File Offset: 0x00094F7E
		public ServiceContractGenerator ServiceContractGenerator
		{
			get
			{
				return this.serviceContractGenerator;
			}
		}

		// Token: 0x04002204 RID: 8708
		private readonly CodeMemberMethod syncMethod;

		// Token: 0x04002205 RID: 8709
		private readonly CodeMemberMethod beginMethod;

		// Token: 0x04002206 RID: 8710
		private readonly ServiceContractGenerationContext contract;

		// Token: 0x04002207 RID: 8711
		private readonly CodeMemberMethod endMethod;

		// Token: 0x04002208 RID: 8712
		private readonly OperationDescription operation;

		// Token: 0x04002209 RID: 8713
		private readonly ServiceContractGenerator serviceContractGenerator;

		// Token: 0x0400220A RID: 8714
		private readonly CodeTypeDeclaration declaringType;

		// Token: 0x0400220B RID: 8715
		private readonly CodeMemberMethod taskMethod;

		// Token: 0x0400220C RID: 8716
		private CodeTypeReference declaringTypeReference;
	}
}

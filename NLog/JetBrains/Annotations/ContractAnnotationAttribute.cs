using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	internal sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002130 File Offset: 0x00000330
		public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000213A File Offset: 0x0000033A
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002158 File Offset: 0x00000358
		public string Contract { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002161 File Offset: 0x00000361
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002169 File Offset: 0x00000369
		public bool ForceFullStates { get; private set; }
	}
}

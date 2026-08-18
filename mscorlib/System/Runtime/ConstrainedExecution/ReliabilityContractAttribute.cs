using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020004D7 RID: 1239
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		// Token: 0x06003131 RID: 12593 RVA: 0x000A8F37 File Offset: 0x000A7F37
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this._consistency = consistencyGuarantee;
			this._cer = cer;
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000A8F4D File Offset: 0x000A7F4D
		public Consistency ConsistencyGuarantee
		{
			get
			{
				return this._consistency;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000A8F55 File Offset: 0x000A7F55
		public Cer Cer
		{
			get
			{
				return this._cer;
			}
		}

		// Token: 0x040018E7 RID: 6375
		private Consistency _consistency;

		// Token: 0x040018E8 RID: 6376
		private Cer _cer;
	}
}

using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000287 RID: 647
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlMethodAttribute : SqlFunctionAttribute
	{
		// Token: 0x0600220D RID: 8717 RVA: 0x0028AB38 File Offset: 0x00289F38
		public SqlMethodAttribute()
		{
			this.m_fCallOnNullInputs = true;
			this.m_fMutator = false;
			this.m_fInvokeIfReceiverIsNull = false;
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x0028AB68 File Offset: 0x00289F68
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x0028AB88 File Offset: 0x00289F88
		public bool OnNullCall
		{
			get
			{
				return this.m_fCallOnNullInputs;
			}
			set
			{
				this.m_fCallOnNullInputs = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x0028ABA8 File Offset: 0x00289FA8
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x0028ABC8 File Offset: 0x00289FC8
		public bool IsMutator
		{
			get
			{
				return this.m_fMutator;
			}
			set
			{
				this.m_fMutator = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0028ABE8 File Offset: 0x00289FE8
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x0028AC08 File Offset: 0x0028A008
		public bool InvokeIfReceiverIsNull
		{
			get
			{
				return this.m_fInvokeIfReceiverIsNull;
			}
			set
			{
				this.m_fInvokeIfReceiverIsNull = value;
			}
		}

		// Token: 0x04001654 RID: 5716
		private bool m_fCallOnNullInputs;

		// Token: 0x04001655 RID: 5717
		private bool m_fMutator;

		// Token: 0x04001656 RID: 5718
		private bool m_fInvokeIfReceiverIsNull;
	}
}

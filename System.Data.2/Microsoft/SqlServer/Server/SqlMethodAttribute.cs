using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlMethodAttribute : SqlFunctionAttribute
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x000465E4 File Offset: 0x000459E4
		public SqlMethodAttribute()
		{
			this.m_fCallOnNullInputs = true;
			this.m_fMutator = false;
			this.m_fInvokeIfReceiverIsNull = false;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0004660C File Offset: 0x00045A0C
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00046620 File Offset: 0x00045A20
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00046634 File Offset: 0x00045A34
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00046648 File Offset: 0x00045A48
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0004665C File Offset: 0x00045A5C
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00046670 File Offset: 0x00045A70
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

		// Token: 0x040001DB RID: 475
		private bool m_fCallOnNullInputs;

		// Token: 0x040001DC RID: 476
		private bool m_fMutator;

		// Token: 0x040001DD RID: 477
		private bool m_fInvokeIfReceiverIsNull;
	}
}

using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200060A RID: 1546
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000BBE00 File Offset: 0x000BAE00
		// (set) Token: 0x0600380B RID: 14347 RVA: 0x000BBE08 File Offset: 0x000BAE08
		public bool WrapNonExceptionThrows
		{
			get
			{
				return this.m_wrapNonExceptionThrows;
			}
			set
			{
				this.m_wrapNonExceptionThrows = value;
			}
		}

		// Token: 0x04001D09 RID: 7433
		private bool m_wrapNonExceptionThrows;
	}
}

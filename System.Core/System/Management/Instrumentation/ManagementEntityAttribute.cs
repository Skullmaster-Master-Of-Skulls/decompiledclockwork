using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000287 RID: 647
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementEntityAttribute : Attribute
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00057168 File Offset: 0x00055368
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x00057170 File Offset: 0x00055370
		public string Name
		{
			get
			{
				return this._nounName;
			}
			set
			{
				this._nounName = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00057179 File Offset: 0x00055379
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x00057181 File Offset: 0x00055381
		public bool External
		{
			get
			{
				return this._isExternalClass;
			}
			set
			{
				this._isExternalClass = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0005718A File Offset: 0x0005538A
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00057192 File Offset: 0x00055392
		public bool Singleton
		{
			get
			{
				return this._isSingleton;
			}
			set
			{
				this._isSingleton = value;
			}
		}

		// Token: 0x04000B7A RID: 2938
		private string _nounName;

		// Token: 0x04000B7B RID: 2939
		private bool _isExternalClass;

		// Token: 0x04000B7C RID: 2940
		private bool _isSingleton;
	}
}

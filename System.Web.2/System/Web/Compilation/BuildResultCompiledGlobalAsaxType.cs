using System;

namespace System.Web.Compilation
{
	// Token: 0x0200081A RID: 2074
	internal class BuildResultCompiledGlobalAsaxType : BuildResultCompiledType
	{
		// Token: 0x06006357 RID: 25431 RVA: 0x0015C09F File Offset: 0x0015A29F
		public BuildResultCompiledGlobalAsaxType()
		{
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x0015C0A7 File Offset: 0x0015A2A7
		public BuildResultCompiledGlobalAsaxType(Type t) : base(t)
		{
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x0015C0DC File Offset: 0x0015A2DC
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultCompiledGlobalAsaxType;
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x0600635A RID: 25434 RVA: 0x0015C0DF File Offset: 0x0015A2DF
		// (set) Token: 0x0600635B RID: 25435 RVA: 0x0015C0F1 File Offset: 0x0015A2F1
		internal bool HasAppOrSessionObjects
		{
			get
			{
				return this._flags[524288];
			}
			set
			{
				this._flags[524288] = value;
			}
		}
	}
}

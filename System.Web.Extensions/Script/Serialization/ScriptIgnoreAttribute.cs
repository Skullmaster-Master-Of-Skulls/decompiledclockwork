using System;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000103 RID: 259
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class ScriptIgnoreAttribute : Attribute
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00031028 File Offset: 0x0002F228
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x00031030 File Offset: 0x0002F230
		public bool ApplyToOverrides { get; set; }
	}
}

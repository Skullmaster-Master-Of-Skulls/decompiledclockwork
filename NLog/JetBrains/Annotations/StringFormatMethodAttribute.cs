using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	internal sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public StringFormatMethodAttribute(string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020EF File Offset: 0x000002EF
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F7 File Offset: 0x000002F7
		public string FormatParameterName { get; private set; }
	}
}

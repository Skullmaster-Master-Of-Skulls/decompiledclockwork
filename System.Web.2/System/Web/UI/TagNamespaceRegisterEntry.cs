using System;

namespace System.Web.UI
{
	// Token: 0x02000243 RID: 579
	internal class TagNamespaceRegisterEntry : RegisterDirectiveEntry
	{
		// Token: 0x06001AEB RID: 6891 RVA: 0x00054838 File Offset: 0x00052A38
		internal TagNamespaceRegisterEntry(string tagPrefix, string namespaceName, string assemblyName) : base(tagPrefix)
		{
			this._ns = namespaceName;
			this._assemblyName = assemblyName;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0005484F File Offset: 0x00052A4F
		internal string Namespace
		{
			get
			{
				return this._ns;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00054857 File Offset: 0x00052A57
		internal string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
		}

		// Token: 0x04001877 RID: 6263
		private string _ns;

		// Token: 0x04001878 RID: 6264
		private string _assemblyName;
	}
}

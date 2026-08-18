using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004E3 RID: 1251
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		// Token: 0x0600314D RID: 12621 RVA: 0x000A90B6 File Offset: 0x000A80B6
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000A90CA File Offset: 0x000A80CA
		public string Value
		{
			get
			{
				return this._importClassName;
			}
		}

		// Token: 0x040018FE RID: 6398
		internal string _importClassName;
	}
}

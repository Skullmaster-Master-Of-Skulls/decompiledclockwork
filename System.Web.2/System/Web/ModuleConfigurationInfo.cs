using System;

namespace System.Web
{
	// Token: 0x020000DA RID: 218
	internal class ModuleConfigurationInfo
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x00027EE6 File Offset: 0x000260E6
		internal ModuleConfigurationInfo(string name, string type, string condition)
		{
			this._type = type;
			this._name = name;
			this._precondition = condition;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00027F03 File Offset: 0x00026103
		internal string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00027F0B File Offset: 0x0002610B
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00027F13 File Offset: 0x00026113
		internal string Precondition
		{
			get
			{
				return this._precondition;
			}
		}

		// Token: 0x04000533 RID: 1331
		private string _type;

		// Token: 0x04000534 RID: 1332
		private string _name;

		// Token: 0x04000535 RID: 1333
		private string _precondition;
	}
}

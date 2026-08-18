using System;
using System.Runtime.InteropServices;

namespace Spire.CompoundFile.XLS.Net
{
	// Token: 0x020002C7 RID: 711
	public interface IPropertyData
	{
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002B26 RID: 11046
		object Value { get; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002B27 RID: 11047
		VarEnum Type { get; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002B28 RID: 11048
		string Name { get; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002B29 RID: 11049
		// (set) Token: 0x06002B2A RID: 11050
		int Id { get; set; }

		// Token: 0x06002B2B RID: 11051
		bool SetValue(object value, PropertyType type);
	}
}

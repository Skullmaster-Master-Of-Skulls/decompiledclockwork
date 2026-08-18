using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x0200071F RID: 1823
	[ComVisible(true)]
	public interface IFieldInfo
	{
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004164 RID: 16740
		// (set) Token: 0x06004165 RID: 16741
		string[] FieldNames { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)] set; }

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004166 RID: 16742
		// (set) Token: 0x06004167 RID: 16743
		Type[] FieldTypes { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)] set; }
	}
}

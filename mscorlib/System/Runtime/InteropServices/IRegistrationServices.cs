using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000526 RID: 1318
	[ComVisible(true)]
	[Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
	public interface IRegistrationServices
	{
		// Token: 0x060032EB RID: 13035
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

		// Token: 0x060032EC RID: 13036
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		bool UnregisterAssembly(Assembly assembly);

		// Token: 0x060032ED RID: 13037
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		Type[] GetRegistrableTypesInAssembly(Assembly assembly);

		// Token: 0x060032EE RID: 13038
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		string GetProgIdForType(Type type);

		// Token: 0x060032EF RID: 13039
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void RegisterTypeForComClients(Type type, ref Guid g);

		// Token: 0x060032F0 RID: 13040
		Guid GetManagedCategoryGuid();

		// Token: 0x060032F1 RID: 13041
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		bool TypeRequiresRegistration(Type type);

		// Token: 0x060032F2 RID: 13042
		bool TypeRepresentsComType(Type type);
	}
}

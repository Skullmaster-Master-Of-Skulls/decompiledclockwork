using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200004D RID: 77
	[Guid("8c87810c-2541-4f75-b2d0-9af515488e23")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppIdAuthority
	{
		// Token: 0x06000173 RID: 371
		[SecurityCritical]
		IDefinitionAppId TextToDefinition([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06000174 RID: 372
		[SecurityCritical]
		IReferenceAppId TextToReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06000175 RID: 373
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string DefinitionToText([In] uint Flags, [In] IDefinitionAppId DefinitionAppId);

		// Token: 0x06000176 RID: 374
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string ReferenceToText([In] uint Flags, [In] IReferenceAppId ReferenceAppId);

		// Token: 0x06000177 RID: 375
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreDefinitionsEqual([In] uint Flags, [In] IDefinitionAppId Definition1, [In] IDefinitionAppId Definition2);

		// Token: 0x06000178 RID: 376
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreReferencesEqual([In] uint Flags, [In] IReferenceAppId Reference1, [In] IReferenceAppId Reference2);

		// Token: 0x06000179 RID: 377
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualDefinitionsEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdRight);

		// Token: 0x0600017A RID: 378
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualReferencesEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string AppIdRight);

		// Token: 0x0600017B RID: 379
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesDefinitionMatchReference([In] uint Flags, [In] IDefinitionAppId DefinitionIdentity, [In] IReferenceAppId ReferenceIdentity);

		// Token: 0x0600017C RID: 380
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesTextualDefinitionMatchTextualReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Definition, [MarshalAs(UnmanagedType.LPWStr)] [In] string Reference);

		// Token: 0x0600017D RID: 381
		[SecurityCritical]
		ulong HashReference([In] uint Flags, [In] IReferenceAppId ReferenceIdentity);

		// Token: 0x0600017E RID: 382
		[SecurityCritical]
		ulong HashDefinition([In] uint Flags, [In] IDefinitionAppId DefinitionIdentity);

		// Token: 0x0600017F RID: 383
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateDefinitionKey([In] uint Flags, [In] IDefinitionAppId DefinitionIdentity);

		// Token: 0x06000180 RID: 384
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateReferenceKey([In] uint Flags, [In] IReferenceAppId ReferenceIdentity);

		// Token: 0x06000181 RID: 385
		[SecurityCritical]
		IDefinitionAppId CreateDefinition();

		// Token: 0x06000182 RID: 386
		[SecurityCritical]
		IReferenceAppId CreateReference();
	}
}

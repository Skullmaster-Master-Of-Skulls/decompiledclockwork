using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200004A RID: 74
	[Guid("261a6983-c35d-4d0d-aa5b-7867259e77bc")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IIdentityAuthority
	{
		// Token: 0x06000161 RID: 353
		[SecurityCritical]
		IDefinitionIdentity TextToDefinition([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06000162 RID: 354
		[SecurityCritical]
		IReferenceIdentity TextToReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Identity);

		// Token: 0x06000163 RID: 355
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string DefinitionToText([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

		// Token: 0x06000164 RID: 356
		[SecurityCritical]
		uint DefinitionToTextBuffer([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity, [In] uint BufferSize, [MarshalAs(UnmanagedType.LPArray)] [Out] char[] Buffer);

		// Token: 0x06000165 RID: 357
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string ReferenceToText([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x06000166 RID: 358
		[SecurityCritical]
		uint ReferenceToTextBuffer([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity, [In] uint BufferSize, [MarshalAs(UnmanagedType.LPArray)] [Out] char[] Buffer);

		// Token: 0x06000167 RID: 359
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreDefinitionsEqual([In] uint Flags, [In] IDefinitionIdentity Definition1, [In] IDefinitionIdentity Definition2);

		// Token: 0x06000168 RID: 360
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreReferencesEqual([In] uint Flags, [In] IReferenceIdentity Reference1, [In] IReferenceIdentity Reference2);

		// Token: 0x06000169 RID: 361
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualDefinitionsEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityRight);

		// Token: 0x0600016A RID: 362
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool AreTextualReferencesEqual([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityLeft, [MarshalAs(UnmanagedType.LPWStr)] [In] string IdentityRight);

		// Token: 0x0600016B RID: 363
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesDefinitionMatchReference([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x0600016C RID: 364
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool DoesTextualDefinitionMatchTextualReference([In] uint Flags, [MarshalAs(UnmanagedType.LPWStr)] [In] string Definition, [MarshalAs(UnmanagedType.LPWStr)] [In] string Reference);

		// Token: 0x0600016D RID: 365
		[SecurityCritical]
		ulong HashReference([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x0600016E RID: 366
		[SecurityCritical]
		ulong HashDefinition([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

		// Token: 0x0600016F RID: 367
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateDefinitionKey([In] uint Flags, [In] IDefinitionIdentity DefinitionIdentity);

		// Token: 0x06000170 RID: 368
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GenerateReferenceKey([In] uint Flags, [In] IReferenceIdentity ReferenceIdentity);

		// Token: 0x06000171 RID: 369
		[SecurityCritical]
		IDefinitionIdentity CreateDefinition();

		// Token: 0x06000172 RID: 370
		[SecurityCritical]
		IReferenceIdentity CreateReference();
	}
}

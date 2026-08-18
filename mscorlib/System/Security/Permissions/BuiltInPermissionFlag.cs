using System;

namespace System.Security.Permissions
{
	// Token: 0x02000635 RID: 1589
	[Serializable]
	internal enum BuiltInPermissionFlag
	{
		// Token: 0x04001DC7 RID: 7623
		EnvironmentPermission = 1,
		// Token: 0x04001DC8 RID: 7624
		FileDialogPermission,
		// Token: 0x04001DC9 RID: 7625
		FileIOPermission = 4,
		// Token: 0x04001DCA RID: 7626
		IsolatedStorageFilePermission = 8,
		// Token: 0x04001DCB RID: 7627
		ReflectionPermission = 16,
		// Token: 0x04001DCC RID: 7628
		RegistryPermission = 32,
		// Token: 0x04001DCD RID: 7629
		SecurityPermission = 64,
		// Token: 0x04001DCE RID: 7630
		UIPermission = 128,
		// Token: 0x04001DCF RID: 7631
		PrincipalPermission = 256,
		// Token: 0x04001DD0 RID: 7632
		PublisherIdentityPermission = 512,
		// Token: 0x04001DD1 RID: 7633
		SiteIdentityPermission = 1024,
		// Token: 0x04001DD2 RID: 7634
		StrongNameIdentityPermission = 2048,
		// Token: 0x04001DD3 RID: 7635
		UrlIdentityPermission = 4096,
		// Token: 0x04001DD4 RID: 7636
		ZoneIdentityPermission = 8192,
		// Token: 0x04001DD5 RID: 7637
		KeyContainerPermission = 16384
	}
}

using System;
using System.ComponentModel;

namespace System.DirectoryServices
{
	// Token: 0x0200000F RID: 15
	internal sealed class PropertyAccessTranslator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002638 File Offset: 0x00001638
		internal static int AccessMaskFromPropertyAccess(PropertyAccess access)
		{
			if (access < PropertyAccess.Read || access > PropertyAccess.Write)
			{
				throw new InvalidEnumArgumentException("access", (int)access, typeof(PropertyAccess));
			}
			int result;
			switch (access)
			{
			case PropertyAccess.Read:
				result = ActiveDirectoryRightsTranslator.AccessMaskFromRights(ActiveDirectoryRights.ReadProperty);
				break;
			case PropertyAccess.Write:
				result = ActiveDirectoryRightsTranslator.AccessMaskFromRights(ActiveDirectoryRights.WriteProperty);
				break;
			default:
				throw new ArgumentException("access");
			}
			return result;
		}
	}
}

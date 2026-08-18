using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005DC RID: 1500
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesigntimeLicenseContextSerializer
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x000F1142 File Offset: 0x000EF342
		private DesigntimeLicenseContextSerializer()
		{
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000F114C File Offset: 0x000EF34C
		public static void Serialize(Stream o, string cryptoKey, DesigntimeLicenseContext context)
		{
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(o, new object[]
			{
				cryptoKey,
				context.savedLicenseKeys
			});
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000F117C File Offset: 0x000EF37C
		internal static void Deserialize(Stream o, string cryptoKey, RuntimeLicenseContext context)
		{
			IFormatter formatter = new BinaryFormatter();
			object obj = formatter.Deserialize(o);
			if (obj is object[])
			{
				object[] array = (object[])obj;
				if (array[0] is string && (string)array[0] == cryptoKey)
				{
					context.savedLicenseKeys = (Hashtable)array[1];
				}
			}
		}
	}
}

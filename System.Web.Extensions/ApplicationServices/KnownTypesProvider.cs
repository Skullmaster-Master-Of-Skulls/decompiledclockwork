using System;
using System.Configuration;
using System.Reflection;
using System.Web.Profile;

namespace System.Web.ApplicationServices
{
	// Token: 0x0200011F RID: 287
	public static class KnownTypesProvider
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00036198 File Offset: 0x00034398
		public static Type[] GetKnownTypes(ICustomAttributeProvider knownTypeAttributeTarget)
		{
			if (ProfileBase.Properties == null)
			{
				return new Type[0];
			}
			Type[] array = new Type[ProfileBase.Properties.Count];
			int num = 0;
			foreach (object obj in ProfileBase.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				array[num++] = settingsProperty.PropertyType;
			}
			return array;
		}
	}
}

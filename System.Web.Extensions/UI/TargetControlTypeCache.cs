using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000082 RID: 130
	internal static class TargetControlTypeCache
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00019FA8 File Offset: 0x000181A8
		public static Type[] GetTargetControlTypes(Type extenderControlType)
		{
			Type[] array = (Type[])TargetControlTypeCache._targetControlTypeCache[extenderControlType];
			if (array == null)
			{
				array = TargetControlTypeCache.GetTargetControlTypesInternal(extenderControlType);
				TargetControlTypeCache._targetControlTypeCache[extenderControlType] = array;
			}
			return array;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00019FE0 File Offset: 0x000181E0
		private static Type[] GetTargetControlTypesInternal(Type extenderControlType)
		{
			object[] customAttributes = extenderControlType.GetCustomAttributes(typeof(TargetControlTypeAttribute), true);
			Type[] array = new Type[customAttributes.Length];
			for (int i = 0; i < customAttributes.Length; i++)
			{
				array[i] = ((TargetControlTypeAttribute)customAttributes[i]).TargetControlType;
			}
			return array;
		}

		// Token: 0x04000203 RID: 515
		private static readonly Hashtable _targetControlTypeCache = Hashtable.Synchronized(new Hashtable());
	}
}

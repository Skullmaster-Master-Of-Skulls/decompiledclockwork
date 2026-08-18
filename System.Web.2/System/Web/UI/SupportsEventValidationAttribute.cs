using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000304 RID: 772
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class SupportsEventValidationAttribute : Attribute
	{
		// Token: 0x060023C1 RID: 9153 RVA: 0x000746A8 File Offset: 0x000728A8
		internal static bool SupportsEventValidation(Type type)
		{
			object obj = SupportsEventValidationAttribute._typesSupportsEventValidation[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(SupportsEventValidationAttribute), false);
			bool flag = customAttributes != null && customAttributes.Length != 0;
			SupportsEventValidationAttribute._typesSupportsEventValidation[type] = flag;
			return flag;
		}

		// Token: 0x04001CCA RID: 7370
		private static Hashtable _typesSupportsEventValidation = Hashtable.Synchronized(new Hashtable());
	}
}

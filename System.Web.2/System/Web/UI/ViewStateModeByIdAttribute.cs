using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200032D RID: 813
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ViewStateModeByIdAttribute : Attribute
	{
		// Token: 0x060025E2 RID: 9698 RVA: 0x0007CC4C File Offset: 0x0007AE4C
		internal static bool IsEnabled(Type type)
		{
			if (!ViewStateModeByIdAttribute._viewStateIdTypes.ContainsKey(type))
			{
				AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
				ViewStateModeByIdAttribute viewStateModeByIdAttribute = (ViewStateModeByIdAttribute)attributes[typeof(ViewStateModeByIdAttribute)];
				ViewStateModeByIdAttribute._viewStateIdTypes[type] = (viewStateModeByIdAttribute != null);
			}
			return (bool)ViewStateModeByIdAttribute._viewStateIdTypes[type];
		}

		// Token: 0x04001DA4 RID: 7588
		private static Hashtable _viewStateIdTypes = Hashtable.Synchronized(new Hashtable());
	}
}

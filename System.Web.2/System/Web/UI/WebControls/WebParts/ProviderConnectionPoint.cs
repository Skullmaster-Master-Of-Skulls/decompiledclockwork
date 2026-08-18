using System;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000563 RID: 1379
	public class ProviderConnectionPoint : ConnectionPoint
	{
		// Token: 0x06004609 RID: 17929 RVA: 0x000E7080 File Offset: 0x000E5280
		static ProviderConnectionPoint()
		{
			ConstructorInfo constructor = typeof(ProviderConnectionPoint).GetConstructors()[0];
			ProviderConnectionPoint.ConstructorTypes = WebPartUtil.GetTypesForConstructor(constructor);
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000E19D2 File Offset: 0x000DFBD2
		public ProviderConnectionPoint(MethodInfo callbackMethod, Type interfaceType, Type controlType, string displayName, string id, bool allowsMultipleConnections) : base(callbackMethod, interfaceType, controlType, displayName, id, allowsMultipleConnections)
		{
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x000E70AA File Offset: 0x000E52AA
		public virtual ConnectionInterfaceCollection GetSecondaryInterfaces(Control control)
		{
			return ConnectionInterfaceCollection.Empty;
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000E70B1 File Offset: 0x000E52B1
		public virtual object GetObject(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return base.CallbackMethod.Invoke(control, null);
		}

		// Token: 0x0400268E RID: 9870
		internal static readonly Type[] ConstructorTypes;
	}
}

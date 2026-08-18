using System;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000535 RID: 1333
	public class ConsumerConnectionPoint : ConnectionPoint
	{
		// Token: 0x060043F2 RID: 17394 RVA: 0x000E19A8 File Offset: 0x000DFBA8
		static ConsumerConnectionPoint()
		{
			ConstructorInfo constructor = typeof(ConsumerConnectionPoint).GetConstructors()[0];
			ConsumerConnectionPoint.ConstructorTypes = WebPartUtil.GetTypesForConstructor(constructor);
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000E19D2 File Offset: 0x000DFBD2
		public ConsumerConnectionPoint(MethodInfo callbackMethod, Type interfaceType, Type controlType, string displayName, string id, bool allowsMultipleConnections) : base(callbackMethod, interfaceType, controlType, displayName, id, allowsMultipleConnections)
		{
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000E19E3 File Offset: 0x000DFBE3
		public virtual void SetObject(Control control, object data)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			base.CallbackMethod.Invoke(control, new object[]
			{
				data
			});
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool SupportsConnection(Control control, ConnectionInterfaceCollection secondaryInterfaces)
		{
			return true;
		}

		// Token: 0x0400261C RID: 9756
		internal static readonly Type[] ConstructorTypes;
	}
}

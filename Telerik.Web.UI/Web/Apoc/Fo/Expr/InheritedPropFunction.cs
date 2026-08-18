using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B4 RID: 5044
	internal class InheritedPropFunction : FunctionBase
	{
		// Token: 0x170042E8 RID: 17128
		// (get) Token: 0x0600D145 RID: 53573 RVA: 0x002E467E File Offset: 0x002E287E
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D146 RID: 53574 RVA: 0x002E4684 File Offset: 0x002E2884
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			string @string = args[0].GetString();
			if (@string == null)
			{
				throw new PropertyException("Incorrect parameter to inherited-property-value function");
			}
			return pInfo.getPropertyList().GetInheritedProperty(@string);
		}
	}
}

using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B9 RID: 5049
	internal class NearestSpecPropFunction : FunctionBase
	{
		// Token: 0x170042EC RID: 17132
		// (get) Token: 0x0600D155 RID: 53589 RVA: 0x002E4877 File Offset: 0x002E2A77
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D156 RID: 53590 RVA: 0x002E487C File Offset: 0x002E2A7C
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			string @string = args[0].GetString();
			if (@string == null)
			{
				throw new PropertyException("Incorrect parameter to from-nearest-specified-value function");
			}
			return pInfo.getPropertyList().GetNearestSpecifiedProperty(@string);
		}
	}
}

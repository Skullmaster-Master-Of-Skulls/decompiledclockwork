using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000734 RID: 1844
	[Serializable]
	internal class DynamicTypeInfo : TypeInfo
	{
		// Token: 0x0600420A RID: 16906 RVA: 0x000E097D File Offset: 0x000DF97D
		internal DynamicTypeInfo(Type typeOfObj) : base(typeOfObj)
		{
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x000E0986 File Offset: 0x000DF986
		public override bool CanCastTo(Type castType, object o)
		{
			return ((MarshalByRefObject)o).IsInstanceOfType(castType);
		}
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020006A2 RID: 1698
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x06003D66 RID: 15718 RVA: 0x000D2233 File Offset: 0x000D1233
		internal ConstructionLevelActivator()
		{
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000D223B File Offset: 0x000D123B
		// (set) Token: 0x06003D68 RID: 15720 RVA: 0x000D223E File Offset: 0x000D123E
		public virtual IActivator NextActivator
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x000D2245 File Offset: 0x000D1245
		public virtual ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x000D2248 File Offset: 0x000D1248
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoServerContextActivation(ctorMsg);
		}
	}
}

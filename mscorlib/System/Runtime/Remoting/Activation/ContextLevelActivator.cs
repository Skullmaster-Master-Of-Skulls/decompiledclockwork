using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020006A1 RID: 1697
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06003D60 RID: 15712 RVA: 0x000D21C1 File Offset: 0x000D11C1
		internal ContextLevelActivator()
		{
			this.m_NextActivator = null;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000D21D0 File Offset: 0x000D11D0
		internal ContextLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x000D2206 File Offset: 0x000D1206
		// (set) Token: 0x06003D63 RID: 15715 RVA: 0x000D220E File Offset: 0x000D120E
		public virtual IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000D2217 File Offset: 0x000D1217
		public virtual ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x000D221A File Offset: 0x000D121A
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = ctorMsg.Activator.NextActivator;
			return ActivationServices.DoCrossContextActivation(ctorMsg);
		}

		// Token: 0x04001F6B RID: 8043
		private IActivator m_NextActivator;
	}
}

using System;

namespace Telerik.Licensing
{
	// Token: 0x0200040B RID: 1035
	internal class ComponentUsedEventArgs : EventArgs
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x0007CF98 File Offset: 0x0007B198
		public ComponentUsedEventArgs(Type type, string sessionId)
		{
			this._type = type;
			this._sessionId = sessionId;
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x0007CFAE File Offset: 0x0007B1AE
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x0007CFB6 File Offset: 0x0007B1B6
		public string SessionId
		{
			get
			{
				return this._sessionId;
			}
		}

		// Token: 0x040009A5 RID: 2469
		private readonly Type _type;

		// Token: 0x040009A6 RID: 2470
		private readonly string _sessionId;
	}
}

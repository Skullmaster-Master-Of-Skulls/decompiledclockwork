using System;

namespace Telerik.Licensing
{
	// Token: 0x0200040D RID: 1037
	internal class ProductUsedEventArgs : ComponentUsedEventArgs
	{
		// Token: 0x060025C3 RID: 9667 RVA: 0x0007CFBE File Offset: 0x0007B1BE
		public ProductUsedEventArgs(Type type, string sessionId) : base(type, sessionId)
		{
			this._key = string.Empty;
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x0007CFD3 File Offset: 0x0007B1D3
		public string InstalationKey
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x040009A7 RID: 2471
		private readonly string _key;
	}
}

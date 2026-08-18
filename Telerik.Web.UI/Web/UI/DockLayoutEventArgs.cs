using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001041 RID: 4161
	public class DockLayoutEventArgs : EventArgs
	{
		// Token: 0x0600A3B0 RID: 41904 RVA: 0x00246AE8 File Offset: 0x00244CE8
		public DockLayoutEventArgs(Dictionary<string, string> positions, Dictionary<string, int> indices)
		{
			this._positions = positions;
			this._indices = indices;
		}

		// Token: 0x170033A7 RID: 13223
		// (get) Token: 0x0600A3B1 RID: 41905 RVA: 0x00246AFE File Offset: 0x00244CFE
		public Dictionary<string, string> Positions
		{
			get
			{
				return this._positions;
			}
		}

		// Token: 0x170033A8 RID: 13224
		// (get) Token: 0x0600A3B2 RID: 41906 RVA: 0x00246B06 File Offset: 0x00244D06
		public Dictionary<string, int> Indices
		{
			get
			{
				return this._indices;
			}
		}

		// Token: 0x04002D8F RID: 11663
		private readonly Dictionary<string, string> _positions;

		// Token: 0x04002D90 RID: 11664
		private readonly Dictionary<string, int> _indices;
	}
}

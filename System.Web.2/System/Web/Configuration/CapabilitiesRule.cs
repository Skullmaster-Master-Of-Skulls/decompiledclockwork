using System;

namespace System.Web.Configuration
{
	// Token: 0x020006B3 RID: 1715
	internal abstract class CapabilitiesRule
	{
		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x00124693 File Offset: 0x00122893
		internal virtual int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06005317 RID: 21271
		internal abstract void Evaluate(CapabilitiesState state);

		// Token: 0x04002B94 RID: 11156
		internal const int Use = 0;

		// Token: 0x04002B95 RID: 11157
		internal const int Assign = 1;

		// Token: 0x04002B96 RID: 11158
		internal const int Filter = 2;

		// Token: 0x04002B97 RID: 11159
		internal const int Case = 3;

		// Token: 0x04002B98 RID: 11160
		internal int _type;
	}
}

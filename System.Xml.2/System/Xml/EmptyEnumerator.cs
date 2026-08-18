using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x0200006D RID: 109
	internal sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000EE7D File Offset: 0x0000D07D
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000EE80 File Offset: 0x0000D080
		void IEnumerator.Reset()
		{
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000EE82 File Offset: 0x0000D082
		object IEnumerator.Current
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}
	}
}

using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007D6 RID: 2006
	internal interface IStreamable
	{
		// Token: 0x0600472B RID: 18219
		void Read(__BinaryParser input);

		// Token: 0x0600472C RID: 18220
		void Write(__BinaryWriter sout);
	}
}

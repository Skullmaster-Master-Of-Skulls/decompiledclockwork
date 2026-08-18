using System;
using System.Drawing;

namespace Spire.Doc.Interface
{
	// Token: 0x02000368 RID: 872
	public interface IXDLSAttributeWriter
	{
		// Token: 0x06003102 RID: 12546
		void WriteValue(string name, float value);

		// Token: 0x06003103 RID: 12547
		void WriteValue(string name, double value);

		// Token: 0x06003104 RID: 12548
		void WriteValue(string name, int value);

		// Token: 0x06003105 RID: 12549
		void WriteValue(string name, string value);

		// Token: 0x06003106 RID: 12550
		void WriteValue(string name, Enum value);

		// Token: 0x06003107 RID: 12551
		void WriteValue(string name, bool value);

		// Token: 0x06003108 RID: 12552
		void WriteValue(string name, Color value);

		// Token: 0x06003109 RID: 12553
		void WriteValue(string name, DateTime value);
	}
}

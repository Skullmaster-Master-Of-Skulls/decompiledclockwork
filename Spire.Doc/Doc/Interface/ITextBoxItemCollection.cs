using System;

namespace Spire.Doc.Interface
{
	// Token: 0x0200050D RID: 1293
	public interface ITextBoxItemCollection
	{
		// Token: 0x1700043C RID: 1084
		ITextBox this[int index]
		{
			get;
		}

		// Token: 0x06004270 RID: 17008
		int Add(ITextBox textBox);
	}
}

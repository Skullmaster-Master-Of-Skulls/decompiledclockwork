using System;
using Spire.Doc.Fields;

namespace Spire.Doc.Collections
{
	// Token: 0x0200053C RID: 1340
	public class TextBoxCollection : CollectionEx
	{
		// Token: 0x06004610 RID: 17936 RVA: 0x0040D350 File Offset: 0x0040C350
		internal TextBoxCollection(Document A_0) : base(A_0, A_0)
		{
		}

		// Token: 0x1700054F RID: 1359
		public TextBox this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.InnerList[index] as TextBox;
			}
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x0040D3B4 File Offset: 0x0040C3B4
		public void RemoveAt(int index)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			TextBox textBox = base.InnerList[index] as TextBox;
			textBox.OwnerParagraph.Items.Remove(textBox);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x0040D414 File Offset: 0x0040C414
		public void Clear()
		{
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 3:
					{
						if (base.InnerList.Count <= 0)
						{
							num = 0;
							continue;
						}
						int index = base.InnerList.Count - 1;
						this.RemoveAt(index);
						if (true)
						{
						}
						num = 2;
						continue;
					}
					}
					break;
				}
				num = 3;
			}
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x0040D4B4 File Offset: 0x0040C4B4
		internal void ᜁ(TextBox A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x0040D4FC File Offset: 0x0040C4FC
		internal void ᜀ(TextBox A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InnerList.Remove(A_0);
		}
	}
}

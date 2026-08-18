using System;
using System.Drawing;
using System.Drawing.Text;
using Spire.DataExport.PropEditors;

// Token: 0x02000031 RID: 49
internal class sprᣓ : ListComponentEditor
{
	// Token: 0x06000197 RID: 407 RVA: 0x0000EC38 File Offset: 0x0000DC38
	public virtual void ᜀ()
	{
		switch (0)
		{
		default:
		{
			FontFamily[] array;
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_A5:
				FontFamily fontFamily = array[num];
				this.m_listBox.Items.Add(fontFamily.Name);
				num++;
				num2 = 1;
				break;
			}
			default:
				if (false)
				{
				}
				goto IL_4B;
			}
			for (;;)
			{
				IL_2C:
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_8B;
				case 2:
					if (num >= array.Length)
					{
						num2 = 0;
						continue;
					}
					goto IL_A5;
				case 3:
					goto IL_8B;
				}
				goto IL_4B;
				IL_8B:
				num2 = 2;
			}
			return;
			IL_4B:
			this.m_listBox.Sorted = true;
			this.m_listBox.Items.Clear();
			FontFamily[] families = new InstalledFontCollection().Families;
			array = families;
			num = 0;
			num2 = 3;
			goto IL_2C;
		}
		}
	}
}

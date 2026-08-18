using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.Forms;

// Token: 0x0200001C RID: 28
internal class sprᰍ : UITypeEditor
{
	// Token: 0x06000108 RID: 264 RVA: 0x0000AA7C File Offset: 0x00009A7C
	public virtual UITypeEditorEditStyle ᜀ(ITypeDescriptorContext A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.Instance != null)
				{
					num = 3;
					continue;
				}
				goto IL_77;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_77;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				return UITypeEditorEditStyle.Modal;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				goto IL_77;
			}
			num = 1;
		}
		return UITypeEditorEditStyle.Modal;
		IL_77:
		return base.GetEditStyle(A_0);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000AB08 File Offset: 0x00009B08
	public virtual object ᜀ(ITypeDescriptorContext A_0, IServiceProvider A_1, object A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return A_2;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 3:
				AboutDataExport.ShowAbout(true);
				num = 0;
				continue;
			case 4:
				if (A_0.Instance != null)
				{
					num = 3;
					continue;
				}
				return A_2;
			}
			IL_24:
			if (A_0 != null)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			break;
			goto IL_24;
		}
		return A_2;
	}
}

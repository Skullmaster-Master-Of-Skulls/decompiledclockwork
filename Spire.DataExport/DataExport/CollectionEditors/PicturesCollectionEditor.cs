using System;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x02000195 RID: 405
	public class PicturesCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x00073400 File Offset: 0x00072400
		public PicturesCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00073414 File Offset: 0x00072414
		protected override object CreateInstance(Type itemType)
		{
			int a_ = 9;
			CellPicture cellPicture;
			for (;;)
			{
				cellPicture = (base.CreateInstance(itemType) as CellPicture);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.Context.Instance is CellExport)
						{
							num = 6;
							continue;
						}
						goto IL_16E;
					case 1:
						num = 0;
						continue;
					case 2:
						goto IL_67;
					case 3:
					{
						if (true)
						{
						}
						int num2;
						int num3;
						if (!(base.Context.Instance as CellExport).Pictures.Find(string.Format(HyperlinksCollectionEditor.b("甤並䨨弪堬崮吰氲临ܶ䐸", a_), num2), ref num3))
						{
							num = 9;
							continue;
						}
						num2++;
						num = 10;
						continue;
					}
					case 4:
						if (base.Context.Instance != null)
						{
							num = 1;
							continue;
						}
						goto IL_16E;
					case 5:
						num = 4;
						continue;
					case 6:
					{
						int num2 = 0;
						int num3 = 0;
						num = 2;
						continue;
					}
					case 7:
						goto IL_16E;
					case 8:
						IL_52:
						if (base.Context != null)
						{
							num = 5;
							continue;
						}
						goto IL_16E;
					case 9:
					{
						int num2;
						cellPicture.Name = string.Format(HyperlinksCollectionEditor.b("甤並䨨弪堬崮吰氲临ܶ䐸", a_), num2);
						num = 7;
						continue;
					}
					case 10:
						goto IL_67;
					}
					break;
					IL_67:
					num = 3;
					continue;
					IL_16E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_184;
					}
				}
			}
			IL_184:
			if (false)
			{
			}
			return cellPicture;
		}
	}
}

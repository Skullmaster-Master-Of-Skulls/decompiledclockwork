using System;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x02000196 RID: 406
	public class SheetsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x000735AC File Offset: 0x000725AC
		public SheetsCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000735C0 File Offset: 0x000725C0
		protected override object CreateInstance(Type itemType)
		{
			int a_ = 7;
			WorkSheet workSheet;
			for (;;)
			{
				IL_2D:
				workSheet = (base.CreateInstance(itemType) as WorkSheet);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_E4:
					num = 1;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.Context != null)
						{
							num = 5;
							continue;
						}
						return workSheet;
					case 1:
						return workSheet;
					case 2:
						if (base.Context.Instance is CellExport)
						{
							num = 4;
							continue;
						}
						return workSheet;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_119;
					case 5:
						num = 6;
						continue;
					case 6:
						if (base.Context.Instance != null)
						{
							num = 3;
							continue;
						}
						return workSheet;
					}
					goto IL_2D;
				}
				IL_119:
				workSheet.ExportCell = (base.Context.Instance as CellExport);
				workSheet.SheetName = string.Format(HyperlinksCollectionEditor.b("瀢䴤䈦䰨弪ബ吮İ串", a_), (base.Context.Instance as CellExport).Sheets.Count + 1);
				workSheet.Name = workSheet.SheetName;
				goto IL_E4;
			}
			return workSheet;
		}
	}
}

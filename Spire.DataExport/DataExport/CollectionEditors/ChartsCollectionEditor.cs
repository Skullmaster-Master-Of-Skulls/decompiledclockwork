using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x02000198 RID: 408
	public class ChartsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x00073AAC File Offset: 0x00072AAC
		public ChartsCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00073AC0 File Offset: 0x00072AC0
		private void ᜀ(ITypeDescriptorContext A_0, Chart A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Instance is WorkSheet)
					{
						num = 3;
						continue;
					}
					return;
				case 1:
					if (A_1.DataRangeSheet.Equals(string.Empty))
					{
						num = 14;
						continue;
					}
					goto IL_1C3;
				case 3:
					A_1.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.ListView = (A_0.Instance as WorkSheet).ListView;
					num = 1;
					continue;
				case 4:
					if (A_0.Instance is CellExport)
					{
						num = 12;
						continue;
					}
					num = 0;
					continue;
				case 5:
					if (A_0.Instance != null)
					{
						num = 10;
						continue;
					}
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 7:
					goto IL_20A;
				case 8:
					if (A_1.DataRangeSheet.Equals(string.Empty))
					{
						num = 13;
						continue;
					}
					goto IL_E4;
				case 9:
					goto IL_1E4;
				case 10:
					num = 4;
					continue;
				case 11:
					goto IL_1C3;
				case 12:
					A_1.ExportSource = (A_0.Instance as CellExport).DataSource;
					A_1.Command = (A_0.Instance as CellExport).SQLCommand;
					A_1.DataTable = (A_0.Instance as CellExport).DataTable;
					A_1.ListView = (A_0.Instance as CellExport).ListView;
					num = 8;
					continue;
				case 13:
					A_1.DataRangeSheet = (A_0.Instance as CellExport).SheetName;
					num = 7;
					continue;
				case 14:
					A_1.DataRangeSheet = (A_0.Instance as WorkSheet).SheetName;
					num = 11;
					continue;
				}
				if (A_0 != null)
				{
					num = 6;
					continue;
				}
				return;
				IL_1C3:
				A_1.CellExport = (A_0.Instance as WorkSheet).ᜀ;
				num = 9;
			}
			IL_E4:
			if (true)
			{
			}
			A_1.CellExport = (A_0.Instance as CellExport);
			return;
			IL_1E4:
			return;
			IL_20A:
			goto IL_E4;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00073D64 File Offset: 0x00072D64
		protected override object CreateInstance(Type itemType)
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
			Chart chart = base.CreateInstance(itemType) as Chart;
			this.ᜀ(base.Context, chart);
			return chart;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00073DBC File Offset: 0x00072DBC
		protected override object[] GetItems(object editValue)
		{
			int num = 4;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_104;
				case 2:
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_BE;
							case 2:
								goto IL_9C;
							case 3:
							{
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								Chart a_ = (Chart)enumerator.Current;
								this.ᜀ(base.Context, a_);
								num = 2;
								continue;
							}
							case 4:
								goto IL_B4;
							}
							goto IL_5B;
							IL_9C:
							num = 3;
							continue;
							IL_5B:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_B4:
								num = 1;
								break;
							default:
								if (false)
								{
								}
								goto IL_9C;
							}
						}
						IL_BE:
						goto IL_152;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									disposable.Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_101;
								case 2:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_103;
								}
								break;
							}
						}
						IL_101:
						IL_103:;
					}
					goto IL_104;
				case 3:
					if (true)
					{
					}
					if (editValue is Charts)
					{
						num = 1;
						continue;
					}
					goto IL_152;
				}
				if (editValue != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_104:
				enumerator = (editValue as Charts).GetEnumerator();
				num = 2;
			}
			IL_152:
			return base.GetItems(editValue);
		}
	}
}

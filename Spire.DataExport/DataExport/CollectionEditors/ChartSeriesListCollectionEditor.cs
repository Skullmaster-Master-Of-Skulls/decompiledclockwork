using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x0200019B RID: 411
	public class ChartSeriesListCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x000748B0 File Offset: 0x000738B0
		public ChartSeriesListCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000748C4 File Offset: 0x000738C4
		private void ᜀ(ITypeDescriptorContext A_0, ChartSeries A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					num = 3;
					continue;
				case 3:
					if (A_0.Instance != null)
					{
						num = 6;
						continue;
					}
					return;
				case 4:
					goto IL_11E;
				case 5:
					A_1.DataRangeSheet = (A_0.Instance as Chart).DataRangeSheet;
					if (true)
					{
					}
					num = 4;
					continue;
				case 6:
					goto IL_167;
				case 7:
					A_1.ExportSource = (A_0.Instance as Chart).ExportSource;
					A_1.Command = (A_0.Instance as Chart).Command;
					A_1.DataTable = (A_0.Instance as Chart).DataTable;
					A_1.ListView = (A_0.Instance as Chart).ListView;
					num = 8;
					continue;
				case 8:
					if (A_1.DataRangeSheet.Equals(string.Empty))
					{
						num = 5;
						continue;
					}
					goto IL_11E;
				case 9:
					if (A_0.Instance is Chart)
					{
						num = 7;
						continue;
					}
					return;
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_11E:
				A_1.CellExport = (A_0.Instance as Chart).CellExport;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_167:
					num = 9;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00074A64 File Offset: 0x00073A64
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
			ChartSeries chartSeries = base.CreateInstance(itemType) as ChartSeries;
			this.ᜀ(base.Context, chartSeries);
			return chartSeries;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00074ABC File Offset: 0x00073ABC
		protected override object[] GetItems(object editValue)
		{
			int num = 4;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
								{
									if (false)
									{
									}
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									ChartSeries a_ = (ChartSeries)enumerator.Current;
									this.ᜀ(base.Context, a_);
									break;
								}
								}
								num = 2;
								continue;
							case 4:
								goto IL_BE;
							}
							IL_80:
							num = 1;
							continue;
							goto IL_80;
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
				case 1:
					if (true)
					{
					}
					if (editValue is ChartSeriesList)
					{
						num = 3;
						continue;
					}
					goto IL_152;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_104;
				}
				if (editValue != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_104:
				enumerator = (editValue as ChartSeriesList).GetEnumerator();
				num = 0;
			}
			IL_152:
			return base.GetItems(editValue);
		}
	}
}

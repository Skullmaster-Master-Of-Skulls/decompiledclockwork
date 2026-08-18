using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x0200019C RID: 412
	public class CellsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B3D RID: 2877 RVA: 0x00074C34 File Offset: 0x00073C34
		public CellsCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00074C48 File Offset: 0x00073C48
		private void ᜀ(ITypeDescriptorContext A_0, Cell A_1)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Instance is WorkSheet)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					A_1.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Format.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.Format.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.Format.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.ListView = (A_0.Instance as WorkSheet).ListView;
					A_1.Format.ListView = (A_0.Instance as WorkSheet).ListView;
					A_1.DateTimeFormat = (A_0.Instance as WorkSheet).FormatsExport.DateTime;
					A_1.NumericFormat = (A_0.Instance as WorkSheet).FormatsExport.Float;
					A_1.CultureName = (A_0.Instance as WorkSheet).FormatsExport.CultureName;
					num = 4;
					continue;
				case 2:
					goto IL_31E;
				case 3:
					num = 8;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_195;
					default:
						goto IL_2E6;
					}
					break;
				case 6:
					if (A_0.Instance is CellExport)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				case 7:
					num = 6;
					continue;
				case 8:
					goto IL_195;
				}
				if (A_0 != null)
				{
					num = 3;
					continue;
				}
				return;
				IL_195:
				if (A_0.Instance == null)
				{
					return;
				}
				num = 7;
			}
			IL_2E6:
			if (false)
			{
			}
			return;
			IL_31E:
			A_1.ExportSource = (A_0.Instance as CellExport).DataSource;
			A_1.Format.ExportSource = (A_0.Instance as CellExport).DataSource;
			A_1.Command = (A_0.Instance as CellExport).SQLCommand;
			A_1.Format.Command = (A_0.Instance as CellExport).SQLCommand;
			A_1.DataTable = (A_0.Instance as CellExport).DataTable;
			A_1.Format.DataTable = (A_0.Instance as CellExport).DataTable;
			A_1.ListView = (A_0.Instance as CellExport).ListView;
			A_1.Format.ListView = (A_0.Instance as CellExport).ListView;
			A_1.DateTimeFormat = (A_0.Instance as CellExport).DataFormats.DateTime;
			A_1.NumericFormat = (A_0.Instance as CellExport).DataFormats.Float;
			A_1.CultureName = (A_0.Instance as CellExport).DataFormats.CultureName;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00074F78 File Offset: 0x00073F78
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
			Cell cell = base.CreateInstance(itemType) as Cell;
			this.ᜀ(base.Context, cell);
			return cell;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00074FD0 File Offset: 0x00073FD0
		protected override object[] GetItems(object editValue)
		{
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_152;
				default:
				{
					if (false)
					{
					}
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 2:
						goto IL_10E;
					case 3:
						try
						{
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 1;
									continue;
								case 1:
									goto IL_C8;
								case 3:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									Cell a_ = (Cell)enumerator.Current;
									this.ᜀ(base.Context, a_);
									num = 2;
									continue;
								}
								}
								IL_A6:
								num = 3;
								continue;
								goto IL_A6;
							}
							IL_C8:
							goto IL_152;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_10D;
									case 2:
										goto IL_10B;
									}
									break;
								}
							}
							IL_10B:
							IL_10D:;
						}
						goto IL_10E;
					case 4:
						if (true)
						{
						}
						if (editValue is Cells)
						{
							num = 2;
							continue;
						}
						goto IL_152;
					}
					if (editValue != null)
					{
						num = 0;
						break;
					}
					goto IL_152;
					IL_10E:
					enumerator = (editValue as Cells).GetEnumerator();
					num = 3;
					break;
				}
				}
			}
			IL_152:
			return base.GetItems(editValue);
		}
	}
}

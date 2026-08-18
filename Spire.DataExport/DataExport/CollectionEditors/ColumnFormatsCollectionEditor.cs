using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x02000197 RID: 407
	public class ColumnFormatsCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x00073714 File Offset: 0x00072714
		public ColumnFormatsCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00073728 File Offset: 0x00072728
		private void ᜀ(ITypeDescriptorContext A_0, ColumnFormat A_1)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D8;
				case 1:
					goto IL_1A1;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_179;
				case 4:
					A_1.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.ListView = (A_0.Instance as WorkSheet).ListView;
					num = 3;
					continue;
				case 5:
					if (A_0.Instance is WorkSheet)
					{
						num = 4;
						continue;
					}
					return;
				case 7:
					if (A_0.Instance is CellExport)
					{
						num = 1;
						continue;
					}
					if (true)
					{
					}
					num = 5;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D8;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				if (A_0 != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_D8:
				if (A_0.Instance == null)
				{
					break;
				}
				num = 8;
			}
			IL_179:
			return;
			IL_1A1:
			A_1.ExportSource = (A_0.Instance as CellExport).DataSource;
			A_1.Command = (A_0.Instance as CellExport).SQLCommand;
			A_1.DataTable = (A_0.Instance as CellExport).DataTable;
			A_1.ListView = (A_0.Instance as CellExport).ListView;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000738DC File Offset: 0x000728DC
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
			ColumnFormat columnFormat = base.CreateInstance(itemType) as ColumnFormat;
			this.ᜀ(base.Context, columnFormat);
			return columnFormat;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00073934 File Offset: 0x00072934
		protected override object[] GetItems(object editValue)
		{
			int num = 0;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 1:
					goto IL_104;
				case 2:
					if (true)
					{
					}
					if (editValue is ColumnFormats)
					{
						num = 1;
						continue;
					}
					goto IL_152;
				case 3:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_BE;
							case 3:
								num = 0;
								continue;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									goto IL_AC;
								}
								ColumnFormat a_ = (ColumnFormat)enumerator.Current;
								this.ᜀ(base.Context, a_);
								num = 2;
								continue;
							}
							}
							IL_80:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_AC:
								num = 3;
								continue;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
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
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_103;
								case 1:
									goto IL_101;
								case 2:
									disposable.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_101:
						IL_103:;
					}
					goto IL_104;
				case 4:
					num = 2;
					continue;
				}
				if (editValue != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_104:
				enumerator = (editValue as ColumnFormats).GetEnumerator();
				num = 3;
			}
			IL_152:
			return base.GetItems(editValue);
		}
	}
}

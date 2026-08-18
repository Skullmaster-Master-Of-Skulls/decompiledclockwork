using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x0200019D RID: 413
	public class HyperlinksCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00075148 File Offset: 0x00074148
		public HyperlinksCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0007515C File Offset: 0x0007415C
		private void ᜀ(ITypeDescriptorContext A_0, CellHyperlink A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Instance != null)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					num = 0;
					continue;
				case 3:
					A_1.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Format.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.Format.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.Format.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.ListView = (A_0.Instance as WorkSheet).ListView;
					A_1.Format.ListView = (A_0.Instance as WorkSheet).ListView;
					num = 5;
					continue;
				case 4:
					num = 8;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						goto IL_24E;
					}
					break;
				case 6:
					if (A_0.Instance is WorkSheet)
					{
						num = 3;
						continue;
					}
					return;
				case 7:
					goto IL_27C;
				case 8:
					if (A_0.Instance is CellExport)
					{
						num = 7;
						continue;
					}
					goto IL_47;
				}
				if (A_0 != null)
				{
					num = 2;
					continue;
				}
				return;
				IL_47:
				if (true)
				{
				}
				num = 6;
			}
			IL_24E:
			if (false)
			{
			}
			return;
			IL_27C:
			A_1.ExportSource = (A_0.Instance as CellExport).DataSource;
			A_1.Format.ExportSource = (A_0.Instance as CellExport).DataSource;
			A_1.Command = (A_0.Instance as CellExport).SQLCommand;
			A_1.Format.Command = (A_0.Instance as CellExport).SQLCommand;
			A_1.DataTable = (A_0.Instance as CellExport).DataTable;
			A_1.Format.DataTable = (A_0.Instance as CellExport).DataTable;
			A_1.ListView = (A_0.Instance as CellExport).ListView;
			A_1.Format.ListView = (A_0.Instance as CellExport).ListView;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000753EC File Offset: 0x000743EC
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
			CellHyperlink cellHyperlink = base.CreateInstance(itemType) as CellHyperlink;
			this.ᜀ(base.Context, cellHyperlink);
			return cellHyperlink;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00075444 File Offset: 0x00074444
		protected override object[] GetItems(object editValue)
		{
			int num = 4;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (editValue is CellHyperlinks)
					{
						num = 2;
						continue;
					}
					goto IL_152;
				case 1:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 2:
								goto IL_BE;
							case 3:
								num = 2;
								continue;
							case 4:
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
										num = 3;
										continue;
									}
									CellHyperlink a_ = (CellHyperlink)enumerator.Current;
									this.ᜀ(base.Context, a_);
									num = 0;
									continue;
								}
								}
								break;
							}
							IL_80:
							num = 4;
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
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_103;
								case 1:
									disposable.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_101;
								}
								break;
							}
						}
						IL_101:
						IL_103:;
					}
					goto IL_104;
				case 2:
					goto IL_104;
				case 3:
					num = 0;
					continue;
				}
				if (editValue != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_104:
				enumerator = (editValue as CellHyperlinks).GetEnumerator();
				num = 1;
			}
			IL_152:
			return base.GetItems(editValue);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000755BC File Offset: 0x000745BC
		internal static string b(string A_0, int A_1)
		{
			char[] array = A_0.ToCharArray();
			int num = 52376859 + A_1;
			int num3;
			int num2;
			if ((num2 = (num3 = 0)) < 1)
			{
				goto IL_47;
			}
			IL_14:
			int num5;
			int num4 = num5 = num2;
			char[] array2 = array;
			int num6 = num5;
			char c = array[num5];
			byte b = (byte)((int)(c & 'ÿ') ^ num++);
			byte b2 = (byte)((int)(c >> 8) ^ num++);
			byte b3 = b2;
			b2 = b;
			b = b3;
			array2[num6] = (ushort)((int)b2 << 8 | (int)b);
			num3 = num4 + 1;
			IL_47:
			if ((num2 = num3) >= array.Length)
			{
				return string.Intern(new string(array));
			}
			goto IL_14;
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x02000199 RID: 409
	public class CellItemStylesCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x00073F34 File Offset: 0x00072F34
		public CellItemStylesCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00073F48 File Offset: 0x00072F48
		private void ᜀ(ITypeDescriptorContext A_0, StripStyle A_1)
		{
			int a_ = 5;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0.Instance != null)
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					if (A_0.Instance is CellExport)
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				case 4:
					if (A_0.Instance is WorkSheet)
					{
						num = 5;
						continue;
					}
					return;
				case 5:
					A_1.Name = string.Format(HyperlinksCollectionEditor.b("爠圢圤並夨砪夬嘮崰嘲樴䰶स䘺", a_), (A_0.Instance as WorkSheet).ItemStyles.Count);
					A_1.ExportSource = (A_0.Instance as WorkSheet).DataSource;
					A_1.Command = (A_0.Instance as WorkSheet).SQLCommand;
					A_1.DataTable = (A_0.Instance as WorkSheet).DataTable;
					A_1.ListView = (A_0.Instance as WorkSheet).ListView;
					goto IL_1E0;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E0;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 7:
					goto IL_213;
				case 8:
					goto IL_1EB;
				}
				if (true)
				{
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_1E0:
				num = 8;
			}
			IL_1EB:
			return;
			IL_213:
			A_1.Name = string.Format(HyperlinksCollectionEditor.b("爠圢圤並夨砪夬嘮崰嘲樴䰶स䘺", a_), (A_0.Instance as CellExport).ItemStyles.Count);
			A_1.ExportSource = (A_0.Instance as CellExport).DataSource;
			A_1.Command = (A_0.Instance as CellExport).SQLCommand;
			A_1.DataTable = (A_0.Instance as CellExport).DataTable;
			A_1.ListView = (A_0.Instance as CellExport).ListView;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00074170 File Offset: 0x00073170
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
			StripStyle stripStyle = base.CreateInstance(itemType) as StripStyle;
			this.ᜀ(base.Context, stripStyle);
			return stripStyle;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000741C8 File Offset: 0x000731C8
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
					num = 4;
					continue;
				case 3:
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_BE;
							case 1:
								num = 0;
								continue;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								StripStyle a_ = (StripStyle)enumerator.Current;
								this.ᜀ(base.Context, a_);
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_BE;
								default:
									if (false)
									{
									}
									num = 2;
									continue;
								}
								break;
							}
							}
							IL_9C:
							num = 4;
							continue;
							goto IL_9C;
						}
						IL_BE:
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
									goto IL_103;
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
				case 4:
					if (true)
					{
					}
					if (editValue is ItemStyles)
					{
						num = 1;
						continue;
					}
					goto IL_152;
				}
				if (editValue != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_104:
				enumerator = (editValue as ItemStyles).GetEnumerator();
				num = 3;
			}
			IL_152:
			return base.GetItems(editValue);
		}
	}
}

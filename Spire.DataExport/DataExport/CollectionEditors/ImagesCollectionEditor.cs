using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.CollectionEditors
{
	// Token: 0x0200019A RID: 410
	public class ImagesCollectionEditor : CollectionEditor
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x00074340 File Offset: 0x00073340
		public ImagesCollectionEditor(Type Type) : base(Type)
		{
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00074354 File Offset: 0x00073354
		private void ᜀ(ITypeDescriptorContext A_0, CellImage A_1)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Instance is WorkSheet)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					goto IL_DB;
				case 2:
					A_1.ExportCELLExport = (A_0.Instance as WorkSheet).ExportCell;
					num = 1;
					continue;
				case 3:
					num = 8;
					continue;
				case 4:
					num = 7;
					continue;
				case 5:
					if (A_0.Instance != null)
					{
						num = 3;
						continue;
					}
					return;
				case 7:
					if ((A_0.Instance as WorkSheet).ExportCell != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					return;
				case 8:
					if (A_0.Instance is CellExport)
					{
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 9:
					goto IL_100;
				case 10:
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					break;
				}
				num = 10;
			}
			IL_DB:
			return;
			IL_100:
			A_1.ExportCELLExport = (A_0.Instance as CellExport);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000744A8 File Offset: 0x000734A8
		protected override object CreateInstance(Type itemType)
		{
			int a_ = 5;
			CellImage cellImage;
			for (;;)
			{
				cellImage = (base.CreateInstance(itemType) as CellImage);
				this.ᜀ(base.Context, cellImage);
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_F9;
					case 1:
						num = 13;
						continue;
					case 2:
						return cellImage;
					case 3:
						goto IL_F9;
					case 4:
					{
						int num3;
						if (!(base.Context.Instance as CellExport).Images.Find(string.Format(HyperlinksCollectionEditor.b("栠丢䐤䀦䰨琪嘬Ἦ䰰", a_), num2), ref num3))
						{
							num = 14;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F9;
						default:
							if (false)
							{
							}
							num2++;
							num = 17;
							continue;
						}
						break;
					}
					case 5:
						goto IL_1C2;
					case 6:
						if (base.Context.Instance is CellExport)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						num = 8;
						continue;
					case 7:
						goto IL_187;
					case 8:
						if (base.Context.Instance is WorkSheet)
						{
							num = 11;
							continue;
						}
						goto IL_187;
					case 9:
						num = 5;
						continue;
					case 10:
						goto IL_187;
					case 11:
						num = 0;
						continue;
					case 12:
						if (base.Context != null)
						{
							num = 1;
							continue;
						}
						return cellImage;
					case 13:
						if (base.Context.Instance != null)
						{
							num = 15;
							continue;
						}
						return cellImage;
					case 14:
						num = 7;
						continue;
					case 15:
					{
						num2 = 0;
						int num3 = 0;
						num = 6;
						continue;
					}
					case 16:
					{
						int num3;
						if (!(base.Context.Instance as WorkSheet).Images.Find(string.Format(HyperlinksCollectionEditor.b("栠丢䐤䀦䰨琪嘬Ἦ䰰", a_), num2), ref num3))
						{
							num = 10;
							continue;
						}
						num2++;
						num = 3;
						continue;
					}
					case 17:
						goto IL_1C2;
					}
					break;
					IL_F9:
					num = 16;
					continue;
					IL_187:
					cellImage.Title = string.Format(HyperlinksCollectionEditor.b("栠丢䐤䀦䰨琪嘬Ἦ䰰", a_), num2);
					cellImage.Name = cellImage.Title;
					num = 2;
					continue;
					IL_1C2:
					num = 4;
				}
			}
			return cellImage;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00074738 File Offset: 0x00073738
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
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_BE;
							case 1:
							{
								if (!enumerator.MoveNext())
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										num = 2;
										continue;
									}
								}
								CellImage a_ = (CellImage)enumerator.Current;
								this.ᜀ(base.Context, a_);
								num = 3;
								continue;
							}
							case 2:
								num = 0;
								continue;
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
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_101;
								case 1:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_103;
								case 2:
									disposable.Dispose();
									num = 0;
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
					if (true)
					{
					}
					if (editValue is CellImages)
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
				enumerator = (editValue as CellImages).GetEnumerator();
				num = 3;
			}
			IL_152:
			return base.GetItems(editValue);
		}
	}
}

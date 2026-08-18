using System;
using System.Collections;
using System.ComponentModel;
using Spire.DataExport.XLS;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000219 RID: 537
	public class PictureNameEditor : ListComponentEditor
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x000ACFE0 File Offset: 0x000ABFE0
		public override void AdditionalSettings()
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
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000AD01C File Offset: 0x000AC01C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_20C;
				case 3:
					if (context.Instance != null)
					{
						num = 0;
						continue;
					}
					goto IL_246;
				case 4:
				{
					IEnumerator enumerator = (context.Instance as CellImage).ExportCELLExport.Pictures.GetEnumerator();
					num = 10;
					continue;
				}
				case 5:
					if (context.Instance is CellImage)
					{
						num = 2;
						continue;
					}
					goto IL_246;
				case 6:
					if ((context.Instance as CellImage).ExportCELLExport.Pictures != null)
					{
						num = 4;
						continue;
					}
					goto IL_246;
				case 8:
					num = 6;
					continue;
				case 9:
					if ((context.Instance as CellImage).ExportCELLExport != null)
					{
						num = 8;
						continue;
					}
					goto IL_246;
				case 10:
					if (true)
					{
					}
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 6;
								continue;
							case 2:
							{
								CellPicture cellPicture;
								if (this.m_listBox.Items.IndexOf(cellPicture.Name) == -1)
								{
									num = 5;
									continue;
								}
								break;
							}
							case 4:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								CellPicture cellPicture = (CellPicture)enumerator.Current;
								num = 2;
								continue;
							}
							case 5:
							{
								CellPicture cellPicture;
								this.m_listBox.Items.Add(cellPicture.Name);
								num = 3;
								continue;
							}
							case 6:
								goto IL_1AA;
							}
							IL_17F:
							num = 4;
							continue;
							goto IL_17F;
						}
						IL_1AA:
						goto IL_246;
					}
					finally
					{
						for (;;)
						{
							for (;;)
							{
								IEnumerator enumerator;
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
										if (disposable == null)
										{
											goto IL_20B;
										}
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											if (false)
											{
											}
											num = 0;
											continue;
										}
										break;
									case 2:
										goto IL_209;
									}
									break;
								}
							}
						}
						IL_209:
						IL_20B:;
					}
					goto IL_20C;
				}
				if (context != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_20C:
				num = 9;
			}
			IL_246:
			return base.EditValue(context, provider, value);
		}
	}
}

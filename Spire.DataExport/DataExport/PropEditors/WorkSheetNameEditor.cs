using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200020D RID: 525
	public class WorkSheetNameEditor : ListComponentEditor
	{
		// Token: 0x06000FE1 RID: 4065 RVA: 0x000AAF48 File Offset: 0x000A9F48
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

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000AAF84 File Offset: 0x000A9F84
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					CellExport cellExport = null;
					this.m_listBox.Items.Clear();
					int num = 20;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (context.Instance is CustomItem)
							{
								num = 5;
								continue;
							}
							return value;
						case 1:
							if (true)
							{
							}
							if (this.m_edSvc != null)
							{
								num = 7;
								continue;
							}
							return value;
						case 2:
							if (cellExport == null)
							{
								num = 4;
								continue;
							}
							num = 16;
							continue;
						case 3:
						{
							IEnumerator enumerator = cellExport.Sheets.GetEnumerator();
							num = 12;
							continue;
						}
						case 4:
							return value;
						case 5:
							cellExport = (context.Instance as CustomItem).CellExport;
							num = 2;
							continue;
						case 6:
							num = 13;
							continue;
						case 7:
							this.m_edSvc.DropDownControl(this.m_listBox);
							num = 17;
							continue;
						case 8:
							num = 0;
							continue;
						case 9:
							if (context.Instance != null)
							{
								num = 8;
								continue;
							}
							return value;
						case 10:
							if (cellExport.Sheets.Count > 0)
							{
								num = 3;
								continue;
							}
							goto IL_164;
						case 11:
							goto IL_1AA;
						case 12:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										WorkSheet workSheet = (WorkSheet)enumerator.Current;
										this.m_listBox.Items.Add(workSheet.SheetName);
										num = 3;
										continue;
									}
									case 1:
										goto IL_2C8;
									case 2:
										num = 1;
										continue;
									}
									IL_2A3:
									num = 0;
									continue;
									goto IL_2A3;
								}
								IL_2C8:
								goto IL_F1;
							}
							finally
							{
								for (;;)
								{
									IL_2E2:
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_31C:
										disposable.Dispose();
										num = 1;
										break;
									default:
										if (false)
										{
										}
										num = 2;
										break;
									}
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_31A;
										case 1:
											goto IL_32B;
										case 2:
											if (disposable != null)
											{
												num = 0;
												continue;
											}
											goto IL_32D;
										}
										goto IL_2E2;
									}
									IL_31A:
									goto IL_31C;
								}
								IL_32B:
								IL_32D:;
							}
							return value;
						case 13:
							if (provider != null)
							{
								num = 14;
								continue;
							}
							return value;
						case 14:
							num = 9;
							continue;
						case 15:
							value = this.m_listBox.SelectedItem.ToString();
							num = 11;
							continue;
						case 16:
							if (cellExport.Sheets != null)
							{
								num = 18;
								continue;
							}
							goto IL_164;
						case 17:
							if (this.m_listBox.SelectedIndex >= 0)
							{
								num = 15;
								continue;
							}
							return value;
						case 18:
							num = 10;
							continue;
						case 19:
							goto IL_F1;
						case 20:
							if (context != null)
							{
								num = 6;
								continue;
							}
							return value;
						}
						break;
						IL_F1:
						this.m_edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
						num = 1;
						continue;
						IL_164:
						this.m_listBox.Items.Add(cellExport.SheetName);
						num = 19;
					}
				}
				IL_1AA:
				return value;
			}
		}
	}
}

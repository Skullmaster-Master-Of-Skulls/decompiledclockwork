using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Common;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.XLS;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000212 RID: 530
	public class ColumnNameEditor : ListComponentEditor
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x000ABC48 File Offset: 0x000AAC48
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

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000ABC84 File Offset: 0x000AAC84
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				CustomItem customItem2;
				for (;;)
				{
					this.m_listBox.Items.Clear();
					int num = 45;
					for (;;)
					{
						CustomItem customItem;
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							goto IL_B36;
						case 1:
							if (customItem.CellExport.Sheets.Count > 0)
							{
								num = 27;
								continue;
							}
							goto IL_603;
						case 2:
							num = 15;
							continue;
						case 3:
							if (customItem.CellExport.Sheets != null)
							{
								num = 14;
								continue;
							}
							goto IL_364;
						case 4:
							customItem2 = customItem;
							num = 53;
							continue;
						case 5:
						{
							int num2;
							customItem2.ExportSource = customItem.CellExport.Sheets[num2].DataSource;
							customItem2.DataTable = customItem.CellExport.Sheets[num2].DataTable;
							customItem2.Command = customItem.CellExport.Sheets[num2].SQLCommand;
							customItem2.ListView = customItem.CellExport.Sheets[num2].ListView;
							num = 17;
							continue;
						}
						case 6:
							goto IL_947;
						case 7:
							num = 8;
							continue;
						case 8:
							if (context.Instance is CustomItem)
							{
								num = 40;
								continue;
							}
							return value;
						case 9:
							goto IL_1F8;
						case 10:
						{
							ExportSource exportSource;
							switch (exportSource)
							{
							case ExportSource.SqlCommand:
								goto IL_924;
							case ExportSource.DataTable:
								num = 46;
								continue;
							case ExportSource.ListView:
								num = 30;
								continue;
							default:
								num = 34;
								continue;
							}
							break;
						}
						case 11:
							goto IL_225;
						case 12:
							goto IL_5E0;
						case 13:
							if (customItem2.Command.Connection.State != ConnectionState.Open)
							{
								num = 41;
								continue;
							}
							goto IL_225;
						case 14:
							num = 26;
							continue;
						case 15:
							if (customItem.CellExport.Sheets != null)
							{
								num = 42;
								continue;
							}
							goto IL_603;
						case 16:
							if (provider != null)
							{
								num = 36;
								continue;
							}
							return value;
						case 17:
							goto IL_715;
						case 18:
						{
							if (customItem2 == null)
							{
								num = 29;
								continue;
							}
							ExportSource exportSource = customItem2.ExportSource;
							num = 10;
							continue;
						}
						case 19:
							goto IL_715;
						case 20:
							value = this.m_listBox.SelectedItem.ToString();
							num = 37;
							continue;
						case 21:
						{
							customItem2 = new ChartSeries();
							int num2 = (customItem as ChartSeries).CellExport.ᜁ((customItem as ChartSeries).DataRangeSheet);
							num = 43;
							continue;
						}
						case 22:
							if (customItem is ChartSeries)
							{
								num = 21;
								continue;
							}
							goto IL_5E0;
						case 23:
							this.m_edSvc.DropDownControl(this.m_listBox);
							num = 35;
							continue;
						case 24:
							if (customItem2.Command == null)
							{
								num = 6;
								continue;
							}
							num = 31;
							continue;
						case 25:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_8D6;
									case 1:
									{
										if (!enumerator.MoveNext())
										{
											num = 3;
											continue;
										}
										DataColumn dataColumn = (DataColumn)enumerator.Current;
										this.m_listBox.Items.Add(dataColumn.ColumnName);
										num = 4;
										continue;
									}
									case 3:
										num = 0;
										continue;
									}
									IL_8B0:
									num = 1;
									continue;
									goto IL_8B0;
								}
								IL_8D6:
								goto IL_94C;
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
											goto IL_923;
										case 1:
											goto IL_921;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_921:
								IL_923:;
							}
							goto IL_924;
						case 26:
							if (customItem.CellExport.Sheets.Count > 0)
							{
								num = 5;
								continue;
							}
							goto IL_364;
						case 27:
						{
							int num2;
							customItem2.ExportSource = customItem.CellExport.Sheets[num2].DataSource;
							customItem2.DataTable = customItem.CellExport.Sheets[num2].DataTable;
							customItem2.Command = customItem.CellExport.Sheets[num2].SQLCommand;
							customItem2.ListView = customItem.CellExport.Sheets[num2].ListView;
							num = 12;
							continue;
						}
						case 28:
						{
							int num2;
							if (num2 >= 0)
							{
								num = 38;
								continue;
							}
							goto IL_715;
						}
						case 29:
							return value;
						case 30:
						{
							if (customItem2.ListView == null)
							{
								num = 9;
								continue;
							}
							IEnumerator enumerator2 = customItem2.ListView.Columns.GetEnumerator();
							num = 32;
							continue;
						}
						case 31:
							if (customItem2.Command.CommandText.Length == 0)
							{
								num = 33;
								continue;
							}
							goto IL_337;
						case 32:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 3;
											continue;
										}
										ColumnHeader columnHeader = (ColumnHeader)enumerator2.Current;
										this.m_listBox.Items.Add(columnHeader.Text);
										num = 1;
										continue;
									}
									case 2:
										goto IL_56F;
									case 3:
										num = 2;
										continue;
									}
									IL_549:
									num = 0;
									continue;
									goto IL_549;
								}
								IL_56F:
								goto IL_94C;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable2 = enumerator2 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable2.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_5BC;
										case 2:
											goto IL_5BA;
										}
										break;
									}
								}
								IL_5BA:
								IL_5BC:;
							}
							goto IL_5BD;
						case 33:
							goto IL_7EB;
						case 34:
							num = 39;
							continue;
						case 35:
							if (this.m_listBox.SelectedIndex >= 0)
							{
								num = 20;
								continue;
							}
							return value;
						case 36:
							num = 47;
							continue;
						case 37:
							goto IL_3FA;
						case 38:
							num = 3;
							continue;
						case 39:
							goto IL_94C;
						case 40:
						{
							int num2 = 0;
							customItem2 = null;
							customItem = (context.Instance as CustomItem);
							num = 55;
							continue;
						}
						case 41:
							customItem2.Command.Connection.Open();
							num = 11;
							continue;
						case 42:
							num = 1;
							continue;
						case 43:
						{
							int num2;
							if (num2 >= 0)
							{
								num = 2;
								continue;
							}
							goto IL_5E0;
						}
						case 44:
							goto IL_5E0;
						case 45:
							if (context != null)
							{
								num = 51;
								continue;
							}
							return value;
						case 46:
							if (customItem2.DataTable == null)
							{
								num = 0;
								continue;
							}
							goto IL_5BD;
						case 47:
							if (context.Instance != null)
							{
								num = 7;
								continue;
							}
							return value;
						case 48:
							if (this.m_edSvc != null)
							{
								num = 23;
								continue;
							}
							return value;
						case 49:
							if (customItem is Chart)
							{
								num = 52;
								continue;
							}
							goto IL_715;
						case 50:
							if (customItem2.Command.Connection == null)
							{
								num = 54;
								continue;
							}
							num = 13;
							continue;
						case 51:
							num = 16;
							continue;
						case 52:
						{
							customItem2 = new Chart();
							int num2 = customItem.CellExport.ᜁ((customItem as Chart).DataRangeSheet);
							if (true)
							{
							}
							num = 28;
							continue;
						}
						case 53:
							goto IL_1FD;
						case 54:
							goto IL_35F;
						case 55:
							if (customItem is CellFormat)
							{
								num = 4;
								continue;
							}
							goto IL_1FD;
						}
						break;
						IL_1FD:
						num = 49;
						continue;
						IL_337:
						num = 50;
						continue;
						try
						{
							IL_225:
							lock (this)
							{
								IDataReader dataReader = customItem2.Command.ExecuteReader(CommandBehavior.SchemaOnly);
								try
								{
									for (;;)
									{
										int num3 = 0;
										num = 3;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_294;
											case 1:
												num = 4;
												continue;
											case 2:
												if (num3 >= dataReader.FieldCount)
												{
													num = 1;
													continue;
												}
												this.m_listBox.Items.Add(dataReader.GetName(num3));
												num3++;
												num = 0;
												continue;
											case 3:
												goto IL_294;
											case 4:
												goto IL_2BB;
											}
											break;
											IL_294:
											num = 2;
										}
									}
									IL_2BB:;
								}
								finally
								{
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												break;
											default:
												if (false)
												{
												}
												break;
											}
											dataReader.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_314;
										}
										if (dataReader == null)
										{
											break;
										}
										num = 0;
									}
									IL_314:;
								}
							}
							goto IL_94C;
						}
						finally
						{
							customItem2.Command.Connection.Close();
						}
						goto IL_337;
						IL_364:
						customItem2.ExportSource = customItem.CellExport.DataSource;
						customItem2.DataTable = customItem.CellExport.DataTable;
						customItem2.Command = customItem.CellExport.SQLCommand;
						customItem2.ListView = customItem.CellExport.ListView;
						num = 19;
						continue;
						IL_5BD:
						enumerator = customItem2.DataTable.Columns.GetEnumerator();
						num = 25;
						continue;
						IL_5E0:
						num = 18;
						continue;
						IL_603:
						customItem2.ExportSource = customItem.CellExport.DataSource;
						customItem2.DataTable = customItem.CellExport.DataTable;
						customItem2.Command = customItem.CellExport.SQLCommand;
						customItem2.ListView = customItem.CellExport.ListView;
						num = 44;
						continue;
						IL_715:
						num = 22;
						continue;
						IL_924:
						num = 24;
						continue;
						IL_94C:
						this.m_edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
						num = 48;
					}
				}
				IL_1F8:
				throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搤唦丨堪爬欮倰䜲吴搶嘸为似尾⑀ق⡄㝆㵈㉊", a_)), spr\u2059.ᜀ(customItem2.ExportSource)));
				IL_35F:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搤唦丨堪爬氮帰帲場嘶圸强縼倾⽀ⵂ⁄⑆㵈≊≌ⅎ", a_)));
				IL_3FA:
				return value;
				IL_7EB:
				throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搤唦丨堪爬氮帰帲場嘶圸强椼娾㥀㝂D⩆㥈㽊㑌", a_)));
				IL_947:
				throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搤唦丨堪爬欮倰䜲吴搶嘸为似尾⑀ق⡄㝆㵈㉊", a_)), spr\u2059.ᜀ(customItem2.ExportSource)));
				IL_B36:
				throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搤唦丨堪爬欮倰䜲吴搶嘸为似尾⑀ق⡄㝆㵈㉊", a_)), spr\u2059.ᜀ(customItem2.ExportSource)));
			}
			}
		}
	}
}

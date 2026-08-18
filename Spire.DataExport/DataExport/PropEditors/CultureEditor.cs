using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000221 RID: 545
	public class CultureEditor : ListComponentEditor
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x000ADF94 File Offset: 0x000ACF94
		public override void AdditionalSettings()
		{
			int a_ = 12;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					this.m_listBox.Sorted = true;
					this.m_listBox.Items.Clear();
					CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
					int num = 0;
					if (true)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							CultureInfo cultureInfo;
							this.m_listBox.Items.Add(string.Format(HyperlinksCollectionEditor.b("匧ᨩ儫ᔭု䤱Գ䬵", a_), cultureInfo.Name, cultureInfo.DisplayName));
							num2 = 4;
							continue;
						}
						case 1:
							goto IL_109;
						case 2:
						{
							if (num >= cultures.Length)
							{
								num2 = 5;
								continue;
							}
							CultureInfo cultureInfo = cultures[num];
							num2 = 6;
							continue;
						}
						case 3:
							goto IL_109;
						case 4:
							goto IL_83;
						case 5:
							return;
						case 6:
						{
							CultureInfo cultureInfo;
							if (cultureInfo.Name != string.Empty)
							{
								num2 = 0;
								continue;
							}
							goto IL_83;
						}
						}
						break;
						IL_83:
						num++;
						num2 = 3;
						continue;
						IL_109:
						num2 = 2;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000AE0C8 File Offset: 0x000AD0C8
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A7;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					if (provider != null)
					{
						num = 10;
						continue;
					}
					return value;
				case 2:
				{
					string text = this.m_listBox.SelectedItem.ToString();
					num = 5;
					continue;
				}
				case 3:
				{
					string[] array;
					if (array.Length >= 2)
					{
						num = 12;
						continue;
					}
					return value;
				}
				case 4:
				{
					string text;
					string[] array = text.Split(new char[]
					{
						';'
					});
					num = 3;
					continue;
				}
				case 5:
				{
					string text;
					if (text.IndexOf(';') > -1)
					{
						num = 4;
						continue;
					}
					return value;
				}
				case 6:
					return value;
				case 7:
					if (this.m_listBox.SelectedIndex >= 0)
					{
						num = 2;
						continue;
					}
					return value;
				case 8:
					this.m_edSvc.DropDownControl(this.m_listBox);
					goto IL_A7;
				case 9:
					if (this.m_edSvc != null)
					{
						num = 8;
						continue;
					}
					return value;
				case 10:
					this.m_edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
					num = 9;
					continue;
				case 12:
				{
					string[] array;
					value = array[0].Trim();
					num = 6;
					continue;
				}
				case 13:
					num = 14;
					continue;
				case 14:
					if (context.Instance != null)
					{
						num = 0;
						continue;
					}
					return value;
				}
				if (context != null)
				{
					num = 13;
					continue;
				}
				break;
				IL_A7:
				if (true)
				{
				}
				num = 7;
			}
			return value;
		}
	}
}

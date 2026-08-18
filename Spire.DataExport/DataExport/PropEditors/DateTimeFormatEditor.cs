using System;
using System.Globalization;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200021B RID: 539
	public class DateTimeFormatEditor : ListComponentEditor
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x000AD5A0 File Offset: 0x000AC5A0
		public override void AdditionalSettings()
		{
			for (;;)
			{
				this.m_listBox.Items.Clear();
				string[] allDateTimePatterns = DateTimeFormatInfo.CurrentInfo.GetAllDateTimePatterns();
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num < allDateTimePatterns.Length)
						{
							string item = allDateTimePatterns[num];
							this.m_listBox.Items.Add(item);
							num++;
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_86;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						goto IL_51;
					case 2:
						goto IL_51;
					case 3:
						goto IL_86;
					}
					break;
					IL_51:
					num2 = 0;
				}
			}
			IL_86:
			this.m_listBox.Items.Add('d');
			this.m_listBox.Items.Add('D');
			this.m_listBox.Items.Add('f');
			this.m_listBox.Items.Add('F');
			this.m_listBox.Items.Add('g');
			this.m_listBox.Items.Add('G');
			this.m_listBox.Items.Add('M');
			this.m_listBox.Items.Add('R');
			this.m_listBox.Items.Add('s');
			this.m_listBox.Items.Add('u');
			this.m_listBox.Items.Add('U');
			this.m_listBox.Items.Add('Y');
		}
	}
}

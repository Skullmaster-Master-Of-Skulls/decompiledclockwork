using System;
using System.Globalization;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000222 RID: 546
	public class TimeFormatEditor : ListComponentEditor
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x000AE2B8 File Offset: 0x000AD2B8
		public override void AdditionalSettings()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.m_listBox.Items.Clear();
					string[] allDateTimePatterns = DateTimeFormatInfo.CurrentInfo.GetAllDateTimePatterns('t');
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
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
								string[] allDateTimePatterns2 = DateTimeFormatInfo.CurrentInfo.GetAllDateTimePatterns('T');
								int num3 = 0;
								break;
							}
							}
							num2 = 5;
							continue;
						case 1:
							goto IL_FB;
						case 2:
							goto IL_120;
						case 3:
							goto IL_11E;
						case 4:
						{
							string[] allDateTimePatterns2;
							int num3;
							if (num3 >= allDateTimePatterns2.Length)
							{
								num2 = 3;
								continue;
							}
							string item = allDateTimePatterns2[num3];
							this.m_listBox.Items.Add(item);
							num3++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						case 5:
							goto IL_FB;
						case 6:
							goto IL_120;
						case 7:
						{
							if (num >= allDateTimePatterns.Length)
							{
								num2 = 0;
								continue;
							}
							string item2 = allDateTimePatterns[num];
							this.m_listBox.Items.Add(item2);
							num++;
							num2 = 6;
							continue;
						}
						}
						break;
						IL_FB:
						num2 = 4;
						continue;
						IL_120:
						num2 = 7;
					}
				}
				IL_11E:
				this.m_listBox.Items.Add('t');
				this.m_listBox.Items.Add('T');
				return;
			}
		}
	}
}

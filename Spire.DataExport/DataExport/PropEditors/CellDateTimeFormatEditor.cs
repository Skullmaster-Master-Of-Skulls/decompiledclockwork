using System;
using System.Globalization;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200021F RID: 543
	public class CellDateTimeFormatEditor : ListComponentEditor
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x000ADE34 File Offset: 0x000ACE34
		public override void AdditionalSettings()
		{
			for (;;)
			{
				this.m_listBox.Items.Clear();
				string[] allDateTimePatterns = DateTimeFormatInfo.CurrentInfo.GetAllDateTimePatterns();
				int i = 0;
				int num = 1;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						while (i < allDateTimePatterns.Length)
						{
							string item = allDateTimePatterns[i];
							this.m_listBox.Items.Add(item);
							i++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 3;
								goto IL_02;
							}
						}
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						goto IL_51;
					case 2:
						return;
					case 3:
						goto IL_51;
					}
					break;
					IL_51:
					num = 0;
				}
			}
		}
	}
}

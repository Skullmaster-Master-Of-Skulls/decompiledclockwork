using System;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x0200000E RID: 14
	public struct ControlCodes
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006B80 File Offset: 0x00004D80
		public static string GetDescription(int controlCode)
		{
			if (controlCode <= 50)
			{
				switch (controlCode)
				{
				case 1:
					return "Textbox";
				case 2:
					return "Checkbox";
				case 3:
					return "Droplist";
				case 4:
					return "Radio button";
				case 5:
					return "Label";
				case 6:
					return "Date";
				case 7:
					return "Time";
				case 8:
					return "Horizontal Rule";
				case 9:
					return "Blank space";
				case 10:
					return "Table";
				case 11:
				case 12:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 22:
				case 23:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
					break;
				case 13:
					return "Indent";
				case 20:
					return "File list";
				case 21:
					return "Picture";
				case 30:
					return "Group box";
				case 31:
					return "Group box end";
				case 32:
					return "Tab control";
				case 33:
					return "Tab page";
				case 34:
					return "Tab page close";
				case 35:
					return "Tab control close";
				default:
					if (controlCode == 50)
					{
						return "Column break";
					}
					break;
				}
			}
			else
			{
				if (controlCode == 100)
				{
					return "Staff drop list";
				}
				if (controlCode == 200)
				{
					return "School year chooser";
				}
				if (controlCode == 300)
				{
					return "Masked text box";
				}
			}
			return "Unknown";
		}

		// Token: 0x04000038 RID: 56
		public const int _textBox = 1;

		// Token: 0x04000039 RID: 57
		public const int _checkBox = 2;

		// Token: 0x0400003A RID: 58
		public const int _comboBox = 3;

		// Token: 0x0400003B RID: 59
		public const int _radioButton = 4;

		// Token: 0x0400003C RID: 60
		public const int _label = 5;

		// Token: 0x0400003D RID: 61
		public const int _date = 6;

		// Token: 0x0400003E RID: 62
		public const int _time = 7;

		// Token: 0x0400003F RID: 63
		public const int _horizontalRule = 8;

		// Token: 0x04000040 RID: 64
		public const int _blankSpace = 9;

		// Token: 0x04000041 RID: 65
		public const int _listView = 10;

		// Token: 0x04000042 RID: 66
		public const int _myCheckBox = 12;

		// Token: 0x04000043 RID: 67
		public const int _myTextBox = 11;

		// Token: 0x04000044 RID: 68
		public const int _indent = 13;

		// Token: 0x04000045 RID: 69
		public const int _radioGroup = 14;

		// Token: 0x04000046 RID: 70
		public const int _fileList = 20;

		// Token: 0x04000047 RID: 71
		public const int _picture = 21;

		// Token: 0x04000048 RID: 72
		public const int _panelStart = 30;

		// Token: 0x04000049 RID: 73
		public const int _panelClose = 31;

		// Token: 0x0400004A RID: 74
		public const int _tabControlStart = 32;

		// Token: 0x0400004B RID: 75
		public const int _tabPageStart = 33;

		// Token: 0x0400004C RID: 76
		public const int _tabPageClose = 34;

		// Token: 0x0400004D RID: 77
		public const int _tabControlClose = 35;

		// Token: 0x0400004E RID: 78
		public const int _columnBreak = 50;

		// Token: 0x0400004F RID: 79
		public const int _staffComboBox = 100;

		// Token: 0x04000050 RID: 80
		public const int _schoolYearChooser = 200;

		// Token: 0x04000051 RID: 81
		public const int _maskedTextBox = 300;

		// Token: 0x04000052 RID: 82
		public const int _file = 400;

		// Token: 0x04000053 RID: 83
		public const int _multiCheckBox = 500;

		// Token: 0x04000054 RID: 84
		public const int _multiCheckBoxText = 510;

		// Token: 0x04000055 RID: 85
		public const int _multiCheckBoxDropList = 520;

		// Token: 0x04000056 RID: 86
		public const int _multiLabelHeader = 530;

		// Token: 0x04000057 RID: 87
		public const int _rtfTextBox = 600;

		// Token: 0x04000058 RID: 88
		public const int _multiLineTextBox = 620;

		// Token: 0x04000059 RID: 89
		public const int VerticalPadTextBox = 6;

		// Token: 0x0400005A RID: 90
		public const int VerticalPadComboBox = 6;

		// Token: 0x0400005B RID: 91
		public const int VerticalPadRadioButton = 2;

		// Token: 0x0400005C RID: 92
		public const int VerticalPadCheckBox = 2;

		// Token: 0x0400005D RID: 93
		public const int VerticalPadLabel = 2;

		// Token: 0x0400005E RID: 94
		public const int VerticalPadDateTimePicker = 4;

		// Token: 0x0400005F RID: 95
		public const int _unknown = 999999;
	}
}

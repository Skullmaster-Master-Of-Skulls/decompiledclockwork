using System;

namespace DynamicScreens
{
	// Token: 0x02000039 RID: 57
	public struct ControlCodes
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0001FEF0 File Offset: 0x0001EEF0
		public static string GetDescription(int controlCode)
		{
			return DynamicScreen.GetControlNameByControlCode(controlCode);
		}

		// Token: 0x04000231 RID: 561
		public const int _textBox = 1;

		// Token: 0x04000232 RID: 562
		public const int _checkBox = 2;

		// Token: 0x04000233 RID: 563
		public const int _comboBox = 3;

		// Token: 0x04000234 RID: 564
		public const int _radioButton = 4;

		// Token: 0x04000235 RID: 565
		public const int _label = 5;

		// Token: 0x04000236 RID: 566
		public const int _date = 6;

		// Token: 0x04000237 RID: 567
		public const int _time = 7;

		// Token: 0x04000238 RID: 568
		public const int _horizontalRule = 8;

		// Token: 0x04000239 RID: 569
		public const int _blankSpace = 9;

		// Token: 0x0400023A RID: 570
		public const int _listView = 10;

		// Token: 0x0400023B RID: 571
		public const int _myCheckBox = 12;

		// Token: 0x0400023C RID: 572
		public const int _myTextBox = 11;

		// Token: 0x0400023D RID: 573
		public const int _indent = 13;

		// Token: 0x0400023E RID: 574
		public const int _radioGroup = 14;

		// Token: 0x0400023F RID: 575
		public const int _fileList = 20;

		// Token: 0x04000240 RID: 576
		public const int _picture = 21;

		// Token: 0x04000241 RID: 577
		public const int _dynamicTable = 25;

		// Token: 0x04000242 RID: 578
		public const int _panelStart = 30;

		// Token: 0x04000243 RID: 579
		public const int _panelClose = 31;

		// Token: 0x04000244 RID: 580
		public const int _tabControlStart = 32;

		// Token: 0x04000245 RID: 581
		public const int _tabPageStart = 33;

		// Token: 0x04000246 RID: 582
		public const int _tabPageClose = 34;

		// Token: 0x04000247 RID: 583
		public const int _tabControlClose = 35;

		// Token: 0x04000248 RID: 584
		public const int _tableControl = 40;

		// Token: 0x04000249 RID: 585
		public const int _columnBreak = 50;

		// Token: 0x0400024A RID: 586
		public const int _staffComboBox = 100;

		// Token: 0x0400024B RID: 587
		public const int _schoolYearChooser = 200;

		// Token: 0x0400024C RID: 588
		public const int _maskedTextBox = 300;

		// Token: 0x0400024D RID: 589
		public const int _listSelect = 301;

		// Token: 0x0400024E RID: 590
		public const int _file = 400;

		// Token: 0x0400024F RID: 591
		public const int _multiCheckBox = 500;

		// Token: 0x04000250 RID: 592
		public const int _multiCheckBoxText = 510;

		// Token: 0x04000251 RID: 593
		public const int _multiCheckBoxDropList = 520;

		// Token: 0x04000252 RID: 594
		public const int _multiLabelHeader = 530;

		// Token: 0x04000253 RID: 595
		public const int _rtfTextBox = 600;

		// Token: 0x04000254 RID: 596
		public const int _multiLineTextBox = 620;

		// Token: 0x04000255 RID: 597
		public const int _accommodationCheckbox = 700;

		// Token: 0x04000256 RID: 598
		public const int _accommodationTextbox = 701;

		// Token: 0x04000257 RID: 599
		public const int _accommodationDatePicker = 702;

		// Token: 0x04000258 RID: 600
		public const int _accommodationDropList = 703;

		// Token: 0x04000259 RID: 601
		public const int _FormSettings = 800;

		// Token: 0x0400025A RID: 602
		public const int _dynamicControlsChooser = 801;

		// Token: 0x0400025B RID: 603
		public const int _multiDatabaseItemChooser = 802;

		// Token: 0x0400025C RID: 604
		public const int _infoDisplayBox = 803;

		// Token: 0x0400025D RID: 605
		public const int _calcButton = 804;

		// Token: 0x0400025E RID: 606
		public const int _PMTable = 805;

		// Token: 0x0400025F RID: 607
		public const int _caseComboBox = 806;

		// Token: 0x04000260 RID: 608
		public const int _emailHistory = 807;

		// Token: 0x04000261 RID: 609
		public const int _appointmentHistory = 808;

		// Token: 0x04000262 RID: 610
		public const int VerticalPadTextBox = 6;

		// Token: 0x04000263 RID: 611
		public const int VerticalPadComboBox = 6;

		// Token: 0x04000264 RID: 612
		public const int VerticalPadRadioButton = 2;

		// Token: 0x04000265 RID: 613
		public const int VerticalPadCheckBox = 2;

		// Token: 0x04000266 RID: 614
		public const int VerticalPadLabel = 2;

		// Token: 0x04000267 RID: 615
		public const int VerticalPadDateTimePicker = 4;

		// Token: 0x04000268 RID: 616
		public const int _unknown = 999999;
	}
}

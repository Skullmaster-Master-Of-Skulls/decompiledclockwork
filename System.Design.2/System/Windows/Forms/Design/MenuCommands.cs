using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000318 RID: 792
	public sealed class MenuCommands : StandardCommands
	{
		// Token: 0x04001808 RID: 6152
		private static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

		// Token: 0x04001809 RID: 6153
		private static readonly Guid wfMenuGroup = new Guid("{74D21312-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x0400180A RID: 6154
		private static readonly Guid wfCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x0400180B RID: 6155
		private static readonly Guid guidVSStd2K = new Guid("{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}");

		// Token: 0x0400180C RID: 6156
		private const int mnuidSelection = 1280;

		// Token: 0x0400180D RID: 6157
		private const int mnuidContainer = 1281;

		// Token: 0x0400180E RID: 6158
		private const int mnuidTraySelection = 1283;

		// Token: 0x0400180F RID: 6159
		private const int mnuidComponentTray = 1286;

		// Token: 0x04001810 RID: 6160
		private const int cmdidDesignerProperties = 4097;

		// Token: 0x04001811 RID: 6161
		private const int cmdidReverseCancel = 16385;

		// Token: 0x04001812 RID: 6162
		private const int cmdidSetStatusText = 16387;

		// Token: 0x04001813 RID: 6163
		private const int cmdidSetStatusRectangle = 16388;

		// Token: 0x04001814 RID: 6164
		private const int cmdidSpace = 16405;

		// Token: 0x04001815 RID: 6165
		private const int ECMD_CANCEL = 103;

		// Token: 0x04001816 RID: 6166
		private const int ECMD_RETURN = 3;

		// Token: 0x04001817 RID: 6167
		private const int ECMD_UP = 11;

		// Token: 0x04001818 RID: 6168
		private const int ECMD_DOWN = 13;

		// Token: 0x04001819 RID: 6169
		private const int ECMD_LEFT = 7;

		// Token: 0x0400181A RID: 6170
		private const int ECMD_RIGHT = 9;

		// Token: 0x0400181B RID: 6171
		private const int ECMD_RIGHT_EXT = 10;

		// Token: 0x0400181C RID: 6172
		private const int ECMD_UP_EXT = 12;

		// Token: 0x0400181D RID: 6173
		private const int ECMD_LEFT_EXT = 8;

		// Token: 0x0400181E RID: 6174
		private const int ECMD_DOWN_EXT = 14;

		// Token: 0x0400181F RID: 6175
		private const int ECMD_TAB = 4;

		// Token: 0x04001820 RID: 6176
		private const int ECMD_BACKTAB = 5;

		// Token: 0x04001821 RID: 6177
		private const int ECMD_INVOKESMARTTAG = 147;

		// Token: 0x04001822 RID: 6178
		private const int ECMD_CTLMOVELEFT = 1224;

		// Token: 0x04001823 RID: 6179
		private const int ECMD_CTLMOVEDOWN = 1225;

		// Token: 0x04001824 RID: 6180
		private const int ECMD_CTLMOVERIGHT = 1226;

		// Token: 0x04001825 RID: 6181
		private const int ECMD_CTLMOVEUP = 1227;

		// Token: 0x04001826 RID: 6182
		private const int ECMD_CTLSIZEDOWN = 1228;

		// Token: 0x04001827 RID: 6183
		private const int ECMD_CTLSIZEUP = 1229;

		// Token: 0x04001828 RID: 6184
		private const int ECMD_CTLSIZELEFT = 1230;

		// Token: 0x04001829 RID: 6185
		private const int ECMD_CTLSIZERIGHT = 1231;

		// Token: 0x0400182A RID: 6186
		private const int cmdidEditLabel = 338;

		// Token: 0x0400182B RID: 6187
		private const int ECMD_HOME = 15;

		// Token: 0x0400182C RID: 6188
		private const int ECMD_HOME_EXT = 16;

		// Token: 0x0400182D RID: 6189
		private const int ECMD_END = 17;

		// Token: 0x0400182E RID: 6190
		private const int ECMD_END_EXT = 18;

		// Token: 0x0400182F RID: 6191
		public static readonly CommandID SelectionMenu = new CommandID(MenuCommands.wfMenuGroup, 1280);

		// Token: 0x04001830 RID: 6192
		public static readonly CommandID ContainerMenu = new CommandID(MenuCommands.wfMenuGroup, 1281);

		// Token: 0x04001831 RID: 6193
		public static readonly CommandID TraySelectionMenu = new CommandID(MenuCommands.wfMenuGroup, 1283);

		// Token: 0x04001832 RID: 6194
		public static readonly CommandID ComponentTrayMenu = new CommandID(MenuCommands.wfMenuGroup, 1286);

		// Token: 0x04001833 RID: 6195
		public static readonly CommandID DesignerProperties = new CommandID(MenuCommands.wfCommandSet, 4097);

		// Token: 0x04001834 RID: 6196
		public static readonly CommandID KeyCancel = new CommandID(MenuCommands.guidVSStd2K, 103);

		// Token: 0x04001835 RID: 6197
		public static readonly CommandID KeyReverseCancel = new CommandID(MenuCommands.wfCommandSet, 16385);

		// Token: 0x04001836 RID: 6198
		public static readonly CommandID KeyInvokeSmartTag = new CommandID(MenuCommands.guidVSStd2K, 147);

		// Token: 0x04001837 RID: 6199
		public static readonly CommandID KeyDefaultAction = new CommandID(MenuCommands.guidVSStd2K, 3);

		// Token: 0x04001838 RID: 6200
		public static readonly CommandID KeyMoveUp = new CommandID(MenuCommands.guidVSStd2K, 11);

		// Token: 0x04001839 RID: 6201
		public static readonly CommandID KeyMoveDown = new CommandID(MenuCommands.guidVSStd2K, 13);

		// Token: 0x0400183A RID: 6202
		public static readonly CommandID KeyMoveLeft = new CommandID(MenuCommands.guidVSStd2K, 7);

		// Token: 0x0400183B RID: 6203
		public static readonly CommandID KeyMoveRight = new CommandID(MenuCommands.guidVSStd2K, 9);

		// Token: 0x0400183C RID: 6204
		public static readonly CommandID KeyNudgeUp = new CommandID(MenuCommands.guidVSStd2K, 1227);

		// Token: 0x0400183D RID: 6205
		public static readonly CommandID KeyNudgeDown = new CommandID(MenuCommands.guidVSStd2K, 1225);

		// Token: 0x0400183E RID: 6206
		public static readonly CommandID KeyNudgeLeft = new CommandID(MenuCommands.guidVSStd2K, 1224);

		// Token: 0x0400183F RID: 6207
		public static readonly CommandID KeyNudgeRight = new CommandID(MenuCommands.guidVSStd2K, 1226);

		// Token: 0x04001840 RID: 6208
		public static readonly CommandID KeySizeWidthIncrease = new CommandID(MenuCommands.guidVSStd2K, 10);

		// Token: 0x04001841 RID: 6209
		public static readonly CommandID KeySizeHeightIncrease = new CommandID(MenuCommands.guidVSStd2K, 12);

		// Token: 0x04001842 RID: 6210
		public static readonly CommandID KeySizeWidthDecrease = new CommandID(MenuCommands.guidVSStd2K, 8);

		// Token: 0x04001843 RID: 6211
		public static readonly CommandID KeySizeHeightDecrease = new CommandID(MenuCommands.guidVSStd2K, 14);

		// Token: 0x04001844 RID: 6212
		public static readonly CommandID KeyNudgeWidthIncrease = new CommandID(MenuCommands.guidVSStd2K, 1231);

		// Token: 0x04001845 RID: 6213
		public static readonly CommandID KeyNudgeHeightIncrease = new CommandID(MenuCommands.guidVSStd2K, 1228);

		// Token: 0x04001846 RID: 6214
		public static readonly CommandID KeyNudgeWidthDecrease = new CommandID(MenuCommands.guidVSStd2K, 1230);

		// Token: 0x04001847 RID: 6215
		public static readonly CommandID KeyNudgeHeightDecrease = new CommandID(MenuCommands.guidVSStd2K, 1229);

		// Token: 0x04001848 RID: 6216
		public static readonly CommandID KeySelectNext = new CommandID(MenuCommands.guidVSStd2K, 4);

		// Token: 0x04001849 RID: 6217
		public static readonly CommandID KeySelectPrevious = new CommandID(MenuCommands.guidVSStd2K, 5);

		// Token: 0x0400184A RID: 6218
		public static readonly CommandID KeyTabOrderSelect = new CommandID(MenuCommands.wfCommandSet, 16405);

		// Token: 0x0400184B RID: 6219
		public static readonly CommandID EditLabel = new CommandID(MenuCommands.VSStandardCommandSet97, 338);

		// Token: 0x0400184C RID: 6220
		public static readonly CommandID KeyHome = new CommandID(MenuCommands.guidVSStd2K, 15);

		// Token: 0x0400184D RID: 6221
		public static readonly CommandID KeyEnd = new CommandID(MenuCommands.guidVSStd2K, 17);

		// Token: 0x0400184E RID: 6222
		public static readonly CommandID KeyShiftHome = new CommandID(MenuCommands.guidVSStd2K, 16);

		// Token: 0x0400184F RID: 6223
		public static readonly CommandID KeyShiftEnd = new CommandID(MenuCommands.guidVSStd2K, 18);

		// Token: 0x04001850 RID: 6224
		public static readonly CommandID SetStatusText = new CommandID(MenuCommands.wfCommandSet, 16387);

		// Token: 0x04001851 RID: 6225
		public static readonly CommandID SetStatusRectangle = new CommandID(MenuCommands.wfCommandSet, 16388);
	}
}

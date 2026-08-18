using System;

namespace Spire.CompoundFile.XLS
{
	// Token: 0x020001D9 RID: 473
	[Flags]
	public enum PropertyType
	{
		// Token: 0x04001032 RID: 4146
		Bool = 11,
		// Token: 0x04001033 RID: 4147
		Int = 22,
		// Token: 0x04001034 RID: 4148
		Int32 = 3,
		// Token: 0x04001035 RID: 4149
		Int16 = 2,
		// Token: 0x04001036 RID: 4150
		UInt32 = 19,
		// Token: 0x04001037 RID: 4151
		String = 31,
		// Token: 0x04001038 RID: 4152
		AsciiString = 30,
		// Token: 0x04001039 RID: 4153
		DateTime = 64,
		// Token: 0x0400103A RID: 4154
		Blob = 65,
		// Token: 0x0400103B RID: 4155
		Vector = 4096,
		// Token: 0x0400103C RID: 4156
		Object = 12,
		// Token: 0x0400103D RID: 4157
		Double = 5,
		// Token: 0x0400103E RID: 4158
		Empty = 0,
		// Token: 0x0400103F RID: 4159
		Null = 1,
		// Token: 0x04001040 RID: 4160
		ClipboardData = 71,
		// Token: 0x04001041 RID: 4161
		AsciiStringArray = 4126,
		// Token: 0x04001042 RID: 4162
		StringArray = 4127,
		// Token: 0x04001043 RID: 4163
		ObjectArray = 4108
	}
}

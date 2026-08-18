using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000053 RID: 83
	[Flags]
	internal enum EsProtectionFlags
	{
		// Token: 0x040004A0 RID: 1184
		LockRotation = 256,
		// Token: 0x040004A1 RID: 1185
		LockAspectRatio = 128,
		// Token: 0x040004A2 RID: 1186
		LockPosition = 64,
		// Token: 0x040004A3 RID: 1187
		LockAgainstSelect = 32,
		// Token: 0x040004A4 RID: 1188
		LockCropping = 16,
		// Token: 0x040004A5 RID: 1189
		LockVertices = 8,
		// Token: 0x040004A6 RID: 1190
		LockText = 4,
		// Token: 0x040004A7 RID: 1191
		LockAdjustHandles = 2,
		// Token: 0x040004A8 RID: 1192
		LockAgainstGrouping = 1
	}
}

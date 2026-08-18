using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpeechLib
{
	// Token: 0x02000198 RID: 408
	[CompilerGenerated]
	[Guid("269316D8-57BD-11D2-9EEE-00C04F797396")]
	[TypeIdentifier]
	[ComImport]
	public interface ISpeechVoice
	{
		// Token: 0x06000BDE RID: 3038
		void _VtblGap1_21();

		// Token: 0x06000BDF RID: 3039
		[DispId(12)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		int Speak([MarshalAs(UnmanagedType.BStr)] [In] string Text, [In] SpeechVoiceSpeakFlags Flags = SpeechVoiceSpeakFlags.SVSFDefault);
	}
}

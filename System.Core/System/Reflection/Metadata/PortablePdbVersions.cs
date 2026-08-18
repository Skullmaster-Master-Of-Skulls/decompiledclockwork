using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000063 RID: 99
	internal static class PortablePdbVersions
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000749C File Offset: 0x0000569C
		internal static uint DebugDirectoryEntryVersion(ushort portablePdbVersion)
		{
			return 1347223552U | (uint)portablePdbVersion;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000074A5 File Offset: 0x000056A5
		internal static uint DebugDirectoryEmbeddedVersion(ushort portablePdbVersion)
		{
			return 16777216U | (uint)portablePdbVersion;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000074B0 File Offset: 0x000056B0
		internal static string Format(ushort version)
		{
			return (version >> 8).ToString() + "." + ((int)(version & 255)).ToString();
		}

		// Token: 0x04000352 RID: 850
		internal const string DefaultMetadataVersion = "PDB v1.0";

		// Token: 0x04000353 RID: 851
		internal const ushort DefaultFormatVersion = 256;

		// Token: 0x04000354 RID: 852
		internal const ushort MinFormatVersion = 256;

		// Token: 0x04000355 RID: 853
		internal const ushort MinEmbeddedVersion = 256;

		// Token: 0x04000356 RID: 854
		internal const ushort DefaultEmbeddedVersion = 256;

		// Token: 0x04000357 RID: 855
		internal const ushort MinUnsupportedEmbeddedVersion = 512;

		// Token: 0x04000358 RID: 856
		internal const uint DebugDirectoryEmbeddedSignature = 1111773261U;

		// Token: 0x04000359 RID: 857
		internal const ushort PortableCodeViewVersionMagic = 20557;
	}
}

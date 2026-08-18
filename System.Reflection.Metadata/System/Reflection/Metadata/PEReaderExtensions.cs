using System;
using System.ComponentModel;
using System.Reflection.PortableExecutable;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A6 RID: 166
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PEReaderExtensions
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0000FD60 File Offset: 0x0000DF60
		public static MethodBodyBlock GetMethodBody(this PEReader peReader, int relativeVirtualAddress)
		{
			if (peReader == null)
			{
				throw new ArgumentNullException("peReader");
			}
			PEMemoryBlock sectionData = peReader.GetSectionData(relativeVirtualAddress);
			if (sectionData.Length == 0)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidMethodRva, relativeVirtualAddress));
			}
			return MethodBodyBlock.Create(new BlobReader(sectionData.Pointer, sectionData.Length));
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000FDBA File Offset: 0x0000DFBA
		public static MetadataReader GetMetadataReader(this PEReader peReader)
		{
			return peReader.GetMetadataReader(MetadataReaderOptions.Default, null);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public static MetadataReader GetMetadataReader(this PEReader peReader, MetadataReaderOptions options)
		{
			return peReader.GetMetadataReader(options, null);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		public static MetadataReader GetMetadataReader(this PEReader peReader, MetadataReaderOptions options, MetadataStringDecoder utf8Decoder)
		{
			PEMemoryBlock metadata = peReader.GetMetadata();
			return new MetadataReader(metadata.Pointer, metadata.Length, options, utf8Decoder);
		}
	}
}

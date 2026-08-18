using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000012 RID: 18
	internal static class ManagedPEBuilder
	{
		// Token: 0x0600010E RID: 270 RVA: 0x000045DC File Offset: 0x000027DC
		public static void AddManagedSections(this PEBuilder peBuilder, PEDirectoriesBuilder peDirectoriesBuilder, TypeSystemMetadataSerializer metadataSerializer, BlobBuilder ilStream, BlobBuilder mappedFieldData, BlobBuilder managedResourceData, Action<BlobBuilder, PESectionLocation> nativeResourceSectionSerializer, int strongNameSignatureSize, MethodDefinitionHandle entryPoint, string pdbPathOpt, ContentId nativePdbContentId, ContentId portablePdbContentId, CorFlags corFlags)
		{
			int entryPointAddress = 0;
			peBuilder.AddSection(".text", SectionCharacteristics.ContainsCode | SectionCharacteristics.MemExecute | SectionCharacteristics.MemRead, delegate(PESectionLocation location)
			{
				BlobBuilder blobBuilder = new BlobBuilder(256);
				BlobBuilder blobBuilder2 = new BlobBuilder(256);
				ManagedTextSection managedTextSection = new ManagedTextSection(metadataSerializer.MetadataSizes.MetadataSize, ilStream.Count, mappedFieldData.Count, managedResourceData.Count, strongNameSignatureSize, peBuilder.ImageCharacteristics, peBuilder.Machine, pdbPathOpt, peBuilder.IsDeterministic);
				int methodBodyStreamRva = location.RelativeVirtualAddress + managedTextSection.OffsetToILStream;
				int mappedFieldDataStreamRva = location.RelativeVirtualAddress + managedTextSection.CalculateOffsetToMappedFieldDataStream();
				metadataSerializer.SerializeMetadata(blobBuilder2, methodBodyStreamRva, mappedFieldDataStreamRva);
				BlobBuilder blobBuilder3;
				if (pdbPathOpt != null || peBuilder.IsDeterministic)
				{
					blobBuilder3 = new BlobBuilder(256);
					managedTextSection.WriteDebugTable(blobBuilder3, location, nativePdbContentId, portablePdbContentId);
				}
				else
				{
					blobBuilder3 = null;
				}
				entryPointAddress = managedTextSection.GetEntryPointAddress(location.RelativeVirtualAddress);
				managedTextSection.Serialize(blobBuilder, location.RelativeVirtualAddress, entryPoint.IsNil ? 0 : MetadataTokens.GetToken(entryPoint), corFlags, peBuilder.ImageBase, blobBuilder2, ilStream, mappedFieldData, managedResourceData, blobBuilder3);
				peDirectoriesBuilder.AddressOfEntryPoint = entryPointAddress;
				peDirectoriesBuilder.DebugTable = managedTextSection.GetDebugDirectoryEntry(location.RelativeVirtualAddress);
				peDirectoriesBuilder.ImportAddressTable = managedTextSection.GetImportAddressTableDirectoryEntry(location.RelativeVirtualAddress);
				peDirectoriesBuilder.ImportTable = managedTextSection.GetImportTableDirectoryEntry(location.RelativeVirtualAddress);
				peDirectoriesBuilder.CorHeaderTable = managedTextSection.GetCorHeaderDirectoryEntry(location.RelativeVirtualAddress);
				return blobBuilder;
			});
			if (nativeResourceSectionSerializer != null)
			{
				peBuilder.AddSection(".rsrc", SectionCharacteristics.ContainsInitializedData | SectionCharacteristics.MemRead, delegate(PESectionLocation location)
				{
					BlobBuilder blobBuilder = new BlobBuilder(256);
					nativeResourceSectionSerializer(blobBuilder, location);
					peDirectoriesBuilder.ResourceTable = new DirectoryEntry(location.RelativeVirtualAddress, blobBuilder.Count);
					return blobBuilder;
				});
			}
			if (peBuilder.Machine == Machine.I386 || peBuilder.Machine == Machine.Unknown)
			{
				peBuilder.AddSection(".reloc", SectionCharacteristics.ContainsInitializedData | SectionCharacteristics.MemDiscardable | SectionCharacteristics.MemRead, delegate(PESectionLocation location)
				{
					BlobBuilder blobBuilder = new BlobBuilder(256);
					ManagedPEBuilder.WriteRelocSection(blobBuilder, peBuilder.Machine, entryPointAddress);
					peDirectoriesBuilder.BaseRelocationTable = new DirectoryEntry(location.RelativeVirtualAddress, blobBuilder.Count);
					return blobBuilder;
				});
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000046E4 File Offset: 0x000028E4
		private static void WriteRelocSection(BlobBuilder builder, Machine machine, int entryPointAddress)
		{
			builder.WriteUInt32((uint)((entryPointAddress + 2) / 4096 * 4096));
			builder.WriteUInt32((machine == Machine.IA64) ? 14U : 12U);
			uint num = (uint)((entryPointAddress + 2) % 4096);
			uint num2 = (machine == Machine.Amd64 || machine == Machine.IA64) ? 10U : 3U;
			ushort value = (ushort)(num2 << 12 | num);
			builder.WriteUInt16(value);
			if (machine == Machine.IA64)
			{
				builder.WriteUInt32(num2 << 12);
			}
			builder.WriteUInt16(0);
		}
	}
}

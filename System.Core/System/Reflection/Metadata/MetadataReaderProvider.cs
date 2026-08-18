using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;
using System.Security;
using System.Threading;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005A RID: 90
	internal sealed class MetadataReaderProvider : IDisposable
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00006F4C File Offset: 0x0000514C
		private MetadataReaderProvider(AbstractMemoryBlock metadataBlock)
		{
			this._lazyMetadataBlock = metadataBlock;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00006F66 File Offset: 0x00005166
		private MetadataReaderProvider(MemoryBlockProvider blockProvider)
		{
			this._blockProviderOpt = blockProvider;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00006F80 File Offset: 0x00005180
		[SecurityCritical]
		public unsafe static MetadataReaderProvider FromPortablePdbImage(byte* start, int size)
		{
			return MetadataReaderProvider.FromMetadataImage(start, size);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006F89 File Offset: 0x00005189
		[SecurityCritical]
		public unsafe static MetadataReaderProvider FromMetadataImage(byte* start, int size)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return new MetadataReaderProvider(new ExternalMemoryBlockProvider(start, size));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00006FB6 File Offset: 0x000051B6
		public static MetadataReaderProvider FromPortablePdbImage(ImmutableArray<byte> image)
		{
			return MetadataReaderProvider.FromMetadataImage(image);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006FBE File Offset: 0x000051BE
		public static MetadataReaderProvider FromMetadataImage(ImmutableArray<byte> image)
		{
			if (image.IsDefault)
			{
				throw new ArgumentNullException("image");
			}
			return new MetadataReaderProvider(new ByteArrayMemoryProvider(image));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006FDF File Offset: 0x000051DF
		public static MetadataReaderProvider FromPortablePdbStream(Stream stream, MetadataStreamOptions options = MetadataStreamOptions.Default, int size = 0)
		{
			return MetadataReaderProvider.FromMetadataStream(stream, options, size);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006FEC File Offset: 0x000051EC
		public static MetadataReaderProvider FromMetadataStream(Stream stream, MetadataStreamOptions options = MetadataStreamOptions.Default, int size = 0)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException("StreamMustSupportReadAndSeek", "stream");
			}
			if (!options.IsValid())
			{
				throw new ArgumentOutOfRangeException("options");
			}
			long position = stream.Position;
			int andValidateSize = StreamExtensions.GetAndValidateSize(stream, size, "stream");
			bool flag = true;
			MetadataReaderProvider result;
			try
			{
				bool isFileStream = stream is FileStream;
				if ((options & MetadataStreamOptions.PrefetchMetadata) == MetadataStreamOptions.Default)
				{
					result = new MetadataReaderProvider(new StreamMemoryBlockProvider(stream, position, andValidateSize, isFileStream, (options & MetadataStreamOptions.LeaveOpen) > MetadataStreamOptions.Default));
					flag = false;
				}
				else
				{
					result = new MetadataReaderProvider(StreamMemoryBlockProvider.ReadMemoryBlockNoLock(stream, isFileStream, position, andValidateSize));
				}
			}
			finally
			{
				if (flag && (options & MetadataStreamOptions.LeaveOpen) == MetadataStreamOptions.Default)
				{
					stream.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000070AC File Offset: 0x000052AC
		public void Dispose()
		{
			MemoryBlockProvider blockProviderOpt = this._blockProviderOpt;
			if (blockProviderOpt != null)
			{
				blockProviderOpt.Dispose();
			}
			this._blockProviderOpt = null;
			AbstractMemoryBlock lazyMetadataBlock = this._lazyMetadataBlock;
			if (lazyMetadataBlock != null)
			{
				lazyMetadataBlock.Dispose();
			}
			this._lazyMetadataBlock = null;
			this._lazyMetadataReader = null;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000070E8 File Offset: 0x000052E8
		[SecuritySafeCritical]
		public MetadataReader GetMetadataReader(MetadataReaderOptions options = MetadataReaderOptions.Default)
		{
			MetadataReader lazyMetadataReader = this._lazyMetadataReader;
			if (MetadataReaderProvider.CanReuseReader(lazyMetadataReader, options))
			{
				return lazyMetadataReader;
			}
			object metadataReaderGuard = this._metadataReaderGuard;
			MetadataReader result;
			lock (metadataReaderGuard)
			{
				lazyMetadataReader = this._lazyMetadataReader;
				if (MetadataReaderProvider.CanReuseReader(lazyMetadataReader, options))
				{
					result = lazyMetadataReader;
				}
				else
				{
					AbstractMemoryBlock metadataBlock = this.GetMetadataBlock();
					MetadataReader metadataReader = new MetadataReader(metadataBlock.Pointer, metadataBlock.Size, options);
					this._lazyMetadataReader = metadataReader;
					result = metadataReader;
				}
			}
			return result;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007174 File Offset: 0x00005374
		private static bool CanReuseReader(MetadataReader reader, MetadataReaderOptions options)
		{
			return reader != null && reader.Options == options;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007184 File Offset: 0x00005384
		internal AbstractMemoryBlock GetMetadataBlock()
		{
			if (this._lazyMetadataBlock == null)
			{
				if (this._blockProviderOpt == null)
				{
					throw new ObjectDisposedException("MetadataReaderProvider");
				}
				AbstractMemoryBlock memoryBlock = this._blockProviderOpt.GetMemoryBlock(0, this._blockProviderOpt.Size);
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyMetadataBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyMetadataBlock;
		}

		// Token: 0x0400033F RID: 831
		private MemoryBlockProvider _blockProviderOpt;

		// Token: 0x04000340 RID: 832
		private AbstractMemoryBlock _lazyMetadataBlock;

		// Token: 0x04000341 RID: 833
		private MetadataReader _lazyMetadataReader;

		// Token: 0x04000342 RID: 834
		private readonly object _metadataReaderGuard = new object();
	}
}

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200029E RID: 670
	internal sealed class StackTraceSymbols : IDisposable
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x00058457 File Offset: 0x00056657
		public StackTraceSymbols()
		{
			this._metadataCache = new ConcurrentDictionary<IntPtr, MetadataReaderProvider>();
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0005846C File Offset: 0x0005666C
		void IDisposable.Dispose()
		{
			foreach (MetadataReaderProvider metadataReaderProvider in this._metadataCache.Values)
			{
				if (metadataReaderProvider != null)
				{
					metadataReaderProvider.Dispose();
				}
			}
			this._metadataCache.Clear();
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000584CC File Offset: 0x000566CC
		[SecuritySafeCritical]
		public void GetSourceLineInfo(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, out string sourceFile, out int sourceLine, out int sourceColumn)
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			this.GetSourceLineInfoWithoutCasAssert(assemblyPath, loadedPeAddress, loadedPeSize, inMemoryPdbAddress, inMemoryPdbSize, methodToken, ilOffset, out sourceFile, out sourceLine, out sourceColumn);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000584FC File Offset: 0x000566FC
		[SecuritySafeCritical]
		public void GetSourceLineInfoWithoutCasAssert(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, out string sourceFile, out int sourceLine, out int sourceColumn)
		{
			sourceFile = null;
			sourceLine = 0;
			sourceColumn = 0;
			try
			{
				MetadataReader metadataReader = this.TryGetReader(assemblyPath, loadedPeAddress, loadedPeSize, inMemoryPdbAddress, inMemoryPdbSize);
				if (metadataReader != null)
				{
					Handle handle = MetadataTokens.Handle(methodToken);
					if (handle.Kind == HandleKind.MethodDefinition)
					{
						MethodDebugInformationHandle handle2 = ((MethodDefinitionHandle)handle).ToDebugInformationHandle();
						MethodDebugInformation methodDebugInformation = metadataReader.GetMethodDebugInformation(handle2);
						if (!methodDebugInformation.SequencePointsBlob.IsNil)
						{
							SequencePointCollection sequencePoints = methodDebugInformation.GetSequencePoints();
							SequencePoint? sequencePoint = null;
							foreach (SequencePoint value in sequencePoints)
							{
								if (value.Offset > ilOffset)
								{
									break;
								}
								if (value.StartLine != 16707566)
								{
									sequencePoint = new SequencePoint?(value);
								}
							}
							if (sequencePoint != null)
							{
								sourceLine = sequencePoint.Value.StartLine;
								sourceColumn = sequencePoint.Value.StartColumn;
								sourceFile = metadataReader.GetString(metadataReader.GetDocument(sequencePoint.Value.Document).Name);
							}
						}
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0005867C File Offset: 0x0005687C
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery))]
		private MetadataReader TryGetReader(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize)
		{
			if ((loadedPeAddress == IntPtr.Zero || assemblyPath == null) && inMemoryPdbAddress == IntPtr.Zero)
			{
				return null;
			}
			IntPtr key = (inMemoryPdbAddress != IntPtr.Zero) ? inMemoryPdbAddress : loadedPeAddress;
			int num = 0;
			MetadataReaderProvider metadataReaderProvider;
			while (!this._metadataCache.TryGetValue(key, out metadataReaderProvider))
			{
				num++;
				metadataReaderProvider = ((inMemoryPdbAddress != IntPtr.Zero) ? StackTraceSymbols.TryOpenReaderForInMemoryPdb(inMemoryPdbAddress, inMemoryPdbSize) : StackTraceSymbols.TryOpenReaderFromAssemblyFile(assemblyPath, loadedPeAddress, loadedPeSize));
				if (this._metadataCache.TryAdd(key, metadataReaderProvider))
				{
					break;
				}
				if (metadataReaderProvider != null)
				{
					metadataReaderProvider.Dispose();
				}
			}
			if (metadataReaderProvider == null)
			{
				return null;
			}
			return metadataReaderProvider.GetMetadataReader(MetadataReaderOptions.Default);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0005871C File Offset: 0x0005691C
		[SecuritySafeCritical]
		private unsafe static MetadataReaderProvider TryOpenReaderForInMemoryPdb(IntPtr inMemoryPdbAddress, int inMemoryPdbSize)
		{
			if (inMemoryPdbSize < 4 || *(uint*)((void*)inMemoryPdbAddress) != 1112167234U)
			{
				return null;
			}
			MetadataReaderProvider metadataReaderProvider = MetadataReaderProvider.FromMetadataImage((byte*)((void*)inMemoryPdbAddress), inMemoryPdbSize);
			MetadataReaderProvider result;
			try
			{
				metadataReaderProvider.GetMetadataReader(MetadataReaderOptions.Default);
				result = metadataReaderProvider;
			}
			catch (BadImageFormatException)
			{
				metadataReaderProvider.Dispose();
				result = null;
			}
			return result;
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00058774 File Offset: 0x00056974
		[SecuritySafeCritical]
		private static PEReader TryGetPEReader(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize)
		{
			Stream stream = StackTraceSymbols.TryOpenFile(assemblyPath);
			if (stream != null)
			{
				return new PEReader(stream);
			}
			return null;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00058794 File Offset: 0x00056994
		private static MetadataReaderProvider TryOpenReaderFromAssemblyFile(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize)
		{
			using (PEReader pereader = StackTraceSymbols.TryGetPEReader(assemblyPath, loadedPeAddress, loadedPeSize))
			{
				if (pereader == null)
				{
					return null;
				}
				MetadataReaderProvider result;
				string text;
				if (pereader.TryOpenAssociatedPortablePdb(assemblyPath, new Func<string, Stream>(StackTraceSymbols.TryOpenFile), out result, out text))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000587F0 File Offset: 0x000569F0
		private static Stream TryOpenFile(string path)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			Stream result;
			try
			{
				result = File.OpenRead(path);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000BAD RID: 2989
		private readonly ConcurrentDictionary<IntPtr, MetadataReaderProvider> _metadataCache;
	}
}

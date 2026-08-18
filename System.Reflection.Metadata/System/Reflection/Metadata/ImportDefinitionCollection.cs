using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009D RID: 157
	public struct ImportDefinitionCollection : IEnumerable<ImportDefinition>, IEnumerable
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x0000F84F File Offset: 0x0000DA4F
		internal ImportDefinitionCollection(MemoryBlock block)
		{
			this._block = block;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000F858 File Offset: 0x0000DA58
		public ImportDefinitionCollection.Enumerator GetEnumerator()
		{
			return new ImportDefinitionCollection.Enumerator(this._block);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000F865 File Offset: 0x0000DA65
		IEnumerator<ImportDefinition> IEnumerable<ImportDefinition>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000F865 File Offset: 0x0000DA65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400040A RID: 1034
		private readonly MemoryBlock _block;

		// Token: 0x02000191 RID: 401
		public struct Enumerator : IEnumerator<ImportDefinition>, IEnumerator, IDisposable
		{
			// Token: 0x06000C14 RID: 3092 RVA: 0x00021A96 File Offset: 0x0001FC96
			internal Enumerator(MemoryBlock block)
			{
				this._reader = new BlobReader(block);
				this._current = default(ImportDefinition);
			}

			// Token: 0x06000C15 RID: 3093 RVA: 0x00021AB0 File Offset: 0x0001FCB0
			public bool MoveNext()
			{
				if (this._reader.RemainingBytes == 0)
				{
					return false;
				}
				ImportDefinitionKind importDefinitionKind = (ImportDefinitionKind)this._reader.ReadByte();
				switch (importDefinitionKind)
				{
				case ImportDefinitionKind.ImportNamespace:
					this._current = new ImportDefinition(importDefinitionKind, default(BlobHandle), default(AssemblyReferenceHandle), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.ImportAssemblyNamespace:
					this._current = new ImportDefinition(importDefinitionKind, default(BlobHandle), MetadataTokens.AssemblyReferenceHandle(this._reader.ReadCompressedInteger()), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.ImportType:
					this._current = new ImportDefinition(importDefinitionKind, default(BlobHandle), default(AssemblyReferenceHandle), this._reader.ReadTypeHandle());
					break;
				case ImportDefinitionKind.ImportXmlNamespace:
				case ImportDefinitionKind.AliasNamespace:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), default(AssemblyReferenceHandle), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.ImportAssemblyReferenceAlias:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), default(AssemblyReferenceHandle), default(Handle));
					break;
				case ImportDefinitionKind.AliasAssemblyReference:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), MetadataTokens.AssemblyReferenceHandle(this._reader.ReadCompressedInteger()), default(Handle));
					break;
				case ImportDefinitionKind.AliasAssemblyNamespace:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), MetadataTokens.AssemblyReferenceHandle(this._reader.ReadCompressedInteger()), MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()));
					break;
				case ImportDefinitionKind.AliasType:
					this._current = new ImportDefinition(importDefinitionKind, MetadataTokens.BlobHandle(this._reader.ReadCompressedInteger()), default(AssemblyReferenceHandle), this._reader.ReadTypeHandle());
					break;
				default:
					throw new BadImageFormatException(string.Format(SR.InvalidImportDefinitionKind, new object[]
					{
						importDefinitionKind
					}));
				}
				return true;
			}

			// Token: 0x170002F9 RID: 761
			// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00021CF9 File Offset: 0x0001FEF9
			public ImportDefinition Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00021D01 File Offset: 0x0001FF01
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06000C18 RID: 3096 RVA: 0x00021D0E File Offset: 0x0001FF0E
			public void Reset()
			{
				this._reader.SeekOffset(0);
				this._current = default(ImportDefinition);
			}

			// Token: 0x06000C19 RID: 3097 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A21 RID: 2593
			private BlobReader _reader;

			// Token: 0x04000A22 RID: 2594
			private ImportDefinition _current;
		}
	}
}

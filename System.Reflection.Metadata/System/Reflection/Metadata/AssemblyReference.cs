using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000031 RID: 49
	public struct AssemblyReference
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00007455 File Offset: 0x00005655
		internal AssemblyReference(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00007465 File Offset: 0x00005665
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00007473 File Offset: 0x00005673
		private bool IsVirtual
		{
			get
			{
				return (this._treatmentAndRowId & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00007484 File Offset: 0x00005684
		public Version Version
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualVersion();
				}
				if (this.RowId == this._reader.WinMDMscorlibRef)
				{
					return AssemblyReference.s_version_4_0_0_0;
				}
				return this._reader.AssemblyRefTable.GetVersion(this.RowId);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000074C4 File Offset: 0x000056C4
		public AssemblyFlags Flags
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualFlags();
				}
				return this._reader.AssemblyRefTable.GetFlags(this.RowId);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000074EB File Offset: 0x000056EB
		public StringHandle Name
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualName();
				}
				return this._reader.AssemblyRefTable.GetName(this.RowId);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00007512 File Offset: 0x00005712
		public StringHandle Culture
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualCulture();
				}
				return this._reader.AssemblyRefTable.GetCulture(this.RowId);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00007539 File Offset: 0x00005739
		public BlobHandle PublicKeyOrToken
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualPublicKeyOrToken();
				}
				return this._reader.AssemblyRefTable.GetPublicKeyOrToken(this.RowId);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00007560 File Offset: 0x00005760
		public BlobHandle HashValue
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualHashValue();
				}
				return this._reader.AssemblyRefTable.GetHashValue(this.RowId);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007587 File Offset: 0x00005787
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			if (this.IsVirtual)
			{
				return this.GetVirtualCustomAttributes();
			}
			return new CustomAttributeHandleCollection(this._reader, AssemblyReferenceHandle.FromRowId(this.RowId));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000075B3 File Offset: 0x000057B3
		private Version GetVirtualVersion()
		{
			return AssemblyReference.s_version_4_0_0_0;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000075BA File Offset: 0x000057BA
		private AssemblyFlags GetVirtualFlags()
		{
			return this._reader.AssemblyRefTable.GetFlags(this._reader.WinMDMscorlibRef);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000075D7 File Offset: 0x000057D7
		private StringHandle GetVirtualName()
		{
			return StringHandle.FromVirtualIndex(this.GetVirtualNameIndex((AssemblyReferenceHandle.VirtualIndex)this.RowId));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000075EA File Offset: 0x000057EA
		private StringHandle.VirtualIndex GetVirtualNameIndex(AssemblyReferenceHandle.VirtualIndex index)
		{
			switch (index)
			{
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime:
				return StringHandle.VirtualIndex.System_Runtime;
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime:
				return StringHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime;
			case AssemblyReferenceHandle.VirtualIndex.System_ObjectModel:
				return StringHandle.VirtualIndex.System_ObjectModel;
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime:
				return StringHandle.VirtualIndex.System_Runtime_WindowsRuntime;
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml:
				return StringHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml;
			case AssemblyReferenceHandle.VirtualIndex.System_Numerics_Vectors:
				return StringHandle.VirtualIndex.System_Numerics_Vectors;
			default:
				return StringHandle.VirtualIndex.System_Runtime_WindowsRuntime;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000761C File Offset: 0x0000581C
		private StringHandle GetVirtualCulture()
		{
			return default(StringHandle);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007634 File Offset: 0x00005834
		private BlobHandle GetVirtualPublicKeyOrToken()
		{
			AssemblyReferenceHandle.VirtualIndex rowId = (AssemblyReferenceHandle.VirtualIndex)this.RowId;
			if (rowId == AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime || rowId == AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml)
			{
				return this._reader.AssemblyRefTable.GetPublicKeyOrToken(this._reader.WinMDMscorlibRef);
			}
			return BlobHandle.FromVirtualIndex(((this._reader.AssemblyRefTable.GetFlags(this._reader.WinMDMscorlibRef) & AssemblyFlags.PublicKey) > (AssemblyFlags)0) ? BlobHandle.VirtualIndex.ContractPublicKey : BlobHandle.VirtualIndex.ContractPublicKeyToken, 0);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007698 File Offset: 0x00005898
		private BlobHandle GetVirtualHashValue()
		{
			return default(BlobHandle);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000076AE File Offset: 0x000058AE
		private CustomAttributeHandleCollection GetVirtualCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, AssemblyReferenceHandle.FromRowId(this._reader.WinMDMscorlibRef));
		}

		// Token: 0x04000266 RID: 614
		private readonly MetadataReader _reader;

		// Token: 0x04000267 RID: 615
		private readonly uint _treatmentAndRowId;

		// Token: 0x04000268 RID: 616
		private static readonly Version s_version_4_0_0_0 = new Version(4, 0, 0, 0);
	}
}

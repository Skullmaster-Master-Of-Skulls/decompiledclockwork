using System;
using System.Reflection.Metadata.Decoding;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000035 RID: 53
	public struct CustomAttribute
	{
		// Token: 0x060002BB RID: 699 RVA: 0x00007E2A File Offset: 0x0000602A
		internal CustomAttribute(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007E3A File Offset: 0x0000603A
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00007E48 File Offset: 0x00006048
		private CustomAttributeHandle Handle
		{
			get
			{
				return CustomAttributeHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00007E55 File Offset: 0x00006055
		private MethodDefTreatment Treatment
		{
			get
			{
				return (MethodDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00007E61 File Offset: 0x00006061
		public EntityHandle Constructor
		{
			get
			{
				return this._reader.CustomAttributeTable.GetConstructor(this.Handle);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00007E79 File Offset: 0x00006079
		public EntityHandle Parent
		{
			get
			{
				return this._reader.CustomAttributeTable.GetParent(this.Handle);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00007E91 File Offset: 0x00006091
		public BlobHandle Value
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.CustomAttributeTable.GetValue(this.Handle);
				}
				return this.GetProjectedValue();
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007EB8 File Offset: 0x000060B8
		internal CustomAttributeValue<TType> DecodeValue<TType>(ICustomAttributeTypeProvider<TType> provider)
		{
			CustomAttributeDecoder<TType> customAttributeDecoder = new CustomAttributeDecoder<TType>(provider, this._reader);
			return customAttributeDecoder.DecodeValue(this.Constructor, this.Value);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007EE8 File Offset: 0x000060E8
		private BlobHandle GetProjectedValue()
		{
			CustomAttributeValueTreatment customAttributeValueTreatment = this._reader.CalculateCustomAttributeValueTreatment(this.Handle);
			if (customAttributeValueTreatment == CustomAttributeValueTreatment.None)
			{
				return this._reader.CustomAttributeTable.GetValue(this.Handle);
			}
			return this.GetProjectedValue(customAttributeValueTreatment);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007F28 File Offset: 0x00006128
		private BlobHandle GetProjectedValue(CustomAttributeValueTreatment treatment)
		{
			BlobHandle.VirtualIndex virtualIndex;
			bool flag;
			switch (treatment)
			{
			case CustomAttributeValueTreatment.AttributeUsageAllowSingle:
				virtualIndex = BlobHandle.VirtualIndex.AttributeUsage_AllowSingle;
				flag = false;
				break;
			case CustomAttributeValueTreatment.AttributeUsageAllowMultiple:
				virtualIndex = BlobHandle.VirtualIndex.AttributeUsage_AllowMultiple;
				flag = false;
				break;
			case CustomAttributeValueTreatment.AttributeUsageVersionAttribute:
			case CustomAttributeValueTreatment.AttributeUsageDeprecatedAttribute:
				virtualIndex = BlobHandle.VirtualIndex.AttributeUsage_AllowMultiple;
				flag = true;
				break;
			default:
				return default(BlobHandle);
			}
			BlobHandle value = this._reader.CustomAttributeTable.GetValue(this.Handle);
			BlobReader blobReader = this._reader.GetBlobReader(value);
			if (blobReader.Length != 8)
			{
				return value;
			}
			if (blobReader.ReadInt16() != 1)
			{
				return value;
			}
			AttributeTargets attributeTargets = CustomAttribute.ProjectAttributeTargetValue(blobReader.ReadUInt32());
			if (flag)
			{
				attributeTargets |= (AttributeTargets.Constructor | AttributeTargets.Property);
			}
			return BlobHandle.FromVirtualIndex(virtualIndex, (ushort)attributeTargets);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007FCC File Offset: 0x000061CC
		private static AttributeTargets ProjectAttributeTargetValue(uint rawValue)
		{
			if (rawValue == 4294967295U)
			{
				return AttributeTargets.All;
			}
			AttributeTargets attributeTargets = (AttributeTargets)0;
			if ((rawValue & 1U) != 0U)
			{
				attributeTargets |= AttributeTargets.Delegate;
			}
			if ((rawValue & 2U) != 0U)
			{
				attributeTargets |= AttributeTargets.Enum;
			}
			if ((rawValue & 4U) != 0U)
			{
				attributeTargets |= AttributeTargets.Event;
			}
			if ((rawValue & 8U) != 0U)
			{
				attributeTargets |= AttributeTargets.Field;
			}
			if ((rawValue & 16U) != 0U)
			{
				attributeTargets |= AttributeTargets.Interface;
			}
			if ((rawValue & 64U) != 0U)
			{
				attributeTargets |= AttributeTargets.Method;
			}
			if ((rawValue & 128U) != 0U)
			{
				attributeTargets |= AttributeTargets.Parameter;
			}
			if ((rawValue & 256U) != 0U)
			{
				attributeTargets |= AttributeTargets.Property;
			}
			if ((rawValue & 512U) != 0U)
			{
				attributeTargets |= AttributeTargets.Class;
			}
			if ((rawValue & 1024U) != 0U)
			{
				attributeTargets |= AttributeTargets.Struct;
			}
			return attributeTargets;
		}

		// Token: 0x04000281 RID: 641
		private readonly MetadataReader _reader;

		// Token: 0x04000282 RID: 642
		private readonly uint _treatmentAndRowId;
	}
}

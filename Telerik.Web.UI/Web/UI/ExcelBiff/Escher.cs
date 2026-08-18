using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A85 RID: 2693
	internal sealed class Escher
	{
		// Token: 0x06006776 RID: 26486 RVA: 0x00182CB4 File Offset: 0x00180EB4
		internal static byte[] CheckSum(byte[] imageBits)
		{
			if (imageBits == null)
			{
				return null;
			}
			return SoapHexBinary.Parse(new SafeMD5
			{
				ValueAsByte = imageBits
			}.FingerPrint).Value;
		}

		// Token: 0x04001A34 RID: 6708
		private const int ClusterSize = 1024;

		// Token: 0x04001A35 RID: 6709
		private const ushort ContainerVersion = 15;

		// Token: 0x04001A36 RID: 6710
		internal const ushort RecordHeaderLength = 8;

		// Token: 0x02000A86 RID: 2694
		internal class BlockHeader
		{
			// Token: 0x06006778 RID: 26488 RVA: 0x00182CEC File Offset: 0x00180EEC
			internal BlockHeader(ushort ver, uint inst, Escher.RecordType fbt, uint cbLength)
			{
				if (ver < 16)
				{
					this.m_escherHeader = (uint)ver;
				}
				if (inst < 4096U)
				{
					this.m_escherHeader |= inst << 4;
				}
				if (65535 >= (ushort)fbt)
				{
					this.m_escherHeader = (uint)((int)((ushort)this.m_escherHeader) | (int)((ushort)fbt) << 16);
				}
				this.m_cbLength = cbLength;
			}

			// Token: 0x06006779 RID: 26489 RVA: 0x00182D48 File Offset: 0x00180F48
			internal virtual byte[] GetData()
			{
				byte[] array = new byte[8];
				int num = 0;
				byte[] bytes = BitConverter.GetBytes(this.m_escherHeader);
				bytes.CopyTo(array, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(this.m_cbLength);
				bytes.CopyTo(array, num);
				num += bytes.Length;
				return array;
			}

			// Token: 0x17002207 RID: 8711
			// (set) Token: 0x0600677A RID: 26490 RVA: 0x00182D95 File Offset: 0x00180F95
			internal uint Instance
			{
				set
				{
					if (value < 4096U)
					{
						this.m_escherHeader &= 4294901775U;
						this.m_escherHeader |= value << 4;
					}
				}
			}

			// Token: 0x17002208 RID: 8712
			// (get) Token: 0x0600677B RID: 26491 RVA: 0x00182DC1 File Offset: 0x00180FC1
			// (set) Token: 0x0600677C RID: 26492 RVA: 0x00182DC9 File Offset: 0x00180FC9
			internal virtual uint Length
			{
				get
				{
					return this.m_cbLength;
				}
				set
				{
					this.m_cbLength = value;
				}
			}

			// Token: 0x04001A37 RID: 6711
			internal const uint CbLengthOffset = 4U;

			// Token: 0x04001A38 RID: 6712
			private uint m_cbLength;

			// Token: 0x04001A39 RID: 6713
			private uint m_escherHeader;
		}

		// Token: 0x02000A87 RID: 2695
		internal sealed class Blip : Escher.BlockHeader
		{
			// Token: 0x0600677D RID: 26493 RVA: 0x00182DD2 File Offset: 0x00180FD2
			[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
			internal Blip(byte[] checkSum, int streamIndex, int imageLength, Escher.RecordType recordType, Escher.BlipSignature blipSignature) : base(0, (uint)blipSignature, recordType, (uint)(imageLength + 16 + 1))
			{
				this.m_streamIndex = -1;
				this.m_rgbUID = checkSum;
				this.m_bTag = byte.MaxValue;
				this.m_streamIndex = streamIndex;
				this.m_imageLength = imageLength;
			}

			// Token: 0x0600677E RID: 26494 RVA: 0x00182E0C File Offset: 0x0018100C
			internal byte[] GetHeaderData()
			{
				byte[] array = new byte[8 + this.m_rgbUID.Length + 1];
				int num = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, num);
				num += data.Length;
				this.m_rgbUID.CopyTo(array, num);
				num += this.m_rgbUID.Length;
				array[num] = this.m_bTag;
				return array;
			}

			// Token: 0x17002209 RID: 8713
			// (get) Token: 0x0600677F RID: 26495 RVA: 0x00182E64 File Offset: 0x00181064
			internal byte[] CheckSum
			{
				get
				{
					return this.m_rgbUID;
				}
			}

			// Token: 0x1700220A RID: 8714
			// (get) Token: 0x06006780 RID: 26496 RVA: 0x00182E6C File Offset: 0x0018106C
			internal override uint Length
			{
				get
				{
					return (uint)(this.m_imageLength + 16 + 1);
				}
			}

			// Token: 0x1700220B RID: 8715
			// (get) Token: 0x06006781 RID: 26497 RVA: 0x00182E79 File Offset: 0x00181079
			internal int StreamIndex
			{
				get
				{
					return this.m_streamIndex;
				}
			}

			// Token: 0x04001A3A RID: 6714
			private byte m_bTag;

			// Token: 0x04001A3B RID: 6715
			private int m_imageLength;

			// Token: 0x04001A3C RID: 6716
			private byte[] m_rgbUID;

			// Token: 0x04001A3D RID: 6717
			private int m_streamIndex;
		}

		// Token: 0x02000A88 RID: 2696
		internal enum BlipSignature
		{
			// Token: 0x04001A3F RID: 6719
			MSOBICLIENT = 2048,
			// Token: 0x04001A40 RID: 6720
			MSOBICMYKJPEG = 1762,
			// Token: 0x04001A41 RID: 6721
			MSOBIDIB = 1960,
			// Token: 0x04001A42 RID: 6722
			MSOBIEMF = 980,
			// Token: 0x04001A43 RID: 6723
			MSOBIJFIF = 1130,
			// Token: 0x04001A44 RID: 6724
			MSOBIJPEG = 1130,
			// Token: 0x04001A45 RID: 6725
			MSOBIPICT = 1346,
			// Token: 0x04001A46 RID: 6726
			MSOBIPNG = 1760,
			// Token: 0x04001A47 RID: 6727
			MSOBITIFF = 1764,
			// Token: 0x04001A48 RID: 6728
			MSOBIUNKNOWN = 0,
			// Token: 0x04001A49 RID: 6729
			MSOBIWMF = 534
		}

		// Token: 0x02000A89 RID: 2697
		internal class BlipStoreContainer : Escher.BlockHeader
		{
			// Token: 0x06006782 RID: 26498 RVA: 0x00182E81 File Offset: 0x00181081
			public BlipStoreContainer() : base(15, 0U, Escher.RecordType.MSOFBTBSTORECONTAINER, 0U)
			{
			}

			// Token: 0x06006783 RID: 26499 RVA: 0x00182E94 File Offset: 0x00181094
			[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
			internal int AddImage(byte[] checkSum, int streamIndex, int imageLength, Escher.RecordType recordType, Escher.BlipType blipType, Escher.BlipSignature blipSignature)
			{
				if (this.m_bSEList == null)
				{
					this.m_bSEList = new Hashtable();
				}
				string @string = Encoding.ASCII.GetString(checkSum);
				if (this.m_bSEList.ContainsKey(@string))
				{
					Escher.BlipStoreEntry blipStoreEntry = (Escher.BlipStoreEntry)this.m_bSEList[@string];
					blipStoreEntry.ReferenceCount += 1U;
					return blipStoreEntry.ReferenceIndex;
				}
				if (this.m_blipList == null)
				{
					this.m_blipList = new ArrayList();
				}
				Escher.Blip blip = new Escher.Blip(checkSum, streamIndex, imageLength, recordType, blipSignature);
				this.m_blipList.Add(blip);
				base.Instance = (uint)this.m_blipList.Count;
				Escher.BlipStoreEntry value = new Escher.BlipStoreEntry(checkSum, blipType, blip.Length, this.m_blipList.Count);
				this.m_bSEList.Add(@string, value);
				this.m_totalLength += 44 + imageLength + 8 + 16 + 1;
				return this.m_blipList.Count;
			}

			// Token: 0x06006784 RID: 26500 RVA: 0x00182F7D File Offset: 0x0018117D
			internal override byte[] GetData()
			{
				if (this.m_blipList == null)
				{
					return null;
				}
				base.Length = (uint)this.m_totalLength;
				return base.GetData();
			}

			// Token: 0x06006785 RID: 26501 RVA: 0x00182F9C File Offset: 0x0018119C
			internal int GetStreamPosFromCheckSum(byte[] checkSum)
			{
				if (this.m_bSEList == null || !this.m_bSEList.ContainsKey(Encoding.ASCII.GetString(checkSum)))
				{
					return -1;
				}
				Escher.BlipStoreEntry blipStoreEntry = (Escher.BlipStoreEntry)this.m_bSEList[Encoding.ASCII.GetString(checkSum)];
				return ((Escher.Blip)this.m_blipList[blipStoreEntry.ReferenceIndex - 1]).StreamIndex;
			}

			// Token: 0x1700220C RID: 8716
			// (get) Token: 0x06006786 RID: 26502 RVA: 0x00183004 File Offset: 0x00181204
			internal ArrayList BlipList
			{
				get
				{
					return this.m_blipList;
				}
			}

			// Token: 0x1700220D RID: 8717
			// (get) Token: 0x06006787 RID: 26503 RVA: 0x0018300C File Offset: 0x0018120C
			internal Hashtable BSEList
			{
				get
				{
					return this.m_bSEList;
				}
			}

			// Token: 0x1700220E RID: 8718
			// (get) Token: 0x06006788 RID: 26504 RVA: 0x00183014 File Offset: 0x00181214
			internal override uint Length
			{
				get
				{
					return (uint)(8 + this.m_totalLength);
				}
			}

			// Token: 0x1700220F RID: 8719
			// (get) Token: 0x06006789 RID: 26505 RVA: 0x0018301E File Offset: 0x0018121E
			internal uint ShapeCount
			{
				get
				{
					return (uint)this.m_blipList.Count;
				}
			}

			// Token: 0x04001A4A RID: 6730
			private const int BSELength = 44;

			// Token: 0x04001A4B RID: 6731
			private ArrayList m_blipList;

			// Token: 0x04001A4C RID: 6732
			private Hashtable m_bSEList;

			// Token: 0x04001A4D RID: 6733
			private int m_totalLength;
		}

		// Token: 0x02000A8A RID: 2698
		internal sealed class BlipStoreEntry : Escher.BlockHeader
		{
			// Token: 0x0600678A RID: 26506 RVA: 0x0018302C File Offset: 0x0018122C
			[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
			internal BlipStoreEntry(byte[] checkSum, Escher.BlipType blipType, uint atomLength, int referenceIndex) : base(2, (uint)blipType, Escher.RecordType.MSOFBTBSE, (uint)((ushort)(atomLength + 36U + 8U)))
			{
				this.m_tag = 255;
				this.m_btWin32 = (byte)blipType;
				this.m_btMacOS = (byte)blipType;
				this.m_rgbUID = checkSum;
				this.m_size = atomLength + 8U;
				this.m_referenceIndex = referenceIndex;
				this.m_cRef = 1U;
			}

			// Token: 0x0600678B RID: 26507 RVA: 0x00183088 File Offset: 0x00181288
			internal override byte[] GetData()
			{
				byte[] array = new byte[44];
				int num = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num);
				num += array2.Length;
				array[num] = this.m_btWin32;
				num++;
				array[num] = this.m_btMacOS;
				num++;
				this.m_rgbUID.CopyTo(array, num);
				num += this.m_rgbUID.Length;
				array2 = BitConverter.GetBytes(this.m_tag);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_size);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_cRef);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_MSOFO);
				array2.CopyTo(array, num);
				num += array2.Length;
				array[num] = this.usage;
				num++;
				array[num] = this.cbName;
				num++;
				array[num] = this.unused2;
				num++;
				array[num] = this.unused3;
				return array;
			}

			// Token: 0x17002210 RID: 8720
			// (get) Token: 0x0600678C RID: 26508 RVA: 0x0018317F File Offset: 0x0018137F
			// (set) Token: 0x0600678D RID: 26509 RVA: 0x00183187 File Offset: 0x00181387
			internal uint ReferenceCount
			{
				get
				{
					return this.m_cRef;
				}
				set
				{
					this.m_cRef = value;
				}
			}

			// Token: 0x17002211 RID: 8721
			// (get) Token: 0x0600678E RID: 26510 RVA: 0x00183190 File Offset: 0x00181390
			internal int ReferenceIndex
			{
				get
				{
					return this.m_referenceIndex;
				}
			}

			// Token: 0x04001A4E RID: 6734
			private const ushort RecordLength = 36;

			// Token: 0x04001A4F RID: 6735
			private byte cbName;

			// Token: 0x04001A50 RID: 6736
			private byte m_btMacOS;

			// Token: 0x04001A51 RID: 6737
			private byte m_btWin32;

			// Token: 0x04001A52 RID: 6738
			private uint m_cRef;

			// Token: 0x04001A53 RID: 6739
			private uint m_MSOFO;

			// Token: 0x04001A54 RID: 6740
			private int m_referenceIndex;

			// Token: 0x04001A55 RID: 6741
			private byte[] m_rgbUID;

			// Token: 0x04001A56 RID: 6742
			private uint m_size;

			// Token: 0x04001A57 RID: 6743
			private ushort m_tag;

			// Token: 0x04001A58 RID: 6744
			private byte unused2;

			// Token: 0x04001A59 RID: 6745
			private byte unused3;

			// Token: 0x04001A5A RID: 6746
			private byte usage;
		}

		// Token: 0x02000A8B RID: 2699
		internal enum BlipType
		{
			// Token: 0x04001A5C RID: 6748
			MSLBLIPFIRSTCLIENT = 32,
			// Token: 0x04001A5D RID: 6749
			MSLBLIPLASTCLIENT = 255,
			// Token: 0x04001A5E RID: 6750
			MSOBLIPCMYKJPEG = 18,
			// Token: 0x04001A5F RID: 6751
			MSOBLIPDIB = 7,
			// Token: 0x04001A60 RID: 6752
			MSOBLIPEMF = 2,
			// Token: 0x04001A61 RID: 6753
			MSOBLIPERROR = 0,
			// Token: 0x04001A62 RID: 6754
			MSOBLIPJPEG = 5,
			// Token: 0x04001A63 RID: 6755
			MSOBLIPPICT = 4,
			// Token: 0x04001A64 RID: 6756
			MSOBLIPPNG = 6,
			// Token: 0x04001A65 RID: 6757
			MSOBLIPUNKNOWN = 1,
			// Token: 0x04001A66 RID: 6758
			MSOBLIPWMF = 3,
			// Token: 0x04001A67 RID: 6759
			MSOBLITIFF = 17
		}

		// Token: 0x02000A8C RID: 2700
		internal enum BlipUsage
		{
			// Token: 0x04001A69 RID: 6761
			MSOBLIPUSAGEDEFAULT,
			// Token: 0x04001A6A RID: 6762
			MSOBLIPUSAGEMAX = 255,
			// Token: 0x04001A6B RID: 6763
			MSOBLIPUSAGETEXTURE = 1
		}

		// Token: 0x02000A8D RID: 2701
		internal sealed class ClientAnchor : Escher.BlockHeader
		{
			// Token: 0x0600678F RID: 26511 RVA: 0x00183198 File Offset: 0x00181398
			internal ClientAnchor(Escher.ClientAnchor.SPRC clientAnchorInfo) : base(0, 0U, Escher.RecordType.MSOFBTCLIENTANCHOR, 18U)
			{
				this.m_sprc = clientAnchorInfo;
			}

			// Token: 0x06006790 RID: 26512 RVA: 0x001831B0 File Offset: 0x001813B0
			internal override byte[] GetData()
			{
				byte[] array = new byte[26];
				int num = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.wFlags);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_colL);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_dxL);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_rwT);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_dyT);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_colR);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_dxR);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_rwB);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_sprc.m_orc.m_dyB);
				array2.CopyTo(array, num);
				return array;
			}

			// Token: 0x17002212 RID: 8722
			// (get) Token: 0x06006791 RID: 26513 RVA: 0x00183316 File Offset: 0x00181516
			internal override uint Length
			{
				get
				{
					return 18U;
				}
			}

			// Token: 0x04001A6C RID: 6764
			private const uint RecordLength = 18U;

			// Token: 0x04001A6D RID: 6765
			private Escher.ClientAnchor.SPRC m_sprc;

			// Token: 0x02000A8E RID: 2702
			internal sealed class SPRC
			{
				// Token: 0x06006792 RID: 26514 RVA: 0x0018331C File Offset: 0x0018151C
				internal SPRC(ushort leftAnchorCol, short leftAnchorValue, ushort topAnchorRow, short topAnchorValue, ushort rightAnchorCol, short rightAnchorValue, ushort bottomAnchorRow, short bottomAnchorValue)
				{
					this.m_orc = new Escher.ClientAnchor.SPRC.ORC();
					this.m_orc.m_colL = leftAnchorCol;
					this.m_orc.m_dxL = leftAnchorValue;
					this.m_orc.m_rwT = topAnchorRow;
					this.m_orc.m_dyT = topAnchorValue;
					this.m_orc.m_colR = rightAnchorCol;
					this.m_orc.m_dxR = rightAnchorValue;
					this.m_orc.m_rwB = bottomAnchorRow;
					this.m_orc.m_dyB = bottomAnchorValue;
				}

				// Token: 0x04001A6E RID: 6766
				internal Escher.ClientAnchor.SPRC.ORC m_orc;

				// Token: 0x04001A6F RID: 6767
				internal ushort wFlags;

				// Token: 0x02000A8F RID: 2703
				internal sealed class ORC
				{
					// Token: 0x04001A70 RID: 6768
					internal ushort m_colL;

					// Token: 0x04001A71 RID: 6769
					internal ushort m_colR;

					// Token: 0x04001A72 RID: 6770
					internal short m_dxL;

					// Token: 0x04001A73 RID: 6771
					internal short m_dxR;

					// Token: 0x04001A74 RID: 6772
					internal short m_dyB;

					// Token: 0x04001A75 RID: 6773
					internal short m_dyT;

					// Token: 0x04001A76 RID: 6774
					internal ushort m_rwB;

					// Token: 0x04001A77 RID: 6775
					internal ushort m_rwT;
				}
			}
		}

		// Token: 0x02000A90 RID: 2704
		internal sealed class ClientData : Escher.BlockHeader
		{
			// Token: 0x06006794 RID: 26516 RVA: 0x001833A7 File Offset: 0x001815A7
			internal ClientData() : base(0, 0U, Escher.RecordType.MSOFBTCLIENTDATA, 0U)
			{
			}

			// Token: 0x06006795 RID: 26517 RVA: 0x001833B7 File Offset: 0x001815B7
			internal override byte[] GetData()
			{
				return base.GetData();
			}
		}

		// Token: 0x02000A91 RID: 2705
		internal sealed class Drawing : Escher.BlockHeader
		{
			// Token: 0x06006796 RID: 26518 RVA: 0x001833BF File Offset: 0x001815BF
			internal Drawing(ushort drawingID) : base(0, (uint)drawingID, Escher.RecordType.MSOFBTDG, 8U)
			{
			}

			// Token: 0x06006797 RID: 26519 RVA: 0x001833D0 File Offset: 0x001815D0
			internal override byte[] GetData()
			{
				byte[] array = new byte[16];
				int num = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_csp);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_spidCur);
				array2.CopyTo(array, num);
				return array;
			}

			// Token: 0x17002213 RID: 8723
			// (set) Token: 0x06006798 RID: 26520 RVA: 0x0018342B File Offset: 0x0018162B
			internal uint LastSPID
			{
				set
				{
					this.m_spidCur = value;
				}
			}

			// Token: 0x17002214 RID: 8724
			// (set) Token: 0x06006799 RID: 26521 RVA: 0x00183434 File Offset: 0x00181634
			internal uint ShapeCount
			{
				set
				{
					this.m_csp = value;
				}
			}

			// Token: 0x04001A78 RID: 6776
			private uint m_csp;

			// Token: 0x04001A79 RID: 6777
			private uint m_spidCur;
		}

		// Token: 0x02000A92 RID: 2706
		internal sealed class DrawingContainer : Escher.BlockHeader
		{
			// Token: 0x0600679A RID: 26522 RVA: 0x0018343D File Offset: 0x0018163D
			internal DrawingContainer(ushort drawingID) : base(15, 0U, Escher.RecordType.MSOFBTDGCONTAINER, 0U)
			{
				this.m_drawing = new Escher.Drawing(drawingID);
				this.m_shapeGroupContainer = new Escher.ShapeGroupContainer();
			}

			// Token: 0x0600679B RID: 26523 RVA: 0x00183468 File Offset: 0x00181668
			internal int AddShape(uint spid, string imageName, Escher.ClientAnchor.SPRC clientAnchorInfo, uint referenceIndex)
			{
				if (this.m_shapeContainer == null)
				{
					this.m_shapeContainer = new ArrayList();
				}
				if (this.m_shapeContainer.Count == 0)
				{
					uint spid2 = spid / 1024U * 1024U;
					this.m_shapeContainer.Add(new Escher.ShapeContainer(spid2, Escher.ShapeType.MSOSPTMIN, (Escher.ShapeFlag)5));
				}
				this.m_shapeContainer.Add(new Escher.ShapeContainer(spid, Escher.ShapeType.MSOSPTPICTUREFRAME, (Escher.ShapeFlag)2560, clientAnchorInfo, referenceIndex, imageName));
				this.m_drawing.LastSPID = spid;
				this.m_drawing.ShapeCount = (uint)this.m_shapeContainer.Count;
				return this.m_shapeContainer.Count;
			}

			// Token: 0x0600679C RID: 26524 RVA: 0x00183504 File Offset: 0x00181704
			internal int AddShape(uint spid, string imageName, Escher.ClientAnchor.SPRC clientAnchorInfo, uint referenceIndex, string hyperLinkName, BiffCell.HyperLink hyperLinkType)
			{
				if (this.m_shapeContainer == null)
				{
					this.m_shapeContainer = new ArrayList();
				}
				if (this.m_shapeContainer.Count == 0)
				{
					uint spid2 = spid / 1024U * 1024U;
					this.m_shapeContainer.Add(new Escher.ShapeContainer(spid2, Escher.ShapeType.MSOSPTMIN, (Escher.ShapeFlag)5));
				}
				this.m_shapeContainer.Add(new Escher.ShapeContainer(spid, Escher.ShapeType.MSOSPTPICTUREFRAME, (Escher.ShapeFlag)2560, clientAnchorInfo, referenceIndex, imageName, hyperLinkName, hyperLinkType));
				this.m_drawing.LastSPID = spid;
				this.m_drawing.ShapeCount = (uint)this.m_shapeContainer.Count;
				return this.m_shapeContainer.Count;
			}

			// Token: 0x0600679D RID: 26525 RVA: 0x001835A1 File Offset: 0x001817A1
			internal override byte[] GetData()
			{
				return null;
			}

			// Token: 0x0600679E RID: 26526 RVA: 0x001835A4 File Offset: 0x001817A4
			public void WriteToStream(Stream stream)
			{
				if (this.m_shapeContainer != null)
				{
					MsoDrawing msoDrawing = new MsoDrawing();
					long position = stream.Position;
					msoDrawing.WriteMsoDrawingHeader(stream, 0);
					int num = 0;
					long position2 = stream.Position;
					byte[] data = base.GetData();
					stream.Write(data, 0, data.Length);
					num += data.Length;
					data = this.m_drawing.GetData();
					stream.Write(data, 0, data.Length);
					num += data.Length;
					long position3 = stream.Position + 4L;
					data = this.m_shapeGroupContainer.GetData();
					stream.Write(data, 0, data.Length);
					num += data.Length;
					int num2 = 0;
					int num3 = num;
					ushort num4 = 1;
					for (int i = 0; i < this.m_shapeContainer.Count; i++)
					{
						data = ((Escher.ShapeContainer)this.m_shapeContainer[i]).GetData();
						if (i < 2)
						{
							num += data.Length;
						}
						else
						{
							num4 += 1;
							msoDrawing.WriteMsoDrawingHeader(stream, (ushort)data.Length);
						}
						stream.Write(data, 0, data.Length);
						num2 += data.Length;
						num3 += data.Length;
						if (i > 0)
						{
							byte[] data2 = new Obj(num4).GetData();
							stream.Write(data2, 0, data2.Length);
						}
					}
					long position4 = stream.Position;
					uint value = (uint)(num3 - 8);
					stream.Position = position2 + 4L;
					byte[] bytes = BitConverter.GetBytes(value);
					stream.Write(bytes, 0, bytes.Length);
					stream.Position = position3;
					bytes = BitConverter.GetBytes((uint)num2);
					stream.Write(bytes, 0, bytes.Length);
					bytes = BitConverter.GetBytes(num);
					stream.Position = position + 2L;
					stream.Write(bytes, 0, bytes.Length);
					stream.Position = position4;
				}
			}

			// Token: 0x04001A7A RID: 6778
			private Escher.Drawing m_drawing;

			// Token: 0x04001A7B RID: 6779
			private ArrayList m_shapeContainer;

			// Token: 0x04001A7C RID: 6780
			private Escher.ShapeGroupContainer m_shapeGroupContainer;
		}

		// Token: 0x02000A93 RID: 2707
		internal sealed class DrawingGroup : Escher.BlockHeader
		{
			// Token: 0x0600679F RID: 26527 RVA: 0x00183758 File Offset: 0x00181958
			internal DrawingGroup() : base(0, 0U, Escher.RecordType.MSOFBTDGG, 0U)
			{
			}

			// Token: 0x060067A0 RID: 26528 RVA: 0x00183768 File Offset: 0x00181968
			internal void AddCluster(int dgID)
			{
				if (this.m_dgCluster == null)
				{
					this.m_dgCluster = new ArrayList();
				}
				Escher.FIDCL value = new Escher.FIDCL((uint)dgID, 1U);
				if (dgID > this.m_dgCluster.Count)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(value);
					this.m_dgCluster.Add(arrayList);
				}
				else
				{
					ArrayList arrayList2 = (ArrayList)this.m_dgCluster[dgID - 1];
					arrayList2.Add(value);
				}
				this.m_cdgSaved += 1U;
			}

			// Token: 0x060067A1 RID: 26529 RVA: 0x001837E8 File Offset: 0x001819E8
			internal uint GetCurrentSpid(int dgID)
			{
				if (this.m_dgCluster == null || dgID < 1 || dgID > this.m_dgCluster.Count)
				{
					return 0U;
				}
				ArrayList arrayList = (ArrayList)this.m_dgCluster[dgID - 1];
				return ((Escher.FIDCL)arrayList[arrayList.Count - 1]).m_cspidCurr;
			}

			// Token: 0x060067A2 RID: 26530 RVA: 0x00183840 File Offset: 0x00181A40
			internal override byte[] GetData()
			{
				byte[] array = new byte[this.Length + 8U];
				base.Length = this.Length;
				int num = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num);
				num += array2.Length;
				ArrayList arrayList = (ArrayList)this.m_dgCluster[this.m_dgCluster.Count - 1];
				uint value = this.m_cdgSaved * 1024U + ((Escher.FIDCL)arrayList[arrayList.Count - 1]).m_cspidCurr;
				array2 = BitConverter.GetBytes(value);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_cdgSaved + 1U);
				array2.CopyTo(array, num);
				num += array2.Length;
				uint num2 = 0U;
				int num3 = 0;
				byte[] array3 = new byte[this.m_cdgSaved * 8U];
				for (int i = 0; i < this.m_dgCluster.Count; i++)
				{
					ArrayList arrayList2 = (ArrayList)this.m_dgCluster[i];
					for (int j = 0; j < arrayList2.Count; j++)
					{
						array2 = BitConverter.GetBytes(((Escher.FIDCL)arrayList2[j]).m_dgid);
						array2.CopyTo(array3, num3);
						num3 += array2.Length;
						num2 += ((Escher.FIDCL)arrayList2[j]).m_cspidCurr;
						array2 = BitConverter.GetBytes(((Escher.FIDCL)arrayList2[j]).m_cspidCurr);
						array2.CopyTo(array3, num3);
						num3 += array2.Length;
					}
				}
				array2 = BitConverter.GetBytes(num2);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_dgCluster.Count);
				array2.CopyTo(array, num);
				num += array2.Length;
				array3.CopyTo(array, num);
				return array;
			}

			// Token: 0x060067A3 RID: 26531 RVA: 0x00183A03 File Offset: 0x00181C03
			internal int GetStartingSPID(int dgID)
			{
				if (this.m_dgCluster == null || dgID < 1)
				{
					return 0;
				}
				if (dgID == 1)
				{
					return 1024;
				}
				return (int)(this.m_cdgSaved * 1024U);
			}

			// Token: 0x060067A4 RID: 26532 RVA: 0x00183A2C File Offset: 0x00181C2C
			internal void IncrementShapeCount(int dgID)
			{
				if (this.m_dgCluster != null && dgID >= 1 && dgID <= this.m_dgCluster.Count)
				{
					if (this.GetCurrentSpid(dgID) % 1024U == 0U)
					{
						this.AddCluster(dgID);
						return;
					}
					ArrayList arrayList = (ArrayList)this.m_dgCluster[dgID - 1];
					Escher.FIDCL fidcl = (Escher.FIDCL)arrayList[arrayList.Count - 1];
					fidcl.m_cspidCurr += 1U;
				}
			}

			// Token: 0x17002215 RID: 8725
			// (get) Token: 0x060067A5 RID: 26533 RVA: 0x00183AA0 File Offset: 0x00181CA0
			internal override uint Length
			{
				get
				{
					return 16U + this.m_cdgSaved * 8U;
				}
			}

			// Token: 0x04001A7D RID: 6781
			private const uint FixedRecordLength = 16U;

			// Token: 0x04001A7E RID: 6782
			private uint m_cdgSaved;

			// Token: 0x04001A7F RID: 6783
			private ArrayList m_dgCluster;
		}

		// Token: 0x02000A94 RID: 2708
		internal sealed class DrawingGroupContainer : Escher.BlockHeader
		{
			// Token: 0x060067A6 RID: 26534 RVA: 0x00183AAD File Offset: 0x00181CAD
			internal DrawingGroupContainer() : base(15, 0U, Escher.RecordType.MSOFBTDGGCONTAINER, 0U)
			{
				this.m_drawingGroup = new Escher.DrawingGroup();
			}

			// Token: 0x060067A7 RID: 26535 RVA: 0x00183ACC File Offset: 0x00181CCC
			private int AddImage(byte[] checkSum, int streamIndex, int imageLength, Escher.RecordType recordType, Escher.BlipType blipType, Escher.BlipSignature blipSignature, int workSheetId)
			{
				if (this.m_clusters.ContainsKey(workSheetId))
				{
					int dgID = (int)this.m_clusters[workSheetId];
					this.m_drawingGroup.IncrementShapeCount(dgID);
				}
				else
				{
					this.m_clusters.Add(workSheetId, this.m_clusters.Count + 1);
					int dgID = this.m_clusters.Count;
					this.m_drawingGroup.AddCluster(dgID);
					this.m_drawingGroup.IncrementShapeCount(dgID);
				}
				return this.m_bStoreContainer.AddImage(checkSum, streamIndex, imageLength, recordType, blipType, blipSignature);
			}

			// Token: 0x060067A8 RID: 26536 RVA: 0x00183B74 File Offset: 0x00181D74
			internal int AddImage(byte[] imageData, string imageName, Escher.RecordType recordType, Escher.BlipType blipType, Escher.BlipSignature blipSignature, int workSheetId, out int startSPID, out int dgID)
			{
				if (this.m_clusters == null)
				{
					this.m_clusters = new Hashtable();
				}
				if (this.m_bStoreContainer == null)
				{
					this.m_bStoreContainer = new Escher.BlipStoreContainer();
				}
				if (this.m_imageTable == null)
				{
					this.m_imageTable = new Hashtable();
				}
				int num = imageData.Length;
				Escher.DrawingGroupContainer.CheckSumImage checkSumImage;
				if (this.m_imageTable.ContainsKey(imageName))
				{
					checkSumImage = (Escher.DrawingGroupContainer.CheckSumImage)this.m_imageTable[imageName];
				}
				else
				{
					byte[] checkSum = Escher.CheckSum(imageData);
					int streamPosFromCheckSum = this.m_bStoreContainer.GetStreamPosFromCheckSum(checkSum);
					if (streamPosFromCheckSum == -1)
					{
						Stream stream = this.CreateImageStream(imageName, out streamPosFromCheckSum);
						if (stream != null)
						{
							int num2 = 0;
							if (recordType == Escher.RecordType.MSOFBTBLIP_DIB)
							{
								num2 = 14;
								num -= 14;
							}
							stream.Write(imageData, num2, imageData.Length - num2);
						}
					}
					checkSumImage = new Escher.DrawingGroupContainer.CheckSumImage(checkSum, streamPosFromCheckSum);
					this.m_imageTable.Add(imageName, checkSumImage);
				}
				int result = this.AddImage(checkSumImage.CheckSum, checkSumImage.StreamIndex, num, recordType, blipType, blipSignature, workSheetId);
				dgID = (int)this.m_clusters[workSheetId];
				startSPID = this.m_drawingGroup.GetStartingSPID(dgID);
				return result;
			}

			// Token: 0x060067A9 RID: 26537 RVA: 0x00183C8C File Offset: 0x00181E8C
			internal Stream CreateImageStream(string name, out int streamIndex)
			{
				if (this.m_imageStream != null)
				{
					for (int i = 0; i < this.m_imageStream.Count; i++)
					{
						if (((ExcelStream)this.m_imageStream[i]).Name.Equals(name))
						{
							streamIndex = i;
							return null;
						}
					}
				}
				ExcelStream excelStream = new ExcelStream(name);
				if (this.m_imageStream == null)
				{
					this.m_imageStream = new ArrayList();
				}
				this.m_imageStream.Add(excelStream);
				streamIndex = this.m_imageStream.Count - 1;
				return excelStream.ServerStream;
			}

			// Token: 0x17002216 RID: 8726
			// (get) Token: 0x060067AA RID: 26538 RVA: 0x00183D16 File Offset: 0x00181F16
			internal ArrayList BlipList
			{
				get
				{
					if (this.m_bStoreContainer == null)
					{
						return null;
					}
					return this.m_bStoreContainer.BlipList;
				}
			}

			// Token: 0x17002217 RID: 8727
			// (get) Token: 0x060067AB RID: 26539 RVA: 0x00183D2D File Offset: 0x00181F2D
			internal Hashtable BSEList
			{
				get
				{
					if (this.m_bStoreContainer == null)
					{
						return null;
					}
					return this.m_bStoreContainer.BSEList;
				}
			}

			// Token: 0x17002218 RID: 8728
			// (get) Token: 0x060067AC RID: 26540 RVA: 0x00183D44 File Offset: 0x00181F44
			internal byte[] BStoreContainerData
			{
				get
				{
					if (this.m_bStoreContainer == null)
					{
						return null;
					}
					return this.m_bStoreContainer.GetData();
				}
			}

			// Token: 0x17002219 RID: 8729
			// (get) Token: 0x060067AD RID: 26541 RVA: 0x00183D5B File Offset: 0x00181F5B
			internal byte[] DrawingGroupContainerData
			{
				get
				{
					base.Length = this.Length;
					return base.GetData();
				}
			}

			// Token: 0x1700221A RID: 8730
			// (get) Token: 0x060067AE RID: 26542 RVA: 0x00183D6F File Offset: 0x00181F6F
			internal byte[] DrawingGroupData
			{
				get
				{
					if (this.m_drawingGroup == null)
					{
						return null;
					}
					return this.m_drawingGroup.GetData();
				}
			}

			// Token: 0x1700221B RID: 8731
			// (get) Token: 0x060067AF RID: 26543 RVA: 0x00183D86 File Offset: 0x00181F86
			internal override uint Length
			{
				get
				{
					return 50U + this.m_bStoreContainer.Length + this.m_drawingGroup.Length + 8U;
				}
			}

			// Token: 0x1700221C RID: 8732
			// (get) Token: 0x060067B0 RID: 26544 RVA: 0x00183DA4 File Offset: 0x00181FA4
			internal byte[] ShapePropertyData
			{
				get
				{
					return Escher.ShapeProperty.GetData();
				}
			}

			// Token: 0x1700221D RID: 8733
			// (get) Token: 0x060067B1 RID: 26545 RVA: 0x00183DAB File Offset: 0x00181FAB
			internal ArrayList StreamList
			{
				get
				{
					return this.m_imageStream;
				}
			}

			// Token: 0x04001A80 RID: 6784
			private const int BitmapFileHeaderSize = 14;

			// Token: 0x04001A81 RID: 6785
			private Escher.BlipStoreContainer m_bStoreContainer;

			// Token: 0x04001A82 RID: 6786
			private Hashtable m_clusters;

			// Token: 0x04001A83 RID: 6787
			private Escher.DrawingGroup m_drawingGroup;

			// Token: 0x04001A84 RID: 6788
			private ArrayList m_imageStream;

			// Token: 0x04001A85 RID: 6789
			private Hashtable m_imageTable;

			// Token: 0x02000A95 RID: 2709
			internal sealed class CheckSumImage
			{
				// Token: 0x060067B2 RID: 26546 RVA: 0x00183DB3 File Offset: 0x00181FB3
				internal CheckSumImage(byte[] checkSum, int streamIndex)
				{
					this.m_checkSum = checkSum;
					this.m_streamIndex = streamIndex;
				}

				// Token: 0x1700221E RID: 8734
				// (get) Token: 0x060067B3 RID: 26547 RVA: 0x00183DC9 File Offset: 0x00181FC9
				internal byte[] CheckSum
				{
					get
					{
						return this.m_checkSum;
					}
				}

				// Token: 0x1700221F RID: 8735
				// (get) Token: 0x060067B4 RID: 26548 RVA: 0x00183DD1 File Offset: 0x00181FD1
				internal int StreamIndex
				{
					get
					{
						return this.m_streamIndex;
					}
				}

				// Token: 0x04001A86 RID: 6790
				private byte[] m_checkSum;

				// Token: 0x04001A87 RID: 6791
				private int m_streamIndex;
			}
		}

		// Token: 0x02000A96 RID: 2710
		internal sealed class DrawingOpt : Escher.BlockHeader
		{
			// Token: 0x060067B5 RID: 26549 RVA: 0x00183DD9 File Offset: 0x00181FD9
			internal DrawingOpt(string imageName, uint refIndex) : base(3, 2U, Escher.RecordType.MSOFBTOPT, 0U)
			{
				this.m_imageName = imageName;
				this.m_referenceIndex = refIndex;
			}

			// Token: 0x060067B6 RID: 26550 RVA: 0x00183DF7 File Offset: 0x00181FF7
			internal DrawingOpt(string imageName, uint refIndex, string hyperLinkName, BiffCell.HyperLink hyperLinkType) : base(3, 5U, Escher.RecordType.MSOFBTOPT, 0U)
			{
				this.m_imageName = imageName;
				this.m_referenceIndex = refIndex;
				this.m_hyperLinkName = hyperLinkName;
				this.m_hyperLinkType = hyperLinkType;
			}

			// Token: 0x060067B7 RID: 26551 RVA: 0x00183E38 File Offset: 0x00182038
			internal override byte[] GetData()
			{
				int num = 14 + this.m_imageName.Length * 2;
				if (this.m_hyperLinkName != null && this.m_hyperLinkName.Length > 0)
				{
					if (this.m_hyperLinkType == BiffCell.HyperLink.BOOKMARK)
					{
						num += this.m_hyperLinkName.Length * 2 + 48;
					}
					else
					{
						num += this.m_hyperLinkName.Length * 2 + 64;
					}
				}
				base.Length = (uint)num;
				byte[] array = new byte[num + 8];
				int num2 = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num2);
				num2 += array2.Length;
				array2 = BitConverter.GetBytes(16644);
				array2.CopyTo(array, num2);
				num2 += array2.Length;
				array2 = BitConverter.GetBytes(this.m_referenceIndex);
				array2.CopyTo(array, num2);
				num2 += array2.Length;
				array2 = BitConverter.GetBytes(49413);
				array2.CopyTo(array, num2);
				num2 += array2.Length;
				uint value = (uint)(this.m_imageName.Length * 2 + 2);
				array2 = BitConverter.GetBytes(value);
				array2.CopyTo(array, num2);
				num2 += array2.Length;
				if (this.m_hyperLinkName != null && this.m_hyperLinkName.Length > 0)
				{
					byte[] array3 = new byte[]
					{
						191,
						1,
						1,
						0,
						1,
						0
					};
					byte[] array4 = new byte[]
					{
						191,
						3,
						8,
						0,
						8,
						0
					};
					array3.CopyTo(array, num2);
					num2 += 6;
					array2 = BitConverter.GetBytes(50050);
					array2.CopyTo(array, num2);
					num2 += array2.Length;
					uint value2;
					if (this.m_hyperLinkType == BiffCell.HyperLink.BOOKMARK)
					{
						value2 = (uint)(28 + (this.m_hyperLinkName.Length + 1) * 2);
					}
					else
					{
						value2 = (uint)(44 + (this.m_hyperLinkName.Length + 1) * 2);
					}
					array2 = BitConverter.GetBytes(value2);
					array2.CopyTo(array, num2);
					num2 += array2.Length;
					array4.CopyTo(array, num2);
					num2 += 6;
				}
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				array2 = unicodeEncoding.GetBytes(this.m_imageName);
				array2.CopyTo(array, num2);
				num2 += array2.Length + 2;
				if (this.m_hyperLinkName != null && this.m_hyperLinkName.Length > 0)
				{
					Guid guid = new Guid("79EAC9D0-BAF9-11CE-8C82-00AA004BA90B");
					array2 = guid.ToByteArray();
					array2.CopyTo(array, num2);
					num2 += array2.Length;
					uint value3;
					if (this.m_hyperLinkType == BiffCell.HyperLink.BOOKMARK)
					{
						value3 = (uint)(this.m_hyperLinkName.Length + 1);
						uint value4 = 2U;
						uint value5 = 8U;
						array2 = BitConverter.GetBytes(value4);
						array2.CopyTo(array, num2);
						num2 += array2.Length;
						array2 = BitConverter.GetBytes(value5);
						array2.CopyTo(array, num2);
						num2 += array2.Length;
					}
					else
					{
						value3 = (uint)((this.m_hyperLinkName.Length + 1) * 2);
						uint value6 = 2U;
						uint value7 = 3U;
						array2 = BitConverter.GetBytes(value6);
						array2.CopyTo(array, num2);
						num2 += array2.Length;
						array2 = BitConverter.GetBytes(value7);
						array2.CopyTo(array, num2);
						num2 += array2.Length;
						Guid guid2 = new Guid("79EAC9E0-BAF9-11CE-8C82-00AA004BA90B");
						array2 = guid2.ToByteArray();
						array2.CopyTo(array, num2);
						num2 += array2.Length;
					}
					array2 = BitConverter.GetBytes(value3);
					array2.CopyTo(array, num2);
					num2 += array2.Length;
					array2 = unicodeEncoding.GetBytes(this.m_hyperLinkName);
					array2.CopyTo(array, num2);
				}
				return array;
			}

			// Token: 0x04001A88 RID: 6792
			private const ushort BookMarkLength = 48;

			// Token: 0x04001A89 RID: 6793
			private const ushort HyperlinkLength = 64;

			// Token: 0x04001A8A RID: 6794
			private const ushort PropertyIDImageName = 49413;

			// Token: 0x04001A8B RID: 6795
			private const ushort PropertyIDShapeCount = 16644;

			// Token: 0x04001A8C RID: 6796
			private const ushort PropertyPihlShape = 50050;

			// Token: 0x04001A8D RID: 6797
			private const int RecordLength = 14;

			// Token: 0x04001A8E RID: 6798
			private string m_hyperLinkName;

			// Token: 0x04001A8F RID: 6799
			private BiffCell.HyperLink m_hyperLinkType;

			// Token: 0x04001A90 RID: 6800
			private string m_imageName;

			// Token: 0x04001A91 RID: 6801
			private uint m_referenceIndex;
		}

		// Token: 0x02000A97 RID: 2711
		internal sealed class FIDCL
		{
			// Token: 0x060067B8 RID: 26552 RVA: 0x0018413C File Offset: 0x0018233C
			internal FIDCL(uint dgid, uint cspidCurr)
			{
				this.m_dgid = dgid;
				this.m_cspidCurr = cspidCurr;
			}

			// Token: 0x04001A92 RID: 6802
			internal uint m_cspidCurr;

			// Token: 0x04001A93 RID: 6803
			internal uint m_dgid;
		}

		// Token: 0x02000A98 RID: 2712
		internal enum RecordType
		{
			// Token: 0x04001A95 RID: 6805
			MSOFBTBLIP = 61464,
			// Token: 0x04001A96 RID: 6806
			MSOFBTBLIP_DIB = 61471,
			// Token: 0x04001A97 RID: 6807
			MSOFBTBLIP_GIF = 61470,
			// Token: 0x04001A98 RID: 6808
			MSOFBTBLIP_JPEG = 61469,
			// Token: 0x04001A99 RID: 6809
			MSOFBTBLIP_PNG,
			// Token: 0x04001A9A RID: 6810
			MSOFBTBSE = 61447,
			// Token: 0x04001A9B RID: 6811
			MSOFBTBSTORECONTAINER = 61441,
			// Token: 0x04001A9C RID: 6812
			MSOFBTCLIENTANCHOR = 61456,
			// Token: 0x04001A9D RID: 6813
			MSOFBTCLIENTDATA,
			// Token: 0x04001A9E RID: 6814
			MSOFBTCLSID = 61462,
			// Token: 0x04001A9F RID: 6815
			MSOFBTDG = 61448,
			// Token: 0x04001AA0 RID: 6816
			MSOFBTDGCONTAINER = 61442,
			// Token: 0x04001AA1 RID: 6817
			MSOFBTDGG = 61446,
			// Token: 0x04001AA2 RID: 6818
			MSOFBTDGGCONTAINER = 61440,
			// Token: 0x04001AA3 RID: 6819
			MSOFBTOPT = 61451,
			// Token: 0x04001AA4 RID: 6820
			MSOFBTSP = 61450,
			// Token: 0x04001AA5 RID: 6821
			MSOFBTSPCONTAINER = 61444,
			// Token: 0x04001AA6 RID: 6822
			MSOFBTSPGR = 61449,
			// Token: 0x04001AA7 RID: 6823
			MSOFBTSPGRCONTAINER = 61443,
			// Token: 0x04001AA8 RID: 6824
			MSOFBTUNKNOWN = 0
		}

		// Token: 0x02000A99 RID: 2713
		internal sealed class Shape : Escher.BlockHeader
		{
			// Token: 0x060067B9 RID: 26553 RVA: 0x00184152 File Offset: 0x00182352
			internal Shape(Escher.ShapeType shapeType, Escher.ShapeFlag shapeFlags, uint spid) : base(2, (uint)shapeType, Escher.RecordType.MSOFBTSP, 8U)
			{
				this.m_spid = spid;
				this.m_shapeFlag = shapeFlags;
			}

			// Token: 0x060067BA RID: 26554 RVA: 0x00184170 File Offset: 0x00182370
			internal override byte[] GetData()
			{
				byte[] array = new byte[16];
				int num = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_spid);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes((uint)this.m_shapeFlag);
				array2.CopyTo(array, num);
				return array;
			}

			// Token: 0x17002220 RID: 8736
			// (get) Token: 0x060067BB RID: 26555 RVA: 0x001841CB File Offset: 0x001823CB
			internal override uint Length
			{
				get
				{
					return 8U;
				}
			}

			// Token: 0x04001AA9 RID: 6825
			private Escher.ShapeFlag m_shapeFlag;

			// Token: 0x04001AAA RID: 6826
			private uint m_spid;
		}

		// Token: 0x02000A9A RID: 2714
		internal sealed class ShapeContainer : Escher.BlockHeader
		{
			// Token: 0x060067BC RID: 26556 RVA: 0x001841CE File Offset: 0x001823CE
			internal ShapeContainer(uint spid, Escher.ShapeType shapeType, Escher.ShapeFlag shapeFlags) : base(15, 0U, Escher.RecordType.MSOFBTSPCONTAINER, 0U)
			{
				this.m_shapeGroup = new Escher.ShapeGroup(0U, 0U, 0U, 0U);
				this.m_shape = new Escher.Shape(shapeType, shapeFlags, spid);
			}

			// Token: 0x060067BD RID: 26557 RVA: 0x001841FC File Offset: 0x001823FC
			internal ShapeContainer(uint spid, Escher.ShapeType shapeType, Escher.ShapeFlag shapeFlags, Escher.ClientAnchor.SPRC clientAnchorInfo, uint refIndex, string imageName) : base(15, 0U, Escher.RecordType.MSOFBTSPCONTAINER, 0U)
			{
				this.m_shape = new Escher.Shape(shapeType, shapeFlags, spid);
				this.m_drawingOpt = new Escher.DrawingOpt(imageName, refIndex);
				this.m_clientAnchor = new Escher.ClientAnchor(clientAnchorInfo);
				this.m_clientData = new Escher.ClientData();
			}

			// Token: 0x060067BE RID: 26558 RVA: 0x00184250 File Offset: 0x00182450
			internal ShapeContainer(uint spid, Escher.ShapeType shapeType, Escher.ShapeFlag shapeFlags, Escher.ClientAnchor.SPRC clientAnchorInfo, uint refIndex, string imageName, string hyperLinkName, BiffCell.HyperLink hyperLinkType) : base(15, 0U, Escher.RecordType.MSOFBTSPCONTAINER, 0U)
			{
				this.m_shape = new Escher.Shape(shapeType, shapeFlags, spid);
				this.m_drawingOpt = new Escher.DrawingOpt(imageName, refIndex, hyperLinkName, hyperLinkType);
				this.m_clientAnchor = new Escher.ClientAnchor(clientAnchorInfo);
				this.m_clientData = new Escher.ClientData();
			}

			// Token: 0x060067BF RID: 26559 RVA: 0x001842A8 File Offset: 0x001824A8
			internal override byte[] GetData()
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] data = base.GetData();
				memoryStream.Write(data, 0, data.Length);
				if (this.m_shapeGroup != null)
				{
					data = this.m_shapeGroup.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_shape.GetData();
					memoryStream.Write(data, 0, data.Length);
				}
				else
				{
					data = this.m_shape.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_drawingOpt.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_clientAnchor.GetData();
					memoryStream.Write(data, 0, data.Length);
					data = this.m_clientData.GetData();
					memoryStream.Write(data, 0, data.Length);
				}
				uint value = (uint)(memoryStream.Length - 8L);
				byte[] bytes = BitConverter.GetBytes(value);
				bytes.CopyTo(memoryStream.GetBuffer(), 4L);
				memoryStream.Position = 0L;
				return memoryStream.ToArray();
			}

			// Token: 0x04001AAB RID: 6827
			private Escher.ClientAnchor m_clientAnchor;

			// Token: 0x04001AAC RID: 6828
			private Escher.ClientData m_clientData;

			// Token: 0x04001AAD RID: 6829
			private Escher.DrawingOpt m_drawingOpt;

			// Token: 0x04001AAE RID: 6830
			private Escher.Shape m_shape;

			// Token: 0x04001AAF RID: 6831
			private Escher.ShapeGroup m_shapeGroup;
		}

		// Token: 0x02000A9B RID: 2715
		internal enum ShapeFlag
		{
			// Token: 0x04001AB1 RID: 6833
			BACKGROUND = 1024,
			// Token: 0x04001AB2 RID: 6834
			CHILD = 2,
			// Token: 0x04001AB3 RID: 6835
			CONNECTOR = 256,
			// Token: 0x04001AB4 RID: 6836
			DELETED = 8,
			// Token: 0x04001AB5 RID: 6837
			FLIPH = 64,
			// Token: 0x04001AB6 RID: 6838
			FLIPV = 128,
			// Token: 0x04001AB7 RID: 6839
			GROUP = 1,
			// Token: 0x04001AB8 RID: 6840
			HAVEANCHOR = 512,
			// Token: 0x04001AB9 RID: 6841
			HAVEMASTER = 32,
			// Token: 0x04001ABA RID: 6842
			HAVESPT = 2048,
			// Token: 0x04001ABB RID: 6843
			NONE = 0,
			// Token: 0x04001ABC RID: 6844
			OLESHAPE = 16,
			// Token: 0x04001ABD RID: 6845
			PATRIARCH = 4
		}

		// Token: 0x02000A9C RID: 2716
		internal sealed class ShapeGroup : Escher.BlockHeader
		{
			// Token: 0x060067C0 RID: 26560 RVA: 0x0018438F File Offset: 0x0018258F
			public ShapeGroup(uint left, uint right, uint top, uint bottom) : base(1, 0U, Escher.RecordType.MSOFBTSPGR, 16U)
			{
				this.m_left = left;
				this.m_top = top;
				this.m_right = right;
				this.m_bottom = bottom;
			}

			// Token: 0x060067C1 RID: 26561 RVA: 0x001843C0 File Offset: 0x001825C0
			internal override byte[] GetData()
			{
				byte[] array = new byte[24];
				int num = 0;
				byte[] array2 = base.GetData();
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_left);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_top);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_right);
				array2.CopyTo(array, num);
				num += array2.Length;
				array2 = BitConverter.GetBytes(this.m_bottom);
				array2.CopyTo(array, num);
				return array;
			}

			// Token: 0x17002221 RID: 8737
			// (get) Token: 0x060067C2 RID: 26562 RVA: 0x0018444F File Offset: 0x0018264F
			internal override uint Length
			{
				get
				{
					return 16U;
				}
			}

			// Token: 0x04001ABE RID: 6846
			private uint m_bottom;

			// Token: 0x04001ABF RID: 6847
			private uint m_left;

			// Token: 0x04001AC0 RID: 6848
			private uint m_right;

			// Token: 0x04001AC1 RID: 6849
			private uint m_top;
		}

		// Token: 0x02000A9D RID: 2717
		internal sealed class ShapeGroupContainer : Escher.BlockHeader
		{
			// Token: 0x060067C3 RID: 26563 RVA: 0x00184453 File Offset: 0x00182653
			internal ShapeGroupContainer() : base(15, 0U, Escher.RecordType.MSOFBTSPGRCONTAINER, 0U)
			{
			}

			// Token: 0x060067C4 RID: 26564 RVA: 0x00184464 File Offset: 0x00182664
			internal override byte[] GetData()
			{
				byte[] array = new byte[8];
				int index = 0;
				byte[] data = base.GetData();
				data.CopyTo(array, index);
				return array;
			}
		}

		// Token: 0x02000A9E RID: 2718
		internal sealed class ShapeProperty
		{
			// Token: 0x060067C5 RID: 26565 RVA: 0x001844C2 File Offset: 0x001826C2
			internal static byte[] GetData()
			{
				return new byte[]
				{
					51,
					0,
					11,
					240,
					18,
					0,
					0,
					0,
					191,
					0,
					8,
					0,
					8,
					0,
					129,
					1,
					65,
					0,
					0,
					8,
					192,
					1,
					64,
					0,
					0,
					8,
					64,
					0,
					30,
					241,
					16,
					0,
					0,
					0,
					13,
					0,
					0,
					8,
					12,
					0,
					0,
					8,
					23,
					0,
					0,
					8,
					247,
					0,
					0,
					16
				};
			}
		}

		// Token: 0x02000A9F RID: 2719
		internal enum ShapeType
		{
			// Token: 0x04001AC3 RID: 6851
			MSOSPTMIN,
			// Token: 0x04001AC4 RID: 6852
			MSOSPTPICTUREFRAME = 75
		}
	}
}

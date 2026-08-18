using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ABD RID: 2749
	internal sealed class MsoDrawingGroup : BaseBiffRecord, IRecord
	{
		// Token: 0x06006831 RID: 26673 RVA: 0x00185E7A File Offset: 0x0018407A
		internal MsoDrawingGroup(Escher.DrawingGroupContainer drawingGroupContainer) : base(235)
		{
			this.m_drawingGroupContainer = drawingGroupContainer;
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x00185E90 File Offset: 0x00184090
		internal byte[] FillRecordWithBlips(ArrayList blipList, Hashtable bseList, Stream stream, ref int currentBlip, ref int currentLength, ref int imageStreamIndex, ref Stream currentImageStream)
		{
			byte[] array = null;
			if (currentBlip >= blipList.Count)
			{
				return null;
			}
			do
			{
				byte[] checkSum = ((Escher.Blip)blipList[currentBlip]).CheckSum;
				string @string = Encoding.ASCII.GetString(checkSum);
				if (bseList.ContainsKey(@string))
				{
					byte[] array2 = new byte[69];
					((Escher.BlipStoreEntry)bseList[@string]).GetData().CopyTo(array2, 0);
					((Escher.Blip)blipList[currentBlip]).GetHeaderData().CopyTo(array2, 44);
					int num = currentLength + 69;
					currentImageStream = ((ExcelStream)this.m_drawingGroupContainer.StreamList[currentBlip]).ServerStream;
					currentImageStream.Seek(0L, SeekOrigin.Begin);
					if (currentImageStream.Length + (long)num > 8224L)
					{
						int num2 = 8224 - currentLength;
						if (num2 <= 69)
						{
							stream.Write(array2, 0, num2);
							if (num2 < 69)
							{
								array = new byte[69 - num2];
								Array.Copy(array2, num2, array, 0, array.Length);
							}
							currentLength += num2;
						}
						else
						{
							stream.Write(array2, 0, 69);
							imageStreamIndex = num2 - 69;
							byte[] array3 = new byte[imageStreamIndex];
							currentImageStream.Read(array3, 0, imageStreamIndex);
							stream.Write(array3, 0, array3.Length);
							currentLength += num2;
						}
					}
					else
					{
						byte[] array4 = new byte[currentImageStream.Length];
						stream.Write(array2, 0, array2.Length);
						currentImageStream.Read(array4, 0, (int)currentImageStream.Length);
						stream.Write(array4, 0, array4.Length);
						currentLength += (int)currentImageStream.Length + 69;
						currentBlip++;
						imageStreamIndex = 0;
						currentImageStream.Close();
						currentImageStream = null;
					}
				}
			}
			while (currentLength < 8224 && currentBlip < blipList.Count);
			return array;
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x00186065 File Offset: 0x00184265
		public byte[] GetData()
		{
			return null;
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x00186068 File Offset: 0x00184268
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void WriteShareProperties(Stream stream, ref int shapePropertiesIndex, int currentLength)
		{
			if (shapePropertiesIndex + 8224 - currentLength < 50)
			{
				stream.Write(this.m_drawingGroupContainer.ShapePropertyData, shapePropertiesIndex, 8224 - currentLength);
				shapePropertiesIndex += 8224 - currentLength;
				return;
			}
			stream.Write(this.m_drawingGroupContainer.ShapePropertyData, shapePropertiesIndex, 50 - shapePropertiesIndex);
			shapePropertiesIndex = 50;
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x001860C8 File Offset: 0x001842C8
		internal void WriteToStream(Stream stream)
		{
			if (this.m_drawingGroupContainer != null)
			{
				int num = 0;
				uint num2 = this.m_drawingGroupContainer.Length + 8U;
				uint num3 = 0U;
				if (num2 > 8224U)
				{
					base.Length = 8224;
					num = (int)(num2 / 8224U);
					num3 = num2 - (uint)(num * 8224);
				}
				else
				{
					base.Length = (ushort)num2;
				}
				byte[] array = base.GetBaseData();
				int num4 = array.Length;
				stream.Write(array, 0, array.Length);
				array = this.m_drawingGroupContainer.DrawingGroupContainerData;
				num4 = array.Length;
				stream.Write(array, 0, array.Length);
				array = this.m_drawingGroupContainer.DrawingGroupData;
				num4 += array.Length;
				stream.Write(array, 0, array.Length);
				array = this.m_drawingGroupContainer.BStoreContainerData;
				num4 += array.Length;
				stream.Write(array, 0, array.Length);
				Hashtable bselist = this.m_drawingGroupContainer.BSEList;
				ArrayList blipList = this.m_drawingGroupContainer.BlipList;
				int num5 = 0;
				int num6 = 0;
				Stream stream2 = null;
				int num7 = 0;
				byte[] array2 = this.FillRecordWithBlips(blipList, bselist, stream, ref num5, ref num4, ref num6, ref stream2);
				for (int i = 0; i < num; i++)
				{
					Continue @continue = new Continue();
					if (i == num - 1)
					{
						@continue.Length = (ushort)num3;
					}
					else
					{
						@continue.Length = 8224;
					}
					stream.Write(@continue.GetBaseData(), 0, 4);
					num4 = 0;
					if (array2 != null)
					{
						stream.Write(array2, 0, array2.Length);
						num4 += array2.Length;
						array2 = null;
					}
					if (stream2 == null)
					{
						array2 = this.FillRecordWithBlips(blipList, bselist, stream, ref num5, ref num4, ref num6, ref stream2);
						if (num4 < 8224)
						{
							this.WriteShareProperties(stream, ref num7, num4);
						}
					}
					else
					{
						int num8 = 8224 - num4;
						int num9 = (int)stream2.Length - num6;
						if (num9 < num8)
						{
							if (num9 > 0)
							{
								byte[] array3 = new byte[num9];
								stream2.Read(array3, 0, array3.Length);
								stream.Write(array3, 0, array3.Length);
								num4 += array3.Length;
								stream2.Close();
							}
							num5++;
							num6 = 0;
							stream2 = null;
							array2 = this.FillRecordWithBlips(blipList, bselist, stream, ref num5, ref num4, ref num6, ref stream2);
							if (num4 < 8224)
							{
								this.WriteShareProperties(stream, ref num7, num4);
							}
						}
						else
						{
							byte[] buffer = new byte[num8];
							stream2.Read(buffer, 0, num8);
							stream.Write(buffer, 0, num8);
							num6 += num8;
						}
					}
				}
				stream.Write(this.m_drawingGroupContainer.ShapePropertyData, num7, 50 - num7);
			}
		}

		// Token: 0x04001B52 RID: 6994
		private const int BSEBlipLength = 69;

		// Token: 0x04001B53 RID: 6995
		private const int BSELength = 44;

		// Token: 0x04001B54 RID: 6996
		private const int MaxRecordLength = 8224;

		// Token: 0x04001B55 RID: 6997
		private const int ShapePropertyLength = 50;

		// Token: 0x04001B56 RID: 6998
		private Escher.DrawingGroupContainer m_drawingGroupContainer;
	}
}

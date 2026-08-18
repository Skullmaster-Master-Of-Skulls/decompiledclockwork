using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using Net.Sgoliver.NRtfTree.Core;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000004 RID: 4
	public class ImageNode : RtfTreeNode
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003380 File Offset: 0x00001580
		public ImageNode(RtfTreeNode node)
		{
			if (node != null)
			{
				base.NodeKey = node.NodeKey;
				base.HasParameter = node.HasParameter;
				base.Parameter = node.Parameter;
				base.ParentNode = node.ParentNode;
				base.RootNode = node.RootNode;
				base.NodeType = node.NodeType;
				base.ChildNodes = new RtfNodeCollection();
				base.ChildNodes.AddRange(node.ChildNodes);
				this.getImageData();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003400 File Offset: 0x00001600
		public string HexData
		{
			get
			{
				return base.SelectSingleChildNode(RtfNodeType.Text).NodeKey;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003410 File Offset: 0x00001610
		public ImageFormat ImageFormat
		{
			get
			{
				if (base.SelectSingleChildNode("jpegblip") != null)
				{
					return ImageFormat.Jpeg;
				}
				if (base.SelectSingleChildNode("pngblip") != null)
				{
					return ImageFormat.Png;
				}
				if (base.SelectSingleChildNode("emfblip") != null)
				{
					return ImageFormat.Emf;
				}
				if (base.SelectSingleChildNode("wmetafile") != null)
				{
					return ImageFormat.Wmf;
				}
				if (base.SelectSingleChildNode("dibitmap") != null || base.SelectSingleChildNode("wbitmap") != null)
				{
					return ImageFormat.Bmp;
				}
				return null;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000348C File Offset: 0x0000168C
		public int Width
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleChildNode("picw");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.Parameter;
				}
				return -1;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000034B0 File Offset: 0x000016B0
		public int Height
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleChildNode("pich");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.Parameter;
				}
				return -1;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000034D4 File Offset: 0x000016D4
		public int DesiredWidth
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleChildNode("picwgoal");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.Parameter;
				}
				return -1;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000034F8 File Offset: 0x000016F8
		public int DesiredHeight
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleChildNode("pichgoal");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.Parameter;
				}
				return -1;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000351C File Offset: 0x0000171C
		public int ScaleX
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleChildNode("picscalex");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.Parameter;
				}
				return -1;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003540 File Offset: 0x00001740
		public int ScaleY
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleChildNode("picscaley");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.Parameter;
				}
				return -1;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003564 File Offset: 0x00001764
		public Bitmap Bitmap
		{
			get
			{
				MemoryStream stream = new MemoryStream(this.GetByteData(), 0, this.data.Length);
				return new Bitmap(stream);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000358C File Offset: 0x0000178C
		public byte[] GetByteData()
		{
			return this.data;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003594 File Offset: 0x00001794
		public void SaveImage(string filePath)
		{
			if (this.data != null)
			{
				MemoryStream stream = new MemoryStream(this.GetByteData(), 0, this.data.Length);
				Bitmap bitmap = new Bitmap(stream);
				bitmap.Save(filePath, this.ImageFormat);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035D4 File Offset: 0x000017D4
		public void SaveImage(string filePath, ImageFormat format)
		{
			if (this.data != null)
			{
				MemoryStream stream = new MemoryStream(this.data, 0, this.data.Length);
				Bitmap bitmap = new Bitmap(stream);
				bitmap.Save(filePath, format);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003610 File Offset: 0x00001810
		private void getImageData()
		{
			if (base.FirstChild.NodeKey == "pict")
			{
				string nodeKey = base.SelectSingleChildNode(RtfNodeType.Text).NodeKey;
				int num = nodeKey.Length / 2;
				this.data = new byte[num];
				StringBuilder stringBuilder = new StringBuilder(2);
				for (int i = 0; i < nodeKey.Length; i++)
				{
					stringBuilder.Append(nodeKey[i]);
					if (stringBuilder.Length == 2)
					{
						this.data[i / 2] = byte.Parse(stringBuilder.ToString(), NumberStyles.HexNumber);
						stringBuilder.Remove(0, 2);
					}
				}
			}
		}

		// Token: 0x0400000E RID: 14
		private byte[] data;
	}
}

using System;
using System.Drawing;
using System.IO;
using Spire.Doc.Documents;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x0200001B RID: 27
	public interface IImageData
	{
		// Token: 0x06000024 RID: 36
		void SetImage(Image image);

		// Token: 0x06000025 RID: 37
		void SetImage(Stream stream);

		// Token: 0x06000026 RID: 38
		void SetImage(string fileName);

		// Token: 0x06000027 RID: 39
		Image ToImage();

		// Token: 0x06000028 RID: 40
		Stream ToStream();

		// Token: 0x06000029 RID: 41
		byte[] ToByteArray();

		// Token: 0x0600002A RID: 42
		void Save(Stream stream);

		// Token: 0x0600002B RID: 43
		void Save(string fileName);

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44
		// (set) Token: 0x0600002D RID: 45
		byte[] ImageBytes { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46
		bool HasImage { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47
		ImageSize ImageSize { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48
		ImageType ImageType { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49
		bool IsLink { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50
		bool IsLinkOnly { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		string SourceFullName { get; set; }
	}
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x02001330 RID: 4912
	public class FileItem : FileBrowserItem
	{
		// Token: 0x170041F7 RID: 16887
		// (get) Token: 0x0600CD25 RID: 52517 RVA: 0x002DB07F File Offset: 0x002D927F
		// (set) Token: 0x0600CD26 RID: 52518 RVA: 0x002DB087 File Offset: 0x002D9287
		public string Extension { get; set; }

		// Token: 0x170041F8 RID: 16888
		// (get) Token: 0x0600CD27 RID: 52519 RVA: 0x002DB090 File Offset: 0x002D9290
		// (set) Token: 0x0600CD28 RID: 52520 RVA: 0x002DB098 File Offset: 0x002D9298
		public long Length { get; set; }

		// Token: 0x170041F9 RID: 16889
		// (get) Token: 0x0600CD29 RID: 52521 RVA: 0x002DB0A1 File Offset: 0x002D92A1
		public override string Path
		{
			get
			{
				return this.Location;
			}
		}

		// Token: 0x170041FA RID: 16890
		// (get) Token: 0x0600CD2A RID: 52522 RVA: 0x002DB0A9 File Offset: 0x002D92A9
		// (set) Token: 0x0600CD2B RID: 52523 RVA: 0x002DB0B1 File Offset: 0x002D92B1
		public string Location { get; set; }

		// Token: 0x170041FB RID: 16891
		// (get) Token: 0x0600CD2C RID: 52524 RVA: 0x002DB0BA File Offset: 0x002D92BA
		// (set) Token: 0x0600CD2D RID: 52525 RVA: 0x002DB0C2 File Offset: 0x002D92C2
		public string Url { get; set; }

		// Token: 0x0600CD2E RID: 52526 RVA: 0x002DB0CC File Offset: 0x002D92CC
		public override void Serialize(StringWriter writer)
		{
			writer.Write("[{0:D}", FileItemType.File);
			FileBrowserItem.WriteSeparator(writer);
			writer.Write("{0:D}", this.Permissions);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, this.Name);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, this.Location);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, this.Url);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, this.Extension);
			FileBrowserItem.WriteSeparator(writer);
			writer.Write(this.Length);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, base.Tag);
			FileBrowserItem.WriteSeparator(writer);
			base.SerializeAttributes(writer);
			FileBrowserItem.WriteSeparator(writer);
			writer.Write("[]]");
		}

		// Token: 0x0600CD2F RID: 52527 RVA: 0x002DB190 File Offset: 0x002D9390
		public FileItem()
		{
		}

		// Token: 0x0600CD30 RID: 52528 RVA: 0x002DB198 File Offset: 0x002D9398
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FileItem(string name, string extension, long length, string location, string url, string tag, PathPermissions permissions)
		{
			this.Name = name;
			this.Extension = extension;
			this.Length = length;
			this.Location = location;
			this.Url = url;
			this.Permissions = permissions;
			base.Tag = tag;
		}
	}
}

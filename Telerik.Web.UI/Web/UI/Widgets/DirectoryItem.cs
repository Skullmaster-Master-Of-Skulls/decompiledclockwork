using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x0200132B RID: 4907
	public class DirectoryItem : FileBrowserItem
	{
		// Token: 0x170041E3 RID: 16867
		// (get) Token: 0x0600CCE0 RID: 52448 RVA: 0x002DAA4C File Offset: 0x002D8C4C
		public override string Path
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x170041E4 RID: 16868
		// (get) Token: 0x0600CCE1 RID: 52449 RVA: 0x002DAA54 File Offset: 0x002D8C54
		// (set) Token: 0x0600CCE2 RID: 52450 RVA: 0x002DAA5C File Offset: 0x002D8C5C
		public string FullPath { get; set; }

		// Token: 0x170041E5 RID: 16869
		// (get) Token: 0x0600CCE3 RID: 52451 RVA: 0x002DAA65 File Offset: 0x002D8C65
		// (set) Token: 0x0600CCE4 RID: 52452 RVA: 0x002DAA6D File Offset: 0x002D8C6D
		public string Location { get; set; }

		// Token: 0x170041E6 RID: 16870
		// (get) Token: 0x0600CCE5 RID: 52453 RVA: 0x002DAA76 File Offset: 0x002D8C76
		// (set) Token: 0x0600CCE6 RID: 52454 RVA: 0x002DAA7E File Offset: 0x002D8C7E
		public DirectoryItem[] Directories { get; set; }

		// Token: 0x170041E7 RID: 16871
		// (get) Token: 0x0600CCE7 RID: 52455 RVA: 0x002DAA87 File Offset: 0x002D8C87
		// (set) Token: 0x0600CCE8 RID: 52456 RVA: 0x002DAA8F File Offset: 0x002D8C8F
		public FileItem[] Files { get; set; }

		// Token: 0x0600CCE9 RID: 52457 RVA: 0x002DAA98 File Offset: 0x002D8C98
		public void ClearDirectories()
		{
			this.Directories = new DirectoryItem[0];
		}

		// Token: 0x0600CCEA RID: 52458 RVA: 0x002DAAA8 File Offset: 0x002D8CA8
		public override void Serialize(StringWriter writer)
		{
			writer.Write("[{0:D}", FileItemType.Directory);
			FileBrowserItem.WriteSeparator(writer);
			writer.Write("{0:D}", this.Permissions);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, this.Name);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, this.Location);
			FileBrowserItem.WriteSeparator(writer);
			writer.Write("''");
			FileBrowserItem.WriteSeparator(writer);
			writer.Write("''");
			FileBrowserItem.WriteSeparator(writer);
			writer.Write(0);
			FileBrowserItem.WriteSeparator(writer);
			FileBrowserItem.WriteJavascriptString(writer, base.Tag);
			FileBrowserItem.WriteSeparator(writer);
			base.SerializeAttributes(writer);
			FileBrowserItem.WriteSeparator(writer);
			this.SerializeContent(writer);
			writer.Write("]");
		}

		// Token: 0x0600CCEB RID: 52459 RVA: 0x002DAB6C File Offset: 0x002D8D6C
		public void SerializeContent(StringWriter writer)
		{
			writer.Write("[");
			foreach (DirectoryItem directoryItem in this.Directories)
			{
				directoryItem.Serialize(writer);
				FileBrowserItem.WriteSeparator(writer);
			}
			foreach (FileItem fileItem in this.Files)
			{
				fileItem.Serialize(writer);
				FileBrowserItem.WriteSeparator(writer);
			}
			if (this.Directories.Length > 0 || this.Files.Length > 0)
			{
				FileBrowserItem.RemoveLastSeparator(writer);
			}
			writer.Write("]");
		}

		// Token: 0x0600CCEC RID: 52460 RVA: 0x002DAC00 File Offset: 0x002D8E00
		public string GetSerializedContent()
		{
			StringWriter stringWriter = new StringWriter();
			this.SerializeContent(stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x0600CCED RID: 52461 RVA: 0x002DAC20 File Offset: 0x002D8E20
		public DirectoryItem()
		{
		}

		// Token: 0x0600CCEE RID: 52462 RVA: 0x002DAC28 File Offset: 0x002D8E28
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public DirectoryItem(string name, string location, string fullPath, string tag, PathPermissions permissions, FileItem[] files, DirectoryItem[] directories)
		{
			this.Directories = directories;
			this.Files = files;
			this.FullPath = fullPath;
			this.Location = location;
			this.Name = name;
			this.Permissions = permissions;
			base.Tag = tag;
		}
	}
}

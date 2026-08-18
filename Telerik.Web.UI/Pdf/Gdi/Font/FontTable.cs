using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001617 RID: 5655
	internal abstract class FontTable
	{
		// Token: 0x0600DC2E RID: 56366 RVA: 0x003020AF File Offset: 0x003002AF
		public FontTable(string tableName, DirectoryEntry entry)
		{
			this.directoryEntry = entry;
		}

		// Token: 0x17004360 RID: 17248
		// (get) Token: 0x0600DC2F RID: 56367 RVA: 0x003020BE File Offset: 0x003002BE
		// (set) Token: 0x0600DC30 RID: 56368 RVA: 0x003020C6 File Offset: 0x003002C6
		public DirectoryEntry Entry
		{
			get
			{
				return this.directoryEntry;
			}
			set
			{
				this.directoryEntry = value;
			}
		}

		// Token: 0x0600DC31 RID: 56369
		protected internal abstract void Read(FontFileReader reader);

		// Token: 0x0600DC32 RID: 56370
		protected internal abstract void Write(FontFileWriter writer);

		// Token: 0x17004361 RID: 17249
		// (get) Token: 0x0600DC33 RID: 56371 RVA: 0x003020CF File Offset: 0x003002CF
		public string Name
		{
			get
			{
				return this.directoryEntry.TableName;
			}
		}

		// Token: 0x17004362 RID: 17250
		// (get) Token: 0x0600DC34 RID: 56372 RVA: 0x003020DC File Offset: 0x003002DC
		public int Tag
		{
			get
			{
				return this.directoryEntry.Tag;
			}
		}

		// Token: 0x04003DB5 RID: 15797
		private DirectoryEntry directoryEntry;
	}
}

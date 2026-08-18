using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001628 RID: 5672
	internal class TrueTypeHeader
	{
		// Token: 0x0600DC99 RID: 56473 RVA: 0x003035AC File Offset: 0x003017AC
		protected internal void Read(FontFileStream stream)
		{
			stream.Skip(4L);
			int num = stream.ReadUShort();
			stream.Skip(6L);
			this.directoryEntries = new Hashtable(num);
			for (int i = 0; i < num; i++)
			{
				DirectoryEntry directoryEntry = new DirectoryEntry(stream.ReadTag(), stream.ReadULong(), stream.ReadULong(), stream.ReadULong());
				this.directoryEntries.Add(directoryEntry.TableName, directoryEntry);
			}
		}

		// Token: 0x0600DC9A RID: 56474 RVA: 0x00303618 File Offset: 0x00301818
		public bool Contains(string tableName)
		{
			return this.directoryEntries != null && this.directoryEntries.Contains(tableName);
		}

		// Token: 0x1700438C RID: 17292
		public DirectoryEntry this[string tableName]
		{
			get
			{
				if (!this.Contains(tableName))
				{
					throw new ArgumentException("Cannot locate table " + tableName, "tableName");
				}
				return (DirectoryEntry)this.directoryEntries[tableName];
			}
		}

		// Token: 0x1700438D RID: 17293
		// (get) Token: 0x0600DC9C RID: 56476 RVA: 0x00303662 File Offset: 0x00301862
		public int Count
		{
			get
			{
				if (this.directoryEntries == null)
				{
					return 0;
				}
				return this.directoryEntries.Count;
			}
		}

		// Token: 0x04003E38 RID: 15928
		private IDictionary directoryEntries;
	}
}

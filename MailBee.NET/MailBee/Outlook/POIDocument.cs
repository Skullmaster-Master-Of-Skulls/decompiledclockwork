using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x02000292 RID: 658
	[Serializable]
	internal abstract class POIDocument
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x000697ED File Offset: 0x000687ED
		protected POIDocument(DirectoryNode A_0)
		{
			this.directory = A_0;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000697FC File Offset: 0x000687FC
		[Obsolete]
		public POIDocument(DirectoryNode A_0, POIFSFileSystem A_1)
		{
			this.directory = A_0;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0006980B File Offset: 0x0006880B
		public POIDocument(POIFSFileSystem A_0) : this(A_0.Root)
		{
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00069819 File Offset: 0x00068819
		public void d()
		{
			if (!this.initialized)
			{
				this.b();
			}
			if (this.sInf == null)
			{
				this.sInf = a0.b();
			}
			if (this.dsInf == null)
			{
				this.dsInf = a0.a();
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0006984F File Offset: 0x0006884F
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x00069865 File Offset: 0x00068865
		public DocumentSummaryInformation DocumentSummaryInformation
		{
			get
			{
				if (!this.initialized)
				{
					this.b();
				}
				return this.dsInf;
			}
			set
			{
				this.dsInf = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0006986E File Offset: 0x0006886E
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x00069884 File Offset: 0x00068884
		public SummaryInformation SummaryInformation
		{
			get
			{
				if (!this.initialized)
				{
					this.b();
				}
				return this.sInf;
			}
			set
			{
				this.sInf = value;
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00069890 File Offset: 0x00068890
		protected void b()
		{
			PropertySet propertySet = this.a("\u0005DocumentSummaryInformation");
			if (propertySet != null && propertySet is DocumentSummaryInformation)
			{
				this.dsInf = (DocumentSummaryInformation)propertySet;
			}
			propertySet = this.a("\u0005SummaryInformation");
			if (propertySet is SummaryInformation)
			{
				this.sInf = (SummaryInformation)propertySet;
			}
			this.initialized = true;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000698F0 File Offset: 0x000688F0
		protected PropertySet a(string A_0)
		{
			if (this.directory == null || !this.directory.ej(A_0))
			{
				return null;
			}
			az a_;
			try
			{
				a_ = this.directory.a(A_0);
			}
			catch (IOException)
			{
				return null;
			}
			try
			{
				return a0.a(a_);
			}
			catch (IOException)
			{
			}
			catch (HPSFException)
			{
			}
			return null;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00069968 File Offset: 0x00068968
		protected void a(POIFSFileSystem A_0)
		{
			this.a(A_0, null);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00069974 File Offset: 0x00068974
		protected void a(POIFSFileSystem A_0, IList A_1)
		{
			if (this.sInf != null)
			{
				this.a("\u0005SummaryInformation", this.sInf, A_0);
				if (A_1 != null)
				{
					A_1.Add("\u0005SummaryInformation");
				}
			}
			if (this.dsInf != null)
			{
				this.a("\u0005DocumentSummaryInformation", this.dsInf, A_0);
				if (A_1 != null)
				{
					A_1.Add("\u0005DocumentSummaryInformation");
				}
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000699D4 File Offset: 0x000689D4
		protected void a(string A_0, PropertySet A_1, POIFSFileSystem A_2)
		{
			try
			{
				MutablePropertySet mutablePropertySet = new MutablePropertySet(A_1);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					mutablePropertySet.c4(memoryStream);
					using (MemoryStream memoryStream2 = new MemoryStream(memoryStream.ToArray()))
					{
						A_2.a(memoryStream2, A_0);
					}
				}
			}
			catch (WritingNotSupportedException)
			{
			}
		}

		// Token: 0x0600172B RID: 5931
		public abstract void kp(Stream A_0);

		// Token: 0x0600172C RID: 5932 RVA: 0x00069A50 File Offset: 0x00068A50
		[Obsolete]
		protected void a(POIFSFileSystem A_0, POIFSFileSystem A_1, List<string> A_2)
		{
			cn.a(A_0, A_1, A_2);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00069A5A File Offset: 0x00068A5A
		[Obsolete]
		protected void a(DirectoryNode A_0, DirectoryNode A_1, List<string> A_2)
		{
			cn.a(A_0, A_1, A_2);
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00069A64 File Offset: 0x00068A64
		private bool a(string A_0, IList A_1)
		{
			for (int i = 0; i < A_1.Count; i++)
			{
				if (A_1[i].Equals(A_0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00069A94 File Offset: 0x00068A94
		[Obsolete]
		private void a(e1 A_0, ig A_1)
		{
			cn.a(A_0, A_1);
		}

		// Token: 0x04001146 RID: 4422
		protected SummaryInformation sInf;

		// Token: 0x04001147 RID: 4423
		protected DocumentSummaryInformation dsInf;

		// Token: 0x04001148 RID: 4424
		protected DirectoryNode directory;

		// Token: 0x04001149 RID: 4425
		protected bool initialized;
	}
}

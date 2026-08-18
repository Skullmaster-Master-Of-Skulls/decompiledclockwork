using System;
using System.Text.RegularExpressions;
using Spire.Doc.Documents;

namespace Spire.Doc
{
	// Token: 0x0200009D RID: 157
	public abstract class BodyRegion : DocumentBase, IBodyRegion
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000123FC File Offset: 0x000113FC
		public Body OwnerTextBody
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.Owner as Body;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00012444 File Offset: 0x00011444
		public bool IsInsertRevision
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.CheckInsertRev();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00012488 File Offset: 0x00011488
		public bool IsDeleteRevision
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.CheckDeleteRev();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000124CC File Offset: 0x000114CC
		internal bool IsChangedCFormat
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.CheckChangedCFormat();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00012510 File Offset: 0x00011510
		internal bool IsChangedPFormat
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.CheckChangedPFormat();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00012554 File Offset: 0x00011554
		internal BodyRegion NextTextBodyItem
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.GetNextTextBodyItem();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00012598 File Offset: 0x00011598
		public BodyRegion(Document doc) : base(doc, null)
		{
		}

		// Token: 0x06000182 RID: 386
		public abstract TextSelection Find(Regex pattern);

		// Token: 0x06000183 RID: 387
		public abstract int Replace(Regex pattern, string replace);

		// Token: 0x06000184 RID: 388
		public abstract int Replace(string given, string replace, bool caseSensitive, bool wholeWord);

		// Token: 0x06000185 RID: 389
		public abstract int Replace(Regex pattern, TextSelection textSelection);

		// Token: 0x06000186 RID: 390
		public abstract int Replace(Regex pattern, TextSelection textSelection, bool saveFormatting);

		// Token: 0x06000187 RID: 391
		internal abstract spr\u226E FindAll(Regex pattern);

		// Token: 0x06000188 RID: 392
		internal abstract BodyRegion GetNextTextBodyItem();

		// Token: 0x06000189 RID: 393
		internal abstract void Close();

		// Token: 0x0600018A RID: 394
		internal abstract void MakeChanges(bool acceptChanges);

		// Token: 0x0600018B RID: 395
		internal abstract bool CheckInsertRev();

		// Token: 0x0600018C RID: 396
		internal abstract bool CheckDeleteRev();

		// Token: 0x0600018D RID: 397
		internal abstract bool CheckChangedCFormat();

		// Token: 0x0600018E RID: 398
		internal abstract bool CheckChangedPFormat();

		// Token: 0x0600018F RID: 399
		internal abstract void AcceptCChanges();

		// Token: 0x06000190 RID: 400
		internal abstract void AcceptPChanges();

		// Token: 0x06000191 RID: 401
		internal abstract void RemoveCFormatChanges();

		// Token: 0x06000192 RID: 402
		internal abstract void RemovePFormatChanges();

		// Token: 0x06000193 RID: 403
		internal abstract bool HasTrackedChanges();

		// Token: 0x06000194 RID: 404
		internal abstract void SetChangedCFormat(bool check);

		// Token: 0x06000195 RID: 405
		internal abstract void SetChangedPFormat(bool check);

		// Token: 0x06000196 RID: 406
		internal abstract void SetDeleteRev(bool check);

		// Token: 0x06000197 RID: 407
		internal abstract void SetInsertRev(bool check);

		// Token: 0x06000198 RID: 408 RVA: 0x000125B0 File Offset: 0x000115B0
		protected BodyRegion GetNextInSection(Section section)
		{
			int num = 1;
			Section section2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (section2.Body.Items.Count > 0)
					{
						num = 5;
						continue;
					}
					goto IL_B3;
				case 2:
					if (section2 != null)
					{
						num = 4;
						continue;
					}
					goto IL_B3;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_99;
					}
					break;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_60;
				}
				if (section == null)
				{
					num = 3;
				}
				else
				{
					section2 = (section.NextSibling as Section);
					num = 2;
				}
			}
			IL_60:
			return section2.Body.Items[0];
			IL_99:
			if (false)
			{
			}
			return null;
			IL_B3:
			return null;
		}
	}
}

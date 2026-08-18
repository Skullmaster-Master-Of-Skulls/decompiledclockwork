using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007F2 RID: 2034
	internal sealed class NameInfo
	{
		// Token: 0x060047C5 RID: 18373 RVA: 0x000F6149 File Offset: 0x000F5149
		internal NameInfo()
		{
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x000F6154 File Offset: 0x000F5154
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x000F61BE File Offset: 0x000F51BE
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x000F61E6 File Offset: 0x000F51E6
		// (set) Token: 0x060047C9 RID: 18377 RVA: 0x000F6207 File Offset: 0x000F5207
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x0400249B RID: 9371
		internal string NIFullName;

		// Token: 0x0400249C RID: 9372
		internal long NIobjectId;

		// Token: 0x0400249D RID: 9373
		internal long NIassemId;

		// Token: 0x0400249E RID: 9374
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x0400249F RID: 9375
		internal Type NItype;

		// Token: 0x040024A0 RID: 9376
		internal bool NIisSealed;

		// Token: 0x040024A1 RID: 9377
		internal bool NIisArray;

		// Token: 0x040024A2 RID: 9378
		internal bool NIisArrayItem;

		// Token: 0x040024A3 RID: 9379
		internal bool NItransmitTypeOnObject;

		// Token: 0x040024A4 RID: 9380
		internal bool NItransmitTypeOnMember;

		// Token: 0x040024A5 RID: 9381
		internal bool NIisParentTypeOnObject;

		// Token: 0x040024A6 RID: 9382
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x040024A7 RID: 9383
		private bool NIsealedStatusChecked;
	}
}

using System;
using Spire.Doc.Fields;

namespace Spire.Doc.Documents
{
	// Token: 0x02000496 RID: 1174
	public class CommentMark : ParagraphBase
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x003D337C File Offset: 0x003D237C
		// (set) Token: 0x0600405D RID: 16477 RVA: 0x003D33C0 File Offset: 0x003D23C0
		internal int CommentId
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x003D3404 File Offset: 0x003D2404
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.CommentMark;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x003D3444 File Offset: 0x003D2444
		// (set) Token: 0x06004060 RID: 16480 RVA: 0x003D3488 File Offset: 0x003D2488
		public CommentMarkType Type
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
				return this.ᜁ;
			}
			set
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
				this.ᜁ = value;
			}
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x003D34CC File Offset: 0x003D24CC
		internal CommentMark(Document A_0, int A_1) : base(A_0)
		{
			this.ᜀ = A_1;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x003D34F0 File Offset: 0x003D24F0
		internal CommentMark(Document A_0, int A_1, CommentMarkType A_2) : this(A_0, A_1)
		{
			this.ᜁ = A_2;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x003D350C File Offset: 0x003D250C
		protected override object CloneImpl()
		{
			CommentMark commentMark;
			for (;;)
			{
				commentMark = (CommentMark)base.CloneImpl();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_97;
					case 1:
						if (this.ᜀ != -1)
						{
							num = 4;
							continue;
						}
						return commentMark;
					case 2:
						return commentMark;
					case 3:
						if (this.ᜁ == CommentMarkType.CommentStart)
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_97;
						default:
							if (false)
							{
							}
							commentMark.CommentId = spr\u180D.ᜀ(this.ᜀ, false);
							num = 5;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						num = 3;
						continue;
					case 5:
						return commentMark;
					}
					break;
					IL_97:
					commentMark.CommentId = spr\u180D.ᜀ(this.ᜀ, true);
					num = 2;
				}
			}
			return commentMark;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x003D35F0 File Offset: 0x003D25F0
		protected override void CreateLayoutInfo()
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
			this.ᜀ = new spr\u22A8();
			this.ᜀ.ᜁ(true);
		}

		// Token: 0x04002FB7 RID: 12215
		private float \u2460\u00A6\u008D\u0081;

		// Token: 0x04002FB8 RID: 12216
		private string[] \u2460\u00A1\u0097\u009F;

		// Token: 0x04002FB9 RID: 12217
		private bool[] \u2593\u008D\u008F\u0090;

		// Token: 0x04002FBA RID: 12218
		private float \u2593\u0090\u00A9\u009D;

		// Token: 0x04002FBB RID: 12219
		private new int ᜀ = -1;

		// Token: 0x04002FBC RID: 12220
		private float \u2609\u0082\u009D\u0083;

		// Token: 0x04002FBD RID: 12221
		private float[] \u2593\u0092\u007F\u00AF;

		// Token: 0x04002FBE RID: 12222
		private new CommentMarkType ᜁ;
	}
}

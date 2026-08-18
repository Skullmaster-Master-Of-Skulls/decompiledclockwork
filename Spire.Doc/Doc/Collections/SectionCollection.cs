using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200053E RID: 1342
	public class SectionCollection : DocumentObjectCollection, IWSectionCollection
	{
		// Token: 0x17000552 RID: 1362
		public Section this[int index]
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
				return base.InnerList[index] as Section;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x0040D7DC File Offset: 0x0040C7DC
		protected override Type[] TypesOfElement
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
				return SectionCollection.ᜀ;
			}
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0040D81C File Offset: 0x0040C81C
		public SectionCollection(Document doc) : base(doc, doc)
		{
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x0040D834 File Offset: 0x0040C834
		internal SectionCollection() : base(null, null)
		{
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x0040D84C File Offset: 0x0040C84C
		public int Add(ISection section)
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
			return base.Add(section);
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x0040D890 File Offset: 0x0040C890
		public int IndexOf(ISection section)
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
			return base.InnerList.IndexOf(section);
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x0040D8D8 File Offset: 0x0040C8D8
		internal new string ᜀ()
		{
			string text;
			for (;;)
			{
				text = string.Empty;
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_34;
					case 1:
						if (true)
						{
						}
						if (!text.EndsWith(spr\u20E8.\u171F))
						{
							num2 = 2;
							continue;
						}
						goto IL_39;
					case 2:
						text += spr\u20E8.\u171F;
						num2 = 4;
						continue;
					case 3:
						if (num >= base.Count)
						{
							num2 = 5;
							continue;
						}
						text += this[num].ᜋ();
						num2 = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							if (false)
							{
							}
							goto IL_39;
						}
						break;
					case 5:
						return text;
					case 6:
						goto IL_B9;
					}
					break;
					IL_39:
					num++;
					num2 = 6;
					continue;
					IL_B9:
					num2 = 3;
					continue;
					IL_34:
					goto IL_B9;
				}
			}
			return text;
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x0040D9CC File Offset: 0x0040C9CC
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
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
			return new Section(base.Document);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x0040DA14 File Offset: 0x0040CA14
		protected override string GetTagItemName()
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("ᩨ๪๬᭮ᡰᱲ᭴", a_);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x0040DA68 File Offset: 0x0040CA68
		// Note: this type is marked as 'beforefieldinit'.
		static SectionCollection()
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
			SectionCollection.ᜀ = new Type[]
			{
				typeof(Section)
			};
		}

		// Token: 0x0400368B RID: 13963
		private string \u2460\u00B0\u00A5\u0085;

		// Token: 0x0400368C RID: 13964
		private string \u2593\u009B\u0088\u008C;

		// Token: 0x0400368D RID: 13965
		private new static readonly Type[] ᜀ;
	}
}

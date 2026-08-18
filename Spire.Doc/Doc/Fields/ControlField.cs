using System;
using System.IO;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x02000524 RID: 1316
	public class ControlField : Field
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x00406178 File Offset: 0x00405178
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.ControlField;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x004061B8 File Offset: 0x004051B8
		// (set) Token: 0x06004518 RID: 17688 RVA: 0x004061FC File Offset: 0x004051FC
		internal int StoragePicLocation
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00406240 File Offset: 0x00405240
		internal ControlField(IDocument A_0) : base(A_0)
		{
			this.m_paraItemType = ParagraphItemType.ControlField;
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0040625C File Offset: 0x0040525C
		protected override object CloneImpl()
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
			return (ControlField)base.CloneImpl();
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x004062A4 File Offset: 0x004052A4
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			base.CloneRelationsTo(doc, nextOwner);
			if (doc.ObjectPool == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				doc.ObjectPool = new byte[this.m_doc.ObjectPool.Length];
				this.m_doc.ObjectPool.CopyTo(doc.ObjectPool, 0);
				return;
			}
			byte[] array = doc.ObjectPool;
			spr\u1C2D.ᜀ(new MemoryStream(this.m_doc.ObjectPool), this.ᜀ, new MemoryStream(doc.ObjectPool), out array);
		}

		// Token: 0x0400363C RID: 13884
		private byte \u2593\u009C\u0098\u0088;

		// Token: 0x0400363D RID: 13885
		private new int ᜀ;
	}
}

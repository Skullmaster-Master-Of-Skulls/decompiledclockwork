using System;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200061B RID: 1563
	public class WorksheetCustomProperty : ICustomProperty, ICloneable
	{
		// Token: 0x06005ECF RID: 24271 RVA: 0x003B3CF4 File Offset: 0x003B2CF4
		private WorksheetCustomProperty()
		{
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x003B3D08 File Offset: 0x003B2D08
		public WorksheetCustomProperty(string strName)
		{
			this.ᜀ = (sprế)spr\u175E.ᜀ(TBIFFRecord.CustomProperty);
			this.ᜀ.ᜁ(strName);
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x003B3D3C File Offset: 0x003B2D3C
		internal WorksheetCustomProperty(sprế A_0)
		{
			int a_ = 16;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㙅㩇╉㱋⭍≏♑ⵓ", a_));
			}
			this.ᜀ = A_0;
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x003B3D78 File Offset: 0x003B2D78
		public string Name
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
				return this.ᜀ.ᜀ();
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x003B3DC0 File Offset: 0x003B2DC0
		// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x003B3E08 File Offset: 0x003B2E08
		public string Value
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
				return this.ᜀ.ᜁ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x003B3E50 File Offset: 0x003B2E50
		internal void ᜀ(RecordArrayList A_0)
		{
			int a_ = 6;
			if (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
				}
			}
			A_0.ᜀ(this.ᜀ);
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x003B3EBC File Offset: 0x003B2EBC
		public object Clone()
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
			WorksheetCustomProperty worksheetCustomProperty = (WorksheetCustomProperty)base.MemberwiseClone();
			worksheetCustomProperty.ᜀ = (sprế)spr\u1CD3.ᜀ(this.ᜀ);
			return worksheetCustomProperty;
		}

		// Token: 0x04002D84 RID: 11652
		private int \u2593\u0086\u009A\u0097;

		// Token: 0x04002D85 RID: 11653
		private bool \u25D8\u00A6ª\u00A6;

		// Token: 0x04002D86 RID: 11654
		private int[] \u2593\u00AF\u0080\u0086;

		// Token: 0x04002D87 RID: 11655
		private byte \u25D8\u0084\u00B0\u0089;

		// Token: 0x04002D88 RID: 11656
		private string[] \u25D9\u009A\u00A3\u009E;

		// Token: 0x04002D89 RID: 11657
		private long[] \u2460\u0099\u0083\u00AD;

		// Token: 0x04002D8A RID: 11658
		private sprế ᜀ;
	}
}

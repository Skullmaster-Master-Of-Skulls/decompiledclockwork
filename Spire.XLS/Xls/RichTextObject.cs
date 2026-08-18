using System;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x02000107 RID: 263
	public class RichTextObject : IRichTextString
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x00075918 File Offset: 0x00074918
		internal RichTextObject(IRichTextString A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00075934 File Offset: 0x00074934
		public IFont GetFont(int position)
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
			return this.ᜀ.GetFont(position);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0007597C File Offset: 0x0007497C
		public void SetFont(int startPos, int endPos, IFont font)
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
			this.ᜀ.SetFont(startPos, endPos, font);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000759C8 File Offset: 0x000749C8
		public void ClearFormatting()
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
			this.ᜀ.ClearFormatting();
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00075A10 File Offset: 0x00074A10
		public void Clear()
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
			this.ᜀ.Clear();
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00075A58 File Offset: 0x00074A58
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00075AA0 File Offset: 0x00074AA0
		public string Text
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
				return this.ᜀ.Text;
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
				this.ᜀ.Text = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00075AE8 File Offset: 0x00074AE8
		public string RtfText
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
				return this.ᜀ.RtfText;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00075B30 File Offset: 0x00074B30
		public bool IsFormatted
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
				return this.ᜀ.IsFormatted;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00075B78 File Offset: 0x00074B78
		public object Parent
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
				return this.ᜀ.Parent;
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00075BC0 File Offset: 0x00074BC0
		public void BeginUpdate()
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
			this.ᜀ.BeginUpdate();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00075C08 File Offset: 0x00074C08
		public void EndUpdate()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ.EndUpdate();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00075C50 File Offset: 0x00074C50
		public void Append(string text, IFont font)
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
			this.ᜀ.Append(text, font);
		}

		// Token: 0x04000A01 RID: 2561
		private byte \u2460\u00AC\u0092\u00A9;

		// Token: 0x04000A02 RID: 2562
		private IRichTextString ᜀ;
	}
}

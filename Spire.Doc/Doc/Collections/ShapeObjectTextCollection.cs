using System;
using System.Collections.Generic;
using Spire.Doc.Fields;

namespace Spire.Doc.Collections
{
	// Token: 0x02000544 RID: 1348
	[CLSCompliant(false)]
	public class ShapeObjectTextCollection
	{
		// Token: 0x06004657 RID: 18007 RVA: 0x0040E918 File Offset: 0x0040D918
		public void AddTextBox(int shapeId, TextBox textBox)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ = new Dictionary<int, TextBox>();
					num = 1;
					continue;
				case 1:
					goto IL_6F;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6F;
				default:
					if (false)
					{
					}
					if (this.ᜀ != null)
					{
						goto IL_71;
					}
					num = 0;
					break;
				}
			}
			IL_6F:
			IL_71:
			this.ᜀ.Add(shapeId, textBox);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x0040E9A4 File Offset: 0x0040D9A4
		public TextBox GetTextBox(int shapeId)
		{
			TextBox result;
			for (;;)
			{
				result = null;
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_26;
						default:
							if (false)
							{
							}
							result = this.ᜀ[shapeId];
							this.ᜀ.Remove(shapeId);
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_26;
					case 2:
						return result;
					}
					break;
					IL_26:
					if (!this.ᜀ.ContainsKey(shapeId))
					{
						return result;
					}
					num = 0;
				}
			}
			return result;
		}

		// Token: 0x040036A1 RID: 13985
		private int \u25D9\u0092\u00A9\u0080;

		// Token: 0x040036A2 RID: 13986
		private float \u2609\u0096\u0095\u0095;

		// Token: 0x040036A3 RID: 13987
		private bool \u25D9\u0084\u00A4\u009C;

		// Token: 0x040036A4 RID: 13988
		private Dictionary<int, TextBox> ᜀ = new Dictionary<int, TextBox>();
	}
}

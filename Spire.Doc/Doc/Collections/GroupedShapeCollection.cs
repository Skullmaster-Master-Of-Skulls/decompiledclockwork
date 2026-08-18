using System;
using System.Collections.Generic;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200052D RID: 1325
	[CLSCompliant(false)]
	public class GroupedShapeCollection
	{
		// Token: 0x06004559 RID: 17753 RVA: 0x00407C94 File Offset: 0x00406C94
		public GroupedShapeCollection(Document doc)
		{
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00407CB4 File Offset: 0x00406CB4
		public void AddDocObject(int shapeId, IDocumentObject shapeGroup)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜀ = new Dictionary<int, IDocumentObject>();
						num = 0;
						continue;
					}
					break;
				}
				IL_26:
				if (true)
				{
				}
				if (this.ᜀ == null)
				{
					num = 2;
					continue;
				}
				break;
				goto IL_26;
			}
			IL_6F:
			this.ᜀ.Add(shapeId, shapeGroup);
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00407D40 File Offset: 0x00406D40
		public IDocumentObject GetDocObject(int shapeId)
		{
			IDocumentObject result;
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
						goto IL_6C;
					case 1:
						if (this.ᜀ.ContainsKey(shapeId))
						{
							num = 2;
							continue;
						}
						goto IL_6C;
					case 2:
						result = this.ᜀ[shapeId];
						this.ᜀ.Remove(shapeId);
						num = 0;
						continue;
					}
					break;
					IL_6C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_82;
					}
				}
			}
			IL_82:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x0400365A RID: 13914
		private int \u25D8\u00AC\u0098\u008A;

		// Token: 0x0400365B RID: 13915
		private bool[] \u2593\u008D\u00A7\u00AB;

		// Token: 0x0400365C RID: 13916
		private string[] \u2593\u00A1\u0095\u0095;

		// Token: 0x0400365D RID: 13917
		private Dictionary<int, IDocumentObject> ᜀ = new Dictionary<int, IDocumentObject>();
	}
}
